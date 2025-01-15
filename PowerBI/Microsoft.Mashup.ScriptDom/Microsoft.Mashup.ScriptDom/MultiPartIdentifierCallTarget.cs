using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000217 RID: 535
	[Serializable]
	internal class MultiPartIdentifierCallTarget : CallTarget
	{
		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060024AD RID: 9389 RVA: 0x0016207B File Offset: 0x0016027B
		// (set) Token: 0x060024AE RID: 9390 RVA: 0x00162083 File Offset: 0x00160283
		public MultiPartIdentifier MultiPartIdentifier
		{
			get
			{
				return this._multiPartIdentifier;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._multiPartIdentifier = value;
			}
		}

		// Token: 0x060024AF RID: 9391 RVA: 0x00162093 File Offset: 0x00160293
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060024B0 RID: 9392 RVA: 0x0016209F File Offset: 0x0016029F
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.MultiPartIdentifier != null)
			{
				this.MultiPartIdentifier.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001AD6 RID: 6870
		private MultiPartIdentifier _multiPartIdentifier;
	}
}
