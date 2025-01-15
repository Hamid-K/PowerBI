using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000407 RID: 1031
	internal sealed class ExternalImageDataHandler : ImageDataHandler
	{
		// Token: 0x06002C4B RID: 11339 RVA: 0x000CC30A File Offset: 0x000CA50A
		public ExternalImageDataHandler(ReportElement reportElement, IBaseImage image)
			: base(reportElement, image)
		{
		}

		// Token: 0x17001569 RID: 5481
		// (get) Token: 0x06002C4C RID: 11340 RVA: 0x000CC314 File Offset: 0x000CA514
		public override Microsoft.ReportingServices.OnDemandReportRendering.Image.SourceType Source
		{
			get
			{
				return Microsoft.ReportingServices.OnDemandReportRendering.Image.SourceType.External;
			}
		}

		// Token: 0x06002C4D RID: 11341 RVA: 0x000CC318 File Offset: 0x000CA518
		protected override string GetImageDataId()
		{
			List<string> list;
			bool flag;
			string text = this.m_image.GetValueAsString(out list, out flag);
			if (flag || string.IsNullOrEmpty(text))
			{
				text = null;
			}
			return text;
		}

		// Token: 0x06002C4E RID: 11342 RVA: 0x000CC344 File Offset: 0x000CA544
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
			Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext = this.m_reportElement.RenderingContext;
			string text = null;
			imageData = null;
			imageDataId = valueAsString;
			bool flag2;
			if (base.CacheManager.TryGetExternalImage(valueAsString, out imageData, out mimeType, out text, out flag2))
			{
				if (flag2)
				{
					imageDataId = null;
				}
			}
			else if (!this.GetExternalImage(renderingContext, valueAsString, out imageData, out mimeType) || imageData == null)
			{
				renderingContext.OdpContext.ErrorContext.Register(ProcessingErrorCode.rsInvalidExternalImageProperty, Severity.Warning, this.m_image.ObjectType, this.m_image.ObjectName, this.m_image.ImageDataPropertyName, new string[] { text });
				base.CacheManager.AddFailedExternalImage(valueAsString);
				text = null;
				imageDataId = null;
			}
			else
			{
				text = base.CacheManager.AddExternalImage(valueAsString, imageData, mimeType);
			}
			return text;
		}

		// Token: 0x06002C4F RID: 11343 RVA: 0x000CC430 File Offset: 0x000CA630
		private bool GetExternalImage(Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext, string path, out byte[] imageData, out string mimeType)
		{
			imageData = null;
			mimeType = null;
			try
			{
				if (!renderingContext.OdpContext.TopLevelContext.ReportContext.IsSupportedProtocol(path, true))
				{
					renderingContext.OdpContext.ErrorContext.Register(ProcessingErrorCode.rsUnsupportedProtocol, Severity.Error, this.m_image.ObjectType, this.m_image.ObjectName, this.m_image.ImageDataPropertyName, new string[] { path, "http://, https://, ftp://, file:, mailto:, or news:" });
				}
				else
				{
					bool flag;
					renderingContext.OdpContext.GetResource(path, out imageData, out mimeType, out flag);
					if (imageData != null && !Microsoft.ReportingServices.ReportPublishing.Validator.ValidateMimeType(mimeType))
					{
						renderingContext.OdpContext.ErrorContext.Register(ProcessingErrorCode.rsInvalidMIMEType, Severity.Warning, this.m_image.ObjectType, this.m_image.ObjectName, "MIMEType", new string[] { mimeType });
						mimeType = null;
						imageData = null;
					}
					if (flag)
					{
						renderingContext.OdpContext.ErrorContext.Register(ProcessingErrorCode.rsSandboxingExternalResourceExceedsMaximumSize, Severity.Warning, this.m_image.ObjectType, this.m_image.ObjectName, this.m_image.ImageDataPropertyName, Array.Empty<string>());
					}
				}
			}
			catch (ExternalImageSizeException)
			{
				renderingContext.OdpContext.TraceOneTimeWarning(ProcessingErrorCode.rsSandboxingExternalResourceExceedsMaximumSize);
				renderingContext.OdpContext.ErrorContext.Register(ProcessingErrorCode.rsSandboxingExternalResourceExceedsMaximumSize, Severity.Warning, this.m_image.ObjectType, this.m_image.ObjectName, this.m_image.ImageDataPropertyName, Array.Empty<string>());
			}
			catch (ExternalImageLoadingException ex)
			{
				renderingContext.OdpContext.TraceOneTimeWarning(ProcessingErrorCode.rsWarningFetchingExternalImages);
				renderingContext.OdpContext.ErrorContext.Register(ProcessingErrorCode.rsWarningFetchingExternalImages, Severity.Warning, this.m_image.ObjectType, this.m_image.ObjectName, this.m_image.ImageDataPropertyName, new string[] { ex.InnerException.Message });
			}
			catch (ExternalImageLoadingDisabledException)
			{
				renderingContext.OdpContext.ErrorContext.Register(ProcessingErrorCode.rsExternalImageLoadingDisabled, Severity.Warning, this.m_image.ObjectType, this.m_image.ObjectName, this.m_image.ImageDataPropertyName, Array.Empty<string>());
			}
			catch (Exception ex2)
			{
				renderingContext.OdpContext.ErrorContext.Register(ProcessingErrorCode.rsInvalidImageReference, Severity.Warning, this.m_image.ObjectType, this.m_image.ObjectName, this.m_image.ImageDataPropertyName, new string[] { ex2.Message });
				return false;
			}
			return true;
		}

		// Token: 0x1700156A RID: 5482
		// (get) Token: 0x06002C50 RID: 11344 RVA: 0x000CC700 File Offset: 0x000CA900
		protected override ProcessingErrorCode ErrorCodeForSourceType
		{
			get
			{
				return ProcessingErrorCode.rsInvalidExternalImageProperty;
			}
		}

		// Token: 0x06002C51 RID: 11345 RVA: 0x000CC707 File Offset: 0x000CA907
		protected override byte[] LoadExistingImageData(string imageDataId)
		{
			return base.CacheManager.GetCachedExternalImageBytes(imageDataId);
		}
	}
}
