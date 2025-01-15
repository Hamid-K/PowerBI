using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001E9 RID: 489
	[Serializable]
	internal abstract class MDHNode
	{
		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000FCB RID: 4043
		internal abstract int Count { get; }

		// Token: 0x06000FCC RID: 4044
		internal abstract bool GetBatch(IScanner info, BaseEnumeratorState state);

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000FCD RID: 4045
		internal abstract MDHNodeType NodeType { get; }

		// Token: 0x06000FCE RID: 4046
		internal abstract bool CanNodeBeMoved();

		// Token: 0x06000FCF RID: 4047
		internal abstract void VerifyState();
	}
}
