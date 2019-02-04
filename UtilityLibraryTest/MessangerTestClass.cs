using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml.Serialization;
using UtilityLibrary;


namespace UtilityLibraryTest
{
    [TestClass]
    public class MessangerTestClass
    {
        [TestMethod]
        public void isValidMessageType()
        {

            Messages messages = new Messages(UtilityTestClass.NormalTestXMLFilePath);

            PrivateObject privateObject = new PrivateObject(messages);

            Assert.AreEqual(true, privateObject.Invoke("isValidMessageType", UtilityLibrary.MessageType.Custom));
            Assert.AreEqual(true, privateObject.Invoke("isValidMessageType", UtilityLibrary.MessageType.Information));
            Assert.AreEqual(true, privateObject.Invoke("isValidMessageType", UtilityLibrary.MessageType.Error));
            Assert.AreEqual(true, privateObject.Invoke("isValidMessageType", UtilityLibrary.MessageType.Question));
            Assert.AreEqual(true, privateObject.Invoke("isValidMessageType", UtilityLibrary.MessageType.Warning));

            Assert.AreEqual(false, privateObject.Invoke("isValidMessageType", "X"));
        }

        [TestMethod]
        public void Constructor()
        {

            bool isThrownError = false;

            //メッセージタイプエラー
            const string ErrorInvalidTypeTestXMLFilePath = @".\ErrorInvalidTypeTestMessages.xml";
            try
            {
                Messages errorMessages1 = new Messages(ErrorInvalidTypeTestXMLFilePath);
            }
            catch
            {
                isThrownError = true;
            }
            Assert.AreEqual(true, isThrownError);

            //ID重複エラー
            const string ErrorDuplicaIDTestXMLFilePath = @".\ErrorDuplicaIDTestMessages.xml";
            isThrownError = false;
            try
            {
                Messages errorMessages2 = new Messages(ErrorDuplicaIDTestXMLFilePath);
            }
            catch
            {
                isThrownError = true;
            }
            Assert.AreEqual(true, isThrownError);

            //正常系

            Messages messages = new Messages(UtilityTestClass.NormalTestXMLFilePath);
            Assert.AreNotEqual(null, messages);
            Assert.AreEqual("0001", messages[0].ID); Assert.AreEqual("I", messages[0].Type); Assert.AreEqual("情報", messages[0].Caption); Assert.AreEqual("情報メッセージ", messages[0].Text);
            Assert.AreEqual("0002", messages[1].ID); Assert.AreEqual("W", messages[1].Type); Assert.AreEqual("警告", messages[1].Caption); Assert.AreEqual("情報メッセージ", messages[1].Text);
            Assert.AreEqual("0003", messages[2].ID); Assert.AreEqual("E", messages[2].Type); Assert.AreEqual("エラー", messages[2].Caption); Assert.AreEqual("エラーメッセージ", messages[2].Text);
            Assert.AreEqual("0004", messages[3].ID); Assert.AreEqual("Q", messages[3].Type); Assert.AreEqual("質問", messages[3].Caption); Assert.AreEqual("質問メッセージ", messages[3].Text);
            Assert.AreEqual("0005", messages[4].ID); Assert.AreEqual("C", messages[4].Type); Assert.AreEqual("カスタム", messages[4].Caption); Assert.AreEqual("カスタムメッセージ", messages[4].Text);


        }

        [TestMethod]
        public void ShowMessage()
        {

            Messages messages = new Messages(UtilityTestClass.NormalTestXMLFilePath);
            UtilityLibrary.Messanger messanger = new Messanger(UtilityTestClass.NormalTestXMLFilePath);
            DialogResult dialogResult;

            //異常系
            //存在していないIDを指定
            bool isThrownError = false;
            try
            {
                dialogResult = messanger.ShowMessage("0000");
            }
            catch
            {
                isThrownError = true;
            }

            //MessageType = Cを指定
            isThrownError = false;
            try
            {
                dialogResult = messanger.ShowMessage("0005");
            }
            catch
            {
                isThrownError = true;
            }

            Assert.AreEqual(true, isThrownError);

            //正常系
            dialogResult = messanger.ShowMessage("0001");
            Assert.AreEqual(dialogResult, DialogResult.OK) ;

            dialogResult = messanger.ShowMessage("0002");
            Assert.AreEqual(dialogResult, DialogResult.OK);


            dialogResult = messanger.ShowMessage("0003");
            Assert.AreEqual(dialogResult, DialogResult.OK);


            dialogResult = messanger.ShowMessage("0004");
            Assert.AreEqual(dialogResult, DialogResult.Yes);


        }

        [TestMethod]
        public void ShowCustomMessage()
        {

            Messages messages = new Messages(UtilityTestClass.NormalTestXMLFilePath);
            UtilityLibrary.Messanger messanger = new Messanger(UtilityTestClass.NormalTestXMLFilePath);
            DialogResult dialogResult;

            //異常系
            //存在していないIDを指定
            bool isThrownError = false;
            try
            {
                dialogResult = messanger.ShowCustomMessage("0000", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            catch
            {
                isThrownError = true;
            }

            //MessageType = C以外を指定
            isThrownError = false;
            try
            {
                dialogResult = messanger.ShowCustomMessage("0001", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                isThrownError = true;
            }

            Assert.AreEqual(true, isThrownError);

            //正常系
            dialogResult = messanger.ShowCustomMessage("0005", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Assert.AreEqual(dialogResult, DialogResult.OK);


        }


    }
}
