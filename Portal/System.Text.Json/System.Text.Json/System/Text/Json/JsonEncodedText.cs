using System;
using System.Buffers;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text.Encodings.Web;

namespace System.Text.Json
{
	// Token: 0x0200003E RID: 62
	public readonly struct JsonEncodedText : IEquatable<JsonEncodedText>
	{
		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000301 RID: 769 RVA: 0x00009146 File Offset: 0x00007346
		public ReadOnlySpan<byte> EncodedUtf8Bytes
		{
			get
			{
				return this._utf8Value;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000302 RID: 770 RVA: 0x00009153 File Offset: 0x00007353
		[Nullable(1)]
		public string Value
		{
			[NullableContext(1)]
			get
			{
				return this._value ?? string.Empty;
			}
		}

		// Token: 0x06000303 RID: 771 RVA: 0x00009164 File Offset: 0x00007364
		private JsonEncodedText(byte[] utf8Value)
		{
			this._value = JsonReaderHelper.GetTextFromUtf8(utf8Value);
			this._utf8Value = utf8Value;
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000917E File Offset: 0x0000737E
		[NullableContext(1)]
		public static JsonEncodedText Encode(string value, [Nullable(2)] JavaScriptEncoder encoder = null)
		{
			if (value == null)
			{
				ThrowHelper.ThrowArgumentNullException("value");
			}
			return JsonEncodedText.Encode(value.AsSpan(), encoder);
		}

		// Token: 0x06000305 RID: 773 RVA: 0x00009199 File Offset: 0x00007399
		public static JsonEncodedText Encode(ReadOnlySpan<char> value, [Nullable(2)] JavaScriptEncoder encoder = null)
		{
			if (value.Length == 0)
			{
				return new JsonEncodedText(Array.Empty<byte>());
			}
			return JsonEncodedText.TranscodeAndEncode(value, encoder);
		}

		// Token: 0x06000306 RID: 774 RVA: 0x000091B8 File Offset: 0x000073B8
		private static JsonEncodedText TranscodeAndEncode(ReadOnlySpan<char> value, JavaScriptEncoder encoder)
		{
			JsonWriterHelper.ValidateValue(value);
			int utf8ByteCount = JsonReaderHelper.GetUtf8ByteCount(value);
			byte[] array = ArrayPool<byte>.Shared.Rent(utf8ByteCount);
			int utf8FromText = JsonReaderHelper.GetUtf8FromText(value, array);
			JsonEncodedText jsonEncodedText = JsonEncodedText.EncodeHelper(array.AsSpan(0, utf8FromText), encoder);
			array.AsSpan(0, utf8ByteCount).Clear();
			ArrayPool<byte>.Shared.Return(array, false);
			return jsonEncodedText;
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000921D File Offset: 0x0000741D
		public static JsonEncodedText Encode(ReadOnlySpan<byte> utf8Value, [Nullable(2)] JavaScriptEncoder encoder = null)
		{
			if (utf8Value.Length == 0)
			{
				return new JsonEncodedText(Array.Empty<byte>());
			}
			JsonWriterHelper.ValidateValue(utf8Value);
			return JsonEncodedText.EncodeHelper(utf8Value, encoder);
		}

		// Token: 0x06000308 RID: 776 RVA: 0x00009240 File Offset: 0x00007440
		private static JsonEncodedText EncodeHelper(ReadOnlySpan<byte> utf8Value, JavaScriptEncoder encoder)
		{
			int num = JsonWriterHelper.NeedsEscaping(utf8Value, encoder);
			if (num != -1)
			{
				return new JsonEncodedText(JsonHelpers.EscapeValue(utf8Value, num, encoder));
			}
			return new JsonEncodedText(utf8Value.ToArray());
		}

		// Token: 0x06000309 RID: 777 RVA: 0x00009273 File Offset: 0x00007473
		public bool Equals(JsonEncodedText other)
		{
			if (this._value == null)
			{
				return other._value == null;
			}
			return this._value.Equals(other._value);
		}

		// Token: 0x0600030A RID: 778 RVA: 0x00009298 File Offset: 0x00007498
		[NullableContext(2)]
		public override bool Equals([NotNullWhen(true)] object obj)
		{
			if (obj is JsonEncodedText)
			{
				JsonEncodedText jsonEncodedText = (JsonEncodedText)obj;
				return this.Equals(jsonEncodedText);
			}
			return false;
		}

		// Token: 0x0600030B RID: 779 RVA: 0x000092BD File Offset: 0x000074BD
		[NullableContext(1)]
		public override string ToString()
		{
			return this._value ?? string.Empty;
		}

		// Token: 0x0600030C RID: 780 RVA: 0x000092CE File Offset: 0x000074CE
		public override int GetHashCode()
		{
			if (this._value != null)
			{
				return this._value.GetHashCode();
			}
			return 0;
		}

		// Token: 0x04000137 RID: 311
		internal readonly byte[] _utf8Value;

		// Token: 0x04000138 RID: 312
		internal readonly string _value;
	}
}
