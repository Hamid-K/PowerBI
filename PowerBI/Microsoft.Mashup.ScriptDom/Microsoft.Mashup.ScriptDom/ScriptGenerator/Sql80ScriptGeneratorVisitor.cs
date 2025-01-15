using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom.ScriptGenerator
{
	// Token: 0x02000199 RID: 409
	internal sealed class Sql80ScriptGeneratorVisitor : SqlScriptGeneratorVisitor
	{
		// Token: 0x06002198 RID: 8600 RVA: 0x0015E5FA File Offset: 0x0015C7FA
		public Sql80ScriptGeneratorVisitor(SqlScriptGeneratorOptions options, ScriptWriter writer)
			: base(options, writer)
		{
			options.SqlVersion = SqlVersion.Sql80;
		}

		// Token: 0x06002199 RID: 8601 RVA: 0x0015E60C File Offset: 0x0015C80C
		protected override void GenerateIndexOptions(IList<IndexOption> options)
		{
			if (options != null && options.Count > 0)
			{
				bool flag = true;
				foreach (IndexOption indexOption in options)
				{
					IndexStateOption indexStateOption = indexOption as IndexStateOption;
					if (indexStateOption == null || indexStateOption.OptionState == OptionState.On)
					{
						if (flag)
						{
							flag = false;
							base.NewLineAndIndent();
							base.GenerateKeyword(TSqlTokenType.With);
							base.GenerateSpace();
						}
						else
						{
							base.GenerateSymbolAndSpace(TSqlTokenType.Comma);
						}
						base.GenerateFragmentIfNotNull(indexOption);
					}
				}
			}
		}

		// Token: 0x0600219A RID: 8602 RVA: 0x0015E6A0 File Offset: 0x0015C8A0
		public override void ExplicitVisit(IndexStateOption node)
		{
			if (node.OptionState == OptionState.On)
			{
				IndexOptionHelper.Instance.GenerateSourceForOption(this._writer, node.OptionKind);
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600219B RID: 8603 RVA: 0x0015E6C1 File Offset: 0x0015C8C1
		internal override HashSet<Type> StatementsThatCannotHaveSemiColon
		{
			get
			{
				return Sql80ScriptGeneratorVisitor._typesCantHaveSemiColon;
			}
		}

		// Token: 0x0600219C RID: 8604 RVA: 0x0015E6C8 File Offset: 0x0015C8C8
		// Note: this type is marked as 'beforefieldinit'.
		static Sql80ScriptGeneratorVisitor()
		{
			HashSet<Type> hashSet = new HashSet<Type>();
			hashSet.Add(typeof(CreateViewStatement));
			hashSet.Add(typeof(AlterViewStatement));
			hashSet.Add(typeof(CreateFunctionStatement));
			hashSet.Add(typeof(AlterFunctionStatement));
			hashSet.Add(typeof(CreateDefaultStatement));
			hashSet.Add(typeof(CreateRuleStatement));
			hashSet.Add(typeof(CreateSchemaStatement));
			hashSet.Add(typeof(TSqlStatementSnippet));
			hashSet.Add(typeof(CreateTriggerStatement));
			hashSet.Add(typeof(AlterTriggerStatement));
			hashSet.Add(typeof(CreateProcedureStatement));
			hashSet.Add(typeof(AlterProcedureStatement));
			hashSet.Add(typeof(BeginEndBlockStatement));
			hashSet.Add(typeof(IfStatement));
			hashSet.Add(typeof(WhileStatement));
			hashSet.Add(typeof(LabelStatement));
			hashSet.Add(typeof(TryCatchStatement));
			Sql80ScriptGeneratorVisitor._typesCantHaveSemiColon = hashSet;
		}

		// Token: 0x040019BA RID: 6586
		private static HashSet<Type> _typesCantHaveSemiColon;
	}
}
