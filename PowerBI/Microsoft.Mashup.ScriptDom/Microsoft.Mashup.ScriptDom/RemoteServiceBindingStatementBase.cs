using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000365 RID: 869
	[Serializable]
	internal abstract class RemoteServiceBindingStatementBase : TSqlStatement
	{
		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06002C93 RID: 11411 RVA: 0x0016A4DD File Offset: 0x001686DD
		// (set) Token: 0x06002C94 RID: 11412 RVA: 0x0016A4E5 File Offset: 0x001686E5
		public Identifier Name
		{
			get
			{
				return this._name;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._name = value;
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06002C95 RID: 11413 RVA: 0x0016A4F5 File Offset: 0x001686F5
		public IList<RemoteServiceBindingOption> Options
		{
			get
			{
				return this._options;
			}
		}

		// Token: 0x06002C96 RID: 11414 RVA: 0x0016A500 File Offset: 0x00168700
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Name != null)
			{
				this.Name.Accept(visitor);
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

		// Token: 0x04001D13 RID: 7443
		private Identifier _name;

		// Token: 0x04001D14 RID: 7444
		private List<RemoteServiceBindingOption> _options = new List<RemoteServiceBindingOption>();
	}
}
