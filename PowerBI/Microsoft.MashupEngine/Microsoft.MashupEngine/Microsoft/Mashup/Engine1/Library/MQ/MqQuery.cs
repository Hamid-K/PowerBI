using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Action;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.MQ
{
	// Token: 0x02000931 RID: 2353
	internal abstract class MqQuery : DataSourceQuery, IMqConfig<IValueReference>
	{
		// Token: 0x17001553 RID: 5459
		// (get) Token: 0x06004304 RID: 17156 RVA: 0x000E1BBB File Offset: 0x000DFDBB
		private static RowCount MaxRowCount
		{
			get
			{
				return new RowCount(4503599627370495L);
			}
		}

		// Token: 0x06004305 RID: 17157 RVA: 0x000E1BCB File Offset: 0x000DFDCB
		protected MqQuery(IEngineHost host, MqConnectionParameters connectionParameters, IDictionary<MqFunctionOption, object> options, IDictionary<MqColumn, object> filters, RowCount rowCount, ColumnSelection columnSelection)
		{
			this.host = host;
			this.connectionParameters = connectionParameters;
			this.options = options;
			this.filters = filters;
			this.rowCount = rowCount;
			this.queryColumnSelection = columnSelection;
		}

		// Token: 0x17001554 RID: 5460
		// (get) Token: 0x06004306 RID: 17158 RVA: 0x000E1C00 File Offset: 0x000DFE00
		public MqConnectionParameters ConnectionParameters
		{
			get
			{
				return this.connectionParameters;
			}
		}

		// Token: 0x17001555 RID: 5461
		// (get) Token: 0x06004307 RID: 17159 RVA: 0x000E1C08 File Offset: 0x000DFE08
		public IDictionary<MqFunctionOption, object> Options
		{
			get
			{
				return this.options;
			}
		}

		// Token: 0x17001556 RID: 5462
		// (get) Token: 0x06004308 RID: 17160 RVA: 0x000E1C10 File Offset: 0x000DFE10
		public IDictionary<MqColumn, object> Filters
		{
			get
			{
				return this.filters;
			}
		}

		// Token: 0x17001557 RID: 5463
		// (get) Token: 0x06004309 RID: 17161 RVA: 0x000E1C18 File Offset: 0x000DFE18
		public long? BatchSize
		{
			get
			{
				if (this.rowCount.IsZero)
				{
					return new long?(500L);
				}
				if (this.rowCount.Value == MqQuery.MaxRowCount.Value)
				{
					return null;
				}
				return new long?(this.rowCount.Value);
			}
		}

		// Token: 0x17001558 RID: 5464
		// (get) Token: 0x0600430A RID: 17162 RVA: 0x000E1C7B File Offset: 0x000DFE7B
		public override IEngineHost EngineHost
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x17001559 RID: 5465
		// (get) Token: 0x0600430B RID: 17163 RVA: 0x000E1C83 File Offset: 0x000DFE83
		private TypeValue BinaryDisplayTypeValue
		{
			get
			{
				if (this.binaryDisplayTypeValue == null)
				{
					this.binaryDisplayTypeValue = MqQuery.GetTypeValue(this.BinaryDisplayEncoding, TypeValue.Binary);
				}
				return this.binaryDisplayTypeValue;
			}
		}

		// Token: 0x1700155A RID: 5466
		// (get) Token: 0x0600430C RID: 17164 RVA: 0x000E1CA9 File Offset: 0x000DFEA9
		private TypeValue MessageDataDisplayTypeValue
		{
			get
			{
				if (this.messageDataDisplayTypeValue == null)
				{
					this.messageDataDisplayTypeValue = MqQuery.GetTypeValue(this.MessageDataDisplayEncoding, TypeValue.Text);
				}
				return this.messageDataDisplayTypeValue;
			}
		}

		// Token: 0x1700155B RID: 5467
		// (get) Token: 0x0600430D RID: 17165 RVA: 0x000E1CCF File Offset: 0x000DFECF
		private Value BinaryDisplayEncoding
		{
			get
			{
				return this.Options.GetValue(MqFunctionOption.BinaryDisplayEncoding, Value.Null);
			}
		}

		// Token: 0x1700155C RID: 5468
		// (get) Token: 0x0600430E RID: 17166 RVA: 0x000E1CE2 File Offset: 0x000DFEE2
		private Value MessageDataDisplayEncoding
		{
			get
			{
				return this.Options.GetValue(MqFunctionOption.MessageDataDisplayEncoding, Value.Null);
			}
		}

		// Token: 0x1700155D RID: 5469
		// (get) Token: 0x0600430F RID: 17167 RVA: 0x000E1CF8 File Offset: 0x000DFEF8
		private RecordTypeValue RecordType
		{
			get
			{
				if (this.recordType == null)
				{
					List<RecordValue> list = new List<RecordValue>();
					this.selectedColumns = new List<MqColumn>();
					foreach (string text in this.Columns)
					{
						MqColumnInfo mqColumnInfo;
						if (MqQuery.ColumnInfos.TryGetValue(text, out mqColumnInfo))
						{
							this.selectedColumns.Add(mqColumnInfo.Column);
							RecordValue recordValue = RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
							{
								this.GetColumnType(mqColumnInfo),
								LogicalValue.False
							});
							list.Add(recordValue);
						}
					}
					Keys columns = this.Columns;
					Value[] array = list.ToArray();
					this.recordType = RecordTypeValue.New(RecordValue.New(columns, array));
				}
				return this.recordType;
			}
		}

		// Token: 0x1700155E RID: 5470
		// (get) Token: 0x06004310 RID: 17168 RVA: 0x000E1DD4 File Offset: 0x000DFFD4
		public override Keys Columns
		{
			get
			{
				if (this.queryColumnSelection != null)
				{
					return this.queryColumnSelection.Keys;
				}
				return MqQuery.ColumnInfos.AllKeys;
			}
		}

		// Token: 0x1700155F RID: 5471
		// (get) Token: 0x06004311 RID: 17169 RVA: 0x000E1DF4 File Offset: 0x000DFFF4
		public override IList<TableKey> TableKeys
		{
			get
			{
				if (this.tableKeys == null)
				{
					this.tableKeys = new TableKey[0];
				}
				return this.tableKeys;
			}
		}

		// Token: 0x06004312 RID: 17170 RVA: 0x000E1E10 File Offset: 0x000E0010
		public override TypeValue GetColumnType(int column)
		{
			return this.GetColumnType(this.Columns[column]);
		}

		// Token: 0x06004313 RID: 17171 RVA: 0x000E1E24 File Offset: 0x000E0024
		private TypeValue GetColumnType(string key)
		{
			MqColumnInfo mqColumnInfo;
			if (MqQuery.ColumnInfos.TryGetValue(key, out mqColumnInfo))
			{
				return this.GetColumnType(mqColumnInfo);
			}
			return TypeValue.Any;
		}

		// Token: 0x06004314 RID: 17172 RVA: 0x000E1E50 File Offset: 0x000E0050
		private TypeValue GetColumnType(MqColumnInfo columnInfo)
		{
			TypeValue typeValue = null;
			MqColumn column = columnInfo.Column;
			if (column <= MqColumn.CorrelationId)
			{
				if (column == MqColumn.MessageData)
				{
					typeValue = this.MessageDataDisplayTypeValue;
					goto IL_003D;
				}
				if (column - MqColumn.MessageId > 1)
				{
					goto IL_003D;
				}
			}
			else if (column != MqColumn.AccountingToken && column != MqColumn.GroupId && column != MqColumn.MessageToken)
			{
				goto IL_003D;
			}
			typeValue = this.BinaryDisplayTypeValue;
			IL_003D:
			if (typeValue == null)
			{
				typeValue = columnInfo.Type;
			}
			KeysBuilder keysBuilder = default(KeysBuilder);
			ArrayBuilder<IValueReference> arrayBuilder = default(ArrayBuilder<IValueReference>);
			keysBuilder.Add("Documentation.IsWritable");
			arrayBuilder.Add(LogicalValue.New(columnInfo.IsWritable));
			RecordValue recordValue = RecordValue.New(keysBuilder.ToKeys(), arrayBuilder.ToArray());
			return typeValue.NewMeta(recordValue).AsType;
		}

		// Token: 0x06004315 RID: 17173 RVA: 0x000E1EF4 File Offset: 0x000E00F4
		private RecordValue CreateSchemaRecord(Message message)
		{
			return RecordValue.New(this.RecordType, this.GetValues(message));
		}

		// Token: 0x06004316 RID: 17174 RVA: 0x000E1F08 File Offset: 0x000E0108
		private Value[] GetValues(Message message)
		{
			return this.selectedColumns.Select((MqColumn column) => this.GetValue(message, column)).ToArray<Value>();
		}

		// Token: 0x06004317 RID: 17175 RVA: 0x000E1F48 File Offset: 0x000E0148
		private Value GetValue(Message message, MqColumn column)
		{
			switch (column)
			{
			case MqColumn.MessageData:
				return this.ValueFromBytes(message.Data, this.MessageDataDisplayEncoding, this.MessageDataDisplayTypeValue, message.Ccsid);
			case MqColumn.MessageId:
				return this.ValueFromBytes(message.MessageId, this.BinaryDisplayEncoding, this.BinaryDisplayTypeValue, 0);
			case MqColumn.CorrelationId:
				return this.ValueFromBytes(message.Correlator, this.BinaryDisplayEncoding, this.BinaryDisplayTypeValue, 0);
			case MqColumn.PutDateTime:
				return message.OriginContext.PutDateTime;
			case MqColumn.UserIdentifier:
				return TextValue.NewOrNull(message.IdentityContext.UserId);
			case MqColumn.PutApplicationName:
				return TextValue.NewOrNull(message.OriginContext.PutApplicationName);
			case MqColumn.PutApplicationType:
				return message.OriginContext.PutApplicationTypeString;
			case MqColumn.Format:
				return TextValue.NewOrNull(message.Format);
			case MqColumn.AccountingToken:
				return this.ValueFromBytes(message.IdentityContext.AccountToken, this.BinaryDisplayEncoding, this.BinaryDisplayTypeValue, 0);
			case MqColumn.Ccsid:
				return NumberValue.New(message.Ccsid);
			case MqColumn.GroupId:
				return this.ValueFromBytes(message.GroupId, this.BinaryDisplayEncoding, this.BinaryDisplayTypeValue, 0);
			case MqColumn.LogicalSequenceNumber:
				return NumberValue.New(message.SequenceNumber);
			case MqColumn.MessageType:
				return message.MessageTypeString;
			case MqColumn.Offset:
				return NumberValue.New(message.Offset);
			case MqColumn.Persistence:
				return TextValue.NewOrNull(message.PersistenceString);
			case MqColumn.Priority:
				return NumberValue.New(message.PriorityInt);
			case MqColumn.ReplyToQueue:
				return TextValue.NewOrNull(message.ReplyToQueue);
			case MqColumn.ReplyToQueueManager:
				return TextValue.NewOrNull(message.ReplyToQueueManager);
			case MqColumn.MQRFH2:
				return this.GetHeaderValue(message, MqHeaderType.RulesAndFormattingVersion2);
			case MqColumn.MQCIH:
				return this.GetHeaderValue(message, MqHeaderType.CicsBridge);
			}
			ReceiveMessage receiveMessage = message as ReceiveMessage;
			if (receiveMessage != null)
			{
				if (column == MqColumn.OriginalLength)
				{
					return NumberValue.New(receiveMessage.OriginalLength);
				}
				if (column == MqColumn.MessageToken)
				{
					return Utilities.ValueFromBytes(receiveMessage.Token, this.BinaryDisplayEncoding, this.BinaryDisplayTypeValue, 0);
				}
			}
			return Value.Null;
		}

		// Token: 0x06004318 RID: 17176 RVA: 0x000E2138 File Offset: 0x000E0338
		private Value ValueFromBytes(byte[] value, Value encodingValue, TypeValue returnType, int ccsid = 0)
		{
			Value value2;
			try
			{
				value2 = Utilities.ValueFromBytes(value, encodingValue, returnType, ccsid);
			}
			catch (UnsupportedCcsidException ex)
			{
				using (IHostTrace hostTrace = TracingService.CreateTrace(this.host, "Engine/IO/Mq/Encoding", TraceEventType.Error, null))
				{
					hostTrace.Add("codepage", ccsid, false);
					hostTrace.Add("InnerException", ex.InnerException, false);
				}
				value2 = Utilities.ValueFromBytes(value, TextEncoding.Utf8, returnType, 0);
			}
			return value2;
		}

		// Token: 0x06004319 RID: 17177 RVA: 0x000E21C4 File Offset: 0x000E03C4
		private Value GetHeaderValue(Message message, MqHeaderType mqHeaderType)
		{
			if (message.Headers != null)
			{
				foreach (MqHeader mqHeader in message.Headers)
				{
					if (mqHeader.HeaderType == mqHeaderType)
					{
						return mqHeader.GetRecordValue(this.BinaryDisplayEncoding, this.BinaryDisplayTypeValue);
					}
				}
			}
			return Value.Null;
		}

		// Token: 0x0600431A RID: 17178 RVA: 0x000E2240 File Offset: 0x000E0440
		private static TypeValue GetTypeValue(Value encodingValue, TypeValue defaultType)
		{
			if (encodingValue.IsNull)
			{
				return defaultType;
			}
			if (!encodingValue.IsType)
			{
				return TypeValue.Text;
			}
			return encodingValue.AsType;
		}

		// Token: 0x0600431B RID: 17179 RVA: 0x000E2260 File Offset: 0x000E0460
		public IValueReference TransformMessage(Message message)
		{
			if (message == null || message.RealValue == null)
			{
				return Value.Null;
			}
			return this.CreateSchemaRecord(message);
		}

		// Token: 0x0600431C RID: 17180 RVA: 0x000E227A File Offset: 0x000E047A
		public IValueReference TransformException(Exception exception)
		{
			return new ExceptionValueReference(MqExceptionHandler.ToValueException(this.host, exception, this.ConnectionParameters.Resource));
		}

		// Token: 0x0600431D RID: 17181 RVA: 0x000E2298 File Offset: 0x000E0498
		public Exception WrapException(Exception exception, bool isOperationException)
		{
			Exception ex = MqExceptionHandler.ProcessMqException(this.host, exception, this.connectionParameters.Resource);
			if (isOperationException)
			{
				this.operationException = ex;
			}
			return ex;
		}

		// Token: 0x0600431E RID: 17182 RVA: 0x000E22C8 File Offset: 0x000E04C8
		public bool TryWrapException(Exception exception, bool isOperationException, out Exception wrappedException)
		{
			if (!SafeExceptions.IsSafeException(exception))
			{
				wrappedException = null;
				return false;
			}
			wrappedException = this.WrapException(exception, isOperationException);
			return true;
		}

		// Token: 0x0600431F RID: 17183 RVA: 0x000E22E2 File Offset: 0x000E04E2
		public static MqQuery New(IEngineHost host, MqConnectionParameters connectionParameters, IDictionary<MqFunctionOption, object> options)
		{
			return new MqQuery.BrowseQuery(host, connectionParameters, options, null, RowCount.Zero, null);
		}

		// Token: 0x04002336 RID: 9014
		public static readonly MqColumnCollection ColumnInfos = new MqColumnCollection
		{
			new MqColumnInfo(MqColumn.AccountingToken, "AccountingToken", TypeValue.Any, null, null, null, false, false),
			new MqColumnInfo(MqColumn.Ccsid, "Ccsid", TypeValue.Number, new MqColumnInfo.TryUnmarshallValue<Value, int?, object, bool>(Utilities.TryGetCcsid), Message.CcsidInfo, null, false, true),
			new MqColumnInfo(MqColumn.CorrelationId, "CorrelationId", TypeValue.Any, new MqColumnInfo.TryUnmarshallValue<Value, int?, object, bool>(Utilities.TryGetIdentifier), Message.CorrelatorInfo, new int?(24), true, true),
			new MqColumnInfo(MqColumn.Format, "Format", TypeValue.Text, new MqColumnInfo.TryUnmarshallValue<Value, int?, object, bool>(Utilities.TryGetString), Message.FormatInfo, new int?(8), true, true),
			new MqColumnInfo(MqColumn.GroupId, "GroupId", TypeValue.Any, new MqColumnInfo.TryUnmarshallValue<Value, int?, object, bool>(Utilities.TryGetIdentifier), Message.GroupIdInfo, new int?(24), true, false),
			new MqColumnInfo(MqColumn.LogicalSequenceNumber, "LogicalSequenceNumber", TypeValue.Number, new MqColumnInfo.TryUnmarshallValue<Value, int?, object, bool>(Utilities.TryGetInt), Message.SequenceNumberInfo, null, true, false),
			new MqColumnInfo(MqColumn.MessageData, "MessageData", TypeValue.Any, new MqColumnInfo.TryUnmarshallValue<Value, int?, object, bool>(Utilities.TryGetBinaryOrText), Message.DataInfo, null, false, true),
			new MqColumnInfo(MqColumn.MessageId, "MessageId", TypeValue.Any, new MqColumnInfo.TryUnmarshallValue<Value, int?, object, bool>(Utilities.TryGetIdentifier), Message.MessageIdInfo, new int?(24), true, true),
			new MqColumnInfo(MqColumn.MessageToken, "MessageToken", TypeValue.Any, new MqColumnInfo.TryUnmarshallValue<Value, int?, object, bool>(Utilities.TryGetIdentifier), ReceiveMessage.TokenInfo, new int?(16), true, false),
			new MqColumnInfo(MqColumn.MessageType, "MessageType", TypeValue.Text, new MqColumnInfo.TryUnmarshallValue<Value, int?, object, bool>(Utilities.TryGetMessageType), Message.MessageTypeInfo, null, false, true),
			new MqColumnInfo(MqColumn.MQCIH, "MQCIH", TypeValue.Record, null, null, null, false, false),
			new MqColumnInfo(MqColumn.MQRFH2, "MQRFH2", TypeValue.Record, null, null, null, false, false),
			new MqColumnInfo(MqColumn.Offset, "Offset", TypeValue.Number, new MqColumnInfo.TryUnmarshallValue<Value, int?, object, bool>(Utilities.TryGetInt), Message.OffsetInfo, null, true, true),
			new MqColumnInfo(MqColumn.OriginalLength, "OriginalLength", TypeValue.Number, null, ReceiveMessage.OriginalLengthInfo, null, false, false),
			new MqColumnInfo(MqColumn.Persistence, "Persistence", TypeValue.Text, null, Message.PersistenceInfo, null, false, false),
			new MqColumnInfo(MqColumn.Priority, "Priority", TypeValue.Number, null, Message.PriorityInfo, null, false, false),
			new MqColumnInfo(MqColumn.PutApplicationName, "PutApplicationName", TypeValue.Text, null, null, null, false, false),
			new MqColumnInfo(MqColumn.PutApplicationType, "PutApplicationType", TypeValue.Text, null, null, null, false, false),
			new MqColumnInfo(MqColumn.PutDateTime, "PutDateTime", TypeValue.DateTime, null, null, null, false, false),
			new MqColumnInfo(MqColumn.ReplyToQueue, "ReplyToQueue", TypeValue.Text, new MqColumnInfo.TryUnmarshallValue<Value, int?, object, bool>(Utilities.TryGetString), Message.ReplyToQueueInfo, new int?(48), false, true),
			new MqColumnInfo(MqColumn.ReplyToQueueManager, "ReplyToQueueManager", TypeValue.Text, new MqColumnInfo.TryUnmarshallValue<Value, int?, object, bool>(Utilities.TryGetString), Message.ReplyToQueueManagerInfo, new int?(48), false, true),
			new MqColumnInfo(MqColumn.UserIdentifier, "UserIdentifier", TypeValue.Text, null, null, null, false, false)
		};

		// Token: 0x04002337 RID: 9015
		private readonly IEngineHost host;

		// Token: 0x04002338 RID: 9016
		private readonly MqConnectionParameters connectionParameters;

		// Token: 0x04002339 RID: 9017
		private readonly IDictionary<MqFunctionOption, object> options;

		// Token: 0x0400233A RID: 9018
		private readonly IDictionary<MqColumn, object> filters;

		// Token: 0x0400233B RID: 9019
		private readonly ColumnSelection queryColumnSelection;

		// Token: 0x0400233C RID: 9020
		private readonly RowCount rowCount;

		// Token: 0x0400233D RID: 9021
		private List<IValueReference> cachedMessages;

		// Token: 0x0400233E RID: 9022
		private List<MqColumn> selectedColumns;

		// Token: 0x0400233F RID: 9023
		private RecordTypeValue recordType;

		// Token: 0x04002340 RID: 9024
		private IList<TableKey> tableKeys;

		// Token: 0x04002341 RID: 9025
		private TypeValue binaryDisplayTypeValue;

		// Token: 0x04002342 RID: 9026
		private TypeValue messageDataDisplayTypeValue;

		// Token: 0x04002343 RID: 9027
		private Exception operationException;

		// Token: 0x02000932 RID: 2354
		public abstract class MqGetQuery : MqQuery
		{
			// Token: 0x06004321 RID: 17185 RVA: 0x000E26CB File Offset: 0x000E08CB
			protected MqGetQuery(IEngineHost host, MqConnectionParameters connectionParameters, IDictionary<MqFunctionOption, object> options, IDictionary<MqColumn, object> filters, RowCount rowCount, ColumnSelection columnSelection)
				: base(host, connectionParameters, options, filters, rowCount, columnSelection)
			{
			}

			// Token: 0x06004322 RID: 17186
			public abstract MqOperation<IValueReference> BuildOperation();

			// Token: 0x06004323 RID: 17187 RVA: 0x000E26DC File Offset: 0x000E08DC
			public override IEnumerable<IValueReference> GetRows()
			{
				if (this.operationException != null)
				{
					throw this.operationException;
				}
				if (this.cachedMessages != null)
				{
					foreach (IValueReference valueReference in this.cachedMessages)
					{
						yield return valueReference;
					}
					List<IValueReference>.Enumerator enumerator = default(List<IValueReference>.Enumerator);
					yield break;
				}
				using (MqOperation<IValueReference> operation = this.BuildOperation())
				{
					this.cachedMessages = new List<IValueReference>();
					foreach (IValueReference valueReference2 in operation.Execute())
					{
						this.cachedMessages.Add(valueReference2);
						yield return valueReference2;
					}
					IEnumerator<IValueReference> enumerator2 = null;
				}
				MqOperation<IValueReference> operation = null;
				yield break;
				yield break;
			}
		}

		// Token: 0x02000934 RID: 2356
		private sealed class GetQuery : MqQuery.MqGetQuery
		{
			// Token: 0x0600432F RID: 17199 RVA: 0x000E29BF File Offset: 0x000E0BBF
			public GetQuery(IEngineHost host, MqConnectionParameters connectionParameters, IDictionary<MqFunctionOption, object> options, IDictionary<MqColumn, object> filters, RowCount rowCount, ColumnSelection columnSelection)
				: base(host, connectionParameters, options, filters, rowCount, columnSelection)
			{
			}

			// Token: 0x06004330 RID: 17200 RVA: 0x000E29D0 File Offset: 0x000E0BD0
			public override MqOperation<IValueReference> BuildOperation()
			{
				return MqOperation<IValueReference>.NewGetOperation(this);
			}
		}

		// Token: 0x02000935 RID: 2357
		private sealed class ClearQuery : MqQuery.MqGetQuery
		{
			// Token: 0x06004331 RID: 17201 RVA: 0x000E29BF File Offset: 0x000E0BBF
			public ClearQuery(IEngineHost host, MqConnectionParameters connectionParameters, IDictionary<MqFunctionOption, object> options, IDictionary<MqColumn, object> filters, RowCount rowCount, ColumnSelection columnSelection)
				: base(host, connectionParameters, options, filters, rowCount, columnSelection)
			{
			}

			// Token: 0x06004332 RID: 17202 RVA: 0x000E29D8 File Offset: 0x000E0BD8
			public override MqOperation<IValueReference> BuildOperation()
			{
				return MqOperation<IValueReference>.NewClearOperation(this);
			}
		}

		// Token: 0x02000936 RID: 2358
		private sealed class BrowseQuery : MqQuery.MqGetQuery
		{
			// Token: 0x06004333 RID: 17203 RVA: 0x000E29BF File Offset: 0x000E0BBF
			public BrowseQuery(IEngineHost host, MqConnectionParameters connectionParameters, IDictionary<MqFunctionOption, object> options, IDictionary<MqColumn, object> filters, RowCount rowCount, ColumnSelection columnSelection)
				: base(host, connectionParameters, options, filters, rowCount, columnSelection)
			{
			}

			// Token: 0x06004334 RID: 17204 RVA: 0x000E29E0 File Offset: 0x000E0BE0
			public override MqOperation<IValueReference> BuildOperation()
			{
				return MqOperation<IValueReference>.NewBrowseOperation(this);
			}

			// Token: 0x06004335 RID: 17205 RVA: 0x000E29E8 File Offset: 0x000E0BE8
			public override Query Take(RowCount count)
			{
				if (count.IsZero)
				{
					return base.Take(count);
				}
				RowCount rowCount = count;
				if (!this.rowCount.IsZero && count.Value != MqQuery.MaxRowCount.Value)
				{
					rowCount = new RowCount(Math.Min(this.rowCount.Value, count.Value));
				}
				return new MqQuery.BrowseQuery(this.host, this.connectionParameters, this.options, this.filters, rowCount, this.queryColumnSelection);
			}

			// Token: 0x06004336 RID: 17206 RVA: 0x000E2A74 File Offset: 0x000E0C74
			public override Query SelectColumns(ColumnSelection columnSelection)
			{
				ColumnSelection columnSelection2 = new ColumnSelection(MqQuery.ColumnInfos.AllKeys).SelectColumns(columnSelection);
				return new MqQuery.BrowseQuery(this.host, this.connectionParameters, this.options, this.filters, this.rowCount, columnSelection2);
			}

			// Token: 0x06004337 RID: 17207 RVA: 0x000E2ABB File Offset: 0x000E0CBB
			public override Query RenameReorderColumns(ColumnSelection columnSelection)
			{
				return this.SelectColumns(columnSelection);
			}

			// Token: 0x06004338 RID: 17208 RVA: 0x000E2AC4 File Offset: 0x000E0CC4
			public override Query SelectRows(FunctionValue condition)
			{
				QueryExpression queryExpression = QueryExpressionBuilder.ToQueryExpression(QueryTableValue.NewRowType(this), condition);
				Dictionary<MqColumn, object> dictionary = new Dictionary<MqColumn, object>();
				if (this.filters != null)
				{
					foreach (KeyValuePair<MqColumn, object> keyValuePair in this.filters)
					{
						dictionary.Add(keyValuePair.Key, keyValuePair.Value);
					}
				}
				List<QueryExpression> list = new List<QueryExpression>();
				foreach (QueryExpression queryExpression2 in SelectRowsQuery.GetConjunctiveNF(queryExpression))
				{
					MqColumn mqColumn;
					object obj;
					if (this.TryVisitBinaryExpression(queryExpression2, out mqColumn, out obj))
					{
						dictionary[mqColumn] = obj;
					}
					else
					{
						list.Add(queryExpression2);
					}
				}
				if (dictionary.Count == 0)
				{
					return base.SelectRows(condition);
				}
				Query query = new MqQuery.BrowseQuery(this.host, this.connectionParameters, this.options, dictionary, this.rowCount, this.queryColumnSelection);
				if (list.Count != 0)
				{
					return SelectRowsQuery.New(QueryExpressionAssembler.Assemble(this.Columns, SelectRowsQuery.CreateConjunctiveNF(list)), query);
				}
				return query;
			}

			// Token: 0x06004339 RID: 17209 RVA: 0x000E2BFC File Offset: 0x000E0DFC
			public override ActionValue DeleteRows()
			{
				this.host.VerifyActionPermitted(this.connectionParameters.Resource);
				return CountOnlyTableBindingActionValue.New((bool countOnlyTable) => this.DeleteRows(countOnlyTable));
			}

			// Token: 0x0600433A RID: 17210 RVA: 0x000E2C28 File Offset: 0x000E0E28
			private ActionValue DeleteRows(bool countOnlyTable)
			{
				if (countOnlyTable)
				{
					QueryTableValue queryTableValue = new QueryTableValue(new MqQuery.ClearQuery(this.host, this.connectionParameters, this.options, this.filters, this.rowCount, this.queryColumnSelection));
					return ActionValue.New(ListValue.New(new List<IValueReference>
					{
						ActionModule.Action.Return.Invoke(NumberValue.New(queryTableValue.Count)),
						new ReturnTypedTableFromCountFunctionValue(queryTableValue.Type.AsTableType)
					})).ClearCache(this.host);
				}
				return ActionValue.New(delegate
				{
					Value value;
					try
					{
						value = new QueryTableValue(new MqQuery.GetQuery(this.host, this.connectionParameters, this.options, this.filters, this.rowCount, this.queryColumnSelection));
					}
					finally
					{
						this.ClearCache();
					}
					return value;
				}).ClearCache(this.host);
			}

			// Token: 0x0600433B RID: 17211 RVA: 0x000E2CD0 File Offset: 0x000E0ED0
			public override ActionValue InsertRows(Query rowsToInsert)
			{
				this.host.VerifyActionPermitted(this.connectionParameters.Resource);
				HashSet<MqColumn> hashSet = new HashSet<MqColumn> { MqColumn.MessageData };
				List<MqColumnInfo> list = new List<MqColumnInfo>();
				foreach (string text in rowsToInsert.Columns)
				{
					MqColumnInfo mqColumnInfo;
					if (!MqQuery.ColumnInfos.TryGetWritableColumn(text, out mqColumnInfo))
					{
						throw ValueException.NewExpressionError<Message1>(Strings.Mq_ColumnNotWritable(text), null, null);
					}
					list.Add(mqColumnInfo);
					hashSet.Remove(mqColumnInfo.Column);
				}
				if (hashSet.Count > 0)
				{
					throw ValueException.NewExpressionError<Message0>(Strings.Mq_MessageDataMissing, null, null);
				}
				List<SendMessage> messagesToSend = new List<SendMessage>();
				foreach (IValueReference valueReference in new QueryTableValue(rowsToInsert))
				{
					SendMessage sendMessage = new SendMessage();
					RecordValue asRecord = valueReference.Value.AsRecord;
					int num = 0;
					foreach (MqColumnInfo mqColumnInfo2 in list)
					{
						Value value = asRecord[num];
						if (!value.IsNull)
						{
							object obj;
							if (mqColumnInfo2.TryConvertIntoObject(value, out obj))
							{
								try
								{
									mqColumnInfo2.SetProperty(sendMessage, obj);
									goto IL_0193;
								}
								catch (TargetInvocationException ex)
								{
									throw ValueException.NewExpressionError<Message2>(Strings.Mq_MessageInvalidValue(value.ToSource(), mqColumnInfo2.Name), value, ex.InnerException);
								}
							}
							throw ValueException.NewExpressionError<Message2>(Strings.Mq_MessageInvalidValue(value.ToSource(), mqColumnInfo2.Name), value, null);
						}
						if (mqColumnInfo2.Column == MqColumn.MessageData)
						{
							throw ValueException.NewExpressionError<Message2>(Strings.Mq_MessageInvalidValue("null", "MessageData"), value, null);
						}
						IL_0193:
						num++;
					}
					messagesToSend.Add(sendMessage);
				}
				if (messagesToSend.Count == 0)
				{
					throw ValueException.NewExpressionError<Message0>(Strings.Mq_NoMessagesToSend, null, null);
				}
				return ActionValue.New(delegate
				{
					Value value2;
					try
					{
						value2 = new QueryTableValue(new MqQuery.PutQuery(this.host, this.connectionParameters, this.options, this.queryColumnSelection, messagesToSend));
					}
					finally
					{
						this.ClearCache();
					}
					return value2;
				}).ClearCache(this.host);
			}

			// Token: 0x0600433C RID: 17212 RVA: 0x000E2F54 File Offset: 0x000E1154
			private bool TryVisitBinaryExpression(QueryExpression expression, out MqColumn filterColumn, out object filterValue)
			{
				BinaryQueryExpression binaryQueryExpression = expression as BinaryQueryExpression;
				if (binaryQueryExpression != null && binaryQueryExpression.Operator == BinaryOperator2.Equals)
				{
					QueryExpression left = binaryQueryExpression.Left;
					QueryExpression right = binaryQueryExpression.Right;
					ColumnAccessQueryExpression columnAccessQueryExpression = null;
					ConstantQueryExpression constantQueryExpression = null;
					if (left.Kind == QueryExpressionKind.ColumnAccess && right.Kind == QueryExpressionKind.Constant)
					{
						columnAccessQueryExpression = (ColumnAccessQueryExpression)left;
						constantQueryExpression = (ConstantQueryExpression)right;
					}
					else if (right.Kind == QueryExpressionKind.ColumnAccess && left.Kind == QueryExpressionKind.Constant)
					{
						columnAccessQueryExpression = (ColumnAccessQueryExpression)right;
						constantQueryExpression = (ConstantQueryExpression)left;
					}
					if (columnAccessQueryExpression != null)
					{
						string text = this.Columns[columnAccessQueryExpression.Column];
						MqColumnInfo mqColumnInfo;
						if (MqQuery.ColumnInfos.TryGetValue(text, out mqColumnInfo) && mqColumnInfo.IsFoldable)
						{
							if (mqColumnInfo.TryConvertIntoObject(constantQueryExpression.Value, out filterValue))
							{
								filterColumn = mqColumnInfo.Column;
								return true;
							}
							throw ValueException.NewDataSourceError<Message2>(DataSourceException.DataSourceMessage("MQ", Strings.MqUnsupportedQueryOption(mqColumnInfo.Name, constantQueryExpression.Value.ToSource())), Value.Null, null);
						}
					}
				}
				filterColumn = MqColumn.None;
				filterValue = null;
				return false;
			}

			// Token: 0x0600433D RID: 17213 RVA: 0x000E3057 File Offset: 0x000E1257
			private void ClearCache()
			{
				this.cachedMessages = null;
				this.operationException = null;
			}
		}

		// Token: 0x02000938 RID: 2360
		private sealed class PutQuery : MqQuery
		{
			// Token: 0x06004342 RID: 17218 RVA: 0x000E3134 File Offset: 0x000E1334
			public PutQuery(IEngineHost host, MqConnectionParameters connectionParameters, IDictionary<MqFunctionOption, object> options, ColumnSelection columnSelection, List<SendMessage> messages)
				: base(host, connectionParameters, options, null, MqQuery.MaxRowCount, columnSelection)
			{
				this.messages = messages;
			}

			// Token: 0x06004343 RID: 17219 RVA: 0x000E3150 File Offset: 0x000E1350
			public override IEnumerable<IValueReference> GetRows()
			{
				if (this.operationException != null)
				{
					throw this.operationException;
				}
				if (this.cachedMessages == null)
				{
					this.cachedMessages = new List<IValueReference>();
					using (MqOperation<IValueReference> mqOperation = MqOperation<IValueReference>.NewPutOperation(this, this.messages))
					{
						foreach (IValueReference valueReference in mqOperation.Execute())
						{
							this.cachedMessages.Add(valueReference);
						}
					}
				}
				return this.cachedMessages;
			}

			// Token: 0x0400234D RID: 9037
			private readonly List<SendMessage> messages;
		}
	}
}
