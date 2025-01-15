using System;
using System.Runtime.InteropServices;

namespace Microsoft.HostIntegration.Nls
{
	// Token: 0x0200061F RID: 1567
	internal class HisCustomCodePageConverter : HisConverter
	{
		// Token: 0x060034E0 RID: 13536 RVA: 0x000B0E50 File Offset: 0x000AF050
		internal HisCustomCodePageConverter(HisEncoding enc)
			: base(enc)
		{
			this.initializeFromNls(enc.hisCustomEncodingMaps.InheritedCodePage);
			foreach (HisUnicodeToEbcdicMapping hisUnicodeToEbcdicMapping in enc.hisCustomEncodingMaps.UnicodeToEbcDicMapping)
			{
				char unicodeChar = hisUnicodeToEbcdicMapping.UnicodeChar;
				byte ebcdicByte = hisUnicodeToEbcdicMapping.EbcdicByte;
				this.ebcdicTable[(int)unicodeChar] = ebcdicByte;
				if (hisUnicodeToEbcdicMapping.Reversible)
				{
					this.charTable[(int)ebcdicByte] = unicodeChar;
				}
			}
			foreach (HisEbcdicToUnicodeMapping hisEbcdicToUnicodeMapping in enc.hisCustomEncodingMaps.EbcdicToUnicodeMapping)
			{
				char unicodeChar2 = hisEbcdicToUnicodeMapping.UnicodeChar;
				byte ebcdicByte2 = hisEbcdicToUnicodeMapping.EbcdicByte;
				this.charTable[(int)ebcdicByte2] = unicodeChar2;
				if (hisEbcdicToUnicodeMapping.Reversible)
				{
					this.ebcdicTable[(int)unicodeChar2] = ebcdicByte2;
				}
			}
		}

		// Token: 0x060034E1 RID: 13537 RVA: 0x000B0F6C File Offset: 0x000AF16C
		internal override byte[] UnicodeToEbcdic(char[] src, int index, int count, bool flush, ref int requiredLen, ref HisConverter.StreamState stateCount, ref HisConverter.StreamState state, ref bool completed)
		{
			byte[] array = new byte[count];
			for (int i = 0; i < count; i++)
			{
				char c = src[i + index];
				array[i] = this.ebcdicTable[(int)c];
			}
			requiredLen = count;
			completed = true;
			return array;
		}

		// Token: 0x060034E2 RID: 13538 RVA: 0x000B0FA8 File Offset: 0x000AF1A8
		internal override char[] EbcdicToUnicode(byte[] srcBytes, int index, int count, bool flush, ref int requiredLen, ref HisConverter.StreamState stateCount, ref HisConverter.StreamState state, ref bool completed)
		{
			char[] array = new char[count];
			for (int i = 0; i < count; i++)
			{
				byte b = srcBytes[i + index];
				array[i] = this.charTable[(int)b];
			}
			requiredLen = count;
			completed = true;
			return array;
		}

		// Token: 0x060034E3 RID: 13539 RVA: 0x000B0FE4 File Offset: 0x000AF1E4
		internal override char[] EbcdicToUnicode(byte[] srcBytes, int index, int count)
		{
			HisConverter.StreamState streamState = HisConverter.StreamState.NotSet;
			HisConverter.StreamState streamState2 = HisConverter.StreamState.NotSet;
			bool flag = true;
			bool flag2 = false;
			int num = 0;
			return this.EbcdicToUnicode(srcBytes, index, count, flag, ref num, ref streamState2, ref streamState, ref flag2);
		}

		// Token: 0x060034E4 RID: 13540 RVA: 0x000B1010 File Offset: 0x000AF210
		internal override byte[] UnicodeToEbcdic(char[] src, int index, int count)
		{
			HisConverter.StreamState streamState = HisConverter.StreamState.NotSet;
			HisConverter.StreamState streamState2 = HisConverter.StreamState.NotSet;
			bool flag = true;
			bool flag2 = false;
			int num = 0;
			return this.UnicodeToEbcdic(src, index, count, flag, ref num, ref streamState2, ref streamState, ref flag2);
		}

		// Token: 0x060034E5 RID: 13541 RVA: 0x000B103C File Offset: 0x000AF23C
		private void initializeFromNls(int codepage)
		{
			char[] array = new char[65536];
			byte[] array2 = new byte[256];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (char)i;
			}
			for (int j = 0; j < array2.Length; j++)
			{
				array2[j] = (byte)j;
			}
			IntPtr zero = IntPtr.Zero;
			GCHandle gchandle = GCHandle.Alloc(this.charTable, GCHandleType.Pinned);
			IntPtr intPtr = gchandle.AddrOfPinnedObject();
			GCHandle gchandle2 = GCHandle.Alloc(array, GCHandleType.Pinned);
			IntPtr intPtr2 = gchandle2.AddrOfPinnedObject();
			bool flag;
			NativeInterface.WideCharToMultiByte(codepage, 0U, intPtr2, array.Length, this.ebcdicTable, this.ebcdicTable.Length, zero, out flag);
			NativeInterface.MultiByteToWideChar(codepage, 1, array2, array2.Length, intPtr, this.charTable.Length);
			gchandle.Free();
			gchandle2.Free();
		}

		// Token: 0x04001DF9 RID: 7673
		private const int NumberOfEbcdicCharacters = 256;

		// Token: 0x04001DFA RID: 7674
		private const int NumberOfUnicodeCharacters = 65536;

		// Token: 0x04001DFB RID: 7675
		private char[] charTable = new char[256];

		// Token: 0x04001DFC RID: 7676
		private byte[] ebcdicTable = new byte[65536];
	}
}
