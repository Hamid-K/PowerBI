using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200033D RID: 829
	internal sealed class TagCollection : ReportElementCollectionBase<Tag>
	{
		// Token: 0x06001F16 RID: 7958 RVA: 0x000779EC File Offset: 0x00075BEC
		internal TagCollection(Image image)
		{
			this.m_image = image;
			List<ExpressionInfo> tags = this.m_image.ImageDef.Tags;
			this.m_collection = new List<Tag>(tags.Count);
			for (int i = 0; i < tags.Count; i++)
			{
				this.m_collection.Add(new Tag(image, tags[i]));
			}
		}

		// Token: 0x17001179 RID: 4473
		// (get) Token: 0x06001F17 RID: 7959 RVA: 0x00077A51 File Offset: 0x00075C51
		public override int Count
		{
			get
			{
				return this.m_collection.Count;
			}
		}

		// Token: 0x1700117A RID: 4474
		public override Tag this[int i]
		{
			get
			{
				return this.m_collection[i];
			}
		}

		// Token: 0x06001F19 RID: 7961 RVA: 0x00077A6C File Offset: 0x00075C6C
		internal void SetNewContext()
		{
			for (int i = 0; i < this.m_collection.Count; i++)
			{
				this.m_collection[i].SetNewContext();
			}
		}

		// Token: 0x04000FC0 RID: 4032
		private readonly Image m_image;

		// Token: 0x04000FC1 RID: 4033
		private readonly List<Tag> m_collection;
	}
}
