using System;
using System.Security.Permissions;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000282 RID: 642
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class DataCell : IReportScope, IDataRegionCell
	{
		// Token: 0x060018F5 RID: 6389 RVA: 0x00066808 File Offset: 0x00064A08
		internal DataCell(Microsoft.ReportingServices.OnDemandReportRendering.CustomReportItem owner, int rowIndex, int colIndex)
		{
			this.m_owner = owner;
			this.m_rowIndex = rowIndex;
			this.m_columnIndex = colIndex;
			this.m_dataValues = null;
		}

		// Token: 0x17000E4A RID: 3658
		// (get) Token: 0x060018F6 RID: 6390
		public abstract DataValueCollection DataValues { get; }

		// Token: 0x17000E4B RID: 3659
		// (get) Token: 0x060018F7 RID: 6391
		internal abstract Microsoft.ReportingServices.ReportIntermediateFormat.DataCell DataCellDef { get; }

		// Token: 0x17000E4C RID: 3660
		// (get) Token: 0x060018F8 RID: 6392
		internal abstract Microsoft.ReportingServices.ReportRendering.DataCell RenderItem { get; }

		// Token: 0x17000E4D RID: 3661
		// (get) Token: 0x060018F9 RID: 6393 RVA: 0x0006682C File Offset: 0x00064A2C
		internal Microsoft.ReportingServices.OnDemandReportRendering.CustomReportItem CriDef
		{
			get
			{
				return this.m_owner;
			}
		}

		// Token: 0x17000E4E RID: 3662
		// (get) Token: 0x060018FA RID: 6394 RVA: 0x00066834 File Offset: 0x00064A34
		public DataCellInstance Instance
		{
			get
			{
				if (this.m_owner.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					this.m_instance = new DataCellInstance(this);
				}
				return this.m_instance;
			}
		}

		// Token: 0x17000E4F RID: 3663
		// (get) Token: 0x060018FB RID: 6395 RVA: 0x00066864 File Offset: 0x00064A64
		IReportScopeInstance IReportScope.ReportScopeInstance
		{
			get
			{
				return this.Instance;
			}
		}

		// Token: 0x17000E50 RID: 3664
		// (get) Token: 0x060018FC RID: 6396 RVA: 0x0006686C File Offset: 0x00064A6C
		IRIFReportScope IReportScope.RIFReportScope
		{
			get
			{
				return this.RIFReportScope;
			}
		}

		// Token: 0x17000E51 RID: 3665
		// (get) Token: 0x060018FD RID: 6397 RVA: 0x00066874 File Offset: 0x00064A74
		internal virtual IRIFReportScope RIFReportScope
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060018FE RID: 6398 RVA: 0x00066877 File Offset: 0x00064A77
		void IDataRegionCell.SetNewContext()
		{
			this.SetNewContext();
		}

		// Token: 0x060018FF RID: 6399 RVA: 0x0006687F File Offset: 0x00064A7F
		internal virtual void SetNewContext()
		{
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
			if (this.m_dataValues != null)
			{
				this.m_dataValues.SetNewContext();
			}
		}

		// Token: 0x04000C95 RID: 3221
		protected int m_rowIndex;

		// Token: 0x04000C96 RID: 3222
		protected int m_columnIndex;

		// Token: 0x04000C97 RID: 3223
		protected Microsoft.ReportingServices.OnDemandReportRendering.CustomReportItem m_owner;

		// Token: 0x04000C98 RID: 3224
		protected DataValueCollection m_dataValues;

		// Token: 0x04000C99 RID: 3225
		protected DataCellInstance m_instance;
	}
}
