using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.ConceptualSchema.Annotations;

namespace Microsoft.InfoNav.Data.Annotations
{
	// Token: 0x0200002C RID: 44
	internal sealed class ColumnGroupingAnnotationsProviderBuilder
	{
		// Token: 0x06000199 RID: 409 RVA: 0x00008F30 File Offset: 0x00007130
		internal static ColumnGroupingAnnotations BuildColumnGroupingAnnotations(IConceptualSchema schema)
		{
			Dictionary<IConceptualColumn, ColumnGroupingAnnotation> dictionary = new Dictionary<IConceptualColumn, ColumnGroupingAnnotation>();
			foreach (IConceptualEntity conceptualEntity in schema.Entities)
			{
				foreach (IConceptualProperty conceptualProperty in conceptualEntity.Properties)
				{
					IConceptualColumn conceptualColumn = conceptualProperty as IConceptualColumn;
					if (conceptualColumn != null)
					{
						ConceptualColumnGrouping grouping = conceptualColumn.Grouping;
						if (grouping.Identity != GroupingIdentity.EntityKey && !conceptualColumn.IsRowNumber)
						{
							ColumnGroupingAnnotationsProviderBuilder.UpdateReverseQueryGroupColumnReferences(dictionary, grouping.QueryGroupColumns, conceptualColumn);
							if (!grouping.IsQueryGroupOnEntityKey)
							{
								ColumnGroupingAnnotationsProviderBuilder.UpdateReverseQueryGroupColumnReferences(dictionary, conceptualEntity.KeyColumns, conceptualColumn);
							}
						}
					}
				}
			}
			return new ColumnGroupingAnnotations(dictionary);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00009008 File Offset: 0x00007208
		private static void UpdateReverseQueryGroupColumnReferences(Dictionary<IConceptualColumn, ColumnGroupingAnnotation> annotations, IReadOnlyList<IConceptualColumn> columns, IConceptualColumn identity)
		{
			foreach (IConceptualColumn conceptualColumn in columns)
			{
				if (!conceptualColumn.IsRowNumber)
				{
					ColumnGroupingAnnotation columnGroupingAnnotation;
					if (!annotations.TryGetValue(conceptualColumn, out columnGroupingAnnotation))
					{
						columnGroupingAnnotation = new WritableColumnGroupingAnnotation();
						annotations.Add(conceptualColumn, columnGroupingAnnotation);
					}
					((WritableColumnGroupingAnnotation)columnGroupingAnnotation).AddColumn(identity);
				}
			}
		}
	}
}
