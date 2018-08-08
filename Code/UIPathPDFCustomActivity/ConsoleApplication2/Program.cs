using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities;
using UIPathPDFCustomActivity;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            UIPathPDFCustomActivity.ExtractionCustomActivity test = new ExtractionCustomActivity();
            test.ExecuteTest();
        }
    }
}
