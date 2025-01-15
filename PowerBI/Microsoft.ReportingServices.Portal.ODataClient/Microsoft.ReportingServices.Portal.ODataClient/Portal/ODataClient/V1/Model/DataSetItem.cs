using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x0200010F RID: 271
	[OriginalName("DataSetItem")]
	public class DataSetItem : INotifyPropertyChanged
	{
		// Token: 0x06000BBA RID: 3002 RVA: 0x00016D99 File Offset: 0x00014F99
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static DataSetItem CreateDataSetItem(MobileReportDataSetType type, bool isParameterized, Guid ID)
		{
			return new DataSetItem
			{
				Type = type,
				IsParameterized = isParameterized,
				Id = ID
			};
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06000BBB RID: 3003 RVA: 0x00016DB5 File Offset: 0x00014FB5
		// (set) Token: 0x06000BBC RID: 3004 RVA: 0x00016DBD File Offset: 0x00014FBD
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Type")]
		public MobileReportDataSetType Type
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

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06000BBD RID: 3005 RVA: 0x00016DD1 File Offset: 0x00014FD1
		// (set) Token: 0x06000BBE RID: 3006 RVA: 0x00016DD9 File Offset: 0x00014FD9
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("TimeUnit")]
		public string TimeUnit
		{
			get
			{
				return this._TimeUnit;
			}
			set
			{
				this._TimeUnit = value;
				this.OnPropertyChanged("TimeUnit");
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06000BBF RID: 3007 RVA: 0x00016DED File Offset: 0x00014FED
		// (set) Token: 0x06000BC0 RID: 3008 RVA: 0x00016DF5 File Offset: 0x00014FF5
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("DateTimeColumn")]
		public string DateTimeColumn
		{
			get
			{
				return this._DateTimeColumn;
			}
			set
			{
				this._DateTimeColumn = value;
				this.OnPropertyChanged("DateTimeColumn");
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06000BC1 RID: 3009 RVA: 0x00016E09 File Offset: 0x00015009
		// (set) Token: 0x06000BC2 RID: 3010 RVA: 0x00016E11 File Offset: 0x00015011
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("IsParameterized")]
		public bool IsParameterized
		{
			get
			{
				return this._IsParameterized;
			}
			set
			{
				this._IsParameterized = value;
				this.OnPropertyChanged("IsParameterized");
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06000BC3 RID: 3011 RVA: 0x00016E25 File Offset: 0x00015025
		// (set) Token: 0x06000BC4 RID: 3012 RVA: 0x00016E2D File Offset: 0x0001502D
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

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06000BC5 RID: 3013 RVA: 0x00016E41 File Offset: 0x00015041
		// (set) Token: 0x06000BC6 RID: 3014 RVA: 0x00016E49 File Offset: 0x00015049
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

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06000BC7 RID: 3015 RVA: 0x00016E5D File Offset: 0x0001505D
		// (set) Token: 0x06000BC8 RID: 3016 RVA: 0x00016E65 File Offset: 0x00015065
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

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06000BC9 RID: 3017 RVA: 0x00016E79 File Offset: 0x00015079
		// (set) Token: 0x06000BCA RID: 3018 RVA: 0x00016E81 File Offset: 0x00015081
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

		// Token: 0x14000082 RID: 130
		// (add) Token: 0x06000BCB RID: 3019 RVA: 0x00016E98 File Offset: 0x00015098
		// (remove) Token: 0x06000BCC RID: 3020 RVA: 0x00016ED0 File Offset: 0x000150D0
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000BCD RID: 3021 RVA: 0x00016F05 File Offset: 0x00015105
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x04000553 RID: 1363
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private MobileReportDataSetType _Type;

		// Token: 0x04000554 RID: 1364
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _TimeUnit;

		// Token: 0x04000555 RID: 1365
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _DateTimeColumn;

		// Token: 0x04000556 RID: 1366
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _IsParameterized;

		// Token: 0x04000557 RID: 1367
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private Guid _Id;

		// Token: 0x04000558 RID: 1368
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Path;

		// Token: 0x04000559 RID: 1369
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Name;

		// Token: 0x0400055A RID: 1370
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Hash;
	}
}
