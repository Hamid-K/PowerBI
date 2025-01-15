using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003D5 RID: 981
	[Serializable]
	internal class VariableMethodCallTableReference : TableReferenceWithAliasAndColumns
	{
		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06002F5D RID: 12125 RVA: 0x0016D4D6 File Offset: 0x0016B6D6
		// (set) Token: 0x06002F5E RID: 12126 RVA: 0x0016D4DE File Offset: 0x0016B6DE
		public VariableReference Variable
		{
			get
			{
				return this._variable;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._variable = value;
			}
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06002F5F RID: 12127 RVA: 0x0016D4EE File Offset: 0x0016B6EE
		// (set) Token: 0x06002F60 RID: 12128 RVA: 0x0016D4F6 File Offset: 0x0016B6F6
		public Identifier MethodName
		{
			get
			{
				return this._methodName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._methodName = value;
			}
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06002F61 RID: 12129 RVA: 0x0016D506 File Offset: 0x0016B706
		public IList<ScalarExpression> Parameters
		{
			get
			{
				return this._parameters;
			}
		}

		// Token: 0x06002F62 RID: 12130 RVA: 0x0016D50E File Offset: 0x0016B70E
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002F63 RID: 12131 RVA: 0x0016D51C File Offset: 0x0016B71C
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Variable != null)
			{
				this.Variable.Accept(visitor);
			}
			if (this.MethodName != null)
			{
				this.MethodName.Accept(visitor);
			}
			int i = 0;
			int count = this.Parameters.Count;
			while (i < count)
			{
				this.Parameters[i].Accept(visitor);
				i++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001DE8 RID: 7656
		private VariableReference _variable;

		// Token: 0x04001DE9 RID: 7657
		private Identifier _methodName;

		// Token: 0x04001DEA RID: 7658
		private List<ScalarExpression> _parameters = new List<ScalarExpression>();
	}
}
