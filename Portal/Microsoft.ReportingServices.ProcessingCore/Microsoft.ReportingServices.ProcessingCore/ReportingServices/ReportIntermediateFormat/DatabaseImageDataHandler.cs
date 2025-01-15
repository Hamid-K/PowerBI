using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000406 RID: 1030
	internal sealed class DatabaseImageDataHandler : ImageDataHandler
	{
		// Token: 0x06002C46 RID: 11334 RVA: 0x000CC19F File Offset: 0x000CA39F
		public DatabaseImageDataHandler(ReportElement reportElement, IBaseImage image)
			: base(reportElement, image)
		{
		}

		// Token: 0x17001567 RID: 5479
		// (get) Token: 0x06002C47 RID: 11335 RVA: 0x000CC1A9 File Offset: 0x000CA3A9
		public override Microsoft.ReportingServices.OnDemandReportRendering.Image.SourceType Source
		{
			get
			{
				return Microsoft.ReportingServices.OnDemandReportRendering.Image.SourceType.Database;
			}
		}

		// Token: 0x06002C48 RID: 11336 RVA: 0x000CC1AC File Offset: 0x000CA3AC
		protected override string CalculateImageProperties(out string mimeType, out byte[] imageData, out string imageDataId, out List<string> fieldsUsedInValue, out bool isNullImage)
		{
			fieldsUsedInValue = null;
			isNullImage = false;
			mimeType = this.m_image.GetMIMETypeValue();
			mimeType = Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateMimeType(mimeType, this.m_image.ObjectType, this.m_image.ObjectName, this.m_image.MIMETypePropertyName, this.m_reportElement.RenderingContext.OdpContext.ErrorContext);
			if (mimeType == null)
			{
				return base.GetErrorImageProperties(out mimeType, out imageData, out imageDataId);
			}
			string instanceUniqueName = this.m_reportElement.InstanceUniqueName;
			string text;
			if (base.CacheManager.TryGetDatabaseImage(instanceUniqueName, out text))
			{
				imageData = null;
				imageDataId = text;
				return text;
			}
			bool flag;
			imageData = this.m_image.GetImageData(out fieldsUsedInValue, out flag);
			if (flag)
			{
				this.m_reportElement.RenderingContext.OdpContext.ErrorContext.Register(this.ErrorCodeForSourceType, Severity.Warning, this.m_image.ObjectType, this.m_image.ObjectName, this.m_image.ImageDataPropertyName, new string[] { this.m_image.Value.ExpressionString });
				return base.GetErrorImageProperties(out mimeType, out imageData, out imageDataId);
			}
			if (imageData == null || imageData.Length == 0)
			{
				isNullImage = true;
				return base.GetTransparentImageProperties(out mimeType, out imageData, out imageDataId);
			}
			text = base.CacheManager.AddDatabaseImage(instanceUniqueName, imageData, mimeType, this.m_reportElement.RenderingContext.OdpContext);
			imageDataId = text;
			return text;
		}

		// Token: 0x17001568 RID: 5480
		// (get) Token: 0x06002C49 RID: 11337 RVA: 0x000CC2F5 File Offset: 0x000CA4F5
		protected override ProcessingErrorCode ErrorCodeForSourceType
		{
			get
			{
				return ProcessingErrorCode.rsInvalidDatabaseImageProperty;
			}
		}

		// Token: 0x06002C4A RID: 11338 RVA: 0x000CC2FC File Offset: 0x000CA4FC
		protected override byte[] LoadExistingImageData(string imageDataId)
		{
			return base.CacheManager.GetCachedDatabaseImageBytes(imageDataId);
		}
	}
}
