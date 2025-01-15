using System;
using System.Data;
using System.Data.SqlTypes;
using System.IO;
using Microsoft.DataIntegration.FuzzyMatching;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;
using Microsoft.SqlServer.Server;

// Token: 0x02000002 RID: 2
[SqlUserDefinedAggregate(2, MaxByteSize = 4, IsInvariantToDuplicates = false, IsInvariantToNulls = true, IsInvariantToOrder = true, Name = "AddRecord", IsNullIfEmpty = false)]
[Serializable]
public class AddRecord : IBinarySerialize
{
	// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	public void Init()
	{
		this.m_recordCount = 0;
		this.m_updateContext = null;
		this.m_recordUpdate = null;
	}

	// Token: 0x06000002 RID: 2 RVA: 0x00002068 File Offset: 0x00000268
	public void Accumulate(int rowsetConsumerHandle, string rowsetSinkName, SqlInt32 domainManagerHandle, SqlInt32 recordBindingHandle, SqlBytes recordBytes)
	{
		if (recordBytes.IsNull)
		{
			return;
		}
		Record record = new Record(new BinaryReader(recordBytes.Stream), HeapSegmentAllocator<char>.Instance);
		if (this.m_recordUpdate == null)
		{
			object @object = SqlClr.ObjectManager.GetObject(rowsetConsumerHandle);
			if (@object is IRecordUpdate)
			{
				this.m_recordUpdate = @object as IRecordUpdate;
			}
			else
			{
				if (!(@object is IRowsetConsumer))
				{
					throw new InvalidOperationException(string.Format("The specified object of type '{0}' does not implement IRecordUpdate.", SqlClr.ObjectManager.GetObject(rowsetConsumerHandle).GetType().ToString()));
				}
				IRowsetSink rowsetSink = (@object as IRowsetConsumer).FindRowsetSink(rowsetSinkName);
				this.m_recordUpdate = rowsetSink as IRecordUpdate;
				if (this.m_recordUpdate == null)
				{
					throw new Exception(string.Format("The rowset sink of type '{0}' does not implement IRecordUpdate.", rowsetSink.GetType().ToString()));
				}
			}
			RecordBinding recordBinding = null;
			DataTable dataTable = null;
			if (!recordBindingHandle.IsNull)
			{
				recordBinding = (RecordBinding)SqlClr.ObjectManager.GetObject(recordBindingHandle.Value);
				dataTable = recordBinding.Schema;
			}
			this.m_updateContext = this.m_recordUpdate.BeginUpdate(dataTable);
			if (this.m_updateContext is IRecordUpdateContextInitialize)
			{
				if (domainManagerHandle.IsNull)
				{
					throw new ArgumentException("DomainManagerHandle and RecordBindingHandle may not be null as the rowset consumer implements IRecordUpdateContextInitialize.");
				}
				DomainManager domainManager = (DomainManager)SqlClr.ObjectManager.GetObject(domainManagerHandle.Value);
				ITokenIdProvider tokenIdProvider = domainManager.TokenIdProvider;
				(this.m_updateContext as IRecordUpdateContextInitialize).Initialize(null, domainManager, tokenIdProvider, recordBinding);
			}
		}
		this.m_recordUpdate.AddRecord(this.m_updateContext, record);
		this.m_recordCount++;
	}

	// Token: 0x06000003 RID: 3 RVA: 0x000021E4 File Offset: 0x000003E4
	public void Merge(AddRecord Group)
	{
		this.m_recordCount += Group.m_recordCount;
	}

	// Token: 0x06000004 RID: 4 RVA: 0x000021F9 File Offset: 0x000003F9
	public int Terminate()
	{
		if (this.m_recordUpdate != null)
		{
			this.m_recordUpdate.EndUpdate(this.m_updateContext);
		}
		this.m_updateContext = null;
		this.m_recordUpdate = null;
		return this.m_recordCount;
	}

	// Token: 0x06000005 RID: 5 RVA: 0x00002228 File Offset: 0x00000428
	public void Read(BinaryReader r)
	{
		this.m_recordCount = r.ReadInt32();
	}

	// Token: 0x06000006 RID: 6 RVA: 0x00002236 File Offset: 0x00000436
	public void Write(BinaryWriter w)
	{
		if (this.m_recordUpdate != null)
		{
			this.m_recordUpdate.EndUpdate(this.m_updateContext);
		}
		this.m_updateContext = null;
		this.m_recordUpdate = null;
		w.Write(this.m_recordCount);
	}

	// Token: 0x04000001 RID: 1
	private int m_recordCount;

	// Token: 0x04000002 RID: 2
	[NonSerialized]
	private IUpdateContext m_updateContext;

	// Token: 0x04000003 RID: 3
	[NonSerialized]
	private IRecordUpdate m_recordUpdate;
}
