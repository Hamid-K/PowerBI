using System;
using System.Globalization;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation
{
	// Token: 0x02000065 RID: 101
	internal static class CallContextHelpers
	{
		// Token: 0x0600030B RID: 779 RVA: 0x0000E6E0 File Offset: 0x0000C8E0
		internal static void SaveOperationContext(OperationContextForCallContext operationContext)
		{
			CallContext.FreeNamedDataSlot(CallContextHelpers.FieldKey);
			CallContext.LogicalSetData(CallContextHelpers.FieldKey, new ObjectHandle(operationContext));
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000E6FC File Offset: 0x0000C8FC
		internal static OperationContextForCallContext GetCurrentOperationContext()
		{
			ObjectHandle objectHandle = CallContext.LogicalGetData(CallContextHelpers.FieldKey) as ObjectHandle;
			if (objectHandle != null)
			{
				return (OperationContextForCallContext)objectHandle.Unwrap();
			}
			return null;
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000E729 File Offset: 0x0000C929
		internal static void RestoreOperationContext(OperationContextForCallContext parentContext)
		{
			CallContext.FreeNamedDataSlot(CallContextHelpers.FieldKey);
			if (parentContext != null)
			{
				CallContext.LogicalSetData(CallContextHelpers.FieldKey, new ObjectHandle(parentContext));
			}
		}

		// Token: 0x04000151 RID: 337
		private static readonly string FieldKey = string.Format(CultureInfo.InvariantCulture, "Microsoft.ApplicationInsights.Operation.OperationContextStore_{0}", new object[] { AppDomain.CurrentDomain.Id });
	}
}
