using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace UtilityLibrary
{
    public static class FileUtility
    {
        /// <summary>
        /// 指定されたファイルパスのファイルをコピーする。
        /// ディレクトリが存在しない場合は自動的に作成する。
        /// </summary>
        /// <param name="sourcePath">コピー対象のファイルパス</param>
        /// <param name="destinationPath">コピー先のファイルパス</param>
        public static void Copy(string sourcePath, string destinationPath)
        {
            //ファイルが存在しない場合はエラー
            if(!System.IO.File.Exists(sourcePath))
            {
                throw new FileNotFoundException(sourcePath + " is not exists.");
            }

            //ディレクトリ作成
            var directoryPath = Path.GetDirectoryName(destinationPath);
            CreateDirectory(directoryPath);

            //ファイルコピー
            File.Copy(sourcePath, destinationPath);


        }

        /// <summary>
        /// 指定されたディレクトリを作成する。すでに存在する場合は何もしない。
        /// </summary>
        /// <param name="directoryPath">作成対象のディレクトリパス</param>
        private static void CreateDirectory(string directoryPath)
        {
            if (Directory.Exists(directoryPath))
                return;

            Directory.CreateDirectory(directoryPath);
            return;

        }

        /// <summary>
        /// 設定取得処理。設定ファイルをデシリアライズした結果を出力。
        /// </summary>
        /// <returns>設定オブジェクト</returns>
        public static T GeXMLSetting<T>(string xmlFilePath)
        {
            try
            {

                using (var streamReader = new StreamReader(xmlFilePath, Encoding.UTF8))
                {
                    var serializer = new XmlSerializer(typeof(T));
                    var settings = (T)serializer.Deserialize(streamReader);
                    return settings;
                }
            }
            catch
            {
                return default(T);
            }
        }


        /// <summary>
        /// 指定されたファイルを関連付けられたプログラムで開く
        /// </summary>
        /// <param name="filePath">対象のファイルパス</param>
        public static void OpenFileWaitForExit(string filePath)
        {

            var process = System.Diagnostics.Process.Start(filePath);
            process.WaitForExit();
        }

        /// <summary>
        /// ファイル名が問題ないかどうかチェックする
        /// </summary>
        /// <param name="filename">チェックしたいファイル名</param>
        /// <returns>問題がある場合、trueを返す</returns>
        public static bool IsValidFileName(string filename)
        {
            var validFileNameRegex = new System.Text.RegularExpressions.Regex(
                "^(?!(?:CON|PRN|AUX|NUL|COM[1-9]|LPT[1-9])(?:\\.[^.]*)?$)" +
                "[^<>:\"/\\\\|?*\\x00-\\x1F]*[^<>:\"/\\\\|?*\\x00-\\x1F\\ .]$",
                System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            System.Text.RegularExpressions.Match regexMatch = validFileNameRegex.Match(filename);

            return regexMatch.Success;
        }
    }
}
