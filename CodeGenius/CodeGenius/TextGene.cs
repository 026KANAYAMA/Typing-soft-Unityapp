using System;
using MeCab;
using System.IO;
using System.Text;


static class UseMecab
{
    public static void excute()
    {
        var sentence = "マリーアントワネット演じてるだけ";
        var parameter = new MeCabParam();
        var tagger = MeCabTagger.Create(parameter);
        
        foreach (var node in tagger.ParseToNodes(sentence))
        {
            if (node.CharType > 0)
            {
                var features = node.Feature.Split(',');
                var displayFeatures = string.Join(", ", features);

                Console.WriteLine($"{node.Surface}\t{displayFeatures}");
            }
        }
    }
}


static class AccessFile
{
    public static string getFile()
    {
        StreamReader sr = new StreamReader("sample-input.txt", Encoding.GetEncoding("UTF-8"));
        string str = sr.ReadToEnd();
        sr.Close();


        return str;
    } 
}


/// <summary>
/// Test用Class
/// </summary>
static class Test
{
    /// <summary>
    /// Entry Point
    /// </summary>
    static void Main()
    {
        UseMecab.excute();
        string str = AccessFile.getFile();
        Console.WriteLine($"ファイルから受け取ったのは「{str}」です。");

    }
}


