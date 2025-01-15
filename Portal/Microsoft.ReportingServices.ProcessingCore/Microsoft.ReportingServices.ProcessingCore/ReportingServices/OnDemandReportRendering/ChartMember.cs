using System;
using System.Security.Permissions;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000276 RID: 630
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class ChartMember : DataRegionMember
	{
		// Token: 0x0600186D RID: 6253 RVA: 0x00064DD1 File Offset: 0x00062FD1
		internal ChartMember(IDefinitionPath parentDefinitionPath, Chart owner, ChartMember parent, int parentCollectionIndex)
			: base(parentDefinitionPath, owner, parent, parentCollectionIndex)
		{
		}

		// Token: 0x17000DF3 RID: 3571
		// (get) Token: 0x0600186E RID: 6254 RVA: 0x00064DDE File Offset: 0x00062FDE
		public ChartMember Parent
		{
			get
			{
				return this.m_parent as ChartMember;
			}
		}

		// Token: 0x17000DF4 RID: 3572
		// (get) Token: 0x0600186F RID: 6255
		public abstract ReportStringProperty Label { get; }

		// Token: 0x17000DF5 RID: 3573
		// (get) Token: 0x06001870 RID: 6256
		public abstract string DataElementName { get; }

		// Token: 0x17000DF6 RID: 3574
		// (get) Token: 0x06001871 RID: 6257
		public abstract DataElementOutputTypes DataElementOutput { get; }

		// Token: 0x17000DF7 RID: 3575
		// (get) Token: 0x06001872 RID: 6258
		public abstract ChartMemberCollection Children { get; }

		// Token: 0x17000DF8 RID: 3576
		// (get) Token: 0x06001873 RID: 6259
		public abstract bool IsCategory { get; }

		// Token: 0x17000DF9 RID: 3577
		// (get) Token: 0x06001874 RID: 6260
		public abstract int SeriesSpan { get; }

		// Token: 0x17000DFA RID: 3578
		// (get) Token: 0x06001875 RID: 6261
		public abstract int CategorySpan { get; }

		// Token: 0x17000DFB RID: 3579
		// (get) Token: 0x06001876 RID: 6262
		public abstract bool IsTotal { get; }

		// Token: 0x17000DFC RID: 3580
		// (get) Token: 0x06001877 RID: 6263
		internal abstract ChartMember MemberDefinition { get; }

		// Token: 0x17000DFD RID: 3581
		// (get) Token: 0x06001878 RID: 6264 RVA: 0x00064DEB File Offset: 0x00062FEB
		internal override ReportHierarchyNode DataRegionMemberDefinition
		{
			get
			{
				return this.MemberDefinition;
			}
		}

		// Token: 0x17000DFE RID: 3582
		// (get) Token: 0x06001879 RID: 6265 RVA: 0x00064DF3 File Offset: 0x00062FF3
		internal Chart OwnerChart
		{
			get
			{
				return this.m_owner as Chart;
			}
		}

		// Token: 0x17000DFF RID: 3583
		// (get) Token: 0x0600187A RID: 6266
		public abstract ChartMemberInstance Instance { get; }

		// Token: 0x17000E00 RID: 3584
		// (get) Token: 0x0600187B RID: 6267 RVA: 0x00064E00 File Offset: 0x00063000
		internal override IDataRegionMemberCollection SubMembers
		{
			get
			{
				return this.m_children;
			}
		}

		// Token: 0x0600187C RID: 6268 RVA: 0x00064E08 File Offset: 0x00063008
		internal override bool GetIsColumn()
		{
			return this.IsCategory;
		}

		// Token: 0x17000E01 RID: 3585
		// (get) Token: 0x0600187D RID: 6269 RVA: 0x00064E10 File Offset: 0x00063010
		private ChartSeries ChartSeries
		{
			get
			{
				if (this.IsCategory || this.Children != null)
				{
					return null;
				}
				if (this.m_chartSeries == null)
				{
					this.m_chartSeries = ((Chart)this.m_owner).ChartData.SeriesCollection[this.MemberCellIndex];
				}
				return this.m_chartSeries;
			}
		}

		// Token: 0x0600187E RID: 6270 RVA: 0x00064E64 File Offset: 0x00063064
		internal override void SetNewContext(bool fromMoveNext)
		{
			base.SetNewContext(fromMoveNext);
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
			ChartSeries chartSeries = this.ChartSeries;
			if (chartSeries != null)
			{
				chartSeries.SetNewContext();
			}
		}

		// Token: 0x04000C72 RID: 3186
		protected ChartMemberCollection m_children;

		// Token: 0x04000C73 RID: 3187
		protected ChartMemberInstance m_instance;

		// Token: 0x04000C74 RID: 3188
		protected ReportStringProperty m_label;

		// Token: 0x04000C75 RID: 3189
		protected ChartSeries m_chartSeries;
	}
}
