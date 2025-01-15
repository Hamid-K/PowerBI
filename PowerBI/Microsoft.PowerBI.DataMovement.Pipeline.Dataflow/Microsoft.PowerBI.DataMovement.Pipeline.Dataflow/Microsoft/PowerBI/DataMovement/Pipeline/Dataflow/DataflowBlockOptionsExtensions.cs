using System;

namespace Microsoft.PowerBI.DataMovement.Pipeline.Dataflow
{
	// Token: 0x02000007 RID: 7
	internal sealed class DataflowBlockOptionsExtensions
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020B3 File Offset: 0x000002B3
		internal DataflowBlockOptionsExtensions()
		{
			this.AllowUnordered = false;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000020C2 File Offset: 0x000002C2
		// (set) Token: 0x06000009 RID: 9 RVA: 0x000020CA File Offset: 0x000002CA
		internal bool AllowUnordered { get; set; }
	}
}
