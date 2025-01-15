using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Capability;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Cube;
using Microsoft.Mashup.Engine1.Library.Mdx;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OleDb.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Essbase
{
	// Token: 0x02000C65 RID: 3173
	internal class EssbaseCubeContextProvider : MdxCubeContextProvider
	{
		// Token: 0x0600561B RID: 22043 RVA: 0x0012A85D File Offset: 0x00128A5D
		public EssbaseCubeContextProvider(EssbaseServer server, EssbaseApplication application, EssbaseCube cube, EssbaseService service)
			: base(cube)
		{
			this.CubeId = new IdentifierCubeExpression(cube.Name);
			this.server = server;
			this.application = application;
			this.cube = cube;
			this.service = service;
		}

		// Token: 0x17001A17 RID: 6679
		// (get) Token: 0x0600561C RID: 22044 RVA: 0x0012A894 File Offset: 0x00128A94
		public EssbaseService Service
		{
			get
			{
				return this.service;
			}
		}

		// Token: 0x17001A18 RID: 6680
		// (get) Token: 0x0600561D RID: 22045 RVA: 0x0012A89C File Offset: 0x00128A9C
		public EssbaseServer Server
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x17001A19 RID: 6681
		// (get) Token: 0x0600561E RID: 22046 RVA: 0x0012A8A4 File Offset: 0x00128AA4
		public EssbaseApplication Application
		{
			get
			{
				return this.application;
			}
		}

		// Token: 0x17001A1A RID: 6682
		// (get) Token: 0x0600561F RID: 22047 RVA: 0x0012A8AC File Offset: 0x00128AAC
		public override IResource Resource
		{
			get
			{
				return this.Service.Resource;
			}
		}

		// Token: 0x17001A1B RID: 6683
		// (get) Token: 0x06005620 RID: 22048 RVA: 0x0012A8B9 File Offset: 0x00128AB9
		public override IEngineHost EngineHost
		{
			get
			{
				return this.Service.Host;
			}
		}

		// Token: 0x06005621 RID: 22049 RVA: 0x0012A8C8 File Offset: 0x00128AC8
		public override bool TryCreateContext(QueryCubeExpression expression, IList<ParameterArguments> parameters, out CubeContext context)
		{
			Dictionary<string, string> dictionary;
			if (new EssbaseCubeExpressionMdxCompiler(this, parameters).CanCompile(expression, out dictionary))
			{
				context = new EssbaseCubeContextProvider.EssbaseCubeContext(this, expression, parameters);
				return true;
			}
			context = null;
			return false;
		}

		// Token: 0x06005622 RID: 22050 RVA: 0x000091AE File Offset: 0x000073AE
		public override IdentifierCubeExpression GetMeasureProperty(IdentifierCubeExpression measureIdentifier, string propertyIdentifier)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005623 RID: 22051 RVA: 0x0012A8F8 File Offset: 0x00128AF8
		protected override TableValue GetDisplayFolders()
		{
			CubeObjectTableBuilder cubeObjectTableBuilder = CubeObjectTableBuilder.NewWithoutLink();
			foreach (MdxMeasure mdxMeasure in this.Cube.Measures)
			{
				IdentifierCubeExpression identifierCubeExpression = new IdentifierCubeExpression(mdxMeasure.MdxIdentifier);
				MeasureValue measureValue = new MeasureValue(identifierCubeExpression, TypeValue.Any);
				cubeObjectTableBuilder.AddMeasure(identifierCubeExpression.Identifier, mdxMeasure.Caption, measureValue);
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

		// Token: 0x0400307F RID: 12415
		public readonly IdentifierCubeExpression CubeId;

		// Token: 0x04003080 RID: 12416
		private readonly EssbaseServer server;

		// Token: 0x04003081 RID: 12417
		private readonly EssbaseApplication application;

		// Token: 0x04003082 RID: 12418
		private readonly EssbaseCube cube;

		// Token: 0x04003083 RID: 12419
		private readonly EssbaseService service;

		// Token: 0x02000C66 RID: 3174
		public sealed class EssbaseCubeContext : MdxCubeContext
		{
			// Token: 0x06005624 RID: 22052 RVA: 0x0012AA88 File Offset: 0x00128C88
			public EssbaseCubeContext(EssbaseCubeContextProvider contextProvider, QueryCubeExpression expression, IList<ParameterArguments> arguments)
				: base(contextProvider, false, expression, arguments)
			{
				this.service = contextProvider.service;
			}

			// Token: 0x17001A1C RID: 6684
			// (get) Token: 0x06005625 RID: 22053 RVA: 0x0012AAA0 File Offset: 0x00128CA0
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

			// Token: 0x17001A1D RID: 6685
			// (get) Token: 0x06005626 RID: 22054 RVA: 0x0012AAC4 File Offset: 0x00128CC4
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

			// Token: 0x06005627 RID: 22055 RVA: 0x0012AB74 File Offset: 0x00128D74
			public override bool TryGetReader(out IPageReader reader)
			{
				reader = new DataReaderPageReader(this.GetDataReader(base.CubeExpression));
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
					string text = null;
					string text2 = null;
					if (this.columnAliases != null && this.columnAliases.Count > 0)
					{
						text = this.columnAliases.FirstOrDefault((KeyValuePair<string, string> columnAlias) => columnAlias.Value == valueColumnName).Key;
						if (text != null && EssbaseCubeContextProvider.EssbaseCubeContext.measureNamePrefixRegex.IsMatch(text))
						{
							text2 = EssbaseCubeContextProvider.EssbaseCubeContext.measureNamePrefixRegex.Replace(text, "");
						}
					}
					int num2;
					if (dictionary.TryGetValue(valueColumnName, out num2) || (text != null && dictionary.TryGetValue(text, out num2)) || (text2 != null && dictionary.TryGetValue(text2, out num2)))
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

			// Token: 0x06005628 RID: 22056 RVA: 0x0012ADB0 File Offset: 0x00128FB0
			public override void ReportFoldingFailure()
			{
				IFoldingFailureService foldingFailureService = this.service.Host.QueryService<IFoldingFailureService>();
				if (foldingFailureService != null && foldingFailureService.ThrowOnFoldingFailure)
				{
					throw ValueException.NewExpressionError<Message0>(Strings.FoldingFailure, null, null);
				}
			}

			// Token: 0x06005629 RID: 22057 RVA: 0x0012ADE8 File Offset: 0x00128FE8
			protected override IDataReader GetDataReader(QueryCubeExpression expression)
			{
				string query = this.GetQuery(expression, RowRange.All);
				EssbaseCubeContextProvider essbaseCubeContextProvider = (EssbaseCubeContextProvider)this.contextProvider;
				return this.service.ExecuteMdx(essbaseCubeContextProvider.Server.Info, essbaseCubeContextProvider.Application.Name, query, ((EssbaseMdxCubeMetadataProvider)essbaseCubeContextProvider.Cube.Metadata).MeasureAliasMap);
			}

			// Token: 0x0600562A RID: 22058 RVA: 0x0012AE45 File Offset: 0x00129045
			public override bool TryGetNativeQuery(out string nativeQuery)
			{
				nativeQuery = this.GetQuery(base.CubeExpression, RowRange.All);
				return true;
			}

			// Token: 0x0600562B RID: 22059 RVA: 0x0012AE5C File Offset: 0x0012905C
			private string GetQuery(QueryCubeExpression expression, RowRange range)
			{
				MdxCubeExpressionMdxCompiler2 mdxCubeExpressionMdxCompiler = new EssbaseCubeExpressionMdxCompiler((EssbaseCubeContextProvider)this.contextProvider, new List<ParameterArguments>());
				QueryCubeExpression queryCubeExpression = new QueryCubeExpression(expression.From, expression.DimensionAttributes, expression.Properties, expression.Measures, expression.MeasureProperties, expression.Filter, expression.Sort, RowRange.All);
				return MdxExpressionWriter.ToString(mdxCubeExpressionMdxCompiler.Compile(queryCubeExpression, range, out this.columnAliases));
			}

			// Token: 0x04003084 RID: 12420
			private static readonly Regex measureNamePrefixRegex = new Regex("\\[[^\\[\\]]+\\]\\.", RegexOptions.Compiled);

			// Token: 0x04003085 RID: 12421
			private readonly EssbaseService service;

			// Token: 0x04003086 RID: 12422
			private Dictionary<string, string> columnAliases;

			// Token: 0x04003087 RID: 12423
			private TableValue capabilities;

			// Token: 0x04003088 RID: 12424
			private MdxCubeContext.ColumnInfo[] columnInfos;
		}
	}
}
