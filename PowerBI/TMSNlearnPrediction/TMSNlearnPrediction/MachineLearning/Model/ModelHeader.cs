using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Model
{
	// Token: 0x02000472 RID: 1138
	[StructLayout(LayoutKind.Explicit, Size = 256)]
	public struct ModelHeader
	{
		// Token: 0x060017A6 RID: 6054 RVA: 0x00087B50 File Offset: 0x00085D50
		public static void BeginWrite(BinaryWriter writer, out long fpMin, out ModelHeader header)
		{
			Contracts.CheckValue<BinaryWriter>(writer, "writer");
			fpMin = Utils.FpCur(writer);
			header = default(ModelHeader);
			header.Signature = 5495874027660528717UL;
			header.VerWritten = 65537U;
			header.VerReadable = 65537U;
			header.FpModel = 256L;
			byte[] array = new byte[256];
			writer.Write(array);
			Contracts.CheckIO(Utils.FpCur(writer) == fpMin + 256L);
		}

		// Token: 0x060017A7 RID: 6055 RVA: 0x00087BD0 File Offset: 0x00085DD0
		public static void EndWrite(BinaryWriter writer, long fpMin, ref ModelHeader header, NormStr.Pool pool = null)
		{
			Contracts.CheckValue<BinaryWriter>(writer, "writer");
			Contracts.CheckParam(fpMin >= 0L, "fpMin");
			ModelHeader.EndModelCore(writer, fpMin, ref header);
			Contracts.Check(header.FpStringTable == 0L);
			Contracts.Check(header.CbStringTable == 0L);
			Contracts.Check(header.FpStringChars == 0L);
			Contracts.Check(header.CbStringChars == 0L);
			if (pool != null && pool.Count > 0)
			{
				header.FpStringTable = Utils.FpCur(writer) - fpMin;
				long num = 0L;
				int num2 = 0;
				foreach (NormStr normStr in pool)
				{
					num += (long)(normStr.Value.Length * 2);
					writer.Write(num);
					num2++;
				}
				header.CbStringTable = (long)(pool.Count * 8);
				header.FpStringChars = Utils.FpCur(writer) - fpMin;
				foreach (NormStr normStr2 in pool)
				{
					foreach (char c in normStr2.Value)
					{
						writer.Write((short)c);
					}
				}
				header.CbStringChars = Utils.FpCur(writer) - header.FpStringChars - fpMin;
			}
			ModelHeader.WriteHeaderAndTailCore(writer, fpMin, ref header);
		}

		// Token: 0x060017A8 RID: 6056 RVA: 0x00087D5C File Offset: 0x00085F5C
		public static void WriteHeaderAndTailCore(BinaryWriter writer, long fpMin, ref ModelHeader header)
		{
			Contracts.CheckValue<BinaryWriter>(writer, "writer");
			Contracts.CheckParam(fpMin >= 0L, "fpMin");
			header.FpTail = Utils.FpCur(writer) - fpMin;
			writer.Write(5569827171192816972UL);
			header.FpLim = Utils.FpCur(writer) - fpMin;
			Exception ex;
			bool flag = ModelHeader.TryValidate(ref header, header.FpLim, out ex);
			Contracts.Check(flag);
			Utils.Seek(writer, fpMin);
			byte[] array = new byte[256];
			ModelHeader.MarshalToBytes(ref header, array);
			writer.Write(array);
			Utils.Seek(writer, header.FpLim + fpMin);
		}

		// Token: 0x060017A9 RID: 6057 RVA: 0x00087DF4 File Offset: 0x00085FF4
		public static void EndModelCore(BinaryWriter writer, long fpMin, ref ModelHeader header)
		{
			Contracts.Check(header.FpModel == 256L);
			Contracts.Check(header.CbModel == 0L);
			long num = Utils.FpCur(writer);
			Contracts.Check(num - fpMin >= header.FpModel);
			header.CbModel = num - header.FpModel - fpMin;
		}

		// Token: 0x060017AA RID: 6058 RVA: 0x00087E50 File Offset: 0x00086050
		public static void SetVersionInfo(ref ModelHeader header, VersionInfo ver)
		{
			header.ModelSignature = ver.ModelSignature;
			header.ModelVerWritten = ver.VerWrittenCur;
			header.ModelVerReadable = ver.VerReadableCur;
			ModelHeader.SetLoaderSig(ref header, ver.LoaderSignature);
			ModelHeader.SetLoaderSigAlt(ref header, ver.LoaderSignatureAlt);
		}

		// Token: 0x060017AB RID: 6059 RVA: 0x00087EA0 File Offset: 0x000860A0
		public static void SetLoaderSig(ref ModelHeader header, string sig)
		{
			header.LoaderSignature0 = 0UL;
			header.LoaderSignature1 = 0UL;
			header.LoaderSignature2 = 0UL;
			if (sig == null)
			{
				return;
			}
			Contracts.Check(sig.Length <= 24);
			for (int i = 0; i < sig.Length; i++)
			{
				char c = sig[i];
				Contracts.Check(c <= 'ÿ');
				if (i < 8)
				{
					header.LoaderSignature0 |= (ulong)c << i * 8;
				}
				else if (i < 16)
				{
					header.LoaderSignature1 |= (ulong)c << (i - 8) * 8;
				}
				else if (i < 24)
				{
					header.LoaderSignature2 |= (ulong)c << (i - 16) * 8;
				}
			}
		}

		// Token: 0x060017AC RID: 6060 RVA: 0x00087F60 File Offset: 0x00086160
		public static void SetLoaderSigAlt(ref ModelHeader header, string sig)
		{
			header.LoaderSignatureAlt0 = 0UL;
			header.LoaderSignatureAlt1 = 0UL;
			header.LoaderSignatureAlt2 = 0UL;
			if (sig == null)
			{
				return;
			}
			Contracts.Check(sig.Length <= 24);
			for (int i = 0; i < sig.Length; i++)
			{
				char c = sig[i];
				Contracts.Check(c <= 'ÿ');
				if (i < 8)
				{
					header.LoaderSignatureAlt0 |= (ulong)c << i * 8;
				}
				else if (i < 16)
				{
					header.LoaderSignatureAlt1 |= (ulong)c << (i - 8) * 8;
				}
				else if (i < 24)
				{
					header.LoaderSignatureAlt2 |= (ulong)c << (i - 16) * 8;
				}
			}
		}

		// Token: 0x060017AD RID: 6061 RVA: 0x00088020 File Offset: 0x00086220
		public unsafe static void MarshalToBytes(ref ModelHeader header, byte[] bytes)
		{
			Contracts.Check(Utils.Size<byte>(bytes) >= 256);
			fixed (ModelHeader* ptr = &header)
			{
				Marshal.Copy((IntPtr)((void*)ptr), bytes, 0, 256);
			}
		}

		// Token: 0x060017AE RID: 6062 RVA: 0x0008805C File Offset: 0x0008625C
		public static void BeginRead(out long fpMin, out ModelHeader header, out string[] strings, BinaryReader reader)
		{
			fpMin = Utils.FpCur(reader);
			byte[] array = reader.ReadBytes(256);
			Contracts.CheckDecode(array.Length == 256);
			ModelHeader.MarshalFromBytes(out header, array);
			Exception ex;
			if (!ModelHeader.TryValidate(ref header, reader, fpMin, out strings, out ex))
			{
				throw ex;
			}
			Utils.Seek(reader, header.FpModel + fpMin);
		}

		// Token: 0x060017AF RID: 6063 RVA: 0x000880B2 File Offset: 0x000862B2
		public static void EndRead(long fpMin, ref ModelHeader header, BinaryReader reader)
		{
			Contracts.CheckDecode(header.FpModel + header.CbModel == Utils.FpCur(reader) - fpMin);
			Utils.Seek(reader, header.FpLim + fpMin);
		}

		// Token: 0x060017B0 RID: 6064 RVA: 0x000880E0 File Offset: 0x000862E0
		public static void CheckVersionInfo(ref ModelHeader header, VersionInfo ver)
		{
			Contracts.CheckDecode(header.ModelSignature == ver.ModelSignature, "Unknown file type");
			Contracts.CheckDecode(header.ModelVerReadable <= header.ModelVerWritten, "Corrupt file header");
			Contracts.CheckDecode(header.ModelVerReadable <= ver.VerWrittenCur, "Model is too new");
			Contracts.CheckDecode(header.ModelVerWritten >= ver.VerWeCanReadBack, "Model is too old");
		}

		// Token: 0x060017B1 RID: 6065 RVA: 0x0008815C File Offset: 0x0008635C
		public unsafe static void MarshalFromBytes(out ModelHeader header, byte[] bytes)
		{
			Contracts.Check(Utils.Size<byte>(bytes) >= 256);
			fixed (ModelHeader* ptr = &header)
			{
				Marshal.Copy(bytes, 0, (IntPtr)((void*)ptr), 256);
			}
		}

		// Token: 0x060017B2 RID: 6066 RVA: 0x00088198 File Offset: 0x00086398
		public static bool TryValidate(ref ModelHeader header, long size, out Exception ex)
		{
			Contracts.Check(size >= 0L);
			bool flag;
			try
			{
				Contracts.CheckDecode(header.Signature == 5495874027660528717UL, "Wrong file type");
				Contracts.CheckDecode(header.VerReadable <= header.VerWritten, "Corrupt file header");
				Contracts.CheckDecode(header.VerReadable <= 65537U, "File is too new");
				Contracts.CheckDecode(header.VerWritten >= 65537U, "File is too old");
				Contracts.CheckDecode(header.FpModel == 256L);
				Contracts.CheckDecode(header.FpModel + header.CbModel >= header.FpModel);
				if (header.FpStringTable == 0L)
				{
					Contracts.CheckDecode(header.CbStringTable == 0L);
					Contracts.CheckDecode(header.FpStringChars == 0L);
					Contracts.CheckDecode(header.CbStringChars == 0L);
					Contracts.CheckDecode(header.FpTail == header.FpModel + header.CbModel);
				}
				else
				{
					Contracts.CheckDecode(header.FpStringTable == header.FpModel + header.CbModel);
					Contracts.CheckDecode(header.CbStringTable % 8L == 0L);
					Contracts.CheckDecode(header.CbStringTable / 8L < 2147483647L);
					Contracts.CheckDecode(header.FpStringTable + header.CbStringTable > header.FpStringTable);
					Contracts.CheckDecode(header.FpStringChars == header.FpStringTable + header.CbStringTable);
					Contracts.CheckDecode(header.CbStringChars % 2L == 0L);
					Contracts.CheckDecode(header.FpStringChars + header.CbStringChars >= header.FpStringChars);
					Contracts.CheckDecode(header.FpTail == header.FpStringChars + header.CbStringChars);
				}
				Contracts.CheckDecode(header.FpLim == header.FpTail + 8L);
				Contracts.CheckDecode(size == 0L || size >= header.FpLim);
				ex = null;
				flag = true;
			}
			catch (Exception ex2)
			{
				ex = ex2;
				flag = false;
			}
			return flag;
		}

		// Token: 0x060017B3 RID: 6067 RVA: 0x000883BC File Offset: 0x000865BC
		public static bool TryValidate(ref ModelHeader header, BinaryReader reader, long fpMin, out string[] strings, out Exception ex)
		{
			Contracts.CheckValue<BinaryReader>(reader, "reader");
			Contracts.Check(fpMin >= 0L);
			if (!ModelHeader.TryValidate(ref header, reader.BaseStream.Length - fpMin, out ex))
			{
				strings = null;
				return false;
			}
			if (header.FpStringTable == 0L)
			{
				strings = null;
				ex = null;
				return true;
			}
			bool flag;
			try
			{
				long num = Utils.FpCur(reader);
				Utils.Seek(reader, header.FpStringTable + fpMin);
				long num2 = header.CbStringTable / 8L;
				long[] array = Utils.ReadLongArray(reader, (int)num2);
				long num3;
				StringBuilder stringBuilder;
				checked
				{
					Contracts.CheckDecode(array[(int)((IntPtr)(unchecked(num2 - 1L)))] == header.CbStringChars);
					strings = new string[num2];
					num3 = 0L;
					stringBuilder = new StringBuilder();
				}
				for (int i = 0; i < array.Length; i++)
				{
					Contracts.CheckDecode(header.FpStringChars + num3 == Utils.FpCur(reader) - fpMin);
					long num4 = num3;
					num3 = array[i];
					Contracts.CheckDecode((num4 <= num3) & (num3 <= header.CbStringChars));
					Contracts.CheckDecode(num3 % 2L == 0L);
					long num5 = (num3 - num4) / 2L;
					Contracts.CheckDecode(num5 < 2147483647L);
					stringBuilder.Clear();
					for (long num6 = 0L; num6 < num5; num6 += 1L)
					{
						stringBuilder.Append((char)reader.ReadUInt16());
					}
					strings[i] = stringBuilder.ToString();
				}
				Contracts.CheckDecode(num3 == header.CbStringChars);
				Contracts.CheckDecode(header.FpStringChars + header.CbStringChars == Utils.FpCur(reader) - fpMin);
				Contracts.CheckDecode(header.FpTail == Utils.FpCur(reader) - fpMin);
				ulong num7 = reader.ReadUInt64();
				Contracts.CheckDecode(num7 == 5569827171192816972UL, "Corrupt model file tail");
				Utils.Seek(reader, num);
				ex = null;
				flag = true;
			}
			catch (Exception ex2)
			{
				strings = null;
				ex = ex2;
				flag = false;
			}
			return flag;
		}

		// Token: 0x060017B4 RID: 6068 RVA: 0x000885A8 File Offset: 0x000867A8
		public static string GetLoaderSig(ref ModelHeader header)
		{
			char[] array = new char[24];
			for (int i = 0; i < array.Length; i++)
			{
				char c;
				if (i < 8)
				{
					c = (char)((header.LoaderSignature0 >> i * 8) & 255UL);
				}
				else if (i < 16)
				{
					c = (char)((header.LoaderSignature1 >> (i - 8) * 8) & 255UL);
				}
				else
				{
					c = (char)((header.LoaderSignature2 >> (i - 16) * 8) & 255UL);
				}
				array[i] = c;
			}
			int num = 24;
			while (num > 0 && array[num - 1] == '\0')
			{
				num--;
			}
			return new string(array, 0, num);
		}

		// Token: 0x060017B5 RID: 6069 RVA: 0x00088640 File Offset: 0x00086840
		public static string GetLoaderSigAlt(ref ModelHeader header)
		{
			char[] array = new char[24];
			for (int i = 0; i < array.Length; i++)
			{
				char c;
				if (i < 8)
				{
					c = (char)((header.LoaderSignatureAlt0 >> i * 8) & 255UL);
				}
				else if (i < 16)
				{
					c = (char)((header.LoaderSignatureAlt1 >> (i - 8) * 8) & 255UL);
				}
				else
				{
					c = (char)((header.LoaderSignatureAlt2 >> (i - 16) * 8) & 255UL);
				}
				array[i] = c;
			}
			int num = 24;
			while (num > 0 && array[num - 1] == '\0')
			{
				num--;
			}
			return new string(array, 0, num);
		}

		// Token: 0x04000E3E RID: 3646
		public const ulong SignatureValue = 5495874027660528717UL;

		// Token: 0x04000E3F RID: 3647
		public const ulong TailSignatureValue = 5569827171192816972UL;

		// Token: 0x04000E40 RID: 3648
		private const uint VerWrittenCur = 65537U;

		// Token: 0x04000E41 RID: 3649
		private const uint VerReadableCur = 65537U;

		// Token: 0x04000E42 RID: 3650
		private const uint VerWeCanReadBack = 65537U;

		// Token: 0x04000E43 RID: 3651
		public const int Size = 256;

		// Token: 0x04000E44 RID: 3652
		[FieldOffset(0)]
		public ulong Signature;

		// Token: 0x04000E45 RID: 3653
		[FieldOffset(8)]
		public uint VerWritten;

		// Token: 0x04000E46 RID: 3654
		[FieldOffset(12)]
		public uint VerReadable;

		// Token: 0x04000E47 RID: 3655
		[FieldOffset(16)]
		public long FpModel;

		// Token: 0x04000E48 RID: 3656
		[FieldOffset(24)]
		public long CbModel;

		// Token: 0x04000E49 RID: 3657
		[FieldOffset(32)]
		public long FpStringTable;

		// Token: 0x04000E4A RID: 3658
		[FieldOffset(40)]
		public long CbStringTable;

		// Token: 0x04000E4B RID: 3659
		[FieldOffset(48)]
		public long FpStringChars;

		// Token: 0x04000E4C RID: 3660
		[FieldOffset(56)]
		public long CbStringChars;

		// Token: 0x04000E4D RID: 3661
		[FieldOffset(64)]
		public ulong ModelSignature;

		// Token: 0x04000E4E RID: 3662
		[FieldOffset(72)]
		public uint ModelVerWritten;

		// Token: 0x04000E4F RID: 3663
		[FieldOffset(76)]
		public uint ModelVerReadable;

		// Token: 0x04000E50 RID: 3664
		[FieldOffset(80)]
		public ulong LoaderSignature0;

		// Token: 0x04000E51 RID: 3665
		[FieldOffset(88)]
		public ulong LoaderSignature1;

		// Token: 0x04000E52 RID: 3666
		[FieldOffset(96)]
		public ulong LoaderSignature2;

		// Token: 0x04000E53 RID: 3667
		[FieldOffset(104)]
		public ulong LoaderSignatureAlt0;

		// Token: 0x04000E54 RID: 3668
		[FieldOffset(112)]
		public ulong LoaderSignatureAlt1;

		// Token: 0x04000E55 RID: 3669
		[FieldOffset(120)]
		public ulong LoaderSignatureAlt2;

		// Token: 0x04000E56 RID: 3670
		[FieldOffset(128)]
		public long FpTail;

		// Token: 0x04000E57 RID: 3671
		[FieldOffset(136)]
		public long FpLim;
	}
}
