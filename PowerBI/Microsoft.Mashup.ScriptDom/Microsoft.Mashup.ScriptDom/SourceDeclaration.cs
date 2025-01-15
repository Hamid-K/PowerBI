using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000476 RID: 1142
	[Serializable]
	internal class SourceDeclaration : ScalarExpression
	{
		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x060032CC RID: 13004 RVA: 0x001708BB File Offset: 0x0016EABB
		// (set) Token: 0x060032CD RID: 13005 RVA: 0x001708C3 File Offset: 0x0016EAC3
		public EventSessionObjectName Value
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

		// Token: 0x060032CE RID: 13006 RVA: 0x001708D3 File Offset: 0x0016EAD3
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060032CF RID: 13007 RVA: 0x001708DF File Offset: 0x0016EADF
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Value != null)
			{
				this.Value.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001EC6 RID: 7878
		private EventSessionObjectName _value;
	}
}
