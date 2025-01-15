using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000455 RID: 1109
	[Serializable]
	internal class CreateDatabaseEncryptionKeyStatement : DatabaseEncryptionKeyStatement
	{
		// Token: 0x06003215 RID: 12821 RVA: 0x0016FD2D File Offset: 0x0016DF2D
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06003216 RID: 12822 RVA: 0x0016FD39 File Offset: 0x0016DF39
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
