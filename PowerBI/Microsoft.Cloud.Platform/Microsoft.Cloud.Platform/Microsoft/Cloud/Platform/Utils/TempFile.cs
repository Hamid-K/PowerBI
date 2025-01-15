using System;
using System.IO;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200014A RID: 330
	public static class TempFile
	{
		// Token: 0x0600089D RID: 2205 RVA: 0x0001E068 File Offset: 0x0001C268
		public static string GetTempFileName(string extension = "tmp", string filePrefix = "")
		{
			string text = Path.Combine(Path.GetTempPath(), string.Concat(new object[]
			{
				filePrefix,
				Guid.NewGuid(),
				".",
				extension
			}));
			using (File.Create(text))
			{
			}
			return text;
		}
	}
}
