using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000230 RID: 560
	[Serializable]
	internal class DeclareVariableStatement : TSqlStatement
	{
		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06002530 RID: 9520 RVA: 0x00162A34 File Offset: 0x00160C34
		public IList<DeclareVariableElement> Declarations
		{
			get
			{
				return this._declarations;
			}
		}

		// Token: 0x06002531 RID: 9521 RVA: 0x00162A3C File Offset: 0x00160C3C
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002532 RID: 9522 RVA: 0x00162A48 File Offset: 0x00160C48
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			int i = 0;
			int count = this.Declarations.Count;
			while (i < count)
			{
				this.Declarations[i].Accept(visitor);
				i++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001AF5 RID: 6901
		private List<DeclareVariableElement> _declarations = new List<DeclareVariableElement>();
	}
}
