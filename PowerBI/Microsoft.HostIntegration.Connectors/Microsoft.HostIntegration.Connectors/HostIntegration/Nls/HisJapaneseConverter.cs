using System;
using System.IO;
using System.Security;
using System.Security.Permissions;
using Microsoft.HostIntegration.StrictResources.Nls;

namespace Microsoft.HostIntegration.Nls
{
	// Token: 0x02000633 RID: 1587
	internal class HisJapaneseConverter : HisSBMBCSConverter
	{
		// Token: 0x06003592 RID: 13714 RVA: 0x000B39A9 File Offset: 0x000B1BA9
		internal HisJapaneseConverter(HisEncoding enc)
			: base(enc)
		{
			this.encoding = enc;
		}

		// Token: 0x06003593 RID: 13715 RVA: 0x000B39D0 File Offset: 0x000B1BD0
		[SecurityCritical]
		[SecuritySafeCritical]
		[RegistryPermission(SecurityAction.Assert, Read = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Host Integration Server")]
		static HisJapaneseConverter()
		{
			string text = "";
			string text2 = "";
			new byte[128];
			HisJapaneseConverter.fileloadstatus = HisSBMBCSConverter.FileLoadStatus.Uninitialized;
			text2 = HisConverter.installPath.Value + "\\SNASBC.TBL";
			text = HisConverter.installPath.Value + "\\SNADBC.TBL";
			FileIOPermission fileIOPermission = new FileIOPermission(FileIOPermissionAccess.Read, text2);
			fileIOPermission.AddPathList(FileIOPermissionAccess.Read, text);
			try
			{
				fileIOPermission.Demand();
			}
			catch (SecurityException ex)
			{
				Console.WriteLine(ex.Message);
			}
			BinaryReader dataBinaryReader = HisSBMBCSConverter.GetDataBinaryReader("SNASBC.TBL");
			if (!Memory.ReadTable(dataBinaryReader.BaseStream, HisJapaneseConverter.table932_7, 256))
			{
				throw new NotSupportedException(SR.CodePageTableNotFound(text2), new FileLoadException(SR.CodePageTableNotFound(text2)));
			}
			if (!Memory.ReadTable(dataBinaryReader.BaseStream, HisJapaneseConverter.table932_3, 256))
			{
				throw new NotSupportedException(SR.CodePageTableNotFound(text2), new FileLoadException(SR.CodePageTableNotFound(text2)));
			}
			if (!Memory.ReadTable(dataBinaryReader.BaseStream, HisJapaneseConverter.table932_8, 256))
			{
				throw new NotSupportedException(SR.CodePageTableNotFound(text2), new FileLoadException(SR.CodePageTableNotFound(text2)));
			}
			if (!Memory.ReadTable(dataBinaryReader.BaseStream, HisJapaneseConverter.table932_4, 256))
			{
				throw new NotSupportedException(SR.CodePageTableNotFound(text2), new FileLoadException(SR.CodePageTableNotFound(text2)));
			}
			dataBinaryReader.Close();
			HisJapaneseConverter.fileloadstatus = HisSBMBCSConverter.FileLoadStatus.LoadedSingleByteTableOnly;
			BinaryReader dataBinaryReader2 = HisSBMBCSConverter.GetDataBinaryReader("SNADBC.TBL");
			if (!Memory.ReadTable(dataBinaryReader2.BaseStream, HisJapaneseConverter.table932_ibm_pc, 23942))
			{
				throw new NotSupportedException(SR.CodePageTableNotFound(text), new FileLoadException(SR.CodePageTableNotFound(text)));
			}
			dataBinaryReader2.Close();
			HisJapaneseConverter.table932_init();
			HisJapaneseConverter.fileloadstatus = HisSBMBCSConverter.FileLoadStatus.LoadedBothTables;
		}

		// Token: 0x06003594 RID: 13716 RVA: 0x000B3D34 File Offset: 0x000B1F34
		private static void table932_init()
		{
			ushort[] array = new ushort[2];
			Memory.SetMemory(HisJapaneseConverter.table932_pc_ibm, 0L, 254, 23942L);
			for (ushort num = 0; num < 11971; num += 1)
			{
				if (Memory.memcmp(HisJapaneseConverter.table932_ibm_pc, (int)(num * 2), HisJapaneseConverter.cod_8140, 0, 2L) == 0)
				{
					Memory.MemoryCopy(HisJapaneseConverter.cod_4040, 0, HisJapaneseConverter.table932_pc_ibm, 0, 2L);
				}
				if (Memory.memcmp(HisJapaneseConverter.table932_ibm_pc, (int)(num * 2), HisJapaneseConverter.cod_8141, 0, 2L) >= 0 && Memory.memcmp(HisJapaneseConverter.table932_ibm_pc, (int)(num * 2), HisJapaneseConverter.cod_9ffc, 0, 2L) <= 0)
				{
					array[0] = (ushort)((HisJapaneseConverter.table932_ibm_pc[(int)(num * 2)] - 129) * 188);
					if (HisJapaneseConverter.table932_ibm_pc[(int)(num * 2 + 1)] < 127)
					{
						array[1] = (ushort)(HisJapaneseConverter.table932_ibm_pc[(int)(num * 2 + 1)] - 64);
					}
					else
					{
						array[1] = (ushort)(HisJapaneseConverter.table932_ibm_pc[(int)(num * 2 + 1)] - 65);
					}
					ushort num2 = (array[0] + array[1]) * 2;
					array[0] = num - 1;
					array[1] = array[0] % 190;
					array[0] = array[0] / 190;
					HisJapaneseConverter.work2[0] = (byte)(array[0] + 65);
					HisJapaneseConverter.work2[1] = (byte)(array[1] + 65);
					Memory.MemoryCopy(HisJapaneseConverter.work2, 0, HisJapaneseConverter.table932_pc_ibm, (int)num2, 2L);
				}
				if (Memory.memcmp(HisJapaneseConverter.table932_ibm_pc, (int)(num * 2), HisJapaneseConverter.cod_e040, 0, 2L) >= 0 && Memory.memcmp(HisJapaneseConverter.table932_ibm_pc, (int)(num * 2), HisJapaneseConverter.cod_fcfa, 0, 2L) <= 0)
				{
					array[0] = (ushort)((HisJapaneseConverter.table932_ibm_pc[(int)(num * 2)] - 193) * 188);
					if (HisJapaneseConverter.table932_ibm_pc[(int)(num * 2 + 1)] < 127)
					{
						array[1] = (ushort)(HisJapaneseConverter.table932_ibm_pc[(int)(num * 2 + 1)] - 64);
					}
					else
					{
						array[1] = (ushort)(HisJapaneseConverter.table932_ibm_pc[(int)(num * 2 + 1)] - 65);
					}
					ushort num2 = (array[0] + array[1]) * 2;
					array[0] = num - 1;
					array[1] = array[0] % 190;
					array[0] = array[0] / 190;
					HisJapaneseConverter.work2[0] = (byte)(array[0] + 65);
					HisJapaneseConverter.work2[1] = (byte)(array[1] + 65);
					Memory.MemoryCopy(HisJapaneseConverter.work2, 0, HisJapaneseConverter.table932_pc_ibm, (int)num2, 2L);
				}
			}
		}

		// Token: 0x06003595 RID: 13717 RVA: 0x000B3F5E File Offset: 0x000B215E
		internal override byte[] GetLeadBytes()
		{
			return this.leadBytes;
		}

		// Token: 0x06003596 RID: 13718 RVA: 0x000B3F66 File Offset: 0x000B2166
		protected override bool IsLeadByte(char ch)
		{
			return ch >= '\u0081' && ch <= 'ü';
		}

		// Token: 0x06003597 RID: 13719 RVA: 0x000B3F80 File Offset: 0x000B2180
		protected override int ConvertToEbcdic(bool bSingleByte, byte[] src, byte[] outbuff, int inPos, int outPos)
		{
			ushort[] array = new ushort[2];
			byte[] array2 = null;
			if (HisJapaneseConverter.fileloadstatus != HisSBMBCSConverter.FileLoadStatus.LoadedBothTables)
			{
				throw new FileNotFoundException(SR.ErrorOpeningTranslationTable("Japanese"));
			}
			if (bSingleByte)
			{
				HisEncoding.HostCodePages destinationCodePage = this.encoding.destinationCodePage;
				if (destinationCodePage > HisEncoding.HostCodePages.EBCDICJapaneseLowerEnglishAndKanji_931)
				{
					if (destinationCodePage <= HisEncoding.HostCodePages.EBCDICJapaneseExtendedLowerEnglish_1027)
					{
						if (destinationCodePage != HisEncoding.HostCodePages.EBCDICJapaneseExtendedLowerEnglishKanji_939 && destinationCodePage != HisEncoding.HostCodePages.EBCDICJapaneseExtendedLowerEnglish_1027)
						{
							goto IL_00E1;
						}
					}
					else
					{
						if (destinationCodePage == HisEncoding.HostCodePages.EBCDICJapaneseKatakanaKanji_5026)
						{
							goto IL_00C6;
						}
						if (destinationCodePage != HisEncoding.HostCodePages.EBCDICJapaneseLatinKanji_5035)
						{
							goto IL_00E1;
						}
					}
					array2 = HisJapaneseConverter.table932_1;
					goto IL_00E1;
				}
				if (destinationCodePage <= HisEncoding.HostCodePages.EBCDICJapaneseKatakana_290)
				{
					if (destinationCodePage != HisEncoding.HostCodePages.EBCDICLowerEnglish_37)
					{
						if (destinationCodePage != HisEncoding.HostCodePages.EBCDICJapaneseKatakana_290)
						{
							goto IL_00E1;
						}
						goto IL_00C6;
					}
				}
				else
				{
					if (destinationCodePage == HisEncoding.HostCodePages.EBCDICJapaneseKatakanaKanji_290)
					{
						goto IL_00C6;
					}
					if (destinationCodePage != HisEncoding.HostCodePages.EBCDICJapaneseLowerEnglishAndKanji_931)
					{
						goto IL_00E1;
					}
				}
				if (this.encoding.UseEnhancedEbcdicTable)
				{
					array2 = HisJapaneseConverter.table932_1;
					goto IL_00E1;
				}
				array2 = HisJapaneseConverter.table932_3;
				goto IL_00E1;
				IL_00C6:
				if (this.encoding.UseEnhancedEbcdicTable)
				{
					array2 = HisJapaneseConverter.table932_5;
				}
				else
				{
					array2 = HisJapaneseConverter.table932_7;
				}
				IL_00E1:
				outbuff[outPos] = array2[(int)src[inPos]];
			}
			else
			{
				if (src[inPos] <= 159)
				{
					array[0] = (ushort)((src[inPos] - 129) * 188);
				}
				else
				{
					array[0] = (ushort)((src[inPos] - 193) * 188);
				}
				if ((src[inPos + 1] >= 64 && src[inPos + 1] <= 126) || (src[inPos + 1] >= 128 && src[inPos + 1] <= 252))
				{
					if (src[inPos + 1] < 127)
					{
						array[1] = (ushort)(src[inPos + 1] - 64);
					}
					else
					{
						array[1] = (ushort)(src[inPos + 1] - 65);
					}
					ushort num = (array[0] + array[1]) * 2;
					Memory.MemoryCopy(HisJapaneseConverter.table932_pc_ibm, (int)num, outbuff, outPos, 2L);
				}
				else
				{
					Memory.MemoryCopy(HisJapaneseConverter.cod_fefe, 0, outbuff, outPos, 2L);
				}
			}
			return 0;
		}

		// Token: 0x06003598 RID: 13720 RVA: 0x000B4134 File Offset: 0x000B2334
		protected override int ConvertToUnicode(bool bSingleByte, byte[] src, byte[] outbuff, int inPos, int outPos)
		{
			ushort[] array = new ushort[2];
			byte[] array2 = null;
			if (HisJapaneseConverter.fileloadstatus != HisSBMBCSConverter.FileLoadStatus.LoadedBothTables)
			{
				throw new FileNotFoundException(SR.ErrorOpeningTranslationTable("Japanese"));
			}
			if (bSingleByte)
			{
				HisEncoding.HostCodePages destinationCodePage = this.encoding.destinationCodePage;
				if (destinationCodePage > HisEncoding.HostCodePages.EBCDICJapaneseLowerEnglishAndKanji_931)
				{
					if (destinationCodePage <= HisEncoding.HostCodePages.EBCDICJapaneseExtendedLowerEnglish_1027)
					{
						if (destinationCodePage != HisEncoding.HostCodePages.EBCDICJapaneseExtendedLowerEnglishKanji_939 && destinationCodePage != HisEncoding.HostCodePages.EBCDICJapaneseExtendedLowerEnglish_1027)
						{
							goto IL_00E1;
						}
					}
					else
					{
						if (destinationCodePage == HisEncoding.HostCodePages.EBCDICJapaneseKatakanaKanji_5026)
						{
							goto IL_00C6;
						}
						if (destinationCodePage != HisEncoding.HostCodePages.EBCDICJapaneseLatinKanji_5035)
						{
							goto IL_00E1;
						}
					}
					array2 = HisJapaneseConverter.table932_2;
					goto IL_00E1;
				}
				if (destinationCodePage <= HisEncoding.HostCodePages.EBCDICJapaneseKatakana_290)
				{
					if (destinationCodePage != HisEncoding.HostCodePages.EBCDICLowerEnglish_37)
					{
						if (destinationCodePage != HisEncoding.HostCodePages.EBCDICJapaneseKatakana_290)
						{
							goto IL_00E1;
						}
						goto IL_00C6;
					}
				}
				else
				{
					if (destinationCodePage == HisEncoding.HostCodePages.EBCDICJapaneseKatakanaKanji_290)
					{
						goto IL_00C6;
					}
					if (destinationCodePage != HisEncoding.HostCodePages.EBCDICJapaneseLowerEnglishAndKanji_931)
					{
						goto IL_00E1;
					}
				}
				if (this.encoding.UseEnhancedEbcdicTable)
				{
					array2 = HisJapaneseConverter.table932_2;
					goto IL_00E1;
				}
				array2 = HisJapaneseConverter.table932_4;
				goto IL_00E1;
				IL_00C6:
				if (this.encoding.UseEnhancedEbcdicTable)
				{
					array2 = HisJapaneseConverter.table932_6;
				}
				else
				{
					array2 = HisJapaneseConverter.table932_8;
				}
				IL_00E1:
				outbuff[outPos] = array2[(int)src[inPos]];
			}
			else if (Memory.memcmp(src, inPos, HisJapaneseConverter.cod_4040_ibmpc, 0, 2L) == 0)
			{
				Memory.MemoryCopy(HisJapaneseConverter.table932_ibm_pc, 0, outbuff, outPos, 2L);
			}
			else if (src[inPos] >= 65 && src[inPos] <= 127 && src[inPos + 1] >= 65 && src[inPos + 1] <= 254)
			{
				array[0] = (ushort)((src[inPos] - 65) * 190);
				array[1] = (ushort)(src[inPos + 1] - 64);
				ushort num = (array[0] + array[1]) * 2;
				if (Memory.memcmp(HisJapaneseConverter.table932_ibm_pc, (int)num, HisJapaneseConverter.cod_fcfb, 0, 2L) == 0)
				{
					Memory.MemoryCopy(HisJapaneseConverter.cod_fcfc, 0, outbuff, outPos, 2L);
				}
				else
				{
					Memory.MemoryCopy(HisJapaneseConverter.table932_ibm_pc, (int)num, outbuff, outPos, 2L);
				}
			}
			else
			{
				Memory.MemoryCopy(HisJapaneseConverter.cod_fcfc, 0, outbuff, outPos, 2L);
			}
			return 0;
		}

		// Token: 0x04001E9A RID: 7834
		private const string SingleByteTableFileName = "SNASBC.TBL";

		// Token: 0x04001E9B RID: 7835
		private const string DoubleByteTableFileName = "SNADBC.TBL";

		// Token: 0x04001E9C RID: 7836
		private static byte[] table932_pc_ibm = new byte[23942];

		// Token: 0x04001E9D RID: 7837
		private static byte[] table932_ibm_pc = new byte[23942];

		// Token: 0x04001E9E RID: 7838
		private static byte[] table932_3 = new byte[256];

		// Token: 0x04001E9F RID: 7839
		private static byte[] table932_4 = new byte[256];

		// Token: 0x04001EA0 RID: 7840
		private static byte[] table932_7 = new byte[256];

		// Token: 0x04001EA1 RID: 7841
		private static byte[] table932_8 = new byte[256];

		// Token: 0x04001EA2 RID: 7842
		private static byte[] table932_1 = new byte[]
		{
			0, 1, 2, 3, 55, 45, 46, 47, 22, 5,
			37, 11, 12, 13, 63, 63, 16, 17, 18, 19,
			60, 61, 50, 38, 24, 25, 28, 39, 7, 29,
			30, 31, 64, 90, 127, 123, 91, 108, 80, 125,
			77, 93, 92, 78, 107, 96, 75, 97, 240, 241,
			242, 243, 244, 245, 246, 247, 248, 249, 122, 94,
			76, 126, 110, 111, 124, 193, 194, 195, 196, 197,
			198, 199, 200, 201, 209, 210, 211, 212, 213, 214,
			215, 216, 217, 226, 227, 228, 229, 230, 231, 232,
			233, 173, 178, 189, 176, 109, 121, 129, 130, 131,
			132, 133, 134, 135, 136, 137, 145, 146, 147, 148,
			149, 150, 151, 152, 153, 162, 163, 164, 165, 166,
			167, 168, 169, 192, 79, 208, 160, 63, 74, 63,
			63, 63, 63, 63, 63, 63, 63, 63, 63, 63,
			63, 63, 63, 63, 63, 63, 63, 63, 63, 63,
			63, 63, 63, 63, 63, 63, 63, 63, 63, 63,
			177, 66, 67, 68, 69, 70, 71, 72, 73, 81,
			82, 83, 84, 85, 86, 87, 88, 89, 98, 99,
			100, 101, 102, 103, 104, 105, 112, 113, 114, 115,
			116, 117, 118, 119, 120, 138, 139, 140, 141, 142,
			143, 154, 155, 156, 157, 158, 159, 170, 171, 172,
			174, 175, 179, 180, 181, 182, 183, 184, 185, 186,
			187, 188, 190, 191, 63, 63, 63, 63, 63, 63,
			63, 63, 63, 63, 63, 63, 63, 63, 63, 63,
			63, 63, 63, 63, 63, 63, 63, 63, 63, 63,
			63, 63, 63, 95, 224, 161
		};

		// Token: 0x04001EA3 RID: 7843
		private static byte[] table932_2 = new byte[]
		{
			0, 1, 2, 3, 127, 9, 127, 28, 127, 127,
			127, 11, 12, 13, 14, 15, 16, 17, 18, 19,
			127, 127, 8, 127, 24, 25, 127, 127, 28, 29,
			30, 31, 127, 127, 127, 127, 127, 10, 23, 27,
			127, 127, 127, 127, 127, 5, 6, 7, 127, 127,
			22, 127, 127, 127, 127, 4, 127, 127, 127, 127,
			20, 21, 127, 127, 32, 127, 161, 162, 163, 164,
			165, 166, 167, 168, 128, 46, 60, 40, 43, 124,
			38, 169, 170, 171, 172, 173, 174, 175, 176, 177,
			33, 36, 42, 41, 59, 253, 45, 47, 178, 179,
			180, 181, 182, 183, 184, 185, 127, 44, 37, 95,
			62, 63, 186, 187, 188, 189, 190, 191, 192, 193,
			194, 96, 58, 35, 64, 39, 61, 34, 127, 97,
			98, 99, 100, 101, 102, 103, 104, 105, 195, 196,
			197, 198, 199, 200, 127, 106, 107, 108, 109, 110,
			111, 112, 113, 114, 201, 202, 203, 204, 205, 206,
			126, byte.MaxValue, 115, 116, 117, 118, 119, 120, 121, 122,
			207, 208, 209, 91, 210, 211, 94, 160, 92, 212,
			213, 214, 215, 216, 217, 218, 219, 220, 221, 93,
			222, 223, 123, 65, 66, 67, 68, 69, 70, 71,
			72, 73, 127, 127, 127, 127, 127, 127, 125, 74,
			75, 76, 77, 78, 79, 80, 81, 82, 127, 127,
			127, 127, 127, 127, 254, 127, 83, 84, 85, 86,
			87, 88, 89, 90, 127, 127, 127, 127, 127, 127,
			48, 49, 50, 51, 52, 53, 54, 55, 56, 57,
			127, 127, 127, 127, 127, 127
		};

		// Token: 0x04001EA4 RID: 7844
		private static byte[] table932_5 = new byte[]
		{
			0, 1, 2, 3, 55, 45, 46, 47, 22, 5,
			37, 11, 12, 13, 63, 63, 16, 17, 18, 19,
			60, 61, 50, 38, 24, 25, 28, 39, 7, 29,
			30, 31, 64, 90, 127, 123, 224, 108, 80, 125,
			77, 93, 92, 78, 107, 96, 75, 97, 240, 241,
			242, 243, 244, 245, 246, 247, 248, 249, 122, 94,
			76, 126, 110, 111, 124, 193, 194, 195, 196, 197,
			198, 199, 200, 201, 209, 210, 211, 212, 213, 214,
			215, 216, 217, 226, 227, 228, 229, 230, 231, 232,
			233, 112, 91, 128, 176, 109, 121, 98, 99, 100,
			101, 102, 103, 104, 105, 113, 114, 115, 116, 117,
			118, 119, 120, 139, 155, 171, 179, 180, 181, 182,
			183, 184, 185, 192, 79, 208, 161, 63, 177, 63,
			63, 63, 63, 63, 63, 63, 63, 63, 63, 63,
			63, 63, 63, 63, 63, 63, 63, 63, 63, 63,
			63, 63, 63, 63, 63, 63, 63, 63, 63, 63,
			74, 65, 66, 67, 68, 69, 70, 71, 72, 73,
			81, 82, 83, 84, 85, 86, 88, 129, 130, 131,
			132, 133, 134, 135, 136, 137, 138, 140, 141, 142,
			143, 144, 145, 146, 147, 148, 149, 150, 151, 152,
			153, 154, 157, 158, 159, 162, 163, 164, 165, 166,
			167, 168, 169, 170, 172, 173, 174, 175, 186, 187,
			188, 189, 190, 191, 63, 63, 63, 63, 63, 63,
			63, 63, 63, 63, 63, 63, 63, 63, 63, 63,
			63, 63, 63, 63, 63, 63, 63, 63, 63, 63,
			63, 63, 63, 95, 178, 160
		};

		// Token: 0x04001EA5 RID: 7845
		private static byte[] table932_6 = new byte[]
		{
			0, 1, 2, 3, 127, 9, 127, 28, 127, 127,
			127, 11, 12, 13, 14, 15, 16, 17, 18, 19,
			127, 127, 8, 127, 24, 25, 127, 127, 26, 29,
			30, 31, 127, 127, 127, 127, 127, 10, 23, 27,
			127, 127, 127, 127, 127, 5, 6, 7, 127, 127,
			22, 127, 127, 127, 127, 4, 127, 127, 127, 127,
			20, 21, 127, 127, 32, 161, 162, 163, 164, 165,
			166, 167, 168, 169, 160, 46, 60, 40, 43, 124,
			38, 170, 171, 172, 173, 174, 175, 127, 176, 127,
			33, 92, 42, 41, 59, 253, 45, 47, 97, 98,
			99, 100, 101, 102, 103, 104, 127, 44, 37, 95,
			62, 63, 91, 105, 106, 107, 108, 109, 110, 111,
			112, 96, 58, 35, 64, 39, 61, 34, 93, 177,
			178, 179, 180, 181, 182, 183, 184, 185, 186, 113,
			187, 188, 189, 190, 191, 192, 193, 194, 195, 196,
			197, 198, 199, 200, 201, 114, 127, 202, 203, 204,
			byte.MaxValue, 126, 205, 206, 207, 208, 209, 210, 211, 212,
			213, 115, 214, 215, 216, 217, 94, 128, 254, 116,
			117, 118, 119, 120, 121, 122, 218, 219, 220, 221,
			222, 223, 123, 65, 66, 67, 68, 69, 70, 71,
			72, 73, 127, 127, 127, 127, 127, 127, 125, 74,
			75, 76, 77, 78, 79, 80, 81, 82, 127, 127,
			127, 127, 127, 127, 36, 127, 83, 84, 85, 86,
			87, 88, 89, 90, 127, 127, 127, 127, 127, 127,
			48, 49, 50, 51, 52, 53, 54, 55, 56, 57,
			127, 127, 127, 127, 127, 127
		};

		// Token: 0x04001EA6 RID: 7846
		private static HisSBMBCSConverter.FileLoadStatus fileloadstatus;

		// Token: 0x04001EA7 RID: 7847
		private static byte[] cod_4040_ibmpc = new byte[] { 64, 64 };

		// Token: 0x04001EA8 RID: 7848
		private static byte[] cod_fcfb = new byte[] { 252, 251 };

		// Token: 0x04001EA9 RID: 7849
		private static byte[] cod_fcfc = new byte[] { 252, 252 };

		// Token: 0x04001EAA RID: 7850
		private static byte[] cod_4040 = new byte[] { 64, 64 };

		// Token: 0x04001EAB RID: 7851
		private static byte[] cod_8140 = new byte[] { 129, 64 };

		// Token: 0x04001EAC RID: 7852
		private static byte[] cod_8141 = new byte[] { 129, 65 };

		// Token: 0x04001EAD RID: 7853
		private static byte[] cod_9ffc = new byte[] { 159, 252 };

		// Token: 0x04001EAE RID: 7854
		private static byte[] cod_e040 = new byte[] { 224, 64 };

		// Token: 0x04001EAF RID: 7855
		private static byte[] cod_fcfa = new byte[] { 252, 250 };

		// Token: 0x04001EB0 RID: 7856
		private static byte[] work2 = new byte[2];

		// Token: 0x04001EB1 RID: 7857
		private static byte[] cod_fefe = new byte[] { 254, 254 };

		// Token: 0x04001EB2 RID: 7858
		private byte[] leadBytes = new byte[] { 129, 159, 224, 252, 0, 0 };

		// Token: 0x02000634 RID: 1588
		private enum MappingFileSizes
		{
			// Token: 0x04001EB4 RID: 7860
			SbcsSize = 256,
			// Token: 0x04001EB5 RID: 7861
			DbcsSize = 23942
		}
	}
}
