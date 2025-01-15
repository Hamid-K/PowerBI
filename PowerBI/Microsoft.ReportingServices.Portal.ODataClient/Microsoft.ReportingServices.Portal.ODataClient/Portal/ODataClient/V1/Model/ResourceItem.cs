using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x0200010E RID: 270
	[OriginalName("ResourceItem")]
	public class ResourceItem : INotifyPropertyChanged
	{
		// Token: 0x06000BAB RID: 2987 RVA: 0x00016C74 File Offset: 0x00014E74
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static ResourceItem CreateResourceItem(Guid ID)
		{
			return new ResourceItem
			{
				Id = ID
			};
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06000BAC RID: 2988 RVA: 0x00016C82 File Offset: 0x00014E82
		// (set) Token: 0x06000BAD RID: 2989 RVA: 0x00016C8A File Offset: 0x00014E8A
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Key")]
		public string Key
		{
			get
			{
				return this._Key;
			}
			set
			{
				this._Key = value;
				this.OnPropertyChanged("Key");
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06000BAE RID: 2990 RVA: 0x00016C9E File Offset: 0x00014E9E
		// (set) Token: 0x06000BAF RID: 2991 RVA: 0x00016CA6 File Offset: 0x00014EA6
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

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06000BB0 RID: 2992 RVA: 0x00016CBA File Offset: 0x00014EBA
		// (set) Token: 0x06000BB1 RID: 2993 RVA: 0x00016CC2 File Offset: 0x00014EC2
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Path")]
		public string Path
		{
			get
			{
				return this._Path;
			}
			set
			{
				this._Path = value;
				this.OnPropertyChanged("Path");
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06000BB2 RID: 2994 RVA: 0x00016CD6 File Offset: 0x00014ED6
		// (set) Token: 0x06000BB3 RID: 2995 RVA: 0x00016CDE File Offset: 0x00014EDE
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Name")]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				this._Name = value;
				this.OnPropertyChanged("Name");
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06000BB4 RID: 2996 RVA: 0x00016CF2 File Offset: 0x00014EF2
		// (set) Token: 0x06000BB5 RID: 2997 RVA: 0x00016CFA File Offset: 0x00014EFA
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Hash")]
		public string Hash
		{
			get
			{
				return this._Hash;
			}
			set
			{
				this._Hash = value;
				this.OnPropertyChanged("Hash");
			}
		}

		// Token: 0x14000081 RID: 129
		// (add) Token: 0x06000BB6 RID: 2998 RVA: 0x00016D10 File Offset: 0x00014F10
		// (remove) Token: 0x06000BB7 RID: 2999 RVA: 0x00016D48 File Offset: 0x00014F48
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000BB8 RID: 3000 RVA: 0x00016D7D File Offset: 0x00014F7D
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x0400054D RID: 1357
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Key;

		// Token: 0x0400054E RID: 1358
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private Guid _Id;

		// Token: 0x0400054F RID: 1359
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Path;

		// Token: 0x04000550 RID: 1360
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Name;

		// Token: 0x04000551 RID: 1361
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Hash;
	}
}
