using System;

namespace System.Threading.Tasks.Dataflow.Internal
{
	// Token: 0x02000046 RID: 70
	[Flags]
	internal enum TargetCoreOptions : byte
	{
		// Token: 0x040000BB RID: 187
		None = 0,
		// Token: 0x040000BC RID: 188
		UsesAsyncCompletion = 1,
		// Token: 0x040000BD RID: 189
		RepresentsBlockCompletion = 2
	}
}
