using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.Internal.Materialization;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Query.PlanCompiler;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Core.Query.ResultAssembly
{
	// Token: 0x02000328 RID: 808
	internal class BridgeDataReader : DbDataReader, IExtendedDataRecord, IDataRecord
	{
		// Token: 0x0600266F RID: 9839 RVA: 0x0006ECE4 File Offset: 0x0006CEE4
		internal BridgeDataReader(Shaper<RecordState> shaper, CoordinatorFactory<RecordState> coordinatorFactory, int depth, IEnumerator<KeyValuePair<Shaper<RecordState>, CoordinatorFactory<RecordState>>> nextResultShaperInfos)
		{
			BridgeDataReader <>4__this = this;
			this._nextResultShaperInfoEnumerator = nextResultShaperInfos;
			this._initialize = delegate
			{
				<>4__this.SetShaper(shaper, coordinatorFactory, depth);
			};
			this._initializeAsync = (CancellationToken ct) => <>4__this.SetShaperAsync(shaper, coordinatorFactory, depth, ct);
		}

		// Token: 0x06002670 RID: 9840 RVA: 0x0006ED45 File Offset: 0x0006CF45
		protected virtual void EnsureInitialized()
		{
			if (Interlocked.CompareExchange(ref this._initialized, 1, 0) == 0)
			{
				this._initialize();
			}
		}

		// Token: 0x06002671 RID: 9841 RVA: 0x0006ED61 File Offset: 0x0006CF61
		protected virtual Task EnsureInitializedAsync(CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			if (Interlocked.CompareExchange(ref this._initialized, 1, 0) != 0)
			{
				return Task.FromResult<object>(null);
			}
			return this._initializeAsync(cancellationToken);
		}

		// Token: 0x06002672 RID: 9842 RVA: 0x0006ED8C File Offset: 0x0006CF8C
		private void SetShaper(Shaper<RecordState> shaper, CoordinatorFactory<RecordState> coordinatorFactory, int depth)
		{
			this._shaper = shaper;
			this._coordinatorFactory = coordinatorFactory;
			this._dataRecord = new BridgeDataRecord(shaper, depth);
			if (!this._shaper.DataWaiting)
			{
				this._shaper.DataWaiting = this._shaper.RootEnumerator.MoveNext();
			}
			this.InitializeHasRows();
		}

		// Token: 0x06002673 RID: 9843 RVA: 0x0006EDE4 File Offset: 0x0006CFE4
		private async Task SetShaperAsync(Shaper<RecordState> shaper, CoordinatorFactory<RecordState> coordinatorFactory, int depth, CancellationToken cancellationToken)
		{
			this._shaper = shaper;
			this._coordinatorFactory = coordinatorFactory;
			this._dataRecord = new BridgeDataRecord(shaper, depth);
			if (!this._shaper.DataWaiting)
			{
				Shaper<RecordState> shaper2 = this._shaper;
				bool flag = await this._shaper.RootEnumerator.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>();
				shaper2.DataWaiting = flag;
				shaper2 = null;
			}
			this.InitializeHasRows();
		}

		// Token: 0x06002674 RID: 9844 RVA: 0x0006EE4C File Offset: 0x0006D04C
		private void InitializeHasRows()
		{
			this._hasRows = false;
			if (this._shaper.DataWaiting)
			{
				RecordState recordState = this._shaper.RootEnumerator.Current;
				if (recordState != null)
				{
					this._hasRows = recordState.CoordinatorFactory == this._coordinatorFactory;
				}
			}
			this._defaultRecordState = this._coordinatorFactory.GetDefaultRecordState(this._shaper);
		}

		// Token: 0x06002675 RID: 9845 RVA: 0x0006EEAC File Offset: 0x0006D0AC
		private void AssertReaderIsOpen(string methodName)
		{
			if (this.IsClosed)
			{
				if (this._dataRecord.IsImplicitlyClosed)
				{
					throw Error.ADP_ImplicitlyClosedDataReaderError();
				}
				if (this._dataRecord.IsExplicitlyClosed)
				{
					throw Error.ADP_DataReaderClosed(methodName);
				}
			}
		}

		// Token: 0x06002676 RID: 9846 RVA: 0x0006EEDD File Offset: 0x0006D0DD
		internal void CloseImplicitly()
		{
			this.EnsureInitialized();
			this.Consume();
			this._dataRecord.CloseImplicitly();
		}

		// Token: 0x06002677 RID: 9847 RVA: 0x0006EEF8 File Offset: 0x0006D0F8
		internal async Task CloseImplicitlyAsync(CancellationToken cancellationToken)
		{
			await this.EnsureInitializedAsync(cancellationToken).WithCurrentCulture();
			await this.ConsumeAsync(cancellationToken).WithCurrentCulture();
			await this._dataRecord.CloseImplicitlyAsync(cancellationToken).WithCurrentCulture();
		}

		// Token: 0x06002678 RID: 9848 RVA: 0x0006EF45 File Offset: 0x0006D145
		private void Consume()
		{
			while (this.ReadInternal())
			{
			}
		}

		// Token: 0x06002679 RID: 9849 RVA: 0x0006EF50 File Offset: 0x0006D150
		private async Task ConsumeAsync(CancellationToken cancellationToken)
		{
			global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter;
			do
			{
				cultureAwaiter = this.ReadInternalAsync(cancellationToken).WithCurrentCulture<bool>().GetAwaiter();
				if (!cultureAwaiter.IsCompleted)
				{
					await cultureAwaiter;
					global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter2;
					cultureAwaiter = cultureAwaiter2;
					cultureAwaiter2 = default(global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool>);
				}
			}
			while (cultureAwaiter.GetResult());
		}

		// Token: 0x0600267A RID: 9850 RVA: 0x0006EFA0 File Offset: 0x0006D1A0
		internal static Type GetClrTypeFromTypeMetadata(TypeUsage typeUsage)
		{
			PrimitiveType primitiveType;
			Type type;
			if (TypeHelpers.TryGetEdmType<PrimitiveType>(typeUsage, out primitiveType))
			{
				type = primitiveType.ClrEquivalentType;
			}
			else if (TypeSemantics.IsReferenceType(typeUsage))
			{
				type = typeof(EntityKey);
			}
			else if (TypeUtils.IsStructuredType(typeUsage))
			{
				type = typeof(DbDataRecord);
			}
			else if (TypeUtils.IsCollectionType(typeUsage))
			{
				type = typeof(DbDataReader);
			}
			else if (TypeUtils.IsEnumerationType(typeUsage))
			{
				type = ((EnumType)typeUsage.EdmType).UnderlyingType.ClrEquivalentType;
			}
			else
			{
				type = typeof(object);
			}
			return type;
		}

		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x0600267B RID: 9851 RVA: 0x0006F02B File Offset: 0x0006D22B
		public override int Depth
		{
			get
			{
				this.EnsureInitialized();
				this.AssertReaderIsOpen("Depth");
				return this._dataRecord.Depth;
			}
		}

		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x0600267C RID: 9852 RVA: 0x0006F049 File Offset: 0x0006D249
		public override bool HasRows
		{
			get
			{
				this.EnsureInitialized();
				this.AssertReaderIsOpen("HasRows");
				return this._hasRows;
			}
		}

		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x0600267D RID: 9853 RVA: 0x0006F062 File Offset: 0x0006D262
		public override bool IsClosed
		{
			get
			{
				this.EnsureInitialized();
				return this._isClosed || this._dataRecord.IsClosed;
			}
		}

		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x0600267E RID: 9854 RVA: 0x0006F080 File Offset: 0x0006D280
		public override int RecordsAffected
		{
			get
			{
				this.EnsureInitialized();
				int num = -1;
				if (this._dataRecord.Depth == 0)
				{
					num = this._shaper.Reader.RecordsAffected;
				}
				return num;
			}
		}

		// Token: 0x0600267F RID: 9855 RVA: 0x0006F0B4 File Offset: 0x0006D2B4
		public override void Close()
		{
			this.EnsureInitialized();
			this._dataRecord.CloseExplicitly();
			if (!this._isClosed)
			{
				this._isClosed = true;
				if (this._dataRecord.Depth == 0)
				{
					this._shaper.Reader.Close();
				}
				else
				{
					this.Consume();
				}
			}
			if (this._nextResultShaperInfoEnumerator != null)
			{
				this._nextResultShaperInfoEnumerator.Dispose();
				this._nextResultShaperInfoEnumerator = null;
			}
		}

		// Token: 0x06002680 RID: 9856 RVA: 0x0006F120 File Offset: 0x0006D320
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override IEnumerator GetEnumerator()
		{
			return new DbEnumerator(this, true);
		}

		// Token: 0x06002681 RID: 9857 RVA: 0x0006F129 File Offset: 0x0006D329
		public override DataTable GetSchemaTable()
		{
			throw new NotSupportedException(Strings.ADP_GetSchemaTableIsNotSupported);
		}

		// Token: 0x06002682 RID: 9858 RVA: 0x0006F138 File Offset: 0x0006D338
		public override bool NextResult()
		{
			this.EnsureInitialized();
			this.AssertReaderIsOpen("NextResult");
			if (this._nextResultShaperInfoEnumerator != null && this._shaper.Reader.NextResult() && this._nextResultShaperInfoEnumerator.MoveNext())
			{
				KeyValuePair<Shaper<RecordState>, CoordinatorFactory<RecordState>> keyValuePair = this._nextResultShaperInfoEnumerator.Current;
				this._dataRecord.CloseImplicitly();
				this.SetShaper(keyValuePair.Key, keyValuePair.Value, 0);
				return true;
			}
			if (this._dataRecord.Depth == 0)
			{
				CommandHelper.ConsumeReader(this._shaper.Reader);
			}
			else
			{
				this.Consume();
			}
			this.CloseImplicitly();
			this._dataRecord.SetRecordSource(null, false);
			return false;
		}

		// Token: 0x06002683 RID: 9859 RVA: 0x0006F1E4 File Offset: 0x0006D3E4
		public override async Task<bool> NextResultAsync(CancellationToken cancellationToken)
		{
			await this.EnsureInitializedAsync(cancellationToken).WithCurrentCulture();
			this.AssertReaderIsOpen("NextResult");
			bool flag = this._nextResultShaperInfoEnumerator != null;
			if (flag)
			{
				flag = await this._shaper.Reader.NextResultAsync(cancellationToken).WithCurrentCulture<bool>();
			}
			bool flag2;
			if (flag && this._nextResultShaperInfoEnumerator.MoveNext())
			{
				KeyValuePair<Shaper<RecordState>, CoordinatorFactory<RecordState>> nextResultShaperInfo = this._nextResultShaperInfoEnumerator.Current;
				await this._dataRecord.CloseImplicitlyAsync(cancellationToken).WithCurrentCulture();
				this.SetShaper(nextResultShaperInfo.Key, nextResultShaperInfo.Value, 0);
				flag2 = true;
			}
			else
			{
				if (this._dataRecord.Depth == 0)
				{
					await CommandHelper.ConsumeReaderAsync(this._shaper.Reader, cancellationToken).WithCurrentCulture();
				}
				else
				{
					await this.ConsumeAsync(cancellationToken).WithCurrentCulture();
				}
				await this.CloseImplicitlyAsync(cancellationToken).WithCurrentCulture();
				this._dataRecord.SetRecordSource(null, false);
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x06002684 RID: 9860 RVA: 0x0006F234 File Offset: 0x0006D434
		public override bool Read()
		{
			this.EnsureInitialized();
			this.AssertReaderIsOpen("Read");
			this._dataRecord.CloseImplicitly();
			bool flag = this.ReadInternal();
			this._dataRecord.SetRecordSource(this._shaper.RootEnumerator.Current, flag);
			return flag;
		}

		// Token: 0x06002685 RID: 9861 RVA: 0x0006F284 File Offset: 0x0006D484
		public override async Task<bool> ReadAsync(CancellationToken cancellationToken)
		{
			await this.EnsureInitializedAsync(cancellationToken).WithCurrentCulture();
			this.AssertReaderIsOpen("Read");
			await this._dataRecord.CloseImplicitlyAsync(cancellationToken).WithCurrentCulture();
			bool flag = await this.ReadInternalAsync(cancellationToken).WithCurrentCulture<bool>();
			this._dataRecord.SetRecordSource(this._shaper.RootEnumerator.Current, flag);
			return flag;
		}

		// Token: 0x06002686 RID: 9862 RVA: 0x0006F2D4 File Offset: 0x0006D4D4
		private bool ReadInternal()
		{
			bool flag = false;
			if (!this._shaper.DataWaiting)
			{
				this._shaper.DataWaiting = this._shaper.RootEnumerator.MoveNext();
			}
			while (this._shaper.DataWaiting && this._shaper.RootEnumerator.Current.CoordinatorFactory != this._coordinatorFactory && this._shaper.RootEnumerator.Current.CoordinatorFactory.Depth > this._coordinatorFactory.Depth)
			{
				this._shaper.DataWaiting = this._shaper.RootEnumerator.MoveNext();
			}
			if (this._shaper.DataWaiting && this._shaper.RootEnumerator.Current.CoordinatorFactory == this._coordinatorFactory)
			{
				this._shaper.DataWaiting = false;
				this._shaper.RootEnumerator.Current.AcceptPendingValues();
				flag = true;
			}
			return flag;
		}

		// Token: 0x06002687 RID: 9863 RVA: 0x0006F3C8 File Offset: 0x0006D5C8
		private async Task<bool> ReadInternalAsync(CancellationToken cancellationToken)
		{
			bool result = false;
			if (!this._shaper.DataWaiting)
			{
				Shaper<RecordState> shaper = this._shaper;
				bool flag = await this._shaper.RootEnumerator.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>();
				shaper.DataWaiting = flag;
				shaper = null;
			}
			while (this._shaper.DataWaiting && this._shaper.RootEnumerator.Current.CoordinatorFactory != this._coordinatorFactory && this._shaper.RootEnumerator.Current.CoordinatorFactory.Depth > this._coordinatorFactory.Depth)
			{
				Shaper<RecordState> shaper = this._shaper;
				bool flag = await this._shaper.RootEnumerator.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>();
				shaper.DataWaiting = flag;
				shaper = null;
			}
			if (this._shaper.DataWaiting && this._shaper.RootEnumerator.Current.CoordinatorFactory == this._coordinatorFactory)
			{
				this._shaper.DataWaiting = false;
				this._shaper.RootEnumerator.Current.AcceptPendingValues();
				result = true;
			}
			return result;
		}

		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x06002688 RID: 9864 RVA: 0x0006F415 File Offset: 0x0006D615
		public override int FieldCount
		{
			get
			{
				this.EnsureInitialized();
				this.AssertReaderIsOpen("FieldCount");
				return this._defaultRecordState.ColumnCount;
			}
		}

		// Token: 0x06002689 RID: 9865 RVA: 0x0006F434 File Offset: 0x0006D634
		public override string GetDataTypeName(int ordinal)
		{
			this.EnsureInitialized();
			this.AssertReaderIsOpen("GetDataTypeName");
			string text;
			if (this._dataRecord.HasData)
			{
				text = this._dataRecord.GetDataTypeName(ordinal);
			}
			else
			{
				text = this._defaultRecordState.GetTypeUsage(ordinal).ToString();
			}
			return text;
		}

		// Token: 0x0600268A RID: 9866 RVA: 0x0006F484 File Offset: 0x0006D684
		public override Type GetFieldType(int ordinal)
		{
			this.EnsureInitialized();
			this.AssertReaderIsOpen("GetFieldType");
			Type type;
			if (this._dataRecord.HasData)
			{
				type = this._dataRecord.GetFieldType(ordinal);
			}
			else
			{
				type = BridgeDataReader.GetClrTypeFromTypeMetadata(this._defaultRecordState.GetTypeUsage(ordinal));
			}
			return type;
		}

		// Token: 0x0600268B RID: 9867 RVA: 0x0006F4D4 File Offset: 0x0006D6D4
		public override string GetName(int ordinal)
		{
			this.EnsureInitialized();
			this.AssertReaderIsOpen("GetName");
			string text;
			if (this._dataRecord.HasData)
			{
				text = this._dataRecord.GetName(ordinal);
			}
			else
			{
				text = this._defaultRecordState.GetName(ordinal);
			}
			return text;
		}

		// Token: 0x0600268C RID: 9868 RVA: 0x0006F51C File Offset: 0x0006D71C
		public override int GetOrdinal(string name)
		{
			this.EnsureInitialized();
			this.AssertReaderIsOpen("GetOrdinal");
			int num;
			if (this._dataRecord.HasData)
			{
				num = this._dataRecord.GetOrdinal(name);
			}
			else
			{
				num = this._defaultRecordState.GetOrdinal(name);
			}
			return num;
		}

		// Token: 0x0600268D RID: 9869 RVA: 0x0006F564 File Offset: 0x0006D764
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Type GetProviderSpecificFieldType(int ordinal)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000829 RID: 2089
		public override object this[int ordinal]
		{
			get
			{
				this.EnsureInitialized();
				return this._dataRecord[ordinal];
			}
		}

		// Token: 0x1700082A RID: 2090
		public override object this[string name]
		{
			get
			{
				this.EnsureInitialized();
				int ordinal = this.GetOrdinal(name);
				return this._dataRecord[ordinal];
			}
		}

		// Token: 0x06002690 RID: 9872 RVA: 0x0006F5A7 File Offset: 0x0006D7A7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override object GetProviderSpecificValue(int ordinal)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002691 RID: 9873 RVA: 0x0006F5AE File Offset: 0x0006D7AE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetProviderSpecificValues(object[] values)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002692 RID: 9874 RVA: 0x0006F5B5 File Offset: 0x0006D7B5
		public override object GetValue(int ordinal)
		{
			this.EnsureInitialized();
			return this._dataRecord.GetValue(ordinal);
		}

		// Token: 0x06002693 RID: 9875 RVA: 0x0006F5CC File Offset: 0x0006D7CC
		public override async Task<T> GetFieldValueAsync<T>(int ordinal, CancellationToken cancellationToken)
		{
			await this.EnsureInitializedAsync(cancellationToken).WithCurrentCulture();
			return await base.GetFieldValueAsync<T>(ordinal, cancellationToken).WithCurrentCulture<T>();
		}

		// Token: 0x06002694 RID: 9876 RVA: 0x0006F621 File Offset: 0x0006D821
		public override int GetValues(object[] values)
		{
			this.EnsureInitialized();
			return this._dataRecord.GetValues(values);
		}

		// Token: 0x06002695 RID: 9877 RVA: 0x0006F635 File Offset: 0x0006D835
		public override bool GetBoolean(int ordinal)
		{
			this.EnsureInitialized();
			return this._dataRecord.GetBoolean(ordinal);
		}

		// Token: 0x06002696 RID: 9878 RVA: 0x0006F649 File Offset: 0x0006D849
		public override byte GetByte(int ordinal)
		{
			this.EnsureInitialized();
			return this._dataRecord.GetByte(ordinal);
		}

		// Token: 0x06002697 RID: 9879 RVA: 0x0006F65D File Offset: 0x0006D85D
		public override char GetChar(int ordinal)
		{
			this.EnsureInitialized();
			return this._dataRecord.GetChar(ordinal);
		}

		// Token: 0x06002698 RID: 9880 RVA: 0x0006F671 File Offset: 0x0006D871
		public override DateTime GetDateTime(int ordinal)
		{
			this.EnsureInitialized();
			return this._dataRecord.GetDateTime(ordinal);
		}

		// Token: 0x06002699 RID: 9881 RVA: 0x0006F685 File Offset: 0x0006D885
		public override decimal GetDecimal(int ordinal)
		{
			this.EnsureInitialized();
			return this._dataRecord.GetDecimal(ordinal);
		}

		// Token: 0x0600269A RID: 9882 RVA: 0x0006F699 File Offset: 0x0006D899
		public override double GetDouble(int ordinal)
		{
			this.EnsureInitialized();
			return this._dataRecord.GetDouble(ordinal);
		}

		// Token: 0x0600269B RID: 9883 RVA: 0x0006F6AD File Offset: 0x0006D8AD
		public override float GetFloat(int ordinal)
		{
			this.EnsureInitialized();
			return this._dataRecord.GetFloat(ordinal);
		}

		// Token: 0x0600269C RID: 9884 RVA: 0x0006F6C1 File Offset: 0x0006D8C1
		public override Guid GetGuid(int ordinal)
		{
			this.EnsureInitialized();
			return this._dataRecord.GetGuid(ordinal);
		}

		// Token: 0x0600269D RID: 9885 RVA: 0x0006F6D5 File Offset: 0x0006D8D5
		public override short GetInt16(int ordinal)
		{
			this.EnsureInitialized();
			return this._dataRecord.GetInt16(ordinal);
		}

		// Token: 0x0600269E RID: 9886 RVA: 0x0006F6E9 File Offset: 0x0006D8E9
		public override int GetInt32(int ordinal)
		{
			this.EnsureInitialized();
			return this._dataRecord.GetInt32(ordinal);
		}

		// Token: 0x0600269F RID: 9887 RVA: 0x0006F6FD File Offset: 0x0006D8FD
		public override long GetInt64(int ordinal)
		{
			this.EnsureInitialized();
			return this._dataRecord.GetInt64(ordinal);
		}

		// Token: 0x060026A0 RID: 9888 RVA: 0x0006F711 File Offset: 0x0006D911
		public override string GetString(int ordinal)
		{
			this.EnsureInitialized();
			return this._dataRecord.GetString(ordinal);
		}

		// Token: 0x060026A1 RID: 9889 RVA: 0x0006F725 File Offset: 0x0006D925
		public override bool IsDBNull(int ordinal)
		{
			this.EnsureInitialized();
			return this._dataRecord.IsDBNull(ordinal);
		}

		// Token: 0x060026A2 RID: 9890 RVA: 0x0006F739 File Offset: 0x0006D939
		public override long GetBytes(int ordinal, long dataOffset, byte[] buffer, int bufferOffset, int length)
		{
			this.EnsureInitialized();
			return this._dataRecord.GetBytes(ordinal, dataOffset, buffer, bufferOffset, length);
		}

		// Token: 0x060026A3 RID: 9891 RVA: 0x0006F753 File Offset: 0x0006D953
		public override long GetChars(int ordinal, long dataOffset, char[] buffer, int bufferOffset, int length)
		{
			this.EnsureInitialized();
			return this._dataRecord.GetChars(ordinal, dataOffset, buffer, bufferOffset, length);
		}

		// Token: 0x060026A4 RID: 9892 RVA: 0x0006F76D File Offset: 0x0006D96D
		protected override DbDataReader GetDbDataReader(int ordinal)
		{
			this.EnsureInitialized();
			return (DbDataReader)this._dataRecord.GetData(ordinal);
		}

		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x060026A5 RID: 9893 RVA: 0x0006F788 File Offset: 0x0006D988
		public DataRecordInfo DataRecordInfo
		{
			get
			{
				this.EnsureInitialized();
				this.AssertReaderIsOpen("DataRecordInfo");
				DataRecordInfo dataRecordInfo;
				if (this._dataRecord.HasData)
				{
					dataRecordInfo = this._dataRecord.DataRecordInfo;
				}
				else
				{
					dataRecordInfo = this._defaultRecordState.DataRecordInfo;
				}
				return dataRecordInfo;
			}
		}

		// Token: 0x060026A6 RID: 9894 RVA: 0x0006F7CE File Offset: 0x0006D9CE
		public DbDataRecord GetDataRecord(int ordinal)
		{
			this.EnsureInitialized();
			return this._dataRecord.GetDataRecord(ordinal);
		}

		// Token: 0x060026A7 RID: 9895 RVA: 0x0006F7E2 File Offset: 0x0006D9E2
		public DbDataReader GetDataReader(int ordinal)
		{
			this.EnsureInitialized();
			return this.GetDbDataReader(ordinal);
		}

		// Token: 0x04000D70 RID: 3440
		private Shaper<RecordState> _shaper;

		// Token: 0x04000D71 RID: 3441
		private IEnumerator<KeyValuePair<Shaper<RecordState>, CoordinatorFactory<RecordState>>> _nextResultShaperInfoEnumerator;

		// Token: 0x04000D72 RID: 3442
		private CoordinatorFactory<RecordState> _coordinatorFactory;

		// Token: 0x04000D73 RID: 3443
		private RecordState _defaultRecordState;

		// Token: 0x04000D74 RID: 3444
		private BridgeDataRecord _dataRecord;

		// Token: 0x04000D75 RID: 3445
		private bool _hasRows;

		// Token: 0x04000D76 RID: 3446
		private bool _isClosed;

		// Token: 0x04000D77 RID: 3447
		private int _initialized;

		// Token: 0x04000D78 RID: 3448
		private readonly Action _initialize;

		// Token: 0x04000D79 RID: 3449
		private readonly Func<CancellationToken, Task> _initializeAsync;
	}
}
