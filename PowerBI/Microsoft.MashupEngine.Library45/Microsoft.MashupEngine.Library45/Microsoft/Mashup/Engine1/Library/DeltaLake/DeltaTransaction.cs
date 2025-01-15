using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Data.DeltaLake;
using Microsoft.Data.DeltaLake.Commands;
using Microsoft.Data.DeltaLake.Serialization;
using Microsoft.Data.DeltaLake.SqlExpressions;
using Microsoft.Data.DeltaLake.Types;
using Microsoft.Data.DeltaLake.Utilities;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.DeltaLake;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Action;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Library.DeltaLake
{
	// Token: 0x02001EFA RID: 7930
	internal class DeltaTransaction
	{
		// Token: 0x06010B41 RID: 68417 RVA: 0x0039848C File Offset: 0x0039668C
		public DeltaTransaction(DeltaSource originalSource, OptimisticTransaction transaction, string identity)
		{
			this.originalSource = originalSource;
			this.transaction = transaction;
			this.identity = identity;
			this.commands = new List<Command>();
			this.removedFiles = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			this.operationName = new OperationName?(16);
		}

		// Token: 0x17002C34 RID: 11316
		// (get) Token: 0x06010B42 RID: 68418 RVA: 0x003984DC File Offset: 0x003966DC
		private static string EngineName
		{
			get
			{
				if (DeltaTransaction.engineName == null)
				{
					string text;
					try
					{
						text = new Version(FileVersionInfo.GetVersionInfo(typeof(IEngine).Assembly.Location).FileVersion).ToString();
					}
					catch (Exception ex) when (SafeExceptions.IsSafeException(ex))
					{
						text = "(unknown)";
					}
					DeltaTransaction.engineName = "Mashup Engine/" + text;
				}
				return DeltaTransaction.engineName;
			}
		}

		// Token: 0x17002C35 RID: 11317
		// (get) Token: 0x06010B43 RID: 68419 RVA: 0x00398560 File Offset: 0x00396760
		public DeltaSource OriginalSource
		{
			get
			{
				return this.originalSource;
			}
		}

		// Token: 0x17002C36 RID: 11318
		// (get) Token: 0x06010B44 RID: 68420 RVA: 0x00398568 File Offset: 0x00396768
		public string Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x17002C37 RID: 11319
		// (get) Token: 0x06010B45 RID: 68421 RVA: 0x00398570 File Offset: 0x00396770
		public ActionValue CommitAction
		{
			get
			{
				if (this.identity != null)
				{
					return ActionModule.Action.DoNothing;
				}
				return new DeltaTransaction.CommitTransactionActionValue(this);
			}
		}

		// Token: 0x06010B46 RID: 68422 RVA: 0x00398586 File Offset: 0x00396786
		public Protocol GetProtocol()
		{
			return this.transaction.GetProtocol();
		}

		// Token: 0x06010B47 RID: 68423 RVA: 0x00398593 File Offset: 0x00396793
		public Metadata GetMetadata()
		{
			return this.transaction.GetMetadata();
		}

		// Token: 0x06010B48 RID: 68424 RVA: 0x003985A0 File Offset: 0x003967A0
		public AddFile[] GetFiles()
		{
			List<AddFile> list = new List<AddFile>();
			foreach (AddFile addFile in this.transaction.MarkFilesAsRead(SqlExpression.Constant(true)).GetFiles())
			{
				if (!this.removedFiles.Contains(addFile.Path))
				{
					list.Add(addFile);
				}
			}
			foreach (Command command in this.commands.Where((Command cmd) => cmd is AddFile))
			{
				AddFile addFile2 = (AddFile)command;
				list.Add(addFile2);
			}
			return list.ToArray();
		}

		// Token: 0x06010B49 RID: 68425 RVA: 0x00398688 File Offset: 0x00396888
		public long InsertRows(TableValue rowsToInsert, RecordValue partition, bool replaceSchema)
		{
			IReadOnlyDictionary<string, string> readOnlyDictionary = null;
			Metadata metadata;
			try
			{
				if (replaceSchema)
				{
					this.operationName = new OperationName?(11);
					bool flag = DeltaTypeConversion.RequiresColumnMapping(rowsToInsert.Type);
					TypeValue typeValue;
					StructType structType = DeltaTypeConversion.Convert("<row>", rowsToInsert.Type, flag, out typeValue);
					TableValue partitionTable = rowsToInsert.GetPartitionTable();
					bool flag2;
					using (IEnumerator<IValueReference> enumerator = partitionTable.GetEnumerator())
					{
						flag2 = enumerator.MoveNext();
						if (flag2)
						{
							partition = enumerator.Current.Value.AsRecord;
							if (enumerator.MoveNext())
							{
								throw ValueException.NewDataFormatError<Message0>(Resources.CantInsertPartitionedTable, Value.Null, null);
							}
						}
					}
					rowsToInsert = rowsToInsert.ReplaceType(typeValue).AsTable;
					Dictionary<string, string> dictionary = new Dictionary<string, string>(1);
					Protocol protocol = this.originalSource.GetProtocol();
					if (flag)
					{
						int num = Math.Max(protocol.MinReaderVersion, 2);
						int num2 = Math.Max(protocol.MinWriterVersion, 5);
						if (protocol.MinReaderVersion >= 3 || protocol.MinWriterVersion >= 7)
						{
							string[] array = new string[] { "columnMapping" };
							protocol = new Protocol(num, num2, array, array);
						}
						else
						{
							protocol = new Protocol(num, num2);
						}
						dictionary.Add("delta.columnMapping.mode", "name");
					}
					else
					{
						protocol = new Protocol(protocol.MinReaderVersion, protocol.MinWriterVersion);
					}
					this.commands.Add(protocol);
					string text = null;
					string text2 = null;
					string text3 = null;
					Format format = null;
					StructType structType2 = structType;
					DateTime? dateTime = new DateTime?(DateTime.UtcNow);
					metadata = new Metadata(text, text2, text3, format, partitionTable.Columns.ToArray<string>(), dictionary, dateTime, structType2);
					this.transaction.UpdateMetadata(metadata, null);
					if (!flag2)
					{
						return 0L;
					}
				}
				else
				{
					metadata = this.transaction.GetMetadata();
					RecordValue recordValue = rowsToInsert.Type.AsTableType.ItemType.Fields;
					RecordTypeValue asRecordType = DeltaTypeConversion.Convert(metadata.Schema).AsRecordType;
					RecordValue targetType = asRecordType.Fields;
					if (!recordValue.Keys.Equals(targetType.Keys))
					{
						rowsToInsert = rowsToInsert.SelectColumns(ListValue.New(targetType.Keys.ToArray<string>()), MissingFieldMode.UseNull);
						RecordValue oldSourceType = recordValue;
						recordValue = RecordValue.New(targetType.Keys, delegate(int i)
						{
							Value value;
							if (!oldSourceType.TryGetValue(targetType.Keys[i], out value))
							{
								value = targetType[i];
								TypeValue typeValue2 = RecordTypeAlgebra.FieldType(value);
								if (!typeValue2.IsNullable)
								{
									value = RecordTypeAlgebra.NewField(typeValue2.Nullable, false);
								}
							}
							return value;
						});
					}
					bool flag3 = false;
					for (int j = 0; j < targetType.Count; j++)
					{
						TypeValue asType = recordValue[j]["Type"].AsType;
						TypeValue asType2 = targetType[j]["Type"].AsType;
						if (asType != asType2)
						{
							if (asType.TypeKind != asType2.TypeKind)
							{
								throw ValueException.NewDataFormatError<Message3>(Resources.CantInsertType(asType.TypeKind, recordValue.Keys[j], asType2.TypeKind), asType, null);
							}
							flag3 = true;
						}
					}
					if (flag3)
					{
						rowsToInsert = rowsToInsert.ReplaceType(TableTypeValue.New(asRecordType)).AsTable;
					}
					rowsToInsert = rowsToInsert.RenameColumns(DeltaQuery.MakeRenames(metadata, true, Value.Null), MissingFieldMode.Error);
				}
			}
			catch (DeltaLakeException ex)
			{
				throw ValueException.NewDataSourceError(ex.Message, Value.Null, ex);
			}
			catch (InvalidOperationException ex2)
			{
				throw ValueException.NewDataSourceError(ex2.Message, Value.Null, ex2);
			}
			if (partition.Count > 0)
			{
				Dictionary<string, object> dictionary2 = ValueMarshaller.MarshalToClrDictionary(partition);
				readOnlyDictionary = PartitionUtils.TranslateValues(metadata.Schema, dictionary2);
				rowsToInsert = this.VerifyRows(rowsToInsert, partition);
			}
			string filename = Guid.NewGuid().ToString("N") + ".parquet";
			long numRecords = 0L;
			List<KeyValuePair<string, ListStatistics>> statistics = null;
			this.originalSource.CreateFile(filename).Replace(rowsToInsert).Bind(ActionModule.Action.DoNothing)
				.ClearCache(this.originalSource.EngineHost)
				.Bind(FunctionValue.New(delegate
				{
					statistics = this.originalSource.GetStatistics(filename, out numRecords);
					return ActionModule.Action.DoNothing;
				}))
				.Execute();
			long fileSize = this.originalSource.GetFileSize(filename);
			string text4 = ((statistics.Count > 0) ? DeltaStatistics.Convert(metadata, statistics, numRecords) : DeltaStatistics.CreateEmptyStatistics(numRecords));
			Dictionary<string, string> dictionary3 = new Dictionary<string, string>(1);
			if (this.originalSource.UseVOrder)
			{
				dictionary3.Add("VORDER", "true");
			}
			this.commands.Add(new AddFile(filename, readOnlyDictionary, new long?(fileSize), DateTime.UtcNow, true, text4, dictionary3, null));
			return numRecords;
		}

		// Token: 0x06010B4A RID: 68426 RVA: 0x00398B74 File Offset: 0x00396D74
		public long DeleteRows(Func<AddFile, bool> includeFile)
		{
			if (!this.originalSource.Exists)
			{
				return 0L;
			}
			DateTime utcNow = DateTime.UtcNow;
			long? num = new long?(0L);
			long num2 = 0L;
			StructType schema = this.transaction.GetMetadata().Schema;
			foreach (AddFile addFile in this.transaction.MarkFilesAsRead(SqlExpression.Constant(true)).GetFiles().Where(includeFile))
			{
				this.commands.Add(addFile.Remove(new DateTime?(utcNow)));
				this.removedFiles.Add(addFile.Path);
				Statistics statistics = StatisticsSerializer.Deserialize(schema, addFile.StatsString);
				if (statistics == null || statistics.NumRecords == null)
				{
					num = null;
				}
				else if (num != null)
				{
					num += statistics.NumRecords;
				}
				num2 += 1L;
			}
			return num.GetValueOrDefault(num2);
		}

		// Token: 0x06010B4B RID: 68427 RVA: 0x00398CC8 File Offset: 0x00396EC8
		public Value Commit()
		{
			Value value;
			try
			{
				value = NumberValue.New(this.transaction.Commit(this.commands, this.operationName.GetValueOrDefault(16), DeltaTransaction.EngineName, null).Version);
			}
			catch (DeltaLakeException ex)
			{
				throw ValueException.NewDataSourceError(ex.Message, Value.Null, ex);
			}
			catch (InvalidOperationException ex2)
			{
				throw ValueException.NewDataSourceError(ex2.Message, Value.Null, ex2);
			}
			return value;
		}

		// Token: 0x06010B4C RID: 68428 RVA: 0x00398D4C File Offset: 0x00396F4C
		private TableValue VerifyRows(TableValue rowsToInsert, RecordValue partition)
		{
			IExpression expression = null;
			IExpression expression2 = new InclusiveIdentifierExpressionSyntaxNode(Identifier.Underscore);
			for (int i = 0; i < partition.Count; i++)
			{
				IExpression expression3 = BinaryExpressionSyntaxNode.New(BinaryOperator2.Equals, new RequiredFieldAccessExpressionSyntaxNode(expression2, partition.Keys[i]), ConstantExpressionSyntaxNode.New(partition[i]), TokenRange.Null);
				if (expression == null)
				{
					expression = expression3;
				}
				else
				{
					expression = BinaryExpressionSyntaxNode.New(BinaryOperator2.And, expression, expression3, TokenRange.Null);
				}
			}
			IFunctionExpression functionExpression = new FunctionExpressionSyntaxNode(Microsoft.Mashup.Engine1.Language.Query.QueryHelpers.EachFunctionType, new IfExpressionSyntaxNode(expression, ConstantExpressionSyntaxNode.True, new ThrowExpressionSyntaxNode(new InvocationExpressionSyntaxNode2(new ConstantExpressionSyntaxNode(Library.Error.DataFormatError), new ConstantExpressionSyntaxNode(TextValue.New(Resources.PartitionDataMismatch)), expression2)), TokenRange.Null));
			return rowsToInsert.SelectRows(new Compiler(CompileOptions.None).ToFunction(functionExpression));
		}

		// Token: 0x0400640B RID: 25611
		private static string engineName;

		// Token: 0x0400640C RID: 25612
		private readonly DeltaSource originalSource;

		// Token: 0x0400640D RID: 25613
		private readonly OptimisticTransaction transaction;

		// Token: 0x0400640E RID: 25614
		private readonly string identity;

		// Token: 0x0400640F RID: 25615
		private readonly List<Command> commands;

		// Token: 0x04006410 RID: 25616
		private readonly HashSet<string> removedFiles;

		// Token: 0x04006411 RID: 25617
		private OperationName? operationName;

		// Token: 0x02001EFB RID: 7931
		private sealed class CommitTransactionActionValue : ActionValue
		{
			// Token: 0x06010B4D RID: 68429 RVA: 0x00398E15 File Offset: 0x00397015
			public CommitTransactionActionValue(DeltaTransaction transaction)
			{
				this.transaction = transaction;
			}

			// Token: 0x06010B4E RID: 68430 RVA: 0x00398E24 File Offset: 0x00397024
			public override Value Execute()
			{
				return this.transaction.Commit();
			}

			// Token: 0x04006412 RID: 25618
			private readonly DeltaTransaction transaction;
		}
	}
}
