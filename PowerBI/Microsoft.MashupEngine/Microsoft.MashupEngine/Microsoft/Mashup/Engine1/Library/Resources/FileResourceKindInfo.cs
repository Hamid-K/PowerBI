using System;
using Microsoft.Mashup.Engine1.DataSourceReference;

namespace Microsoft.Mashup.Engine1.Library.Resources
{
	// Token: 0x02000509 RID: 1289
	internal class FileResourceKindInfo : FileSystemResourceKindInfo
	{
		// Token: 0x060029E5 RID: 10725 RVA: 0x0007D55D File Offset: 0x0007B75D
		public FileResourceKindInfo()
			: base("File", null, Strings.Resource_FilePath_Absolute, FileDataSourceLocation.Factory)
		{
		}
	}
}
