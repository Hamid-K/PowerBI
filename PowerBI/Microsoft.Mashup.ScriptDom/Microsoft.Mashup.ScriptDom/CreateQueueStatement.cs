using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002A5 RID: 677
	[Serializable]
	internal class CreateQueueStatement : QueueStatement
	{
		// Token: 0x17000244 RID: 580
		// (get) Token: 0x060027E0 RID: 10208 RVA: 0x00165832 File Offset: 0x00163A32
		// (set) Token: 0x060027E1 RID: 10209 RVA: 0x0016583A File Offset: 0x00163A3A
		public IdentifierOrValueExpression OnFileGroup
		{
			get
			{
				return this._onFileGroup;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._onFileGroup = value;
			}
		}

		// Token: 0x060027E2 RID: 10210 RVA: 0x0016584A File Offset: 0x00163A4A
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060027E3 RID: 10211 RVA: 0x00165858 File Offset: 0x00163A58
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (base.Name != null)
			{
				base.Name.Accept(visitor);
			}
			int i = 0;
			int count = base.QueueOptions.Count;
			while (i < count)
			{
				base.QueueOptions[i].Accept(visitor);
				i++;
			}
			if (this.OnFileGroup != null)
			{
				this.OnFileGroup.Accept(visitor);
			}
		}

		// Token: 0x04001BB5 RID: 7093
		private IdentifierOrValueExpression _onFileGroup;
	}
}
