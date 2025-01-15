using System;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.Structures;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.CqlGeneration
{
	// Token: 0x020005BE RID: 1470
	internal sealed class SlotInfo : InternalBase
	{
		// Token: 0x06004734 RID: 18228 RVA: 0x000FBA58 File Offset: 0x000F9C58
		internal SlotInfo(bool isRequiredByParent, bool isProjected, ProjectedSlot slotValue, MemberPath outputMember)
			: this(isRequiredByParent, isProjected, slotValue, outputMember, false)
		{
		}

		// Token: 0x06004735 RID: 18229 RVA: 0x000FBA66 File Offset: 0x000F9C66
		internal SlotInfo(bool isRequiredByParent, bool isProjected, ProjectedSlot slotValue, MemberPath outputMember, bool enforceNotNull)
		{
			this.m_isRequiredByParent = isRequiredByParent;
			this.m_isProjected = isProjected;
			this.m_slotValue = slotValue;
			this.m_outputMember = outputMember;
			this.m_enforceNotNull = enforceNotNull;
		}

		// Token: 0x17000E0B RID: 3595
		// (get) Token: 0x06004736 RID: 18230 RVA: 0x000FBA93 File Offset: 0x000F9C93
		internal bool IsRequiredByParent
		{
			get
			{
				return this.m_isRequiredByParent;
			}
		}

		// Token: 0x17000E0C RID: 3596
		// (get) Token: 0x06004737 RID: 18231 RVA: 0x000FBA9B File Offset: 0x000F9C9B
		internal bool IsProjected
		{
			get
			{
				return this.m_isProjected;
			}
		}

		// Token: 0x17000E0D RID: 3597
		// (get) Token: 0x06004738 RID: 18232 RVA: 0x000FBAA3 File Offset: 0x000F9CA3
		internal MemberPath OutputMember
		{
			get
			{
				return this.m_outputMember;
			}
		}

		// Token: 0x17000E0E RID: 3598
		// (get) Token: 0x06004739 RID: 18233 RVA: 0x000FBAAB File Offset: 0x000F9CAB
		internal ProjectedSlot SlotValue
		{
			get
			{
				return this.m_slotValue;
			}
		}

		// Token: 0x17000E0F RID: 3599
		// (get) Token: 0x0600473A RID: 18234 RVA: 0x000FBAB3 File Offset: 0x000F9CB3
		internal string CqlFieldAlias
		{
			get
			{
				if (this.m_slotValue == null)
				{
					return null;
				}
				return this.m_slotValue.GetCqlFieldAlias(this.m_outputMember);
			}
		}

		// Token: 0x17000E10 RID: 3600
		// (get) Token: 0x0600473B RID: 18235 RVA: 0x000FBAD0 File Offset: 0x000F9CD0
		internal bool IsEnforcedNotNull
		{
			get
			{
				return this.m_enforceNotNull;
			}
		}

		// Token: 0x0600473C RID: 18236 RVA: 0x000FBAD8 File Offset: 0x000F9CD8
		internal void ResetIsRequiredByParent()
		{
			this.m_isRequiredByParent = false;
		}

		// Token: 0x0600473D RID: 18237 RVA: 0x000FBAE4 File Offset: 0x000F9CE4
		internal StringBuilder AsEsql(StringBuilder builder, string blockAlias, int indentLevel)
		{
			if (this.m_enforceNotNull)
			{
				builder.Append('(');
				this.m_slotValue.AsEsql(builder, this.m_outputMember, blockAlias, indentLevel);
				builder.Append(" AND ");
				this.m_slotValue.AsEsql(builder, this.m_outputMember, blockAlias, indentLevel);
				builder.Append(" IS NOT NULL)");
			}
			else
			{
				this.m_slotValue.AsEsql(builder, this.m_outputMember, blockAlias, indentLevel);
			}
			return builder;
		}

		// Token: 0x0600473E RID: 18238 RVA: 0x000FBB5C File Offset: 0x000F9D5C
		internal DbExpression AsCqt(DbExpression row)
		{
			DbExpression dbExpression = this.m_slotValue.AsCqt(row, this.m_outputMember);
			if (this.m_enforceNotNull)
			{
				dbExpression = dbExpression.And(dbExpression.IsNull().Not());
			}
			return dbExpression;
		}

		// Token: 0x0600473F RID: 18239 RVA: 0x000FBB97 File Offset: 0x000F9D97
		internal override void ToCompactString(StringBuilder builder)
		{
			if (this.m_slotValue != null)
			{
				builder.Append(this.CqlFieldAlias);
			}
		}

		// Token: 0x04001946 RID: 6470
		private bool m_isRequiredByParent;

		// Token: 0x04001947 RID: 6471
		private readonly bool m_isProjected;

		// Token: 0x04001948 RID: 6472
		private readonly ProjectedSlot m_slotValue;

		// Token: 0x04001949 RID: 6473
		private readonly MemberPath m_outputMember;

		// Token: 0x0400194A RID: 6474
		private readonly bool m_enforceNotNull;
	}
}
