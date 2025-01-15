using System;
using System.Collections.Generic;
using Microsoft.DataShaping;
using Microsoft.InfoNav.Utils;
using Microsoft.Reporting.QueryDesign.BatchQueries;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchQueryGeneration
{
	// Token: 0x0200015D RID: 349
	internal static class RenameColumnsWithExplictNameGenerator
	{
		// Token: 0x06000CB7 RID: 3255 RVA: 0x000347BC File Offset: 0x000329BC
		internal static GeneratedTable Generate(GeneratedTable input, IReadOnlyList<ColumnWithExplicitName> explicitlyNamedColumns, BatchQueryGenerationNamingContext namingContext)
		{
			Dictionary<QueryTableColumn, string> dictionary = new Dictionary<QueryTableColumn, string>();
			foreach (ColumnWithExplicitName columnWithExplicitName in explicitlyNamedColumns)
			{
				Contract.RetailAssert(namingContext.RegisterName(columnWithExplicitName.Name), "Unable to register name for explicity named column {0}", columnWithExplicitName.Name.MarkAsCustomerContent());
				QueryTableColumn queryTableColumn;
				if (input.ColumnMap.TryGetColumn(columnWithExplicitName.ExpressionId, out queryTableColumn))
				{
					dictionary.Add(queryTableColumn, columnWithExplicitName.Name);
				}
			}
			IReadOnlyList<QueryTableColumn> columns = input.QueryTable.Columns;
			Dictionary<QueryTableColumn, QueryTableColumn> dictionary2 = new Dictionary<QueryTableColumn, QueryTableColumn>();
			List<QueryTableColumn> list = new List<QueryTableColumn>(columns.Count);
			foreach (QueryTableColumn queryTableColumn2 in columns)
			{
				string text;
				if (!dictionary.TryGetValue(queryTableColumn2, out text))
				{
					text = namingContext.CreateAndRegisterUniqueName(queryTableColumn2.Name);
				}
				QueryTableColumn queryTableColumn3 = queryTableColumn2.QdmReference().ToQueryTableColumn(text);
				dictionary2[queryTableColumn2] = queryTableColumn3;
				list.Add(queryTableColumn3);
			}
			WritableGeneratedColumnMap writableGeneratedColumnMap = input.ColumnMap.GenerateWithReplacements(dictionary2, false);
			return new GeneratedTable(input.QueryTable.Project(list, ProjectSubsetStrategy.Default), writableGeneratedColumnMap);
		}
	}
}
