using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeResultWriter
{
	// Token: 0x02000044 RID: 68
	internal sealed class DataShapeWriter : DsrCalculationContainerWriterBase
	{
		// Token: 0x06000170 RID: 368 RVA: 0x00004973 File Offset: 0x00002B73
		internal void WriteId(string value)
		{
			base.Writer.WriteProperty(base.DsrNames.Id, value);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000498C File Offset: 0x00002B8C
		internal CollectionWriter<DataShapeWriter> BeginDataShapes()
		{
			base.Writer.BeginProperty(base.DsrNames.DataShapes);
			base.CreateAndBeginChild<DataShapeWriter>(ref this._dataShapesWriter);
			return this._dataShapesWriter;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x000049B7 File Offset: 0x00002BB7
		internal CollectionWriter<DataMemberWriter> BeginPrimaryHierarchy()
		{
			base.Writer.BeginProperty(base.DsrNames.PrimaryHierarchy);
			base.CreateAndBeginChild<DataMemberWriter>(ref this._membersWriter);
			return this._membersWriter;
		}

		// Token: 0x06000173 RID: 371 RVA: 0x000049E2 File Offset: 0x00002BE2
		internal CollectionWriter<DataMemberWriter> BeginSecondaryHierarchy()
		{
			base.Writer.BeginProperty(base.DsrNames.SecondaryHierarchy);
			base.CreateAndBeginChild<DataMemberWriter>(ref this._membersWriter);
			return this._membersWriter;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00004A0D File Offset: 0x00002C0D
		internal CollectionWriter<DataLimitWriter> BeginDataLimits()
		{
			base.Writer.BeginProperty(base.DsrNames.DataLimitsExceeded);
			base.CreateAndBeginChild<DataLimitWriter>(ref this._limitsWriter);
			return this._limitsWriter;
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00004A38 File Offset: 0x00002C38
		internal CollectionWriter<MessageWriter> BeginMessages()
		{
			base.Writer.BeginProperty(base.DsrNames.DataShapeMessages);
			base.CreateAndBeginChild<MessageWriter>(ref this._messagesWriter);
			return this._messagesWriter;
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00004A63 File Offset: 0x00002C63
		internal CollectionWriter<DataWindowWriter> BeginDataWindows()
		{
			base.Writer.BeginProperty(base.DsrNames.DataWindows);
			base.CreateAndBeginChild<DataWindowWriter>(ref this._dataWindowsWriter);
			return this._dataWindowsWriter;
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00004A8E File Offset: 0x00002C8E
		internal RestartTokenCollectionWriter BeginRestartTokens()
		{
			base.Writer.BeginProperty(base.DsrNames.RestartTokens);
			base.CreateAndBeginChild<RestartTokenCollectionWriter>(ref this._restartTokensWriter);
			return this._restartTokensWriter;
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00004AB9 File Offset: 0x00002CB9
		internal ErrorWriter BeginError()
		{
			base.Writer.BeginProperty(base.DsrNames.OdataError);
			base.CreateAndBeginChild<ErrorWriter>(ref this._errorWriter);
			return this._errorWriter;
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00004AE4 File Offset: 0x00002CE4
		internal DictionaryValuesWriter BeginValueDictionaries()
		{
			base.Writer.BeginProperty(base.DsrNames.ValueDictionaries);
			base.CreateAndBeginChild<DictionaryValuesWriter>(ref this._dictValuesWriter);
			return this._dictValuesWriter;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00004B0F File Offset: 0x00002D0F
		internal void WriteIsComplete(bool isComplete)
		{
			base.Writer.WriteProperty(base.DsrNames.IsComplete, isComplete);
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00004B28 File Offset: 0x00002D28
		internal void WriteHasAllData()
		{
			base.Writer.WriteProperty(base.DsrNames.HasAllData, true);
		}

		// Token: 0x040000A6 RID: 166
		private CollectionWriter<DataShapeWriter> _dataShapesWriter;

		// Token: 0x040000A7 RID: 167
		private CollectionWriter<DataMemberWriter> _membersWriter;

		// Token: 0x040000A8 RID: 168
		private CollectionWriter<DataLimitWriter> _limitsWriter;

		// Token: 0x040000A9 RID: 169
		private CollectionWriter<MessageWriter> _messagesWriter;

		// Token: 0x040000AA RID: 170
		private CollectionWriter<DataWindowWriter> _dataWindowsWriter;

		// Token: 0x040000AB RID: 171
		private RestartTokenCollectionWriter _restartTokensWriter;

		// Token: 0x040000AC RID: 172
		private ErrorWriter _errorWriter;

		// Token: 0x040000AD RID: 173
		private DictionaryValuesWriter _dictValuesWriter;
	}
}
