using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQuery.Expressions
{
	// Token: 0x02000022 RID: 34
	internal sealed class ReadOnlyExpressionTable : ExpressionTable
	{
		// Token: 0x060001A2 RID: 418 RVA: 0x000069B4 File Offset: 0x00004BB4
		internal ReadOnlyExpressionTable(ExpressionIdGenerator idGenerator, ReadOnlyCollection<ExpressionNode> entries)
			: base(idGenerator)
		{
			this.m_entries = entries;
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x000069C4 File Offset: 0x00004BC4
		public override ExpressionNode GetNodeOrDefault(ExpressionId id)
		{
			if (id.Value >= this.m_entries.Count)
			{
				return null;
			}
			return this.m_entries[id.Value];
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x000069EE File Offset: 0x00004BEE
		internal override IEnumerable<ExpressionNode> GetEntries()
		{
			return this.m_entries;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x000069F6 File Offset: 0x00004BF6
		public override ReadOnlyExpressionTable AsReadOnly()
		{
			return this;
		}

		// Token: 0x0400005A RID: 90
		private readonly ReadOnlyCollection<ExpressionNode> m_entries;
	}
}
