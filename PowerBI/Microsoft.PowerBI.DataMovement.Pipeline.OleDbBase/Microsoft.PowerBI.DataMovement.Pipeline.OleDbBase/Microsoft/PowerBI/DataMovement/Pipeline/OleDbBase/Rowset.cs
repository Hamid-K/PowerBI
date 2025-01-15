using System;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x020000BE RID: 190
	public abstract class Rowset : IRowset, IColumnsInfo, IAccessor, IRowsetInfo, IDBAsynchStatus, ISupportErrorInfo
	{
		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000332 RID: 818
		[global::System.Runtime.CompilerServices.Nullable(1)]
		public abstract IRowset _Rowset
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get;
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000333 RID: 819
		[global::System.Runtime.CompilerServices.Nullable(1)]
		public abstract IColumnsInfo ColumnsInfo
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get;
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000334 RID: 820
		[global::System.Runtime.CompilerServices.Nullable(1)]
		public abstract IAccessor Accessor
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get;
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000335 RID: 821
		[global::System.Runtime.CompilerServices.Nullable(1)]
		public abstract IRowsetInfo RowsetInfo
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get;
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000336 RID: 822
		[global::System.Runtime.CompilerServices.Nullable(1)]
		public abstract IDBAsynchStatus DbAsyncStatus
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get;
		}

		// Token: 0x06000337 RID: 823 RVA: 0x000097F2 File Offset: 0x000079F2
		unsafe void IRowset.AddRefRows(DBCOUNTITEM rowCount, HROW* nativeRows, uint* nativeRefCounts, DBROWSTATUS* nativeRowStatus)
		{
			this._Rowset.AddRefRows(rowCount, nativeRows, nativeRefCounts, nativeRowStatus);
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00009804 File Offset: 0x00007A04
		unsafe void IRowset.GetData(HROW row, HACCESSOR accessor, byte* data)
		{
			this._Rowset.GetData(row, accessor, data);
		}

		// Token: 0x06000339 RID: 825 RVA: 0x00009814 File Offset: 0x00007A14
		unsafe int IRowset.GetNextRows(HCHAPTER reserved, DBROWOFFSET rowsOffset, DBROWCOUNT rowCount, out DBCOUNTITEM countRowsObtained, HROW** rows)
		{
			return this._Rowset.GetNextRows(reserved, rowsOffset, rowCount, out countRowsObtained, rows);
		}

		// Token: 0x0600033A RID: 826 RVA: 0x00009828 File Offset: 0x00007A28
		unsafe void IRowset.ReleaseRows(DBCOUNTITEM rowCount, HROW* nativeRows, void* nativeRowOptions, uint* nativeRefCounts, DBROWSTATUS* nativeRowStatus)
		{
			this._Rowset.ReleaseRows(rowCount, nativeRows, nativeRowOptions, nativeRefCounts, nativeRowStatus);
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000983C File Offset: 0x00007A3C
		void IRowset.RestartPosition(HCHAPTER reserved)
		{
			this._Rowset.RestartPosition(reserved);
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000984A File Offset: 0x00007A4A
		unsafe void IColumnsInfo.GetColumnInfo(out DBORDINAL countColumnInfos, out DBCOLUMNINFO* nativeColumnInfos, out char* nativeStrings)
		{
			this.ColumnsInfo.GetColumnInfo(out countColumnInfos, out nativeColumnInfos, out nativeStrings);
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000985A File Offset: 0x00007A5A
		unsafe void IColumnsInfo.MapColumnIDs(DBORDINAL columnIDCount, DBID* columnIDs, DBORDINAL* columns)
		{
			this.ColumnsInfo.MapColumnIDs(columnIDCount, columnIDs, columns);
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000986A File Offset: 0x00007A6A
		unsafe void IAccessor.AddRefAccessor(HACCESSOR accessor, uint* refCount)
		{
			this.Accessor.AddRefAccessor(accessor, refCount);
		}

		// Token: 0x0600033F RID: 831 RVA: 0x00009879 File Offset: 0x00007A79
		unsafe void IAccessor.CreateAccessor(DBACCESSORFLAGS accessorFlags, DBCOUNTITEM bindingCount, DBBINDING* bindings, DBLENGTH rowSize, out HACCESSOR accessor, DBBINDSTATUS* status)
		{
			this.Accessor.CreateAccessor(accessorFlags, bindingCount, bindings, rowSize, out accessor, status);
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0000988F File Offset: 0x00007A8F
		unsafe void IAccessor.GetBindings(HACCESSOR accessor, out DBACCESSORFLAGS accessorFlags, out DBCOUNTITEM bindingCount, out DBBINDING* bindings)
		{
			this.Accessor.GetBindings(accessor, out accessorFlags, out bindingCount, out bindings);
		}

		// Token: 0x06000341 RID: 833 RVA: 0x000098A1 File Offset: 0x00007AA1
		unsafe void IAccessor.ReleaseAccessor(HACCESSOR accessor, uint* refCount)
		{
			this.Accessor.ReleaseAccessor(accessor, refCount);
		}

		// Token: 0x06000342 RID: 834 RVA: 0x000098B0 File Offset: 0x00007AB0
		unsafe int IRowsetInfo.GetProperties(uint countPropertyIDSets, DBPROPIDSET* nativePropertyIDSets, out uint countPropertySets, out DBPROPSET* nativePropertySets)
		{
			return this.RowsetInfo.GetProperties(countPropertyIDSets, nativePropertyIDSets, out countPropertySets, out nativePropertySets);
		}

		// Token: 0x06000343 RID: 835 RVA: 0x000098C2 File Offset: 0x00007AC2
		void IRowsetInfo.GetReferencedRowset(DBORDINAL ordinal, ref Guid iid, out IntPtr referencedRowset)
		{
			this.RowsetInfo.GetReferencedRowset(ordinal, ref iid, out referencedRowset);
		}

		// Token: 0x06000344 RID: 836 RVA: 0x000098D2 File Offset: 0x00007AD2
		void IRowsetInfo.GetSpecification(ref Guid iid, out IntPtr specification)
		{
			this.RowsetInfo.GetSpecification(ref iid, out specification);
		}

		// Token: 0x06000345 RID: 837 RVA: 0x000098E1 File Offset: 0x00007AE1
		public void Abort(HCHAPTER chapter, DBASYNCHOP operation)
		{
			this.DbAsyncStatus.Abort(chapter, operation);
		}

		// Token: 0x06000346 RID: 838 RVA: 0x000098F0 File Offset: 0x00007AF0
		public unsafe void GetStatus(HCHAPTER chapter, DBASYNCHOP operation, DBCOUNTITEM* progress, DBCOUNTITEM* progressMax, out DBASYNCHPHASE asynchPhase, char** statusText)
		{
			this.DbAsyncStatus.GetStatus(chapter, operation, progress, progressMax, out asynchPhase, statusText);
		}

		// Token: 0x06000347 RID: 839 RVA: 0x00009906 File Offset: 0x00007B06
		int ISupportErrorInfo.InterfaceSupportsErrorInfo(ref Guid iid)
		{
			if (iid == IID.IColumnsInfo)
			{
				return 0;
			}
			return 1;
		}
	}
}
