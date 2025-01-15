using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x0200001C RID: 28
	[OriginalName("BulkOperationsResult")]
	public class BulkOperationsResult : INotifyPropertyChanged
	{
		// Token: 0x06000127 RID: 295 RVA: 0x00003AAC File Offset: 0x00001CAC
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static BulkOperationsResult CreateBulkOperationsResult(bool hasErrors)
		{
			return new BulkOperationsResult
			{
				HasErrors = hasErrors
			};
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00003ABA File Offset: 0x00001CBA
		// (set) Token: 0x06000129 RID: 297 RVA: 0x00003AC2 File Offset: 0x00001CC2
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

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00003AD6 File Offset: 0x00001CD6
		// (set) Token: 0x0600012B RID: 299 RVA: 0x00003ADE File Offset: 0x00001CDE
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

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x0600012C RID: 300 RVA: 0x00003AF4 File Offset: 0x00001CF4
		// (remove) Token: 0x0600012D RID: 301 RVA: 0x00003B2C File Offset: 0x00001D2C
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x0600012E RID: 302 RVA: 0x00003B61 File Offset: 0x00001D61
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x040000AD RID: 173
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ObservableCollection<string> _FailedOperations = new ObservableCollection<string>();

		// Token: 0x040000AE RID: 174
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _HasErrors;
	}
}
