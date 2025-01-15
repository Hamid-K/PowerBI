using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001A6 RID: 422
	[Serializable]
	internal class ExecuteStatement : TSqlStatement
	{
		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060021EA RID: 8682 RVA: 0x0015ED16 File Offset: 0x0015CF16
		// (set) Token: 0x060021EB RID: 8683 RVA: 0x0015ED1E File Offset: 0x0015CF1E
		public ExecuteSpecification ExecuteSpecification
		{
			get
			{
				return this._executeSpecification;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._executeSpecification = value;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060021EC RID: 8684 RVA: 0x0015ED2E File Offset: 0x0015CF2E
		public IList<ExecuteOption> Options
		{
			get
			{
				return this._options;
			}
		}

		// Token: 0x060021ED RID: 8685 RVA: 0x0015ED36 File Offset: 0x0015CF36
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060021EE RID: 8686 RVA: 0x0015ED44 File Offset: 0x0015CF44
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.ExecuteSpecification != null)
			{
				this.ExecuteSpecification.Accept(visitor);
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

		// Token: 0x04001A00 RID: 6656
		private ExecuteSpecification _executeSpecification;

		// Token: 0x04001A01 RID: 6657
		private List<ExecuteOption> _options = new List<ExecuteOption>();
	}
}
