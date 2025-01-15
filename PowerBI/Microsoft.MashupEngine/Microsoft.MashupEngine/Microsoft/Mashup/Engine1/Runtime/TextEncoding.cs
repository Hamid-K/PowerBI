using System;
using System.Text;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001665 RID: 5733
	internal static class TextEncoding
	{
		// Token: 0x06009108 RID: 37128 RVA: 0x001E2194 File Offset: 0x001E0394
		public static Encoding GetEncoding(Value encodingValue, Value includeByteOrderMarkValue, TextEncoding.CodePage defaultEncoding = TextEncoding.CodePage.Utf8)
		{
			TextEncoding.CodePage codePage = (encodingValue.IsNull ? defaultEncoding : ((TextEncoding.CodePage)encodingValue.AsInteger32));
			bool flag = !includeByteOrderMarkValue.IsNull && includeByteOrderMarkValue.AsBoolean;
			try
			{
				return TextEncoding.GetEncoding(codePage, flag);
			}
			catch (ArgumentException)
			{
			}
			catch (NotSupportedException)
			{
			}
			throw ValueException.NewExpressionError<Message0>(Strings.Text_GetEncoding_InvalidEncoding, encodingValue, null);
		}

		// Token: 0x06009109 RID: 37129 RVA: 0x001E2200 File Offset: 0x001E0400
		public static TextDecoder GetTextDecoder(Value encodingValue)
		{
			if (encodingValue.IsNull)
			{
				return TextEncoding.autoDetectDecoder;
			}
			TextEncoding.CodePage asInteger = (TextEncoding.CodePage)encodingValue.AsInteger32;
			if (asInteger <= TextEncoding.CodePage.BigEndianUnicode)
			{
				if (asInteger == TextEncoding.CodePage.Unicode)
				{
					return TextEncoding.unicodeDecoder;
				}
				if (asInteger == TextEncoding.CodePage.BigEndianUnicode)
				{
					return TextEncoding.bigEndianUnicodeDecoder;
				}
			}
			else
			{
				if (asInteger == TextEncoding.CodePage.Windows)
				{
					return TextEncoding.windowsDecoder;
				}
				if (asInteger == TextEncoding.CodePage.Ascii)
				{
					return TextEncoding.asciiDecoder;
				}
				if (asInteger == TextEncoding.CodePage.Utf8)
				{
					return TextEncoding.utf8Decoder;
				}
			}
			return new TextEncoding.NoPreambleTextDecoder(TextEncoding.GetEncoding(encodingValue, LogicalValue.False, TextEncoding.CodePage.Utf8));
		}

		// Token: 0x0600910A RID: 37130 RVA: 0x001E2288 File Offset: 0x001E0488
		private static Encoding GetEncoding(TextEncoding.CodePage codePage, bool includeByteOrderMark)
		{
			if (codePage <= TextEncoding.CodePage.BigEndianUnicode)
			{
				if (codePage == TextEncoding.CodePage.Unicode)
				{
					return new UnicodeEncoding(false, includeByteOrderMark);
				}
				if (codePage == TextEncoding.CodePage.BigEndianUnicode)
				{
					return new UnicodeEncoding(true, includeByteOrderMark);
				}
			}
			else
			{
				if (codePage == TextEncoding.CodePage.Ascii)
				{
					return Encoding.ASCII;
				}
				if (codePage == TextEncoding.CodePage.Utf8)
				{
					return new UTF8Encoding(includeByteOrderMark);
				}
			}
			return Encoding.GetEncoding((int)codePage);
		}

		// Token: 0x0600910B RID: 37131 RVA: 0x001E22E4 File Offset: 0x001E04E4
		public static bool TryGetEncoding(byte[] bytes, int offset, int count, out Encoding encoding)
		{
			int num;
			TextEncoding.PreambleType preambleType;
			if (TextEncoding.TryGetEncodingFromByteOrderMarks(bytes, offset, count, out num, out preambleType, out encoding))
			{
				return true;
			}
			if (TextEncoding.TryGetEncodingFromAlternatingZeros(bytes, offset, count, out num, out preambleType, out encoding))
			{
				return true;
			}
			if (TextEncoding.TryGetEncodingFromByteSequences(bytes, offset, count, out num, out preambleType, out encoding))
			{
				return true;
			}
			encoding = null;
			return false;
		}

		// Token: 0x0600910C RID: 37132 RVA: 0x001E2328 File Offset: 0x001E0528
		private static bool TryGetEncodingFromByteOrderMarks(byte[] bytes, int index, int count, out int preambleLength, out TextEncoding.PreambleType preambleType, out Encoding encoding)
		{
			if (count > 0)
			{
				byte b = bytes[0];
				if (b <= 239)
				{
					if (b == 0)
					{
						return TextEncoding.TryGetEncodingFromByteOrderMarks00(bytes, index, count, out preambleLength, out preambleType, out encoding);
					}
					if (b == 239)
					{
						return TextEncoding.TryGetEncodingFromByteOrderMarksEF(bytes, index, count, out preambleLength, out preambleType, out encoding);
					}
				}
				else
				{
					if (b == 254)
					{
						return TextEncoding.TryGetEncodingFromByteOrderMarksFE(bytes, index, count, out preambleLength, out preambleType, out encoding);
					}
					if (b == 255)
					{
						return TextEncoding.TryGetEncodingFromByteOrderMarksFF(bytes, index, count, out preambleLength, out preambleType, out encoding);
					}
				}
			}
			preambleLength = 0;
			preambleType = TextEncoding.PreambleType.None;
			encoding = Encoding.UTF8;
			return false;
		}

		// Token: 0x0600910D RID: 37133 RVA: 0x001E23AC File Offset: 0x001E05AC
		private static bool TryGetEncodingFromByteOrderMarks00(byte[] bytes, int index, int count, out int preambleLength, out TextEncoding.PreambleType preambleType, out Encoding encoding)
		{
			if (count >= 4 && bytes[index] == 0 && bytes[index + 1] == 0 && bytes[index + 2] == 254 && bytes[index + 3] == 255)
			{
				preambleLength = 4;
				preambleType = TextEncoding.PreambleType.BigEndianUTF32;
				encoding = new UTF32Encoding(true, false);
				return true;
			}
			preambleLength = 0;
			preambleType = TextEncoding.PreambleType.None;
			encoding = Encoding.UTF8;
			return false;
		}

		// Token: 0x0600910E RID: 37134 RVA: 0x001E2404 File Offset: 0x001E0604
		private static bool TryGetEncodingFromByteOrderMarksFF(byte[] bytes, int index, int count, out int preambleLength, out TextEncoding.PreambleType preambleType, out Encoding encoding)
		{
			if (count >= 4 && bytes[index] == 255 && bytes[index + 1] == 254 && bytes[index + 2] == 0 && bytes[index + 3] == 0)
			{
				preambleLength = 4;
				preambleType = TextEncoding.PreambleType.UTF32;
				encoding = Encoding.UTF32;
				return true;
			}
			if (count >= 2 && bytes[index] == 255 && bytes[index + 1] == 254)
			{
				preambleLength = 2;
				preambleType = TextEncoding.PreambleType.Unicode;
				encoding = Encoding.Unicode;
				return true;
			}
			preambleLength = 0;
			preambleType = TextEncoding.PreambleType.None;
			encoding = Encoding.UTF8;
			return false;
		}

		// Token: 0x0600910F RID: 37135 RVA: 0x001E2488 File Offset: 0x001E0688
		private static bool TryGetEncodingFromByteOrderMarksEF(byte[] bytes, int index, int count, out int preambleLength, out TextEncoding.PreambleType preambleType, out Encoding encoding)
		{
			if (count >= 3 && bytes[index] == 239 && bytes[index + 1] == 187 && bytes[index + 2] == 191)
			{
				preambleLength = 3;
				preambleType = TextEncoding.PreambleType.UTF8;
				encoding = Encoding.UTF8;
				return true;
			}
			preambleLength = 0;
			preambleType = TextEncoding.PreambleType.None;
			encoding = Encoding.UTF8;
			return false;
		}

		// Token: 0x06009110 RID: 37136 RVA: 0x001E24DC File Offset: 0x001E06DC
		private static bool TryGetEncodingFromByteOrderMarksFE(byte[] bytes, int index, int count, out int preambleLength, out TextEncoding.PreambleType preambleType, out Encoding encoding)
		{
			if (count >= 2 && bytes[index] == 254 && bytes[index + 1] == 255)
			{
				preambleLength = 2;
				preambleType = TextEncoding.PreambleType.BigEndianUnicode;
				encoding = Encoding.BigEndianUnicode;
				return true;
			}
			preambleLength = 0;
			preambleType = TextEncoding.PreambleType.None;
			encoding = Encoding.UTF8;
			return false;
		}

		// Token: 0x06009111 RID: 37137 RVA: 0x001E251C File Offset: 0x001E071C
		private static bool TryGetEncodingFromAlternatingZeros(byte[] bytes, int index, int count, out int preambleLength, out TextEncoding.PreambleType preambleType, out Encoding encoding)
		{
			preambleLength = 0;
			preambleType = TextEncoding.PreambleType.None;
			int[] array = new int[2];
			for (int i = index; i < count; i++)
			{
				if (bytes[index + i] == 0)
				{
					array[i % 2]++;
				}
			}
			int num = count / 4 + 1;
			if (array[0] >= num)
			{
				encoding = new UTF32Encoding(true, false);
				return true;
			}
			if (array[1] >= num)
			{
				encoding = Encoding.Unicode;
				return true;
			}
			encoding = null;
			return false;
		}

		// Token: 0x06009112 RID: 37138 RVA: 0x001E2588 File Offset: 0x001E0788
		private static bool TryGetEncodingFromByteSequences(byte[] bytes, int index, int count, out int preambleLength, out TextEncoding.PreambleType preambleType, out Encoding encoding)
		{
			preambleLength = 0;
			preambleType = TextEncoding.PreambleType.None;
			encoding = null;
			int i = index;
			while (i <= count - 4)
			{
				if (bytes[i] <= 127)
				{
					i++;
				}
				else if (bytes[i] >= 194 && bytes[i] <= 223 && bytes[i + 1] >= 128 && bytes[i + 1] <= 191)
				{
					i += 2;
					encoding = Encoding.UTF8;
				}
				else if (bytes[i] == 224 && bytes[i + 1] >= 160 && bytes[i + 1] <= 191 && bytes[i + 2] >= 128 && bytes[i + 2] <= 191)
				{
					i += 3;
					encoding = Encoding.UTF8;
				}
				else if (bytes[i] >= 225 && bytes[i] <= 236 && bytes[i + 1] >= 128 && bytes[i + 1] <= 191 && bytes[i + 2] >= 128 && bytes[i + 2] <= 191)
				{
					i += 3;
					encoding = Encoding.UTF8;
				}
				else if (bytes[i] == 237 && bytes[i + 1] >= 128 && bytes[i + 1] <= 159 && bytes[i + 2] >= 128 && bytes[i + 2] <= 191)
				{
					i += 3;
					encoding = Encoding.UTF8;
				}
				else if (bytes[i] >= 238 && bytes[i] <= 239 && bytes[i + 1] >= 128 && bytes[i + 1] <= 191 && bytes[i + 2] >= 128 && bytes[i + 2] <= 191)
				{
					i += 3;
					encoding = Encoding.UTF8;
				}
				else if (bytes[i] == 240 && bytes[i + 1] >= 144 && bytes[i + 1] <= 191 && bytes[i + 2] >= 128 && bytes[i + 2] <= 191 && bytes[i + 3] >= 128 && bytes[i + 3] <= 191)
				{
					i += 4;
					encoding = Encoding.UTF8;
				}
				else if (bytes[i] >= 241 && bytes[i] <= 243 && bytes[i + 1] >= 128 && bytes[i + 1] <= 191 && bytes[i + 2] >= 128 && bytes[i + 2] <= 191 && bytes[i + 3] >= 128 && bytes[i + 3] <= 191)
				{
					i += 4;
					encoding = Encoding.UTF8;
				}
				else
				{
					if (bytes[i] != 244 || bytes[i + 1] < 128 || bytes[i + 1] > 143 || bytes[i + 2] < 128 || bytes[i + 2] > 191 || bytes[i + 3] < 128 || bytes[i + 3] > 191)
					{
						encoding = null;
						break;
					}
					i += 4;
					encoding = Encoding.UTF8;
				}
			}
			return encoding != null;
		}

		// Token: 0x04004DDB RID: 19931
		public static readonly IntEnumTypeValue<TextEncoding.CodePage> Type = new IntEnumTypeValue<TextEncoding.CodePage>("TextEncoding.Type");

		// Token: 0x04004DDC RID: 19932
		public static readonly NumberValue Utf8 = TextEncoding.Type.NewEnumValue("TextEncoding.Utf8", 65001, TextEncoding.CodePage.Utf8, null);

		// Token: 0x04004DDD RID: 19933
		public static readonly NumberValue Utf16 = TextEncoding.Type.NewEnumValue("TextEncoding.Utf16", 1200, TextEncoding.CodePage.Unicode, null);

		// Token: 0x04004DDE RID: 19934
		public static readonly NumberValue Ascii = TextEncoding.Type.NewEnumValue("TextEncoding.Ascii", 20127, TextEncoding.CodePage.Ascii, null);

		// Token: 0x04004DDF RID: 19935
		public static readonly NumberValue Unicode = TextEncoding.Type.NewEnumValue("TextEncoding.Unicode", 1200, TextEncoding.CodePage.Unicode, null);

		// Token: 0x04004DE0 RID: 19936
		public static readonly NumberValue BigEndianUnicode = TextEncoding.Type.NewEnumValue("TextEncoding.BigEndianUnicode", 1201, TextEncoding.CodePage.BigEndianUnicode, null);

		// Token: 0x04004DE1 RID: 19937
		public static readonly NumberValue Windows = TextEncoding.Type.NewEnumValue("TextEncoding.Windows", 1252, TextEncoding.CodePage.Windows, null);

		// Token: 0x04004DE2 RID: 19938
		private static readonly TextEncoding.AutoDetectTextDecoder autoDetectDecoder = new TextEncoding.AutoDetectTextDecoder();

		// Token: 0x04004DE3 RID: 19939
		private static readonly TextEncoding.PreambleTextDecoder utf8Decoder = new TextEncoding.PreambleTextDecoder(TextEncoding.PreambleType.UTF8, Encoding.UTF8);

		// Token: 0x04004DE4 RID: 19940
		private static readonly TextEncoding.PreambleTextDecoder unicodeDecoder = new TextEncoding.PreambleTextDecoder(TextEncoding.PreambleType.Unicode, Encoding.Unicode);

		// Token: 0x04004DE5 RID: 19941
		private static readonly TextEncoding.PreambleTextDecoder bigEndianUnicodeDecoder = new TextEncoding.PreambleTextDecoder(TextEncoding.PreambleType.BigEndianUnicode, Encoding.BigEndianUnicode);

		// Token: 0x04004DE6 RID: 19942
		private static readonly TextEncoding.NoPreambleTextDecoder asciiDecoder = new TextEncoding.NoPreambleTextDecoder(Encoding.ASCII);

		// Token: 0x04004DE7 RID: 19943
		private static readonly TextEncoding.NoPreambleTextDecoder windowsDecoder = new TextEncoding.NoPreambleTextDecoder(Encoding.GetEncoding(1252));

		// Token: 0x02001666 RID: 5734
		private enum PreambleType
		{
			// Token: 0x04004DE9 RID: 19945
			None,
			// Token: 0x04004DEA RID: 19946
			UTF8,
			// Token: 0x04004DEB RID: 19947
			BigEndianUnicode,
			// Token: 0x04004DEC RID: 19948
			Unicode,
			// Token: 0x04004DED RID: 19949
			BigEndianUTF32,
			// Token: 0x04004DEE RID: 19950
			UTF32
		}

		// Token: 0x02001667 RID: 5735
		internal enum CodePage
		{
			// Token: 0x04004DF0 RID: 19952
			Unicode = 1200,
			// Token: 0x04004DF1 RID: 19953
			BigEndianUnicode,
			// Token: 0x04004DF2 RID: 19954
			Windows = 1252,
			// Token: 0x04004DF3 RID: 19955
			Ascii = 20127,
			// Token: 0x04004DF4 RID: 19956
			Utf8 = 65001
		}

		// Token: 0x02001668 RID: 5736
		private class AutoDetectTextDecoder : TextDecoder
		{
			// Token: 0x06009113 RID: 37139 RVA: 0x001E2878 File Offset: 0x001E0A78
			public override string Decode(byte[] bytes, int index, int count)
			{
				int num;
				TextEncoding.PreambleType preambleType;
				Encoding encoding;
				TextEncoding.TryGetEncodingFromByteOrderMarks(bytes, index, count, out num, out preambleType, out encoding);
				return encoding.GetString(bytes, index + num, count - num);
			}
		}

		// Token: 0x02001669 RID: 5737
		private class PreambleTextDecoder : TextDecoder
		{
			// Token: 0x06009115 RID: 37141 RVA: 0x001E28A9 File Offset: 0x001E0AA9
			public PreambleTextDecoder(TextEncoding.PreambleType preambleType, Encoding encoding)
			{
				this.preambleType = preambleType;
				this.encoding = encoding;
			}

			// Token: 0x06009116 RID: 37142 RVA: 0x001E28C0 File Offset: 0x001E0AC0
			public override string Decode(byte[] bytes, int index, int count)
			{
				int num;
				TextEncoding.PreambleType preambleType;
				Encoding encoding;
				if (TextEncoding.TryGetEncodingFromByteOrderMarks(bytes, index, count, out num, out preambleType, out encoding) && preambleType != this.preambleType)
				{
					throw ValueException.NewDataFormatError<Message0>(Strings.Binary_UnexpectedPreamble, Value.Null, null);
				}
				return this.encoding.GetString(bytes, index + num, count - num);
			}

			// Token: 0x04004DF5 RID: 19957
			private readonly TextEncoding.PreambleType preambleType;

			// Token: 0x04004DF6 RID: 19958
			private readonly Encoding encoding;
		}

		// Token: 0x0200166A RID: 5738
		private class NoPreambleTextDecoder : TextDecoder
		{
			// Token: 0x06009117 RID: 37143 RVA: 0x001E2909 File Offset: 0x001E0B09
			public NoPreambleTextDecoder(Encoding encoding)
			{
				this.encoding = encoding;
			}

			// Token: 0x06009118 RID: 37144 RVA: 0x001E2918 File Offset: 0x001E0B18
			public override string Decode(byte[] bytes, int index, int count)
			{
				return this.encoding.GetString(bytes, index, count);
			}

			// Token: 0x04004DF7 RID: 19959
			private readonly Encoding encoding;
		}
	}
}
