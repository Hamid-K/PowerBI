using System;
using Microsoft.Apache.Thrift;

namespace Microsoft.Apache.Parquet.Format
{
	// Token: 0x02002023 RID: 8227
	internal class RowGroup
	{
		// Token: 0x17002D8E RID: 11662
		// (get) Token: 0x06011269 RID: 70249 RVA: 0x003B1510 File Offset: 0x003AF710
		// (set) Token: 0x0601126A RID: 70250 RVA: 0x003B1518 File Offset: 0x003AF718
		public ColumnChunk[] Columns { get; set; }

		// Token: 0x17002D8F RID: 11663
		// (get) Token: 0x0601126B RID: 70251 RVA: 0x003B1521 File Offset: 0x003AF721
		// (set) Token: 0x0601126C RID: 70252 RVA: 0x003B1529 File Offset: 0x003AF729
		public long? TotalByteSize { get; set; }

		// Token: 0x17002D90 RID: 11664
		// (get) Token: 0x0601126D RID: 70253 RVA: 0x003B1532 File Offset: 0x003AF732
		// (set) Token: 0x0601126E RID: 70254 RVA: 0x003B153A File Offset: 0x003AF73A
		public long? NumRows { get; set; }

		// Token: 0x17002D91 RID: 11665
		// (get) Token: 0x0601126F RID: 70255 RVA: 0x003B1543 File Offset: 0x003AF743
		// (set) Token: 0x06011270 RID: 70256 RVA: 0x003B154B File Offset: 0x003AF74B
		public SortingColumn[] SortingColumns { get; set; }

		// Token: 0x17002D92 RID: 11666
		// (get) Token: 0x06011271 RID: 70257 RVA: 0x003B1554 File Offset: 0x003AF754
		// (set) Token: 0x06011272 RID: 70258 RVA: 0x003B155C File Offset: 0x003AF75C
		public long? FileOffset { get; set; }

		// Token: 0x17002D93 RID: 11667
		// (get) Token: 0x06011273 RID: 70259 RVA: 0x003B1565 File Offset: 0x003AF765
		// (set) Token: 0x06011274 RID: 70260 RVA: 0x003B156D File Offset: 0x003AF76D
		public long? TotalCompressedSize { get; set; }

		// Token: 0x17002D94 RID: 11668
		// (get) Token: 0x06011275 RID: 70261 RVA: 0x003B1576 File Offset: 0x003AF776
		// (set) Token: 0x06011276 RID: 70262 RVA: 0x003B157E File Offset: 0x003AF77E
		public short? Ordinal { get; set; }

		// Token: 0x06011277 RID: 70263 RVA: 0x003B1588 File Offset: 0x003AF788
		public void Read(FastProtocol prot)
		{
			this.Columns = null;
			this.TotalByteSize = null;
			this.NumRows = null;
			this.SortingColumns = null;
			this.FileOffset = null;
			this.TotalCompressedSize = null;
			this.Ordinal = null;
			prot.IncrementRecursionDepth();
			try
			{
				prot.ReadStructBegin();
				for (;;)
				{
					TField tfield = prot.ReadFieldBegin();
					if (tfield.Type == TType.Stop)
					{
						break;
					}
					bool flag = true;
					switch (tfield.ID)
					{
					case 1:
						if (tfield.Type == TType.List)
						{
							TList tlist = prot.ReadListBegin();
							this.Columns = new ColumnChunk[tlist.Count];
							for (int i = 0; i < tlist.Count; i++)
							{
								ColumnChunk columnChunk = new ColumnChunk();
								columnChunk.Read(prot);
								this.Columns[i] = columnChunk;
							}
							prot.ReadListEnd();
							flag = false;
						}
						break;
					case 2:
						if (tfield.Type == TType.I64)
						{
							this.TotalByteSize = new long?(prot.ReadI64());
							flag = false;
						}
						break;
					case 3:
						if (tfield.Type == TType.I64)
						{
							this.NumRows = new long?(prot.ReadI64());
							flag = false;
						}
						break;
					case 4:
						if (tfield.Type == TType.List)
						{
							TList tlist2 = prot.ReadListBegin();
							this.SortingColumns = new SortingColumn[tlist2.Count];
							for (int j = 0; j < tlist2.Count; j++)
							{
								SortingColumn sortingColumn = new SortingColumn();
								sortingColumn.Read(prot);
								this.SortingColumns[j] = sortingColumn;
							}
							prot.ReadListEnd();
							flag = false;
						}
						break;
					case 5:
						if (tfield.Type == TType.I64)
						{
							this.FileOffset = new long?(prot.ReadI64());
							flag = false;
						}
						break;
					case 6:
						if (tfield.Type == TType.I64)
						{
							this.TotalCompressedSize = new long?(prot.ReadI64());
							flag = false;
						}
						break;
					case 7:
						if (tfield.Type == TType.I16)
						{
							this.Ordinal = new short?(prot.ReadI16());
							flag = false;
						}
						break;
					}
					prot.EndField(tfield.Type, flag);
				}
				prot.ReadStructEnd();
				if (this.Columns == null || this.TotalByteSize == null || this.NumRows == null)
				{
					throw new InvalidOperationException("Required field not found");
				}
			}
			finally
			{
				prot.DecrementRecursionDepth();
			}
		}
	}
}
