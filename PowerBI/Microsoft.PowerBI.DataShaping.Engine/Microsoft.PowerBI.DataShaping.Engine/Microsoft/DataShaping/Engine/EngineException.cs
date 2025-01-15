using System;
using System.Runtime.Serialization;
using Microsoft.DataShaping.ServiceContracts;

namespace Microsoft.DataShaping.Engine
{
	// Token: 0x0200000D RID: 13
	internal sealed class EngineException : DataShapeEngineException
	{
		// Token: 0x06000034 RID: 52 RVA: 0x00002AC4 File Offset: 0x00000CC4
		internal EngineException(EngineMessage errorMessage)
			: base(errorMessage.GetErrorCodeString(), errorMessage.Message, errorMessage.Source)
		{
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002ADE File Offset: 0x00000CDE
		internal EngineException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
