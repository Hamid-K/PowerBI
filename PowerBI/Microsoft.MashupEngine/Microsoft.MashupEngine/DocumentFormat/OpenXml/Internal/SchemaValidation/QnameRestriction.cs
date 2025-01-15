using System;
using System.Diagnostics;
using System.Xml;
using DocumentFormat.OpenXml.Validation;

namespace DocumentFormat.OpenXml.Internal.SchemaValidation
{
	// Token: 0x02003142 RID: 12610
	[Serializable]
	internal class QnameRestriction : StringRestriction
	{
		// Token: 0x17009998 RID: 39320
		// (get) Token: 0x0601B578 RID: 111992 RVA: 0x002EAFCE File Offset: 0x002E91CE
		// (set) Token: 0x0601B579 RID: 111993 RVA: 0x0000336E File Offset: 0x0000156E
		public override XsdType XsdType
		{
			get
			{
				return XsdType.QName;
			}
			set
			{
			}
		}

		// Token: 0x17009999 RID: 39321
		// (get) Token: 0x0601B57A RID: 111994 RVA: 0x003766D8 File Offset: 0x003748D8
		public override string ClrTypeName
		{
			get
			{
				return QnameRestriction.clrTypeName;
			}
		}

		// Token: 0x0601B57B RID: 111995 RVA: 0x003766E0 File Offset: 0x003748E0
		public override bool ValidateValueType(OpenXmlSimpleType attributeValue)
		{
			string innerText = attributeValue.InnerText;
			if (string.IsNullOrEmpty(innerText))
			{
				return false;
			}
			int num = innerText.IndexOf(":", StringComparison.Ordinal);
			if (num == 0 || num == innerText.Length - 1)
			{
				return false;
			}
			if (num > 0)
			{
				try
				{
					XmlConvert.VerifyNCName(innerText.Substring(0, num));
				}
				catch (XmlException)
				{
					return false;
				}
			}
			try
			{
				XmlConvert.VerifyNCName(innerText.Substring(num + 1));
			}
			catch (XmlException)
			{
				return false;
			}
			return true;
		}

		// Token: 0x0400B537 RID: 46391
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[NonSerialized]
		private static readonly string clrTypeName = ValidationResources.TypeName_QName;
	}
}
