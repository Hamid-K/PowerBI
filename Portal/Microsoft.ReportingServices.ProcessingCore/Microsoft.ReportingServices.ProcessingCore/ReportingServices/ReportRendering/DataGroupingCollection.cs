using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x02000065 RID: 101
	public sealed class DataGroupingCollection
	{
		// Token: 0x060006CB RID: 1739 RVA: 0x00019F40 File Offset: 0x00018140
		internal DataGroupingCollection(CustomReportItem owner, DataMember parent, CustomReportItemHeadingList headingDef, CustomReportItemHeadingInstanceList headingInstances)
		{
			this.m_owner = owner;
			this.m_parent = parent;
			this.m_headingInstances = headingInstances;
			this.m_headingDef = headingDef;
		}

		// Token: 0x17000511 RID: 1297
		public DataMemberCollection this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.Count });
				}
				DataMemberCollection dataMemberCollection = null;
				if (index == 0)
				{
					dataMemberCollection = this.m_firstCollection;
				}
				else if (this.m_collections != null)
				{
					dataMemberCollection = this.m_collections[index - 1];
				}
				if (dataMemberCollection == null)
				{
					bool flag = index > 0 && this.m_headingDef[index].Static && !this.m_headingDef[index].Subtotal && this.m_headingDef[index - 1].Subtotal;
					CustomReportItemHeadingInstanceList customReportItemHeadingInstanceList;
					if (this.m_headingDef[index].Static && this.m_headingInstances != null && this.m_headingInstances.Count > index)
					{
						customReportItemHeadingInstanceList = new CustomReportItemHeadingInstanceList(1);
						customReportItemHeadingInstanceList.Add(this.m_headingInstances[index]);
					}
					else
					{
						customReportItemHeadingInstanceList = this.m_headingInstances;
					}
					dataMemberCollection = new DataMemberCollection(this.m_owner, this.m_parent, this.m_headingDef[index], flag, customReportItemHeadingInstanceList);
					if (this.m_owner.UseCache)
					{
						if (index == 0)
						{
							this.m_firstCollection = dataMemberCollection;
						}
						else
						{
							if (this.m_collections == null)
							{
								this.m_collections = new DataMemberCollection[this.Count - 1];
							}
							this.m_collections[index - 1] = dataMemberCollection;
						}
					}
				}
				return dataMemberCollection;
			}
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x060006CD RID: 1741 RVA: 0x0001A0C9 File Offset: 0x000182C9
		public int Count
		{
			get
			{
				return this.m_headingDef.Count;
			}
		}

		// Token: 0x040001CE RID: 462
		private CustomReportItem m_owner;

		// Token: 0x040001CF RID: 463
		private CustomReportItemHeadingList m_headingDef;

		// Token: 0x040001D0 RID: 464
		private CustomReportItemHeadingInstanceList m_headingInstances;

		// Token: 0x040001D1 RID: 465
		private DataMemberCollection[] m_collections;

		// Token: 0x040001D2 RID: 466
		private DataMemberCollection m_firstCollection;

		// Token: 0x040001D3 RID: 467
		private DataMember m_parent;
	}
}
