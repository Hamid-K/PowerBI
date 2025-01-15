using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap2010;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001C5 RID: 453
	internal sealed class GetReportItemReferencesActionParameters : RSSoapActionParameters
	{
		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06000FF1 RID: 4081 RVA: 0x00038978 File Offset: 0x00036B78
		// (set) Token: 0x06000FF2 RID: 4082 RVA: 0x00038980 File Offset: 0x00036B80
		public string ItemPath
		{
			get
			{
				return this.m_itemPath;
			}
			set
			{
				this.m_itemPath = value;
			}
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06000FF3 RID: 4083 RVA: 0x00038989 File Offset: 0x00036B89
		// (set) Token: 0x06000FF4 RID: 4084 RVA: 0x00038991 File Offset: 0x00036B91
		public ItemReferenceData[] ItemReferences
		{
			get
			{
				return this.m_itemReferences;
			}
			set
			{
				this.m_itemReferences = value;
			}
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x06000FF5 RID: 4085 RVA: 0x0003899A File Offset: 0x00036B9A
		// (set) Token: 0x06000FF6 RID: 4086 RVA: 0x000389A2 File Offset: 0x00036BA2
		public ItemTypeEnum? ReferenceItemType
		{
			get
			{
				return this.m_referenceItemType;
			}
			set
			{
				this.m_referenceItemType = new ItemTypeEnum?(value.Value);
			}
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x06000FF7 RID: 4087 RVA: 0x000389B6 File Offset: 0x00036BB6
		internal override string InputTrace
		{
			get
			{
				return this.ItemPath;
			}
		}

		// Token: 0x06000FF8 RID: 4088 RVA: 0x000389C0 File Offset: 0x00036BC0
		internal override void Validate()
		{
			if (this.ItemPath == null)
			{
				throw new MissingParameterException("ItemPath");
			}
			if (this.ReferenceItemType != null && this.ReferenceItemType.Value != ItemTypeEnum.DataSet && this.ReferenceItemType.Value != ItemTypeEnum.DataSource)
			{
				throw new InvalidParameterException("ReferenceItemType");
			}
		}

		// Token: 0x04000644 RID: 1604
		private string m_itemPath;

		// Token: 0x04000645 RID: 1605
		private ItemReferenceData[] m_itemReferences;

		// Token: 0x04000646 RID: 1606
		private ItemTypeEnum? m_referenceItemType;
	}
}
