using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000062 RID: 98
	[Serializable]
	internal class StringLiteral : Literal
	{
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000215 RID: 533 RVA: 0x000066E7 File Offset: 0x000048E7
		public override LiteralType LiteralType
		{
			get
			{
				return LiteralType.String;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000216 RID: 534 RVA: 0x000066EA File Offset: 0x000048EA
		// (set) Token: 0x06000217 RID: 535 RVA: 0x000066F2 File Offset: 0x000048F2
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

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000218 RID: 536 RVA: 0x000066FB File Offset: 0x000048FB
		// (set) Token: 0x06000219 RID: 537 RVA: 0x00006703 File Offset: 0x00004903
		public bool IsLargeObject
		{
			get
			{
				return this._isLargeObject;
			}
			set
			{
				this._isLargeObject = value;
			}
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000670C File Offset: 0x0000490C
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00006718 File Offset: 0x00004918
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x0400017D RID: 381
		private bool _isNational;

		// Token: 0x0400017E RID: 382
		private bool _isLargeObject;
	}
}
