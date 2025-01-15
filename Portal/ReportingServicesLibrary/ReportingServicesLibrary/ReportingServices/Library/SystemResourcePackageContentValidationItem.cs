using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000253 RID: 595
	internal sealed class SystemResourcePackageContentValidationItem
	{
		// Token: 0x060015BF RID: 5567 RVA: 0x000564E8 File Offset: 0x000546E8
		internal SystemResourcePackageContentValidationItem(string key, string contentType, string extension)
		{
			if (string.IsNullOrEmpty(key))
			{
				throw new ArgumentException(ErrorStringsWrapper.rsMissingParameter("key"), "key");
			}
			if (string.IsNullOrEmpty(contentType))
			{
				throw new ArgumentException(ErrorStringsWrapper.rsMissingParameter("contentType"), "contentType");
			}
			if (string.IsNullOrEmpty(extension))
			{
				throw new ArgumentException(ErrorStringsWrapper.rsMissingParameter("extension"), "extension");
			}
			this._key = key;
			this._contentType = contentType;
			this._extension = extension;
		}

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x060015C0 RID: 5568 RVA: 0x00056567 File Offset: 0x00054767
		internal string Key
		{
			get
			{
				return this._key;
			}
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x060015C1 RID: 5569 RVA: 0x0005656F File Offset: 0x0005476F
		internal string ContentType
		{
			get
			{
				return this._contentType;
			}
		}

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x060015C2 RID: 5570 RVA: 0x00056577 File Offset: 0x00054777
		internal string Extension
		{
			get
			{
				return this._extension;
			}
		}

		// Token: 0x040007F7 RID: 2039
		private readonly string _key;

		// Token: 0x040007F8 RID: 2040
		private readonly string _contentType;

		// Token: 0x040007F9 RID: 2041
		private readonly string _extension;
	}
}
