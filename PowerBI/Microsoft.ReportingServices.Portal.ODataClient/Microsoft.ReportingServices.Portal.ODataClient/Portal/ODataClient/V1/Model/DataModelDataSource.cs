using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000F9 RID: 249
	[Key("Id")]
	[OriginalName("DataModelDataSource")]
	public class DataModelDataSource : BaseEntityType, INotifyPropertyChanged
	{
		// Token: 0x06000AED RID: 2797 RVA: 0x000159D3 File Offset: 0x00013BD3
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static DataModelDataSource CreateDataModelDataSource(Guid ID, DataModelDataSourceType type, DataModelDataSourceKind kind, DataModelDataSourceAuthType authType)
		{
			return new DataModelDataSource
			{
				Id = ID,
				Type = type,
				Kind = kind,
				AuthType = authType
			};
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06000AEE RID: 2798 RVA: 0x000159F6 File Offset: 0x00013BF6
		// (set) Token: 0x06000AEF RID: 2799 RVA: 0x000159FE File Offset: 0x00013BFE
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

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06000AF0 RID: 2800 RVA: 0x00015A12 File Offset: 0x00013C12
		// (set) Token: 0x06000AF1 RID: 2801 RVA: 0x00015A1A File Offset: 0x00013C1A
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Type")]
		public DataModelDataSourceType Type
		{
			get
			{
				return this._Type;
			}
			set
			{
				this._Type = value;
				this.OnPropertyChanged("Type");
			}
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06000AF2 RID: 2802 RVA: 0x00015A2E File Offset: 0x00013C2E
		// (set) Token: 0x06000AF3 RID: 2803 RVA: 0x00015A36 File Offset: 0x00013C36
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Kind")]
		public DataModelDataSourceKind Kind
		{
			get
			{
				return this._Kind;
			}
			set
			{
				this._Kind = value;
				this.OnPropertyChanged("Kind");
			}
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000AF4 RID: 2804 RVA: 0x00015A4A File Offset: 0x00013C4A
		// (set) Token: 0x06000AF5 RID: 2805 RVA: 0x00015A52 File Offset: 0x00013C52
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("AuthType")]
		public DataModelDataSourceAuthType AuthType
		{
			get
			{
				return this._AuthType;
			}
			set
			{
				this._AuthType = value;
				this.OnPropertyChanged("AuthType");
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000AF6 RID: 2806 RVA: 0x00015A66 File Offset: 0x00013C66
		// (set) Token: 0x06000AF7 RID: 2807 RVA: 0x00015A6E File Offset: 0x00013C6E
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("ConnectionString")]
		public string ConnectionString
		{
			get
			{
				return this._ConnectionString;
			}
			set
			{
				this._ConnectionString = value;
				this.OnPropertyChanged("ConnectionString");
			}
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06000AF8 RID: 2808 RVA: 0x00015A82 File Offset: 0x00013C82
		// (set) Token: 0x06000AF9 RID: 2809 RVA: 0x00015A8A File Offset: 0x00013C8A
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

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06000AFA RID: 2810 RVA: 0x00015A9E File Offset: 0x00013C9E
		// (set) Token: 0x06000AFB RID: 2811 RVA: 0x00015AA6 File Offset: 0x00013CA6
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Secret")]
		public string Secret
		{
			get
			{
				return this._Secret;
			}
			set
			{
				this._Secret = value;
				this.OnPropertyChanged("Secret");
			}
		}

		// Token: 0x14000077 RID: 119
		// (add) Token: 0x06000AFC RID: 2812 RVA: 0x00015ABC File Offset: 0x00013CBC
		// (remove) Token: 0x06000AFD RID: 2813 RVA: 0x00015AF4 File Offset: 0x00013CF4
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000AFE RID: 2814 RVA: 0x00015B29 File Offset: 0x00013D29
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x040004FF RID: 1279
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private Guid _Id;

		// Token: 0x04000500 RID: 1280
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataModelDataSourceType _Type;

		// Token: 0x04000501 RID: 1281
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataModelDataSourceKind _Kind;

		// Token: 0x04000502 RID: 1282
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataModelDataSourceAuthType _AuthType;

		// Token: 0x04000503 RID: 1283
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _ConnectionString;

		// Token: 0x04000504 RID: 1284
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Username;

		// Token: 0x04000505 RID: 1285
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Secret;
	}
}
