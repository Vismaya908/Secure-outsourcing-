using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace encryption
{
    class GlobalParameter
    {


        public int primeGen()
        {
            n = 1000000;
            nrThreads = 100;

            //add one because one main thread is needed to manage all the others
            ThreadStatuses = new ThreadStatus[nrThreads + 1];

            //1. Create a list of natural numbers 2, 3, 4, 5, ….., n.  None of which is marked.
            nums = new bool[n];
            //suppose all these are primes, we will then start marking them as non primes
            for (int i = 2; i < n; i++)
                nums[i] = true;

            //initialize k with 1, start looking for primes bigger than 1, 1 is not a prime
            k = 1;

            //make sure all threads are waiting so they don't run out and find primes on their
            //own without the main thread regulating them
            MakeWorkerThreadsWait();

            //this is the main thread
            var _mainThread = new System.Threading.Thread(() => FindSmallestPrimeAndBroadcast());
            _mainThread.IsBackground = true;
            _mainThread.Start();

            //worker threads
            for (int i = 1; i < nrThreads + 1; i++)
            {
                //need to declare this as a locol variable so that the value passed to the thread
                //won't be changed causing chaos
                var localI = i;
                var _workerThread = new System.Threading.Thread(() => MarkNonPrimes(localI));

                _workerThread.IsBackground = true;
                _workerThread.Start();
            }

            //wait for all threads to end
            while (!AllThreadsDead())
            //commenting the line above and decommenting the one below really help with debugging
            //because you don't constantly get sidetracked to AllThreadsDead() when pressing F5
            //while (true)
            {
                Thread.Sleep(1);
            }

            int qui = new Random().Next(2, 100000);
            List<int> diff = new List<int>();
            

            //print the primes you found
            for (int i = 0; i < n; i++)
            {
                
                if (nums[i])
                {
                    diff.Add(qui - i); 
                }
                if (i == qui)
                    break;
            }

            diff.Sort();

            return qui-diff[0];
        }

        #region PrimeComponentes
        enum ThreadStatus
        {
            DoingStuff = 0,
            Waiting = 1,
            Dead = 2
        };
        static ThreadStatus[] ThreadStatuses;
        static bool[] nums;
        static int k;
        static int n;
        static int nrThreads;
        static void MakeWorkerThreadsDoStuff()
        {
            for (int i = 1; i < nrThreads + 1; i++)
            {
                ThreadStatuses[i] = ThreadStatus.DoingStuff;
            }
        }
        static void MakeWorkerThreadsWait()
        {
            for (int i = 1; i < nrThreads + 1; i++)
            {
                ThreadStatuses[i] = ThreadStatus.Waiting;
            }
        }
        static void KillWorkerThreads()
        {
            for (int i = 1; i < nrThreads + 1; i++)
            {
                ThreadStatuses[i] = ThreadStatus.Dead;
            }
        }
        static bool WorkerThreadsDoingStuff()
        {
            for (int i = 1; i < nrThreads + 1; i++)
            {
                if (ThreadStatuses[i] == ThreadStatus.DoingStuff)
                    return true;
            }
            return false;
        }
        static bool WorkerThreadsWaiting()
        {
            for (int i = 1; i < nrThreads + 1; i++)
            {
                if (ThreadStatuses[i] == ThreadStatus.Waiting)
                    return true;
            }
            return false;
        }
        static bool MainThreadWaiting()
        {
            return (ThreadStatuses[0] == ThreadStatus.Waiting);
        }
        static bool AllThreadsDead()
        {
            foreach (var threadStatus in ThreadStatuses)
            {
                if (threadStatus != ThreadStatus.Dead)
                    return false;
            }

            return true;
        }

        static void MarkNonPrimes(int ThreadNr)
        {

            while (ThreadStatuses[ThreadNr] == ThreadStatus.Waiting)
            {
                Thread.Sleep(1);
            }

            if (ThreadStatuses[ThreadNr] == ThreadStatus.Dead)
                return;
            //stores how many numbers we have to decide are primes or non primes
            int nrOfNumbers = n - k * k;
            //divide that by the number of threads to get the interval within which each thread should
            //look for primes
            int interval = nrOfNumbers / nrThreads;

            //for this thread and this value of k start at this value
            int startNumber = k * k + interval * (ThreadNr - 1);
            //and end at this value
            int stopNumber = (ThreadNr == nrThreads) ? n : startNumber + interval;

            lock (nums)
            {
                for (int j = startNumber; j < stopNumber; j++)
                {
                    //Mark all multiples of k between k² and n.
                    if (j % k == 0)
                        nums[j] = false;
                }
            }

            //nothig to do now but wait
            ThreadStatuses[ThreadNr] = ThreadStatus.Waiting;

            MarkNonPrimes(ThreadNr);
        }

        /// <summary>
        /// this is the main thread's function it manages the worker therads and finds the smallest prime
        /// when the worker threads finish marking non primes
        /// </summary>
        static void FindSmallestPrimeAndBroadcast()
        {
            while (WorkerThreadsDoingStuff())
            {
                Thread.Sleep(1);
            }

            //c. Process 0 broadcasts k to rest of processes.
            for (int i = k + 1; i < n; i++)
            {
                if (nums[i])
                {

                    k = i;

                    break;
                }
            }
            //Until k² > n.
            if (k * k > n)
            {
                //kill worker threads
                KillWorkerThreads();
                //commit suicide
                ThreadStatuses[0] = ThreadStatus.Dead;
                return;
            }
            //all worker threads can now read the correct new value of k so they can all get to work
            MakeWorkerThreadsDoStuff();

            FindSmallestPrimeAndBroadcast();
        }
        #endregion
    }

}
