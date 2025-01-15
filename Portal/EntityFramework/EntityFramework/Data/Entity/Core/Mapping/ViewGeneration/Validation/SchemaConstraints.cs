using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Validation
{
	// Token: 0x0200057F RID: 1407
	internal class SchemaConstraints<TKeyConstraint> : InternalBase where TKeyConstraint : InternalBase
	{
		// Token: 0x06004417 RID: 17431 RVA: 0x000EF6C8 File Offset: 0x000ED8C8
		internal SchemaConstraints()
		{
			this.m_keyConstraints = new List<TKeyConstraint>();
		}

		// Token: 0x17000D77 RID: 3447
		// (get) Token: 0x06004418 RID: 17432 RVA: 0x000EF6DB File Offset: 0x000ED8DB
		internal IEnumerable<TKeyConstraint> KeyConstraints
		{
			get
			{
				return this.m_keyConstraints;
			}
		}

		// Token: 0x06004419 RID: 17433 RVA: 0x000EF6E3 File Offset: 0x000ED8E3
		internal void Add(TKeyConstraint constraint)
		{
			this.m_keyConstraints.Add(constraint);
		}

		// Token: 0x0600441A RID: 17434 RVA: 0x000EF6F4 File Offset: 0x000ED8F4
		private static void ConstraintsToBuilder<Constraint>(IEnumerable<Constraint> constraints, StringBuilder builder) where Constraint : InternalBase
		{
			foreach (Constraint constraint in constraints)
			{
				constraint.ToCompactString(builder);
				builder.Append(Environment.NewLine);
			}
		}

		// Token: 0x0600441B RID: 17435 RVA: 0x000EF74C File Offset: 0x000ED94C
		internal override void ToCompactString(StringBuilder builder)
		{
			SchemaConstraints<TKeyConstraint>.ConstraintsToBuilder<TKeyConstraint>(this.m_keyConstraints, builder);
		}

		// Token: 0x0400188E RID: 6286
		private readonly List<TKeyConstraint> m_keyConstraints;
	}
}
