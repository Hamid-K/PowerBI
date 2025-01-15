using System;
using System.IO;
using System.Text;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200016E RID: 366
	internal sealed class SilverlightProperties
	{
		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06000D86 RID: 3462 RVA: 0x000311C0 File Offset: 0x0002F3C0
		// (set) Token: 0x06000D87 RID: 3463 RVA: 0x000311C8 File Offset: 0x0002F3C8
		public string StringInitParams
		{
			get
			{
				return this.m_stringInitParams;
			}
			set
			{
				this.m_stringInitParams = value;
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06000D88 RID: 3464 RVA: 0x000311D1 File Offset: 0x0002F3D1
		// (set) Token: 0x06000D89 RID: 3465 RVA: 0x000311D9 File Offset: 0x0002F3D9
		public string StringCulture
		{
			get
			{
				return this.m_stringCulture;
			}
			set
			{
				this.m_stringCulture = value;
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06000D8A RID: 3466 RVA: 0x000311E2 File Offset: 0x0002F3E2
		// (set) Token: 0x06000D8B RID: 3467 RVA: 0x000311EA File Offset: 0x0002F3EA
		public string StringUICulture
		{
			get
			{
				return this.m_stringUICulture;
			}
			set
			{
				this.m_stringUICulture = value;
			}
		}

		// Token: 0x06000D8C RID: 3468 RVA: 0x000311F4 File Offset: 0x0002F3F4
		public SilverlightProperties(string stringSource)
		{
			this.m_stringSource = stringSource;
		}

		// Token: 0x06000D8D RID: 3469 RVA: 0x000312E7 File Offset: 0x0002F4E7
		public void LoadImage(Stream image)
		{
			this.m_image = image;
		}

		// Token: 0x06000D8E RID: 3470 RVA: 0x000312F0 File Offset: 0x0002F4F0
		private void Put(BinaryWriter writer, ulong val)
		{
			writer.Write((uint)val);
		}

		// Token: 0x06000D8F RID: 3471 RVA: 0x000312FC File Offset: 0x0002F4FC
		private void Put(BinaryWriter writer, bool val)
		{
			uint num = (val ? 1U : 0U);
			writer.Write(num);
		}

		// Token: 0x06000D90 RID: 3472 RVA: 0x00031318 File Offset: 0x0002F518
		private void Put(BinaryWriter writer, string val)
		{
			uint length = (uint)val.Length;
			if (length > 0U)
			{
				byte[] bytes = Encoding.Unicode.GetBytes(val);
				writer.Write(bytes.Length + SilverlightProperties.StringNullTerminator.Length);
				writer.Write(bytes);
				writer.Write(SilverlightProperties.StringNullTerminator);
				return;
			}
			writer.Write(length);
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x00031367 File Offset: 0x0002F567
		private void Put(BinaryWriter writer, Stream val)
		{
			if (val.CanSeek)
			{
				val.Seek(0L, SeekOrigin.Begin);
			}
			writer.Write((uint)val.Length);
			SilverlightProperties.WriteStreamToBinaryWriter(writer, val);
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x00031390 File Offset: 0x0002F590
		private static void WriteStreamToBinaryWriter(BinaryWriter writer, Stream val)
		{
			byte[] array = new byte[8192];
			int i = 0;
			int num = (int)val.Length;
			while (i < num)
			{
				int num2 = Math.Min(num - i, array.Length);
				int num3 = val.Read(array, 0, num2);
				i += num3;
				writer.Write(array, 0, num3);
			}
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x000313E0 File Offset: 0x0002F5E0
		public void Serialize(Stream stream)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			uint num = 2304U;
			this.Put(binaryWriter, (ulong)num);
			this.Put(binaryWriter, this.m_cx);
			this.Put(binaryWriter, this.m_cy);
			this.Put(binaryWriter, this.m_stringBackgroundColor);
			this.Put(binaryWriter, this.m_bFrameCounter);
			this.Put(binaryWriter, this.m_bCacheVisualization);
			this.Put(binaryWriter, this.m_bEnableRedrawRegions);
			this.Put(binaryWriter, this.m_stringSource);
			this.Put(binaryWriter, this.m_uMaxFrameRate);
			this.Put(binaryWriter, this.m_bWindowlessMode);
			this.Put(binaryWriter, this.m_stringOnError);
			this.Put(binaryWriter, this.m_stringFullScreenChanged);
			this.Put(binaryWriter, this.m_stringOnResized);
			this.Put(binaryWriter, this.m_stringOnZoomed);
			this.Put(binaryWriter, this.m_stringOnLoaded);
			this.Put(binaryWriter, this.m_stringInitParams);
			this.Put(binaryWriter, this.m_stringCulture);
			this.Put(binaryWriter, this.m_stringUICulture);
			this.Put(binaryWriter, this.m_bHtmlAccessEnabled);
			this.Put(binaryWriter, this.m_bAllowHtmlPopupWindow);
			this.Put(binaryWriter, this.m_stringSplashScreenSource);
			this.Put(binaryWriter, this.m_stringOnSourceDownloadComplete);
			this.Put(binaryWriter, this.m_stringOnSourceDownloadProgressChanged);
			this.Put(binaryWriter, this.m_stringMinVersion);
			this.Put(binaryWriter, this.m_stringAutoUpgrade);
			this.Put(binaryWriter, this.m_enableGPUAcceleration);
			this.Put(binaryWriter, this.m_bAutoZoom);
			this.Put(binaryWriter, this.m_stringAllowNavigation);
			this.Put(binaryWriter, this.m_stringProductId);
			this.Put(binaryWriter, this.m_stringInstanceId);
			this.Put(binaryWriter, this.m_image);
		}

		// Token: 0x0400057B RID: 1403
		private ulong m_cx;

		// Token: 0x0400057C RID: 1404
		private ulong m_cy;

		// Token: 0x0400057D RID: 1405
		private string m_stringBackgroundColor = "";

		// Token: 0x0400057E RID: 1406
		private bool m_bFrameCounter;

		// Token: 0x0400057F RID: 1407
		private bool m_bCacheVisualization;

		// Token: 0x04000580 RID: 1408
		private bool m_bEnableRedrawRegions;

		// Token: 0x04000581 RID: 1409
		private string m_stringSource;

		// Token: 0x04000582 RID: 1410
		private ulong m_uMaxFrameRate = 60UL;

		// Token: 0x04000583 RID: 1411
		private bool m_bWindowlessMode;

		// Token: 0x04000584 RID: 1412
		private string m_stringOnError = "";

		// Token: 0x04000585 RID: 1413
		private string m_stringFullScreenChanged = "";

		// Token: 0x04000586 RID: 1414
		private string m_stringOnResized = "";

		// Token: 0x04000587 RID: 1415
		private string m_stringOnZoomed = "";

		// Token: 0x04000588 RID: 1416
		private string m_stringOnLoaded = "";

		// Token: 0x04000589 RID: 1417
		private string m_stringInitParams = "";

		// Token: 0x0400058A RID: 1418
		private string m_stringCulture = "";

		// Token: 0x0400058B RID: 1419
		private string m_stringUICulture = "";

		// Token: 0x0400058C RID: 1420
		private bool m_bHtmlAccessEnabled = true;

		// Token: 0x0400058D RID: 1421
		private bool m_bAllowHtmlPopupWindow = true;

		// Token: 0x0400058E RID: 1422
		private string m_stringSplashScreenSource = "";

		// Token: 0x0400058F RID: 1423
		private string m_stringOnSourceDownloadComplete = "";

		// Token: 0x04000590 RID: 1424
		private string m_stringOnSourceDownloadProgressChanged = "";

		// Token: 0x04000591 RID: 1425
		private string m_stringMinVersion = "";

		// Token: 0x04000592 RID: 1426
		private string m_stringAutoUpgrade = "";

		// Token: 0x04000593 RID: 1427
		private bool m_enableGPUAcceleration = true;

		// Token: 0x04000594 RID: 1428
		private bool m_bAutoZoom;

		// Token: 0x04000595 RID: 1429
		private string m_stringAllowNavigation = "";

		// Token: 0x04000596 RID: 1430
		private string m_stringProductId = "";

		// Token: 0x04000597 RID: 1431
		private string m_stringInstanceId = "";

		// Token: 0x04000598 RID: 1432
		private Stream m_image;

		// Token: 0x04000599 RID: 1433
		private static byte[] StringNullTerminator = new byte[2];
	}
}
