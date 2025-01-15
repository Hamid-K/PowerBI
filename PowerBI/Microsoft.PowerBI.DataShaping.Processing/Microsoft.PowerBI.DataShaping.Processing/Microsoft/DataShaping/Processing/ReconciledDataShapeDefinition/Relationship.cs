using System;
using System.Collections.Generic;

namespace Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition
{
	// Token: 0x02000049 RID: 73
	internal sealed class Relationship
	{
		// Token: 0x060001F0 RID: 496 RVA: 0x000060EC File Offset: 0x000042EC
		internal Relationship(string parentScopeId, IList<JoinCondition> joinConditions)
		{
			this._parentScopeId = parentScopeId;
			this._joinConditions = joinConditions;
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x00006102 File Offset: 0x00004302
		public string ParentScopeId
		{
			get
			{
				return this._parentScopeId;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x0000610A File Offset: 0x0000430A
		internal IList<JoinCondition> JoinConditions
		{
			get
			{
				return this._joinConditions;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x00006112 File Offset: 0x00004312
		// (set) Token: 0x060001F4 RID: 500 RVA: 0x0000611A File Offset: 0x0000431A
		internal Scope ParentScope
		{
			get
			{
				return this._parentScope;
			}
			set
			{
				this._parentScope = value;
			}
		}

		// Token: 0x04000131 RID: 305
		private readonly string _parentScopeId;

		// Token: 0x04000132 RID: 306
		private readonly IList<JoinCondition> _joinConditions;

		// Token: 0x04000133 RID: 307
		private Scope _parentScope;
	}
}
