using System;
using Microsoft.Apache.Thrift;

namespace Microsoft.Apache.Parquet.Format
{
	// Token: 0x02002001 RID: 8193
	internal class ColumnIndex
	{
		// Token: 0x17002D38 RID: 11576
		// (get) Token: 0x06011183 RID: 70019 RVA: 0x003AEBDC File Offset: 0x003ACDDC
		// (set) Token: 0x06011184 RID: 70020 RVA: 0x003AEBE4 File Offset: 0x003ACDE4
		public bool[] NullPages { get; set; }

		// Token: 0x17002D39 RID: 11577
		// (get) Token: 0x06011185 RID: 70021 RVA: 0x003AEBED File Offset: 0x003ACDED
		// (set) Token: 0x06011186 RID: 70022 RVA: 0x003AEBF5 File Offset: 0x003ACDF5
		public byte[][] MinValues { get; set; }

		// Token: 0x17002D3A RID: 11578
		// (get) Token: 0x06011187 RID: 70023 RVA: 0x003AEBFE File Offset: 0x003ACDFE
		// (set) Token: 0x06011188 RID: 70024 RVA: 0x003AEC06 File Offset: 0x003ACE06
		public byte[][] MaxValues { get; set; }

		// Token: 0x17002D3B RID: 11579
		// (get) Token: 0x06011189 RID: 70025 RVA: 0x003AEC0F File Offset: 0x003ACE0F
		// (set) Token: 0x0601118A RID: 70026 RVA: 0x003AEC17 File Offset: 0x003ACE17
		public BoundaryOrder? BoundaryOrder { get; set; }

		// Token: 0x17002D3C RID: 11580
		// (get) Token: 0x0601118B RID: 70027 RVA: 0x003AEC20 File Offset: 0x003ACE20
		// (set) Token: 0x0601118C RID: 70028 RVA: 0x003AEC28 File Offset: 0x003ACE28
		public long[] NullCounts { get; set; }

		// Token: 0x0601118D RID: 70029 RVA: 0x003AEC34 File Offset: 0x003ACE34
		public void Read(FastProtocol prot)
		{
			this.NullPages = null;
			this.MinValues = null;
			this.MaxValues = null;
			this.BoundaryOrder = null;
			this.NullCounts = null;
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
							this.NullPages = new bool[tlist.Count];
							for (int i = 0; i < tlist.Count; i++)
							{
								this.NullPages[i] = prot.ReadBool();
							}
							prot.ReadListEnd();
							flag = false;
						}
						break;
					case 2:
						if (tfield.Type == TType.List)
						{
							TList tlist2 = prot.ReadListBegin();
							this.MinValues = new byte[tlist2.Count][];
							for (int j = 0; j < tlist2.Count; j++)
							{
								this.MinValues[j] = prot.ReadBytes();
							}
							prot.ReadListEnd();
							flag = false;
						}
						break;
					case 3:
						if (tfield.Type == TType.List)
						{
							TList tlist3 = prot.ReadListBegin();
							this.MaxValues = new byte[tlist3.Count][];
							for (int k = 0; k < tlist3.Count; k++)
							{
								this.MaxValues[k] = prot.ReadBytes();
							}
							prot.ReadListEnd();
							flag = false;
						}
						break;
					case 4:
						if (tfield.Type == TType.I32)
						{
							this.BoundaryOrder = new BoundaryOrder?((BoundaryOrder)prot.ReadI32());
							flag = false;
						}
						break;
					case 5:
						if (tfield.Type == TType.List)
						{
							TList tlist4 = prot.ReadListBegin();
							this.NullCounts = new long[tlist4.Count];
							for (int l = 0; l < tlist4.Count; l++)
							{
								this.NullCounts[l] = prot.ReadI64();
							}
							prot.ReadListEnd();
							flag = false;
						}
						break;
					}
					prot.EndField(tfield.Type, flag);
				}
				prot.ReadStructEnd();
				if (this.NullPages == null || this.MinValues == null || this.MaxValues == null || this.BoundaryOrder == null)
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
