using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000076 RID: 118
	public sealed class ManagedFolderInformation : ComplexProperty
	{
		// Token: 0x06000544 RID: 1348 RVA: 0x00012743 File Offset: 0x00011743
		internal ManagedFolderInformation()
		{
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x0001274C File Offset: 0x0001174C
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			string localName;
			if ((localName = reader.LocalName) != null)
			{
				if (<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x60004e8-1 == null)
				{
					Dictionary<string, int> dictionary = new Dictionary<string, int>(10);
					dictionary.Add("CanDelete", 0);
					dictionary.Add("CanRenameOrMove", 1);
					dictionary.Add("MustDisplayComment", 2);
					dictionary.Add("HasQuota", 3);
					dictionary.Add("IsManagedFoldersRoot", 4);
					dictionary.Add("ManagedFolderId", 5);
					dictionary.Add("Comment", 6);
					dictionary.Add("StorageQuota", 7);
					dictionary.Add("FolderSize", 8);
					dictionary.Add("HomePage", 9);
					<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x60004e8-1 = dictionary;
				}
				int num;
				if (<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x60004e8-1.TryGetValue(localName, ref num))
				{
					switch (num)
					{
					case 0:
						this.canDelete = new bool?(reader.ReadValue<bool>());
						return true;
					case 1:
						this.canRenameOrMove = new bool?(reader.ReadValue<bool>());
						return true;
					case 2:
						this.mustDisplayComment = new bool?(reader.ReadValue<bool>());
						return true;
					case 3:
						this.hasQuota = new bool?(reader.ReadValue<bool>());
						return true;
					case 4:
						this.isManagedFoldersRoot = new bool?(reader.ReadValue<bool>());
						return true;
					case 5:
						this.managedFolderId = reader.ReadValue();
						return true;
					case 6:
						reader.TryReadValue(ref this.comment);
						return true;
					case 7:
						this.storageQuota = new int?(reader.ReadValue<int>());
						return true;
					case 8:
						this.folderSize = new int?(reader.ReadValue<int>());
						return true;
					case 9:
						reader.TryReadValue(ref this.homePage);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x000128F4 File Offset: 0x000118F4
		internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
		{
			foreach (string text in jsonProperty.Keys)
			{
				string text2;
				if ((text2 = text) != null)
				{
					if (<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x60004e9-1 == null)
					{
						Dictionary<string, int> dictionary = new Dictionary<string, int>(10);
						dictionary.Add("CanDelete", 0);
						dictionary.Add("CanRenameOrMove", 1);
						dictionary.Add("MustDisplayComment", 2);
						dictionary.Add("HasQuota", 3);
						dictionary.Add("IsManagedFoldersRoot", 4);
						dictionary.Add("ManagedFolderId", 5);
						dictionary.Add("Comment", 6);
						dictionary.Add("StorageQuota", 7);
						dictionary.Add("FolderSize", 8);
						dictionary.Add("HomePage", 9);
						<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x60004e9-1 = dictionary;
					}
					int num;
					if (<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x60004e9-1.TryGetValue(text2, ref num))
					{
						switch (num)
						{
						case 0:
							this.canDelete = new bool?(jsonProperty.ReadAsBool(text));
							break;
						case 1:
							this.canRenameOrMove = new bool?(jsonProperty.ReadAsBool(text));
							break;
						case 2:
							this.mustDisplayComment = new bool?(jsonProperty.ReadAsBool(text));
							break;
						case 3:
							this.hasQuota = new bool?(jsonProperty.ReadAsBool(text));
							break;
						case 4:
							this.isManagedFoldersRoot = new bool?(jsonProperty.ReadAsBool(text));
							break;
						case 5:
							this.managedFolderId = jsonProperty.ReadAsString(text);
							break;
						case 6:
						{
							string text3 = jsonProperty.ReadAsString(text);
							if (text3 != null)
							{
								this.comment = text3;
							}
							break;
						}
						case 7:
							this.storageQuota = new int?(jsonProperty.ReadAsInt(text));
							break;
						case 8:
							this.folderSize = new int?(jsonProperty.ReadAsInt(text));
							break;
						case 9:
						{
							string text4 = jsonProperty.ReadAsString(text);
							if (text4 != null)
							{
								this.homePage = text4;
							}
							break;
						}
						}
					}
				}
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000547 RID: 1351 RVA: 0x00012B04 File Offset: 0x00011B04
		public bool? CanDelete
		{
			get
			{
				return this.canDelete;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000548 RID: 1352 RVA: 0x00012B0C File Offset: 0x00011B0C
		public bool? CanRenameOrMove
		{
			get
			{
				return this.canRenameOrMove;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000549 RID: 1353 RVA: 0x00012B14 File Offset: 0x00011B14
		public bool? MustDisplayComment
		{
			get
			{
				return this.mustDisplayComment;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x0600054A RID: 1354 RVA: 0x00012B1C File Offset: 0x00011B1C
		public bool? HasQuota
		{
			get
			{
				return this.hasQuota;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x0600054B RID: 1355 RVA: 0x00012B24 File Offset: 0x00011B24
		public bool? IsManagedFoldersRoot
		{
			get
			{
				return this.isManagedFoldersRoot;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600054C RID: 1356 RVA: 0x00012B2C File Offset: 0x00011B2C
		public string ManagedFolderId
		{
			get
			{
				return this.managedFolderId;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600054D RID: 1357 RVA: 0x00012B34 File Offset: 0x00011B34
		public string Comment
		{
			get
			{
				return this.comment;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600054E RID: 1358 RVA: 0x00012B3C File Offset: 0x00011B3C
		public int? StorageQuota
		{
			get
			{
				return this.storageQuota;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600054F RID: 1359 RVA: 0x00012B44 File Offset: 0x00011B44
		public int? FolderSize
		{
			get
			{
				return this.folderSize;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000550 RID: 1360 RVA: 0x00012B4C File Offset: 0x00011B4C
		public string HomePage
		{
			get
			{
				return this.homePage;
			}
		}

		// Token: 0x040001CA RID: 458
		private bool? canDelete;

		// Token: 0x040001CB RID: 459
		private bool? canRenameOrMove;

		// Token: 0x040001CC RID: 460
		private bool? mustDisplayComment;

		// Token: 0x040001CD RID: 461
		private bool? hasQuota;

		// Token: 0x040001CE RID: 462
		private bool? isManagedFoldersRoot;

		// Token: 0x040001CF RID: 463
		private string managedFolderId;

		// Token: 0x040001D0 RID: 464
		private string comment;

		// Token: 0x040001D1 RID: 465
		private int? storageQuota;

		// Token: 0x040001D2 RID: 466
		private int? folderSize;

		// Token: 0x040001D3 RID: 467
		private string homePage;
	}
}
