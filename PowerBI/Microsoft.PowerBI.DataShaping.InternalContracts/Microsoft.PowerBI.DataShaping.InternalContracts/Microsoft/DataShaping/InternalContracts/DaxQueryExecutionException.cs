using System;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.PowerBI.Query.Contracts;

namespace Microsoft.DataShaping.InternalContracts
{
	// Token: 0x02000016 RID: 22
	internal class DaxQueryExecutionException : DataShapeEngineException
	{
		// Token: 0x0600003A RID: 58 RVA: 0x00002B42 File Offset: 0x00000D42
		internal DaxQueryExecutionException(string errorCode, string message, ErrorSource errorSource)
			: base(errorCode, message, errorSource)
		{
		}
	}
}
