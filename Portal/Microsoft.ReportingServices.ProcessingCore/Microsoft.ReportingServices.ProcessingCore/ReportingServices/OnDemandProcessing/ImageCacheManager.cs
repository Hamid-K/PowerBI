using System;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x0200081B RID: 2075
	internal abstract class ImageCacheManager
	{
		// Token: 0x0600734E RID: 29518 RVA: 0x001DEEF6 File Offset: 0x001DD0F6
		public ImageCacheManager(OnDemandMetadata odpMetadata, IChunkFactory chunkFactory)
		{
			this.m_odpMetadata = odpMetadata;
			this.m_chunkFactory = chunkFactory;
		}

		// Token: 0x0600734F RID: 29519 RVA: 0x001DEF0C File Offset: 0x001DD10C
		public bool TryGetExternalImage(string value, out byte[] imageData, out string mimeType, out string streamName, out bool wasError)
		{
			imageData = null;
			mimeType = null;
			streamName = null;
			Microsoft.ReportingServices.ReportIntermediateFormat.ImageInfo imageInfo;
			if (!this.m_odpMetadata.TryGetExternalImage(value, out imageInfo))
			{
				wasError = false;
				return false;
			}
			if (imageInfo.ErrorOccurred)
			{
				wasError = true;
				return true;
			}
			wasError = false;
			return this.ExtractCachedExternalImagePropertiesIfValid(imageInfo, out imageData, out mimeType, out streamName);
		}

		// Token: 0x06007350 RID: 29520
		protected abstract bool ExtractCachedExternalImagePropertiesIfValid(Microsoft.ReportingServices.ReportIntermediateFormat.ImageInfo imageInfo, out byte[] imageData, out string mimeType, out string streamName);

		// Token: 0x06007351 RID: 29521
		public abstract string AddExternalImage(string value, byte[] imageData, string mimeType);

		// Token: 0x06007352 RID: 29522
		public abstract byte[] GetCachedExternalImageBytes(string value);

		// Token: 0x06007353 RID: 29523 RVA: 0x001DEF58 File Offset: 0x001DD158
		public void AddFailedExternalImage(string value)
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.ImageInfo imageInfo = new Microsoft.ReportingServices.ReportIntermediateFormat.ImageInfo(null, null);
			imageInfo.ErrorOccurred = true;
			this.m_odpMetadata.AddExternalImage(value, imageInfo);
		}

		// Token: 0x06007354 RID: 29524
		public abstract bool TryGetEmbeddedImage(string value, Microsoft.ReportingServices.OnDemandReportRendering.Image.EmbeddingModes embeddingMode, OnDemandProcessingContext odpContext, out byte[] imageData, out string mimeType, out string streamName);

		// Token: 0x06007355 RID: 29525
		public abstract byte[] GetCachedEmbeddedImageBytes(string imageName, OnDemandProcessingContext odpContext);

		// Token: 0x06007356 RID: 29526
		public abstract bool TryGetDatabaseImage(string uniqueName, out string streamName);

		// Token: 0x06007357 RID: 29527
		public abstract string AddDatabaseImage(string uniqueName, byte[] imageData, string mimeType, OnDemandProcessingContext odpContext);

		// Token: 0x06007358 RID: 29528
		public abstract byte[] GetCachedDatabaseImageBytes(string chunkName);

		// Token: 0x06007359 RID: 29529
		public abstract string EnsureTransparentImageIsCached(string mimeType, byte[] imageData);

		// Token: 0x04003B0C RID: 15116
		protected readonly OnDemandMetadata m_odpMetadata;

		// Token: 0x04003B0D RID: 15117
		protected readonly IChunkFactory m_chunkFactory;
	}
}
