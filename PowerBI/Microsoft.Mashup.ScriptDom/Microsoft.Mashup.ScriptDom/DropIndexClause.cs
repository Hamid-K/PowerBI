using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002EC RID: 748
	[Serializable]
	internal class DropIndexClause : DropIndexClauseBase
	{
		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x0600297F RID: 10623 RVA: 0x001673F3 File Offset: 0x001655F3
		// (set) Token: 0x06002980 RID: 10624 RVA: 0x001673FB File Offset: 0x001655FB
		public Identifier Index
		{
			get
			{
				return this._index;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._index = value;
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06002981 RID: 10625 RVA: 0x0016740B File Offset: 0x0016560B
		// (set) Token: 0x06002982 RID: 10626 RVA: 0x00167413 File Offset: 0x00165613
		public SchemaObjectName Object
		{
			get
			{
				return this._object;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._object = value;
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06002983 RID: 10627 RVA: 0x00167423 File Offset: 0x00165623
		public IList<IndexOption> Options
		{
			get
			{
				return this._options;
			}
		}

		// Token: 0x06002984 RID: 10628 RVA: 0x0016742B File Offset: 0x0016562B
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002985 RID: 10629 RVA: 0x00167438 File Offset: 0x00165638
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Index != null)
			{
				this.Index.Accept(visitor);
			}
			if (this.Object != null)
			{
				this.Object.Accept(visitor);
			}
			int i = 0;
			int count = this.Options.Count;
			while (i < count)
			{
				this.Options[i].Accept(visitor);
				i++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001C2A RID: 7210
		private Identifier _index;

		// Token: 0x04001C2B RID: 7211
		private SchemaObjectName _object;

		// Token: 0x04001C2C RID: 7212
		private List<IndexOption> _options = new List<IndexOption>();
	}
}
