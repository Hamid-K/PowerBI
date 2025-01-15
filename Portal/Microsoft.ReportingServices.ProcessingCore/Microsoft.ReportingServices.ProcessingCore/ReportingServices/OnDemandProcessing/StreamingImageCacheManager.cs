using System;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x0200081D RID: 2077
	internal sealed class StreamingImageCacheManager : ImageCacheManager
	{
		// Token: 0x06007365 RID: 29541 RVA: 0x001DF1F8 File Offset: 0x001DD3F8
		internal StreamingImageCacheManager(OnDemandMetadata odpMetadata, IChunkFactory chunkFactory)
			: base(odpMetadata, chunkFactory)
		{
		}

		// Token: 0x06007366 RID: 29542 RVA: 0x001DF202 File Offset: 0x001DD402
		protected override bool ExtractCachedExternalImagePropertiesIfValid(Microsoft.ReportingServices.ReportIntermediateFormat.ImageInfo imageInfo, out byte[] imageData, out string mimeType, out string streamName)
		{
			imageData = imageInfo.GetCachedImageData();
			if (imageData != null)
			{
				streamName = imageInfo.StreamName;
				mimeType = imageInfo.MimeType;
				return true;
			}
			streamName = null;
			mimeType = null;
			return false;
		}

		// Token: 0x06007367 RID: 29543 RVA: 0x001DF22C File Offset: 0x001DD42C
		public override string AddExternalImage(string value, byte[] imageData, string mimeType)
		{
			Global.Tracer.Assert(imageData != null, "Missing imageData for external image");
			string text = ImageHelper.GenerateImageStreamName();
			Microsoft.ReportingServices.ReportIntermediateFormat.ImageInfo imageInfo;
			if (!this.m_odpMetadata.TryGetExternalImage(value, out imageInfo))
			{
				imageInfo = new Microsoft.ReportingServices.ReportIntermediateFormat.ImageInfo(text, mimeType);
				this.m_odpMetadata.AddExternalImage(value, imageInfo);
			}
			imageInfo.SetCachedImageData(imageData);
			return text;
		}

		// Token: 0x06007368 RID: 29544 RVA: 0x001DF27F File Offset: 0x001DD47F
		public override byte[] GetCachedExternalImageBytes(string value)
		{
			Global.Tracer.Assert(false, "Chunks are not supported in this processing mode.");
			throw new InvalidOperationException("Chunks are not supported in this processing mode.");
		}

		// Token: 0x06007369 RID: 29545 RVA: 0x001DF29B File Offset: 0x001DD49B
		public override bool TryGetEmbeddedImage(string value, Microsoft.ReportingServices.OnDemandReportRendering.Image.EmbeddingModes embeddingMode, OnDemandProcessingContext odpContext, out byte[] imageData, out string mimeType, out string streamName)
		{
			if (embeddingMode == Microsoft.ReportingServices.OnDemandReportRendering.Image.EmbeddingModes.Package)
			{
				imageData = null;
				mimeType = null;
				streamName = null;
				return true;
			}
			Global.Tracer.Assert(false, "Embedded Images are not supported in Streaming ODP mode.");
			throw new InvalidOperationException("Embedded Images are not supported in Streaming ODP mode.");
		}

		// Token: 0x0600736A RID: 29546 RVA: 0x001DF2C9 File Offset: 0x001DD4C9
		public override byte[] GetCachedEmbeddedImageBytes(string imageName, OnDemandProcessingContext odpContext)
		{
			Global.Tracer.Assert(false, "Chunks are not supported in this processing mode.");
			throw new InvalidOperationException("Chunks are not supported in this processing mode.");
		}

		// Token: 0x0600736B RID: 29547 RVA: 0x001DF2E5 File Offset: 0x001DD4E5
		public override bool TryGetDatabaseImage(string uniqueName, out string streamName)
		{
			streamName = null;
			return false;
		}

		// Token: 0x0600736C RID: 29548 RVA: 0x001DF2EB File Offset: 0x001DD4EB
		public override string AddDatabaseImage(string uniqueName, byte[] imageData, string mimeType, OnDemandProcessingContext odpContext)
		{
			return ImageHelper.GenerateImageStreamName();
		}

		// Token: 0x0600736D RID: 29549 RVA: 0x001DF2F2 File Offset: 0x001DD4F2
		public override byte[] GetCachedDatabaseImageBytes(string chunkName)
		{
			Global.Tracer.Assert(false, "Chunks are not supported in this processing mode.");
			throw new InvalidOperationException("Chunks are not supported in this processing mode.");
		}

		// Token: 0x0600736E RID: 29550 RVA: 0x001DF30E File Offset: 0x001DD50E
		public override string EnsureTransparentImageIsCached(string mimeType, byte[] imageData)
		{
			return ImageHelper.GenerateImageStreamName();
		}
	}
}
