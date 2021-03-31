using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IIG.Core.FileWorkingUtils;

namespace Lab2 {
    [TestClass]
    public class FileWorkerTest {

        private string _file;
        private string _text;
        private static string _bigString;
        private static string _bigStringLines;
        private static string _longName;

        [ClassInitialize()]
        public static void ClassInit(TestContext context) { 
            _bigString = "F";
            _bigStringLines = "F\n";
            _longName = "A";
            for (int i = 0; i < 29; i++) {
                _bigString += _bigString;
                if(i < 25) _bigStringLines += _bigStringLines;
            }
            for (int i = 0; i < 254; i++) {
                _longName += "A";
            }
        }

        [TestInitialize]
        public void Initialization() {
            _file = "./test.txt";
            _text = "Some text to test";
        }

        [TestMethod]
        public void Test_Constructor_Exception_EmptyString() {
            Assert.ThrowsException<ArgumentException>(() => new FileWorker(""), "Constructor error: empty string");
            Assert.ThrowsException<ArgumentException>(() => new FileWorker("     "), "Constructor error: only spaces");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_Constructor_Exception_Null() {
            new FileWorker(null);
        }
        
        [TestMethod]
        public void Test_Constructor_Exception_InvalidSymbols() {
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
        public void Test_Constructor_NonexistentFile() {
            var fileWorker = new FileWorker("./nottest.txt");
            Assert.IsNotNull(fileWorker, "Constructor error: nonexisten file");
        }

        [TestMethod]
        public void Test_Constructor_NonexistentPath() {
            Assert.ThrowsException<ArgumentException>(() => new FileWorker("./nottest/nottest.txt"),
                "Constructor error: nonexistent folder");
           
        }

        // �����,���� � ������� ������� �� ������, � �� ������ � ������ --trywrite, write


        [TestMethod]
        public void Test_GetFileName_ValidName() {
            var fileWorker = new FileWorker("./test.txt");
            var expected = "test.txt";
            var actual = fileWorker.GetFileName();
            Assert.IsNotNull(actual, "GetFileName error");
            Assert.AreEqual(expected, actual, "GetFileName error: valid name");
        }

        // ������ ������ ������ ��� ��� ����� ������
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
            Assert.IsNotNull(actual1, "GetFullPath of file error: current directory(with './')");

            var fileWorker2 = new FileWorker("test.txt");
            var actual2 = fileWorker2.GetFullPath();
            Assert.IsNotNull(actual2, "GetFullPath of file error: current directory(without './')");
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
            var actual = fileWorker.GetFullPath();
            Assert.IsNotNull(actual, "GetFullPath of file error: root directory");
        }
        // "../../test/test.txt"

        // TryWrite ����� �������������


        [TestMethod]
        public void Test_TryWrite_DefaultTries() {
            var fileWorker = new FileWorker(_file);
            var result = fileWorker.TryWrite(_text);
            Assert.IsNotNull(result, "TryWrite error");
            Assert.IsTrue(result, "TryWrite in file error: default number of tries");
        }

        [TestMethod]
        public void Test_TryWrite_CustomTries() {
            var fileWorker = new FileWorker(_file);
            var result = fileWorker.TryWrite(_text, 100);
            Assert.IsNotNull(result, "TryWrite error");
            Assert.IsTrue(result, "TryWrite in file error: custom number of tries");
        }

        [TestMethod]
        public void Test_TryWrite_MaxTries() {
            var fileWorker = new FileWorker(_file);
            var result = fileWorker.TryWrite(_text, int.MaxValue);
            Assert.IsNotNull(result, "TryWrite error");
            Assert.IsTrue(result, "TryWrite in file error: max number of tries");
        }

        // ������� �������� >0

        [TestMethod]
        public void Test_TryWrite_Valid_OnBorder() {
            var fileWorker = new FileWorker(_file);
            var result = fileWorker.TryWrite(_text, int.MaxValue);
            Assert.IsNotNull(result, "TryWrite error");
            Assert.IsTrue(result, "TryWrite in file error: max number of tries");
        }

        // out of range ������ �� �� ����������� ���� int
        [TestMethod]
        public void Test_TryWrite_Valid_InRange_Case1() {
            var fileWorker = new FileWorker(_file);
            var result = fileWorker.TryWrite(_text, int.MaxValue - 1);
            Assert.IsNotNull(result, "TryWrite error");
            Assert.IsTrue(result, "TryWrite in file error: max-1 number of tries");
        }

        [TestMethod]
        public void Test_TryWrite_Valid_InRange_Case2() {
            var fileWorker = new FileWorker(_file);
            var result = fileWorker.TryWrite(_text, 2);
            Assert.IsNotNull(result, "TryWrite error");
            Assert.IsTrue(result, "TryWrite in file error: min+1 number of tries");
        }

        [TestMethod]
        public void Test_TryWrite_Invalid_OutOfRange_Case1() {
            var fileWorker = new FileWorker(_file);
            var result = fileWorker.TryWrite(_text, 0);
            Assert.IsNotNull(result, "TryWrite error");
            Assert.IsFalse(result, "TryWrite in file error: min-1 number of tries");
        }

        [TestMethod]
        public void Test_TryWrite_Invalid_OutOfRange_Case2() {
            var fileWorker = new FileWorker(_file);
            var result = fileWorker.TryWrite(_text, int.MinValue);
            Assert.IsNotNull(result, "TryWrite error");
            Assert.IsFalse(result, "TryWrite in file error: invalid number of tries");
        }

        [TestMethod]
        public void Test_TryWrite_Null() {
            var fileWorker = new FileWorker(_file);
            var result = fileWorker.TryWrite(null, 2);
            Assert.IsNotNull(result, "TryWrite error");
            Assert.IsTrue(result, "TryWrite in file error: null string");
        }

        [TestMethod]
        public void Test_TryWrite_EmptyString() {
            var fileWorker = new FileWorker(_file);
            var result = fileWorker.TryWrite("", 2);
            Assert.IsNotNull(result, "TryWrite error");
            Assert.IsTrue(result, "TryWrite in file error: empty string");
        }

        [TestMethod]
        public void Test_TryWrite_NonexistentFile() {
            var fileWorker = new FileWorker("./nottest.txt");
            var result = fileWorker.TryWrite(_text, 2);
            Assert.IsNotNull(result, "TryWrite error");
            Assert.IsTrue(result, "TryWrite in file error: nonexistent file");
        }

        [TestMethod]
        public void Test_TryWrite_NoNameFile() {
            var fileWorker = new FileWorker("./");
            var result = fileWorker.TryWrite(_text, 2);
            Assert.IsNotNull(result, "TryWrite error");
            Assert.IsFalse(result, "TryWrite in file error: no name file");
        }

        [TestMethod]
        public void Test_TryWrite_BigString() {
            var fileWorker = new FileWorker(_file);
            var result = fileWorker.TryWrite(_bigString, 10);
            Assert.IsNotNull(result, "TryWrite error");
            Assert.IsTrue(result, "TryWrite in file error: big string");
        }

        [TestMethod]
        public void Test_Write() {
            var fileWorker = new FileWorker(_file);
            var result = fileWorker.Write(_text);
            Assert.IsNotNull(result, "Write error");
            Assert.IsTrue(result, "Write in file error");
        }

        [TestMethod]
        public void Test_Write_EmptyString() {
            var fileWorker = new FileWorker(_file);
            var result = fileWorker.Write(string.Empty);
            Assert.IsNotNull(result, "Write error");
            Assert.IsTrue(result, "Write in file error: empty string");
        }

        [TestMethod]
        public void Test_Write_Null() {
            var fileWorker = new FileWorker(_file);
            var result = fileWorker.Write(null);
            Assert.IsNotNull(result, "Write error");
            Assert.IsTrue(result, "Write in file error: null");
        }

        [TestMethod]
        public void Test_Write_NonexistentFile() {
            var fileWorker = new FileWorker("./nottest.txt");
            var result = fileWorker.Write(_text);
            Assert.IsNotNull(result, "Write error");
            Assert.IsTrue(result, "Write in file error: nonexistent file");
        }

        [TestMethod]
        public void Test_Write_NoNameFile() {
            var fileWorker = new FileWorker("./");
            var result = fileWorker.Write(_text);
            Assert.IsNotNull(result, "Write error");
            Assert.IsFalse(result, "Write in file error: no name file");
        }

        [TestMethod]
        public void Test_Write_BigString() {
            var fileWorker = new FileWorker(_file);
            var result = fileWorker.Write(_bigString);
            Assert.IsNotNull(result, "Write error");
            Assert.IsTrue(result, "Write in file error: big string");
        }

        [TestMethod]
        public void Test_ReadAll() {
            var fileWorker = new FileWorker(_file);
            fileWorker.Write(_text);
            var expected = _text;
            var actual = fileWorker.ReadAll();
            Assert.IsNotNull(actual, "ReadAll error");
            Assert.AreEqual(expected, actual, "ReadAll error: result are not equal");
        }

        [TestMethod]
        public void Test_ReadAll_EmptyFile() {
            var fileWorker = new FileWorker(_file);
            fileWorker.Write(string.Empty);
            var expected = "";
            var actual = fileWorker.ReadAll();
            Assert.IsNotNull(actual, "ReadAll error");
            Assert.AreEqual(expected, actual, "ReadAll error: empty file");
        }

        [TestMethod]
        public void Test_ReadAll_Null() {
            var fileWorker = new FileWorker(_file);
            fileWorker.Write(null);
            var expected = "";
            var actual = fileWorker.ReadAll();
            Assert.IsNotNull(actual, "ReadAll error");
            Assert.AreEqual(expected, actual, "ReadAll error: null");
        }

        [TestMethod]
        public void Test_ReadAll_NonexistentFile() {
            var fileWorker = new FileWorker("C:/nottest.txt");
            var actual = fileWorker.ReadAll();
            Assert.IsNull(actual, "ReadAll error: nonexistent file return not null");
        }

        [TestMethod]
        public void Test_ReadAll_NoNameFile() {
            var fileWorker = new FileWorker("./");
            var actual = fileWorker.ReadAll();
            Assert.IsNull(actual, "ReadAll error: no name file return not null");
        }

        [TestMethod]
        public void Test_ReadAll_BigFile() {
            var fileWorker = new FileWorker(_file);
            fileWorker.Write(_bigString);
            var actual = fileWorker.ReadAll();
            Assert.IsNotNull(actual, "ReadAll error: big file");
        }

        [TestMethod]
        public void Test_ReadLines() {
            var fileWorker = new FileWorker(_file);
            fileWorker.Write("test\ntest\ntest");
            string[] expected = { "test", "test", "test" };
            var actual = fileWorker.ReadLines();
            Assert.IsNotNull(actual, "ReadLines error");
            Assert.AreEqual(expected.ToString(), actual.ToString(), "ReadLines error: result are not equal");
        }

        [TestMethod]
        public void Test_ReadLines_EmptyFile() {
            var fileWorker = new FileWorker(_file);
            fileWorker.Write("");
            string[] expected = { };
            var actual = fileWorker.ReadLines();
            Assert.IsNotNull(actual, "ReadLines error");
            Assert.AreEqual(expected.ToString(), actual.ToString(), "ReadLines error: result are not empty");
        }

        [TestMethod]
        public void Test_ReadLines_NonexistentFile() {
            var fileWorker = new FileWorker("C:/nottest.txt");
            var actual = fileWorker.ReadLines();
            Assert.IsNull(actual, "ReadLines error: nonexistent file return not null");
        }

        [TestMethod]
        public void Test_ReadLines_NoNameFile() {
            var fileWorker = new FileWorker("./");
            var actual = fileWorker.ReadLines();
            Assert.IsNull(actual, "ReadLines error: no name file return not null");
        }

        [TestMethod]
        public void Test_ReadLines_BigFile() {
            var fileWorker = new FileWorker(_file);
            fileWorker.Write(_bigStringLines);
            var actual = fileWorker.ReadLines();
            Assert.IsNotNull(actual, "ReadLines error: big file");
        }

        [TestMethod]
        public void Test_MkDir() {
            var result = FileWorker.MkDir("./test");
            Assert.IsNotNull(result, "MkDir error");
        }

        [TestMethod]
        public void Test_MkDir_NoNameFolder() {
            var result = FileWorker.MkDir("./");
            Assert.IsNotNull(result, "MkDir error: no name folder");
        }

        [TestMethod]
        public void Test_MkDir_EmptyString() {
            Assert.ThrowsException<ArgumentException>(() => FileWorker.MkDir(""), 
                "Mkdir error: empty string");
        }

        [TestMethod]
        public void Test_MkDir_InNonexistentFolder() {
            Assert.ThrowsException<ArgumentException>(() => FileWorker.MkDir("./tests/test"), 
                "Mkdir error: in nonexistent folder");
        }

        [TestMethod]
        public void Test_MkDir_LongName() {
            var result = FileWorker.MkDir(_longName);
            Assert.IsNotNull(result, "MkDir error: long name");
        }
        
        //???????????????????????????/////////
        [TestMethod]
        public void Test_MkDir_AlreadyExistentFolder() {
            FileWorker.MkDir("./test");
            var result = FileWorker.MkDir("./test");
            Assert.IsNotNull(result, "MkDir error: already existent folder");
        }

        [TestMethod]
        public void Test_IsPathValid() {
            var result = FileWorker.IsPathValid(_longName);
            Assert.IsTrue(result, "IsPathValid error:");
        }


        // max lenght of filename
        [TestMethod]
        public void Test() {
            //bool result = FileWorker.IsPathValid("./");
            //var result = FileWorker.MkDir("./Test");
            //var result = FileWorker.TryCopy(_file, "C:/test.txt", true);
            
            //Assert.AreEqual("", result);
        }

    }
}
