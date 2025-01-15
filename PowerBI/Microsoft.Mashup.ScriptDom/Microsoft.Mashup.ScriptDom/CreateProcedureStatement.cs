using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001C3 RID: 451
	[Serializable]
	internal class CreateProcedureStatement : ProcedureStatementBody
	{
		// Token: 0x0600229D RID: 8861 RVA: 0x0015F9BD File Offset: 0x0015DBBD
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600229E RID: 8862 RVA: 0x0015F9CC File Offset: 0x0015DBCC
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
