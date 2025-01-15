using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Xml;
using DocumentFormat.OpenXml.Validation;

namespace DocumentFormat.OpenXml.Internal.SchemaValidation
{
	// Token: 0x02003148 RID: 12616
	[Serializable]
	internal class LanguageRestriction : TokenRestriction
	{
		// Token: 0x170099A4 RID: 39332
		// (get) Token: 0x0601B59E RID: 112030 RVA: 0x002D0B3B File Offset: 0x002CED3B
		// (set) Token: 0x0601B59F RID: 112031 RVA: 0x0000336E File Offset: 0x0000156E
		public override XsdType XsdType
		{
			get
			{
				return XsdType.Language;
			}
			set
			{
			}
		}

		// Token: 0x170099A5 RID: 39333
		// (get) Token: 0x0601B5A0 RID: 112032 RVA: 0x003769A8 File Offset: 0x00374BA8
		public override string ClrTypeName
		{
			get
			{
				return LanguageRestriction.clrTypeName;
			}
		}

		// Token: 0x0601B5A1 RID: 112033 RVA: 0x003769B0 File Offset: 0x00374BB0
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
			return Regex.IsMatch(attributeValue.InnerText, LanguageRestriction.LanguageLexicalPattern, RegexOptions.CultureInvariant);
		}

		// Token: 0x0400B53E RID: 46398
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[NonSerialized]
		private static readonly string clrTypeName = ValidationResources.TypeName_language;

		// Token: 0x0400B53F RID: 46399
		[NonSerialized]
		private static string LanguageLexicalPattern = "\\A[a-zA-Z]{1,8}(-[a-zA-Z0-9]{1,8})*\\z";
	}
}
