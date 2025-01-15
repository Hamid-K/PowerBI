using System;

namespace Microsoft.Mashup.SapBwProvider
{
	// Token: 0x02000025 RID: 37
	internal abstract class MdxDataReader : ParsingDataReader
	{
		// Token: 0x060001E4 RID: 484 RVA: 0x000086F4 File Offset: 0x000068F4
		protected MdxDataReader(SapBwCommand command, MdxCommand mdxCommand, MdxColumnProvider columnProvider, int startRow = 1)
			: base(command, startRow)
		{
			this.mdxCommand = mdxCommand;
			this.columnProvider = columnProvider;
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x0000870D File Offset: 0x0000690D
		protected override ColumnProvider ColumnProvider
		{
			get
			{
				return this.columnProvider;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x00008715 File Offset: 0x00006915
		protected override int BatchSize
		{
			get
			{
				return this.connection.BatchSize;
			}
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00008722 File Offset: 0x00006922
		public override void Close()
		{
			if (this.mdxCommand != null)
			{
				this.mdxCommand.Dispose();
			}
			if (this.rowEnumerator != null)
			{
				this.rowEnumerator.Dispose();
				this.rowEnumerator = null;
			}
			this.exhausted = true;
		}

		// Token: 0x040000B3 RID: 179
		protected readonly MdxColumnProvider columnProvider;

		// Token: 0x040000B4 RID: 180
		protected readonly MdxCommand mdxCommand;
	}
}
