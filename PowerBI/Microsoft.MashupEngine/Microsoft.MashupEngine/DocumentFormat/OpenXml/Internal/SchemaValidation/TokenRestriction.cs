using System;
using System.Diagnostics;
using System.Xml;
using DocumentFormat.OpenXml.Validation;

namespace DocumentFormat.OpenXml.Internal.SchemaValidation
{
	// Token: 0x02003141 RID: 12609
	[Serializable]
	internal class TokenRestriction : StringRestriction
	{
		// Token: 0x17009996 RID: 39318
		// (get) Token: 0x0601B572 RID: 111986 RVA: 0x0000240C File Offset: 0x0000060C
		// (set) Token: 0x0601B573 RID: 111987 RVA: 0x0000336E File Offset: 0x0000156E
		public override XsdType XsdType
		{
			get
			{
				return XsdType.Token;
			}
			set
			{
			}
		}

		// Token: 0x17009997 RID: 39319
		// (get) Token: 0x0601B574 RID: 111988 RVA: 0x00376691 File Offset: 0x00374891
		public override string ClrTypeName
		{
			get
			{
				return TokenRestriction.clrTypeName;
			}
		}

		// Token: 0x0601B575 RID: 111989 RVA: 0x00376698 File Offset: 0x00374898
		public override bool ValidateValueType(OpenXmlSimpleType attributeValue)
		{
			try
			{
				XmlConvert.VerifyTOKEN(attributeValue.InnerText);
			}
			catch (XmlException)
			{
				return false;
			}
			return true;
		}

		// Token: 0x0400B536 RID: 46390
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[NonSerialized]
		private static readonly string clrTypeName = ValidationResources.TypeName_token;
	}
}
