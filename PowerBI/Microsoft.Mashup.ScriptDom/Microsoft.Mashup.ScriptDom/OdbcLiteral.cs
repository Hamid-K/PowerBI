using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000066 RID: 102
	[Serializable]
	internal class OdbcLiteral : Literal
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000229 RID: 553 RVA: 0x00006789 File Offset: 0x00004989
		public override LiteralType LiteralType
		{
			get
			{
				return LiteralType.Odbc;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600022A RID: 554 RVA: 0x0000678C File Offset: 0x0000498C
		// (set) Token: 0x0600022B RID: 555 RVA: 0x00006794 File Offset: 0x00004994
		public OdbcLiteralType OdbcLiteralType
		{
			get
			{
				return this._odbcLiteralType;
			}
			set
			{
				this._odbcLiteralType = value;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600022C RID: 556 RVA: 0x0000679D File Offset: 0x0000499D
		// (set) Token: 0x0600022D RID: 557 RVA: 0x000067A5 File Offset: 0x000049A5
		public bool IsNational
		{
			get
			{
				return this._isNational;
			}
			set
			{
				this._isNational = value;
			}
		}

		// Token: 0x0600022E RID: 558 RVA: 0x000067AE File Offset: 0x000049AE
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600022F RID: 559 RVA: 0x000067BA File Offset: 0x000049BA
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x0400017F RID: 383
		private OdbcLiteralType _odbcLiteralType;

		// Token: 0x04000180 RID: 384
		private bool _isNational;
	}
}
