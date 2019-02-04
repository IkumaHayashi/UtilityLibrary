using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace UtilityLibrary
{
    public class Utility
    {
        /// <summary>
        /// 設定取得処理。設定ファイルをデシリアライズした結果を出力。
        /// </summary>
        /// <returns>設定オブジェクト。</returns>
        public static T GetSettingsObject<T>(string xmlFilePath)
            where T : new()
        {
            using (var streamReader = new StreamReader(xmlFilePath, Encoding.UTF8))
            {
                var serializer = new XmlSerializer(typeof(T));
                T settingObject = (T)serializer.Deserialize(streamReader);
                return settingObject;
            }
        }
    }
}
