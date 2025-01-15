using System;
using System.IO;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x02000072 RID: 114
	internal sealed class InternalImage : ImageBase
	{
		// Token: 0x06000704 RID: 1796 RVA: 0x0001AD2A File Offset: 0x00018F2A
		internal InternalImage(Image.SourceType imgType, string mimeType, object valueObject, RenderingContext rc)
		{
			this.m_imageType = imgType;
			this.m_MIMEType = mimeType;
			this.m_valueObject = valueObject;
			this.m_renderingContext = rc;
			this.m_transparent = false;
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x0001AD58 File Offset: 0x00018F58
		internal InternalImage(Image.SourceType imgType, string mimeType, object valueObject, RenderingContext rc, bool brokenImage, ImageMapAreaInstanceList imageMapAreas)
		{
			this.m_imageType = imgType;
			this.m_MIMEType = mimeType;
			this.m_valueObject = valueObject;
			this.m_renderingContext = rc;
			this.m_transparent = !brokenImage && valueObject == null;
			if (!brokenImage)
			{
				this.m_imageMapAreas = imageMapAreas;
			}
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06000706 RID: 1798 RVA: 0x0001ADA8 File Offset: 0x00018FA8
		internal byte[] ImageData
		{
			get
			{
				if (this.m_imageData != null)
				{
					return this.m_imageData;
				}
				byte[] array = ((this.m_imageDataRef != null) ? ((byte[])this.m_imageDataRef.Target) : null);
				if (array == null)
				{
					ImageInfo imageInfo = null;
					Image.SourceType imageType = this.m_imageType;
					if (imageType != Image.SourceType.External)
					{
						if (imageType == Image.SourceType.Embedded)
						{
							string imageValue = this.ImageValue;
							if (imageValue != null && this.m_renderingContext.EmbeddedImages != null)
							{
								imageInfo = this.m_renderingContext.EmbeddedImages[imageValue];
							}
						}
					}
					else
					{
						string imageValue2 = this.ImageValue;
						if (imageValue2 != null)
						{
							imageInfo = this.m_renderingContext.ImageStreamNames[imageValue2];
						}
					}
					if (imageInfo != null && imageInfo.ImageDataRef != null)
					{
						this.m_imageDataRef = imageInfo.ImageDataRef;
						array = (byte[])this.m_imageDataRef.Target;
					}
					if (array == null)
					{
						string text;
						this.GetImageData(out array, out text);
						if (this.m_renderingContext.CacheState)
						{
							this.m_imageData = array;
							this.m_MIMEType = text;
						}
						else
						{
							this.m_imageDataRef = new WeakReference(array);
							if (imageInfo != null)
							{
								imageInfo.ImageDataRef = this.m_imageDataRef;
							}
						}
					}
				}
				return array;
			}
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x06000707 RID: 1799 RVA: 0x0001AEB4 File Offset: 0x000190B4
		internal string MIMEType
		{
			get
			{
				string mimetype = this.m_MIMEType;
				if (mimetype == null)
				{
					this.GetImageMimeType(out mimetype);
					if (this.m_renderingContext.CacheState)
					{
						this.m_MIMEType = mimetype;
					}
				}
				return mimetype;
			}
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x06000708 RID: 1800 RVA: 0x0001AEE8 File Offset: 0x000190E8
		internal string StreamName
		{
			get
			{
				string streamName = this.m_streamName;
				if (streamName == null)
				{
					string text = null;
					this.GetImageInfo(out streamName, out text);
					if (this.m_renderingContext.CacheState)
					{
						this.m_streamName = streamName;
					}
				}
				return streamName;
			}
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x06000709 RID: 1801 RVA: 0x0001AF20 File Offset: 0x00019120
		internal ImageMapAreaInstanceList ImageMapAreaInstances
		{
			get
			{
				return this.m_imageMapAreas;
			}
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x0600070A RID: 1802 RVA: 0x0001AF28 File Offset: 0x00019128
		private string ImageValue
		{
			get
			{
				return this.m_valueObject as string;
			}
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x0600070B RID: 1803 RVA: 0x0001AF35 File Offset: 0x00019135
		private ImageData Data
		{
			get
			{
				return this.m_valueObject as ImageData;
			}
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x0001AF44 File Offset: 0x00019144
		private static void ReadStream(Stream input, out byte[] streamContents)
		{
			if (input == null)
			{
				streamContents = null;
				return;
			}
			int num = 1024;
			using (MemoryStream memoryStream = new MemoryStream(num))
			{
				byte[] array = new byte[num];
				int num2;
				while ((num2 = input.Read(array, 0, num)) > 0)
				{
					memoryStream.Write(array, 0, num2);
				}
				streamContents = memoryStream.ToArray();
			}
		}

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x0600070D RID: 1805 RVA: 0x0001AFAC File Offset: 0x000191AC
		internal static byte[] TransparentImage
		{
			get
			{
				if (InternalImage.m_transparentImage == null)
				{
					MemoryStream memoryStream = new MemoryStream(45);
					ReportProcessing.RuntimeRICollection.FetchTransparentImage(memoryStream);
					InternalImage.m_transparentImage = memoryStream.ToArray();
				}
				return InternalImage.m_transparentImage;
			}
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x0001AFD4 File Offset: 0x000191D4
		private void GetImageData(out byte[] imageData, out string mimeType)
		{
			if (this.m_transparent)
			{
				mimeType = "image/gif";
				imageData = InternalImage.TransparentImage;
				return;
			}
			string streamName = this.StreamName;
			if (streamName != null)
			{
				using (Stream stream = this.m_renderingContext.GetChunkCallback(streamName, ReportProcessing.ReportChunkTypes.Image, out mimeType))
				{
					InternalImage.ReadStream(stream, out imageData);
					return;
				}
			}
			imageData = null;
			mimeType = null;
			if (this.GetUrlString() == null)
			{
				ImageData data = this.Data;
				if (data != null)
				{
					imageData = data.Data;
					mimeType = data.MIMEType;
				}
			}
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x0001B064 File Offset: 0x00019264
		private void GetImageInfo(out string streamName, out string mimeType)
		{
			streamName = null;
			mimeType = null;
			switch (this.m_imageType)
			{
			case Image.SourceType.External:
			{
				string imageValue = this.ImageValue;
				if (imageValue != null)
				{
					ImageInfo imageInfo = this.m_renderingContext.ImageStreamNames[imageValue];
					if (imageInfo != null)
					{
						streamName = imageInfo.StreamName;
						mimeType = imageInfo.MimeType;
						return;
					}
				}
				break;
			}
			case Image.SourceType.Embedded:
			{
				string imageValue2 = this.ImageValue;
				if (imageValue2 != null && this.m_renderingContext.EmbeddedImages != null)
				{
					ImageInfo imageInfo2 = this.m_renderingContext.EmbeddedImages[imageValue2];
					if (imageInfo2 != null)
					{
						streamName = imageInfo2.StreamName;
						mimeType = imageInfo2.MimeType;
						return;
					}
				}
				break;
			}
			case Image.SourceType.Database:
				streamName = this.ImageValue;
				break;
			default:
				return;
			}
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x0001B10C File Offset: 0x0001930C
		private void GetImageMimeType(out string mimeType)
		{
			if (this.m_transparent)
			{
				mimeType = "image/gif";
				return;
			}
			string text = null;
			this.GetImageInfo(out text, out mimeType);
			if (mimeType == null)
			{
				if (text != null)
				{
					mimeType = this.m_renderingContext.GetChunkMimeType(text, ReportProcessing.ReportChunkTypes.Image);
					return;
				}
				mimeType = null;
				if (this.GetUrlString() == null)
				{
					ImageData data = this.Data;
					if (data != null)
					{
						mimeType = data.MIMEType;
					}
				}
			}
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x0001B16D File Offset: 0x0001936D
		private string GetUrlString()
		{
			if (this.m_imageType != Image.SourceType.External)
			{
				return null;
			}
			return this.ImageValue;
		}

		// Token: 0x040001F5 RID: 501
		private Image.SourceType m_imageType;

		// Token: 0x040001F6 RID: 502
		private object m_valueObject;

		// Token: 0x040001F7 RID: 503
		private RenderingContext m_renderingContext;

		// Token: 0x040001F8 RID: 504
		private bool m_transparent;

		// Token: 0x040001F9 RID: 505
		private static byte[] m_transparentImage;

		// Token: 0x040001FA RID: 506
		private byte[] m_imageData;

		// Token: 0x040001FB RID: 507
		private string m_MIMEType;

		// Token: 0x040001FC RID: 508
		private WeakReference m_imageDataRef;

		// Token: 0x040001FD RID: 509
		private string m_streamName;

		// Token: 0x040001FE RID: 510
		private ImageMapAreaInstanceList m_imageMapAreas;
	}
}
