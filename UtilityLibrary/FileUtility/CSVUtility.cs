using CsvHelper;
using CsvHelper.Configuration;
using Hnx8.ReadJEnc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilityLibrary
{
    public static class CSVUtility
    {
        public static List<T> ConvertCSVtoList<T, M>(string csvFilePath, bool hasHeaderRecord, bool ignoreQuotes)
            where M : ClassMap
        {

            //文字コード判定
            var encodeName = DetectEncodeName(csvFilePath);


            //CSV読み込み
            List<T> records = null;
            using (var streamReader = new StreamReader(csvFilePath, Encoding.GetEncoding(encodeName)))
            using (var csvReader = new CsvReader(streamReader))
            {

                csvReader.Configuration.HasHeaderRecord = hasHeaderRecord;
                csvReader.Configuration.IgnoreQuotes = ignoreQuotes;
                csvReader.Configuration.RegisterClassMap<M>();

                records = csvReader.GetRecords<T>().ToList();

            }


            return records;
        }

        /// <summary>
        /// 対象のテキストファイルのエンコードを取得する
        /// </summary>
        /// <param name="filePath">取得対象のテキストファイルパス</param>
        /// <returns>エンコード名</returns>
        private static string DetectEncodeName(string filePath)
        {
            var fileInfo = new System.IO.FileInfo(filePath);

            using (Hnx8.ReadJEnc.FileReader reader = new FileReader(fileInfo))
            {
                Hnx8.ReadJEnc.CharCode charCode = reader.Read(fileInfo);

                switch(charCode.Name){
                    case "ShiftJIS":
                        return "Shift_JIS";
                    case "UTF-8":
                        return "utf-8";
                    default:
                        return charCode.Name;
                    
                }

            }
        }
    }

}
