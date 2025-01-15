using System;
using System.Diagnostics;
using System.Xml;
using DocumentFormat.OpenXml.Validation;

namespace DocumentFormat.OpenXml.Internal.SchemaValidation
{
	// Token: 0x02003144 RID: 12612
	[Serializable]
	internal class IdStringRestriction : StringRestriction
	{
		// Token: 0x1700999C RID: 39324
		// (get) Token: 0x0601B584 RID: 112004 RVA: 0x002D0E0F File Offset: 0x002CF00F
		// (set) Token: 0x0601B585 RID: 112005 RVA: 0x0000336E File Offset: 0x0000156E
		public override XsdType XsdType
		{
			get
			{
				return XsdType.ID;
			}
			set
			{
			}
		}

		// Token: 0x1700999D RID: 39325
		// (get) Token: 0x0601B586 RID: 112006 RVA: 0x003767D0 File Offset: 0x003749D0
		public override string ClrTypeName
		{
			get
			{
				return IdStringRestriction.clrTypeName;
			}
		}

		// Token: 0x0601B587 RID: 112007 RVA: 0x003767D8 File Offset: 0x003749D8
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

		// Token: 0x0400B539 RID: 46393
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[NonSerialized]
		private static readonly string clrTypeName = ValidationResources.TypeName_ID;
	}
}
