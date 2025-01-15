using System;
using Microsoft.DataShaping.ServiceContracts;

namespace Microsoft.DataShaping.InternalContracts.Errors
{
	// Token: 0x0200002E RID: 46
	internal sealed class DataModelParsingException : DataShapeEngineException
	{
		// Token: 0x0600010A RID: 266 RVA: 0x00003C86 File Offset: 0x00001E86
		internal DataModelParsingException(string errorCode, string message, Exception innerException)
			: base(errorCode, message, innerException)
		{
		}
	}
}
