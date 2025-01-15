using System;
using System.Data;
using System.Runtime.CompilerServices;
using Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x0200000F RID: 15
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
	public class CellsRowsetPageReader : RowsetPageReaderBase<IMDDataset>
	{
		// Token: 0x0600003F RID: 63 RVA: 0x00002993 File Offset: 0x00000B93
		public CellsRowsetPageReader(IMDDataset dataset, uint cellsCount)
			: base(dataset)
		{
			this.cellsCount = cellsCount;
			this.offset = 0U;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000029AA File Offset: 0x00000BAA
		public override IPage CreatePage()
		{
			return new CellsRowsetPageReader.CellsPage(base.SchemaTable, base.Bindings);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000029BD File Offset: 0x00000BBD
		internal IPage CreatePage(int maxRowCount)
		{
			return new CellsRowsetPageReader.CellsPage(base.SchemaTable, base.Bindings, maxRowCount);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000029D1 File Offset: 0x00000BD1
		public override void Read(IPage page)
		{
			this.Read((CellsRowsetPageReader.CellsPage)page);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000029E0 File Offset: 0x00000BE0
		private void Read(CellsRowsetPageReader.CellsPage page)
		{
			if (this.offset >= this.cellsCount)
			{
				page.Clear();
				return;
			}
			uint num = this.cellsCount - this.offset;
			page.Read(base.Source, base.Accessor, this.offset, num);
			this.offset += (uint)page.RowCount;
			base.ReaderWriterProgress.OnRows(page.RowCount, 0);
		}

		// Token: 0x04000029 RID: 41
		private readonly uint cellsCount;

		// Token: 0x0400002A RID: 42
		private uint offset;

		// Token: 0x020000E7 RID: 231
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal class CellsPage : RowsetPageReaderBase<IMDDataset>.RowsetPageBase
		{
			// Token: 0x060004A8 RID: 1192 RVA: 0x0000E2EA File Offset: 0x0000C4EA
			internal CellsPage(DataTable schemaTable, BindingsInfo bindings)
				: base(schemaTable, bindings)
			{
			}

			// Token: 0x060004A9 RID: 1193 RVA: 0x0000E2F4 File Offset: 0x0000C4F4
			internal CellsPage(DataTable schemaTable, BindingsInfo bindings, int maxRowCount)
				: base(schemaTable, bindings, maxRowCount)
			{
			}

			// Token: 0x060004AA RID: 1194 RVA: 0x0000E300 File Offset: 0x0000C500
			public void Read(IMDDataset dataset, HACCESSOR accessor, uint offset, uint count)
			{
				base.Clear();
				uint num = Math.Min(count, (uint)base.MaxRowCount);
				dataset.GetCellData(accessor, offset, offset + num - 1U, base.Buffer);
				base.RowCount = (int)num;
			}
		}
	}
}
