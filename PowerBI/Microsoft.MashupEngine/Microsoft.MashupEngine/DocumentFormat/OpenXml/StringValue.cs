using System;
using System.Diagnostics;

namespace DocumentFormat.OpenXml
{
	// Token: 0x02002138 RID: 8504
	[DebuggerDisplay("{InnerText}")]
	public class StringValue : OpenXmlSimpleType
	{
		// Token: 0x0600D30B RID: 54027 RVA: 0x0029D744 File Offset: 0x0029B944
		public StringValue()
		{
		}

		// Token: 0x0600D30C RID: 54028 RVA: 0x0029E7E8 File Offset: 0x0029C9E8
		public StringValue(string value)
		{
			base.TextValue = value;
		}

		// Token: 0x0600D30D RID: 54029 RVA: 0x0029E7F7 File Offset: 0x0029C9F7
		public StringValue(StringValue source)
			: base(source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
		}

		// Token: 0x170032FD RID: 13053
		// (get) Token: 0x0600D30E RID: 54030 RVA: 0x0029E80E File Offset: 0x0029CA0E
		// (set) Token: 0x0600D30F RID: 54031 RVA: 0x0029E816 File Offset: 0x0029CA16
		public string Value
		{
			get
			{
				return base.TextValue;
			}
			set
			{
				base.TextValue = value;
			}
		}

		// Token: 0x0600D310 RID: 54032 RVA: 0x0029E81F File Offset: 0x0029CA1F
		public static implicit operator string(StringValue xmlAttribute)
		{
			if (xmlAttribute == null)
			{
				return null;
			}
			return StringValue.ToString(xmlAttribute);
		}

		// Token: 0x0600D311 RID: 54033 RVA: 0x0029E82C File Offset: 0x0029CA2C
		public static implicit operator StringValue(string value)
		{
			return StringValue.FromString(value);
		}

		// Token: 0x0600D312 RID: 54034 RVA: 0x0029E834 File Offset: 0x0029CA34
		public static StringValue FromString(string value)
		{
			return new StringValue(value);
		}

		// Token: 0x0600D313 RID: 54035 RVA: 0x0029E83C File Offset: 0x0029CA3C
		public static string ToString(StringValue xmlAttribute)
		{
			if (xmlAttribute == null)
			{
				throw new InvalidOperationException(ExceptionMessages.ImplicitConversionExceptionOnNull);
			}
			return xmlAttribute.Value;
		}

		// Token: 0x0600D314 RID: 54036 RVA: 0x0029E852 File Offset: 0x0029CA52
		internal override OpenXmlSimpleType CloneImp()
		{
			return new StringValue(this);
		}
	}
}
