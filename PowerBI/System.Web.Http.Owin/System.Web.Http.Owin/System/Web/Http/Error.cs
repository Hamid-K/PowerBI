using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Web.Http.Properties;

namespace System.Web.Http
{
	// Token: 0x02000006 RID: 6
	internal static class Error
	{
		// Token: 0x06000017 RID: 23 RVA: 0x000023F0 File Offset: 0x000005F0
		internal static string Format(string format, params object[] args)
		{
			return string.Format(CultureInfo.CurrentCulture, format, args);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000023FE File Offset: 0x000005FE
		internal static ArgumentException Argument(string messageFormat, params object[] messageArgs)
		{
			return new ArgumentException(Error.Format(messageFormat, messageArgs));
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000240C File Offset: 0x0000060C
		internal static ArgumentException Argument(string parameterName, string messageFormat, params object[] messageArgs)
		{
			return new ArgumentException(Error.Format(messageFormat, messageArgs), parameterName);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000241B File Offset: 0x0000061B
		internal static ArgumentException ArgumentUriNotHttpOrHttpsScheme(string parameterName, Uri actualValue)
		{
			return new ArgumentException(Error.Format(CommonWebApiResources.ArgumentInvalidHttpUriScheme, new object[] { actualValue, "http", "https" }), parameterName);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002447 File Offset: 0x00000647
		internal static ArgumentException ArgumentUriNotAbsolute(string parameterName, Uri actualValue)
		{
			return new ArgumentException(Error.Format(CommonWebApiResources.ArgumentInvalidAbsoluteUri, new object[] { actualValue }), parameterName);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002463 File Offset: 0x00000663
		internal static ArgumentException ArgumentUriHasQueryOrFragment(string parameterName, Uri actualValue)
		{
			return new ArgumentException(Error.Format(CommonWebApiResources.ArgumentUriHasQueryOrFragment, new object[] { actualValue }), parameterName);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000247F File Offset: 0x0000067F
		internal static ArgumentNullException PropertyNull()
		{
			return new ArgumentNullException("value");
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000248B File Offset: 0x0000068B
		internal static ArgumentNullException ArgumentNull(string parameterName)
		{
			return new ArgumentNullException(parameterName);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002493 File Offset: 0x00000693
		internal static ArgumentNullException ArgumentNull(string parameterName, string messageFormat, params object[] messageArgs)
		{
			return new ArgumentNullException(parameterName, Error.Format(messageFormat, messageArgs));
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000024A2 File Offset: 0x000006A2
		internal static ArgumentException ArgumentNullOrEmpty(string parameterName)
		{
			return Error.Argument(parameterName, CommonWebApiResources.ArgumentNullOrEmpty, new object[] { parameterName });
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000024B9 File Offset: 0x000006B9
		internal static ArgumentOutOfRangeException ArgumentOutOfRange(string parameterName, object actualValue, string messageFormat, params object[] messageArgs)
		{
			return new ArgumentOutOfRangeException(parameterName, actualValue, Error.Format(messageFormat, messageArgs));
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000024C9 File Offset: 0x000006C9
		internal static ArgumentOutOfRangeException ArgumentMustBeGreaterThanOrEqualTo(string parameterName, object actualValue, object minValue)
		{
			return new ArgumentOutOfRangeException(parameterName, actualValue, Error.Format(CommonWebApiResources.ArgumentMustBeGreaterThanOrEqualTo, new object[] { minValue }));
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000024E6 File Offset: 0x000006E6
		internal static ArgumentOutOfRangeException ArgumentMustBeLessThanOrEqualTo(string parameterName, object actualValue, object maxValue)
		{
			return new ArgumentOutOfRangeException(parameterName, actualValue, Error.Format(CommonWebApiResources.ArgumentMustBeLessThanOrEqualTo, new object[] { maxValue }));
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002503 File Offset: 0x00000703
		internal static KeyNotFoundException KeyNotFound()
		{
			return new KeyNotFoundException();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000250A File Offset: 0x0000070A
		internal static KeyNotFoundException KeyNotFound(string messageFormat, params object[] messageArgs)
		{
			return new KeyNotFoundException(Error.Format(messageFormat, messageArgs));
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002518 File Offset: 0x00000718
		internal static ObjectDisposedException ObjectDisposed(string messageFormat, params object[] messageArgs)
		{
			return new ObjectDisposedException(null, Error.Format(messageFormat, messageArgs));
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002527 File Offset: 0x00000727
		internal static OperationCanceledException OperationCanceled()
		{
			return new OperationCanceledException();
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000252E File Offset: 0x0000072E
		internal static OperationCanceledException OperationCanceled(string messageFormat, params object[] messageArgs)
		{
			return new OperationCanceledException(Error.Format(messageFormat, messageArgs));
		}

		// Token: 0x06000029 RID: 41 RVA: 0x0000253C File Offset: 0x0000073C
		internal static ArgumentException InvalidEnumArgument(string parameterName, int invalidValue, Type enumClass)
		{
			return new InvalidEnumArgumentException(parameterName, invalidValue, enumClass);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002546 File Offset: 0x00000746
		internal static InvalidOperationException InvalidOperation(string messageFormat, params object[] messageArgs)
		{
			return new InvalidOperationException(Error.Format(messageFormat, messageArgs));
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002554 File Offset: 0x00000754
		internal static InvalidOperationException InvalidOperation(Exception innerException, string messageFormat, params object[] messageArgs)
		{
			return new InvalidOperationException(Error.Format(messageFormat, messageArgs), innerException);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002563 File Offset: 0x00000763
		internal static NotSupportedException NotSupported(string messageFormat, params object[] messageArgs)
		{
			return new NotSupportedException(Error.Format(messageFormat, messageArgs));
		}

		// Token: 0x04000006 RID: 6
		private const string HttpScheme = "http";

		// Token: 0x04000007 RID: 7
		private const string HttpsScheme = "https";
	}
}
