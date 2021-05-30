using Microsoft.VisualStudio.TestTools.UnitTesting;
using IIG.Core.FileWorkingUtils;
using System.Text;
using System.Data;

namespace Lab4 {
    [TestClass]
    public class IntegrationTestFileWorker {
        private static string _longName;
        private static string _bigString;
        [ClassInitialize()]
        public static void ClassInit(TestContext context) {
            _longName = "A";
            _bigString = "A";
            for (int i = 0; i < 254; i++) {
                _longName += "A";
            }
            for (int i = 0; i < 25; i++) {
                _bigString += _bigString;
            }
        }

        private const string Server = @"WIN-6QL49633JP9\SQLNASA";
        private const string Database = @"IIG.CoSWE.StorageDB";
        private const bool IsTrusted = false;
        private const string Login = @"sa";
        private const string Password = @"sqlnasa";
        private const int ConnectionTimeout = 75;
        private StorageDatabaseUtils _databaseConnection;

        [TestInitialize]
        public void TestInitialize() {
            _databaseConnection =
                    new StorageDatabaseUtils(Server, Database, IsTrusted, Login, Password, ConnectionTimeout);
            Assert.IsNotNull(_databaseConnection);
        }

        [TestMethod]
        public void Test_Add_EmptyFile() {
            var fileWorker = new FileWorker("../../../test/test1.txt");
            string content = fileWorker.ReadAll();
            byte[] data = Encoding.ASCII.GetBytes(content);
            var result =_databaseConnection.AddFile(fileWorker.GetFileName(), data);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_Add_SameFile() {
            var fileWorker = new FileWorker("../../../test/test1.txt");
            string content = fileWorker.ReadAll();
            byte[] data = Encoding.ASCII.GetBytes(content);
            var result = _databaseConnection.AddFile(fileWorker.GetFileName(), data);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_Add_LongFile() {
            var fileWorker = new FileWorker("../../../test/test2.txt");
            fileWorker.Write(_bigString);
            string content = fileWorker.ReadAll();
            byte[] data = Encoding.ASCII.GetBytes(content);
            var result = _databaseConnection.AddFile(fileWorker.GetFileName(), data);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_Add_LongNameFile() {
            var fileWorker = new FileWorker("../../../test/"+_longName);
            fileWorker.Write("test");
            string content = fileWorker.ReadAll();
            byte[] data = Encoding.ASCII.GetBytes(content);
            var result = _databaseConnection.AddFile(fileWorker.GetFileName(), data);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_Add_NonexistentFile() {
            var fileWorker = new FileWorker("../../../test/nottest.txt");
            string content = fileWorker.ReadAll();
            bool result;
            if (content == null) result = _databaseConnection.AddFile(fileWorker.GetFileName(), null);
            else {
                byte[] data = Encoding.ASCII.GetBytes(content);
                result = _databaseConnection.AddFile(fileWorker.GetFileName(), data);
            } 
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_Add_NoNameFile() {
            var fileWorker = new FileWorker("../../../test/");
            string content = fileWorker.ReadAll();
            bool result;
            if (content == null) result = _databaseConnection.AddFile(fileWorker.GetFileName(), null);
            else {
                byte[] data = Encoding.ASCII.GetBytes(content);
                result = _databaseConnection.AddFile(fileWorker.GetFileName(), data);
            }
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_Add_NoTextFile() {
            var fileWorker = new FileWorker("../../../test/test3.txt");
            string content = fileWorker.ReadAll();
            bool result;
            if (content == null) result = _databaseConnection.AddFile(fileWorker.GetFileName(), null);
            else {
                byte[] data = Encoding.ASCII.GetBytes(content);
                result = _databaseConnection.AddFile(fileWorker.GetFileName(), data);
            }
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_Add_CyrillicNameFile() {
            var fileWorker = new FileWorker("../../../test/тест.txt");
            string content = fileWorker.ReadAll();
            byte[] data = Encoding.ASCII.GetBytes(content);
            var result = _databaseConnection.AddFile(fileWorker.GetFileName(), data);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_GetFile() {
            string fileName;
            byte[] fileContent;
            int id = (int)_databaseConnection.GetFiles().Rows[0][0];
            var result = _databaseConnection.GetFile(id, out fileName, out fileContent);
            var fileWorker = new FileWorker("../../../test/" + fileName);
            string content = fileWorker.ReadAll();
            string data = Encoding.ASCII.GetString(fileContent);
            Assert.IsTrue(result);
            Assert.AreEqual(content, data);
        }

        [TestMethod]
        public void Test_GetFiles() {
            DataTable StorageDB = _databaseConnection.GetFiles();
            foreach (DataRow row in StorageDB.Rows) {
                var fileName = row[1];
                var fileContent = row[2];
                var fileWorker = new FileWorker("../../../test/" + fileName);
                string content = fileWorker.ReadAll();
                string data = Encoding.ASCII.GetString((byte[])fileContent);
                Assert.AreEqual(data, content);
            }
         
        }
    }


}
