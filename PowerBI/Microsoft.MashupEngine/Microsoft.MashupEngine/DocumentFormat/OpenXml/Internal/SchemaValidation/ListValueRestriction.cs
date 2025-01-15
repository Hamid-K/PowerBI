using System;

namespace DocumentFormat.OpenXml.Internal.SchemaValidation
{
	// Token: 0x0200312D RID: 12589
	[Serializable]
	internal class ListValueRestriction : SimpleTypeRestriction
	{
		// Token: 0x17009953 RID: 39251
		// (get) Token: 0x0601B4EB RID: 111851 RVA: 0x002DE0C4 File Offset: 0x002DC2C4
		// (set) Token: 0x0601B4EC RID: 111852 RVA: 0x0000336E File Offset: 0x0000156E
		public override XsdType XsdType
		{
			get
			{
				return XsdType.List;
			}
			set
			{
			}
		}

		// Token: 0x17009954 RID: 39252
		// (get) Token: 0x0601B4ED RID: 111853 RVA: 0x00376089 File Offset: 0x00374289
		public override string ClrTypeName
		{
			get
			{
				return this.ListItemType.ClrTypeName;
			}
		}

		// Token: 0x17009955 RID: 39253
		// (get) Token: 0x0601B4EE RID: 111854 RVA: 0x00002139 File Offset: 0x00000339
		public override bool IsList
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17009956 RID: 39254
		// (get) Token: 0x0601B4EF RID: 111855 RVA: 0x00376096 File Offset: 0x00374296
		// (set) Token: 0x0601B4F0 RID: 111856 RVA: 0x0037609E File Offset: 0x0037429E
		public SimpleTypeRestriction ListItemType { get; set; }

		// Token: 0x0601B4F1 RID: 111857 RVA: 0x003760A8 File Offset: 0x003742A8
		public override bool ValidateValueType(OpenXmlSimpleType attributeValue)
		{
			if (attributeValue.HasValue)
			{
				foreach (OpenXmlSimpleType openXmlSimpleType in attributeValue.GetListItems())
				{
					if (!this.ListItemType.ValidateValueType(openXmlSimpleType) || this.ListItemType.Validate(openXmlSimpleType) != RestrictionField.None)
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}
	}
}
