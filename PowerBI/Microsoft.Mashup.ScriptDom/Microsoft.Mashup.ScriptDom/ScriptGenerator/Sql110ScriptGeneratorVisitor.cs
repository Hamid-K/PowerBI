using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom.ScriptGenerator
{
	// Token: 0x020000DD RID: 221
	internal class Sql110ScriptGeneratorVisitor : SqlScriptGeneratorVisitor
	{
		// Token: 0x06001421 RID: 5153 RVA: 0x0008EE60 File Offset: 0x0008D060
		public Sql110ScriptGeneratorVisitor(SqlScriptGeneratorOptions options, ScriptWriter writer)
			: base(options, writer)
		{
			options.SqlVersion = SqlVersion.Sql110;
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06001422 RID: 5154 RVA: 0x0008EE71 File Offset: 0x0008D071
		internal override HashSet<Type> StatementsThatCannotHaveSemiColon
		{
			get
			{
				return Sql110ScriptGeneratorVisitor._typesCantHaveSemiColon;
			}
		}

		// Token: 0x06001423 RID: 5155 RVA: 0x0008EE78 File Offset: 0x0008D078
		// Note: this type is marked as 'beforefieldinit'.
		static Sql110ScriptGeneratorVisitor()
		{
			HashSet<Type> hashSet = new HashSet<Type>();
			hashSet.Add(typeof(CreateProcedureStatement));
			hashSet.Add(typeof(AlterProcedureStatement));
			hashSet.Add(typeof(CreateFunctionStatement));
			hashSet.Add(typeof(AlterFunctionStatement));
			hashSet.Add(typeof(CreateTriggerStatement));
			hashSet.Add(typeof(AlterTriggerStatement));
			hashSet.Add(typeof(TSqlStatementSnippet));
			hashSet.Add(typeof(BeginEndBlockStatement));
			hashSet.Add(typeof(IfStatement));
			hashSet.Add(typeof(WhileStatement));
			hashSet.Add(typeof(LabelStatement));
			hashSet.Add(typeof(TryCatchStatement));
			Sql110ScriptGeneratorVisitor._typesCantHaveSemiColon = hashSet;
		}

		// Token: 0x04000932 RID: 2354
		private static HashSet<Type> _typesCantHaveSemiColon;
	}
}
