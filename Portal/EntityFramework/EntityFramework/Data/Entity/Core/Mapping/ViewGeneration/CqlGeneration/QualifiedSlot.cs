using System;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.Structures;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.CqlGeneration
{
	// Token: 0x020005B7 RID: 1463
	internal sealed class QualifiedSlot : ProjectedSlot
	{
		// Token: 0x06004706 RID: 18182 RVA: 0x000FAFD3 File Offset: 0x000F91D3
		internal QualifiedSlot(CqlBlock block, ProjectedSlot slot)
		{
			this.m_block = block;
			this.m_slot = slot;
		}

		// Token: 0x06004707 RID: 18183 RVA: 0x000FAFE9 File Offset: 0x000F91E9
		internal override ProjectedSlot DeepQualify(CqlBlock block)
		{
			return new QualifiedSlot(block, this.m_slot);
		}

		// Token: 0x06004708 RID: 18184 RVA: 0x000FAFF7 File Offset: 0x000F91F7
		internal override string GetCqlFieldAlias(MemberPath outputMember)
		{
			return this.GetOriginalSlot().GetCqlFieldAlias(outputMember);
		}

		// Token: 0x06004709 RID: 18185 RVA: 0x000FB008 File Offset: 0x000F9208
		internal ProjectedSlot GetOriginalSlot()
		{
			ProjectedSlot projectedSlot = this.m_slot;
			for (;;)
			{
				QualifiedSlot qualifiedSlot = projectedSlot as QualifiedSlot;
				if (qualifiedSlot == null)
				{
					break;
				}
				projectedSlot = qualifiedSlot.m_slot;
			}
			return projectedSlot;
		}

		// Token: 0x0600470A RID: 18186 RVA: 0x000FB030 File Offset: 0x000F9230
		internal string GetQualifiedCqlName(MemberPath outputMember)
		{
			return CqlWriter.GetQualifiedName(this.m_block.CqlAlias, this.GetCqlFieldAlias(outputMember));
		}

		// Token: 0x0600470B RID: 18187 RVA: 0x000FB049 File Offset: 0x000F9249
		internal override StringBuilder AsEsql(StringBuilder builder, MemberPath outputMember, string blockAlias, int indentLevel)
		{
			builder.Append(this.GetQualifiedCqlName(outputMember));
			return builder;
		}

		// Token: 0x0600470C RID: 18188 RVA: 0x000FB05A File Offset: 0x000F925A
		internal override DbExpression AsCqt(DbExpression row, MemberPath outputMember)
		{
			return this.m_block.GetInput(row).Property(this.GetCqlFieldAlias(outputMember));
		}

		// Token: 0x0600470D RID: 18189 RVA: 0x000FB074 File Offset: 0x000F9274
		internal override void ToCompactString(StringBuilder builder)
		{
			StringUtil.FormatStringBuilder(builder, "{0} ", new object[] { this.m_block.CqlAlias });
			this.m_slot.ToCompactString(builder);
		}

		// Token: 0x04001935 RID: 6453
		private readonly CqlBlock m_block;

		// Token: 0x04001936 RID: 6454
		private readonly ProjectedSlot m_slot;
	}
}
