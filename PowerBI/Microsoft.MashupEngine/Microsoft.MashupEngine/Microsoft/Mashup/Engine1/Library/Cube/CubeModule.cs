using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000CFA RID: 3322
	public sealed class CubeModule : Module
	{
		// Token: 0x17001AD7 RID: 6871
		// (get) Token: 0x06005A11 RID: 23057 RVA: 0x0013AF07 File Offset: 0x00139107
		public override string Name
		{
			get
			{
				return "CubeModule";
			}
		}

		// Token: 0x17001AD8 RID: 6872
		// (get) Token: 0x06005A12 RID: 23058 RVA: 0x0013AF0E File Offset: 0x0013910E
		public override Keys ExportKeys
		{
			get
			{
				if (CubeModule.exportKeys == null)
				{
					CubeModule.exportKeys = Keys.New(14, delegate(int index)
					{
						switch (index)
						{
						case 0:
							return "Cube.DisplayFolders";
						case 1:
							return "Cube.Dimensions";
						case 2:
							return "Cube.Measures";
						case 3:
							return "Cube.ReplaceDimensions";
						case 4:
							return "Cube.Transform";
						case 5:
							return "Cube.AddMeasureColumn";
						case 6:
							return "Cube.AddAndExpandDimensionColumn";
						case 7:
							return "Cube.CollapseAndRemoveColumns";
						case 8:
							return "Cube.AttributeMemberId";
						case 9:
							return "Cube.AttributeMemberProperty";
						case 10:
							return "Cube.PropertyKey";
						case 11:
							return "Cube.MeasureProperty";
						case 12:
							return "Cube.Properties";
						case 13:
							return "Cube.MeasureProperties";
						default:
							throw new InvalidOperationException(Strings.UnreachableCodePath);
						}
					});
				}
				return CubeModule.exportKeys;
			}
		}

		// Token: 0x06005A13 RID: 23059 RVA: 0x0013AF47 File Offset: 0x00139147
		protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost hostEnvironment)
		{
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				switch (index)
				{
				case 0:
					return CubeModule.Cube.DisplayFolders;
				case 1:
					return CubeModule.Cube.Dimensions;
				case 2:
					return CubeModule.Cube.Measures;
				case 3:
					return CubeModule.Cube.ReplaceDimensions;
				case 4:
					return CubeModule.Cube.Transform;
				case 5:
					return CubeModule.Cube.AddMeasureColumn;
				case 6:
					return CubeModule.Cube.AddAndExpandDimensionColumn;
				case 7:
					return CubeModule.Cube.CollapseAndRemoveColumns;
				case 8:
					return CubeModule.Cube.AttributeMemberId;
				case 9:
					return CubeModule.Cube.AttributeMemberProperty;
				case 10:
					return CubeModule.Cube.PropertyKey;
				case 11:
					return CubeModule.Cube.MeasureProperty;
				case 12:
					return CubeModule.Cube.Properties;
				case 13:
					return CubeModule.Cube.MeasureProperties;
				default:
					throw new InvalidOperationException(Strings.UnreachableCodePath);
				}
			});
		}

		// Token: 0x0400324A RID: 12874
		private static Keys exportKeys;

		// Token: 0x02000CFB RID: 3323
		private enum Exports
		{
			// Token: 0x0400324C RID: 12876
			Cube_DisplayFolders,
			// Token: 0x0400324D RID: 12877
			Cube_Dimensions,
			// Token: 0x0400324E RID: 12878
			Cube_Measures,
			// Token: 0x0400324F RID: 12879
			Cube_ReplaceDimensions,
			// Token: 0x04003250 RID: 12880
			Cube_Transform,
			// Token: 0x04003251 RID: 12881
			Cube_AddMeasureColumn,
			// Token: 0x04003252 RID: 12882
			Cube_AddAndExpandDimensionColumn,
			// Token: 0x04003253 RID: 12883
			Cube_CollapseAndRemoveColumns,
			// Token: 0x04003254 RID: 12884
			Cube_AttributeMemberId,
			// Token: 0x04003255 RID: 12885
			Cube_AttributeMemberProperty,
			// Token: 0x04003256 RID: 12886
			Cube_PropertyKey,
			// Token: 0x04003257 RID: 12887
			Cube_MeasureProperty,
			// Token: 0x04003258 RID: 12888
			Cube_Properties,
			// Token: 0x04003259 RID: 12889
			Cube_MeasureProperties,
			// Token: 0x0400325A RID: 12890
			Count
		}

		// Token: 0x02000CFC RID: 3324
		public static class Cube
		{
			// Token: 0x0400325B RID: 12891
			public static readonly FunctionValue DisplayFolders = new CubeModule.Cube.DisplayFoldersFunctionValue();

			// Token: 0x0400325C RID: 12892
			public static readonly FunctionValue Dimensions = new CubeModule.Cube.DimensionsFunctionValue();

			// Token: 0x0400325D RID: 12893
			public static readonly FunctionValue Measures = new CubeModule.Cube.MeasuresFunctionValue();

			// Token: 0x0400325E RID: 12894
			public static readonly FunctionValue ReplaceDimensions = new CubeModule.Cube.ReplaceDimensionsFunctionValue();

			// Token: 0x0400325F RID: 12895
			public static readonly FunctionValue Transform = new CubeModule.Cube.TransformFunctionValue();

			// Token: 0x04003260 RID: 12896
			public static readonly FunctionValue AddMeasureColumn = new CubeModule.Cube.AddMeasureColumnFunctionValue();

			// Token: 0x04003261 RID: 12897
			public static readonly FunctionValue AddAndExpandDimensionColumn = new CubeModule.Cube.AddAndExpandDimensionColumnFunctionValue();

			// Token: 0x04003262 RID: 12898
			public static readonly FunctionValue CollapseAndRemoveColumns = new CubeModule.Cube.CollapseAndRemoveColumnsFunctionValue();

			// Token: 0x04003263 RID: 12899
			public static readonly FunctionValue AttributeMemberId = new CubeModule.Cube.AttributeMemberIdFunctionValue();

			// Token: 0x04003264 RID: 12900
			public static readonly FunctionValue AttributeMemberProperty = new CubeModule.Cube.AttributeMemberPropertyFunctionValue();

			// Token: 0x04003265 RID: 12901
			public static readonly FunctionValue PropertyKey = new CubeModule.Cube.PropertyKeyFunctionValue();

			// Token: 0x04003266 RID: 12902
			public static readonly FunctionValue MeasureProperty = new CubeModule.Cube.MeasurePropertyFunctionValue();

			// Token: 0x04003267 RID: 12903
			public static readonly FunctionValue Properties = new CubeModule.Cube.PropertiesFunctionValue();

			// Token: 0x04003268 RID: 12904
			public static readonly FunctionValue MeasureProperties = new CubeModule.Cube.MeasurePropertiesFunctionValue();

			// Token: 0x02000CFD RID: 3325
			private class DisplayFoldersFunctionValue : NativeFunctionValue1<TableValue, TableValue>
			{
				// Token: 0x06005A16 RID: 23062 RVA: 0x0013B00D File Offset: 0x0013920D
				public DisplayFoldersFunctionValue()
					: base(TypeValue.Table, "cube", TypeValue.Table)
				{
				}

				// Token: 0x06005A17 RID: 23063 RVA: 0x0013B024 File Offset: 0x00139224
				public override TableValue TypedInvoke(TableValue cube)
				{
					return cube.AsCube.DisplayFolders;
				}
			}

			// Token: 0x02000CFE RID: 3326
			private class DimensionsFunctionValue : NativeFunctionValue1<TableValue, TableValue>
			{
				// Token: 0x06005A18 RID: 23064 RVA: 0x0013B00D File Offset: 0x0013920D
				public DimensionsFunctionValue()
					: base(TypeValue.Table, "cube", TypeValue.Table)
				{
				}

				// Token: 0x06005A19 RID: 23065 RVA: 0x0013B031 File Offset: 0x00139231
				public override TableValue TypedInvoke(TableValue cube)
				{
					return cube.AsCube.Dimensions;
				}
			}

			// Token: 0x02000CFF RID: 3327
			private class MeasuresFunctionValue : NativeFunctionValue1<TableValue, Value>
			{
				// Token: 0x06005A1A RID: 23066 RVA: 0x0013B03E File Offset: 0x0013923E
				public MeasuresFunctionValue()
					: base(TypeValue.Table, "cube", TypeValue.Any)
				{
				}

				// Token: 0x06005A1B RID: 23067 RVA: 0x0013B055 File Offset: 0x00139255
				public override TableValue TypedInvoke(Value cube)
				{
					if (cube.IsTable)
					{
						return cube.AsTable.AsCube.Measures;
					}
					throw ValueException.NewExpressionError<Message0>(Strings.Cube_QueryNotSupported, null, null);
				}
			}

			// Token: 0x02000D00 RID: 3328
			private sealed class ReplaceDimensionsFunctionValue : NativeFunctionValue2<TableValue, TableValue, Value>
			{
				// Token: 0x06005A1C RID: 23068 RVA: 0x0013B07C File Offset: 0x0013927C
				public ReplaceDimensionsFunctionValue()
					: base(TypeValue.Table, 2, "cube", TypeValue.Table, "dimensions", TypeValue.Any)
				{
				}

				// Token: 0x06005A1D RID: 23069 RVA: 0x0013B0A0 File Offset: 0x001392A0
				public override TableValue TypedInvoke(TableValue cube, Value dimensions)
				{
					FunctionValue functionValue;
					if (dimensions.Kind == ValueKind.Table)
					{
						functionValue = new CubeModule.Cube.ReplaceDimensionsFunctionValue.DimensionsFunctionValue(dimensions.AsTable);
					}
					else
					{
						functionValue = dimensions.AsFunction;
					}
					return new ReplaceDimensionsCubeValue(cube.AsCube, functionValue.AsFunction);
				}

				// Token: 0x02000D01 RID: 3329
				private sealed class DimensionsFunctionValue : NativeFunctionValue1<TableValue, TableValue>
				{
					// Token: 0x06005A1E RID: 23070 RVA: 0x0013B0DD File Offset: 0x001392DD
					public DimensionsFunctionValue(TableValue dimensions)
						: base(TypeValue.Table, 1, "cube", TypeValue.Table)
					{
						this.dimensions = dimensions;
					}

					// Token: 0x06005A1F RID: 23071 RVA: 0x0013B0FC File Offset: 0x001392FC
					public override TableValue TypedInvoke(TableValue cube)
					{
						return this.dimensions;
					}

					// Token: 0x04003269 RID: 12905
					private readonly TableValue dimensions;
				}
			}

			// Token: 0x02000D02 RID: 3330
			private class TransformFunctionValue : NativeFunctionValue2<TableValue, TableValue, ListValue>
			{
				// Token: 0x06005A20 RID: 23072 RVA: 0x0013B104 File Offset: 0x00139304
				public TransformFunctionValue()
					: base(TypeValue.Table, "cube", TypeValue.Table, "transforms", TypeValue.List)
				{
				}

				// Token: 0x06005A21 RID: 23073 RVA: 0x0013B128 File Offset: 0x00139328
				public override TableValue TypedInvoke(TableValue cube, ListValue transforms)
				{
					foreach (IValueReference valueReference in transforms)
					{
						ListValue asList = valueReference.Value.AsList;
						FunctionValue asFunction = asList.Item0.AsFunction;
						Value[] array = new Value[asList.Count];
						array[0] = cube;
						for (int i = 1; i < array.Length; i++)
						{
							array[i] = asList[i];
						}
						cube = asFunction.Invoke(array).AsTable;
					}
					return cube;
				}
			}

			// Token: 0x02000D03 RID: 3331
			private class AddMeasureColumnFunctionValue : NativeFunctionValue3<TableValue, TableValue, TextValue, Value>
			{
				// Token: 0x06005A22 RID: 23074 RVA: 0x0013B1BC File Offset: 0x001393BC
				public AddMeasureColumnFunctionValue()
					: base(TypeValue.Table, "cube", TypeValue.Table, "column", TypeValue.Text, "measureSelector", TypeValue.Any)
				{
				}

				// Token: 0x06005A23 RID: 23075 RVA: 0x0013B1E7 File Offset: 0x001393E7
				public override TableValue TypedInvoke(TableValue cube, TextValue newColumnName, Value measureSelector)
				{
					if (measureSelector.IsText)
					{
						measureSelector = CubeObjectTableBuilder.GetObjectById(CubeModule.Cube.Measures.Invoke(cube).AsTable, measureSelector.AsText);
					}
					return cube.AsCube.AddMeasureColumn(newColumnName.AsString, measureSelector.AsFunction);
				}
			}

			// Token: 0x02000D04 RID: 3332
			private class AddAndExpandDimensionColumnFunctionValue : NativeFunctionValue4<TableValue, TableValue, Value, ListValue, Value>
			{
				// Token: 0x06005A24 RID: 23076 RVA: 0x0013B228 File Offset: 0x00139428
				public AddAndExpandDimensionColumnFunctionValue()
					: base(TypeValue.Table, 3, "cube", TypeValue.Table, "dimensionSelector", TypeValue.Any, "attributeNames", ListTypeValue.Text, "newColumnNames", TypeValue.Any)
				{
				}

				// Token: 0x06005A25 RID: 23077 RVA: 0x0013B26C File Offset: 0x0013946C
				public override TableValue TypedInvoke(TableValue cube, Value dimensionSelector, ListValue attributeNames, Value newColumnNames)
				{
					if (dimensionSelector.IsText)
					{
						dimensionSelector = CubeObjectTableBuilder.GetObjectById(CubeModule.Cube.Dimensions.Invoke(cube).AsTable, dimensionSelector.AsText);
					}
					KeysBuilder keysBuilder = default(KeysBuilder);
					KeysBuilder keysBuilder2 = default(KeysBuilder);
					ListValue listValue = (newColumnNames.IsNull ? attributeNames : newColumnNames.AsList);
					if (attributeNames.Count != listValue.Count)
					{
						throw ValueException.NewExpressionError<Message0>(Strings.CubeAddAndExpandDimensionColumn_AttributeNamesAndNewColumnNamesMustHaveSameCount, newColumnNames, null);
					}
					for (int i = 0; i < attributeNames.Count; i++)
					{
						keysBuilder.Add(attributeNames[i].AsString);
						keysBuilder2.Add(listValue[i].AsString);
					}
					return cube.AsCube.ExpandDimensions(dimensionSelector.AsTable, keysBuilder.ToKeys(), keysBuilder2.ToKeys());
				}
			}

			// Token: 0x02000D05 RID: 3333
			private class CollapseAndRemoveColumnsFunctionValue : NativeFunctionValue2<TableValue, TableValue, ListValue>
			{
				// Token: 0x06005A26 RID: 23078 RVA: 0x0013B334 File Offset: 0x00139534
				public CollapseAndRemoveColumnsFunctionValue()
					: base(TypeValue.Table, "cube", TypeValue.Table, "columnNames", ListTypeValue.Text)
				{
				}

				// Token: 0x06005A27 RID: 23079 RVA: 0x0013B358 File Offset: 0x00139558
				public override TableValue TypedInvoke(TableValue cube, ListValue columnNames)
				{
					KeysBuilder keysBuilder = default(KeysBuilder);
					for (int i = 0; i < columnNames.Count; i++)
					{
						keysBuilder.Add(columnNames[i].AsString);
					}
					return cube.AsCube.CollapseDimensions(keysBuilder.ToKeys());
				}
			}

			// Token: 0x02000D06 RID: 3334
			private sealed class AttributeMemberIdFunctionValue : NativeFunctionValue1<Value, Value>
			{
				// Token: 0x06005A28 RID: 23080 RVA: 0x0013B3A3 File Offset: 0x001395A3
				public AttributeMemberIdFunctionValue()
					: base(TypeValue.Any, 1, "attribute", TypeValue.Any)
				{
				}

				// Token: 0x06005A29 RID: 23081 RVA: 0x0013B3BB File Offset: 0x001395BB
				public override Value TypedInvoke(Value value)
				{
					if (value.MetaValue.TryGetValue("Cube.AttributeMemberId", out value) && value.IsText)
					{
						return value.AsText;
					}
					return Value.Null;
				}
			}

			// Token: 0x02000D07 RID: 3335
			private sealed class AttributeMemberPropertyFunctionValue : NativeFunctionValue2<Value, Value, TextValue>
			{
				// Token: 0x06005A2A RID: 23082 RVA: 0x0013B3E5 File Offset: 0x001395E5
				public AttributeMemberPropertyFunctionValue()
					: base(TypeValue.Any, "attribute", TypeValue.Any, "propertyName", TypeValue.Text)
				{
				}

				// Token: 0x06005A2B RID: 23083 RVA: 0x0013B406 File Offset: 0x00139606
				public override Value TypedInvoke(Value value, TextValue propertyName)
				{
					throw ValueException.NewExpressionError<Message0>(Strings.Cube_QueryNotSupported, null, null);
				}
			}

			// Token: 0x02000D08 RID: 3336
			private sealed class PropertyKeyFunctionValue : NativeFunctionValue1<Value, Value>
			{
				// Token: 0x06005A2C RID: 23084 RVA: 0x0013B414 File Offset: 0x00139614
				public PropertyKeyFunctionValue()
					: base(TypeValue.Any, "property", TypeValue.Any)
				{
				}

				// Token: 0x06005A2D RID: 23085 RVA: 0x0013B406 File Offset: 0x00139606
				public override Value TypedInvoke(Value value)
				{
					throw ValueException.NewExpressionError<Message0>(Strings.Cube_QueryNotSupported, null, null);
				}
			}

			// Token: 0x02000D09 RID: 3337
			private sealed class MeasurePropertyFunctionValue : NativeFunctionValue2<Value, Value, TextValue>
			{
				// Token: 0x06005A2E RID: 23086 RVA: 0x0013B42B File Offset: 0x0013962B
				public MeasurePropertyFunctionValue()
					: base(TypeValue.Any, "measure", TypeValue.Any, "propertyName", TypeValue.Text)
				{
				}

				// Token: 0x06005A2F RID: 23087 RVA: 0x0013B406 File Offset: 0x00139606
				public override Value TypedInvoke(Value value, TextValue propertyName)
				{
					throw ValueException.NewExpressionError<Message0>(Strings.Cube_QueryNotSupported, null, null);
				}
			}

			// Token: 0x02000D0A RID: 3338
			private sealed class PropertiesFunctionValue : NativeFunctionValue1<TableValue, TableValue>
			{
				// Token: 0x06005A30 RID: 23088 RVA: 0x0013B00D File Offset: 0x0013920D
				public PropertiesFunctionValue()
					: base(TypeValue.Table, "cube", TypeValue.Table)
				{
				}

				// Token: 0x06005A31 RID: 23089 RVA: 0x0013B44C File Offset: 0x0013964C
				public override TableValue TypedInvoke(TableValue cube)
				{
					return cube.AsCube.Properties;
				}
			}

			// Token: 0x02000D0B RID: 3339
			private sealed class MeasurePropertiesFunctionValue : NativeFunctionValue1<TableValue, TableValue>
			{
				// Token: 0x06005A32 RID: 23090 RVA: 0x0013B00D File Offset: 0x0013920D
				public MeasurePropertiesFunctionValue()
					: base(TypeValue.Table, "cube", TypeValue.Table)
				{
				}

				// Token: 0x06005A33 RID: 23091 RVA: 0x0013B459 File Offset: 0x00139659
				public override TableValue TypedInvoke(TableValue cube)
				{
					return cube.AsCube.MeasureProperties;
				}
			}
		}
	}
}
