using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Microsoft.Data.DeltaLake;
using Microsoft.Data.DeltaLake.Storage;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Library.Action;
using Microsoft.Mashup.Engine1.Library.Table;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.DeltaLake
{
	// Token: 0x02001F05 RID: 7941
	internal sealed class MashupStorage : IStorage
	{
		// Token: 0x06010B7E RID: 68478 RVA: 0x00399D43 File Offset: 0x00397F43
		public MashupStorage(IEngineHost engineHost, TableValue directory, RecordValue options)
		{
			this.engineHost = engineHost;
			this.tracingService = TracingService.GetService(engineHost);
			this.directory = directory;
			this.options = options;
			this.parquetDocument = MashupStorage.ParquetDocument(engineHost);
		}

		// Token: 0x17002C3C RID: 11324
		// (get) Token: 0x06010B7F RID: 68479 RVA: 0x00399D78 File Offset: 0x00397F78
		public IEngineHost EngineHost
		{
			get
			{
				return this.engineHost;
			}
		}

		// Token: 0x17002C3D RID: 11325
		// (get) Token: 0x06010B80 RID: 68480 RVA: 0x00399D80 File Offset: 0x00397F80
		public TableValue Directory
		{
			get
			{
				return this.directory;
			}
		}

		// Token: 0x17002C3E RID: 11326
		// (get) Token: 0x06010B81 RID: 68481 RVA: 0x00399D88 File Offset: 0x00397F88
		public bool UseStatistics
		{
			get
			{
				return DeltaLakeOptions.UseStatistics(this.options);
			}
		}

		// Token: 0x17002C3F RID: 11327
		// (get) Token: 0x06010B82 RID: 68482 RVA: 0x00399D95 File Offset: 0x00397F95
		public bool UseVOrder
		{
			get
			{
				return DeltaLakeOptions.UseVOrder(this.options);
			}
		}

		// Token: 0x06010B83 RID: 68483 RVA: 0x00399DA2 File Offset: 0x00397FA2
		public bool TryGetExpression(out IExpression expression)
		{
			expression = this.directory.Expression;
			if (expression.Kind != ExpressionKind.Constant)
			{
				return true;
			}
			expression = null;
			return false;
		}

		// Token: 0x06010B84 RID: 68484 RVA: 0x00399DC1 File Offset: 0x00397FC1
		public BinaryValue GetFile(string partialPath)
		{
			return this.GetItem(partialPath).AsBinary;
		}

		// Token: 0x06010B85 RID: 68485 RVA: 0x00399DD0 File Offset: 0x00397FD0
		public long GetFileSize(string partialPath)
		{
			Value value = this.directory;
			string[] array = partialPath.Split(new char[] { '/' });
			for (int i = 0; i < array.Length - 1; i++)
			{
				value = value.AsTable[RecordValue.New(MashupStorage.nameKeys, new Value[] { TextValue.New(array[i]) })].AsRecord["Content"];
			}
			return value.AsTable[RecordValue.New(MashupStorage.nameKeys, new Value[] { TextValue.New(array[array.Length - 1]) })].AsRecord["Attributes"]["Size"].AsNumber.AsInteger64;
		}

		// Token: 0x06010B86 RID: 68486 RVA: 0x00399E8C File Offset: 0x0039808C
		public BinaryValue CreateFile(string partialPath)
		{
			ActionValue actionValue;
			return this.CreateFile(partialPath, false, false, out actionValue);
		}

		// Token: 0x06010B87 RID: 68487 RVA: 0x00399EA4 File Offset: 0x003980A4
		public TableValue ParquetDocument(BinaryValue binary)
		{
			RecordValue asRecord = DeltaLakeOptions.DefaultParquetOptions.Concatenate(this.options).AsRecord;
			return this.parquetDocument.Invoke(binary, asRecord).AsTable;
		}

		// Token: 0x06010B88 RID: 68488 RVA: 0x00399EDC File Offset: 0x003980DC
		public string CombinePath(params string[] segments)
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			foreach (string text in segments)
			{
				if (!string.IsNullOrWhiteSpace(text))
				{
					if (flag)
					{
						flag = false;
					}
					else
					{
						stringBuilder.Append('/');
					}
					stringBuilder.Append(text);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06010B89 RID: 68489 RVA: 0x00399F30 File Offset: 0x00398130
		public string GetDirectory(string path)
		{
			int num = path.LastIndexOf('/');
			if (num <= 0)
			{
				return path;
			}
			return path.Substring(0, num);
		}

		// Token: 0x06010B8A RID: 68490 RVA: 0x00399F54 File Offset: 0x00398154
		public IEnumerable<StorageItem> GetFiles(string path)
		{
			TableValue asTable;
			try
			{
				asTable = this.GetItem(path).AsTable;
			}
			catch (ValueException ex)
			{
				using (IHostTrace hostTrace = this.tracingService.CreateTrace("DeltaLake/MashupStorage/GetFiles", null, TraceEventType.Information, null))
				{
					hostTrace.Add(ex, true);
				}
				yield break;
			}
			foreach (IValueReference valueReference in asTable)
			{
				RecordValue asRecord = valueReference.Value.AsRecord;
				if (asRecord["Content"].IsBinary)
				{
					string asString = asRecord["Name"].AsString;
					Value value = asRecord["Date modified"];
					if (value.IsNull)
					{
						value = asRecord["Date created"];
					}
					if (value.IsNull)
					{
						value = asRecord["Date accessed"];
					}
					DateTime dateTime = (value.IsDateTime ? value.AsDateTime.AsClrDateTime : default(DateTime));
					yield return new StorageItem(asString, this.CombinePath(new string[] { path, asString }), dateTime);
				}
			}
			IEnumerator<IValueReference> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06010B8B RID: 68491 RVA: 0x00399F6C File Offset: 0x0039816C
		public bool TryCreatePath(string partialPath, bool allowOverwrite, out Stream file)
		{
			bool flag;
			try
			{
				file = this.CreateFile(partialPath, allowOverwrite);
				flag = true;
			}
			catch (ValueException ex)
			{
				using (IHostTrace hostTrace = this.tracingService.CreateTrace("DeltaLake/MashupStorage/TryCreatePath", null, TraceEventType.Information, null))
				{
					hostTrace.Add(ex, true);
				}
				file = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x06010B8C RID: 68492 RVA: 0x00399FD4 File Offset: 0x003981D4
		public bool TryReadPath(string partialPath, out Stream file)
		{
			bool flag;
			try
			{
				file = this.GetItem(partialPath).AsBinary.Open(true);
				flag = true;
			}
			catch (ValueException ex)
			{
				using (IHostTrace hostTrace = this.tracingService.CreateTrace("DeltaLake/MashupStorage/TryReadPath", null, TraceEventType.Information, null))
				{
					hostTrace.Add(ex, true);
				}
				file = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x06010B8D RID: 68493 RVA: 0x001D2D64 File Offset: 0x001D0F64
		public bool TryDeletePath(string partialPath)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06010B8E RID: 68494 RVA: 0x001D2D64 File Offset: 0x001D0F64
		public bool TryRenamePath(string sourcePath, string destinationPath, bool allowOverwrite)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06010B8F RID: 68495 RVA: 0x0039A048 File Offset: 0x00398248
		public Value GetItem(string partialPath)
		{
			Value value = this.directory;
			foreach (string text in partialPath.Split(new char[] { '/' }))
			{
				value = value.AsTable[RecordValue.New(MashupStorage.nameKeys, new Value[] { TextValue.New(text) })].AsRecord["Content"];
			}
			return value;
		}

		// Token: 0x06010B90 RID: 68496 RVA: 0x0039A0B8 File Offset: 0x003982B8
		public Stream CreateFile(string partialPath, bool allowOverwrite)
		{
			ActionValue rename;
			Stream stream = this.CreateFile(partialPath, allowOverwrite, !allowOverwrite, out rename).OpenForWrite();
			if (rename != null)
			{
				stream = stream.AfterDispose(delegate
				{
					try
					{
						this.isRenaming = true;
						rename.Bind(ActionModule.Action.DoNothing).ClearCache(this.engineHost).Execute();
					}
					finally
					{
						this.isRenaming = false;
					}
				});
			}
			return stream;
		}

		// Token: 0x06010B91 RID: 68497 RVA: 0x0039A108 File Offset: 0x00398308
		private BinaryValue CreateFile(string partialPath, bool allowOverwrite, bool useTempFile, out ActionValue rename)
		{
			Value value = this.directory;
			string[] array = partialPath.Split(new char[] { '/' });
			for (int i = 0; i < array.Length - 1; i++)
			{
				RecordValue recordValue = RecordValue.New(MashupStorage.nameKeys, new Value[] { TextValue.New(array[i]) });
				Value value2;
				if (value.AsTable.TryGetValue(recordValue, out value2))
				{
					value = value2.AsRecord["Content"];
				}
				else
				{
					RecordValue recordValue2 = RecordValue.New(MashupStorage.nameAndContentKeys, new Value[]
					{
						TextValue.New(array[i]),
						TableValue.Empty
					});
					value.AsTable.InsertRows(TableModule.Table.FromRecords.Invoke(ListValue.New(new Value[] { recordValue2 })).AsTable).Bind(ActionModule.Action.DoNothing).ClearCache(this.engineHost)
						.Execute();
					value = value.AsTable[recordValue].AsRecord["Content"];
				}
			}
			string text = array[array.Length - 1];
			if (allowOverwrite && !useTempFile)
			{
				RecordValue recordValue3 = RecordValue.New(MashupStorage.nameKeys, new Value[] { TextValue.New(text) });
				Value value3;
				if (value.AsTable.TryGetValue(recordValue3, out value3))
				{
					rename = null;
					return value3.AsRecord["Content"].AsBinary;
				}
			}
			string text2 = (useTempFile ? Guid.NewGuid().ToString("N") : text);
			RecordValue recordValue4 = RecordValue.New(MashupStorage.nameAndContentKeys, new Value[]
			{
				TextValue.New(text2),
				BinaryValue.Empty
			});
			value.AsTable.InsertRows(TableModule.Table.FromRecords.Invoke(ListValue.New(new Value[] { recordValue4 })).AsTable).Bind(ActionModule.Action.DoNothing).ClearCache(this.engineHost)
				.Execute();
			RecordValue recordValue5 = RecordValue.New(MashupStorage.nameKeys, new Value[] { TextValue.New(text2) });
			TableValue tableValue = value.AsTable.SelectRows(recordValue5);
			rename = (useTempFile ? tableValue.UpdateRows(ListValue.New(new Value[]
			{
				TextValue.New("Name"),
				ConstantFunctionValue.Each(TextValue.New(text))
			})) : null);
			return tableValue[0].AsRecord["Content"].AsBinary;
		}

		// Token: 0x06010B92 RID: 68498 RVA: 0x0039A36C File Offset: 0x0039856C
		public void DeleteFile(string partialPath)
		{
			Value value = this.directory;
			string[] array = partialPath.Split(new char[] { '/' });
			for (int i = 0; i < array.Length; i++)
			{
				bool flag = i == array.Length - 1;
				RecordValue recordValue = RecordValue.New(MashupStorage.nameKeys, new Value[] { TextValue.New(array[i]) });
				if (flag)
				{
					value.AsTable.SelectRows(recordValue).DeleteRows().Bind(ActionModule.Action.DoNothing)
						.ClearCache(this.engineHost)
						.Execute();
					return;
				}
				Value value2;
				if (!value.AsTable.TryGetValue(recordValue, out value2))
				{
					return;
				}
				value = value2.AsRecord["Content"];
			}
		}

		// Token: 0x06010B93 RID: 68499 RVA: 0x0039A41E File Offset: 0x0039861E
		public bool ShouldRetry(Exception exception, string partialPath)
		{
			if (this.isRenaming)
			{
				this.renameFailureCount++;
			}
			return this.isRenaming && this.renameFailureCount < MashupStorage.maxRenameFailureCount;
		}

		// Token: 0x06010B94 RID: 68500 RVA: 0x000024FE File Offset: 0x000006FE
		private static FunctionValue ParquetDocument(IEngineHost engineHost)
		{
			return Modules.GetLibrary(engineHost, null)["Parquet.Document"].AsFunction;
		}

		// Token: 0x0400642B RID: 25643
		public const char VirtualSeparator = '/';

		// Token: 0x0400642C RID: 25644
		private static readonly Keys nameKeys = Keys.New("Name");

		// Token: 0x0400642D RID: 25645
		private static readonly Keys nameAndContentKeys = Keys.New("Name", "Content");

		// Token: 0x0400642E RID: 25646
		private static readonly int maxRenameFailureCount = new DeltaConfiguration().MaximumCommitRetries;

		// Token: 0x0400642F RID: 25647
		private readonly IEngineHost engineHost;

		// Token: 0x04006430 RID: 25648
		private readonly ITracingService tracingService;

		// Token: 0x04006431 RID: 25649
		private readonly TableValue directory;

		// Token: 0x04006432 RID: 25650
		private readonly RecordValue options;

		// Token: 0x04006433 RID: 25651
		private readonly FunctionValue parquetDocument;

		// Token: 0x04006434 RID: 25652
		private bool isRenaming;

		// Token: 0x04006435 RID: 25653
		private int renameFailureCount;
	}
}
