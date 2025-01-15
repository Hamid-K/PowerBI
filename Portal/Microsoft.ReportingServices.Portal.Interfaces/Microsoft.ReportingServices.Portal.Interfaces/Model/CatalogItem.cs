using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Microsoft.BIServer.HostingEnvironment;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Model
{
	// Token: 0x02000076 RID: 118
	public abstract class CatalogItem
	{
		// Token: 0x06000373 RID: 883 RVA: 0x000041F1 File Offset: 0x000023F1
		protected CatalogItem(CatalogItemType type)
		{
			this.Type = type;
			this.Content = new byte[0];
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000374 RID: 884 RVA: 0x0000420C File Offset: 0x0000240C
		// (set) Token: 0x06000375 RID: 885 RVA: 0x00004214 File Offset: 0x00002414
		[ReadOnly(true)]
		public Guid Id { get; set; }

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000376 RID: 886 RVA: 0x0000421D File Offset: 0x0000241D
		// (set) Token: 0x06000377 RID: 887 RVA: 0x00004225 File Offset: 0x00002425
		public string Name { get; set; }

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000378 RID: 888 RVA: 0x0000422E File Offset: 0x0000242E
		// (set) Token: 0x06000379 RID: 889 RVA: 0x00004236 File Offset: 0x00002436
		public string Description { get; set; }

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x0600037A RID: 890 RVA: 0x0000423F File Offset: 0x0000243F
		// (set) Token: 0x0600037B RID: 891 RVA: 0x00004247 File Offset: 0x00002447
		public string Path { get; set; }

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x0600037C RID: 892 RVA: 0x00004250 File Offset: 0x00002450
		// (set) Token: 0x0600037D RID: 893 RVA: 0x00004258 File Offset: 0x00002458
		[JsonConverter(typeof(StringEnumConverter))]
		public CatalogItemType Type { get; set; }

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x0600037E RID: 894 RVA: 0x00004261 File Offset: 0x00002461
		// (set) Token: 0x0600037F RID: 895 RVA: 0x00004269 File Offset: 0x00002469
		public bool Hidden { get; set; }

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000380 RID: 896 RVA: 0x00004272 File Offset: 0x00002472
		// (set) Token: 0x06000381 RID: 897 RVA: 0x0000427A File Offset: 0x0000247A
		public long Size { get; set; }

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000382 RID: 898 RVA: 0x00004283 File Offset: 0x00002483
		// (set) Token: 0x06000383 RID: 899 RVA: 0x0000428B File Offset: 0x0000248B
		public string ModifiedBy { get; set; }

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000384 RID: 900 RVA: 0x00004294 File Offset: 0x00002494
		// (set) Token: 0x06000385 RID: 901 RVA: 0x0000429C File Offset: 0x0000249C
		public DateTimeOffset ModifiedDate { get; set; }

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000386 RID: 902 RVA: 0x000042A5 File Offset: 0x000024A5
		// (set) Token: 0x06000387 RID: 903 RVA: 0x000042AD File Offset: 0x000024AD
		public string CreatedBy { get; set; }

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000388 RID: 904 RVA: 0x000042B6 File Offset: 0x000024B6
		// (set) Token: 0x06000389 RID: 905 RVA: 0x000042BE File Offset: 0x000024BE
		public DateTimeOffset CreatedDate { get; set; }

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x0600038A RID: 906 RVA: 0x000042C7 File Offset: 0x000024C7
		// (set) Token: 0x0600038B RID: 907 RVA: 0x000042CF File Offset: 0x000024CF
		public Guid? ParentFolderId { get; set; }

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x0600038C RID: 908 RVA: 0x000042D8 File Offset: 0x000024D8
		// (set) Token: 0x0600038D RID: 909 RVA: 0x000042E0 File Offset: 0x000024E0
		public Folder ParentFolder { get; set; }

		// Token: 0x0600038E RID: 910 RVA: 0x000042E9 File Offset: 0x000024E9
		public string ComputeParentPath()
		{
			if (!this.Path.EndsWith(this.Name, StringComparison.OrdinalIgnoreCase))
			{
				return this.Path;
			}
			return CatalogItem.GetParentPathFromFullPath(this.Path);
		}

		// Token: 0x0600038F RID: 911 RVA: 0x00004311 File Offset: 0x00002511
		public static string GetNameFromFullPath(string itemPath)
		{
			return itemPath.Split(new char[] { '/' }).Last<string>();
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0000432C File Offset: 0x0000252C
		public static string GetParentPathFromFullPath(string itemPath)
		{
			string[] array = itemPath.Split(new char[] { '/' });
			int num = itemPath.Length - array[array.Length - 1].Length;
			if (num > 1)
			{
				num--;
			}
			string text = itemPath.Substring(0, num);
			if (!text.StartsWith("/"))
			{
				text = "/" + text;
			}
			return text;
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000391 RID: 913 RVA: 0x0000438C File Offset: 0x0000258C
		// (set) Token: 0x06000392 RID: 914 RVA: 0x000043B2 File Offset: 0x000025B2
		public IList<Property> Properties
		{
			get
			{
				IList<Property> list;
				if ((list = this._properties) == null)
				{
					list = (this._properties = this.LoadProperties());
				}
				return list;
			}
			set
			{
				this._properties = value;
			}
		}

		// Token: 0x06000393 RID: 915 RVA: 0x00002F76 File Offset: 0x00001176
		protected virtual IList<Property> LoadProperties()
		{
			return new List<Property>();
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000394 RID: 916 RVA: 0x000043BB File Offset: 0x000025BB
		// (set) Token: 0x06000395 RID: 917 RVA: 0x000043C3 File Offset: 0x000025C3
		public bool IsFavorite { get; set; }

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000396 RID: 918 RVA: 0x000043CC File Offset: 0x000025CC
		public IList<Comment> Comments
		{
			get
			{
				IList<Comment> list;
				if ((list = this._comments) == null)
				{
					list = (this._comments = this.LoadComments());
				}
				return list;
			}
		}

		// Token: 0x06000397 RID: 919 RVA: 0x000043F2 File Offset: 0x000025F2
		protected virtual IList<Comment> LoadComments()
		{
			return new List<Comment>();
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000398 RID: 920 RVA: 0x000043FC File Offset: 0x000025FC
		public IList<AlertSubscription> AlertSubscriptions
		{
			get
			{
				IList<AlertSubscription> list;
				if ((list = this._alertSubscriptions) == null)
				{
					list = (this._alertSubscriptions = this.LoadAlertSubscriptions());
				}
				return list;
			}
		}

		// Token: 0x06000399 RID: 921 RVA: 0x00004422 File Offset: 0x00002622
		protected virtual IList<AlertSubscription> LoadAlertSubscriptions()
		{
			return new List<AlertSubscription>();
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x0600039A RID: 922 RVA: 0x0000442C File Offset: 0x0000262C
		public IList<AllowedAction> AllowedActions
		{
			get
			{
				IList<AllowedAction> list;
				if ((list = this._allowedActions) == null)
				{
					list = (this._allowedActions = this.LoadAllowedActions());
				}
				return list;
			}
		}

		// Token: 0x0600039B RID: 923 RVA: 0x00002F10 File Offset: 0x00001110
		protected virtual IList<AllowedAction> LoadAllowedActions()
		{
			return new List<AllowedAction>();
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x0600039C RID: 924 RVA: 0x00004454 File Offset: 0x00002654
		public IList<ItemPolicy> Policies
		{
			get
			{
				IList<ItemPolicy> list;
				if ((list = this._itemPolicies) == null)
				{
					list = (this._itemPolicies = this.LoadPolicies());
				}
				return list;
			}
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0000447A File Offset: 0x0000267A
		protected virtual IList<ItemPolicy> LoadPolicies()
		{
			return new List<ItemPolicy>();
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x0600039E RID: 926 RVA: 0x00004484 File Offset: 0x00002684
		public IList<Role> Roles
		{
			get
			{
				IList<Role> list;
				if ((list = this._itemRoles) == null)
				{
					list = (this._itemRoles = this.LoadRoles());
				}
				return list;
			}
		}

		// Token: 0x0600039F RID: 927 RVA: 0x00002F46 File Offset: 0x00001146
		protected virtual IList<Role> LoadRoles()
		{
			return new List<Role>();
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x000044AC File Offset: 0x000026AC
		public IList<CatalogItem> DependentItems
		{
			get
			{
				IList<CatalogItem> list;
				if ((list = this._dependentItems) == null)
				{
					list = (this._dependentItems = this.LoadDependentItems());
				}
				return list;
			}
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x000041EA File Offset: 0x000023EA
		protected virtual IList<CatalogItem> LoadDependentItems()
		{
			return new List<CatalogItem>();
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060003A2 RID: 930 RVA: 0x000044D2 File Offset: 0x000026D2
		// (set) Token: 0x060003A3 RID: 931 RVA: 0x000044DA File Offset: 0x000026DA
		public string ContentType { get; set; }

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x000044E4 File Offset: 0x000026E4
		// (set) Token: 0x060003A5 RID: 933 RVA: 0x00004598 File Offset: 0x00002798
		public byte[] Content
		{
			get
			{
				if ((this._content != null && this._content.Length != 0) || this._contentStream == null || !this._contentStream.CanRead || this._contentStream.Length == 0L)
				{
					return this._content;
				}
				MemoryStream memoryStream = this._contentStream as MemoryStream;
				if (memoryStream != null)
				{
					Logger.Debug("Perf warning, array copied through stream... setter should just set content(byte[])", Array.Empty<object>());
					this._content = memoryStream.ToArray();
				}
				else
				{
					using (MemoryStream memoryStream2 = new MemoryStream())
					{
						this._contentStream.CopyTo(memoryStream2);
						this._content = memoryStream2.ToArray();
					}
				}
				return this._content;
			}
			set
			{
				this._content = value;
				if (this._contentStream != null)
				{
					Logger.Debug("Perf warning, replacing _contentStream... this can likely be avoided", Array.Empty<object>());
					this._contentStream.Dispose();
				}
				this._contentStream = null;
			}
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x000045CC File Offset: 0x000027CC
		public void SetContent(Stream content)
		{
			if (content == null)
			{
				throw new ArgumentNullException("content");
			}
			if (!content.CanRead)
			{
				throw new ArgumentException("stream cannot be read");
			}
			this._content = null;
			if (this._contentStream != null)
			{
				this._contentStream.Dispose();
			}
			this._contentStream = content;
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0000461B File Offset: 0x0000281B
		public Stream GetContentStream()
		{
			if (this._contentStream != null)
			{
				return this._contentStream;
			}
			if (this._content != null)
			{
				this._contentStream = new MemoryStream(this._content);
			}
			return this._contentStream;
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0000464B File Offset: 0x0000284B
		public bool HasContent()
		{
			return (this._contentStream != null && this._contentStream.Length > 0L) || (this.Content != null && this.Content.Length != 0);
		}

		// Token: 0x04000274 RID: 628
		private IList<Comment> _comments;

		// Token: 0x04000275 RID: 629
		private IList<AlertSubscription> _alertSubscriptions;

		// Token: 0x04000276 RID: 630
		private IList<AllowedAction> _allowedActions;

		// Token: 0x04000277 RID: 631
		private IList<CatalogItem> _dependentItems;

		// Token: 0x04000278 RID: 632
		private IList<ItemPolicy> _itemPolicies;

		// Token: 0x04000279 RID: 633
		private IList<Role> _itemRoles;

		// Token: 0x0400027A RID: 634
		private IList<Property> _properties;

		// Token: 0x0400028A RID: 650
		private byte[] _content;

		// Token: 0x0400028B RID: 651
		private Stream _contentStream;
	}
}
