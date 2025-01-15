using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020000A5 RID: 165
	[Serializable]
	internal enum QueueOptionKind
	{
		// Token: 0x040003D9 RID: 985
		Status,
		// Token: 0x040003DA RID: 986
		Retention,
		// Token: 0x040003DB RID: 987
		ActivationStatus,
		// Token: 0x040003DC RID: 988
		ActivationProcedureName,
		// Token: 0x040003DD RID: 989
		ActivationMaxQueueReaders,
		// Token: 0x040003DE RID: 990
		ActivationExecuteAs,
		// Token: 0x040003DF RID: 991
		ActivationDrop,
		// Token: 0x040003E0 RID: 992
		PoisonMessageHandlingStatus
	}
}
