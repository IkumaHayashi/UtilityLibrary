using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;
using System.Windows.Forms;
using UtilityLibrary;


namespace UtilityLibrary
{
    public partial class Messanger
    {
        /// <summary>
        /// メッセージリストオブジェクト
        /// </summary>
        private Messages _messages;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="messageFilePath">メッセージ設定ファイル(XML)のパス</param>
        public Messanger(string settingXMLPath)
        {
            _messages = new Messages(settingXMLPath);
            
        }

        public DialogResult ShowCustomMessage(string messageID, MessageBoxButtons messageBoxButton, MessageBoxIcon messageBoxIcon)
        {
            //メッセージオブジェクト取得
            var message = _messages.FirstOrDefault(m => m.ID == messageID);
            if (message == null)
                throw new Exception("指定されたmessageIDは存在しません。");


            //カスタムの以外の場合エラーを投げる
            if (message.Type != MessageType.Custom)
                throw new Exception("MessageTypeが[C]以外場合は、[ShowMessage]を指定してください。");


            //メッセージの表示
            DialogResult messageBoxResult;
            messageBoxResult = MessageBox.Show(
                message.Text
                , message.Caption
                , messageBoxButton
                , messageBoxIcon);

            return messageBoxResult;
        }

        private Message GetMessageFromID(string messageID)
        {

            var message = _messages.FirstOrDefault(m => m.ID == messageID);
            if (message == null)
                throw new Exception("指定されたmessageIDは存在しません。");

            return message;
        }

        public DialogResult ShowMessage(string messageID, string messageText)
        {


            //メッセージオブジェクト取得
            var message = GetMessageFromID(messageID);


            //カスタムの場合エラーを投げる
            if (message.Type == MessageType.Custom)
                throw new Exception("MessageTypeが[C]の場合は、messageBoxIcon, messageBoxButtonを指定してください。");



            //メッセージ表示プロパティの生成
            MessageBoxButtons messageBoxButton;
            MessageBoxIcon messageBoxIcon;
            switch (message.Type)
            {
                case MessageType.Information:
                    messageBoxIcon = MessageBoxIcon.Information;
                    messageBoxButton = MessageBoxButtons.OK;
                    break;
                case MessageType.Question:
                    messageBoxIcon = MessageBoxIcon.Question;
                    messageBoxButton = MessageBoxButtons.YesNo;
                    break;
                case MessageType.Warning:
                    messageBoxIcon = MessageBoxIcon.Warning;
                    messageBoxButton = MessageBoxButtons.OKCancel;
                    break;
                default: //case MessageType.Error:
                    messageBoxIcon = MessageBoxIcon.Error;
                    messageBoxButton = MessageBoxButtons.OK;
                    break;

            }




            //メッセージの表示
            DialogResult messageBoxResult;
            messageBoxResult = MessageBox.Show(
                message.Text
                , message.Caption
                , messageBoxButton
                , messageBoxIcon);

            return messageBoxResult;
        }

        public DialogResult ShowMessage(string messageID)
        {

            //メッセージオブジェクト取得
            var message = _messages.FirstOrDefault(m => m.ID == messageID);
            if (message == null)
                throw new Exception("指定されたmessageIDは存在しません。");


            //カスタムの場合エラーを投げる
            if (message.Type == MessageType.Custom)
                throw new Exception("MessageTypeが[C]の場合は、messageBoxIcon, messageBoxButtonを指定してください。");



            //メッセージ表示プロパティの生成
            MessageBoxButtons messageBoxButton;
            MessageBoxIcon messageBoxIcon;
            switch(message.Type)
            {
                case MessageType.Information:
                    messageBoxIcon = MessageBoxIcon.Information;
                    messageBoxButton = MessageBoxButtons.OK;
                    break;
                case MessageType.Question:
                    messageBoxIcon = MessageBoxIcon.Question;
                    messageBoxButton = MessageBoxButtons.YesNo;
                    break;
                case MessageType.Warning:
                    messageBoxIcon = MessageBoxIcon.Warning;
                    messageBoxButton = MessageBoxButtons.OKCancel;
                    break;
                default: //case MessageType.Error:
                    messageBoxIcon = MessageBoxIcon.Error;
                    messageBoxButton = MessageBoxButtons.OK;
                    break;

            }




            //メッセージの表示
            DialogResult messageBoxResult;
            messageBoxResult = MessageBox.Show(
                message.Text
                , message.Caption
                , messageBoxButton
                , messageBoxIcon);

            return messageBoxResult;
        }

    }



}
