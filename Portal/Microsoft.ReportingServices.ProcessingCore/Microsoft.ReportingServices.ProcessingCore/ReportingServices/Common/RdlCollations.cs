using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x020005C3 RID: 1475
	internal static class RdlCollations
	{
		// Token: 0x17001EEB RID: 7915
		// (get) Token: 0x0600534E RID: 21326 RVA: 0x0015E940 File Offset: 0x0015CB40
		internal static Dictionary<string, uint> Collations
		{
			get
			{
				return RdlCollations.m_sqlCollations2LCID;
			}
		}

		// Token: 0x0600534F RID: 21327 RVA: 0x0015E948 File Offset: 0x0015CB48
		static RdlCollations()
		{
			List<KeyValuePair<string, uint>> list = new List<KeyValuePair<string, uint>>();
			list.Add(new KeyValuePair<string, uint>("Albanian", 1052U));
			list.Add(new KeyValuePair<string, uint>("Albanian_100", 1052U));
			list.Add(new KeyValuePair<string, uint>("Amharic_100", 1067U));
			list.Add(new KeyValuePair<string, uint>("Arabic", 1025U));
			list.Add(new KeyValuePair<string, uint>("Arabic_100", 1025U));
			list.Add(new KeyValuePair<string, uint>("Armenian_100", 1067U));
			list.Add(new KeyValuePair<string, uint>("Assamese_100", 1101U));
			list.Add(new KeyValuePair<string, uint>("Azeri_Cyrillic_90", 2092U));
			list.Add(new KeyValuePair<string, uint>("Azeri_Cyrillic_100", 2092U));
			list.Add(new KeyValuePair<string, uint>("Azeri_Latin_90", 1068U));
			list.Add(new KeyValuePair<string, uint>("Azeri_Latin_100", 1068U));
			list.Add(new KeyValuePair<string, uint>("Bashkir_100", 1133U));
			list.Add(new KeyValuePair<string, uint>("Bengali_100", 1081U));
			list.Add(new KeyValuePair<string, uint>("Bosnian_Cyrillic_100", 1026U));
			list.Add(new KeyValuePair<string, uint>("Bosnian_Latin_100", 1050U));
			list.Add(new KeyValuePair<string, uint>("Breton_100", 1150U));
			list.Add(new KeyValuePair<string, uint>("Chinese_Hong_Kong_Stroke_90", 3076U));
			list.Add(new KeyValuePair<string, uint>("Chinese_Hong_Kong_Stroke_100", 3076U));
			list.Add(new KeyValuePair<string, uint>("Chinese_Macao_100", 5124U));
			list.Add(new KeyValuePair<string, uint>("Chinese_Macao_Stroke_100", 136196U));
			list.Add(new KeyValuePair<string, uint>("Chinese_PRC", 2052U));
			list.Add(new KeyValuePair<string, uint>("Chinese_PRC_100", 2052U));
			list.Add(new KeyValuePair<string, uint>("Chinese_PRC_Stroke", 133124U));
			list.Add(new KeyValuePair<string, uint>("Chinese_PRC_Stroke_100", 133124U));
			list.Add(new KeyValuePair<string, uint>("Chinese_Simplified_Pinyin_100", 2052U));
			list.Add(new KeyValuePair<string, uint>("Chinese_Simplified_Stroke_Order_100", 133124U));
			list.Add(new KeyValuePair<string, uint>("Chinese_Taiwan_Bopomofo", 197636U));
			list.Add(new KeyValuePair<string, uint>("Chinese_Taiwan_Bopomofo_100", 197636U));
			list.Add(new KeyValuePair<string, uint>("Chinese_Taiwan_Stroke", 1028U));
			list.Add(new KeyValuePair<string, uint>("Chinese_Taiwan_Stroke_100", 1028U));
			list.Add(new KeyValuePair<string, uint>("Chinese_Traditional_Bopomofo_100", 197636U));
			list.Add(new KeyValuePair<string, uint>("Chinese_Traditional_Pinyin_100", 5124U));
			list.Add(new KeyValuePair<string, uint>("Chinese_Traditional_Stroke_Count_100", 1028U));
			list.Add(new KeyValuePair<string, uint>("Chinese_Traditional_Stroke_Order_100", 136196U));
			list.Add(new KeyValuePair<string, uint>("Corsican_100", 1155U));
			list.Add(new KeyValuePair<string, uint>("Croatian", 1050U));
			list.Add(new KeyValuePair<string, uint>("Croatian_100", 1050U));
			list.Add(new KeyValuePair<string, uint>("Cyrillic_General", 1049U));
			list.Add(new KeyValuePair<string, uint>("Cyrillic_General_100", 1049U));
			list.Add(new KeyValuePair<string, uint>("Czech", 1029U));
			list.Add(new KeyValuePair<string, uint>("Czech_100", 1029U));
			list.Add(new KeyValuePair<string, uint>("Danish_Greenlandic_100", 1030U));
			list.Add(new KeyValuePair<string, uint>("Danish_Norwegian", 1030U));
			list.Add(new KeyValuePair<string, uint>("Dari_100", 1164U));
			list.Add(new KeyValuePair<string, uint>("Divehi_90", 1125U));
			list.Add(new KeyValuePair<string, uint>("Divehi_100", 1125U));
			list.Add(new KeyValuePair<string, uint>("Estonian", 1061U));
			list.Add(new KeyValuePair<string, uint>("Estonian_100", 1061U));
			list.Add(new KeyValuePair<string, uint>("Finnish_Swedish", 1035U));
			list.Add(new KeyValuePair<string, uint>("Finnish_Swedish_100", 1035U));
			list.Add(new KeyValuePair<string, uint>("French", 1036U));
			list.Add(new KeyValuePair<string, uint>("French_100", 1036U));
			list.Add(new KeyValuePair<string, uint>("Frisian_100", 1122U));
			list.Add(new KeyValuePair<string, uint>("Georgian_Modern_Sort", 66615U));
			list.Add(new KeyValuePair<string, uint>("Georgian_Traditional_100", 1067U));
			list.Add(new KeyValuePair<string, uint>("German_PhoneBook", 66567U));
			list.Add(new KeyValuePair<string, uint>("German_PhoneBook_100", 66567U));
			list.Add(new KeyValuePair<string, uint>("Greek", 1032U));
			list.Add(new KeyValuePair<string, uint>("Greek_100", 1032U));
			list.Add(new KeyValuePair<string, uint>("Hebrew", 1037U));
			list.Add(new KeyValuePair<string, uint>("Hebrew_100", 1037U));
			list.Add(new KeyValuePair<string, uint>("Hindi", 1081U));
			list.Add(new KeyValuePair<string, uint>("Hungarian", 1038U));
			list.Add(new KeyValuePair<string, uint>("Hungarian_100", 1038U));
			list.Add(new KeyValuePair<string, uint>("Hungarian_Technical", 66574U));
			list.Add(new KeyValuePair<string, uint>("Hungarian_Technical_100", 66574U));
			list.Add(new KeyValuePair<string, uint>("Icelandic", 1039U));
			list.Add(new KeyValuePair<string, uint>("Icelandic_100", 1039U));
			list.Add(new KeyValuePair<string, uint>("Indic_General_90", 1081U));
			list.Add(new KeyValuePair<string, uint>("Indic_General_100", 1081U));
			list.Add(new KeyValuePair<string, uint>("Inuktitut_100", 1027U));
			list.Add(new KeyValuePair<string, uint>("Japanese", 1041U));
			list.Add(new KeyValuePair<string, uint>("Japanese_90", 1041U));
			list.Add(new KeyValuePair<string, uint>("Japanese_100", 1041U));
			list.Add(new KeyValuePair<string, uint>("Japanese_Bushu_Kakusu_100", 263185U));
			list.Add(new KeyValuePair<string, uint>("Japanese_Radical_Stroke_100", 263185U));
			list.Add(new KeyValuePair<string, uint>("Japanese_Unicode", 66577U));
			list.Add(new KeyValuePair<string, uint>("Kazakh_90", 1087U));
			list.Add(new KeyValuePair<string, uint>("Kazakh_100", 1087U));
			list.Add(new KeyValuePair<string, uint>("Khmer_100", 1107U));
			list.Add(new KeyValuePair<string, uint>("Korean", 1042U));
			list.Add(new KeyValuePair<string, uint>("Korean_90", 1042U));
			list.Add(new KeyValuePair<string, uint>("Korean_100", 1042U));
			list.Add(new KeyValuePair<string, uint>("Korean_Wansung", 1042U));
			list.Add(new KeyValuePair<string, uint>("Korean_Wansung_Unicode", 66578U));
			list.Add(new KeyValuePair<string, uint>("Lao_100", 1108U));
			list.Add(new KeyValuePair<string, uint>("Latin1_General", 1033U));
			list.Add(new KeyValuePair<string, uint>("Latin1_General_100", 1033U));
			list.Add(new KeyValuePair<string, uint>("Latvian", 1062U));
			list.Add(new KeyValuePair<string, uint>("Latvian_100", 1062U));
			list.Add(new KeyValuePair<string, uint>("Lithuanian", 1063U));
			list.Add(new KeyValuePair<string, uint>("Lithuanian_100", 1063U));
			list.Add(new KeyValuePair<string, uint>("Lithuanian_Classic", 2087U));
			list.Add(new KeyValuePair<string, uint>("Macedonian", 1071U));
			list.Add(new KeyValuePair<string, uint>("Macedonian_FYROM_90", 1071U));
			list.Add(new KeyValuePair<string, uint>("Macedonian_FYROM_100", 1071U));
			list.Add(new KeyValuePair<string, uint>("Maltese_100", 1082U));
			list.Add(new KeyValuePair<string, uint>("Maori_100", 1153U));
			list.Add(new KeyValuePair<string, uint>("Mapudungan_100", 1146U));
			list.Add(new KeyValuePair<string, uint>("Modern_Spanish", 3082U));
			list.Add(new KeyValuePair<string, uint>("Modern_Spanish_100", 3082U));
			list.Add(new KeyValuePair<string, uint>("Mohawk_100", 1148U));
			list.Add(new KeyValuePair<string, uint>("Mongolian_100", 1067U));
			list.Add(new KeyValuePair<string, uint>("Nepali_100", 1121U));
			list.Add(new KeyValuePair<string, uint>("Norwegian_100", 1044U));
			list.Add(new KeyValuePair<string, uint>("Norwegian_Sami_100", 1083U));
			list.Add(new KeyValuePair<string, uint>("Pashto_100", 1123U));
			list.Add(new KeyValuePair<string, uint>("Persian_100", 1065U));
			list.Add(new KeyValuePair<string, uint>("Polish", 1045U));
			list.Add(new KeyValuePair<string, uint>("Polish_100", 1045U));
			list.Add(new KeyValuePair<string, uint>("Romanian", 1048U));
			list.Add(new KeyValuePair<string, uint>("Romanian_100", 1048U));
			list.Add(new KeyValuePair<string, uint>("Romansh_100", 1047U));
			list.Add(new KeyValuePair<string, uint>("Sami_Norway_100", 1083U));
			list.Add(new KeyValuePair<string, uint>("Sami_Sweden_Finland_100", 2107U));
			list.Add(new KeyValuePair<string, uint>("Serbian_Cyrillic_100", 3098U));
			list.Add(new KeyValuePair<string, uint>("Serbian_Latin_100", 1050U));
			list.Add(new KeyValuePair<string, uint>("Slovak", 1051U));
			list.Add(new KeyValuePair<string, uint>("Slovak_100", 1051U));
			list.Add(new KeyValuePair<string, uint>("Slovenian", 1060U));
			list.Add(new KeyValuePair<string, uint>("Slovenian_100", 1060U));
			list.Add(new KeyValuePair<string, uint>("Swedish_Finnish_Sami_100", 1035U));
			list.Add(new KeyValuePair<string, uint>("Syriac_90", 1114U));
			list.Add(new KeyValuePair<string, uint>("Syriac_100", 1114U));
			list.Add(new KeyValuePair<string, uint>("Tatar_90", 1092U));
			list.Add(new KeyValuePair<string, uint>("Tatar_100", 1092U));
			list.Add(new KeyValuePair<string, uint>("Tamazight_100", 2143U));
			list.Add(new KeyValuePair<string, uint>("Thai", 1054U));
			list.Add(new KeyValuePair<string, uint>("Thai_100", 1054U));
			list.Add(new KeyValuePair<string, uint>("Tibetan_PRC_100", 1105U));
			list.Add(new KeyValuePair<string, uint>("Traditional_Spanish", 1034U));
			list.Add(new KeyValuePair<string, uint>("Traditional_Spanish_100", 1034U));
			list.Add(new KeyValuePair<string, uint>("Turkish", 1055U));
			list.Add(new KeyValuePair<string, uint>("Turkish_100", 1055U));
			list.Add(new KeyValuePair<string, uint>("Turkmen_100", 1090U));
			list.Add(new KeyValuePair<string, uint>("Uighur_PRC_100", 1152U));
			list.Add(new KeyValuePair<string, uint>("Ukrainian", 1058U));
			list.Add(new KeyValuePair<string, uint>("Ukrainian_100", 1058U));
			list.Add(new KeyValuePair<string, uint>("Upper_Sorbian_100", 1070U));
			list.Add(new KeyValuePair<string, uint>("Urdu_100", 1056U));
			list.Add(new KeyValuePair<string, uint>("Uzbek_Latin_90", 1091U));
			list.Add(new KeyValuePair<string, uint>("Uzbek_Latin_100", 1091U));
			list.Add(new KeyValuePair<string, uint>("Vietnamese", 1066U));
			list.Add(new KeyValuePair<string, uint>("Vietnamese_100", 1066U));
			list.Add(new KeyValuePair<string, uint>("Welsh_100", 1106U));
			list.Add(new KeyValuePair<string, uint>("Yakut_100", 1157U));
			list.Add(new KeyValuePair<string, uint>("Yi_100", 1067U));
			RdlCollations.m_sqlCollations2LCID = new Dictionary<string, uint>(list.Count);
			foreach (KeyValuePair<string, uint> keyValuePair in list)
			{
				RdlCollations.m_sqlCollations2LCID.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x040029F4 RID: 10740
		private static Dictionary<string, uint> m_sqlCollations2LCID;
	}
}
