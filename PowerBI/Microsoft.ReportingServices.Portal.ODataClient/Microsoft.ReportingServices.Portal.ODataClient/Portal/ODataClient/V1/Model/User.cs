using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000E4 RID: 228
	[Key("Id")]
	[OriginalName("User")]
	public class User : BaseEntityType, INotifyPropertyChanged
	{
		// Token: 0x06000A24 RID: 2596 RVA: 0x000146BB File Offset: 0x000128BB
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static User CreateUser(Guid ID, bool hasFavoriteItems)
		{
			return new User
			{
				Id = ID,
				HasFavoriteItems = hasFavoriteItems
			};
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000A25 RID: 2597 RVA: 0x000146D0 File Offset: 0x000128D0
		// (set) Token: 0x06000A26 RID: 2598 RVA: 0x000146D8 File Offset: 0x000128D8
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

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000A27 RID: 2599 RVA: 0x000146EC File Offset: 0x000128EC
		// (set) Token: 0x06000A28 RID: 2600 RVA: 0x000146F4 File Offset: 0x000128F4
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

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000A29 RID: 2601 RVA: 0x00014708 File Offset: 0x00012908
		// (set) Token: 0x06000A2A RID: 2602 RVA: 0x00014710 File Offset: 0x00012910
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

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000A2B RID: 2603 RVA: 0x00014724 File Offset: 0x00012924
		// (set) Token: 0x06000A2C RID: 2604 RVA: 0x0001472C File Offset: 0x0001292C
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

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000A2D RID: 2605 RVA: 0x00014740 File Offset: 0x00012940
		// (set) Token: 0x06000A2E RID: 2606 RVA: 0x00014748 File Offset: 0x00012948
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

		// Token: 0x1400006D RID: 109
		// (add) Token: 0x06000A2F RID: 2607 RVA: 0x0001475C File Offset: 0x0001295C
		// (remove) Token: 0x06000A30 RID: 2608 RVA: 0x00014794 File Offset: 0x00012994
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000A31 RID: 2609 RVA: 0x000147C9 File Offset: 0x000129C9
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x040004AF RID: 1199
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private Guid _Id;

		// Token: 0x040004B0 RID: 1200
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Username;

		// Token: 0x040004B1 RID: 1201
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _DisplayName;

		// Token: 0x040004B2 RID: 1202
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _HasFavoriteItems;

		// Token: 0x040004B3 RID: 1203
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _MyReportsPath;
	}
}
