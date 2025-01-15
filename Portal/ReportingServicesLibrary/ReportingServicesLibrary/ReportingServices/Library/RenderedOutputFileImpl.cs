using System;
using System.IO;
using System.Text;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000087 RID: 135
	internal class RenderedOutputFileImpl : RenderedOutputFile
	{
		// Token: 0x060005B3 RID: 1459 RVA: 0x00017567 File Offset: 0x00015767
		public RenderedOutputFileImpl(ReportImpl report, string streamName)
			: this(report, streamName, false)
		{
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x00017572 File Offset: 0x00015772
		public RenderedOutputFileImpl(RSStream rsStream)
		{
			this.m_encoding = Encoding.Default;
			base..ctor();
			this.SetObjectProperties(rsStream);
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x0001758C File Offset: 0x0001578C
		public RenderedOutputFileImpl(ReportImpl report, string streamName, bool isPersisted)
		{
			this.m_encoding = Encoding.Default;
			base..ctor();
			this.m_report = report;
			this.m_streamName = streamName;
			this.m_persistedStream = isPersisted;
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x000175B4 File Offset: 0x000157B4
		private void GetRsStream()
		{
			if (this.m_initialized)
			{
				return;
			}
			RSStream rsstream;
			if (this.m_persistedStream)
			{
				rsstream = this.m_report.Service.StreamManager.PersistedStreamManager.GetNextStream();
			}
			else
			{
				CatalogItemContext catalogItemContext = new CatalogItemContext(this.m_report.Service);
				catalogItemContext.SetPath(this.m_report.Notification.m_path, ItemPathOptions.None);
				catalogItemContext.RSRequestParameters.SetCatalogParameters(null);
				catalogItemContext.RSRequestParameters.SetRenderingParameters(this.m_report.DeviceInfo);
				catalogItemContext.RSRequestParameters.ImageIDParamValue = this.m_streamName;
				catalogItemContext.RSRequestParameters.FormatParamValue = this.m_report.RenderFormat;
				catalogItemContext.RSRequestParameters.SetReportParameters(this.m_report.Notification.m_parameters);
				RenderReportAction renderReportAction = RenderReportAction.CreateWithCatalogItemContext(this.m_report.Session, this.m_report.Service, catalogItemContext);
				renderReportAction.Render();
				rsstream = renderReportAction.ResultStream;
			}
			this.m_report.StreamFactory.RegisterExternalStreamForClosing(rsstream);
			this.SetObjectProperties(rsstream);
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x000176C4 File Offset: 0x000158C4
		private void SetObjectProperties(RSStream rsStream)
		{
			if (rsStream != null)
			{
				this.m_fileName = rsStream.Name;
				this.m_type = rsStream.MimeType;
				this.m_extension = rsStream.Extension;
				this.m_encoding = rsStream.Encoding;
				this.m_data = rsStream;
			}
			this.m_initialized = true;
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060005B8 RID: 1464 RVA: 0x00017712 File Offset: 0x00015912
		public override string FileName
		{
			get
			{
				this.GetRsStream();
				return this.m_fileName;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060005B9 RID: 1465 RVA: 0x00017720 File Offset: 0x00015920
		public override string Type
		{
			get
			{
				this.GetRsStream();
				return this.m_type;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060005BA RID: 1466 RVA: 0x0001772E File Offset: 0x0001592E
		public override Stream Data
		{
			get
			{
				this.GetRsStream();
				return this.m_data;
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060005BB RID: 1467 RVA: 0x0001773C File Offset: 0x0001593C
		public override string Extension
		{
			get
			{
				this.GetRsStream();
				return this.m_extension;
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060005BC RID: 1468 RVA: 0x0001774A File Offset: 0x0001594A
		public override Encoding Encoding
		{
			get
			{
				this.GetRsStream();
				return this.m_encoding;
			}
		}

		// Token: 0x040002FD RID: 765
		private string m_fileName;

		// Token: 0x040002FE RID: 766
		private string m_type;

		// Token: 0x040002FF RID: 767
		private string m_extension;

		// Token: 0x04000300 RID: 768
		private Encoding m_encoding;

		// Token: 0x04000301 RID: 769
		private Stream m_data;

		// Token: 0x04000302 RID: 770
		private bool m_initialized;

		// Token: 0x04000303 RID: 771
		private string m_streamName;

		// Token: 0x04000304 RID: 772
		private bool m_persistedStream;

		// Token: 0x04000305 RID: 773
		private ReportImpl m_report;
	}
}
