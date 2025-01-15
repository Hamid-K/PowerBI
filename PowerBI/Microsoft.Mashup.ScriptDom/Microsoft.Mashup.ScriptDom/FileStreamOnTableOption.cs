using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000285 RID: 645
	[Serializable]
	internal class FileStreamOnTableOption : TableOption
	{
		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06002727 RID: 10023 RVA: 0x00164C4A File Offset: 0x00162E4A
		// (set) Token: 0x06002728 RID: 10024 RVA: 0x00164C52 File Offset: 0x00162E52
		public IdentifierOrValueExpression Value
		{
			get
			{
				return this._value;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._value = value;
			}
		}

		// Token: 0x06002729 RID: 10025 RVA: 0x00164C62 File Offset: 0x00162E62
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600272A RID: 10026 RVA: 0x00164C6E File Offset: 0x00162E6E
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.Value != null)
			{
				this.Value.Accept(visitor);
			}
		}

		// Token: 0x04001B83 RID: 7043
		private IdentifierOrValueExpression _value;
	}
}
