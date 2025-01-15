using System;
using System.IO;
using System.Threading.Tasks;

namespace Microsoft.PowerBI.Packaging.Project
{
	// Token: 0x02000061 RID: 97
	public interface IProjectFilesReader
	{
		// Token: 0x060002BD RID: 701
		Task<bool> ExistsAsync(string path);

		// Token: 0x060002BE RID: 702
		Task<Stream> GetAsync(string path);

		// Token: 0x060002BF RID: 703
		Task<string[]> GetAllPathsAsync(string folderPath);
	}
}
