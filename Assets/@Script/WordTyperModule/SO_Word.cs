using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "Word Collection", menuName = "Word Collection")]
public class SO_Word : ScriptableObject
{
	public WordCollection words;
	[SerializeField] private bool addWithJson;
	[ShowIf("addWithJson")] [SerializeField] private TextAsset commandWords;
	[ShowIf("addWithJson")] [SerializeField] private TextAsset upperRowWords;
	[ShowIf("addWithJson")] [SerializeField] private TextAsset midRowWords;
	[ShowIf("addWithJson")] [SerializeField] private TextAsset bottomRowWords;
	[Button("Genereate Word")]
	public void GenereateWord()
	{
		if (commandWords == null|| upperRowWords == null || midRowWords == null || bottomRowWords == null)
        {
            Debug.Log("JSON Text Not Available");
            return;
        }
		//Debug.Log(JsonUtility.ToJson(words.commandWords));
		//Debug.Log("{\"words\":" + commandWords.text + "}");
		words.commandWords = JsonUtility.FromJson<CommandWords > ("{\"words\":" + commandWords.text + "}");
		words.upperRowWords = JsonUtility.FromJson<UpperWords>("{\"words\":" + upperRowWords.text + "}");
		words.midRowWords = JsonUtility.FromJson<MidWords>("{\"words\":" + midRowWords.text + "}");
		words.bottomRowWords = JsonUtility.FromJson<LowerWords>("{\"words\":" + bottomRowWords.text + "}");
	}

}

[System.Serializable]
public class WordCollection
{
	public CommandWords commandWords;
	public UpperWords upperRowWords;
	public MidWords midRowWords;
	public LowerWords bottomRowWords;
}
[System.Serializable]
public class CommandWords
{
	public string tutorialWord = "Menjadi garis yang kita inginkan saat kita berada di bawah dengan Baik keduanya yang di mana dunia pohon berhenti ditemukan Setiap perjalanan antar dunia dilakukan sebelum India kehidupan tidak setiap rumah sering kali menyanyikan Udaranya melihat garis air yang bagus tanpa beberapa saat lihat induk tanaman, lihat suara, pelajari cara membuatnya cukup malam mereka bisa mulai bekerja lebih banyak ditemukan Jenis yang sangat kita akhirnya berhenti tampaknya mungkin.";

	public List<Word> words = new List<Word>();
}
[System.Serializable]
public class UpperWords
{
	public string tutorialWord = "Towery troupe typier uptore equip erupt Outer outre pewit piety pique pouty power Purty query quiet twirp twyer uteri wiper write wrote Etui euro peri pert pier pity poet pore port pour pout prey prow pure puri pyre pyro quip quit Repo riot ripe rite rope ropy rote roti roup rout ryot Tier tire tiro tope tore piroque tori tory tour towy trey trio trip trop Trow troy true type typo tyre tyro weir wept wert wipe wire wiry Wore ter purity pyrite quoter";

	public List<Word> words = new List<Word>();
}
[System.Serializable]
public class MidWords
{
	public string tutorialWord = "Dahl agha hajj lass gaff alga dahs asks Gals lags jagg alas jags flak daff Lakh dash gala ashfalls hallahs Ashfall daggas skalds hallah salads Flasks flash skald kakas slags jaggs galas hadal Slash shahs gaffs algal dagga flask dahls daffs Lakhs shags halls glass sagas Kasha salad galls shall dadas falls salsa shads Flags alfas saga half sags fads dhal glad lash Kaka lads fallh hall gads alfa Adds gags gaga hash jaja Fafa ashg dash flash gas daka haka sasa";

	public List<Word> words = new List<Word>();
}[System.Serializable]
public class LowerWords
{
	public string tutorialWord = "cvv cvn cvm cvc cvb cv/ cv. cv, cv  cnc cnb cn/ cn. cn, cn cn cn cmz cmx cmv cmn cmm cmc cmb cm/ cm.cm, cm cm cm ccz cCx ccV ccn com coc ccb cc/ cc cbz cbx cbv cbn cbm cbc cbb cb/ cb.cb, cb cb cb c/z c/x c/v c/n  c/ c/ c.z c.x c.V c.n c.m c.c c.b c. / c..c., c.c.c.c, z c, x c, v c, n c, m c, c c, b c, / c, • c, , c, c, c, bzx bzv bzn bzm bzc bzb bz/ bz.bz, bz bz bz bxz bxx bxv bxn bxm bxc bxb bx/ bx.bx, bx bx bx bvz bvx bvv  ban bnm bac bnb bn/ bn.bn, bn bn bn bmz bmx bmv bmn bmm bmc bmb";

	public List<Word> words = new List<Word>();
}
