using System;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;

namespace Microsoft.Identity.Json.Serialization
{
	// Token: 0x020000A4 RID: 164
	internal class TraceJsonReader : JsonReader, IJsonLineInfo
	{
		// Token: 0x06000840 RID: 2112 RVA: 0x00023CE0 File Offset: 0x00021EE0
		public TraceJsonReader(JsonReader innerReader)
		{
			this._innerReader = innerReader;
			this._sw = new StringWriter(CultureInfo.InvariantCulture);
			this._sw.Write("Deserialized JSON: " + Environment.NewLine);
			this._textWriter = new JsonTextWriter(this._sw);
			this._textWriter.Formatting = Formatting.Indented;
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x00023D41 File Offset: 0x00021F41
		public string GetDeserializedJsonMessage()
		{
			return this._sw.ToString();
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x00023D4E File Offset: 0x00021F4E
		public override bool Read()
		{
			bool flag = this._innerReader.Read();
			this.WriteCurrentToken();
			return flag;
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x00023D61 File Offset: 0x00021F61
		public override int? ReadAsInt32()
		{
			int? num = this._innerReader.ReadAsInt32();
			this.WriteCurrentToken();
			return num;
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x00023D74 File Offset: 0x00021F74
		[NullableContext(2)]
		public override string ReadAsString()
		{
			string text = this._innerReader.ReadAsString();
			this.WriteCurrentToken();
			return text;
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x00023D87 File Offset: 0x00021F87
		[NullableContext(2)]
		public override byte[] ReadAsBytes()
		{
			byte[] array = this._innerReader.ReadAsBytes();
			this.WriteCurrentToken();
			return array;
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x00023D9A File Offset: 0x00021F9A
		public override decimal? ReadAsDecimal()
		{
			decimal? num = this._innerReader.ReadAsDecimal();
			this.WriteCurrentToken();
			return num;
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x00023DAD File Offset: 0x00021FAD
		public override double? ReadAsDouble()
		{
			double? num = this._innerReader.ReadAsDouble();
			this.WriteCurrentToken();
			return num;
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x00023DC0 File Offset: 0x00021FC0
		public override bool? ReadAsBoolean()
		{
			bool? flag = this._innerReader.ReadAsBoolean();
			this.WriteCurrentToken();
			return flag;
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x00023DD3 File Offset: 0x00021FD3
		public override DateTime? ReadAsDateTime()
		{
			DateTime? dateTime = this._innerReader.ReadAsDateTime();
			this.WriteCurrentToken();
			return dateTime;
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x00023DE6 File Offset: 0x00021FE6
		public override DateTimeOffset? ReadAsDateTimeOffset()
		{
			DateTimeOffset? dateTimeOffset = this._innerReader.ReadAsDateTimeOffset();
			this.WriteCurrentToken();
			return dateTimeOffset;
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x00023DF9 File Offset: 0x00021FF9
		public void WriteCurrentToken()
		{
			this._textWriter.WriteToken(this._innerReader, false, false, true);
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x0600084C RID: 2124 RVA: 0x00023E0F File Offset: 0x0002200F
		public override int Depth
		{
			get
			{
				return this._innerReader.Depth;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x0600084D RID: 2125 RVA: 0x00023E1C File Offset: 0x0002201C
		public override string Path
		{
			get
			{
				return this._innerReader.Path;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x0600084E RID: 2126 RVA: 0x00023E29 File Offset: 0x00022029
		// (set) Token: 0x0600084F RID: 2127 RVA: 0x00023E36 File Offset: 0x00022036
		public override char QuoteChar
		{
			get
			{
				return this._innerReader.QuoteChar;
			}
			protected internal set
			{
				this._innerReader.QuoteChar = value;
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000850 RID: 2128 RVA: 0x00023E44 File Offset: 0x00022044
		public override JsonToken TokenType
		{
			get
			{
				return this._innerReader.TokenType;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000851 RID: 2129 RVA: 0x00023E51 File Offset: 0x00022051
		[Nullable(2)]
		public override object Value
		{
			[NullableContext(2)]
			get
			{
				return this._innerReader.Value;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000852 RID: 2130 RVA: 0x00023E5E File Offset: 0x0002205E
		[Nullable(2)]
		public override Type ValueType
		{
			[NullableContext(2)]
			get
			{
				return this._innerReader.ValueType;
			}
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x00023E6B File Offset: 0x0002206B
		public override void Close()
		{
			this._innerReader.Close();
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x00023E78 File Offset: 0x00022078
		bool IJsonLineInfo.HasLineInfo()
		{
			IJsonLineInfo jsonLineInfo = this._innerReader as IJsonLineInfo;
			return jsonLineInfo != null && jsonLineInfo.HasLineInfo();
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000855 RID: 2133 RVA: 0x00023E9C File Offset: 0x0002209C
		int IJsonLineInfo.LineNumber
		{
			get
			{
				IJsonLineInfo jsonLineInfo = this._innerReader as IJsonLineInfo;
				if (jsonLineInfo == null)
				{
					return 0;
				}
				return jsonLineInfo.LineNumber;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000856 RID: 2134 RVA: 0x00023EC0 File Offset: 0x000220C0
		int IJsonLineInfo.LinePosition
		{
			get
			{
				IJsonLineInfo jsonLineInfo = this._innerReader as IJsonLineInfo;
				if (jsonLineInfo == null)
				{
					return 0;
				}
				return jsonLineInfo.LinePosition;
			}
		}

		// Token: 0x040002CC RID: 716
		private readonly JsonReader _innerReader;

		// Token: 0x040002CD RID: 717
		private readonly JsonTextWriter _textWriter;

		// Token: 0x040002CE RID: 718
		private readonly StringWriter _sw;
	}
}
