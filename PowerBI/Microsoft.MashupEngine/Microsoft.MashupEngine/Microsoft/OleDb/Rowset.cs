using System;

namespace Microsoft.OleDb
{
	// Token: 0x02001F23 RID: 7971
	public abstract class Rowset : IRowset, IColumnsInfo, IAccessor, IRowsetInfo, IDBAsynchStatus, ISupportErrorInfo, IEvaluationResultSource
	{
		// Token: 0x17002FAE RID: 12206
		// (get) Token: 0x0600C33E RID: 49982
		public abstract IRowset _Rowset { get; }

		// Token: 0x17002FAF RID: 12207
		// (get) Token: 0x0600C33F RID: 49983
		public abstract IColumnsInfo ColumnsInfo { get; }

		// Token: 0x17002FB0 RID: 12208
		// (get) Token: 0x0600C340 RID: 49984
		public abstract IAccessor Accessor { get; }

		// Token: 0x17002FB1 RID: 12209
		// (get) Token: 0x0600C341 RID: 49985
		public abstract IRowsetInfo RowsetInfo { get; }

		// Token: 0x17002FB2 RID: 12210
		// (get) Token: 0x0600C342 RID: 49986
		public abstract IDBAsynchStatus DbAsyncStatus { get; }

		// Token: 0x17002FB3 RID: 12211
		// (get) Token: 0x0600C343 RID: 49987 RVA: 0x000020FA File Offset: 0x000002FA
		public virtual IEvaluationResultSource EvaluationResultSource
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600C344 RID: 49988 RVA: 0x00271D62 File Offset: 0x0026FF62
		unsafe void IRowset.AddRefRows(DBCOUNTITEM cRows, HROW* nativeRows, uint* nativeRefCounts, DBROWSTATUS* nativeRowStatus)
		{
			this._Rowset.AddRefRows(cRows, nativeRows, nativeRefCounts, nativeRowStatus);
		}

		// Token: 0x0600C345 RID: 49989 RVA: 0x00271D74 File Offset: 0x0026FF74
		unsafe void IRowset.GetData(HROW hRow, HACCESSOR hAccessor, byte* pData)
		{
			this._Rowset.GetData(hRow, hAccessor, pData);
		}

		// Token: 0x0600C346 RID: 49990 RVA: 0x00271D84 File Offset: 0x0026FF84
		unsafe int IRowset.GetNextRows(HCHAPTER hReserved, DBROWOFFSET lRowsOffset, DBROWCOUNT cRows, out DBCOUNTITEM countRowsObtained, HROW** pRows)
		{
			return this._Rowset.GetNextRows(hReserved, lRowsOffset, cRows, out countRowsObtained, pRows);
		}

		// Token: 0x0600C347 RID: 49991 RVA: 0x00271D98 File Offset: 0x0026FF98
		unsafe void IRowset.ReleaseRows(DBCOUNTITEM cRows, HROW* nativeRows, void* nativeRowOptions, uint* nativeRefCounts, DBROWSTATUS* nativeRowStatus)
		{
			this._Rowset.ReleaseRows(cRows, nativeRows, nativeRowOptions, nativeRefCounts, nativeRowStatus);
		}

		// Token: 0x0600C348 RID: 49992 RVA: 0x00271DAC File Offset: 0x0026FFAC
		void IRowset.RestartPosition(HCHAPTER hReserved)
		{
			this._Rowset.RestartPosition(hReserved);
		}

		// Token: 0x0600C349 RID: 49993 RVA: 0x00271DBA File Offset: 0x0026FFBA
		unsafe void IColumnsInfo.GetColumnInfo(out DBORDINAL countColumnInfos, out DBCOLUMNINFO* nativeColumnInfos, out char* nativeStrings)
		{
			this.ColumnsInfo.GetColumnInfo(out countColumnInfos, out nativeColumnInfos, out nativeStrings);
		}

		// Token: 0x0600C34A RID: 49994 RVA: 0x00271DCA File Offset: 0x0026FFCA
		unsafe void IColumnsInfo.MapColumnIDs(DBORDINAL cColumnIDs, DBID* rgColumnIDs, DBORDINAL* rgColumns)
		{
			this.ColumnsInfo.MapColumnIDs(cColumnIDs, rgColumnIDs, rgColumns);
		}

		// Token: 0x0600C34B RID: 49995 RVA: 0x00271DDA File Offset: 0x0026FFDA
		unsafe void IAccessor.AddRefAccessor(HACCESSOR hAccessor, uint* pcRefCount)
		{
			this.Accessor.AddRefAccessor(hAccessor, pcRefCount);
		}

		// Token: 0x0600C34C RID: 49996 RVA: 0x00271DE9 File Offset: 0x0026FFE9
		unsafe void IAccessor.CreateAccessor(DBACCESSORFLAGS dwAccessorFlags, DBCOUNTITEM cBindings, DBBINDING* rgBindings, DBLENGTH cbRowSize, out HACCESSOR hAccessor, DBBINDSTATUS* rgStatus)
		{
			this.Accessor.CreateAccessor(dwAccessorFlags, cBindings, rgBindings, cbRowSize, out hAccessor, rgStatus);
		}

		// Token: 0x0600C34D RID: 49997 RVA: 0x00271DFF File Offset: 0x0026FFFF
		unsafe void IAccessor.GetBindings(HACCESSOR hAccessor, out DBACCESSORFLAGS dwAccessorFlags, out DBCOUNTITEM pcBindings, out DBBINDING* rgBindings)
		{
			this.Accessor.GetBindings(hAccessor, out dwAccessorFlags, out pcBindings, out rgBindings);
		}

		// Token: 0x0600C34E RID: 49998 RVA: 0x00271E11 File Offset: 0x00270011
		unsafe void IAccessor.ReleaseAccessor(HACCESSOR hAccessor, uint* pcRefCount)
		{
			this.Accessor.ReleaseAccessor(hAccessor, pcRefCount);
		}

		// Token: 0x0600C34F RID: 49999 RVA: 0x00271E20 File Offset: 0x00270020
		unsafe int IRowsetInfo.GetProperties(uint countPropertyIDSets, DBPROPIDSET* nativePropertyIDSets, out uint countPropertySets, out DBPROPSET* nativePropertySets)
		{
			return this.RowsetInfo.GetProperties(countPropertyIDSets, nativePropertyIDSets, out countPropertySets, out nativePropertySets);
		}

		// Token: 0x0600C350 RID: 50000 RVA: 0x00271E32 File Offset: 0x00270032
		void IRowsetInfo.GetReferencedRowset(DBORDINAL iOrdinal, ref Guid iid, out IntPtr referencedRowset)
		{
			this.RowsetInfo.GetReferencedRowset(iOrdinal, ref iid, out referencedRowset);
		}

		// Token: 0x0600C351 RID: 50001 RVA: 0x00271E42 File Offset: 0x00270042
		void IRowsetInfo.GetSpecification(ref Guid iid, out IntPtr specification)
		{
			this.RowsetInfo.GetSpecification(ref iid, out specification);
		}

		// Token: 0x0600C352 RID: 50002 RVA: 0x00271E51 File Offset: 0x00270051
		public void Abort(HCHAPTER hChapter, DBASYNCHOP eOperation)
		{
			this.DbAsyncStatus.Abort(hChapter, eOperation);
		}

		// Token: 0x0600C353 RID: 50003 RVA: 0x00271E60 File Offset: 0x00270060
		public unsafe void GetStatus(HCHAPTER hChapter, DBASYNCHOP eOperation, DBCOUNTITEM* pulProgress, DBCOUNTITEM* pulProgressMax, out DBASYNCHPHASE peAsynchPhase, char** ppwszStatusText)
		{
			this.DbAsyncStatus.GetStatus(hChapter, eOperation, pulProgress, pulProgressMax, out peAsynchPhase, ppwszStatusText);
		}

		// Token: 0x0600C354 RID: 50004 RVA: 0x00271E76 File Offset: 0x00270076
		public virtual int InterfaceSupportsErrorInfo(ref Guid iid)
		{
			if (iid == IID.IColumnsInfo)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x0600C355 RID: 50005 RVA: 0x00271E8D File Offset: 0x0027008D
		void IEvaluationResultSource.WaitForResults()
		{
			IEvaluationResultSource evaluationResultSource = this.EvaluationResultSource;
			if (evaluationResultSource == null)
			{
				return;
			}
			evaluationResultSource.WaitForResults();
		}
	}
}
