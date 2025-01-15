using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000465 RID: 1125
	[Serializable]
	internal abstract class BrokerPriorityStatement : TSqlStatement
	{
		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x06003260 RID: 12896 RVA: 0x0017015D File Offset: 0x0016E35D
		// (set) Token: 0x06003261 RID: 12897 RVA: 0x00170165 File Offset: 0x0016E365
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

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x06003262 RID: 12898 RVA: 0x00170175 File Offset: 0x0016E375
		public IList<BrokerPriorityParameter> BrokerPriorityParameters
		{
			get
			{
				return this._brokerPriorityParameters;
			}
		}

		// Token: 0x06003263 RID: 12899 RVA: 0x00170180 File Offset: 0x0016E380
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Name != null)
			{
				this.Name.Accept(visitor);
			}
			int i = 0;
			int count = this.BrokerPriorityParameters.Count;
			while (i < count)
			{
				this.BrokerPriorityParameters[i].Accept(visitor);
				i++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001EA6 RID: 7846
		private Identifier _name;

		// Token: 0x04001EA7 RID: 7847
		private List<BrokerPriorityParameter> _brokerPriorityParameters = new List<BrokerPriorityParameter>();
	}
}
