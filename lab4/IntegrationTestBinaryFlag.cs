using Microsoft.VisualStudio.TestTools.UnitTesting;
using IIG.BinaryFlag;

namespace Lab4 {
    [TestClass]
    public class IntegrationTestBinaryFlag {
        private const string Server = @"WIN-6QL49633JP9\SQLNASA";
        private const string Database = @"IIG.CoSWE.FlagpoleDB";
        private const bool IsTrusted = false;
        private const string Login = @"sa";
        private const string Password = @"sqlnasa";
        private const int ConnectionTimeout = 75;
        private FlagpoleDatabaseUtils _databaseConnection;

        [TestInitialize]
        public void TestInitialize() {
            _databaseConnection =
                    new FlagpoleDatabaseUtils(Server, Database, IsTrusted, Login, Password, ConnectionTimeout);
            Assert.IsNotNull(_databaseConnection);
        }

        [TestMethod]
        public void Test_Add_MaxFlag_InitialValueTrue() {
            var flag = new MultipleBinaryFlag(171798689);
            var result = _databaseConnection.AddFlag(flag.ToString(), (bool)flag.GetFlag());
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_Add_MinFlag_InitialValueTrue() {
            var flag = new MultipleBinaryFlag(2);
            var result = _databaseConnection.AddFlag(flag.ToString(), (bool)flag.GetFlag());
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_Add_MaxFlag_InitialValueFalse() {
            var flag = new MultipleBinaryFlag(171798689, false);
            var result = _databaseConnection.AddFlag(flag.ToString(), (bool)flag.GetFlag());
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_Add_MinFlag_InitialValueFalse() {
            var flag = new MultipleBinaryFlag(2, false);
            var result = _databaseConnection.AddFlag(flag.ToString(), (bool)flag.GetFlag());
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_Add_SetFlag_InitialValueFalse() {
            var flag = new MultipleBinaryFlag(10, false);
            flag.SetFlag(5);
            var result = _databaseConnection.AddFlag(flag.ToString(), (bool)flag.GetFlag());
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_Add_ResetFlag_InitialValueTrue() {
            var flag = new MultipleBinaryFlag(5);
            flag.ResetFlag(4);
            var result = _databaseConnection.AddFlag(flag.ToString(), (bool)flag.GetFlag());
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_GetFlag() {
            var flag = new MultipleBinaryFlag(5, false);
            flag.SetFlag(3); flag.SetFlag(4);
            _databaseConnection.AddFlag(flag.ToString(), (bool)flag.GetFlag());
            int? id = _databaseConnection.GetIntBySql("SELECT TOP (1) [MultipleBinaryFlagID] FROM [IIG.CoSWE.FlagpoleDB].[dbo].[MultipleBinaryFlags] ORDER BY [MultipleBinaryFlagID] DESC");
            string actualFlagView;
            bool? actualFlagValue;
            var result = _databaseConnection.GetFlag((int)id, out actualFlagView, out actualFlagValue);
            Assert.IsTrue(result);
            Assert.AreEqual(actualFlagView, flag.ToString());
            Assert.AreEqual(actualFlagValue, flag.GetFlag());
        }

    }
}
