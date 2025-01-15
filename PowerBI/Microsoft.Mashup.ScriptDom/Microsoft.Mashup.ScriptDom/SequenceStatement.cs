using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200026B RID: 619
	[Serializable]
	internal abstract class SequenceStatement : TSqlStatement
	{
		// Token: 0x170001EF RID: 495
		// (get) Token: 0x060026A0 RID: 9888 RVA: 0x00164342 File Offset: 0x00162542
		// (set) Token: 0x060026A1 RID: 9889 RVA: 0x0016434A File Offset: 0x0016254A
		public SchemaObjectName Name
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

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x060026A2 RID: 9890 RVA: 0x0016435A File Offset: 0x0016255A
		public IList<SequenceOption> SequenceOptions
		{
			get
			{
				return this._sequenceOptions;
			}
		}

		// Token: 0x060026A3 RID: 9891 RVA: 0x00164364 File Offset: 0x00162564
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Name != null)
			{
				this.Name.Accept(visitor);
			}
			int i = 0;
			int count = this.SequenceOptions.Count;
			while (i < count)
			{
				this.SequenceOptions[i].Accept(visitor);
				i++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001B60 RID: 7008
		private SchemaObjectName _name;

		// Token: 0x04001B61 RID: 7009
		private List<SequenceOption> _sequenceOptions = new List<SequenceOption>();
	}
}
