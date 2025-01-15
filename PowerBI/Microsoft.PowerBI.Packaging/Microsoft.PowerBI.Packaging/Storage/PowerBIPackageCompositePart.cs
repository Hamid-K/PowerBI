using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;

namespace Microsoft.PowerBI.Packaging.Storage
{
	// Token: 0x0200004A RID: 74
	internal sealed class PowerBIPackageCompositePart : BasePowerBIPackagePart
	{
		// Token: 0x06000212 RID: 530 RVA: 0x00007174 File Offset: 0x00005374
		public PowerBIPackageCompositePart(Package package, Uri storagePath, bool isOptional, CompressionOption compressionOption = CompressionOption.Normal)
			: base(package, storagePath, isOptional, compressionOption)
		{
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00007184 File Offset: 0x00005384
		public void SetContent(Uri relativeUri, IStreamablePowerBIPackagePartContent content)
		{
			Uri uri = new Uri(this.storagePath.OriginalString + "/" + relativeUri.OriginalString, UriKind.Relative);
			if (content != null)
			{
				using (Stream stream = content.GetStream())
				{
					PowerBIPackagingUtils.SetContent(this.package, uri, stream, content.ContentType, this.isOptional, this.compressionOption);
				}
				return;
			}
			if (this.isOptional)
			{
				if (this.package.PartExists(uri))
				{
					this.package.DeletePart(uri);
				}
				return;
			}
			string text = "Non-optional storage part ";
			Uri uri2 = uri;
			throw new IOException(text + ((uri2 != null) ? uri2.ToString() : null) + " is missing.");
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00007240 File Offset: 0x00005440
		public IDictionary<Uri, IStreamablePowerBIPackagePartContent> GetSubPartsContents()
		{
			return this.GetSubPartFullUris().ToDictionary((Uri uri) => this.GetRelativePathToStorgePath(uri), (Uri uri) => new PowerBIPackagePart(base.Package, uri, this.isOptional, CompressionOption.Normal).GetContent());
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00007265 File Offset: 0x00005465
		public HashSet<Uri> GetSubParts()
		{
			return new HashSet<Uri>(from uri in this.GetSubPartFullUris()
				select this.GetRelativePathToStorgePath(uri));
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00007284 File Offset: 0x00005484
		public void DeletePart(Uri relativeUri)
		{
			Uri uri = new Uri(this.storagePath.OriginalString + "/" + relativeUri.OriginalString, UriKind.Relative);
			if (this.package.PartExists(uri))
			{
				this.package.DeletePart(uri);
			}
		}

		// Token: 0x06000217 RID: 535 RVA: 0x000072D0 File Offset: 0x000054D0
		private IEnumerable<Uri> GetSubPartFullUris()
		{
			return from p in base.Package.GetParts()
				where p.Uri.OriginalString.StartsWith(this.storagePath.OriginalString, StringComparison.OrdinalIgnoreCase)
				select p.Uri;
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00007320 File Offset: 0x00005520
		private Uri GetRelativePathToStorgePath(Uri uri)
		{
			string text = uri.OriginalString;
			if (!text.StartsWith(this.storagePath.OriginalString, StringComparison.OrdinalIgnoreCase))
			{
				return null;
			}
			text = text.Remove(0, this.storagePath.OriginalString.Length + 1);
			return new Uri(text, UriKind.Relative);
		}
	}
}
