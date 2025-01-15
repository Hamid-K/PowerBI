using System;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x020030B9 RID: 12473
	internal static class ImagePartTypeInfo
	{
		// Token: 0x0601B1D3 RID: 111059 RVA: 0x0036C088 File Offset: 0x0036A288
		internal static string GetContentType(ImagePartType imageType)
		{
			switch (imageType)
			{
			case ImagePartType.Bmp:
				return "image/bmp";
			case ImagePartType.Gif:
				return "image/gif";
			case ImagePartType.Png:
				return "image/png";
			case ImagePartType.Tiff:
				return "image/tiff";
			case ImagePartType.Icon:
				return "image/x-icon";
			case ImagePartType.Pcx:
				return "image/x-pcx";
			case ImagePartType.Jpeg:
				return "image/jpeg";
			case ImagePartType.Emf:
				return "image/x-emf";
			case ImagePartType.Wmf:
				return "image/x-wmf";
			default:
				throw new ArgumentOutOfRangeException("imageType");
			}
		}

		// Token: 0x0601B1D4 RID: 111060 RVA: 0x0036C104 File Offset: 0x0036A304
		internal static string GetTargetExtension(ImagePartType imageType)
		{
			switch (imageType)
			{
			case ImagePartType.Bmp:
				return ".bmp";
			case ImagePartType.Gif:
				return ".gif";
			case ImagePartType.Png:
				return ".png";
			case ImagePartType.Tiff:
				return ".tiff";
			case ImagePartType.Icon:
				return ".ico";
			case ImagePartType.Pcx:
				return ".pcx";
			case ImagePartType.Jpeg:
				return ".jpg";
			case ImagePartType.Emf:
				return ".emf";
			case ImagePartType.Wmf:
				return ".wmf";
			default:
				return ".image";
			}
		}
	}
}
