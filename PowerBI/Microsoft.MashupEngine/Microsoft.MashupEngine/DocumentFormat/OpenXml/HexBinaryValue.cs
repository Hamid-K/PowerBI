using System;
using System.Diagnostics;

namespace DocumentFormat.OpenXml
{
	// Token: 0x02002139 RID: 8505
	[DebuggerDisplay("{InnerText}")]
	public class HexBinaryValue : OpenXmlSimpleType
	{
		// Token: 0x0600D315 RID: 54037 RVA: 0x0029D744 File Offset: 0x0029B944
		public HexBinaryValue()
		{
		}

		// Token: 0x0600D316 RID: 54038 RVA: 0x0029E7E8 File Offset: 0x0029C9E8
		public HexBinaryValue(string hexBinary)
		{
			base.TextValue = hexBinary;
		}

		// Token: 0x0600D317 RID: 54039 RVA: 0x0029E7F7 File Offset: 0x0029C9F7
		public HexBinaryValue(HexBinaryValue source)
			: base(source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
		}

		// Token: 0x170032FE RID: 13054
		// (get) Token: 0x0600D318 RID: 54040 RVA: 0x0029E80E File Offset: 0x0029CA0E
		// (set) Token: 0x0600D319 RID: 54041 RVA: 0x0029E816 File Offset: 0x0029CA16
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

		// Token: 0x0600D31A RID: 54042 RVA: 0x0029E85A File Offset: 0x0029CA5A
		public static implicit operator string(HexBinaryValue xmlAttribute)
		{
			if (xmlAttribute == null)
			{
				return null;
			}
			return HexBinaryValue.ToString(xmlAttribute);
		}

		// Token: 0x0600D31B RID: 54043 RVA: 0x0029E867 File Offset: 0x0029CA67
		public static implicit operator HexBinaryValue(string value)
		{
			return HexBinaryValue.FromString(value);
		}

		// Token: 0x0600D31C RID: 54044 RVA: 0x0029E86F File Offset: 0x0029CA6F
		public static HexBinaryValue FromString(string value)
		{
			return new HexBinaryValue(value);
		}

		// Token: 0x0600D31D RID: 54045 RVA: 0x0029E877 File Offset: 0x0029CA77
		public static string ToString(HexBinaryValue xmlAttribute)
		{
			if (xmlAttribute == null)
			{
				throw new InvalidOperationException(ExceptionMessages.ImplicitConversionExceptionOnNull);
			}
			return xmlAttribute.Value;
		}

		// Token: 0x0600D31E RID: 54046 RVA: 0x0029E88D File Offset: 0x0029CA8D
		internal override OpenXmlSimpleType CloneImp()
		{
			return new HexBinaryValue(this);
		}
	}
}
