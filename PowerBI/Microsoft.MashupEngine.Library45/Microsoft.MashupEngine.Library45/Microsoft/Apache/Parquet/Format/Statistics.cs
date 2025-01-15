using System;
using Microsoft.Apache.Thrift;

namespace Microsoft.Apache.Parquet.Format
{
	// Token: 0x02002027 RID: 8231
	internal class Statistics
	{
		// Token: 0x17002DA2 RID: 11682
		// (get) Token: 0x06011299 RID: 70297 RVA: 0x003B1D50 File Offset: 0x003AFF50
		// (set) Token: 0x0601129A RID: 70298 RVA: 0x003B1D58 File Offset: 0x003AFF58
		public byte[] Max { get; set; }

		// Token: 0x17002DA3 RID: 11683
		// (get) Token: 0x0601129B RID: 70299 RVA: 0x003B1D61 File Offset: 0x003AFF61
		// (set) Token: 0x0601129C RID: 70300 RVA: 0x003B1D69 File Offset: 0x003AFF69
		public byte[] Min { get; set; }

		// Token: 0x17002DA4 RID: 11684
		// (get) Token: 0x0601129D RID: 70301 RVA: 0x003B1D72 File Offset: 0x003AFF72
		// (set) Token: 0x0601129E RID: 70302 RVA: 0x003B1D7A File Offset: 0x003AFF7A
		public long? NullCount { get; set; }

		// Token: 0x17002DA5 RID: 11685
		// (get) Token: 0x0601129F RID: 70303 RVA: 0x003B1D83 File Offset: 0x003AFF83
		// (set) Token: 0x060112A0 RID: 70304 RVA: 0x003B1D8B File Offset: 0x003AFF8B
		public long? DistinctCount { get; set; }

		// Token: 0x17002DA6 RID: 11686
		// (get) Token: 0x060112A1 RID: 70305 RVA: 0x003B1D94 File Offset: 0x003AFF94
		// (set) Token: 0x060112A2 RID: 70306 RVA: 0x003B1D9C File Offset: 0x003AFF9C
		public byte[] MaxValue { get; set; }

		// Token: 0x17002DA7 RID: 11687
		// (get) Token: 0x060112A3 RID: 70307 RVA: 0x003B1DA5 File Offset: 0x003AFFA5
		// (set) Token: 0x060112A4 RID: 70308 RVA: 0x003B1DAD File Offset: 0x003AFFAD
		public byte[] MinValue { get; set; }

		// Token: 0x060112A5 RID: 70309 RVA: 0x003B1DB8 File Offset: 0x003AFFB8
		public void Read(FastProtocol prot)
		{
			this.Max = null;
			this.Min = null;
			this.NullCount = null;
			this.DistinctCount = null;
			this.MaxValue = null;
			this.MinValue = null;
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
						if (tfield.Type == TType.String)
						{
							this.Max = prot.ReadBytes();
							flag = false;
						}
						break;
					case 2:
						if (tfield.Type == TType.String)
						{
							this.Min = prot.ReadBytes();
							flag = false;
						}
						break;
					case 3:
						if (tfield.Type == TType.I64)
						{
							this.NullCount = new long?(prot.ReadI64());
							flag = false;
						}
						break;
					case 4:
						if (tfield.Type == TType.I64)
						{
							this.DistinctCount = new long?(prot.ReadI64());
							flag = false;
						}
						break;
					case 5:
						if (tfield.Type == TType.String)
						{
							this.MaxValue = prot.ReadBytes();
							flag = false;
						}
						break;
					case 6:
						if (tfield.Type == TType.String)
						{
							this.MinValue = prot.ReadBytes();
							flag = false;
						}
						break;
					}
					prot.EndField(tfield.Type, flag);
				}
				prot.ReadStructEnd();
			}
			finally
			{
				prot.DecrementRecursionDepth();
			}
		}
	}
}
