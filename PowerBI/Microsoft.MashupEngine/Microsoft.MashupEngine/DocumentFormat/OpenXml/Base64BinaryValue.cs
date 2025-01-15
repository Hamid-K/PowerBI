using System;
using System.Diagnostics;

namespace DocumentFormat.OpenXml
{
	// Token: 0x0200213A RID: 8506
	[DebuggerDisplay("{InnerText}")]
	internal class Base64BinaryValue : OpenXmlSimpleType
	{
		// Token: 0x0600D31F RID: 54047 RVA: 0x0029D744 File Offset: 0x0029B944
		public Base64BinaryValue()
		{
		}

		// Token: 0x0600D320 RID: 54048 RVA: 0x0029E7E8 File Offset: 0x0029C9E8
		public Base64BinaryValue(string base64Binary)
		{
			base.TextValue = base64Binary;
		}

		// Token: 0x0600D321 RID: 54049 RVA: 0x0029E7F7 File Offset: 0x0029C9F7
		public Base64BinaryValue(Base64BinaryValue source)
			: base(source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
		}

		// Token: 0x170032FF RID: 13055
		// (get) Token: 0x0600D322 RID: 54050 RVA: 0x0029E80E File Offset: 0x0029CA0E
		// (set) Token: 0x0600D323 RID: 54051 RVA: 0x0029E816 File Offset: 0x0029CA16
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

		// Token: 0x0600D324 RID: 54052 RVA: 0x0029E895 File Offset: 0x0029CA95
		public static implicit operator string(Base64BinaryValue xmlAttribute)
		{
			if (xmlAttribute == null)
			{
				return null;
			}
			return Base64BinaryValue.ToString(xmlAttribute);
		}

		// Token: 0x0600D325 RID: 54053 RVA: 0x0029E8A2 File Offset: 0x0029CAA2
		public static implicit operator Base64BinaryValue(string value)
		{
			return Base64BinaryValue.FromString(value);
		}

		// Token: 0x0600D326 RID: 54054 RVA: 0x0029E8AA File Offset: 0x0029CAAA
		public static Base64BinaryValue FromString(string value)
		{
			return new Base64BinaryValue(value);
		}

		// Token: 0x0600D327 RID: 54055 RVA: 0x0029E8B2 File Offset: 0x0029CAB2
		public static string ToString(Base64BinaryValue xmlAttribute)
		{
			if (xmlAttribute == null)
			{
				throw new InvalidOperationException(ExceptionMessages.ImplicitConversionExceptionOnNull);
			}
			return xmlAttribute.Value;
		}

		// Token: 0x0600D328 RID: 54056 RVA: 0x0029E8C8 File Offset: 0x0029CAC8
		internal override OpenXmlSimpleType CloneImp()
		{
			return new Base64BinaryValue(this);
		}
	}
}
