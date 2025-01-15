using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000033 RID: 51
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public class ColumnsInfo : IColumnsInfo
	{
		// Token: 0x060001BB RID: 443 RVA: 0x000055A5 File Offset: 0x000037A5
		public ColumnsInfo(ColumnInfo[] columnInfos)
		{
			this.columnInfos = columnInfos;
		}

		// Token: 0x060001BC RID: 444 RVA: 0x000055B4 File Offset: 0x000037B4
		public ColumnsInfo(IColumnsInfo columnsInfo)
			: this(ColumnsInfo.GetColumnInfos(columnsInfo))
		{
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001BD RID: 445 RVA: 0x000055C2 File Offset: 0x000037C2
		public ColumnInfo[] ColumnInfos
		{
			get
			{
				return this.columnInfos;
			}
		}

		// Token: 0x060001BE RID: 446 RVA: 0x000055CC File Offset: 0x000037CC
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		unsafe void IColumnsInfo.GetColumnInfo(out DBORDINAL countColumnInfos, out DBCOLUMNINFO* nativeColumnInfos, out char* nativeStrings1)
		{
			countColumnInfos = default(DBORDINAL);
			nativeColumnInfos = (IntPtr)((UIntPtr)0);
			nativeStrings1 = (IntPtr)((UIntPtr)0);
			if (this.columnInfos.Length != 0)
			{
				using (ComHeap comHeap = new ComHeap())
				{
					StringBuilder stringBuilder = new StringBuilder();
					foreach (ColumnInfo columnInfo in this.columnInfos)
					{
						stringBuilder.Append(columnInfo.ColumnID.Name);
						stringBuilder.Append('\0');
					}
					char* ptr = comHeap.AllocString(stringBuilder.ToString());
					int num = 0;
					DBCOLUMNINFO* ptr2 = (DBCOLUMNINFO*)comHeap.AllocArray(this.columnInfos.Length, sizeof(DBCOLUMNINFO));
					for (int j = 0; j < this.columnInfos.Length; j++)
					{
						DBCOLUMNINFO* ptr3 = ptr2 + j;
						ColumnInfo columnInfo2 = this.columnInfos[j];
						ColumnID columnID = columnInfo2.ColumnID;
						ptr3->Name = ptr + num;
						num += columnID.Name.Length + 1;
						ptr3->TypeInfo = null;
						ptr3->Ordinal = columnInfo2.Ordinal;
						ptr3->Flags = columnInfo2.Flags;
						ptr3->ColumnSize = columnInfo2.ColumnSize;
						ptr3->Type = columnInfo2.Type;
						ptr3->Precision = columnInfo2.Precision;
						ptr3->Scale = columnInfo2.Scale;
						ptr3->ColumnId.Kind = ColumnsInfo.GetKind(columnID);
						if (columnID.HasName)
						{
							ptr3->ColumnId.Name = ptr3->Name;
						}
						if (columnID.HasGuid)
						{
							ptr3->ColumnId.Guid = columnID.Guid;
						}
						if (columnID.HasPropertyID)
						{
							ptr3->ColumnId.PropId = (uint)columnID.PropertyID;
						}
					}
					comHeap.Commit();
					countColumnInfos = new DBORDINAL
					{
						Value = (ulong)this.columnInfos.Length
					};
					nativeColumnInfos = ptr2;
					nativeStrings1 = ptr;
				}
			}
		}

		// Token: 0x060001BF RID: 447 RVA: 0x000057DC File Offset: 0x000039DC
		private static DBKIND GetKind(ColumnID columnID)
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

		// Token: 0x060001C0 RID: 448 RVA: 0x0000581A File Offset: 0x00003A1A
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		unsafe void IColumnsInfo.MapColumnIDs(DBORDINAL columnIDCount, DBID* columnIDs, DBORDINAL* columns)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00005824 File Offset: 0x00003A24
		private unsafe static ColumnInfo[] GetColumnInfos(IColumnsInfo columnsInfo)
		{
			DBORDINAL dbordinal;
			DBCOLUMNINFO* ptr;
			char* ptr2;
			columnsInfo.GetColumnInfo(out dbordinal, out ptr, out ptr2);
			ColumnInfo[] array2;
			try
			{
				ColumnInfo[] array = new ColumnInfo[dbordinal.Value];
				for (int i = 0; i < array.Length; i++)
				{
					DBCOLUMNINFO* ptr3 = ptr + i;
					ColumnInfo columnInfo = new ColumnInfo(ColumnsInfo.GetColumnID(ptr3->ColumnId, ptr3->Name), ptr3->Ordinal, ptr3->Flags, ptr3->ColumnSize, ptr3->Type, ptr3->Precision, ptr3->Scale);
					array[i] = columnInfo;
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

		// Token: 0x060001C2 RID: 450 RVA: 0x000058E4 File Offset: 0x00003AE4
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		private unsafe static ColumnID GetColumnID(DBID columnID, char* name)
		{
			if (name != null)
			{
				return new ColumnID(new string(name));
			}
			switch (columnID.Kind)
			{
			case DBKIND.GUID_NAME:
				return new ColumnID(columnID.Guid, new string(columnID.Name));
			case DBKIND.GUID_PROPID:
				return new ColumnID(columnID.Guid, (DBPROPID)columnID.PropId);
			case DBKIND.NAME:
				return new ColumnID(new string(columnID.Name));
			case DBKIND.PGUID_NAME:
				return new ColumnID(*columnID.GuidPointer, new string(columnID.Name));
			case DBKIND.PGUID_PROPID:
				return new ColumnID(*columnID.GuidPointer, (DBPROPID)columnID.PropId);
			case DBKIND.PROPID:
				return new ColumnID((DBPROPID)columnID.PropId);
			case DBKIND.GUID:
				return new ColumnID(columnID.Guid);
			default:
				throw new ArgumentException("Invalid columnID");
			}
		}

		// Token: 0x04000057 RID: 87
		private readonly ColumnInfo[] columnInfos;
	}
}
