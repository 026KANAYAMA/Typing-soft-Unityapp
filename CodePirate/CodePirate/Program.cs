using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

/// <summary>
/// 任意の個数のテキストを生成するクラス
/// </summary>
public class TextGeneration
{
    const int LIST_NUM = 10000;
    internal int geneNum;
    internal string nounPath = "Noun.txt";
    internal string nounHiraPath = "Noun_hira.txt";
    internal string adjPath = "Adjective.txt";
    internal string adjHiraPath = "Adjective_hira.txt";
   

    /// <summary>
    /// コンストラクター : 作成する文の個数を受け取る
    /// </summary>
    /// <param name="_geneNum"></param>
    public TextGeneration(int _geneNum)
    {
        geneNum = _geneNum;
    }
    /// <summary>
    /// テキストファイルにアクセスし、単語の集合を取得する関数
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    internal string AccessFiles(string path)
    {
        StreamReader sr = new StreamReader(path, Encoding.GetEncoding("UTF-8"));
        string str = sr.ReadToEnd();
        sr.Close();
        //Console.WriteLine("Success : AccessFiles\n");
        return str;
    }

    /// <summary>
    /// geneNumの個数だけテキストと読み仮名のタプルのリストをリストからランダムに作る
    /// </summary>
    /// <param name="listNoun"></param>
    /// <param name="listAdj"></param>
    /// <param name="listHiraNoun"></param>
    /// <param name="listHiraAdj"></param>
    /// <returns></returns>
    internal List<Tuple<string,string>> RandomConText(List<string> listNoun, List<string> listAdj, List<string> listHiraNoun, List<string> listHiraAdj)
    {
        var conTextList = new List<Tuple<string,string>>();
        Random rand = new System.Random();
        
        for (int i=0; i<geneNum; i++)
        {
            // 0 ～ 9999の範囲の乱数を生成
            int idx = rand.Next(0, LIST_NUM);
            string[] ls = { listAdj[idx],listNoun[idx]};
            string[] lsHira = { listHiraAdj[idx], listHiraNoun[idx] };
            var tp = Tuple.Create(string.Join("", ls),string.Join("", lsHira));
            conTextList.Add(tp);
        }

        return conTextList;
    }

    /// <summary>
    /// メイン処理を行う関数 : AccessFIles関数, RandomConText関数を使う
    /// </summary>
    /// <returns></returns>
    public List<Tuple<string, string>> GetText()
    {

        string strNoun = AccessFiles(nounPath);
        string strAdj = AccessFiles(adjPath);
        string strHiraNoun = AccessFiles(nounHiraPath);
        string strHiraAdj = AccessFiles(adjHiraPath);

        // listの10001番目に正体不明の空文字列が足されているのでGetRangeで10000番目まで取得するようにしている
        var listNoun = (strNoun.Split(',').Cast<string>().ToList()).GetRange(0,10000);
        var listAdj = (strAdj.Split(',').Cast<string>().ToList()).GetRange(0,10000);
        var listHiraNoun = (strHiraNoun.Split(',').Cast<string>().ToList()).GetRange(0,10000);
        var listHiraAdj = (strHiraAdj.Split(',').Cast<string>().ToList()).GetRange(0,10000);
      
        var listCon = RandomConText(listNoun, listAdj, listHiraNoun, listHiraAdj);

        return listCon;
    }

    /// <summary>
    /// GetText関数で取得したタプルのリストをコンソールに出力する関数
    /// </summary>
    /// <param name="textList"></param>
    public void PrintText(List<Tuple<string,string>> textList)
    {
        foreach (var pair in textList)
        {
            Console.WriteLine(pair);
        }
    }
}


/// <summary>
/// クラスTextGenerationを試すためのsampleクラス
/// </summary>
public class Sample
{   
    /// <summary>
    /// エントリーポイント
    /// </summary>
    static void Main()
    {
        var text = new TextGeneration(10);
        var textList = text.GetText();

        text.PrintText(textList);
    }

}
