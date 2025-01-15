using System;
using System.IO;
using System.IO.Packaging;

namespace Microsoft.Mashup.Storage
{
	// Token: 0x02002099 RID: 8345
	public class ZipPackagePart
	{
		// Token: 0x0600CC3C RID: 52284 RVA: 0x00289FD7 File Offset: 0x002881D7
		internal ZipPackagePart(PackagePart packagePart)
		{
			this.packagePart = packagePart;
		}

		// Token: 0x17003121 RID: 12577
		// (get) Token: 0x0600CC3D RID: 52285 RVA: 0x00289FE6 File Offset: 0x002881E6
		public string ContentType
		{
			get
			{
				return this.packagePart.ContentType;
			}
		}

		// Token: 0x17003122 RID: 12578
		// (get) Token: 0x0600CC3E RID: 52286 RVA: 0x00289FF3 File Offset: 0x002881F3
		public Uri Uri
		{
			get
			{
				return this.packagePart.Uri;
			}
		}

		// Token: 0x0600CC3F RID: 52287 RVA: 0x0028A000 File Offset: 0x00288200
		public Stream GetStream(FileMode mode, FileAccess access)
		{
			return this.packagePart.GetStream(mode, access);
		}

		// Token: 0x04006786 RID: 26502
		private readonly PackagePart packagePart;
	}
}
