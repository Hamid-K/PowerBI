using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.Data.Contracts.ResolvedSemanticQuery
{
	// Token: 0x02000099 RID: 153
	internal sealed class ResolvedQueryDefinitionSerializer
	{
		// Token: 0x060003BA RID: 954 RVA: 0x0000A2CD File Offset: 0x000084CD
		private ResolvedQueryDefinitionSerializer()
		{
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0000A2D5 File Offset: 0x000084D5
		internal static bool TrySerializeResolvedQuery(ResolvedQueryDefinition resolvedQuery, out QueryDefinition query)
		{
			return new ResolvedQueryDefinitionSerializer().TrySerializeResolvedQueryDefinition(resolvedQuery, out query);
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0000A2E3 File Offset: 0x000084E3
		internal static bool TrySerializeResolvedFilterDefinition(ResolvedFilterDefinition resolvedFilterDefinition, out FilterDefinition filterDefinition)
		{
			return new ResolvedQueryDefinitionSerializer().TrySerializeFilterDefinition(resolvedFilterDefinition, out filterDefinition);
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0000A2F4 File Offset: 0x000084F4
		private static bool TrySerializeEach<InputType, OutputType>(IEnumerable<InputType> inputList, ResolvedQueryDefinitionSerializer.TryResolveItem<InputType, OutputType> trySerialize, out List<OutputType> outputList)
		{
			bool flag = true;
			outputList = new List<OutputType>();
			foreach (InputType inputType in inputList)
			{
				OutputType outputType;
				if (trySerialize(inputType, out outputType))
				{
					outputList.Add(outputType);
				}
				else
				{
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0000A358 File Offset: 0x00008558
		private bool TrySerializeResolvedQueryDefinition(ResolvedQueryDefinition resolvedQuery, out QueryDefinition query)
		{
			List<QueryExpressionContainer> list;
			ResolvedQueryDefinitionSerializer.TrySerializeEach<ResolvedQueryParameterDeclaration, QueryExpressionContainer>(resolvedQuery.Parameters, new ResolvedQueryDefinitionSerializer.TryResolveItem<ResolvedQueryParameterDeclaration, QueryExpressionContainer>(this.TrySerializeParameterDeclaration), out list);
			List<QueryExpressionContainer> list2;
			List<EntitySource> list3;
			List<QueryTransform> list4;
			List<QuerySortClause> list5;
			List<QueryExpressionContainer> list6;
			List<QueryAxis> list7;
			List<QueryExpressionContainer> list8;
			List<QueryFilter> list9;
			if (!(ResolvedQueryDefinitionSerializer.TrySerializeEach<ResolvedQueryLetBinding, QueryExpressionContainer>(resolvedQuery.Let, new ResolvedQueryDefinitionSerializer.TryResolveItem<ResolvedQueryLetBinding, QueryExpressionContainer>(this.TrySerializeLetBinding), out list2) & ResolvedQueryDefinitionSerializer.TrySerializeEach<ResolvedQuerySource, EntitySource>(resolvedQuery.From, new ResolvedQueryDefinitionSerializer.TryResolveItem<ResolvedQuerySource, EntitySource>(this.TrySerializeResolvedQuerySource), out list3) & ResolvedQueryDefinitionSerializer.TrySerializeEach<ResolvedQueryTransform, QueryTransform>(resolvedQuery.Transform, new ResolvedQueryDefinitionSerializer.TryResolveItem<ResolvedQueryTransform, QueryTransform>(this.TrySerializeTransform), out list4) & ResolvedQueryDefinitionSerializer.TrySerializeEach<ResolvedQuerySortClause, QuerySortClause>(resolvedQuery.OrderBy.EmptyIfNull<ResolvedQuerySortClause>(), new ResolvedQueryDefinitionSerializer.TryResolveItem<ResolvedQuerySortClause, QuerySortClause>(this.TrySerializeQuerySortClause), out list5) & ResolvedQueryDefinitionSerializer.TrySerializeEach<ResolvedQuerySelect, QueryExpressionContainer>(resolvedQuery.Select, new ResolvedQueryDefinitionSerializer.TryResolveItem<ResolvedQuerySelect, QueryExpressionContainer>(ResolvedQueryDefinitionSerializer.TrySerializeQueryExpression), out list6) & ResolvedQueryDefinitionSerializer.TrySerializeEach<ResolvedQueryAxis, QueryAxis>(resolvedQuery.VisualShape, new ResolvedQueryDefinitionSerializer.TryResolveItem<ResolvedQueryAxis, QueryAxis>(this.TrySerializeAxis), out list7) & ResolvedQueryDefinitionSerializer.TrySerializeEach<ResolvedQueryExpression, QueryExpressionContainer>(resolvedQuery.GroupBy.EmptyIfNull<ResolvedQueryExpression>(), new ResolvedQueryDefinitionSerializer.TryResolveItem<ResolvedQueryExpression, QueryExpressionContainer>(ResolvedQueryDefinitionSerializer.TrySerializeQueryExpression), out list8) & ResolvedQueryDefinitionSerializer.TrySerializeEach<ResolvedQueryFilter, QueryFilter>(resolvedQuery.Where.EmptyIfNull<ResolvedQueryFilter>(), new ResolvedQueryDefinitionSerializer.TryResolveItem<ResolvedQueryFilter, QueryFilter>(this.TrySerializeQueryFilter), out list9)))
			{
				query = null;
				return false;
			}
			query = new QueryDefinition
			{
				Version = new int?(2),
				Parameters = ((list.Count > 0) ? list : null),
				Let = ((list2.Count > 0) ? list2 : null),
				From = list3,
				Where = ((list9.Count > 0) ? list9 : null),
				Transform = ((list4.Count > 0) ? list4 : null),
				OrderBy = ((list5.Count > 0) ? list5 : null),
				Select = list6,
				VisualShape = list7,
				GroupBy = ((list8.Count > 0) ? list8 : null),
				Top = resolvedQuery.Top,
				Skip = resolvedQuery.Skip
			};
			return true;
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0000A51F File Offset: 0x0000871F
		private bool TrySerializeParameterDeclaration(ResolvedQueryParameterDeclaration resolvedDeclaration, out QueryExpressionContainer declaration)
		{
			if (!ResolvedQueryDefinitionSerializer.TrySerializeQueryExpression(resolvedDeclaration.TypeExpression, out declaration))
			{
				return false;
			}
			declaration.Name = resolvedDeclaration.Name;
			return true;
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0000A53F File Offset: 0x0000873F
		private bool TrySerializeLetBinding(ResolvedQueryLetBinding resolvedBinding, out QueryExpressionContainer binding)
		{
			if (!ResolvedQueryDefinitionSerializer.TrySerializeQueryExpression(resolvedBinding.Expression, out binding))
			{
				return false;
			}
			binding.Name = resolvedBinding.Name;
			return true;
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0000A55F File Offset: 0x0000875F
		private bool TrySerializeQueryFilter(ResolvedQueryFilter resolvedQueryFilter, out QueryFilter queryFilter)
		{
			queryFilter = ResolvedQueryDefinitionSerializer.SerializeQueryFilter(resolvedQueryFilter);
			return queryFilter != null;
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0000A574 File Offset: 0x00008774
		internal static QueryFilter SerializeQueryFilter(ResolvedQueryFilter resolvedQueryFilter)
		{
			QueryExpressionContainer queryExpressionContainer;
			if (!ResolvedQueryDefinitionSerializer.TrySerializeQueryExpression(resolvedQueryFilter.Condition, out queryExpressionContainer))
			{
				return null;
			}
			List<QueryExpressionContainer> list = null;
			if (!resolvedQueryFilter.Target.IsNullOrEmpty<ResolvedQueryExpression>() && !ResolvedQueryDefinitionSerializer.TrySerializeEach<ResolvedQueryExpression, QueryExpressionContainer>(resolvedQueryFilter.Target, new ResolvedQueryDefinitionSerializer.TryResolveItem<ResolvedQueryExpression, QueryExpressionContainer>(ResolvedQueryDefinitionSerializer.TrySerializeQueryExpression), out list))
			{
				return null;
			}
			return new QueryFilter
			{
				Target = list,
				Condition = queryExpressionContainer,
				Annotations = resolvedQueryFilter.Annotations
			};
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0000A5DD File Offset: 0x000087DD
		private bool TrySerializeResolvedQuerySource(ResolvedQuerySource resolvedQuerySource, out EntitySource entitySource)
		{
			entitySource = ResolvedQueryDefinitionSerializer.SerializeResolvedQuerySource(resolvedQuerySource);
			return entitySource != null;
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0000A5F0 File Offset: 0x000087F0
		internal static EntitySource SerializeResolvedQuerySource(ResolvedQuerySource resolvedQuerySource)
		{
			ResolvedEntitySource resolvedEntitySource = resolvedQuerySource as ResolvedEntitySource;
			if (resolvedEntitySource != null)
			{
				if (resolvedEntitySource.Entity != null)
				{
					return new EntitySource
					{
						Name = resolvedEntitySource.Name,
						Entity = resolvedEntitySource.Entity.Name,
						Schema = ConceptualSchemaNames.NormalizeForSerialization(resolvedEntitySource.Schema)
					};
				}
			}
			else
			{
				ResolvedExpressionSource resolvedExpressionSource = resolvedQuerySource as ResolvedExpressionSource;
				QueryExpressionContainer queryExpressionContainer;
				if (resolvedExpressionSource != null && ResolvedQueryDefinitionSerializer.TrySerializeQueryExpression(resolvedExpressionSource.Expression, out queryExpressionContainer))
				{
					return new EntitySource
					{
						Name = resolvedExpressionSource.Name,
						Type = EntitySourceType.Expression,
						Expression = queryExpressionContainer
					};
				}
			}
			return null;
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0000A680 File Offset: 0x00008880
		private bool TrySerializeQuerySortClause(ResolvedQuerySortClause resolvedSortClause, out QuerySortClause sortClause)
		{
			QueryExpressionContainer queryExpressionContainer;
			if (!ResolvedQueryDefinitionSerializer.TrySerializeQueryExpression(resolvedSortClause.Expression, out queryExpressionContainer))
			{
				sortClause = null;
				return false;
			}
			sortClause = new QuerySortClause
			{
				Expression = queryExpressionContainer.Expression,
				Direction = resolvedSortClause.Direction
			};
			return true;
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0000A6C8 File Offset: 0x000088C8
		private bool TrySerializeAxis(ResolvedQueryAxis resolvedAxis, out QueryAxis axis)
		{
			List<QueryAxisGroup> list;
			if (!ResolvedQueryDefinitionSerializer.TrySerializeEach<ResolvedQueryAxisGroup, QueryAxisGroup>(resolvedAxis.Groups, new ResolvedQueryDefinitionSerializer.TryResolveItem<ResolvedQueryAxisGroup, QueryAxisGroup>(this.TrySerializeAxisGroup), out list))
			{
				axis = null;
				return false;
			}
			axis = new QueryAxis
			{
				Name = resolvedAxis.Name,
				Groups = list
			};
			return true;
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0000A710 File Offset: 0x00008910
		private bool TrySerializeAxisGroup(ResolvedQueryAxisGroup resolvedAxisGroup, out QueryAxisGroup axisGroup)
		{
			List<QueryExpressionContainer> list;
			if (!ResolvedQueryDefinitionSerializer.TrySerializeEach<ResolvedQueryExpression, QueryExpressionContainer>(resolvedAxisGroup.Keys, new ResolvedQueryDefinitionSerializer.TryResolveItem<ResolvedQueryExpression, QueryExpressionContainer>(ResolvedQueryDefinitionSerializer.TrySerializeQueryExpression), out list))
			{
				axisGroup = null;
				return false;
			}
			axisGroup = new QueryAxisGroup
			{
				Keys = list,
				Subtotal = resolvedAxisGroup.Subtotal
			};
			return true;
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0000A758 File Offset: 0x00008958
		private bool TrySerializeTransform(ResolvedQueryTransform resolvedTransform, out QueryTransform transform)
		{
			QueryTransformInput queryTransformInput;
			QueryTransformOutput queryTransformOutput;
			if (!(true & this.TrySerializeTransformInput(resolvedTransform.Input, out queryTransformInput) & this.TrySerializeTransformOutput(resolvedTransform.Output, out queryTransformOutput)))
			{
				transform = null;
				return false;
			}
			transform = new QueryTransform
			{
				Name = resolvedTransform.Name,
				Algorithm = resolvedTransform.Algorithm,
				Input = queryTransformInput,
				Output = queryTransformOutput
			};
			return true;
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0000A7BC File Offset: 0x000089BC
		private bool TrySerializeTransformInput(ResolvedQueryTransformInput resolvedInput, out QueryTransformInput input)
		{
			bool flag = true;
			List<QueryExpressionContainer> list = null;
			if (resolvedInput.Parameters != null)
			{
				flag &= ResolvedQueryDefinitionSerializer.TrySerializeEach<ResolvedQueryTransformParameter, QueryExpressionContainer>(resolvedInput.Parameters, new ResolvedQueryDefinitionSerializer.TryResolveItem<ResolvedQueryTransformParameter, QueryExpressionContainer>(this.TrySerializeTransformParameter), out list);
			}
			QueryTransformTable queryTransformTable;
			if (!(flag & this.TrySerializeTransformTable(resolvedInput.Table, out queryTransformTable)))
			{
				input = null;
				return false;
			}
			input = new QueryTransformInput
			{
				Parameters = list,
				Table = queryTransformTable
			};
			return true;
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0000A820 File Offset: 0x00008A20
		private bool TrySerializeTransformOutput(ResolvedQueryTransformOutput resolvedOutput, out QueryTransformOutput output)
		{
			QueryTransformTable queryTransformTable;
			if (!this.TrySerializeTransformTable(resolvedOutput.Table, out queryTransformTable))
			{
				output = null;
				return false;
			}
			output = new QueryTransformOutput
			{
				Table = queryTransformTable
			};
			return true;
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0000A851 File Offset: 0x00008A51
		private bool TrySerializeTransformParameter(ResolvedQueryTransformParameter resolvedParam, out QueryExpressionContainer param)
		{
			if (!ResolvedQueryDefinitionSerializer.TrySerializeQueryExpression(resolvedParam.Expression, out param))
			{
				return false;
			}
			param.Name = resolvedParam.Name;
			return true;
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0000A874 File Offset: 0x00008A74
		private bool TrySerializeTransformTable(ResolvedQueryTransformTable resolvedTable, out QueryTransformTable table)
		{
			List<QueryTransformTableColumn> list;
			if (!ResolvedQueryDefinitionSerializer.TrySerializeEach<ResolvedQueryTransformTableColumn, QueryTransformTableColumn>(resolvedTable.Columns, new ResolvedQueryDefinitionSerializer.TryResolveItem<ResolvedQueryTransformTableColumn, QueryTransformTableColumn>(this.TryResolveTransformTableColumn), out list))
			{
				table = null;
				return false;
			}
			table = new QueryTransformTable
			{
				Name = resolvedTable.Name,
				Columns = list
			};
			return true;
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0000A8BC File Offset: 0x00008ABC
		private bool TryResolveTransformTableColumn(ResolvedQueryTransformTableColumn resolvedColumn, out QueryTransformTableColumn column)
		{
			QueryExpressionContainer queryExpressionContainer;
			if (!ResolvedQueryDefinitionSerializer.TrySerializeQueryExpression(resolvedColumn.Expression, out queryExpressionContainer))
			{
				column = null;
				return false;
			}
			queryExpressionContainer.Name = resolvedColumn.Name;
			column = new QueryTransformTableColumn
			{
				Role = resolvedColumn.Role,
				Expression = queryExpressionContainer
			};
			return true;
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0000A904 File Offset: 0x00008B04
		private bool TrySerializeFilterDefinition(ResolvedFilterDefinition resolvedFilterDefinition, out FilterDefinition filterDefinition)
		{
			filterDefinition = null;
			List<EntitySource> list;
			List<QueryFilter> list2;
			if (!(ResolvedQueryDefinitionSerializer.TrySerializeEach<ResolvedQuerySource, EntitySource>(resolvedFilterDefinition.From, new ResolvedQueryDefinitionSerializer.TryResolveItem<ResolvedQuerySource, EntitySource>(this.TrySerializeResolvedQuerySource), out list) & ResolvedQueryDefinitionSerializer.TrySerializeEach<ResolvedQueryFilter, QueryFilter>(resolvedFilterDefinition.Where.EmptyIfNull<ResolvedQueryFilter>(), new ResolvedQueryDefinitionSerializer.TryResolveItem<ResolvedQueryFilter, QueryFilter>(this.TrySerializeQueryFilter), out list2)))
			{
				filterDefinition = null;
				return false;
			}
			filterDefinition = new FilterDefinition
			{
				Version = new int?(2),
				From = list,
				Where = list2
			};
			return true;
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0000A975 File Offset: 0x00008B75
		internal static bool TrySerializeQueryExpression(ResolvedQuerySelect resolvedQuerySelect, out QueryExpressionContainer queryExpressionContainer)
		{
			if (ResolvedQueryDefinitionSerializer.TrySerializeQueryExpression(resolvedQuerySelect.Expression, out queryExpressionContainer))
			{
				queryExpressionContainer.Name = resolvedQuerySelect.Name;
				queryExpressionContainer.NativeReferenceName = resolvedQuerySelect.NativeReferenceName;
				return true;
			}
			return false;
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0000A9A2 File Offset: 0x00008BA2
		internal static bool TrySerializeQueryExpression(ResolvedQueryExpression resolvedQueryExpression, out QueryExpressionContainer queryExpressionContainer)
		{
			queryExpressionContainer = resolvedQueryExpression.Accept<QueryExpression>(ResolvedQueryExpressionSerializer.Instance);
			return queryExpressionContainer != null;
		}

		// Token: 0x0200030A RID: 778
		// (Invoke) Token: 0x0600195F RID: 6495
		private delegate bool TryResolveItem<I, O>(I input, out O output);
	}
}
