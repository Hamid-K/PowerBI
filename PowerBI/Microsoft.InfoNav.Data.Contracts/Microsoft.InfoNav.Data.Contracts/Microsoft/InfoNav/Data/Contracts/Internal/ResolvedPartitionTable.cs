using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001ED RID: 493
	[ImmutableObject(true)]
	internal sealed class ResolvedPartitionTable
	{
		// Token: 0x06000D6A RID: 3434 RVA: 0x0001A660 File Offset: 0x00018860
		internal ResolvedPartitionTable(ResolvedPartitionTableDefinition definition, ResolvedPartitionTableResult result)
		{
			this._definition = definition;
			this._result = result;
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06000D6B RID: 3435 RVA: 0x0001A676 File Offset: 0x00018876
		internal ResolvedPartitionTableDefinition Definition
		{
			get
			{
				return this._definition;
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06000D6C RID: 3436 RVA: 0x0001A67E File Offset: 0x0001887E
		internal ResolvedPartitionTableResult Result
		{
			get
			{
				return this._result;
			}
		}

		// Token: 0x040006DA RID: 1754
		private readonly ResolvedPartitionTableDefinition _definition;

		// Token: 0x040006DB RID: 1755
		private readonly ResolvedPartitionTableResult _result;
	}
}
