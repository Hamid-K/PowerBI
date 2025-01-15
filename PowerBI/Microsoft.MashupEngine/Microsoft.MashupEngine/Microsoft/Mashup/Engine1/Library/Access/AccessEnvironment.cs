using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Host;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Language.Typeflow;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Common.Creators;
using Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql;
using Microsoft.Mashup.Engine1.Library.Excel;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OleDb;

namespace Microsoft.Mashup.Engine1.Library.Access
{
	// Token: 0x0200122C RID: 4652
	internal class AccessEnvironment : OleDbEnvironment
	{
		// Token: 0x06007AFA RID: 31482 RVA: 0x001A86E0 File Offset: 0x001A68E0
		protected AccessEnvironment(IEngineHost host, AceSourceFile file, Value options)
			: base(host, Microsoft.Mashup.Engine1.Library.Resource.New("File", file.Path), "Microsoft Access", file.Path, file.IsTempFile, options)
		{
			this.impersonate = file.Impersonate;
			this.mutexName = AceMutex.GetMutexName(file.Path);
		}

		// Token: 0x170021A3 RID: 8611
		// (get) Token: 0x06007AFB RID: 31483 RVA: 0x001A8733 File Offset: 0x001A6933
		public override OptionRecordDefinition ValidOptions
		{
			get
			{
				return AccessModule.OptionRecord;
			}
		}

		// Token: 0x170021A4 RID: 8612
		// (get) Token: 0x06007AFC RID: 31484 RVA: 0x001A873A File Offset: 0x001A693A
		protected override string ProviderName
		{
			get
			{
				return AccessDatabaseEngine.ProviderName;
			}
		}

		// Token: 0x170021A5 RID: 8613
		// (get) Token: 0x06007AFD RID: 31485 RVA: 0x001A8741 File Offset: 0x001A6941
		public override HashSet<string> SearchableTypes
		{
			get
			{
				return AccessEnvironment.searchableTypes;
			}
		}

		// Token: 0x170021A6 RID: 8614
		// (get) Token: 0x06007AFE RID: 31486 RVA: 0x001A8748 File Offset: 0x001A6948
		public override Dictionary<string, TypeValue> NativeToClrTypeMapping
		{
			get
			{
				return AccessEnvironment.nativeToClrTypeMapping;
			}
		}

		// Token: 0x170021A7 RID: 8615
		// (get) Token: 0x06007AFF RID: 31487 RVA: 0x001A874F File Offset: 0x001A694F
		public override bool CreateRelationships
		{
			get
			{
				return base.UserOptions.GetBool("CreateNavigationProperties", false);
			}
		}

		// Token: 0x06007B00 RID: 31488 RVA: 0x001A8762 File Offset: 0x001A6962
		protected override void Check(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor)
		{
			AccessAstExpressionChecker.Check(expression, cursor, this);
		}

		// Token: 0x06007B01 RID: 31489 RVA: 0x001A876C File Offset: 0x001A696C
		protected override SqlSettings LoadSqlSettings()
		{
			return AccessEnvironment.sqlSettings;
		}

		// Token: 0x06007B02 RID: 31490 RVA: 0x001A8773 File Offset: 0x001A6973
		public override DbAstCreator NewAstCreator(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor)
		{
			return AccessAstCreator.Create(expression, cursor, this);
		}

		// Token: 0x06007B03 RID: 31491 RVA: 0x001A8780 File Offset: 0x001A6980
		public static AccessEnvironment Create(IEngineHost host, AceSourceFile file, Value options)
		{
			AccessEnvironment accessEnvironment2;
			using (IHostTrace hostTrace = TracingService.CreateTrace(host, "Engine/IO/Access/Create", TraceEventType.Information, null))
			{
				try
				{
					host.QueryService<IFeatureLoggingService>().LogFeature(AccessDatabaseEngine.ProviderName);
					AccessEnvironment accessEnvironment = new AccessEnvironment(host, file, options);
					hostTrace.AddResource(accessEnvironment.Resource);
					accessEnvironment2 = accessEnvironment;
				}
				catch (DbException ex)
				{
					throw DataSourceException.NewDataSourceError<Message2>(host, DataSourceException.DataSourceMessage("Microsoft Access", ex.Message), null, DbExceptionInfo.GetDetails(ex), null);
				}
			}
			return accessEnvironment2;
		}

		// Token: 0x06007B04 RID: 31492 RVA: 0x000878D5 File Offset: 0x00085AD5
		protected override DbProviderFactory CreateDbProviderFactory()
		{
			return OleDbFactory.Instance;
		}

		// Token: 0x06007B05 RID: 31493 RVA: 0x001A880C File Offset: 0x001A6A0C
		protected override ConnectionInfo CreateConnectionInfo()
		{
			return new ConnectionInfo(AccessEnvironment.GetConnectionString(base.SourcePath), null, this.impersonate, false);
		}

		// Token: 0x06007B06 RID: 31494 RVA: 0x001A8828 File Offset: 0x001A6A28
		public override T ConvertDbExceptions<T>(Func<T> action)
		{
			T t;
			try
			{
				t = action();
			}
			catch (DbException ex)
			{
				string filePath = this.GetFilePath();
				string fileName = Path.GetFileName(filePath);
				throw ValueException.NewDataFormatError(ex.Message.Replace(filePath, fileName), TextValue.New(fileName), ex);
			}
			catch (Exception ex2)
			{
				if (Microsoft.Mashup.Common.SafeExceptions.IsSafeException(ex2))
				{
					string fileName2 = Path.GetFileName(this.GetFilePath());
					AceSourceFile.ThrowIfProviderMissing(ex2, "Microsoft Access", fileName2, base.IsTempFile);
				}
				throw;
			}
			return t;
		}

		// Token: 0x06007B07 RID: 31495 RVA: 0x00002105 File Offset: 0x00000305
		protected override ResourceExceptionKind GetResourceExceptionKind(DbException exception)
		{
			return ResourceExceptionKind.None;
		}

		// Token: 0x06007B08 RID: 31496 RVA: 0x001A88B4 File Offset: 0x001A6AB4
		protected static string GetConnectionString(string databasePath)
		{
			OleDbConnectionStringBuilder oleDbConnectionStringBuilder = new OleDbConnectionStringBuilder();
			oleDbConnectionStringBuilder.DataSource = databasePath;
			oleDbConnectionStringBuilder.Provider = AccessDatabaseEngine.ProviderName;
			oleDbConnectionStringBuilder["Jet OLEDB:Support Complex Data"] = true;
			return oleDbConnectionStringBuilder.ToString();
		}

		// Token: 0x06007B09 RID: 31497 RVA: 0x001A88E3 File Offset: 0x001A6AE3
		protected override bool TryGetDataTypeValue(DataColumnCollection columns, DataRow schemaRow, out TypeValue clrDataType, out bool isSearchable)
		{
			if (DbEnvironment.GetStringSchemaColumn(schemaRow, "DATA_TYPE") == "9")
			{
				clrDataType = null;
				isSearchable = false;
				return false;
			}
			return base.TryGetDataTypeValue(columns, schemaRow, out clrDataType, out isSearchable);
		}

		// Token: 0x06007B0A RID: 31498 RVA: 0x001A8910 File Offset: 0x001A6B10
		public override TableValue WrapDatabaseTable(TableValue tableValue)
		{
			return new AccessEnvironment.AccessTableValue(tableValue, new Value[]
			{
				TextValue.New(this.GetFilePath()),
				base.OptionsRecord
			});
		}

		// Token: 0x06007B0B RID: 31499 RVA: 0x0000EE09 File Offset: 0x0000D009
		public override DataTable LoadSchemas(DbConnection connection)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06007B0C RID: 31500 RVA: 0x001A8938 File Offset: 0x001A6B38
		public override DataTable LoadForeignKeysParent(DbConnection connection, string schema, string table)
		{
			OleDbConnection oleDbConnection = (OleDbConnection)DbEnvironment.GetUnwrappedConnection(connection);
			string[] array = new string[] { null, schema, table };
			OleDbConnection oleDbConnection2 = oleDbConnection;
			Guid foreign_Keys = OleDbSchemaGuid.Foreign_Keys;
			object[] array2 = array;
			return AccessEnvironment.AppendTableNames(oleDbConnection2.GetOleDbSchemaTable(foreign_Keys, array2));
		}

		// Token: 0x06007B0D RID: 31501 RVA: 0x001A8974 File Offset: 0x001A6B74
		public override DataTable LoadForeignKeysChild(DbConnection connection, string schema, string table)
		{
			OleDbConnection oleDbConnection = (OleDbConnection)DbEnvironment.GetUnwrappedConnection(connection);
			string[] array = new string[] { null, null, null, null, schema, table };
			OleDbConnection oleDbConnection2 = oleDbConnection;
			Guid foreign_Keys = OleDbSchemaGuid.Foreign_Keys;
			object[] array2 = array;
			return AccessEnvironment.AppendTableNames(oleDbConnection2.GetOleDbSchemaTable(foreign_Keys, array2));
		}

		// Token: 0x06007B0E RID: 31502 RVA: 0x001A89AF File Offset: 0x001A6BAF
		protected override DbConnection WrapConnection(DbConnection baseConnection)
		{
			return new AccessEnvironment.AceMutexConnection(base.WrapConnection(baseConnection), this);
		}

		// Token: 0x06007B0F RID: 31503 RVA: 0x000091AE File Offset: 0x000073AE
		protected override ConnectionStringResourceCredentialDispatcher CreateConnectionStringDispatcher()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06007B10 RID: 31504 RVA: 0x001A89BE File Offset: 0x001A6BBE
		protected override bool TryGetOleDbClientCore(out OleDbClient client)
		{
			client = new AccessEnvironment.AceOleDbClient(this);
			return true;
		}

		// Token: 0x06007B11 RID: 31505 RVA: 0x001A89CC File Offset: 0x001A6BCC
		private static DataTable AppendTableNames(DataTable table)
		{
			foreach (object obj in table.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				DataRow dataRow2 = dataRow;
				string text = "FK_NAME";
				string[] array = new string[5];
				int num = 0;
				object obj2 = dataRow["FK_NAME"];
				array[num] = ((obj2 != null) ? obj2.ToString() : null);
				array[1] = ", ";
				int num2 = 2;
				object obj3 = dataRow["FK_TABLE_NAME"];
				array[num2] = ((obj3 != null) ? obj3.ToString() : null);
				array[3] = ", ";
				int num3 = 4;
				object obj4 = dataRow["PK_TABLE_NAME"];
				array[num3] = ((obj4 != null) ? obj4.ToString() : null);
				dataRow2[text] = string.Concat(array);
			}
			return table;
		}

		// Token: 0x06007B12 RID: 31506 RVA: 0x001A8A9C File Offset: 0x001A6C9C
		private string GetFilePath()
		{
			return new OleDbConnectionStringBuilder(base.ConnectionInfo.ConnectionString).DataSource;
		}

		// Token: 0x04004422 RID: 17442
		public const string AccessFileExtension = ".accdb";

		// Token: 0x04004423 RID: 17443
		private const string DataSourceName = "Microsoft Access";

		// Token: 0x04004424 RID: 17444
		private static readonly SqlSettings sqlSettings = new SqlSettings
		{
			InvalidIdentifierCharacters = new char[] { '.', '!', '`', '[', ']' },
			MaxIdentifierLength = 64,
			QuoteNationalStringLiteral = SqlSettings.StandardQuote("'"),
			QuoteIdentifier = new Func<string, string>(DbEnvironment.BracketQuoteIdentifier),
			DateTimePrefix = "#",
			DateTimeSuffix = "#",
			PagingStrategy = PagingStrategy.TopAndRowCount,
			SupportsForeignKeys = true
		};

		// Token: 0x04004425 RID: 17445
		private static readonly Dictionary<string, TypeValue> nativeToClrTypeMapping = new Dictionary<string, TypeValue>
		{
			{
				"2",
				TypeValue.Int16
			},
			{
				"3",
				TypeValue.Int32
			},
			{
				"4",
				TypeValue.Single
			},
			{
				"5",
				TypeValue.Double
			},
			{
				"6",
				TypeValue.Currency
			},
			{
				"7",
				TypeValue.DateTime
			},
			{
				"11",
				TypeValue.Logical
			},
			{
				"17",
				TypeValue.Byte
			},
			{
				"72",
				TypeValue.Guid
			},
			{
				"128",
				TypeValue.Binary
			},
			{
				"130",
				TypeValue.Text
			},
			{
				"131",
				TypeValue.Decimal
			}
		};

		// Token: 0x04004426 RID: 17446
		private static readonly HashSet<string> searchableTypes = new HashSet<string>
		{
			"2", "3", "4", "5", "6", "7", "11", "17", "72", "130",
			"131"
		};

		// Token: 0x04004427 RID: 17447
		private readonly Func<IDisposable> impersonate;

		// Token: 0x04004428 RID: 17448
		private readonly string mutexName;

		// Token: 0x0200122D RID: 4653
		private class AceMutexConnection : DelegatingDbConnection
		{
			// Token: 0x06007B14 RID: 31508 RVA: 0x001A8C9E File Offset: 0x001A6E9E
			public AceMutexConnection(DbConnection connection, AccessEnvironment environment)
				: base(connection)
			{
				this.environment = environment;
			}

			// Token: 0x06007B15 RID: 31509 RVA: 0x001A8CB0 File Offset: 0x001A6EB0
			public override void Open()
			{
				using (new AceMutex(this.environment.mutexName, this.environment.Host))
				{
					base.Open();
				}
			}

			// Token: 0x04004429 RID: 17449
			private readonly AccessEnvironment environment;
		}

		// Token: 0x0200122E RID: 4654
		private class AccessTableValue : WrappingTableValue
		{
			// Token: 0x06007B16 RID: 31510 RVA: 0x001A8CFC File Offset: 0x001A6EFC
			public AccessTableValue(TableValue table, Value[] arguments)
				: base(table)
			{
				this.arguments = arguments;
			}

			// Token: 0x170021A8 RID: 8616
			// (get) Token: 0x06007B17 RID: 31511 RVA: 0x001A8D0C File Offset: 0x001A6F0C
			public override IQueryDomain QueryDomain
			{
				get
				{
					return new OptimizableTableQueryDomain(this);
				}
			}

			// Token: 0x170021A9 RID: 8617
			// (get) Token: 0x06007B18 RID: 31512 RVA: 0x001A8D14 File Offset: 0x001A6F14
			public override IExpression Expression
			{
				get
				{
					INativeQueryDomain nativeQueryDomain = base.Table.QueryDomain as INativeQueryDomain;
					IResource resource;
					Value value;
					RecordValue recordValue;
					if (nativeQueryDomain != null && nativeQueryDomain.TryGetNativeQuery(base.Table.Query, out resource, out value, out recordValue))
					{
						IExpression expression = new ConstantExpressionSyntaxNode(AccessEnvironment.AccessTableValue.AccessDatabaseFunctionValue);
						IExpression[] array = this.arguments.Select((Value argument) => new ConstantExpressionSyntaxNode(argument)).ToArray<ConstantExpressionSyntaxNode>();
						IExpression expression2 = new InvocationExpressionSyntaxNodeN(expression, array);
						if (value == null || !value.IsNull)
						{
							expression2 = new InvocationExpressionSyntaxNodeN(new ConstantExpressionSyntaxNode(Library._Value.NativeQuery), new IExpression[]
							{
								expression2,
								QueryToExpressionVisitor.NewNativeQueryExpression(value)
							});
						}
						return expression2;
					}
					return new ConstantExpressionSyntaxNode(new QueryTableValue(base.Table.Query));
				}
			}

			// Token: 0x06007B19 RID: 31513 RVA: 0x001A8DDD File Offset: 0x001A6FDD
			protected override TableValue New(TableValue table)
			{
				return new AccessEnvironment.AccessTableValue(table, this.arguments);
			}

			// Token: 0x06007B1A RID: 31514 RVA: 0x001A8DEC File Offset: 0x001A6FEC
			public override TableValue Optimize()
			{
				Value[] array = new Value[this.arguments.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = Library._Value.Optimize.Invoke(this.arguments[i]);
				}
				return new OptimizedTableValue(new AccessEnvironment.AccessTableValue(base.Table, array));
			}

			// Token: 0x0400442A RID: 17450
			private readonly Value[] arguments;

			// Token: 0x0400442B RID: 17451
			private static readonly FunctionValue AccessDatabaseFunctionValue = new AccessModule.DatabaseFunctionValue(EngineHost.Empty);
		}

		// Token: 0x02001230 RID: 4656
		private class AceOleDbClient : OleDbClient
		{
			// Token: 0x06007B1F RID: 31519 RVA: 0x001A8E58 File Offset: 0x001A7058
			public AceOleDbClient(AccessEnvironment environment)
			{
				this.environment = environment;
				this.dataSource = new AccessEnvironment.AceOleDbClient.AceDataSource();
				this.hostProgress = ProgressService.GetHostProgress(environment.Host, environment.Resource.Kind, environment.SourcePath);
				((IDBProperties)this.dataSource).SetValue(DBPROPGROUP.DBInit, DBPROPID.INIT_DATASOURCE, environment.SourcePath);
				using (this.environment.impersonate())
				{
					((IDBInitialize)this.dataSource).Initialize();
				}
			}

			// Token: 0x06007B20 RID: 31520 RVA: 0x001A8EFC File Offset: 0x001A70FC
			public override IPageReader ExecuteCommand(IList<Type> columnTypes, string text)
			{
				IDisposable handle = HostResourcePermissionService.WaitForGovernedHandle(this.environment.Host, this.environment.Resource);
				object session = null;
				object command = null;
				IRowset rowset = null;
				IPageReader pageReader;
				try
				{
					using (new AceMutex(this.environment.mutexName, this.environment.Host))
					{
						using (this.environment.impersonate())
						{
							IDBCreateSession idbcreateSession = (IDBCreateSession)this.dataSource;
							session = idbcreateSession.CreateSession();
						}
					}
					using (this.environment.impersonate())
					{
						IDBCreateCommand idbcreateCommand = (IDBCreateCommand)session;
						command = idbcreateCommand.CreateCommand();
						ICommandText commandText = (ICommandText)command;
						commandText.SetCommand(text);
						using (new ProgressRequest(this.hostProgress))
						{
							rowset = commandText.Execute(SqlOleDbErrorHandler.Instance);
							pageReader = new ProgressPageReader(new RowsetPageReader(rowset, SqlOleDbErrorHandler.Instance, new Func<DBSTATUS, ISerializedException>(OleDbCellErrorHandler.ConvertError), new Func<Exception, ISerializedException>(this.environment.GetPageReaderExceptionProperties)), this.hostProgress).AfterDispose(delegate
							{
								Marshal.FinalReleaseComObject(rowset);
								Marshal.FinalReleaseComObject(command);
								Marshal.FinalReleaseComObject(session);
								handle.Dispose();
							});
						}
					}
				}
				catch
				{
					if (rowset != null)
					{
						Marshal.FinalReleaseComObject(rowset);
					}
					if (command != null)
					{
						Marshal.FinalReleaseComObject(command);
					}
					if (session != null)
					{
						Marshal.FinalReleaseComObject(session);
					}
					handle.Dispose();
					throw;
				}
				return pageReader;
			}

			// Token: 0x06007B21 RID: 31521 RVA: 0x001A912C File Offset: 0x001A732C
			public override void Dispose()
			{
				if (this.dataSource != null)
				{
					Marshal.FinalReleaseComObject(this.dataSource);
					this.dataSource = null;
				}
			}

			// Token: 0x0400442E RID: 17454
			private readonly AccessEnvironment environment;

			// Token: 0x0400442F RID: 17455
			private readonly IHostProgress hostProgress;

			// Token: 0x04004430 RID: 17456
			private object dataSource;

			// Token: 0x02001231 RID: 4657
			[Guid("3BE786A0-0366-4F5C-9434-25CF162E475E")]
			[ComImport]
			private class AceDataSource
			{
				// Token: 0x06007B22 RID: 31522
				[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
				public extern AceDataSource();
			}
		}
	}
}
