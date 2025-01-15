using System;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Mapping.ViewGeneration.CqlGeneration;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Structures
{
	// Token: 0x020005AE RID: 1454
	internal sealed class QualifiedCellIdBoolean : CellIdBoolean
	{
		// Token: 0x060046C3 RID: 18115 RVA: 0x000FA085 File Offset: 0x000F8285
		internal QualifiedCellIdBoolean(CqlBlock block, CqlIdentifiers identifiers, int originalCellNum)
			: base(identifiers, originalCellNum)
		{
			this.m_block = block;
		}

		// Token: 0x060046C4 RID: 18116 RVA: 0x000FA096 File Offset: 0x000F8296
		internal override StringBuilder AsEsql(StringBuilder builder, string blockAlias, bool skipIsNotNull)
		{
			return base.AsEsql(builder, this.m_block.CqlAlias, skipIsNotNull);
		}

		// Token: 0x060046C5 RID: 18117 RVA: 0x000FA0AB File Offset: 0x000F82AB
		internal override DbExpression AsCqt(DbExpression row, bool skipIsNotNull)
		{
			return base.AsCqt(this.m_block.GetInput(row), skipIsNotNull);
		}

		// Token: 0x04001927 RID: 6439
		private readonly CqlBlock m_block;
	}
}
