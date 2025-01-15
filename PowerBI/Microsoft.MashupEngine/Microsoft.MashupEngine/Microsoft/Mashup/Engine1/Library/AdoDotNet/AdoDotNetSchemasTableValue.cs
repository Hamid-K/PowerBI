using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.IO;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.AdoDotNet
{
	// Token: 0x02000F4A RID: 3914
	internal class AdoDotNetSchemasTableValue : TableValue
	{
		// Token: 0x06006769 RID: 26473 RVA: 0x00163DE8 File Offset: 0x00161FE8
		public AdoDotNetSchemasTableValue(AdoDotNetEnvironment environment)
		{
			this.environment = environment;
		}

		// Token: 0x17001DE9 RID: 7657
		// (get) Token: 0x0600676A RID: 26474 RVA: 0x00163DF7 File Offset: 0x00161FF7
		public override TypeValue Type
		{
			get
			{
				if (this.tableType == null)
				{
					this.Initialize();
				}
				return this.tableType;
			}
		}

		// Token: 0x17001DEA RID: 7658
		// (get) Token: 0x0600676B RID: 26475 RVA: 0x00163E0D File Offset: 0x0016200D
		public override Keys Columns
		{
			get
			{
				if (this.columns == null)
				{
					this.Initialize();
				}
				return this.columns;
			}
		}

		// Token: 0x0600676C RID: 26476 RVA: 0x00163E24 File Offset: 0x00162024
		public override IEnumerator<IValueReference> GetEnumerator()
		{
			if (this.collections == null)
			{
				this.Initialize();
			}
			return new AdoDotNetSchemasTableValue.AdoDotNetSchemasEnumerator(this, DbDataReaderEnumerator.New(this.environment.Host, this.collections.CreateDataReader(), true, this.environment.DataSourceNameString, this.baseTableType.ItemType, this.environment.Resource));
		}

		// Token: 0x0600676D RID: 26477 RVA: 0x00163E82 File Offset: 0x00162082
		public override void TestConnection()
		{
			this.environment.TestConnection();
		}

		// Token: 0x0600676E RID: 26478 RVA: 0x00163E8F File Offset: 0x0016208F
		public override Value NativeQuery(TextValue query, Value parameters, Value options)
		{
			return this.environment.NativeQuery(this, query, parameters, options);
		}

		// Token: 0x0600676F RID: 26479 RVA: 0x00163EA0 File Offset: 0x001620A0
		public override ActionValue NativeStatement(TextValue statement, Value parameters, Value options)
		{
			return this.environment.NativeStatement(this, statement, parameters, options);
		}

		// Token: 0x06006770 RID: 26480 RVA: 0x00163EB1 File Offset: 0x001620B1
		private void Initialize()
		{
			this.environment.ConvertDbExceptions<bool>(delegate
			{
				using (DbConnection dbConnection = this.environment.CreateConnection())
				{
					dbConnection.Open();
					this.collections = dbConnection.GetSchema();
					this.restrictions = dbConnection.GetSchema("Restrictions");
					using (DbDataReader dbDataReader = this.collections.CreateDataReader())
					{
						this.baseTableType = NativeQueryTableValue.GetTableTypeFromSchemaTable(this.environment, dbDataReader.GetSchemaTable());
						this.collectionCollectionColumn = this.collections.Columns["CollectionName"];
						this.collectionRestrictionsColumn = this.collections.Columns["NumberOfRestrictions"];
						this.restrictionCollectionColumn = this.restrictions.Columns["CollectionName"];
						this.restrictionNumberColumn = this.restrictions.Columns["RestrictionNumber"];
						this.restrictionNameColumn = this.restrictions.Columns["RestrictionName"];
						if (this.collectionCollectionColumn != null)
						{
							this.canUnderstandRestrictions = this.collectionRestrictionsColumn != null && this.restrictionCollectionColumn != null && this.restrictionNumberColumn != null && this.restrictionNameColumn != null;
							this.rowType = AdoDotNetSchemasTableValue.CreateRecordType(this.baseTableType.ItemType.Fields);
							this.tableType = TableTypeValue.New(this.rowType);
						}
						else
						{
							this.rowType = this.baseTableType.ItemType;
							this.tableType = this.baseTableType;
						}
						this.columns = this.rowType.Fields.Keys;
					}
				}
				return true;
			});
		}

		// Token: 0x06006771 RID: 26481 RVA: 0x00163ECC File Offset: 0x001620CC
		private static RecordTypeValue CreateRecordType(RecordValue oldFields)
		{
			RecordBuilder recordBuilder = new RecordBuilder(oldFields.Count + 2);
			for (int i = 0; i < oldFields.Count; i++)
			{
				recordBuilder.Add(oldFields.Keys[i], oldFields[i], TypeValue.Any);
			}
			HashSet<string> hashSet = new HashSet<string>();
			recordBuilder.Add(NavigationPropertiesHelper.EnsureUniqueName("Data", oldFields.Keys, hashSet), RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
			{
				TypeValue.Any,
				LogicalValue.False
			}), TypeValue.Any);
			recordBuilder.Add(NavigationPropertiesHelper.EnsureUniqueName("Function", oldFields.Keys, hashSet), RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
			{
				TypeValue.Function,
				LogicalValue.False
			}), TypeValue.Any);
			return RecordTypeValue.New(recordBuilder.ToRecord());
		}

		// Token: 0x06006772 RID: 26482 RVA: 0x00163FA4 File Offset: 0x001621A4
		private string[] GetRestrictionNames(string schemaName, int count)
		{
			HashSet<string> hashSet = new HashSet<string>();
			string[] array = new string[count];
			foreach (object obj in this.restrictions.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				string text = dataRow[this.restrictionCollectionColumn] as string;
				if (text != null && text == schemaName)
				{
					int? num = dataRow[this.restrictionNumberColumn] as int?;
					if (num != null && num.Value > 0 && num.Value <= count)
					{
						text = dataRow[this.restrictionNameColumn] as string;
						if (hashSet.Add(text))
						{
							array[num.Value - 1] = text;
						}
					}
				}
			}
			for (int i = 0; i < count; i++)
			{
				if (array[i] == null)
				{
					array[i] = NavigationPropertiesHelper.EnsureUniqueName(string.Format(CultureInfo.InvariantCulture, "Restriction{0}", i + 1), Keys.Empty, hashSet);
				}
			}
			return array;
		}

		// Token: 0x040038D8 RID: 14552
		private const string RestrictionsSchemaName = "Restrictions";

		// Token: 0x040038D9 RID: 14553
		private const string CollectionNameColumnName = "CollectionName";

		// Token: 0x040038DA RID: 14554
		private const string RestrictionNumberColumnName = "RestrictionNumber";

		// Token: 0x040038DB RID: 14555
		private const string RestrictionNameColumnName = "RestrictionName";

		// Token: 0x040038DC RID: 14556
		private const string NumberOfRestrictionsColumnName = "NumberOfRestrictions";

		// Token: 0x040038DD RID: 14557
		private readonly AdoDotNetEnvironment environment;

		// Token: 0x040038DE RID: 14558
		private Keys columns;

		// Token: 0x040038DF RID: 14559
		private TableTypeValue baseTableType;

		// Token: 0x040038E0 RID: 14560
		private RecordTypeValue rowType;

		// Token: 0x040038E1 RID: 14561
		private TableTypeValue tableType;

		// Token: 0x040038E2 RID: 14562
		private DataTable collections;

		// Token: 0x040038E3 RID: 14563
		private DataTable restrictions;

		// Token: 0x040038E4 RID: 14564
		private DataColumn collectionCollectionColumn;

		// Token: 0x040038E5 RID: 14565
		private DataColumn collectionRestrictionsColumn;

		// Token: 0x040038E6 RID: 14566
		private DataColumn restrictionCollectionColumn;

		// Token: 0x040038E7 RID: 14567
		private DataColumn restrictionNumberColumn;

		// Token: 0x040038E8 RID: 14568
		private DataColumn restrictionNameColumn;

		// Token: 0x040038E9 RID: 14569
		private bool canUnderstandRestrictions;

		// Token: 0x02000F4B RID: 3915
		private class AdoDotNetSchemasEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
		{
			// Token: 0x06006774 RID: 26484 RVA: 0x00164294 File Offset: 0x00162494
			public AdoDotNetSchemasEnumerator(AdoDotNetSchemasTableValue schemas, IEnumerator<IValueReference> baseEnumerator)
			{
				this.schemas = schemas;
				this.environment = schemas.environment;
				this.rowType = schemas.rowType;
				this.canUnderstandRestrictions = schemas.canUnderstandRestrictions;
				this.baseEnumerator = baseEnumerator;
			}

			// Token: 0x17001DEB RID: 7659
			// (get) Token: 0x06006775 RID: 26485 RVA: 0x001642D0 File Offset: 0x001624D0
			public IValueReference Current
			{
				get
				{
					RecordValue asRecord = this.baseEnumerator.Current.Value.AsRecord;
					if (this.schemas.collectionCollectionColumn == null)
					{
						return asRecord;
					}
					string @string = asRecord["CollectionName"].AsText.String;
					Value[] array = new Value[asRecord.Count + 2];
					for (int i = 0; i < asRecord.Count; i++)
					{
						array[i] = asRecord[i];
					}
					FunctionValue functionValue = new AdoDotNetSchemasTableValue.GenericSchemaFunctionValue(this.environment, @string);
					if (this.canUnderstandRestrictions)
					{
						int asInteger = asRecord["NumberOfRestrictions"].AsNumber.AsInteger32;
						array[asRecord.Count] = ((asInteger == 0) ? new AdoDotNetSchemasTableValue.SchemaTableValue(this.environment, @string, null) : new AdoDotNetSchemasTableValue.TypedSchemaFunctionValue(this.environment, @string, this.schemas.GetRestrictionNames(@string, asInteger)));
					}
					else
					{
						array[asRecord.Count] = functionValue;
					}
					array[asRecord.Count + 1] = functionValue;
					return RecordValue.New(this.rowType, array);
				}
			}

			// Token: 0x17001DEC RID: 7660
			// (get) Token: 0x06006776 RID: 26486 RVA: 0x001643CA File Offset: 0x001625CA
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06006777 RID: 26487 RVA: 0x001643D2 File Offset: 0x001625D2
			public bool MoveNext()
			{
				return this.baseEnumerator.MoveNext();
			}

			// Token: 0x06006778 RID: 26488 RVA: 0x001643DF File Offset: 0x001625DF
			public void Reset()
			{
				this.baseEnumerator.MoveNext();
			}

			// Token: 0x06006779 RID: 26489 RVA: 0x001643ED File Offset: 0x001625ED
			public void Dispose()
			{
				this.baseEnumerator.Dispose();
			}

			// Token: 0x040038EA RID: 14570
			private readonly AdoDotNetSchemasTableValue schemas;

			// Token: 0x040038EB RID: 14571
			private readonly AdoDotNetEnvironment environment;

			// Token: 0x040038EC RID: 14572
			private readonly RecordTypeValue rowType;

			// Token: 0x040038ED RID: 14573
			private readonly bool canUnderstandRestrictions;

			// Token: 0x040038EE RID: 14574
			private readonly IEnumerator<IValueReference> baseEnumerator;
		}

		// Token: 0x02000F4C RID: 3916
		private class TypedSchemaFunctionValue : NativeFunctionValueN<TableValue>
		{
			// Token: 0x0600677A RID: 26490 RVA: 0x001643FA File Offset: 0x001625FA
			public TypedSchemaFunctionValue(AdoDotNetEnvironment environment, string schemaName, string[] restrictionNames)
				: base(TypeValue.Table, restrictionNames, AdoDotNetSchemasTableValue.TypedSchemaFunctionValue.MakeSignature(restrictionNames.Length))
			{
				this.environment = environment;
				this.schemaName = schemaName;
			}

			// Token: 0x0600677B RID: 26491 RVA: 0x00164420 File Offset: 0x00162620
			protected override TableValue TypedInvokeN(Value[] args)
			{
				string[] array = new string[args.Length];
				for (int i = 0; i < args.Length; i++)
				{
					if (!args[i].IsNull)
					{
						array[i] = args[i].AsText.String;
					}
				}
				return new AdoDotNetSchemasTableValue.SchemaTableValue(this.environment, this.schemaName, array);
			}

			// Token: 0x0600677C RID: 26492 RVA: 0x00164470 File Offset: 0x00162670
			private static TypeValue[] MakeSignature(int count)
			{
				TypeValue[] array = new TypeValue[count];
				for (int i = 0; i < count; i++)
				{
					array[i] = NullableTypeValue.Text;
				}
				return array;
			}

			// Token: 0x040038EF RID: 14575
			private readonly AdoDotNetEnvironment environment;

			// Token: 0x040038F0 RID: 14576
			private readonly string schemaName;
		}

		// Token: 0x02000F4D RID: 3917
		private class GenericSchemaFunctionValue : NativeFunctionValue1<TableValue, Value>
		{
			// Token: 0x0600677D RID: 26493 RVA: 0x00164499 File Offset: 0x00162699
			public GenericSchemaFunctionValue(AdoDotNetEnvironment environment, string schemaName)
				: base(TypeValue.Table, 0, "parameters", NullableTypeValue.List)
			{
				this.environment = environment;
				this.schemaName = schemaName;
			}

			// Token: 0x0600677E RID: 26494 RVA: 0x001644C0 File Offset: 0x001626C0
			public override TableValue TypedInvoke(Value parameters)
			{
				string[] array;
				if (parameters.IsNull)
				{
					array = null;
				}
				else
				{
					array = new string[parameters.AsList.Count];
					int num = 0;
					foreach (IValueReference valueReference in parameters.AsList)
					{
						if (!valueReference.Value.IsNull)
						{
							array[num++] = valueReference.Value.AsText.String;
						}
					}
				}
				return new AdoDotNetSchemasTableValue.SchemaTableValue(this.environment, this.schemaName, array);
			}

			// Token: 0x040038F1 RID: 14577
			private readonly AdoDotNetEnvironment environment;

			// Token: 0x040038F2 RID: 14578
			private readonly string schemaName;
		}

		// Token: 0x02000F4E RID: 3918
		private class SchemaTableValue : TableValue
		{
			// Token: 0x0600677F RID: 26495 RVA: 0x0016455C File Offset: 0x0016275C
			public SchemaTableValue(AdoDotNetEnvironment environment, string schemaName, string[] restrictions = null)
			{
				this.environment = environment;
				this.schemaName = schemaName;
				this.restrictions = restrictions;
			}

			// Token: 0x17001DED RID: 7661
			// (get) Token: 0x06006780 RID: 26496 RVA: 0x00164579 File Offset: 0x00162779
			public override TypeValue Type
			{
				get
				{
					if (this.tableType == null)
					{
						this.Initialize();
					}
					return this.tableType;
				}
			}

			// Token: 0x17001DEE RID: 7662
			// (get) Token: 0x06006781 RID: 26497 RVA: 0x0016458F File Offset: 0x0016278F
			public override Keys Columns
			{
				get
				{
					if (this.columns == null)
					{
						this.Initialize();
					}
					return this.columns;
				}
			}

			// Token: 0x06006782 RID: 26498 RVA: 0x001645A8 File Offset: 0x001627A8
			public override IEnumerator<IValueReference> GetEnumerator()
			{
				if (this.values == null)
				{
					this.Initialize();
				}
				return DbDataReaderEnumerator.New(this.environment.Host, this.values.CreateDataReader(), true, this.environment.DataSourceNameString, this.tableType.ItemType, this.environment.Resource);
			}

			// Token: 0x06006783 RID: 26499 RVA: 0x00164600 File Offset: 0x00162800
			public override void TestConnection()
			{
				this.environment.TestConnection();
			}

			// Token: 0x06006784 RID: 26500 RVA: 0x00164610 File Offset: 0x00162810
			private void Initialize()
			{
				IPersistentCache persistentCache = this.environment.Host.GetPersistentCache();
				string text = PersistentCacheKey.ServerCatalog.Qualify(this.environment.CacheKey, this.schemaName, DbEnvironment.GetKey(this.restrictions ?? EmptyArray<string>.Instance));
				Stream stream;
				if (!persistentCache.TryGetValue(text, out stream))
				{
					this.values = this.environment.ConvertDbExceptions<DataTable>(delegate
					{
						DataTable dataTable;
						using (DbConnection dbConnection = this.environment.CreateConnection())
						{
							dbConnection.Open();
							dataTable = ((this.restrictions == null) ? dbConnection.GetSchema(this.schemaName) : dbConnection.GetSchema(this.schemaName, this.restrictions));
						}
						return dataTable;
					});
					stream = persistentCache.BeginAdd();
					DbData.WriteTable(stream, this.values);
					persistentCache.EndAdd(text, stream).Close();
				}
				else
				{
					this.values = DbData.ReadTable(stream);
					stream.Close();
				}
				using (DbDataReader dbDataReader = this.values.CreateDataReader())
				{
					this.tableType = NativeQueryTableValue.GetTableTypeFromSchemaTable(this.environment, dbDataReader.GetSchemaTable());
					this.columns = this.tableType.ItemType.Fields.Keys;
				}
			}

			// Token: 0x040038F3 RID: 14579
			private readonly AdoDotNetEnvironment environment;

			// Token: 0x040038F4 RID: 14580
			private readonly string schemaName;

			// Token: 0x040038F5 RID: 14581
			private readonly string[] restrictions;

			// Token: 0x040038F6 RID: 14582
			private DataTable values;

			// Token: 0x040038F7 RID: 14583
			private Keys columns;

			// Token: 0x040038F8 RID: 14584
			private TableTypeValue tableType;
		}
	}
}
