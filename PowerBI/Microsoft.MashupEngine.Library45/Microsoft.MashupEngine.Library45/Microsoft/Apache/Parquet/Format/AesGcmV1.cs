using System;
using Microsoft.Apache.Thrift;

namespace Microsoft.Apache.Parquet.Format
{
	// Token: 0x02001FF8 RID: 8184
	internal class AesGcmV1
	{
		// Token: 0x17002D23 RID: 11555
		// (get) Token: 0x06011149 RID: 69961 RVA: 0x003AE2D4 File Offset: 0x003AC4D4
		// (set) Token: 0x0601114A RID: 69962 RVA: 0x003AE2DC File Offset: 0x003AC4DC
		public byte[] AadPrefix { get; set; }

		// Token: 0x17002D24 RID: 11556
		// (get) Token: 0x0601114B RID: 69963 RVA: 0x003AE2E5 File Offset: 0x003AC4E5
		// (set) Token: 0x0601114C RID: 69964 RVA: 0x003AE2ED File Offset: 0x003AC4ED
		public byte[] AadFileUnique { get; set; }

		// Token: 0x17002D25 RID: 11557
		// (get) Token: 0x0601114D RID: 69965 RVA: 0x003AE2F6 File Offset: 0x003AC4F6
		// (set) Token: 0x0601114E RID: 69966 RVA: 0x003AE2FE File Offset: 0x003AC4FE
		public bool? SupplyAadPrefix { get; set; }

		// Token: 0x0601114F RID: 69967 RVA: 0x003AE308 File Offset: 0x003AC508
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
