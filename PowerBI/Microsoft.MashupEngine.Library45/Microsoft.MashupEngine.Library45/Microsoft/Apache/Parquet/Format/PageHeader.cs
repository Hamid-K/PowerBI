using System;
using Microsoft.Apache.Thrift;

namespace Microsoft.Apache.Parquet.Format
{
	// Token: 0x02002020 RID: 8224
	internal class PageHeader
	{
		// Token: 0x17002D83 RID: 11651
		// (get) Token: 0x0601124F RID: 70223 RVA: 0x003B10A8 File Offset: 0x003AF2A8
		// (set) Token: 0x06011250 RID: 70224 RVA: 0x003B10B0 File Offset: 0x003AF2B0
		public PageType? Type { get; set; }

		// Token: 0x17002D84 RID: 11652
		// (get) Token: 0x06011251 RID: 70225 RVA: 0x003B10B9 File Offset: 0x003AF2B9
		// (set) Token: 0x06011252 RID: 70226 RVA: 0x003B10C1 File Offset: 0x003AF2C1
		public int? UncompressedPageSize { get; set; }

		// Token: 0x17002D85 RID: 11653
		// (get) Token: 0x06011253 RID: 70227 RVA: 0x003B10CA File Offset: 0x003AF2CA
		// (set) Token: 0x06011254 RID: 70228 RVA: 0x003B10D2 File Offset: 0x003AF2D2
		public int? CompressedPageSize { get; set; }

		// Token: 0x17002D86 RID: 11654
		// (get) Token: 0x06011255 RID: 70229 RVA: 0x003B10DB File Offset: 0x003AF2DB
		// (set) Token: 0x06011256 RID: 70230 RVA: 0x003B10E3 File Offset: 0x003AF2E3
		public int? Crc { get; set; }

		// Token: 0x17002D87 RID: 11655
		// (get) Token: 0x06011257 RID: 70231 RVA: 0x003B10EC File Offset: 0x003AF2EC
		// (set) Token: 0x06011258 RID: 70232 RVA: 0x003B10F4 File Offset: 0x003AF2F4
		public DataPageHeader DataPageHeader { get; set; }

		// Token: 0x17002D88 RID: 11656
		// (get) Token: 0x06011259 RID: 70233 RVA: 0x003B10FD File Offset: 0x003AF2FD
		// (set) Token: 0x0601125A RID: 70234 RVA: 0x003B1105 File Offset: 0x003AF305
		public IndexPageHeader IndexPageHeader { get; set; }

		// Token: 0x17002D89 RID: 11657
		// (get) Token: 0x0601125B RID: 70235 RVA: 0x003B110E File Offset: 0x003AF30E
		// (set) Token: 0x0601125C RID: 70236 RVA: 0x003B1116 File Offset: 0x003AF316
		public DictionaryPageHeader DictionaryPageHeader { get; set; }

		// Token: 0x17002D8A RID: 11658
		// (get) Token: 0x0601125D RID: 70237 RVA: 0x003B111F File Offset: 0x003AF31F
		// (set) Token: 0x0601125E RID: 70238 RVA: 0x003B1127 File Offset: 0x003AF327
		public DataPageHeaderV2 DataPageHeaderV2 { get; set; }

		// Token: 0x0601125F RID: 70239 RVA: 0x003B1130 File Offset: 0x003AF330
		public void Read(FastProtocol prot)
		{
			this.Type = null;
			this.UncompressedPageSize = null;
			this.CompressedPageSize = null;
			this.Crc = null;
			this.DataPageHeader = null;
			this.IndexPageHeader = null;
			this.DictionaryPageHeader = null;
			this.DataPageHeaderV2 = null;
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
							this.Type = new PageType?((PageType)prot.ReadI32());
							flag = false;
						}
						break;
					case 2:
						if (tfield.Type == TType.I32)
						{
							this.UncompressedPageSize = new int?(prot.ReadI32());
							flag = false;
						}
						break;
					case 3:
						if (tfield.Type == TType.I32)
						{
							this.CompressedPageSize = new int?(prot.ReadI32());
							flag = false;
						}
						break;
					case 4:
						if (tfield.Type == TType.I32)
						{
							this.Crc = new int?(prot.ReadI32());
							flag = false;
						}
						break;
					case 5:
						if (tfield.Type == TType.Struct)
						{
							DataPageHeader dataPageHeader = new DataPageHeader();
							dataPageHeader.Read(prot);
							this.DataPageHeader = dataPageHeader;
							flag = false;
						}
						break;
					case 6:
						if (tfield.Type == TType.Struct)
						{
							IndexPageHeader indexPageHeader = new IndexPageHeader();
							indexPageHeader.Read(prot);
							this.IndexPageHeader = indexPageHeader;
							flag = false;
						}
						break;
					case 7:
						if (tfield.Type == TType.Struct)
						{
							DictionaryPageHeader dictionaryPageHeader = new DictionaryPageHeader();
							dictionaryPageHeader.Read(prot);
							this.DictionaryPageHeader = dictionaryPageHeader;
							flag = false;
						}
						break;
					case 8:
						if (tfield.Type == TType.Struct)
						{
							DataPageHeaderV2 dataPageHeaderV = new DataPageHeaderV2();
							dataPageHeaderV.Read(prot);
							this.DataPageHeaderV2 = dataPageHeaderV;
							flag = false;
						}
						break;
					}
					prot.EndField(tfield.Type, flag);
				}
				prot.ReadStructEnd();
				if (this.Type == null || this.UncompressedPageSize == null || this.CompressedPageSize == null)
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
