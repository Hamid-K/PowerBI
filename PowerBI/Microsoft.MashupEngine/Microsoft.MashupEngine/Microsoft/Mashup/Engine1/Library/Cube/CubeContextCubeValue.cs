using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Serialization;
using Microsoft.Internal;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Capability;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Table;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000CCB RID: 3275
	internal sealed class CubeContextCubeValue : CubeValue
	{
		// Token: 0x0600589A RID: 22682 RVA: 0x00135719 File Offset: 0x00133919
		public static CubeValue New(CubeContextProvider contextProvider, CubeContext context, Keys columns)
		{
			return CubeContextCubeValue.New(contextProvider, context, new View(columns));
		}

		// Token: 0x0600589B RID: 22683 RVA: 0x00135728 File Offset: 0x00133928
		private static CubeValue New(CubeContextProvider contextProvider, CubeContext context, View view)
		{
			return CubeContextCubeValue.AddCubeMetadata(new CubeContextCubeValue(contextProvider, context, view));
		}

		// Token: 0x0600589C RID: 22684 RVA: 0x00135737 File Offset: 0x00133937
		private static CubeValue AddCubeMetadata(CubeValue cube)
		{
			return cube.NewMeta(RecordValue.New(CubeContextCubeValue.CubeMetadataKeys, delegate(int index)
			{
				if (index == 0)
				{
					return LogicalValue.True;
				}
				throw new InvalidOperationException(Strings.UnreachableCodePath);
			})).AsTable.AsCube;
		}

		// Token: 0x0600589D RID: 22685 RVA: 0x00135772 File Offset: 0x00133972
		private CubeContextCubeValue(CubeContextProvider contextProvider, CubeContext context, View view)
		{
			this.contextProvider = contextProvider;
			this.context = context;
			this.view = view;
		}

		// Token: 0x0600589E RID: 22686 RVA: 0x0013578F File Offset: 0x0013398F
		public static RecordValue NewGroupByKeyMetadata(string uniqueIdColumn)
		{
			return RecordValue.New(Keys.New("Cube.GroupByKey"), new Value[] { ListValue.New(new Value[] { TextValue.New(uniqueIdColumn) }) });
		}

		// Token: 0x17001A96 RID: 6806
		// (get) Token: 0x0600589F RID: 22687 RVA: 0x001357C0 File Offset: 0x001339C0
		public override Keys DimensionAttributes
		{
			get
			{
				if (this.dimensionAttributes == null)
				{
					KeysBuilder keysBuilder = default(KeysBuilder);
					for (int i = 0; i < this.view.Keys.Length; i++)
					{
						if (this.view.GetColumn(i) < this.context.CubeExpression.DimensionAttributes.Count)
						{
							keysBuilder.Add(this.view.Keys[i]);
						}
					}
					this.dimensionAttributes = keysBuilder.ToKeys();
				}
				return this.dimensionAttributes;
			}
		}

		// Token: 0x17001A97 RID: 6807
		// (get) Token: 0x060058A0 RID: 22688 RVA: 0x0013584F File Offset: 0x00133A4F
		public override TableValue DisplayFolders
		{
			get
			{
				return this.context.DisplayFolders;
			}
		}

		// Token: 0x17001A98 RID: 6808
		// (get) Token: 0x060058A1 RID: 22689 RVA: 0x0013585C File Offset: 0x00133A5C
		public override TableValue MeasureGroups
		{
			get
			{
				return this.context.MeasureGroups;
			}
		}

		// Token: 0x17001A99 RID: 6809
		// (get) Token: 0x060058A2 RID: 22690 RVA: 0x00135869 File Offset: 0x00133A69
		public override TableValue Dimensions
		{
			get
			{
				return this.context.Dimensions;
			}
		}

		// Token: 0x17001A9A RID: 6810
		// (get) Token: 0x060058A3 RID: 22691 RVA: 0x00135876 File Offset: 0x00133A76
		public override TableValue Measures
		{
			get
			{
				return this.context.Measures;
			}
		}

		// Token: 0x17001A9B RID: 6811
		// (get) Token: 0x060058A4 RID: 22692 RVA: 0x00135883 File Offset: 0x00133A83
		public override TableValue Parameters
		{
			get
			{
				return this.context.GetParameters(this);
			}
		}

		// Token: 0x17001A9C RID: 6812
		// (get) Token: 0x060058A5 RID: 22693 RVA: 0x00135891 File Offset: 0x00133A91
		public override TableValue Properties
		{
			get
			{
				return this.context.GetAvailableProperties();
			}
		}

		// Token: 0x17001A9D RID: 6813
		// (get) Token: 0x060058A6 RID: 22694 RVA: 0x0013589E File Offset: 0x00133A9E
		public override TableValue MeasureProperties
		{
			get
			{
				return this.context.GetAvailableMeasureProperties();
			}
		}

		// Token: 0x17001A9E RID: 6814
		// (get) Token: 0x060058A7 RID: 22695 RVA: 0x001358AC File Offset: 0x00133AAC
		public override Query Query
		{
			get
			{
				if (this.query == null)
				{
					Query query;
					if (!this.context.TryGetQuery(out query))
					{
						QueryCubeExpression cubeExpression = this.context.CubeExpression;
						IList<IdentifierCubeExpression> list = cubeExpression.DimensionAttributes;
						IList<IdentifierCubeExpression> properties = cubeExpression.Properties;
						IList<IdentifierCubeExpression> measures = cubeExpression.Measures;
						IList<IdentifierCubeExpression> measureProperties = cubeExpression.MeasureProperties;
						KeysBuilder keysBuilder = new KeysBuilder(list.Count + properties.Count + measures.Count + measureProperties.Count);
						ArrayBuilder<TypeValue> arrayBuilder = new ArrayBuilder<TypeValue>(list.Count + properties.Count + measures.Count + measureProperties.Count);
						foreach (IdentifierCubeExpression identifierCubeExpression in list.Concat(properties).Concat(measures).Concat(measureProperties))
						{
							keysBuilder.Add(identifierCubeExpression.Identifier);
							arrayBuilder.Add(this.contextProvider.GetType(identifierCubeExpression));
						}
						query = new CubeContextCubeValue.CubeContextQuery(keysBuilder.ToKeys(), arrayBuilder.ToArray(), this.context);
					}
					int length = query.Columns.Length;
					KeysBuilder keysBuilder2 = new KeysBuilder(length);
					string text = Guid.NewGuid().ToString();
					for (int i = 0; i < length; i++)
					{
						keysBuilder2.Add(text + "_" + query.Columns[i]);
					}
					Keys keys = keysBuilder2.ToKeys();
					query = query.RenameReorderColumns(new ColumnSelection(keys));
					int length2 = this.Columns.Length;
					FunctionValue[] array = new FunctionValue[length2];
					TypeValue[] array2 = new TypeValue[length2];
					int[] array3 = new int[length2];
					for (int j = 0; j < length2; j++)
					{
						int column = this.view.GetColumn(j);
						array[j] = QueryExpressionAssembler.Assemble(keys, new ColumnAccessQueryExpression(column));
						array2[j] = this.GetColumnType(j);
						array3[j] = j + length;
					}
					Query query2 = query;
					Keys columns = this.Columns;
					FunctionValue functionValue = new TableValue.FunctionsColumnsConstructorFunctionValue(array);
					IValueReference[] array4 = array2;
					query = query2.AddColumns(new ColumnsConstructor(columns, functionValue, array4));
					query = query.SelectColumns(new ColumnSelection(this.Columns, array3));
					this.query = new CubeContextCubeValue.TabularCubeQuery(this, query);
				}
				return this.query;
			}
		}

		// Token: 0x17001A9F RID: 6815
		// (get) Token: 0x060058A8 RID: 22696 RVA: 0x00135B04 File Offset: 0x00133D04
		public override IQueryDomain QueryDomain
		{
			get
			{
				return this.Query.QueryDomain;
			}
		}

		// Token: 0x17001AA0 RID: 6816
		// (get) Token: 0x060058A9 RID: 22697 RVA: 0x00135B11 File Offset: 0x00133D11
		public IEngineHost EngineHost
		{
			get
			{
				return this.contextProvider.EngineHost;
			}
		}

		// Token: 0x17001AA1 RID: 6817
		// (get) Token: 0x060058AA RID: 22698 RVA: 0x00135B1E File Offset: 0x00133D1E
		private TableValue Table
		{
			get
			{
				return new QueryTableValue(this.Query, this.Type.AsTableType);
			}
		}

		// Token: 0x060058AB RID: 22699 RVA: 0x00135B38 File Offset: 0x00133D38
		private int GetDimensionAttribute(int column)
		{
			int column2 = this.view.GetColumn(column);
			if (column2 > -1 && column2 < this.context.CubeExpression.DimensionAttributes.Count)
			{
				return column2;
			}
			return -1;
		}

		// Token: 0x060058AC RID: 22700 RVA: 0x00135B74 File Offset: 0x00133D74
		private int GetProperty(int column)
		{
			int num = this.view.GetColumn(column) - this.context.CubeExpression.DimensionAttributes.Count;
			if (num > -1 && num < this.context.CubeExpression.Properties.Count)
			{
				return num;
			}
			return -1;
		}

		// Token: 0x060058AD RID: 22701 RVA: 0x00135BC8 File Offset: 0x00133DC8
		private int GetMeasure(int column)
		{
			int num = this.view.GetColumn(column) - this.context.CubeExpression.DimensionAttributes.Count - this.context.CubeExpression.Properties.Count;
			if (num > -1 && num < this.context.CubeExpression.Measures.Count)
			{
				return num;
			}
			return -1;
		}

		// Token: 0x060058AE RID: 22702 RVA: 0x00135C30 File Offset: 0x00133E30
		private int GetMeasureProperty(int column)
		{
			int num = this.view.GetColumn(column) - this.context.CubeExpression.DimensionAttributes.Count - this.context.CubeExpression.Properties.Count - this.context.CubeExpression.Measures.Count;
			if (num > -1 && num < this.context.CubeExpression.MeasureProperties.Count)
			{
				return num;
			}
			return -1;
		}

		// Token: 0x060058AF RID: 22703 RVA: 0x00135CB0 File Offset: 0x00133EB0
		private int IndexOfIdentifier(IdentifierCubeExpression identifier)
		{
			int num = 0;
			using (IEnumerator<IdentifierCubeExpression> enumerator = this.context.CubeExpression.DimensionAttributes.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Identifier == identifier.Identifier)
					{
						return num;
					}
					num++;
				}
			}
			using (IEnumerator<IdentifierCubeExpression> enumerator = this.context.CubeExpression.Properties.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Identifier == identifier.Identifier)
					{
						return num;
					}
					num++;
				}
			}
			using (IEnumerator<IdentifierCubeExpression> enumerator = this.context.CubeExpression.Measures.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Identifier == identifier.Identifier)
					{
						return num;
					}
					num++;
				}
			}
			using (IEnumerator<IdentifierCubeExpression> enumerator = this.context.CubeExpression.MeasureProperties.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Identifier == identifier.Identifier)
					{
						return num;
					}
					num++;
				}
			}
			return -1;
		}

		// Token: 0x060058B0 RID: 22704 RVA: 0x00135E2C File Offset: 0x0013402C
		private bool IsDimensionAttribute(IdentifierCubeExpression identifier)
		{
			return this.contextProvider.GetObjectKind(identifier) == CubeObjectKind.DimensionAttribute;
		}

		// Token: 0x060058B1 RID: 22705 RVA: 0x00135E3D File Offset: 0x0013403D
		private bool IsProperty(IdentifierCubeExpression identifier)
		{
			return this.contextProvider.GetObjectKind(identifier) == CubeObjectKind.Property;
		}

		// Token: 0x060058B2 RID: 22706 RVA: 0x00135E4E File Offset: 0x0013404E
		private bool IsMeasure(IdentifierCubeExpression identifier)
		{
			return this.contextProvider.GetObjectKind(identifier) == CubeObjectKind.Measure;
		}

		// Token: 0x060058B3 RID: 22707 RVA: 0x00135E5F File Offset: 0x0013405F
		private bool IsMeasureProperty(IdentifierCubeExpression identifier)
		{
			return this.contextProvider.GetObjectKind(identifier) == CubeObjectKind.MeasureProperty;
		}

		// Token: 0x17001AA2 RID: 6818
		// (get) Token: 0x060058B4 RID: 22708 RVA: 0x00135E70 File Offset: 0x00134070
		public override TypeValue Type
		{
			get
			{
				if (this.type == null)
				{
					IdentifierCubeExpression[] array = new IdentifierCubeExpression[this.view.Keys.Length];
					Dictionary<string, int> dictionary = new Dictionary<string, int>();
					Dictionary<string, int> dictionary2 = new Dictionary<string, int>();
					for (int i = 0; i < array.Length; i++)
					{
						if (this.GetDimensionAttribute(i) != -1)
						{
							IdentifierCubeExpression identifierCubeExpression = this.context.CubeExpression.DimensionAttributes[this.GetDimensionAttribute(i)];
							array[i] = identifierCubeExpression;
							if (this.contextProvider.IsDimensionAttributeUniqueId(identifierCubeExpression))
							{
								dictionary[identifierCubeExpression.Identifier] = i;
							}
						}
						else if (this.GetProperty(i) != -1)
						{
							IdentifierCubeExpression identifierCubeExpression2 = this.context.CubeExpression.Properties[this.GetProperty(i)];
							array[i] = identifierCubeExpression2;
							CubePropertyKind propertyKind = this.contextProvider.GetPropertyKind(identifierCubeExpression2);
							if (propertyKind != CubePropertyKind.UniqueId)
							{
								if (propertyKind == CubePropertyKind.UserDefined)
								{
									if (this.contextProvider.PropertyIsKey(identifierCubeExpression2))
									{
										dictionary2[identifierCubeExpression2.Identifier] = i;
									}
								}
							}
							else
							{
								IdentifierCubeExpression propertyDimensionAttribute = this.contextProvider.GetPropertyDimensionAttribute(identifierCubeExpression2);
								dictionary[propertyDimensionAttribute.Identifier] = i;
							}
						}
						else if (this.GetMeasure(i) != -1)
						{
							array[i] = this.context.CubeExpression.Measures[this.GetMeasure(i)];
						}
						else
						{
							array[i] = this.context.CubeExpression.MeasureProperties[this.GetMeasureProperty(i)];
						}
					}
					TypeValue[] array2 = new TypeValue[array.Length];
					for (int j = 0; j < array2.Length; j++)
					{
						array2[j] = this.contextProvider.GetType(array[j]);
					}
					IValueReference[] array3 = new IValueReference[array2.Length];
					Value[] array4 = new Value[array2.Length];
					for (int k = 0; k < array2.Length; k++)
					{
						int dimensionAttribute = this.GetDimensionAttribute(k);
						int num2;
						if (dimensionAttribute != -1)
						{
							array2[k] = BinaryOperator.AddMeta.Invoke(array2[k], CubeContextCubeValue.CubeDimensionAttributeMetadata).AsType;
							IdentifierCubeExpression identifierCubeExpression3 = this.context.CubeExpression.DimensionAttributes[dimensionAttribute];
							int num;
							if (dictionary.TryGetValue(identifierCubeExpression3.Identifier, out num))
							{
								array2[k] = BinaryOperator.AddMeta.Invoke(array2[k], CubeContextCubeValue.NewGroupByKeyMetadata(this.Columns[num])).AsType;
							}
						}
						else if (this.GetProperty(k) != -1 && this.TryGetPropertyKeyIndex(this.GetProperty(k), dictionary, dictionary2, out num2))
						{
							array2[k] = BinaryOperator.AddMeta.Invoke(array2[k], CubeContextCubeValue.NewGroupByKeyMetadata(this.Columns[num2])).AsType;
						}
						array3[k] = RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
						{
							array2[k],
							LogicalValue.False
						});
						array4[k] = TextValue.New(this.contextProvider.GetDisplayName(array[k]));
					}
					RecordValue recordValue = RecordValue.New(Keys.New("Documentation.FieldCaption"), new Value[] { RecordValue.New(this.view.Keys, array4) });
					this.type = TableTypeValue.New(RecordTypeValue.New(RecordValue.New(this.Columns, array3), false)).NewMeta(recordValue).AsType;
				}
				return this.type;
			}
		}

		// Token: 0x060058B5 RID: 22709 RVA: 0x001361BC File Offset: 0x001343BC
		private bool TryGetPropertyKeyIndex(int propertyIndex, Dictionary<string, int> uniqueIdColumns, Dictionary<string, int> propertyKeyIndices, out int keyIndex)
		{
			keyIndex = -1;
			IdentifierCubeExpression identifierCubeExpression = this.context.CubeExpression.Properties[propertyIndex];
			IdentifierCubeExpression identifierCubeExpression2;
			if (this.contextProvider.TryGetPropertyKey(identifierCubeExpression, out identifierCubeExpression2))
			{
				CubePropertyKind propertyKind = this.contextProvider.GetPropertyKind(identifierCubeExpression2);
				if (propertyKind == CubePropertyKind.UniqueId)
				{
					IdentifierCubeExpression propertyDimensionAttribute = this.contextProvider.GetPropertyDimensionAttribute(identifierCubeExpression);
					return propertyDimensionAttribute != null && uniqueIdColumns.TryGetValue(propertyDimensionAttribute.Identifier, out keyIndex);
				}
				if (propertyKind == CubePropertyKind.UserDefined)
				{
					return propertyKeyIndices.TryGetValue(identifierCubeExpression2.Identifier, out keyIndex);
				}
			}
			return false;
		}

		// Token: 0x17001AA3 RID: 6819
		// (get) Token: 0x060058B6 RID: 22710 RVA: 0x0013623C File Offset: 0x0013443C
		public override Keys Columns
		{
			get
			{
				return this.view.Keys;
			}
		}

		// Token: 0x17001AA4 RID: 6820
		// (get) Token: 0x060058B7 RID: 22711 RVA: 0x00136258 File Offset: 0x00134458
		public override TableSortOrder SortOrder
		{
			get
			{
				if (this.sortOrder == null && this.context.CubeExpression.Sort.Count > 0)
				{
					ArrayBuilder<SortOrder> arrayBuilder = default(ArrayBuilder<SortOrder>);
					for (int i = 0; i < this.context.CubeExpression.Sort.Count; i++)
					{
						CubeSortOrder cubeSortOrder = this.context.CubeExpression.Sort[i];
						View.Map map = this.view.CreateMap(this.context.CubeExpression.DimensionAttributes.Count + this.context.CubeExpression.Properties.Count + this.context.CubeExpression.Measures.Count + this.context.CubeExpression.MeasureProperties.Count);
						int num = -1;
						IdentifierCubeExpression identifierCubeExpression = cubeSortOrder.Expression as IdentifierCubeExpression;
						if (identifierCubeExpression != null)
						{
							num = this.IndexOfIdentifier(identifierCubeExpression);
						}
						int num2 = map.MapColumnToSomeKey(num);
						if (num2 == -1)
						{
							arrayBuilder = default(ArrayBuilder<SortOrder>);
							break;
						}
						FunctionValue functionValue = new TableValue.ColumnSelectorFunctionValue(this.Columns[num2], num2);
						arrayBuilder.Add(new SortOrder(functionValue, null, cubeSortOrder.Ascending));
					}
					this.sortOrder = new TableSortOrder(arrayBuilder.ToArray());
				}
				return this.sortOrder;
			}
		}

		// Token: 0x17001AA5 RID: 6821
		// (get) Token: 0x060058B8 RID: 22712 RVA: 0x001363B2 File Offset: 0x001345B2
		public override IExpression Expression
		{
			get
			{
				return this.Table.Expression;
			}
		}

		// Token: 0x060058B9 RID: 22713 RVA: 0x001363C0 File Offset: 0x001345C0
		public override IEnumerator<IValueReference> GetEnumerator()
		{
			QueryCubeExpression cubeExpression = this.context.CubeExpression;
			if (cubeExpression.DimensionAttributes.Count > 0 || cubeExpression.Properties.Count > 0 || cubeExpression.Measures.Count > 0 || cubeExpression.MeasureProperties.Count > 0)
			{
				this.EnsureParametersProvided();
			}
			this.context.ReportFoldingFailure();
			return ProjectColumnsQuery.Project(this.context.Evaluate(), this.view.InnerSelection);
		}

		// Token: 0x060058BA RID: 22714 RVA: 0x00136440 File Offset: 0x00134640
		public override CubeValue ApplyParameter(FunctionValue parameter, Value[] args)
		{
			List<ParameterArguments> list = new List<ParameterArguments>(this.context.ParameterArguments.Count + 1);
			list.AddRange(this.context.ParameterArguments);
			ParameterValue parameterValue = (ParameterValue)parameter;
			list.Add(new ParameterArguments(parameterValue.Parameter, args));
			CubeContext cubeContext;
			if (this.contextProvider.TryCreateContext(this.context.CubeExpression, list, out cubeContext))
			{
				return CubeContextCubeValue.New(this.contextProvider, cubeContext, this.view);
			}
			throw CubeValue.NewInvalidCubeException();
		}

		// Token: 0x060058BB RID: 22715 RVA: 0x001364C4 File Offset: 0x001346C4
		public override bool TrySelectColumns(ColumnSelection columnSelection, out TableValue newTable)
		{
			CubeValue cubeValue;
			if (this.TryProjectColumns(columnSelection, out cubeValue))
			{
				newTable = cubeValue;
				return true;
			}
			newTable = null;
			return false;
		}

		// Token: 0x060058BC RID: 22716 RVA: 0x001364E8 File Offset: 0x001346E8
		private bool TryProjectColumns(ColumnSelection columnSelection, out CubeValue newCube)
		{
			View view = this.view.SelectColumns(columnSelection);
			View.Map map = view.CreateMap(this.context.CubeExpression.DimensionAttributes.Count + this.context.CubeExpression.Properties.Count + this.context.CubeExpression.Measures.Count + this.context.CubeExpression.MeasureProperties.Count);
			ArrayBuilder<IdentifierCubeExpression> arrayBuilder = new ArrayBuilder<IdentifierCubeExpression>(this.context.CubeExpression.Properties.Count);
			for (int i = this.context.CubeExpression.Properties.Count - 1; i >= 0; i--)
			{
				if (map.MapColumnToSomeKey(this.context.CubeExpression.DimensionAttributes.Count + i) != -1)
				{
					arrayBuilder.Add(this.context.CubeExpression.Properties[i]);
				}
				else
				{
					view = view.RemoveInner(this.context.CubeExpression.DimensionAttributes.Count + i);
				}
			}
			IdentifierCubeExpression[] array = arrayBuilder.ToArray();
			Array.Reverse(array);
			ArrayBuilder<IdentifierCubeExpression> arrayBuilder2 = new ArrayBuilder<IdentifierCubeExpression>(this.context.CubeExpression.MeasureProperties.Count);
			for (int j = this.context.CubeExpression.MeasureProperties.Count - 1; j >= 0; j--)
			{
				if (map.MapColumnToSomeKey(this.context.CubeExpression.DimensionAttributes.Count + array.Length + this.context.CubeExpression.Measures.Count + j) != -1)
				{
					arrayBuilder2.Add(this.context.CubeExpression.MeasureProperties[j]);
				}
				else
				{
					view = view.RemoveInner(this.context.CubeExpression.DimensionAttributes.Count + array.Length + this.context.CubeExpression.Measures.Count + j);
				}
			}
			IdentifierCubeExpression[] array2 = arrayBuilder2.ToArray();
			Array.Reverse(array2);
			QueryCubeExpression queryCubeExpression = new QueryCubeExpression(this.context.CubeExpression.From, this.context.CubeExpression.DimensionAttributes, array, this.context.CubeExpression.Measures, array2, this.context.CubeExpression.Filter, this.context.CubeExpression.Sort, this.context.CubeExpression.RowRange);
			CubeContext cubeContext;
			if (this.contextProvider.TryCreateContext(queryCubeExpression, this.context.ParameterArguments, out cubeContext))
			{
				newCube = CubeContextCubeValue.New(this.contextProvider, cubeContext, view);
				return true;
			}
			newCube = null;
			return false;
		}

		// Token: 0x060058BD RID: 22717 RVA: 0x001367A1 File Offset: 0x001349A1
		public override TableValue SelectRows(FunctionValue condition)
		{
			return CubeContextCubeValue.PromoteIfCube(this.Table.SelectRows(condition));
		}

		// Token: 0x060058BE RID: 22718 RVA: 0x001367B4 File Offset: 0x001349B4
		private bool TrySelectRows(FunctionValue condition, out CubeValue newCube)
		{
			if (this.context.CubeExpression.RowRange.IsAll)
			{
				QueryExpression queryExpression = QueryExpressionBuilder.ToQueryExpression(this.Type.AsTableType.ItemType, condition);
				CubeExpression cubeExpression;
				if (this.TryToCubeExpression(queryExpression, out cubeExpression))
				{
					QueryCubeExpression queryCubeExpression = new QueryCubeExpression(this.context.CubeExpression.From, this.context.CubeExpression.DimensionAttributes, this.context.CubeExpression.Properties, this.context.CubeExpression.Measures, this.context.CubeExpression.MeasureProperties, CubeContextCubeValue.CombineFilters(this.context.CubeExpression.Filter, cubeExpression), this.context.CubeExpression.Sort, this.context.CubeExpression.RowRange);
					CubeContext cubeContext;
					if (this.contextProvider.TryCreateContext(queryCubeExpression, this.context.ParameterArguments, out cubeContext))
					{
						newCube = CubeContextCubeValue.New(this.contextProvider, cubeContext, this.view);
						return true;
					}
				}
			}
			newCube = null;
			return false;
		}

		// Token: 0x060058BF RID: 22719 RVA: 0x001368C5 File Offset: 0x00134AC5
		public override TableValue Sort(TableSortOrder sortOrder)
		{
			return CubeContextCubeValue.PromoteIfCube(this.Table.Sort(sortOrder));
		}

		// Token: 0x060058C0 RID: 22720 RVA: 0x001368D8 File Offset: 0x00134AD8
		private bool TrySort(TableSortOrder sortOrder, out CubeValue newCube)
		{
			QueryExpression[] array;
			bool[] array2;
			if (this.context.CubeExpression.RowRange.IsAll && SortQuery.TryGetSelectors(sortOrder, this.Type.AsTableType.ItemType, out array, out array2))
			{
				List<CubeSortOrder> list = new List<CubeSortOrder>();
				int i = 0;
				while (i < array.Length)
				{
					CubeExpression cubeExpression;
					if (this.TryToCubeExpression(array[i], out cubeExpression))
					{
						CubeExpressionKind kind = cubeExpression.Kind;
						if (kind != CubeExpressionKind.Constant)
						{
							if (kind != CubeExpressionKind.Identifier)
							{
								goto IL_007A;
							}
							list.Add(new CubeSortOrder(cubeExpression, array2[i]));
						}
						i++;
						continue;
					}
					IL_007A:
					list = null;
					break;
				}
				if (list != null)
				{
					if (list.Count == 0)
					{
						return this.TryUnordered(out newCube);
					}
					HashSet<string> hashSet = new HashSet<string>();
					ArrayBuilder<CubeSortOrder> arrayBuilder = default(ArrayBuilder<CubeSortOrder>);
					foreach (CubeSortOrder cubeSortOrder in list)
					{
						hashSet.Add(((IdentifierCubeExpression)cubeSortOrder.Expression).Identifier);
						arrayBuilder.Add(cubeSortOrder);
					}
					foreach (CubeSortOrder cubeSortOrder2 in this.context.CubeExpression.Sort)
					{
						if (!hashSet.Contains(((IdentifierCubeExpression)cubeSortOrder2.Expression).Identifier))
						{
							arrayBuilder.Add(cubeSortOrder2);
						}
					}
					QueryCubeExpression queryCubeExpression = new QueryCubeExpression(this.context.CubeExpression.From, this.context.CubeExpression.DimensionAttributes, this.context.CubeExpression.Properties, this.context.CubeExpression.Measures, this.context.CubeExpression.MeasureProperties, this.context.CubeExpression.Filter, arrayBuilder.ToArray(), this.context.CubeExpression.RowRange);
					CubeContext cubeContext;
					if (this.contextProvider.TryCreateContext(queryCubeExpression, this.context.ParameterArguments, out cubeContext))
					{
						newCube = CubeContextCubeValue.New(this.contextProvider, cubeContext, this.view);
						return true;
					}
				}
			}
			newCube = null;
			return false;
		}

		// Token: 0x060058C1 RID: 22721 RVA: 0x00136B10 File Offset: 0x00134D10
		public override TableValue Unordered()
		{
			return CubeContextCubeValue.PromoteIfCube(this.Table.Unordered());
		}

		// Token: 0x060058C2 RID: 22722 RVA: 0x00136B24 File Offset: 0x00134D24
		private bool TryUnordered(out CubeValue newCube)
		{
			if (this.context.CubeExpression.RowRange.IsAll)
			{
				QueryCubeExpression queryCubeExpression = new QueryCubeExpression(this.context.CubeExpression.From, this.context.CubeExpression.DimensionAttributes, this.context.CubeExpression.Properties, this.context.CubeExpression.Measures, this.context.CubeExpression.MeasureProperties, this.context.CubeExpression.Filter, EmptyArray<CubeSortOrder>.Instance, this.context.CubeExpression.RowRange);
				CubeContext cubeContext;
				if (this.contextProvider.TryCreateContext(queryCubeExpression, this.context.ParameterArguments, out cubeContext))
				{
					newCube = CubeContextCubeValue.New(this.contextProvider, cubeContext, this.view);
					return true;
				}
			}
			newCube = null;
			return false;
		}

		// Token: 0x060058C3 RID: 22723 RVA: 0x00136C00 File Offset: 0x00134E00
		public override TableValue Skip(RowCount count)
		{
			TableValue tableValue = CubeContextCubeValue.PromoteIfCube(this.Table.Skip(count));
			if (tableValue.IsCube)
			{
				tableValue = new CubeContextCubeValue.SkipTakeCubeValue(tableValue.AsCube);
			}
			return tableValue;
		}

		// Token: 0x060058C4 RID: 22724 RVA: 0x00136C34 File Offset: 0x00134E34
		private bool TrySkip(RowCount count, out CubeValue newCube)
		{
			QueryCubeExpression queryCubeExpression = new QueryCubeExpression(this.context.CubeExpression.From, this.context.CubeExpression.DimensionAttributes, this.context.CubeExpression.Properties, this.context.CubeExpression.Measures, this.context.CubeExpression.MeasureProperties, this.context.CubeExpression.Filter, this.context.CubeExpression.Sort, this.context.CubeExpression.RowRange.Skip(count));
			CubeContext cubeContext;
			if (this.contextProvider.TryCreateContext(queryCubeExpression, this.context.ParameterArguments, out cubeContext))
			{
				newCube = CubeContextCubeValue.New(this.contextProvider, cubeContext, this.view);
				return true;
			}
			newCube = null;
			return false;
		}

		// Token: 0x060058C5 RID: 22725 RVA: 0x00136D08 File Offset: 0x00134F08
		public override TableValue Take(RowCount count)
		{
			TableValue tableValue = CubeContextCubeValue.PromoteIfCube(this.Table.Take(count));
			if (tableValue.IsCube)
			{
				tableValue = new CubeContextCubeValue.SkipTakeCubeValue(tableValue.AsCube);
			}
			return tableValue;
		}

		// Token: 0x060058C6 RID: 22726 RVA: 0x00136D3C File Offset: 0x00134F3C
		public override bool TryInvokeAsArgument(FunctionValue function, Value[] arguments, int index, out Value result)
		{
			if (function.FunctionIdentity.Equals(CapabilityModule.DirectQueryCapabilities.From.FunctionIdentity) && index == 0)
			{
				result = this.context.DirectQueryCapabilities;
				return true;
			}
			result = null;
			return false;
		}

		// Token: 0x060058C7 RID: 22727 RVA: 0x00136D70 File Offset: 0x00134F70
		private bool TryTake(RowCount count, out CubeValue newCube)
		{
			QueryCubeExpression queryCubeExpression = new QueryCubeExpression(this.context.CubeExpression.From, this.context.CubeExpression.DimensionAttributes, this.context.CubeExpression.Properties, this.context.CubeExpression.Measures, this.context.CubeExpression.MeasureProperties, this.context.CubeExpression.Filter, this.context.CubeExpression.Sort, this.context.CubeExpression.RowRange.Take(count));
			CubeContext cubeContext;
			if (this.contextProvider.TryCreateContext(queryCubeExpression, this.context.ParameterArguments, out cubeContext))
			{
				newCube = CubeContextCubeValue.New(this.contextProvider, cubeContext, this.view);
				return true;
			}
			newCube = null;
			return false;
		}

		// Token: 0x060058C8 RID: 22728 RVA: 0x00136E44 File Offset: 0x00135044
		public override CubeValue ExpandDimensionAttributes(TableValue dimensionTable)
		{
			if (this.context.CubeExpression.RowRange.IsAll)
			{
				CubeContextCubeValue cubeContextCubeValue = dimensionTable.SubtractMetaValue.AsTable.AsCube as CubeContextCubeValue;
				if (cubeContextCubeValue != null && cubeContextCubeValue.context.CubeExpression.From.Kind == CubeExpressionKind.Identifier && cubeContextCubeValue.context.CubeExpression.Measures.Count == 0 && (cubeContextCubeValue.context.CubeExpression.Filter == null || !cubeContextCubeValue.ContainsMeasures(cubeContextCubeValue.context.CubeExpression.Filter)))
				{
					int[] array;
					List<IdentifierCubeExpression> newItemsAndBuildMap = this.GetNewItemsAndBuildMap(this.context.CubeExpression.DimensionAttributes, cubeContextCubeValue.context.CubeExpression.DimensionAttributes, 0, out array);
					int[] array2;
					List<IdentifierCubeExpression> newItemsAndBuildMap2 = this.GetNewItemsAndBuildMap(this.context.CubeExpression.Properties, cubeContextCubeValue.context.CubeExpression.Properties, newItemsAndBuildMap.Count, out array2);
					View.Builder builder = this.BuildNewView(newItemsAndBuildMap.Count, newItemsAndBuildMap2.Count, this.context.CubeExpression.Measures.Count, this.context.CubeExpression.MeasureProperties.Count);
					for (int i = 0; i < cubeContextCubeValue.view.Keys.Length; i++)
					{
						int dimensionAttribute = cubeContextCubeValue.GetDimensionAttribute(i);
						if (dimensionAttribute != -1)
						{
							builder.Add(dimensionTable.Columns[i], array[dimensionAttribute]);
						}
						else
						{
							int property = cubeContextCubeValue.GetProperty(i);
							if (property == -1)
							{
								throw ValueException.NewExpressionError<Message1>(Strings.Cube_ColumnNotDimensionAttribute(dimensionTable.Columns[i]), dimensionTable, null);
							}
							builder.Add(dimensionTable.Columns[i], array2[property]);
						}
					}
					QueryCubeExpression queryCubeExpression = this.PushDownFilterAndSort(this.context.CubeExpression, newItemsAndBuildMap);
					queryCubeExpression = new QueryCubeExpression(queryCubeExpression.From, queryCubeExpression.DimensionAttributes, newItemsAndBuildMap2, queryCubeExpression.Measures, queryCubeExpression.MeasureProperties, CubeContextCubeValue.CombineFilters(queryCubeExpression.Filter, cubeContextCubeValue.context.CubeExpression.Filter), queryCubeExpression.Sort, queryCubeExpression.RowRange);
					CubeContext cubeContext;
					if (this.contextProvider.TryCreateContext(queryCubeExpression, this.context.ParameterArguments, out cubeContext))
					{
						return CubeContextCubeValue.New(this.contextProvider, cubeContext, builder.ToView());
					}
				}
			}
			throw CubeValue.NewInvalidCubeException();
		}

		// Token: 0x060058C9 RID: 22729 RVA: 0x001370B4 File Offset: 0x001352B4
		public override CubeValue CollapseDimensionAttributes(int[] columns)
		{
			if (this.context.CubeExpression.RowRange.IsAll)
			{
				List<IdentifierCubeExpression> list = new List<IdentifierCubeExpression>(this.context.CubeExpression.DimensionAttributes);
				View view = this.view;
				int[] array = columns.OrderByDescending((int c) => c).ToArray<int>();
				int[] array2 = (from c in columns
					select this.GetDimensionAttribute(c) into d
					orderby d descending
					select d).ToArray<int>();
				foreach (int num in array)
				{
					if (this.GetDimensionAttribute(num) == -1)
					{
						throw ValueException.NewExpressionError<Message1>(Strings.Cube_ColumnNotDimensionAttribute(this.Columns[num]), this, null);
					}
					view = view.Remove(num);
				}
				View.Map map = view.CreateMap(this.context.CubeExpression.DimensionAttributes.Count + this.context.CubeExpression.Properties.Count + this.context.CubeExpression.Measures.Count + this.context.CubeExpression.MeasureProperties.Count);
				foreach (int num2 in array2)
				{
					if (map.MapColumnToSomeKey(num2) == -1 && num2 < list.Count && list[num2].Equals(this.context.CubeExpression.DimensionAttributes[num2]))
					{
						list.RemoveAt(num2);
						view = view.RemoveInner(num2);
					}
				}
				HashSet<IdentifierCubeExpression> hashSet = new HashSet<IdentifierCubeExpression>(list);
				for (int k = 0; k < this.context.CubeExpression.Properties.Count; k++)
				{
					IdentifierCubeExpression identifierCubeExpression = this.context.CubeExpression.Properties[k];
					IdentifierCubeExpression propertyDimensionAttribute = this.contextProvider.GetPropertyDimensionAttribute(identifierCubeExpression);
					if (!hashSet.Contains(propertyDimensionAttribute))
					{
						View.Map map2 = this.view.CreateMap(this.context.CubeExpression.DimensionAttributes.Count + this.context.CubeExpression.Properties.Count + this.context.CubeExpression.Measures.Count + this.context.CubeExpression.MeasureProperties.Count);
						int num3 = map2.MapColumnToSomeKey(this.context.CubeExpression.DimensionAttributes.Count + k);
						int num4 = map2.MapColumnToSomeKey(this.IndexOfIdentifier(propertyDimensionAttribute));
						throw ValueException.NewExpressionError<Message2>(Strings.Cube_PropertyRequiresDimensionAttribute(this.Columns[num3], this.Columns[num4]), this, null);
					}
				}
				QueryCubeExpression queryCubeExpression = this.PushDownFilterAndSort(this.context.CubeExpression, list);
				CubeContext cubeContext;
				if (this.contextProvider.TryCreateContext(queryCubeExpression, this.context.ParameterArguments, out cubeContext))
				{
					return CubeContextCubeValue.New(this.contextProvider, cubeContext, view);
				}
			}
			throw CubeValue.NewInvalidCubeException();
		}

		// Token: 0x060058CA RID: 22730 RVA: 0x001373E0 File Offset: 0x001355E0
		public override CubeValue AddMeasureColumn(string columnName, FunctionValue function)
		{
			MeasureValue measureValue = function.SubtractMetaValue.AsFunction as MeasureValue;
			if (measureValue == null)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.Cube_FunctionNotAMeasure, function, null);
			}
			IdentifierCubeExpression[] array = this.context.CubeExpression.Measures.ToArray<IdentifierCubeExpression>();
			int num = this.IndexOfIdentifier(measureValue.Measure);
			if (num == -1)
			{
				num = this.context.CubeExpression.DimensionAttributes.Count + this.context.CubeExpression.Properties.Count + array.Length;
				array = array.Add(measureValue.Measure);
			}
			View.Builder builder = this.BuildNewView(this.context.CubeExpression.DimensionAttributes.Count, this.context.CubeExpression.Properties.Count, array.Length, this.context.CubeExpression.MeasureProperties.Count);
			builder.Add(columnName, num);
			QueryCubeExpression queryCubeExpression = new QueryCubeExpression(this.context.CubeExpression.From, this.context.CubeExpression.DimensionAttributes, this.context.CubeExpression.Properties, array, this.context.CubeExpression.MeasureProperties, this.context.CubeExpression.Filter, this.context.CubeExpression.Sort, this.context.CubeExpression.RowRange);
			CubeContext cubeContext;
			if (this.contextProvider.TryCreateContext(queryCubeExpression, this.context.ParameterArguments, out cubeContext))
			{
				return CubeContextCubeValue.New(this.contextProvider, cubeContext, builder.ToView());
			}
			throw CubeValue.NewInvalidCubeException();
		}

		// Token: 0x060058CB RID: 22731 RVA: 0x00137572 File Offset: 0x00135772
		public override TableValue AddColumns(ColumnsConstructor columnsCtor)
		{
			return CubeContextCubeValue.PromoteIfCube(this.Table.AddColumns(columnsCtor));
		}

		// Token: 0x060058CC RID: 22732 RVA: 0x00137585 File Offset: 0x00135785
		private Func<CubeExpression, TypeValue, IdentifierCubeExpression> ToGetIdentifierOrNull(Func<IdentifierCubeExpression, bool> filter)
		{
			return delegate(CubeExpression expr, TypeValue typeValue)
			{
				IdentifierCubeExpression identifierCubeExpression = expr as IdentifierCubeExpression;
				if (identifierCubeExpression != null && filter(identifierCubeExpression))
				{
					return identifierCubeExpression;
				}
				return null;
			};
		}

		// Token: 0x060058CD RID: 22733 RVA: 0x001375A0 File Offset: 0x001357A0
		private List<KeyValuePair<IdentifierCubeExpression, int>> ToCubeExpressions(IList<QueryExpression> expressions, IValueReference[] types, Func<CubeExpression, TypeValue, IdentifierCubeExpression> getIdentifierOrNull)
		{
			List<KeyValuePair<IdentifierCubeExpression, int>> list = new List<KeyValuePair<IdentifierCubeExpression, int>>();
			for (int i = 0; i < expressions.Count; i++)
			{
				CubeExpression cubeExpression;
				if (this.TryToCubeExpression(expressions[i], out cubeExpression))
				{
					IdentifierCubeExpression identifierCubeExpression = getIdentifierOrNull(cubeExpression, types[i].Value.AsType);
					if (identifierCubeExpression != null)
					{
						list.Add(new KeyValuePair<IdentifierCubeExpression, int>(identifierCubeExpression, i));
					}
				}
			}
			return list;
		}

		// Token: 0x060058CE RID: 22734 RVA: 0x001375FC File Offset: 0x001357FC
		private List<IdentifierCubeExpression> GetNewItemsAndBuildMap(IList<IdentifierCubeExpression> existingItems, IList<IdentifierCubeExpression> newIdentifiers, int baseCount, out int[] itemMap)
		{
			List<IdentifierCubeExpression> list = new List<IdentifierCubeExpression>(existingItems);
			itemMap = new int[newIdentifiers.Count];
			for (int i = 0; i < itemMap.Length; i++)
			{
				IdentifierCubeExpression identifierCubeExpression = newIdentifiers[i];
				int num = this.IndexOfIdentifier(identifierCubeExpression);
				if (num == -1)
				{
					num = baseCount + list.Count;
					list.Add(identifierCubeExpression);
				}
				itemMap[i] = num;
			}
			return list;
		}

		// Token: 0x060058CF RID: 22735 RVA: 0x0013765C File Offset: 0x0013585C
		private bool TryAddColumns(ColumnsConstructor columnsCtor, out CubeValue newCube)
		{
			IList<QueryExpression> list = AddColumnsQuery.CreateQueryExpressions(columnsCtor.Function, this.Type.AsTableType.ItemType);
			if (list != null)
			{
				List<KeyValuePair<IdentifierCubeExpression, int>> list2 = this.ToCubeExpressions(list, columnsCtor.Types, this.ToGetIdentifierOrNull(new Func<IdentifierCubeExpression, bool>(this.IsProperty)));
				List<KeyValuePair<IdentifierCubeExpression, int>> list3 = this.ToCubeExpressions(list, columnsCtor.Types, this.ToGetIdentifierOrNull(new Func<IdentifierCubeExpression, bool>(this.IsMeasureProperty)));
				List<KeyValuePair<IdentifierCubeExpression, int>> list4 = this.ToCubeExpressions(list, columnsCtor.Types, new Func<CubeExpression, TypeValue, IdentifierCubeExpression>(this.GetDynamicDimensionAttributeOrNull));
				if (list2.Count + list3.Count + list4.Count == list.Count)
				{
					int[] array = new int[list.Count];
					int num = this.context.CubeExpression.DimensionAttributes.Count;
					int num2 = 0;
					foreach (KeyValuePair<IdentifierCubeExpression, int> keyValuePair in list4)
					{
						int num3 = this.IndexOfIdentifier(keyValuePair.Key);
						if (num3 == -1)
						{
							num3 = num + num2;
							num2++;
						}
						array[keyValuePair.Value] = num3;
					}
					num += list4.Count + this.context.CubeExpression.Properties.Count;
					num2 = 0;
					foreach (KeyValuePair<IdentifierCubeExpression, int> keyValuePair2 in list2)
					{
						int num4 = this.IndexOfIdentifier(keyValuePair2.Key);
						if (num4 == -1)
						{
							num4 = num + num2;
							num2++;
						}
						array[keyValuePair2.Value] = num4;
					}
					num += list2.Count + this.context.CubeExpression.Measures.Count + this.context.CubeExpression.MeasureProperties.Count;
					num2 = 0;
					foreach (KeyValuePair<IdentifierCubeExpression, int> keyValuePair3 in list3)
					{
						int num5 = this.IndexOfIdentifier(keyValuePair3.Key);
						if (num5 == -1)
						{
							num5 = num + num2;
							num2++;
						}
						array[keyValuePair3.Value] = num5;
					}
					List<IdentifierCubeExpression> list5 = new List<IdentifierCubeExpression>(this.context.CubeExpression.DimensionAttributes);
					list5.AddRange(list4.Select((KeyValuePair<IdentifierCubeExpression, int> d) => d.Key));
					List<IdentifierCubeExpression> list6 = new List<IdentifierCubeExpression>(this.context.CubeExpression.Properties);
					list6.AddRange(list2.Select((KeyValuePair<IdentifierCubeExpression, int> p) => p.Key));
					List<IdentifierCubeExpression> list7 = new List<IdentifierCubeExpression>(this.context.CubeExpression.MeasureProperties);
					list7.AddRange(list3.Select((KeyValuePair<IdentifierCubeExpression, int> p) => p.Key));
					View.Builder builder = this.BuildNewView(list5.Count, list6.Count, this.context.CubeExpression.Measures.Count, list7.Count);
					for (int i = 0; i < array.Length; i++)
					{
						builder.Add(columnsCtor.Names[i], array[i]);
					}
					QueryCubeExpression queryCubeExpression = new QueryCubeExpression(this.context.CubeExpression.From, list5, list6.ToArray(), this.context.CubeExpression.Measures, list7.ToArray(), this.context.CubeExpression.Filter, this.context.CubeExpression.Sort, this.context.CubeExpression.RowRange);
					CubeContext cubeContext;
					if (this.contextProvider.TryCreateContext(queryCubeExpression, this.context.ParameterArguments, out cubeContext))
					{
						newCube = CubeContextCubeValue.New(this.contextProvider, cubeContext, builder.ToView());
						return true;
					}
				}
			}
			newCube = null;
			return false;
		}

		// Token: 0x060058D0 RID: 22736 RVA: 0x00137A84 File Offset: 0x00135C84
		private View.Builder BuildNewView(int dimensionAttributeCount, int propertiesCount, int measuresCount, int measurePropertiesCount)
		{
			View.Builder builder = default(View.Builder);
			for (int i = 0; i < this.view.Keys.Length; i++)
			{
				int num = this.GetDimensionAttribute(i);
				if (num == -1)
				{
					num = this.GetProperty(i);
					if (num == -1)
					{
						num = this.GetMeasure(i);
						if (num == -1)
						{
							num = this.GetMeasureProperty(i);
							if (num == -1)
							{
								num = dimensionAttributeCount + propertiesCount + measuresCount + measurePropertiesCount;
							}
							else
							{
								num += dimensionAttributeCount + propertiesCount + measuresCount;
							}
						}
						else
						{
							num += dimensionAttributeCount + propertiesCount;
						}
					}
					else
					{
						num += dimensionAttributeCount;
					}
				}
				builder.Add(this.view.Keys[i], num);
			}
			return builder;
		}

		// Token: 0x060058D1 RID: 22737 RVA: 0x00137B28 File Offset: 0x00135D28
		private IdentifierCubeExpression GetDynamicDimensionAttributeOrNull(CubeExpression expression, TypeValue typeValue)
		{
			IdentifierCubeExpression identifierCubeExpression;
			if (!this.contextProvider.TryGetDynamicDimensionAttribute(expression, typeValue, out identifierCubeExpression))
			{
				identifierCubeExpression = null;
			}
			return identifierCubeExpression;
		}

		// Token: 0x060058D2 RID: 22738 RVA: 0x00137B49 File Offset: 0x00135D49
		public override TableValue Distinct(TableDistinct distinctCriteria)
		{
			return this.Table.Distinct(distinctCriteria);
		}

		// Token: 0x060058D3 RID: 22739 RVA: 0x00137B57 File Offset: 0x00135D57
		public override TableValue ExpandListColumn(int columnIndex, bool singleOrDefault)
		{
			return this.Table.ExpandListColumn(columnIndex, singleOrDefault);
		}

		// Token: 0x060058D4 RID: 22740 RVA: 0x00137B66 File Offset: 0x00135D66
		public override TableValue ExpandRecordColumn(int columnToExpand, Keys fieldsToProject, Keys newColumns)
		{
			return this.Table.ExpandRecordColumn(columnToExpand, fieldsToProject, newColumns);
		}

		// Token: 0x060058D5 RID: 22741 RVA: 0x00137B76 File Offset: 0x00135D76
		public override TableValue Group(Grouping grouping)
		{
			return this.Table.Group(grouping);
		}

		// Token: 0x060058D6 RID: 22742 RVA: 0x00137B84 File Offset: 0x00135D84
		public override TableValue NestedJoin(int[] leftKeyColumns, Value rightTable, Keys rightKey, TableTypeAlgebra.JoinKind joinKind, string newColumn, Keys joinKeys, FunctionValue[] keyEqualityComparers)
		{
			return this.Table.NestedJoin(leftKeyColumns, rightTable, rightKey, joinKind, newColumn, joinKeys, keyEqualityComparers);
		}

		// Token: 0x060058D7 RID: 22743 RVA: 0x00137B9C File Offset: 0x00135D9C
		public override IPageReader GetReader()
		{
			return this.Table.GetReader();
		}

		// Token: 0x060058D8 RID: 22744 RVA: 0x00137BAC File Offset: 0x00135DAC
		private void EnsureParametersProvided()
		{
			using (IEnumerator<IValueReference> enumerator = this.context.GetParameters(this).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current.Value[CubeParametersTableValue.IsOptional].AsBoolean)
					{
						throw ValueException.NewParameterError<Message0>(Strings.Cube_ParameterMissing, Value.Null);
					}
				}
			}
		}

		// Token: 0x060058D9 RID: 22745 RVA: 0x00137C20 File Offset: 0x00135E20
		private CubeContextCubeValue ExposeDimensionAttributes(IEnumerable<IdentifierCubeExpression> attributesToExpose)
		{
			HashSet<IdentifierCubeExpression> hashSet = new HashSet<IdentifierCubeExpression>(attributesToExpose);
			IList<IdentifierCubeExpression> list = this.context.CubeExpression.DimensionAttributes;
			IList<IdentifierCubeExpression> properties = this.context.CubeExpression.Properties;
			IList<IdentifierCubeExpression> measures = this.context.CubeExpression.Measures;
			IList<IdentifierCubeExpression> measureProperties = this.context.CubeExpression.MeasureProperties;
			View.Map map = this.view.CreateMap(list.Count + properties.Count + measures.Count + measureProperties.Count);
			View.Builder builder = default(View.Builder);
			builder.Add(this.view);
			for (int i = 0; i < list.Count; i++)
			{
				if (map.MapColumnToSomeKey(i) == -1 && hashSet.Contains(list[i]))
				{
					builder.Add(TableValue.GetUniqueName(this.view.Keys, i), i);
				}
			}
			return new CubeContextCubeValue(this.contextProvider, this.context, builder.ToView());
		}

		// Token: 0x060058DA RID: 22746 RVA: 0x00137D24 File Offset: 0x00135F24
		private QueryCubeExpression PushDownFilterAndSort(QueryCubeExpression query, IList<IdentifierCubeExpression> newDimensionAttributes)
		{
			HashSet<IdentifierCubeExpression> newDimensionAttributesSet = new HashSet<IdentifierCubeExpression>(newDimensionAttributes);
			object obj = query.DimensionAttributes.Count != newDimensionAttributes.Count || query.DimensionAttributes.Any((IdentifierCubeExpression da) => !newDimensionAttributesSet.Contains(da));
			HashSet<IdentifierCubeExpression> hashSet = new HashSet<IdentifierCubeExpression>();
			object obj2 = obj;
			bool flag = obj2 != null && this.RequiresPushDown(hashSet, query.DimensionAttributes, query.Filter, newDimensionAttributesSet);
			bool flag2 = false;
			List<CubeSortOrder> list = new List<CubeSortOrder>();
			if (obj2 != null)
			{
				foreach (CubeSortOrder cubeSortOrder in query.Sort)
				{
					if (this.RequiresPushDown(hashSet, query.DimensionAttributes, cubeSortOrder.Expression, newDimensionAttributesSet))
					{
						flag2 = !query.RowRange.IsAll;
					}
					else
					{
						list.Add(cubeSortOrder);
					}
				}
			}
			CubeExpression cubeExpression = query.From;
			if (flag || flag2)
			{
				CubeExpression from = query.From;
				IList<IdentifierCubeExpression> list2 = hashSet.Where(new Func<IdentifierCubeExpression, bool>(this.IsDimensionAttribute)).ToArray<IdentifierCubeExpression>();
				IList<IdentifierCubeExpression> list3 = hashSet.Where(new Func<IdentifierCubeExpression, bool>(this.IsProperty)).ToArray<IdentifierCubeExpression>();
				IList<IdentifierCubeExpression> list4 = hashSet.Where(new Func<IdentifierCubeExpression, bool>(this.IsMeasure)).ToArray<IdentifierCubeExpression>();
				IList<IdentifierCubeExpression> list5 = hashSet.Where(new Func<IdentifierCubeExpression, bool>(this.IsMeasureProperty)).ToArray<IdentifierCubeExpression>();
				CubeExpression cubeExpression2 = (flag ? query.Filter : null);
				IList<CubeSortOrder> list6;
				if (!flag2)
				{
					IList<CubeSortOrder> instance = EmptyArray<CubeSortOrder>.Instance;
					list6 = instance;
				}
				else
				{
					list6 = query.Sort;
				}
				cubeExpression = new QueryCubeExpression(from, list2, list3, list4, list5, cubeExpression2, list6, query.RowRange);
			}
			return new QueryCubeExpression(cubeExpression, newDimensionAttributes, query.Properties, query.Measures, query.MeasureProperties, flag ? null : query.Filter, flag2 ? EmptyArray<CubeSortOrder>.Instance : list.ToArray(), RowRange.All);
		}

		// Token: 0x060058DB RID: 22747 RVA: 0x00137EF8 File Offset: 0x001360F8
		private bool RequiresPushDown(HashSet<IdentifierCubeExpression> identifiersToPush, IList<IdentifierCubeExpression> dimensionAttributes, CubeExpression expression, HashSet<IdentifierCubeExpression> newDimensionAttributes)
		{
			bool flag = false;
			if (expression != null)
			{
				foreach (IdentifierCubeExpression identifierCubeExpression in expression.GetReferences())
				{
					IdentifierCubeExpression identifierCubeExpression2 = identifierCubeExpression;
					if (this.IsProperty(identifierCubeExpression))
					{
						identifierCubeExpression2 = this.contextProvider.GetPropertyDimensionAttribute(identifierCubeExpression);
					}
					if (!newDimensionAttributes.Contains(identifierCubeExpression2))
					{
						identifiersToPush.Add(identifierCubeExpression2);
						identifiersToPush.Add(identifierCubeExpression);
						flag = true;
					}
					if (this.IsMeasure(identifierCubeExpression))
					{
						identifiersToPush.UnionWith(dimensionAttributes);
						flag = true;
					}
				}
			}
			return flag;
		}

		// Token: 0x060058DC RID: 22748 RVA: 0x00137F8C File Offset: 0x0013618C
		private bool ContainsMeasures(CubeExpression expression)
		{
			return expression.GetReferences().Any((IdentifierCubeExpression i) => this.IsMeasure(i));
		}

		// Token: 0x060058DD RID: 22749 RVA: 0x00137FA5 File Offset: 0x001361A5
		private bool TryToCubeExpression(QueryExpression queryExpr, out CubeExpression cubeExpr)
		{
			cubeExpr = new QueryExpressionTranslator(new CubeContextCubeValue.CubeContextCubeValueCubeMetadataProvider(this)).Translate(queryExpr);
			return cubeExpr != null;
		}

		// Token: 0x060058DE RID: 22750 RVA: 0x00137FC0 File Offset: 0x001361C0
		private static TableValue PromoteIfCube(TableValue table)
		{
			CubeContextCubeValue.TabularCubeQuery tabularCubeQuery = table.Query as CubeContextCubeValue.TabularCubeQuery;
			if (tabularCubeQuery != null)
			{
				return CubeContextCubeValue.AddCubeMetadata(tabularCubeQuery.Cube);
			}
			return table;
		}

		// Token: 0x060058DF RID: 22751 RVA: 0x00137FEC File Offset: 0x001361EC
		private static CubeExpression CombineFilters(CubeExpression filter1, CubeExpression filter2)
		{
			CubeExpression cubeExpression = filter1.And(filter2);
			if (ConstantCubeExpression.True.Equals(cubeExpression))
			{
				return null;
			}
			return cubeExpression;
		}

		// Token: 0x060058E0 RID: 22752 RVA: 0x00138014 File Offset: 0x00136214
		private static bool TryGetNotEmptyFilter(int columnCount, Func<int, bool> notNullRequired, QueryExpression expression, out QueryExpression newExpression)
		{
			bool flag = false;
			List<QueryExpression> conjunctiveNF = SelectRowsQuery.GetConjunctiveNF(expression);
			for (int i = 0; i < conjunctiveNF.Count; i++)
			{
				bool[] array = new bool[columnCount];
				List<QueryExpression> disjunctiveNF = SelectRowsQuery.GetDisjunctiveNF(conjunctiveNF[i]);
				for (int j = 0; j < disjunctiveNF.Count; j++)
				{
					int num;
					if (!CubeContextCubeValue.TryGetNotNullFilter(disjunctiveNF[j], out num))
					{
						array = null;
						break;
					}
					array[num] = true;
				}
				bool flag2 = array != null;
				int num2 = 0;
				while (flag2 && num2 < array.Length)
				{
					flag2 &= array[num2] == notNullRequired(num2);
					num2++;
				}
				if (flag2)
				{
					conjunctiveNF[i] = ConstantQueryExpression.True;
					flag = true;
				}
			}
			if (flag)
			{
				newExpression = SelectRowsQuery.CreateConjunctiveNF(conjunctiveNF);
				return true;
			}
			newExpression = null;
			return false;
		}

		// Token: 0x060058E1 RID: 22753 RVA: 0x001380DC File Offset: 0x001362DC
		private static bool TryGetNotNullFilter(QueryExpression expression, out int column)
		{
			QueryExpressionKind kind = expression.Kind;
			if (kind == QueryExpressionKind.Binary)
			{
				return CubeContextCubeValue.TryGetNullFilter((BinaryQueryExpression)expression, BinaryOperator2.NotEquals, out column);
			}
			if (kind == QueryExpressionKind.Unary)
			{
				return CubeContextCubeValue.TryGetNotNullFilter((UnaryQueryExpression)expression, out column);
			}
			column = -1;
			return false;
		}

		// Token: 0x060058E2 RID: 22754 RVA: 0x00138117 File Offset: 0x00136317
		private static bool TryGetNotNullFilter(UnaryQueryExpression unary, out int column)
		{
			if (unary.Operator == UnaryOperator2.Not && unary.Expression.Kind == QueryExpressionKind.Binary)
			{
				return CubeContextCubeValue.TryGetNullFilter((BinaryQueryExpression)unary.Expression, BinaryOperator2.Equals, out column);
			}
			column = -1;
			return false;
		}

		// Token: 0x060058E3 RID: 22755 RVA: 0x00138148 File Offset: 0x00136348
		private static bool TryGetNullFilter(BinaryQueryExpression binary, BinaryOperator2 op, out int column)
		{
			column = -1;
			Value value;
			return binary.Operator == op && ((binary.Left.TryGetColumnAccess(out column) && binary.Right.TryGetConstant(out value) && value.IsNull) || (binary.Right.TryGetColumnAccess(out column) && binary.Left.TryGetConstant(out value) && value.IsNull));
		}

		// Token: 0x040031E1 RID: 12769
		public static readonly Keys CubeMetadataKeys = Keys.New("Cube.Cube");

		// Token: 0x040031E2 RID: 12770
		public static readonly RecordValue CubeDimensionAttributeMetadata = RecordValue.New(Keys.New("Cube.DimensionAttribute"), new Value[] { LogicalValue.True });

		// Token: 0x040031E3 RID: 12771
		private readonly CubeContextProvider contextProvider;

		// Token: 0x040031E4 RID: 12772
		private readonly CubeContext context;

		// Token: 0x040031E5 RID: 12773
		private readonly View view;

		// Token: 0x040031E6 RID: 12774
		private Keys dimensionAttributes;

		// Token: 0x040031E7 RID: 12775
		private TypeValue type;

		// Token: 0x040031E8 RID: 12776
		private TableSortOrder sortOrder;

		// Token: 0x040031E9 RID: 12777
		private Query query;

		// Token: 0x02000CCC RID: 3276
		private class CubeContextCubeValueCubeMetadataProvider : ICubeMetadataProvider
		{
			// Token: 0x060058E7 RID: 22759 RVA: 0x001381F4 File Offset: 0x001363F4
			public CubeContextCubeValueCubeMetadataProvider(CubeContextCubeValue cube)
			{
				this.cube = cube;
			}

			// Token: 0x060058E8 RID: 22760 RVA: 0x00138204 File Offset: 0x00136404
			public IdentifierCubeExpression GetIdentifier(int columnIndex)
			{
				if (this.cube.GetDimensionAttribute(columnIndex) != -1)
				{
					return this.cube.context.CubeExpression.DimensionAttributes[this.cube.GetDimensionAttribute(columnIndex)];
				}
				if (this.cube.GetProperty(columnIndex) != -1)
				{
					return this.cube.context.CubeExpression.Properties[this.cube.GetProperty(columnIndex)];
				}
				if (this.cube.GetMeasure(columnIndex) != -1)
				{
					return this.cube.context.CubeExpression.Measures[this.cube.GetMeasure(columnIndex)];
				}
				return this.cube.context.CubeExpression.MeasureProperties[this.cube.GetMeasureProperty(columnIndex)];
			}

			// Token: 0x060058E9 RID: 22761 RVA: 0x001382D9 File Offset: 0x001364D9
			public bool IsDimensionAttribute(IdentifierCubeExpression identifier)
			{
				return this.cube.IsDimensionAttribute(identifier);
			}

			// Token: 0x060058EA RID: 22762 RVA: 0x001382E7 File Offset: 0x001364E7
			public bool IsProperty(IdentifierCubeExpression identifier)
			{
				return this.cube.IsProperty(identifier);
			}

			// Token: 0x060058EB RID: 22763 RVA: 0x001382F5 File Offset: 0x001364F5
			public bool IsMeasure(IdentifierCubeExpression identifier)
			{
				return this.cube.IsMeasure(identifier);
			}

			// Token: 0x060058EC RID: 22764 RVA: 0x00138303 File Offset: 0x00136503
			public IdentifierCubeExpression GetProperty(IdentifierCubeExpression attribute, CubePropertyKind kind, string name)
			{
				return this.cube.contextProvider.GetProperty(attribute, kind, name);
			}

			// Token: 0x060058ED RID: 22765 RVA: 0x00138318 File Offset: 0x00136518
			public bool TryGetPropertyKey(IdentifierCubeExpression property, out IdentifierCubeExpression key)
			{
				return this.cube.contextProvider.TryGetPropertyKey(property, out key);
			}

			// Token: 0x060058EE RID: 22766 RVA: 0x0013832C File Offset: 0x0013652C
			public IdentifierCubeExpression GetMeasureProperty(IdentifierCubeExpression measure, string propertyName)
			{
				return this.cube.contextProvider.GetMeasureProperty(measure, propertyName);
			}

			// Token: 0x040031EA RID: 12778
			private readonly CubeContextCubeValue cube;
		}

		// Token: 0x02000CCD RID: 3277
		private class SkipTakeCubeValue : DelegatingCubeValue
		{
			// Token: 0x060058EF RID: 22767 RVA: 0x00138340 File Offset: 0x00136540
			public SkipTakeCubeValue(CubeValue cube)
				: base(cube)
			{
			}

			// Token: 0x060058F0 RID: 22768 RVA: 0x00138349 File Offset: 0x00136549
			public override bool TrySelectColumns(ColumnSelection columnSelection, out TableValue table)
			{
				if (base.TrySelectColumns(columnSelection, out table))
				{
					table = CubeContextCubeValue.SkipTakeCubeValue.RemoveCubeMetadata(table);
					return true;
				}
				table = null;
				return false;
			}

			// Token: 0x060058F1 RID: 22769 RVA: 0x00138364 File Offset: 0x00136564
			public override TableValue SelectRows(FunctionValue condition)
			{
				return CubeContextCubeValue.SkipTakeCubeValue.RemoveCubeMetadata(base.SelectRows(condition));
			}

			// Token: 0x060058F2 RID: 22770 RVA: 0x00138372 File Offset: 0x00136572
			public override TableValue AddColumns(ColumnsConstructor columnsCtor)
			{
				return CubeContextCubeValue.SkipTakeCubeValue.RemoveCubeMetadata(base.AddColumns(columnsCtor));
			}

			// Token: 0x060058F3 RID: 22771 RVA: 0x00138380 File Offset: 0x00136580
			public override TableValue TransformColumns(ColumnTransforms columnTransforms)
			{
				return CubeContextCubeValue.SkipTakeCubeValue.RemoveCubeMetadata(base.TransformColumns(columnTransforms));
			}

			// Token: 0x060058F4 RID: 22772 RVA: 0x0013838E File Offset: 0x0013658E
			public override TableValue Group(Grouping grouping)
			{
				return CubeContextCubeValue.SkipTakeCubeValue.RemoveCubeMetadata(base.Group(grouping));
			}

			// Token: 0x060058F5 RID: 22773 RVA: 0x0013839C File Offset: 0x0013659C
			public override TableValue Skip(RowCount count)
			{
				return CubeContextCubeValue.SkipTakeCubeValue.RemoveCubeMetadata(base.Skip(count));
			}

			// Token: 0x060058F6 RID: 22774 RVA: 0x001383AA File Offset: 0x001365AA
			public override TableValue Take(RowCount count)
			{
				return CubeContextCubeValue.SkipTakeCubeValue.RemoveCubeMetadata(base.Take(count));
			}

			// Token: 0x060058F7 RID: 22775 RVA: 0x001383B8 File Offset: 0x001365B8
			public override TableValue Sort(TableSortOrder sortOrder)
			{
				return CubeContextCubeValue.SkipTakeCubeValue.RemoveCubeMetadata(base.Sort(sortOrder));
			}

			// Token: 0x060058F8 RID: 22776 RVA: 0x001383C6 File Offset: 0x001365C6
			public override TableValue Unordered()
			{
				return CubeContextCubeValue.SkipTakeCubeValue.RemoveCubeMetadata(base.Unordered());
			}

			// Token: 0x060058F9 RID: 22777 RVA: 0x001383D3 File Offset: 0x001365D3
			public override TableValue Distinct(TableDistinct distinctCriteria)
			{
				return CubeContextCubeValue.SkipTakeCubeValue.RemoveCubeMetadata(base.Distinct(distinctCriteria));
			}

			// Token: 0x060058FA RID: 22778 RVA: 0x001383E1 File Offset: 0x001365E1
			public override TableValue NestedJoin(int[] leftKeyColumns, Value rightTable, Keys rightKey, TableTypeAlgebra.JoinKind joinKind, string newColumn, Keys joinKeys, FunctionValue[] keyEqualityComparers)
			{
				return CubeContextCubeValue.SkipTakeCubeValue.RemoveCubeMetadata(base.NestedJoin(leftKeyColumns, rightTable, rightKey, joinKind, newColumn, joinKeys, keyEqualityComparers));
			}

			// Token: 0x060058FB RID: 22779 RVA: 0x001383F9 File Offset: 0x001365F9
			public override TableValue ExpandListColumn(int columnIndex, bool singleOrDefault)
			{
				return CubeContextCubeValue.SkipTakeCubeValue.RemoveCubeMetadata(base.ExpandListColumn(columnIndex, singleOrDefault));
			}

			// Token: 0x060058FC RID: 22780 RVA: 0x00138408 File Offset: 0x00136608
			public override TableValue ExpandRecordColumn(int columnToExpand, Keys fieldsToProject, Keys newColumns)
			{
				return CubeContextCubeValue.SkipTakeCubeValue.RemoveCubeMetadata(base.ExpandRecordColumn(columnToExpand, fieldsToProject, newColumns));
			}

			// Token: 0x060058FD RID: 22781 RVA: 0x00138418 File Offset: 0x00136618
			private static TableValue RemoveCubeMetadata(TableValue cube)
			{
				RecordValue asRecord = Library.Record.RemoveFields.Invoke(cube.MetaValue, CubeContextCubeValue.SkipTakeCubeValue.CubeCubeKey, Library.MissingField.Ignore).AsRecord;
				return cube.NewMeta(asRecord).AsTable;
			}

			// Token: 0x040031EB RID: 12779
			private static readonly TextValue CubeCubeKey = TextValue.New("Cube.Cube");
		}

		// Token: 0x02000CCE RID: 3278
		private sealed class CubeContextQuery : DataSourceQuery
		{
			// Token: 0x060058FF RID: 22783 RVA: 0x00138462 File Offset: 0x00136662
			public CubeContextQuery(Keys columns, TypeValue[] types, CubeContext context)
			{
				this.columns = columns;
				this.types = types;
				this.context = context;
			}

			// Token: 0x17001AA6 RID: 6822
			// (get) Token: 0x06005900 RID: 22784 RVA: 0x0013847F File Offset: 0x0013667F
			public CubeContext Context
			{
				get
				{
					return this.context;
				}
			}

			// Token: 0x17001AA7 RID: 6823
			// (get) Token: 0x06005901 RID: 22785 RVA: 0x00138487 File Offset: 0x00136687
			public override Keys Columns
			{
				get
				{
					return this.columns;
				}
			}

			// Token: 0x17001AA8 RID: 6824
			// (get) Token: 0x06005902 RID: 22786 RVA: 0x0013848F File Offset: 0x0013668F
			public override IQueryDomain QueryDomain
			{
				get
				{
					if (this.queryDomain == null)
					{
						this.queryDomain = new CubeContextCubeValue.CubeContextQuery.CubeContextQueryDomain(this.context);
					}
					return this.queryDomain;
				}
			}

			// Token: 0x17001AA9 RID: 6825
			// (get) Token: 0x06005903 RID: 22787 RVA: 0x001384B0 File Offset: 0x001366B0
			public override IEngineHost EngineHost
			{
				get
				{
					return this.context.EngineHost;
				}
			}

			// Token: 0x06005904 RID: 22788 RVA: 0x001384BD File Offset: 0x001366BD
			public override TypeValue GetColumnType(int column)
			{
				return this.types[column];
			}

			// Token: 0x06005905 RID: 22789 RVA: 0x001384C7 File Offset: 0x001366C7
			public override IEnumerable<IValueReference> GetRows()
			{
				this.context.ReportFoldingFailure();
				return DeferredEnumerable.From<IValueReference>(new Func<IEnumerator<IValueReference>>(this.context.Evaluate));
			}

			// Token: 0x06005906 RID: 22790 RVA: 0x001384EB File Offset: 0x001366EB
			public override bool TryGetReader(out IPageReader reader)
			{
				return this.context.TryGetReader(out reader);
			}

			// Token: 0x040031EC RID: 12780
			private readonly Keys columns;

			// Token: 0x040031ED RID: 12781
			private readonly TypeValue[] types;

			// Token: 0x040031EE RID: 12782
			private readonly CubeContext context;

			// Token: 0x040031EF RID: 12783
			private IQueryDomain queryDomain;

			// Token: 0x02000CCF RID: 3279
			public sealed class CubeContextQueryDomain : IQueryDomain, INativeQueryDomain
			{
				// Token: 0x06005907 RID: 22791 RVA: 0x001384F9 File Offset: 0x001366F9
				public CubeContextQueryDomain(CubeContext context)
				{
					this.context = context;
				}

				// Token: 0x17001AAA RID: 6826
				// (get) Token: 0x06005908 RID: 22792 RVA: 0x00002105 File Offset: 0x00000305
				public bool CanIndex
				{
					get
					{
						return false;
					}
				}

				// Token: 0x06005909 RID: 22793 RVA: 0x000952C1 File Offset: 0x000934C1
				public bool IsCompatibleWith(IQueryDomain domain)
				{
					return domain == this;
				}

				// Token: 0x0600590A RID: 22794 RVA: 0x0000A6A5 File Offset: 0x000088A5
				public Query Optimize(Query query)
				{
					return query;
				}

				// Token: 0x0600590B RID: 22795 RVA: 0x00138508 File Offset: 0x00136708
				public bool TryGetNativeQuery(Query query, out IResource resource, out Value nativeQuery, out RecordValue options)
				{
					nativeQuery = null;
					string text;
					if (this.context.TryGetNativeQuery(out text) && !string.IsNullOrEmpty(text))
					{
						nativeQuery = TextValue.New(text);
					}
					resource = this.context.ContextProvider.Resource;
					options = RecordValue.Empty;
					return true;
				}

				// Token: 0x040031F0 RID: 12784
				private readonly CubeContext context;
			}
		}

		// Token: 0x02000CD0 RID: 3280
		public sealed class TabularCubeQuery : DataSourceQuery
		{
			// Token: 0x0600590C RID: 22796 RVA: 0x00138552 File Offset: 0x00136752
			public TabularCubeQuery(CubeContextCubeValue cube, Query query)
			{
				this.cube = cube;
				this.query = query;
			}

			// Token: 0x17001AAB RID: 6827
			// (get) Token: 0x0600590D RID: 22797 RVA: 0x00138568 File Offset: 0x00136768
			public CubeContextCubeValue Cube
			{
				get
				{
					return this.cube;
				}
			}

			// Token: 0x17001AAC RID: 6828
			// (get) Token: 0x0600590E RID: 22798 RVA: 0x00138570 File Offset: 0x00136770
			public override Keys Columns
			{
				get
				{
					return this.query.Columns;
				}
			}

			// Token: 0x17001AAD RID: 6829
			// (get) Token: 0x0600590F RID: 22799 RVA: 0x0013857D File Offset: 0x0013677D
			public override IList<TableKey> TableKeys
			{
				get
				{
					return this.query.TableKeys;
				}
			}

			// Token: 0x17001AAE RID: 6830
			// (get) Token: 0x06005910 RID: 22800 RVA: 0x0013858A File Offset: 0x0013678A
			public override IList<ComputedColumn> ComputedColumns
			{
				get
				{
					return this.query.ComputedColumns;
				}
			}

			// Token: 0x17001AAF RID: 6831
			// (get) Token: 0x06005911 RID: 22801 RVA: 0x00138597 File Offset: 0x00136797
			public override TableSortOrder SortOrder
			{
				get
				{
					return this.query.SortOrder;
				}
			}

			// Token: 0x17001AB0 RID: 6832
			// (get) Token: 0x06005912 RID: 22802 RVA: 0x001385A4 File Offset: 0x001367A4
			public override RowCount RowCount
			{
				get
				{
					return this.query.RowCount;
				}
			}

			// Token: 0x17001AB1 RID: 6833
			// (get) Token: 0x06005913 RID: 22803 RVA: 0x001385B1 File Offset: 0x001367B1
			public override IQueryDomain QueryDomain
			{
				get
				{
					return CubeContextCubeValue.TabularCubeQuery.TabularCubeQueryDomain.Instance;
				}
			}

			// Token: 0x17001AB2 RID: 6834
			// (get) Token: 0x06005914 RID: 22804 RVA: 0x001385B8 File Offset: 0x001367B8
			public override IEngineHost EngineHost
			{
				get
				{
					return this.Cube.EngineHost;
				}
			}

			// Token: 0x06005915 RID: 22805 RVA: 0x001385C5 File Offset: 0x001367C5
			public override TypeValue GetColumnType(int column)
			{
				return this.query.GetColumnType(column);
			}

			// Token: 0x06005916 RID: 22806 RVA: 0x001385D3 File Offset: 0x001367D3
			public override IEnumerable<IValueReference> GetRows()
			{
				return this.query.GetRows();
			}

			// Token: 0x06005917 RID: 22807 RVA: 0x001385E0 File Offset: 0x001367E0
			public override bool TryGetReader(out IPageReader reader)
			{
				return this.query.TryGetReader(out reader);
			}

			// Token: 0x06005918 RID: 22808 RVA: 0x001385F0 File Offset: 0x001367F0
			public override Query SelectColumns(ColumnSelection selection)
			{
				CubeValue cubeValue;
				if (this.cube.TryProjectColumns(selection, out cubeValue))
				{
					return cubeValue.Query;
				}
				return this.query.SelectColumns(selection);
			}

			// Token: 0x06005919 RID: 22809 RVA: 0x00138620 File Offset: 0x00136820
			public override Query RenameReorderColumns(ColumnSelection selection)
			{
				CubeValue cubeValue;
				if (this.cube.TryProjectColumns(selection, out cubeValue))
				{
					return cubeValue.Query;
				}
				return this.query.RenameReorderColumns(selection);
			}

			// Token: 0x0600591A RID: 22810 RVA: 0x00138650 File Offset: 0x00136850
			public override Query SelectRows(FunctionValue condition)
			{
				CubeValue cubeValue;
				if (this.cube.TrySelectRows(condition, out cubeValue))
				{
					return cubeValue.Query;
				}
				return this.query.SelectRows(condition);
			}

			// Token: 0x0600591B RID: 22811 RVA: 0x00138680 File Offset: 0x00136880
			public override Query Take(RowCount count)
			{
				CubeValue cubeValue;
				if (this.cube.TryTake(count, out cubeValue))
				{
					return cubeValue.Query;
				}
				return this.query.Take(count);
			}

			// Token: 0x0600591C RID: 22812 RVA: 0x001386B0 File Offset: 0x001368B0
			public override Query Skip(RowCount count)
			{
				CubeValue cubeValue;
				if (this.cube.TrySkip(count, out cubeValue))
				{
					return cubeValue.Query;
				}
				return this.query.Skip(count);
			}

			// Token: 0x0600591D RID: 22813 RVA: 0x001386E0 File Offset: 0x001368E0
			public override Query Sort(TableSortOrder sortOrder)
			{
				CubeValue cubeValue;
				if (this.cube.TrySort(sortOrder, out cubeValue))
				{
					return cubeValue.Query;
				}
				return this.query.Sort(sortOrder);
			}

			// Token: 0x0600591E RID: 22814 RVA: 0x00138710 File Offset: 0x00136910
			public override Query Unordered()
			{
				CubeValue cubeValue;
				if (this.cube.TryUnordered(out cubeValue))
				{
					return cubeValue.Query;
				}
				return this.query.Unordered();
			}

			// Token: 0x0600591F RID: 22815 RVA: 0x00138740 File Offset: 0x00136940
			public override bool TryJoinAsLeft(RowCount take, int[] leftKeyColumns, Query rightQuery, int[] rightKeyColumns, TableTypeAlgebra.JoinKind joinKind, Keys joinKeys, JoinColumn[] joinColumns, JoinAlgorithm joinAlgorithm, FunctionValue[] keyEqualityComparers, out Query query)
			{
				if (leftKeyColumns.Length == 0 && rightKeyColumns.Length == 0 && joinKind == TableTypeAlgebra.JoinKind.Inner)
				{
					CubeContextCubeValue.TabularCubeQuery tabularCubeQuery = rightQuery as CubeContextCubeValue.TabularCubeQuery;
					if (tabularCubeQuery != null)
					{
						query = CubeContextCubeValue.TabularCubeQuery.Join(take, this, leftKeyColumns, tabularCubeQuery, rightKeyColumns, joinKind, joinKeys, joinColumns, joinAlgorithm, keyEqualityComparers);
						return true;
					}
					if (rightQuery.TryJoinAsRight(take, this, leftKeyColumns, rightKeyColumns, joinKind, joinKeys, joinColumns, joinAlgorithm, keyEqualityComparers, out query))
					{
						return true;
					}
				}
				return this.query.TryJoinAsLeft(take, leftKeyColumns, rightQuery, rightKeyColumns, joinKind, joinKeys, joinColumns, joinAlgorithm, keyEqualityComparers, out query);
			}

			// Token: 0x06005920 RID: 22816 RVA: 0x001387B8 File Offset: 0x001369B8
			public override bool TryJoinAsRight(RowCount take, Query leftQuery, int[] leftKeyColumns, int[] rightKeyColumns, TableTypeAlgebra.JoinKind joinKind, Keys joinKeys, JoinColumn[] joinColumns, JoinAlgorithm joinAlgorithm, FunctionValue[] keyEqualityComparers, out Query query)
			{
				if (leftKeyColumns.Length == 0 && rightKeyColumns.Length == 0 && joinKind == TableTypeAlgebra.JoinKind.Inner)
				{
					CubeContextCubeValue.TabularCubeQuery tabularCubeQuery = leftQuery as CubeContextCubeValue.TabularCubeQuery;
					if (tabularCubeQuery != null)
					{
						query = CubeContextCubeValue.TabularCubeQuery.Join(take, tabularCubeQuery, leftKeyColumns, this, rightKeyColumns, joinKind, joinKeys, joinColumns, joinAlgorithm, keyEqualityComparers);
						return true;
					}
					if (leftQuery.TryJoinAsLeft(take, leftKeyColumns, this, rightKeyColumns, joinKind, joinKeys, joinColumns, joinAlgorithm, keyEqualityComparers, out query))
					{
						return true;
					}
				}
				return this.query.TryJoinAsRight(take, leftQuery, leftKeyColumns, rightKeyColumns, joinKind, joinKeys, joinColumns, joinAlgorithm, keyEqualityComparers, out query);
			}

			// Token: 0x06005921 RID: 22817 RVA: 0x00138830 File Offset: 0x00136A30
			public override Query Group(Grouping grouping)
			{
				int num = 0;
				for (int i = 0; i < grouping.Constructors.Length; i++)
				{
					if (this.IsRowCount(grouping.Constructors[i].Function))
					{
						num++;
					}
				}
				if (grouping.Constructors.Length - num != 0)
				{
					return new CubeContextCubeValue.TabularCubeQuery.WithGroupQuery(grouping, this);
				}
				Query query;
				if (this.TryGroup(grouping, out query))
				{
					return query;
				}
				return base.Group(grouping);
			}

			// Token: 0x06005922 RID: 22818 RVA: 0x00138894 File Offset: 0x00136A94
			private bool TryGroup(Grouping grouping, out Query query)
			{
				HashSet<IdentifierCubeExpression> newDimensionality = new HashSet<IdentifierCubeExpression>();
				for (int i = 0; i < grouping.KeyColumns.Length; i++)
				{
					int property = this.cube.GetProperty(grouping.KeyColumns[i]);
					if (property != -1)
					{
						IdentifierCubeExpression identifierCubeExpression = this.cube.context.CubeExpression.Properties[property];
						if (this.cube.contextProvider.GetPropertyKind(identifierCubeExpression) == CubePropertyKind.UniqueId)
						{
							newDimensionality.Add(this.cube.contextProvider.GetPropertyDimensionAttribute(identifierCubeExpression));
						}
					}
				}
				for (int j = 0; j < grouping.KeyColumns.Length; j++)
				{
					IdentifierCubeExpression identifierCubeExpression2 = null;
					int num = grouping.KeyColumns[j];
					int dimensionAttribute = this.cube.GetDimensionAttribute(num);
					if (dimensionAttribute != -1)
					{
						identifierCubeExpression2 = this.cube.context.CubeExpression.DimensionAttributes[dimensionAttribute];
						if (this.cube.contextProvider.IsDimensionAttributeUniqueId(identifierCubeExpression2))
						{
							newDimensionality.Add(identifierCubeExpression2);
						}
					}
					else
					{
						int property2 = this.cube.GetProperty(num);
						if (property2 != -1)
						{
							IdentifierCubeExpression identifierCubeExpression3 = this.cube.context.CubeExpression.Properties[property2];
							identifierCubeExpression2 = this.cube.contextProvider.GetPropertyDimensionAttribute(identifierCubeExpression3);
						}
					}
					if (identifierCubeExpression2 == null || !newDimensionality.Contains(identifierCubeExpression2))
					{
						newDimensionality = null;
						break;
					}
				}
				if (newDimensionality != null)
				{
					CubeContextCubeValue cubeContextCubeValue = this.cube.ExposeDimensionAttributes(this.cube.context.CubeExpression.DimensionAttributes.Where((IdentifierCubeExpression d) => !newDimensionality.Contains(d)));
					HashSet<string> hashSet = new HashSet<string>(grouping.KeyKeys);
					HashSet<string> hashSet2 = new HashSet<string>(cubeContextCubeValue.DimensionAttributes);
					HashSet<string> hashSet3 = new HashSet<string>();
					ColumnSelectionBuilder columnSelectionBuilder = default(ColumnSelectionBuilder);
					for (int k = 0; k < cubeContextCubeValue.Columns.Length; k++)
					{
						string text = cubeContextCubeValue.Columns[k];
						if (hashSet2.Contains(text) || hashSet.Contains(text))
						{
							columnSelectionBuilder.Add(text, k);
						}
						int dimensionAttribute2 = cubeContextCubeValue.GetDimensionAttribute(k);
						if (dimensionAttribute2 != -1 && newDimensionality.Contains(cubeContextCubeValue.context.CubeExpression.DimensionAttributes[dimensionAttribute2]))
						{
							hashSet3.Add(text);
						}
					}
					TableValue tableValue = cubeContextCubeValue.SelectColumns(columnSelectionBuilder.ToColumnSelection());
					CubeValue cubeValue = (tableValue.IsCube ? tableValue.AsCube : null);
					FunctionValue[] array = new FunctionValue[grouping.Constructors.Length];
					int num2 = 0;
					while (cubeValue != null && num2 < array.Length)
					{
						ColumnConstructor columnConstructor = grouping.Constructors[num2];
						FunctionValue functionValue;
						if (!CubeContextCubeValue.TabularCubeQuery.TryGetMeasureApplication(cubeValue, columnConstructor.Function, out functionValue))
						{
							cubeValue = null;
							break;
						}
						array[num2] = functionValue;
						num2++;
					}
					if (cubeValue != null)
					{
						ArrayBuilder<int> arrayBuilder = default(ArrayBuilder<int>);
						for (int l = 0; l < cubeValue.Columns.Length; l++)
						{
							string text2 = cubeValue.Columns[l];
							if (hashSet2.Contains(text2) && !hashSet3.Contains(text2) && !hashSet.Contains(text2))
							{
								arrayBuilder.Add(l);
							}
						}
						try
						{
							cubeValue = cubeValue.CollapseDimensionAttributes(arrayBuilder.ToArray());
						}
						catch (ValueException)
						{
							cubeValue = null;
						}
					}
					int num3 = 0;
					while (cubeValue != null && num3 < grouping.Constructors.Length)
					{
						ColumnConstructor columnConstructor2 = grouping.Constructors[num3];
						FunctionValue functionValue2 = array[num3];
						if (functionValue2 != null)
						{
							try
							{
								cubeValue = cubeValue.AddMeasureColumn(columnConstructor2.Name, functionValue2);
								goto IL_0389;
							}
							catch (ValueException)
							{
								cubeValue = null;
								goto IL_0389;
							}
							goto IL_0386;
						}
						goto IL_0386;
						IL_0389:
						num3++;
						continue;
						IL_0386:
						cubeValue = null;
						goto IL_0389;
					}
					if (cubeValue != null)
					{
						query = cubeValue.Query;
						ColumnSelection columnSelection = new ColumnSelection(grouping.ResultKeys, TableValue.GetColumns(query.Columns, grouping.ResultKeys));
						query = query.RenameReorderColumns(columnSelection);
						return true;
					}
				}
				query = null;
				return false;
			}

			// Token: 0x06005923 RID: 22819 RVA: 0x00138C9C File Offset: 0x00136E9C
			public override bool TryInvokeAsArgument(FunctionValue function, Value[] arguments, int index, out Value result)
			{
				return this.cube.TryInvokeAsArgument(function, arguments, index, out result);
			}

			// Token: 0x06005924 RID: 22820 RVA: 0x00138CB0 File Offset: 0x00136EB0
			public override Query AddColumns(ColumnsConstructor columnsCtor)
			{
				CubeValue cubeValue;
				if (this.cube.TryAddColumns(columnsCtor, out cubeValue))
				{
					return cubeValue.Query;
				}
				return this.query.AddColumns(columnsCtor);
			}

			// Token: 0x06005925 RID: 22821 RVA: 0x00138CE0 File Offset: 0x00136EE0
			public override Query Distinct(TableDistinct distinctCriteria)
			{
				return this.query.Distinct(distinctCriteria);
			}

			// Token: 0x06005926 RID: 22822 RVA: 0x00138CEE File Offset: 0x00136EEE
			public override bool TryExpandListColumn(int columnIndex, bool singleOrDefault, out Query query)
			{
				return this.query.TryExpandListColumn(columnIndex, singleOrDefault, out query);
			}

			// Token: 0x06005927 RID: 22823 RVA: 0x00138CFE File Offset: 0x00136EFE
			public override bool TryExpandRecordColumn(int columnToExpand, Keys fieldsToProject, Keys newColumns, out Query query)
			{
				return this.query.TryExpandRecordColumn(columnToExpand, fieldsToProject, newColumns, out query);
			}

			// Token: 0x06005928 RID: 22824 RVA: 0x00138D10 File Offset: 0x00136F10
			public override Query NestedJoin(int[] leftKeyColumns, Value rightTable, Keys rightKey, TableTypeAlgebra.JoinKind joinKind, string newColumn, Keys joinKeys, FunctionValue[] keyEqualityComparers)
			{
				return this.query.NestedJoin(leftKeyColumns, rightTable, rightKey, joinKind, newColumn, joinKeys, keyEqualityComparers);
			}

			// Token: 0x06005929 RID: 22825 RVA: 0x00138D28 File Offset: 0x00136F28
			private bool IsRowCount(FunctionValue function)
			{
				FunctionValue functionValue;
				return CubeContextCubeValue.TabularCubeQuery.TryGetMeasureApplication(this.cube, function, out functionValue) && functionValue.FunctionIdentity.Equals(MeasureValue.Count.FunctionIdentity);
			}

			// Token: 0x0600592A RID: 22826 RVA: 0x00138D5C File Offset: 0x00136F5C
			public static bool IsMeasureApplication(FunctionValue function)
			{
				FunctionValue functionValue;
				return CubeContextCubeValue.TabularCubeQuery.TryGetMeasureApplication(null, function, out functionValue);
			}

			// Token: 0x0600592B RID: 22827 RVA: 0x00138D74 File Offset: 0x00136F74
			private static bool TryGetMeasureApplication(CubeValue cube, FunctionValue function, out FunctionValue measure)
			{
				Value value = function;
				IFunctionExpression functionExpression = function.Expression as IFunctionExpression;
				if (functionExpression != null)
				{
					functionExpression = ConstantFoldingVisitor.Fold(functionExpression) as IFunctionExpression;
				}
				if (functionExpression != null)
				{
					Dictionary<string, IExpression> dictionary;
					Value value2;
					if (CubeContextCubeValue.TabularCubeQuery.measureByIdPattern.TryMatch(functionExpression, out dictionary) && dictionary.TryGetConstant("measureIdRecord", out value2) && value2.IsRecord)
					{
						if (cube == null)
						{
							value = MeasureValue.Count;
						}
						else if (cube.Measures.TryGetValue(value2, out value))
						{
							value = value["Data"];
						}
					}
					else if ((!CubeContextCubeValue.TabularCubeQuery.measureByObjectPattern.TryMatch(functionExpression, out dictionary) || !dictionary.TryGetConstant("measure", out value)) && cube != null)
					{
						CubeContextCubeValue cubeContextCubeValue = cube.SubtractMetaValue as CubeContextCubeValue;
						if (cubeContextCubeValue != null)
						{
							QueryExpression queryExpression = QueryExpressionBuilder.ToQueryExpression(cube.Type, function);
							CubeExpression cubeExpression;
							IdentifierCubeExpression identifierCubeExpression;
							if (cubeContextCubeValue.TryToCubeExpression(queryExpression, out cubeExpression) && cubeContextCubeValue.contextProvider.TryGetDynamicMeasure(cubeExpression, out identifierCubeExpression))
							{
								TypeValue type = cubeContextCubeValue.contextProvider.GetType(identifierCubeExpression);
								value = new MeasureValue(identifierCubeExpression, type);
							}
						}
					}
				}
				if (value.IsFunction)
				{
					measure = value.AsFunction.SubtractMetaValue as MeasureValue;
					if (measure == null && value.AsFunction.FunctionIdentity.Equals(TableModule.Table.RowCount))
					{
						measure = MeasureValue.Count;
					}
					return measure != null;
				}
				measure = null;
				return false;
			}

			// Token: 0x0600592C RID: 22828 RVA: 0x00138EB8 File Offset: 0x001370B8
			private static bool IsIdentifierReference(IExpression expression, Identifier identifier)
			{
				IIdentifierExpression identifierExpression = expression as IIdentifierExpression;
				return identifierExpression != null && identifierExpression.Name.Equals(identifier);
			}

			// Token: 0x0600592D RID: 22829 RVA: 0x00138EE0 File Offset: 0x001370E0
			private static Query Join(RowCount take, CubeContextCubeValue.TabularCubeQuery leftQuery, int[] leftKeyColumns, CubeContextCubeValue.TabularCubeQuery rightQuery, int[] rightKeyColumns, TableTypeAlgebra.JoinKind joinKind, Keys joinKeys, JoinColumn[] joinColumns, JoinAlgorithm joinAlgorithm, FunctionValue[] keyEqualityComparers)
			{
				bool flag = true;
				int num = 0;
				while (flag && num < leftQuery.Columns.Length)
				{
					flag &= leftQuery.cube.GetDimensionAttribute(num) != -1 || leftQuery.cube.GetProperty(num) != -1;
					num++;
				}
				int num2 = 0;
				while (flag && num2 < rightQuery.Columns.Length)
				{
					flag &= rightQuery.cube.GetDimensionAttribute(num2) != -1 || rightQuery.cube.GetProperty(num2) != -1;
					num2++;
				}
				if (flag)
				{
					try
					{
						return leftQuery.cube.ExpandDimensionAttributes(rightQuery.cube).Query.Take(take);
					}
					catch (ValueException)
					{
					}
				}
				return new CubeContextCubeValue.TabularCubeQuery.WithJoinQuery(take, leftQuery, leftKeyColumns, rightQuery, rightKeyColumns, joinKind, joinKeys, joinColumns, joinAlgorithm, keyEqualityComparers);
			}

			// Token: 0x040031F1 RID: 12785
			private readonly CubeContextCubeValue cube;

			// Token: 0x040031F2 RID: 12786
			private readonly Query query;

			// Token: 0x040031F3 RID: 12787
			private static readonly ExpressionPattern measureByIdPattern = new ExpressionPattern(new string[] { "(__row) => Cube.Measures(__row){__measureIdRecord}[Data](__row)" });

			// Token: 0x040031F4 RID: 12788
			private static readonly ExpressionPattern measureByObjectPattern = new ExpressionPattern(new string[] { "(__row) => __measure(__row)" });

			// Token: 0x02000CD1 RID: 3281
			private sealed class WithJoinQuery : JoinQuery
			{
				// Token: 0x0600592F RID: 22831 RVA: 0x00138FF0 File Offset: 0x001371F0
				public WithJoinQuery(RowCount take, CubeContextCubeValue.TabularCubeQuery leftQuery, int[] leftKeyColumns, CubeContextCubeValue.TabularCubeQuery rightQuery, int[] rightKeyColumns, TableTypeAlgebra.JoinKind joinKind, Keys joinKeys, JoinColumn[] joinColumns, JoinAlgorithm joinAlgorithm, FunctionValue[] keyEqualityComparers)
					: base(take, leftQuery, leftKeyColumns, rightQuery, rightKeyColumns, joinKind, joinKeys, joinColumns, joinAlgorithm, keyEqualityComparers)
				{
				}

				// Token: 0x17001AB3 RID: 6835
				// (get) Token: 0x06005930 RID: 22832 RVA: 0x00139014 File Offset: 0x00137214
				public new CubeContextCubeValue.TabularCubeQuery LeftQuery
				{
					get
					{
						return (CubeContextCubeValue.TabularCubeQuery)base.LeftQuery;
					}
				}

				// Token: 0x17001AB4 RID: 6836
				// (get) Token: 0x06005931 RID: 22833 RVA: 0x00139021 File Offset: 0x00137221
				public new CubeContextCubeValue.TabularCubeQuery RightQuery
				{
					get
					{
						return (CubeContextCubeValue.TabularCubeQuery)base.RightQuery;
					}
				}

				// Token: 0x06005932 RID: 22834 RVA: 0x00139030 File Offset: 0x00137230
				public override Query SelectColumns(ColumnSelection selection)
				{
					Query query = base.SelectColumns(selection);
					JoinQuery joinQuery = query as JoinQuery;
					if (joinQuery != null)
					{
						CubeContextCubeValue.TabularCubeQuery tabularCubeQuery = joinQuery.LeftQuery as CubeContextCubeValue.TabularCubeQuery;
						CubeContextCubeValue.TabularCubeQuery tabularCubeQuery2 = joinQuery.RightQuery as CubeContextCubeValue.TabularCubeQuery;
						if (tabularCubeQuery != null && tabularCubeQuery2 != null)
						{
							query = CubeContextCubeValue.TabularCubeQuery.Join(joinQuery.TakeCount, tabularCubeQuery, joinQuery.LeftKeyColumns, tabularCubeQuery2, joinQuery.RightKeyColumns, joinQuery.JoinKind, joinQuery.JoinKeys, joinQuery.JoinColumns, joinQuery.JoinAlgorithm, joinQuery.KeyEqualityComparers);
						}
					}
					return query;
				}

				// Token: 0x06005933 RID: 22835 RVA: 0x001390A8 File Offset: 0x001372A8
				public override Query Group(Grouping grouping)
				{
					bool flag = true;
					int num = 0;
					while (flag && num < grouping.Constructors.Length)
					{
						ColumnConstructor columnConstructor = grouping.Constructors[num];
						FunctionValue functionValue;
						flag &= CubeContextCubeValue.TabularCubeQuery.TryGetMeasureApplication(this.LeftQuery.Cube, columnConstructor.Function, out functionValue);
						num++;
					}
					if (flag)
					{
						ColumnSelection columnSelection;
						ColumnSelection columnSelection2;
						new ColumnSelection(grouping.KeyKeys, grouping.KeyColumns).Split(this.Columns, out columnSelection, out columnSelection2);
						Query query = this.SelectColumns(columnSelection).RenameReorderColumns(columnSelection2);
						if (query.Columns.Length < this.Columns.Length)
						{
							int[] array = new int[grouping.KeyColumns.Length];
							for (int i = 0; i < array.Length; i++)
							{
								array[i] = i;
							}
							Grouping grouping2 = new Grouping(grouping.Adjacent, grouping.ResultKeys, grouping.KeyKeys, array, grouping.Constructors, grouping.CompareRecords, grouping.Comparer, new QueryTableValue(query).Type.AsTableType);
							return query.Group(grouping2);
						}
					}
					return base.Group(grouping);
				}
			}

			// Token: 0x02000CD2 RID: 3282
			private class WithGroupQuery : GroupQuery
			{
				// Token: 0x06005934 RID: 22836 RVA: 0x001391B9 File Offset: 0x001373B9
				public WithGroupQuery(Grouping grouping, CubeContextCubeValue.TabularCubeQuery query)
					: base(grouping, query, false)
				{
				}

				// Token: 0x17001AB5 RID: 6837
				// (get) Token: 0x06005935 RID: 22837 RVA: 0x001391C4 File Offset: 0x001373C4
				public new CubeContextCubeValue.TabularCubeQuery InnerQuery
				{
					get
					{
						return (CubeContextCubeValue.TabularCubeQuery)base.InnerQuery;
					}
				}

				// Token: 0x06005936 RID: 22838 RVA: 0x001391D4 File Offset: 0x001373D4
				public override Query SelectRows(FunctionValue condition)
				{
					QueryExpression queryExpression = QueryExpressionBuilder.ToQueryExpression(QueryTableValue.NewRowType(this), condition);
					Query query;
					if (CubeContextCubeValue.TryGetNotEmptyFilter(this.Columns.Length, new Func<int, bool>(this.IsMeasureColumn), queryExpression, out queryExpression) && this.InnerQuery.TryGroup(base.Grouping, out query))
					{
						condition = QueryExpressionAssembler.Assemble(this.Columns, queryExpression);
						return query.SelectRows(condition);
					}
					return base.SelectRows(condition);
				}

				// Token: 0x06005937 RID: 22839 RVA: 0x00139244 File Offset: 0x00137444
				private bool IsMeasureColumn(int column)
				{
					int num = column - base.Grouping.KeyColumns.Length;
					return num >= 0 && !this.InnerQuery.IsRowCount(base.Grouping.Constructors[num].Function);
				}
			}

			// Token: 0x02000CD3 RID: 3283
			private sealed class TabularCubeQueryDomain : IQueryDomain, INativeQueryDomain
			{
				// Token: 0x06005938 RID: 22840 RVA: 0x000020FD File Offset: 0x000002FD
				private TabularCubeQueryDomain()
				{
				}

				// Token: 0x17001AB6 RID: 6838
				// (get) Token: 0x06005939 RID: 22841 RVA: 0x00002105 File Offset: 0x00000305
				public bool CanIndex
				{
					get
					{
						return false;
					}
				}

				// Token: 0x0600593A RID: 22842 RVA: 0x00139287 File Offset: 0x00137487
				public bool IsCompatibleWith(IQueryDomain domain)
				{
					return domain is CubeContextCubeValue.TabularCubeQuery.TabularCubeQueryDomain;
				}

				// Token: 0x0600593B RID: 22843 RVA: 0x00139294 File Offset: 0x00137494
				public Query Optimize(Query query)
				{
					Query query2 = CubeContextCubeValue.TabularCubeQuery.TabularCubeQueryDomain.TabularQueryRemovalVisitor.Instance.VisitQuery(query);
					if (query2.QueryDomain is CubeContextCubeValue.CubeContextQuery.CubeContextQueryDomain)
					{
						return query;
					}
					return query2.QueryDomain.Optimize(query2);
				}

				// Token: 0x0600593C RID: 22844 RVA: 0x001392C8 File Offset: 0x001374C8
				public bool TryGetNativeQuery(Query query, out IResource resource, out Value nativeQuery, out RecordValue options)
				{
					nativeQuery = null;
					CubeContextCubeValue.CubeContextQuery cubeContextQuery;
					string text;
					if (CubeContextCubeValue.TabularCubeQuery.TabularCubeQueryDomain.CubeContextExtractorVisitor.TryExtractCubeContextQuery(query, out cubeContextQuery) && cubeContextQuery.Context.TryGetNativeQuery(out text) && !string.IsNullOrEmpty(text))
					{
						nativeQuery = TextValue.New(text);
					}
					CubeContextCubeValue.TabularCubeQuery tabularCubeQuery = query as CubeContextCubeValue.TabularCubeQuery;
					if (tabularCubeQuery != null)
					{
						resource = tabularCubeQuery.Cube.contextProvider.Resource;
						options = RecordValue.Empty;
						return true;
					}
					resource = null;
					options = null;
					return false;
				}

				// Token: 0x040031F5 RID: 12789
				public static readonly CubeContextCubeValue.TabularCubeQuery.TabularCubeQueryDomain Instance = new CubeContextCubeValue.TabularCubeQuery.TabularCubeQueryDomain();

				// Token: 0x02000CD4 RID: 3284
				private class TabularQueryRemovalVisitor : QueryVisitor
				{
					// Token: 0x0600593E RID: 22846 RVA: 0x0013933B File Offset: 0x0013753B
					protected TabularQueryRemovalVisitor()
					{
					}

					// Token: 0x0600593F RID: 22847 RVA: 0x00139344 File Offset: 0x00137544
					protected override Query VisitDataSource(DataSourceQuery query)
					{
						CubeContextCubeValue.TabularCubeQuery tabularCubeQuery = query as CubeContextCubeValue.TabularCubeQuery;
						if (tabularCubeQuery != null)
						{
							return this.VisitQuery(tabularCubeQuery.query);
						}
						return base.VisitDataSource(query);
					}

					// Token: 0x040031F6 RID: 12790
					public static readonly QueryVisitor Instance = new CubeContextCubeValue.TabularCubeQuery.TabularCubeQueryDomain.TabularQueryRemovalVisitor();
				}

				// Token: 0x02000CD5 RID: 3285
				private sealed class CubeContextExtractorVisitor : CubeContextCubeValue.TabularCubeQuery.TabularCubeQueryDomain.TabularQueryRemovalVisitor
				{
					// Token: 0x06005941 RID: 22849 RVA: 0x0013937B File Offset: 0x0013757B
					private CubeContextExtractorVisitor()
					{
					}

					// Token: 0x06005942 RID: 22850 RVA: 0x00139384 File Offset: 0x00137584
					public static bool TryExtractCubeContextQuery(Query query, out CubeContextCubeValue.CubeContextQuery cubeContextQuery)
					{
						CubeContextCubeValue.TabularCubeQuery.TabularCubeQueryDomain.CubeContextExtractorVisitor cubeContextExtractorVisitor = new CubeContextCubeValue.TabularCubeQuery.TabularCubeQueryDomain.CubeContextExtractorVisitor();
						cubeContextExtractorVisitor.VisitQuery(query);
						cubeContextQuery = cubeContextExtractorVisitor.cubeContextQuery;
						return cubeContextQuery != null;
					}

					// Token: 0x06005943 RID: 22851 RVA: 0x001393AC File Offset: 0x001375AC
					public override Query VisitQuery(Query query)
					{
						if (this.cubeContextQuery == null)
						{
							this.cubeContextQuery = query as CubeContextCubeValue.CubeContextQuery;
						}
						return base.VisitQuery(query);
					}

					// Token: 0x040031F7 RID: 12791
					private CubeContextCubeValue.CubeContextQuery cubeContextQuery;
				}
			}
		}
	}
}
