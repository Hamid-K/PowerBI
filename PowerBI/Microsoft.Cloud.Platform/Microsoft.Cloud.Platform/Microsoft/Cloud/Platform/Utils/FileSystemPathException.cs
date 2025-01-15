using System;
using System.Text;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000217 RID: 535
	[Serializable]
	public class FileSystemPathException : Exception
	{
		// Token: 0x06000E1D RID: 3613 RVA: 0x00032046 File Offset: 0x00030246
		internal FileSystemPathException(string path, string message)
			: base(new StringBuilder().Append("Error with ").Append(path).Append(". Details are: ")
				.Append(message)
				.ToString())
		{
		}

		// Token: 0x06000E1E RID: 3614 RVA: 0x00032078 File Offset: 0x00030278
		internal FileSystemPathException(string volume, string[] fileSystemPathParts, string message)
			: base(new StringBuilder().Append("Error with path: ").Append("volume=").Append(volume)
				.Append("; ")
				.Append("path parts=[")
				.Append(string.Join(", ", fileSystemPathParts))
				.Append("]. ")
				.Append("Details are: ")
				.Append(message)
				.ToString())
		{
		}
	}
}
