using System;
using System.IO;
using Microsoft.Mashup.Engine1.DataSourceReference;

namespace Microsoft.Mashup.Engine1.Library.Resources
{
	// Token: 0x0200050A RID: 1290
	internal class FolderResourceKindInfo : FileSystemResourceKindInfo
	{
		// Token: 0x060029E6 RID: 10726 RVA: 0x0007D57A File Offset: 0x0007B77A
		public FolderResourceKindInfo()
			: base("Folder", null, Strings.Resource_FolderPath_Absolute, FolderDataSourceLocation.Factory)
		{
		}

		// Token: 0x060029E7 RID: 10727 RVA: 0x0007D597 File Offset: 0x0007B797
		public override bool IsSubset(string permittedResourcePath, string attemptedResourcePath)
		{
			return Resource.IsSubPath(permittedResourcePath, attemptedResourcePath, Path.DirectorySeparatorChar);
		}
	}
}
