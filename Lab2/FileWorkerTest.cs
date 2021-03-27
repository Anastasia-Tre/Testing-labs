using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IIG.Core.FileWorkingUtils;

namespace Lab2 {
    [TestClass]
    public class FileWorkerTest {

        private FileWorker _fileWorker;

        [TestInitialize]
        //[TestMethod]
        public void Test_Constructors() {
            try {
                var path1 = "C:/test.txt";
                var path2 = "C:/test/test.txt";
                var path3 = 1;
                var path4 = "./test.txt";

                var fileWorker1 = new FileWorker(path1);
                var fileWorker11 = new FileWorker("C:/test.txt");
                
                var fileWorker2 = new FileWorker(path2);

                var fileWorker3 = new FileWorker(path3.ToString());

                Assert.IsNotNull(fileWorker1, "");
                Assert.IsNotNull(fileWorker2, "");
                Assert.IsNotNull(fileWorker3, "");

                _fileWorker = fileWorker1;

                //fileWorker1.FilePath
                Assert.AreEqual(fileWorker1.FilePath, fileWorker11.FilePath,
                    "Object with the same parameters are not equal");
                Assert.AreNotEqual(fileWorker1.FilePath, fileWorker2.FilePath,
                    "Object with the different parameters are equal");

                
            } catch (Exception ex) {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void Constructor_EqualityCheck() {

        }

        [TestMethod]
        public void Test_Exception_EmptyString_Construstor() {
            Assert.ThrowsException<ArgumentException>(() => new FileWorker(""));
            Assert.ThrowsException<ArgumentException>(() => new FileWorker("     "));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_Exception_Null_Construstor() {
            new FileWorker(null);
        }
        
        [TestMethod]
        public void Test_Exception_InvalidSymbols_Construstor() {
            Assert.ThrowsException<ArgumentException>(() => new FileWorker(":"), "InvalidSymbols(:) in path");
            Assert.ThrowsException<ArgumentException>(() => new FileWorker("<"), "InvalidSymbols(<) in path");
            Assert.ThrowsException<ArgumentException>(() => new FileWorker(">"), "InvalidSymbols(>) in path");
            Assert.ThrowsException<ArgumentException>(() => new FileWorker("\""), "InvalidSymbols(\") in path");
            Assert.ThrowsException<ArgumentException>(() => new FileWorker("/"), "InvalidSymbols(/) in path");
            Assert.ThrowsException<ArgumentException>(() => new FileWorker("\\"), "InvalidSymbols(\\) in path");
            Assert.ThrowsException<ArgumentException>(() => new FileWorker("|"), "InvalidSymbols(|) in path");
            Assert.ThrowsException<ArgumentException>(() => new FileWorker("*"), "InvalidSymbols(*) in path");
            Assert.ThrowsException<ArgumentException>(() => new FileWorker("?"), "InvalidSymbols(?) in path");
        }

        [TestMethod]
        public void Test_GetFileName_ValidName() {
            var fileWorker = new FileWorker("C:/test.txt");
            var expected = "test.txt";
            var actual = fileWorker.GetFileName();
            Assert.IsNotNull(actual, "GetFileName error");
            Assert.AreEqual(expected, actual, "GetFileName error: valid name");
        }

        [TestMethod]
        public void Test_GetFileName_NoName() {
            var fileWorker = new FileWorker("./");
            var expected = "";
            var actual = fileWorker.GetFileName();
            Assert.IsNotNull(actual, "GetFileName error");
            Assert.AreEqual(expected, actual, "GetFileName error: no name");
        }

        [TestMethod]
        public void Test_GetFullPath_ValidPath() {
            var fileWorker = new FileWorker("C:\\test.txt");
            var expected = "C:\\test.txt";
            var actual = fileWorker.GetFullPath();
            Assert.IsNotNull(actual, "GetFullPath error");
            Assert.AreEqual(expected, actual, "GetFullPath of file error: valid path");
        }

        [TestMethod]
        public void Test_GetFullPath_CurrentDirectory() {
            var fileWorker1 = new FileWorker("./test.txt");
            var actual1 = fileWorker1.GetFullPath();
            Assert.IsNotNull(actual1, "GetFullPath of file error: current directory");

            var fileWorker2 = new FileWorker("test.txt");
            var actual2 = fileWorker2.GetFullPath();
            Assert.IsNotNull(actual2, "GetFullPath of file error: current directory");
        }

        [TestMethod]
        public void Test_GetFullPath_ParentDirectory() {
            var fileWorker = new FileWorker("../test.txt");
            var actual = fileWorker.GetFullPath();
            Assert.IsNotNull(actual, "GetFullPath of file error: parent directory");
        }

        [TestMethod]
        public void Test_GetFullPath_RootDirectory() {
            var fileWorker = new FileWorker("/test.txt");
            //var expected = "";
            var actual = fileWorker.GetFullPath();
            //Assert.AreEqual(expected, actual, "GetFullPath error: root directory");
            Assert.IsNotNull(actual, "GetFullPath of file error: root directory");
        }

        [TestMethod]
        public void Test_TryWrite_DefaultTries() {
            var fileWorker = new FileWorker("C:/test.txt");
            var text = "test";
            var result = fileWorker.TryWrite(text);
            Assert.IsNotNull(result, "TryWrite error");
            Assert.IsTrue(result, "TryWrite in file error: default number of tries");
        }

        [TestMethod]
        public void Test_TryWrite_CustomTries() {
            var fileWorker = new FileWorker("C:/test.txt");
            var text = "1\n2\n3\n4";
            var result = fileWorker.TryWrite(text, 10);
            Assert.IsNotNull(result, "TryWrite error");
            Assert.IsTrue(result, "TryWrite in file error: custom number of tries");
        }

        [TestMethod]
        public void Test_TryWrite_InvalidTries() {
            var fileWorker = new FileWorker("C:/test.txt");
            var text = "test";
            var result = fileWorker.TryWrite(text, -1);
            Assert.IsNotNull(result, "TryWrite error");
            Assert.IsFalse(result, "TryWrite in file error: invalid number of tries");
        }

        [TestMethod]
        public void Test_TryWrite_Null() {
            var fileWorker = new FileWorker("C:/test.txt");
            var result = fileWorker.TryWrite(null);
            Assert.IsNotNull(result, "TryWrite error");
            Assert.IsTrue(result, "TryWrite in file error: null string");
        }

        [TestMethod]
        public void Test_Write() {
            var fileWorker = new FileWorker("C:/test.txt");
            var text = "test";
            var result = fileWorker.Write(text);
            Assert.IsNotNull(result, "Write error");
            Assert.IsTrue(result, "Write in file error");
        }

        [TestMethod]
        public void Test_Write_EmptyString() {
            var fileWorker = new FileWorker("C:/test.txt");
            var text = string.Empty;
            var result = fileWorker.Write(text);
            Assert.IsNotNull(result, "Write error");
            Assert.IsTrue(result, "Write in file error: empty string");
        }

        [TestMethod]
        public void Test_Write_Null() {
            var fileWorker = new FileWorker("C:/test.txt");
            var result = fileWorker.Write(null);
            Assert.IsNotNull(result, "Write error");
            Assert.IsTrue(result, "Write in file error: null");
        }

        [TestMethod]
        public void Test_Write_Case1() {
            var fileWorker = new FileWorker("C:/test1.txt");
            var text = string.Empty;
            var result = fileWorker.Write(text);
            Assert.IsNotNull(result, "Write error");
            Assert.IsTrue(result, "Write in file error: empty string");
        }

        [TestMethod]
        public void Test_ReadAll() {
            var fileWorker = new FileWorker("C:/test.txt");
            fileWorker.Write("test");
            var expected = "test";
            var actual = fileWorker.ReadAll();
            Assert.IsNotNull(actual, "ReadAll error");
            Assert.AreEqual(expected, actual, "ReadAll error: result are not equal");
        }

        [TestMethod]
        public void Test_ReadAll_EmptyFile() {
            var fileWorker = new FileWorker("C:/test.txt");
            fileWorker.Write(string.Empty);
            var expected = "";
            var actual = fileWorker.ReadAll();
            Assert.IsNotNull(actual, "ReadAll error");
            Assert.AreEqual(expected, actual, "ReadAll error: empty file");
        }

        [TestMethod]
        public void Test_ReadAll_Null() {
            var fileWorker = new FileWorker("C:/test.txt");
            fileWorker.Write(null);
            var expected = "";
            var actual = fileWorker.ReadAll();
            Assert.IsNotNull(actual, "ReadAll error");
            Assert.AreEqual(expected, actual, "ReadAll error: null");
        }

        [TestMethod]
        public void Test_ReadAll_NonexistentFile() {
            var fileWorker = new FileWorker("C:/tset2.txt");
            var actual = fileWorker.ReadAll();
            Assert.IsNull(actual, "ReadAll error: nonexistent file return not null");
        }

        [TestMethod]
        public void Test_ReadLines() {
            var fileWorker = new FileWorker("C:/test.txt");
            fileWorker.Write("test\ntest\ntest");
            string[] expected = { "test", "test", "test" };
            var actual = fileWorker.ReadLines();
            Assert.IsNotNull(actual, "ReadLines error");
            Assert.AreEqual(expected.ToString(), actual.ToString(), "ReadLines error: result are not equal");
        }



        [TestMethod]
        public void Method() {
            
            //var fileWorker3 = new FileWorker("+=[]:;«,./?");
            // Недопустимі символи :<>?"    \/|*
            var fileWorker3 = new FileWorker("C:/test.txt");
            
            var name = fileWorker3.GetFileName();
            var path = fileWorker3.GetFullPath();
            //fileWorker3.
            Assert.IsNotNull(name);
            Assert.AreEqual("test.txt", name);
            //Assert.AreEqual("test.txt", path);

            //fileWorker3.Write("Hello");
            fileWorker3.ReadLines();

        }





    }
}
