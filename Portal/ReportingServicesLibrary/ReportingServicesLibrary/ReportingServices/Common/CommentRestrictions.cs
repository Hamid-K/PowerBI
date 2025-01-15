using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x02000378 RID: 888
	internal static class CommentRestrictions
	{
		// Token: 0x06001D12 RID: 7442 RVA: 0x00075FA9 File Offset: 0x000741A9
		public static bool IsValidCommentAttachmentMimeType(string mimeType)
		{
			return CommentRestrictions.ValidMimeTypes.Contains((mimeType != null) ? mimeType.ToLowerInvariant() : null);
		}

		// Token: 0x06001D13 RID: 7443 RVA: 0x00075FC4 File Offset: 0x000741C4
		public static void EnsureValidCommentAttachment(string mimeType, string itemName)
		{
			string extension = Path.GetExtension(itemName);
			if (!CommentRestrictions.ValidFileFormats.Contains((extension != null) ? extension.ToLowerInvariant() : null))
			{
				throw new ResourceFileFormatNotAllowedException("Invalid File format for comment attachment");
			}
			if (!CommentRestrictions.IsValidCommentAttachmentMimeType(mimeType))
			{
				throw new ResourceMimeTypeNotAllowedException("Invalid Mime Type for comment attachment");
			}
		}

		// Token: 0x06001D14 RID: 7444 RVA: 0x00076010 File Offset: 0x00074210
		public static void EnsureValidCommentAttachmentForDownload(string itemName)
		{
			string text = Path.GetExtension(itemName).ToLowerInvariant();
			if (!CommentRestrictions.ValidFileFormats.Contains(text) && !CommentRestrictions.AdditionalValidFileFormatsForDownload.Contains(text))
			{
				throw new ResourceFileFormatNotAllowedException("Invalid File format for comment attachment");
			}
		}

		// Token: 0x04000C56 RID: 3158
		private static IReadOnlyCollection<string> ValidMimeTypes = new HashSet<string>(new string[] { "image/bmp", "image/gif", "image/jpeg", "image/png", "image/tiff" });

		// Token: 0x04000C57 RID: 3159
		private static IReadOnlyCollection<string> ValidFileFormats = new HashSet<string>(new string[] { ".tif", ".tiff", ".png", ".jpg", ".jpeg", ".gif", ".bmp" });

		// Token: 0x04000C58 RID: 3160
		private static IReadOnlyCollection<string> AdditionalValidFileFormatsForDownload = new HashSet<string>(new string[] { ".pdf" });
	}
}
