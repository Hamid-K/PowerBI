using System;
using Microsoft.DataShaping.InternalContracts.QueryExecution;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition;

namespace Microsoft.DataShaping.Processing.QueryExecution
{
	// Token: 0x0200005D RID: 93
	internal sealed class CommandExecutionContext
	{
		// Token: 0x06000241 RID: 577 RVA: 0x0000682F File Offset: 0x00004A2F
		internal CommandExecutionContext(DataSet dataSet, QueryCommandOptions commandOptions, string connectionCategory)
		{
			this.DataSet = dataSet;
			this.CommandOptions = commandOptions;
			this.ConnectionCategory = connectionCategory;
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000242 RID: 578 RVA: 0x0000684C File Offset: 0x00004A4C
		public DataSet DataSet { get; }

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000243 RID: 579 RVA: 0x00006854 File Offset: 0x00004A54
		public QueryCommandOptions CommandOptions { get; }

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000244 RID: 580 RVA: 0x0000685C File Offset: 0x00004A5C
		public string ConnectionCategory { get; }
	}
}
