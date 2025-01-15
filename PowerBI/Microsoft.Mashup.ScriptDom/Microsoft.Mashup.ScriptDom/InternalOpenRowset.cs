using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000209 RID: 521
	[Serializable]
	internal class InternalOpenRowset : TableReferenceWithAlias
	{
		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06002445 RID: 9285 RVA: 0x001618DC File Offset: 0x0015FADC
		// (set) Token: 0x06002446 RID: 9286 RVA: 0x001618E4 File Offset: 0x0015FAE4
		public Identifier Identifier
		{
			get
			{
				return this._identifier;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._identifier = value;
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06002447 RID: 9287 RVA: 0x001618F4 File Offset: 0x0015FAF4
		public IList<ScalarExpression> VarArgs
		{
			get
			{
				return this._varArgs;
			}
		}

		// Token: 0x06002448 RID: 9288 RVA: 0x001618FC File Offset: 0x0015FAFC
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002449 RID: 9289 RVA: 0x00161908 File Offset: 0x0015FB08
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.Identifier != null)
			{
				this.Identifier.Accept(visitor);
			}
			int i = 0;
			int count = this.VarArgs.Count;
			while (i < count)
			{
				this.VarArgs[i].Accept(visitor);
				i++;
			}
		}

		// Token: 0x04001AB5 RID: 6837
		private Identifier _identifier;

		// Token: 0x04001AB6 RID: 6838
		private List<ScalarExpression> _varArgs = new List<ScalarExpression>();
	}
}
