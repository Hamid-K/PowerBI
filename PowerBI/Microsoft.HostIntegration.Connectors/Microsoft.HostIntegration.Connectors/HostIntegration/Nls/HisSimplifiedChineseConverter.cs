using System;
using System.IO;
using System.Security;
using System.Security.Permissions;
using Microsoft.HostIntegration.StrictResources.Nls;

namespace Microsoft.HostIntegration.Nls
{
	// Token: 0x02000617 RID: 1559
	internal class HisSimplifiedChineseConverter : HisSBMBCSConverter
	{
		// Token: 0x060034BC RID: 13500 RVA: 0x000B05B8 File Offset: 0x000AE7B8
		[SecurityCritical]
		[SecuritySafeCritical]
		static HisSimplifiedChineseConverter()
		{
			string text = "";
			string text2 = "";
			HisSimplifiedChineseConverter.fileloadstatus = HisSBMBCSConverter.FileLoadStatus.Uninitialized;
			text2 = HisConverter.installPath.Value + "\\SNASBCS.TBL";
			text = HisConverter.installPath.Value + "\\SNADBCS.TBL";
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
			BinaryReader dataBinaryReader = HisSBMBCSConverter.GetDataBinaryReader("SNASBCS.TBL");
			if (!Memory.ReadTable(dataBinaryReader.BaseStream, HisSimplifiedChineseConverter.table936_1, 256))
			{
				throw new NotSupportedException(SR.CodePageTableNotFound(text2), new FileLoadException(SR.CodePageTableNotFound(text2)));
			}
			if (!Memory.ReadTable(dataBinaryReader.BaseStream, HisSimplifiedChineseConverter.table936_2, 256))
			{
				throw new NotSupportedException(SR.CodePageTableNotFound(text2), new FileLoadException(SR.CodePageTableNotFound(text2)));
			}
			dataBinaryReader.Close();
			HisSimplifiedChineseConverter.fileloadstatus = HisSBMBCSConverter.FileLoadStatus.LoadedSingleByteTableOnly;
			BinaryReader dataBinaryReader2 = HisSBMBCSConverter.GetDataBinaryReader("SNADBCS.TBL");
			if (!Memory.ReadTable(dataBinaryReader2.BaseStream, HisSimplifiedChineseConverter.table936_ibm_pc, 20480))
			{
				throw new NotSupportedException(SR.CodePageTableNotFound(text), new FileLoadException(SR.CodePageTableNotFound(text)));
			}
			dataBinaryReader2.Close();
			HisSimplifiedChineseConverter.fileloadstatus = HisSBMBCSConverter.FileLoadStatus.LoadedBothTables;
			HisSimplifiedChineseConverter.table936_init();
		}

		// Token: 0x060034BD RID: 13501 RVA: 0x000B0798 File Offset: 0x000AE998
		private static void table936_init()
		{
			ushort[] array = new ushort[2];
			if (HisSimplifiedChineseConverter.table936_pc_ibm != null)
			{
				return;
			}
			HisSimplifiedChineseConverter.table936_pc_ibm = new byte[20480];
			if (HisSimplifiedChineseConverter.table936_pc_ibm == null)
			{
				throw new OutOfMemoryException(SR.OutOfMemoryAllocatingTable("Simplified Chinese "));
			}
			Memory.SetMemory(HisSimplifiedChineseConverter.table936_pc_ibm, 0L, 0, 20480L);
			for (ushort num = 0; num < 10240; num += 1)
			{
				if (HisSimplifiedChineseConverter.table936_ibm_pc[(int)(num * 2)] != 0 || HisSimplifiedChineseConverter.table936_ibm_pc[(int)(num * 2 + 1)] != 0)
				{
					array[0] = (ushort)((HisSimplifiedChineseConverter.table936_ibm_pc[(int)(num * 2)] - 161) * 94);
					array[1] = (ushort)(HisSimplifiedChineseConverter.table936_ibm_pc[(int)(num * 2 + 1)] - 161);
					ushort num2 = (array[0] + array[1]) * 2;
					if (num == 0)
					{
						Memory.MemoryCopy(HisSimplifiedChineseConverter.cod_4040, 0, HisSimplifiedChineseConverter.table936_pc_ibm, (int)num2, 2L);
					}
					else
					{
						array[0] = num - 1;
						array[1] = array[0] % 189;
						ushort[] array2 = array;
						int num3 = 0;
						array2[num3] /= 189;
						if (num >= 8316)
						{
							HisSimplifiedChineseConverter.work2[0] = (byte)(array[0] + 74);
						}
						else
						{
							HisSimplifiedChineseConverter.work2[0] = (byte)(array[0] + 65);
						}
						HisSimplifiedChineseConverter.work2[1] = (byte)(array[1] + 65);
						Memory.MemoryCopy(HisSimplifiedChineseConverter.work2, 0, HisSimplifiedChineseConverter.table936_pc_ibm, (int)num2, 2L);
					}
				}
			}
		}

		// Token: 0x060034BE RID: 13502 RVA: 0x000B08D9 File Offset: 0x000AEAD9
		internal HisSimplifiedChineseConverter(HisEncoding enc)
		{
			byte[] array = new byte[4];
			array[0] = 129;
			array[1] = 254;
			this.leadBytes = array;
			base..ctor(enc);
		}

		// Token: 0x060034BF RID: 13503 RVA: 0x000B08FE File Offset: 0x000AEAFE
		internal override byte[] GetLeadBytes()
		{
			return this.leadBytes;
		}

		// Token: 0x060034C0 RID: 13504 RVA: 0x000B0906 File Offset: 0x000AEB06
		protected override bool IsLeadByte(char ch)
		{
			return ch >= '\u0081' && ch <= 'þ';
		}

		// Token: 0x060034C1 RID: 13505 RVA: 0x000B0920 File Offset: 0x000AEB20
		protected override int ConvertToEbcdic(bool bSingleByte, byte[] src, byte[] outbuff, int inPos, int outPos)
		{
			ushort[] array = new ushort[2];
			if (HisSimplifiedChineseConverter.fileloadstatus != HisSBMBCSConverter.FileLoadStatus.LoadedBothTables)
			{
				throw new FileNotFoundException(SR.ErrorOpeningTranslationTable("Simplified Chinese"));
			}
			if (bSingleByte)
			{
				outbuff[outPos] = HisSimplifiedChineseConverter.table936_1[(int)src[inPos]];
			}
			else if (src[inPos] >= 161 && src[inPos] <= 254 && src[inPos + 1] >= 161 && src[inPos + 1] <= 254)
			{
				array[0] = (ushort)((src[inPos] - 161) * 94);
				array[1] = (ushort)(src[inPos + 1] - 161);
				ushort num = (array[0] + array[1]) * 2;
				Memory.MemoryCopy(HisSimplifiedChineseConverter.table936_pc_ibm, (int)num, outbuff, outPos, 2L);
			}
			else
			{
				Memory.MemoryCopy(HisSimplifiedChineseConverter.cod_426f, 0, outbuff, outPos, 2L);
			}
			return 0;
		}

		// Token: 0x060034C2 RID: 13506 RVA: 0x000B09E0 File Offset: 0x000AEBE0
		protected override int ConvertToUnicode(bool bSingleByte, byte[] src, byte[] outbuff, int inPos, int outPos)
		{
			ushort[] array = new ushort[2];
			if (HisSimplifiedChineseConverter.fileloadstatus != HisSBMBCSConverter.FileLoadStatus.LoadedBothTables)
			{
				throw new FileNotFoundException(SR.ErrorOpeningTranslationTable("Simplified Chinese"));
			}
			if (bSingleByte)
			{
				outbuff[outPos] = HisSimplifiedChineseConverter.table936_2[(int)src[inPos]];
			}
			else if (Memory.memcmp(src, inPos, HisSimplifiedChineseConverter.cod_4040_ibmpc, 0, 2L) == 0)
			{
				Memory.MemoryCopy(HisSimplifiedChineseConverter.table936_ibm_pc, 0, outbuff, outPos, 2L);
			}
			else if (src[inPos] >= 65 && src[inPos] <= 108 && src[inPos + 1] >= 65 && src[inPos + 1] <= 253)
			{
				array[0] = (ushort)((src[inPos] - 65) * 189);
				array[1] = (ushort)(src[inPos + 1] - 64);
				ushort num = (array[0] + array[1]) * 2;
				if (Memory.memcmp(HisSimplifiedChineseConverter.table936_ibm_pc, (int)num, HisSimplifiedChineseConverter.cod_null_ibmpc, 0, 2L) == 0)
				{
					Memory.MemoryCopy(HisSimplifiedChineseConverter.cod_a3bf, 0, outbuff, outPos, 2L);
				}
				else
				{
					Memory.MemoryCopy(HisSimplifiedChineseConverter.table936_ibm_pc, (int)num, outbuff, outPos, 2L);
				}
			}
			else if (src[inPos] >= 118 && src[inPos] <= 127 && src[inPos + 1] >= 65 && src[inPos + 1] <= 253)
			{
				array[0] = (ushort)((src[inPos] - 74) * 189);
				array[1] = (ushort)(src[inPos + 1] - 64);
				ushort num = (array[0] + array[1]) * 2;
				if (Memory.memcmp(HisSimplifiedChineseConverter.table936_ibm_pc, (int)num, HisSimplifiedChineseConverter.cod_null_ibmpc, 0, 2L) == 0)
				{
					Memory.MemoryCopy(HisSimplifiedChineseConverter.cod_a3bf, 0, outbuff, outPos, 2L);
				}
				else
				{
					Memory.MemoryCopy(HisSimplifiedChineseConverter.table936_ibm_pc, (int)num, outbuff, outPos, 2L);
				}
			}
			else
			{
				Memory.MemoryCopy(HisSimplifiedChineseConverter.cod_a3bf, 0, outbuff, outPos, 2L);
			}
			return 0;
		}

		// Token: 0x04001DD9 RID: 7641
		private const string SingleByteTableFileName = "SNASBCS.TBL";

		// Token: 0x04001DDA RID: 7642
		private const string DoubleByteTableFileName = "SNADBCS.TBL";

		// Token: 0x04001DDB RID: 7643
		private static byte[] cod_null = new byte[2];

		// Token: 0x04001DDC RID: 7644
		private static byte[] cod_426f = new byte[] { 66, 111 };

		// Token: 0x04001DDD RID: 7645
		private byte[] leadBytes;

		// Token: 0x04001DDE RID: 7646
		private static byte[] cod_4040 = new byte[] { 64, 64 };

		// Token: 0x04001DDF RID: 7647
		private static byte[] work2 = new byte[2];

		// Token: 0x04001DE0 RID: 7648
		private static byte[] table936_pc_ibm;

		// Token: 0x04001DE1 RID: 7649
		private static byte[] table936_ibm_pc = new byte[20480];

		// Token: 0x04001DE2 RID: 7650
		private static byte[] table936_1 = new byte[256];

		// Token: 0x04001DE3 RID: 7651
		private static byte[] table936_2 = new byte[256];

		// Token: 0x04001DE4 RID: 7652
		private static HisSBMBCSConverter.FileLoadStatus fileloadstatus;

		// Token: 0x04001DE5 RID: 7653
		private static byte[] cod_4040_ibmpc = new byte[] { 64, 64 };

		// Token: 0x04001DE6 RID: 7654
		private static byte[] cod_null_ibmpc = new byte[2];

		// Token: 0x04001DE7 RID: 7655
		private static byte[] cod_a3bf = new byte[] { 163, 191 };

		// Token: 0x02000618 RID: 1560
		private enum MappingFileSizes
		{
			// Token: 0x04001DE9 RID: 7657
			SbcsSize = 256,
			// Token: 0x04001DEA RID: 7658
			DbcsSize = 20480
		}
	}
}
