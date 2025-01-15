using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000061 RID: 97
	[Serializable]
	internal class BinaryLiteral : Literal
	{
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600020F RID: 527 RVA: 0x000066B6 File Offset: 0x000048B6
		public override LiteralType LiteralType
		{
			get
			{
				return LiteralType.Binary;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000210 RID: 528 RVA: 0x000066B9 File Offset: 0x000048B9
		// (set) Token: 0x06000211 RID: 529 RVA: 0x000066C1 File Offset: 0x000048C1
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

		// Token: 0x06000212 RID: 530 RVA: 0x000066CA File Offset: 0x000048CA
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06000213 RID: 531 RVA: 0x000066D6 File Offset: 0x000048D6
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x0400017C RID: 380
		private bool _isLargeObject;
	}
}
