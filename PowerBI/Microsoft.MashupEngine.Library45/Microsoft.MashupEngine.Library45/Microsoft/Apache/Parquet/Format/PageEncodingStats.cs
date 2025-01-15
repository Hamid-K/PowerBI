using System;
using Microsoft.Apache.Thrift;

namespace Microsoft.Apache.Parquet.Format
{
	// Token: 0x0200201F RID: 8223
	internal class PageEncodingStats
	{
		// Token: 0x17002D80 RID: 11648
		// (get) Token: 0x06011247 RID: 70215 RVA: 0x003B0F2C File Offset: 0x003AF12C
		// (set) Token: 0x06011248 RID: 70216 RVA: 0x003B0F34 File Offset: 0x003AF134
		public PageType? PageType { get; set; }

		// Token: 0x17002D81 RID: 11649
		// (get) Token: 0x06011249 RID: 70217 RVA: 0x003B0F3D File Offset: 0x003AF13D
		// (set) Token: 0x0601124A RID: 70218 RVA: 0x003B0F45 File Offset: 0x003AF145
		public Encoding? Encoding { get; set; }

		// Token: 0x17002D82 RID: 11650
		// (get) Token: 0x0601124B RID: 70219 RVA: 0x003B0F4E File Offset: 0x003AF14E
		// (set) Token: 0x0601124C RID: 70220 RVA: 0x003B0F56 File Offset: 0x003AF156
		public int? Count { get; set; }

		// Token: 0x0601124D RID: 70221 RVA: 0x003B0F60 File Offset: 0x003AF160
		public void Read(FastProtocol prot)
		{
			this.PageType = null;
			this.Encoding = null;
			this.Count = null;
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
						if (tfield.Type == TType.I32)
						{
							this.PageType = new PageType?((PageType)prot.ReadI32());
							flag = false;
						}
						break;
					case 2:
						if (tfield.Type == TType.I32)
						{
							this.Encoding = new Encoding?((Encoding)prot.ReadI32());
							flag = false;
						}
						break;
					case 3:
						if (tfield.Type == TType.I32)
						{
							this.Count = new int?(prot.ReadI32());
							flag = false;
						}
						break;
					}
					prot.EndField(tfield.Type, flag);
				}
				prot.ReadStructEnd();
				if (this.PageType == null || this.Encoding == null || this.Count == null)
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
