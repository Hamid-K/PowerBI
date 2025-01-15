using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Library.Common.Navigation
{
	// Token: 0x02001166 RID: 4454
	internal sealed class HierarchicalNavigationQuery : DataSourceQuery
	{
		// Token: 0x060074B0 RID: 29872 RVA: 0x00190934 File Offset: 0x0018EB34
		public static TableValue New(IEngineHost engineHost, IMultiLevelNavigationProvider navigator)
		{
			HierarchicalNavigationQuery.IHierarchicalNavigationLevel hierarchicalNavigationLevel;
			if (navigator.SupportsCatalogs)
			{
				hierarchicalNavigationLevel = new HierarchicalNavigationQuery.CatalogLevel(engineHost, navigator);
			}
			else if (navigator.SupportsSchemas)
			{
				hierarchicalNavigationLevel = new HierarchicalNavigationQuery.SchemaLevel(engineHost, navigator, Restriction.Any);
			}
			else
			{
				hierarchicalNavigationLevel = new HierarchicalNavigationQuery.TableLevel(navigator, Restriction.Any, Restriction.Any);
			}
			return HierarchicalNavigationQuery.WrapQuery(new HierarchicalNavigationQuery(engineHost, hierarchicalNavigationLevel));
		}

		// Token: 0x060074B1 RID: 29873 RVA: 0x00190987 File Offset: 0x0018EB87
		private static TableValue WrapQuery(Query query)
		{
			QueryTableValue queryTableValue = new QueryTableValue(query);
			return queryTableValue.ReplaceType(queryTableValue.Type.NewMeta(HierarchicalNavigationQuery.tableType.MetaValue).AsType).AsTable;
		}

		// Token: 0x060074B2 RID: 29874 RVA: 0x001909B3 File Offset: 0x0018EBB3
		private HierarchicalNavigationQuery(IEngineHost engineHost, HierarchicalNavigationQuery.IHierarchicalNavigationLevel level)
		{
			this.engineHost = engineHost;
			this.level = level;
		}

		// Token: 0x060074B3 RID: 29875 RVA: 0x001909C9 File Offset: 0x0018EBC9
		public override TypeValue GetColumnType(int column)
		{
			return HierarchicalNavigationQuery.tableType.ItemType.Fields[column][0].AsType;
		}

		// Token: 0x17002068 RID: 8296
		// (get) Token: 0x060074B4 RID: 29876 RVA: 0x001909EB File Offset: 0x0018EBEB
		public override IList<TableKey> TableKeys
		{
			get
			{
				return new TableKey[]
				{
					new TableKey(HierarchicalNavigationQuery.tableKeys, true)
				};
			}
		}

		// Token: 0x17002069 RID: 8297
		// (get) Token: 0x060074B5 RID: 29877 RVA: 0x00190A01 File Offset: 0x0018EC01
		public override Keys Columns
		{
			get
			{
				return HierarchicalNavigationQuery.columns;
			}
		}

		// Token: 0x1700206A RID: 8298
		// (get) Token: 0x060074B6 RID: 29878 RVA: 0x00190A08 File Offset: 0x0018EC08
		public override IEngineHost EngineHost
		{
			get
			{
				return this.engineHost;
			}
		}

		// Token: 0x060074B7 RID: 29879 RVA: 0x00190A10 File Offset: 0x0018EC10
		public override Query SelectRows(FunctionValue condition)
		{
			RecordValue recordValue;
			Value value;
			Value value2;
			if (NavigationTableServices.TryGetIndexRecord(HierarchicalNavigationQuery.tableType.ItemType, condition, out recordValue) && recordValue.Keys.Length == HierarchicalNavigationQuery.tableKeys.Length && recordValue.TryGetValue(this.Columns[3], out value) && recordValue.TryGetValue(this.Columns[0], out value2) && value.IsText && value2.IsText && !this.level.NonParentalNames.Contains(value2.AsString))
			{
				if (this.level.TableKinds.Contains(value.AsString))
				{
					return ListValue.New(new Value[] { this.level.NewLazyRow(value2.AsString, value.AsText) }).ToTable(HierarchicalNavigationQuery.tableType).Query;
				}
				if (this.level.NonParentalNames.Count<string>() == 1)
				{
					string text = this.level.NonParentalNames.Single<string>();
					return this.level.NewData(text, null).Value.AsTable.SelectRows(condition).Query;
				}
			}
			return base.SelectRows(condition);
		}

		// Token: 0x060074B8 RID: 29880 RVA: 0x00190B49 File Offset: 0x0018ED49
		public override Value NativeQuery(TextValue query, Value parameters, Value options)
		{
			return this.level.NativeQuery(HierarchicalNavigationQuery.WrapQuery(this), query, parameters, options);
		}

		// Token: 0x060074B9 RID: 29881 RVA: 0x00190B5F File Offset: 0x0018ED5F
		public override ActionValue NativeStatement(TextValue statement, Value parameters, Value options)
		{
			return this.level.NativeStatement(HierarchicalNavigationQuery.WrapQuery(this), statement, parameters, options);
		}

		// Token: 0x060074BA RID: 29882 RVA: 0x00190B75 File Offset: 0x0018ED75
		public override IEnumerable<IValueReference> GetRows()
		{
			foreach (HierarchyItem hierarchyItem in this.level.Items)
			{
				if (!this.level.NonParentalNames.Contains(hierarchyItem.Name))
				{
					yield return this.level.NewRow(hierarchyItem, this.level.NewData(hierarchyItem.Name, hierarchyItem.TableType));
				}
				else
				{
					foreach (IValueReference valueReference in this.level.NewData(hierarchyItem.Name, hierarchyItem.TableType).Value.AsTable)
					{
						yield return valueReference;
					}
					IEnumerator<IValueReference> enumerator2 = null;
				}
			}
			IEnumerator<HierarchyItem> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060074BB RID: 29883 RVA: 0x00190B85 File Offset: 0x0018ED85
		public override void TestConnection()
		{
			this.level.NavigationProvider.TestConnection();
		}

		// Token: 0x060074BC RID: 29884 RVA: 0x00190B98 File Offset: 0x0018ED98
		private static RecordTypeValue CreateRecordType(string linkKind, bool isLeaf)
		{
			return RecordTypeValue.New(RecordValue.New(HierarchicalNavigationQuery.columns, new Value[]
			{
				RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
				{
					TypeValue.Text,
					LogicalValue.False
				}),
				RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
				{
					TypeValue.Text.Nullable,
					LogicalValue.False
				}),
				RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
				{
					NavigationTableServices.ConvertToLink(HierarchicalNavigationQuery.tableType.Nullable, linkKind, isLeaf),
					LogicalValue.False
				}),
				RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
				{
					TypeValue.Text,
					LogicalValue.False
				})
			}));
		}

		// Token: 0x04004028 RID: 16424
		public const int NameOrdinal = 0;

		// Token: 0x04004029 RID: 16425
		public const int DescriptionOrdinal = 1;

		// Token: 0x0400402A RID: 16426
		public const int DataOrdinal = 2;

		// Token: 0x0400402B RID: 16427
		public const int KindOrdinal = 3;

		// Token: 0x0400402C RID: 16428
		private static readonly int[] tableKeys = new int[] { 0, 3 };

		// Token: 0x0400402D RID: 16429
		private static readonly Keys columns = Keys.New("Name", "Description", "Data", "Kind");

		// Token: 0x0400402E RID: 16430
		private static readonly TableTypeValue tableType = NavigationTableServices.AddNavigationTableMetadataWithKind(TableTypeValue.New(RecordTypeValue.New(RecordValue.New(HierarchicalNavigationQuery.columns, new Value[]
		{
			RecordTypeAlgebra.NewField(TypeValue.Text, false),
			RecordTypeAlgebra.NewField(NullableTypeValue.Text, false),
			RecordTypeAlgebra.NewField(BinaryOperator.AddMeta.Invoke(NavigationTableServices.ConvertToLink(TypeValue.Table, "Table", false), RecordValue.New(new NamedValue[]
			{
				new NamedValue("NavigationTable.RowConfigurationColumn", NavigationTableServices.KindColumnValue)
			})).AsType, false),
			RecordTypeAlgebra.NewField(TypeValue.Text, false)
		})), new TableKey[]
		{
			new TableKey(HierarchicalNavigationQuery.tableKeys, true)
		}), TextValue.New("Name"), TextValue.New("Data"), TextValue.New("Kind"));

		// Token: 0x0400402F RID: 16431
		private readonly IEngineHost engineHost;

		// Token: 0x04004030 RID: 16432
		private readonly HierarchicalNavigationQuery.IHierarchicalNavigationLevel level;

		// Token: 0x02001167 RID: 4455
		private interface IHierarchicalNavigationLevel
		{
			// Token: 0x1700206B RID: 8299
			// (get) Token: 0x060074BE RID: 29886
			IEnumerable<string> TableKinds { get; }

			// Token: 0x1700206C RID: 8300
			// (get) Token: 0x060074BF RID: 29887
			IEnumerable<HierarchyItem> Items { get; }

			// Token: 0x1700206D RID: 8301
			// (get) Token: 0x060074C0 RID: 29888
			IEnumerable<string> NonParentalNames { get; }

			// Token: 0x1700206E RID: 8302
			// (get) Token: 0x060074C1 RID: 29889
			IMultiLevelNavigationProvider NavigationProvider { get; }

			// Token: 0x060074C2 RID: 29890
			RecordValue NewRow(HierarchyItem item, IValueReference data);

			// Token: 0x060074C3 RID: 29891
			RecordValue NewLazyRow(string name, TextValue kind);

			// Token: 0x060074C4 RID: 29892
			IValueReference NewData(string name, TableType tableType);

			// Token: 0x060074C5 RID: 29893
			Value NativeQuery(Value target, TextValue query, Value parameters, Value options);

			// Token: 0x060074C6 RID: 29894
			ActionValue NativeStatement(Value target, TextValue query, Value parameters, Value options);
		}

		// Token: 0x02001168 RID: 4456
		private class CatalogLevel : HierarchicalNavigationQuery.IHierarchicalNavigationLevel
		{
			// Token: 0x060074C7 RID: 29895 RVA: 0x00190D5B File Offset: 0x0018EF5B
			public CatalogLevel(IEngineHost engineHost, IMultiLevelNavigationProvider navigator)
			{
				this.engineHost = engineHost;
				this.navigator = navigator;
			}

			// Token: 0x1700206F RID: 8303
			// (get) Token: 0x060074C8 RID: 29896 RVA: 0x00190D71 File Offset: 0x0018EF71
			public IEnumerable<string> TableKinds
			{
				get
				{
					yield return "Database";
					yield break;
				}
			}

			// Token: 0x17002070 RID: 8304
			// (get) Token: 0x060074C9 RID: 29897 RVA: 0x00190D7A File Offset: 0x0018EF7A
			public IEnumerable<string> NonParentalNames
			{
				get
				{
					return this.navigator.NonParentalNames;
				}
			}

			// Token: 0x17002071 RID: 8305
			// (get) Token: 0x060074CA RID: 29898 RVA: 0x00190D87 File Offset: 0x0018EF87
			public IEnumerable<HierarchyItem> Items
			{
				get
				{
					if (this.catalogs == null)
					{
						this.catalogs = this.navigator.GetCatalogItems();
					}
					return this.catalogs.Cast<HierarchyItem>();
				}
			}

			// Token: 0x17002072 RID: 8306
			// (get) Token: 0x060074CB RID: 29899 RVA: 0x00190DAD File Offset: 0x0018EFAD
			public IMultiLevelNavigationProvider NavigationProvider
			{
				get
				{
					return this.navigator;
				}
			}

			// Token: 0x060074CC RID: 29900 RVA: 0x00190DB5 File Offset: 0x0018EFB5
			public RecordValue NewRow(HierarchyItem item, IValueReference data)
			{
				return RecordValue.New(HierarchicalNavigationQuery.CatalogLevel.catalogRecordType, new IValueReference[]
				{
					TextValue.New(item.Name),
					item.Description,
					data,
					HierarchyItem.DatabaseKindValue
				});
			}

			// Token: 0x060074CD RID: 29901 RVA: 0x00190DEA File Offset: 0x0018EFEA
			public RecordValue NewLazyRow(string name, TextValue kind)
			{
				return this.NewRow(new HierarchyCatalogItem(name, Value.Null), this.NewData(name, null));
			}

			// Token: 0x060074CE RID: 29902 RVA: 0x00190E08 File Offset: 0x0018F008
			public IValueReference NewData(string name, TableType tableType)
			{
				HierarchicalNavigationQuery.IHierarchicalNavigationLevel hierarchicalNavigationLevel;
				if (this.navigator.SupportsSchemas)
				{
					hierarchicalNavigationLevel = new HierarchicalNavigationQuery.SchemaLevel(this.engineHost, this.navigator, Restriction.To(name));
				}
				else
				{
					hierarchicalNavigationLevel = new HierarchicalNavigationQuery.TableLevel(this.navigator, Restriction.To(name), Restriction.Any);
				}
				return HierarchicalNavigationQuery.WrapQuery(new HierarchicalNavigationQuery(this.engineHost, hierarchicalNavigationLevel));
			}

			// Token: 0x060074CF RID: 29903 RVA: 0x00190E64 File Offset: 0x0018F064
			public Value NativeQuery(Value target, TextValue query, Value parameters, Value options)
			{
				return this.navigator.NativeQuery(target, query, parameters, options, null);
			}

			// Token: 0x060074D0 RID: 29904 RVA: 0x00190E77 File Offset: 0x0018F077
			public ActionValue NativeStatement(Value target, TextValue query, Value parameters, Value options)
			{
				return this.navigator.NativeStatement(target, query, parameters, options, null);
			}

			// Token: 0x04004031 RID: 16433
			private static readonly RecordTypeValue catalogRecordType = HierarchicalNavigationQuery.CreateRecordType("Database", false);

			// Token: 0x04004032 RID: 16434
			private readonly IEngineHost engineHost;

			// Token: 0x04004033 RID: 16435
			private readonly IMultiLevelNavigationProvider navigator;

			// Token: 0x04004034 RID: 16436
			private IEnumerable<HierarchyCatalogItem> catalogs;
		}

		// Token: 0x0200116A RID: 4458
		private sealed class SchemaLevel : HierarchicalNavigationQuery.IHierarchicalNavigationLevel
		{
			// Token: 0x060074DA RID: 29914 RVA: 0x00190F3F File Offset: 0x0018F13F
			public SchemaLevel(IEngineHost engineHost, IMultiLevelNavigationProvider navigator, Restriction catalogName)
			{
				this.engineHost = engineHost;
				this.navigator = navigator;
				this.catalogName = catalogName;
			}

			// Token: 0x17002075 RID: 8309
			// (get) Token: 0x060074DB RID: 29915 RVA: 0x00190F5C File Offset: 0x0018F15C
			public IEnumerable<string> TableKinds
			{
				get
				{
					yield return "Schema";
					yield break;
				}
			}

			// Token: 0x17002076 RID: 8310
			// (get) Token: 0x060074DC RID: 29916 RVA: 0x00190F65 File Offset: 0x0018F165
			public IEnumerable<string> NonParentalNames
			{
				get
				{
					return this.navigator.NonParentalNames;
				}
			}

			// Token: 0x17002077 RID: 8311
			// (get) Token: 0x060074DD RID: 29917 RVA: 0x00190F72 File Offset: 0x0018F172
			public IEnumerable<HierarchyItem> Items
			{
				get
				{
					if (this.schemas == null)
					{
						this.schemas = this.navigator.GetSchemaItems(this.catalogName);
					}
					return this.schemas.Cast<HierarchyItem>();
				}
			}

			// Token: 0x17002078 RID: 8312
			// (get) Token: 0x060074DE RID: 29918 RVA: 0x00190F9E File Offset: 0x0018F19E
			public IMultiLevelNavigationProvider NavigationProvider
			{
				get
				{
					return this.navigator;
				}
			}

			// Token: 0x060074DF RID: 29919 RVA: 0x00190FA6 File Offset: 0x0018F1A6
			public RecordValue NewRow(HierarchyItem item, IValueReference data)
			{
				return RecordValue.New(HierarchicalNavigationQuery.SchemaLevel.schemaRecordType, new IValueReference[]
				{
					TextValue.New(item.Name),
					TextValue.Empty,
					data,
					HierarchyItem.SchemaKindValue
				});
			}

			// Token: 0x060074E0 RID: 29920 RVA: 0x00190FDC File Offset: 0x0018F1DC
			public RecordValue NewLazyRow(string name, TextValue kind)
			{
				return this.NewRow(new HierarchySchemaItem(this.catalogName.Item, name, Value.Null), this.NewData(name, null));
			}

			// Token: 0x060074E1 RID: 29921 RVA: 0x00191010 File Offset: 0x0018F210
			public IValueReference NewData(string name, TableType tableType)
			{
				return HierarchicalNavigationQuery.WrapQuery(new HierarchicalNavigationQuery(this.engineHost, new HierarchicalNavigationQuery.TableLevel(this.navigator, this.catalogName, Restriction.To(name))));
			}

			// Token: 0x060074E2 RID: 29922 RVA: 0x0019103C File Offset: 0x0018F23C
			public Value NativeQuery(Value target, TextValue query, Value parameters, Value options)
			{
				return this.navigator.NativeQuery(target, query, parameters, options, this.catalogName.Item);
			}

			// Token: 0x060074E3 RID: 29923 RVA: 0x00191068 File Offset: 0x0018F268
			public ActionValue NativeStatement(Value target, TextValue query, Value parameters, Value options)
			{
				return this.navigator.NativeStatement(target, query, parameters, options, this.catalogName.Item);
			}

			// Token: 0x04004038 RID: 16440
			private static readonly RecordTypeValue schemaRecordType = HierarchicalNavigationQuery.CreateRecordType("Schema", false);

			// Token: 0x04004039 RID: 16441
			private readonly IEngineHost engineHost;

			// Token: 0x0400403A RID: 16442
			private readonly IMultiLevelNavigationProvider navigator;

			// Token: 0x0400403B RID: 16443
			private readonly Restriction catalogName;

			// Token: 0x0400403C RID: 16444
			private IEnumerable<HierarchySchemaItem> schemas;
		}

		// Token: 0x0200116C RID: 4460
		private sealed class TableLevel : HierarchicalNavigationQuery.IHierarchicalNavigationLevel
		{
			// Token: 0x060074ED RID: 29933 RVA: 0x00191147 File Offset: 0x0018F347
			public TableLevel(IMultiLevelNavigationProvider navigator, Restriction catalogName, Restriction schemaName)
			{
				this.navigator = navigator;
				this.selectedCatalog = catalogName;
				this.selectedSchema = schemaName;
			}

			// Token: 0x1700207B RID: 8315
			// (get) Token: 0x060074EE RID: 29934 RVA: 0x00191164 File Offset: 0x0018F364
			public IEnumerable<string> TableKinds
			{
				get
				{
					return this.navigator.TableTypes.Select((TableType t) => t.Kind);
				}
			}

			// Token: 0x1700207C RID: 8316
			// (get) Token: 0x060074EF RID: 29935 RVA: 0x00191195 File Offset: 0x0018F395
			public IEnumerable<string> NonParentalNames
			{
				get
				{
					return EmptyArray<string>.Instance;
				}
			}

			// Token: 0x1700207D RID: 8317
			// (get) Token: 0x060074F0 RID: 29936 RVA: 0x0019119C File Offset: 0x0018F39C
			public IEnumerable<HierarchyItem> Items
			{
				get
				{
					if (this.tables == null)
					{
						this.tables = this.navigator.GetTableItems(this.selectedCatalog, this.selectedSchema);
					}
					return this.tables.Cast<HierarchyItem>();
				}
			}

			// Token: 0x1700207E RID: 8318
			// (get) Token: 0x060074F1 RID: 29937 RVA: 0x001911CE File Offset: 0x0018F3CE
			public IMultiLevelNavigationProvider NavigationProvider
			{
				get
				{
					return this.navigator;
				}
			}

			// Token: 0x060074F2 RID: 29938 RVA: 0x001911D8 File Offset: 0x0018F3D8
			public RecordValue NewRow(HierarchyItem item, IValueReference data)
			{
				TableType tableType = item.TableType;
				return RecordValue.New(HierarchicalNavigationQuery.CreateRecordType(((tableType != null) ? tableType.LinkKind : null) ?? item.Kind.String, true), new IValueReference[]
				{
					TextValue.New(item.Name),
					item.Description,
					data,
					item.Kind
				});
			}

			// Token: 0x060074F3 RID: 29939 RVA: 0x0019123C File Offset: 0x0018F43C
			public RecordValue NewLazyRow(string name, TextValue kind)
			{
				HierarchyItem row = null;
				TextValue nameValue = TextValue.New(name);
				Func<TableType, bool> <>9__1;
				Func<HierarchyItem, bool> <>9__2;
				return RecordValue.New(HierarchicalNavigationQuery.columns, delegate(int i)
				{
					switch (i)
					{
					case 0:
						return nameValue;
					case 2:
					{
						HierarchicalNavigationQuery.TableLevel <>4__this = this;
						string name2 = name;
						IEnumerable<TableType> tableTypes = this.navigator.TableTypes;
						Func<TableType, bool> func;
						if ((func = <>9__1) == null)
						{
							func = (<>9__1 = (TableType t) => t.Kind == kind.String);
						}
						return <>4__this.NewData(name2, tableTypes.FirstOrDefault(func)).Value;
					}
					case 3:
						return kind;
					}
					IEnumerable<HierarchyItem> items = this.Items;
					Func<HierarchyItem, bool> func2;
					if ((func2 = <>9__2) == null)
					{
						func2 = (<>9__2 = (HierarchyItem item) => item.Restriction.Matches(name));
					}
					row = items.FirstOrDefault(func2);
					if (row == null)
					{
						throw ValueException.NewExpressionError<Message0>(Strings.ValueException_KeyNotFound, null, null);
					}
					return row.Description;
				});
			}

			// Token: 0x060074F4 RID: 29940 RVA: 0x00191294 File Offset: 0x0018F494
			public IValueReference NewData(string name, TableType tableType)
			{
				return this.navigator.CreateDataTable(this.selectedCatalog.Item, this.selectedSchema.Item, name, tableType);
			}

			// Token: 0x060074F5 RID: 29941 RVA: 0x001912CC File Offset: 0x0018F4CC
			public Value NativeQuery(Value target, TextValue query, Value parameters, Value options)
			{
				if (this.selectedCatalog.HasFilter && !this.selectedSchema.HasFilter)
				{
					return this.navigator.NativeQuery(target, query, parameters, options, this.selectedCatalog.Item);
				}
				throw ValueException.NewExpressionError<Message0>(Strings.NativeQuery_NotSupported, target, null);
			}

			// Token: 0x060074F6 RID: 29942 RVA: 0x00191324 File Offset: 0x0018F524
			public ActionValue NativeStatement(Value target, TextValue query, Value parameters, Value options)
			{
				if (this.selectedCatalog.HasFilter && !this.selectedSchema.HasFilter)
				{
					return this.navigator.NativeStatement(target, query, parameters, options, this.selectedCatalog.Item);
				}
				throw ValueException.NewExpressionError<Message0>(Strings.Action_NativeStatementsNotSupported, target, null);
			}

			// Token: 0x04004040 RID: 16448
			private readonly IMultiLevelNavigationProvider navigator;

			// Token: 0x04004041 RID: 16449
			private readonly Restriction selectedCatalog;

			// Token: 0x04004042 RID: 16450
			private readonly Restriction selectedSchema;

			// Token: 0x04004043 RID: 16451
			private IEnumerable<HierarchyTableItem> tables;
		}
	}
}
