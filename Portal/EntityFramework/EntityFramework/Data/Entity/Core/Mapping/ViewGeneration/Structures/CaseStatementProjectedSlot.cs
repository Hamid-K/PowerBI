using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Mapping.ViewGeneration.CqlGeneration;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Structures
{
	// Token: 0x02000598 RID: 1432
	internal sealed class CaseStatementProjectedSlot : ProjectedSlot
	{
		// Token: 0x06004547 RID: 17735 RVA: 0x000F4A24 File Offset: 0x000F2C24
		internal CaseStatementProjectedSlot(CaseStatement statement, IEnumerable<WithRelationship> withRelationships)
		{
			this.m_caseStatement = statement;
			this.m_withRelationships = withRelationships;
		}

		// Token: 0x06004548 RID: 17736 RVA: 0x000F4A3A File Offset: 0x000F2C3A
		internal override ProjectedSlot DeepQualify(CqlBlock block)
		{
			return new CaseStatementProjectedSlot(this.m_caseStatement.DeepQualify(block), null);
		}

		// Token: 0x06004549 RID: 17737 RVA: 0x000F4A4E File Offset: 0x000F2C4E
		internal override StringBuilder AsEsql(StringBuilder builder, MemberPath outputMember, string blockAlias, int indentLevel)
		{
			this.m_caseStatement.AsEsql(builder, this.m_withRelationships, blockAlias, indentLevel);
			return builder;
		}

		// Token: 0x0600454A RID: 17738 RVA: 0x000F4A67 File Offset: 0x000F2C67
		internal override DbExpression AsCqt(DbExpression row, MemberPath outputMember)
		{
			return this.m_caseStatement.AsCqt(row, this.m_withRelationships);
		}

		// Token: 0x0600454B RID: 17739 RVA: 0x000F4A7B File Offset: 0x000F2C7B
		internal override void ToCompactString(StringBuilder builder)
		{
			this.m_caseStatement.ToCompactString(builder);
		}

		// Token: 0x040018DE RID: 6366
		private readonly CaseStatement m_caseStatement;

		// Token: 0x040018DF RID: 6367
		private readonly IEnumerable<WithRelationship> m_withRelationships;
	}
}
