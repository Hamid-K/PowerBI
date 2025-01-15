using System;
using System.Diagnostics;
using System.Xml;
using DocumentFormat.OpenXml.Validation;

namespace DocumentFormat.OpenXml.Internal.SchemaValidation
{
	// Token: 0x02003143 RID: 12611
	[Serializable]
	internal class NcNameRestriction : StringRestriction
	{
		// Token: 0x1700999A RID: 39322
		// (get) Token: 0x0601B57E RID: 111998 RVA: 0x002C8749 File Offset: 0x002C6949
		// (set) Token: 0x0601B57F RID: 111999 RVA: 0x0000336E File Offset: 0x0000156E
		public override XsdType XsdType
		{
			get
			{
				return XsdType.NCName;
			}
			set
			{
			}
		}

		// Token: 0x1700999B RID: 39323
		// (get) Token: 0x0601B580 RID: 112000 RVA: 0x00376778 File Offset: 0x00374978
		public override string ClrTypeName
		{
			get
			{
				return NcNameRestriction.clrTypeName;
			}
		}

		// Token: 0x0601B581 RID: 112001 RVA: 0x00376780 File Offset: 0x00374980
		public override bool ValidateValueType(OpenXmlSimpleType attributeValue)
		{
			if (string.IsNullOrEmpty(attributeValue.InnerText))
			{
				return false;
			}
			try
			{
				XmlConvert.VerifyNCName(attributeValue.InnerText);
			}
			catch (XmlException)
			{
				return false;
			}
			return true;
		}

		// Token: 0x0400B538 RID: 46392
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[NonSerialized]
		private static readonly string clrTypeName = ValidationResources.TypeName_NCName;
	}
}
