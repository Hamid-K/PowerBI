using System;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;

namespace Microsoft.IdentityModel.Json.Serialization
{
	// Token: 0x020000A5 RID: 165
	[NullableContext(1)]
	[Nullable(0)]
	internal class TraceJsonReader : JsonReader, IJsonLineInfo
	{
		// Token: 0x0600084A RID: 2122 RVA: 0x00024344 File Offset: 0x00022544
		public TraceJsonReader(JsonReader innerReader)
		{
			this._innerReader = innerReader;
			this._sw = new StringWriter(CultureInfo.InvariantCulture);
			this._sw.Write("Deserialized JSON: " + Environment.NewLine);
			this._textWriter = new JsonTextWriter(this._sw);
			this._textWriter.Formatting = Formatting.Indented;
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x000243A5 File Offset: 0x000225A5
		public string GetDeserializedJsonMessage()
		{
			return this._sw.ToString();
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x000243B2 File Offset: 0x000225B2
		public override bool Read()
		{
			bool flag = this._innerReader.Read();
			this.WriteCurrentToken();
			return flag;
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x000243C5 File Offset: 0x000225C5
		public override int? ReadAsInt32()
		{
			int? num = this._innerReader.ReadAsInt32();
			this.WriteCurrentToken();
			return num;
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x000243D8 File Offset: 0x000225D8
		[NullableContext(2)]
		public override string ReadAsString()
		{
			string text = this._innerReader.ReadAsString();
			this.WriteCurrentToken();
			return text;
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x000243EB File Offset: 0x000225EB
		[NullableContext(2)]
		public override byte[] ReadAsBytes()
		{
			byte[] array = this._innerReader.ReadAsBytes();
			this.WriteCurrentToken();
			return array;
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x000243FE File Offset: 0x000225FE
		public override decimal? ReadAsDecimal()
		{
			decimal? num = this._innerReader.ReadAsDecimal();
			this.WriteCurrentToken();
			return num;
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x00024411 File Offset: 0x00022611
		public override double? ReadAsDouble()
		{
			double? num = this._innerReader.ReadAsDouble();
			this.WriteCurrentToken();
			return num;
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x00024424 File Offset: 0x00022624
		public override bool? ReadAsBoolean()
		{
			bool? flag = this._innerReader.ReadAsBoolean();
			this.WriteCurrentToken();
			return flag;
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x00024437 File Offset: 0x00022637
		public override DateTime? ReadAsDateTime()
		{
			DateTime? dateTime = this._innerReader.ReadAsDateTime();
			this.WriteCurrentToken();
			return dateTime;
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x0002444A File Offset: 0x0002264A
		public override DateTimeOffset? ReadAsDateTimeOffset()
		{
			DateTimeOffset? dateTimeOffset = this._innerReader.ReadAsDateTimeOffset();
			this.WriteCurrentToken();
			return dateTimeOffset;
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x0002445D File Offset: 0x0002265D
		public void WriteCurrentToken()
		{
			this._textWriter.WriteToken(this._innerReader, false, false, true);
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000856 RID: 2134 RVA: 0x00024473 File Offset: 0x00022673
		public override int Depth
		{
			get
			{
				return this._innerReader.Depth;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000857 RID: 2135 RVA: 0x00024480 File Offset: 0x00022680
		public override string Path
		{
			get
			{
				return this._innerReader.Path;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000858 RID: 2136 RVA: 0x0002448D File Offset: 0x0002268D
		// (set) Token: 0x06000859 RID: 2137 RVA: 0x0002449A File Offset: 0x0002269A
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
		// (get) Token: 0x0600085A RID: 2138 RVA: 0x000244A8 File Offset: 0x000226A8
		public override JsonToken TokenType
		{
			get
			{
				return this._innerReader.TokenType;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x0600085B RID: 2139 RVA: 0x000244B5 File Offset: 0x000226B5
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
		// (get) Token: 0x0600085C RID: 2140 RVA: 0x000244C2 File Offset: 0x000226C2
		[Nullable(2)]
		public override Type ValueType
		{
			[NullableContext(2)]
			get
			{
				return this._innerReader.ValueType;
			}
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x000244CF File Offset: 0x000226CF
		public override void Close()
		{
			this._innerReader.Close();
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x000244DC File Offset: 0x000226DC
		bool IJsonLineInfo.HasLineInfo()
		{
			IJsonLineInfo jsonLineInfo = this._innerReader as IJsonLineInfo;
			return jsonLineInfo != null && jsonLineInfo.HasLineInfo();
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x0600085F RID: 2143 RVA: 0x00024500 File Offset: 0x00022700
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
		// (get) Token: 0x06000860 RID: 2144 RVA: 0x00024524 File Offset: 0x00022724
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

		// Token: 0x040002E7 RID: 743
		private readonly JsonReader _innerReader;

		// Token: 0x040002E8 RID: 744
		private readonly JsonTextWriter _textWriter;

		// Token: 0x040002E9 RID: 745
		private readonly StringWriter _sw;
	}
}
