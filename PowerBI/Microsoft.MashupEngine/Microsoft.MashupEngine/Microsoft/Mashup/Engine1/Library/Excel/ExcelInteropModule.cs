using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.DataSourceReference;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Resources;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Excel
{
	// Token: 0x02000C13 RID: 3091
	internal sealed class ExcelInteropModule : Module
	{
		// Token: 0x06005447 RID: 21575 RVA: 0x001210D0 File Offset: 0x0011F2D0
		public static ValueException GetExcelError(int errorCode)
		{
			string text;
			if (!ExcelInteropModule.errorCodes.TryGetValue(errorCode, out text))
			{
				text = "#ERROR!";
			}
			return ExcelModule.GetExcelError(TextValue.New(text));
		}

		// Token: 0x170019BF RID: 6591
		// (get) Token: 0x06005448 RID: 21576 RVA: 0x001210FD File Offset: 0x0011F2FD
		public override string Name
		{
			get
			{
				return "ExcelInterop";
			}
		}

		// Token: 0x170019C0 RID: 6592
		// (get) Token: 0x06005449 RID: 21577 RVA: 0x00121104 File Offset: 0x0011F304
		public override Keys ExportKeys
		{
			get
			{
				if (this.exportKeys == null)
				{
					this.exportKeys = Keys.New(1, delegate(int index)
					{
						if (index == 0)
						{
							return "Excel.CurrentWorkbook";
						}
						throw new InvalidOperationException();
					});
				}
				return this.exportKeys;
			}
		}

		// Token: 0x170019C1 RID: 6593
		// (get) Token: 0x0600544A RID: 21578 RVA: 0x0012113F File Offset: 0x0011F33F
		public override ResourceKindInfo[] DataSources
		{
			get
			{
				return new ResourceKindInfo[] { ExcelInteropModule.resourceKindInfo };
			}
		}

		// Token: 0x0600544B RID: 21579 RVA: 0x00121150 File Offset: 0x0011F350
		protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost hostEnvironment)
		{
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				if (index == 0)
				{
					return new ExcelInteropModule.CurrentWorkbookFunctionValue(hostEnvironment);
				}
				throw new InvalidOperationException();
			});
		}

		// Token: 0x04002E99 RID: 11929
		private static readonly Dictionary<int, string> errorCodes = new Dictionary<int, string>
		{
			{ -2146826281, "#DIV/0!" },
			{ -2146826246, "#N/A" },
			{ -2146826259, "#NAME?" },
			{ -2146826288, "#NULL!" },
			{ -2146826252, "#NUM!" },
			{ -2146826265, "#REF!" },
			{ -2146826273, "#VALUE!" }
		};

		// Token: 0x04002E9A RID: 11930
		public const string ExcelCurrentWorkbook = "Excel.CurrentWorkbook";

		// Token: 0x04002E9B RID: 11931
		private static readonly ResourceKindInfo resourceKindInfo = new CurrentWorkbookResourceKindInfo();

		// Token: 0x04002E9C RID: 11932
		private Keys exportKeys;

		// Token: 0x02000C14 RID: 3092
		private enum Exports
		{
			// Token: 0x04002E9E RID: 11934
			ExcelCurrentWorkbook,
			// Token: 0x04002E9F RID: 11935
			Count
		}

		// Token: 0x02000C15 RID: 3093
		private sealed class CurrentWorkbookFunctionValue : NativeFunctionValue0<TableValue>
		{
			// Token: 0x0600544E RID: 21582 RVA: 0x00121215 File Offset: 0x0011F415
			public CurrentWorkbookFunctionValue(IEngineHost host)
				: base(TypeValue.Table)
			{
				this.host = host;
			}

			// Token: 0x0600544F RID: 21583 RVA: 0x00121229 File Offset: 0x0011F429
			public override TableValue TypedInvoke()
			{
				HostResourcePermissionService.VerifyPermissionAndGetCredentials(this.host, ExcelInteropModule.CurrentWorkbookFunctionValue.resource, null);
				return new ExcelInteropModule.CurrentWorkbookFunctionValue.ExcelCurrentWorkbookValue(HostExcelService.GetExcelService(this.host));
			}

			// Token: 0x06005450 RID: 21584 RVA: 0x0012124D File Offset: 0x0011F44D
			private static ValueException ExcelTableNotFoundError(string tableName)
			{
				return ValueException.NewExpressionError<Message1>(Strings.ValueException_ExcelTableNotFound(tableName), TextValue.New(tableName), null);
			}

			// Token: 0x170019C2 RID: 6594
			// (get) Token: 0x06005451 RID: 21585 RVA: 0x00121261 File Offset: 0x0011F461
			public override string PrimaryResourceKind
			{
				get
				{
					return "CurrentWorkbook";
				}
			}

			// Token: 0x06005452 RID: 21586 RVA: 0x00121268 File Offset: 0x0011F468
			public override bool TryGetLocation(IExpression expression, out IDataSourceLocation location, out RecordValue foundOptions, out Keys unknownOptions)
			{
				Dictionary<string, IExpression> dictionary;
				if (ExcelInteropModule.CurrentWorkbookFunctionValue.pattern.TryMatch(expression, out dictionary))
				{
					CurrentWorkbookDataSourceLocation currentWorkbookDataSourceLocation = new CurrentWorkbookDataSourceLocation();
					Value value;
					if (dictionary.TryGetConstant("tableName", out value) && value.IsText)
					{
						currentWorkbookDataSourceLocation.TableName = value.AsString;
					}
					location = currentWorkbookDataSourceLocation;
					foundOptions = RecordValue.Empty;
					unknownOptions = Keys.Empty;
					return true;
				}
				location = null;
				foundOptions = null;
				unknownOptions = null;
				return false;
			}

			// Token: 0x04002EA0 RID: 11936
			private const string excelTablesContentKey = "Content";

			// Token: 0x04002EA1 RID: 11937
			private const string excelTablesNameKey = "Name";

			// Token: 0x04002EA2 RID: 11938
			private static readonly IResource resource = Resource.New("CurrentWorkbook", string.Empty);

			// Token: 0x04002EA3 RID: 11939
			private static readonly Keys excelTablesKeys = Keys.New("Content", "Name");

			// Token: 0x04002EA4 RID: 11940
			private static readonly IList<TableKey> excelTablesTableKeys = new TableKey[]
			{
				new TableKey(new int[] { 1 }, true)
			};

			// Token: 0x04002EA5 RID: 11941
			private static readonly TableTypeValue excelTablesType = NavigationTableServices.AddNavigationTableMetadata(TableTypeValue.New(RecordTypeValue.New(RecordValue.New(ExcelInteropModule.CurrentWorkbookFunctionValue.excelTablesKeys, new Value[]
			{
				RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
				{
					TypeValue.Any,
					LogicalValue.False
				}),
				RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
				{
					TypeValue.Text,
					LogicalValue.False
				})
			}), false), ExcelInteropModule.CurrentWorkbookFunctionValue.excelTablesTableKeys), TextValue.New("Name"), TextValue.New("Content"));

			// Token: 0x04002EA6 RID: 11942
			private static readonly ExpressionPattern pattern = new ExpressionPattern(new string[] { "__func()", "__func(){[Name=__tableName]}[Content]" });

			// Token: 0x04002EA7 RID: 11943
			private readonly IEngineHost host;

			// Token: 0x02000C16 RID: 3094
			private class ExcelCurrentWorkbookValue : TableValue
			{
				// Token: 0x06005454 RID: 21588 RVA: 0x001213C3 File Offset: 0x0011F5C3
				public ExcelCurrentWorkbookValue(IExcelService excelService)
				{
					this.excelService = excelService;
				}

				// Token: 0x170019C3 RID: 6595
				// (get) Token: 0x06005455 RID: 21589 RVA: 0x001213D2 File Offset: 0x0011F5D2
				public override TypeValue Type
				{
					get
					{
						return ExcelInteropModule.CurrentWorkbookFunctionValue.excelTablesType;
					}
				}

				// Token: 0x170019C4 RID: 6596
				// (get) Token: 0x06005456 RID: 21590 RVA: 0x001213D9 File Offset: 0x0011F5D9
				public override Keys Columns
				{
					get
					{
						return ExcelInteropModule.CurrentWorkbookFunctionValue.excelTablesKeys;
					}
				}

				// Token: 0x170019C5 RID: 6597
				public override Value this[Value key]
				{
					get
					{
						if (key.IsRecord)
						{
							RecordValue asRecord = key.AsRecord;
							Keys keys = asRecord.Keys;
							if (keys.Length == 1 && keys[0] == "Name" && asRecord[0].IsText)
							{
								TableValue table = ExcelInteropModule.CurrentWorkbookFunctionValue.ExcelTableValue.GetTable(this.excelService, asRecord[0].AsString);
								return RecordValue.New(ExcelInteropModule.CurrentWorkbookFunctionValue.excelTablesKeys, new Value[]
								{
									table,
									asRecord[0]
								});
							}
						}
						return base[key];
					}
				}

				// Token: 0x06005458 RID: 21592 RVA: 0x0012146C File Offset: 0x0011F66C
				public override IEnumerator<IValueReference> GetEnumerator()
				{
					string[] array;
					if (!this.excelService.TryGetTableNames(out array))
					{
						return ListValue.Empty.GetEnumerator();
					}
					Value[] array2 = new Value[array.Length];
					for (int i = 0; i < array.Length; i++)
					{
						string text = array[i];
						array2[i] = RecordValue.New(ExcelInteropModule.CurrentWorkbookFunctionValue.excelTablesKeys, new Value[]
						{
							new ExcelInteropModule.CurrentWorkbookFunctionValue.ExcelTableValue(this.excelService, text),
							TextValue.New(text)
						});
					}
					return ListValue.New(array2).GetEnumerator();
				}

				// Token: 0x04002EA8 RID: 11944
				private readonly IExcelService excelService;
			}

			// Token: 0x02000C17 RID: 3095
			private class ExcelTableValue : TableValue
			{
				// Token: 0x06005459 RID: 21593 RVA: 0x001214E4 File Offset: 0x0011F6E4
				public ExcelTableValue(IExcelService excelService, string tableName)
					: this(excelService, tableName, 0, null)
				{
				}

				// Token: 0x0600545A RID: 21594 RVA: 0x00121503 File Offset: 0x0011F703
				public ExcelTableValue(IExcelService excelService, string tableName, int skip, int? take)
				{
					this.excelService = excelService;
					this.tableName = tableName;
					this.skip = skip;
					this.take = take;
				}

				// Token: 0x0600545B RID: 21595 RVA: 0x00121528 File Offset: 0x0011F728
				public static TableValue GetTable(IExcelService service, string tableName)
				{
					ExcelInteropModule.CurrentWorkbookFunctionValue.ExcelTableValue excelTableValue = new ExcelInteropModule.CurrentWorkbookFunctionValue.ExcelTableValue(service, tableName);
					excelTableValue.EnsureTypeAndMetaCreated();
					return excelTableValue;
				}

				// Token: 0x170019C6 RID: 6598
				// (get) Token: 0x0600545C RID: 21596 RVA: 0x00121537 File Offset: 0x0011F737
				public override RecordValue MetaValue
				{
					get
					{
						this.EnsureTypeAndMetaCreated();
						return this.metaValue;
					}
				}

				// Token: 0x170019C7 RID: 6599
				// (get) Token: 0x0600545D RID: 21597 RVA: 0x00121545 File Offset: 0x0011F745
				public override TypeValue Type
				{
					get
					{
						this.EnsureTypeAndMetaCreated();
						return this.typeValue;
					}
				}

				// Token: 0x0600545E RID: 21598 RVA: 0x00121553 File Offset: 0x0011F753
				public override IEnumerator<IValueReference> GetEnumerator()
				{
					int offset = 0;
					for (;;)
					{
						int attemptedPageSize;
						if (this.take != null)
						{
							attemptedPageSize = Math.Min(1000, this.take.Value - offset);
						}
						else
						{
							attemptedPageSize = 1000;
						}
						if (attemptedPageSize == 0)
						{
							break;
						}
						int actualPageSize = 0;
						foreach (IValueReference valueReference in new ExcelInteropModule.CurrentWorkbookFunctionValue.ExcelTableValue.Enumerator(this.GetDataReader(offset, attemptedPageSize)))
						{
							int num = actualPageSize;
							actualPageSize = num + 1;
							yield return valueReference;
						}
						IEnumerator<IValueReference> enumerator = null;
						if (actualPageSize < attemptedPageSize)
						{
							break;
						}
						offset += actualPageSize;
					}
					yield break;
					yield break;
				}

				// Token: 0x0600545F RID: 21599 RVA: 0x00121564 File Offset: 0x0011F764
				private void EnsureTypeAndMetaCreated()
				{
					if (this.metaValue == null)
					{
						bool flag;
						using (IDataReader dataReader = this.GetDataReader(0, 0, out flag))
						{
							DataTable schemaTable = dataReader.GetSchemaTable();
							string[] array = new string[schemaTable.Rows.Count];
							for (int i = 0; i < array.Length; i++)
							{
								array[i] = (string)schemaTable.Rows[i][InformationSchemaTableColumnName.ColumnName];
							}
							this.keys = Keys.New(array);
							Value[] array2 = new Value[array.Length];
							for (int j = 0; j < this.keys.Length; j++)
							{
								array2[j] = RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
								{
									TypeValue.Any,
									LogicalValue.False
								});
							}
							this.typeValue = TableTypeValue.New(RecordTypeValue.New(RecordValue.New(this.keys, array2), false), new TableKey[0]);
							this.metaValue = ValueServices.AddShouldInferTableTypeMeta(RecordValue.Empty);
							if (flag)
							{
								this.metaValue = ValueServices.AddFirstRowMayContainHeadersMeta(this.metaValue);
							}
						}
					}
				}

				// Token: 0x06005460 RID: 21600 RVA: 0x0012168C File Offset: 0x0011F88C
				private IDataReader GetDataReader(int skip, int take)
				{
					bool flag;
					return this.GetDataReader(skip, take, out flag);
				}

				// Token: 0x06005461 RID: 21601 RVA: 0x001216A4 File Offset: 0x0011F8A4
				private IDataReader GetDataReader(int skip, int take, out bool columnNamesGenerated)
				{
					IDataReader dataReader;
					string text;
					if (this.excelService.TryGetTable(this.tableName, skip, take, out dataReader, out columnNamesGenerated, out text))
					{
						return dataReader;
					}
					if (text != null)
					{
						throw ValueException.NewExpressionError(text, TextValue.New(this.tableName), null);
					}
					throw ExcelInteropModule.CurrentWorkbookFunctionValue.ExcelTableNotFoundError(this.tableName);
				}

				// Token: 0x04002EA9 RID: 11945
				private const int maxPageSize = 1000;

				// Token: 0x04002EAA RID: 11946
				private readonly IExcelService excelService;

				// Token: 0x04002EAB RID: 11947
				private readonly string tableName;

				// Token: 0x04002EAC RID: 11948
				private readonly int skip;

				// Token: 0x04002EAD RID: 11949
				private readonly int? take;

				// Token: 0x04002EAE RID: 11950
				private TableTypeValue typeValue;

				// Token: 0x04002EAF RID: 11951
				private Keys keys;

				// Token: 0x04002EB0 RID: 11952
				private RecordValue metaValue;

				// Token: 0x02000C18 RID: 3096
				private class Enumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator, IEnumerable<IValueReference>, IEnumerable
				{
					// Token: 0x06005462 RID: 21602 RVA: 0x001216F0 File Offset: 0x0011F8F0
					public Enumerator(IDataReader reader)
					{
						this.reader = reader;
						DataTable schemaTable = reader.GetSchemaTable();
						string[] array = new string[schemaTable.Rows.Count];
						for (int i = 0; i < array.Length; i++)
						{
							array[i] = (string)schemaTable.Rows[i][InformationSchemaTableColumnName.ColumnName];
						}
						this.keys = Keys.New(array);
					}

					// Token: 0x170019C8 RID: 6600
					// (get) Token: 0x06005463 RID: 21603 RVA: 0x0012175A File Offset: 0x0011F95A
					public IValueReference Current
					{
						get
						{
							if (this.row == null)
							{
								throw new InvalidOperationException();
							}
							return this.row;
						}
					}

					// Token: 0x06005464 RID: 21604 RVA: 0x00121770 File Offset: 0x0011F970
					public void Dispose()
					{
						this.reader.Dispose();
					}

					// Token: 0x170019C9 RID: 6601
					// (get) Token: 0x06005465 RID: 21605 RVA: 0x0012177D File Offset: 0x0011F97D
					object IEnumerator.Current
					{
						get
						{
							return this.Current;
						}
					}

					// Token: 0x06005466 RID: 21606 RVA: 0x00121785 File Offset: 0x0011F985
					public bool MoveNext()
					{
						if (this.reader.Read())
						{
							this.row = this.CreateRow();
							return true;
						}
						this.row = null;
						return false;
					}

					// Token: 0x06005467 RID: 21607 RVA: 0x001217AC File Offset: 0x0011F9AC
					private RecordValue CreateRow()
					{
						Value[] values = new Value[this.keys.Length];
						ValueException[] errors = null;
						for (int i = 0; i < this.keys.Length; i++)
						{
							Value value;
							ValueException ex;
							if (this.TryGetCellValue(this.reader[i], out value, out ex))
							{
								values[i] = value;
							}
							else
							{
								if (errors == null)
								{
									errors = new ValueException[this.keys.Length];
								}
								errors[i] = ex;
							}
						}
						if (errors == null)
						{
							return RecordValue.New(this.keys, values);
						}
						return RecordValue.New(this.keys, delegate(int index)
						{
							if (errors[index] != null)
							{
								throw errors[index];
							}
							return values[index];
						});
					}

					// Token: 0x06005468 RID: 21608 RVA: 0x00121870 File Offset: 0x0011FA70
					private bool TryGetCellValue(object cell, out Value value, out ValueException error)
					{
						value = null;
						error = null;
						TypeCode typeCode = Convert.GetTypeCode(cell);
						if (typeCode == TypeCode.DBNull)
						{
							value = Value.Null;
							return true;
						}
						if (typeCode != TypeCode.Boolean)
						{
							switch (typeCode)
							{
							case TypeCode.Int32:
								error = ExcelInteropModule.GetExcelError((int)cell);
								return false;
							case TypeCode.Int64:
								value = NumberValue.New((long)cell);
								return true;
							case TypeCode.Double:
								value = NumberValue.New((double)cell);
								return true;
							case TypeCode.Decimal:
								value = NumberValue.New((decimal)cell);
								return true;
							case TypeCode.DateTime:
								value = DateTimeValue.New((DateTime)cell);
								return true;
							case TypeCode.String:
								value = TextValue.New((string)cell);
								return true;
							}
							throw new InvalidOperationException();
						}
						value = LogicalValue.New((bool)cell);
						return true;
					}

					// Token: 0x06005469 RID: 21609 RVA: 0x000091AE File Offset: 0x000073AE
					public void Reset()
					{
						throw new NotImplementedException();
					}

					// Token: 0x0600546A RID: 21610 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
					public IEnumerator<IValueReference> GetEnumerator()
					{
						return this;
					}

					// Token: 0x0600546B RID: 21611 RVA: 0x0012193C File Offset: 0x0011FB3C
					IEnumerator IEnumerable.GetEnumerator()
					{
						return this.GetEnumerator();
					}

					// Token: 0x04002EB1 RID: 11953
					private Keys keys;

					// Token: 0x04002EB2 RID: 11954
					private IDataReader reader;

					// Token: 0x04002EB3 RID: 11955
					private RecordValue row;
				}
			}
		}
	}
}
