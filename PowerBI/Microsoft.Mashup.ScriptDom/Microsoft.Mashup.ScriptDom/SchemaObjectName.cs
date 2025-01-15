using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200004A RID: 74
	[Serializable]
	internal class SchemaObjectName : MultiPartIdentifier
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x000061B9 File Offset: 0x000043B9
		public virtual Identifier ServerIdentifier
		{
			get
			{
				return this.ChooseIdentifier(4);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x000061C2 File Offset: 0x000043C2
		public virtual Identifier DatabaseIdentifier
		{
			get
			{
				return this.ChooseIdentifier(3);
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x000061CB File Offset: 0x000043CB
		public virtual Identifier SchemaIdentifier
		{
			get
			{
				return this.ChooseIdentifier(2);
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x000061D4 File Offset: 0x000043D4
		public virtual Identifier BaseIdentifier
		{
			get
			{
				return this.ChooseIdentifier(1);
			}
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x000061E0 File Offset: 0x000043E0
		protected Identifier ChooseIdentifier(int modifier)
		{
			int num = base.Identifiers.Count - modifier;
			if (num < 0)
			{
				return null;
			}
			return base.Identifiers[num];
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000620D File Offset: 0x0000440D
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00006219 File Offset: 0x00004419
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x0400014F RID: 335
		private const int MaxIdentifiers = 5;

		// Token: 0x04000150 RID: 336
		private const int ServerModifier = 4;

		// Token: 0x04000151 RID: 337
		private const int DatabaseModifier = 3;

		// Token: 0x04000152 RID: 338
		private const int SchemaModifier = 2;

		// Token: 0x04000153 RID: 339
		private const int BaseModifier = 1;
	}
}
