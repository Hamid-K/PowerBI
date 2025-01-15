using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000061 RID: 97
	[Key("Id")]
	[OriginalName("User")]
	public class User : BaseEntityType, INotifyPropertyChanged
	{
		// Token: 0x06000455 RID: 1109 RVA: 0x000095F7 File Offset: 0x000077F7
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static User CreateUser(Guid ID, bool hasFavoriteItems)
		{
			return new User
			{
				Id = ID,
				HasFavoriteItems = hasFavoriteItems
			};
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000456 RID: 1110 RVA: 0x0000960C File Offset: 0x0000780C
		// (set) Token: 0x06000457 RID: 1111 RVA: 0x00009614 File Offset: 0x00007814
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Id")]
		public Guid Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				this._Id = value;
				this.OnPropertyChanged("Id");
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000458 RID: 1112 RVA: 0x00009628 File Offset: 0x00007828
		// (set) Token: 0x06000459 RID: 1113 RVA: 0x00009630 File Offset: 0x00007830
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Username")]
		public string Username
		{
			get
			{
				return this._Username;
			}
			set
			{
				this._Username = value;
				this.OnPropertyChanged("Username");
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x0600045A RID: 1114 RVA: 0x00009644 File Offset: 0x00007844
		// (set) Token: 0x0600045B RID: 1115 RVA: 0x0000964C File Offset: 0x0000784C
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("DisplayName")]
		public string DisplayName
		{
			get
			{
				return this._DisplayName;
			}
			set
			{
				this._DisplayName = value;
				this.OnPropertyChanged("DisplayName");
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x0600045C RID: 1116 RVA: 0x00009660 File Offset: 0x00007860
		// (set) Token: 0x0600045D RID: 1117 RVA: 0x00009668 File Offset: 0x00007868
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("HasFavoriteItems")]
		public bool HasFavoriteItems
		{
			get
			{
				return this._HasFavoriteItems;
			}
			set
			{
				this._HasFavoriteItems = value;
				this.OnPropertyChanged("HasFavoriteItems");
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x0600045E RID: 1118 RVA: 0x0000967C File Offset: 0x0000787C
		// (set) Token: 0x0600045F RID: 1119 RVA: 0x00009684 File Offset: 0x00007884
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("MyReportsPath")]
		public string MyReportsPath
		{
			get
			{
				return this._MyReportsPath;
			}
			set
			{
				this._MyReportsPath = value;
				this.OnPropertyChanged("MyReportsPath");
			}
		}

		// Token: 0x1400002C RID: 44
		// (add) Token: 0x06000460 RID: 1120 RVA: 0x00009698 File Offset: 0x00007898
		// (remove) Token: 0x06000461 RID: 1121 RVA: 0x000096D0 File Offset: 0x000078D0
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000462 RID: 1122 RVA: 0x00009705 File Offset: 0x00007905
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x0400020B RID: 523
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private Guid _Id;

		// Token: 0x0400020C RID: 524
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Username;

		// Token: 0x0400020D RID: 525
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _DisplayName;

		// Token: 0x0400020E RID: 526
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _HasFavoriteItems;

		// Token: 0x0400020F RID: 527
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _MyReportsPath;
	}
}
