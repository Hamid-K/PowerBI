using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom.ScriptGenerator
{
	// Token: 0x0200018C RID: 396
	internal class Sql100ScriptGeneratorVisitor : SqlScriptGeneratorVisitor
	{
		// Token: 0x0600215C RID: 8540 RVA: 0x0015DC7D File Offset: 0x0015BE7D
		public Sql100ScriptGeneratorVisitor(SqlScriptGeneratorOptions options, ScriptWriter writer)
			: base(options, writer)
		{
			options.SqlVersion = SqlVersion.Sql100;
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600215D RID: 8541 RVA: 0x0015DC8E File Offset: 0x0015BE8E
		internal override HashSet<Type> StatementsThatCannotHaveSemiColon
		{
			get
			{
				return Sql100ScriptGeneratorVisitor._typesCantHaveSemiColon;
			}
		}

		// Token: 0x0600215E RID: 8542 RVA: 0x0015DC98 File Offset: 0x0015BE98
		// Note: this type is marked as 'beforefieldinit'.
		static Sql100ScriptGeneratorVisitor()
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
			Sql100ScriptGeneratorVisitor._typesCantHaveSemiColon = hashSet;
		}

		// Token: 0x040019A3 RID: 6563
		private static HashSet<Type> _typesCantHaveSemiColon;
	}
}
