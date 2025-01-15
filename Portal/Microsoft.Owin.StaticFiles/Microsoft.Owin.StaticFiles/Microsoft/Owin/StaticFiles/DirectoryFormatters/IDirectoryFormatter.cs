using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Owin.FileSystems;

namespace Microsoft.Owin.StaticFiles.DirectoryFormatters
{
	// Token: 0x0200001A RID: 26
	public interface IDirectoryFormatter
	{
		// Token: 0x06000083 RID: 131
		Task GenerateContentAsync(IOwinContext context, IEnumerable<IFileInfo> contents);
	}
}
