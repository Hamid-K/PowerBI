using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Capability;
using Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql;
using Microsoft.Mashup.Engine1.Library.Cube;
using Microsoft.Mashup.Engine1.Library.Odbc;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.SapHana
{
	// Token: 0x02000425 RID: 1061
	internal sealed class SapHanaCubeContextProvider : CubeContextProvider
	{
		// Token: 0x06002421 RID: 9249 RVA: 0x00065F5C File Offset: 0x0006415C
		public SapHanaCubeContextProvider(SapHanaOdbcDataSource dataSource, SapHanaCubeBase cube, bool useSemanticSet)
		{
			this.dataSource = dataSource;
			this.cube = cube;
			this.queryDomain = SapHanaQueryDomain.NewCubeQueryDomain(dataSource, cube, useSemanticSet);
			if (useSemanticSet)
			{
				this.visitor = new SapHanaCubeExpressionVisitor2(cube, this.queryDomain);
				return;
			}
			this.visitor = new SapHanaCubeExpressionVisitor1(cube, this.queryDomain);
		}

		// Token: 0x17000EDE RID: 3806
		// (get) Token: 0x06002422 RID: 9250 RVA: 0x00065FB3 File Offset: 0x000641B3
		public override IResource Resource
		{
			get
			{
				return this.cube.Resource;
			}
		}

		// Token: 0x17000EDF RID: 3807
		// (get) Token: 0x06002423 RID: 9251 RVA: 0x00065FC0 File Offset: 0x000641C0
		public override IEngineHost EngineHost
		{
			get
			{
				return this.dataSource.Host;
			}
		}

		// Token: 0x06002424 RID: 9252 RVA: 0x00065FD0 File Offset: 0x000641D0
		public override bool TryCreateContext(QueryCubeExpression cubeExpression, IList<ParameterArguments> parameters, out CubeContext context)
		{
			Query query;
			if (this.visitor.TryVisit(cubeExpression, parameters, out query))
			{
				context = new SapHanaCubeContextProvider.SapHanaCubeContext(this, cubeExpression, query, parameters);
				return true;
			}
			context = null;
			return false;
		}

		// Token: 0x06002425 RID: 9253 RVA: 0x00066000 File Offset: 0x00064200
		public override CubeObjectKind GetObjectKind(IdentifierCubeExpression identifier)
		{
			ICubeObject cubeObject;
			if (this.cube.TryGetObject(identifier, out cubeObject))
			{
				return cubeObject.Kind;
			}
			throw new NotSupportedException();
		}

		// Token: 0x06002426 RID: 9254 RVA: 0x0006602C File Offset: 0x0006422C
		public override string GetDisplayName(IdentifierCubeExpression identifier)
		{
			ICubeObject cubeObject;
			if (this.cube.TryGetObject(identifier, out cubeObject))
			{
				switch (cubeObject.Kind)
				{
				case CubeObjectKind.DimensionAttribute:
					return ((SapHanaDimensionAttribute)cubeObject).Caption;
				case CubeObjectKind.Property:
					return ((SapHanaProperty)cubeObject).Caption;
				case CubeObjectKind.Measure:
					return ((SapHanaMeasure)cubeObject).Caption;
				}
			}
			return identifier.Identifier;
		}

		// Token: 0x06002427 RID: 9255 RVA: 0x00066090 File Offset: 0x00064290
		public override TypeValue GetType(IdentifierCubeExpression identifier)
		{
			ICubeObject cubeObject;
			if (this.cube.TryGetObject(identifier, out cubeObject))
			{
				switch (cubeObject.Kind)
				{
				case CubeObjectKind.DimensionAttribute:
				{
					DynamicSapHanaDimensionAttribute dynamicSapHanaDimensionAttribute = cubeObject as DynamicSapHanaDimensionAttribute;
					if (dynamicSapHanaDimensionAttribute != null)
					{
						return dynamicSapHanaDimensionAttribute.TypeValue;
					}
					ColumnSapHanaDimensionAttribute columnSapHanaDimensionAttribute = (ColumnSapHanaDimensionAttribute)cubeObject;
					TypeValue typeValue = columnSapHanaDimensionAttribute.CaptionColumn.Type;
					typeValue = this.AddHierarchyMetadata(typeValue, columnSapHanaDimensionAttribute);
					return CubeSourceColumnMetadata.AddColumnMetadata(typeValue, columnSapHanaDimensionAttribute.CaptionColumn.Name);
				}
				case CubeObjectKind.Property:
				{
					SapHanaProperty sapHanaProperty = (SapHanaProperty)cubeObject;
					TypeValue typeValue = sapHanaProperty.Column.Type;
					return CubeSourceColumnMetadata.AddColumnMetadata(typeValue, sapHanaProperty.Column.Name);
				}
				case CubeObjectKind.Measure:
					return ((SapHanaMeasure)cubeObject).TypeValue;
				}
			}
			return TypeValue.Any;
		}

		// Token: 0x06002428 RID: 9256 RVA: 0x0006614A File Offset: 0x0006434A
		public override IdentifierCubeExpression GetProperty(IdentifierCubeExpression dimensionAttribute, CubePropertyKind kind, string userDefinedIdentifier = null)
		{
			return this.cube.GetProperty(dimensionAttribute, kind);
		}

		// Token: 0x06002429 RID: 9257 RVA: 0x0006615C File Offset: 0x0006435C
		public override IdentifierCubeExpression GetPropertyDimensionAttribute(IdentifierCubeExpression property)
		{
			CubePropertyKind cubePropertyKind;
			return this.cube.GetPropertyAttributeAndKind(property, out cubePropertyKind);
		}

		// Token: 0x0600242A RID: 9258 RVA: 0x00066178 File Offset: 0x00064378
		public override CubePropertyKind GetPropertyKind(IdentifierCubeExpression property)
		{
			CubePropertyKind cubePropertyKind;
			this.cube.GetPropertyAttributeAndKind(property, out cubePropertyKind);
			return cubePropertyKind;
		}

		// Token: 0x0600242B RID: 9259 RVA: 0x00066198 File Offset: 0x00064398
		public override bool IsDimensionAttributeUniqueId(IdentifierCubeExpression dimensionAttribute)
		{
			ICubeObject cubeObject;
			if (!this.cube.TryGetObject(dimensionAttribute, out cubeObject) || cubeObject.Kind != CubeObjectKind.DimensionAttribute)
			{
				return false;
			}
			ColumnSapHanaDimensionAttribute columnSapHanaDimensionAttribute = cubeObject as ColumnSapHanaDimensionAttribute;
			if (columnSapHanaDimensionAttribute == null)
			{
				return cubeObject is DynamicSapHanaDimensionAttribute;
			}
			return columnSapHanaDimensionAttribute.Column.Equals(columnSapHanaDimensionAttribute.CaptionColumn);
		}

		// Token: 0x0600242C RID: 9260 RVA: 0x000661E4 File Offset: 0x000643E4
		private TypeValue AddHierarchyMetadata(TypeValue type, SapHanaDimensionAttribute attribute)
		{
			List<SapHanaLevel> list;
			if (this.cube.AttributeLevels.TryGetValue(attribute.Name, out list))
			{
				CubeHierarchiesMetadata.HierarchyInfo[] array = new CubeHierarchiesMetadata.HierarchyInfo[list.Count];
				for (int i = 0; i < array.Length; i++)
				{
					SapHanaLevel sapHanaLevel = list[i];
					array[i] = new CubeHierarchiesMetadata.HierarchyInfo
					{
						hierarchyId = sapHanaLevel.Hierarchy.Name,
						hierarchyCaption = sapHanaLevel.Hierarchy.Caption,
						dimensionId = sapHanaLevel.Hierarchy.Dimension.Name,
						dimensionCaption = sapHanaLevel.Hierarchy.Dimension.Caption,
						level = sapHanaLevel.Number,
						levelCaption = sapHanaLevel.Caption
					};
				}
				type = CubeHierarchiesMetadata.AddHierarchies(type, array);
			}
			return type;
		}

		// Token: 0x0600242D RID: 9261 RVA: 0x000662BD File Offset: 0x000644BD
		private static IEnumerable<SapHanaHierarchy> Order(IEnumerable<SapHanaHierarchy> hierarchies)
		{
			return hierarchies.OrderBy((SapHanaHierarchy h) => h.Name, StringComparer.Ordinal);
		}

		// Token: 0x0600242E RID: 9262 RVA: 0x000662E9 File Offset: 0x000644E9
		private static IEnumerable<SapHanaDimensionAttribute> Order(IEnumerable<SapHanaDimensionAttribute> attributes)
		{
			return attributes.OrderBy((SapHanaDimensionAttribute a) => a.Name, StringComparer.Ordinal);
		}

		// Token: 0x0600242F RID: 9263 RVA: 0x000091AE File Offset: 0x000073AE
		public override IdentifierCubeExpression GetMeasureProperty(IdentifierCubeExpression dimensionAttribute, string userDefinedIdentifier = null)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002430 RID: 9264 RVA: 0x00066315 File Offset: 0x00064515
		public override bool TryGetDynamicDimensionAttribute(CubeExpression expression, TypeValue typeValue, out IdentifierCubeExpression dimensionAttribute)
		{
			return this.cube.TryGetDynamicDimensionAttribute(expression, typeValue, out dimensionAttribute);
		}

		// Token: 0x04000E9A RID: 3738
		private static readonly Keys DocumentationCaptionKeys = Keys.New("Documentation.Caption");

		// Token: 0x04000E9B RID: 3739
		private readonly SapHanaOdbcDataSource dataSource;

		// Token: 0x04000E9C RID: 3740
		private readonly SapHanaCubeBase cube;

		// Token: 0x04000E9D RID: 3741
		private readonly OdbcQueryDomain queryDomain;

		// Token: 0x04000E9E RID: 3742
		private readonly SapHanaCubeExpressionVisitor visitor;

		// Token: 0x02000426 RID: 1062
		private sealed class SapHanaCubeContext : CubeContext
		{
			// Token: 0x06002432 RID: 9266 RVA: 0x00066336 File Offset: 0x00064536
			public SapHanaCubeContext(SapHanaCubeContextProvider provider, QueryCubeExpression queryExpression, Query query, IList<ParameterArguments> arguments)
				: base(queryExpression, arguments)
			{
				this.provider = provider;
				this.query = query;
			}

			// Token: 0x17000EE0 RID: 3808
			// (get) Token: 0x06002433 RID: 9267 RVA: 0x0006634F File Offset: 0x0006454F
			private Query OptimizedQuery
			{
				get
				{
					if (this.optimizedQuery == null)
					{
						this.optimizedQuery = this.query.QueryDomain.Optimize(this.query);
					}
					return this.optimizedQuery;
				}
			}

			// Token: 0x17000EE1 RID: 3809
			// (get) Token: 0x06002434 RID: 9268 RVA: 0x0006637C File Offset: 0x0006457C
			public override TableValue DirectQueryCapabilities
			{
				get
				{
					if (this.capabilities == null)
					{
						List<Value> list = new List<Value>();
						list.Add(CapabilityModule.NewCapability("Core", Value.Null));
						list.Add(CapabilityModule.NewCapability("LiteralCount", NumberValue.New(1000)));
						list.Add(CapabilityModule.NewCapability("Table.ApplyMultiColumnFiltersOnNonGroupByCols", Value.Null));
						TableTypeValue asTableType = CapabilityModule.DirectQueryCapabilities.From.Type.AsFunctionType.ReturnType.AsTableType;
						this.capabilities = ListValue.New(list.ToArray()).ToTable(asTableType);
					}
					return this.capabilities;
				}
			}

			// Token: 0x17000EE2 RID: 3810
			// (get) Token: 0x06002435 RID: 9269 RVA: 0x00066414 File Offset: 0x00064614
			public override TableValue DisplayFolders
			{
				get
				{
					if (this.displayFolders == null)
					{
						CubeObjectTableBuilder cubeObjectTableBuilder = CubeObjectTableBuilder.NewWithoutLink();
						foreach (SapHanaMeasure sapHanaMeasure in this.provider.cube.Measures)
						{
							MeasureValue measureValue = new MeasureValue(new IdentifierCubeExpression(sapHanaMeasure.Name), sapHanaMeasure.TypeValue);
							cubeObjectTableBuilder.AddMeasure(sapHanaMeasure.Name, sapHanaMeasure.Caption, measureValue);
						}
						if (this.provider.cube.FlattenDimensions)
						{
							this.AddDimensionContent(cubeObjectTableBuilder, this.provider.cube.Dimensions.Single<SapHanaDimension>());
						}
						else
						{
							foreach (SapHanaDimension sapHanaDimension in this.provider.cube.Dimensions)
							{
								CubeObjectTableBuilder cubeObjectTableBuilder2 = CubeObjectTableBuilder.NewWithoutLink();
								this.AddDimensionContent(cubeObjectTableBuilder2, sapHanaDimension);
								cubeObjectTableBuilder.AddDimensionFolder(sapHanaDimension.Name, sapHanaDimension.Caption, cubeObjectTableBuilder2.ToTable());
							}
						}
						this.displayFolders = cubeObjectTableBuilder.ToTable();
					}
					return this.displayFolders;
				}
			}

			// Token: 0x17000EE3 RID: 3811
			// (get) Token: 0x06002436 RID: 9270 RVA: 0x00066554 File Offset: 0x00064754
			public override TableValue MeasureGroups
			{
				get
				{
					return TableValue.Empty;
				}
			}

			// Token: 0x17000EE4 RID: 3812
			// (get) Token: 0x06002437 RID: 9271 RVA: 0x0006655C File Offset: 0x0006475C
			public override TableValue Dimensions
			{
				get
				{
					if (this.dimensionsTable == null)
					{
						CubeObjectTableBuilder cubeObjectTableBuilder = CubeObjectTableBuilder.New();
						foreach (SapHanaDimension sapHanaDimension in this.provider.cube.Dimensions)
						{
							cubeObjectTableBuilder.AddDimension(sapHanaDimension.Name, sapHanaDimension.Caption, this.CreateDimensionTable(sapHanaDimension));
						}
						this.dimensionsTable = cubeObjectTableBuilder.ToTable();
					}
					return this.dimensionsTable;
				}
			}

			// Token: 0x17000EE5 RID: 3813
			// (get) Token: 0x06002438 RID: 9272 RVA: 0x000665E8 File Offset: 0x000647E8
			public override TableValue Measures
			{
				get
				{
					if (this.measuresTable == null)
					{
						CubeObjectTableBuilder cubeObjectTableBuilder = CubeObjectTableBuilder.New();
						foreach (SapHanaMeasure sapHanaMeasure in this.provider.cube.Measures)
						{
							MeasureValue measureValue = new MeasureValue(new IdentifierCubeExpression(sapHanaMeasure.Name), sapHanaMeasure.TypeValue);
							cubeObjectTableBuilder.AddMeasure(sapHanaMeasure.Name, sapHanaMeasure.Caption, measureValue);
						}
						this.measuresTable = cubeObjectTableBuilder.ToTable();
					}
					return this.measuresTable;
				}
			}

			// Token: 0x17000EE6 RID: 3814
			// (get) Token: 0x06002439 RID: 9273 RVA: 0x00066684 File Offset: 0x00064884
			public override CubeContextProvider ContextProvider
			{
				get
				{
					return this.provider;
				}
			}

			// Token: 0x17000EE7 RID: 3815
			// (get) Token: 0x0600243A RID: 9274 RVA: 0x0006668C File Offset: 0x0006488C
			public override IEngineHost EngineHost
			{
				get
				{
					return this.provider.EngineHost;
				}
			}

			// Token: 0x0600243B RID: 9275 RVA: 0x0006669C File Offset: 0x0006489C
			public override TableValue GetParameters(CubeValue cube)
			{
				if (this.parameters == null)
				{
					HashSet<string> hashSet = new HashSet<string>();
					foreach (ParameterArguments parameterArguments in base.ParameterArguments)
					{
						hashSet.Add(parameterArguments.Parameter.Identifier);
					}
					SapHanaParameterValueFactory sapHanaParameterValueFactory = new SapHanaParameterValueFactory(this.provider.dataSource, cube);
					List<IValueReference> list = new List<IValueReference>();
					foreach (SapHanaParameter sapHanaParameter in this.provider.cube.Parameters)
					{
						if (!hashSet.Contains(sapHanaParameter.Name))
						{
							list.Add(RecordValue.New(CubeParametersTableValue.Columns, new Value[]
							{
								TextValue.New(sapHanaParameter.Name),
								TextValue.New(sapHanaParameter.Description),
								LogicalValue.New(!sapHanaParameter.IsMandatory),
								sapHanaParameterValueFactory.NewParameterValue(sapHanaParameter)
							}));
						}
					}
					this.parameters = ListValue.New(list).ToTable(CubeParametersTableValue.Type);
				}
				return this.parameters;
			}

			// Token: 0x0600243C RID: 9276 RVA: 0x000667E0 File Offset: 0x000649E0
			public override TableValue GetAvailableProperties()
			{
				return ListValue.New(this.GetPropertiesData(base.CubeExpression.DimensionAttributes)).ToTable(CubePropertiesTableValue.Type);
			}

			// Token: 0x0600243D RID: 9277 RVA: 0x00066802 File Offset: 0x00064A02
			public override TableValue GetAvailableMeasureProperties()
			{
				return CubeMeasurePropertiesTableValue.Empty;
			}

			// Token: 0x0600243E RID: 9278 RVA: 0x00066809 File Offset: 0x00064A09
			public override IEnumerator<IValueReference> Evaluate()
			{
				return this.OptimizedQuery.GetRows().GetEnumerator();
			}

			// Token: 0x0600243F RID: 9279 RVA: 0x0006681B File Offset: 0x00064A1B
			public override bool TryGetReader(out IPageReader reader)
			{
				return this.OptimizedQuery.TryGetReader(out reader);
			}

			// Token: 0x06002440 RID: 9280 RVA: 0x0006682C File Offset: 0x00064A2C
			public override bool TryGetQuery(out Query query)
			{
				query = this.query;
				OdbcQuery odbcQuery = query as OdbcQuery;
				if (odbcQuery != null)
				{
					query = new OdbcQuery(SapHanaQueryDomain.NewRelationalQueryDomain(this.provider.dataSource), odbcQuery.ColumnInfos, odbcQuery.QuerySpecification, odbcQuery.TableKeys, odbcQuery.CanInlineFrom, odbcQuery.SortOrder, odbcQuery.RowRange, null);
				}
				return true;
			}

			// Token: 0x06002441 RID: 9281 RVA: 0x0006688C File Offset: 0x00064A8C
			public override void ReportFoldingFailure()
			{
				IFoldingFailureService foldingFailureService = this.provider.dataSource.Host.QueryService<IFoldingFailureService>();
				if (foldingFailureService != null && foldingFailureService.ThrowOnFoldingFailure)
				{
					throw ValueException.NewExpressionError<Message0>(Strings.FoldingFailure, null, null);
				}
			}

			// Token: 0x06002442 RID: 9282 RVA: 0x000668C7 File Offset: 0x00064AC7
			private IEnumerable<IValueReference> GetPropertiesData(IList<IdentifierCubeExpression> dimensionAttributes)
			{
				foreach (IdentifierCubeExpression dimensionAttribute in dimensionAttributes)
				{
					foreach (IdentifierCubeExpression identifierCubeExpression in this.GetDimensionAttributeProperties(dimensionAttribute))
					{
						ICubeObject cubeObject;
						if (this.provider.cube.TryGetObject(identifierCubeExpression, out cubeObject))
						{
							SapHanaProperty sapHanaProperty = (SapHanaProperty)cubeObject;
							yield return RecordValue.New(CubePropertiesTableValue.Type.ItemType, new Value[]
							{
								TextValue.New(dimensionAttribute.Identifier),
								TextValue.New(sapHanaProperty.Name),
								TextValue.New(sapHanaProperty.Caption),
								TextValue.New(sapHanaProperty.PropertyKind.ToString()),
								TextValue.New(sapHanaProperty.Attribute.Dimension.Name)
							});
						}
					}
					IEnumerator<IdentifierCubeExpression> enumerator2 = null;
					dimensionAttribute = null;
				}
				IEnumerator<IdentifierCubeExpression> enumerator = null;
				yield break;
				yield break;
			}

			// Token: 0x06002443 RID: 9283 RVA: 0x000668DE File Offset: 0x00064ADE
			private IEnumerable<IdentifierCubeExpression> GetDimensionAttributeProperties(IdentifierCubeExpression dimensionAttribute)
			{
				yield return this.provider.GetProperty(dimensionAttribute, CubePropertyKind.UniqueId, null);
				yield return this.provider.GetProperty(dimensionAttribute, CubePropertyKind.Caption, null);
				yield break;
			}

			// Token: 0x06002444 RID: 9284 RVA: 0x000668F8 File Offset: 0x00064AF8
			private string ToString(PagingQuerySpecification querySpecification)
			{
				string text;
				using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
				{
					SqlSettings sqlSettings = OdbcSqlSettings.From(this.provider.queryDomain.DataSource.Info);
					ScriptWriter scriptWriter = new ScriptWriter(stringWriter, sqlSettings);
					querySpecification.WriteCreateScript(scriptWriter);
					text = stringWriter.ToString();
				}
				return text;
			}

			// Token: 0x06002445 RID: 9285 RVA: 0x00066960 File Offset: 0x00064B60
			private bool TryProjectColumns(PagingQuerySpecification querySpecification, ColumnSelection columnNames, out PagingQuerySpecification newQuerySpecification)
			{
				newQuerySpecification = querySpecification.ShallowCopy();
				newQuerySpecification.SelectItems.Clear();
				for (int i = 0; i < columnNames.Keys.Length; i++)
				{
					int column = columnNames.GetColumn(i);
					SelectItem selectItem = querySpecification.SelectItems[column];
					if (columnNames.Keys[i].Length > this.provider.queryDomain.DataSource.Info.MaxIdentifierNameLength)
					{
						newQuerySpecification = null;
						return false;
					}
					Alias alias = Alias.NewNativeAlias(columnNames.Keys[i]);
					newQuerySpecification.SelectItems.Add(new SelectItem(selectItem.Expression, alias));
				}
				return true;
			}

			// Token: 0x06002446 RID: 9286 RVA: 0x00066A0C File Offset: 0x00064C0C
			private void AddDimensionContent(CubeObjectTableBuilder dimensionBuilder, SapHanaDimension dimension)
			{
				foreach (SapHanaDimensionAttribute sapHanaDimensionAttribute in SapHanaCubeContextProvider.Order(dimension.Attributes.Values))
				{
					dimensionBuilder.AddDimensionAttribute(sapHanaDimensionAttribute.Name, sapHanaDimensionAttribute.Caption, string.Empty, dimension.Name);
				}
				if (this.provider.cube.UseHierarchies)
				{
					foreach (SapHanaHierarchy sapHanaHierarchy in SapHanaCubeContextProvider.Order(dimension.Hierarchies.Values))
					{
						CubeObjectTableBuilder cubeObjectTableBuilder = CubeObjectTableBuilder.NewWithoutLink();
						foreach (SapHanaLevel sapHanaLevel in sapHanaHierarchy.Levels)
						{
							cubeObjectTableBuilder.AddDimensionAttribute(sapHanaLevel.Attribute.Name, sapHanaLevel.Caption, string.Empty, dimension.Name);
						}
						dimensionBuilder.AddDimensionHierarchyFolder(sapHanaHierarchy.Name, sapHanaHierarchy.Caption, cubeObjectTableBuilder.ToTable());
					}
				}
			}

			// Token: 0x06002447 RID: 9287 RVA: 0x00066B54 File Offset: 0x00064D54
			private CubeValue CreateDimensionTable(SapHanaDimension dimension)
			{
				KeysBuilder keysBuilder = default(KeysBuilder);
				ArrayBuilder<IdentifierCubeExpression> arrayBuilder = default(ArrayBuilder<IdentifierCubeExpression>);
				foreach (SapHanaDimensionAttribute sapHanaDimensionAttribute in SapHanaCubeContextProvider.Order(dimension.Attributes.Values))
				{
					keysBuilder.Add(sapHanaDimensionAttribute.Name);
					arrayBuilder.Add(new IdentifierCubeExpression(sapHanaDimensionAttribute.Name));
				}
				QueryCubeExpression queryCubeExpression = new QueryCubeExpression(new IdentifierCubeExpression(this.provider.cube.ViewName), arrayBuilder.ToArray(), EmptyArray<IdentifierCubeExpression>.Instance, EmptyArray<IdentifierCubeExpression>.Instance, EmptyArray<IdentifierCubeExpression>.Instance, null, EmptyArray<CubeSortOrder>.Instance, RowRange.All);
				CubeContext cubeContext;
				this.provider.TryCreateContext(queryCubeExpression, base.ParameterArguments, out cubeContext);
				return CubeContextCubeValue.New(this.provider, cubeContext, keysBuilder.ToKeys());
			}

			// Token: 0x04000E9F RID: 3743
			private readonly SapHanaCubeContextProvider provider;

			// Token: 0x04000EA0 RID: 3744
			private readonly Query query;

			// Token: 0x04000EA1 RID: 3745
			private Query optimizedQuery;

			// Token: 0x04000EA2 RID: 3746
			private TableValue capabilities;

			// Token: 0x04000EA3 RID: 3747
			private TableValue parameters;

			// Token: 0x04000EA4 RID: 3748
			private TableValue displayFolders;

			// Token: 0x04000EA5 RID: 3749
			private TableValue dimensionsTable;

			// Token: 0x04000EA6 RID: 3750
			private TableValue measuresTable;
		}
	}
}
