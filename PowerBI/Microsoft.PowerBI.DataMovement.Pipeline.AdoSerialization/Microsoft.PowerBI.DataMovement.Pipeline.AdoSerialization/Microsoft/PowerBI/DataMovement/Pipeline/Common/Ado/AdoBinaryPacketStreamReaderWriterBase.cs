using System;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.Common.Ado
{
	// Token: 0x02000007 RID: 7
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class AdoBinaryPacketStreamReaderWriterBase
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020B3 File Offset: 0x000002B3
		// (set) Token: 0x06000008 RID: 8 RVA: 0x000020BB File Offset: 0x000002BB
		protected byte[] DataPage { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000020C4 File Offset: 0x000002C4
		// (set) Token: 0x0600000A RID: 10 RVA: 0x000020CC File Offset: 0x000002CC
		protected byte[] VarDataPage { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000020D5 File Offset: 0x000002D5
		// (set) Token: 0x0600000C RID: 12 RVA: 0x000020DD File Offset: 0x000002DD
		protected int DataPageCurrentOffset { get; set; }
	}
}
