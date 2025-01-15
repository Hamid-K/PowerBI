using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase.Serialization
{
	// Token: 0x020000CB RID: 203
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public class PageReaderCells : ICells, IDisposable
	{
		// Token: 0x0600038F RID: 911 RVA: 0x0000A7E0 File Offset: 0x000089E0
		public PageReaderCells(IPageReader reader)
		{
			ColumnInfo[] array = reader.SchemaTable.ToColumnInfos(null);
			this.accessor = new Accessor();
			this.columnsInfo = new ColumnsInfo(array);
			this.ordinalIndices = array.CreateOrdinalIndices();
			this.reader = reader;
			this.page = reader.CreatePage();
			this.currentPageFirstCellIndex = -1;
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000390 RID: 912 RVA: 0x0000A83D File Offset: 0x00008A3D
		public IColumnsInfo ColumnsInfo
		{
			get
			{
				return this.columnsInfo;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000391 RID: 913 RVA: 0x0000A845 File Offset: 0x00008A45
		public IAccessor Accessor
		{
			get
			{
				return this.accessor;
			}
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0000A850 File Offset: 0x00008A50
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public unsafe int GetCells(HACCESSOR accessor, DBORDINAL startCell, DBORDINAL endCell, void* destBuffer)
		{
			int num;
			int num2;
			Binder binder;
			byte* ptr;
			checked
			{
				num = (int)startCell.Value;
				num2 = (int)endCell.Value;
				if (num > num2)
				{
					return -2147217820;
				}
				if (num < this.currentPageFirstCellIndex)
				{
					return -2147217884;
				}
				binder = this.accessor.GetBinder(accessor);
				if (binder.RowSize.Value == 0UL && num != num2)
				{
					return -2147217820;
				}
				ptr = (byte*)destBuffer;
			}
			for (int i = num; i <= num2; i++)
			{
				this.EnsurePage(i);
				int num3 = i - this.currentPageFirstCellIndex;
				RowsetUtils.GetData(this.page, this.ordinalIndices, binder.Bindings, DataConvert.GetInstance(), num3, ptr);
				ptr += binder.RowSize.Value;
			}
			return 0;
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000A900 File Offset: 0x00008B00
		public void Dispose()
		{
			if (this.page != null)
			{
				this.page.Dispose();
				this.page = null;
			}
			if (this.reader != null)
			{
				this.reader.Dispose();
				this.reader = null;
			}
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000A938 File Offset: 0x00008B38
		private bool EnsurePage(int cellIndex)
		{
			if (this.currentPageFirstCellIndex < 0)
			{
				this.reader.Read(this.page);
				this.currentPageFirstCellIndex = 0;
			}
			if (this.page.RowCount == 0)
			{
				throw new COMException("Can't fetch cells", -2147217820);
			}
			int num = this.currentPageFirstCellIndex + this.page.RowCount - 1;
			while (cellIndex > num)
			{
				this.currentPageFirstCellIndex += this.page.RowCount;
				this.reader.Read(this.page);
				if (this.page.RowCount == 0)
				{
					throw new COMException("Can't fetch cells", -2147217820);
				}
				num += this.page.RowCount;
			}
			return true;
		}

		// Token: 0x04000384 RID: 900
		private readonly Accessor accessor;

		// Token: 0x04000385 RID: 901
		private readonly ColumnsInfo columnsInfo;

		// Token: 0x04000386 RID: 902
		private readonly int[] ordinalIndices;

		// Token: 0x04000387 RID: 903
		private IPage page;

		// Token: 0x04000388 RID: 904
		private IPageReader reader;

		// Token: 0x04000389 RID: 905
		private int currentPageFirstCellIndex;
	}
}
