using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000325 RID: 805
	[Serializable]
	internal class AlterDatabaseSetStatement : AlterDatabaseStatement
	{
		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06002AD2 RID: 10962 RVA: 0x0016876B File Offset: 0x0016696B
		// (set) Token: 0x06002AD3 RID: 10963 RVA: 0x00168773 File Offset: 0x00166973
		public AlterDatabaseTermination Termination
		{
			get
			{
				return this._termination;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._termination = value;
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06002AD4 RID: 10964 RVA: 0x00168783 File Offset: 0x00166983
		public IList<DatabaseOption> Options
		{
			get
			{
				return this._options;
			}
		}

		// Token: 0x06002AD5 RID: 10965 RVA: 0x0016878B File Offset: 0x0016698B
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002AD6 RID: 10966 RVA: 0x00168798 File Offset: 0x00166998
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.Termination != null)
			{
				this.Termination.Accept(visitor);
			}
			int i = 0;
			int count = this.Options.Count;
			while (i < count)
			{
				this.Options[i].Accept(visitor);
				i++;
			}
		}

		// Token: 0x04001C84 RID: 7300
		private AlterDatabaseTermination _termination;

		// Token: 0x04001C85 RID: 7301
		private List<DatabaseOption> _options = new List<DatabaseOption>();
	}
}
