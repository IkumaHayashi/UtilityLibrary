using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace UtilityLibrary
{
    public class Messages
        : List<Message>
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="settingXMLPath">読み込み対象のXMLファイルパス</param>
        public Messages(string settingXMLPath)
        {

            //設定ファイルを読み込みリストに格納する。
            DeserializeMessages deserializeMessages = Utility.GetSettingsObject<DeserializeMessages>(settingXMLPath);
            foreach(var message in deserializeMessages.MessageList)
            {
                if(! isValidMessageType(message.Type))
                {
                    throw new Exception("メッセージクラスが規定のものではありません。");
                }
                if(Find( x=> x.ID == message.ID) != null)
                {
                    throw new Exception("IDが重複しています。 ID = " + message.ID );

                }
                Add(message);
            }
        }

        /// <summary>
        /// MessageTypeの値の確認を行う
        /// </summary>
        /// <param name="messageType">確認対象の値</param>
        /// <returns>規定通りであればtrue, その他の値はfalseを返す。</returns>
        private bool isValidMessageType(string messageType)
        {
            if (messageType != MessageType.Custom
                && messageType != MessageType.Error
                && messageType != MessageType.Information
                && messageType != MessageType.Question
                && messageType != MessageType.Warning)
                return false;
            return true;
        }

    }


    /// <summary>
    /// メッセージ一覧クラス
    /// </summary>
    [XmlRoot("Messages")]
    public class DeserializeMessages
    {
        [System.Xml.Serialization.XmlElement("Message")]
        public List<Message> MessageList { get; set; }
    }

    /// <summary>
    /// メッセージクラス
    /// </summary>
    public class Message
    {

        [XmlAttribute("ID")]
        public string ID { get; set; }

        [XmlAttribute("Type")]
        public string Type { get; set; }

        [XmlAttribute("Caption")]
        public string Caption { get; set; }

        [XmlAttribute("Text")]
        public string Text { get; set; }
    }

}
