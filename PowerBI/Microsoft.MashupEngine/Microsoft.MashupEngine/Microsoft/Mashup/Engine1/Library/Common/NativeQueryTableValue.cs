using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Capability;
using Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql;
using Microsoft.Mashup.Engine1.Library.Table;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;
using Microsoft.OleDb.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x020010DE RID: 4318
	internal sealed class NativeQueryTableValue : TableValue, IQueryResultTableValue
	{
		// Token: 0x060070F7 RID: 28919 RVA: 0x00183C18 File Offset: 0x00181E18
		public NativeQueryTableValue(IEngineHost host, DbEnvironment environment, Value target, string query)
			: this(host, environment, target, query, Value.Null)
		{
		}

		// Token: 0x060070F8 RID: 28920 RVA: 0x00183C2A File Offset: 0x00181E2A
		public NativeQueryTableValue(IEngineHost host, DbEnvironment environment, Value target, string query, Value parameters)
			: this(host, environment, target, query, parameters, RowCount.Infinite, null)
		{
		}

		// Token: 0x060070F9 RID: 28921 RVA: 0x00183C40 File Offset: 0x00181E40
		public NativeQueryTableValue(IEngineHost host, DbEnvironment environment, Value target, string query, Value parameters, Value options)
			: this(host, environment, target, query, parameters, RowCount.Infinite, options, null)
		{
		}

		// Token: 0x060070FA RID: 28922 RVA: 0x00183C64 File Offset: 0x00181E64
		public NativeQueryTableValue(IEngineHost host, DbEnvironment environment, Value target, string query, Value parameters, RowCount takeCount, NativeQueryTableValue sameTypeParent = null)
			: this(host, environment, target, query, parameters, takeCount, Value.Null, sameTypeParent)
		{
		}

		// Token: 0x060070FB RID: 28923 RVA: 0x00183C88 File Offset: 0x00181E88
		public NativeQueryTableValue(IEngineHost host, DbEnvironment environment, Value target, string query, Value parameters, RowCount takeCount, Value options, NativeQueryTableValue sameTypeParent = null)
		{
			this.host = host;
			this.environment = environment;
			this.target = target;
			this.query = query;
			this.parameters = parameters;
			this.takeCount = takeCount;
			this.sameTypeParent = sameTypeParent;
			this.options = options;
		}

		// Token: 0x17001FBE RID: 8126
		// (get) Token: 0x060070FC RID: 28924 RVA: 0x00183CD8 File Offset: 0x00181ED8
		public EnvironmentBase Environment
		{
			get
			{
				return this.environment;
			}
		}

		// Token: 0x17001FBF RID: 8127
		// (get) Token: 0x060070FD RID: 28925 RVA: 0x00183CE0 File Offset: 0x00181EE0
		public IEngineHost Host
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x17001FC0 RID: 8128
		// (get) Token: 0x060070FE RID: 28926 RVA: 0x00183CE8 File Offset: 0x00181EE8
		public IExpression SyntaxTree
		{
			get
			{
				return this.Expression;
			}
		}

		// Token: 0x17001FC1 RID: 8129
		// (get) Token: 0x060070FF RID: 28927 RVA: 0x00183CF0 File Offset: 0x00181EF0
		public ValueBuilderBase ValueBuilder
		{
			get
			{
				if (this.valueBuilder == null)
				{
					this.valueBuilder = this.environment.Compile(new TableQuery(this, this.Host), this.SyntaxTree);
				}
				return this.valueBuilder;
			}
		}

		// Token: 0x17001FC2 RID: 8130
		// (get) Token: 0x06007100 RID: 28928 RVA: 0x00183D24 File Offset: 0x00181F24
		public override TypeValue Type
		{
			get
			{
				if (this.sameTypeParent != null)
				{
					if (this.type == null)
					{
						this.type = this.sameTypeParent.Type.AsTableType;
					}
				}
				else
				{
					bool enableFolding = this.EnableFolding;
					this.EnsureInitialized(enableFolding);
				}
				return this.type;
			}
		}

		// Token: 0x06007101 RID: 28929 RVA: 0x00183D6D File Offset: 0x00181F6D
		public override bool TryGetProcessor(out QueryProcessor processor)
		{
			processor = QueryResultTableValue.queryProcessor;
			return true;
		}

		// Token: 0x17001FC3 RID: 8131
		// (get) Token: 0x06007102 RID: 28930 RVA: 0x00183D78 File Offset: 0x00181F78
		// (set) Token: 0x06007103 RID: 28931 RVA: 0x00183DB4 File Offset: 0x00181FB4
		private int? RecordsAffected
		{
			get
			{
				int? num = this.recordsAffected;
				if (num != null)
				{
					return num;
				}
				if (this.sameTypeParent == null)
				{
					return null;
				}
				return this.sameTypeParent.RecordsAffected;
			}
			set
			{
				this.recordsAffected = value;
				if (this.sameTypeParent != null)
				{
					this.sameTypeParent.RecordsAffected = value;
				}
			}
		}

		// Token: 0x17001FC4 RID: 8132
		// (get) Token: 0x06007104 RID: 28932 RVA: 0x00183DD1 File Offset: 0x00181FD1
		public override IQueryDomain QueryDomain
		{
			get
			{
				if (this.EnableFolding)
				{
					return NativeQueryTableValue.FoldingNativeQueryDomain.Instance;
				}
				return NativeQueryTableValue.NativeQueryDomain.Instance;
			}
		}

		// Token: 0x17001FC5 RID: 8133
		// (get) Token: 0x06007105 RID: 28933 RVA: 0x00183DE8 File Offset: 0x00181FE8
		public override IExpression Expression
		{
			get
			{
				if (this.expression == null)
				{
					this.CheckCredentialsAndPermission();
					IExpression expression;
					if (this.parameters.IsNull && this.options.IsNull)
					{
						expression = new InvocationExpressionSyntaxNode2(new ConstantExpressionSyntaxNode(Library._Value.NativeQuery), new ConstantExpressionSyntaxNode(this.target), new ConstantExpressionSyntaxNode(TextValue.New(this.query)));
					}
					else
					{
						expression = new InvocationExpressionSyntaxNodeN(new ConstantExpressionSyntaxNode(Library._Value.NativeQuery), new IExpression[]
						{
							new ConstantExpressionSyntaxNode(this.target),
							new ConstantExpressionSyntaxNode(TextValue.New(this.query)),
							new ConstantExpressionSyntaxNode(this.parameters),
							new ConstantExpressionSyntaxNode(this.options)
						});
					}
					if (!this.takeCount.IsInfinite)
					{
						expression = new InvocationExpressionSyntaxNode2(new ConstantExpressionSyntaxNode(TableModule.Table.FirstN), expression, new ConstantExpressionSyntaxNode(NumberValue.New(this.takeCount.Value)));
					}
					this.expression = expression;
				}
				return this.expression;
			}
		}

		// Token: 0x06007106 RID: 28934 RVA: 0x00183EE8 File Offset: 0x001820E8
		public override TableValue Take(RowCount count)
		{
			if (this.EnableFolding)
			{
				return new QueryTableValue(new TableQuery(this, this.host)).Take(count);
			}
			RowRange rowRange = RowRange.All.Take(this.takeCount).Take(count);
			return new NativeQueryTableValue(this.host, this.environment, this.target, this.query, this.parameters, rowRange.TakeCount, this.sameTypeParent ?? this);
		}

		// Token: 0x06007107 RID: 28935 RVA: 0x00183F68 File Offset: 0x00182168
		public override IEnumerator<IValueReference> GetEnumerator()
		{
			this.EnsureInitialized(false);
			if (this.RecordsAffected != null)
			{
				RecordValue recordValue = RecordValue.New(this.Type.AsTableType.ItemType, new Value[] { NumberValue.New(this.RecordsAffected.Value) });
				return new ListTableValue(ListValue.New(new Value[] { recordValue }), this.Type.AsTableType).GetEnumerator();
			}
			TableValue tableValue;
			if (this.environment.TryExecuteCommand(this.Type.AsTableType, () => this.query, out tableValue))
			{
				return tableValue.GetEnumerator();
			}
			return new SkipTakeEnumerator<IValueReference>(new DbValueBuilder.PagingEnumerator(this.GetReaderToEnumerate(), this.environment, this.Type.AsTableType, RowCount.Zero, null, this.takeCount, this.environment.Host.QueryService<ICacheSets>().Data.PersistentCache, new Func<RowCount, RowCount, DbValueBuilder.NativeCommand>(this.GetCommand)), RowCount.Zero, this.takeCount);
		}

		// Token: 0x17001FC6 RID: 8134
		// (get) Token: 0x06007108 RID: 28936 RVA: 0x00184076 File Offset: 0x00182276
		public override long LargeCount
		{
			get
			{
				if (this.EnableFolding)
				{
					return this.ValueBuilder.CreateCountOverEnumerator();
				}
				return base.LargeCount;
			}
		}

		// Token: 0x06007109 RID: 28937 RVA: 0x00184094 File Offset: 0x00182294
		public override IPageReader GetReader()
		{
			this.EnsureInitialized(false);
			if (this.RecordsAffected != null)
			{
				return base.GetReader();
			}
			TableValue tableValue;
			if (this.environment.TryExecuteCommand(this.Type.AsTableType, () => this.query, out tableValue))
			{
				return tableValue.GetReader();
			}
			DbValueBuilder.ConnectionReader readerToEnumerate = this.GetReaderToEnumerate();
			return new DataReaderPageReader(new SkipTakeDataReader(new DbValueBuilder.AdoNetDataReader(this.Execute(readerToEnumerate, false), this.Type.AsTableType.ItemType, this.environment), RowRange.All.Take(this.takeCount)), new DataReaderPageReader.ExceptionPropertyGetter(this.environment.TryGetPageReaderExceptionProperties)).OnDispose(new Action(readerToEnumerate.Dispose));
		}

		// Token: 0x0600710A RID: 28938 RVA: 0x00184156 File Offset: 0x00182356
		public override void TestConnection()
		{
			this.environment.TestConnection();
		}

		// Token: 0x0600710B RID: 28939 RVA: 0x00184163 File Offset: 0x00182363
		public override bool TryInvokeAsArgument(FunctionValue function, Value[] arguments, int index, out Value result)
		{
			result = null;
			if (function.Equals(CapabilityModule.DirectQueryCapabilities.From) && this.EnableFolding)
			{
				result = this.Environment.GetDirectQueryCapabilities();
				return true;
			}
			return false;
		}

		// Token: 0x0600710C RID: 28940 RVA: 0x0018418F File Offset: 0x0018238F
		private DbValueBuilder.NativeCommand GetCommand(RowCount skip, RowCount take)
		{
			return new DbValueBuilder.NativeCommand(this.query, this.parameters);
		}

		// Token: 0x0600710D RID: 28941 RVA: 0x001841A4 File Offset: 0x001823A4
		private DbDataReaderWithTableSchema Execute(DbValueBuilder.ConnectionReader connectionReader, bool selectTopForTableSchema)
		{
			DbDataReaderWithTableSchema dbDataReaderWithTableSchema;
			using (IHostTrace hostTrace = this.environment.Tracer.CreateTrace("NativeQueryTableValue/Execute", TraceEventType.Information))
			{
				CommandBehavior commandBehavior = CommandBehavior.Default;
				try
				{
					string text = this.QueryForTableSchema(selectTopForTableSchema);
					DbEnvironment.DbReaderWrapper dbReaderWrapper = this.environment.CreateReaderWrapper(null, true);
					dbDataReaderWithTableSchema = connectionReader.Execute(text, commandBehavior, this.parameters, dbReaderWrapper);
				}
				catch (ResourceSecurityException)
				{
					throw;
				}
				catch (DeadlockException)
				{
					throw;
				}
				catch (Exception ex)
				{
					Microsoft.Mashup.Common.SafeExceptions.TraceIsSafeException(hostTrace, ex);
					throw;
				}
			}
			return dbDataReaderWithTableSchema;
		}

		// Token: 0x17001FC7 RID: 8135
		// (get) Token: 0x0600710E RID: 28942 RVA: 0x00184248 File Offset: 0x00182448
		private bool EnableFolding
		{
			get
			{
				return this.environment.SupportsNativeQueryFolding && Options.IsNativeQueryFoldingEnabled(this.parameters, this.options);
			}
		}

		// Token: 0x17001FC8 RID: 8136
		// (get) Token: 0x0600710F RID: 28943 RVA: 0x0018426C File Offset: 0x0018246C
		private bool PreserveTypes
		{
			get
			{
				Value value;
				return this.environment.SupportsNativeQueryTypePreservation && this.options.IsRecord && this.options.AsRecord.TryGetValue("PreserveTypes", out value) && value.IsLogical && value.AsBoolean;
			}
		}

		// Token: 0x06007110 RID: 28944 RVA: 0x001842BC File Offset: 0x001824BC
		private string SelectStarWithPagingStrategy()
		{
			string text;
			using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
			{
				ScriptWriter scriptWriter = new ScriptWriter(stringWriter, this.environment.SqlSettings);
				new PagingQuerySpecification
				{
					SelectItems = 
					{
						new SelectItem(SqlConstant.SelectAll)
					},
					FromItems = 
					{
						new FromQuery
						{
							Query = new VerbatimSqlQueryExpression(this.query, null),
							Alias = Alias.Underscore
						}
					},
					PagingClause = new PagingClause
					{
						FetchExpression = new long?(0L)
					}
				}.WriteCreateScript(scriptWriter);
				text = stringWriter.ToString();
			}
			return text;
		}

		// Token: 0x06007111 RID: 28945 RVA: 0x00184370 File Offset: 0x00182570
		private string GetSchemaCacheKey(string tableQuerySchema)
		{
			string cacheKey = new DbValueBuilder.NativeCommand(tableQuerySchema, this.parameters).CacheKey;
			if (cacheKey == null)
			{
				return null;
			}
			return PersistentCacheKey.ServerCatalog.Qualify(this.environment.CacheKey, "NativeQueryType", cacheKey);
		}

		// Token: 0x06007112 RID: 28946 RVA: 0x001843B5 File Offset: 0x001825B5
		private string QueryForTableSchema(bool selectTopForTableSchema)
		{
			if (!selectTopForTableSchema)
			{
				return this.query;
			}
			return this.SelectStarWithPagingStrategy();
		}

		// Token: 0x06007113 RID: 28947 RVA: 0x001843C7 File Offset: 0x001825C7
		private void EnsureInitialized(bool selectTopForTableSchema)
		{
			if (this.sameTypeParent != null)
			{
				this.sameTypeParent.EnsureInitialized(selectTopForTableSchema);
				return;
			}
			if (!this.initialized)
			{
				this.CheckCredentialsAndPermission();
				this.Initialize(selectTopForTableSchema);
				this.initialized = true;
			}
		}

		// Token: 0x06007114 RID: 28948 RVA: 0x001843FC File Offset: 0x001825FC
		private void CheckCredentialsAndPermission()
		{
			if (!this.permissionChecked)
			{
				int num = 0;
				string[] array = null;
				if (this.parameters.IsList)
				{
					num = this.parameters.AsList.Count;
				}
				else if (this.parameters.IsRecord)
				{
					array = this.parameters.AsRecord.Keys.ToArray<string>();
					num = array.Length;
				}
				else if (!this.parameters.IsNull)
				{
					throw ValueException.NewExpressionError<Message0>(Strings.AdoDotNetParametersMustBeRecordOrList, this.parameters, null);
				}
				DbEnvironment dbEnvironment = this.Environment as DbEnvironment;
				if (dbEnvironment == null || !dbEnvironment.SuppressNativeQueryPermissionChallenge)
				{
					HostResourceQueryPermissionService.VerifyQueryPermission(this.host, this.environment.Resource, QueryPermissionChallengeType.EvaluateNativeQueryUnpermitted, this.query, num, array);
				}
				this.environment.EnsureCredentials();
				this.permissionChecked = true;
			}
		}

		// Token: 0x06007115 RID: 28949 RVA: 0x001844D0 File Offset: 0x001826D0
		private DbValueBuilder.ConnectionReader GetReaderToEnumerate()
		{
			if (this.sameTypeParent != null)
			{
				return this.sameTypeParent.GetReaderToEnumerate();
			}
			if (this.connectionReader != null && this.EnableFolding)
			{
				this.connectionReader.Dispose();
				this.connectionReader = null;
			}
			DbValueBuilder.ConnectionReader connectionReader = this.connectionReader ?? new DbValueBuilder.ConnectionReader(this.environment, null);
			this.connectionReader = null;
			return connectionReader;
		}

		// Token: 0x06007116 RID: 28950 RVA: 0x00184530 File Offset: 0x00182730
		private void Initialize(bool selectTopForTableSchema)
		{
			if (this.query != string.Empty)
			{
				IPersistentObjectCache cache = this.host.QueryService<ICacheSets>().Metadata.PersistentObjectCache;
				string text = this.QueryForTableSchema(selectTopForTableSchema);
				string cacheKey = this.GetSchemaCacheKey(text);
				TableSchema schema = null;
				TableValue tableValue;
				if (this.environment.TryExecuteNativeQuery(text, this.parameters, this.options, out tableValue))
				{
					if (selectTopForTableSchema)
					{
						this.type = tableValue.Type.AsTableType;
						return;
					}
				}
				else if (cacheKey != null && cache.TryGetValue(cacheKey, out schema))
				{
					this.RecordsAffected = null;
				}
				else
				{
					this.connectionReader = new DbValueBuilder.SingletonReader(this.environment);
					this.connectionReader.RegisterForCleanup();
					DbDataReaderWithTableSchema reader = this.Execute(this.connectionReader, selectTopForTableSchema);
					this.RecordsAffected = this.environment.ConvertDbExceptions<int?>(delegate
					{
						if (reader.VisibleFieldCount == 0)
						{
							return new int?(reader.RecordsAffected);
						}
						DataTable schemaTable = reader.GetSchemaTable();
						schema = TableSchema.FromDataTable(schemaTable);
						if (!schemaTable.Columns.Contains(InformationSchemaTableColumnName.DataTypeName))
						{
							for (int i = 0; i < schema.ColumnCount; i++)
							{
								SchemaColumn column = schema.GetColumn(i);
								column.DataTypeName = reader.GetDataTypeName(reader.GetOrdinal(column.Name));
							}
						}
						if (cacheKey != null)
						{
							cache.CommitValue(cacheKey, schema);
						}
						return null;
					});
					int? num = this.RecordsAffected;
					int num2 = 0;
					if ((num.GetValueOrDefault() >= num2) & (num != null))
					{
						this.type = TableTypeValue.New(NativeQueryTableValue.AffectedRecordsRowType);
						this.connectionReader.Dispose();
						this.connectionReader = null;
						return;
					}
				}
				if (schema != null)
				{
					this.type = NativeQueryTableValue.GetTableTypeFromSchemaTable(this.environment, schema, this.PreserveTypes);
					return;
				}
			}
			throw ValueException.NewExpressionError<Message0>(Strings.NativeQuery_NullSchema, null, null);
		}

		// Token: 0x06007117 RID: 28951 RVA: 0x001846C0 File Offset: 0x001828C0
		public static TableTypeValue GetTableTypeFromSchemaTable(DbEnvironment environment, DataTable schema)
		{
			return NativeQueryTableValue.GetTableTypeFromSchemaTable(environment, TableSchema.FromDataTable(schema), false);
		}

		// Token: 0x06007118 RID: 28952 RVA: 0x001846D0 File Offset: 0x001828D0
		private static TableTypeValue GetTableTypeFromSchemaTable(DbEnvironment environment, TableSchema schema, bool preserveTypes = false)
		{
			Keys columnNamesFromSchemaTable = NativeQueryTableValue.GetColumnNamesFromSchemaTable(schema);
			int length = columnNamesFromSchemaTable.Length;
			Value[] array = new Value[length];
			for (int i = 0; i < length; i++)
			{
				SchemaColumn column = schema.GetColumn(i);
				TypeValue typeValue = TypeValue.None;
				if (preserveTypes)
				{
					if (!environment.NativeToClrTypeMapping.TryGetValue(column.DataTypeName, out typeValue))
					{
						typeValue = TypeValue.None;
					}
				}
				else
				{
					Type dataType = column.DataType;
					if (dataType == null)
					{
						throw ValueException.NewExpressionError<Message1>(Strings.NativeQuery_TypeNotSupported(column.Name), null, null);
					}
					if (environment.IsTypeSupported(dataType))
					{
						typeValue = ValueMarshaller.GetMType(dataType);
					}
				}
				if (typeValue.TypeKind != ValueKind.None && column.Nullable)
				{
					typeValue = typeValue.Nullable;
				}
				long? num = column.ColumnSize;
				int? num2 = column.NumericPrecision;
				int? num3 = column.NumericScale;
				long? num4 = num;
				long num5 = 1073741823L;
				if ((num4.GetValueOrDefault() >= num5) & (num4 != null))
				{
					num = null;
				}
				int? num6 = num2;
				int num7 = 255;
				if ((num6.GetValueOrDefault() == num7) & (num6 != null))
				{
					num2 = null;
				}
				num6 = num3;
				num7 = 255;
				if ((num6.GetValueOrDefault() == num7) & (num6 != null))
				{
					num3 = null;
				}
				int? num8 = ((num2 != null || num3 != null) ? new int?(10) : null);
				TypeFacets typeFacets = TypeFacets.None;
				switch (typeValue.TypeKind)
				{
				case ValueKind.Time:
				case ValueKind.DateTime:
				case ValueKind.DateTimeZone:
					typeFacets = TypeFacets.NewDateTime(num3, null);
					break;
				case ValueKind.Number:
					typeFacets = TypeFacets.NewNumeric(num8, num2, num3, null);
					break;
				case ValueKind.Text:
					typeFacets = TypeFacets.NewText(num, environment.IsVariableLengthType(column.DataTypeName), null);
					break;
				case ValueKind.Binary:
					typeFacets = TypeFacets.NewBinary(num, environment.IsVariableLengthType(column.DataTypeName), null);
					break;
				}
				typeFacets = typeFacets.AddNative(column.DataTypeName, null, null);
				typeValue = typeValue.NewFacets(typeFacets);
				array[i] = RecordTypeAlgebra.NewField(typeValue, false);
			}
			return TableTypeValue.New(RecordTypeValue.New(RecordValue.New(columnNamesFromSchemaTable, array)));
		}

		// Token: 0x06007119 RID: 28953 RVA: 0x00184910 File Offset: 0x00182B10
		private static Keys GetColumnNamesFromSchemaTable(TableSchema schema)
		{
			string[] array = new string[schema.ColumnCount];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = schema.GetColumn(i).Name;
			}
			return ColumnLabelGenerator.GenerateKeys(array, array.Length);
		}

		// Token: 0x04003E34 RID: 15924
		private const string NativeQueryTypeCacheKey = "NativeQueryType";

		// Token: 0x04003E35 RID: 15925
		private const string AffectedRecordsKey = "Records Affected";

		// Token: 0x04003E36 RID: 15926
		private static readonly RecordTypeValue AffectedRecordsRowType = RecordTypeValue.New(RecordValue.New(Keys.New("Records Affected"), new Value[] { RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
		{
			TypeValue.Number,
			LogicalValue.False
		}) }));

		// Token: 0x04003E37 RID: 15927
		private readonly IEngineHost host;

		// Token: 0x04003E38 RID: 15928
		private readonly DbEnvironment environment;

		// Token: 0x04003E39 RID: 15929
		private readonly Value target;

		// Token: 0x04003E3A RID: 15930
		private readonly string query;

		// Token: 0x04003E3B RID: 15931
		private readonly Value parameters;

		// Token: 0x04003E3C RID: 15932
		private readonly Value options;

		// Token: 0x04003E3D RID: 15933
		private readonly RowCount takeCount;

		// Token: 0x04003E3E RID: 15934
		private readonly NativeQueryTableValue sameTypeParent;

		// Token: 0x04003E3F RID: 15935
		private bool permissionChecked;

		// Token: 0x04003E40 RID: 15936
		private bool initialized;

		// Token: 0x04003E41 RID: 15937
		private TableTypeValue type;

		// Token: 0x04003E42 RID: 15938
		private DbValueBuilder.ConnectionReader connectionReader;

		// Token: 0x04003E43 RID: 15939
		private int? recordsAffected;

		// Token: 0x04003E44 RID: 15940
		private IExpression expression;

		// Token: 0x04003E45 RID: 15941
		private ValueBuilderBase valueBuilder;

		// Token: 0x020010DF RID: 4319
		private class NativeQueryDomain : INativeQueryDomain, IQueryDomain
		{
			// Token: 0x0600711D RID: 28957 RVA: 0x000020FD File Offset: 0x000002FD
			protected NativeQueryDomain()
			{
			}

			// Token: 0x17001FC9 RID: 8137
			// (get) Token: 0x0600711E RID: 28958 RVA: 0x00002105 File Offset: 0x00000305
			public bool CanIndex
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0600711F RID: 28959 RVA: 0x001849A7 File Offset: 0x00182BA7
			public virtual bool IsCompatibleWith(IQueryDomain domain)
			{
				return domain == NativeQueryTableValue.NativeQueryDomain.Instance;
			}

			// Token: 0x06007120 RID: 28960 RVA: 0x0000A6A5 File Offset: 0x000088A5
			public virtual Query Optimize(Query query)
			{
				return query;
			}

			// Token: 0x06007121 RID: 28961 RVA: 0x001849B4 File Offset: 0x00182BB4
			public bool TryGetNativeQuery(Query query, out IResource resource, out Value nativeQuery, out RecordValue options)
			{
				TableQuery tableQuery = query as TableQuery;
				if (tableQuery != null)
				{
					NativeQueryTableValue nativeQueryTableValue = tableQuery.Table as NativeQueryTableValue;
					if (nativeQueryTableValue != null)
					{
						nativeQueryTableValue.EnsureInitialized(false);
						resource = nativeQueryTableValue.environment.Resource;
						nativeQuery = TextValue.New(nativeQueryTableValue.query);
						options = nativeQueryTableValue.environment.OptionsRecord;
						return true;
					}
				}
				resource = null;
				nativeQuery = null;
				options = RecordValue.Empty;
				return false;
			}

			// Token: 0x04003E46 RID: 15942
			public static readonly INativeQueryDomain Instance = new NativeQueryTableValue.NativeQueryDomain();
		}

		// Token: 0x020010E0 RID: 4320
		private class FoldingNativeQueryDomain : NativeQueryTableValue.NativeQueryDomain
		{
			// Token: 0x06007123 RID: 28963 RVA: 0x00184A26 File Offset: 0x00182C26
			private FoldingNativeQueryDomain()
			{
			}

			// Token: 0x06007124 RID: 28964 RVA: 0x00184A2E File Offset: 0x00182C2E
			public override Query Optimize(Query query)
			{
				return QueryFolder.Fold(query);
			}

			// Token: 0x06007125 RID: 28965 RVA: 0x00184A36 File Offset: 0x00182C36
			public override bool IsCompatibleWith(IQueryDomain domain)
			{
				return domain == NativeQueryTableValue.FoldingNativeQueryDomain.Instance || domain == ExpressionQueryDomain.Instance;
			}

			// Token: 0x04003E47 RID: 15943
			public new static readonly INativeQueryDomain Instance = new NativeQueryTableValue.FoldingNativeQueryDomain();
		}
	}
}
