using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200004B RID: 75
	[Serializable]
	internal class ChildObjectName : SchemaObjectName
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060001DB RID: 475 RVA: 0x0000622A File Offset: 0x0000442A
		public override Identifier BaseIdentifier
		{
			get
			{
				return base.ChooseIdentifier(2);
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060001DC RID: 476 RVA: 0x00006233 File Offset: 0x00004433
		public override Identifier DatabaseIdentifier
		{
			get
			{
				return base.ChooseIdentifier(4);
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060001DD RID: 477 RVA: 0x0000623C File Offset: 0x0000443C
		public override Identifier SchemaIdentifier
		{
			get
			{
				return base.ChooseIdentifier(3);
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060001DE RID: 478 RVA: 0x00006245 File Offset: 0x00004445
		public override Identifier ServerIdentifier
		{
			get
			{
				return base.ChooseIdentifier(5);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060001DF RID: 479 RVA: 0x0000624E File Offset: 0x0000444E
		public virtual Identifier ChildIdentifier
		{
			get
			{
				return base.ChooseIdentifier(1);
			}
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00006257 File Offset: 0x00004457
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00006263 File Offset: 0x00004463
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04000154 RID: 340
		private const int ServerModifierNumber = 5;

		// Token: 0x04000155 RID: 341
		private const int DatabaseModifierNumber = 4;

		// Token: 0x04000156 RID: 342
		private const int SchemaModifierNumber = 3;

		// Token: 0x04000157 RID: 343
		private const int BaseModifierNumber = 2;

		// Token: 0x04000158 RID: 344
		private const int ChildModifierNumber = 1;
	}
}
