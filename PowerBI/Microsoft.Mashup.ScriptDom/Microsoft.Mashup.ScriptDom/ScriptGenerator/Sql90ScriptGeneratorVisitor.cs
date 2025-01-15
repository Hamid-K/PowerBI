using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom.ScriptGenerator
{
	// Token: 0x0200019A RID: 410
	internal class Sql90ScriptGeneratorVisitor : SqlScriptGeneratorVisitor
	{
		// Token: 0x0600219D RID: 8605 RVA: 0x0015E802 File Offset: 0x0015CA02
		public Sql90ScriptGeneratorVisitor(SqlScriptGeneratorOptions options, ScriptWriter writer)
			: base(options, writer)
		{
			options.SqlVersion = SqlVersion.Sql90;
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600219E RID: 8606 RVA: 0x0015E813 File Offset: 0x0015CA13
		internal override HashSet<Type> StatementsThatCannotHaveSemiColon
		{
			get
			{
				return Sql90ScriptGeneratorVisitor._typesCantHaveSemiColon;
			}
		}

		// Token: 0x0600219F RID: 8607 RVA: 0x0015E81C File Offset: 0x0015CA1C
		// Note: this type is marked as 'beforefieldinit'.
		static Sql90ScriptGeneratorVisitor()
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
			Sql90ScriptGeneratorVisitor._typesCantHaveSemiColon = hashSet;
		}

		// Token: 0x040019BB RID: 6587
		private static HashSet<Type> _typesCantHaveSemiColon;
	}
}
