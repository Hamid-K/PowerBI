using System;
using System.Data;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x020000C1 RID: 193
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
	public class RowsetPageReader : RowsetPageReaderBase<IRowset>
	{
		// Token: 0x06000350 RID: 848 RVA: 0x00009997 File Offset: 0x00007B97
		public RowsetPageReader(IRowset rowset)
			: base(rowset)
		{
			this.first = true;
		}

		// Token: 0x06000351 RID: 849 RVA: 0x000099A7 File Offset: 0x00007BA7
		public override IPage CreatePage()
		{
			return new RowsetPageReader.RowsetPage(base.SchemaTable, base.Bindings);
		}

		// Token: 0x06000352 RID: 850 RVA: 0x000099BA File Offset: 0x00007BBA
		public override void Read(IPage page)
		{
			this.Read((RowsetPageReader.RowsetPage)page);
		}

		// Token: 0x06000353 RID: 851 RVA: 0x000099C8 File Offset: 0x00007BC8
		private void Read(RowsetPageReader.RowsetPage page)
		{
			page.Read(base.Source, base.Accessor, this.first);
			this.first = false;
			base.ReaderWriterProgress.OnRows(page.RowCount, page.ExceptionRows.Count);
		}

		// Token: 0x04000378 RID: 888
		private bool first;

		// Token: 0x020000F1 RID: 241
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal class RowsetPage : RowsetPageReaderBase<IRowset>.RowsetPageBase
		{
			// Token: 0x060004C4 RID: 1220 RVA: 0x0000E5CF File Offset: 0x0000C7CF
			internal RowsetPage(DataTable schemaTable, BindingsInfo bindings)
				: base(schemaTable, bindings)
			{
				this.hrows = new HROW[base.MaxRowCount];
			}

			// Token: 0x060004C5 RID: 1221 RVA: 0x0000E5EC File Offset: 0x0000C7EC
			public unsafe void Read(IRowset rowset, HACCESSOR accessor, bool first)
			{
				base.Clear();
				int num = (first ? Math.Min(100, this.hrows.Length) : this.hrows.Length);
				HROW[] array;
				HROW* ptr;
				if ((array = this.hrows) == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				byte[] array2;
				byte* ptr2;
				if ((array2 = base.Buffer) == null || array2.Length == 0)
				{
					ptr2 = null;
				}
				else
				{
					ptr2 = &array2[0];
				}
				byte* ptr3 = ptr2;
				int num2 = 0;
				while (num > 0 && num2 != 265926)
				{
					HROW* ptr4 = ptr;
					DBCOUNTITEM dbcountitem;
					dbcountitem.Value = 0UL;
					num2 = rowset.GetNextRows(default(HCHAPTER), default(DBROWOFFSET), new DBROWCOUNT
					{
						Value = (long)num
					}, out dbcountitem, &ptr4);
					Marshal.ThrowExceptionForHR(num2);
					num -= (int)dbcountitem.Value;
					try
					{
						for (int i = 0; i < (int)dbcountitem.Value; i++)
						{
							rowset.GetData(this.hrows[i], accessor, ptr3);
							ptr3 += base.Bindings.RowLength;
							int rowCount = base.RowCount;
							base.RowCount = rowCount + 1;
						}
					}
					finally
					{
						rowset.ReleaseRows(dbcountitem, ptr4, null, null, null);
					}
				}
				array2 = null;
				array = null;
			}

			// Token: 0x0400040F RID: 1039
			private readonly HROW[] hrows;
		}
	}
}
