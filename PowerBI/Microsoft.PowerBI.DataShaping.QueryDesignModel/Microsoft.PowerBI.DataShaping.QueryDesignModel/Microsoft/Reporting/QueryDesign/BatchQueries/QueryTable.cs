using System;
using System.Collections.Generic;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.BatchQueries
{
	// Token: 0x0200026F RID: 623
	internal abstract class QueryTable
	{
		// Token: 0x06001AD9 RID: 6873 RVA: 0x0004B127 File Offset: 0x00049327
		protected QueryTable(IReadOnlyList<QueryTableColumn> columns)
		{
			this._columns = columns;
		}

		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x06001ADA RID: 6874 RVA: 0x0004B136 File Offset: 0x00049336
		public IReadOnlyList<QueryTableColumn> Columns
		{
			get
			{
				return this._columns;
			}
		}

		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x06001ADB RID: 6875
		internal abstract QueryExpression Expression { get; }

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x06001ADC RID: 6876
		internal abstract string BindingVariableNameSuggestion { get; }

		// Token: 0x04000EDC RID: 3804
		private readonly IReadOnlyList<QueryTableColumn> _columns;
	}
}
