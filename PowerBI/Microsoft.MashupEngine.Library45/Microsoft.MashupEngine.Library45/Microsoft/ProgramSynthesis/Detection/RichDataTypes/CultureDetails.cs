using System;

namespace Microsoft.ProgramSynthesis.Detection.RichDataTypes
{
	// Token: 0x02000A79 RID: 2681
	public static class CultureDetails
	{
		// Token: 0x04001DEF RID: 7663
		public static readonly string[] NumberSeparators = new string[] { ",", "\u00a0", ".", "’", "$", "`", "/" };

		// Token: 0x04001DF0 RID: 7664
		public static readonly string[] CurrencySymbols = new string[]
		{
			"смн", "$", "₹", "kr", "¥", "Rs", "₽", "Q", "€", "FC",
			"Ft", "د.ع.\u200f", "R", "FCFA", "Ksh", "£", "Bs.", "TSh", "NAf.", "₮",
			"Bs", "R$", "Kz", "Nfk", "kr.", "Fdj", "₦", "MOP", "CFA", "FBu",
			"د.ب.\u200f", "KM", "RF", "IRR", "₩", "Br", "ብር", "៛", "CHF", "\u200b",
			"MAD", "ރ.", "Rp", "₪", "US$", "₼", "؋", "сўм", "₾", "Le",
			"SDG", "SR", "E", "₱", "UM", "КМ", "ر.ع.\u200f", "฿", "NT$", "GH₵",
			"र\u0941", "K", "USh", "MTn", "₲", "৳", "DA", "soʻm", "Kč", "RM",
			"₸", "₴", "FG", "ل.ل.\u200f", "د.ل.\u200f", "HK$", "дин.", "EC$", "L", "ⴷⵔ",
			"ден", "m.", "Afl.", "ܠ.ܣ.\u200f", "Nu.", "د.ج.\u200f", "XDR", "лв.", "ර\u0dd4.", "Lekë",
			"₡", "֏", "сом", "FCFP", "د.إ.\u200f", "STN", "zł", "lei", "ߖߕ.", "C$",
			"DT", "د.ت.\u200f", "₺", "ر.ي.\u200f", "RSD", "S/", "₫", "¤", "ر.س.\u200f", "kn",
			"MOP$", "Ar", "MK", "ر.ق.\u200f", "WS$", "CF", "د.ك.\u200f", "ريال", "T$", "P",
			"G", "D", "VT", "₭", "S", "den", "LS", "ج.س.", "ل.س.\u200f", "DH",
			"د.م.\u200f", "أ.م.\u200f", "B/.", "Rs.", "ج.م.\u200f", "د.ا.\u200f"
		};

		// Token: 0x04001DF1 RID: 7665
		public static readonly string[] NaNSymbols = new string[]
		{
			"NaN", "чыыһыла\u00a0буотах", "NeuN", "не\u00a0число", "epäluku", "ᠲᠤᠭᠠᠠ ᠪᠤᠰᠤ", "NS", "Micca numericu", "非數值", "ليس\u00a0رقم\u064bا",
			"epiloho", "¤¤¤", "ҳақиқий\u00a0сон\u00a0эмас", "არ\u00a0არის\u00a0რიცხვი", "mnn", "Терхьаш\u00a0дац", "НН", "ndaha’éi papaha", "war amdhan", "son\u00a0emas",
			"ND", "сан\u00a0емес", "ဂဏန\u103a\u1038မဟ\u102fတ\u103aသ\u1031\u102c", "ⵡⴰⵔ ⴰⵎⴹⴰⵏ", "san\u00a0däl", "ՈչԹ", "сан\u00a0эмес", "ߝߙߍߕߍ\u07eb ߕߍ\u07eb", "ناعدد", "MdM",
			"TF", "ບ\u0ecd\u0ec8\u200bແມ\u0ec8ນ\u200bໂຕ\u200bເລກ", "Non Numérique"
		};

		// Token: 0x04001DF2 RID: 7666
		public static readonly string[] PositiveInfinitySymbols = new string[] { "∞", "Infinity", "Infinito", "ᠡᠶ\u180bᠡᠷᠬᠦ ᠬᠢᠵᠠᠭᠠᠷᠭᠦᠢ ᠶᠠᠬᠡ", "+Infinit", "+Infinitu", "+ifedh", "+∞", "ߘߊ\u07ec\u07f2ߒߕߊ\u07eb\u07f2+", "+Infini" };

		// Token: 0x04001DF3 RID: 7667
		public static readonly string[] NegativeInfinitySymbols = new string[] { "-∞", "Infinity-", "-Infinito", "-Infinity", "ᠰᠦᠬᠡᠷᠬᠦ ᠬᠢᠵᠠᠭᠠᠷᠭᠦᠢ ᠶᠡᠬᠡ", "-Infinit", "-Infinitu", "-ifedh", "ߘߊ\u07ec\u07f2ߒߕߊ\u07eb\u07f2-", "-Infini" };

		// Token: 0x04001DF4 RID: 7668
		public static readonly string[][] NativeDigits = new string[][]
		{
			new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" },
			new string[] { "०", "१", "२", "३", "४", "५", "६", "७", "८", "९" },
			new string[] { "༠", "༡", "༢", "༣", "༤", "༥", "༦", "༧", "༨", "༩" },
			new string[] { "۰", "۱", "۲", "۳", "۴", "۵", "۶", "۷", "۸", "۹" },
			new string[] { "૦", "૧", "૨", "૩", "૪", "૫", "૬", "૭", "૮", "૯" },
			new string[] { "٠", "١", "٢", "٣", "٤", "٥", "٦", "٧", "٨", "٩" },
			new string[] { "০", "১", "২", "৩", "৪", "৫", "৬", "৭", "৮", "৯" },
			new string[] { "꘠", "꘡", "꘢", "꘣", "꘤", "꘥", "꘦", "꘧", "꘨", "꘩" },
			new string[] { "〇", "一", "二", "三", "四", "五", "六", "七", "八", "九" },
			new string[] { "០", "១", "២", "៣", "៤", "៥", "៦", "៧", "៨", "៩" },
			new string[] { "೦", "೧", "೨", "೩", "೪", "೫", "೬", "೭", "೮", "೯" },
			new string[] { "๐", "๑", "๒", "๓", "๔", "๕", "๖", "๗", "๘", "๙" },
			new string[] { "౦", "౧", "౨", "౩", "౪", "౫", "౬", "౭", "౮", "౯" },
			new string[] { "၀", "၁", "၂", "၃", "၄", "၅", "၆", "၇", "၈", "၉" },
			new string[] { "௦", "௧", "௨", "௩", "௪", "௫", "௬", "௭", "௮", "௯" },
			new string[] { "୦", "୧", "୨", "୩", "୪", "୫", "୬", "୭", "୮", "୯" },
			new string[] { "൦", "൧", "൨", "൩", "൪", "൫", "൬", "൭", "൮", "൯" },
			new string[] { "߀", "߁", "߂", "߃", "߄", "߅", "߆", "߇", "߈", "߉" },
			new string[] { "੦", "੧", "੨", "੩", "੪", "੫", "੬", "੭", "੮", "੯" },
			new string[] { "໐", "໑", "໒", "໓", "໔", "໕", "໖", "໗", "໘", "໙" }
		};
	}
}
