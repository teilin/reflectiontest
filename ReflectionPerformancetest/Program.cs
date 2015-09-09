using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionPerformancetest
{
    public class Test
    {
        private string testString;

        public Test()
        {
            testString = "Hello World!";
        }

        public void SetString(string value)
        {
            testString = value;
        }

        public string StringProperty
        {
            get { return testString; }
        }
    }

    class Program
    {
        private const int Iterations = 2000000;

        static void Main(string[] args)
        {
            Stopwatch stopWatch = new Stopwatch();

            //Uten reflection
            stopWatch.Start();
            for (var i = 0; i < Iterations; i++)
            {
                Test test = new Test();
                test.SetString("Hello World 2");
            }
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            Console.WriteLine("Uten reflection tidspruk: " + elapsedTime);

            stopWatch.Reset();

            //Med reflection
            stopWatch.Start();
            for (var i=0; i<Iterations; i++)
            {
                Test test = new Test();
                Type testType = typeof (Test);
                FieldInfo testFieldInfo = testType.GetField("testString", BindingFlags.NonPublic | BindingFlags.Instance);
                testFieldInfo.SetValue(test, "Hello Wolrd 2!");
            }
            stopWatch.Stop();
            TimeSpan tsMed = stopWatch.Elapsed;
            string elapsedTimemed = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            tsMed.Hours, tsMed.Minutes, tsMed.Seconds,
            tsMed.Milliseconds / 10);
            Console.WriteLine("Med reflection tidspruk: " + elapsedTimemed);

            Console.Read();
        }
    }
}
