using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x02000066 RID: 102
	public sealed class DataMemberCollection
	{
		// Token: 0x060006CE RID: 1742 RVA: 0x0001A0D6 File Offset: 0x000182D6
		internal DataMemberCollection(CustomReportItem owner, DataMember parent, CustomReportItemHeading headingDef, bool headingDefIsStaticSubtotal, CustomReportItemHeadingInstanceList headingInstances)
		{
			this.m_owner = owner;
			this.m_parent = parent;
			this.m_headingInstances = headingInstances;
			this.m_isSubtotal = headingDefIsStaticSubtotal;
			this.m_headingDef = headingDef;
		}

		// Token: 0x17000513 RID: 1299
		public DataMember this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.Count });
				}
				DataMember dataMember = null;
				if (index == 0)
				{
					dataMember = this.m_firstMember;
				}
				else if (this.m_members != null)
				{
					dataMember = this.m_members[index - 1];
				}
				if (dataMember == null)
				{
					CustomReportItemHeadingInstance customReportItemHeadingInstance = null;
					if (this.m_headingInstances != null && index < this.m_headingInstances.Count)
					{
						customReportItemHeadingInstance = this.m_headingInstances[index];
					}
					dataMember = new DataMember(this.m_owner, this.m_parent, this.m_headingDef, customReportItemHeadingInstance, this.m_isSubtotal, index);
					if (this.m_owner.UseCache)
					{
						if (index == 0)
						{
							this.m_firstMember = dataMember;
						}
						else
						{
							if (this.m_members == null)
							{
								this.m_members = new DataMember[this.Count - 1];
							}
							this.m_members[index - 1] = dataMember;
						}
					}
				}
				return dataMember;
			}
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x060006D0 RID: 1744 RVA: 0x0001A1FB File Offset: 0x000183FB
		public int Count
		{
			get
			{
				if (this.m_headingInstances == null || this.m_headingInstances.Count == 0)
				{
					return 1;
				}
				return this.m_headingInstances.Count;
			}
		}

		// Token: 0x040001D4 RID: 468
		private CustomReportItem m_owner;

		// Token: 0x040001D5 RID: 469
		private CustomReportItemHeading m_headingDef;

		// Token: 0x040001D6 RID: 470
		private CustomReportItemHeadingInstanceList m_headingInstances;

		// Token: 0x040001D7 RID: 471
		private DataMember[] m_members;

		// Token: 0x040001D8 RID: 472
		private DataMember m_firstMember;

		// Token: 0x040001D9 RID: 473
		private DataMember m_parent;

		// Token: 0x040001DA RID: 474
		private bool m_isSubtotal;
	}
}
