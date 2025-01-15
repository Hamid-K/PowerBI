using System;
using System.Text;
using Microsoft.HostIntegration.Nls;

namespace Microsoft.HostIntegration.MqClient
{
	// Token: 0x02000BCD RID: 3021
	public class ConversionHelpers
	{
		// Token: 0x06005DB9 RID: 23993 RVA: 0x0017F2E4 File Offset: 0x0017D4E4
		public unsafe static void MoveIntToAddressBigEndian(byte* address, int value)
		{
			address += 3;
			for (int i = 0; i < 4; i++)
			{
				*address = (byte)(value & 255);
				address--;
				value >>= 8;
			}
		}

		// Token: 0x06005DBA RID: 23994 RVA: 0x0017F318 File Offset: 0x0017D518
		public static int ChangeEndiannessIfNeeded(bool dontChange, int littleEndianValue)
		{
			if (dontChange)
			{
				return littleEndianValue;
			}
			return ((littleEndianValue & 255) << 24) | ((littleEndianValue & 65280) << 8) | (int)((uint)(littleEndianValue & 16711680) >> 8) | (int)((uint)(littleEndianValue & -16777216) >> 24);
		}

		// Token: 0x06005DBB RID: 23995 RVA: 0x0017F358 File Offset: 0x0017D558
		public unsafe static int ExtractIntFromBuffer(byte[] buffer, int index, bool littleEndian)
		{
			fixed (byte* ptr = &buffer[index])
			{
				int* ptr2 = (int*)ptr;
				return ConversionHelpers.ExtractIntFromAddress(ref ptr2, littleEndian);
			}
		}

		// Token: 0x06005DBC RID: 23996 RVA: 0x0017F378 File Offset: 0x0017D578
		public unsafe static int ExtractIntFromAddress(ref int* address, bool littleEndian)
		{
			int num;
			if (littleEndian)
			{
				num = ConversionHelpers.ExtractIntFromAddressLittleEndian(address);
			}
			else
			{
				num = ConversionHelpers.ExtractIntFromAddressBigEndian(address);
			}
			address += 4;
			return num;
		}

		// Token: 0x06005DBD RID: 23997 RVA: 0x0017F3A4 File Offset: 0x0017D5A4
		private unsafe static int ExtractIntFromAddressBigEndian(byte* address)
		{
			int num = 0;
			byte* ptr = (byte*)(&num);
			address += 3;
			for (int i = 0; i < 4; i++)
			{
				*(ptr++) = *(address--);
			}
			return num;
		}

		// Token: 0x06005DBE RID: 23998 RVA: 0x0017F3D8 File Offset: 0x0017D5D8
		private unsafe static int ExtractIntFromAddressLittleEndian(byte* address)
		{
			return *(int*)address;
		}

		// Token: 0x06005DBF RID: 23999 RVA: 0x0017F3EC File Offset: 0x0017D5EC
		public unsafe static short ExtractShortFromAddress(ref short* address, bool littleEndian)
		{
			short num;
			if (littleEndian)
			{
				num = ConversionHelpers.ExtractShortFromAddressLittleEndian(address);
			}
			else
			{
				num = ConversionHelpers.ExtractShortFromAddressBigEndian(address);
			}
			address += 2;
			return num;
		}

		// Token: 0x06005DC0 RID: 24000 RVA: 0x0017F418 File Offset: 0x0017D618
		private unsafe static short ExtractShortFromAddressBigEndian(byte* address)
		{
			short num = 0;
			byte* ptr = (byte*)(&num);
			address++;
			for (int i = 0; i < 2; i++)
			{
				*(ptr++) = *(address--);
			}
			return num;
		}

		// Token: 0x06005DC1 RID: 24001 RVA: 0x0017F44C File Offset: 0x0017D64C
		private unsafe static short ExtractShortFromAddressLittleEndian(byte* address)
		{
			return *(short*)address;
		}

		// Token: 0x06005DC2 RID: 24002 RVA: 0x0017F460 File Offset: 0x0017D660
		public static void MoveStringToBufferAscii(byte[] buffer, int index, string value, int maxLength, bool blankPad)
		{
			string text = value.Trim();
			if (text.Length > maxLength)
			{
				text = text.Substring(0, maxLength);
			}
			else if (blankPad)
			{
				text = text.PadRight(maxLength);
			}
			ConversionHelpers.encoding1252.GetBytes(text.ToCharArray(), 0, text.Length, buffer, index);
			if (!blankPad && text.Length < maxLength)
			{
				index += text.Length;
				for (int i = text.Length; i < maxLength; i++)
				{
					buffer[index++] = 0;
				}
			}
		}

		// Token: 0x06005DC3 RID: 24003 RVA: 0x0017F4DF File Offset: 0x0017D6DF
		public static void MoveStringToBufferAscii(byte[] buffer, int index, string value)
		{
			ConversionHelpers.encoding1252.GetBytes(value.ToCharArray(), 0, value.Length, buffer, index);
		}

		// Token: 0x06005DC4 RID: 24004 RVA: 0x0017F4FC File Offset: 0x0017D6FC
		public static void MoveStringToBufferSingleByte(byte[] buffer, int index, string value, int maxLength, bool blankPad, HisEncoding encoding)
		{
			int num = 0;
			string text = null;
			if (!string.IsNullOrWhiteSpace(value))
			{
				text = value.Trim();
				if (text.Length > maxLength)
				{
					text = text.Substring(0, maxLength);
				}
				else if (blankPad)
				{
					text = text.PadRight(maxLength);
				}
				num = text.Length;
			}
			else if (blankPad)
			{
				text = new string(' ', maxLength);
				num = maxLength;
			}
			if (num != 0 && encoding.GetBytes(text.ToCharArray(), 0, num, buffer, index) != num)
			{
				throw new ArgumentOutOfRangeException("encoding");
			}
			if (!blankPad && num < maxLength)
			{
				index += num;
				for (int i = num; i < maxLength; i++)
				{
					buffer[index++] = 0;
				}
			}
		}

		// Token: 0x06005DC5 RID: 24005 RVA: 0x0017F598 File Offset: 0x0017D798
		public static string GetStringOrNull(byte[] buffer, int index, int length, Encoding encoding)
		{
			int num = -1;
			for (int i = index; i < index + length; i++)
			{
				if (buffer[i] == 0)
				{
					num = i;
					break;
				}
			}
			if (num == index)
			{
				return null;
			}
			int num2 = ((num == -1) ? length : (num - index));
			string text = encoding.GetString(buffer, index, num2).Trim();
			if (text.Length == 0)
			{
				return null;
			}
			return text;
		}

		// Token: 0x06005DC6 RID: 24006 RVA: 0x0017F5EC File Offset: 0x0017D7EC
		public static byte[] ByteArrayNullOrCopy(byte[] buffer, bool deepCopy)
		{
			if (buffer == null || !deepCopy)
			{
				return buffer;
			}
			byte[] array = new byte[buffer.Length];
			Array.Copy(buffer, array, buffer.Length);
			return array;
		}

		// Token: 0x04004FB1 RID: 20401
		private static Encoding encoding1252 = Encoding.GetEncoding(1252);
	}
}
