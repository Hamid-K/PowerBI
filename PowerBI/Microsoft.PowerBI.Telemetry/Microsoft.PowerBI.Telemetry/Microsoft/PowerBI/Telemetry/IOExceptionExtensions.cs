using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.PowerBI.Telemetry
{
	// Token: 0x02000029 RID: 41
	public static class IOExceptionExtensions
	{
		// Token: 0x060000F8 RID: 248 RVA: 0x00003E2D File Offset: 0x0000202D
		public static bool IsFileInUseException(this IOException e)
		{
			return e.IsErrorOfType(IOExceptionExtensions.FileInUseErrorCodes);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00003E3A File Offset: 0x0000203A
		public static bool IsInvalidPathException(this IOException e)
		{
			return e.IsErrorOfType(IOExceptionExtensions.InvalidFilePathErrorCodes);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00003E47 File Offset: 0x00002047
		public static bool IsDeviceNotReadyException(this IOException e)
		{
			return e.IsErrorOfType(new HashSet<int> { IOExceptionExtensions.ERROR_NOT_READY });
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00003E60 File Offset: 0x00002060
		private static bool IsErrorOfType(this IOException e, HashSet<int> errorCodes)
		{
			int num = e.GetHResult() & IOExceptionExtensions.HRESULT_CODE_MASK;
			return errorCodes.Contains(num);
		}

		// Token: 0x0400009A RID: 154
		private static readonly int HRESULT_CODE_MASK = 65535;

		// Token: 0x0400009B RID: 155
		private static readonly int ERROR_FILE_NOT_FOUND = 2;

		// Token: 0x0400009C RID: 156
		private static readonly int ERROR_PATH_NOT_FOUND = 3;

		// Token: 0x0400009D RID: 157
		private static readonly int ERROR_NOT_READY = 21;

		// Token: 0x0400009E RID: 158
		private static readonly int ERROR_SHARING_VIOLATION = 32;

		// Token: 0x0400009F RID: 159
		private static readonly int ERROR_LOCK_VIOLATION = 33;

		// Token: 0x040000A0 RID: 160
		private static readonly int ERROR_BAD_PATHNAME = 161;

		// Token: 0x040000A1 RID: 161
		private static readonly int ERROR_INVALID_NAME = 123;

		// Token: 0x040000A2 RID: 162
		private static readonly int ERROR_DIRECTORY = 267;

		// Token: 0x040000A3 RID: 163
		private static readonly int ERROR_CANT_RESOLVE_FILENAME = 1921;

		// Token: 0x040000A4 RID: 164
		private static readonly HashSet<int> FileInUseErrorCodes = new HashSet<int>(new int[]
		{
			IOExceptionExtensions.ERROR_SHARING_VIOLATION,
			IOExceptionExtensions.ERROR_LOCK_VIOLATION
		});

		// Token: 0x040000A5 RID: 165
		private static readonly HashSet<int> InvalidFilePathErrorCodes = new HashSet<int>(new int[]
		{
			IOExceptionExtensions.ERROR_FILE_NOT_FOUND,
			IOExceptionExtensions.ERROR_PATH_NOT_FOUND,
			IOExceptionExtensions.ERROR_INVALID_NAME,
			IOExceptionExtensions.ERROR_BAD_PATHNAME,
			IOExceptionExtensions.ERROR_DIRECTORY,
			IOExceptionExtensions.ERROR_CANT_RESOLVE_FILENAME
		});
	}
}
