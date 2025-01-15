using System;
using System.IO;
using System.Security;
using System.Security.Permissions;
using Microsoft.HostIntegration.StrictResources.Nls;

namespace Microsoft.HostIntegration.Nls
{
	// Token: 0x02000635 RID: 1589
	internal class HisKoreanConverter : HisSBMBCSConverter
	{
		// Token: 0x06003599 RID: 13721 RVA: 0x000B42F5 File Offset: 0x000B24F5
		internal HisKoreanConverter(HisEncoding enc)
		{
			byte[] array = new byte[4];
			array[0] = 129;
			array[1] = 254;
			this.leadBytes = array;
			base..ctor(enc);
		}

		// Token: 0x0600359A RID: 13722 RVA: 0x000B431C File Offset: 0x000B251C
		[SecurityCritical]
		[SecuritySafeCritical]
		[RegistryPermission(SecurityAction.Assert, Read = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\SNA Server\\CurrentVersion")]
		static HisKoreanConverter()
		{
			string text = "";
			string text2 = "";
			HisKoreanConverter.fileloadstatus = HisSBMBCSConverter.FileLoadStatus.Uninitialized;
			text2 = HisConverter.installPath.Value + "\\SNASBCK.TBL";
			text = HisConverter.installPath.Value + "\\SNADBCK.TBL";
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
			BinaryReader dataBinaryReader = HisSBMBCSConverter.GetDataBinaryReader("SNASBCK.TBL");
			if (!Memory.ReadTable(dataBinaryReader.BaseStream, HisKoreanConverter.table949_1, 256))
			{
				throw new NotSupportedException(SR.CodePageTableNotFound(text2), new FileLoadException(SR.CodePageTableNotFound(text2)));
			}
			if (!Memory.ReadTable(dataBinaryReader.BaseStream, HisKoreanConverter.table949_2, 256))
			{
				throw new NotSupportedException(SR.CodePageTableNotFound(text2), new FileLoadException(SR.CodePageTableNotFound(text2)));
			}
			dataBinaryReader.Close();
			HisKoreanConverter.fileloadstatus = HisSBMBCSConverter.FileLoadStatus.LoadedSingleByteTableOnly;
			BinaryReader dataBinaryReader2 = HisSBMBCSConverter.GetDataBinaryReader("SNADBCK.TBL");
			if (!Memory.ReadTable(dataBinaryReader2.BaseStream, HisKoreanConverter.table949_ibm_pc, 45982))
			{
				throw new NotSupportedException(SR.CodePageTableNotFound(text), new FileLoadException(SR.CodePageTableNotFound(text)));
			}
			dataBinaryReader2.Close();
			HisKoreanConverter.fileloadstatus = HisSBMBCSConverter.FileLoadStatus.LoadedBothTables;
			HisKoreanConverter.table949_init();
		}

		// Token: 0x0600359B RID: 13723 RVA: 0x000B44CC File Offset: 0x000B26CC
		private static void table949_init()
		{
			ushort[] array = new ushort[2];
			if (HisKoreanConverter.table949_pc_ibm != null)
			{
				return;
			}
			HisKoreanConverter.table949_pc_ibm = new byte[45982];
			if (HisKoreanConverter.table949_pc_ibm == null)
			{
				throw new OutOfMemoryException(SR.OutOfMemoryAllocatingTable("Korean"));
			}
			Memory.SetMemory(HisKoreanConverter.table949_pc_ibm, 0L, 0, 45982L);
			for (ushort num = 0; num < 22991; num += 1)
			{
				if (HisKoreanConverter.table949_ibm_pc[(int)(num * 2)] != 0 || HisKoreanConverter.table949_ibm_pc[(int)(num * 2 + 1)] != 0)
				{
					array[0] = (ushort)((HisKoreanConverter.table949_ibm_pc[(int)(num * 2)] - 129) * 94);
					array[1] = (ushort)(HisKoreanConverter.table949_ibm_pc[(int)(num * 2 + 1)] - 161);
					ushort num2 = (array[0] + array[1]) * 2;
					if (num == 0)
					{
						Memory.MemoryCopy(HisKoreanConverter.cod_4040, 0, HisKoreanConverter.table949_pc_ibm, (int)num2, 2L);
					}
					else
					{
						array[0] = num - 1;
						array[1] = array[0] % 190;
						ushort[] array2 = array;
						int num3 = 0;
						array2[num3] /= 190;
						if (num >= 7600)
						{
							HisKoreanConverter.work2[0] = (byte)(array[0] + 92);
						}
						else if (num >= 2090)
						{
							HisKoreanConverter.work2[0] = (byte)(array[0] + 69);
						}
						else
						{
							HisKoreanConverter.work2[0] = (byte)(array[0] + 65);
						}
						HisKoreanConverter.work2[1] = (byte)(array[1] + 65);
						Memory.MemoryCopy(HisKoreanConverter.work2, 0, HisKoreanConverter.table949_pc_ibm, (int)num2, 2L);
					}
				}
			}
		}

		// Token: 0x0600359C RID: 13724 RVA: 0x000B4628 File Offset: 0x000B2828
		internal override byte[] GetLeadBytes()
		{
			return this.leadBytes;
		}

		// Token: 0x0600359D RID: 13725 RVA: 0x000B0906 File Offset: 0x000AEB06
		protected override bool IsLeadByte(char ch)
		{
			return ch >= '\u0081' && ch <= 'þ';
		}

		// Token: 0x0600359E RID: 13726 RVA: 0x000B4630 File Offset: 0x000B2830
		protected override int ConvertToEbcdic(bool bSingleByte, byte[] src, byte[] outbuff, int inPos, int outPos)
		{
			ushort[] array = new ushort[2];
			if (HisKoreanConverter.fileloadstatus != HisSBMBCSConverter.FileLoadStatus.LoadedBothTables)
			{
				throw new FileNotFoundException(SR.ErrorOpeningTranslationTable("Korean"));
			}
			if (bSingleByte)
			{
				outbuff[outPos] = HisKoreanConverter.table949_1[(int)src[inPos]];
			}
			else
			{
				array[0] = (ushort)((src[inPos] - 129) * 94);
				if (src[inPos + 1] >= 161 && src[inPos + 1] <= 254)
				{
					array[1] = (ushort)(src[inPos + 1] - 161);
					ushort num = (array[0] + array[1]) * 2;
					Memory.MemoryCopy(HisKoreanConverter.table949_pc_ibm, (int)num, outbuff, outPos, 2L);
				}
				else
				{
					Memory.MemoryCopy(HisKoreanConverter.cod_null, 0, outbuff, outPos, 2L);
				}
			}
			return 0;
		}

		// Token: 0x0600359F RID: 13727 RVA: 0x000B46DC File Offset: 0x000B28DC
		protected override int ConvertToUnicode(bool bSingleByte, byte[] src, byte[] outbuff, int inPos, int outPos)
		{
			ushort[] array = new ushort[2];
			if (HisKoreanConverter.fileloadstatus != HisSBMBCSConverter.FileLoadStatus.LoadedBothTables)
			{
				throw new FileNotFoundException(SR.ErrorOpeningTranslationTable("Korean"));
			}
			if (bSingleByte)
			{
				outbuff[outPos] = HisKoreanConverter.table949_2[(int)src[inPos]];
			}
			else if (Memory.memcmp(src, inPos, HisKoreanConverter.cod_4040_ibmpc, 0, 2L) == 0)
			{
				Memory.MemoryCopy(HisKoreanConverter.table949_ibm_pc, 0, outbuff, outPos, 2L);
			}
			else if (src[inPos] >= 65 && src[inPos] <= 75 && src[inPos + 1] >= 65 && src[inPos + 1] <= 254)
			{
				array[0] = (ushort)((src[inPos] - 65) * 190);
				array[1] = (ushort)(src[inPos + 1] - 64);
				ushort num = (array[0] + array[1]) * 2;
				Memory.MemoryCopy(HisKoreanConverter.table949_ibm_pc, (int)num, outbuff, outPos, 2L);
			}
			else if (src[inPos] >= 80 && src[inPos] <= 108 && src[inPos + 1] >= 65 && src[inPos + 1] <= 254)
			{
				array[0] = (ushort)((src[inPos] - 69) * 190);
				array[1] = (ushort)(src[inPos + 1] - 64);
				ushort num = (array[0] + array[1]) * 2;
				Memory.MemoryCopy(HisKoreanConverter.table949_ibm_pc, (int)num, outbuff, outPos, 2L);
			}
			else if (src[inPos] >= 132 && src[inPos] <= 212 && src[inPos + 1] >= 65 && src[inPos + 1] <= 254)
			{
				array[0] = (ushort)((src[inPos] - 92) * 190);
				array[1] = (ushort)(src[inPos + 1] - 64);
				ushort num = (array[0] + array[1]) * 2;
				Memory.MemoryCopy(HisKoreanConverter.table949_ibm_pc, (int)num, outbuff, outPos, 2L);
			}
			else
			{
				Memory.MemoryCopy(HisKoreanConverter.cod_null_ibmpc, 0, outbuff, outPos, 2L);
			}
			return 0;
		}

		// Token: 0x04001EB6 RID: 7862
		private byte[] leadBytes;

		// Token: 0x04001EB7 RID: 7863
		private const string SingleByteTableFileName = "SNASBCK.TBL";

		// Token: 0x04001EB8 RID: 7864
		private const string DoubleByteTableFileName = "SNADBCK.TBL";

		// Token: 0x04001EB9 RID: 7865
		private static byte[] cod_null = new byte[2];

		// Token: 0x04001EBA RID: 7866
		private static byte[] table949_pc_ibm;

		// Token: 0x04001EBB RID: 7867
		private static byte[] table949_ibm_pc = new byte[45982];

		// Token: 0x04001EBC RID: 7868
		private static byte[] table949_1 = new byte[256];

		// Token: 0x04001EBD RID: 7869
		private static byte[] table949_2 = new byte[256];

		// Token: 0x04001EBE RID: 7870
		private static HisSBMBCSConverter.FileLoadStatus fileloadstatus;

		// Token: 0x04001EBF RID: 7871
		private static byte[] cod_4040 = new byte[] { 64, 64 };

		// Token: 0x04001EC0 RID: 7872
		private static byte[] work2 = new byte[2];

		// Token: 0x04001EC1 RID: 7873
		private static byte[] cod_4040_ibmpc = new byte[] { 64, 64 };

		// Token: 0x04001EC2 RID: 7874
		private static byte[] cod_null_ibmpc = new byte[2];

		// Token: 0x02000636 RID: 1590
		private enum MappingFileSizes
		{
			// Token: 0x04001EC4 RID: 7876
			SbcsSize = 256,
			// Token: 0x04001EC5 RID: 7877
			DbcsSize = 45982
		}
	}
}
