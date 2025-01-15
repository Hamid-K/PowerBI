using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.AnalysisServices.AdomdClient;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Capability;
using Microsoft.Mashup.Engine1.Library.Cube;
using Microsoft.Mashup.Engine1.Library.Mdx;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.AnalysisServices
{
	// Token: 0x02000F06 RID: 3846
	internal class AnalysisServicesCubeContextProvider : MdxCubeContextProvider
	{
		// Token: 0x060065E6 RID: 26086 RVA: 0x0015ED24 File Offset: 0x0015CF24
		public AnalysisServicesCubeContextProvider(AnalysisServicesService service, AnalysisServicesMdxCube cube, bool typedMeasureColumns, bool useSemanticSetModel)
			: base(cube)
		{
			this.service = service;
			this.cubeId = new IdentifierCubeExpression(cube.MdxIdentifier);
			this.typedMeasureColumns = typedMeasureColumns;
			if (useSemanticSetModel)
			{
				this.compiler = new MdxCubeExpressionMdxCompiler2(cube, false, false);
				return;
			}
			this.compiler = new MdxCubeExpressionMdxCompiler1(cube);
		}

		// Token: 0x17001D9C RID: 7580
		// (get) Token: 0x060065E7 RID: 26087 RVA: 0x0015ED76 File Offset: 0x0015CF76
		public CubeContext DefaultContext
		{
			get
			{
				return new AnalysisServicesCubeContextProvider.AnalysisServicesCubeContext(this, new QueryCubeExpression(this.cubeId));
			}
		}

		// Token: 0x17001D9D RID: 7581
		// (get) Token: 0x060065E8 RID: 26088 RVA: 0x0015ED89 File Offset: 0x0015CF89
		public override IResource Resource
		{
			get
			{
				return this.service.Resource;
			}
		}

		// Token: 0x17001D9E RID: 7582
		// (get) Token: 0x060065E9 RID: 26089 RVA: 0x0015ED96 File Offset: 0x0015CF96
		public override IEngineHost EngineHost
		{
			get
			{
				return this.service.EngineHost;
			}
		}

		// Token: 0x060065EA RID: 26090 RVA: 0x0015EDA3 File Offset: 0x0015CFA3
		public override TypeValue GetType(IdentifierCubeExpression identifier)
		{
			if (this.Cube.GetObject(identifier.Identifier).Kind == MdxCubeObjectKind.Measure && !this.typedMeasureColumns)
			{
				return TypeValue.Number.Nullable;
			}
			return base.GetType(identifier);
		}

		// Token: 0x060065EB RID: 26091 RVA: 0x0015EDD7 File Offset: 0x0015CFD7
		public override bool TryCreateContext(QueryCubeExpression expression, IList<ParameterArguments> parameters, out CubeContext context)
		{
			if (this.compiler.CanCompile(expression))
			{
				context = new AnalysisServicesCubeContextProvider.AnalysisServicesCubeContext(this, expression);
				return true;
			}
			context = null;
			return false;
		}

		// Token: 0x060065EC RID: 26092 RVA: 0x0015EDF6 File Offset: 0x0015CFF6
		protected override TableValue GetDisplayFolders()
		{
			if (this.service.IsInTabularMode)
			{
				return this.NewTabularDisplayFolders();
			}
			return this.NewMultiDimensionalDisplayFolders();
		}

		// Token: 0x060065ED RID: 26093 RVA: 0x0015EE14 File Offset: 0x0015D014
		private TableValue NewTabularDisplayFolders()
		{
			Dictionary<string, List<MdxMeasure>> dictionary = (from m in this.Cube.Measures
				where m.IsVisible && m.MeasureGroup != null
				group m by m.MeasureGroup.Name).ToDictionary((IGrouping<string, MdxMeasure> g) => g.Key, (IGrouping<string, MdxMeasure> g) => g.ToList<MdxMeasure>());
			Dictionary<string, List<MdxKpi>> dictionary2 = (from m in this.Cube.Kpis
				where m.MeasureGroup != null
				select m into k
				group k by k.MeasureGroup.Name).ToDictionary((IGrouping<string, MdxKpi> g) => g.Key, (IGrouping<string, MdxKpi> g) => g.ToList<MdxKpi>());
			CubeObjectTableBuilder cubeObjectTableBuilder = CubeObjectTableBuilder.NewWithoutLink();
			foreach (MdxDimension mdxDimension in this.Cube.Dimensions.Values)
			{
				MdxDisplayFolderBuilder mdxDisplayFolderBuilder = new MdxDisplayFolderBuilder();
				foreach (MdxHierarchy mdxHierarchy in mdxDimension.VisibleHierarchies)
				{
					mdxDisplayFolderBuilder.Add(mdxHierarchy);
				}
				MdxIdentifier mdxIdentifier = MdxIdentifier.Parse(mdxDimension.MdxIdentifier);
				List<MdxMeasure> list;
				if (dictionary.TryGetValue(mdxIdentifier.DimensionName, out list))
				{
					foreach (MdxMeasure mdxMeasure in list)
					{
						mdxDisplayFolderBuilder.Add(mdxMeasure);
					}
				}
				List<MdxKpi> list2;
				if (dictionary2.TryGetValue(mdxIdentifier.DimensionName, out list2))
				{
					foreach (MdxKpi mdxKpi in list2)
					{
						mdxDisplayFolderBuilder.Add(mdxKpi, false);
					}
				}
				if (mdxDisplayFolderBuilder.Count > 0)
				{
					cubeObjectTableBuilder.AddDimensionFolder(mdxDimension.MdxIdentifier, mdxDimension.Caption, mdxDisplayFolderBuilder.ToTable());
				}
			}
			return cubeObjectTableBuilder.ToTable();
		}

		// Token: 0x060065EE RID: 26094 RVA: 0x0015F0FC File Offset: 0x0015D2FC
		private TableValue NewMultiDimensionalDisplayFolders()
		{
			CubeObjectTableBuilder cubeObjectTableBuilder = CubeObjectTableBuilder.NewWithoutLink();
			MdxDisplayFolderBuilder mdxDisplayFolderBuilder = new MdxDisplayFolderBuilder();
			foreach (MdxMeasure mdxMeasure in this.Cube.Measures)
			{
				if (mdxMeasure.IsVisible)
				{
					IdentifierCubeExpression identifierCubeExpression = new IdentifierCubeExpression(mdxMeasure.MdxIdentifier);
					MeasureValue measureValue = new MeasureValue(identifierCubeExpression, TypeValue.Any);
					this.CreateMultidimensionMeasureFolder(mdxDisplayFolderBuilder, mdxMeasure.MeasureGroup).CreateSubfoldersFromPaths(mdxMeasure.DisplayFolders, CubeObjectTableBuilder.FolderKind, this.Cube.SupportsProperties).AddValue(identifierCubeExpression.Identifier, mdxMeasure.Caption, CubeObjectTableBuilder.MeasureKind, measureValue);
				}
			}
			foreach (MdxKpi mdxKpi in this.Cube.Kpis)
			{
				this.CreateMultidimensionMeasureFolder(mdxDisplayFolderBuilder, mdxKpi.MeasureGroup).CreateSubfoldersFromPaths(mdxKpi.DisplayFolders, CubeObjectTableBuilder.FolderKind, this.Cube.SupportsProperties).Add(mdxKpi, true);
			}
			mdxDisplayFolderBuilder.Populate(cubeObjectTableBuilder);
			foreach (MdxDimension mdxDimension in this.Cube.Dimensions.Values)
			{
				MdxDisplayFolderBuilder mdxDisplayFolderBuilder2 = new MdxDisplayFolderBuilder();
				foreach (MdxHierarchy mdxHierarchy in mdxDimension.VisibleHierarchies)
				{
					MdxDisplayFolderContainerBuilder mdxDisplayFolderContainerBuilder = mdxDisplayFolderBuilder2.CreateSubfoldersFromPaths(mdxHierarchy.DisplayFolders, CubeObjectTableBuilder.FolderKind, this.Cube.SupportsProperties);
					if (this.Cube.SupportsProperties)
					{
						mdxDisplayFolderContainerBuilder.AddWithPropertiesPerLevel(mdxHierarchy);
					}
					else
					{
						mdxDisplayFolderContainerBuilder.Add(mdxHierarchy);
					}
				}
				if (mdxDisplayFolderBuilder2.Count > 0)
				{
					cubeObjectTableBuilder.AddDimensionFolder(mdxDimension.MdxIdentifier, mdxDimension.Caption, mdxDisplayFolderBuilder2.ToTable());
				}
			}
			return cubeObjectTableBuilder.ToTable();
		}

		// Token: 0x060065EF RID: 26095 RVA: 0x0015F32C File Offset: 0x0015D52C
		private MdxDisplayFolderBuilder CreateMultidimensionMeasureFolder(MdxDisplayFolderBuilder parent, MdxMeasureGroup measureGroup)
		{
			TextValue textValue = ((measureGroup == null) ? AnalysisServicesCubeContextProvider.DefaultMeasuresFolderId : TextValue.New(measureGroup.Caption));
			return parent.CreateSubfolder(textValue, textValue, CubeObjectTableBuilder.MeasureFolderKind);
		}

		// Token: 0x060065F0 RID: 26096 RVA: 0x000091AE File Offset: 0x000073AE
		public override IdentifierCubeExpression GetMeasureProperty(IdentifierCubeExpression measureIdentifier, string propertyIdentifier)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060065F1 RID: 26097 RVA: 0x0015F35C File Offset: 0x0015D55C
		protected override MdxProperty GetPropertyByUserDefinedIdentifier(MdxLevel level, string userDefinedIdentifier)
		{
			return level.Properties.Single((MdxProperty p) => p.MdxIdentifier.EndsWith(userDefinedIdentifier, StringComparison.Ordinal));
		}

		// Token: 0x040037F8 RID: 14328
		private static readonly TextValue DefaultMeasuresFolderId = TextValue.New("Measures");

		// Token: 0x040037F9 RID: 14329
		private readonly AnalysisServicesService service;

		// Token: 0x040037FA RID: 14330
		private readonly MdxCubeExpressionMdxCompiler compiler;

		// Token: 0x040037FB RID: 14331
		private readonly IdentifierCubeExpression cubeId;

		// Token: 0x040037FC RID: 14332
		private readonly bool typedMeasureColumns;

		// Token: 0x02000F07 RID: 3847
		protected class AnalysisServicesCubeContext : MdxCubeContext
		{
			// Token: 0x060065F3 RID: 26099 RVA: 0x0015F39E File Offset: 0x0015D59E
			public AnalysisServicesCubeContext(AnalysisServicesCubeContextProvider contextProvider, QueryCubeExpression expression)
				: base(contextProvider, contextProvider.service.IsInTabularMode, expression)
			{
				this.service = contextProvider.service;
				this.compiler = contextProvider.compiler;
			}

			// Token: 0x17001D9F RID: 7583
			// (get) Token: 0x060065F4 RID: 26100 RVA: 0x0015F3CC File Offset: 0x0015D5CC
			public override TableValue DirectQueryCapabilities
			{
				get
				{
					if (this.capabilities == null)
					{
						List<Value> list = new List<Value>();
						list.Add(CapabilityModule.NewCapability("Core", Value.Null));
						list.Add(CapabilityModule.NewCapability("LiteralCount", NumberValue.New(1000)));
						TableTypeValue asTableType = CapabilityModule.DirectQueryCapabilities.From.Type.AsFunctionType.ReturnType.AsTableType;
						this.capabilities = ListValue.New(list.ToArray()).ToTable(asTableType);
					}
					return this.capabilities;
				}
			}

			// Token: 0x060065F5 RID: 26101 RVA: 0x0015F450 File Offset: 0x0015D650
			protected override IDataReader GetDataReader(QueryCubeExpression expression)
			{
				string query = this.GetQuery(expression, RowRange.All);
				return this.service.ExecuteCommand(this.service.DataCache, CommandBehavior.Default, query, EmptyArray<KeyValuePair<string, object>>.Instance, expression.RowRange, (RowRange range) => this.GetQuery(expression, range));
			}

			// Token: 0x060065F6 RID: 26102 RVA: 0x0015F4B7 File Offset: 0x0015D6B7
			public override bool TryGetNativeQuery(out string nativeQuery)
			{
				nativeQuery = this.GetQuery(base.CubeExpression, RowRange.All);
				return true;
			}

			// Token: 0x060065F7 RID: 26103 RVA: 0x0015F4D0 File Offset: 0x0015D6D0
			private string GetQuery(QueryCubeExpression expression, RowRange range)
			{
				QueryCubeExpression queryCubeExpression = new QueryCubeExpression(expression.From, expression.DimensionAttributes, expression.Properties, expression.Measures, expression.MeasureProperties, expression.Filter, expression.Sort, RowRange.All);
				return MdxExpressionWriter.ToString(this.compiler.Compile(queryCubeExpression, range));
			}

			// Token: 0x060065F8 RID: 26104 RVA: 0x0015F524 File Offset: 0x0015D724
			protected override MdxCubeContext.ResultEnumerator CreateResultEnumerator(Keys keys)
			{
				return new AnalysisServicesCubeContextProvider.AnalysisServicesCubeContext.AnalysisServicesResultEnumerator(this, base.CubeExpression, keys);
			}

			// Token: 0x040037FD RID: 14333
			private AnalysisServicesService service;

			// Token: 0x040037FE RID: 14334
			private MdxCubeExpressionMdxCompiler compiler;

			// Token: 0x040037FF RID: 14335
			private TableValue capabilities;

			// Token: 0x02000F08 RID: 3848
			private sealed class AnalysisServicesResultEnumerator : MdxCubeContext.ResultEnumerator
			{
				// Token: 0x060065F9 RID: 26105 RVA: 0x0015F533 File Offset: 0x0015D733
				public AnalysisServicesResultEnumerator(AnalysisServicesCubeContextProvider.AnalysisServicesCubeContext context, QueryCubeExpression expression, Keys keys)
					: base(context, expression, keys)
				{
				}

				// Token: 0x060065FA RID: 26106 RVA: 0x0015F540 File Offset: 0x0015D740
				public override bool MoveNext()
				{
					bool flag;
					try
					{
						flag = base.MoveNext();
					}
					catch (AdomdErrorResponseException ex)
					{
						throw ValueException.NewDataSourceError<Message1>(Strings.ReadingFromProviderError(ex.Message), Value.Null, ex);
					}
					return flag;
				}

				// Token: 0x060065FB RID: 26107 RVA: 0x0015F580 File Offset: 0x0015D780
				protected override Dictionary<string, int> GetColumns(IDataReader reader, MdxCubeContext.ColumnInfo[] columnInfos)
				{
					int num = 0;
					Dictionary<string, int> dictionary = new Dictionary<string, int>();
					for (int i = 0; i < reader.FieldCount; i++)
					{
						string name = reader.GetName(i);
						if (name.LastIndexOf(".[key]", StringComparison.OrdinalIgnoreCase) == -1)
						{
							dictionary.Add(name, i);
						}
						else
						{
							bool flag = false;
							while (!flag && num < columnInfos.Length)
							{
								if (columnInfos[num].ValueColumnName.LastIndexOf(".[key", StringComparison.OrdinalIgnoreCase) != -1)
								{
									dictionary.Add(columnInfos[num].ValueColumnName, i);
									flag = true;
								}
								num++;
							}
							if (!flag)
							{
								throw ValueException.TableColumnNotFound(name);
							}
						}
					}
					return dictionary;
				}
			}
		}
	}
}
