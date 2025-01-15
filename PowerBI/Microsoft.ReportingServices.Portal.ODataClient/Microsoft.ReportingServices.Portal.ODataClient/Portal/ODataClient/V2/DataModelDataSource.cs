using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000070 RID: 112
	[OriginalName("DataModelDataSource")]
	public class DataModelDataSource : INotifyPropertyChanged
	{
		// Token: 0x060004FA RID: 1274 RVA: 0x0000A455 File Offset: 0x00008655
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static DataModelDataSource CreateDataModelDataSource(DataModelDataSourceType type, DataModelDataSourceKind kind, DataModelDataSourceAuthType authType)
		{
			return new DataModelDataSource
			{
				Type = type,
				Kind = kind,
				AuthType = authType
			};
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x060004FB RID: 1275 RVA: 0x0000A471 File Offset: 0x00008671
		// (set) Token: 0x060004FC RID: 1276 RVA: 0x0000A479 File Offset: 0x00008679
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

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x0000A48D File Offset: 0x0000868D
		// (set) Token: 0x060004FE RID: 1278 RVA: 0x0000A495 File Offset: 0x00008695
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

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x060004FF RID: 1279 RVA: 0x0000A4A9 File Offset: 0x000086A9
		// (set) Token: 0x06000500 RID: 1280 RVA: 0x0000A4B1 File Offset: 0x000086B1
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

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000501 RID: 1281 RVA: 0x0000A4C5 File Offset: 0x000086C5
		// (set) Token: 0x06000502 RID: 1282 RVA: 0x0000A4CD File Offset: 0x000086CD
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("SupportedAuthTypes")]
		public ObservableCollection<DataModelDataSourceAuthType> SupportedAuthTypes
		{
			get
			{
				return this._SupportedAuthTypes;
			}
			set
			{
				this._SupportedAuthTypes = value;
				this.OnPropertyChanged("SupportedAuthTypes");
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000503 RID: 1283 RVA: 0x0000A4E1 File Offset: 0x000086E1
		// (set) Token: 0x06000504 RID: 1284 RVA: 0x0000A4E9 File Offset: 0x000086E9
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

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000505 RID: 1285 RVA: 0x0000A4FD File Offset: 0x000086FD
		// (set) Token: 0x06000506 RID: 1286 RVA: 0x0000A505 File Offset: 0x00008705
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

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000507 RID: 1287 RVA: 0x0000A519 File Offset: 0x00008719
		// (set) Token: 0x06000508 RID: 1288 RVA: 0x0000A521 File Offset: 0x00008721
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("ModelConnectionName")]
		public string ModelConnectionName
		{
			get
			{
				return this._ModelConnectionName;
			}
			set
			{
				this._ModelConnectionName = value;
				this.OnPropertyChanged("ModelConnectionName");
			}
		}

		// Token: 0x14000038 RID: 56
		// (add) Token: 0x06000509 RID: 1289 RVA: 0x0000A538 File Offset: 0x00008738
		// (remove) Token: 0x0600050A RID: 1290 RVA: 0x0000A570 File Offset: 0x00008770
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x0600050B RID: 1291 RVA: 0x0000A5A5 File Offset: 0x000087A5
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x04000247 RID: 583
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataModelDataSourceType _Type;

		// Token: 0x04000248 RID: 584
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataModelDataSourceKind _Kind;

		// Token: 0x04000249 RID: 585
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataModelDataSourceAuthType _AuthType;

		// Token: 0x0400024A RID: 586
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ObservableCollection<DataModelDataSourceAuthType> _SupportedAuthTypes = new ObservableCollection<DataModelDataSourceAuthType>();

		// Token: 0x0400024B RID: 587
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Username;

		// Token: 0x0400024C RID: 588
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Secret;

		// Token: 0x0400024D RID: 589
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _ModelConnectionName;
	}
}
