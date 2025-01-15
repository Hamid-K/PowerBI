using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.CompilerServices;

namespace System.Text.Json
{
	// Token: 0x0200003B RID: 59
	[DebuggerDisplay("{DebuggerDisplay,nq}")]
	public readonly struct JsonElement
	{
		// Token: 0x060002B5 RID: 693 RVA: 0x0000880D File Offset: 0x00006A0D
		internal JsonElement(JsonDocument parent, int idx)
		{
			this._parent = parent;
			this._idx = idx;
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x0000881D File Offset: 0x00006A1D
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private JsonTokenType TokenType
		{
			get
			{
				JsonDocument parent = this._parent;
				if (parent == null)
				{
					return JsonTokenType.None;
				}
				return parent.GetJsonTokenType(this._idx);
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x00008836 File Offset: 0x00006A36
		public JsonValueKind ValueKind
		{
			get
			{
				return this.TokenType.ToValueKind();
			}
		}

		// Token: 0x17000113 RID: 275
		public JsonElement this[int index]
		{
			get
			{
				this.CheckValidInstance();
				return this._parent.GetArrayIndexElement(this._idx, index);
			}
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000885D File Offset: 0x00006A5D
		public int GetArrayLength()
		{
			this.CheckValidInstance();
			return this._parent.GetArrayLength(this._idx);
		}

		// Token: 0x060002BA RID: 698 RVA: 0x00008878 File Offset: 0x00006A78
		[NullableContext(1)]
		public JsonElement GetProperty(string propertyName)
		{
			if (propertyName == null)
			{
				ThrowHelper.ThrowArgumentNullException("propertyName");
			}
			JsonElement jsonElement;
			if (this.TryGetProperty(propertyName, out jsonElement))
			{
				return jsonElement;
			}
			throw new KeyNotFoundException();
		}

		// Token: 0x060002BB RID: 699 RVA: 0x000088A4 File Offset: 0x00006AA4
		public JsonElement GetProperty(ReadOnlySpan<char> propertyName)
		{
			JsonElement jsonElement;
			if (this.TryGetProperty(propertyName, out jsonElement))
			{
				return jsonElement;
			}
			throw new KeyNotFoundException();
		}

		// Token: 0x060002BC RID: 700 RVA: 0x000088C4 File Offset: 0x00006AC4
		public JsonElement GetProperty(ReadOnlySpan<byte> utf8PropertyName)
		{
			JsonElement jsonElement;
			if (this.TryGetProperty(utf8PropertyName, out jsonElement))
			{
				return jsonElement;
			}
			throw new KeyNotFoundException();
		}

		// Token: 0x060002BD RID: 701 RVA: 0x000088E3 File Offset: 0x00006AE3
		[NullableContext(1)]
		public bool TryGetProperty(string propertyName, out JsonElement value)
		{
			if (propertyName == null)
			{
				ThrowHelper.ThrowArgumentNullException("propertyName");
			}
			return this.TryGetProperty(propertyName.AsSpan(), out value);
		}

		// Token: 0x060002BE RID: 702 RVA: 0x000088FF File Offset: 0x00006AFF
		public bool TryGetProperty(ReadOnlySpan<char> propertyName, out JsonElement value)
		{
			this.CheckValidInstance();
			return this._parent.TryGetNamedPropertyValue(this._idx, propertyName, out value);
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000891A File Offset: 0x00006B1A
		public bool TryGetProperty(ReadOnlySpan<byte> utf8PropertyName, out JsonElement value)
		{
			this.CheckValidInstance();
			return this._parent.TryGetNamedPropertyValue(this._idx, utf8PropertyName, out value);
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00008938 File Offset: 0x00006B38
		public bool GetBoolean()
		{
			JsonTokenType tokenType = this.TokenType;
			return tokenType == JsonTokenType.True || (tokenType != JsonTokenType.False && JsonElement.<GetBoolean>g__ThrowJsonElementWrongTypeException|17_0(tokenType));
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00008960 File Offset: 0x00006B60
		[NullableContext(2)]
		public string GetString()
		{
			this.CheckValidInstance();
			return this._parent.GetString(this._idx, JsonTokenType.String);
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000897A File Offset: 0x00006B7A
		[NullableContext(2)]
		public bool TryGetBytesFromBase64([NotNullWhen(true)] out byte[] value)
		{
			this.CheckValidInstance();
			return this._parent.TryGetValue(this._idx, out value);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00008994 File Offset: 0x00006B94
		[NullableContext(1)]
		public byte[] GetBytesFromBase64()
		{
			byte[] array;
			if (!this.TryGetBytesFromBase64(out array))
			{
				ThrowHelper.ThrowFormatException();
			}
			return array;
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x000089B1 File Offset: 0x00006BB1
		[CLSCompliant(false)]
		public bool TryGetSByte(out sbyte value)
		{
			this.CheckValidInstance();
			return this._parent.TryGetValue(this._idx, out value);
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x000089CC File Offset: 0x00006BCC
		[CLSCompliant(false)]
		public sbyte GetSByte()
		{
			sbyte b;
			if (this.TryGetSByte(out b))
			{
				return b;
			}
			throw new FormatException();
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x000089EA File Offset: 0x00006BEA
		public bool TryGetByte(out byte value)
		{
			this.CheckValidInstance();
			return this._parent.TryGetValue(this._idx, out value);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00008A04 File Offset: 0x00006C04
		public byte GetByte()
		{
			byte b;
			if (this.TryGetByte(out b))
			{
				return b;
			}
			throw new FormatException();
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00008A22 File Offset: 0x00006C22
		public bool TryGetInt16(out short value)
		{
			this.CheckValidInstance();
			return this._parent.TryGetValue(this._idx, out value);
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00008A3C File Offset: 0x00006C3C
		public short GetInt16()
		{
			short num;
			if (this.TryGetInt16(out num))
			{
				return num;
			}
			throw new FormatException();
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00008A5A File Offset: 0x00006C5A
		[CLSCompliant(false)]
		public bool TryGetUInt16(out ushort value)
		{
			this.CheckValidInstance();
			return this._parent.TryGetValue(this._idx, out value);
		}

		// Token: 0x060002CB RID: 715 RVA: 0x00008A74 File Offset: 0x00006C74
		[CLSCompliant(false)]
		public ushort GetUInt16()
		{
			ushort num;
			if (this.TryGetUInt16(out num))
			{
				return num;
			}
			throw new FormatException();
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00008A92 File Offset: 0x00006C92
		public bool TryGetInt32(out int value)
		{
			this.CheckValidInstance();
			return this._parent.TryGetValue(this._idx, out value);
		}

		// Token: 0x060002CD RID: 717 RVA: 0x00008AAC File Offset: 0x00006CAC
		public int GetInt32()
		{
			int num;
			if (!this.TryGetInt32(out num))
			{
				ThrowHelper.ThrowFormatException();
			}
			return num;
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00008AC9 File Offset: 0x00006CC9
		[CLSCompliant(false)]
		public bool TryGetUInt32(out uint value)
		{
			this.CheckValidInstance();
			return this._parent.TryGetValue(this._idx, out value);
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00008AE4 File Offset: 0x00006CE4
		[CLSCompliant(false)]
		public uint GetUInt32()
		{
			uint num;
			if (!this.TryGetUInt32(out num))
			{
				ThrowHelper.ThrowFormatException();
			}
			return num;
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x00008B01 File Offset: 0x00006D01
		public bool TryGetInt64(out long value)
		{
			this.CheckValidInstance();
			return this._parent.TryGetValue(this._idx, out value);
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00008B1C File Offset: 0x00006D1C
		public long GetInt64()
		{
			long num;
			if (!this.TryGetInt64(out num))
			{
				ThrowHelper.ThrowFormatException();
			}
			return num;
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00008B39 File Offset: 0x00006D39
		[CLSCompliant(false)]
		public bool TryGetUInt64(out ulong value)
		{
			this.CheckValidInstance();
			return this._parent.TryGetValue(this._idx, out value);
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x00008B54 File Offset: 0x00006D54
		[CLSCompliant(false)]
		public ulong GetUInt64()
		{
			ulong num;
			if (!this.TryGetUInt64(out num))
			{
				ThrowHelper.ThrowFormatException();
			}
			return num;
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x00008B71 File Offset: 0x00006D71
		public bool TryGetDouble(out double value)
		{
			this.CheckValidInstance();
			return this._parent.TryGetValue(this._idx, out value);
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x00008B8C File Offset: 0x00006D8C
		public double GetDouble()
		{
			double num;
			if (!this.TryGetDouble(out num))
			{
				ThrowHelper.ThrowFormatException();
			}
			return num;
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x00008BA9 File Offset: 0x00006DA9
		public bool TryGetSingle(out float value)
		{
			this.CheckValidInstance();
			return this._parent.TryGetValue(this._idx, out value);
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x00008BC4 File Offset: 0x00006DC4
		public float GetSingle()
		{
			float num;
			if (!this.TryGetSingle(out num))
			{
				ThrowHelper.ThrowFormatException();
			}
			return num;
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00008BE1 File Offset: 0x00006DE1
		public bool TryGetDecimal(out decimal value)
		{
			this.CheckValidInstance();
			return this._parent.TryGetValue(this._idx, out value);
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00008BFC File Offset: 0x00006DFC
		public decimal GetDecimal()
		{
			decimal num;
			if (!this.TryGetDecimal(out num))
			{
				ThrowHelper.ThrowFormatException();
			}
			return num;
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00008C19 File Offset: 0x00006E19
		public bool TryGetDateTime(out DateTime value)
		{
			this.CheckValidInstance();
			return this._parent.TryGetValue(this._idx, out value);
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00008C34 File Offset: 0x00006E34
		public DateTime GetDateTime()
		{
			DateTime dateTime;
			if (!this.TryGetDateTime(out dateTime))
			{
				ThrowHelper.ThrowFormatException();
			}
			return dateTime;
		}

		// Token: 0x060002DC RID: 732 RVA: 0x00008C51 File Offset: 0x00006E51
		public bool TryGetDateTimeOffset(out DateTimeOffset value)
		{
			this.CheckValidInstance();
			return this._parent.TryGetValue(this._idx, out value);
		}

		// Token: 0x060002DD RID: 733 RVA: 0x00008C6C File Offset: 0x00006E6C
		public DateTimeOffset GetDateTimeOffset()
		{
			DateTimeOffset dateTimeOffset;
			if (!this.TryGetDateTimeOffset(out dateTimeOffset))
			{
				ThrowHelper.ThrowFormatException();
			}
			return dateTimeOffset;
		}

		// Token: 0x060002DE RID: 734 RVA: 0x00008C89 File Offset: 0x00006E89
		public bool TryGetGuid(out Guid value)
		{
			this.CheckValidInstance();
			return this._parent.TryGetValue(this._idx, out value);
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00008CA4 File Offset: 0x00006EA4
		public Guid GetGuid()
		{
			Guid guid;
			if (!this.TryGetGuid(out guid))
			{
				ThrowHelper.ThrowFormatException();
			}
			return guid;
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x00008CC1 File Offset: 0x00006EC1
		internal string GetPropertyName()
		{
			this.CheckValidInstance();
			return this._parent.GetNameOfPropertyValue(this._idx);
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x00008CDA File Offset: 0x00006EDA
		[NullableContext(1)]
		public string GetRawText()
		{
			this.CheckValidInstance();
			return this._parent.GetRawValueAsString(this._idx);
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00008CF3 File Offset: 0x00006EF3
		internal ReadOnlyMemory<byte> GetRawValue()
		{
			this.CheckValidInstance();
			return this._parent.GetRawValue(this._idx, true);
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x00008D0D File Offset: 0x00006F0D
		internal string GetPropertyRawText()
		{
			this.CheckValidInstance();
			return this._parent.GetPropertyRawValueAsString(this._idx);
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x00008D26 File Offset: 0x00006F26
		[NullableContext(2)]
		public bool ValueEquals(string text)
		{
			if (this.TokenType == JsonTokenType.Null)
			{
				return text == null;
			}
			return this.TextEqualsHelper(text.AsSpan(), false);
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x00008D44 File Offset: 0x00006F44
		public bool ValueEquals(ReadOnlySpan<byte> utf8Text)
		{
			if (this.TokenType == JsonTokenType.Null)
			{
				return utf8Text == default(ReadOnlySpan<byte>);
			}
			return this.TextEqualsHelper(utf8Text, false, true);
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x00008D74 File Offset: 0x00006F74
		public bool ValueEquals(ReadOnlySpan<char> text)
		{
			if (this.TokenType == JsonTokenType.Null)
			{
				return text == default(ReadOnlySpan<char>);
			}
			return this.TextEqualsHelper(text, false);
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x00008DA3 File Offset: 0x00006FA3
		internal bool TextEqualsHelper(ReadOnlySpan<byte> utf8Text, bool isPropertyName, bool shouldUnescape)
		{
			this.CheckValidInstance();
			return this._parent.TextEquals(this._idx, utf8Text, isPropertyName, shouldUnescape);
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x00008DBF File Offset: 0x00006FBF
		internal bool TextEqualsHelper(ReadOnlySpan<char> text, bool isPropertyName)
		{
			this.CheckValidInstance();
			return this._parent.TextEquals(this._idx, text, isPropertyName);
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x00008DDA File Offset: 0x00006FDA
		[NullableContext(1)]
		public void WriteTo(Utf8JsonWriter writer)
		{
			if (writer == null)
			{
				ThrowHelper.ThrowArgumentNullException("writer");
			}
			this.CheckValidInstance();
			this._parent.WriteElementTo(this._idx, writer);
		}

		// Token: 0x060002EA RID: 746 RVA: 0x00008E04 File Offset: 0x00007004
		public JsonElement.ArrayEnumerator EnumerateArray()
		{
			this.CheckValidInstance();
			JsonTokenType tokenType = this.TokenType;
			if (tokenType != JsonTokenType.StartArray)
			{
				ThrowHelper.ThrowJsonElementWrongTypeException(JsonTokenType.StartArray, tokenType);
			}
			return new JsonElement.ArrayEnumerator(this);
		}

		// Token: 0x060002EB RID: 747 RVA: 0x00008E34 File Offset: 0x00007034
		public JsonElement.ObjectEnumerator EnumerateObject()
		{
			this.CheckValidInstance();
			JsonTokenType tokenType = this.TokenType;
			if (tokenType != JsonTokenType.StartObject)
			{
				ThrowHelper.ThrowJsonElementWrongTypeException(JsonTokenType.StartObject, tokenType);
			}
			return new JsonElement.ObjectEnumerator(this);
		}

		// Token: 0x060002EC RID: 748 RVA: 0x00008E64 File Offset: 0x00007064
		[NullableContext(1)]
		public override string ToString()
		{
			switch (this.TokenType)
			{
			case JsonTokenType.None:
			case JsonTokenType.Null:
				return string.Empty;
			case JsonTokenType.StartObject:
			case JsonTokenType.StartArray:
			case JsonTokenType.Number:
				return this._parent.GetRawValueAsString(this._idx);
			case JsonTokenType.String:
				return this.GetString();
			case JsonTokenType.True:
				return bool.TrueString;
			case JsonTokenType.False:
				return bool.FalseString;
			}
			return string.Empty;
		}

		// Token: 0x060002ED RID: 749 RVA: 0x00008EE0 File Offset: 0x000070E0
		public JsonElement Clone()
		{
			this.CheckValidInstance();
			if (!this._parent.IsDisposable)
			{
				return this;
			}
			return this._parent.CloneElement(this._idx);
		}

		// Token: 0x060002EE RID: 750 RVA: 0x00008F0D File Offset: 0x0000710D
		private void CheckValidInstance()
		{
			if (this._parent == null)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060002EF RID: 751 RVA: 0x00008F1D File Offset: 0x0000711D
		[Nullable(1)]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string DebuggerDisplay
		{
			get
			{
				return string.Format("ValueKind = {0} : \"{1}\"", this.ValueKind, this.ToString());
			}
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x00008F40 File Offset: 0x00007140
		public static JsonElement ParseValue(ref Utf8JsonReader reader)
		{
			JsonDocument jsonDocument;
			bool flag = JsonDocument.TryParseValue(ref reader, out jsonDocument, true, false);
			return jsonDocument.RootElement;
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00008F60 File Offset: 0x00007160
		internal static JsonElement ParseValue(Stream utf8Json, JsonDocumentOptions options)
		{
			JsonDocument jsonDocument = JsonDocument.ParseValue(utf8Json, options);
			return jsonDocument.RootElement;
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x00008F7C File Offset: 0x0000717C
		internal static JsonElement ParseValue(ReadOnlySpan<byte> utf8Json, JsonDocumentOptions options)
		{
			JsonDocument jsonDocument = JsonDocument.ParseValue(utf8Json, options);
			return jsonDocument.RootElement;
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x00008F98 File Offset: 0x00007198
		internal static JsonElement ParseValue(string json, JsonDocumentOptions options)
		{
			JsonDocument jsonDocument = JsonDocument.ParseValue(json, options);
			return jsonDocument.RootElement;
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x00008FB4 File Offset: 0x000071B4
		public static bool TryParseValue(ref Utf8JsonReader reader, [NotNullWhen(true)] out JsonElement? element)
		{
			JsonDocument jsonDocument;
			bool flag = JsonDocument.TryParseValue(ref reader, out jsonDocument, false, false);
			element = ((jsonDocument != null) ? new JsonElement?(jsonDocument.RootElement) : null);
			return flag;
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x00008FEC File Offset: 0x000071EC
		[CompilerGenerated]
		internal static bool <GetBoolean>g__ThrowJsonElementWrongTypeException|17_0(JsonTokenType actualType)
		{
			throw ThrowHelper.GetJsonElementWrongTypeException("Boolean", actualType.ToValueKind());
		}

		// Token: 0x0400012A RID: 298
		private readonly JsonDocument _parent;

		// Token: 0x0400012B RID: 299
		private readonly int _idx;

		// Token: 0x02000119 RID: 281
		[DebuggerDisplay("{Current,nq}")]
		public struct ArrayEnumerator : IEnumerable<JsonElement>, IEnumerable, IEnumerator<JsonElement>, IDisposable, IEnumerator
		{
			// Token: 0x06000D52 RID: 3410 RVA: 0x00033F76 File Offset: 0x00032176
			internal ArrayEnumerator(JsonElement target)
			{
				this._target = target;
				this._curIdx = -1;
				this._endIdxOrVersion = target._parent.GetEndIndex(this._target._idx, false);
			}

			// Token: 0x170002D8 RID: 728
			// (get) Token: 0x06000D53 RID: 3411 RVA: 0x00033FA4 File Offset: 0x000321A4
			public JsonElement Current
			{
				get
				{
					if (this._curIdx < 0)
					{
						return default(JsonElement);
					}
					return new JsonElement(this._target._parent, this._curIdx);
				}
			}

			// Token: 0x06000D54 RID: 3412 RVA: 0x00033FDC File Offset: 0x000321DC
			public JsonElement.ArrayEnumerator GetEnumerator()
			{
				JsonElement.ArrayEnumerator arrayEnumerator = this;
				arrayEnumerator._curIdx = -1;
				return arrayEnumerator;
			}

			// Token: 0x06000D55 RID: 3413 RVA: 0x00033FF9 File Offset: 0x000321F9
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06000D56 RID: 3414 RVA: 0x00034006 File Offset: 0x00032206
			IEnumerator<JsonElement> IEnumerable<JsonElement>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06000D57 RID: 3415 RVA: 0x00034013 File Offset: 0x00032213
			public void Dispose()
			{
				this._curIdx = this._endIdxOrVersion;
			}

			// Token: 0x06000D58 RID: 3416 RVA: 0x00034021 File Offset: 0x00032221
			public void Reset()
			{
				this._curIdx = -1;
			}

			// Token: 0x170002D9 RID: 729
			// (get) Token: 0x06000D59 RID: 3417 RVA: 0x0003402A File Offset: 0x0003222A
			[Nullable(1)]
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000D5A RID: 3418 RVA: 0x00034038 File Offset: 0x00032238
			public bool MoveNext()
			{
				if (this._curIdx >= this._endIdxOrVersion)
				{
					return false;
				}
				if (this._curIdx < 0)
				{
					this._curIdx = this._target._idx + 12;
				}
				else
				{
					this._curIdx = this._target._parent.GetEndIndex(this._curIdx, true);
				}
				return this._curIdx < this._endIdxOrVersion;
			}

			// Token: 0x04000469 RID: 1129
			private readonly JsonElement _target;

			// Token: 0x0400046A RID: 1130
			private int _curIdx;

			// Token: 0x0400046B RID: 1131
			private readonly int _endIdxOrVersion;
		}

		// Token: 0x0200011A RID: 282
		[DebuggerDisplay("{Current,nq}")]
		public struct ObjectEnumerator : IEnumerable<JsonProperty>, IEnumerable, IEnumerator<JsonProperty>, IDisposable, IEnumerator
		{
			// Token: 0x06000D5B RID: 3419 RVA: 0x0003409F File Offset: 0x0003229F
			internal ObjectEnumerator(JsonElement target)
			{
				this._target = target;
				this._curIdx = -1;
				this._endIdxOrVersion = target._parent.GetEndIndex(this._target._idx, false);
			}

			// Token: 0x170002DA RID: 730
			// (get) Token: 0x06000D5C RID: 3420 RVA: 0x000340CC File Offset: 0x000322CC
			public JsonProperty Current
			{
				get
				{
					if (this._curIdx < 0)
					{
						return default(JsonProperty);
					}
					return new JsonProperty(new JsonElement(this._target._parent, this._curIdx), null);
				}
			}

			// Token: 0x06000D5D RID: 3421 RVA: 0x00034108 File Offset: 0x00032308
			public JsonElement.ObjectEnumerator GetEnumerator()
			{
				JsonElement.ObjectEnumerator objectEnumerator = this;
				objectEnumerator._curIdx = -1;
				return objectEnumerator;
			}

			// Token: 0x06000D5E RID: 3422 RVA: 0x00034125 File Offset: 0x00032325
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06000D5F RID: 3423 RVA: 0x00034132 File Offset: 0x00032332
			IEnumerator<JsonProperty> IEnumerable<JsonProperty>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06000D60 RID: 3424 RVA: 0x0003413F File Offset: 0x0003233F
			public void Dispose()
			{
				this._curIdx = this._endIdxOrVersion;
			}

			// Token: 0x06000D61 RID: 3425 RVA: 0x0003414D File Offset: 0x0003234D
			public void Reset()
			{
				this._curIdx = -1;
			}

			// Token: 0x170002DB RID: 731
			// (get) Token: 0x06000D62 RID: 3426 RVA: 0x00034156 File Offset: 0x00032356
			[Nullable(1)]
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000D63 RID: 3427 RVA: 0x00034164 File Offset: 0x00032364
			public bool MoveNext()
			{
				if (this._curIdx >= this._endIdxOrVersion)
				{
					return false;
				}
				if (this._curIdx < 0)
				{
					this._curIdx = this._target._idx + 12;
				}
				else
				{
					this._curIdx = this._target._parent.GetEndIndex(this._curIdx, true);
				}
				this._curIdx += 12;
				return this._curIdx < this._endIdxOrVersion;
			}

			// Token: 0x0400046C RID: 1132
			private readonly JsonElement _target;

			// Token: 0x0400046D RID: 1133
			private int _curIdx;

			// Token: 0x0400046E RID: 1134
			private readonly int _endIdxOrVersion;
		}
	}
}
