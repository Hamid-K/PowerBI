using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x0200010C RID: 268
	[OriginalName("DefinitionItem")]
	public class DefinitionItem : INotifyPropertyChanged
	{
		// Token: 0x06000B93 RID: 2963 RVA: 0x00016A6E File Offset: 0x00014C6E
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static DefinitionItem CreateDefinitionItem(Guid ID)
		{
			return new DefinitionItem
			{
				Id = ID
			};
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06000B94 RID: 2964 RVA: 0x00016A7C File Offset: 0x00014C7C
		// (set) Token: 0x06000B95 RID: 2965 RVA: 0x00016A84 File Offset: 0x00014C84
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

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06000B96 RID: 2966 RVA: 0x00016A98 File Offset: 0x00014C98
		// (set) Token: 0x06000B97 RID: 2967 RVA: 0x00016AA0 File Offset: 0x00014CA0
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

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06000B98 RID: 2968 RVA: 0x00016AB4 File Offset: 0x00014CB4
		// (set) Token: 0x06000B99 RID: 2969 RVA: 0x00016ABC File Offset: 0x00014CBC
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

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06000B9A RID: 2970 RVA: 0x00016AD0 File Offset: 0x00014CD0
		// (set) Token: 0x06000B9B RID: 2971 RVA: 0x00016AD8 File Offset: 0x00014CD8
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

		// Token: 0x1400007F RID: 127
		// (add) Token: 0x06000B9C RID: 2972 RVA: 0x00016AEC File Offset: 0x00014CEC
		// (remove) Token: 0x06000B9D RID: 2973 RVA: 0x00016B24 File Offset: 0x00014D24
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000B9E RID: 2974 RVA: 0x00016B59 File Offset: 0x00014D59
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x04000544 RID: 1348
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private Guid _Id;

		// Token: 0x04000545 RID: 1349
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Path;

		// Token: 0x04000546 RID: 1350
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Name;

		// Token: 0x04000547 RID: 1351
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Hash;
	}
}
