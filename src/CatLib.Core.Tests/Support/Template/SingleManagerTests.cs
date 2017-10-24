﻿/*
 * This file is part of the CatLib package.
 *
 * (c) Yu Bin <support@catlib.io>
 *
 * For the full copyright and license information, please view the LICENSE
 * file that was distributed with this source code.
 * 
 * Document: http://catlib.io/
 */

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CatLib.Tests.Stl
{
    [TestClass]
    public class SingleManagerTests
    {
        public interface ITestInterface
        {
            string Call();
        }

        public class InterfaceImpl : ITestInterface
        {
            public string Call()
            {
                return "InterfaceImpl";
            }
        }

        public class TestManager : SingleManager<ITestInterface>
        {

        }

        [TestMethod]
        public void TestSingleGet()
        {
            var manager = new TestManager();
            manager.Extend(() => new InterfaceImpl());
            Assert.AreSame(manager.Default, manager.Get());
        }

        [TestMethod]
        public void TestCoverToManagerGet()
        {
            var manager = new TestManager();
            manager.Extend(() => new InterfaceImpl());
            var manager2 = manager as Manager<ITestInterface>;
            Assert.AreNotSame(manager.Default, manager2[null]);
        }

        [TestMethod]
        public void TestCoverToSingleManagerGet()
        {
            var manager = new TestManager();
            manager.Extend(() => new InterfaceImpl());
            var manager2 = manager as SingleManager<ITestInterface>;
            Assert.AreSame(manager.Default, manager2.Get());
        }

        [TestMethod]
        public void TestCoverToInterfaceManagerGet()
        {
            var manager = new TestManager();
            manager.Extend(() => new InterfaceImpl());
            var manager2 = manager as IManager<ITestInterface>;
            Assert.AreSame(manager.Default, manager2.Get());
        }

        [TestMethod]
        public void TestCoverToInterfaceSingleManagerGet()
        {
            var manager = new TestManager();
            manager.Extend(() => new InterfaceImpl());
            var manager2 = manager as ISingleManager<ITestInterface>;
            Assert.AreSame(manager.Default, manager2.Get());
        }

        [TestMethod]
        public void TestManagerRelease()
        {
            var manager = new TestManager();
            manager.Extend(() => new InterfaceImpl());
            var manager2 = manager as ISingleManager<ITestInterface>;
            var def = manager.Default;
            manager.Release();

            Assert.AreNotSame(def, manager2.Default);
        }

        [TestMethod]
        public void TestContainsExtend()
        {
            var manager = new TestManager();
            manager.Extend(() => new InterfaceImpl() , "hello");

            Assert.AreEqual(false, manager.ContainsExtend());
            Assert.AreEqual(true, manager.ContainsExtend("hello"));
        }

        [TestMethod]
        public void TestReleaseExtend()
        {
            var manager = new TestManager();
            manager.Extend(() => new InterfaceImpl(), "hello");

            Assert.AreEqual(true, manager.ContainsExtend("hello"));
            manager.ReleaseExtend("hello");
            Assert.AreEqual(false, manager.ContainsExtend());
            Assert.AreEqual(false, manager.ContainsExtend("hello"));
        }
    }
}
