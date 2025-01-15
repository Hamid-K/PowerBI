using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Validation;

namespace DocumentFormat.OpenXml.Internal.SchemaValidation
{
	// Token: 0x02003146 RID: 12614
	[Serializable]
	internal class HexBinaryRestriction : StringRestriction
	{
		// Token: 0x170099A0 RID: 39328
		// (get) Token: 0x0601B590 RID: 112016 RVA: 0x00075E2C File Offset: 0x0007402C
		// (set) Token: 0x0601B591 RID: 112017 RVA: 0x0000336E File Offset: 0x0000156E
		public override XsdType XsdType
		{
			get
			{
				return XsdType.HexBinary;
			}
			set
			{
			}
		}

		// Token: 0x170099A1 RID: 39329
		// (get) Token: 0x0601B592 RID: 112018 RVA: 0x003768B1 File Offset: 0x00374AB1
		public override string ClrTypeName
		{
			get
			{
				return HexBinaryRestriction.clrTypeName;
			}
		}

		// Token: 0x0601B593 RID: 112019 RVA: 0x003768B8 File Offset: 0x00374AB8
		public override bool ValidateValueType(OpenXmlSimpleType attributeValue)
		{
			if (attributeValue.InnerText == null)
			{
				return false;
			}
			if (attributeValue.InnerText.Length == 0)
			{
				return true;
			}
			string text = "\\A([0-9a-fA-F][0-9a-fA-F])+\\z";
			return Regex.IsMatch(attributeValue.InnerText, text, RegexOptions.CultureInvariant);
		}

		// Token: 0x0601B594 RID: 112020 RVA: 0x003768F8 File Offset: 0x00374AF8
		internal override int GetValueLength(OpenXmlSimpleType attributeValue)
		{
			int length = attributeValue.InnerText.Length;
			return (length + 1) / 2;
		}

		// Token: 0x0400B53C RID: 46396
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[NonSerialized]
		private static readonly string clrTypeName = ValidationResources.TypeName_hexBinary;
	}
}
