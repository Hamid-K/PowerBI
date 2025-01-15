using System;
using System.IO;
using System.Security;
using System.Security.Permissions;
using Microsoft.HostIntegration.StrictResources.Nls;

namespace Microsoft.HostIntegration.Nls
{
	// Token: 0x0200063C RID: 1596
	internal class HisTraditionalChineseConverter : HisSBMBCSConverter
	{
		// Token: 0x060035B4 RID: 13748 RVA: 0x000B5610 File Offset: 0x000B3810
		internal HisTraditionalChineseConverter(HisEncoding enc)
		{
			byte[] array = new byte[4];
			array[0] = 129;
			array[1] = 254;
			this.leadBytes = array;
			base..ctor(enc);
		}

		// Token: 0x060035B5 RID: 13749 RVA: 0x000B5635 File Offset: 0x000B3835
		internal override byte[] GetLeadBytes()
		{
			return this.leadBytes;
		}

		// Token: 0x060035B6 RID: 13750 RVA: 0x000B5640 File Offset: 0x000B3840
		[SecurityCritical]
		[SecuritySafeCritical]
		static HisTraditionalChineseConverter()
		{
			string text = "";
			string text2 = "";
			HisTraditionalChineseConverter.fileloadstatus = HisSBMBCSConverter.FileLoadStatus.Uninitialized;
			text2 = HisConverter.installPath.Value + "\\SNASBCT.TBL";
			text = HisConverter.installPath.Value + "\\SNADBCT.TBL";
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
			BinaryReader dataBinaryReader = HisSBMBCSConverter.GetDataBinaryReader("SNASBCT.TBL");
			if (!Memory.ReadTable(dataBinaryReader.BaseStream, HisTraditionalChineseConverter.table950_1, 256))
			{
				throw new NotSupportedException(SR.CodePageTableNotFound(text2), new FileLoadException(SR.CodePageTableNotFound(text2)));
			}
			if (!Memory.ReadTable(dataBinaryReader.BaseStream, HisTraditionalChineseConverter.table950_2, 256))
			{
				throw new NotSupportedException(SR.CodePageTableNotFound(text2), new FileLoadException(SR.CodePageTableNotFound(text2)));
			}
			dataBinaryReader.Close();
			HisTraditionalChineseConverter.fileloadstatus = HisSBMBCSConverter.FileLoadStatus.LoadedSingleByteTableOnly;
			BinaryReader dataBinaryReader2 = HisSBMBCSConverter.GetDataBinaryReader("SNADBCT.TBL");
			if (!Memory.ReadTable(dataBinaryReader2.BaseStream, HisTraditionalChineseConverter.table950_ibm_pc, 43094))
			{
				throw new NotSupportedException(SR.CodePageTableNotFound(text), new FileLoadException(SR.CodePageTableNotFound(text)));
			}
			dataBinaryReader2.Close();
			HisTraditionalChineseConverter.fileloadstatus = HisSBMBCSConverter.FileLoadStatus.LoadedBothTables;
			HisTraditionalChineseConverter.table950_init();
		}

		// Token: 0x060035B7 RID: 13751 RVA: 0x000B5824 File Offset: 0x000B3A24
		private static void table950_init()
		{
			ushort[] array = new ushort[2];
			if (HisTraditionalChineseConverter.table950_pc_ibm != null)
			{
				return;
			}
			HisTraditionalChineseConverter.table950_pc_ibm = new byte[43094];
			if (HisTraditionalChineseConverter.table950_pc_ibm == null)
			{
				throw new OutOfMemoryException(SR.OutOfMemoryAllocatingTable("Traditional Chinese"));
			}
			Memory.SetMemory(HisTraditionalChineseConverter.table950_pc_ibm, 0L, 254, 43094L);
			for (ushort num = 0; num < 21547; num += 1)
			{
				if (HisTraditionalChineseConverter.table950_ibm_pc[(int)(num * 2)] != 0 || HisTraditionalChineseConverter.table950_ibm_pc[(int)(num * 2 + 1)] != 0)
				{
					array[0] = (ushort)((HisTraditionalChineseConverter.table950_ibm_pc[(int)(num * 2)] - 129) * 157);
					if (HisTraditionalChineseConverter.table950_ibm_pc[(int)(num * 2 + 1)] >= 64 && HisTraditionalChineseConverter.table950_ibm_pc[(int)(num * 2 + 1)] <= 126)
					{
						array[1] = (ushort)(HisTraditionalChineseConverter.table950_ibm_pc[(int)(num * 2 + 1)] - 64);
					}
					else
					{
						if (HisTraditionalChineseConverter.table950_ibm_pc[(int)(num * 2 + 1)] < 161 || HisTraditionalChineseConverter.table950_ibm_pc[(int)(num * 2 + 1)] > 254)
						{
							goto IL_0184;
						}
						array[1] = (ushort)(HisTraditionalChineseConverter.table950_ibm_pc[(int)(num * 2 + 1)] - 98);
					}
					ushort num2 = (array[0] + array[1]) * 2;
					if (num == 0)
					{
						Memory.MemoryCopy(HisTraditionalChineseConverter.cod_4040, 0, HisTraditionalChineseConverter.table950_pc_ibm, (int)num2, 2L);
					}
					else
					{
						array[0] = num - 1;
						array[1] = array[0] % 189;
						ushort[] array2 = array;
						int num3 = 0;
						array2[num3] /= 189;
						if (num >= 15309)
						{
							HisTraditionalChineseConverter.work2[0] = (byte)(array[0] + 113);
						}
						else
						{
							HisTraditionalChineseConverter.work2[0] = (byte)(array[0] + 65);
						}
						HisTraditionalChineseConverter.work2[1] = (byte)(array[1] + 65);
						Memory.MemoryCopy(HisTraditionalChineseConverter.work2, 0, HisTraditionalChineseConverter.table950_pc_ibm, (int)num2, 2L);
					}
				}
				IL_0184:;
			}
		}

		// Token: 0x060035B8 RID: 13752 RVA: 0x000B0906 File Offset: 0x000AEB06
		protected override bool IsLeadByte(char ch)
		{
			return ch >= '\u0081' && ch <= 'þ';
		}

		// Token: 0x060035B9 RID: 13753 RVA: 0x000B59C8 File Offset: 0x000B3BC8
		protected override int ConvertToEbcdic(bool bSingleByte, byte[] src, byte[] outbuff, int inPos, int outPos)
		{
			ushort[] array = new ushort[2];
			if (HisTraditionalChineseConverter.fileloadstatus != HisSBMBCSConverter.FileLoadStatus.LoadedBothTables)
			{
				throw new FileNotFoundException(SR.ErrorOpeningTranslationTable("Traditional Chinese"));
			}
			if (bSingleByte)
			{
				outbuff[outPos] = HisTraditionalChineseConverter.table950_1[(int)src[inPos]];
			}
			else
			{
				array[0] = (ushort)((src[inPos] - 129) * 157);
				if ((src[inPos + 1] >= 64 && src[inPos + 1] <= 126) || (src[inPos + 1] >= 161 && src[inPos + 1] <= 254))
				{
					if (src[inPos + 1] < 127)
					{
						array[1] = (ushort)(src[inPos + 1] - 64);
					}
					else
					{
						array[1] = (ushort)(src[inPos + 1] - 98);
					}
					ushort num = (array[0] + array[1]) * 2;
					Memory.MemoryCopy(HisTraditionalChineseConverter.table950_pc_ibm, (int)num, outbuff, outPos, 2L);
				}
				else
				{
					Memory.MemoryCopy(HisTraditionalChineseConverter.cod_fefe, 0, outbuff, outPos, 2L);
				}
			}
			return 0;
		}

		// Token: 0x060035BA RID: 13754 RVA: 0x000B5AA4 File Offset: 0x000B3CA4
		protected override int ConvertToUnicode(bool bSingleByte, byte[] src, byte[] outbuff, int inPos, int outPos)
		{
			ushort[] array = new ushort[2];
			if (HisTraditionalChineseConverter.fileloadstatus != HisSBMBCSConverter.FileLoadStatus.LoadedBothTables)
			{
				throw new FileNotFoundException(SR.ErrorOpeningTranslationTable("Traditional Chinese"));
			}
			if (bSingleByte)
			{
				outbuff[outPos] = HisTraditionalChineseConverter.table950_2[(int)src[inPos]];
			}
			else if (Memory.memcmp(src, inPos, HisTraditionalChineseConverter.cod_4040_ibmpc, 0, 2L) == 0)
			{
				Memory.MemoryCopy(HisTraditionalChineseConverter.table950_ibm_pc, 0, outbuff, outPos, 2L);
			}
			else if (src[inPos] >= 65 && src[inPos] <= 145 && src[inPos + 1] >= 65 && src[inPos + 1] <= 253)
			{
				array[0] = (ushort)((src[inPos] - 65) * 189);
				array[1] = (ushort)(src[inPos + 1] - 64);
				ushort num = (array[0] + array[1]) * 2;
				if (Memory.memcmp(HisTraditionalChineseConverter.table950_ibm_pc, (int)num, HisTraditionalChineseConverter.cod_null_ibmpc, 0, 2L) == 0)
				{
					Memory.MemoryCopy(HisTraditionalChineseConverter.cod_c8fe_ibmpc, 0, outbuff, outPos, 2L);
				}
				else
				{
					Memory.MemoryCopy(HisTraditionalChineseConverter.table950_ibm_pc, (int)num, outbuff, outPos, 2L);
				}
			}
			else if (src[inPos] >= 194 && src[inPos] <= 226 && src[inPos + 1] >= 65 && src[inPos + 1] <= 253)
			{
				array[0] = (ushort)((src[inPos] - 113) * 189);
				array[1] = (ushort)(src[inPos + 1] - 64);
				ushort num = (array[0] + array[1]) * 2;
				if (Memory.memcmp(HisTraditionalChineseConverter.table950_ibm_pc, (int)num, HisTraditionalChineseConverter.cod_null_ibmpc, 0, 2L) == 0)
				{
					Memory.MemoryCopy(HisTraditionalChineseConverter.cod_c8fe_ibmpc, 0, outbuff, outPos, 2L);
				}
				else
				{
					Memory.memcmp(HisTraditionalChineseConverter.table950_ibm_pc, (int)num, outbuff, outPos, 2L);
				}
			}
			else
			{
				Memory.MemoryCopy(HisTraditionalChineseConverter.cod_c8fe_ibmpc, 0, outbuff, outPos, 2L);
			}
			return 0;
		}

		// Token: 0x04001ED3 RID: 7891
		private const string SingleByteTableFileName = "SNASBCT.TBL";

		// Token: 0x04001ED4 RID: 7892
		private const string DoubleByteTableFileName = "SNADBCT.TBL";

		// Token: 0x04001ED5 RID: 7893
		private byte[] leadBytes;

		// Token: 0x04001ED6 RID: 7894
		private static byte[] cod_4040 = new byte[] { 64, 64 };

		// Token: 0x04001ED7 RID: 7895
		private static byte[] work2 = new byte[2];

		// Token: 0x04001ED8 RID: 7896
		private static byte[] cod_4040_ibmpc = new byte[] { 64, 64 };

		// Token: 0x04001ED9 RID: 7897
		private static byte[] cod_null_ibmpc = new byte[2];

		// Token: 0x04001EDA RID: 7898
		private static byte[] cod_c8fe_ibmpc = new byte[] { 200, 254 };

		// Token: 0x04001EDB RID: 7899
		private static byte[] table950_pc_ibm;

		// Token: 0x04001EDC RID: 7900
		private static byte[] table950_ibm_pc = new byte[43094];

		// Token: 0x04001EDD RID: 7901
		private static byte[] table950_1 = new byte[256];

		// Token: 0x04001EDE RID: 7902
		private static byte[] table950_2 = new byte[256];

		// Token: 0x04001EDF RID: 7903
		private static HisSBMBCSConverter.FileLoadStatus fileloadstatus;

		// Token: 0x04001EE0 RID: 7904
		private static byte[] cod_null = new byte[2];

		// Token: 0x04001EE1 RID: 7905
		private static byte[] cod_fefe = new byte[] { 254, 254 };

		// Token: 0x0200063D RID: 1597
		private enum MappingFileSizes
		{
			// Token: 0x04001EE3 RID: 7907
			SbcsSize = 256,
			// Token: 0x04001EE4 RID: 7908
			DbcsSize = 43094
		}
	}
}
