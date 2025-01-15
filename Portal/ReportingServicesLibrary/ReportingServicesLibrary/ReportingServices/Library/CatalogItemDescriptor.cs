using System;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020002AB RID: 683
	internal class CatalogItemDescriptor
	{
		// Token: 0x060018D2 RID: 6354 RVA: 0x00064818 File Offset: 0x00062A18
		public CatalogItemDescriptor()
		{
			this.m_id = null;
			this.m_name = null;
			this.m_path = null;
			this.m_virtualPath = null;
			this.m_type = ItemType.Unknown;
			this.m_size = -1;
			this.m_description = null;
			this.m_hidden = false;
			this.m_creationDate = DateTime.MinValue;
			this.m_modifiedDate = DateTime.MinValue;
			this.m_createdBy = null;
			this.m_modifiedBy = null;
			this.m_itemMetadata = new ItemMetadata();
		}

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x060018D3 RID: 6355 RVA: 0x00064892 File Offset: 0x00062A92
		// (set) Token: 0x060018D4 RID: 6356 RVA: 0x0006489A File Offset: 0x00062A9A
		public string ID
		{
			get
			{
				return this.m_id;
			}
			set
			{
				this.m_id = value;
			}
		}

		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x060018D5 RID: 6357 RVA: 0x000648A3 File Offset: 0x00062AA3
		// (set) Token: 0x060018D6 RID: 6358 RVA: 0x000648AB File Offset: 0x00062AAB
		public string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x060018D7 RID: 6359 RVA: 0x000648B4 File Offset: 0x00062AB4
		// (set) Token: 0x060018D8 RID: 6360 RVA: 0x000648BC File Offset: 0x00062ABC
		public ExternalItemPath Path
		{
			get
			{
				return this.m_path;
			}
			set
			{
				this.m_path = value;
			}
		}

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x060018D9 RID: 6361 RVA: 0x000648C5 File Offset: 0x00062AC5
		// (set) Token: 0x060018DA RID: 6362 RVA: 0x000648CD File Offset: 0x00062ACD
		public string VirtualPath
		{
			get
			{
				return this.m_virtualPath;
			}
			set
			{
				this.m_virtualPath = value;
			}
		}

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x060018DB RID: 6363 RVA: 0x000648D6 File Offset: 0x00062AD6
		// (set) Token: 0x060018DC RID: 6364 RVA: 0x000648DE File Offset: 0x00062ADE
		public ItemType Type
		{
			get
			{
				return this.m_type;
			}
			set
			{
				this.m_type = value;
			}
		}

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x060018DD RID: 6365 RVA: 0x000648E7 File Offset: 0x00062AE7
		// (set) Token: 0x060018DE RID: 6366 RVA: 0x000648EF File Offset: 0x00062AEF
		public int Size
		{
			get
			{
				return this.m_size;
			}
			set
			{
				this.m_size = value;
			}
		}

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x060018DF RID: 6367 RVA: 0x000648F8 File Offset: 0x00062AF8
		// (set) Token: 0x060018E0 RID: 6368 RVA: 0x00064900 File Offset: 0x00062B00
		public string Description
		{
			get
			{
				return this.m_description;
			}
			set
			{
				this.m_description = value;
			}
		}

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x060018E1 RID: 6369 RVA: 0x00064909 File Offset: 0x00062B09
		// (set) Token: 0x060018E2 RID: 6370 RVA: 0x00064911 File Offset: 0x00062B11
		public bool Hidden
		{
			get
			{
				return this.m_hidden;
			}
			set
			{
				this.m_hidden = value;
			}
		}

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x060018E3 RID: 6371 RVA: 0x0006491A File Offset: 0x00062B1A
		// (set) Token: 0x060018E4 RID: 6372 RVA: 0x00064922 File Offset: 0x00062B22
		public DateTime CreationDate
		{
			get
			{
				return this.m_creationDate;
			}
			set
			{
				this.m_creationDate = value;
			}
		}

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x060018E5 RID: 6373 RVA: 0x0006492B File Offset: 0x00062B2B
		// (set) Token: 0x060018E6 RID: 6374 RVA: 0x00064933 File Offset: 0x00062B33
		public DateTime ModifiedDate
		{
			get
			{
				return this.m_modifiedDate;
			}
			set
			{
				this.m_modifiedDate = value;
			}
		}

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x060018E7 RID: 6375 RVA: 0x0006493C File Offset: 0x00062B3C
		// (set) Token: 0x060018E8 RID: 6376 RVA: 0x00064944 File Offset: 0x00062B44
		public string CreatedBy
		{
			get
			{
				return this.m_createdBy;
			}
			set
			{
				this.m_createdBy = value;
			}
		}

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x060018E9 RID: 6377 RVA: 0x0006494D File Offset: 0x00062B4D
		// (set) Token: 0x060018EA RID: 6378 RVA: 0x00064955 File Offset: 0x00062B55
		public string ModifiedBy
		{
			get
			{
				return this.m_modifiedBy;
			}
			set
			{
				this.m_modifiedBy = value;
			}
		}

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x060018EB RID: 6379 RVA: 0x0006495E File Offset: 0x00062B5E
		// (set) Token: 0x060018EC RID: 6380 RVA: 0x0006496B File Offset: 0x00062B6B
		public string MimeType
		{
			get
			{
				return this.ItemMetadata.MimeType;
			}
			set
			{
				this.ItemMetadata.MimeType = value;
			}
		}

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x060018ED RID: 6381 RVA: 0x00064979 File Offset: 0x00062B79
		// (set) Token: 0x060018EE RID: 6382 RVA: 0x00064986 File Offset: 0x00062B86
		public DateTime ExecutionDate
		{
			get
			{
				return this.ItemMetadata.ExecutionDateTime;
			}
			set
			{
				this.ItemMetadata.ExecutionDateTime = value;
			}
		}

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x060018EF RID: 6383 RVA: 0x00064994 File Offset: 0x00062B94
		// (set) Token: 0x060018F0 RID: 6384 RVA: 0x000649A1 File Offset: 0x00062BA1
		public string SubType
		{
			get
			{
				return this.ItemMetadata.SubType;
			}
			set
			{
				this.ItemMetadata.SubType = value;
			}
		}

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x060018F1 RID: 6385 RVA: 0x000649AF File Offset: 0x00062BAF
		// (set) Token: 0x060018F2 RID: 6386 RVA: 0x000649BC File Offset: 0x00062BBC
		public Guid ComponentID
		{
			get
			{
				return this.ItemMetadata.ComponentID;
			}
			set
			{
				this.ItemMetadata.ComponentID = value;
			}
		}

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x060018F3 RID: 6387 RVA: 0x000649CA File Offset: 0x00062BCA
		// (set) Token: 0x060018F4 RID: 6388 RVA: 0x000649D7 File Offset: 0x00062BD7
		public bool HasDataSources
		{
			get
			{
				return this.ItemMetadata.HasDataSources;
			}
			set
			{
				this.ItemMetadata.HasDataSources = value;
			}
		}

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x060018F5 RID: 6389 RVA: 0x000649E5 File Offset: 0x00062BE5
		// (set) Token: 0x060018F6 RID: 6390 RVA: 0x000649F2 File Offset: 0x00062BF2
		public bool HasParameters
		{
			get
			{
				return this.ItemMetadata.HasParameters;
			}
			set
			{
				this.ItemMetadata.HasParameters = value;
			}
		}

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x060018F7 RID: 6391 RVA: 0x00064A00 File Offset: 0x00062C00
		// (set) Token: 0x060018F8 RID: 6392 RVA: 0x00064A08 File Offset: 0x00062C08
		public ItemMetadata ItemMetadata
		{
			get
			{
				return this.m_itemMetadata;
			}
			internal set
			{
				this.m_itemMetadata = value;
			}
		}

		// Token: 0x040008F2 RID: 2290
		private string m_id;

		// Token: 0x040008F3 RID: 2291
		private string m_name;

		// Token: 0x040008F4 RID: 2292
		private ExternalItemPath m_path;

		// Token: 0x040008F5 RID: 2293
		private string m_virtualPath;

		// Token: 0x040008F6 RID: 2294
		private ItemType m_type;

		// Token: 0x040008F7 RID: 2295
		private int m_size;

		// Token: 0x040008F8 RID: 2296
		private string m_description;

		// Token: 0x040008F9 RID: 2297
		private bool m_hidden;

		// Token: 0x040008FA RID: 2298
		private DateTime m_creationDate;

		// Token: 0x040008FB RID: 2299
		private DateTime m_modifiedDate;

		// Token: 0x040008FC RID: 2300
		private string m_createdBy;

		// Token: 0x040008FD RID: 2301
		private string m_modifiedBy;

		// Token: 0x040008FE RID: 2302
		private ItemMetadata m_itemMetadata;
	}
}
