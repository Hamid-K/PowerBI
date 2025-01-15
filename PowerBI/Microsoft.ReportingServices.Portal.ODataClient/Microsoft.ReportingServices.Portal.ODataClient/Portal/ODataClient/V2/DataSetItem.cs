using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000081 RID: 129
	[OriginalName("DataSetItem")]
	public class DataSetItem : INotifyPropertyChanged
	{
		// Token: 0x060005A1 RID: 1441 RVA: 0x0000B3B1 File Offset: 0x000095B1
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

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x060005A2 RID: 1442 RVA: 0x0000B3CD File Offset: 0x000095CD
		// (set) Token: 0x060005A3 RID: 1443 RVA: 0x0000B3D5 File Offset: 0x000095D5
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

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x060005A4 RID: 1444 RVA: 0x0000B3E9 File Offset: 0x000095E9
		// (set) Token: 0x060005A5 RID: 1445 RVA: 0x0000B3F1 File Offset: 0x000095F1
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

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x060005A6 RID: 1446 RVA: 0x0000B405 File Offset: 0x00009605
		// (set) Token: 0x060005A7 RID: 1447 RVA: 0x0000B40D File Offset: 0x0000960D
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

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x060005A8 RID: 1448 RVA: 0x0000B421 File Offset: 0x00009621
		// (set) Token: 0x060005A9 RID: 1449 RVA: 0x0000B429 File Offset: 0x00009629
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

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x060005AA RID: 1450 RVA: 0x0000B43D File Offset: 0x0000963D
		// (set) Token: 0x060005AB RID: 1451 RVA: 0x0000B445 File Offset: 0x00009645
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

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x060005AC RID: 1452 RVA: 0x0000B459 File Offset: 0x00009659
		// (set) Token: 0x060005AD RID: 1453 RVA: 0x0000B461 File Offset: 0x00009661
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

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x060005AE RID: 1454 RVA: 0x0000B475 File Offset: 0x00009675
		// (set) Token: 0x060005AF RID: 1455 RVA: 0x0000B47D File Offset: 0x0000967D
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

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x060005B0 RID: 1456 RVA: 0x0000B491 File Offset: 0x00009691
		// (set) Token: 0x060005B1 RID: 1457 RVA: 0x0000B499 File Offset: 0x00009699
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

		// Token: 0x14000044 RID: 68
		// (add) Token: 0x060005B2 RID: 1458 RVA: 0x0000B4B0 File Offset: 0x000096B0
		// (remove) Token: 0x060005B3 RID: 1459 RVA: 0x0000B4E8 File Offset: 0x000096E8
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x060005B4 RID: 1460 RVA: 0x0000B51D File Offset: 0x0000971D
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x04000288 RID: 648
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private MobileReportDataSetType _Type;

		// Token: 0x04000289 RID: 649
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _TimeUnit;

		// Token: 0x0400028A RID: 650
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _DateTimeColumn;

		// Token: 0x0400028B RID: 651
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _IsParameterized;

		// Token: 0x0400028C RID: 652
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private Guid _Id;

		// Token: 0x0400028D RID: 653
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Path;

		// Token: 0x0400028E RID: 654
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Name;

		// Token: 0x0400028F RID: 655
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Hash;
	}
}
