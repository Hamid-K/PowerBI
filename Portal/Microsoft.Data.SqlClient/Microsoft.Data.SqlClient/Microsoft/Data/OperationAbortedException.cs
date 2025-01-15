using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Microsoft.Data.Common;

namespace Microsoft.Data
{
	// Token: 0x0200000A RID: 10
	[TypeForwardedFrom("System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public sealed class OperationAbortedException : SystemException
	{
		// Token: 0x060005F8 RID: 1528 RVA: 0x0000A9C5 File Offset: 0x00008BC5
		private OperationAbortedException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2146232010;
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x0000A9DA File Offset: 0x00008BDA
		private OperationAbortedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x0000A9E4 File Offset: 0x00008BE4
		internal static OperationAbortedException Aborted(Exception inner)
		{
			OperationAbortedException ex;
			if (inner == null)
			{
				ex = new OperationAbortedException(Strings.ADP_OperationAborted, null);
			}
			else
			{
				ex = new OperationAbortedException(Strings.ADP_OperationAbortedExceptionMessage, inner);
			}
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}
	}
}
