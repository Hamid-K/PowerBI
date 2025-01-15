using System;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Mapping.ViewGeneration.CqlGeneration;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Structures
{
	// Token: 0x020005A0 RID: 1440
	internal sealed class ConstantProjectedSlot : ProjectedSlot
	{
		// Token: 0x060045C3 RID: 17859 RVA: 0x000F5FC9 File Offset: 0x000F41C9
		internal ConstantProjectedSlot(Constant value)
		{
			this.m_constant = value;
		}

		// Token: 0x17000DC8 RID: 3528
		// (get) Token: 0x060045C4 RID: 17860 RVA: 0x000F5FD8 File Offset: 0x000F41D8
		internal Constant CellConstant
		{
			get
			{
				return this.m_constant;
			}
		}

		// Token: 0x060045C5 RID: 17861 RVA: 0x000F5FE0 File Offset: 0x000F41E0
		internal override ProjectedSlot DeepQualify(CqlBlock block)
		{
			return this;
		}

		// Token: 0x060045C6 RID: 17862 RVA: 0x000F5FE3 File Offset: 0x000F41E3
		internal override StringBuilder AsEsql(StringBuilder builder, MemberPath outputMember, string blockAlias, int indentLevel)
		{
			return this.m_constant.AsEsql(builder, outputMember, blockAlias);
		}

		// Token: 0x060045C7 RID: 17863 RVA: 0x000F5FF3 File Offset: 0x000F41F3
		internal override DbExpression AsCqt(DbExpression row, MemberPath outputMember)
		{
			return this.m_constant.AsCqt(row, outputMember);
		}

		// Token: 0x060045C8 RID: 17864 RVA: 0x000F6004 File Offset: 0x000F4204
		protected override bool IsEqualTo(ProjectedSlot right)
		{
			ConstantProjectedSlot constantProjectedSlot = right as ConstantProjectedSlot;
			return constantProjectedSlot != null && Constant.EqualityComparer.Equals(this.m_constant, constantProjectedSlot.m_constant);
		}

		// Token: 0x060045C9 RID: 17865 RVA: 0x000F6033 File Offset: 0x000F4233
		protected override int GetHash()
		{
			return Constant.EqualityComparer.GetHashCode(this.m_constant);
		}

		// Token: 0x060045CA RID: 17866 RVA: 0x000F6045 File Offset: 0x000F4245
		internal override void ToCompactString(StringBuilder builder)
		{
			this.m_constant.ToCompactString(builder);
		}

		// Token: 0x040018FE RID: 6398
		private readonly Constant m_constant;
	}
}
