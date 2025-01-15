using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Web.Http.Properties;

namespace System.Web.Http
{
	// Token: 0x0200000F RID: 15
	internal static class Error
	{
		// Token: 0x06000066 RID: 102 RVA: 0x000034F0 File Offset: 0x000016F0
		internal static string Format(string format, params object[] args)
		{
			return string.Format(CultureInfo.CurrentCulture, format, args);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000034FE File Offset: 0x000016FE
		internal static ArgumentException Argument(string messageFormat, params object[] messageArgs)
		{
			return new ArgumentException(Error.Format(messageFormat, messageArgs));
		}

		// Token: 0x06000068 RID: 104 RVA: 0x0000350C File Offset: 0x0000170C
		internal static ArgumentException Argument(string parameterName, string messageFormat, params object[] messageArgs)
		{
			return new ArgumentException(Error.Format(messageFormat, messageArgs), parameterName);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x0000351B File Offset: 0x0000171B
		internal static ArgumentException ArgumentUriNotHttpOrHttpsScheme(string parameterName, Uri actualValue)
		{
			return new ArgumentException(Error.Format(CommonWebApiResources.ArgumentInvalidHttpUriScheme, new object[] { actualValue, "http", "https" }), parameterName);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003547 File Offset: 0x00001747
		internal static ArgumentException ArgumentUriNotAbsolute(string parameterName, Uri actualValue)
		{
			return new ArgumentException(Error.Format(CommonWebApiResources.ArgumentInvalidAbsoluteUri, new object[] { actualValue }), parameterName);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003563 File Offset: 0x00001763
		internal static ArgumentException ArgumentUriHasQueryOrFragment(string parameterName, Uri actualValue)
		{
			return new ArgumentException(Error.Format(CommonWebApiResources.ArgumentUriHasQueryOrFragment, new object[] { actualValue }), parameterName);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x0000357F File Offset: 0x0000177F
		internal static ArgumentNullException PropertyNull()
		{
			return new ArgumentNullException("value");
		}

		// Token: 0x0600006D RID: 109 RVA: 0x0000358B File Offset: 0x0000178B
		internal static ArgumentNullException ArgumentNull(string parameterName)
		{
			return new ArgumentNullException(parameterName);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003593 File Offset: 0x00001793
		internal static ArgumentNullException ArgumentNull(string parameterName, string messageFormat, params object[] messageArgs)
		{
			return new ArgumentNullException(parameterName, Error.Format(messageFormat, messageArgs));
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000035A2 File Offset: 0x000017A2
		internal static ArgumentException ArgumentNullOrEmpty(string parameterName)
		{
			return Error.Argument(parameterName, CommonWebApiResources.ArgumentNullOrEmpty, new object[] { parameterName });
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000035B9 File Offset: 0x000017B9
		internal static ArgumentOutOfRangeException ArgumentOutOfRange(string parameterName, object actualValue, string messageFormat, params object[] messageArgs)
		{
			return new ArgumentOutOfRangeException(parameterName, actualValue, Error.Format(messageFormat, messageArgs));
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000035C9 File Offset: 0x000017C9
		internal static ArgumentOutOfRangeException ArgumentMustBeGreaterThanOrEqualTo(string parameterName, object actualValue, object minValue)
		{
			return new ArgumentOutOfRangeException(parameterName, actualValue, Error.Format(CommonWebApiResources.ArgumentMustBeGreaterThanOrEqualTo, new object[] { minValue }));
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000035E6 File Offset: 0x000017E6
		internal static ArgumentOutOfRangeException ArgumentMustBeLessThanOrEqualTo(string parameterName, object actualValue, object maxValue)
		{
			return new ArgumentOutOfRangeException(parameterName, actualValue, Error.Format(CommonWebApiResources.ArgumentMustBeLessThanOrEqualTo, new object[] { maxValue }));
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003603 File Offset: 0x00001803
		internal static KeyNotFoundException KeyNotFound()
		{
			return new KeyNotFoundException();
		}

		// Token: 0x06000074 RID: 116 RVA: 0x0000360A File Offset: 0x0000180A
		internal static KeyNotFoundException KeyNotFound(string messageFormat, params object[] messageArgs)
		{
			return new KeyNotFoundException(Error.Format(messageFormat, messageArgs));
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003618 File Offset: 0x00001818
		internal static ObjectDisposedException ObjectDisposed(string messageFormat, params object[] messageArgs)
		{
			return new ObjectDisposedException(null, Error.Format(messageFormat, messageArgs));
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003627 File Offset: 0x00001827
		internal static OperationCanceledException OperationCanceled()
		{
			return new OperationCanceledException();
		}

		// Token: 0x06000077 RID: 119 RVA: 0x0000362E File Offset: 0x0000182E
		internal static OperationCanceledException OperationCanceled(string messageFormat, params object[] messageArgs)
		{
			return new OperationCanceledException(Error.Format(messageFormat, messageArgs));
		}

		// Token: 0x06000078 RID: 120 RVA: 0x0000363C File Offset: 0x0000183C
		internal static ArgumentException InvalidEnumArgument(string parameterName, int invalidValue, Type enumClass)
		{
			return new InvalidEnumArgumentException(parameterName, invalidValue, enumClass);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003646 File Offset: 0x00001846
		internal static InvalidOperationException InvalidOperation(string messageFormat, params object[] messageArgs)
		{
			return new InvalidOperationException(Error.Format(messageFormat, messageArgs));
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003654 File Offset: 0x00001854
		internal static InvalidOperationException InvalidOperation(Exception innerException, string messageFormat, params object[] messageArgs)
		{
			return new InvalidOperationException(Error.Format(messageFormat, messageArgs), innerException);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003663 File Offset: 0x00001863
		internal static NotSupportedException NotSupported(string messageFormat, params object[] messageArgs)
		{
			return new NotSupportedException(Error.Format(messageFormat, messageArgs));
		}

		// Token: 0x04000010 RID: 16
		private const string HttpScheme = "http";

		// Token: 0x04000011 RID: 17
		private const string HttpsScheme = "https";
	}
}
