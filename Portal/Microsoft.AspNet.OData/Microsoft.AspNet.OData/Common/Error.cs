using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

namespace Microsoft.AspNet.OData.Common
{
	// Token: 0x02000068 RID: 104
	internal static class Error
	{
		// Token: 0x060003E5 RID: 997 RVA: 0x0000CD98 File Offset: 0x0000AF98
		internal static string Format(string format, params object[] args)
		{
			return string.Format(CultureInfo.CurrentCulture, format, args);
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0000CDA6 File Offset: 0x0000AFA6
		internal static ArgumentException Argument(string messageFormat, params object[] messageArgs)
		{
			return new ArgumentException(Error.Format(messageFormat, messageArgs));
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x0000CDB4 File Offset: 0x0000AFB4
		internal static ArgumentException Argument(string parameterName, string messageFormat, params object[] messageArgs)
		{
			return new ArgumentException(Error.Format(messageFormat, messageArgs), parameterName);
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x0000CDC3 File Offset: 0x0000AFC3
		internal static ArgumentException ArgumentUriNotHttpOrHttpsScheme(string parameterName, Uri actualValue)
		{
			return new ArgumentException(Error.Format(CommonWebApiResources.ArgumentInvalidHttpUriScheme, new object[] { actualValue, "http", "https" }), parameterName);
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0000CDEF File Offset: 0x0000AFEF
		internal static ArgumentException ArgumentUriNotAbsolute(string parameterName, Uri actualValue)
		{
			return new ArgumentException(Error.Format(CommonWebApiResources.ArgumentInvalidAbsoluteUri, new object[] { actualValue }), parameterName);
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0000CE0B File Offset: 0x0000B00B
		internal static ArgumentException ArgumentUriHasQueryOrFragment(string parameterName, Uri actualValue)
		{
			return new ArgumentException(Error.Format(CommonWebApiResources.ArgumentUriHasQueryOrFragment, new object[] { actualValue }), parameterName);
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0000CE27 File Offset: 0x0000B027
		internal static ArgumentNullException PropertyNull()
		{
			return new ArgumentNullException("value");
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0000CE33 File Offset: 0x0000B033
		internal static ArgumentNullException ArgumentNull(string parameterName)
		{
			return new ArgumentNullException(parameterName);
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0000CE3B File Offset: 0x0000B03B
		internal static ArgumentNullException ArgumentNull(string parameterName, string messageFormat, params object[] messageArgs)
		{
			return new ArgumentNullException(parameterName, Error.Format(messageFormat, messageArgs));
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0000CE4A File Offset: 0x0000B04A
		internal static ArgumentException ArgumentNullOrEmpty(string parameterName)
		{
			return Error.Argument(parameterName, CommonWebApiResources.ArgumentNullOrEmpty, new object[] { parameterName });
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0000CE61 File Offset: 0x0000B061
		internal static ArgumentOutOfRangeException ArgumentOutOfRange(string parameterName, object actualValue, string messageFormat, params object[] messageArgs)
		{
			return new ArgumentOutOfRangeException(parameterName, actualValue, Error.Format(messageFormat, messageArgs));
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0000CE71 File Offset: 0x0000B071
		internal static ArgumentOutOfRangeException ArgumentMustBeGreaterThanOrEqualTo(string parameterName, object actualValue, object minValue)
		{
			return new ArgumentOutOfRangeException(parameterName, actualValue, Error.Format(CommonWebApiResources.ArgumentMustBeGreaterThanOrEqualTo, new object[] { minValue }));
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0000CE8E File Offset: 0x0000B08E
		internal static ArgumentOutOfRangeException ArgumentMustBeLessThanOrEqualTo(string parameterName, object actualValue, object maxValue)
		{
			return new ArgumentOutOfRangeException(parameterName, actualValue, Error.Format(CommonWebApiResources.ArgumentMustBeLessThanOrEqualTo, new object[] { maxValue }));
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0000CEAB File Offset: 0x0000B0AB
		internal static KeyNotFoundException KeyNotFound()
		{
			return new KeyNotFoundException();
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x0000CEB2 File Offset: 0x0000B0B2
		internal static KeyNotFoundException KeyNotFound(string messageFormat, params object[] messageArgs)
		{
			return new KeyNotFoundException(Error.Format(messageFormat, messageArgs));
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0000CEC0 File Offset: 0x0000B0C0
		internal static ObjectDisposedException ObjectDisposed(string messageFormat, params object[] messageArgs)
		{
			return new ObjectDisposedException(null, Error.Format(messageFormat, messageArgs));
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0000CECF File Offset: 0x0000B0CF
		internal static OperationCanceledException OperationCanceled()
		{
			return new OperationCanceledException();
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0000CED6 File Offset: 0x0000B0D6
		internal static OperationCanceledException OperationCanceled(string messageFormat, params object[] messageArgs)
		{
			return new OperationCanceledException(Error.Format(messageFormat, messageArgs));
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0000CEE4 File Offset: 0x0000B0E4
		internal static ArgumentException InvalidEnumArgument(string parameterName, int invalidValue, Type enumClass)
		{
			return new InvalidEnumArgumentException(parameterName, invalidValue, enumClass);
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0000CEEE File Offset: 0x0000B0EE
		internal static InvalidOperationException InvalidOperation(string messageFormat, params object[] messageArgs)
		{
			return new InvalidOperationException(Error.Format(messageFormat, messageArgs));
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0000CEFC File Offset: 0x0000B0FC
		internal static InvalidOperationException InvalidOperation(Exception innerException, string messageFormat, params object[] messageArgs)
		{
			return new InvalidOperationException(Error.Format(messageFormat, messageArgs), innerException);
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0000CF0B File Offset: 0x0000B10B
		internal static NotSupportedException NotSupported(string messageFormat, params object[] messageArgs)
		{
			return new NotSupportedException(Error.Format(messageFormat, messageArgs));
		}

		// Token: 0x040000C9 RID: 201
		private const string HttpScheme = "http";

		// Token: 0x040000CA RID: 202
		private const string HttpsScheme = "https";
	}
}
