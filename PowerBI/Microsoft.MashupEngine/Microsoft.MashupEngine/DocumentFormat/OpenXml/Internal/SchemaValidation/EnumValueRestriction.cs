using System;

namespace DocumentFormat.OpenXml.Internal.SchemaValidation
{
	// Token: 0x0200312C RID: 12588
	[Serializable]
	internal class EnumValueRestriction : SimpleTypeRestriction
	{
		// Token: 0x17009950 RID: 39248
		// (get) Token: 0x0601B4E5 RID: 111845 RVA: 0x002C826A File Offset: 0x002C646A
		// (set) Token: 0x0601B4E6 RID: 111846 RVA: 0x0000336E File Offset: 0x0000156E
		public override XsdType XsdType
		{
			get
			{
				return XsdType.Enum;
			}
			set
			{
			}
		}

		// Token: 0x17009951 RID: 39249
		// (get) Token: 0x0601B4E7 RID: 111847 RVA: 0x0007E4B5 File Offset: 0x0007C6B5
		public override string ClrTypeName
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17009952 RID: 39250
		// (get) Token: 0x0601B4E8 RID: 111848 RVA: 0x00002139 File Offset: 0x00000339
		public override bool IsEnum
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0601B4E9 RID: 111849 RVA: 0x00376071 File Offset: 0x00374271
		public override bool ValidateValueType(OpenXmlSimpleType attributeValue)
		{
			return attributeValue.HasValue && attributeValue.IsInVersion(base.FileFormat);
		}
	}
}
