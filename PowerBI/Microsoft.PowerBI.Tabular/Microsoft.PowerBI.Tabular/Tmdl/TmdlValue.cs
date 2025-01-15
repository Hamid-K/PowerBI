using System;
using System.Globalization;
using Microsoft.AnalysisServices.Tabular.Extensions;

namespace Microsoft.AnalysisServices.Tabular.Tmdl
{
	// Token: 0x02000152 RID: 338
	internal abstract class TmdlValue : IFormattable
	{
		// Token: 0x0600158A RID: 5514 RVA: 0x0009075A File Offset: 0x0008E95A
		private protected TmdlValue(TmdlValueType type, string rawValue, bool hasBody, bool indentBody = true)
			: this(type, rawValue, rawValue, hasBody, indentBody)
		{
		}

		// Token: 0x0600158B RID: 5515 RVA: 0x00090768 File Offset: 0x0008E968
		private protected TmdlValue(TmdlValueType type, string rawValue, object value, bool hasBody, bool indentBody = true)
		{
			this.indentBody = indentBody;
			this.Type = type;
			this.RawValue = rawValue;
			this.Value = value;
			this.HasBody = hasBody;
		}

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x0600158C RID: 5516 RVA: 0x00090795 File Offset: 0x0008E995
		public TmdlValueType Type { get; }

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x0600158D RID: 5517 RVA: 0x0009079D File Offset: 0x0008E99D
		public string RawValue { get; }

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x0600158E RID: 5518 RVA: 0x000907A5 File Offset: 0x0008E9A5
		public virtual object Value { get; }

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x0600158F RID: 5519 RVA: 0x000907AD File Offset: 0x0008E9AD
		public virtual bool IsNull
		{
			get
			{
				return this.Value == null;
			}
		}

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x06001590 RID: 5520 RVA: 0x000907B8 File Offset: 0x0008E9B8
		internal bool HasBody { get; }

		// Token: 0x06001591 RID: 5521 RVA: 0x000907C0 File Offset: 0x0008E9C0
		public static TmdlScalarValue<T> ParseScalar<T>(string rawValue) where T : struct
		{
			if (string.IsNullOrWhiteSpace(rawValue))
			{
				return new TmdlScalarValue<T>(rawValue, null);
			}
			TmdlScalarValue<T> tmdlScalarValue;
			try
			{
				TypeCode typeCode = global::System.Type.GetTypeCode(typeof(T));
				object obj;
				if (typeCode <= TypeCode.Int32)
				{
					if (typeCode == TypeCode.Boolean)
					{
						obj = bool.Parse(rawValue);
						goto IL_00C4;
					}
					if (typeCode == TypeCode.Int32)
					{
						obj = int.Parse(rawValue, CultureInfo.InvariantCulture);
						goto IL_00C4;
					}
				}
				else
				{
					if (typeCode == TypeCode.Int64)
					{
						obj = long.Parse(rawValue, CultureInfo.InvariantCulture);
						goto IL_00C4;
					}
					if (typeCode == TypeCode.Double)
					{
						obj = double.Parse(rawValue, CultureInfo.InvariantCulture);
						goto IL_00C4;
					}
					if (typeCode == TypeCode.DateTime)
					{
						obj = DateTime.ParseExact(rawValue, "yyyy-MM-dd", CultureInfo.InvariantCulture);
						goto IL_00C4;
					}
				}
				throw new TmdlParserException(ParsingError.UnsupportedScalarType, TomSR.Exception_TmdlParserInvalidScalarType(typeof(T).Name));
				IL_00C4:
				tmdlScalarValue = new TmdlScalarValue<T>(rawValue, new T?((T)((object)obj)));
			}
			catch (FormatException ex)
			{
				throw new TmdlParserException(ParsingError.InvalidValueFormat, TomSR.Exception_TmdlParserConvertValue(rawValue, typeof(T).Name), ex);
			}
			return tmdlScalarValue;
		}

		// Token: 0x06001592 RID: 5522 RVA: 0x000908D8 File Offset: 0x0008EAD8
		public static TmdlEnumValue ParseEnum(Type enumType, string rawValue)
		{
			TmdlEnumValue tmdlEnumValue;
			try
			{
				object obj = Enum.Parse(enumType, rawValue, true);
				tmdlEnumValue = new TmdlEnumValue(rawValue, (Enum)obj);
			}
			catch (ArgumentException ex)
			{
				throw new TmdlParserException(ParsingError.InvalidValueFormat, TomSR.Exception_TmdlParserConvertValue(rawValue, enumType.Name), ex);
			}
			return tmdlEnumValue;
		}

		// Token: 0x06001593 RID: 5523 RVA: 0x00090924 File Offset: 0x0008EB24
		public static TmdlValue FromString(string rawValue)
		{
			if (rawValue.HasLineSeparators())
			{
				throw new TmdlParserException(ParsingError.InvalidValueFormat, TomSR.Exception_TmdlParserMultiStringLine);
			}
			string text = rawValue.Trim();
			int length = text.Length;
			if (length != 0)
			{
				if (length != 1)
				{
					if (text[0] == '"' && text[text.Length - 1] == '"')
					{
						try
						{
							text = text.UnescapeString('"', 1, text.Length - 2);
						}
						catch (ArgumentException ex)
						{
							throw new TmdlParserException(ParsingError.InvalidValueFormat, ex.Message, ex);
						}
					}
				}
				else if (text[0] == '"')
				{
					throw new TmdlParserException(ParsingError.InvalidValueFormat, TomSR.Exception_TmdlParserSingleCharQuoteString);
				}
				return new TmdlStringValue(text, rawValue, false);
			}
			return new TmdlStringValue(string.Empty, "\"\"", false);
		}

		// Token: 0x06001594 RID: 5524 RVA: 0x000909E0 File Offset: 0x0008EBE0
		public static TmdlValue FromExpression(string rawValue)
		{
			return new TmdlStringValue(rawValue.Trim(), rawValue, true);
		}

		// Token: 0x06001595 RID: 5525 RVA: 0x000909EF File Offset: 0x0008EBEF
		public static TmdlValue FromScalar<T>(T value) where T : struct
		{
			return new TmdlScalarValue<T>(Convert.ToString(value, CultureInfo.InvariantCulture), new T?(value));
		}

		// Token: 0x06001596 RID: 5526 RVA: 0x00090A0C File Offset: 0x0008EC0C
		public static TmdlValue FromEnum<T>(T value) where T : Enum
		{
			return new TmdlEnumValue(value.ToString("G"), value);
		}

		// Token: 0x06001597 RID: 5527 RVA: 0x00090A2C File Offset: 0x0008EC2C
		public void WriteTo(ITmdlWriter writer, int? bodyIndentCount = null)
		{
			if (this.HasBody)
			{
				writer.WriteLine();
				if (this.indentBody)
				{
					using (writer.Indent(bodyIndentCount.Value))
					{
						this.WriteBody(writer);
						return;
					}
				}
				this.WriteBody(writer);
				return;
			}
			this.WriteValue(writer);
			writer.WriteLine();
		}

		// Token: 0x06001598 RID: 5528 RVA: 0x00090A98 File Offset: 0x0008EC98
		public virtual TypeCode GetTypeCode()
		{
			switch (this.Type)
			{
			case TmdlValueType.String:
			case TmdlValueType.ModelReference:
				return TypeCode.String;
			case TmdlValueType.Scalar:
				return global::System.Type.GetTypeCode(this.Value.GetType());
			case TmdlValueType.Struct:
			case TmdlValueType.MetadataObject:
				return TypeCode.Object;
			}
			throw new NotSupportedException(this.Type.ToString());
		}

		// Token: 0x06001599 RID: 5529 RVA: 0x00090AFC File Offset: 0x0008ECFC
		public override string ToString()
		{
			Enum @enum = this.Value as Enum;
			if (@enum != null)
			{
				return @enum.ToString("G");
			}
			object obj = this.Value;
			if (obj is bool)
			{
				return ((bool)obj).ToString().ToLowerInvariant();
			}
			obj = this.Value;
			if (obj is DateTime)
			{
				return ((DateTime)obj).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
			}
			string text = this.Value as string;
			if (text != null)
			{
				TmdlStringValue tmdlStringValue = this as TmdlStringValue;
				if (tmdlStringValue != null && tmdlStringValue.UseExpressionSemantics)
				{
					return text;
				}
				if (text.Length == 0)
				{
					return "\"\"";
				}
				if (char.IsWhiteSpace(text, 0) || char.IsWhiteSpace(text, text.Length - 1) || (text[0] == '"' && text[text.Length - 1] == '"'))
				{
					return string.Format("\"{0}\"", text.EscapeString('"'));
				}
			}
			object value = this.Value;
			return ((value != null) ? value.ToString() : null) ?? string.Empty;
		}

		// Token: 0x0600159A RID: 5530 RVA: 0x00090C10 File Offset: 0x0008EE10
		public string ToString(string format, IFormatProvider formatProvider)
		{
			IFormattable formattable = this.Value as IFormattable;
			if (formattable != null)
			{
				return formattable.ToString(format, formatProvider);
			}
			return this.ToString();
		}

		// Token: 0x0600159B RID: 5531 RVA: 0x00090C3B File Offset: 0x0008EE3B
		private protected virtual void WriteValue(ITmdlWriter writer)
		{
			if (this.Value != null)
			{
				writer.Write(this.Value.ToString(), Array.Empty<object>());
			}
		}

		// Token: 0x0600159C RID: 5532 RVA: 0x00090C5B File Offset: 0x0008EE5B
		private protected virtual void WriteBody(ITmdlWriter writer)
		{
		}

		// Token: 0x040003CB RID: 971
		private readonly bool indentBody;
	}
}
