using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Microsoft.OData.Client
{
	// Token: 0x020000D4 RID: 212
	internal static class Error
	{
		// Token: 0x060006E0 RID: 1760 RVA: 0x0001CC09 File Offset: 0x0001AE09
		internal static ArgumentException Argument(string message, string parameterName)
		{
			return Error.Trace<ArgumentException>(new ArgumentException(message, parameterName));
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x0001CC17 File Offset: 0x0001AE17
		internal static InvalidOperationException InvalidOperation(string message)
		{
			return Error.Trace<InvalidOperationException>(new InvalidOperationException(message));
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x0001CC24 File Offset: 0x0001AE24
		internal static InvalidOperationException InvalidOperation(string message, Exception innerException)
		{
			return Error.Trace<InvalidOperationException>(new InvalidOperationException(message, innerException));
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x0001CC32 File Offset: 0x0001AE32
		internal static NotSupportedException NotSupported(string message)
		{
			return Error.Trace<NotSupportedException>(new NotSupportedException(message));
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x0001CC3F File Offset: 0x0001AE3F
		internal static void ThrowObjectDisposed(Type type)
		{
			throw Error.Trace<ObjectDisposedException>(new ObjectDisposedException(type.ToString()));
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x0001CC51 File Offset: 0x0001AE51
		[SuppressMessage("Microsoft.Usage", "CA1801", Justification = "errorCode ignored for code sharing")]
		internal static InvalidOperationException HttpHeaderFailure(int errorCode, string message)
		{
			return Error.Trace<InvalidOperationException>(new InvalidOperationException(message));
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x0001CC5E File Offset: 0x0001AE5E
		internal static NotSupportedException MethodNotSupported(MethodCallExpression m)
		{
			return Error.NotSupported(Strings.ALinq_MethodNotSupported(m.Method.Name));
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x0001CC75 File Offset: 0x0001AE75
		internal static void ThrowBatchUnexpectedContent(InternalError value)
		{
			throw Error.InvalidOperation(Strings.Batch_UnexpectedContent((int)value));
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x0001CC87 File Offset: 0x0001AE87
		internal static void ThrowBatchExpectedResponse(InternalError value)
		{
			throw Error.InvalidOperation(Strings.Batch_ExpectedResponse((int)value));
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x0001CC99 File Offset: 0x0001AE99
		internal static InvalidOperationException InternalError(InternalError value)
		{
			return Error.InvalidOperation(Strings.Context_InternalError((int)value));
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x0001CCAB File Offset: 0x0001AEAB
		internal static void ThrowInternalError(InternalError value)
		{
			throw Error.InternalError(value);
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x00002DF3 File Offset: 0x00000FF3
		private static T Trace<T>(T exception) where T : Exception
		{
			return exception;
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x0001CCB3 File Offset: 0x0001AEB3
		internal static Exception ArgumentNull(string paramName)
		{
			return new ArgumentNullException(paramName);
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x0001CCBB File Offset: 0x0001AEBB
		internal static Exception ArgumentOutOfRange(string paramName)
		{
			return new ArgumentOutOfRangeException(paramName);
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x0001CCC3 File Offset: 0x0001AEC3
		internal static Exception NotImplemented()
		{
			return new NotImplementedException();
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x0001CCCA File Offset: 0x0001AECA
		internal static Exception NotSupported()
		{
			return new NotSupportedException();
		}
	}
}
