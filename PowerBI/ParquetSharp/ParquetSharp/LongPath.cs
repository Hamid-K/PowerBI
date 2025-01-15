using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ParquetSharp
{
	// Token: 0x02000075 RID: 117
	internal static class LongPath
	{
		// Token: 0x06000307 RID: 775 RVA: 0x0000C81C File Offset: 0x0000AA1C
		[NullableContext(1)]
		public static string EnsureLongPathSafe(string path)
		{
			if (!Path.IsPathRooted(path) || !global::System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(global::System.Runtime.InteropServices.OSPlatform.Windows))
			{
				return path;
			}
			if (path.StartsWith("//?/") || path.StartsWith("\\\\?\\"))
			{
				return path;
			}
			if (path.StartsWith("//") || path.StartsWith("\\\\"))
			{
				return "\\\\?\\UNC\\" + path.Substring(2);
			}
			return "\\\\?\\" + path;
		}
	}
}
