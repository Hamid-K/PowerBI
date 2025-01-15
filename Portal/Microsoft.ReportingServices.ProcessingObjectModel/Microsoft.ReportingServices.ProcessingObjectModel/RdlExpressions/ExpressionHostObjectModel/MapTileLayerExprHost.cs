using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x020000EC RID: 236
	public abstract class MapTileLayerExprHost : MapLayerExprHost
	{
		// Token: 0x170003AA RID: 938
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x00003D15 File Offset: 0x00001F15
		public virtual object ServiceUrlExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x060004D7 RID: 1239 RVA: 0x00003D18 File Offset: 0x00001F18
		public virtual object TileStyleExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x00003D1B File Offset: 0x00001F1B
		public virtual object UseSecureConnectionExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x060004D9 RID: 1241 RVA: 0x00003D1E File Offset: 0x00001F1E
		internal IList<MapTileExprHost> MapTilesHostsRemotable
		{
			get
			{
				return this.m_mapTilesHostsRemotable;
			}
		}

		// Token: 0x04000164 RID: 356
		[CLSCompliant(false)]
		protected IList<MapTileExprHost> m_mapTilesHostsRemotable;
	}
}
