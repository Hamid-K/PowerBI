using System;
using Microsoft.Apache.Thrift;

namespace Microsoft.Apache.Parquet.Format
{
	// Token: 0x02002021 RID: 8225
	internal class PageLocation
	{
		// Token: 0x17002D8B RID: 11659
		// (get) Token: 0x06011261 RID: 70241 RVA: 0x003B1394 File Offset: 0x003AF594
		// (set) Token: 0x06011262 RID: 70242 RVA: 0x003B139C File Offset: 0x003AF59C
		public long? Offset { get; set; }

		// Token: 0x17002D8C RID: 11660
		// (get) Token: 0x06011263 RID: 70243 RVA: 0x003B13A5 File Offset: 0x003AF5A5
		// (set) Token: 0x06011264 RID: 70244 RVA: 0x003B13AD File Offset: 0x003AF5AD
		public int? CompressedPageSize { get; set; }

		// Token: 0x17002D8D RID: 11661
		// (get) Token: 0x06011265 RID: 70245 RVA: 0x003B13B6 File Offset: 0x003AF5B6
		// (set) Token: 0x06011266 RID: 70246 RVA: 0x003B13BE File Offset: 0x003AF5BE
		public long? FirstRowIndex { get; set; }

		// Token: 0x06011267 RID: 70247 RVA: 0x003B13C8 File Offset: 0x003AF5C8
		public void Read(FastProtocol prot)
		{
			this.Offset = null;
			this.CompressedPageSize = null;
			this.FirstRowIndex = null;
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
						if (tfield.Type == TType.I64)
						{
							this.Offset = new long?(prot.ReadI64());
							flag = false;
						}
						break;
					case 2:
						if (tfield.Type == TType.I32)
						{
							this.CompressedPageSize = new int?(prot.ReadI32());
							flag = false;
						}
						break;
					case 3:
						if (tfield.Type == TType.I64)
						{
							this.FirstRowIndex = new long?(prot.ReadI64());
							flag = false;
						}
						break;
					}
					prot.EndField(tfield.Type, flag);
				}
				prot.ReadStructEnd();
				if (this.Offset == null || this.CompressedPageSize == null || this.FirstRowIndex == null)
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
