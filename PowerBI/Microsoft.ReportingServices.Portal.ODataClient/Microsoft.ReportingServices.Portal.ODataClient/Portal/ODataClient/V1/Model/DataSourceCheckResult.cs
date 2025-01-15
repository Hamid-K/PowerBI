using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000C7 RID: 199
	[OriginalName("DataSourceCheckResult")]
	public class DataSourceCheckResult : INotifyPropertyChanged
	{
		// Token: 0x060008D4 RID: 2260 RVA: 0x000123BB File Offset: 0x000105BB
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static DataSourceCheckResult CreateDataSourceCheckResult(bool isSuccessful)
		{
			return new DataSourceCheckResult
			{
				IsSuccessful = isSuccessful
			};
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x060008D5 RID: 2261 RVA: 0x000123C9 File Offset: 0x000105C9
		// (set) Token: 0x060008D6 RID: 2262 RVA: 0x000123D1 File Offset: 0x000105D1
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

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x060008D7 RID: 2263 RVA: 0x000123E5 File Offset: 0x000105E5
		// (set) Token: 0x060008D8 RID: 2264 RVA: 0x000123ED File Offset: 0x000105ED
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

		// Token: 0x14000059 RID: 89
		// (add) Token: 0x060008D9 RID: 2265 RVA: 0x00012404 File Offset: 0x00010604
		// (remove) Token: 0x060008DA RID: 2266 RVA: 0x0001243C File Offset: 0x0001063C
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x060008DB RID: 2267 RVA: 0x00012471 File Offset: 0x00010671
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x04000433 RID: 1075
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _IsSuccessful;

		// Token: 0x04000434 RID: 1076
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _ErrorMessage;
	}
}
