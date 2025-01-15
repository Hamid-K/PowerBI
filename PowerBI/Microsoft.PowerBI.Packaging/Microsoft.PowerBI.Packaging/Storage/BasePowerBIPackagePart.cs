using System;
using System.IO;
using System.IO.Packaging;

namespace Microsoft.PowerBI.Packaging.Storage
{
	// Token: 0x0200003D RID: 61
	public abstract class BasePowerBIPackagePart
	{
		// Token: 0x060001A8 RID: 424 RVA: 0x00006620 File Offset: 0x00004820
		public BasePowerBIPackagePart(Package package, Uri storagePath, bool isOptional, CompressionOption compressionOption = CompressionOption.Normal)
		{
			if (package == null)
			{
				throw new ArgumentNullException("package");
			}
			if (storagePath == null)
			{
				throw new ArgumentNullException("storagePath");
			}
			this.package = package;
			this.storagePath = storagePath;
			this.isOptional = isOptional;
			this.compressionOption = compressionOption;
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00006672 File Offset: 0x00004872
		public bool PartExists
		{
			get
			{
				return this.package.PartExists(this.storagePath);
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00006685 File Offset: 0x00004885
		protected Package Package
		{
			get
			{
				return this.package;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001AB RID: 427 RVA: 0x0000668D File Offset: 0x0000488D
		protected Uri StoragePath
		{
			get
			{
				return this.storagePath;
			}
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00006698 File Offset: 0x00004898
		public void ValidateExistsIfRequired()
		{
			if (this.package == null)
			{
				throw new InvalidOperationException("Cannot validate an empty package.");
			}
			if (!this.isOptional && !this.PartExists)
			{
				FileFormatException ex = new FileFormatException();
				ex.Data["MissingBIPackagePart"] = this.StoragePath.ToString();
				throw ex;
			}
		}

		// Token: 0x040000F2 RID: 242
		private const int streamBufferSize = 4096;

		// Token: 0x040000F3 RID: 243
		protected readonly bool isOptional;

		// Token: 0x040000F4 RID: 244
		protected readonly Uri storagePath;

		// Token: 0x040000F5 RID: 245
		protected readonly CompressionOption compressionOption;

		// Token: 0x040000F6 RID: 246
		protected Package package;
	}
}
