using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000222 RID: 546
	[Serializable]
	internal class AlterFunctionStatement : FunctionStatementBody
	{
		// Token: 0x060024EB RID: 9451 RVA: 0x0016250B File Offset: 0x0016070B
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060024EC RID: 9452 RVA: 0x00162518 File Offset: 0x00160718
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
