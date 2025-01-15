using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Web.Http.Properties;

namespace System.Web.Http
{
	// Token: 0x0200005D RID: 93
	internal static class Error
	{
		// Token: 0x0600035C RID: 860 RVA: 0x0000C574 File Offset: 0x0000A774
		internal static string Format(string format, params object[] args)
		{
			return string.Format(CultureInfo.CurrentCulture, format, args);
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0000C582 File Offset: 0x0000A782
		internal static ArgumentException Argument(string messageFormat, params object[] messageArgs)
		{
			return new ArgumentException(Error.Format(messageFormat, messageArgs));
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0000C590 File Offset: 0x0000A790
		internal static ArgumentException Argument(string parameterName, string messageFormat, params object[] messageArgs)
		{
			return new ArgumentException(Error.Format(messageFormat, messageArgs), parameterName);
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0000C59F File Offset: 0x0000A79F
		internal static ArgumentException ArgumentUriNotHttpOrHttpsScheme(string parameterName, Uri actualValue)
		{
			return new ArgumentException(Error.Format(CommonWebApiResources.ArgumentInvalidHttpUriScheme, new object[] { actualValue, "http", "https" }), parameterName);
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0000C5CB File Offset: 0x0000A7CB
		internal static ArgumentException ArgumentUriNotAbsolute(string parameterName, Uri actualValue)
		{
			return new ArgumentException(Error.Format(CommonWebApiResources.ArgumentInvalidAbsoluteUri, new object[] { actualValue }), parameterName);
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0000C5E7 File Offset: 0x0000A7E7
		internal static ArgumentException ArgumentUriHasQueryOrFragment(string parameterName, Uri actualValue)
		{
			return new ArgumentException(Error.Format(CommonWebApiResources.ArgumentUriHasQueryOrFragment, new object[] { actualValue }), parameterName);
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000C603 File Offset: 0x0000A803
		internal static ArgumentNullException PropertyNull()
		{
			return new ArgumentNullException("value");
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0000C60F File Offset: 0x0000A80F
		internal static ArgumentNullException ArgumentNull(string parameterName)
		{
			return new ArgumentNullException(parameterName);
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0000C617 File Offset: 0x0000A817
		internal static ArgumentNullException ArgumentNull(string parameterName, string messageFormat, params object[] messageArgs)
		{
			return new ArgumentNullException(parameterName, Error.Format(messageFormat, messageArgs));
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000C626 File Offset: 0x0000A826
		internal static ArgumentException ArgumentNullOrEmpty(string parameterName)
		{
			return Error.Argument(parameterName, CommonWebApiResources.ArgumentNullOrEmpty, new object[] { parameterName });
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000C63D File Offset: 0x0000A83D
		internal static ArgumentOutOfRangeException ArgumentOutOfRange(string parameterName, object actualValue, string messageFormat, params object[] messageArgs)
		{
			return new ArgumentOutOfRangeException(parameterName, actualValue, Error.Format(messageFormat, messageArgs));
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0000C64D File Offset: 0x0000A84D
		internal static ArgumentOutOfRangeException ArgumentMustBeGreaterThanOrEqualTo(string parameterName, object actualValue, object minValue)
		{
			return new ArgumentOutOfRangeException(parameterName, actualValue, Error.Format(CommonWebApiResources.ArgumentMustBeGreaterThanOrEqualTo, new object[] { minValue }));
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0000C66A File Offset: 0x0000A86A
		internal static ArgumentOutOfRangeException ArgumentMustBeLessThanOrEqualTo(string parameterName, object actualValue, object maxValue)
		{
			return new ArgumentOutOfRangeException(parameterName, actualValue, Error.Format(CommonWebApiResources.ArgumentMustBeLessThanOrEqualTo, new object[] { maxValue }));
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0000C687 File Offset: 0x0000A887
		internal static KeyNotFoundException KeyNotFound()
		{
			return new KeyNotFoundException();
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0000C68E File Offset: 0x0000A88E
		internal static KeyNotFoundException KeyNotFound(string messageFormat, params object[] messageArgs)
		{
			return new KeyNotFoundException(Error.Format(messageFormat, messageArgs));
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0000C69C File Offset: 0x0000A89C
		internal static ObjectDisposedException ObjectDisposed(string messageFormat, params object[] messageArgs)
		{
			return new ObjectDisposedException(null, Error.Format(messageFormat, messageArgs));
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0000C6AB File Offset: 0x0000A8AB
		internal static OperationCanceledException OperationCanceled()
		{
			return new OperationCanceledException();
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0000C6B2 File Offset: 0x0000A8B2
		internal static OperationCanceledException OperationCanceled(string messageFormat, params object[] messageArgs)
		{
			return new OperationCanceledException(Error.Format(messageFormat, messageArgs));
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0000C6C0 File Offset: 0x0000A8C0
		internal static ArgumentException InvalidEnumArgument(string parameterName, int invalidValue, Type enumClass)
		{
			return new InvalidEnumArgumentException(parameterName, invalidValue, enumClass);
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0000C6CA File Offset: 0x0000A8CA
		internal static InvalidOperationException InvalidOperation(string messageFormat, params object[] messageArgs)
		{
			return new InvalidOperationException(Error.Format(messageFormat, messageArgs));
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0000C6D8 File Offset: 0x0000A8D8
		internal static InvalidOperationException InvalidOperation(Exception innerException, string messageFormat, params object[] messageArgs)
		{
			return new InvalidOperationException(Error.Format(messageFormat, messageArgs), innerException);
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000C6E7 File Offset: 0x0000A8E7
		internal static NotSupportedException NotSupported(string messageFormat, params object[] messageArgs)
		{
			return new NotSupportedException(Error.Format(messageFormat, messageArgs));
		}

		// Token: 0x04000134 RID: 308
		private const string HttpScheme = "http";

		// Token: 0x04000135 RID: 309
		private const string HttpsScheme = "https";
	}
}
