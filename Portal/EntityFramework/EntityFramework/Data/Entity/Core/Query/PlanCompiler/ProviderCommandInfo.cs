using System;
using System.Data.Entity.Core.Common.CommandTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000338 RID: 824
	internal sealed class ProviderCommandInfo
	{
		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x0600273E RID: 10046 RVA: 0x00072302 File Offset: 0x00070502
		internal DbCommandTree CommandTree
		{
			get
			{
				return this._commandTree;
			}
		}

		// Token: 0x0600273F RID: 10047 RVA: 0x0007230A File Offset: 0x0007050A
		internal ProviderCommandInfo(DbCommandTree commandTree)
		{
			this._commandTree = commandTree;
		}

		// Token: 0x04000DAC RID: 3500
		private readonly DbCommandTree _commandTree;
	}
}
