using System;
using System.IO;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001B7 RID: 439
	public sealed class MapShapefileInstance : MapSpatialDataInstance
	{
		// Token: 0x0600115A RID: 4442 RVA: 0x0004885A File Offset: 0x00046A5A
		internal MapShapefileInstance(MapShapefile defObject)
			: base(defObject)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x0600115B RID: 4443 RVA: 0x0004886C File Offset: 0x00046A6C
		public string Source
		{
			get
			{
				if (this.m_source == null)
				{
					this.m_source = ((MapShapefile)this.m_defObject.MapSpatialDataDef).EvaluateSource(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext);
				}
				return this.m_source;
			}
		}

		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x0600115C RID: 4444 RVA: 0x000488BD File Offset: 0x00046ABD
		public Stream Stream
		{
			get
			{
				if (this.m_stream == null)
				{
					this.m_stream = this.GetFileStream(this.Source);
				}
				return this.m_stream;
			}
		}

		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x0600115D RID: 4445 RVA: 0x000488DF File Offset: 0x00046ADF
		public Stream DBFStream
		{
			get
			{
				if (this.m_dbfStream == null)
				{
					this.m_dbfStream = this.GetFileStream(this.GetDBFUrl());
				}
				return this.m_dbfStream;
			}
		}

		// Token: 0x0600115E RID: 4446 RVA: 0x00048901 File Offset: 0x00046B01
		protected override void ResetInstanceCache()
		{
			base.ResetInstanceCache();
			this.m_source = null;
			this.m_stream = null;
			this.m_dbfStream = null;
		}

		// Token: 0x0600115F RID: 4447 RVA: 0x00048920 File Offset: 0x00046B20
		private Stream GetFileStream(string url)
		{
			if (string.IsNullOrEmpty(url))
			{
				return null;
			}
			string fileStreamName = ((MapShapefile)this.m_defObject.MapSpatialDataDef).GetFileStreamName(this.m_defObject.MapDef.RenderingContext, url);
			if (fileStreamName != null)
			{
				string text;
				return this.m_defObject.MapDef.RenderingContext.OdpContext.ChunkFactory.GetChunk(fileStreamName, ReportProcessing.ReportChunkTypes.Shapefile, ChunkMode.Open, out text);
			}
			return null;
		}

		// Token: 0x06001160 RID: 4448 RVA: 0x00048988 File Offset: 0x00046B88
		private string GetDBFUrl()
		{
			if (this.Source.EndsWith(".shp", StringComparison.OrdinalIgnoreCase))
			{
				return this.Source.Substring(0, this.Source.Length - 3) + "dbf";
			}
			return null;
		}

		// Token: 0x04000834 RID: 2100
		private MapShapefile m_defObject;

		// Token: 0x04000835 RID: 2101
		private string m_source;

		// Token: 0x04000836 RID: 2102
		private Stream m_stream;

		// Token: 0x04000837 RID: 2103
		private Stream m_dbfStream;
	}
}
