using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000216 RID: 534
	public sealed class BandLayoutOptions
	{
		// Token: 0x06001456 RID: 5206 RVA: 0x00053781 File Offset: 0x00051981
		internal BandLayoutOptions(BandLayoutOptions bandLayoutDef)
		{
			this.m_bandLayoutDef = bandLayoutDef;
		}

		// Token: 0x17000ACD RID: 2765
		// (get) Token: 0x06001457 RID: 5207 RVA: 0x00053790 File Offset: 0x00051990
		public int RowCount
		{
			get
			{
				return this.m_bandLayoutDef.RowCount;
			}
		}

		// Token: 0x17000ACE RID: 2766
		// (get) Token: 0x06001458 RID: 5208 RVA: 0x0005379D File Offset: 0x0005199D
		public int ColumnCount
		{
			get
			{
				return this.m_bandLayoutDef.ColumnCount;
			}
		}

		// Token: 0x17000ACF RID: 2767
		// (get) Token: 0x06001459 RID: 5209 RVA: 0x000537AC File Offset: 0x000519AC
		public Navigation Navigation
		{
			get
			{
				if (this.m_navigation == null && this.m_bandLayoutDef.Navigation != null)
				{
					switch (this.m_bandLayoutDef.Navigation.GetObjectType())
					{
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Coverflow:
						this.m_navigation = new Coverflow(this.m_bandLayoutDef);
						goto IL_00B5;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PlayAxis:
						this.m_navigation = new PlayAxis(this.m_bandLayoutDef);
						goto IL_00B5;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Tabstrip:
						this.m_navigation = new Tabstrip(this.m_bandLayoutDef);
						goto IL_00B5;
					}
					Global.Tracer.Assert(false, "Unknown Band Navigation Type: {0}", new object[] { this.m_bandLayoutDef.Navigation.GetObjectType() });
				}
				IL_00B5:
				return this.m_navigation;
			}
		}

		// Token: 0x0400099F RID: 2463
		private readonly BandLayoutOptions m_bandLayoutDef;

		// Token: 0x040009A0 RID: 2464
		private Navigation m_navigation;
	}
}
