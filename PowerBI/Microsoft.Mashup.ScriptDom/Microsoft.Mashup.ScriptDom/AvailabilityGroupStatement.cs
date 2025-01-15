using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200048C RID: 1164
	[Serializable]
	internal abstract class AvailabilityGroupStatement : TSqlStatement
	{
		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x0600334B RID: 13131 RVA: 0x00171076 File Offset: 0x0016F276
		// (set) Token: 0x0600334C RID: 13132 RVA: 0x0017107E File Offset: 0x0016F27E
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

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x0600334D RID: 13133 RVA: 0x0017108E File Offset: 0x0016F28E
		public IList<AvailabilityGroupOption> Options
		{
			get
			{
				return this._options;
			}
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x0600334E RID: 13134 RVA: 0x00171096 File Offset: 0x0016F296
		public IList<Identifier> Databases
		{
			get
			{
				return this._databases;
			}
		}

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x0600334F RID: 13135 RVA: 0x0017109E File Offset: 0x0016F29E
		public IList<AvailabilityReplica> Replicas
		{
			get
			{
				return this._replicas;
			}
		}

		// Token: 0x06003350 RID: 13136 RVA: 0x001710A8 File Offset: 0x0016F2A8
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
			int j = 0;
			int count2 = this.Databases.Count;
			while (j < count2)
			{
				this.Databases[j].Accept(visitor);
				j++;
			}
			int k = 0;
			int count3 = this.Replicas.Count;
			while (k < count3)
			{
				this.Replicas[k].Accept(visitor);
				k++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001EE9 RID: 7913
		private Identifier _name;

		// Token: 0x04001EEA RID: 7914
		private List<AvailabilityGroupOption> _options = new List<AvailabilityGroupOption>();

		// Token: 0x04001EEB RID: 7915
		private List<Identifier> _databases = new List<Identifier>();

		// Token: 0x04001EEC RID: 7916
		private List<AvailabilityReplica> _replicas = new List<AvailabilityReplica>();
	}
}
