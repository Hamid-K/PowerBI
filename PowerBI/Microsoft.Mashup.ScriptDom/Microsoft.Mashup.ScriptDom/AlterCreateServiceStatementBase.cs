using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003A1 RID: 929
	[Serializable]
	internal abstract class AlterCreateServiceStatementBase : TSqlStatement
	{
		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06002E08 RID: 11784 RVA: 0x0016BCD3 File Offset: 0x00169ED3
		// (set) Token: 0x06002E09 RID: 11785 RVA: 0x0016BCDB File Offset: 0x00169EDB
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

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06002E0A RID: 11786 RVA: 0x0016BCEB File Offset: 0x00169EEB
		// (set) Token: 0x06002E0B RID: 11787 RVA: 0x0016BCF3 File Offset: 0x00169EF3
		public SchemaObjectName QueueName
		{
			get
			{
				return this._queueName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._queueName = value;
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06002E0C RID: 11788 RVA: 0x0016BD03 File Offset: 0x00169F03
		public IList<ServiceContract> ServiceContracts
		{
			get
			{
				return this._serviceContracts;
			}
		}

		// Token: 0x06002E0D RID: 11789 RVA: 0x0016BD0C File Offset: 0x00169F0C
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Name != null)
			{
				this.Name.Accept(visitor);
			}
			if (this.QueueName != null)
			{
				this.QueueName.Accept(visitor);
			}
			int i = 0;
			int count = this.ServiceContracts.Count;
			while (i < count)
			{
				this.ServiceContracts[i].Accept(visitor);
				i++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001D7F RID: 7551
		private Identifier _name;

		// Token: 0x04001D80 RID: 7552
		private SchemaObjectName _queueName;

		// Token: 0x04001D81 RID: 7553
		private List<ServiceContract> _serviceContracts = new List<ServiceContract>();
	}
}
