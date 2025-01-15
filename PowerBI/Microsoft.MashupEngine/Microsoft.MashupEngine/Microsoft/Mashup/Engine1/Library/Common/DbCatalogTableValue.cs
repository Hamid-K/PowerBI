using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x02001048 RID: 4168
	internal class DbCatalogTableValue : TableValue
	{
		// Token: 0x06006CCC RID: 27852 RVA: 0x00176504 File Offset: 0x00174704
		public DbCatalogTableValue(DbEnvironment environment, string schemaName, SchemaItem? itemFilter = null)
		{
			this.environment = environment;
			this.schemaName = schemaName;
			this.itemFilter = itemFilter;
			if (schemaName == null)
			{
				this.columns = NavigationPropertiesHelper.DbNavigationKeys;
				this.tableType = this.CreateCatalogTableType(NavigationPropertiesHelper.DbNavigationTableTypeValue);
				return;
			}
			this.columns = NavigationPropertiesHelper.SchemaNavigationKeys;
			this.tableType = this.CreateCatalogTableType(NavigationPropertiesHelper.SchemaNavigationTableTypeValue);
		}

		// Token: 0x17001EF2 RID: 7922
		// (get) Token: 0x06006CCD RID: 27853 RVA: 0x00176568 File Offset: 0x00174768
		public override TypeValue Type
		{
			get
			{
				return this.tableType;
			}
		}

		// Token: 0x17001EF3 RID: 7923
		// (get) Token: 0x06006CCE RID: 27854 RVA: 0x00176570 File Offset: 0x00174770
		public override Keys Columns
		{
			get
			{
				return this.columns;
			}
		}

		// Token: 0x17001EF4 RID: 7924
		// (get) Token: 0x06006CCF RID: 27855 RVA: 0x00176578 File Offset: 0x00174778
		public DbEnvironment Environment
		{
			get
			{
				return this.environment;
			}
		}

		// Token: 0x06006CD0 RID: 27856 RVA: 0x00176580 File Offset: 0x00174780
		public override IEnumerator<IValueReference> GetEnumerator()
		{
			IEnumerator<IValueReference> enumerator;
			try
			{
				enumerator = new DbTableWithPageReaderWrapper(NavigationPropertiesHelper.AddNavigationPropertiesToDatabase(this.environment.GetNavigationPropertiesRecord(this.itemFilter), this.schemaName), this.environment).GetEnumerator();
			}
			catch (DbException ex)
			{
				throw this.environment.ProcessDbException(ex);
			}
			return enumerator;
		}

		// Token: 0x06006CD1 RID: 27857 RVA: 0x001765DC File Offset: 0x001747DC
		public bool TrySelectRows(FunctionValue condition, out TableValue table)
		{
			RecordValue recordValue;
			if (this.schemaName == null && this.itemFilter == null && NavigationTableServices.TryGetIndexRecord(this.Type.AsTableType.ItemType, condition, out recordValue))
			{
				Value value;
				Value value2;
				if (recordValue.Keys.Length == 2 && recordValue.TryGetValue("Schema", out value) && value.IsText && recordValue.TryGetValue("Item", out value2) && value2.IsText)
				{
					table = new DbCatalogTableValue(this.environment, this.schemaName, new SchemaItem?(new SchemaItem(value.AsString, value2.AsString, null)));
					table = table.SelectRows(condition);
					return true;
				}
				Value value3;
				SchemaItem schemaItem;
				if (recordValue.Keys.Length == 1 && recordValue.TryGetValue("Name", out value3) && value3.IsText && this.environment.TryGetSchemaItemFromName(value3.AsString, out schemaItem))
				{
					table = new DbCatalogTableValue(this.environment, this.schemaName, new SchemaItem?(schemaItem));
					table = table.SelectRows(condition);
					return true;
				}
			}
			table = null;
			return false;
		}

		// Token: 0x06006CD2 RID: 27858 RVA: 0x001766F8 File Offset: 0x001748F8
		public override TableValue SelectRows(FunctionValue condition)
		{
			TableValue tableValue;
			if (this.TrySelectRows(condition, out tableValue))
			{
				return tableValue;
			}
			return base.SelectRows(condition);
		}

		// Token: 0x06006CD3 RID: 27859 RVA: 0x00176719 File Offset: 0x00174919
		public override Value NativeQuery(TextValue query, Value parameters, Value options)
		{
			return this.environment.NativeQuery(this, query, parameters, options);
		}

		// Token: 0x06006CD4 RID: 27860 RVA: 0x0017672A File Offset: 0x0017492A
		public override ActionValue NativeStatement(TextValue statement, Value parameters, Value options)
		{
			return this.environment.NativeStatement(this, statement, parameters, options);
		}

		// Token: 0x06006CD5 RID: 27861 RVA: 0x0017673B File Offset: 0x0017493B
		public override void TestConnection()
		{
			this.environment.TestConnection();
		}

		// Token: 0x06006CD6 RID: 27862 RVA: 0x00176748 File Offset: 0x00174948
		private TableTypeValue CreateCatalogTableType(TableTypeValue catalogTableType)
		{
			RecordValue recordValue = RecordValue.New(DbCatalogTableValue.metaType, (int i) => this.GetDatabaseDescription());
			return BinaryOperator.AddMeta.Invoke(catalogTableType, recordValue).AsType.AsTableType;
		}

		// Token: 0x06006CD7 RID: 27863 RVA: 0x00176784 File Offset: 0x00174984
		private Value GetDatabaseDescription()
		{
			DataTable databaseInformation = this.environment.GetDatabaseInformation();
			using (IEnumerator enumerator = databaseInformation.Rows.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					DataRow dataRow = (DataRow)enumerator.Current;
					return TextValue.NewOrNull(DbEnvironment.GetStringSchemaColumnOrNull(databaseInformation.Columns, dataRow, "DESCRIPTION"));
				}
			}
			return Value.Null;
		}

		// Token: 0x04003C6E RID: 15470
		private static readonly RecordTypeValue metaType = RecordTypeValue.New(RecordValue.New(new NamedValue[]
		{
			new NamedValue("Documentation.Description", DbEnvironment.DelayedNullableTextTypeField)
		}), false);

		// Token: 0x04003C6F RID: 15471
		private readonly DbEnvironment environment;

		// Token: 0x04003C70 RID: 15472
		private readonly string schemaName;

		// Token: 0x04003C71 RID: 15473
		private readonly SchemaItem? itemFilter;

		// Token: 0x04003C72 RID: 15474
		private readonly Keys columns;

		// Token: 0x04003C73 RID: 15475
		private readonly TableTypeValue tableType;
	}
}
