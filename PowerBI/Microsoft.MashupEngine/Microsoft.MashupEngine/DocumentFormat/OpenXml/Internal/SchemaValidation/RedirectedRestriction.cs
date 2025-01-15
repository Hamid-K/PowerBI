using System;

namespace DocumentFormat.OpenXml.Internal.SchemaValidation
{
	// Token: 0x0200312A RID: 12586
	[Serializable]
	internal class RedirectedRestriction : SimpleTypeRestriction
	{
		// Token: 0x17009948 RID: 39240
		// (get) Token: 0x0601B4D2 RID: 111826 RVA: 0x00375F81 File Offset: 0x00374181
		// (set) Token: 0x0601B4D3 RID: 111827 RVA: 0x00375F89 File Offset: 0x00374189
		public SimpleTypeRestriction TargetRestriction { get; set; }

		// Token: 0x17009949 RID: 39241
		// (get) Token: 0x0601B4D4 RID: 111828 RVA: 0x00375F92 File Offset: 0x00374192
		// (set) Token: 0x0601B4D5 RID: 111829 RVA: 0x00375F9A File Offset: 0x0037419A
		internal int AttributeId { get; set; }

		// Token: 0x1700994A RID: 39242
		// (get) Token: 0x0601B4D7 RID: 111831 RVA: 0x002E03A2 File Offset: 0x002DE5A2
		// (set) Token: 0x0601B4D8 RID: 111832 RVA: 0x0000336E File Offset: 0x0000156E
		public override XsdType XsdType
		{
			get
			{
				return XsdType.Redirected;
			}
			set
			{
			}
		}

		// Token: 0x1700994B RID: 39243
		// (get) Token: 0x0601B4D9 RID: 111833 RVA: 0x00375FAB File Offset: 0x003741AB
		public override string ClrTypeName
		{
			get
			{
				return this.TargetRestriction.ClrTypeName;
			}
		}

		// Token: 0x0601B4DA RID: 111834 RVA: 0x00375FB8 File Offset: 0x003741B8
		public OpenXmlSimpleType ConvertValue(OpenXmlSimpleType value)
		{
			OpenXmlSimpleType openXmlSimpleType = UnionHelper.CreateTargetValueObject(this, base.FileFormat);
			openXmlSimpleType.InnerText = value.InnerText;
			return openXmlSimpleType;
		}
	}
}
