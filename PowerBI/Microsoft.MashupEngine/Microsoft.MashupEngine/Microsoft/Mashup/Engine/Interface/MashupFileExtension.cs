using System;
using System.IO;
using System.Linq;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x020000FF RID: 255
	public static class MashupFileExtension
	{
		// Token: 0x060003CA RID: 970 RVA: 0x00004CD0 File Offset: 0x00002ED0
		public static bool IsKnownExtension(string filePath)
		{
			string fileExtension = MashupFileExtension.GetFileExtension(filePath);
			return MashupFileExtension.knownSourceFileExtensions.Contains(fileExtension) || MashupFileExtension.knownLibraryFileExtensions.Contains(fileExtension);
		}

		// Token: 0x060003CB RID: 971 RVA: 0x00004D00 File Offset: 0x00002F00
		public static bool IsKnownSourceExtension(string filePath)
		{
			string fileExtension = MashupFileExtension.GetFileExtension(filePath);
			return MashupFileExtension.knownSourceFileExtensions.Contains(fileExtension);
		}

		// Token: 0x060003CC RID: 972 RVA: 0x00004D20 File Offset: 0x00002F20
		public static bool IsKnownLibraryExtension(string filePath)
		{
			string fileExtension = MashupFileExtension.GetFileExtension(filePath);
			return MashupFileExtension.knownLibraryFileExtensions.Contains(fileExtension);
		}

		// Token: 0x060003CD RID: 973 RVA: 0x00004D3F File Offset: 0x00002F3F
		public static bool IsSdkArtifact(string filePath)
		{
			return MashupFileExtension.GetFileExtension(Path.GetFileNameWithoutExtension(filePath)).Equals(".query");
		}

		// Token: 0x060003CE RID: 974 RVA: 0x00004D56 File Offset: 0x00002F56
		public static string[] GetSourceExtensions()
		{
			return MashupFileExtension.knownSourceFileExtensions;
		}

		// Token: 0x060003CF RID: 975 RVA: 0x00004D5D File Offset: 0x00002F5D
		private static string GetFileExtension(string filePath)
		{
			return Path.GetExtension(filePath).ToLowerInvariant();
		}

		// Token: 0x04000229 RID: 553
		private static string[] knownSourceFileExtensions = new string[] { ".pq", ".m" };

		// Token: 0x0400022A RID: 554
		private static string[] knownLibraryFileExtensions = new string[] { ".pqx", ".mez" };
	}
}
