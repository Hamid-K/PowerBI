using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x0200081C RID: 2076
	internal sealed class SnapshotImageCacheManager : ImageCacheManager
	{
		// Token: 0x0600735A RID: 29530 RVA: 0x001DEF81 File Offset: 0x001DD181
		public SnapshotImageCacheManager(OnDemandMetadata odpMetadata, IChunkFactory chunkFactory)
			: base(odpMetadata, chunkFactory)
		{
		}

		// Token: 0x0600735B RID: 29531 RVA: 0x001DEF8B File Offset: 0x001DD18B
		protected override bool ExtractCachedExternalImagePropertiesIfValid(Microsoft.ReportingServices.ReportIntermediateFormat.ImageInfo imageInfo, out byte[] imageData, out string mimeType, out string streamName)
		{
			imageData = imageInfo.GetCachedImageData();
			streamName = imageInfo.StreamName;
			mimeType = imageInfo.MimeType;
			return true;
		}

		// Token: 0x0600735C RID: 29532 RVA: 0x001DEFA8 File Offset: 0x001DD1A8
		public override string AddExternalImage(string value, byte[] imageData, string mimeType)
		{
			Global.Tracer.Assert(imageData != null, "Missing imageData for external image");
			string text = ImageHelper.StoreImageDataInChunk(ReportProcessing.ReportChunkTypes.Image, imageData, mimeType, this.m_odpMetadata, this.m_chunkFactory);
			Microsoft.ReportingServices.ReportIntermediateFormat.ImageInfo imageInfo = new Microsoft.ReportingServices.ReportIntermediateFormat.ImageInfo(text, mimeType);
			imageInfo.SetCachedImageData(imageData);
			this.m_odpMetadata.AddExternalImage(value, imageInfo);
			return text;
		}

		// Token: 0x0600735D RID: 29533 RVA: 0x001DEFF8 File Offset: 0x001DD1F8
		public override byte[] GetCachedExternalImageBytes(string value)
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.ImageInfo imageInfo;
			bool flag = this.m_odpMetadata.TryGetExternalImage(value, out imageInfo);
			Global.Tracer.Assert(flag, "Missing ImageInfo for external image");
			byte[] array = this.ReadImageDataFromChunk(imageInfo.StreamName, ReportProcessing.ReportChunkTypes.Image);
			if (array != null)
			{
				imageInfo.SetCachedImageData(array);
			}
			return array;
		}

		// Token: 0x0600735E RID: 29534 RVA: 0x001DF040 File Offset: 0x001DD240
		public override bool TryGetEmbeddedImage(string value, Microsoft.ReportingServices.OnDemandReportRendering.Image.EmbeddingModes embeddingMode, OnDemandProcessingContext odpContext, out byte[] imageData, out string mimeType, out string streamName)
		{
			Global.Tracer.Assert(embeddingMode == Microsoft.ReportingServices.OnDemandReportRendering.Image.EmbeddingModes.Inline, "Invalid image embedding mode");
			Microsoft.ReportingServices.ReportIntermediateFormat.ImageInfo imageInfo = null;
			Dictionary<string, Microsoft.ReportingServices.ReportIntermediateFormat.ImageInfo> embeddedImages = odpContext.EmbeddedImages;
			if (embeddedImages == null || !embeddedImages.TryGetValue(value, out imageInfo))
			{
				imageData = null;
				mimeType = null;
				streamName = null;
				return false;
			}
			imageData = imageInfo.GetCachedImageData();
			streamName = imageInfo.StreamName;
			mimeType = imageInfo.MimeType;
			return true;
		}

		// Token: 0x0600735F RID: 29535 RVA: 0x001DF0A4 File Offset: 0x001DD2A4
		public override byte[] GetCachedEmbeddedImageBytes(string imageName, OnDemandProcessingContext odpContext)
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.ImageInfo imageInfo;
			bool flag = odpContext.EmbeddedImages.TryGetValue(imageName, out imageInfo);
			Global.Tracer.Assert(flag, "Missing ImageInfo for embedded image");
			byte[] array = this.ReadImageDataFromChunk(imageInfo.StreamName, ReportProcessing.ReportChunkTypes.StaticImage);
			if (array != null)
			{
				imageInfo.SetCachedImageData(array);
			}
			return array;
		}

		// Token: 0x06007360 RID: 29536 RVA: 0x001DF0E9 File Offset: 0x001DD2E9
		public override bool TryGetDatabaseImage(string uniqueName, out string streamName)
		{
			return this.m_odpMetadata.ReportSnapshot.TryGetImageChunkName(uniqueName, out streamName);
		}

		// Token: 0x06007361 RID: 29537 RVA: 0x001DF100 File Offset: 0x001DD300
		public override string AddDatabaseImage(string uniqueName, byte[] imageData, string mimeType, OnDemandProcessingContext odpContext)
		{
			if (odpContext.IsPageHeaderFooter)
			{
				return null;
			}
			string text = ImageHelper.StoreImageDataInChunk(ReportProcessing.ReportChunkTypes.Image, imageData, mimeType, this.m_odpMetadata, this.m_chunkFactory);
			this.m_odpMetadata.ReportSnapshot.AddImageChunkName(uniqueName, text);
			return text;
		}

		// Token: 0x06007362 RID: 29538 RVA: 0x001DF140 File Offset: 0x001DD340
		public override byte[] GetCachedDatabaseImageBytes(string chunkName)
		{
			return this.ReadImageDataFromChunk(chunkName, ReportProcessing.ReportChunkTypes.Image);
		}

		// Token: 0x06007363 RID: 29539 RVA: 0x001DF14C File Offset: 0x001DD34C
		public override string EnsureTransparentImageIsCached(string mimeType, byte[] imageData)
		{
			string text = this.m_odpMetadata.TransparentImageChunkName;
			if (text == null)
			{
				text = ImageHelper.StoreImageDataInChunk(ReportProcessing.ReportChunkTypes.Image, imageData, mimeType, this.m_odpMetadata, this.m_chunkFactory);
				this.m_odpMetadata.TransparentImageChunkName = text;
			}
			return text;
		}

		// Token: 0x06007364 RID: 29540 RVA: 0x001DF18C File Offset: 0x001DD38C
		private byte[] ReadImageDataFromChunk(string chunkName, ReportProcessing.ReportChunkTypes chunkType)
		{
			byte[] array = null;
			string text;
			Stream chunk = this.m_chunkFactory.GetChunk(chunkName, chunkType, ChunkMode.Open, out text);
			Global.Tracer.Assert(chunk != null, "Could not find expected image data chunk.  Name='{0}', Type={1}", new object[] { chunkName, chunkType });
			using (chunk)
			{
				array = StreamSupport.ReadToEndUsingLength(chunk);
			}
			return array;
		}
	}
}
