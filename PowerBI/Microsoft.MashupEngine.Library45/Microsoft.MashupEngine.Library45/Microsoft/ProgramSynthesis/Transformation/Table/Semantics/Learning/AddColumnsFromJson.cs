using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.ProgramSynthesis.Extraction.Json;
using Microsoft.ProgramSynthesis.Transformation.Table.Build;
using Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Table.Semantics.Meta;
using Microsoft.ProgramSynthesis.VersionSpace;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.Exceptions;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Semantics.Learning
{
	// Token: 0x02001AF6 RID: 6902
	internal class AddColumnsFromJson : ColumnSuggester
	{
		// Token: 0x0600E3A1 RID: 58273 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public override bool CanBeDestructive()
		{
			return false;
		}

		// Token: 0x0600E3A2 RID: 58274 RVA: 0x00304F49 File Offset: 0x00303149
		public override bool CanSuggest(Operators allowedOperators)
		{
			return allowedOperators.HasFlag(Operators.AddColumnsFromJson);
		}

		// Token: 0x0600E3A3 RID: 58275 RVA: 0x00304F60 File Offset: 0x00303160
		public override ProgramSetBuilder<table> SuggestForColumn(GrammarBuilders build, Options options, Table<object> inputTable, int columnIdx, string columnName)
		{
			Program program;
			if (this.TryParseColumnAsJson(inputTable.Column(columnIdx), out program))
			{
				return build.Set.Join.AddColumnsFromJson(build.Node.UnnamedConversion.table_inputTable(build.Node.Variable.inputTable).ToProgramSet<table>(), build.Node.Rule.sourceColumnName(columnName).ToProgramSet<sourceColumnName>(), build.Node.Rule.ejsonProgram(program).ToProgramSet<ejsonProgram>());
			}
			return null;
		}

		// Token: 0x0600E3A4 RID: 58276 RVA: 0x00304FE4 File Offset: 0x003031E4
		private bool TryParseColumnAsJson(IEnumerable<object> column, out Program program)
		{
			program = null;
			List<string> list = null;
			try
			{
				list = column.Cast<string>().Select(delegate(string s)
				{
					if (s == null)
					{
						return null;
					}
					return s.Trim();
				}).ToList<string>();
			}
			catch (Exception ex) when (ex is ArgumentNullException || ex is InvalidCastException)
			{
				return false;
			}
			if (list.Any((string s) => string.IsNullOrWhiteSpace(s)))
			{
				return false;
			}
			string text = "[" + string.Join(",", list) + "]";
			try
			{
				IEnumerable<Constraint<string, ITable<string>>> enumerable = Enumerable.Empty<Constraint<string, ITable<string>>>();
				program = Learner.Instance.Learn(enumerable, new string[] { text }, default(CancellationToken));
			}
			catch (LearningException)
			{
			}
			return program != null;
		}
	}
}
