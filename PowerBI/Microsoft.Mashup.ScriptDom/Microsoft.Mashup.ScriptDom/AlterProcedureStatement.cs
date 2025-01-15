using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001C2 RID: 450
	[Serializable]
	internal class AlterProcedureStatement : ProcedureStatementBody
	{
		// Token: 0x0600229A RID: 8858 RVA: 0x0015F909 File Offset: 0x0015DB09
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600229B RID: 8859 RVA: 0x0015F918 File Offset: 0x0015DB18
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (base.ProcedureReference != null)
			{
				base.ProcedureReference.Accept(visitor);
			}
			int i = 0;
			int count = base.Parameters.Count;
			while (i < count)
			{
				base.Parameters[i].Accept(visitor);
				i++;
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
		}
	}
}
