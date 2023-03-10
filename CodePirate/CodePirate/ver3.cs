using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using global::System.Diagnostics;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;


namespace Typing
{
    /// <summary> タイピングに関する諸情報を管理するクラス</summary>
    class TypeUtils
    {
        /// <summary>かな文字列からローマ字文字列のリストを返す辞書</summary>
        public static Dictionary<string, string[]> kana2romaList = new Dictionary<string, string[]>()
        {
            ["ぁ"] = new string[2] { "la", "xa" },
            ["あ"] = new string[1] { "a" },
            ["ぃ"] = new string[2] { "li", "xi" },
            ["い"] = new string[1] { "i" },
            ["ぅ"] = new string[2] { "lu", "xu" },
            ["う"] = new string[2] { "u", "wu" },
            ["ぇ"] = new string[2] { "le", "xe" },
            ["え"] = new string[1] { "e" },
            ["ぉ"] = new string[2] { "lo", "xo" },
            ["お"] = new string[1] { "o" },
            ["か"] = new string[1] { "ka", /*"ca"*/ },
            ["が"] = new string[1] { "ga", },
            ["き"] = new string[1] { "ki" },
            ["ぎ"] = new string[1] { "gi" },
            ["く"] = new string[2] { "ku", "qu" },
            ["ぐ"] = new string[1] { "gu" },
            ["け"] = new string[1] { "ke" },
            ["げ"] = new string[1] { "ge" },
            ["こ"] = new string[1] { "ko" },
            ["ご"] = new string[1] { "go" },
            ["さ"] = new string[1] { "sa" },
            ["ざ"] = new string[1] { "za" },
            ["し"] = new string[2] { "si", "shi" },
            ["じ"] = new string[2] { "ji", "zi", },
            ["す"] = new string[1] { "su" },
            ["ず"] = new string[1] { "zu" },
            ["せ"] = new string[1] { "se" },
            ["ぜ"] = new string[1] { "ze" },
            ["そ"] = new string[1] { "so" },
            ["ぞ"] = new string[1] { "zo" },
            ["た"] = new string[1] { "ta" },
            ["だ"] = new string[1] { "da" },
            ["ち"] = new string[2] { "ti", "chi" },
            ["ぢ"] = new string[1] { "di" },
            ["っ"] = new string[4] { "ltu", "xtu", "ltsu", "xtsu" },
            ["つ"] = new string[2] { "tu", "tsu" },
            ["づ"] = new string[1] { "du" },
            ["て"] = new string[1] { "te" },
            ["で"] = new string[1] { "de" },
            ["と"] = new string[1] { "to" },
            ["ど"] = new string[1] { "do" },
            ["な"] = new string[1] { "na" },
            ["に"] = new string[1] { "ni" },
            ["ぬ"] = new string[1] { "nu" },
            ["ね"] = new string[1] { "ne" },
            ["の"] = new string[1] { "no" },
            ["は"] = new string[1] { "ha" },
            ["ば"] = new string[1] { "ba" },
            ["ぱ"] = new string[1] { "pa" },
            ["ひ"] = new string[1] { "hi" },
            ["び"] = new string[1] { "bi" },
            ["ぴ"] = new string[1] { "pi" },
            ["ふ"] = new string[1] { "hu" },
            ["ぶ"] = new string[1] { "bu" },
            ["ぷ"] = new string[1] { "pu" },
            ["へ"] = new string[1] { "he" },
            ["べ"] = new string[1] { "be" },
            ["ぺ"] = new string[1] { "pe" },
            ["ほ"] = new string[1] { "ho" },
            ["ぼ"] = new string[1] { "bo" },
            ["ぽ"] = new string[1] { "po" },
            ["ま"] = new string[1] { "ma" },
            ["み"] = new string[1] { "mi" },
            ["む"] = new string[1] { "mu" },
            ["め"] = new string[1] { "me" },
            ["も"] = new string[1] { "mo" },
            ["ゃ"] = new string[2] { "lya", "xya" },
            ["や"] = new string[1] { "ya" },
            ["ゅ"] = new string[2] { "lyu", "xyu" },
            ["ゆ"] = new string[1] { "yu" },
            ["ょ"] = new string[2] { "lyo", "xyo" },
            ["よ"] = new string[1] { "yo" },
            ["ら"] = new string[1] { "ra" },
            ["り"] = new string[1] { "ri" },
            ["る"] = new string[1] { "ru" },
            ["れ"] = new string[1] { "re" },
            ["ろ"] = new string[1] { "ro" },
            ["ゎ"] = new string[2] { "lwa", "xwa" },
            ["わ"] = new string[1] { "wa" },
            ["ゐ"] = new string[1] { "wi" },
            ["ゑ"] = new string[1] { "we" },
            ["を"] = new string[1] { "wo" },
            ["ん"] = new string[3] { "n", "nn", "xn" },
            ["ゔ"] = new string[1] { "vu" },
            ["ゕ"] = new string[2] { "lka", "xka" },
            ["ゖ"] = new string[2] { "lke", "xke" },
            ["っぁ"] = new string[2] { "lla", "xxa" },
            ["っぃ"] = new string[2] { "lli", "xxi" },
            ["っぅ"] = new string[2] { "llu", "xxu" },
            ["っぇ"] = new string[2] { "lle", "xxe" },
            ["っぉ"] = new string[2] { "llo", "xxo" },
            ["っか"] = new string[1] { "kka", /*"ca"*/ },
            ["っが"] = new string[1] { "gga", },
            ["っき"] = new string[1] { "kki" },
            ["っぎ"] = new string[1] { "ggi" },
            ["っく"] = new string[1] { "kku" },
            ["っぐ"] = new string[1] { "ggu" },
            ["っけ"] = new string[1] { "kke" },
            ["っげ"] = new string[1] { "gge" },
            ["っこ"] = new string[1] { "kko" },
            ["っご"] = new string[1] { "ggo" },
            ["っさ"] = new string[1] { "ssa" },
            ["っざ"] = new string[1] { "zza" },
            ["っし"] = new string[2] { "ssi", "sshi" },
            ["っじ"] = new string[2] { "zzi","jji" },
            ["っす"] = new string[1] { "ssu" },
            ["っず"] = new string[1] { "zzu" },
            ["っせ"] = new string[1] { "sse" },
            ["っぜ"] = new string[1] { "zze" },
            ["っそ"] = new string[1] { "sso" },
            ["っぞ"] = new string[1] { "zzo" },
            ["った"] = new string[1] { "tta" },
            ["っだ"] = new string[1] { "dda" },
            ["っち"] = new string[2] { "tti", "cchi" },
            ["っぢ"] = new string[1] { "ddi" },
            ["っっ"] = new string[2] { "lltu", "xxtu" },
            ["っつ"] = new string[2] { "ttu", "ttsu" },
            ["っづ"] = new string[1] { "ddu" },
            ["って"] = new string[1] { "tte" },
            ["っで"] = new string[1] { "dde" },
            ["っと"] = new string[1] { "tto" },
            ["っど"] = new string[1] { "ddo" },
            ["っは"] = new string[1] { "hha" },
            ["っば"] = new string[1] { "bba" },
            ["っぱ"] = new string[1] { "ppa" },
            ["っひ"] = new string[1] { "hhi" },
            ["っび"] = new string[1] { "bbi" },
            ["っぴ"] = new string[1] { "ppi" },
            ["っふ"] = new string[1] { "hhu" },
            ["っぶ"] = new string[1] { "bbu" },
            ["っぷ"] = new string[1] { "ppu" },
            ["っへ"] = new string[1] { "hhe" },
            ["っべ"] = new string[1] { "bbe" },
            ["っぺ"] = new string[1] { "ppe" },
            ["っほ"] = new string[1] { "hho" },
            ["っぼ"] = new string[1] { "bbo" },
            ["っぽ"] = new string[1] { "ppo" },
            ["っま"] = new string[1] { "mma" },
            ["っみ"] = new string[1] { "mmi" },
            ["っむ"] = new string[1] { "mmu" },
            ["っめ"] = new string[1] { "mme" },
            ["っも"] = new string[1] { "mmo" },
            ["っゃ"] = new string[2] { "llya", "xxya" },
            ["っや"] = new string[1] { "yya" },
            ["っゅ"] = new string[2] { "llyu", "xxyu" },
            ["っゆ"] = new string[1] { "yyu" },
            ["っょ"] = new string[2] { "llyo", "xxyo" },
            ["っよ"] = new string[1] { "yyo" },
            ["っら"] = new string[1] { "rra" },
            ["っり"] = new string[1] { "rri" },
            ["っる"] = new string[1] { "rru" },
            ["っれ"] = new string[1] { "rre" },
            ["っろ"] = new string[1] { "rro" },
            ["っゎ"] = new string[2] { "llwa", "xxwa" },
            ["っわ"] = new string[1] { "wwa" },
            ["っゐ"] = new string[1] { "wwi" },
            ["っゑ"] = new string[1] { "wwe" },
            ["っを"] = new string[1] { "wwo" },
            ["っゔ"] = new string[1] { "vvu" },
            ["っゕ"] = new string[2] { "llka", "xxka" },
            ["っゖ"] = new string[2] { "llke", "xxke" },

            ["きゃ"] = new string[1] { "kya" },
            ["きぃ"] = new string[1] { "kyi" },
            ["きゅ"] = new string[1] { "kyu" },
            ["きぇ"] = new string[1] { "kye" },
            ["きょ"] = new string[1] { "kyo" },
            ["ぎゃ"] = new string[1] { "gya" },
            ["ぎゅ"] = new string[1] { "gyu" },
            ["ぎぇ"] = new string[1] { "gye" },
            ["ぎょ"] = new string[1] { "gyo" },
            ["しゃ"] = new string[2] { "sya", "sha" },
            ["しぃ"] = new string[1] { "syi" },
            ["しゅ"] = new string[2] { "syu", "shu" },
            ["しぇ"] = new string[2] { "sye", "she" },
            ["しょ"] = new string[2] { "syo", "sho" },
            ["じゃ"] = new string[3] { "zya", "ja", "jya" },
            ["じぃ"] = new string[2] { "zyi", "jyi" },
            ["じゅ"] = new string[3] { "zyu", "ju", "jyu" },
            ["じぇ"] = new string[3] { "zye", "je", "jye" },
            ["じょ"] = new string[3] { "zyo", "jo", "jyo" },
            ["ちゃ"] = new string[2] { "tya", "cha" },
            ["ちぃ"] = new string[1] { "tyi" },
            ["ちゅ"] = new string[2] { "tyu", "chu" },
            ["ちぇ"] = new string[2] { "tye", "che" },
            ["ちょ"] = new string[2] { "tyo", "cho" },
            ["ぢゃ"] = new string[1] { "dya", },
            ["ぢぃ"] = new string[1] { "dyi", },
            ["ぢゅ"] = new string[1] { "dyu" },
            ["ぢぇ"] = new string[1] { "dye" },
            ["ぢょ"] = new string[1] { "dyo" },
            ["にゃ"] = new string[1] { "nya" },
            ["にぃ"] = new string[1] { "nyi" },
            ["にゅ"] = new string[1] { "nyu" },
            ["にぇ"] = new string[1] { "nye" },
            ["にょ"] = new string[1] { "nyo" },
            ["ひゃ"] = new string[1] { "hya" },
            ["ひぃ"] = new string[1] { "hyi" },
            ["ひゅ"] = new string[1] { "hyu" },
            ["ひぇ"] = new string[1] { "hye" },
            ["ひょ"] = new string[1] { "hyo" },
            ["びゃ"] = new string[1] { "bya" },
            ["びぃ"] = new string[1] { "byi" },
            ["びゅ"] = new string[1] { "byu" },
            ["びぇ"] = new string[1] { "bye" },
            ["びょ"] = new string[1] { "byo" },
            ["ぴゃ"] = new string[1] { "pya" },
            ["ぴぃ"] = new string[1] { "pyi" },
            ["ぴゅ"] = new string[1] { "pyu" },
            ["ぴぇ"] = new string[1] { "pye" },
            ["ぴょ"] = new string[1] { "pyo" },
            ["みゃ"] = new string[1] { "mya" },
            ["みぃ"] = new string[1] { "myi" },
            ["みゅ"] = new string[1] { "myu" },
            ["みぇ"] = new string[1] { "mye" },
            ["みょ"] = new string[1] { "myo" },
            ["りゃ"] = new string[1] { "rya" },
            ["りぃ"] = new string[1] { "ryi" },
            ["りゅ"] = new string[1] { "ryu" },
            ["りぇ"] = new string[1] { "rye" },
            ["りょ"] = new string[1] { "ryo" },

            ["っきゃ"] = new string[1] { "kkya" },
            ["っきぃ"] = new string[1] { "kkyi" },
            ["っきゅ"] = new string[1] { "kkyu" },
            ["っきぇ"] = new string[1] { "kkye" },
            ["っきょ"] = new string[1] { "kkyo" },
            ["っぎゃ"] = new string[1] { "ggya" },
            ["っぎゅ"] = new string[1] { "ggyu" },
            ["っぎぇ"] = new string[1] { "ggye" },
            ["っぎょ"] = new string[1] { "ggyo" },
            ["っしゃ"] = new string[2] { "ssya", "ssha" },
            ["っしぃ"] = new string[1] { "ssyi" },
            ["っしゅ"] = new string[2] { "ssyu", "shu" },
            ["っしぇ"] = new string[2] { "ssye", "she" },
            ["っしょ"] = new string[2] { "ssyo", "sho" },
            ["っじゃ"] = new string[3] { "zzya", "jja", "jjya" },
            ["っじぃ"] = new string[2] { "zzyi", "jjyi" },
            ["っじゅ"] = new string[3] { "zzyu", "jju", "jjyu" },
            ["っじぇ"] = new string[3] { "zzye", "jje", "jjye" },
            ["っじょ"] = new string[3] { "zzyo", "jjo", "jjyo" },
            ["っちゃ"] = new string[2] { "ttya", "ccha" },
            ["っちぃ"] = new string[1] { "ttyi" },
            ["っちゅ"] = new string[2] { "ttyu", "cchu" },
            ["っちぇ"] = new string[2] { "ttye", "cche" },
            ["っちょ"] = new string[2] { "ttyo", "ccho" },
            ["っぢゃ"] = new string[1] { "ddya", },
            ["っぢぃ"] = new string[1] { "ddyi", },
            ["っぢゅ"] = new string[1] { "ddyu" },
            ["っぢぇ"] = new string[1] { "ddye" },
            ["っぢょ"] = new string[1] { "ddyo" },
            ["っひゃ"] = new string[1] { "hhya" },
            ["っひぃ"] = new string[1] { "hhyi" },
            ["っひゅ"] = new string[1] { "hhyu" },
            ["っひぇ"] = new string[1] { "hhye" },
            ["っひょ"] = new string[1] { "hhyo" },
            ["っびゃ"] = new string[1] { "bbya" },
            ["っびぃ"] = new string[1] { "bbyi" },
            ["っびゅ"] = new string[1] { "bbyu" },
            ["っびぇ"] = new string[1] { "bbye" },
            ["っびょ"] = new string[1] { "bbyo" },
            ["っぴゃ"] = new string[1] { "ppya" },
            ["っぴぃ"] = new string[1] { "ppyi" },
            ["っぴゅ"] = new string[1] { "ppyu" },
            ["っぴぇ"] = new string[1] { "ppye" },
            ["っぴょ"] = new string[1] { "ppyo" },
            ["っみゃ"] = new string[1] { "mmya" },
            ["っみぃ"] = new string[1] { "mmyi" },
            ["っみゅ"] = new string[1] { "mmyu" },
            ["っみぇ"] = new string[1] { "mmye" },
            ["っみょ"] = new string[1] { "mmyo" },
            ["っりゃ"] = new string[1] { "rrya" },
            ["っりぃ"] = new string[1] { "rryi" },
            ["っりゅ"] = new string[1] { "rryu" },
            ["っりぇ"] = new string[1] { "rrye" },
            ["っりょ"] = new string[1] { "rryo" },

            ["くぁ"] = new string[2] { "qa", "qwa" },
            ["くぃ"] = new string[2] { "qi", "qwi" },
            ["くぅ"] = new string[1] { "qwu" },
            ["くぇ"] = new string[2] { "qe", "qwe" },
            ["くぉ"] = new string[2] { "qo", "qwo" },
            ["ぐぁ"] = new string[1] { "gwa" },
            ["ぐぃ"] = new string[1] { "gwi" },
            ["ぐぅ"] = new string[1] { "gwu" },
            ["ぐぇ"] = new string[1] { "gwe" },
            ["ぐぉ"] = new string[1] { "gwo" },
            ["つぁ"] = new string[1] { "tsa" },
            ["つぃ"] = new string[1] { "tsi" },
            ["つぇ"] = new string[1] { "tse" },
            ["つぉ"] = new string[1] { "tso" },
            ["てゃ"] = new string[1] { "tha" },
            ["てぃ"] = new string[1] { "thi" },
            ["てゅ"] = new string[1] { "thu" },
            ["てぇ"] = new string[1] { "the" },
            ["てょ"] = new string[1] { "tho" },
            ["でゃ"] = new string[1] { "dha" },
            ["でぃ"] = new string[1] { "dhi" },
            ["でゅ"] = new string[1] { "dhu" },
            ["でぇ"] = new string[1] { "dhe" },
            ["でょ"] = new string[1] { "dho" },
            ["とぁ"] = new string[1] { "twa" },
            ["とぃ"] = new string[1] { "twi" },
            ["とぅ"] = new string[1] { "twu" },
            ["とぇ"] = new string[1] { "twe" },
            ["とぉ"] = new string[1] { "two" },
            ["どぁ"] = new string[1] { "dwa" },
            ["どぃ"] = new string[1] { "dwi" },
            ["どぅ"] = new string[1] { "dwu" },
            ["どぇ"] = new string[1] { "dwe" },
            ["どぉ"] = new string[1] { "dwo" },
            ["ふぁ"] = new string[1] { "fa" },
            ["ふぃ"] = new string[1] { "fi" },
            ["ふぇ"] = new string[1] { "fe" },
            ["ふぉ"] = new string[1] { "fo" },
            ["ふゃ"] = new string[1] { "fya" },
            ["ふゅ"] = new string[1] { "fyu" },
            ["ふょ"] = new string[1] { "fyo" },

            ["っくぁ"] = new string[2] { "qqa", "qqwa" },
            ["っくぃ"] = new string[2] { "qqi", "qqwi" },
            ["っくぅ"] = new string[1] { "qqwu" },
            ["っくぇ"] = new string[2] { "qqe", "qqwe" },
            ["っくぉ"] = new string[2] { "qqo", "qqwo" },
            ["っぐぁ"] = new string[1] { "ggwa" },
            ["っぐぃ"] = new string[1] { "ggwi" },
            ["っぐぅ"] = new string[1] { "ggwu" },
            ["っぐぇ"] = new string[1] { "ggwe" },
            ["っぐぉ"] = new string[1] { "ggwo" },
            ["っつぁ"] = new string[1] { "ttsa" },
            ["っつぃ"] = new string[1] { "ttsi" },
            ["っつぇ"] = new string[1] { "ttse" },
            ["っつぉ"] = new string[1] { "ttso" },
            ["ってゃ"] = new string[1] { "ttha" },
            ["ってぃ"] = new string[1] { "tthi" },
            ["ってゅ"] = new string[1] { "tthu" },
            ["ってぇ"] = new string[1] { "tthe" },
            ["ってょ"] = new string[1] { "ttho" },
            ["っでゃ"] = new string[1] { "ddha" },
            ["っでぃ"] = new string[1] { "ddhi" },
            ["っでゅ"] = new string[1] { "ddhu" },
            ["っでぇ"] = new string[1] { "ddhe" },
            ["っでょ"] = new string[1] { "ddho" },
            ["っとぁ"] = new string[1] { "ttwa" },
            ["っとぃ"] = new string[1] { "ttwi" },
            ["っとぅ"] = new string[1] { "ttwu" },
            ["っとぇ"] = new string[1] { "ttwe" },
            ["っとぉ"] = new string[1] { "ttwo" },
            ["っどぁ"] = new string[1] { "ddwa" },
            ["っどぃ"] = new string[1] { "ddwi" },
            ["っどぅ"] = new string[1] { "ddwu" },
            ["っどぇ"] = new string[1] { "ddwe" },
            ["っどぉ"] = new string[1] { "ddwo" },
            ["っふぁ"] = new string[1] { "ffa" },
            ["っふぃ"] = new string[1] { "ffi" },
            ["っふぇ"] = new string[1] { "ffe" },
            ["っふぉ"] = new string[1] { "ffo" },
            ["っふゃ"] = new string[1] { "ffya" },
            ["っふゅ"] = new string[1] { "ffyu" },
            ["っふょ"] = new string[1] { "ffyo" },

            ["ー"] = new string[1] { "-" },
            ["・"] = new string[1] { "/" },
            [" "] = new string[1] { " " },
            ["　"] = new string[1] { " " },
            ["0"] = new string[1] { "0" },
            ["1"] = new string[1] { "1" },
            ["2"] = new string[1] { "2" },
            ["3"] = new string[1] { "3" },
            ["4"] = new string[1] { "4" },
            ["5"] = new string[1] { "5" },
            ["6"] = new string[1] { "6" },
            ["7"] = new string[1] { "7" },
            ["8"] = new string[1] { "8" },
            ["9"] = new string[1] { "9" },
            ["０"] = new string[1] { "0" },
            ["１"] = new string[1] { "1" },
            ["２"] = new string[1] { "2" },
            ["３"] = new string[1] { "3" },
            ["４"] = new string[1] { "4" },
            ["５"] = new string[1] { "5" },
            ["６"] = new string[1] { "6" },
            ["７"] = new string[1] { "7" },
            ["８"] = new string[1] { "8" },
            ["９"] = new string[1] { "9" },

        };
        /// <summary>んを入力するときにnではなくnnと入力しなければならなくなるかな文字の配列 </summary>
        public static string[] must_nn_list = { "な", "に", "ぬ", "ね", "の", "にゃ", "にぃ", "にゅ", "にぇ", "にょ", "や", "ゆ", "よ", "っや", "っゆ", "っよ" };
        /// <summary>問題文長上限</summary>
        public const int MAX_SENTENCE_LEN = 8192;

        /// <summary>
        /// 特殊キー以外の文字を入力受付する
        /// </summary>
        /// <returns></returns>
        public static char getChar()
        {
            ConsoleKeyInfo keyinfo;
            do
            {
                keyinfo = Console.ReadKey(true);
                // Ignore if Alt or Ctrl is pressed.
                if ((keyinfo.Modifiers & ConsoleModifiers.Alt) == ConsoleModifiers.Alt)
                    continue;
                if ((keyinfo.Modifiers & ConsoleModifiers.Control) == ConsoleModifiers.Control)
                    continue;
                // Ignore if KeyChar value is \u0000.
                // 押されたキーが Unicode 文字にマップされない場合 (たとえば、ユーザーが F1 キーまたは Home キーを押した場合)、プロパティの KeyChar 値は \U0000
                if (keyinfo.KeyChar == '\u0000') continue;
                // Ignore tab key.
                if (keyinfo.Key == ConsoleKey.Tab) continue;
                // Ignore backspace.
                if (keyinfo.Key == ConsoleKey.Backspace) continue;
                // Ignore escape key
                if (keyinfo.Key == ConsoleKey.Escape) continue;
                // Ignore Eneter
                if (keyinfo.Key == ConsoleKey.Enter) continue;

                break;
            } while (true);
            if (Char.IsLetter(keyinfo.KeyChar))
            {
                // 入力された文字がアルファベットなどの文字の場合
                if ((keyinfo.Modifiers & ConsoleModifiers.Shift) == 0)
                {
                    // Shiftキーが押されていない場合は、入力された文字をそのままバッファに追加する
                    return keyinfo.KeyChar;
                }
                else
                {
                    // Shiftキーが押されている場合は、CapsLockキーの状態に応じて大文字または小文字にする
                    // Windowsだけ 後々それ以外のOSに対応します
                    if (Console.CapsLock)
                        return Char.ToLower(keyinfo.KeyChar);
                    else
                        return Char.ToUpper(keyinfo.KeyChar);
                }
            }

            return keyinfo.KeyChar;
        }
    }

    /// <summary>
    /// 1つの入力お題について管理するクラス
    /// </summary>
    class TypeSentence
    {
        /// <summary>お題の文章</summary>
        public string? Sentence { get; set; }
        /// <summary>表示する文章</summary>
        public string? Text { get; set; }
        /// <summary>問題文長</summary>
        public int N { get; set; }
        /// <summary>グラフの辺 <br></br>edge[v] = {s, u}の時, v文字目からu文字目にかな文字列s</summary>
        public List<Tuple<string, int>>[] edge;
        /// <summary>入力中の単語の候補</summary>
        public List<Tuple<string, string, int>>? Current;
        /// <summary>入力中の文字番号</summary>
        public int idx { get; set; }

        public string prev_romaji;

        /// <summary> Sentenceを入力するための正式な入力ローマ字列 </summary>
        public string formally_entered_string { get; set; } = "";
        /// <summary>実際の入力ローマ字列</summary>
        public string acturally_entered_string { get; set; } = "";
        private Stopwatch sw = new();
        public List<TimeSpan> TimeSpanList { get; } = new();


        public TypeSentence(string _str, string text = "")
        {
            Sentence = _str;    // 問題文
            N = _str.Length;    // 問題文長

            // 問題文のオートマトンっぽいグラフの構築
            edge = new List<Tuple<string, int>>[this.Sentence.Length];
            for (int i = 0; i < this.N; i++) { edge[i] = new List<Tuple<string, int>>(); }
            // 尺取り法っぽくノードの検索をしてエッジの追加をしていく
            for (int l = 0, r = 1; l < this.Sentence.Length; l++, r = l + 1)
            {
                while (r <= this.Sentence.Length && TypeUtils.kana2romaList.ContainsKey(this.Sentence[l..r]))
                {
                    edge[l].Add(new Tuple<string, int>(this.Sentence[l..r], r));
                    r++;
                }

            }
            MakeCandidate(0);

            idx = 0;
            prev_romaji = "";
            Text = text;
            if (text == "") Text = Sentence;
        }

        public List<Tuple<string, string, int>> MakedCandidate(int _start)
        {
            var current = new List<Tuple<string, string, int>>();
            // 半角文字はここで候補に追加する
            if (0x20 <= Sentence[_start] && Sentence[_start] <= 0x7F)
            {
                current.Add(new Tuple<string, string, int>(Sentence[idx].ToString(), Sentence[idx].ToString(), 0));
            }

            for (int i = 0; i < edge[_start].Count; i++)
            {
                string _word; int _next;
                (_word, _next) = edge[_start][i];

                foreach (var romaji in TypeUtils.kana2romaList[_word])
                {

                    if (_word == "ん" && (_next == N || Array.IndexOf(TypeUtils.must_nn_list, edge[_next][0].Item1) != -1) && romaji == "n") continue;
                    current.Add(new Tuple<string, string, int>(_word, romaji, 0));
                }
            }
            return current;
        }

        /// <summary>
        /// _start文字目から入力される可能性のある単語群の生成
        /// </summary>
        /// <param name="_start">入力する文字の添え字</param>
        /// <returns></returns>
        public List<Tuple<string, string, int>> MakeCandidate(int _start)
        {
            return this.Current = MakedCandidate(_start);
        }

        /// <summary>
        /// 問題文の入力が完了していればtrueを返す
        /// </summary>
        /// <returns></returns>
        public bool hasEndedEntry()
        {
            return idx == N;
        }

        public bool getChar()
        {

            var c = TypeUtils.getChar();
            Console.Write(c);
            var ret = inputChar(c);
            return ret;
        }
        public void TimerStart() { sw.Start(); }
        public void TimerStop() { sw.Stop(); }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="c"></param>
        /// <returns>問題文に対し正しい入力であればtrueを返す</returns>
        public bool inputChar(char c)
        {
            if (sw.IsRunning)
            {
                sw.Stop();
                TimeSpanList.Add(sw.Elapsed);
            }
            sw.Restart();

            acturally_entered_string += c;

            // "ん"のnn入力を許容する
            if (c == 'n' && prev_romaji == "n")
            {
                formally_entered_string += c;
                prev_romaji = "";
                return true;
            }


            bool exists_collect_input = false;

            // 入力文字が候補と一致しているか検索
            foreach (var val in Current)
            {
                // 各候補ローマ字列の入力待ち文字が入力された場合 trueにする
                if (val.Item2[val.Item3] == c)
                {
                    exists_collect_input = true;
                    break;
                }
            }

            // 候補と一致する入力である場合
            if (exists_collect_input)
            {
                formally_entered_string += c;
                // 候補から外れた候補群を削除
                Current.RemoveAll(val => val.Item2[val.Item3] != c);

                for (int i = 0; i < Current.Count; ++i)
                {
                    // 入力をかな1文字分満たした場合
                    if (Current[i].Item2.Length == Current[i].Item3 + 1)
                    {
                        prev_romaji = Current[i].Item2;
                        idx += Current[i].Item1.Length;

                        // 全文字入力した場合
                        if (idx == Sentence.Length)
                        {
                            break;
                        }

                        Current.Clear();
                        MakeCandidate(idx);
                        break;
                    }
                    else
                    {
                        Current[i] = new Tuple<string, string, int>(Current[i].Item1, Current[i].Item2, Current[i].Item3 + 1);
                    }
                }
            }


            return exists_collect_input;
        }

        /// <summary>
        /// 入力に要した時間
        /// </summary>
        /// <returns></returns>
        public TimeSpan sumTime()
        {
            return TimeSpanList.Aggregate((p, x) => p + x);
        }

        public string ToJson()
        {
            var options = new JsonSerializerOptions
            {
                // JavaScriptEncoder.Createでエンコードしない文字を指定するのも可
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                // 読みやすいようインデントを付ける
                WriteIndented = true
            };
            return JsonSerializer.Serialize(this, options);
            //return JsonConvert.SerializeObject(this);
        }
        public async Task write()
        {
            await File.WriteAllTextAsync(DateTime.Now.ToFileTime().ToString() + "_sentence.txt", ToJson());
        }

        public static TypeSentence FromJson(string json)
        {
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            // optionsは省略可
            TypeSentence sentence = JsonSerializer.Deserialize<TypeSentence>(json, options);
            return sentence;
        }

        public static TypeSentence Read(string filepath)
        {
            string json = System.IO.File.ReadAllText(filepath);
            return FromJson(json);
        }
    }


    class TypeGame
    {
        public TypeSentence[] Sentences;
        /// <summary>入力中の問題番号</summary>
        public int problem_no { get; set; } = 0;
        public int sentence_idx { get; set; } = 0;

        public int N
        {
            get
            {
                return Sentences.Length;
            }
        }

        public bool is_moving_sentence = true;

        public TypeGame(int n = 10)
        {
            Sentences = new TypeSentence[n];
            MakeSentences(n);

        }

        void MakeSentences(int n = 10)
        {            
            var text = new TextGeneration(n);
            var textList = text.GetText();
            for(int i = 0; i < n; ++i)
            {
                Sentences[i] = new TypeSentence(textList[i].Item2, textList[i].Item1);
            }
            
            /*
            Sentences.Add(new TypeSentence("あさはぱん"));
            /*
            Sentences.Add(new TypeSentence("しゃばだばさかばじゃあさからなま はなたかだかなかなかあたまがから", "シャバダバ酒場じゃ朝から生　鼻高々なかなか頭が空"));
            Sentences.Add(new TypeSentence("えんじゃくいずくんぞこうこくのこころざしをしらんや"));
            */
        }

        void Start()
        {
            Sentences[0].TimerStart();
        }
        void Next()
        {
            CurrentSentence().TimerStop();
            sentence_idx++;
            problem_no++;
            if (!hasEndedEntry())
                CurrentSentence().TimerStart();
        }

        public TypeSentence CurrentSentence()
        {
            return Sentences[problem_no];
        }

        public bool hasEndedEntry()
        {
            return problem_no == N;
        }

        public bool inputChar(char c)
        {
            is_moving_sentence = false;
            var is_collect = CurrentSentence().inputChar(c);

            if (CurrentSentence().hasEndedEntry())
            {
                is_moving_sentence = true;
                Next();

            }

            return is_collect;
        }

        public bool getChar()
        {
            var c = TypeUtils.getChar();
            Console.Write(c);
            var ret = inputChar(c);
            return ret;
        }

    }

    static class Programs
    {

        public static string prompt()
        {
            string str;
            Console.Write("お題をひらがなで入力 : ");
            str = Console.ReadLine();

            return str;
        }

        static void Main()
        {


            TypeGame game = new(10);

            while (!game.hasEndedEntry())
            {
                if (game.is_moving_sentence)
                {
                    Console.WriteLine("");
                    Console.WriteLine(game.CurrentSentence().Text);
                    Console.WriteLine(game.CurrentSentence().Sentence);
                }
                var is_collect = game.getChar();
                if (!is_collect)
                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);

            }

            /*
            Console.Write("\n");
            Console.WriteLine("実際に入力された文字列 : {0}", problem.acturally_entered_string);
            Console.WriteLine("受け取られた文字列 : {0}", problem.formally_entered_string);

            Console.WriteLine("正確性 : {0}%", (double)problem.formally_entered_string.Length / problem.acturally_entered_string.Length * 100);
            Console.WriteLine("入力時間: {0} tps: {1}", problem.sumTime().TotalSeconds, problem.formally_entered_string.Length / problem.sumTime().TotalSeconds);

            problem.write();

            */
        }
    }
}