using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200005C RID: 92
	[Serializable]
	internal abstract class Literal : ValueExpression
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060001FA RID: 506
		public abstract LiteralType LiteralType { get; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060001FB RID: 507 RVA: 0x00006613 File Offset: 0x00004813
		// (set) Token: 0x060001FC RID: 508 RVA: 0x0000661B File Offset: 0x0000481B
		public string Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00006624 File Offset: 0x00004824
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x0400017B RID: 379
		private string _value;
	}
}
