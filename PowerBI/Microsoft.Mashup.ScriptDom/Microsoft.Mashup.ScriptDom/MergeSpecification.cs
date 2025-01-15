using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000432 RID: 1074
	[Serializable]
	internal class MergeSpecification : DataModificationSpecification
	{
		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06003161 RID: 12641 RVA: 0x0016F2A2 File Offset: 0x0016D4A2
		// (set) Token: 0x06003162 RID: 12642 RVA: 0x0016F2AA File Offset: 0x0016D4AA
		public Identifier TableAlias
		{
			get
			{
				return this._tableAlias;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._tableAlias = value;
			}
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x06003163 RID: 12643 RVA: 0x0016F2BA File Offset: 0x0016D4BA
		// (set) Token: 0x06003164 RID: 12644 RVA: 0x0016F2C2 File Offset: 0x0016D4C2
		public TableReference TableReference
		{
			get
			{
				return this._tableReference;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._tableReference = value;
			}
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x06003165 RID: 12645 RVA: 0x0016F2D2 File Offset: 0x0016D4D2
		// (set) Token: 0x06003166 RID: 12646 RVA: 0x0016F2DA File Offset: 0x0016D4DA
		public BooleanExpression SearchCondition
		{
			get
			{
				return this._searchCondition;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._searchCondition = value;
			}
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x06003167 RID: 12647 RVA: 0x0016F2EA File Offset: 0x0016D4EA
		public IList<MergeActionClause> ActionClauses
		{
			get
			{
				return this._actionClauses;
			}
		}

		// Token: 0x06003168 RID: 12648 RVA: 0x0016F2F2 File Offset: 0x0016D4F2
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06003169 RID: 12649 RVA: 0x0016F300 File Offset: 0x0016D500
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.TableAlias != null)
			{
				this.TableAlias.Accept(visitor);
			}
			if (this.TableReference != null)
			{
				this.TableReference.Accept(visitor);
			}
			if (this.SearchCondition != null)
			{
				this.SearchCondition.Accept(visitor);
			}
			int i = 0;
			int count = this.ActionClauses.Count;
			while (i < count)
			{
				this.ActionClauses[i].Accept(visitor);
				i++;
			}
		}

		// Token: 0x04001E69 RID: 7785
		private Identifier _tableAlias;

		// Token: 0x04001E6A RID: 7786
		private TableReference _tableReference;

		// Token: 0x04001E6B RID: 7787
		private BooleanExpression _searchCondition;

		// Token: 0x04001E6C RID: 7788
		private List<MergeActionClause> _actionClauses = new List<MergeActionClause>();
	}
}
