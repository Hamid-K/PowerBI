using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x020000B8 RID: 184
	public abstract class MapMemberExprHost : MemberNodeExprHost<MapMemberExprHost>
	{
		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000424 RID: 1060 RVA: 0x000039BF File Offset: 0x00001BBF
		internal IList<MapPolygonLayerExprHost> MapPolygonLayersHostsRemotable
		{
			get
			{
				return this.m_mapPolygonLayersHostsRemotable;
			}
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x000039C7 File Offset: 0x00001BC7
		internal IList<MapPointLayerExprHost> MapPointLayersHostsRemotable
		{
			get
			{
				return this.m_mapPointLayersHostsRemotable;
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000426 RID: 1062 RVA: 0x000039CF File Offset: 0x00001BCF
		internal IList<MapLineLayerExprHost> MapLineLayersHostsRemotable
		{
			get
			{
				return this.m_mapLineLayersHostsRemotable;
			}
		}

		// Token: 0x0400013A RID: 314
		[CLSCompliant(false)]
		protected IList<MapPolygonLayerExprHost> m_mapPolygonLayersHostsRemotable;

		// Token: 0x0400013B RID: 315
		[CLSCompliant(false)]
		protected IList<MapPointLayerExprHost> m_mapPointLayersHostsRemotable;

		// Token: 0x0400013C RID: 316
		[CLSCompliant(false)]
		protected IList<MapLineLayerExprHost> m_mapLineLayersHostsRemotable;
	}
}
