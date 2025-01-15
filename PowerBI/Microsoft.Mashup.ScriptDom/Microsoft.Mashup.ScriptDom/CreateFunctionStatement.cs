using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200022D RID: 557
	[Serializable]
	internal class CreateFunctionStatement : FunctionStatementBody
	{
		// Token: 0x0600251D RID: 9501 RVA: 0x00162836 File Offset: 0x00160A36
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600251E RID: 9502 RVA: 0x00162844 File Offset: 0x00160A44
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (base.Name != null)
			{
				base.Name.Accept(visitor);
			}
			int i = 0;
			int count = base.Parameters.Count;
			while (i < count)
			{
				base.Parameters[i].Accept(visitor);
				i++;
			}
			if (base.ReturnType != null)
			{
				base.ReturnType.Accept(visitor);
			}
			int j = 0;
			int count2 = base.Options.Count;
			while (j < count2)
			{
				base.Options[j].Accept(visitor);
				j++;
			}
			if (base.StatementList != null)
			{
				base.StatementList.Accept(visitor);
			}
			if (base.MethodSpecifier != null)
			{
				base.MethodSpecifier.Accept(visitor);
			}
			if (base.OrderHint != null)
			{
				base.OrderHint.Accept(visitor);
			}
		}
	}
}
