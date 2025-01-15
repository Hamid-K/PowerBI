using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Text.Json
{
	// Token: 0x0200003C RID: 60
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("{DebuggerDisplay,nq}")]
	public readonly struct JsonProperty
	{
		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x00008FFE File Offset: 0x000071FE
		public JsonElement Value { get; }

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x00009006 File Offset: 0x00007206
		[Nullable(2)]
		private string _name { get; }

		// Token: 0x060002F8 RID: 760 RVA: 0x0000900E File Offset: 0x0000720E
		internal JsonProperty(JsonElement value, string name = null)
		{
			this.Value = value;
			this._name = name;
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x00009020 File Offset: 0x00007220
		public string Name
		{
			get
			{
				return this._name ?? this.Value.GetPropertyName();
			}
		}

		// Token: 0x060002FA RID: 762 RVA: 0x00009045 File Offset: 0x00007245
		[NullableContext(2)]
		public bool NameEquals(string text)
		{
			return this.NameEquals(text.AsSpan());
		}

		// Token: 0x060002FB RID: 763 RVA: 0x00009054 File Offset: 0x00007254
		[NullableContext(0)]
		public bool NameEquals(ReadOnlySpan<byte> utf8Text)
		{
			return this.Value.TextEqualsHelper(utf8Text, true, true);
		}

		// Token: 0x060002FC RID: 764 RVA: 0x00009074 File Offset: 0x00007274
		[NullableContext(0)]
		public bool NameEquals(ReadOnlySpan<char> text)
		{
			return this.Value.TextEqualsHelper(text, true);
		}

		// Token: 0x060002FD RID: 765 RVA: 0x00009094 File Offset: 0x00007294
		internal bool EscapedNameEquals(ReadOnlySpan<byte> utf8Text)
		{
			return this.Value.TextEqualsHelper(utf8Text, true, false);
		}

		// Token: 0x060002FE RID: 766 RVA: 0x000090B4 File Offset: 0x000072B4
		public void WriteTo(Utf8JsonWriter writer)
		{
			if (writer == null)
			{
				ThrowHelper.ThrowArgumentNullException("writer");
			}
			writer.WritePropertyName(this.Name);
			this.Value.WriteTo(writer);
		}

		// Token: 0x060002FF RID: 767 RVA: 0x000090EC File Offset: 0x000072EC
		public override string ToString()
		{
			return this.Value.GetPropertyRawText();
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000300 RID: 768 RVA: 0x00009108 File Offset: 0x00007308
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string DebuggerDisplay
		{
			get
			{
				if (this.Value.ValueKind != JsonValueKind.Undefined)
				{
					return "\"" + this.ToString() + "\"";
				}
				return "<Undefined>";
			}
		}
	}
}
