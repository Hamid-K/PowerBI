using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.DataShaping;
using Microsoft.DataShaping.Common;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.QueryGeneration
{
	// Token: 0x02000090 RID: 144
	internal sealed class QueryLimitConstraintContext : IQueryConstraint, IEquatable<IQueryConstraint>
	{
		// Token: 0x060006D7 RID: 1751 RVA: 0x00019EED File Offset: 0x000180ED
		internal QueryLimitConstraintContext(IEnumerable<GroupReference> groups, LimitOperator limitOp, bool isPostRegroup)
		{
			this.m_groups = groups.ToReadOnlyCollection<GroupReference>();
			this.m_operator = limitOp;
			this.m_isPostRegroup = isPostRegroup;
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060006D8 RID: 1752 RVA: 0x00019F0F File Offset: 0x0001810F
		public ReadOnlyCollection<GroupReference> Groups
		{
			get
			{
				return this.m_groups;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060006D9 RID: 1753 RVA: 0x00019F17 File Offset: 0x00018117
		public LimitOperator Operator
		{
			get
			{
				return this.m_operator;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060006DA RID: 1754 RVA: 0x00019F1F File Offset: 0x0001811F
		public bool IsPostRegroup
		{
			get
			{
				return this.m_isPostRegroup;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060006DB RID: 1755 RVA: 0x00019F27 File Offset: 0x00018127
		public int PaddedCount
		{
			get
			{
				return this.m_operator.Count;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060006DC RID: 1756 RVA: 0x00019F34 File Offset: 0x00018134
		public int RawCount
		{
			get
			{
				return this.m_operator.Count - 2;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060006DD RID: 1757 RVA: 0x00019F43 File Offset: 0x00018143
		public bool IsWindow
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x00019F48 File Offset: 0x00018148
		public bool Equals(IQueryConstraint other)
		{
			QueryLimitConstraintContext queryLimitConstraintContext = other as QueryLimitConstraintContext;
			return queryLimitConstraintContext != null && this.Groups == queryLimitConstraintContext.Groups && this.Operator == queryLimitConstraintContext.Operator && this.IsPostRegroup == queryLimitConstraintContext.IsPostRegroup;
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x00019F8B File Offset: 0x0001818B
		public override bool Equals(object obj)
		{
			return this.Equals(obj as IQueryConstraint);
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x00019F9C File Offset: 0x0001819C
		public override int GetHashCode()
		{
			return Hashing.CombineHash<int>(new int[]
			{
				this.Groups.GetHashCode(),
				this.Operator.GetHashCode(),
				this.IsPostRegroup.GetHashCode()
			}, null);
		}

		// Token: 0x04000354 RID: 852
		private readonly ReadOnlyCollection<GroupReference> m_groups;

		// Token: 0x04000355 RID: 853
		private readonly LimitOperator m_operator;

		// Token: 0x04000356 RID: 854
		private readonly bool m_isPostRegroup;
	}
}
