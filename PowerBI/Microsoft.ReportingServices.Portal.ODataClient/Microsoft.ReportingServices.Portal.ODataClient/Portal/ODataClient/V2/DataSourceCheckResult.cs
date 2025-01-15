using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000029 RID: 41
	[OriginalName("DataSourceCheckResult")]
	public class DataSourceCheckResult : INotifyPropertyChanged
	{
		// Token: 0x060001BB RID: 443 RVA: 0x00004C4B File Offset: 0x00002E4B
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static DataSourceCheckResult CreateDataSourceCheckResult(bool isSuccessful)
		{
			return new DataSourceCheckResult
			{
				IsSuccessful = isSuccessful
			};
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060001BC RID: 444 RVA: 0x00004C59 File Offset: 0x00002E59
		// (set) Token: 0x060001BD RID: 445 RVA: 0x00004C61 File Offset: 0x00002E61
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("IsSuccessful")]
		public bool IsSuccessful
		{
			get
			{
				return this._IsSuccessful;
			}
			set
			{
				this._IsSuccessful = value;
				this.OnPropertyChanged("IsSuccessful");
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060001BE RID: 446 RVA: 0x00004C75 File Offset: 0x00002E75
		// (set) Token: 0x060001BF RID: 447 RVA: 0x00004C7D File Offset: 0x00002E7D
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("ErrorMessage")]
		public string ErrorMessage
		{
			get
			{
				return this._ErrorMessage;
			}
			set
			{
				this._ErrorMessage = value;
				this.OnPropertyChanged("ErrorMessage");
			}
		}

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x060001C0 RID: 448 RVA: 0x00004C94 File Offset: 0x00002E94
		// (remove) Token: 0x060001C1 RID: 449 RVA: 0x00004CCC File Offset: 0x00002ECC
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x060001C2 RID: 450 RVA: 0x00004D01 File Offset: 0x00002F01
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x040000EC RID: 236
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _IsSuccessful;

		// Token: 0x040000ED RID: 237
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _ErrorMessage;
	}
}
