using System;
using System.IO;
using System.Security;

namespace Microsoft.Mashup.EngineHost
{
	// Token: 0x02001968 RID: 6504
	internal static class FilePath
	{
		// Token: 0x0600A51C RID: 42268 RVA: 0x00222C28 File Offset: 0x00220E28
		public static bool IsLocalFilePath(string path)
		{
			try
			{
				new FileInfo(path);
				DriveType driveType = new DriveInfo(path).DriveType;
				if (driveType - DriveType.Removable <= 1 || driveType - DriveType.CDRom <= 1)
				{
					return true;
				}
			}
			catch (ArgumentException)
			{
			}
			catch (SecurityException)
			{
			}
			catch (PathTooLongException)
			{
			}
			catch (NotSupportedException)
			{
			}
			catch (UnauthorizedAccessException)
			{
			}
			return false;
		}
	}
}
