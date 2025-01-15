using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using Microsoft.HostIntegration.ConfigurationSectionHandlers.Encoding;
using Microsoft.HostIntegration.StrictResources.Nls;

namespace Microsoft.HostIntegration.Nls
{
	// Token: 0x0200062B RID: 1579
	public abstract class HisEncoding : Encoding
	{
		// Token: 0x17000B81 RID: 2945
		// (get) Token: 0x06003525 RID: 13605 RVA: 0x000B24E4 File Offset: 0x000B06E4
		// (set) Token: 0x06003526 RID: 13606 RVA: 0x000B24EC File Offset: 0x000B06EC
		public bool AutomaticShiftInShiftOut
		{
			get
			{
				return this.autoSISO;
			}
			set
			{
				this.autoSISO = value;
			}
		}

		// Token: 0x17000B82 RID: 2946
		// (get) Token: 0x06003527 RID: 13607 RVA: 0x000B24F5 File Offset: 0x000B06F5
		// (set) Token: 0x06003528 RID: 13608 RVA: 0x000B24FD File Offset: 0x000B06FD
		public bool DoubleByteStart { get; set; }

		// Token: 0x17000B83 RID: 2947
		// (get) Token: 0x06003529 RID: 13609 RVA: 0x000B2506 File Offset: 0x000B0706
		// (set) Token: 0x0600352A RID: 13610 RVA: 0x000B250E File Offset: 0x000B070E
		public bool Truncate { get; set; }

		// Token: 0x17000B84 RID: 2948
		// (get) Token: 0x0600352B RID: 13611 RVA: 0x000B2517 File Offset: 0x000B0717
		// (set) Token: 0x0600352C RID: 13612 RVA: 0x000B251F File Offset: 0x000B071F
		public bool UseEnhancedEbcdicTable
		{
			get
			{
				return this.useIBMTable;
			}
			set
			{
				this.useIBMTable = value;
			}
		}

		// Token: 0x17000B85 RID: 2949
		// (get) Token: 0x0600352D RID: 13613 RVA: 0x000B2528 File Offset: 0x000B0728
		// (set) Token: 0x0600352E RID: 13614 RVA: 0x000B2530 File Offset: 0x000B0730
		public bool DontUsePresentationFormsB
		{
			get
			{
				return this.dontUsePresentationFormsB;
			}
			set
			{
				this.dontUsePresentationFormsB = value;
			}
		}

		// Token: 0x17000B86 RID: 2950
		// (get) Token: 0x0600352F RID: 13615 RVA: 0x000B2539 File Offset: 0x000B0739
		// (set) Token: 0x06003530 RID: 13616 RVA: 0x000B2555 File Offset: 0x000B0755
		public bool UsePhysicalOrderForCodePage420
		{
			get
			{
				return this.usePhysicalOrderForCodePage420 != null && this.usePhysicalOrderForCodePage420.Value;
			}
			set
			{
				this.usePhysicalOrderForCodePage420 = new bool?(value);
			}
		}

		// Token: 0x17000B87 RID: 2951
		// (get) Token: 0x06003531 RID: 13617 RVA: 0x000B2563 File Offset: 0x000B0763
		public override int CodePage
		{
			get
			{
				return (int)this.destinationCodePage;
			}
		}

		// Token: 0x17000B88 RID: 2952
		// (get) Token: 0x06003532 RID: 13618 RVA: 0x000B256B File Offset: 0x000B076B
		public override int WindowsCodePage
		{
			get
			{
				return (int)this.windowsCodePage;
			}
		}

		// Token: 0x17000B89 RID: 2953
		// (get) Token: 0x06003533 RID: 13619 RVA: 0x00006F04 File Offset: 0x00005104
		public override bool IsBrowserDisplay
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000B8A RID: 2954
		// (get) Token: 0x06003534 RID: 13620 RVA: 0x00006F04 File Offset: 0x00005104
		public override bool IsBrowserSave
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000B8B RID: 2955
		// (get) Token: 0x06003535 RID: 13621 RVA: 0x00006F04 File Offset: 0x00005104
		public override bool IsMailNewsDisplay
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000B8C RID: 2956
		// (get) Token: 0x06003536 RID: 13622 RVA: 0x00006F04 File Offset: 0x00005104
		public override bool IsMailNewsSave
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000B8D RID: 2957
		// (get) Token: 0x06003537 RID: 13623 RVA: 0x00006F04 File Offset: 0x00005104
		public new bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000B8E RID: 2958
		// (get) Token: 0x06003538 RID: 13624 RVA: 0x000B2573 File Offset: 0x000B0773
		public override string EncodingName
		{
			get
			{
				return this.CodePageName;
			}
		}

		// Token: 0x17000B8F RID: 2959
		// (get) Token: 0x06003539 RID: 13625 RVA: 0x000B2573 File Offset: 0x000B0773
		public override string BodyName
		{
			get
			{
				return this.CodePageName;
			}
		}

		// Token: 0x17000B90 RID: 2960
		// (get) Token: 0x0600353A RID: 13626 RVA: 0x000B2573 File Offset: 0x000B0773
		public override string HeaderName
		{
			get
			{
				return this.CodePageName;
			}
		}

		// Token: 0x17000B91 RID: 2961
		// (get) Token: 0x0600353B RID: 13627 RVA: 0x000B2573 File Offset: 0x000B0773
		public override string WebName
		{
			get
			{
				return this.CodePageName;
			}
		}

		// Token: 0x17000B92 RID: 2962
		// (get) Token: 0x0600353C RID: 13628 RVA: 0x000B257B File Offset: 0x000B077B
		internal Encoding WindowsEncoding
		{
			get
			{
				return this.windowsEncoding;
			}
		}

		// Token: 0x17000B93 RID: 2963
		// (get) Token: 0x0600353D RID: 13629 RVA: 0x000B2583 File Offset: 0x000B0783
		internal HisConverter Converter
		{
			get
			{
				return this.converter;
			}
		}

		// Token: 0x17000B94 RID: 2964
		// (get) Token: 0x0600353E RID: 13630 RVA: 0x000B258C File Offset: 0x000B078C
		internal bool IsShiftInOutCodePage
		{
			get
			{
				HisEncoding.HostCodePages hostCodePages = this.destinationCodePage;
				if (hostCodePages <= HisEncoding.HostCodePages.EBCDICJapaneseExtendedLowerEnglishKanji_939)
				{
					if (hostCodePages != HisEncoding.HostCodePages.EBCDICJapaneseKatakana_290)
					{
						switch (hostCodePages)
						{
						case HisEncoding.HostCodePages.EBCDICJapaneseKatakanaKanji_290:
						case HisEncoding.HostCodePages.EBCDICJapaneseLowerEnglishAndKanji_931:
						case HisEncoding.HostCodePages.EBCDICKorean_933:
						case HisEncoding.HostCodePages.EBCDICSimplifiedChinese_935:
						case HisEncoding.HostCodePages.EBCDICTraditionalChinese_937:
						case HisEncoding.HostCodePages.EBCDICJapaneseExtendedLowerEnglishKanji_939:
							break;
						case (HisEncoding.HostCodePages)932:
						case (HisEncoding.HostCodePages)934:
						case (HisEncoding.HostCodePages)936:
						case (HisEncoding.HostCodePages)938:
							return false;
						default:
							return false;
						}
					}
				}
				else if (hostCodePages != HisEncoding.HostCodePages.EBCDICJapaneseExtendedLowerEnglish_1027 && hostCodePages != HisEncoding.HostCodePages.EBCDICJapaneseKatakanaKanji_5026 && hostCodePages != HisEncoding.HostCodePages.EBCDICJapaneseLatinKanji_5035)
				{
					return false;
				}
				return true;
			}
		}

		// Token: 0x17000B95 RID: 2965
		// (get) Token: 0x0600353F RID: 13631 RVA: 0x000B2604 File Offset: 0x000B0804
		internal bool IsMBCSCodePage
		{
			get
			{
				HisEncoding.HostCodePages hostCodePages = this.destinationCodePage;
				if (hostCodePages <= HisEncoding.HostCodePages.EBCDICJapaneseKatakanaKanji_5026)
				{
					if (hostCodePages <= HisEncoding.HostCodePages.EBCDICJapaneseExtendedLowerEnglishKanji_939)
					{
						if (hostCodePages != HisEncoding.HostCodePages.EBCDICJapaneseKatakana_290)
						{
							switch (hostCodePages)
							{
							case HisEncoding.HostCodePages.EBCDICJapaneseKatakanaKanji_290:
							case HisEncoding.HostCodePages.EBCDICJapaneseLowerEnglishAndKanji_931:
							case HisEncoding.HostCodePages.EBCDICKorean_933:
							case HisEncoding.HostCodePages.EBCDICSimplifiedChinese_935:
							case HisEncoding.HostCodePages.EBCDICTraditionalChinese_937:
							case HisEncoding.HostCodePages.EBCDICJapaneseExtendedLowerEnglishKanji_939:
								break;
							case (HisEncoding.HostCodePages)932:
							case (HisEncoding.HostCodePages)934:
							case (HisEncoding.HostCodePages)936:
							case (HisEncoding.HostCodePages)938:
								return false;
							default:
								return false;
							}
						}
					}
					else if (hostCodePages != HisEncoding.HostCodePages.EBCDICJapaneseExtendedLowerEnglish_1027 && hostCodePages != HisEncoding.HostCodePages.EBCDICJapaneseKatakanaKanji_5026)
					{
						return false;
					}
				}
				else if (hostCodePages <= HisEncoding.HostCodePages.EBCDICArabic_420)
				{
					if (hostCodePages != HisEncoding.HostCodePages.EBCDICJapaneseLatinKanji_5035 && hostCodePages != HisEncoding.HostCodePages.EBCDICArabic_420)
					{
						return false;
					}
				}
				else if (hostCodePages != HisEncoding.HostCodePages.EBCDICHebrew_424 && hostCodePages != HisEncoding.HostCodePages.CLRCodePageGB18030)
				{
					return false;
				}
				return true;
			}
		}

		// Token: 0x06003540 RID: 13632 RVA: 0x000B26A5 File Offset: 0x000B08A5
		internal HisEncoding(HisEncoding.HostCodePages hostCP)
		{
			this.hisCustomEncodingMaps = null;
			this.Intialize(hostCP);
		}

		// Token: 0x06003541 RID: 13633 RVA: 0x000B26D8 File Offset: 0x000B08D8
		static HisEncoding()
		{
			EncodingInfo[] encodings = Encoding.GetEncodings();
			HisEncoding.hisEncodinginfos = new SortedList<int, HisEncodingInfo>();
			HisEncoding.hisEncodinginfos.Add(290, new HisEncodingInfo(290, "IBM EBCDIC (Japan Katakana)", "IBM290", false));
			HisEncoding.hisEncodinginfos.Add(930, new HisEncodingInfo(930, "IBM EBCDIC (Japan Katakana/Kanji)", "IBM930", false));
			HisEncoding.hisEncodinginfos.Add(931, new HisEncodingInfo(931, "IBM EBCDIC (Japanese)", "IBM931", false));
			HisEncoding.hisEncodinginfos.Add(939, new HisEncodingInfo(939, "IBM EBCDIC (Japan English/Kanji)", "IBM939", false));
			HisEncoding.hisEncodinginfos.Add(5026, new HisEncodingInfo(5026, "IBM EBCDIC (Japan Katakana/Kanji)", "IBM5026", false));
			HisEncoding.hisEncodinginfos.Add(5035, new HisEncodingInfo(5035, "IBM EBCDIC (Japan English/Kanji)", "IBM5035", false));
			HisEncoding.hisEncodinginfos.Add(933, new HisEncodingInfo(933, "IBM EBCDIC (Korea)", "IBM933", false));
			HisEncoding.hisEncodinginfos.Add(935, new HisEncodingInfo(935, "IBM EBCDIC (Simplified Chinese)", "IBM935", false));
			HisEncoding.hisEncodinginfos.Add(937, new HisEncodingInfo(937, "IBM EBCDIC (Traditional Chinese)", "IBM937", false));
			HisEncoding.hisEncodinginfos.Add(20420, new HisEncodingInfo(20420, "IBM EBCDIC (Arabic)", "IBM420", false));
			HisEncoding.hisEncodinginfos.Add(20424, new HisEncodingInfo(20424, "IBM EBCDIC (Hebrew)", "IBM424", false));
			if (HisEncoding.config == null)
			{
				HisEncoding.config = new HisEncodingConfiguration(ConfigurationManager.GetSection("hostIntegration.encoding") as EncodingConfigurationSectionHandler);
			}
			HisEncoding.config.AddEuroReplacementPages();
			foreach (HisCustomEncodingMappings hisCustomEncodingMappings in HisEncoding.config.CustomEncodings)
			{
				if (!HisEncoding.hisEncodinginfos.ContainsKey(hisCustomEncodingMappings.CodePage))
				{
					HisEncoding.hisEncodinginfos.Add(hisCustomEncodingMappings.CodePage, new HisEncodingInfo(hisCustomEncodingMappings.CodePage, hisCustomEncodingMappings.Description, hisCustomEncodingMappings.Name, !hisCustomEncodingMappings.IsEuroReplacement));
				}
			}
			foreach (EncodingInfo encodingInfo in encodings)
			{
				if (!HisEncoding.hisEncodinginfos.ContainsKey(encodingInfo.CodePage))
				{
					HisEncoding.hisEncodinginfos.Add(encodingInfo.CodePage, new HisEncodingInfo(encodingInfo.CodePage, encodingInfo.DisplayName, encodingInfo.Name));
				}
			}
		}

		// Token: 0x06003542 RID: 13634 RVA: 0x000B2988 File Offset: 0x000B0B88
		internal virtual void Intialize(HisEncoding.HostCodePages hostCP)
		{
			this.destinationCodePage = hostCP;
			this.windowsCodePage = this.GetCorrespondingWindowsCodePage();
			this.windowsEncoding = Encoding.GetEncoding((int)this.windowsCodePage);
			if (this.IsMBCSCodePage && this.IsShiftInOutCodePage)
			{
				this.autoSISO = true;
				return;
			}
			this.autoSISO = false;
		}

		// Token: 0x06003543 RID: 13635 RVA: 0x000B29D8 File Offset: 0x000B0BD8
		public new static HisEncoding GetEncoding(int codepage)
		{
			if (codepage < 0 || codepage > 65535)
			{
				throw new ArgumentOutOfRangeException(SR.InvalidCodePage(codepage));
			}
			HisEncoding.HostCodePages hostCodePages = HisEncoding.FixCodePage((HisEncoding.HostCodePages)codepage);
			foreach (HisCustomEncodingMappings hisCustomEncodingMappings in HisEncoding.config.CustomEncodings)
			{
				if (hisCustomEncodingMappings.CodePage == (int)hostCodePages)
				{
					return new HisCustomEncoding(hostCodePages, hisCustomEncodingMappings);
				}
			}
			return HisEncoding.CreateEncoding(hostCodePages);
		}

		// Token: 0x06003544 RID: 13636 RVA: 0x000B2A68 File Offset: 0x000B0C68
		public new static HisEncoding GetEncoding(string encodingname)
		{
			string text = encodingname.ToUpperInvariant();
			HisEncoding.HostCodePages hostCodePages = HisEncoding.HostCodePages.InvalidCodePage;
			HisEncoding hisEncoding = null;
			if (text != null)
			{
				uint num = <e899bf4c-7db4-471c-b532-60afd93e9d52><PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 1918073399U)
				{
					if (num <= 1834185304U)
					{
						if (num != 1767074828U)
						{
							if (num == 1834185304U)
							{
								if (text == "IBM935")
								{
									hostCodePages = HisEncoding.HostCodePages.EBCDICSimplifiedChinese_935;
									goto IL_01E2;
								}
							}
						}
						else if (text == "IBM939")
						{
							hostCodePages = HisEncoding.HostCodePages.EBCDICJapaneseExtendedLowerEnglishKanji_939;
							goto IL_01E2;
						}
					}
					else if (num != 1867740542U)
					{
						if (num != 1901295780U)
						{
							if (num == 1918073399U)
							{
								if (text == "IBM930")
								{
									hostCodePages = HisEncoding.HostCodePages.EBCDICJapaneseKatakanaKanji_290;
									goto IL_01E2;
								}
							}
						}
						else if (text == "IBM931")
						{
							hostCodePages = HisEncoding.HostCodePages.EBCDICJapaneseLowerEnglishAndKanji_931;
							goto IL_01E2;
						}
					}
					else if (text == "IBM937")
					{
						hostCodePages = HisEncoding.HostCodePages.EBCDICTraditionalChinese_937;
						goto IL_01E2;
					}
				}
				else if (num <= 2385129153U)
				{
					if (num != 1934851018U)
					{
						if (num != 2105876412U)
						{
							if (num == 2385129153U)
							{
								if (text == "IBM420")
								{
									hostCodePages = HisEncoding.HostCodePages.EBCDICArabic_420;
									goto IL_01E2;
								}
							}
						}
						else if (text == "IBM5035")
						{
							hostCodePages = HisEncoding.HostCodePages.EBCDICJapaneseLatinKanji_5035;
							goto IL_01E2;
						}
					}
					else if (text == "IBM933")
					{
						hostCodePages = HisEncoding.HostCodePages.EBCDICKorean_933;
						goto IL_01E2;
					}
				}
				else if (num != 2390948840U)
				{
					if (num != 2452239629U)
					{
						if (num == 2647663716U)
						{
							if (text == "IBM290")
							{
								hostCodePages = HisEncoding.HostCodePages.EBCDICJapaneseKatakana_290;
								goto IL_01E2;
							}
						}
					}
					else if (text == "IBM424")
					{
						hostCodePages = HisEncoding.HostCodePages.EBCDICHebrew_424;
						goto IL_01E2;
					}
				}
				else if (text == "IBM5026")
				{
					hostCodePages = HisEncoding.HostCodePages.EBCDICJapaneseKatakanaKanji_5026;
					goto IL_01E2;
				}
			}
			hisEncoding = new HisForwardEncoding(Encoding.GetEncoding(encodingname));
			IL_01E2:
			if (hisEncoding == null)
			{
				hisEncoding = HisEncoding.GetEncoding((int)hostCodePages);
			}
			return hisEncoding;
		}

		// Token: 0x06003545 RID: 13637 RVA: 0x000B2C64 File Offset: 0x000B0E64
		public new static HisEncodingInfo[] GetEncodings()
		{
			HisEncodingInfo[] array = new HisEncodingInfo[HisEncoding.hisEncodinginfos.Count];
			HisEncoding.hisEncodinginfos.Values.CopyTo(array, 0);
			return array;
		}

		// Token: 0x06003546 RID: 13638 RVA: 0x000B2C94 File Offset: 0x000B0E94
		private static HisEncoding CreateEncoding(HisEncoding.HostCodePages codepage)
		{
			if (codepage <= HisEncoding.HostCodePages.EBCDICJapaneseExtendedLowerEnglish_1027)
			{
				if (codepage != HisEncoding.HostCodePages.EBCDICJapaneseKatakana_290)
				{
					switch (codepage)
					{
					case HisEncoding.HostCodePages.EBCDICJapaneseKatakanaKanji_290:
					case HisEncoding.HostCodePages.EBCDICJapaneseLowerEnglishAndKanji_931:
					case HisEncoding.HostCodePages.EBCDICJapaneseExtendedLowerEnglishKanji_939:
						break;
					case (HisEncoding.HostCodePages)932:
					case (HisEncoding.HostCodePages)934:
					case (HisEncoding.HostCodePages)936:
					case (HisEncoding.HostCodePages)938:
						goto IL_00B4;
					case HisEncoding.HostCodePages.EBCDICKorean_933:
						return new HisKoreanEncoding(codepage);
					case HisEncoding.HostCodePages.EBCDICSimplifiedChinese_935:
						return new HisSimplifiedChineseEncoding(codepage);
					case HisEncoding.HostCodePages.EBCDICTraditionalChinese_937:
						return new HisTraditionalChineseEncoding(codepage);
					default:
						if (codepage != HisEncoding.HostCodePages.EBCDICJapaneseExtendedLowerEnglish_1027)
						{
							goto IL_00B4;
						}
						break;
					}
				}
			}
			else if (codepage <= HisEncoding.HostCodePages.EBCDICJapaneseLatinKanji_5035)
			{
				if (codepage != HisEncoding.HostCodePages.EBCDICJapaneseKatakanaKanji_5026 && codepage != HisEncoding.HostCodePages.EBCDICJapaneseLatinKanji_5035)
				{
					goto IL_00B4;
				}
			}
			else
			{
				if (codepage == HisEncoding.HostCodePages.EBCDICArabic_420)
				{
					return new HisArabicVisualEncoding(codepage);
				}
				if (codepage != HisEncoding.HostCodePages.EBCDICHebrew_424)
				{
					goto IL_00B4;
				}
				return new HisHebrewVisualEncoding(codepage);
			}
			return new HisJapaneseEncoding(codepage);
			IL_00B4:
			return new HisForwardEncoding(codepage);
		}

		// Token: 0x17000B96 RID: 2966
		// (get) Token: 0x06003547 RID: 13639 RVA: 0x000B2D60 File Offset: 0x000B0F60
		private string CodePageName
		{
			get
			{
				HisEncoding.HostCodePages hostCodePages = this.destinationCodePage;
				if (hostCodePages <= HisEncoding.HostCodePages.EBCDICJapaneseKatakanaKanji_5026)
				{
					if (hostCodePages == HisEncoding.HostCodePages.EBCDICJapaneseKatakana_290)
					{
						return "IBM290";
					}
					switch (hostCodePages)
					{
					case HisEncoding.HostCodePages.EBCDICJapaneseKatakanaKanji_290:
						return "IBM930";
					case HisEncoding.HostCodePages.EBCDICJapaneseLowerEnglishAndKanji_931:
						return "IBM931";
					case (HisEncoding.HostCodePages)932:
					case (HisEncoding.HostCodePages)934:
					case (HisEncoding.HostCodePages)936:
					case (HisEncoding.HostCodePages)938:
						break;
					case HisEncoding.HostCodePages.EBCDICKorean_933:
						return "IBM933";
					case HisEncoding.HostCodePages.EBCDICSimplifiedChinese_935:
						return "IBM935";
					case HisEncoding.HostCodePages.EBCDICTraditionalChinese_937:
						return "IBM937";
					case HisEncoding.HostCodePages.EBCDICJapaneseExtendedLowerEnglishKanji_939:
						return "IBM939";
					default:
						if (hostCodePages == HisEncoding.HostCodePages.EBCDICJapaneseKatakanaKanji_5026)
						{
							return "IBM5026";
						}
						break;
					}
				}
				else
				{
					if (hostCodePages == HisEncoding.HostCodePages.EBCDICJapaneseLatinKanji_5035)
					{
						return "IBM5035";
					}
					if (hostCodePages == HisEncoding.HostCodePages.EBCDICArabic_420)
					{
						return "IBM420";
					}
					if (hostCodePages == HisEncoding.HostCodePages.EBCDICHebrew_424)
					{
						return "IBM424";
					}
				}
				return " ";
			}
		}

		// Token: 0x06003548 RID: 13640 RVA: 0x000B2E24 File Offset: 0x000B1024
		private static HisEncoding.HostCodePages FixCodePage(HisEncoding.HostCodePages hostCP)
		{
			if (hostCP <= HisEncoding.HostCodePages.IBMISO885915)
			{
				if (hostCP == HisEncoding.HostCodePages.IBMISO88597)
				{
					return HisEncoding.HostCodePages.CLRCodePageISO88597;
				}
				if (hostCP == HisEncoding.HostCodePages.IBMISO88591)
				{
					return HisEncoding.HostCodePages.CLRCodePageISO88591;
				}
				switch (hostCP)
				{
				case HisEncoding.HostCodePages.IBMISO88592:
					return HisEncoding.HostCodePages.CLRCodePageISO88592;
				case HisEncoding.HostCodePages.IBMISO88594:
					return HisEncoding.HostCodePages.CLRCodePageISO88594;
				case HisEncoding.HostCodePages.IBMISO88595:
					return HisEncoding.HostCodePages.CLRCodePageISO88595;
				case HisEncoding.HostCodePages.IBMISO88598:
					return HisEncoding.HostCodePages.CLRCodePageISO88598;
				case HisEncoding.HostCodePages.IBMISO88599:
					return HisEncoding.HostCodePages.CLRCodePageISO88599;
				case HisEncoding.HostCodePages.IBMISO885913:
					return HisEncoding.HostCodePages.CLRCodePageISO885913;
				case HisEncoding.HostCodePages.IBMISO885915:
					return HisEncoding.HostCodePages.CLRCodePageISO885915;
				}
			}
			else if (hostCP <= HisEncoding.HostCodePages.CodePageUTF8)
			{
				if (hostCP == HisEncoding.HostCodePages.IBMISO88596)
				{
					return HisEncoding.HostCodePages.CLRCodePageISO88596;
				}
				if (hostCP == HisEncoding.HostCodePages.CodePageUTF8)
				{
					return HisEncoding.HostCodePages.CLRCodePageUTF8;
				}
			}
			else
			{
				if (hostCP == HisEncoding.HostCodePages.EBCDICJapaneseKatakanaKanji_5026)
				{
					return HisEncoding.HostCodePages.EBCDICJapaneseKatakanaKanji_290;
				}
				if (hostCP == HisEncoding.HostCodePages.EBCDICJapaneseLatinKanji_5035)
				{
					return HisEncoding.HostCodePages.EBCDICJapaneseExtendedLowerEnglishKanji_939;
				}
			}
			if (hostCP == (HisEncoding.HostCodePages)273 || hostCP == (HisEncoding.HostCodePages)277 || hostCP == (HisEncoding.HostCodePages)278 || hostCP == (HisEncoding.HostCodePages)280 || hostCP == (HisEncoding.HostCodePages)284 || hostCP == (HisEncoding.HostCodePages)285 || hostCP == HisEncoding.HostCodePages.EBCDICJapaneseKatakana_290 || hostCP == (HisEncoding.HostCodePages)297 || hostCP == (HisEncoding.HostCodePages)420 || hostCP == (HisEncoding.HostCodePages)423 || hostCP == (HisEncoding.HostCodePages)424 || hostCP == (HisEncoding.HostCodePages)833 || hostCP == (HisEncoding.HostCodePages)838 || hostCP == (HisEncoding.HostCodePages)871 || hostCP == (HisEncoding.HostCodePages)880 || hostCP == (HisEncoding.HostCodePages)905 || hostCP == (HisEncoding.HostCodePages)1025)
			{
				return (HisEncoding.HostCodePages)20000 + (int)hostCP;
			}
			if (hostCP == (HisEncoding.HostCodePages)1388)
			{
				return HisEncoding.HostCodePages.EBCDICSimplifiedChinese_935;
			}
			if (hostCP == (HisEncoding.HostCodePages)1392 || hostCP == (HisEncoding.HostCodePages)5488)
			{
				return HisEncoding.HostCodePages.CLRCodePageGB18030;
			}
			if (hostCP >= (HisEncoding.HostCodePages)5346 && hostCP <= (HisEncoding.HostCodePages)5351)
			{
				return hostCP - 5346 + 1250;
			}
			return hostCP;
		}

		// Token: 0x06003549 RID: 13641 RVA: 0x000B2FDC File Offset: 0x000B11DC
		private HisEncoding.WindowsCodePages GetCorrespondingWindowsCodePage()
		{
			HisEncoding.HostCodePages hostCodePages = this.destinationCodePage;
			if (hostCodePages <= HisEncoding.HostCodePages.EBCDICJapaneseExtendedLowerEnglish_1027)
			{
				if (hostCodePages != HisEncoding.HostCodePages.EBCDICJapaneseKatakana_290)
				{
					switch (hostCodePages)
					{
					case HisEncoding.HostCodePages.EBCDICJapaneseKatakanaKanji_290:
					case HisEncoding.HostCodePages.EBCDICJapaneseLowerEnglishAndKanji_931:
					case HisEncoding.HostCodePages.EBCDICJapaneseExtendedLowerEnglishKanji_939:
						break;
					case (HisEncoding.HostCodePages)932:
					case (HisEncoding.HostCodePages)934:
					case (HisEncoding.HostCodePages)936:
					case (HisEncoding.HostCodePages)938:
						goto IL_00A5;
					case HisEncoding.HostCodePages.EBCDICKorean_933:
						return HisEncoding.WindowsCodePages.KoreanCodePage_949;
					case HisEncoding.HostCodePages.EBCDICSimplifiedChinese_935:
						return HisEncoding.WindowsCodePages.SimplifiedChineseCodePage_936;
					case HisEncoding.HostCodePages.EBCDICTraditionalChinese_937:
						return HisEncoding.WindowsCodePages.TraditionalChineseCodePage_950;
					default:
						if (hostCodePages != HisEncoding.HostCodePages.EBCDICJapaneseExtendedLowerEnglish_1027)
						{
							goto IL_00A5;
						}
						break;
					}
				}
			}
			else if (hostCodePages <= HisEncoding.HostCodePages.EBCDICJapaneseLatinKanji_5035)
			{
				if (hostCodePages != HisEncoding.HostCodePages.EBCDICJapaneseKatakanaKanji_5026 && hostCodePages != HisEncoding.HostCodePages.EBCDICJapaneseLatinKanji_5035)
				{
					goto IL_00A5;
				}
			}
			else
			{
				if (hostCodePages == HisEncoding.HostCodePages.EBCDICArabic_420)
				{
					return HisEncoding.WindowsCodePages.ArabicCodePage_420;
				}
				if (hostCodePages != HisEncoding.HostCodePages.EBCDICHebrew_424)
				{
					goto IL_00A5;
				}
				return HisEncoding.WindowsCodePages.HebrewCodePage_424;
			}
			return HisEncoding.WindowsCodePages.JapaneseCodePage_932;
			IL_00A5:
			HisEncoding.WindowsCodePages windowsCodePages;
			try
			{
				windowsCodePages = (HisEncoding.WindowsCodePages)Encoding.GetEncoding((int)this.destinationCodePage).WindowsCodePage;
			}
			catch (Exception)
			{
				windowsCodePages = HisEncoding.WindowsCodePages.UnicodeCodePage;
			}
			return windowsCodePages;
		}

		// Token: 0x0600354A RID: 13642 RVA: 0x000B30BC File Offset: 0x000B12BC
		internal static bool IsHostCodePage(HisEncoding.HostCodePages cp)
		{
			if (cp <= HisEncoding.HostCodePages.EBCDICJapaneseLatinKanji_5035)
			{
				if (cp <= HisEncoding.HostCodePages.EBCDICGreekModern_875)
				{
					if (cp <= HisEncoding.HostCodePages.EBCDICJapaneseKatakana_290)
					{
						if (cp != HisEncoding.HostCodePages.EBCDICLowerEnglish_37 && cp != HisEncoding.HostCodePages.EBCDICJapaneseKatakana_290)
						{
							return false;
						}
					}
					else if (cp != HisEncoding.HostCodePages.EBCDICInternational_500 && cp != HisEncoding.HostCodePages.EBCDICMultilingualLatin2_870 && cp != HisEncoding.HostCodePages.EBCDICGreekModern_875)
					{
						return false;
					}
				}
				else if (cp <= HisEncoding.HostCodePages.EBCDICJapaneseExtendedLowerEnglish_1027)
				{
					switch (cp)
					{
					case HisEncoding.HostCodePages.EBCDICJapaneseKatakanaKanji_290:
					case HisEncoding.HostCodePages.EBCDICJapaneseLowerEnglishAndKanji_931:
					case HisEncoding.HostCodePages.EBCDICKorean_933:
					case HisEncoding.HostCodePages.EBCDICSimplifiedChinese_935:
					case HisEncoding.HostCodePages.EBCDICTraditionalChinese_937:
					case HisEncoding.HostCodePages.EBCDICJapaneseExtendedLowerEnglishKanji_939:
						break;
					case (HisEncoding.HostCodePages)932:
					case (HisEncoding.HostCodePages)934:
					case (HisEncoding.HostCodePages)936:
					case (HisEncoding.HostCodePages)938:
						return false;
					default:
						if (cp - HisEncoding.HostCodePages.EBCDICTurkishLatin5_1026 > 1)
						{
							return false;
						}
						break;
					}
				}
				else if (cp - HisEncoding.HostCodePages.EBCDICUsaCanadaEuro_1140 > 9 && cp != HisEncoding.HostCodePages.EBCDICJapaneseKatakanaKanji_5026 && cp != HisEncoding.HostCodePages.EBCDICJapaneseLatinKanji_5035)
				{
					return false;
				}
			}
			else if (cp <= HisEncoding.HostCodePages.EBCDICHebrew_424)
			{
				if (cp <= HisEncoding.HostCodePages.EBCDICUnitedKingdom_285)
				{
					if (cp != HisEncoding.HostCodePages.EBCDICGermany_273)
					{
						switch (cp)
						{
						case HisEncoding.HostCodePages.EBCDICDenmarkNorway_277:
						case HisEncoding.HostCodePages.EBCDICFinlandSweden_278:
						case HisEncoding.HostCodePages.EBCDICItaly_280:
						case HisEncoding.HostCodePages.EBCDICSpain_284:
						case HisEncoding.HostCodePages.EBCDICUnitedKingdom_285:
							break;
						case (HisEncoding.HostCodePages)20279:
						case (HisEncoding.HostCodePages)20281:
						case (HisEncoding.HostCodePages)20282:
						case (HisEncoding.HostCodePages)20283:
							return false;
						default:
							return false;
						}
					}
				}
				else if (cp != HisEncoding.HostCodePages.EBCDICFrance_297 && cp != HisEncoding.HostCodePages.EBCDICArabic_420 && cp - HisEncoding.HostCodePages.EBCDICGreek_423 > 1)
				{
					return false;
				}
			}
			else if (cp <= HisEncoding.HostCodePages.EBCDICIcelandic_871)
			{
				if (cp != HisEncoding.HostCodePages.EBCDICThai_838 && cp != HisEncoding.HostCodePages.EBCDICIcelandic_871)
				{
					return false;
				}
			}
			else if (cp != HisEncoding.HostCodePages.EBCDICCyrillicRussian_880 && cp != HisEncoding.HostCodePages.EBCDICTurkish_905 && cp != HisEncoding.HostCodePages.EBCDICCyrillicSerbianBulgarian_1025)
			{
				return false;
			}
			return true;
		}

		// Token: 0x0600354B RID: 13643 RVA: 0x000B3237 File Offset: 0x000B1437
		public override int GetByteCount(char[] chars, int index, int count)
		{
			return this.converter.GetByteCount(chars, index, count);
		}

		// Token: 0x0600354C RID: 13644 RVA: 0x000B3247 File Offset: 0x000B1447
		public override byte[] GetBytes(char[] chars, int index, int count)
		{
			return this.converter.UnicodeToEbcdic(chars, index, count);
		}

		// Token: 0x0600354D RID: 13645 RVA: 0x000B3258 File Offset: 0x000B1458
		public override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
		{
			byte[] bytes2 = this.GetBytes(chars, charIndex, charCount);
			Buffer.BlockCopy(bytes2, 0, bytes, byteIndex, bytes2.Length);
			return bytes2.Length;
		}

		// Token: 0x0600354E RID: 13646 RVA: 0x000B3280 File Offset: 0x000B1480
		public override char[] GetChars(byte[] bytes, int byteIndex, int byteCount)
		{
			return this.converter.EbcdicToUnicode(bytes, byteIndex, byteCount);
		}

		// Token: 0x0600354F RID: 13647 RVA: 0x000B3290 File Offset: 0x000B1490
		public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
		{
			char[] chars2 = this.GetChars(bytes, byteIndex, byteCount);
			Array.Copy(chars2, 0, chars, charIndex, chars2.Length);
			return chars2.Length;
		}

		// Token: 0x06003550 RID: 13648 RVA: 0x000B32B8 File Offset: 0x000B14B8
		public override int GetCharCount(byte[] bytes, int index, int count)
		{
			return this.converter.GetCharCount(bytes, index, count);
		}

		// Token: 0x06003551 RID: 13649 RVA: 0x000B32C8 File Offset: 0x000B14C8
		public override Encoder GetEncoder()
		{
			if (this.encoder == null)
			{
				this.encoder = new HisEncoder(this, this.converter);
			}
			return this.encoder;
		}

		// Token: 0x06003552 RID: 13650 RVA: 0x000B32EA File Offset: 0x000B14EA
		public override Decoder GetDecoder()
		{
			if (this.decoder == null)
			{
				this.decoder = new HisDecoder(this, this.converter);
			}
			return this.decoder;
		}

		// Token: 0x06003553 RID: 13651 RVA: 0x000B330C File Offset: 0x000B150C
		public override object Clone()
		{
			return (HisEncoding)base.MemberwiseClone();
		}

		// Token: 0x04001E1D RID: 7709
		internal bool? usePhysicalOrderForCodePage420;

		// Token: 0x04001E1E RID: 7710
		internal HisCustomEncodingMappings hisCustomEncodingMaps;

		// Token: 0x04001E1F RID: 7711
		internal HisEncoding.HostCodePages destinationCodePage = HisEncoding.HostCodePages.EBCDICLowerEnglish_37;

		// Token: 0x04001E20 RID: 7712
		internal HisConverter converter;

		// Token: 0x04001E21 RID: 7713
		protected HisEncoding.WindowsCodePages windowsCodePage = HisEncoding.WindowsCodePages.UnicodeCodePage;

		// Token: 0x04001E22 RID: 7714
		protected Encoding windowsEncoding;

		// Token: 0x04001E23 RID: 7715
		private bool useIBMTable;

		// Token: 0x04001E24 RID: 7716
		private bool autoSISO = true;

		// Token: 0x04001E25 RID: 7717
		private HisEncoder encoder;

		// Token: 0x04001E26 RID: 7718
		private HisDecoder decoder;

		// Token: 0x04001E27 RID: 7719
		private static HisEncodingConfiguration config;

		// Token: 0x04001E28 RID: 7720
		private static SortedList<int, HisEncodingInfo> hisEncodinginfos;

		// Token: 0x04001E29 RID: 7721
		private bool dontUsePresentationFormsB;

		// Token: 0x04001E2A RID: 7722
		private const string ibm290 = "IBM290";

		// Token: 0x04001E2B RID: 7723
		private const string ibm290_display = "IBM EBCDIC (Japan Katakana)";

		// Token: 0x04001E2C RID: 7724
		private const string ibm930 = "IBM930";

		// Token: 0x04001E2D RID: 7725
		private const string ibm930_display = "IBM EBCDIC (Japan Katakana/Kanji)";

		// Token: 0x04001E2E RID: 7726
		private const string ibm931 = "IBM931";

		// Token: 0x04001E2F RID: 7727
		private const string ibm931_display = "IBM EBCDIC (Japanese)";

		// Token: 0x04001E30 RID: 7728
		private const string ibm939 = "IBM939";

		// Token: 0x04001E31 RID: 7729
		private const string ibm939_display = "IBM EBCDIC (Japan English/Kanji)";

		// Token: 0x04001E32 RID: 7730
		private const string ibm5026 = "IBM5026";

		// Token: 0x04001E33 RID: 7731
		private const string ibm5026_display = "IBM EBCDIC (Japan Katakana/Kanji)";

		// Token: 0x04001E34 RID: 7732
		private const string ibm5035 = "IBM5035";

		// Token: 0x04001E35 RID: 7733
		private const string ibm5035_display = "IBM EBCDIC (Japan English/Kanji)";

		// Token: 0x04001E36 RID: 7734
		private const string ibm933 = "IBM933";

		// Token: 0x04001E37 RID: 7735
		private const string ibm933_display = "IBM EBCDIC (Korea)";

		// Token: 0x04001E38 RID: 7736
		private const string ibm935 = "IBM935";

		// Token: 0x04001E39 RID: 7737
		private const string ibm935_display = "IBM EBCDIC (Simplified Chinese)";

		// Token: 0x04001E3A RID: 7738
		private const string ibm937 = "IBM937";

		// Token: 0x04001E3B RID: 7739
		private const string ibm937_display = "IBM EBCDIC (Traditional Chinese)";

		// Token: 0x04001E3C RID: 7740
		private const string ibm420 = "IBM420";

		// Token: 0x04001E3D RID: 7741
		private const string ibm420_display = "IBM EBCDIC (Arabic)";

		// Token: 0x04001E3E RID: 7742
		private const string ibm424 = "IBM424";

		// Token: 0x04001E3F RID: 7743
		private const string ibm424_display = "IBM EBCDIC (Hebrew)";

		// Token: 0x0200062C RID: 1580
		internal enum HostCodePages
		{
			// Token: 0x04001E43 RID: 7747
			InvalidCodePage,
			// Token: 0x04001E44 RID: 7748
			EBCDICLowerEnglish_37 = 37,
			// Token: 0x04001E45 RID: 7749
			EBCDICJapaneseKatakana_290 = 290,
			// Token: 0x04001E46 RID: 7750
			EBCDICJapaneseKatakanaKanji_290 = 930,
			// Token: 0x04001E47 RID: 7751
			EBCDICJapaneseLowerEnglishAndKanji_931,
			// Token: 0x04001E48 RID: 7752
			EBCDICKorean_933 = 933,
			// Token: 0x04001E49 RID: 7753
			EBCDICSimplifiedChinese_935 = 935,
			// Token: 0x04001E4A RID: 7754
			EBCDICTraditionalChinese_937 = 937,
			// Token: 0x04001E4B RID: 7755
			EBCDICJapaneseExtendedLowerEnglishKanji_939 = 939,
			// Token: 0x04001E4C RID: 7756
			EBCDICJapaneseExtendedLowerEnglish_1027 = 1027,
			// Token: 0x04001E4D RID: 7757
			EBCDICJapaneseKatakanaKanji_5026 = 5026,
			// Token: 0x04001E4E RID: 7758
			EBCDICJapaneseLatinKanji_5035 = 5035,
			// Token: 0x04001E4F RID: 7759
			EBCDICArabic_420 = 20420,
			// Token: 0x04001E50 RID: 7760
			EBCDICHebrew_424 = 20424,
			// Token: 0x04001E51 RID: 7761
			EBCDICCyrillicRussian_880 = 20880,
			// Token: 0x04001E52 RID: 7762
			EBCDICCyrillicSerbianBulgarian_1025 = 21025,
			// Token: 0x04001E53 RID: 7763
			EBCDICDenmarkNorwayEuro_1142 = 1142,
			// Token: 0x04001E54 RID: 7764
			EBCDICDenmarkNorway_277 = 20277,
			// Token: 0x04001E55 RID: 7765
			EBCDICFinlandSwedenEuro_1143 = 1143,
			// Token: 0x04001E56 RID: 7766
			EBCDICFinlandSweden_278 = 20278,
			// Token: 0x04001E57 RID: 7767
			EBCDICFranceEuro_1147 = 1147,
			// Token: 0x04001E58 RID: 7768
			EBCDICFrance_297 = 20297,
			// Token: 0x04001E59 RID: 7769
			EBCDICGermanyEuro_1141 = 1141,
			// Token: 0x04001E5A RID: 7770
			EBCDICGermany_273 = 20273,
			// Token: 0x04001E5B RID: 7771
			EBCDICGreekModern_875 = 875,
			// Token: 0x04001E5C RID: 7772
			EBCDICGreek_423 = 20423,
			// Token: 0x04001E5D RID: 7773
			EBCDICIcelandicEuro_1149 = 1149,
			// Token: 0x04001E5E RID: 7774
			EBCDICIcelandic_871 = 20871,
			// Token: 0x04001E5F RID: 7775
			EBCDICInternationalEuro_1148 = 1148,
			// Token: 0x04001E60 RID: 7776
			EBCDICInternational_500 = 500,
			// Token: 0x04001E61 RID: 7777
			EBCDICItalyEuro_1144 = 1144,
			// Token: 0x04001E62 RID: 7778
			EBCDICItaly_280 = 20280,
			// Token: 0x04001E63 RID: 7779
			EBCDICSpainEuro_1145 = 1145,
			// Token: 0x04001E64 RID: 7780
			EBCDICSpain_284 = 20284,
			// Token: 0x04001E65 RID: 7781
			EBCDICMultilingualLatin2_870 = 870,
			// Token: 0x04001E66 RID: 7782
			EBCDICThai_838 = 20838,
			// Token: 0x04001E67 RID: 7783
			EBCDICTurkish_905 = 20905,
			// Token: 0x04001E68 RID: 7784
			EBCDICTurkishLatin5_1026 = 1026,
			// Token: 0x04001E69 RID: 7785
			EBCDICUsaCanadaEuro_1140 = 1140,
			// Token: 0x04001E6A RID: 7786
			EBCDICUnitedKingdomEuro_1146 = 1146,
			// Token: 0x04001E6B RID: 7787
			EBCDICUnitedKingdom_285 = 20285,
			// Token: 0x04001E6C RID: 7788
			EBCDICUnicodeBigEndian_1201 = 1201,
			// Token: 0x04001E6D RID: 7789
			IBMISO88591 = 819,
			// Token: 0x04001E6E RID: 7790
			IBMISO88592 = 912,
			// Token: 0x04001E6F RID: 7791
			IBMISO88594 = 914,
			// Token: 0x04001E70 RID: 7792
			IBMISO88595,
			// Token: 0x04001E71 RID: 7793
			IBMISO88596 = 1089,
			// Token: 0x04001E72 RID: 7794
			IBMISO88597 = 813,
			// Token: 0x04001E73 RID: 7795
			IBMISO88598 = 916,
			// Token: 0x04001E74 RID: 7796
			IBMISO88599 = 920,
			// Token: 0x04001E75 RID: 7797
			IBMISO885913,
			// Token: 0x04001E76 RID: 7798
			IBMISO885915 = 923,
			// Token: 0x04001E77 RID: 7799
			CodePageUTF8 = 1208,
			// Token: 0x04001E78 RID: 7800
			CLRCodePageUTF8 = 65001,
			// Token: 0x04001E79 RID: 7801
			CodePageUTF16 = 1200,
			// Token: 0x04001E7A RID: 7802
			CLRCodePageGB18030 = 54936,
			// Token: 0x04001E7B RID: 7803
			CLRCodePageISO88591 = 28591,
			// Token: 0x04001E7C RID: 7804
			CLRCodePageISO88592,
			// Token: 0x04001E7D RID: 7805
			CLRCodePageISO88594 = 28594,
			// Token: 0x04001E7E RID: 7806
			CLRCodePageISO88595,
			// Token: 0x04001E7F RID: 7807
			CLRCodePageISO88596,
			// Token: 0x04001E80 RID: 7808
			CLRCodePageISO88597,
			// Token: 0x04001E81 RID: 7809
			CLRCodePageISO88598,
			// Token: 0x04001E82 RID: 7810
			CLRCodePageISO88599,
			// Token: 0x04001E83 RID: 7811
			CLRCodePageISO885913 = 28603,
			// Token: 0x04001E84 RID: 7812
			CLRCodePageISO885915 = 28605
		}

		// Token: 0x0200062D RID: 1581
		protected enum WindowsCodePages
		{
			// Token: 0x04001E86 RID: 7814
			JapaneseCodePage_932 = 932,
			// Token: 0x04001E87 RID: 7815
			KoreanCodePage_949 = 949,
			// Token: 0x04001E88 RID: 7816
			SimplifiedChineseCodePage_936 = 936,
			// Token: 0x04001E89 RID: 7817
			TraditionalChineseCodePage_950 = 950,
			// Token: 0x04001E8A RID: 7818
			WesternEuropeanCodePage_1252 = 1252,
			// Token: 0x04001E8B RID: 7819
			ArabicCodePage_420 = 1256,
			// Token: 0x04001E8C RID: 7820
			HebrewCodePage_424 = 1255,
			// Token: 0x04001E8D RID: 7821
			CodePageUTF = 65001,
			// Token: 0x04001E8E RID: 7822
			UnicodeCodePage = 1200,
			// Token: 0x04001E8F RID: 7823
			GB18030 = 54936
		}
	}
}
