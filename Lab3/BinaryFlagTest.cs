using Microsoft.VisualStudio.TestTools.UnitTesting;
using IIG.BinaryFlag;
using System;

namespace Lab3 {
    [TestClass]
    public class BinaryFlagTest {

        // Execution Routes
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ExecutionRoute_0_1_2_11_LengthLessMin_Exception() {
            new MultipleBinaryFlag(1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ExecutionRoute_0_1_2_11_LengthBiggerMax_Exception() {
            new MultipleBinaryFlag(17179868705);
        }

        // UIntConcreteBinaryFlag
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ExecutionRoute_0_1_3_6_7_11_InitialValueTrue_Exception() {
            var flag = new MultipleBinaryFlag(32, true);
            flag.SetFlag(33);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ExecutionRoute_0_1_3_6_8_11_InitialValueTrue_Exception() {
            var flag = new MultipleBinaryFlag(3, true);
            flag.SetFlag(2);
            flag.ResetFlag(4);
        }

        [TestMethod]
        public void ExecutionRoute_0_1_3_6_9_11_InitialValueTrue() {
            var flag = new MultipleBinaryFlag(3, true);
            flag.SetFlag(2);
            flag.ResetFlag(1);
            var result = flag.GetFlag();
            Assert.IsFalse((bool)result);
        }

        [TestMethod]
        public void ExecutionRoute_0_1_3_6_10_11_InitialValueTrue() {
            var flag = new MultipleBinaryFlag(3, true);
            flag.ResetFlag(2);
            flag.SetFlag(2);
            var result = flag.GetFlag();
            Assert.IsTrue((bool)result);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ExecutionRoute_0_1_3_6_7_11_InitialValueFalse_Exception() {
            var flag = new MultipleBinaryFlag(32, false);
            flag.SetFlag(33);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ExecutionRoute_0_1_3_6_8_11_InitialValueFalse_Exception() {
            var flag = new MultipleBinaryFlag(3, false);
            flag.SetFlag(2);
            flag.ResetFlag(4);
        }

        [TestMethod]
        public void ExecutionRoute_0_1_3_6_9_11_InitialValueFalse() {
            var flag = new MultipleBinaryFlag(3, false);
            flag.SetFlag(2);
            flag.ResetFlag(1);
            var result = flag.GetFlag();
            Assert.IsFalse((bool)result);
        }

        [TestMethod]
        public void ExecutionRoute_0_1_3_6_10_11_InitialValueFalse() {
            var flag = new MultipleBinaryFlag(3, false);
            flag.SetFlag(2);
            flag.ResetFlag(2);
            var result = flag.GetFlag();
            Assert.IsFalse((bool)result);
        }

        // ULongConcreteBinaryFlag
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ExecutionRoute_0_1_4_6_7_11_InitialValueTrue_Exception() {
            var flag = new MultipleBinaryFlag(33, true);
            flag.SetFlag(64);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ExecutionRoute_0_1_4_6_8_11_InitialValueTrue_Exception() {
            var flag = new MultipleBinaryFlag(63, true);
            flag.SetFlag(32);
            flag.ResetFlag(76);
        }

        [TestMethod]
        public void ExecutionRoute_0_1_4_6_9_11_InitialValueTrue() {
            var flag = new MultipleBinaryFlag(63, true);
            flag.SetFlag(32);
            flag.ResetFlag(31);
            var result = flag.GetFlag();
            Assert.IsFalse((bool)result);
        }

        [TestMethod]
        public void ExecutionRoute_0_1_4_6_10_11_InitialValueTrue() {
            var flag = new MultipleBinaryFlag(63, true);
            flag.ResetFlag(32);
            flag.SetFlag(32);
            var result = flag.GetFlag();
            Assert.IsTrue((bool)result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ExecutionRoute_0_1_4_6_7_11_InitialValueFalse_Exception() {
            var flag = new MultipleBinaryFlag(33, false);
            flag.SetFlag(64);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ExecutionRoute_0_1_4_6_8_11_InitialValueFalse_Exception() {
            var flag = new MultipleBinaryFlag(63, false);
            flag.SetFlag(32);
            flag.ResetFlag(76);
        }

        [TestMethod]
        public void ExecutionRoute_0_1_4_6_9_11_InitialValueFalse() {
            var flag = new MultipleBinaryFlag(63, false);
            flag.SetFlag(32);
            flag.ResetFlag(31);
            var result = flag.GetFlag();
            Assert.IsFalse((bool)result);
        }

        [TestMethod]
        public void ExecutionRoute_0_1_4_6_10_11_InitialValueFalse() {
            var flag = new MultipleBinaryFlag(63, false);
            flag.ResetFlag(32);
            flag.SetFlag(32);
            var result = flag.GetFlag();
            Assert.IsFalse((bool)result);
        }


        // UIntArrayConcreteBinaryFlag
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ExecutionRoute_0_1_5_6_7_11_InitialValueTrue_Exception() {
            var flag = new MultipleBinaryFlag(64, true);
            flag.SetFlag(78);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ExecutionRoute_0_1_5_6_8_11_InitialValueTrue_Exception() {
            var flag = new MultipleBinaryFlag(64, true);
            flag.SetFlag(32);
            flag.ResetFlag(100);
        }

        [TestMethod]
        public void ExecutionRoute_0_1_5_6_9_11_InitialValueTrue() {
            var flag = new MultipleBinaryFlag(64, true);
            flag.SetFlag(32);
            flag.ResetFlag(31);
            var result = flag.GetFlag();
            Assert.IsFalse((bool)result);
        }

        [TestMethod]
        public void ExecutionRoute_0_1_5_6_10_11_InitialValueTrue() {
            var flag = new MultipleBinaryFlag(124, true);
            flag.ResetFlag(32);
            flag.SetFlag(32);
            var result = flag.GetFlag();
            Assert.IsTrue((bool)result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ExecutionRoute_0_1_5_6_7_11_InitialValueFalse_Exception() {
            var flag = new MultipleBinaryFlag(64, false);
            flag.SetFlag(78);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ExecutionRoute_0_1_5_6_8_11_InitialValueFalse_Exception() {
            var flag = new MultipleBinaryFlag(64, false);
            flag.SetFlag(32);
            flag.ResetFlag(100);
        }

        [TestMethod]
        public void ExecutionRoute_0_1_5_6_9_11_InitialValueFalse() {
            var flag = new MultipleBinaryFlag(64, false);
            flag.SetFlag(32);
            flag.ResetFlag(31);
            var result = flag.GetFlag();
            Assert.IsFalse((bool)result);
        }

        [TestMethod]
        public void ExecutionRoute_0_1_5_6_10_11_InitialValueFalse() {
            var flag = new MultipleBinaryFlag(124, false);
            flag.ResetFlag(32);
            flag.SetFlag(32);
            var result = flag.GetFlag();
            Assert.IsFalse((bool)result);
        }

        // Other Tests
        [TestMethod]
        public void Test_ToString_InitialValueTrue() {
            var flag = new MultipleBinaryFlag(4);
            flag.ResetFlag(3);
            var expected = "TTTF";
            var actual = flag.ToString();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_ToString_InitialValueFalse() {
            var flag = new MultipleBinaryFlag(4, false);
            flag.SetFlag(3);
            var expected = "FFFT";
            var actual = flag.ToString();
            Assert.AreEqual(expected, actual);
        }



    }
}
