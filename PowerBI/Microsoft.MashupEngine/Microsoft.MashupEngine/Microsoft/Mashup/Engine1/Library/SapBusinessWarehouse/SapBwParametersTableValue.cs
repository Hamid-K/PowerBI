using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Cube;
using Microsoft.Mashup.Engine1.Library.Mdx;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Library.SapBusinessWarehouse
{
	// Token: 0x020004D0 RID: 1232
	internal static class SapBwParametersTableValue
	{
		// Token: 0x06002855 RID: 10325 RVA: 0x000782EC File Offset: 0x000764EC
		public static TableValue New(ISapBwService service, SapBwMdxCube mdxCube, CubeValue cubeValue, IList<SapBwVariable> variables)
		{
			return new QueryTableValue(new SapBwParametersTableValue.SapBwParametersTableValueQuery(service, mdxCube, cubeValue, variables, null, null, null));
		}

		// Token: 0x04001135 RID: 4405
		private static readonly TypeValue DelayedNullableTableType = PreviewServices.ConvertToDelayedValue(TypeValue.Table.Nullable, "Value");

		// Token: 0x04001136 RID: 4406
		private static readonly RecordTypeValue allowedValuesMetaType = RecordTypeValue.New(RecordValue.New(new NamedValue[]
		{
			new NamedValue("Documentation.AllowedValues", RecordTypeAlgebra.NewField(SapBwParametersTableValue.DelayedNullableTableType, false))
		}), false);

		// Token: 0x04001137 RID: 4407
		private static readonly RecordTypeValue allowedValuesFuncMetaType = RecordTypeValue.New(RecordValue.New(new NamedValue[]
		{
			new NamedValue("Documentation.AllowedValues", RecordTypeAlgebra.NewField(TypeValue.Function, false))
		}), false);

		// Token: 0x04001138 RID: 4408
		private static readonly string[] singleValueParameterNames = new string[] { "value" };

		// Token: 0x04001139 RID: 4409
		private static readonly string[] intervalParameterNames = new string[] { "start", "end" };

		// Token: 0x0400113A RID: 4410
		private static readonly string[] complexParameterNames = new string[] { "values" };

		// Token: 0x0400113B RID: 4411
		private static readonly Keys columns = Keys.New(new string[]
		{
			"Id",
			"Name",
			CubeParametersTableValue.IsOptional.AsString,
			"Data",
			"Description"
		});

		// Token: 0x0400113C RID: 4412
		private static readonly TableTypeValue type = TableTypeValue.New(RecordTypeValue.New(RecordValue.New(SapBwParametersTableValue.columns, new Value[]
		{
			RecordTypeAlgebra.NewField(TypeValue.Text, false),
			RecordTypeAlgebra.NewField(TypeValue.Text, false),
			RecordTypeAlgebra.NewField(TypeValue.Logical, false),
			RecordTypeAlgebra.NewField(TypeValue.Function, false),
			RecordTypeAlgebra.NewField(TypeValue.Text, false)
		})), new TableKey[]
		{
			new TableKey(new int[1], true)
		});

		// Token: 0x020004D1 RID: 1233
		private class SapBwParametersTableValueQuery : DataSourceQuery
		{
			// Token: 0x06002857 RID: 10327 RVA: 0x00078496 File Offset: 0x00076696
			public SapBwParametersTableValueQuery(ISapBwService service, SapBwMdxCube mdxCube, CubeValue cubeValue, IList<SapBwVariable> variables, string idColumnFilter, string nameColumnFilter, bool? isOptionalFilter)
			{
				this.service = service;
				this.mdxCube = mdxCube;
				this.cubeValue = cubeValue;
				this.variables = variables;
				this.idColumnFilter = idColumnFilter;
				this.nameColumnFilter = nameColumnFilter;
				this.isOptionalFilter = isOptionalFilter;
			}

			// Token: 0x17000FA8 RID: 4008
			// (get) Token: 0x06002858 RID: 10328 RVA: 0x000784D3 File Offset: 0x000766D3
			public override Keys Columns
			{
				get
				{
					return SapBwParametersTableValue.columns;
				}
			}

			// Token: 0x17000FA9 RID: 4009
			// (get) Token: 0x06002859 RID: 10329 RVA: 0x000784DA File Offset: 0x000766DA
			public override IEngineHost EngineHost
			{
				get
				{
					return this.service.Host;
				}
			}

			// Token: 0x0600285A RID: 10330 RVA: 0x000784E7 File Offset: 0x000766E7
			public override IEnumerable<IValueReference> GetRows()
			{
				foreach (SapBwVariable sapBwVariable in this.variables)
				{
					if (this.Selected(sapBwVariable))
					{
						yield return RecordValue.New(SapBwParametersTableValue.columns, new Value[]
						{
							TextValue.New(sapBwVariable.MdxIdentifier),
							TextValue.NewOrNull(sapBwVariable.Caption),
							LogicalValue.New(sapBwVariable.EntryType == SapBwVariableEntryType.Optional),
							this.NewParameterValue(sapBwVariable),
							TextValue.NewOrNull(sapBwVariable.Description)
						});
					}
				}
				IEnumerator<SapBwVariable> enumerator = null;
				yield break;
				yield break;
			}

			// Token: 0x0600285B RID: 10331 RVA: 0x000784F8 File Offset: 0x000766F8
			private FunctionValue NewParameterValue(SapBwVariable variable)
			{
				switch (variable.SelectionType)
				{
				case SapBwVariableSelectionType.Value:
					return this.NewSingleValue(variable);
				case SapBwVariableSelectionType.Interval:
					return this.NewInterval(variable);
				case SapBwVariableSelectionType.SelectionOption:
				case SapBwVariableSelectionType.SeveralSingleValues:
					return this.NewMultipleSingleValue(variable);
				case SapBwVariableSelectionType.PrecalculatedValueSet:
					return this.NewPrecalculatedValueSet(variable);
				default:
					throw new InvalidOperationException(variable.ToString());
				}
			}

			// Token: 0x0600285C RID: 10332 RVA: 0x00078558 File Offset: 0x00076758
			private FunctionValue NewSingleValue(SapBwVariable variable)
			{
				IdentifierCubeExpression identifierCubeExpression = new IdentifierCubeExpression(variable.MdxIdentifier);
				TypeValue typeValue = this.NewFunctionParameterTypeValue(variable, variable.DefaultLowValueCaption, variable.DefaultLow, variable.EntryType != SapBwVariableEntryType.MandatoryNotInitial);
				TypeValue[] array = new TypeValue[] { typeValue };
				return new ParameterValue(this.cubeValue, identifierCubeExpression, 1, SapBwParametersTableValue.singleValueParameterNames, array);
			}

			// Token: 0x0600285D RID: 10333 RVA: 0x000785B0 File Offset: 0x000767B0
			private FunctionValue NewInterval(SapBwVariable variable)
			{
				IdentifierCubeExpression identifierCubeExpression = new IdentifierCubeExpression(variable.MdxIdentifier);
				TypeValue typeValue = this.NewFunctionParameterTypeValue(variable, variable.DefaultLowValueCaption, variable.DefaultLow, false);
				TypeValue typeValue2 = this.NewFunctionParameterTypeValue(variable, variable.DefaultLowValueCaption, variable.DefaultLow, false);
				TypeValue[] array = new TypeValue[] { typeValue, typeValue2 };
				return new ParameterValue(this.cubeValue, identifierCubeExpression, 2, SapBwParametersTableValue.intervalParameterNames, array);
			}

			// Token: 0x0600285E RID: 10334 RVA: 0x00078618 File Offset: 0x00076818
			private FunctionValue NewMultipleSingleValue(SapBwVariable variable)
			{
				IdentifierCubeExpression identifierCubeExpression = new IdentifierCubeExpression(variable.MdxIdentifier);
				TypeValue typeValue = this.NewFunctionParameterTypeValue(variable, variable.DefaultLowValueCaption, variable.DefaultLow, variable.EntryType != SapBwVariableEntryType.MandatoryNotInitial);
				TypeValue[] array = new TypeValue[] { ListTypeValue.New(typeValue) };
				return new ParameterValue(this.cubeValue, identifierCubeExpression, 1, SapBwParametersTableValue.complexParameterNames, array);
			}

			// Token: 0x0600285F RID: 10335 RVA: 0x00078674 File Offset: 0x00076874
			private FunctionValue NewPrecalculatedValueSet(SapBwVariable variable)
			{
				IdentifierCubeExpression identifierCubeExpression = new IdentifierCubeExpression(variable.MdxIdentifier);
				TypeValue[] array = new TypeValue[] { TypeValue.Text };
				return new ParameterValue(this.cubeValue, identifierCubeExpression, 1, SapBwParametersTableValue.complexParameterNames, array);
			}

			// Token: 0x06002860 RID: 10336 RVA: 0x000786B0 File Offset: 0x000768B0
			private TypeValue NewFunctionParameterTypeValue(SapBwVariable variable, string defaultValueCaption, object defaultValue, bool allowNonAssignedMember)
			{
				TypeValue typeValue = TypeValue.Any;
				if (variable.SelectionType != SapBwVariableSelectionType.SelectionOption && variable.DataType != null)
				{
					typeValue = variable.DataType.TypeValue;
				}
				RecordValue recordValue = RecordValue.Empty;
				if (variable.IsDate)
				{
					typeValue = TypeValue.Date;
					defaultValue = SapBwIdentifier.ExtractDateOrNull(defaultValue);
					recordValue = recordValue.Concatenate(NavigationTableServices.NewAllowedValuesIsOpenSetMetadata(true)).AsRecord;
				}
				else
				{
					switch (variable.Type)
					{
					case SapBwVariableType.CharacteristicValue:
					case SapBwVariableType.Hierarchy:
						recordValue = this.ConcatenateMetadataForVariables(variable, SapBwParametersTableValue.allowedValuesMetaType, allowNonAssignedMember, recordValue);
						break;
					case SapBwVariableType.HierarchyNode:
					{
						bool flag = false;
						if (string.IsNullOrEmpty(variable.Hierarchy))
						{
							Dictionary<string, MdxHierarchy> externalHierarchies = SapBwVariableHierarchyNodeProvider.GetExternalHierarchies(this.service, variable);
							string text;
							Dictionary<string, MdxHierarchy> hierarchies = SapBwVariableHierarchyNodeProvider.GetHierarchies(variable, this.mdxCube, variable.Hierarchy, externalHierarchies, out text);
							if (hierarchies != null && hierarchies.Count > 0)
							{
								flag = SapBwHierarchyNodeVariableInfo.New(hierarchies.Values, text).HasMultipleHierarchies;
							}
						}
						recordValue = this.ConcatenateMetadataForVariables(variable, flag ? SapBwParametersTableValue.allowedValuesFuncMetaType : SapBwParametersTableValue.allowedValuesMetaType, allowNonAssignedMember, recordValue);
						break;
					}
					}
				}
				recordValue = recordValue.Concatenate(NavigationTableServices.NewDefaultValueMetadata(defaultValueCaption, ValueMarshaller.MarshalFromClr(defaultValue))).AsRecord;
				return typeValue.NewMeta(recordValue).AsType;
			}

			// Token: 0x06002861 RID: 10337 RVA: 0x000787E4 File Offset: 0x000769E4
			private RecordValue ConcatenateMetadataForVariables(SapBwVariable variable, RecordTypeValue allowedValuesMetadataType, bool allowNonAssignedMember, RecordValue metadata)
			{
				metadata = metadata.Concatenate(RecordValue.New(allowedValuesMetadataType, (int index) => this.GetAllowedValues(variable, allowNonAssignedMember))).AsRecord;
				return metadata.Concatenate(NavigationTableServices.NewAllowedValuesIsOpenSetMetadata(true)).AsRecord;
			}

			// Token: 0x06002862 RID: 10338 RVA: 0x00078840 File Offset: 0x00076A40
			private Value GetAllowedValues(SapBwVariable variable, bool allowNonAssigned)
			{
				SapBwVariableValueProvider sapBwVariableValueProvider = null;
				switch (variable.Type)
				{
				case SapBwVariableType.CharacteristicValue:
					sapBwVariableValueProvider = new SapBwVariableMemberProvider(this.service, this.mdxCube, variable, allowNonAssigned, true, null);
					if (!sapBwVariableValueProvider.HasValues)
					{
						sapBwVariableValueProvider = new SapBwVariableMasterTableProvider(this.service, this.mdxCube, variable, allowNonAssigned);
					}
					break;
				case SapBwVariableType.HierarchyNode:
					if (!string.IsNullOrEmpty(variable.Hierarchy))
					{
						sapBwVariableValueProvider = new SapBwVariableMemberProvider(this.service, this.mdxCube, variable, allowNonAssigned, true, null);
					}
					else
					{
						SapBwVariableHierarchyNodeProvider sapBwVariableHierarchyNodeProvider = this.CreateHierarchyNodeProvider(true, variable, allowNonAssigned);
						if (sapBwVariableHierarchyNodeProvider.HasMultipleHierarchies)
						{
							return new SapBwParametersTableValue.SapBwParametersTableValueQuery.HierarchyNodeRetrievalFunction(variable, this.mdxCube, this.service, allowNonAssigned);
						}
						sapBwVariableValueProvider = new SapBwVariableMemberProvider(this.service, this.mdxCube, variable, allowNonAssigned, true, sapBwVariableHierarchyNodeProvider.SingleHierarchyUniqueName);
					}
					if (!sapBwVariableValueProvider.HasValues)
					{
						sapBwVariableValueProvider = this.CreateHierarchyNodeProvider(false, variable, allowNonAssigned);
					}
					break;
				case SapBwVariableType.Hierarchy:
					return ListValue.New(this.GetDimensionHierarchies(variable));
				}
				if (sapBwVariableValueProvider != null)
				{
					return new QueryTableValue(new SapBwParametersTableValue.SapBwParametersTableValueQuery.SapBwParameterValuesQuery(sapBwVariableValueProvider, RowRange.All));
				}
				return null;
			}

			// Token: 0x06002863 RID: 10339 RVA: 0x0007894C File Offset: 0x00076B4C
			private SapBwVariableHierarchyNodeProvider CreateHierarchyNodeProvider(bool primary, SapBwVariable variable, bool allowNonAssigned)
			{
				if ((this.service.PreferTablesForMultipleHierarchyNodes && primary) || (!this.service.PreferTablesForMultipleHierarchyNodes && !primary))
				{
					return new SapBwVariableHierarchyNodeTableProvider(this.service, this.mdxCube, variable, allowNonAssigned);
				}
				return new SapBwVariableHierarchyNodeMemberProvider(this.service, this.mdxCube, variable, allowNonAssigned);
			}

			// Token: 0x06002864 RID: 10340 RVA: 0x0007899F File Offset: 0x00076B9F
			private IEnumerable<IValueReference> GetDimensionHierarchies(SapBwVariable variable)
			{
				MdxDimension mdxDimension;
				if (variable.Dimension != null && this.mdxCube.Dimensions.TryGetValue(variable.Dimension, out mdxDimension))
				{
					foreach (MdxHierarchy mdxHierarchy in mdxDimension.Hierarchies.Values)
					{
						if (SapBwIdentifier.Parse(mdxHierarchy.MdxIdentifier).HierarchyName != null)
						{
							Value value = TextValue.New(mdxHierarchy.MdxIdentifier);
							value = NavigationTableServices.AddCaption(value, mdxHierarchy.Caption);
							yield return value;
						}
					}
					Dictionary<string, MdxHierarchy>.ValueCollection.Enumerator enumerator = default(Dictionary<string, MdxHierarchy>.ValueCollection.Enumerator);
				}
				yield break;
				yield break;
			}

			// Token: 0x06002865 RID: 10341 RVA: 0x000789B8 File Offset: 0x00076BB8
			private bool Selected(SapBwVariable variable)
			{
				if (this.idColumnFilter != null && !variable.MdxIdentifier.Equals(this.idColumnFilter))
				{
					return false;
				}
				if (this.nameColumnFilter != null && !variable.Caption.Equals(this.nameColumnFilter))
				{
					return false;
				}
				if (this.isOptionalFilter != null)
				{
					if (this.isOptionalFilter.Value && variable.EntryType != SapBwVariableEntryType.Optional)
					{
						return false;
					}
					if (!this.isOptionalFilter.Value && variable.EntryType == SapBwVariableEntryType.Optional)
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x06002866 RID: 10342 RVA: 0x00078A3C File Offset: 0x00076C3C
			public override Query SelectRows(FunctionValue condition)
			{
				QueryExpression queryExpression = QueryExpressionBuilder.ToQueryExpression(QueryTableValue.NewRowType(this), condition);
				string asString = this.idColumnFilter;
				string asString2 = this.nameColumnFilter;
				bool? flag = this.isOptionalFilter;
				bool flag2 = false;
				List<QueryExpression> list = new List<QueryExpression>();
				foreach (QueryExpression queryExpression2 in SelectRowsQuery.GetConjunctiveNF(queryExpression))
				{
					bool flag3 = false;
					BinaryQueryExpression binaryQueryExpression = queryExpression2 as BinaryQueryExpression;
					int num;
					Value value;
					if (binaryQueryExpression != null && binaryQueryExpression.Operator == BinaryOperator2.Equals && ((binaryQueryExpression.Left.TryGetColumnAccess(out num) && binaryQueryExpression.Right.TryGetConstant(out value)) || (binaryQueryExpression.Right.TryGetColumnAccess(out num) && binaryQueryExpression.Left.TryGetConstant(out value))))
					{
						string text = this.Columns[num];
						if (!(text == "Id"))
						{
							if (!(text == "Name"))
							{
								if (text == "IsOptional")
								{
									flag3 = true;
									flag = new bool?(value.AsBoolean);
								}
							}
							else
							{
								flag3 = true;
								asString2 = value.AsString;
							}
						}
						else
						{
							flag3 = true;
							asString = value.AsString;
						}
						flag2 = !flag2 && flag3;
					}
					if (!flag3)
					{
						list.Add(queryExpression2);
					}
				}
				if (!flag2)
				{
					return base.SelectRows(condition);
				}
				Query query = new SapBwParametersTableValue.SapBwParametersTableValueQuery(this.service, this.mdxCube, this.cubeValue, this.variables, asString, asString2, flag);
				if (list.Count > 0)
				{
					query = SelectRowsQuery.New(QueryExpressionAssembler.Assemble(this.Columns, SelectRowsQuery.CreateConjunctiveNF(list)), query);
				}
				return query;
			}

			// Token: 0x0400113D RID: 4413
			private readonly ISapBwService service;

			// Token: 0x0400113E RID: 4414
			private readonly SapBwMdxCube mdxCube;

			// Token: 0x0400113F RID: 4415
			private readonly CubeValue cubeValue;

			// Token: 0x04001140 RID: 4416
			private readonly IList<SapBwVariable> variables;

			// Token: 0x04001141 RID: 4417
			private readonly string idColumnFilter;

			// Token: 0x04001142 RID: 4418
			private readonly string nameColumnFilter;

			// Token: 0x04001143 RID: 4419
			private readonly bool? isOptionalFilter;

			// Token: 0x020004D2 RID: 1234
			private class SapBwParameterValuesQuery : DataSourceQuery
			{
				// Token: 0x06002867 RID: 10343 RVA: 0x00078BE8 File Offset: 0x00076DE8
				public SapBwParameterValuesQuery(SapBwVariableValueProvider provider, RowRange range)
				{
					this.provider = provider;
					this.range = range;
				}

				// Token: 0x17000FAA RID: 4010
				// (get) Token: 0x06002868 RID: 10344 RVA: 0x00078BFE File Offset: 0x00076DFE
				public override IList<TableKey> TableKeys
				{
					get
					{
						return SapBwParametersTableValue.SapBwParametersTableValueQuery.SapBwParameterValuesQuery.tableKeys;
					}
				}

				// Token: 0x17000FAB RID: 4011
				// (get) Token: 0x06002869 RID: 10345 RVA: 0x00078C05 File Offset: 0x00076E05
				public override Keys Columns
				{
					get
					{
						return SapBwParametersTableValue.SapBwParametersTableValueQuery.SapBwParameterValuesQuery.columns;
					}
				}

				// Token: 0x17000FAC RID: 4012
				// (get) Token: 0x0600286A RID: 10346 RVA: 0x00078C0C File Offset: 0x00076E0C
				public override IEngineHost EngineHost
				{
					get
					{
						return this.provider.Service.Host;
					}
				}

				// Token: 0x0600286B RID: 10347 RVA: 0x00078C1E File Offset: 0x00076E1E
				public override IEnumerable<IValueReference> GetRows()
				{
					if (!this.range.TakeCount.IsZero)
					{
						long num = (this.range.TakeCount.IsInfinite ? long.MaxValue : this.range.TakeCount.Value);
						foreach (IValueReference valueReference in this.provider.GetValues(this.range.SkipCount.Value).TakeLong(num))
						{
							yield return RecordValue.New(SapBwParametersTableValue.SapBwParametersTableValueQuery.SapBwParameterValuesQuery.recordType, new IValueReference[] { valueReference });
						}
						IEnumerator<IValueReference> enumerator = null;
					}
					yield break;
					yield break;
				}

				// Token: 0x0600286C RID: 10348 RVA: 0x00078C30 File Offset: 0x00076E30
				public override Query Take(RowCount count)
				{
					return new SapBwParametersTableValue.SapBwParametersTableValueQuery.SapBwParameterValuesQuery(this.provider, this.range.Take(count));
				}

				// Token: 0x0600286D RID: 10349 RVA: 0x00078C58 File Offset: 0x00076E58
				public override Query Skip(RowCount count)
				{
					return new SapBwParametersTableValue.SapBwParametersTableValueQuery.SapBwParameterValuesQuery(this.provider, this.range.Skip(count));
				}

				// Token: 0x04001144 RID: 4420
				private static readonly Keys columns = Keys.New("Data");

				// Token: 0x04001145 RID: 4421
				private static readonly RecordTypeValue recordType = RecordTypeValue.New(RecordValue.New(SapBwParametersTableValue.SapBwParametersTableValueQuery.SapBwParameterValuesQuery.columns, new Value[] { RecordTypeAlgebra.NewField(TypeValue.Text, false) }));

				// Token: 0x04001146 RID: 4422
				private static readonly IList<TableKey> tableKeys = new List<TableKey>
				{
					new TableKey(new int[1], true)
				};

				// Token: 0x04001147 RID: 4423
				private readonly RowRange range;

				// Token: 0x04001148 RID: 4424
				private readonly SapBwVariableValueProvider provider;
			}

			// Token: 0x020004D4 RID: 1236
			private class HierarchyNodeRetrievalFunction : NativeFunctionValue1<ListValue, TableValue>
			{
				// Token: 0x06002878 RID: 10360 RVA: 0x00078EEB File Offset: 0x000770EB
				public HierarchyNodeRetrievalFunction(SapBwVariable variable, SapBwMdxCube cube, ISapBwService service, bool allowNonAssigned)
					: base(TypeValue.List, 1, "values", TypeValue.Table)
				{
					this.variable = variable;
					this.cube = cube;
					this.service = service;
					this.allowNonAssigned = allowNonAssigned;
				}

				// Token: 0x06002879 RID: 10361 RVA: 0x00078F20 File Offset: 0x00077120
				private string GetAssociatedHierarchyVariableName(string dimension)
				{
					return (from v in this.cube.Variables
						where v.Type == SapBwVariableType.Hierarchy && v.Dimension.Equals(dimension)
						select v.MdxIdentifier).FirstOrDefault<string>();
				}

				// Token: 0x0600287A RID: 10362 RVA: 0x00078F80 File Offset: 0x00077180
				public override ListValue TypedInvoke(TableValue argumentTable)
				{
					string associatedHierarchyVariableName = this.GetAssociatedHierarchyVariableName(this.variable.Dimension);
					Value value = (from r in argumentTable
						where r.Value.AsRecord["functionId"].ToString() == associatedHierarchyVariableName
						select r.Value.AsRecord["value"]).FirstOrDefault<Value>();
					if (value == null)
					{
						return ListValue.New(new List<IValueReference>());
					}
					return ListValue.New(new SapBwVariableSelectedHierarchyMemberProvider(this.service, this.cube, this.variable, value.ToString(), this.allowNonAssigned).GetValues());
				}

				// Token: 0x0400114E RID: 4430
				private readonly SapBwVariable variable;

				// Token: 0x0400114F RID: 4431
				private readonly SapBwMdxCube cube;

				// Token: 0x04001150 RID: 4432
				private readonly ISapBwService service;

				// Token: 0x04001151 RID: 4433
				private readonly bool allowNonAssigned;
			}
		}
	}
}
