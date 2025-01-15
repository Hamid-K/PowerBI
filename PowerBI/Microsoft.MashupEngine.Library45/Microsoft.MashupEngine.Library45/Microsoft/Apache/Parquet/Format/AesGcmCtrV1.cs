using System;
using Microsoft.Apache.Thrift;

namespace Microsoft.Apache.Parquet.Format
{
	// Token: 0x02001FF7 RID: 8183
	internal class AesGcmCtrV1
	{
		// Token: 0x17002D20 RID: 11552
		// (get) Token: 0x06011141 RID: 69953 RVA: 0x003AE1AF File Offset: 0x003AC3AF
		// (set) Token: 0x06011142 RID: 69954 RVA: 0x003AE1B7 File Offset: 0x003AC3B7
		public byte[] AadPrefix { get; set; }

		// Token: 0x17002D21 RID: 11553
		// (get) Token: 0x06011143 RID: 69955 RVA: 0x003AE1C0 File Offset: 0x003AC3C0
		// (set) Token: 0x06011144 RID: 69956 RVA: 0x003AE1C8 File Offset: 0x003AC3C8
		public byte[] AadFileUnique { get; set; }

		// Token: 0x17002D22 RID: 11554
		// (get) Token: 0x06011145 RID: 69957 RVA: 0x003AE1D1 File Offset: 0x003AC3D1
		// (set) Token: 0x06011146 RID: 69958 RVA: 0x003AE1D9 File Offset: 0x003AC3D9
		public bool? SupplyAadPrefix { get; set; }

		// Token: 0x06011147 RID: 69959 RVA: 0x003AE1E4 File Offset: 0x003AC3E4
		public void Read(FastProtocol prot)
		{
			this.AadPrefix = null;
			this.AadFileUnique = null;
			this.SupplyAadPrefix = null;
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
							this.AadPrefix = prot.ReadBytes();
							flag = false;
						}
						break;
					case 2:
						if (tfield.Type == TType.String)
						{
							this.AadFileUnique = prot.ReadBytes();
							flag = false;
						}
						break;
					case 3:
						if (tfield.Type == TType.Bool)
						{
							this.SupplyAadPrefix = new bool?(prot.ReadBool());
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
