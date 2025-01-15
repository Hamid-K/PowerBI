using System;
using System.IO;

namespace NLog.Internal
{
	// Token: 0x0200014D RID: 333
	internal class Win32FileCharacteristicsHelper : FileCharacteristicsHelper
	{
		// Token: 0x06001000 RID: 4096 RVA: 0x00029414 File Offset: 0x00027614
		public override FileCharacteristics GetFileCharacteristics(string fileName, FileStream fileStream)
		{
			Win32FileNativeMethods.BY_HANDLE_FILE_INFORMATION by_HANDLE_FILE_INFORMATION;
			if (fileStream != null && Win32FileNativeMethods.GetFileInformationByHandle(fileStream.SafeFileHandle.DangerousGetHandle(), out by_HANDLE_FILE_INFORMATION))
			{
				return new FileCharacteristics(DateTime.FromFileTimeUtc(by_HANDLE_FILE_INFORMATION.ftCreationTime), DateTime.FromFileTimeUtc(by_HANDLE_FILE_INFORMATION.ftLastWriteTime), (long)((ulong)by_HANDLE_FILE_INFORMATION.nFileSizeLow + ((ulong)by_HANDLE_FILE_INFORMATION.nFileSizeHigh << 32)));
			}
			return null;
		}
	}
}
