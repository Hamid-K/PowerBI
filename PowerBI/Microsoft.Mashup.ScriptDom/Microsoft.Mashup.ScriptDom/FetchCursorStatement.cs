using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002E5 RID: 741
	[Serializable]
	internal class FetchCursorStatement : CursorStatement
	{
		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06002960 RID: 10592 RVA: 0x00167168 File Offset: 0x00165368
		// (set) Token: 0x06002961 RID: 10593 RVA: 0x00167170 File Offset: 0x00165370
		public FetchType FetchType
		{
			get
			{
				return this._fetchType;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._fetchType = value;
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06002962 RID: 10594 RVA: 0x00167180 File Offset: 0x00165380
		public IList<VariableReference> IntoVariables
		{
			get
			{
				return this._intoVariables;
			}
		}

		// Token: 0x06002963 RID: 10595 RVA: 0x00167188 File Offset: 0x00165388
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002964 RID: 10596 RVA: 0x00167194 File Offset: 0x00165394
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.FetchType != null)
			{
				this.FetchType.Accept(visitor);
			}
			int i = 0;
			int count = this.IntoVariables.Count;
			while (i < count)
			{
				this.IntoVariables[i].Accept(visitor);
				i++;
			}
		}

		// Token: 0x04001C22 RID: 7202
		private FetchType _fetchType;

		// Token: 0x04001C23 RID: 7203
		private List<VariableReference> _intoVariables = new List<VariableReference>();
	}
}
