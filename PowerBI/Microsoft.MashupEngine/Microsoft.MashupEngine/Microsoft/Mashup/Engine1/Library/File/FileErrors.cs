using System;
using System.IO;
using System.Net;
using System.Security;
using Microsoft.Mashup.Engine1.Library.Http;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.File
{
	// Token: 0x02000B6C RID: 2924
	internal static class FileErrors
	{
		// Token: 0x060050CB RID: 20683 RVA: 0x0010E90B File Offset: 0x0010CB0B
		public static ValueException FilePathExpected(string path)
		{
			return ValueException.NewExpressionError<Message1>(Strings.FilePathExpected(path), TextValue.New(path), null);
		}

		// Token: 0x060050CC RID: 20684 RVA: 0x0010E920 File Offset: 0x0010CB20
		public static Exception HandleException(Exception exception, Value detail)
		{
			if (exception is ArgumentOutOfRangeException)
			{
				return ValueException.NewExpressionError(exception.Message, detail, exception);
			}
			if (exception is ArgumentException)
			{
				return ValueException.NewExpressionError(exception.Message, detail, exception);
			}
			if (exception is UnauthorizedAccessException || exception is SecurityException)
			{
				return ValueException.NewDataSourceError(exception.Message, detail, exception);
			}
			if (exception is DirectoryNotFoundException || exception is FileNotFoundException)
			{
				return ValueException.NewDataSourceNotFound<Message2>(DataSourceException.DataSourceMessage("File or Folder", exception.Message), detail, exception);
			}
			if (exception is PathTooLongException)
			{
				return ValueException.NewDataFormatError(exception.Message, detail, exception);
			}
			if (exception is FileFormatException)
			{
				return ValueException.NewDataFormatError(exception.Message, detail, exception);
			}
			if (exception is IOException)
			{
				if (exception is FileNotFoundException)
				{
					return ValueException.NewDataSourceNotFound(exception.Message, detail, exception);
				}
				return ValueException.NewDataSourceError(exception.Message, detail, exception);
			}
			else if (exception is WebException)
			{
				MashupHttpWebResponse mashupHttpWebResponse = (exception as WebException).Response as MashupHttpWebResponse;
				if (mashupHttpWebResponse != null && mashupHttpWebResponse.StatusCode == HttpStatusCode.NotFound)
				{
					return ValueException.NewDataSourceNotFound(exception.Message, detail, exception);
				}
				return ValueException.NewDataSourceError(exception.Message, detail, exception);
			}
			else
			{
				ValueException ex = exception as ValueException;
				if (ex != null)
				{
					return ex;
				}
				return exception;
			}
		}

		// Token: 0x060050CD RID: 20685 RVA: 0x0010EA4C File Offset: 0x0010CC4C
		public static Exception WinIOError(int errorCode, string path)
		{
			switch (errorCode)
			{
			case 2:
				return new FileNotFoundException(Strings.FileNotFound(path));
			case 3:
				if (path.Length >= 260)
				{
					return new PathTooLongException(Strings.FilePathTooLong(path));
				}
				return new DirectoryNotFoundException(Strings.FileDirectoryNotFound(path));
			case 4:
				break;
			case 5:
				return new UnauthorizedAccessException(Strings.FileAccessDenied(path));
			default:
				if (errorCode == 123)
				{
					return new ArgumentException(Strings.FilePathInvalid(path));
				}
				if (errorCode == 206)
				{
					return new PathTooLongException(Strings.FilePathTooLong(path));
				}
				break;
			}
			return new IOException(Strings.FileIOError(errorCode, path));
		}

		// Token: 0x04002B64 RID: 11108
		public const int NO_ERROR = 0;

		// Token: 0x04002B65 RID: 11109
		public const int ERROR_FILE_NOT_FOUND = 2;

		// Token: 0x04002B66 RID: 11110
		public const int ERROR_PATH_NOT_FOUND = 3;

		// Token: 0x04002B67 RID: 11111
		public const int ERROR_ACCESS_DENIED = 5;

		// Token: 0x04002B68 RID: 11112
		public const int ERROR_NO_MORE_FILES = 18;

		// Token: 0x04002B69 RID: 11113
		public const int ERROR_INVALID_NAME = 123;

		// Token: 0x04002B6A RID: 11114
		public const int ERROR_FILENAME_EXCED_RANGE = 206;

		// Token: 0x04002B6B RID: 11115
		public const int MAX_PATH = 260;
	}
}
