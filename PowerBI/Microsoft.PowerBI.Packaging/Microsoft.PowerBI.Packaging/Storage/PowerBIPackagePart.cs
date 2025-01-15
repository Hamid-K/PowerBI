using System;
using System.IO;
using System.IO.Packaging;

namespace Microsoft.PowerBI.Packaging.Storage
{
	// Token: 0x0200004B RID: 75
	internal class PowerBIPackagePart : BasePowerBIPackagePart
	{
		// Token: 0x0600021D RID: 541 RVA: 0x000073B5 File Offset: 0x000055B5
		public PowerBIPackagePart(Package package, Uri storagePath, bool isOptional, CompressionOption compressionOption = CompressionOption.Normal)
			: base(package, storagePath, isOptional, compressionOption)
		{
		}

		// Token: 0x0600021E RID: 542 RVA: 0x000073C4 File Offset: 0x000055C4
		public void SetContent(IStreamablePowerBIPackagePartContent content)
		{
			if (content != null)
			{
				using (Stream stream = content.GetStream())
				{
					PowerBIPackagingUtils.SetContent(this.package, this.storagePath, stream, content.ContentType, this.isOptional, this.compressionOption);
				}
				return;
			}
			if (this.isOptional)
			{
				if (base.PartExists)
				{
					this.package.DeletePart(this.storagePath);
				}
				return;
			}
			string text = "Non-optional storage part ";
			Uri storagePath = this.storagePath;
			throw new IOException(text + ((storagePath != null) ? storagePath.ToString() : null) + " is missing.");
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00007464 File Offset: 0x00005664
		public virtual IStreamablePowerBIPackagePartContent GetContent()
		{
			if (base.PartExists)
			{
				return new StreamablePowerBIPackagePartContent(() => this.GetReadStream(), this.GetContentType());
			}
			return null;
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00007488 File Offset: 0x00005688
		private Stream GetReadStream()
		{
			PackagePart packagePart;
			if (!this.TryGetPart(out packagePart))
			{
				return null;
			}
			return packagePart.GetStream(FileMode.Open, FileAccess.Read);
		}

		// Token: 0x06000221 RID: 545 RVA: 0x000074AC File Offset: 0x000056AC
		private string GetContentType()
		{
			PackagePart packagePart;
			if (!this.TryGetPart(out packagePart))
			{
				return string.Empty;
			}
			return packagePart.ContentType;
		}

		// Token: 0x06000222 RID: 546 RVA: 0x000074CF File Offset: 0x000056CF
		private bool TryGetPart(out PackagePart part)
		{
			if (this.package == null || !this.package.PartExists(this.storagePath))
			{
				part = null;
				return false;
			}
			part = this.package.GetPart(this.storagePath);
			return true;
		}
	}
}
