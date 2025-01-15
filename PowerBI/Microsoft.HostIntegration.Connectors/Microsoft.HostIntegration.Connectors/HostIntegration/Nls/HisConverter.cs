using System;
using System.IO;
using System.Security;
using Microsoft.Win32;

namespace Microsoft.HostIntegration.Nls
{
	// Token: 0x0200061A RID: 1562
	internal abstract class HisConverter
	{
		// Token: 0x060034C6 RID: 13510 RVA: 0x000B0B96 File Offset: 0x000AED96
		protected HisConverter(HisEncoding enc)
		{
			this.encoding = enc;
		}

		// Token: 0x060034C7 RID: 13511 RVA: 0x000B0BA5 File Offset: 0x000AEDA5
		internal virtual int GetEbcdicByteCount(byte[] src, int index, int count)
		{
			return count;
		}

		// Token: 0x060034C8 RID: 13512 RVA: 0x000B0BA5 File Offset: 0x000AEDA5
		internal virtual int GetUnicodeByteCount(byte[] src, int index, int count)
		{
			return count;
		}

		// Token: 0x060034C9 RID: 13513 RVA: 0x000B0BA5 File Offset: 0x000AEDA5
		internal virtual int GetUnicodeCharCount(byte[] src, int index, int count)
		{
			return count;
		}

		// Token: 0x060034CA RID: 13514 RVA: 0x000B0BA5 File Offset: 0x000AEDA5
		internal virtual int GetUnicodeByteCount(byte[] src, int index, int count, bool flush, ref HisConverter.StatusFlags status, ref HisConverter.StreamState state, ref HisConverter.PositionFlags flags)
		{
			return count;
		}

		// Token: 0x060034CB RID: 13515 RVA: 0x000B0BA5 File Offset: 0x000AEDA5
		internal virtual int GetEbcdicByteCount(byte[] src, int index, int count, bool flush, ref HisConverter.StatusFlags status, ref HisConverter.StreamState state, ref HisConverter.PositionFlags flags)
		{
			return count;
		}

		// Token: 0x060034CC RID: 13516 RVA: 0x000B0BA5 File Offset: 0x000AEDA5
		internal virtual int GetUnicodeCharCount(byte[] src, int index, int count, bool flush, ref HisConverter.StatusFlags status, ref HisConverter.StreamState state, ref HisConverter.PositionFlags flags)
		{
			return count;
		}

		// Token: 0x060034CD RID: 13517 RVA: 0x00006F04 File Offset: 0x00005104
		internal virtual int EbcdicToUnicode(byte[] src, byte[] dest, int srcIndex, int srcLength, int destIndex, int totalDestLength, bool flush, ref int requiredLen, ref HisConverter.StreamState state, ref bool completed)
		{
			return 0;
		}

		// Token: 0x060034CE RID: 13518 RVA: 0x00006F04 File Offset: 0x00005104
		internal virtual int UnicodeToEbcdic(byte[] src, byte[] dest, int srcIndex, int srcLength, int destIndex, int totalDestLength, bool flush, ref int requiredLen, ref HisConverter.StreamState state, ref bool completed)
		{
			return 0;
		}

		// Token: 0x060034CF RID: 13519 RVA: 0x000B0BA8 File Offset: 0x000AEDA8
		internal virtual byte[] GetLeadBytes()
		{
			return new byte[0];
		}

		// Token: 0x060034D0 RID: 13520 RVA: 0x000B0BB0 File Offset: 0x000AEDB0
		internal virtual char[] EbcdicToUnicode(byte[] srcBytes, int index, int count)
		{
			int num = 0;
			bool flag = false;
			HisConverter.StreamState streamState = HisConverter.StreamState.NotSet;
			num = this.GetUnicodeByteCount(srcBytes, index, count);
			byte[] array = new byte[num];
			this.EbcdicToUnicode(srcBytes, array, index, count, 0, array.Length, true, ref num, ref streamState, ref flag);
			char[] chars = this.encoding.WindowsEncoding.GetChars(array);
			this.FilterOuput(srcBytes, ref chars);
			return chars;
		}

		// Token: 0x060034D1 RID: 13521 RVA: 0x000B0C0C File Offset: 0x000AEE0C
		internal virtual byte[] UnicodeToEbcdic(char[] src, int index, int count)
		{
			int num = 0;
			char[] array = this.FilterInput(src);
			byte[] bytes = this.encoding.WindowsEncoding.GetBytes(array, index, count);
			bool flag = false;
			HisConverter.StreamState streamState = HisConverter.StreamState.NotSet;
			num = this.GetByteCount(bytes, 0, bytes.Length);
			byte[] array2 = new byte[num];
			this.UnicodeToEbcdic(bytes, array2, 0, bytes.Length, 0, array2.Length, true, ref num, ref streamState, ref flag);
			return array2;
		}

		// Token: 0x060034D2 RID: 13522 RVA: 0x000B0C6C File Offset: 0x000AEE6C
		internal virtual char[] EbcdicToUnicode(byte[] srcBytes, int index, int count, bool flush, ref int requiredLen, ref HisConverter.StreamState stateCount, ref HisConverter.StreamState state, ref bool completed)
		{
			HisConverter.StatusFlags statusFlags = HisConverter.StatusFlags.NoStatus;
			HisConverter.PositionFlags positionFlags = HisConverter.PositionFlags.AtSingleByte;
			requiredLen = this.GetUnicodeByteCount(srcBytes, index, count, flush, ref statusFlags, ref stateCount, ref positionFlags);
			byte[] array = new byte[requiredLen];
			this.EbcdicToUnicode(srcBytes, array, index, count, 0, array.Length, flush, ref requiredLen, ref state, ref completed);
			char[] chars = this.encoding.WindowsEncoding.GetChars(array);
			this.FilterOuput(srcBytes, ref chars);
			return chars;
		}

		// Token: 0x060034D3 RID: 13523 RVA: 0x000B0CD0 File Offset: 0x000AEED0
		internal virtual byte[] UnicodeToEbcdic(char[] src, int index, int count, bool flush, ref int requiredLen, ref HisConverter.StreamState stateCount, ref HisConverter.StreamState state, ref bool completed)
		{
			char[] array = this.FilterInput(src);
			HisConverter.StatusFlags statusFlags = HisConverter.StatusFlags.NoStatus;
			HisConverter.PositionFlags positionFlags = HisConverter.PositionFlags.AtSingleByte;
			byte[] bytes = this.encoding.WindowsEncoding.GetBytes(array, index, count);
			requiredLen = this.GetEbcdicByteCount(bytes, 0, bytes.Length, flush, ref statusFlags, ref stateCount, ref positionFlags);
			byte[] array2 = new byte[requiredLen];
			this.UnicodeToEbcdic(bytes, array2, 0, bytes.Length, 0, array2.Length, flush, ref requiredLen, ref state, ref completed);
			return array2;
		}

		// Token: 0x060034D4 RID: 13524 RVA: 0x000B0D40 File Offset: 0x000AEF40
		internal virtual int GetByteCount(char[] src, int index, int count)
		{
			byte[] bytes = this.encoding.WindowsEncoding.GetBytes(src, index, count);
			return this.GetEbcdicByteCount(bytes, 0, bytes.Length);
		}

		// Token: 0x060034D5 RID: 13525 RVA: 0x000B0D6C File Offset: 0x000AEF6C
		internal virtual int GetByteCount(byte[] src, int index, int count)
		{
			return this.GetEbcdicByteCount(src, index, count);
		}

		// Token: 0x060034D6 RID: 13526 RVA: 0x000AFD32 File Offset: 0x000ADF32
		internal virtual int GetCharCount(byte[] srcBytes, int index, int count)
		{
			return this.GetUnicodeCharCount(srcBytes, index, count);
		}

		// Token: 0x060034D7 RID: 13527 RVA: 0x000B0D77 File Offset: 0x000AEF77
		internal virtual int GetDoubleByteCutOffIndex(byte[] src, int index, int count, ref int requiredCount, ref HisConverter.StreamState parseState)
		{
			requiredCount = src.Length;
			return src.Length;
		}

		// Token: 0x060034D8 RID: 13528 RVA: 0x00002B16 File Offset: 0x00000D16
		internal virtual int GetMinimumByteSizeRequired(ref HisConverter.StreamState parseState)
		{
			return 1;
		}

		// Token: 0x060034D9 RID: 13529 RVA: 0x000B0D84 File Offset: 0x000AEF84
		[SecurityCritical]
		private static string GetInstallPath()
		{
			RegistryKey registryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey("SOFTWARE\\Microsoft\\Host Integration Server");
			string text = null;
			if (registryKey != null)
			{
				text = (string)registryKey.GetValue("InstallPath");
			}
			if (string.IsNullOrEmpty(text))
			{
				registryKey = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\DRDA Service\\1.0");
				if (registryKey != null)
				{
					text = (string)registryKey.GetValue("InstallPath");
				}
			}
			if (string.IsNullOrEmpty(text))
			{
				return Directory.GetCurrentDirectory();
			}
			if (!text.EndsWith("\\"))
			{
				return text + "\\system";
			}
			return text + "system";
		}

		// Token: 0x060034DA RID: 13530 RVA: 0x00028FA6 File Offset: 0x000271A6
		protected virtual char[] FilterInput(char[] src)
		{
			return src;
		}

		// Token: 0x060034DB RID: 13531 RVA: 0x000036A9 File Offset: 0x000018A9
		protected virtual void FilterOuput(byte[] ebcdic, ref char[] src)
		{
		}

		// Token: 0x04001DEB RID: 7659
		protected HisEncoding encoding;

		// Token: 0x04001DEC RID: 7660
		protected static Lazy<string> installPath = new Lazy<string>(() => HisConverter.GetInstallPath());

		// Token: 0x0200061B RID: 1563
		internal enum StreamState
		{
			// Token: 0x04001DEE RID: 7662
			NotSet,
			// Token: 0x04001DEF RID: 7663
			SI,
			// Token: 0x04001DF0 RID: 7664
			SO
		}

		// Token: 0x0200061C RID: 1564
		internal enum PositionFlags
		{
			// Token: 0x04001DF2 RID: 7666
			AtSingleByte,
			// Token: 0x04001DF3 RID: 7667
			AtDoubleByte
		}

		// Token: 0x0200061D RID: 1565
		internal enum StatusFlags
		{
			// Token: 0x04001DF5 RID: 7669
			NoStatus,
			// Token: 0x04001DF6 RID: 7670
			ExitNull = 128,
			// Token: 0x04001DF7 RID: 7671
			ExitOver = 256
		}
	}
}
