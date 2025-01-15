using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020002BB RID: 699
	public abstract class DynamicImageInstance : DataRegionInstance
	{
		// Token: 0x06001AA2 RID: 6818 RVA: 0x0006B206 File Offset: 0x00069406
		internal DynamicImageInstance(DataRegion reportItemDef)
			: base(reportItemDef)
		{
		}

		// Token: 0x06001AA3 RID: 6819 RVA: 0x0006B225 File Offset: 0x00069425
		public virtual void SetDpi(int xDpi, int yDpi)
		{
			this.m_dpiX = (float)xDpi;
			this.m_dpiY = (float)yDpi;
		}

		// Token: 0x06001AA4 RID: 6820 RVA: 0x0006B237 File Offset: 0x00069437
		public void SetSize(double width, double height)
		{
			this.m_widthOverride = new double?(width);
			this.m_heightOverride = new double?(height);
		}

		// Token: 0x06001AA5 RID: 6821 RVA: 0x0006B254 File Offset: 0x00069454
		public Stream GetImage()
		{
			bool flag;
			return this.GetImage(DynamicImageInstance.ImageType.PNG, out flag);
		}

		// Token: 0x06001AA6 RID: 6822 RVA: 0x0006B26C File Offset: 0x0006946C
		public Stream GetImage(DynamicImageInstance.ImageType type)
		{
			bool flag;
			return this.GetImage(type, out flag);
		}

		// Token: 0x06001AA7 RID: 6823 RVA: 0x0006B282 File Offset: 0x00069482
		public Stream GetImage(out ActionInfoWithDynamicImageMapCollection actionImageMaps)
		{
			return this.GetImage(DynamicImageInstance.ImageType.PNG, out actionImageMaps);
		}

		// Token: 0x06001AA8 RID: 6824 RVA: 0x0006B28C File Offset: 0x0006948C
		public virtual Stream GetImage(DynamicImageInstance.ImageType type, out ActionInfoWithDynamicImageMapCollection actionImageMaps)
		{
			Stream stream2;
			try
			{
				Stream stream;
				this.GetImage(type, out actionImageMaps, out stream);
				stream2 = stream;
			}
			catch (Exception ex)
			{
				actionImageMaps = null;
				stream2 = this.CreateExceptionImage(ex);
			}
			return stream2;
		}

		// Token: 0x06001AA9 RID: 6825 RVA: 0x0006B2C8 File Offset: 0x000694C8
		protected virtual Stream GetImage(DynamicImageInstance.ImageType type, out bool hasImageMap)
		{
			ActionInfoWithDynamicImageMapCollection actionInfoWithDynamicImageMapCollection;
			Stream image = this.GetImage(type, out actionInfoWithDynamicImageMapCollection);
			hasImageMap = actionInfoWithDynamicImageMapCollection != null;
			return image;
		}

		// Token: 0x06001AAA RID: 6826 RVA: 0x0006B2E4 File Offset: 0x000694E4
		protected MemoryStream CreateExceptionImage(Exception exception)
		{
			return DynamicImageInstance.CreateExceptionImage(exception, this.WidthInPixels, this.HeightInPixels, this.m_dpiX, this.m_dpiY);
		}

		// Token: 0x06001AAB RID: 6827 RVA: 0x0006B304 File Offset: 0x00069504
		internal static MemoryStream CreateExceptionImage(Exception exception, int width, int height, float dpiX, float dpiY)
		{
			Bitmap bitmap = null;
			Graphics graphics = null;
			Brush brush = null;
			Brush brush2 = null;
			Pen pen = null;
			Pen pen2 = null;
			Font font = null;
			MemoryStream memoryStream = new MemoryStream();
			try
			{
				bitmap = new Bitmap(width, height);
				bitmap.SetResolution(dpiX, dpiY);
				graphics = Graphics.FromImage(bitmap);
				brush = new SolidBrush(Color.White);
				graphics.FillRectangle(brush, 0, 0, width, height);
				float num = (float)MappingHelper.ToPixels(new ReportSize("1pt"), dpiX);
				float num2 = (float)MappingHelper.ToPixels(new ReportSize("1pt"), dpiY);
				pen = new Pen(Color.Black, num);
				pen2 = new Pen(Color.Black, num2);
				graphics.DrawLine(pen, num, num2, (float)width - num, num2);
				graphics.DrawLine(pen2, (float)width - num, num2, (float)width - num, (float)height - num2);
				graphics.DrawLine(pen, (float)width - num, (float)height - num2, num, (float)height - num2);
				graphics.DrawLine(pen2, num, (float)height - num2, num, num2);
				brush2 = new SolidBrush(Color.Black);
				font = MappingHelper.GetDefaultFont();
				graphics.DrawString(DynamicImageInstance.GetInnerMostException(exception).Message, font, brush2, new RectangleF(num, num2, (float)width - num, (float)height - num2));
				bitmap.Save(memoryStream, ImageFormat.Png);
			}
			catch (Exception ex)
			{
				if (AsynchronousExceptionDetection.IsStoppingException(ex))
				{
					throw;
				}
				Global.Tracer.Trace(TraceLevel.Verbose, ex.Message);
				memoryStream = null;
			}
			finally
			{
				if (brush != null)
				{
					brush.Dispose();
					brush = null;
				}
				if (pen != null)
				{
					pen.Dispose();
					pen = null;
				}
				if (pen2 != null)
				{
					pen2.Dispose();
					pen2 = null;
				}
				if (brush2 != null)
				{
					brush2.Dispose();
					brush2 = null;
				}
				if (font != null)
				{
					font.Dispose();
					font = null;
				}
				if (graphics != null)
				{
					graphics.Dispose();
					graphics = null;
				}
				if (bitmap != null)
				{
					bitmap.Dispose();
					bitmap = null;
				}
			}
			return memoryStream;
		}

		// Token: 0x06001AAC RID: 6828
		protected abstract void GetImage(DynamicImageInstance.ImageType type, out ActionInfoWithDynamicImageMapCollection actionImageMaps, out Stream image);

		// Token: 0x17000F20 RID: 3872
		// (get) Token: 0x06001AAD RID: 6829 RVA: 0x0006B4EC File Offset: 0x000696EC
		protected virtual int WidthInPixels
		{
			get
			{
				return MappingHelper.ToIntPixels(((ReportItem)this.m_reportElementDef).Width, this.m_dpiX);
			}
		}

		// Token: 0x17000F21 RID: 3873
		// (get) Token: 0x06001AAE RID: 6830 RVA: 0x0006B509 File Offset: 0x00069709
		protected virtual int HeightInPixels
		{
			get
			{
				return MappingHelper.ToIntPixels(((ReportItem)this.m_reportElementDef).Height, this.m_dpiX);
			}
		}

		// Token: 0x06001AAF RID: 6831 RVA: 0x0006B528 File Offset: 0x00069728
		private static Exception GetInnerMostException(Exception exception)
		{
			Exception ex = exception;
			while (ex.InnerException != null)
			{
				ex = ex.InnerException;
			}
			return ex;
		}

		// Token: 0x04000D3C RID: 3388
		protected float m_dpiX = 96f;

		// Token: 0x04000D3D RID: 3389
		protected float m_dpiY = 96f;

		// Token: 0x04000D3E RID: 3390
		protected double? m_widthOverride;

		// Token: 0x04000D3F RID: 3391
		protected double? m_heightOverride;

		// Token: 0x02000942 RID: 2370
		public enum ImageType
		{
			// Token: 0x04004031 RID: 16433
			PNG,
			// Token: 0x04004032 RID: 16434
			EMF
		}
	}
}
