using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001ED RID: 493
	[Serializable]
	internal class UserDefinedTypePropertyAccess : PrimaryExpression
	{
		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600238A RID: 9098 RVA: 0x00160A91 File Offset: 0x0015EC91
		// (set) Token: 0x0600238B RID: 9099 RVA: 0x00160A99 File Offset: 0x0015EC99
		public CallTarget CallTarget
		{
			get
			{
				return this._callTarget;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._callTarget = value;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600238C RID: 9100 RVA: 0x00160AA9 File Offset: 0x0015ECA9
		// (set) Token: 0x0600238D RID: 9101 RVA: 0x00160AB1 File Offset: 0x0015ECB1
		public Identifier PropertyName
		{
			get
			{
				return this._propertyName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._propertyName = value;
			}
		}

		// Token: 0x0600238E RID: 9102 RVA: 0x00160AC1 File Offset: 0x0015ECC1
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600238F RID: 9103 RVA: 0x00160ACD File Offset: 0x0015ECCD
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.CallTarget != null)
			{
				this.CallTarget.Accept(visitor);
			}
			if (this.PropertyName != null)
			{
				this.PropertyName.Accept(visitor);
			}
		}

		// Token: 0x04001A79 RID: 6777
		private CallTarget _callTarget;

		// Token: 0x04001A7A RID: 6778
		private Identifier _propertyName;
	}
}
