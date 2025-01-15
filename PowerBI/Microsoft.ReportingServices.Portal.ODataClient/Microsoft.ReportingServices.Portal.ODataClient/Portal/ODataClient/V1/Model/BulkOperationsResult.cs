using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000B8 RID: 184
	[OriginalName("BulkOperationsResult")]
	public class BulkOperationsResult : INotifyPropertyChanged
	{
		// Token: 0x060007E0 RID: 2016 RVA: 0x0000FFA9 File Offset: 0x0000E1A9
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static BulkOperationsResult CreateBulkOperationsResult(bool hasErrors)
		{
			return new BulkOperationsResult
			{
				HasErrors = hasErrors
			};
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x060007E1 RID: 2017 RVA: 0x0000FFB7 File Offset: 0x0000E1B7
		// (set) Token: 0x060007E2 RID: 2018 RVA: 0x0000FFBF File Offset: 0x0000E1BF
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("FailedOperations")]
		public ObservableCollection<string> FailedOperations
		{
			get
			{
				return this._FailedOperations;
			}
			set
			{
				this._FailedOperations = value;
				this.OnPropertyChanged("FailedOperations");
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x060007E3 RID: 2019 RVA: 0x0000FFD3 File Offset: 0x0000E1D3
		// (set) Token: 0x060007E4 RID: 2020 RVA: 0x0000FFDB File Offset: 0x0000E1DB
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("HasErrors")]
		public bool HasErrors
		{
			get
			{
				return this._HasErrors;
			}
			set
			{
				this._HasErrors = value;
				this.OnPropertyChanged("HasErrors");
			}
		}

		// Token: 0x14000051 RID: 81
		// (add) Token: 0x060007E5 RID: 2021 RVA: 0x0000FFF0 File Offset: 0x0000E1F0
		// (remove) Token: 0x060007E6 RID: 2022 RVA: 0x00010028 File Offset: 0x0000E228
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x060007E7 RID: 2023 RVA: 0x0001005D File Offset: 0x0000E25D
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x040003CB RID: 971
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ObservableCollection<string> _FailedOperations = new ObservableCollection<string>();

		// Token: 0x040003CC RID: 972
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _HasErrors;
	}
}
