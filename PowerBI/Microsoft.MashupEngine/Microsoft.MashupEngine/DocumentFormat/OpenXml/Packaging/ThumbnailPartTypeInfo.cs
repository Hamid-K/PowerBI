using System;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x020030C1 RID: 12481
	internal static class ThumbnailPartTypeInfo
	{
		// Token: 0x0601B1DB RID: 111067 RVA: 0x0036C298 File Offset: 0x0036A498
		internal static string GetContentType(ThumbnailPartType imageType)
		{
			switch (imageType)
			{
			case ThumbnailPartType.Jpeg:
				return "image/jpeg";
			case ThumbnailPartType.Emf:
				return "image/x-emf";
			case ThumbnailPartType.Wmf:
				return "image/x-wmf";
			default:
				throw new ArgumentOutOfRangeException("imageType");
			}
		}

		// Token: 0x0601B1DC RID: 111068 RVA: 0x0036C2D8 File Offset: 0x0036A4D8
		internal static string GetTargetExtension(ThumbnailPartType imageType)
		{
			switch (imageType)
			{
			case ThumbnailPartType.Jpeg:
				return ".jpg";
			case ThumbnailPartType.Emf:
				return ".emf";
			case ThumbnailPartType.Wmf:
				return ".wmf";
			default:
				return ".image";
			}
		}
	}
}
