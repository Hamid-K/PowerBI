using System;
using System.Globalization;
using System.IO;
using System.Text;
using NLog.MessageTemplates;

namespace NLog.Internal
{
	// Token: 0x02000144 RID: 324
	internal static class StringBuilderExt
	{
		// Token: 0x06000FB4 RID: 4020 RVA: 0x000282F8 File Offset: 0x000264F8
		public static void AppendFormattedValue(this StringBuilder builder, object value, string format, IFormatProvider formatProvider)
		{
			if (value is string && string.IsNullOrEmpty(format))
			{
				builder.Append(value);
				return;
			}
			if (format == "@")
			{
				ValueFormatter.Instance.FormatValue(value, null, CaptureType.Serialize, formatProvider, builder);
				return;
			}
			if (value != null)
			{
				ValueFormatter.Instance.FormatValue(value, format, CaptureType.Normal, formatProvider, builder);
			}
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x00028350 File Offset: 0x00026550
		public static void AppendInvariant(this StringBuilder builder, int value)
		{
			if (value < 0)
			{
				builder.Append('-');
				uint num = (uint)(-1 - value + 1);
				builder.AppendInvariant(num);
				return;
			}
			builder.AppendInvariant((uint)value);
		}

		// Token: 0x06000FB6 RID: 4022 RVA: 0x00028380 File Offset: 0x00026580
		public static void AppendInvariant(this StringBuilder builder, uint value)
		{
			if (value == 0U)
			{
				builder.Append('0');
				return;
			}
			int i = 0;
			uint num = value;
			do
			{
				num /= 10U;
				i++;
			}
			while (num > 0U);
			builder.Append('0', i);
			int num2 = builder.Length;
			while (i > 0)
			{
				num2--;
				builder[num2] = StringBuilderExt.charToInt[(int)(value % 10U)];
				value /= 10U;
				i--;
			}
		}

		// Token: 0x06000FB7 RID: 4023 RVA: 0x000283E1 File Offset: 0x000265E1
		public static void ClearBuilder(this StringBuilder builder)
		{
			builder.Clear();
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x000283EC File Offset: 0x000265EC
		public static void CopyToStream(this StringBuilder builder, MemoryStream ms, Encoding encoding, char[] transformBuffer)
		{
			if (transformBuffer != null)
			{
				int num = encoding.GetMaxByteCount(builder.Length);
				ms.SetLength(ms.Position + (long)num);
				for (int i = 0; i < builder.Length; i += transformBuffer.Length)
				{
					int num2 = Math.Min(builder.Length - i, transformBuffer.Length);
					builder.CopyTo(i, transformBuffer, 0, num2);
					num = encoding.GetBytes(transformBuffer, 0, num2, ms.GetBuffer(), (int)ms.Position);
					ms.Position += (long)num;
				}
				if (ms.Position != ms.Length)
				{
					ms.SetLength(ms.Position);
					return;
				}
			}
			else
			{
				string text = builder.ToString();
				byte[] bytes = encoding.GetBytes(text);
				ms.Write(bytes, 0, bytes.Length);
			}
		}

		// Token: 0x06000FB9 RID: 4025 RVA: 0x000284A8 File Offset: 0x000266A8
		public static void CopyTo(this StringBuilder builder, StringBuilder destination)
		{
			int length = builder.Length;
			if (length > 0)
			{
				destination.EnsureCapacity(length + destination.Length);
				if (length < 8)
				{
					for (int i = 0; i < length; i++)
					{
						destination.Append(builder[i]);
					}
					return;
				}
				if (length < 512)
				{
					destination.Append(builder.ToString());
					return;
				}
				char[] array = new char[256];
				for (int j = 0; j < length; j += array.Length)
				{
					int num = Math.Min(length - j, array.Length);
					builder.CopyTo(j, array, 0, num);
					destination.Append(array, 0, num);
				}
			}
		}

		// Token: 0x06000FBA RID: 4026 RVA: 0x00028544 File Offset: 0x00026744
		public static int IndexOf(this StringBuilder builder, char needle, int startPos = 0)
		{
			for (int i = startPos; i < builder.Length; i++)
			{
				if (builder[i] == needle)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000FBB RID: 4027 RVA: 0x00028570 File Offset: 0x00026770
		public static int IndexOfAny(this StringBuilder builder, char[] needles, int startPos = 0)
		{
			for (int i = startPos; i < builder.Length; i++)
			{
				if (StringBuilderExt.CharArrayContains(builder[i], needles))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000FBC RID: 4028 RVA: 0x000285A0 File Offset: 0x000267A0
		private static bool CharArrayContains(char searchChar, char[] needles)
		{
			for (int i = 0; i < needles.Length; i++)
			{
				if (needles[i] == searchChar)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000FBD RID: 4029 RVA: 0x000285C4 File Offset: 0x000267C4
		public static bool EqualTo(this StringBuilder builder, StringBuilder other)
		{
			if (builder.Length != other.Length)
			{
				return false;
			}
			for (int i = 0; i < builder.Length; i++)
			{
				if (builder[i] != other[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000FBE RID: 4030 RVA: 0x00028608 File Offset: 0x00026808
		public static bool EqualTo(this StringBuilder builder, string other)
		{
			if (builder.Length != other.Length)
			{
				return false;
			}
			for (int i = 0; i < other.Length; i++)
			{
				if (builder[i] != other[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000FBF RID: 4031 RVA: 0x00028649 File Offset: 0x00026849
		internal static void Append2DigitsZeroPadded(this StringBuilder builder, int number)
		{
			builder.Append((char)(number / 10 + 48));
			builder.Append((char)(number % 10 + 48));
		}

		// Token: 0x06000FC0 RID: 4032 RVA: 0x0002866C File Offset: 0x0002686C
		internal static void Append4DigitsZeroPadded(this StringBuilder builder, int number)
		{
			builder.Append((char)(number / 1000 % 10 + 48));
			builder.Append((char)(number / 100 % 10 + 48));
			builder.Append((char)(number / 10 % 10 + 48));
			builder.Append((char)(number / 1 % 10 + 48));
		}

		// Token: 0x06000FC1 RID: 4033 RVA: 0x000286C4 File Offset: 0x000268C4
		internal static void AppendIntegerAsString(this StringBuilder sb, IConvertible value, TypeCode objTypeCode)
		{
			switch (objTypeCode)
			{
			case TypeCode.SByte:
				sb.AppendInvariant((int)value.ToSByte(CultureInfo.InvariantCulture));
				return;
			case TypeCode.Byte:
				sb.AppendInvariant((int)value.ToByte(CultureInfo.InvariantCulture));
				return;
			case TypeCode.Int16:
				sb.AppendInvariant((int)value.ToInt16(CultureInfo.InvariantCulture));
				return;
			case TypeCode.UInt16:
				sb.AppendInvariant((int)value.ToUInt16(CultureInfo.InvariantCulture));
				return;
			case TypeCode.Int32:
				sb.AppendInvariant(value.ToInt32(CultureInfo.InvariantCulture));
				return;
			case TypeCode.UInt32:
				sb.AppendInvariant(value.ToUInt32(CultureInfo.InvariantCulture));
				return;
			case TypeCode.Int64:
			{
				long num = value.ToInt64(CultureInfo.InvariantCulture);
				if (num < 2147483647L && num > -2147483648L)
				{
					sb.AppendInvariant((int)num);
					return;
				}
				sb.Append(num);
				return;
			}
			case TypeCode.UInt64:
			{
				ulong num2 = value.ToUInt64(CultureInfo.InvariantCulture);
				if (num2 < (ulong)(-1))
				{
					sb.AppendInvariant((uint)num2);
					return;
				}
				sb.Append(num2);
				return;
			}
			default:
				sb.Append(XmlHelper.XmlConvertToString(value, objTypeCode, false));
				return;
			}
		}

		// Token: 0x06000FC2 RID: 4034 RVA: 0x000287CC File Offset: 0x000269CC
		public static void TrimRight(this StringBuilder sb, int startPos = 0)
		{
			int num = sb.Length - 1;
			while (num >= startPos && char.IsWhiteSpace(sb[num]))
			{
				num--;
			}
			if (num < sb.Length - 1)
			{
				sb.Length = num + 1;
			}
		}

		// Token: 0x04000436 RID: 1078
		private static readonly char[] charToInt = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
	}
}
