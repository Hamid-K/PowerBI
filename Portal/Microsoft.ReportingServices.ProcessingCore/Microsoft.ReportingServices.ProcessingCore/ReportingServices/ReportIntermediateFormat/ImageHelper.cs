using System;
using System.IO;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000409 RID: 1033
	internal static class ImageHelper
	{
		// Token: 0x06002C58 RID: 11352 RVA: 0x000CC808 File Offset: 0x000CAA08
		internal static string StoreImageDataInChunk(ReportProcessing.ReportChunkTypes chunkType, byte[] imageData, string mimeType, OnDemandMetadata odpMetadata, IChunkFactory chunkFactory)
		{
			string text = ImageHelper.GenerateImageStreamName();
			ReportSnapshot reportSnapshot = odpMetadata.ReportSnapshot;
			using (Stream stream = chunkFactory.CreateChunk(text, chunkType, mimeType))
			{
				stream.Write(imageData, 0, imageData.Length);
			}
			return text;
		}

		// Token: 0x06002C59 RID: 11353 RVA: 0x000CC858 File Offset: 0x000CAA58
		internal static string GenerateImageStreamName()
		{
			return Guid.NewGuid().ToString("N");
		}
	}
}
