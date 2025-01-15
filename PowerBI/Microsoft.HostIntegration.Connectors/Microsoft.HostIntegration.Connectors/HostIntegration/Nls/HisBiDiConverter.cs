using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using Microsoft.HostIntegration.StrictResources.Nls;
using Microsoft.Win32;

namespace Microsoft.HostIntegration.Nls
{
	// Token: 0x02000615 RID: 1557
	[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.UnmanagedCode)]
	internal class HisBiDiConverter : HisConverter
	{
		// Token: 0x0600349C RID: 13468 RVA: 0x000AF33C File Offset: 0x000AD53C
		[SecurityCritical]
		[RegistryPermission(SecurityAction.Assert, Read = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Host Integration Server")]
		internal HisBiDiConverter(HisEncoding enc)
			: base(enc)
		{
			this.physicalCodePoints = false;
			if (this.encoding.usePhysicalOrderForCodePage420 == null)
			{
				RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Host Integration Server");
				if (registryKey != null)
				{
					int num = (int)registryKey.GetValue("CP420PhysicalStorage", 0);
					this.physicalCodePoints = num == 1;
				}
			}
			else
			{
				this.physicalCodePoints = this.encoding.usePhysicalOrderForCodePage420.Value;
			}
			this.CreateMemoryDC();
			this.GetSCMap();
			this.freeResources();
		}

		// Token: 0x0600349D RID: 13469 RVA: 0x000AF4CC File Offset: 0x000AD6CC
		internal override int GetEbcdicByteCount(byte[] src, int index, int count)
		{
			HisConverter.StatusFlags statusFlags = HisConverter.StatusFlags.NoStatus;
			HisConverter.PositionFlags positionFlags = HisConverter.PositionFlags.AtSingleByte;
			HisConverter.StreamState streamState = HisConverter.StreamState.NotSet;
			bool flag = true;
			return this.GetEbcdicByteCount(src, index, count, flag, ref statusFlags, ref streamState, ref positionFlags);
		}

		// Token: 0x0600349E RID: 13470 RVA: 0x000AF4F4 File Offset: 0x000AD6F4
		internal override int GetUnicodeByteCount(byte[] src, int index, int count)
		{
			HisConverter.StatusFlags statusFlags = HisConverter.StatusFlags.NoStatus;
			HisConverter.PositionFlags positionFlags = HisConverter.PositionFlags.AtSingleByte;
			HisConverter.StreamState streamState = HisConverter.StreamState.NotSet;
			bool flag = true;
			return this.GetUnicodeByteCount(src, index, count, flag, ref statusFlags, ref streamState, ref positionFlags);
		}

		// Token: 0x0600349F RID: 13471 RVA: 0x000AF51C File Offset: 0x000AD71C
		internal override int EbcdicToUnicode(byte[] src, byte[] dest, int srcIndex, int srcLength, int destIndex, int totalDestLength, bool flush, ref int requiredLen, ref HisConverter.StreamState state, ref bool completed)
		{
			HisConverter.StreamState streamState = HisConverter.StreamState.NotSet;
			char[] array = this.EbcdicToUnicode(src, srcIndex, srcLength, flush, ref requiredLen, ref streamState, ref state, ref completed);
			Array bytes = Encoding.GetEncoding(this.encoding.WindowsCodePage).GetBytes(array, 0, array.Length);
			int num = ((array.Length < totalDestLength) ? array.Length : totalDestLength);
			Array.Copy(bytes, 0, dest, destIndex, num);
			return num;
		}

		// Token: 0x060034A0 RID: 13472 RVA: 0x000AF574 File Offset: 0x000AD774
		internal override int UnicodeToEbcdic(byte[] src, byte[] dest, int srcIndex, int srcLength, int destIndex, int totalDestLength, bool flush, ref int requiredLen, ref HisConverter.StreamState state, ref bool completed)
		{
			char[] chars = Encoding.GetEncoding(this.encoding.WindowsCodePage).GetChars(src, srcIndex, srcLength);
			HisConverter.StreamState streamState = HisConverter.StreamState.NotSet;
			completed = false;
			byte[] array = this.UnicodeToEbcdic(chars, 0, chars.Length, flush, ref requiredLen, ref streamState, ref state, ref completed);
			int num = ((array.Length < totalDestLength) ? array.Length : totalDestLength);
			Array.Copy(array, 0, dest, destIndex, num);
			return num;
		}

		// Token: 0x060034A1 RID: 13473 RVA: 0x000AF5D4 File Offset: 0x000AD7D4
		internal override int GetUnicodeCharCount(byte[] src, int index, int count)
		{
			HisConverter.StatusFlags statusFlags = HisConverter.StatusFlags.NoStatus;
			HisConverter.PositionFlags positionFlags = HisConverter.PositionFlags.AtSingleByte;
			HisConverter.StreamState streamState = HisConverter.StreamState.NotSet;
			bool flag = true;
			return this.GetUnicodeByteCount(src, index, count, flag, ref statusFlags, ref streamState, ref positionFlags);
		}

		// Token: 0x060034A2 RID: 13474 RVA: 0x000AF5FC File Offset: 0x000AD7FC
		internal override int GetUnicodeByteCount(byte[] src, int index, int count, bool flush, ref HisConverter.StatusFlags status, ref HisConverter.StreamState state, ref HisConverter.PositionFlags flags)
		{
			char[] array = null;
			char[] array2 = null;
			char c = '\0';
			byte[] array3;
			if (this.encoding.destinationCodePage == HisEncoding.HostCodePages.EBCDICArabic_420)
			{
				if (this.encoding.DontUsePresentationFormsB)
				{
					array = this.ConvTable4201;
					array2 = this.ConvTable4202;
				}
				else
				{
					array = this.ConvTableU4201;
					array2 = this.ConvTableU4202;
				}
				array3 = this.RtlChar420;
				c = 'E';
			}
			else
			{
				array3 = this.RtlChar424;
			}
			byte[] array4 = new byte[count];
			char[] array5 = new char[count * 2];
			Array.Copy(src, 0, array4, 0, count);
			int num = this.Direction(array4, 0, count, array3, true);
			if (this.encoding.usePhysicalOrderForCodePage420 != null)
			{
				this.physicalCodePoints = this.encoding.usePhysicalOrderForCodePage420.Value;
			}
			if (!this.physicalCodePoints)
			{
				if (num == 10)
				{
					this.ReOrderRTL(array4, 0, count, array3);
				}
				else
				{
					this.ReOrderLTR(array4, 0, count, array3, true);
				}
			}
			int num2 = 0;
			int num3 = 0;
			if (this.encoding.destinationCodePage == HisEncoding.HostCodePages.EBCDICHebrew_424)
			{
				try
				{
					num2 = this.Decode(20424, array4, ref array5);
					goto IL_01F0;
				}
				catch (ArgumentException)
				{
					throw new ArgumentException(SR.InvalidCodePage(this.encoding.destinationCodePage));
				}
			}
			for (int i = 0; i < count; i++)
			{
				if ((char)array4[i] != c)
				{
					if (num == 10 && array4[i] == 77)
					{
						array5[num2++] = array[93];
					}
					else if (num == 10 && array4[i] == 93)
					{
						array5[num2++] = array[77];
					}
					else
					{
						array5[num2++] = array[(int)array4[i]];
						if (array2[(int)array4[i]] != '\0')
						{
							array5[num2++] = array2[(int)array4[i]];
						}
					}
				}
				else if (i + 1 < count && array4[i + 1] != 64)
				{
					array5[num2++] = array[64];
				}
				else
				{
					num3++;
				}
			}
			for (int j = 0; j < num3; j++)
			{
				array5[num2++] = array[64];
			}
			IL_01F0:
			int num4 = num2;
			array5 = null;
			return num4;
		}

		// Token: 0x060034A3 RID: 13475 RVA: 0x000AF814 File Offset: 0x000ADA14
		internal override int GetEbcdicByteCount(byte[] src, int index, int count, bool flush, ref HisConverter.StatusFlags status, ref HisConverter.StreamState state, ref HisConverter.PositionFlags flags)
		{
			char[] chars = Encoding.GetEncoding(this.encoding.WindowsCodePage).GetChars(src, index, count);
			int num = 0;
			this.CreateMemoryDC();
			this.AllocateClass(count);
			if (this.WDirClass(chars, 0, count, ref this.clss, true, this.encoding.destinationCodePage) == 11)
			{
				NativeInterface.SetTextAlign(this.hDC, NativeInterface.GetTextAlign(this.hDC) ^ 256U);
			}
			string text = new string(chars);
			char[] characterPlacement = this.GetCharacterPlacement(text, out num);
			byte[] array = new byte[num * 4];
			int num2 = this.GlypIndexToEbcdic(characterPlacement, 0, num, ref array, 0);
			this.freeResources();
			return num2;
		}

		// Token: 0x060034A4 RID: 13476 RVA: 0x000AF8B2 File Offset: 0x000ADAB2
		internal override int GetUnicodeCharCount(byte[] src, int index, int count, bool flush, ref HisConverter.StatusFlags status, ref HisConverter.StreamState state, ref HisConverter.PositionFlags flags)
		{
			return this.GetUnicodeByteCount(src, index, count, flush, ref status, ref state, ref flags);
		}

		// Token: 0x060034A5 RID: 13477 RVA: 0x000AF8C8 File Offset: 0x000ADAC8
		internal override char[] EbcdicToUnicode(byte[] srcBytes, int index, int count)
		{
			HisConverter.StreamState streamState = HisConverter.StreamState.NotSet;
			HisConverter.StreamState streamState2 = HisConverter.StreamState.NotSet;
			bool flag = true;
			bool flag2 = false;
			int num = 0;
			return this.EbcdicToUnicode(srcBytes, index, count, flag, ref num, ref streamState2, ref streamState, ref flag2);
		}

		// Token: 0x060034A6 RID: 13478 RVA: 0x000AF8F4 File Offset: 0x000ADAF4
		internal override byte[] UnicodeToEbcdic(char[] src, int index, int count)
		{
			HisConverter.StreamState streamState = HisConverter.StreamState.NotSet;
			HisConverter.StreamState streamState2 = HisConverter.StreamState.NotSet;
			bool flag = true;
			bool flag2 = false;
			int num = 0;
			return this.UnicodeToEbcdic(src, index, count, flag, ref num, ref streamState2, ref streamState, ref flag2);
		}

		// Token: 0x060034A7 RID: 13479 RVA: 0x000AF920 File Offset: 0x000ADB20
		internal override char[] EbcdicToUnicode(byte[] srcBytes, int index, int count, bool flush, ref int requiredLen, ref HisConverter.StreamState stateCount, ref HisConverter.StreamState state, ref bool completed)
		{
			char[] array = null;
			char[] array2 = null;
			char c = '\0';
			requiredLen = 0;
			byte[] array3;
			if (this.encoding.destinationCodePage == HisEncoding.HostCodePages.EBCDICArabic_420)
			{
				if (this.encoding.DontUsePresentationFormsB)
				{
					array = this.ConvTable4201;
					array2 = this.ConvTable4202;
				}
				else
				{
					array = this.ConvTableU4201;
					array2 = this.ConvTableU4202;
				}
				array3 = this.RtlChar420;
				c = 'E';
			}
			else
			{
				array3 = this.RtlChar424;
			}
			byte[] array4 = new byte[count];
			char[] array5 = new char[count * 2];
			Array.Copy(srcBytes, 0, array4, 0, count);
			int num = this.Direction(array4, 0, count, array3, true);
			if (this.encoding.usePhysicalOrderForCodePage420 != null)
			{
				this.physicalCodePoints = this.encoding.usePhysicalOrderForCodePage420.Value;
			}
			if (!this.physicalCodePoints)
			{
				if (num == 10)
				{
					this.ReOrderRTL(array4, 0, count, array3);
				}
				else
				{
					this.ReOrderLTR(array4, 0, count, array3, true);
				}
			}
			int num2 = 0;
			int num3 = 0;
			if (this.encoding.destinationCodePage == HisEncoding.HostCodePages.EBCDICHebrew_424)
			{
				try
				{
					num2 = this.Decode(20424, array4, ref array5);
					goto IL_01F9;
				}
				catch (ArgumentException)
				{
					throw new Exception(SR.InvalidCodePage(this.encoding.destinationCodePage));
				}
			}
			for (int i = 0; i < count; i++)
			{
				if ((char)array4[i] != c)
				{
					if (num == 10 && array4[i] == 77)
					{
						array5[num2++] = array[93];
					}
					else if (num == 10 && array4[i] == 93)
					{
						array5[num2++] = array[77];
					}
					else
					{
						array5[num2++] = array[(int)array4[i]];
						if (array2[(int)array4[i]] != '\0')
						{
							array5[num2++] = array2[(int)array4[i]];
						}
					}
				}
				else if (i + 1 < count && array4[i + 1] != 64)
				{
					array5[num2++] = array[64];
				}
				else
				{
					num3++;
				}
			}
			for (int j = 0; j < num3; j++)
			{
				array5[num2++] = array[64];
			}
			IL_01F9:
			requiredLen = num2;
			char[] array6 = new char[requiredLen];
			Array.Copy(array5, 0, array6, 0, count);
			array5 = null;
			return array6;
		}

		// Token: 0x060034A8 RID: 13480 RVA: 0x000AFB58 File Offset: 0x000ADD58
		internal override byte[] UnicodeToEbcdic(char[] src, int index, int count, bool flush, ref int requiredLen, ref HisConverter.StreamState stateCount, ref HisConverter.StreamState state, ref bool completed)
		{
			requiredLen = 0;
			int num = 0;
			this.CreateMemoryDC();
			this.AllocateClass(count);
			int num2 = this.WDirClass(src, 0, count, ref this.clss, true, this.encoding.destinationCodePage);
			if (num2 == 11)
			{
				NativeInterface.SetTextAlign(this.hDC, NativeInterface.GetTextAlign(this.hDC) ^ 256U);
			}
			string text = new string(src, index, count);
			char[] characterPlacement = this.GetCharacterPlacement(text, out num);
			byte[] array = new byte[num * 4];
			requiredLen = this.GlypIndexToEbcdic(characterPlacement, 0, num, ref array, 0);
			if (this.encoding.usePhysicalOrderForCodePage420 != null)
			{
				this.physicalCodePoints = this.encoding.usePhysicalOrderForCodePage420.Value;
			}
			if (this.physicalCodePoints)
			{
				if (num2 == 10)
				{
					if (this.encoding.destinationCodePage == HisEncoding.HostCodePages.EBCDICHebrew_424)
					{
						this.ReOrderRTL(array, 0, requiredLen, this.RtlChar424);
					}
					else
					{
						this.ReOrderRTL(array, 0, requiredLen, this.RtlChar420);
					}
				}
				else if (this.encoding.destinationCodePage == HisEncoding.HostCodePages.EBCDICHebrew_424)
				{
					this.ReOrderLTR(array, 0, requiredLen, this.RtlChar424, true);
				}
				else
				{
					this.ReOrderLTR(array, 0, requiredLen, this.RtlChar420, true);
				}
			}
			byte[] array2 = new byte[requiredLen];
			Array.Copy(array, 0, array2, 0, requiredLen);
			this.freeResources();
			return array2;
		}

		// Token: 0x060034A9 RID: 13481 RVA: 0x000AFCAC File Offset: 0x000ADEAC
		internal override int GetByteCount(char[] src, int index, int count)
		{
			int num = 0;
			this.CreateMemoryDC();
			this.AllocateClass(count);
			if (this.WDirClass(src, 0, count, ref this.clss, true, this.encoding.destinationCodePage) == 11)
			{
				NativeInterface.SetTextAlign(this.hDC, NativeInterface.GetTextAlign(this.hDC) ^ 256U);
			}
			string text = new string(src, index, count);
			char[] characterPlacement = this.GetCharacterPlacement(text, out num);
			byte[] array = new byte[num * 4];
			int num2 = this.GlypIndexToEbcdic(characterPlacement, 0, num, ref array, 0);
			this.freeResources();
			return num2;
		}

		// Token: 0x060034AA RID: 13482 RVA: 0x000AFD32 File Offset: 0x000ADF32
		internal override int GetCharCount(byte[] srcBytes, int index, int count)
		{
			return this.GetUnicodeCharCount(srcBytes, index, count);
		}

		// Token: 0x060034AB RID: 13483 RVA: 0x000AFD40 File Offset: 0x000ADF40
		protected void CreateMemoryDC()
		{
			this.hDC = NativeInterface.GetDC(IntPtr.Zero);
			NativeInterface.LOGFONT logfont = new NativeInterface.LOGFONT();
			if (this.encoding.destinationCodePage == HisEncoding.HostCodePages.EBCDICHebrew_424)
			{
				logfont.lfCharSet = 177;
			}
			else
			{
				logfont.lfCharSet = 178;
			}
			logfont.lfHeight = 24;
			logfont.lfFaceName = "Tahoma";
			this.newFont = NativeInterface.CreateFontIndirect(logfont);
			this.oldFont = NativeInterface.SelectObject(this.hDC, this.newFont);
			this.oldTA = NativeInterface.SetTextAlign(this.hDC, NativeInterface.GetTextAlign(this.hDC) | 256U);
		}

		// Token: 0x060034AC RID: 13484 RVA: 0x000AFDE8 File Offset: 0x000ADFE8
		private unsafe void GetSCMap()
		{
			GCHandle gchandle = GCHandle.Alloc(this.memoryGlyphs, GCHandleType.Pinned);
			this.CP420Table[0] = '\u0001';
			string text = new string(this.CP420Table);
			NativeInterface.SCRIPT_CACHE script_CACHE = default(NativeInterface.SCRIPT_CACHE);
			IntPtr intPtr = new IntPtr((void*)(&script_CACHE));
			IntPtr intPtr2 = gchandle.AddrOfPinnedObject();
			NativeInterface.ScriptGetCMap(this.hDC, intPtr, text, 256, 1048576U, intPtr2);
			this.CP420Table[0] = '\0';
			NativeInterface.ScriptFreeCache(intPtr);
			gchandle.Free();
		}

		// Token: 0x060034AD RID: 13485 RVA: 0x000AFE64 File Offset: 0x000AE064
		private void freeResources()
		{
			NativeInterface.SelectObject(this.hDC, this.oldFont);
			NativeInterface.DeleteObject(this.newFont);
			NativeInterface.SetTextAlign(this.hDC, this.oldTA);
			NativeInterface.ReleaseDC(IntPtr.Zero, this.hDC);
			this.hDC = IntPtr.Zero;
		}

		// Token: 0x060034AE RID: 13486 RVA: 0x000AFEBD File Offset: 0x000AE0BD
		private void AllocateClass(int len)
		{
			this.clss = new byte[len];
		}

		// Token: 0x060034AF RID: 13487 RVA: 0x000AFECC File Offset: 0x000AE0CC
		private char[] GetCharacterPlacement(string text, out int maxGlyphs)
		{
			int length = text.Length;
			char[] array = new char[1024];
			maxGlyphs = 0;
			GCHandle gchandle = GCHandle.Alloc(this.clss, GCHandleType.Pinned);
			GCHandle gchandle2 = GCHandle.Alloc(array, GCHandleType.Pinned);
			try
			{
				NativeInterface.GpcResults gpcResults = default(NativeInterface.GpcResults);
				gpcResults.StructSize = Marshal.SizeOf(typeof(NativeInterface.GpcResults));
				gpcResults.Class = gchandle.AddrOfPinnedObject();
				gpcResults.Glyphs = gchandle2.AddrOfPinnedObject();
				gpcResults.GlyphCount = 1024;
				gpcResults.MaxFit = 0;
				NativeInterface.GcpFlags gcpFlags = NativeInterface.GetFontLanguageInfo(this.hDC) | NativeInterface.GcpFlags.NumericsLatin | NativeInterface.GcpFlags.NumericsLocal;
				if (NativeInterface.GetCharacterPlacementW(this.hDC, text, length, 1048576, ref gpcResults, gcpFlags) == 0)
				{
					Marshal.GetLastWin32Error();
					return null;
				}
				maxGlyphs = gpcResults.GlyphCount;
			}
			finally
			{
				gchandle.Free();
				gchandle2.Free();
			}
			return array;
		}

		// Token: 0x060034B0 RID: 13488 RVA: 0x000AFFBC File Offset: 0x000AE1BC
		protected void ReOrderLTR(byte[] VisualStr, int srcIndex, int srcCount, byte[] rtlChar, bool isData)
		{
			byte[] array = new byte[srcCount];
			byte b = 0;
			int num = srcCount - 1;
			bool flag = false;
			int i;
			for (i = srcIndex; i < srcCount; i++)
			{
				array[i] = rtlChar[(int)VisualStr[i]];
				if (array[i] == 5)
				{
					array[i] = 3;
				}
				if (!isData && VisualStr[i] == 125)
				{
					array[i] = 0;
				}
				if (array[i] != 2)
				{
					if (array[i] != 3)
					{
						if (array[i] == 4)
						{
							array[i] = 3;
							b = 1;
						}
						else
						{
							b = array[i];
						}
					}
					else if (b != 1 || !flag)
					{
						b = (array[i] = 0);
					}
				}
				else
				{
					flag = true;
					array[i] = b;
				}
			}
			i = srcIndex;
			while (i < srcCount)
			{
				b = array[i];
				if (array[i] == 1)
				{
					int j;
					num = (j = i);
					while (num < srcCount && array[num] == 1)
					{
						num++;
					}
					num--;
					i = num;
					while (j < num)
					{
						byte b2 = VisualStr[j];
						VisualStr[j] = VisualStr[num];
						VisualStr[num] = b2;
						num--;
						j++;
					}
					i++;
				}
				else
				{
					i++;
				}
			}
		}

		// Token: 0x060034B1 RID: 13489 RVA: 0x000B00B0 File Offset: 0x000AE2B0
		protected void ReOrderRTL(byte[] VisualStr, int srcIndex, int srcCount, byte[] rtlChar)
		{
			ushort[] array = new ushort[srcCount];
			ushort num = 1;
			int i = 0;
			int num2 = srcCount - 1;
			byte b = 0;
			while (i < num2)
			{
				this.Swap_bytes(ref VisualStr[i], ref VisualStr[num2], ref b);
				num2--;
				i++;
			}
			bool flag = false;
			int j;
			for (j = srcIndex; j < srcCount; j++)
			{
				array[j] = (ushort)rtlChar[(int)VisualStr[j]];
				if (array[j] == 5)
				{
					array[j] = 4;
				}
				if (array[j] != 2)
				{
					if (array[j] != 3)
					{
						if (array[j] == 4)
						{
							array[j] = 3;
							num = 1;
						}
						else
						{
							num = array[j];
						}
					}
					else if (num != 1 || !flag)
					{
						num = (array[j] = 0);
					}
				}
				else
				{
					flag = true;
					array[j] = num;
				}
			}
			j = srcIndex;
			num = 1;
			num2 = (i = -1);
			while (j < srcCount)
			{
				if (num != array[j])
				{
					if (i != num2)
					{
						if (num2 == -1)
						{
							num2 = j - 1;
						}
						while (i < num2)
						{
							this.Swap_bytes(ref VisualStr[i], ref VisualStr[num2], ref b);
							num2--;
							i++;
						}
					}
					if (array[j] != 1)
					{
						i = j;
						num2 = -1;
					}
					else
					{
						num2 = (i = -1);
					}
					num = array[j];
				}
				j++;
			}
			if (num2 == -1 && i != -1)
			{
				num2 = j - 1;
			}
			while (i < num2)
			{
				this.Swap_bytes(ref VisualStr[i], ref VisualStr[num2], ref b);
				num2--;
				i++;
			}
		}

		// Token: 0x060034B2 RID: 13490 RVA: 0x000B01FC File Offset: 0x000AE3FC
		protected int AdjustStr(char[] Str, int srcIndex, int TrgLen, int SrcLen)
		{
			char[] array = new char[SrcLen];
			Buffer.BlockCopy(Str, srcIndex, array, 0, SrcLen);
			int num2;
			int num = (num2 = 0);
			int num3 = SrcLen;
			while (num < SrcLen && SrcLen - num2 > TrgLen && array[num++] == ' ')
			{
				num2++;
			}
			num = num3;
			while (num > num2 && num3 - num2 > TrgLen && array[--num] == ' ')
			{
				num3--;
			}
			Buffer.BlockCopy(array, num2, Str, srcIndex, num3 - num2);
			return num3 - num2;
		}

		// Token: 0x060034B3 RID: 13491 RVA: 0x000B0270 File Offset: 0x000AE470
		protected int WDirection(char[] wideCharStr, int srcIndex, int srcCount, ref ushort[] stringAttributes, int inStrAttrPos, bool isData)
		{
			if (!isData)
			{
				return 11;
			}
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < srcCount; i++)
			{
				switch (stringAttributes[i + inStrAttrPos])
				{
				case 0:
				case 3:
					num2++;
					break;
				case 1:
				case 4:
					num++;
					break;
				}
			}
			if (num > num2)
			{
				return 10;
			}
			return 11;
		}

		// Token: 0x060034B4 RID: 13492 RVA: 0x000B02D0 File Offset: 0x000AE4D0
		protected int WDirClass(char[] wideCharStr, int srcIndex, int srcCount, ref byte[] LPClass, bool isData, HisEncoding.HostCodePages CodePage)
		{
			ushort[] array = new ushort[srcCount];
			string text = new string(this.NtStr);
			for (int i = 0; i < srcCount; i++)
			{
				if (text.IndexOf(wideCharStr[i + srcIndex]) != -1)
				{
					array[i] = 2;
				}
				else if ((wideCharStr[i + srcIndex] > '\u0600' && wideCharStr[i + srcIndex] < '\u0670') || (wideCharStr[i + srcIndex] > '\u0590' && wideCharStr[i + srcIndex] < '\u05ff'))
				{
					array[i] = 1;
				}
				else if (wideCharStr[i + srcIndex] > '@' && wideCharStr[i + srcIndex] < '{')
				{
					array[i] = 0;
				}
				else if (wideCharStr[i + srcIndex] >= '0' && wideCharStr[i + srcIndex] < ':')
				{
					array[i] = 3;
				}
			}
			int num = this.WDirection(wideCharStr, srcIndex, srcCount, ref array, 0, isData);
			Memory.SetMemory(LPClass, 0L, 0, (long)srcCount);
			if (!isData)
			{
				for (int j = srcIndex; j < srcCount; j++)
				{
					if (wideCharStr[j + srcIndex] == '\'')
					{
						LPClass[j++] = 1;
						int num2 = j;
						while (j < srcCount && wideCharStr[srcIndex + j] != '\'')
						{
							j++;
						}
						ushort num3;
						if (10 == this.WDirection(wideCharStr, srcIndex + num2, j - num2, ref array, num2, true))
						{
							num3 = 1;
						}
						else
						{
							num3 = 0;
						}
						bool flag = num3 == array[num2];
						for (int i = num2; i < j; i++)
						{
							if (array[i] != 2)
							{
								if (array[i] == 3)
								{
									array[i] = 3;
									if (num3 == 1)
									{
										LPClass[i] = 4;
									}
									else
									{
										LPClass[i] = 5;
									}
								}
								else if (num3 != 1 || !flag)
								{
									num3 = (array[i] = 0);
								}
								else
								{
									num3 = array[i];
								}
							}
							else
							{
								flag = true;
								array[i] = num3;
								if (num3 == 1)
								{
									if (this.encoding.destinationCodePage == HisEncoding.HostCodePages.EBCDICHebrew_424)
									{
										LPClass[i] = 2;
									}
									else
									{
										LPClass[i] = 2;
									}
								}
								else
								{
									LPClass[i] = 1;
								}
							}
						}
					}
					if (j < srcCount && (',' == wideCharStr[srcIndex + j] || '\'' == wideCharStr[srcIndex + j] || ' ' == wideCharStr[srcIndex + j]))
					{
						LPClass[j] = 1;
					}
				}
			}
			return num;
		}

		// Token: 0x060034B5 RID: 13493 RVA: 0x000B04C4 File Offset: 0x000AE6C4
		protected int Direction(byte[] CharStr, int srcIndex, int srcCount, byte[] rtlChar, bool isData)
		{
			if (!isData)
			{
				return 11;
			}
			ushort[] array = new ushort[srcCount];
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < srcCount; i++)
			{
				array[i] = (ushort)rtlChar[(int)CharStr[i + srcIndex]];
				switch (array[i])
				{
				case 0:
				case 3:
					num2++;
					break;
				case 1:
				case 4:
					num++;
					break;
				}
			}
			if (num > num2)
			{
				return 10;
			}
			return 11;
		}

		// Token: 0x060034B6 RID: 13494 RVA: 0x00006F04 File Offset: 0x00005104
		protected virtual int GlypIndexToEbcdic(char[] glyphsFromText, int srcIndex, int totalGlyphs, ref byte[] ebcdicBytes, int destIndex)
		{
			return 0;
		}

		// Token: 0x060034B7 RID: 13495 RVA: 0x000B0534 File Offset: 0x000AE734
		private int Encode(int codepage, char[] inputbuffer, ref byte[] outputbuffer)
		{
			int num;
			int num2;
			bool flag;
			Encoding.GetEncoding(codepage).GetEncoder().Convert(inputbuffer, 0, inputbuffer.Length, outputbuffer, 0, outputbuffer.Length, true, out num, out num2, out flag);
			return num2;
		}

		// Token: 0x060034B8 RID: 13496 RVA: 0x000B0568 File Offset: 0x000AE768
		private int Decode(int codepage, byte[] inputbuffer, ref char[] outputbuffer)
		{
			int num;
			int num2;
			bool flag;
			Encoding.GetEncoding(codepage).GetDecoder().Convert(inputbuffer, 0, inputbuffer.Length, outputbuffer, 0, outputbuffer.Length, true, out num, out num2, out flag);
			return num;
		}

		// Token: 0x060034B9 RID: 13497 RVA: 0x000B0599 File Offset: 0x000AE799
		private void Swap_bytes(ref byte x, ref byte y, ref byte Temp)
		{
			Temp = x;
			x = y;
			y = Temp;
		}

		// Token: 0x060034BA RID: 13498 RVA: 0x000B05A7 File Offset: 0x000AE7A7
		private void Swap_chars(ref char x, ref char y, ref char Temp)
		{
			Temp = x;
			x = y;
			y = Temp;
		}

		// Token: 0x060034BB RID: 13499 RVA: 0x000B05A7 File Offset: 0x000AE7A7
		private void Swap_ushort(ref ushort x, ref ushort y, ref ushort Temp)
		{
			Temp = x;
			x = y;
			y = Temp;
		}

		// Token: 0x04001DA0 RID: 7584
		private IntPtr hDC = IntPtr.Zero;

		// Token: 0x04001DA1 RID: 7585
		protected char[] memoryGlyphs = new char[256];

		// Token: 0x04001DA2 RID: 7586
		protected byte[] clss;

		// Token: 0x04001DA3 RID: 7587
		protected char[] NtStr = new char[]
		{
			' ', '!', '"', '#', '$', '%', '&', '\'', '(', ')',
			'*', '+', ',', '-', '.', '/', ':', ';', '<', '=',
			'>', '?', '@', '[', '\\', ']', '^', '_', '`', '{',
			'|', '}', '~', '\0'
		};

		// Token: 0x04001DA4 RID: 7588
		protected char[] CP420Table = new char[]
		{
			'\0', '\u0001', '\u0002', '\u0003', '\u009c', '\t', '\u0086', '\u007f', '\u0097', '\u008d',
			'\u008e', '\v', '\f', '\r', '\u000e', '\u000f', '\u0010', '\u0011', '\u0012', '\u0013',
			'\u009d', '\u0085', '\b', '\u0087', '\u0018', '\u0019', '\u0092', '\u008f', '\u001c', '\u001d',
			'\u001e', '\u001f', '\u0080', '\u0081', '\u0082', '\u0083', '\u0084', '\n', '\u0017', '\u001b',
			'\u0088', '\u0089', '\u008a', '\u008b', '\u008c', '\u0005', '\u0006', '\a', '\u0090', '\u0091',
			'\u0016', '\u0093', '\u0094', '\u0095', '\u0096', '\u0004', '\u0098', '\u0099', '\u009a', '\u009b',
			'\u0014', '\u0015', '\u009e', '\u001a', ' ', '\u00a0', '\u0651', 'ﹽ', 'ـ', '\uf8fc',
			'ء', 'ﺁ', 'ﺂ', 'ﺃ', '¢', '.', '<', '(', '+', '|',
			'&', 'ﺄ', 'ﺅ', '\u001a', '\u001a', 'ﺌ', 'ﺍ', 'ﺎ', 'ﺏ', 'ﺒ',
			'!', '$', '*', ')', ';', '¬', '-', '/', 'ﺓ', 'ﺕ',
			'ﺘ', 'ﺙ', 'ﺜ', 'ﺝ', 'ﺠ', 'ﺡ', '¦', ',', '%', '_',
			'>', '?', 'ﺤ', 'ﺥ', 'ﺨ', 'ﺩ', 'ﺫ', 'ﺭ', 'ﺯ', 'ﺱ',
			'ﺴ', '،', ':', '#', '@', '\'', '=', '"', 'ﺵ', 'a',
			'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'ﺸ', 'ﺹ',
			'ﺼ', 'ﺽ', 'ﻀ', 'ﻁ', 'ﻅ', 'j', 'k', 'l', 'm', 'n',
			'o', 'p', 'q', 'r', 'ﻉ', 'ﻊ', 'ﻋ', 'ﻌ', 'ﻍ', 'ﻎ',
			'ﻏ', '÷', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
			'ﻐ', 'ﻑ', 'ﻓ', 'ﻕ', 'ﻗ', 'ﻙ', 'ﻜ', 'ﻝ', 'ﻵ', 'ﻶ',
			'ﻷ', 'ﻸ', '\u001a', '\u001a', 'ﻻ', 'ﻼ', 'ﻟ', 'ﻡ', 'ﻤ', 'ﻥ',
			'ﻨ', 'ﻩ', '؛', 'A', 'B', 'C', 'D', 'E', 'F', 'G',
			'H', 'I', '\u00ad', 'ﻫ', '\u001a', 'ﻬ', '\u001a', 'ﻭ', '؟', 'J',
			'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'ﻯ', 'ﻰ',
			'ﻱ', 'ﻲ', 'ﻴ', '٠', '×', '\u2007', 'S', 'T', 'U', 'V',
			'W', 'X', 'Y', 'Z', '١', '٢', '\u001a', '٣', '٤', '٥',
			'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
			'€', '٦', '٧', '٨', '٩', '\u009f'
		};

		// Token: 0x04001DA5 RID: 7589
		protected char[] ConvTableU4201 = new char[]
		{
			'\0', '\u0001', '\u0002', '\u0003', '\u009c', '\t', '\u0086', '\u007f', '\u0097', '\u008d',
			'\u008e', '\v', '\f', '\r', '\u000e', '\u000f', '\u0010', '\u0011', '\u0012', '\u0013',
			'\u009d', '\u0085', '\b', '\u0087', '\u0018', '\u0019', '\u0092', '\u008f', '\u001c', '\u001d',
			'\u001e', '\u001f', '\u0080', '\u0081', '\u0082', '\u0083', '\u0084', '\n', '\u0017', '\u001b',
			'\u0088', '\u0089', '\u008a', '\u008b', '\u008c', '\u0005', '\u0006', '\a', '\u0090', '\u0091',
			'\u0016', '\u0093', '\u0094', '\u0095', '\u0096', '\u0004', '\u0098', '\u0099', '\u009a', '\u009b',
			'\u0014', '\u0015', '\u009e', '\u001a', ' ', '\u00a0', '\u0651', 'ﹽ', 'ـ', '\uf8fc',
			'ء', 'ﺁ', 'ﺂ', 'ﺃ', '¢', '.', '<', '(', '+', '|',
			'&', 'ﺄ', 'ﺅ', '\u001a', '\u001a', 'ﺌ', 'ﺍ', 'ﺎ', 'ﺏ', 'ﺒ',
			'!', '$', '*', ')', ';', '¬', '-', '/', 'ﺓ', 'ﺕ',
			'ﺘ', 'ﺙ', 'ﺜ', 'ﺝ', 'ﺠ', 'ﺡ', '¦', ',', '%', '_',
			'>', '?', 'ﺤ', 'ﺥ', 'ﺨ', 'ﺩ', 'ﺫ', 'ﺭ', 'ﺯ', 'ﺱ',
			'ﺴ', '،', ':', '#', '@', '\'', '=', '"', 'ﺵ', 'a',
			'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'ﺸ', 'ﺹ',
			'ﺼ', 'ﺽ', 'ﻀ', 'ﻁ', 'ﻅ', 'j', 'k', 'l', 'm', 'n',
			'o', 'p', 'q', 'r', 'ﻉ', 'ﻊ', 'ﻋ', 'ﻌ', 'ﻍ', 'ﻎ',
			'ﻏ', '÷', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
			'ﻐ', 'ﻑ', 'ﻓ', 'ﻕ', 'ﻗ', 'ﻙ', 'ﻜ', 'ﻝ', 'ﻵ', 'ﻶ',
			'ﻷ', 'ﻸ', '\u001a', '\u001a', 'ﻻ', 'ﻼ', 'ﻟ', 'ﻡ', 'ﻤ', 'ﻥ',
			'ﻨ', 'ﻩ', '؛', 'A', 'B', 'C', 'D', 'E', 'F', 'G',
			'H', 'I', '\u00ad', 'ﻫ', '\u001a', 'ﻬ', '\u001a', 'ﻭ', '؟', 'J',
			'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'ﻯ', 'ﻰ',
			'ﻱ', 'ﻲ', 'ﻴ', '٠', '×', '\u2007', 'S', 'T', 'U', 'V',
			'W', 'X', 'Y', 'Z', '١', '٢', '\u001a', '٣', '٤', '٥',
			'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
			'€', '٦', '٧', '٨', '٩', '\u009f'
		};

		// Token: 0x04001DA6 RID: 7590
		protected byte[] RtlChar420 = new byte[]
		{
			2, 2, 2, 2, 2, 2, 2, 2, 2, 2,
			2, 2, 2, 2, 2, 2, 2, 2, 2, 2,
			2, 2, 2, 2, 2, 2, 2, 2, 2, 2,
			2, 2, 2, 2, 2, 2, 2, 2, 2, 2,
			2, 2, 2, 2, 2, 2, 2, 2, 2, 2,
			2, 2, 2, 2, 2, 2, 2, 2, 2, 2,
			2, 2, 2, 2, 2, 2, 1, 1, 1, 1,
			1, 1, 1, 1, 2, 2, 2, 2, 2, 2,
			0, 1, 1, 2, 2, 1, 1, 1, 1, 1,
			2, 2, 2, 2, 2, 2, 2, 2, 1, 1,
			1, 1, 1, 1, 1, 1, 2, 2, 2, 2,
			2, 2, 1, 1, 1, 1, 1, 1, 1, 1,
			1, 1, 2, 2, 2, 2, 2, 2, 1, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 1, 1,
			1, 1, 1, 1, 1, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 1, 1, 1, 1, 1, 1,
			1, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
			1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
			1, 1, 1, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 2, 1, 1, 1, 1, 1, 1, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 1, 1,
			1, 1, 1, 4, 2, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 4, 4, 4, 4, 4, 4,
			3, 3, 3, 3, 3, 3, 3, 3, 3, 3,
			2, 4, 4, 4, 4, 2
		};

		// Token: 0x04001DA7 RID: 7591
		protected ushort[] RtlCharU420 = new ushort[]
		{
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 2, 2, 2, 2, 2, 2, 2, 2,
			2, 2, 2, 2, 2, 2, 2, 2, 3, 3,
			3, 3, 3, 3, 3, 3, 3, 3, 2, 2,
			2, 2, 2, 2, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 1, 2, 1, 1, 2, 1, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 1, 0, 1, 2, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 1, 2, 1, 1, 1, 2, 1, 1, 1,
			1, 1, 2, 0, 1, 1, 1, 1, 4, 0,
			1, 1, 0, 1, 1, 1, 1, 1, 0, 1,
			1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
			1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
			4, 4, 4, 4, 0, 1, 1, 4, 4, 4,
			4, 1, 1, 1, 1, 1, 1, 1, 1, 1,
			1, 1, 0, 1, 1, 0, 1, 1, 1, 1,
			1, 1, 1, 0, 1, 1, 0, 0, 1, 1,
			1, 1, 1, 1, 1, 4
		};

		// Token: 0x04001DA8 RID: 7592
		protected char[] ConvTableU4202 = new char[256];

		// Token: 0x04001DA9 RID: 7593
		protected char[] ConvTable4201 = new char[]
		{
			'\0', '\u0001', '\u0002', '\u0003', '\u009c', '\t', '\u0086', '\u007f', '\u0097', '\u008d',
			'\u008e', '\v', '\f', '\r', '\u000e', '\u000f', '\u0010', '\u0011', '\u0012', '\u0013',
			'\u009d', '\u0085', '\b', '\u0087', '\u0018', '\u0019', '\u0092', '\u008f', '\u001c', '\u001d',
			'\u001e', '\u001f', '\u0080', '\u0081', '\u0082', '\u0083', '\u0084', '\n', '\u0017', '\u001b',
			'\u0088', '\u0089', '\u008a', '\u008b', '\u008c', '\u0005', '\u0006', '\a', '\u0090', '\u0091',
			'\u0016', '\u0093', '\u0094', '\u0095', '\u0096', '\u0004', '\u0098', '\u0099', '\u009a', '\u009b',
			'\u0014', '\u0015', '\u009e', '\u001a', ' ', '\u00a0', '\u0651', 'ـ', 'ـ', 'F',
			'ء', 'آ', 'آ', 'أ', '¢', '.', '<', '(', '+', '|',
			'&', 'أ', 'ؤ', '\u001a', '\u001a', 'ئ', 'ا', 'ا', 'ب', 'ب',
			'!', '$', '*', ')', ';', '¬', '-', '/', 'ة', 'ت',
			'ت', 'ث', 'ث', 'ج', 'ج', 'ح', '¦', ',', '%', '_',
			'>', '?', 'ح', 'خ', 'خ', 'د', 'ذ', 'ر', 'ز', 'س',
			'س', '،', ':', '#', '@', '\'', '=', '"', 'ش', 'a',
			'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'ش', 'ص',
			'ص', 'ض', 'ض', 'ط', 'ظ', 'j', 'k', 'l', 'm', 'n',
			'o', 'p', 'q', 'r', 'ع', 'ع', 'ع', 'ع', 'غ', 'غ',
			'غ', '÷', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
			'غ', 'ف', 'ف', 'ق', 'ق', 'ك', 'ك', 'ل', 'ل', 'ل',
			'ل', 'ل', '\u001a', '\u001a', 'ل', 'ل', 'ل', 'م', 'م', 'ن',
			'ن', 'ه', '؛', 'A', 'B', 'C', 'D', 'E', 'F', 'G',
			'H', 'I', '\u00ad', 'ه', '\u001a', 'ه', '\u001a', 'و', '؟', 'J',
			'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'ى', 'ى',
			'ي', 'ي', 'ي', '0', '×', '\u2007', 'S', 'T', 'U', 'V',
			'W', 'X', 'Y', 'Z', '1', '2', '\u001a', '3', '4', '5',
			'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
			'€', '6', '7', '8', '9', '\u009f'
		};

		// Token: 0x04001DAA RID: 7594
		protected char[] ConvTable4202 = new char[]
		{
			'\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
			'\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
			'\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
			'\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
			'\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
			'\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
			'\0', '\0', '\0', '\0', '\0', '\0', '\0', '\u0651', '\0', '\0',
			'\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
			'\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
			'\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
			'\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
			'\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
			'\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
			'\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
			'\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
			'\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
			'\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
			'\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', 'آ', 'آ',
			'أ', 'أ', '\0', '\0', 'ا', 'ا', '\0', '\0', '\0', '\0',
			'\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
			'\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
			'\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
			'\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
			'\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
			'\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
			'\0', '\0', '\0', '\0', '\0', '\0'
		};

		// Token: 0x04001DAB RID: 7595
		protected byte[] RtlChar424 = new byte[]
		{
			2, 2, 2, 2, 2, 2, 2, 2, 2, 2,
			2, 2, 2, 2, 2, 2, 2, 2, 2, 2,
			2, 2, 2, 2, 2, 2, 2, 2, 2, 2,
			2, 2, 2, 2, 2, 2, 2, 2, 2, 2,
			2, 2, 2, 2, 2, 2, 2, 2, 2, 2,
			2, 2, 2, 2, 2, 2, 2, 2, 2, 2,
			2, 2, 2, 2, 2, 1, 1, 1, 1, 1,
			1, 1, 1, 1, 2, 2, 2, 2, 2, 2,
			0, 1, 1, 1, 1, 1, 1, 1, 1, 1,
			2, 2, 2, 2, 2, 2, 2, 2, 1, 1,
			1, 1, 1, 1, 1, 1, 2, 2, 2, 2,
			2, 2, 2, 1, 2, 2, 2, 2, 2, 2,
			2, 2, 2, 2, 2, 2, 2, 2, 2, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 2, 2,
			2, 2, 2, 2, 2, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 2, 2, 2, 2, 2, 2,
			2, 2, 0, 0, 0, 0, 0, 0, 0, 0,
			2, 2, 2, 2, 2, 2, 2, 2, 2, 2,
			2, 2, 2, 2, 2, 2, 2, 2, 2, 2,
			2, 2, 2, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 2, 2, 2, 2, 2, 2, 2, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 2, 2,
			2, 2, 2, 2, 2, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 2, 2, 2, 2, 2, 2,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			2, 2, 2, 2, 2, 2
		};

		// Token: 0x04001DAC RID: 7596
		private IntPtr oldFont;

		// Token: 0x04001DAD RID: 7597
		private IntPtr newFont;

		// Token: 0x04001DAE RID: 7598
		private uint oldTA;

		// Token: 0x04001DAF RID: 7599
		private bool physicalCodePoints;

		// Token: 0x04001DB0 RID: 7600
		private const byte GCPCLASS_LATIN = 1;

		// Token: 0x04001DB1 RID: 7601
		private const byte GCPCLASS_HEBREW = 2;

		// Token: 0x04001DB2 RID: 7602
		private const byte GCPCLASS_ARABIC = 2;

		// Token: 0x04001DB3 RID: 7603
		private const byte GCPCLASS_NEUTRAL = 3;

		// Token: 0x04001DB4 RID: 7604
		private const byte GCPCLASS_LOCALNUMBER = 4;

		// Token: 0x04001DB5 RID: 7605
		private const byte GCPCLASS_LATINNUMBER = 5;

		// Token: 0x04001DB6 RID: 7606
		private const byte GCPCLASS_LATINNUMERICTERMINATOR = 6;

		// Token: 0x04001DB7 RID: 7607
		private const byte GCPCLASS_LATINNUMERICSEPARATOR = 7;

		// Token: 0x04001DB8 RID: 7608
		private const byte GCPCLASS_NUMERICSEPARATOR = 8;

		// Token: 0x04001DB9 RID: 7609
		private const byte GCPCLASS_PREBOUNDLTR = 128;

		// Token: 0x04001DBA RID: 7610
		private const byte GCPCLASS_PREBOUNDRTL = 64;

		// Token: 0x04001DBB RID: 7611
		private const byte GCPCLASS_POSTBOUNDLTR = 32;

		// Token: 0x04001DBC RID: 7612
		private const byte GCPCLASS_POSTBOUNDRTL = 16;

		// Token: 0x04001DBD RID: 7613
		private const int TA_RTLREADING = 256;

		// Token: 0x04001DBE RID: 7614
		private const int FLI_GLYPHS = 262144;

		// Token: 0x04001DBF RID: 7615
		private const int GCP_CLASSIN = 524288;

		// Token: 0x04001DC0 RID: 7616
		private const int GCP_MAXEXTENT = 1048576;

		// Token: 0x04001DC1 RID: 7617
		private const int GCP_JUSTIFYIN = 2097152;

		// Token: 0x04001DC2 RID: 7618
		private const int GCP_DISPLAYZWG = 4194304;

		// Token: 0x04001DC3 RID: 7619
		private const int GCP_SYMSWAPOFF = 8388608;

		// Token: 0x04001DC4 RID: 7620
		private const int GCP_NUMERICOVERRIDE = 16777216;

		// Token: 0x04001DC5 RID: 7621
		private const int GCP_NEUTRALOVERRIDE = 33554432;

		// Token: 0x04001DC6 RID: 7622
		private const int GCP_NUMERICSLATIN = 67108864;

		// Token: 0x04001DC7 RID: 7623
		private const int GCP_NUMERICSLOCAL = 134217728;

		// Token: 0x04001DC8 RID: 7624
		private const string STORAGE_ORIENTATION_REG_ENTRY = "CP420PhysicalStorage";

		// Token: 0x04001DC9 RID: 7625
		private const string USE_NET_ENCODER = "UseNetBidiEncoder";

		// Token: 0x04001DCA RID: 7626
		private const int CP_420 = 1;

		// Token: 0x04001DCB RID: 7627
		private const int CP_U420 = 2;

		// Token: 0x04001DCC RID: 7628
		private const int CP_424 = 3;

		// Token: 0x04001DCD RID: 7629
		private const int MAX_CHARS = 1024;

		// Token: 0x04001DCE RID: 7630
		private const byte LTRC = 0;

		// Token: 0x04001DCF RID: 7631
		private const byte RTLC = 1;

		// Token: 0x04001DD0 RID: 7632
		private const byte NUTC = 2;

		// Token: 0x04001DD1 RID: 7633
		private const byte LTRN = 3;

		// Token: 0x04001DD2 RID: 7634
		private const byte RTLN = 4;

		// Token: 0x04001DD3 RID: 7635
		private const byte NUTN = 5;

		// Token: 0x04001DD4 RID: 7636
		private const byte RTL = 10;

		// Token: 0x04001DD5 RID: 7637
		private const byte LTR = 11;

		// Token: 0x02000616 RID: 1558
		protected enum BiDiCharSet
		{
			// Token: 0x04001DD7 RID: 7639
			HEBREW_CHARSET = 177,
			// Token: 0x04001DD8 RID: 7640
			ARABIC_CHARSET
		}
	}
}
