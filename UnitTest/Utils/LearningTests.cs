using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShauliBlog.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShauliBlog.Utils.Tests
{
    [TestClass()]
    public class LearningTests
    {
        [TestMethod()]
        public void MainTest()
        {
            Learning learning = new Learning();

            learning.Recommend(null);
        }
    }
}