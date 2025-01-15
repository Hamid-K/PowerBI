using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000A9 RID: 169
	[OriginalName("KpiDataItem")]
	public abstract class KpiDataItem : INotifyPropertyChanged
	{
		// Token: 0x17000253 RID: 595
		// (get) Token: 0x0600070A RID: 1802 RVA: 0x0000EB44 File Offset: 0x0000CD44
		// (set) Token: 0x0600070B RID: 1803 RVA: 0x0000EB4C File Offset: 0x0000CD4C
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Type")]
		public KpiDataItemType Type
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

		// Token: 0x14000049 RID: 73
		// (add) Token: 0x0600070C RID: 1804 RVA: 0x0000EB60 File Offset: 0x0000CD60
		// (remove) Token: 0x0600070D RID: 1805 RVA: 0x0000EB98 File Offset: 0x0000CD98
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x0600070E RID: 1806 RVA: 0x0000EBCD File Offset: 0x0000CDCD
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x04000374 RID: 884
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private KpiDataItemType _Type;
	}
}
