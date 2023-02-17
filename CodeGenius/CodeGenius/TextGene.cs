using System;
using MeCab;

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
    }
}


