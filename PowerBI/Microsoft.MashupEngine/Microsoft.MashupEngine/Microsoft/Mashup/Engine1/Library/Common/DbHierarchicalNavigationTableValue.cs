using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x0200108D RID: 4237
	internal sealed class DbHierarchicalNavigationTableValue : TableValue
	{
		// Token: 0x06006EE4 RID: 28388 RVA: 0x0017E9AF File Offset: 0x0017CBAF
		public DbHierarchicalNavigationTableValue(DbEnvironment environment, Func<string, TableValue> createCatalogTable)
			: this(environment, createCatalogTable, null)
		{
		}

		// Token: 0x06006EE5 RID: 28389 RVA: 0x0017E9BA File Offset: 0x0017CBBA
		private DbHierarchicalNavigationTableValue(DbEnvironment environment, Func<string, TableValue> createCatalogTable, string schemaFilter)
		{
			this.environment = environment;
			this.createCatalogTable = createCatalogTable;
			this.schemaFilter = schemaFilter;
		}

		// Token: 0x17001F46 RID: 8006
		// (get) Token: 0x06006EE6 RID: 28390 RVA: 0x0017E9D7 File Offset: 0x0017CBD7
		public override TypeValue Type
		{
			get
			{
				return DbHierarchicalNavigationTableValue.tableType;
			}
		}

		// Token: 0x17001F47 RID: 8007
		// (get) Token: 0x06006EE7 RID: 28391 RVA: 0x0017E9DE File Offset: 0x0017CBDE
		public override Keys Columns
		{
			get
			{
				return DbHierarchicalNavigationTableValue.columnNames;
			}
		}

		// Token: 0x06006EE8 RID: 28392 RVA: 0x0017E9E8 File Offset: 0x0017CBE8
		public bool TrySelectRows(FunctionValue condition, out TableValue table)
		{
			RecordValue recordValue;
			Value value;
			if (NavigationTableServices.TryGetIndexRecord(this.Type.AsTableType.ItemType, condition, out recordValue) && recordValue.Keys.Length == 1 && recordValue.TryGetValue(this.Columns[0], out value) && value.IsText && (this.schemaFilter == null || this.schemaFilter == value.AsString))
			{
				table = new DbHierarchicalNavigationTableValue(this.environment, this.createCatalogTable, value.AsString);
				return true;
			}
			table = null;
			return false;
		}

		// Token: 0x06006EE9 RID: 28393 RVA: 0x0017EA78 File Offset: 0x0017CC78
		public override TableValue SelectRows(FunctionValue condition)
		{
			TableValue tableValue;
			if (this.TrySelectRows(condition, out tableValue))
			{
				return tableValue;
			}
			return base.SelectRows(condition);
		}

		// Token: 0x06006EEA RID: 28394 RVA: 0x0017EA99 File Offset: 0x0017CC99
		public override Value NativeQuery(TextValue query, Value parameters, Value options)
		{
			return this.environment.NativeQuery(this, query, parameters, options);
		}

		// Token: 0x06006EEB RID: 28395 RVA: 0x0017EAAA File Offset: 0x0017CCAA
		public override ActionValue NativeStatement(TextValue statement, Value parameters, Value options)
		{
			return this.environment.NativeStatement(this, statement, parameters, options);
		}

		// Token: 0x06006EEC RID: 28396 RVA: 0x0017EABB File Offset: 0x0017CCBB
		public override IEnumerator<IValueReference> GetEnumerator()
		{
			return (from schema in this.environment.AllSchemas
				where this.schemaFilter == null || schema == this.schemaFilter
				select this.CreateSchemaRecord(schema)).GetEnumerator();
		}

		// Token: 0x06006EED RID: 28397 RVA: 0x0017EAEF File Offset: 0x0017CCEF
		public override void TestConnection()
		{
			this.environment.TestConnection();
		}

		// Token: 0x06006EEE RID: 28398 RVA: 0x0017EAFC File Offset: 0x0017CCFC
		private RecordValue CreateSchemaRecord(string schemaName)
		{
			return RecordValue.New(DbHierarchicalNavigationTableValue.recordType, delegate(int i)
			{
				if (i == 0)
				{
					return TextValue.New(schemaName);
				}
				return this.createCatalogTable(schemaName);
			});
		}

		// Token: 0x04003D7F RID: 15743
		private static readonly Keys columnNames = Keys.New("Schema", "Data");

		// Token: 0x04003D80 RID: 15744
		private static RecordTypeValue recordType = RecordTypeValue.New(RecordValue.New(DbHierarchicalNavigationTableValue.columnNames, new Value[]
		{
			RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
			{
				TypeValue.Text,
				LogicalValue.False
			}),
			RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
			{
				NavigationTableServices.ConvertToLink(NavigationTableServices.DefaultTypeValue.Nullable, "Schema", false),
				LogicalValue.False
			})
		}));

		// Token: 0x04003D81 RID: 15745
		private static readonly TableTypeValue tableType = NavigationTableServices.AddNavigationTableMetadata(TableTypeValue.New(DbHierarchicalNavigationTableValue.recordType, new TableKey[]
		{
			new TableKey(new int[1], true)
		}), TextValue.New("Schema"), TextValue.New("Data"));

		// Token: 0x04003D82 RID: 15746
		private readonly DbEnvironment environment;

		// Token: 0x04003D83 RID: 15747
		private readonly Func<string, TableValue> createCatalogTable;

		// Token: 0x04003D84 RID: 15748
		private readonly string schemaFilter;
	}
}
