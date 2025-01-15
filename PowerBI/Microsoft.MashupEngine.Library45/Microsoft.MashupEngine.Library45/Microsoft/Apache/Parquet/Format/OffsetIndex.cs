using System;
using Microsoft.Apache.Thrift;

namespace Microsoft.Apache.Parquet.Format
{
	// Token: 0x0200201E RID: 8222
	internal class OffsetIndex
	{
		// Token: 0x17002D7F RID: 11647
		// (get) Token: 0x06011243 RID: 70211 RVA: 0x003B0E40 File Offset: 0x003AF040
		// (set) Token: 0x06011244 RID: 70212 RVA: 0x003B0E48 File Offset: 0x003AF048
		public PageLocation[] PageLocations { get; set; }

		// Token: 0x06011245 RID: 70213 RVA: 0x003B0E54 File Offset: 0x003AF054
		public void Read(FastProtocol prot)
		{
			this.PageLocations = null;
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
					if (tfield.ID == 1 && tfield.Type == TType.List)
					{
						TList tlist = prot.ReadListBegin();
						this.PageLocations = new PageLocation[tlist.Count];
						for (int i = 0; i < tlist.Count; i++)
						{
							PageLocation pageLocation = new PageLocation();
							pageLocation.Read(prot);
							this.PageLocations[i] = pageLocation;
						}
						prot.ReadListEnd();
						flag = false;
					}
					prot.EndField(tfield.Type, flag);
				}
				prot.ReadStructEnd();
				if (this.PageLocations == null)
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
