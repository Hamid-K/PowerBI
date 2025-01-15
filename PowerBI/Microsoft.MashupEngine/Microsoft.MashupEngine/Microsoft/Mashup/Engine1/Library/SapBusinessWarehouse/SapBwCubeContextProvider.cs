using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.Linq;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Capability;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Cube;
using Microsoft.Mashup.Engine1.Library.Mdx;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OleDb.Serialization;

namespace Microsoft.Mashup.Engine1.Library.SapBusinessWarehouse
{
	// Token: 0x0200049A RID: 1178
	internal class SapBwCubeContextProvider : MdxCubeContextProvider
	{
		// Token: 0x060026DD RID: 9949 RVA: 0x00071303 File Offset: 0x0006F503
		public SapBwCubeContextProvider(ISapBwService service, SapBwMdxCube cube)
			: base(cube)
		{
			this.service = service;
		}

		// Token: 0x17000F6C RID: 3948
		// (get) Token: 0x060026DE RID: 9950 RVA: 0x00071313 File Offset: 0x0006F513
		public ISapBwService Service
		{
			get
			{
				return this.service;
			}
		}

		// Token: 0x17000F6D RID: 3949
		// (get) Token: 0x060026DF RID: 9951 RVA: 0x0007131B File Offset: 0x0006F51B
		public override IResource Resource
		{
			get
			{
				return this.service.Resource;
			}
		}

		// Token: 0x17000F6E RID: 3950
		// (get) Token: 0x060026E0 RID: 9952 RVA: 0x00071328 File Offset: 0x0006F528
		public override IEngineHost EngineHost
		{
			get
			{
				return this.service.Host;
			}
		}

		// Token: 0x060026E1 RID: 9953 RVA: 0x00071338 File Offset: 0x0006F538
		protected override TableValue GetDisplayFolders()
		{
			CubeObjectTableBuilder cubeObjectTableBuilder = CubeObjectTableBuilder.NewWithoutLink();
			foreach (MdxMeasure mdxMeasure in this.Cube.Measures)
			{
				if (mdxMeasure.IsVisible)
				{
					IdentifierCubeExpression identifierCubeExpression = new IdentifierCubeExpression(mdxMeasure.MdxIdentifier);
					MeasureValue measureValue = new MeasureValue(identifierCubeExpression, TypeValue.Any);
					cubeObjectTableBuilder.AddMeasure(identifierCubeExpression.Identifier, mdxMeasure.Caption, measureValue);
				}
			}
			if (cubeObjectTableBuilder.Count > 0)
			{
				List<MdxCellPropertyMetadata> cellProperties = this.Cube.Metadata.GetCellProperties().ToList<MdxCellPropertyMetadata>();
				if (cellProperties.Any<MdxCellPropertyMetadata>())
				{
					cubeObjectTableBuilder.AddMeasurePropertiesFolder("Measure Properties", "Measure Properties", CubeObjectTableBuilder.NewLazy(delegate(CubeObjectTableBuilder populate)
					{
						foreach (MdxCellPropertyMetadata mdxCellPropertyMetadata in cellProperties)
						{
							populate.AddObject(mdxCellPropertyMetadata.Name, mdxCellPropertyMetadata.Caption, CubeObjectTableBuilder.MeasurePropertyKind, Value.Null);
						}
					}));
				}
			}
			foreach (MdxDimension mdxDimension in this.Cube.Dimensions.Values)
			{
				MdxDisplayFolderBuilder mdxDisplayFolderBuilder = new MdxDisplayFolderBuilder();
				foreach (MdxHierarchy mdxHierarchy in mdxDimension.VisibleHierarchies)
				{
					mdxDisplayFolderBuilder.CreateSubfoldersFromPaths(mdxHierarchy.DisplayFolders, CubeObjectTableBuilder.FolderKind, this.Cube.SupportsProperties).Add(mdxHierarchy);
				}
				if (mdxDisplayFolderBuilder.Count > 0)
				{
					cubeObjectTableBuilder.AddDimensionFolder(mdxDimension.MdxIdentifier, mdxDimension.Caption, mdxDisplayFolderBuilder.ToTable());
				}
			}
			TableValue tableValue = cubeObjectTableBuilder.ToTable();
			NavigationTableTypeValueBuilder navigationTableTypeValueBuilder = new NavigationTableTypeValueBuilder(tableValue.Type.AsTableType, 1);
			navigationTableTypeValueBuilder.AddPreviewIdColumnEnabledDefault(true);
			return tableValue.NewType(navigationTableTypeValueBuilder.ToTypeValue()).AsTable;
		}

		// Token: 0x060026E2 RID: 9954 RVA: 0x00071530 File Offset: 0x0006F730
		public override bool TryCreateContext(QueryCubeExpression expression, IList<ParameterArguments> parameters, out CubeContext context)
		{
			return this.TryCreateContext(expression, parameters, true, false, out context);
		}

		// Token: 0x060026E3 RID: 9955 RVA: 0x00071540 File Offset: 0x0006F740
		public bool TryCreateContext(QueryCubeExpression expression, IList<ParameterArguments> parameters, bool cacheResults, bool newConnection, out CubeContext context)
		{
			Dictionary<string, string> dictionary;
			if (new SapBwCubeExpressionMdxCompiler(this, parameters).CanCompile(expression, out dictionary))
			{
				context = new SapBwCubeContextProvider.SapBwCubeContext(this, expression, parameters, cacheResults, newConnection);
				return true;
			}
			context = null;
			return false;
		}

		// Token: 0x060026E4 RID: 9956 RVA: 0x00071574 File Offset: 0x0006F774
		public override bool TryGetPropertyKey(IdentifierCubeExpression propertyExpression, out IdentifierCubeExpression keyExpression)
		{
			MdxProperty mdxProperty = (MdxProperty)this.Cube.GetObject(propertyExpression.Identifier);
			if (mdxProperty.Key != null)
			{
				keyExpression = new IdentifierCubeExpression(mdxProperty.Key.MdxIdentifier);
				return true;
			}
			keyExpression = null;
			return false;
		}

		// Token: 0x060026E5 RID: 9957 RVA: 0x000715B8 File Offset: 0x0006F7B8
		public override bool PropertyIsKey(IdentifierCubeExpression propertyExpression)
		{
			return ((MdxProperty)this.Cube.GetObject(propertyExpression.Identifier)).IsKey();
		}

		// Token: 0x060026E6 RID: 9958 RVA: 0x000715D5 File Offset: 0x0006F7D5
		public override IdentifierCubeExpression GetMeasureProperty(IdentifierCubeExpression measureIdentifier, string propertyIdentifier)
		{
			return new IdentifierCubeExpression(string.Format(CultureInfo.InvariantCulture, "{0}.{1}", measureIdentifier.Identifier, propertyIdentifier));
		}

		// Token: 0x0400105A RID: 4186
		private readonly ISapBwService service;

		// Token: 0x0200049B RID: 1179
		protected class SapBwCubeContext : MdxCubeContext
		{
			// Token: 0x060026E7 RID: 9959 RVA: 0x000715F2 File Offset: 0x0006F7F2
			public SapBwCubeContext(SapBwCubeContextProvider contextProvider, QueryCubeExpression expression, IList<ParameterArguments> arguments, bool cacheResults, bool newConnection)
				: base(contextProvider, false, expression, arguments)
			{
				this.service = contextProvider.service;
				this.cacheResults = cacheResults;
				this.newConnection = newConnection;
			}

			// Token: 0x17000F6F RID: 3951
			// (get) Token: 0x060026E8 RID: 9960 RVA: 0x0007161C File Offset: 0x0006F81C
			public override TableValue DirectQueryCapabilities
			{
				get
				{
					if (this.capabilities == null)
					{
						List<Value> list = new List<Value>();
						list.Add(CapabilityModule.NewCapability("Core", Value.Null));
						list.Add(CapabilityModule.NewCapability("LiteralCount", NumberValue.New(1000)));
						list.Add(CapabilityModule.NewCapability("Table.RowCount", Value.Null));
						list.Add(CapabilityModule.NewCapability("Text.PositionOf", Value.Null));
						TableTypeValue asTableType = CapabilityModule.DirectQueryCapabilities.From.Type.AsFunctionType.ReturnType.AsTableType;
						this.capabilities = ListValue.New(list.ToArray()).ToTable(asTableType);
					}
					return this.capabilities;
				}
			}

			// Token: 0x17000F70 RID: 3952
			// (get) Token: 0x060026E9 RID: 9961 RVA: 0x00066554 File Offset: 0x00064754
			public override TableValue MeasureGroups
			{
				get
				{
					return TableValue.Empty;
				}
			}

			// Token: 0x17000F71 RID: 3953
			// (get) Token: 0x060026EA RID: 9962 RVA: 0x000716CA File Offset: 0x0006F8CA
			private MdxCubeContext.ColumnInfo[] ColumnInfos
			{
				get
				{
					if (this.columnInfos == null)
					{
						this.columnInfos = base.GetColumnInfos(base.CubeExpression);
					}
					return this.columnInfos;
				}
			}

			// Token: 0x060026EB RID: 9963 RVA: 0x000716EC File Offset: 0x0006F8EC
			public override TableValue GetParameters(CubeValue cube)
			{
				if (this.parameters == null)
				{
					HashSet<string> appliedParameters = new HashSet<string>(base.ParameterArguments.Select((ParameterArguments a) => a.Parameter.Identifier));
					IList<SapBwVariable> list = ((SapBwMdxCube)this.contextProvider.Cube).Variables.Where((SapBwVariable v) => !appliedParameters.Contains(v.MdxIdentifier)).ToArray<SapBwVariable>();
					this.parameters = SapBwParametersTableValue.New(this.service, (SapBwMdxCube)this.contextProvider.Cube, cube, list);
				}
				return this.parameters;
			}

			// Token: 0x060026EC RID: 9964 RVA: 0x00071794 File Offset: 0x0006F994
			protected override IDataReader GetDataReader(QueryCubeExpression expression)
			{
				return this.GetDataReader(SapBwCubeContextProvider.SapBwCubeContext.RemoveRowRange(base.CubeExpression), base.CubeExpression.RowRange, this.cacheResults);
			}

			// Token: 0x060026ED RID: 9965 RVA: 0x000717B8 File Offset: 0x0006F9B8
			protected override TextValue GetPropertyKind(MdxDimension dimension, MdxProperty property)
			{
				TextValue textValue = base.GetPropertyKind(dimension, property);
				if (property.PropertyKind == MdxPropertyKind.UserDefined)
				{
					textValue = SapBwCubeContextProvider.SapBwCubeContext.AddSapPropertyKind(textValue, property.IsNavigationProperty(dimension));
				}
				return textValue;
			}

			// Token: 0x060026EE RID: 9966 RVA: 0x000717E8 File Offset: 0x0006F9E8
			protected override IEnumerable<IValueReference> GetPropertiesData(IList<IdentifierCubeExpression> dimensionAttributes)
			{
				Dictionary<string, OleDbType> dictionary = new Dictionary<string, OleDbType>();
				foreach (IdentifierCubeExpression identifierCubeExpression in dimensionAttributes)
				{
					MdxLevel mdxLevel = this.contextProvider.Cube.GetObject(identifierCubeExpression.Identifier) as MdxLevel;
					if (mdxLevel == null)
					{
						throw ValueException.NewDataFormatError<Message1>(Strings.ValueException_InvalidDimensionAttribute(identifierCubeExpression.Identifier), TextValue.New(identifierCubeExpression.Identifier), null);
					}
					dictionary[mdxLevel.Hierarchy.Dimension.MdxIdentifier] = mdxLevel.Hierarchy.Dimension.Type;
				}
				if (dictionary.Count == this.contextProvider.Cube.Dimensions.Count)
				{
					return from property in ((SapBwMdxCubeMetadataProvider)this.contextProvider.Cube.Metadata).GetAllLevelProperties(dictionary)
						select RecordValue.New(CubePropertiesTableValue.Type.ItemType, new Value[]
						{
							TextValue.New(property.LevelUniqueName),
							TextValue.New(property.UniqueName),
							TextValue.New(property.Caption),
							this.GetPropertyKind(property),
							TextValue.New(property.DimensionUniqueName)
						});
				}
				return base.GetPropertiesData(dimensionAttributes);
			}

			// Token: 0x060026EF RID: 9967 RVA: 0x000718E4 File Offset: 0x0006FAE4
			private TextValue GetPropertyKind(MdxPropertyMetadata property)
			{
				TextValue textValue = property.PropertyKind.ToTextValue();
				if (property.PropertyKind == MdxPropertyKind.UserDefined)
				{
					textValue = SapBwCubeContextProvider.SapBwCubeContext.AddSapPropertyKind(textValue, property.IsNavigationProperty());
				}
				return textValue;
			}

			// Token: 0x060026F0 RID: 9968 RVA: 0x00071914 File Offset: 0x0006FB14
			private static TextValue AddSapPropertyKind(TextValue propertyKind, bool isNavigationProperty)
			{
				RecordValue recordValue = RecordValue.New(Keys.New("SAP.PropertyKind"), new Value[] { TextValue.New(isNavigationProperty ? "Navigation" : "Display") });
				return propertyKind.NewMeta(recordValue).AsText;
			}

			// Token: 0x060026F1 RID: 9969 RVA: 0x0007195C File Offset: 0x0006FB5C
			private IDataReaderWithTableSchema GetDataReader(QueryCubeExpression expression, RowRange rowRange, bool cacheResults)
			{
				return this.service.ExceptionHandler.InvokeWithRetry<IDataReaderWithTableSchema>(delegate
				{
					Dictionary<string, string> dictionary;
					string query = this.GetQuery(expression, out dictionary);
					IDataReaderWithTableSchema dataReaderWithTableSchema = this.service.ExecuteMdx(query, rowRange, cacheResults, this.newConnection, this.ExtractRequiredColumns(), this.contextProvider.Cube.Name);
					if (dictionary.Count > 0)
					{
						dataReaderWithTableSchema = new SapBwCubeContextProvider.SapBwCubeContext.RenameColumnsDataReader(dataReaderWithTableSchema, dictionary);
					}
					if (!this.service.MeasuresAsDbNull && expression.Measures.Count > 0)
					{
						dataReaderWithTableSchema = new SapBwCubeContextProvider.SapBwCubeContext.MeasureColumnEmptyStringToDBNullDataReader(dataReaderWithTableSchema, this.GetNumericMeasures(expression.Measures));
					}
					return dataReaderWithTableSchema;
				});
			}

			// Token: 0x060026F2 RID: 9970 RVA: 0x000719A8 File Offset: 0x0006FBA8
			private object[][] ExtractRequiredColumns()
			{
				if (!this.service.SupportsEnhancedMetadata && !this.service.SupportsColumnFolding)
				{
					return null;
				}
				SapBwMdxCubeMetadataProvider sapBwMdxCubeMetadataProvider = this.contextProvider.Cube.Metadata as SapBwMdxCubeMetadataProvider;
				object[][] array = new object[this.ColumnInfos.Length][];
				for (int i = 0; i < this.ColumnInfos.Length; i++)
				{
					string text = null;
					int? num = null;
					if (sapBwMdxCubeMetadataProvider != null && this.ColumnInfos[i].CubeObjectKind == MdxCubeObjectKind.Measure)
					{
						sapBwMdxCubeMetadataProvider.TryGetMeasureInfo(this.ColumnInfos[i].ValueColumnName, out text, out num);
					}
					array[i] = new object[]
					{
						(int)this.ColumnInfos[i].CubeObjectKind,
						(int)this.ColumnInfos[i].Type,
						this.ColumnInfos[i].ValueColumnName,
						this.ColumnInfos[i].MdxIdentifierColumnName,
						this.IsKeyDimensionProperty(this.ColumnInfos[i]),
						text,
						num
					};
				}
				return array;
			}

			// Token: 0x060026F3 RID: 9971 RVA: 0x00071AD6 File Offset: 0x0006FCD6
			private bool IsKeyDimensionProperty(MdxCubeContext.ColumnInfo info)
			{
				return info.CubeObjectKind == MdxCubeObjectKind.Property && ((MdxProperty)this.contextProvider.Cube.GetObject(info.ValueColumnName)).IsKey();
			}

			// Token: 0x060026F4 RID: 9972 RVA: 0x00071B08 File Offset: 0x0006FD08
			public override bool TryGetReader(out IPageReader reader)
			{
				IDataReader dataReader = this.GetDataReader(base.CubeExpression, RowRange.All, false);
				reader = new DataReaderPageReader(dataReader);
				Dictionary<string, int> dictionary = new Dictionary<string, int>(reader.Schema.ColumnCount);
				foreach (SchemaColumn schemaColumn in reader.Schema)
				{
					dictionary.Add(schemaColumn.Name, dictionary.Count);
				}
				IEnumerable<IdentifierCubeExpression> enumerable = base.CubeExpression.DimensionAttributes.Concat(base.CubeExpression.Properties).Concat(base.CubeExpression.Measures).Concat(base.CubeExpression.MeasureProperties);
				KeysBuilder keysBuilder = default(KeysBuilder);
				ColumnSelectionBuilder columnSelectionBuilder = default(ColumnSelectionBuilder);
				int num = 0;
				foreach (IdentifierCubeExpression identifierCubeExpression in enumerable)
				{
					string identifier = identifierCubeExpression.Identifier;
					string valueColumnName = this.ColumnInfos[num].ValueColumnName;
					int num2;
					if (dictionary.TryGetValue(valueColumnName, out num2))
					{
						columnSelectionBuilder.Add(identifier, num2);
					}
					else
					{
						columnSelectionBuilder.Add(identifier, dictionary.Count + keysBuilder.Count);
						keysBuilder.Add(identifier);
					}
					num++;
				}
				if (keysBuilder.Count > 0)
				{
					reader = new AddNullColumnsPageReader(reader, keysBuilder.ToKeys());
				}
				reader = new ProjectColumnsPageReader(reader, columnSelectionBuilder.ToColumnSelection());
				return true;
			}

			// Token: 0x060026F5 RID: 9973 RVA: 0x00071C9C File Offset: 0x0006FE9C
			public override void ReportFoldingFailure()
			{
				IFoldingFailureService foldingFailureService = this.service.Host.QueryService<IFoldingFailureService>();
				if (foldingFailureService != null && foldingFailureService.ThrowOnFoldingFailure)
				{
					throw ValueException.NewExpressionError<Message0>(Strings.FoldingFailure, null, null);
				}
			}

			// Token: 0x060026F6 RID: 9974 RVA: 0x00071CD4 File Offset: 0x0006FED4
			public override bool TryGetNativeQuery(out string nativeQuery)
			{
				Dictionary<string, string> dictionary;
				nativeQuery = this.GetQuery(base.CubeExpression, out dictionary);
				return true;
			}

			// Token: 0x060026F7 RID: 9975 RVA: 0x00071CF2 File Offset: 0x0006FEF2
			private string GetQuery(QueryCubeExpression cubeExpression, out Dictionary<string, string> aliases)
			{
				return MdxExpressionWriter.ToString(new SapBwCubeExpressionMdxCompiler((SapBwCubeContextProvider)this.contextProvider, base.ParameterArguments).Compile(cubeExpression, RowRange.All, out aliases));
			}

			// Token: 0x060026F8 RID: 9976 RVA: 0x00071D1B File Offset: 0x0006FF1B
			private IEnumerable<IdentifierCubeExpression> GetNumericMeasures(IEnumerable<IdentifierCubeExpression> measures)
			{
				return measures.Where((IdentifierCubeExpression m) => this.contextProvider.GetType(m).TypeKind == ValueKind.Number);
			}

			// Token: 0x060026F9 RID: 9977 RVA: 0x00071D2F File Offset: 0x0006FF2F
			private static QueryCubeExpression RemoveRowRange(QueryCubeExpression expression)
			{
				return new QueryCubeExpression(expression.From, expression.DimensionAttributes, expression.Properties, expression.Measures, expression.MeasureProperties, expression.Filter, expression.Sort, RowRange.All);
			}

			// Token: 0x0400105B RID: 4187
			private const string SapPropertyKind = "SAP.PropertyKind";

			// Token: 0x0400105C RID: 4188
			private const string SapPropertyKindNavigation = "Navigation";

			// Token: 0x0400105D RID: 4189
			private const string SapPropertyKindDisplay = "Display";

			// Token: 0x0400105E RID: 4190
			private readonly ISapBwService service;

			// Token: 0x0400105F RID: 4191
			private readonly bool cacheResults;

			// Token: 0x04001060 RID: 4192
			private readonly bool newConnection;

			// Token: 0x04001061 RID: 4193
			private MdxCubeContext.ColumnInfo[] columnInfos;

			// Token: 0x04001062 RID: 4194
			private TableValue parameters;

			// Token: 0x04001063 RID: 4195
			private TableValue capabilities;

			// Token: 0x0200049C RID: 1180
			private class RenameColumnsDataReader : DelegatingDataReaderWithTableSchema
			{
				// Token: 0x060026FC RID: 9980 RVA: 0x00071DE4 File Offset: 0x0006FFE4
				public RenameColumnsDataReader(IDataReaderWithTableSchema reader, Dictionary<string, string> renames)
					: base(reader)
				{
					this.renames = renames;
					this.reverseRenames = new Dictionary<string, string>(renames.Count);
					foreach (KeyValuePair<string, string> keyValuePair in this.renames)
					{
						this.reverseRenames.Add(keyValuePair.Value, keyValuePair.Key);
					}
				}

				// Token: 0x17000F72 RID: 3954
				// (get) Token: 0x060026FD RID: 9981 RVA: 0x00071E68 File Offset: 0x00070068
				public override TableSchema Schema
				{
					get
					{
						TableSchema schema = base.Schema;
						TableSchema tableSchema = new TableSchema(schema.ColumnCount);
						foreach (SchemaColumn schemaColumn in schema)
						{
							tableSchema.AddColumn(schemaColumn.Clone(this.GetOutputName(schemaColumn.Name)));
						}
						return tableSchema;
					}
				}

				// Token: 0x060026FE RID: 9982 RVA: 0x00071ED4 File Offset: 0x000700D4
				public override string GetName(int ordinal)
				{
					return this.GetOutputName(base.GetName(ordinal));
				}

				// Token: 0x060026FF RID: 9983 RVA: 0x00071EE3 File Offset: 0x000700E3
				public override int GetOrdinal(string name)
				{
					return base.GetOrdinal(this.GetInputName(name));
				}

				// Token: 0x17000F73 RID: 3955
				public override object this[string name]
				{
					get
					{
						return base[this.GetInputName(name)];
					}
				}

				// Token: 0x06002701 RID: 9985 RVA: 0x00071F04 File Offset: 0x00070104
				private string GetOutputName(string inputName)
				{
					string text;
					if (!this.renames.TryGetValue(inputName, out text))
					{
						text = inputName;
					}
					return text;
				}

				// Token: 0x06002702 RID: 9986 RVA: 0x00071F24 File Offset: 0x00070124
				private string GetInputName(string outputName)
				{
					string text;
					if (!this.reverseRenames.TryGetValue(outputName, out text))
					{
						text = outputName;
					}
					return text;
				}

				// Token: 0x04001064 RID: 4196
				private readonly Dictionary<string, string> renames;

				// Token: 0x04001065 RID: 4197
				private readonly Dictionary<string, string> reverseRenames;
			}

			// Token: 0x0200049D RID: 1181
			private class MeasureColumnEmptyStringToDBNullDataReader : DelegatingDataReaderWithTableSchema
			{
				// Token: 0x06002703 RID: 9987 RVA: 0x00071F44 File Offset: 0x00070144
				public MeasureColumnEmptyStringToDBNullDataReader(IDataReaderWithTableSchema reader, IEnumerable<IdentifierCubeExpression> measures)
					: base(reader)
				{
					HashSet<string> hashSet = new HashSet<string>(measures.Select((IdentifierCubeExpression s) => s.Identifier));
					TableSchema tableSchema = TableSchema.FromDataReader(reader);
					this.measureColumns = new bool[tableSchema.ColumnCount];
					foreach (SchemaColumn schemaColumn in tableSchema)
					{
						if (hashSet.Contains(schemaColumn.Name))
						{
							this.measureColumns[schemaColumn.Ordinal.Value] = true;
						}
					}
				}

				// Token: 0x06002704 RID: 9988 RVA: 0x00071FF4 File Offset: 0x000701F4
				public override object GetValue(int i)
				{
					object obj = base.GetValue(i);
					if (this.measureColumns[i] && SapBwCubeContextProvider.SapBwCubeContext.MeasureColumnEmptyStringToDBNullDataReader.IsEmptyString(obj))
					{
						obj = DBNull.Value;
					}
					return obj;
				}

				// Token: 0x06002705 RID: 9989 RVA: 0x00072024 File Offset: 0x00070224
				public override int GetValues(object[] values)
				{
					int values2 = base.GetValues(values);
					for (int i = 0; i < values2; i++)
					{
						if (this.measureColumns[i] && SapBwCubeContextProvider.SapBwCubeContext.MeasureColumnEmptyStringToDBNullDataReader.IsEmptyString(values[i]))
						{
							values[i] = DBNull.Value;
						}
					}
					return values2;
				}

				// Token: 0x06002706 RID: 9990 RVA: 0x00072062 File Offset: 0x00070262
				public override bool IsDBNull(int i)
				{
					return base.IsDBNull(i) || (this.measureColumns[i] && SapBwCubeContextProvider.SapBwCubeContext.MeasureColumnEmptyStringToDBNullDataReader.IsEmptyString(base.GetValue(i)));
				}

				// Token: 0x17000F74 RID: 3956
				public override object this[string name]
				{
					get
					{
						return this.GetValue(this.GetOrdinal(name));
					}
				}

				// Token: 0x17000F75 RID: 3957
				public override object this[int i]
				{
					get
					{
						return this.GetValue(i);
					}
				}

				// Token: 0x06002709 RID: 9993 RVA: 0x00072098 File Offset: 0x00070298
				private static bool IsEmptyString(object value)
				{
					string text = value as string;
					return text != null && text.Length == 0;
				}

				// Token: 0x04001066 RID: 4198
				private readonly bool[] measureColumns;
			}
		}
	}
}
