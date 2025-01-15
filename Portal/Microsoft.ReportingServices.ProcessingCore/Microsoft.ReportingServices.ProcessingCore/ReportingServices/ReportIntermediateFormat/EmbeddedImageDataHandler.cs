using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000408 RID: 1032
	internal sealed class EmbeddedImageDataHandler : ImageDataHandler
	{
		// Token: 0x06002C52 RID: 11346 RVA: 0x000CC715 File Offset: 0x000CA915
		public EmbeddedImageDataHandler(ReportElement reportElement, IBaseImage image)
			: base(reportElement, image)
		{
		}

		// Token: 0x1700156B RID: 5483
		// (get) Token: 0x06002C53 RID: 11347 RVA: 0x000CC71F File Offset: 0x000CA91F
		public override Microsoft.ReportingServices.OnDemandReportRendering.Image.SourceType Source
		{
			get
			{
				return Microsoft.ReportingServices.OnDemandReportRendering.Image.SourceType.Embedded;
			}
		}

		// Token: 0x06002C54 RID: 11348 RVA: 0x000CC724 File Offset: 0x000CA924
		protected override string CalculateImageProperties(out string mimeType, out byte[] imageData, out string imageDataId, out List<string> fieldsUsedInValue, out bool isNullImage)
		{
			isNullImage = false;
			bool flag;
			string valueAsString = this.m_image.GetValueAsString(out fieldsUsedInValue, out flag);
			if (flag)
			{
				return base.GetErrorImageProperties(out mimeType, out imageData, out imageDataId);
			}
			if (string.IsNullOrEmpty(valueAsString))
			{
				isNullImage = true;
				return base.GetTransparentImageProperties(out mimeType, out imageData, out imageDataId);
			}
			imageDataId = valueAsString;
			string text;
			if (!base.CacheManager.TryGetEmbeddedImage(valueAsString, this.m_image.EmbeddingMode, this.OdpContext, out imageData, out mimeType, out text))
			{
				this.OdpContext.ErrorContext.Register(ProcessingErrorCode.rsInvalidEmbeddedImageProperty, Severity.Warning, this.m_image.ObjectType, this.m_image.ObjectName, this.m_image.ImageDataPropertyName, new string[] { valueAsString });
				return base.GetErrorImageProperties(out mimeType, out imageData, out imageDataId);
			}
			return text;
		}

		// Token: 0x1700156C RID: 5484
		// (get) Token: 0x06002C55 RID: 11349 RVA: 0x000CC7DB File Offset: 0x000CA9DB
		protected override ProcessingErrorCode ErrorCodeForSourceType
		{
			get
			{
				return ProcessingErrorCode.rsInvalidEmbeddedImageProperty;
			}
		}

		// Token: 0x06002C56 RID: 11350 RVA: 0x000CC7E2 File Offset: 0x000CA9E2
		protected override byte[] LoadExistingImageData(string imageDataId)
		{
			return base.CacheManager.GetCachedEmbeddedImageBytes(imageDataId, this.OdpContext);
		}

		// Token: 0x1700156D RID: 5485
		// (get) Token: 0x06002C57 RID: 11351 RVA: 0x000CC7F6 File Offset: 0x000CA9F6
		private OnDemandProcessingContext OdpContext
		{
			get
			{
				return this.m_reportElement.RenderingContext.OdpContext;
			}
		}
	}
}
