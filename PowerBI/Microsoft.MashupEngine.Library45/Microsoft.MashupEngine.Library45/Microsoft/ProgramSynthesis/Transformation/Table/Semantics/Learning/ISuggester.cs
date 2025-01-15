using System;
using Microsoft.ProgramSynthesis.Transformation.Table.Build;
using Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Table.Semantics.Meta;
using Microsoft.ProgramSynthesis.VersionSpace;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Semantics.Learning
{
	// Token: 0x02001B21 RID: 6945
	public interface ISuggester
	{
		// Token: 0x0600E468 RID: 58472
		ProgramSetBuilder<table> Suggest(GrammarBuilders build, Options options, Table<object> inputTable);

		// Token: 0x0600E469 RID: 58473
		bool CanBeDestructive();

		// Token: 0x0600E46A RID: 58474
		bool CanSuggest(Operators allowedOperators);
	}
}
