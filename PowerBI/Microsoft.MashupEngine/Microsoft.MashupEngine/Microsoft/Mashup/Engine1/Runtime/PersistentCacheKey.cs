using System;
using Microsoft.Mashup.Common;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020015CF RID: 5583
	internal struct PersistentCacheKey
	{
		// Token: 0x170024C1 RID: 9409
		// (get) Token: 0x06008C08 RID: 35848 RVA: 0x001D71D5 File Offset: 0x001D53D5
		public static PersistentCacheKey SchemeInvoke
		{
			get
			{
				return new PersistentCacheKey("Scheme.Invoke/3");
			}
		}

		// Token: 0x170024C2 RID: 9410
		// (get) Token: 0x06008C09 RID: 35849 RVA: 0x001D71E1 File Offset: 0x001D53E1
		public static PersistentCacheKey ServerDatabase
		{
			get
			{
				return new PersistentCacheKey("Server.Database/2");
			}
		}

		// Token: 0x170024C3 RID: 9411
		// (get) Token: 0x06008C0A RID: 35850 RVA: 0x001D71ED File Offset: 0x001D53ED
		public static PersistentCacheKey ODataFeed
		{
			get
			{
				return new PersistentCacheKey("OData.Feed/4");
			}
		}

		// Token: 0x170024C4 RID: 9412
		// (get) Token: 0x06008C0B RID: 35851 RVA: 0x001D71F9 File Offset: 0x001D53F9
		public static PersistentCacheKey DatabasePage
		{
			get
			{
				return new PersistentCacheKey("Database.Page/4");
			}
		}

		// Token: 0x170024C5 RID: 9413
		// (get) Token: 0x06008C0C RID: 35852 RVA: 0x001D7205 File Offset: 0x001D5405
		public static PersistentCacheKey ODataQuery
		{
			get
			{
				return new PersistentCacheKey("OData.Cache/1");
			}
		}

		// Token: 0x170024C6 RID: 9414
		// (get) Token: 0x06008C0D RID: 35853 RVA: 0x001D7211 File Offset: 0x001D5411
		public static PersistentCacheKey XmlText
		{
			get
			{
				return new PersistentCacheKey("Xml.Tables/3");
			}
		}

		// Token: 0x170024C7 RID: 9415
		// (get) Token: 0x06008C0E RID: 35854 RVA: 0x001D721D File Offset: 0x001D541D
		public static PersistentCacheKey WebBrowserContents
		{
			get
			{
				return new PersistentCacheKey("Web.BrowserContents/2");
			}
		}

		// Token: 0x170024C8 RID: 9416
		// (get) Token: 0x06008C0F RID: 35855 RVA: 0x001D7229 File Offset: 0x001D5429
		public static PersistentCacheKey WebPageUri
		{
			get
			{
				return new PersistentCacheKey("Web.Page.Uri/2");
			}
		}

		// Token: 0x170024C9 RID: 9417
		// (get) Token: 0x06008C10 RID: 35856 RVA: 0x001D7235 File Offset: 0x001D5435
		public static PersistentCacheKey WebPageText
		{
			get
			{
				return new PersistentCacheKey("Web.Page.Text/4");
			}
		}

		// Token: 0x170024CA RID: 9418
		// (get) Token: 0x06008C11 RID: 35857 RVA: 0x001D7241 File Offset: 0x001D5441
		public static PersistentCacheKey ActiveDirectory
		{
			get
			{
				return new PersistentCacheKey("ActiveDirectory.Domains/1");
			}
		}

		// Token: 0x170024CB RID: 9419
		// (get) Token: 0x06008C12 RID: 35858 RVA: 0x001D724D File Offset: 0x001D544D
		public static PersistentCacheKey ExchangeContents
		{
			get
			{
				return new PersistentCacheKey("Exchange.Contents/2");
			}
		}

		// Token: 0x170024CC RID: 9420
		// (get) Token: 0x06008C13 RID: 35859 RVA: 0x001D7259 File Offset: 0x001D5459
		public static PersistentCacheKey AzureStorageBinary
		{
			get
			{
				return new PersistentCacheKey("Azure.Storage.Binary/4");
			}
		}

		// Token: 0x170024CD RID: 9421
		// (get) Token: 0x06008C14 RID: 35860 RVA: 0x001D7265 File Offset: 0x001D5465
		public static PersistentCacheKey ServerCatalog
		{
			get
			{
				return new PersistentCacheKey("Server.Catalog/5");
			}
		}

		// Token: 0x170024CE RID: 9422
		// (get) Token: 0x06008C15 RID: 35861 RVA: 0x001D7271 File Offset: 0x001D5471
		public static PersistentCacheKey ExcelWorkbook
		{
			get
			{
				return new PersistentCacheKey("Excel.Workbook/4");
			}
		}

		// Token: 0x170024CF RID: 9423
		// (get) Token: 0x06008C16 RID: 35862 RVA: 0x001D727D File Offset: 0x001D547D
		public static PersistentCacheKey SapBw
		{
			get
			{
				return new PersistentCacheKey("Sap.BusinessWarehouse/2");
			}
		}

		// Token: 0x170024D0 RID: 9424
		// (get) Token: 0x06008C17 RID: 35863 RVA: 0x001D7289 File Offset: 0x001D5489
		public static PersistentCacheKey AdobeAnalytics
		{
			get
			{
				return new PersistentCacheKey("Adobe.Cube/1");
			}
		}

		// Token: 0x170024D1 RID: 9425
		// (get) Token: 0x06008C18 RID: 35864 RVA: 0x001D7295 File Offset: 0x001D5495
		public static PersistentCacheKey PdfTables
		{
			get
			{
				return new PersistentCacheKey("Pdf.Tables/3");
			}
		}

		// Token: 0x170024D2 RID: 9426
		// (get) Token: 0x06008C19 RID: 35865 RVA: 0x001D72A1 File Offset: 0x001D54A1
		public static PersistentCacheKey SeekableBinaryValue
		{
			get
			{
				return new PersistentCacheKey("SeekableBinaryValue/1");
			}
		}

		// Token: 0x170024D3 RID: 9427
		// (get) Token: 0x06008C1A RID: 35866 RVA: 0x001D72AD File Offset: 0x001D54AD
		public static PersistentCacheKey CdpaDatabase
		{
			get
			{
				return new PersistentCacheKey("Cdpa.Database/1");
			}
		}

		// Token: 0x170024D4 RID: 9428
		// (get) Token: 0x06008C1B RID: 35867 RVA: 0x001D72B9 File Offset: 0x001D54B9
		public static PersistentCacheKey Extension
		{
			get
			{
				return new PersistentCacheKey("Extension/1");
			}
		}

		// Token: 0x170024D5 RID: 9429
		// (get) Token: 0x06008C1C RID: 35868 RVA: 0x001D72C5 File Offset: 0x001D54C5
		public static PersistentCacheKey SqlClassification
		{
			get
			{
				return new PersistentCacheKey("SqlClassification/1");
			}
		}

		// Token: 0x170024D6 RID: 9430
		// (get) Token: 0x06008C1D RID: 35869 RVA: 0x001D72D1 File Offset: 0x001D54D1
		public static PersistentCacheKey InformationProtection
		{
			get
			{
				return new PersistentCacheKey("InformationProtection/1");
			}
		}

		// Token: 0x06008C1E RID: 35870 RVA: 0x001D72DD File Offset: 0x001D54DD
		public PersistentCacheKey(string root)
		{
			this.root = PersistentCacheKey.Escape(root);
		}

		// Token: 0x06008C1F RID: 35871 RVA: 0x001D72EB File Offset: 0x001D54EB
		public string Qualify(string key)
		{
			return this.root + "/" + PersistentCacheKey.Escape(key);
		}

		// Token: 0x06008C20 RID: 35872 RVA: 0x001D7303 File Offset: 0x001D5503
		public string Qualify(string key1, string key2)
		{
			return string.Concat(new string[]
			{
				this.root,
				"/",
				PersistentCacheKey.Escape(key1),
				"/",
				PersistentCacheKey.Escape(key2)
			});
		}

		// Token: 0x06008C21 RID: 35873 RVA: 0x001D733C File Offset: 0x001D553C
		public string Qualify(string key1, string key2, string key3)
		{
			return string.Concat(new string[]
			{
				this.root,
				"/",
				PersistentCacheKey.Escape(key1),
				"/",
				PersistentCacheKey.Escape(key2),
				"/",
				PersistentCacheKey.Escape(key3)
			});
		}

		// Token: 0x06008C22 RID: 35874 RVA: 0x001D7390 File Offset: 0x001D5590
		public string Qualify(string key1, string key2, string key3, string key4)
		{
			return string.Concat(new string[]
			{
				this.root,
				"/",
				PersistentCacheKey.Escape(key1),
				"/",
				PersistentCacheKey.Escape(key2),
				"/",
				PersistentCacheKey.Escape(key3),
				"/",
				PersistentCacheKey.Escape(key4)
			});
		}

		// Token: 0x06008C23 RID: 35875 RVA: 0x001D73F8 File Offset: 0x001D55F8
		public string Qualify(string key1, string key2, string key3, string key4, string key5)
		{
			return string.Concat(new string[]
			{
				this.root,
				"/",
				PersistentCacheKey.Escape(key1),
				"/",
				PersistentCacheKey.Escape(key2),
				"/",
				PersistentCacheKey.Escape(key3),
				"/",
				PersistentCacheKey.Escape(key4),
				"/",
				PersistentCacheKey.Escape(key5)
			});
		}

		// Token: 0x06008C24 RID: 35876 RVA: 0x001D7474 File Offset: 0x001D5674
		public string Qualify(string key1, string key2, string key3, string key4, string key5, string key6)
		{
			return string.Concat(new string[]
			{
				this.root,
				"/",
				PersistentCacheKey.Escape(key1),
				"/",
				PersistentCacheKey.Escape(key2),
				"/",
				PersistentCacheKey.Escape(key3),
				"/",
				PersistentCacheKey.Escape(key4),
				"/",
				PersistentCacheKey.Escape(key5),
				"/",
				PersistentCacheKey.Escape(key6)
			});
		}

		// Token: 0x06008C25 RID: 35877 RVA: 0x001D7504 File Offset: 0x001D5704
		public string Qualify(params string[] keys)
		{
			PersistentCacheKeyBuilder persistentCacheKeyBuilder = new PersistentCacheKeyBuilder();
			persistentCacheKeyBuilder.Add(this.root);
			foreach (string text in keys)
			{
				persistentCacheKeyBuilder.Add(text);
			}
			return persistentCacheKeyBuilder.ToString();
		}

		// Token: 0x06008C26 RID: 35878 RVA: 0x001D7544 File Offset: 0x001D5744
		public static string Escape(string key)
		{
			return StructuredCacheKeyExtensions.Escape(key);
		}

		// Token: 0x04004CB3 RID: 19635
		public const string Separator = "/";

		// Token: 0x04004CB4 RID: 19636
		private readonly string root;
	}
}
