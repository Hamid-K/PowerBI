using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Microsoft.Data.DeltaLake;
using Microsoft.Data.DeltaLake.Commands;
using Microsoft.Data.DeltaLake.Storage;
using Microsoft.Data.DeltaLake.Types;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.DeltaLake;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Parquet;

namespace Microsoft.Mashup.Engine1.Library.DeltaLake
{
	// Token: 0x02001EF4 RID: 7924
	internal abstract class DeltaSource
	{
		// Token: 0x06010B03 RID: 68355 RVA: 0x00397A6B File Offset: 0x00395C6B
		protected DeltaSource(MashupStorage storage)
		{
			this.storage = storage;
			this.deltaLog = DeltaLog.ForTable(DeltaSource.deltaConfiguration, storage, string.Empty);
		}

		// Token: 0x06010B04 RID: 68356 RVA: 0x00397A90 File Offset: 0x00395C90
		protected DeltaSource(DeltaSource source)
		{
			this.storage = source.storage;
			this.deltaLog = source.deltaLog;
		}

		// Token: 0x06010B05 RID: 68357 RVA: 0x00397AB0 File Offset: 0x00395CB0
		public static DeltaSource New(MashupStorage storage, long? version = null)
		{
			return new DeltaSource.SnapshotSource(storage, version);
		}

		// Token: 0x17002C28 RID: 11304
		// (get) Token: 0x06010B06 RID: 68358 RVA: 0x00397AB9 File Offset: 0x00395CB9
		public IEngineHost EngineHost
		{
			get
			{
				return this.storage.EngineHost;
			}
		}

		// Token: 0x17002C29 RID: 11305
		// (get) Token: 0x06010B07 RID: 68359 RVA: 0x00397AC6 File Offset: 0x00395CC6
		public bool Exists
		{
			get
			{
				return this.deltaLog.Exists;
			}
		}

		// Token: 0x17002C2A RID: 11306
		// (get) Token: 0x06010B08 RID: 68360
		public abstract string VersionIdentity { get; }

		// Token: 0x17002C2B RID: 11307
		// (get) Token: 0x06010B09 RID: 68361
		public abstract bool ReadOnly { get; }

		// Token: 0x17002C2C RID: 11308
		// (get) Token: 0x06010B0A RID: 68362 RVA: 0x00397AD3 File Offset: 0x00395CD3
		public bool UseStatistics
		{
			get
			{
				return this.storage.UseStatistics;
			}
		}

		// Token: 0x17002C2D RID: 11309
		// (get) Token: 0x06010B0B RID: 68363 RVA: 0x00397AE0 File Offset: 0x00395CE0
		public bool UseVOrder
		{
			get
			{
				return this.storage.UseVOrder;
			}
		}

		// Token: 0x06010B0C RID: 68364
		public abstract Protocol GetProtocol();

		// Token: 0x06010B0D RID: 68365
		public abstract Metadata GetMetadata();

		// Token: 0x06010B0E RID: 68366
		public abstract AddFile[] GetFiles();

		// Token: 0x06010B0F RID: 68367 RVA: 0x00397AF0 File Offset: 0x00395CF0
		public bool TryGetExpression(out IExpression expression)
		{
			if (this.storage.TryGetExpression(out expression))
			{
				expression = new InvocationExpressionSyntaxNode1(new ConstantExpressionSyntaxNode(new DeltaLakeModule.DeltaLake.TableFunctionValue(this.EngineHost)), expression);
				string versionIdentity = this.VersionIdentity;
				if (versionIdentity != null)
				{
					expression = new RequiredFieldAccessExpressionSyntaxNode(new RequiredElementAccessExpressionSyntaxNode(new InvocationExpressionSyntaxNode1(new ConstantExpressionSyntaxNode(Library._Value.Versions), expression), new ConstantExpressionSyntaxNode(RecordValue.New(Keys.New("Version"), new Value[] { TextValue.New(versionIdentity) }))), "Data");
				}
				return true;
			}
			return false;
		}

		// Token: 0x06010B10 RID: 68368 RVA: 0x00397B7B File Offset: 0x00395D7B
		public StructType GetSchema()
		{
			return this.GetMetadata().Schema;
		}

		// Token: 0x06010B11 RID: 68369 RVA: 0x00397B88 File Offset: 0x00395D88
		public TableValue CreateTable(long? version = null)
		{
			DeltaSource deltaSource = new DeltaSource.SnapshotSource(this, version);
			return new DeltaSource.DeltaTableTableValue(new DeltaQuery(this.storage.EngineHost, deltaSource));
		}

		// Token: 0x06010B12 RID: 68370 RVA: 0x00397BB4 File Offset: 0x00395DB4
		public IValueReference CreateDraftTable(string identity)
		{
			DeltaSource.TransactionSource transactionSource;
			if (this.TryGetTransactionSource(identity, out transactionSource))
			{
				return new DeltaSource.DeltaTableTableValue(new DeltaQuery(this.storage.EngineHost, transactionSource));
			}
			return new ExceptionValueReference(ValueException.NewDataSourceError<Message0>(Microsoft.Mashup.DeltaLake.Resources.CreatedByOtherProcess, Value.Null, null));
		}

		// Token: 0x06010B13 RID: 68371 RVA: 0x00397BF8 File Offset: 0x00395DF8
		public bool TryCreateVersion(string identity)
		{
			string text = this.TransactionFile(identity);
			string text2;
			IResource resource;
			DeltaSource.TransactionSource transactionSource;
			if (DataSource.TryGetFileExtensionAndResource(this.storage.CreateFile(text), out text2, out resource) && !DeltaSource.openTransactions.TryGetValue(resource, out transactionSource))
			{
				transactionSource = new DeltaSource.TransactionSource(new DeltaTransaction(this, this.deltaLog.StartTransaction(), identity));
				DeltaSource.openTransactions.Add(resource, transactionSource);
				return true;
			}
			return false;
		}

		// Token: 0x06010B14 RID: 68372 RVA: 0x00397C5A File Offset: 0x00395E5A
		public IEnumerable<TableVersion> GetVersions()
		{
			return this.deltaLog.GetVersions();
		}

		// Token: 0x06010B15 RID: 68373 RVA: 0x00397C67 File Offset: 0x00395E67
		public IEnumerable<KeyValuePair<string, DateTime>> GetDraftVersions()
		{
			string text = this.storage.CombinePath(new string[] { "_delta_log", "_mashup_temporary" });
			foreach (StorageItem storageItem in this.storage.GetFiles(text))
			{
				int num = storageItem.PartialPath.LastIndexOf('/');
				if (num > 0 && storageItem.PartialPath.EndsWith(".version", StringComparison.OrdinalIgnoreCase))
				{
					int num2 = storageItem.Name.Length - ".version".Length;
					yield return new KeyValuePair<string, DateTime>(storageItem.PartialPath.Substring(num + 1, num2), storageItem.Timestamp);
				}
			}
			IEnumerator<StorageItem> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06010B16 RID: 68374 RVA: 0x00397C77 File Offset: 0x00395E77
		public BinaryValue GetRawFile(string partialPath)
		{
			return this.storage.GetFile(partialPath);
		}

		// Token: 0x06010B17 RID: 68375 RVA: 0x00397C85 File Offset: 0x00395E85
		public TableValue GetFile(string partialPath)
		{
			return this.storage.ParquetDocument(this.GetRawFile(partialPath));
		}

		// Token: 0x06010B18 RID: 68376 RVA: 0x00397C99 File Offset: 0x00395E99
		public List<KeyValuePair<string, ListStatistics>> GetStatistics(string partialPath, out long rowCount)
		{
			return ParquetApi.GetStatistics(this.EngineHost, this.GetRawFile(partialPath), DeltaLakeOptions.DefaultParquetOptions, out rowCount);
		}

		// Token: 0x06010B19 RID: 68377 RVA: 0x00397CB4 File Offset: 0x00395EB4
		public TableValue CreateFile(string partialPath)
		{
			BinaryValue binaryValue = this.storage.CreateFile(partialPath);
			return this.storage.ParquetDocument(binaryValue);
		}

		// Token: 0x06010B1A RID: 68378 RVA: 0x00397CDA File Offset: 0x00395EDA
		public long GetFileSize(string partialPath)
		{
			return this.storage.GetFileSize(partialPath);
		}

		// Token: 0x06010B1B RID: 68379
		public abstract DeltaTransaction StartTransaction();

		// Token: 0x06010B1C RID: 68380 RVA: 0x00397CE8 File Offset: 0x00395EE8
		public void CommitTransaction(string identity)
		{
			DeltaSource.TransactionSource transactionSource;
			if (this.TryGetTransactionSource(identity, out transactionSource))
			{
				try
				{
					transactionSource.Commit();
					return;
				}
				finally
				{
					try
					{
						this.storage.DeleteFile(this.TransactionFile(identity));
					}
					catch (Exception ex) when (SafeExceptions.IsSafeException(ex))
					{
						using (IHostTrace hostTrace = this.EngineHost.QueryService<ITracingService>().CreateTrace("DeltaLake/DeltaSource/CommitTransaction", null, TraceEventType.Information, null))
						{
							hostTrace.Add(ex, true);
						}
					}
				}
			}
			throw ValueException.NewDataSourceError<Message0>(Microsoft.Mashup.DeltaLake.Resources.CreatedByOtherProcess, Value.Null, null);
		}

		// Token: 0x06010B1D RID: 68381 RVA: 0x00397DA0 File Offset: 0x00395FA0
		public void CheckWriterFeatures()
		{
			string[] array = DeltaFeatures.UnsupportedWriteFeatures(this.deltaLog.GetSnapshot());
			if (array.Length != 0)
			{
				throw ValueException.NewDataFormatError<Message1>(Microsoft.Mashup.DeltaLake.Resources.UnsupportedWrite(PiiFree.New(string.Join(", ", array))), this.storage.Directory, null);
			}
		}

		// Token: 0x06010B1E RID: 68382 RVA: 0x00397DEC File Offset: 0x00395FEC
		private bool TryGetTransactionSource(string identity, out DeltaSource.TransactionSource source)
		{
			string text = this.TransactionFile(identity);
			BinaryValue file = this.storage.GetFile(text);
			source = null;
			string text2;
			IResource resource;
			return DataSource.TryGetFileExtensionAndResource(file, out text2, out resource) && DeltaSource.openTransactions.TryGetValue(resource, out source);
		}

		// Token: 0x06010B1F RID: 68383 RVA: 0x00397E29 File Offset: 0x00396029
		private string TransactionFile(string identity)
		{
			return this.storage.CombinePath(new string[]
			{
				"_delta_log",
				"_mashup_temporary",
				identity + ".version"
			});
		}

		// Token: 0x040063FC RID: 25596
		private const string TempFolder = "_mashup_temporary";

		// Token: 0x040063FD RID: 25597
		private const string VersionSuffix = ".version";

		// Token: 0x040063FE RID: 25598
		private static Dictionary<IResource, DeltaSource.TransactionSource> openTransactions = new Dictionary<IResource, DeltaSource.TransactionSource>(ResourceEqualityComparer.Instance);

		// Token: 0x040063FF RID: 25599
		private static readonly DeltaConfiguration deltaConfiguration = new DeltaConfiguration
		{
			UseRenameForFileWrite = false,
			CheckpointAccumulatorRemoveNoneExistingFiles = true,
			DeleteExpiredLogs = false
		};

		// Token: 0x04006400 RID: 25600
		private readonly MashupStorage storage;

		// Token: 0x04006401 RID: 25601
		private readonly DeltaLog deltaLog;

		// Token: 0x02001EF5 RID: 7925
		private class DeltaTableTableValue : WrappingTableValue
		{
			// Token: 0x06010B21 RID: 68385 RVA: 0x00397E8A File Offset: 0x0039608A
			public DeltaTableTableValue(DeltaQuery query)
				: base(new QueryTableValue(query))
			{
			}

			// Token: 0x06010B22 RID: 68386 RVA: 0x00397E98 File Offset: 0x00396098
			private DeltaTableTableValue(TableValue table)
				: base(table)
			{
			}

			// Token: 0x06010B23 RID: 68387 RVA: 0x00397EA1 File Offset: 0x003960A1
			protected override TableValue New(TableValue table)
			{
				if (this.Query == table.Query)
				{
					return new DeltaSource.DeltaTableTableValue(table);
				}
				return table;
			}

			// Token: 0x06010B24 RID: 68388 RVA: 0x00397EB9 File Offset: 0x003960B9
			public override ActionValue Replace(Value value)
			{
				return ((DeltaQuery)this.Query).Replace(value.AsTable);
			}
		}

		// Token: 0x02001EF6 RID: 7926
		private sealed class SnapshotSource : DeltaSource
		{
			// Token: 0x06010B25 RID: 68389 RVA: 0x00397ED1 File Offset: 0x003960D1
			public SnapshotSource(MashupStorage storage, long? version)
				: base(storage)
			{
				this.version = version;
			}

			// Token: 0x06010B26 RID: 68390 RVA: 0x00397EE1 File Offset: 0x003960E1
			public SnapshotSource(DeltaSource source, long? version)
				: base(source)
			{
				this.version = version;
			}

			// Token: 0x17002C2E RID: 11310
			// (get) Token: 0x06010B27 RID: 68391 RVA: 0x00397EF1 File Offset: 0x003960F1
			public override bool ReadOnly
			{
				get
				{
					return this.version != null;
				}
			}

			// Token: 0x17002C2F RID: 11311
			// (get) Token: 0x06010B28 RID: 68392 RVA: 0x00397F00 File Offset: 0x00396100
			public override string VersionIdentity
			{
				get
				{
					if (this.version == null)
					{
						return null;
					}
					return this.version.Value.ToString(CultureInfo.InvariantCulture);
				}
			}

			// Token: 0x06010B29 RID: 68393 RVA: 0x00397F34 File Offset: 0x00396134
			private Snapshot GetSnapshot(bool checkFeatures)
			{
				if (this.snapshot == null)
				{
					try
					{
						this.snapshot = ((this.version != null) ? this.deltaLog.GetSnapshot(this.version.Value) : this.deltaLog.GetSnapshot());
					}
					catch (Exception ex) when (SafeExceptions.IsSafeException(ex) && !(ex is ValueException))
					{
						throw ValueException.NewDataFormatError<Message0>(Microsoft.Mashup.DeltaLake.Resources.DeltaTable_NotFound, this.storage.Directory, ex);
					}
				}
				if (checkFeatures)
				{
					if (this.unsupportedFeatures == null)
					{
						this.unsupportedFeatures = DeltaFeatures.UnsupportedReadFeatures(this.snapshot);
					}
					if (this.unsupportedFeatures.Length != 0)
					{
						throw ValueException.NewDataFormatError<Message1>(Microsoft.Mashup.DeltaLake.Resources.UnsupportedRead(PiiFree.New(string.Join(", ", this.unsupportedFeatures))), this.storage.Directory, null);
					}
				}
				return this.snapshot;
			}

			// Token: 0x06010B2A RID: 68394 RVA: 0x0039802C File Offset: 0x0039622C
			public override Protocol GetProtocol()
			{
				if (!base.Exists)
				{
					return new Protocol(1, 2);
				}
				return this.GetSnapshot(false).GetProtocol();
			}

			// Token: 0x06010B2B RID: 68395 RVA: 0x0039804A File Offset: 0x0039624A
			public override Metadata GetMetadata()
			{
				return this.GetSnapshot(true).GetMetadata();
			}

			// Token: 0x06010B2C RID: 68396 RVA: 0x00398058 File Offset: 0x00396258
			public override AddFile[] GetFiles()
			{
				return this.GetSnapshot(true).GetFiles();
			}

			// Token: 0x06010B2D RID: 68397 RVA: 0x00398066 File Offset: 0x00396266
			public override DeltaTransaction StartTransaction()
			{
				return new DeltaTransaction(this, this.deltaLog.StartTransaction(), null);
			}

			// Token: 0x04006402 RID: 25602
			private readonly long? version;

			// Token: 0x04006403 RID: 25603
			private Snapshot snapshot;

			// Token: 0x04006404 RID: 25604
			private string[] unsupportedFeatures;
		}

		// Token: 0x02001EF7 RID: 7927
		private sealed class TransactionSource : DeltaSource
		{
			// Token: 0x06010B2E RID: 68398 RVA: 0x0039807A File Offset: 0x0039627A
			public TransactionSource(DeltaTransaction transaction)
				: base(transaction.OriginalSource)
			{
				this.transaction = transaction;
			}

			// Token: 0x17002C30 RID: 11312
			// (get) Token: 0x06010B2F RID: 68399 RVA: 0x0000FA11 File Offset: 0x0000DC11
			public override bool ReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17002C31 RID: 11313
			// (get) Token: 0x06010B30 RID: 68400 RVA: 0x0039808F File Offset: 0x0039628F
			public override string VersionIdentity
			{
				get
				{
					return this.transaction.Identity;
				}
			}

			// Token: 0x06010B31 RID: 68401 RVA: 0x0039809C File Offset: 0x0039629C
			public override Protocol GetProtocol()
			{
				return this.transaction.GetProtocol();
			}

			// Token: 0x06010B32 RID: 68402 RVA: 0x003980A9 File Offset: 0x003962A9
			public override Metadata GetMetadata()
			{
				return this.transaction.GetMetadata();
			}

			// Token: 0x06010B33 RID: 68403 RVA: 0x003980B6 File Offset: 0x003962B6
			public override AddFile[] GetFiles()
			{
				return this.transaction.GetFiles();
			}

			// Token: 0x06010B34 RID: 68404 RVA: 0x003980C3 File Offset: 0x003962C3
			public override DeltaTransaction StartTransaction()
			{
				return this.transaction;
			}

			// Token: 0x06010B35 RID: 68405 RVA: 0x003980CB File Offset: 0x003962CB
			public void Commit()
			{
				this.transaction.Commit();
			}

			// Token: 0x04006405 RID: 25605
			private readonly DeltaTransaction transaction;
		}
	}
}
