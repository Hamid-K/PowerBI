using System;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;

namespace System.IO
{
	// Token: 0x02000013 RID: 19
	[NullableContext(1)]
	[Nullable(0)]
	public static class FileSystemAclExtensions
	{
		// Token: 0x06000040 RID: 64 RVA: 0x000024F4 File Offset: 0x000006F4
		public static FileStream Create(this FileInfo fileInfo, FileMode mode, FileSystemRights rights, FileShare share, int bufferSize, FileOptions options, FileSecurity fileSecurity)
		{
			if (fileInfo == null)
			{
				throw new ArgumentNullException("fileInfo");
			}
			if (fileSecurity == null)
			{
				throw new ArgumentNullException("fileSecurity");
			}
			return new FileStream(fileInfo.FullName, mode, rights, share, bufferSize, options, fileSecurity);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002527 File Offset: 0x00000727
		public static void Create(this DirectoryInfo directoryInfo, DirectorySecurity directorySecurity)
		{
			if (directoryInfo == null)
			{
				throw new ArgumentNullException("directoryInfo");
			}
			if (directorySecurity == null)
			{
				throw new ArgumentNullException("directorySecurity");
			}
			directoryInfo.Create(directorySecurity);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x0000254C File Offset: 0x0000074C
		public static DirectoryInfo CreateDirectory(this DirectorySecurity directorySecurity, string path)
		{
			if (directorySecurity == null)
			{
				throw new ArgumentNullException("directorySecurity");
			}
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException(SR.Arg_PathEmpty);
			}
			return Directory.CreateDirectory(path, directorySecurity);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002584 File Offset: 0x00000784
		public static DirectorySecurity GetAccessControl(this DirectoryInfo directoryInfo)
		{
			if (directoryInfo == null)
			{
				throw new ArgumentNullException("directoryInfo");
			}
			return directoryInfo.GetAccessControl();
		}

		// Token: 0x06000044 RID: 68 RVA: 0x0000259A File Offset: 0x0000079A
		public static DirectorySecurity GetAccessControl(this DirectoryInfo directoryInfo, AccessControlSections includeSections)
		{
			if (directoryInfo == null)
			{
				throw new ArgumentNullException("directoryInfo");
			}
			return directoryInfo.GetAccessControl(includeSections);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000025B1 File Offset: 0x000007B1
		public static void SetAccessControl(this DirectoryInfo directoryInfo, DirectorySecurity directorySecurity)
		{
			if (directoryInfo == null)
			{
				throw new ArgumentNullException("directoryInfo");
			}
			if (directorySecurity == null)
			{
				throw new ArgumentNullException("directorySecurity");
			}
			directoryInfo.SetAccessControl(directorySecurity);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000025D6 File Offset: 0x000007D6
		public static FileSecurity GetAccessControl(this FileInfo fileInfo)
		{
			if (fileInfo == null)
			{
				throw new ArgumentNullException("fileInfo");
			}
			return fileInfo.GetAccessControl();
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000025EC File Offset: 0x000007EC
		public static FileSecurity GetAccessControl(this FileInfo fileInfo, AccessControlSections includeSections)
		{
			if (fileInfo == null)
			{
				throw new ArgumentNullException("fileInfo");
			}
			return fileInfo.GetAccessControl(includeSections);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002603 File Offset: 0x00000803
		public static void SetAccessControl(this FileInfo fileInfo, FileSecurity fileSecurity)
		{
			if (fileInfo == null)
			{
				throw new ArgumentNullException("fileInfo");
			}
			if (fileSecurity == null)
			{
				throw new ArgumentNullException("fileSecurity");
			}
			fileInfo.SetAccessControl(fileSecurity);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002628 File Offset: 0x00000828
		public static FileSecurity GetAccessControl(this FileStream fileStream)
		{
			if (fileStream == null)
			{
				throw new ArgumentNullException("fileStream");
			}
			return fileStream.GetAccessControl();
		}

		// Token: 0x0600004A RID: 74 RVA: 0x0000263E File Offset: 0x0000083E
		public static void SetAccessControl(this FileStream fileStream, FileSecurity fileSecurity)
		{
			if (fileStream == null)
			{
				throw new ArgumentNullException("fileStream");
			}
			if (fileSecurity == null)
			{
				throw new ArgumentNullException("fileSecurity");
			}
			fileStream.SetAccessControl(fileSecurity);
		}
	}
}
