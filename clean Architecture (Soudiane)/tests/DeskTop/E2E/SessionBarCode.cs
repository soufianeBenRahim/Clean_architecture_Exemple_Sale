using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;

namespace WPF_POS_Test
{
    [TestClass]
    public  class SessionBarCode : WPFPOSSession
    {
        [TestMethod]
        public void EditorEnterText()
        {
            Thread.Sleep(TimeSpan.FromSeconds(6));
        }


        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            Setup(context);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            TearDown();
        }
    }
}
