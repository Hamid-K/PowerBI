using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000405 RID: 1029
	internal abstract class ImageDataHandler
	{
		// Token: 0x06002C31 RID: 11313 RVA: 0x000CBF36 File Offset: 0x000CA136
		public ImageDataHandler(ReportElement reportElement, IBaseImage image)
		{
			this.m_reportElement = reportElement;
			this.m_image = image;
		}

		// Token: 0x1700155E RID: 5470
		// (get) Token: 0x06002C32 RID: 11314
		public abstract Microsoft.ReportingServices.OnDemandReportRendering.Image.SourceType Source { get; }

		// Token: 0x1700155F RID: 5471
		// (get) Token: 0x06002C33 RID: 11315 RVA: 0x000CBF4C File Offset: 0x000CA14C
		public string MIMEType
		{
			get
			{
				this.EnsureCacheIsPopulated();
				return this.m_mimeType;
			}
		}

		// Token: 0x17001560 RID: 5472
		// (get) Token: 0x06002C34 RID: 11316 RVA: 0x000CBF5C File Offset: 0x000CA15C
		public byte[] ImageData
		{
			get
			{
				this.EnsureCacheIsPopulated();
				if (this.m_imageData == null && this.m_imageDataId != null)
				{
					this.m_imageData = this.LoadExistingImageData(this.m_imageDataId);
					if (this.m_imageData == null)
					{
						this.m_reportElement.RenderingContext.OdpContext.ErrorContext.Register(this.ErrorCodeForSourceType, Severity.Warning, this.m_image.ObjectType, this.m_image.ObjectName, this.m_image.ImageDataPropertyName, new string[] { this.m_imageDataId });
					}
					this.m_imageDataId = null;
				}
				return this.m_imageData;
			}
		}

		// Token: 0x17001561 RID: 5473
		// (get) Token: 0x06002C35 RID: 11317 RVA: 0x000CBFF8 File Offset: 0x000CA1F8
		public string ImageDataId
		{
			get
			{
				if (this.m_imageDataId == null)
				{
					this.m_imageDataId = this.GetImageDataId();
				}
				return this.m_imageDataId;
			}
		}

		// Token: 0x17001562 RID: 5474
		// (get) Token: 0x06002C36 RID: 11318 RVA: 0x000CC014 File Offset: 0x000CA214
		public string StreamName
		{
			get
			{
				this.EnsureCacheIsPopulated();
				return this.m_streamName;
			}
		}

		// Token: 0x17001563 RID: 5475
		// (get) Token: 0x06002C37 RID: 11319 RVA: 0x000CC022 File Offset: 0x000CA222
		public List<string> FieldsUsedInValue
		{
			get
			{
				this.EnsureCacheIsPopulated();
				return this.m_fieldsUsedInValue;
			}
		}

		// Token: 0x17001564 RID: 5476
		// (get) Token: 0x06002C38 RID: 11320 RVA: 0x000CC030 File Offset: 0x000CA230
		public bool IsNullImage
		{
			get
			{
				if (this.GetIsNullImage())
				{
					return true;
				}
				this.EnsureCacheIsPopulated();
				return this.m_isNullImage;
			}
		}

		// Token: 0x06002C39 RID: 11321 RVA: 0x000CC048 File Offset: 0x000CA248
		private bool GetIsNullImage()
		{
			return string.IsNullOrEmpty(this.m_image.Value.ExpressionString);
		}

		// Token: 0x06002C3A RID: 11322 RVA: 0x000CC064 File Offset: 0x000CA264
		public void ClearCache()
		{
			this.m_mimeType = null;
			this.m_imageData = null;
			this.m_imageDataId = null;
			this.m_streamName = null;
			this.m_fieldsUsedInValue = null;
			this.m_isNullImage = false;
			this.m_isCachePopulated = false;
		}

		// Token: 0x06002C3B RID: 11323 RVA: 0x000CC097 File Offset: 0x000CA297
		private void EnsureCacheIsPopulated()
		{
			if (this.m_isCachePopulated)
			{
				return;
			}
			this.m_isCachePopulated = true;
			this.m_streamName = this.GetCalculatedImageProperties(out this.m_mimeType, out this.m_imageData, out this.m_imageDataId, out this.m_fieldsUsedInValue, out this.m_isNullImage);
		}

		// Token: 0x06002C3C RID: 11324 RVA: 0x000CC0D3 File Offset: 0x000CA2D3
		private string GetCalculatedImageProperties(out string mimeType, out byte[] imageData, out string imageDataId, out List<string> fieldsUsedInValue, out bool isNullImage)
		{
			if (this.GetIsNullImage())
			{
				fieldsUsedInValue = null;
				imageDataId = null;
				isNullImage = true;
				return this.m_image.GetTransparentImageProperties(out mimeType, out imageData);
			}
			return this.CalculateImageProperties(out mimeType, out imageData, out imageDataId, out fieldsUsedInValue, out isNullImage);
		}

		// Token: 0x06002C3D RID: 11325
		protected abstract string CalculateImageProperties(out string mimeType, out byte[] imageData, out string imageDataId, out List<string> fieldsUsedInValue, out bool isNullImage);

		// Token: 0x06002C3E RID: 11326 RVA: 0x000CC103 File Offset: 0x000CA303
		protected virtual string GetImageDataId()
		{
			this.EnsureCacheIsPopulated();
			return this.m_imageDataId;
		}

		// Token: 0x06002C3F RID: 11327
		protected abstract byte[] LoadExistingImageData(string imageDataId);

		// Token: 0x17001565 RID: 5477
		// (get) Token: 0x06002C40 RID: 11328
		protected abstract ProcessingErrorCode ErrorCodeForSourceType { get; }

		// Token: 0x17001566 RID: 5478
		// (get) Token: 0x06002C41 RID: 11329 RVA: 0x000CC111 File Offset: 0x000CA311
		protected ImageCacheManager CacheManager
		{
			get
			{
				return this.m_reportElement.RenderingContext.OdpContext.ImageCacheManager;
			}
		}

		// Token: 0x06002C42 RID: 11330 RVA: 0x000CC128 File Offset: 0x000CA328
		protected string GetTransparentImageProperties(out string mimeType, out byte[] imageData, out string imageDataId)
		{
			imageDataId = null;
			return this.m_image.GetTransparentImageProperties(out mimeType, out imageData);
		}

		// Token: 0x06002C43 RID: 11331 RVA: 0x000CC13A File Offset: 0x000CA33A
		protected string GetErrorImageProperties(out string mimeType, out byte[] imageData, out string imageDataId)
		{
			mimeType = null;
			imageData = null;
			imageDataId = null;
			return null;
		}

		// Token: 0x06002C44 RID: 11332 RVA: 0x000CC146 File Offset: 0x000CA346
		public string LoadAndCacheTransparentImage(out string mimeType, out byte[] imageData)
		{
			imageData = new byte[ImageDataHandler.TransparentImageBytes.Length];
			Array.Copy(ImageDataHandler.TransparentImageBytes, imageData, imageData.Length);
			mimeType = ImageDataHandler.TransparentImageMimeType;
			return this.CacheManager.EnsureTransparentImageIsCached(mimeType, imageData);
		}

		// Token: 0x040017D9 RID: 6105
		protected readonly IBaseImage m_image;

		// Token: 0x040017DA RID: 6106
		protected readonly ReportElement m_reportElement;

		// Token: 0x040017DB RID: 6107
		private string m_mimeType;

		// Token: 0x040017DC RID: 6108
		private byte[] m_imageData;

		// Token: 0x040017DD RID: 6109
		private string m_imageDataId;

		// Token: 0x040017DE RID: 6110
		private string m_streamName;

		// Token: 0x040017DF RID: 6111
		private List<string> m_fieldsUsedInValue;

		// Token: 0x040017E0 RID: 6112
		private bool m_isNullImage;

		// Token: 0x040017E1 RID: 6113
		private bool m_isCachePopulated;

		// Token: 0x040017E2 RID: 6114
		private static readonly byte[] TransparentImageBytes = new byte[]
		{
			71, 73, 70, 56, 57, 97, 1, 0, 1, 0,
			240, 0, 0, 219, 223, 239, 0, 0, 0, 33,
			249, 4, 1, 0, 0, 0, 0, 44, 0, 0,
			0, 0, 1, 0, 1, 0, 0, 2, 2, 68,
			1, 0, 59
		};

		// Token: 0x040017E3 RID: 6115
		private static readonly string TransparentImageMimeType = "image/gif";
	}
}
