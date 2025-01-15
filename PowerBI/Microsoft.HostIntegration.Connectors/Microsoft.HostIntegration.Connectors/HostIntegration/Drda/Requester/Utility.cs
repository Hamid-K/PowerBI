using System;
using Microsoft.HostIntegration.Drda.Common;
using Microsoft.Win32;

namespace Microsoft.HostIntegration.Drda.Requester
{
	// Token: 0x0200099E RID: 2462
	internal class Utility
	{
		// Token: 0x06004C63 RID: 19555 RVA: 0x00131288 File Offset: 0x0012F488
		public static bool CheckRegistryKey(string key)
		{
			bool flag = false;
			RegistryKey registryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey("Software\\Microsoft\\Host Integration Server\\Data Integration");
			if (registryKey != null)
			{
				object value = registryKey.GetValue(key);
				if (value != null)
				{
					int num;
					if (int.TryParse(value.ToString(), out num) && num != 0)
					{
						flag = true;
					}
					return flag;
				}
			}
			RegistryKey registryKey2 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey("Software\\Microsoft\\Host Integration Server\\Data Integration");
			if (registryKey2 != null)
			{
				object value2 = registryKey2.GetValue(key);
				if (value2 != null)
				{
					int num2;
					if (int.TryParse(value2.ToString(), out num2) && num2 != 0)
					{
						flag = true;
					}
					return flag;
				}
			}
			return flag;
		}

		// Token: 0x06004C64 RID: 19556 RVA: 0x00131319 File Offset: 0x0012F519
		public static bool ParseBoolean(string value)
		{
			return Utility.ParseBoolean(value, false);
		}

		// Token: 0x06004C65 RID: 19557 RVA: 0x00131324 File Offset: 0x0012F524
		public static bool ParseBoolean(string value, bool defaultValue)
		{
			if (!string.IsNullOrWhiteSpace(value))
			{
				string text = value.ToUpperInvariant();
				return text == "YES" || text == "TRUE" || text == "T" || text == "Y" || text == "1";
			}
			return defaultValue;
		}

		// Token: 0x06004C66 RID: 19558 RVA: 0x00131388 File Offset: 0x0012F588
		public static Ccsid ParseCcsid(string value)
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				return null;
			}
			ushort num = 0;
			if (!ushort.TryParse(value, out num))
			{
				return null;
			}
			Ccsid ccsid = new Ccsid();
			ushort num2 = Utility.MapCodePageToCcsidCode(num);
			if (Utility.IsUnicode(num2))
			{
				ccsid._ccsiddbc = (int)num2;
				ccsid._ccsidsbc = (int)num2;
				ccsid._ccsidmbc = 0;
			}
			else if (Utility.IsGraphic(num2))
			{
				ccsid._ccsiddbc = (int)num2;
				ccsid._ccsidsbc = 0;
				ccsid._ccsidmbc = 0;
			}
			else if (Utility.IsMBCS(num2))
			{
				ccsid._ccsiddbc = (int)Utility.GetCcsidDbcFromCcsidMbc(num2);
				ccsid._ccsidsbc = (int)Utility.GetCcsidSbcFromCcsidMbc(num2);
				ccsid._ccsidmbc = (int)num2;
			}
			else
			{
				ccsid._ccsiddbc = (Utility.IsEbcdic(num2) ? 1200 : 1208);
				ccsid._ccsidsbc = (int)num2;
				ccsid._ccsidmbc = (int)num2;
			}
			return ccsid;
		}

		// Token: 0x06004C67 RID: 19559 RVA: 0x00131450 File Offset: 0x0012F650
		public static bool IsEbcdic(ushort codePage)
		{
			return codePage == 37 || codePage == 273 || codePage == 277 || codePage == 278 || codePage == 280 || codePage == 284 || codePage == 285 || codePage == 290 || codePage == 297 || codePage == 300 || codePage == 420 || codePage == 423 || codePage == 424 || codePage == 500 || codePage == 833 || codePage == 834 || codePage == 835 || codePage == 836 || codePage == 837 || codePage == 838 || codePage == 870 || codePage == 871 || codePage == 875 || codePage == 880 || codePage == 905 || codePage == 930 || codePage == 931 || codePage == 933 || codePage == 935 || codePage == 937 || codePage == 939 || codePage == 1025 || codePage == 1026 || codePage == 1027 || codePage == 1140 || codePage == 1141 || codePage == 1142 || codePage == 1143 || codePage == 1144 || codePage == 1145 || codePage == 1146 || codePage == 1147 || codePage == 1148 || codePage == 1149 || codePage == 4396 || codePage == 5026 || codePage == 5035 || codePage == 28709 || codePage == 1388 || codePage == 13124 || codePage == 4933;
		}

		// Token: 0x06004C68 RID: 19560 RVA: 0x0013165D File Offset: 0x0012F85D
		public static bool IsUnicode(ushort codePage)
		{
			return codePage == ushort.MaxValue || codePage == 13488 || codePage == 1200 || codePage == 1201;
		}

		// Token: 0x06004C69 RID: 19561 RVA: 0x00131681 File Offset: 0x0012F881
		public static bool IsGraphic(ushort codePage)
		{
			return codePage == 300 || codePage == 834 || codePage == 835 || codePage == 837 || codePage == 941 || codePage == 4396;
		}

		// Token: 0x06004C6A RID: 19562 RVA: 0x001316B8 File Offset: 0x0012F8B8
		public static bool IsMBCS(ushort codePage)
		{
			return codePage == 930 || codePage == 933 || codePage == 935 || codePage == 937 || codePage == 939 || codePage == 943 || codePage == 5026 || codePage == 5035 || codePage == 1381 || codePage == 1386 || codePage == 1208 || codePage == 5488 || codePage == 1388;
		}

		// Token: 0x06004C6B RID: 19563 RVA: 0x00131730 File Offset: 0x0012F930
		public static ushort MapCcsidCodeToCodePage(ushort ccsidCode)
		{
			if (ccsidCode <= 905)
			{
				if (ccsidCode <= 424)
				{
					if (ccsidCode <= 297)
					{
						if (ccsidCode == 273)
						{
							return 20273;
						}
						switch (ccsidCode)
						{
						case 277:
							return 20277;
						case 278:
							return 20278;
						case 279:
						case 281:
						case 282:
						case 283:
							break;
						case 280:
							return 20280;
						case 284:
							return 20284;
						case 285:
							return 20285;
						default:
							if (ccsidCode == 297)
							{
								return 20297;
							}
							break;
						}
					}
					else
					{
						if (ccsidCode == 300)
						{
							return 930;
						}
						if (ccsidCode == 367)
						{
							return 1252;
						}
						switch (ccsidCode)
						{
						case 420:
							return 20420;
						case 423:
							return 20423;
						case 424:
							return 20424;
						}
					}
				}
				else if (ccsidCode <= 838)
				{
					if (ccsidCode == 813)
					{
						return 28597;
					}
					if (ccsidCode == 819)
					{
						return 28591;
					}
					switch (ccsidCode)
					{
					case 833:
						return 933;
					case 834:
						return 933;
					case 835:
						return 937;
					case 836:
						return 935;
					case 837:
						return 935;
					case 838:
						return 20838;
					}
				}
				else
				{
					if (ccsidCode == 871)
					{
						return 20871;
					}
					if (ccsidCode == 880)
					{
						return 20880;
					}
					if (ccsidCode == 905)
					{
						return 20905;
					}
				}
			}
			else if (ccsidCode <= 1089)
			{
				if (ccsidCode <= 970)
				{
					switch (ccsidCode)
					{
					case 912:
						return 28592;
					case 913:
					case 914:
						break;
					case 915:
						return 28595;
					case 916:
						return 28598;
					default:
						switch (ccsidCode)
						{
						case 920:
							return 28599;
						case 921:
						case 922:
							break;
						case 923:
							return 28605;
						case 924:
							return 20924;
						default:
							if (ccsidCode == 970)
							{
								return 51949;
							}
							break;
						}
						break;
					}
				}
				else
				{
					if (ccsidCode == 971)
					{
						return 51949;
					}
					if (ccsidCode == 1025)
					{
						return 21025;
					}
					if (ccsidCode == 1089)
					{
						return 28596;
					}
				}
			}
			else if (ccsidCode <= 5026)
			{
				if (ccsidCode == 4396)
				{
					return 930;
				}
				if (ccsidCode == 4933)
				{
					return 1388;
				}
				if (ccsidCode == 5026)
				{
					return 930;
				}
			}
			else if (ccsidCode <= 13124)
			{
				if (ccsidCode == 5035)
				{
					return 939;
				}
				if (ccsidCode == 13124)
				{
					return 1388;
				}
			}
			else
			{
				if (ccsidCode == 13488)
				{
					return 1201;
				}
				if (ccsidCode == 28709)
				{
					return 937;
				}
			}
			return ccsidCode;
		}

		// Token: 0x06004C6C RID: 19564 RVA: 0x00131A34 File Offset: 0x0012FC34
		public static ushort MapCodePageToCcsidCode(ushort codePage)
		{
			if (codePage <= 20838)
			{
				if (codePage <= 20285)
				{
					if (codePage == 367)
					{
						return 1252;
					}
					if (codePage == 20273)
					{
						return 273;
					}
					switch (codePage)
					{
					case 20277:
						return 277;
					case 20278:
						return 278;
					case 20280:
						return 280;
					case 20284:
						return 284;
					case 20285:
						return 285;
					}
				}
				else
				{
					if (codePage == 20297)
					{
						return 297;
					}
					switch (codePage)
					{
					case 20420:
						return 420;
					case 20421:
					case 20422:
						break;
					case 20423:
						return 423;
					case 20424:
						return 424;
					default:
						if (codePage == 20838)
						{
							return 838;
						}
						break;
					}
				}
			}
			else if (codePage <= 20905)
			{
				if (codePage == 20871)
				{
					return 871;
				}
				if (codePage == 20880)
				{
					return 880;
				}
				if (codePage == 20905)
				{
					return 905;
				}
			}
			else
			{
				if (codePage == 20924)
				{
					return 924;
				}
				if (codePage == 21025)
				{
					return 1025;
				}
				switch (codePage)
				{
				case 28591:
					return 819;
				case 28592:
					return 912;
				case 28595:
					return 915;
				case 28596:
					return 1089;
				case 28597:
					return 813;
				case 28598:
					return 916;
				case 28599:
					return 920;
				case 28605:
					return 923;
				}
			}
			return codePage;
		}

		// Token: 0x06004C6D RID: 19565 RVA: 0x00131C04 File Offset: 0x0012FE04
		public static ushort GetCcsidSbcFromCcsidMbc(ushort codePage)
		{
			if (codePage <= 1381)
			{
				if (codePage <= 939)
				{
					if (codePage == 930)
					{
						return 290;
					}
					switch (codePage)
					{
					case 933:
						return 833;
					case 935:
						return 836;
					case 937:
						return 28709;
					case 939:
						return 1027;
					}
				}
				else
				{
					if (codePage == 943)
					{
						return 897;
					}
					if (codePage == 1208)
					{
						return 1208;
					}
					if (codePage == 1381)
					{
						return 1115;
					}
				}
			}
			else if (codePage <= 1388)
			{
				if (codePage == 1386)
				{
					return 1114;
				}
				if (codePage == 1388)
				{
					return 13124;
				}
			}
			else
			{
				if (codePage == 5026)
				{
					return 290;
				}
				if (codePage == 5035)
				{
					return 1027;
				}
				if (codePage == 5488)
				{
					return 5488;
				}
			}
			return 0;
		}

		// Token: 0x06004C6E RID: 19566 RVA: 0x00131CF8 File Offset: 0x0012FEF8
		public static ushort GetCcsidDbcFromCcsidMbc(ushort codePage)
		{
			if (codePage <= 1381)
			{
				if (codePage <= 939)
				{
					if (codePage == 930)
					{
						return 300;
					}
					switch (codePage)
					{
					case 933:
						return 834;
					case 935:
						return 837;
					case 937:
						return 835;
					case 939:
						return 300;
					}
				}
				else
				{
					if (codePage == 943)
					{
						return 941;
					}
					if (codePage == 1208)
					{
						return 1200;
					}
					if (codePage == 1381)
					{
						return 1380;
					}
				}
			}
			else if (codePage <= 1388)
			{
				if (codePage == 1386)
				{
					return 1385;
				}
				if (codePage == 1388)
				{
					return 4933;
				}
			}
			else
			{
				if (codePage == 5026)
				{
					return 4396;
				}
				if (codePage == 5035)
				{
					return 4396;
				}
				if (codePage == 5488)
				{
					return 5488;
				}
			}
			return 0;
		}
	}
}
