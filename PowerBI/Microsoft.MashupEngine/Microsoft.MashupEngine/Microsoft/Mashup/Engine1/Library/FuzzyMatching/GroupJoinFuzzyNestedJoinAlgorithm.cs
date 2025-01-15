using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.FuzzyMatching
{
	// Token: 0x02000B53 RID: 2899
	public class GroupJoinFuzzyNestedJoinAlgorithm : GroupJoinNestedJoinAlgorithm
	{
		// Token: 0x0600503A RID: 20538 RVA: 0x0010CC0F File Offset: 0x0010AE0F
		public GroupJoinFuzzyNestedJoinAlgorithm(IEngineHost host)
		{
			this.host = host;
		}

		// Token: 0x0600503B RID: 20539 RVA: 0x0010CC20 File Offset: 0x0010AE20
		public override IEnumerable<IValueReference> NestedJoin(NestedJoinParameters parameters)
		{
			TableValue tableValue = new QueryTableValue(parameters.LeftQuery);
			TextValue textValue = TextValue.New(parameters.NewColumnName);
			TableValue asTable = parameters.RightTable.AsTable;
			KeysBuilder keysBuilder = new KeysBuilder(asTable.Columns.Length + 1);
			keysBuilder.Union(asTable.Columns);
			FuzzyJoinOptions joinOptions = ((FuzzyNestedJoinParameters)parameters).JoinOptions;
			if (joinOptions.SimilarityColumnName != null)
			{
				keysBuilder.Add(joinOptions.SimilarityColumnName);
			}
			IFunctionExpression functionExpression = new FunctionExpressionSyntaxNode(Microsoft.Mashup.Engine1.Language.Query.QueryHelpers.EachFunctionType, new RequiredMultiFieldRecordProjectionExpressionSyntaxNode(new InclusiveIdentifierExpressionSyntaxNode(Identifier.Underscore), GroupJoinNestedJoinAlgorithm.IdentifiersFromKeys(keysBuilder.ToKeys())));
			return base.Invoke(tableValue, parameters.LeftKeyColumns, parameters.RightTable.AsTable, TableValue.GetColumns(parameters.RightTable.AsTable.Columns, parameters.RightKey), textValue, functionExpression, parameters);
		}

		// Token: 0x0600503C RID: 20540 RVA: 0x0010CCF4 File Offset: 0x0010AEF4
		protected override TableValue GetJoinTableValue(TableValue leftTable, int[] leftKeyColumns, TableValue rightTable, int[] rightKeyColumns, NestedJoinParameters parameters)
		{
			return FuzzyMatchingTableValueFactory.FuzzyJoin(this.host, leftTable, leftKeyColumns, rightTable, rightKeyColumns, parameters.JoinKind, FuzzyJoinAlgorithm.Fuzzy, null, ((FuzzyNestedJoinParameters)parameters).JoinOptions);
		}

		// Token: 0x04002B0D RID: 11021
		private readonly IEngineHost host;
	}
}
