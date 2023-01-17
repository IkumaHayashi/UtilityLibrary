using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using UtilityLibrary;

namespace UtilityLibraryTest
{

    /// <summary>
    /// GetSettingsObject ÇÃäTóvÇÃê‡ñæ
    /// </summary>
    [TestClass]
    public class UtilityTestClass
    {
        public static readonly string NormalTestXMLFilePath = @".\NormalTestMessages.xml";
        public static readonly string ErrorNotExistsXMLFilePath = @"C:\Dummy.xml";

        [TestMethod]
        public void GetSettingsObject()
        {
            var MessageList = Utility.GetSettingsObject<DeserializeMessages>(NormalTestXMLFilePath);
            Assert.AreNotEqual(null, MessageList);

            bool isErrorThrown = false;
            DeserializeMessages errorDeserializeMessages = null;
            try
            {

                errorDeserializeMessages = Utility.GetSettingsObject<DeserializeMessages>(ErrorNotExistsXMLFilePath);
            }
            catch
            {
                isErrorThrown = true;
            }
            Assert.AreEqual(true, isErrorThrown);
            Assert.AreEqual(null, errorDeserializeMessages);
        }

        [TestMethod]
        public void IsValidFileName()
        {
            string testFileName = string.Empty;
            testFileName = ":.xlsx";
            Assert.AreEqual(true, FileUtility.IsValidFileName(testFileName));
        }




    }
}
