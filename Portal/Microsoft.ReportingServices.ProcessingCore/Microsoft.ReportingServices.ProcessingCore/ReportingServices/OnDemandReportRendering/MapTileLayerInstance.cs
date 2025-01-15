using System;
using System.IO;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001BD RID: 445
	public sealed class MapTileLayerInstance : MapLayerInstance
	{
		// Token: 0x0600116D RID: 4461 RVA: 0x00048AF7 File Offset: 0x00046CF7
		internal MapTileLayerInstance(MapTileLayer defObject)
			: base(defObject)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x0600116E RID: 4462 RVA: 0x00048B08 File Offset: 0x00046D08
		public string ServiceUrl
		{
			get
			{
				if (this.m_serviceUrl == null)
				{
					this.m_serviceUrl = ((MapTileLayer)this.m_defObject.MapLayerDef).EvaluateServiceUrl(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext);
				}
				return this.m_serviceUrl;
			}
		}

		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x0600116F RID: 4463 RVA: 0x00048B5C File Offset: 0x00046D5C
		public MapTileStyle TileStyle
		{
			get
			{
				if (this.m_tileStyle == null)
				{
					this.m_tileStyle = new MapTileStyle?(((MapTileLayer)this.m_defObject.MapLayerDef).EvaluateTileStyle(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_tileStyle.Value;
			}
		}

		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x06001170 RID: 4464 RVA: 0x00048BBC File Offset: 0x00046DBC
		public bool UseSecureConnection
		{
			get
			{
				if (this.m_useSecureConnection == null)
				{
					this.m_useSecureConnection = new bool?(((MapTileLayer)this.m_defObject.MapLayerDef).EvaluateUseSecureConnection(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_useSecureConnection.Value;
			}
		}

		// Token: 0x06001171 RID: 4465 RVA: 0x00048C1C File Offset: 0x00046E1C
		public Stream GetTileData(string url, out string mimeType)
		{
			return this.m_defObject.MapTileLayerDef.GetTileData(url, out mimeType, this.m_defObject.MapDef.RenderingContext);
		}

		// Token: 0x06001172 RID: 4466 RVA: 0x00048C40 File Offset: 0x00046E40
		public void SetTileData(string url, byte[] data, string mimeType)
		{
			this.m_defObject.MapTileLayerDef.SetTileData(url, data, mimeType, this.m_defObject.MapDef.RenderingContext);
		}

		// Token: 0x06001173 RID: 4467 RVA: 0x00048C65 File Offset: 0x00046E65
		protected override void ResetInstanceCache()
		{
			base.ResetInstanceCache();
			this.m_serviceUrl = null;
			this.m_tileStyle = null;
			this.m_useSecureConnection = null;
		}

		// Token: 0x0400083F RID: 2111
		private MapTileLayer m_defObject;

		// Token: 0x04000840 RID: 2112
		private string m_serviceUrl;

		// Token: 0x04000841 RID: 2113
		private MapTileStyle? m_tileStyle;

		// Token: 0x04000842 RID: 2114
		private bool? m_useSecureConnection;
	}
}
