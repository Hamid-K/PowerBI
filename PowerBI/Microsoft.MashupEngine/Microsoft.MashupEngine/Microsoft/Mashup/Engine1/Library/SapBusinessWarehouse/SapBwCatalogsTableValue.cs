using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Cube;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Library.SapBusinessWarehouse
{
	// Token: 0x02000493 RID: 1171
	internal static class SapBwCatalogsTableValue
	{
		// Token: 0x060026C7 RID: 9927 RVA: 0x00070C88 File Offset: 0x0006EE88
		public static TableValue New(ISapBwService service)
		{
			return new SapBwCatalogsTableValue.SapBwCatalogsTableValueBuilder(service).Build();
		}

		// Token: 0x04001031 RID: 4145
		public const string CatalogNameColumnName = "Name";

		// Token: 0x04001032 RID: 4146
		public const string CubeIdColumnName = "Id";

		// Token: 0x04001033 RID: 4147
		private static readonly RecordTypeValue databaseItemType = RecordTypeValue.New(RecordValue.New(Keys.New("Id", "Name", "Data", "Kind"), new Value[]
		{
			RecordTypeAlgebra.NewField(TypeValue.Text, false),
			RecordTypeAlgebra.NewField(TypeValue.Text, false),
			RecordTypeAlgebra.NewField(NavigationTableServices.ConvertToLink(TypeValue.Table, "Cube", false), false),
			RecordTypeAlgebra.NewField(TypeValue.Text, false)
		}));

		// Token: 0x04001034 RID: 4148
		private static readonly TableTypeValue databaseTableType = SapBwCatalogsTableValue.databaseTableType.NewMeta(SapBwCatalogsTableValue.databaseTableType.MetaValue.Concatenate(RecordValue.New(Keys.New("NavigationTable.IdColumn", "NavigationTable.NameColumn", "NavigationTable.DataColumn", "NavigationTable.KindColumn"), new Value[]
		{
			NavigationTableServices.IdColumnValue,
			NavigationTableServices.NameColumnValue,
			NavigationTableServices.DataColumnValue,
			NavigationTableServices.KindColumnValue
		})).AsRecord).AsType.AsTableType;

		// Token: 0x04001035 RID: 4149
		private static readonly RecordTypeValue databasesItemType = RecordTypeValue.New(RecordValue.New(Keys.New("Name", "Data", "Kind", "DisplayName"), new Value[]
		{
			RecordTypeAlgebra.NewField(TypeValue.Text, false),
			RecordTypeAlgebra.NewField(NavigationTableServices.ConvertToLink(SapBwCatalogsTableValue.databaseItemType, "Database", false), false),
			RecordTypeAlgebra.NewField(TypeValue.Text, false),
			RecordTypeAlgebra.NewField(TypeValue.Text.Nullable, false)
		}));

		// Token: 0x04001036 RID: 4150
		private static readonly TableTypeValue databasesTableType = SapBwCatalogsTableValue.databasesTableType.NewMeta(SapBwCatalogsTableValue.databasesTableType.MetaValue.Concatenate(RecordValue.New(Keys.New(new string[] { "NavigationTable.NameColumn", "NavigationTable.DataColumn", "NavigationTable.KindColumn", "NavigationTable.DisplayNameColumn", "Preview.IdColumnEnabledDefault" }), new Value[]
		{
			NavigationTableServices.NameColumnValue,
			NavigationTableServices.DataColumnValue,
			NavigationTableServices.KindColumnValue,
			NavigationTableServices.DisplayNameColumnValue,
			LogicalValue.True
		})).AsRecord).AsType.AsTableType;

		// Token: 0x02000494 RID: 1172
		private class SapBwCatalogsTableValueBuilder
		{
			// Token: 0x060026C8 RID: 9928 RVA: 0x00070C95 File Offset: 0x0006EE95
			public SapBwCatalogsTableValueBuilder(ISapBwService service)
			{
				this.service = service;
			}

			// Token: 0x060026C9 RID: 9929 RVA: 0x00070CB4 File Offset: 0x0006EEB4
			public TableValue Build()
			{
				return ListValue.New(DeferredEnumerable.From<IValueReference>(new Func<IEnumerable<IValueReference>>(this.GetCatalogsRows))).ToTable(SapBwCatalogsTableValue.databasesTableType);
			}

			// Token: 0x060026CA RID: 9930 RVA: 0x00070CD8 File Offset: 0x0006EED8
			private IEnumerable<IValueReference> GetCatalogsRows()
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				List<IValueReference> list = new List<IValueReference>();
				using (IDataReader dataReader = this.service.ExtractMetadata("BAPI_MDPROVIDER_GET_CATALOGS", "CATALOGS", null))
				{
					while (dataReader.Read())
					{
						dictionary.Add(dataReader.GetString(0), null);
					}
				}
				foreach (KeyValuePair<string, string> keyValuePair in this.GetCubes("$INFOCUBE"))
				{
					string text = keyValuePair.Key.TrimStart(new char[] { '$' });
					string value = keyValuePair.Value;
					if (dictionary.ContainsKey(text))
					{
						dictionary[text] = value;
					}
				}
				string[] array = (from kvp in dictionary
					where kvp.Value == null && !kvp.Key.StartsWith("$", StringComparison.Ordinal) && !kvp.Key.StartsWith("@", StringComparison.Ordinal)
					select kvp.Key).ToArray<string>();
				if (array.Length != 0)
				{
					SapBwMetadataAstCreator sapBwMetadataAstCreator = this.BuildCubeTextsSql(array);
					IDataReaderWithTableSchema dataReaderWithTableSchema;
					if (this.service.TryExtractTable("Metadata/RSDCUBET", sapBwMetadataAstCreator, out dataReaderWithTableSchema))
					{
						using (dataReaderWithTableSchema)
						{
							while (dataReaderWithTableSchema.Read())
							{
								string @string = dataReaderWithTableSchema.GetString(0);
								if (dictionary.ContainsKey(@string))
								{
									dictionary[@string] = dataReaderWithTableSchema.GetString(1);
								}
							}
						}
					}
				}
				foreach (KeyValuePair<string, string> keyValuePair2 in dictionary)
				{
					string text2 = (string.IsNullOrEmpty(keyValuePair2.Value) ? keyValuePair2.Key : keyValuePair2.Value.TrimStart(Array.Empty<char>()));
					string catalogKey = keyValuePair2.Key;
					list.Add(RecordValue.New(SapBwCatalogsTableValue.databasesItemType, new Value[]
					{
						TextValue.New(catalogKey),
						ListValue.New(DeferredEnumerable.From<IValueReference>(() => this.GetCatalogRow(catalogKey))).ToTable(SapBwCatalogsTableValue.databaseTableType),
						this.databaseLinkKind,
						TextValue.New(text2)
					}));
				}
				return list;
			}

			// Token: 0x060026CB RID: 9931 RVA: 0x00070F54 File Offset: 0x0006F154
			private SapBwMetadataAstCreator BuildCubeTextsSql(string[] infocubes)
			{
				SapBwMetadataAstCreator sapBwMetadataAstCreator = new SapBwMetadataAstCreator(null);
				sapBwMetadataAstCreator.AddSelectColumns(new string[] { "INFOCUBE", "TXTLG" });
				sapBwMetadataAstCreator.AddTable("RSDCUBET");
				sapBwMetadataAstCreator.AddCondition("INFOCUBE", ListValue.New(infocubes));
				sapBwMetadataAstCreator.AddCondition("OBJVERS", TextValue.New("A"));
				sapBwMetadataAstCreator.AddCondition("LANGU", TextValue.New(this.service.Language));
				return sapBwMetadataAstCreator;
			}

			// Token: 0x060026CC RID: 9932 RVA: 0x00070FDC File Offset: 0x0006F1DC
			private IEnumerable<IValueReference> GetCatalogRow(string catalogName)
			{
				HashSet<string> hashSet = new HashSet<string>();
				List<IValueReference> list = new List<IValueReference>();
				foreach (KeyValuePair<string, string> keyValuePair in this.GetCubes(catalogName))
				{
					string key = keyValuePair.Key;
					if (hashSet.Add(key))
					{
						string value = keyValuePair.Value;
						SapBwMdxCube sapBwMdxCube = new SapBwMdxCube(this.service, catalogName, key);
						SapBwCubeContextProvider sapBwCubeContextProvider = new SapBwCubeContextProvider(this.service, sapBwMdxCube);
						CubeContext cubeContext;
						sapBwCubeContextProvider.TryCreateContext(new QueryCubeExpression(new IdentifierCubeExpression(sapBwMdxCube.MdxIdentifier)), EmptyArray<ParameterArguments>.Instance, out cubeContext);
						list.Add(RecordValue.New(SapBwCatalogsTableValue.databaseItemType, new Value[]
						{
							TextValue.New(key),
							TextValue.New(value),
							CubeContextCubeValue.New(sapBwCubeContextProvider, cubeContext, Keys.Empty),
							CubeObjectTableBuilder.CubeKind
						}));
					}
				}
				return list;
			}

			// Token: 0x060026CD RID: 9933 RVA: 0x000710D8 File Offset: 0x0006F2D8
			private IEnumerable<KeyValuePair<string, string>> GetCubes(string catalogName)
			{
				using (IDataReader reader = this.service.ExtractMetadata("BAPI_MDPROVIDER_GET_CUBES", "CUBES", new SapBwRestrictions { { "CAT_NAM", catalogName } }))
				{
					while (reader.Read())
					{
						string @string = reader.GetString(1);
						string string2 = reader.GetString(9);
						yield return new KeyValuePair<string, string>(@string, string2);
					}
				}
				IDataReader reader = null;
				yield break;
				yield break;
			}

			// Token: 0x04001037 RID: 4151
			private readonly TextValue databaseLinkKind = TextValue.New("Database");

			// Token: 0x04001038 RID: 4152
			private readonly ISapBwService service;

			// Token: 0x04001039 RID: 4153
			private const string InfoCubePrefix = "$";

			// Token: 0x0400103A RID: 4154
			private const char InfoCubePrefixChar = '$';

			// Token: 0x0400103B RID: 4155
			private const string WorkspacePrefix = "@";
		}
	}
}
