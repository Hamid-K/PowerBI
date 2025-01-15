using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Microsoft.OleDb
{
	// Token: 0x02001E83 RID: 7811
	public class ColumnsInfo : IColumnsInfo
	{
		// Token: 0x0600C0FF RID: 49407 RVA: 0x0026CF59 File Offset: 0x0026B159
		public ColumnsInfo(ColumnInfo[] columnInfos)
		{
			this.columnInfos = columnInfos;
		}

		// Token: 0x0600C100 RID: 49408 RVA: 0x0026CF68 File Offset: 0x0026B168
		public ColumnsInfo(IColumnsInfo columnsInfo)
			: this(ColumnsInfo.GetColumnInfos(columnsInfo))
		{
		}

		// Token: 0x17002F31 RID: 12081
		// (get) Token: 0x0600C101 RID: 49409 RVA: 0x0026CF76 File Offset: 0x0026B176
		public ColumnInfo[] ColumnInfos
		{
			get
			{
				return this.columnInfos;
			}
		}

		// Token: 0x0600C102 RID: 49410 RVA: 0x0026CF80 File Offset: 0x0026B180
		unsafe void IColumnsInfo.GetColumnInfo(out DBORDINAL countColumnInfos, out DBCOLUMNINFO* _nativeColumnInfos, out char* _nativeStrings)
		{
			countColumnInfos = default(DBORDINAL);
			_nativeColumnInfos = (IntPtr)((UIntPtr)0);
			_nativeStrings = (IntPtr)((UIntPtr)0);
			if (this.columnInfos.Length != 0)
			{
				using (ComHeap comHeap = new ComHeap())
				{
					StringBuilder stringBuilder = new StringBuilder();
					foreach (ColumnInfo columnInfo in this.columnInfos)
					{
						if (columnInfo.ColumnID.HasName)
						{
							stringBuilder.Append(columnInfo.ColumnID.Name);
							stringBuilder.Append('\0');
						}
					}
					char* ptr = comHeap.AllocString(stringBuilder.ToString());
					int num = 0;
					DBCOLUMNINFO* ptr2 = (DBCOLUMNINFO*)comHeap.AllocArray(this.columnInfos.Length, sizeof(DBCOLUMNINFO));
					for (int j = 0; j < this.columnInfos.Length; j++)
					{
						DBCOLUMNINFO* ptr3 = ptr2 + j;
						ColumnInfo columnInfo2 = this.columnInfos[j];
						ColumnID columnID = columnInfo2.ColumnID;
						ptr3->pwszName = ptr + num;
						ptr3->pTypeInfo = null;
						ptr3->iOrdinal = columnInfo2.Ordinal;
						ptr3->dwFlags = columnInfo2.Flags;
						ptr3->ulColumnSize = columnInfo2.ColumnSize;
						ptr3->wType = columnInfo2.Type;
						ptr3->bPrecision = columnInfo2.Precision;
						ptr3->bScale = columnInfo2.Scale;
						ptr3->columnid.eKind = ColumnsInfo.GetKind(columnID);
						if (columnID.HasName)
						{
							ptr3->columnid.pwszName = ptr3->pwszName;
							num += columnID.Name.Length + 1;
						}
						if (columnID.HasGuid)
						{
							ptr3->columnid.guid = columnID.Guid;
						}
						if (columnID.HasPropertyID)
						{
							ptr3->columnid.ulPropid = (uint)columnID.PropertyID;
						}
					}
					comHeap.Commit();
					countColumnInfos = new DBORDINAL
					{
						value = (ulong)this.columnInfos.Length
					};
					_nativeColumnInfos = ptr2;
					_nativeStrings = ptr;
				}
			}
		}

		// Token: 0x0600C103 RID: 49411 RVA: 0x0026D1A0 File Offset: 0x0026B3A0
		internal static DBKIND GetKind(ColumnID columnID)
		{
			if (columnID.HasGuid)
			{
				if (columnID.HasName)
				{
					return DBKIND.GUID_NAME;
				}
				if (columnID.HasPropertyID)
				{
					return DBKIND.GUID_PROPID;
				}
				return DBKIND.GUID;
			}
			else
			{
				if (columnID.HasName)
				{
					return DBKIND.NAME;
				}
				if (columnID.HasPropertyID)
				{
					return DBKIND.PROPID;
				}
				throw new ArgumentException("Invalid columnID");
			}
		}

		// Token: 0x0600C104 RID: 49412 RVA: 0x000091AE File Offset: 0x000073AE
		unsafe void IColumnsInfo.MapColumnIDs(DBORDINAL cColumnIDs, DBID* rgColumnIDs, DBORDINAL* rgColumns)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600C105 RID: 49413 RVA: 0x0026D1DE File Offset: 0x0026B3DE
		private static bool IgnoreColumn(ref DBCOLUMNINFO info)
		{
			return (info.dwFlags & DBCOLUMNFLAGS.ISBOOKMARK) > DBCOLUMNFLAGS.NONE;
		}

		// Token: 0x0600C106 RID: 49414 RVA: 0x0026D1EC File Offset: 0x0026B3EC
		private unsafe static ColumnInfo[] GetColumnInfos(IColumnsInfo columnsInfo)
		{
			DBORDINAL dbordinal;
			DBCOLUMNINFO* ptr;
			char* ptr2;
			columnsInfo.GetColumnInfo(out dbordinal, out ptr, out ptr2);
			ColumnInfo[] array2;
			try
			{
				ColumnInfo[] array = new ColumnInfo[dbordinal.value];
				int num = 0;
				uint num2 = 0U;
				while ((ulong)num2 < (ulong)((long)array.Length))
				{
					DBCOLUMNINFO* ptr3 = ptr + (ulong)num2 * (ulong)((long)sizeof(DBCOLUMNINFO)) / (ulong)sizeof(DBCOLUMNINFO);
					if (!ColumnsInfo.IgnoreColumn(ref *ptr3))
					{
						ColumnInfo columnInfo = new ColumnInfo(ColumnsInfo.GetColumnID(ptr3->columnid), ptr3->iOrdinal, ptr3->dwFlags, ptr3->ulColumnSize, ptr3->wType, ptr3->bPrecision, ptr3->bScale);
						array[num] = columnInfo;
						num++;
					}
					num2 += 1U;
				}
				if (num != array.Length)
				{
					Array.Resize<ColumnInfo>(ref array, num);
				}
				array2 = array;
			}
			finally
			{
				Marshal.FreeCoTaskMem(new IntPtr((void*)ptr));
				Marshal.FreeCoTaskMem(new IntPtr((void*)ptr2));
			}
			return array2;
		}

		// Token: 0x0600C107 RID: 49415 RVA: 0x0026D2CC File Offset: 0x0026B4CC
		private unsafe static ColumnID GetColumnID(DBID columnID)
		{
			switch (columnID.eKind)
			{
			case DBKIND.GUID_NAME:
				return new ColumnID(columnID.guid, new string(columnID.pwszName));
			case DBKIND.GUID_PROPID:
				return new ColumnID(columnID.guid, (DBPROPID)columnID.ulPropid);
			case DBKIND.NAME:
				return new ColumnID(new string(columnID.pwszName));
			case DBKIND.PGUID_NAME:
				return new ColumnID(*columnID.pguid, new string(columnID.pwszName));
			case DBKIND.PGUID_PROPID:
				return new ColumnID(*columnID.pguid, (DBPROPID)columnID.ulPropid);
			case DBKIND.PROPID:
				return new ColumnID((DBPROPID)columnID.ulPropid);
			case DBKIND.GUID:
				return new ColumnID(columnID.guid);
			default:
				throw new ArgumentException("Invalid columnID");
			}
		}

		// Token: 0x04006165 RID: 24933
		private readonly ColumnInfo[] columnInfos;
	}
}
