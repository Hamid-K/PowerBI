using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000006 RID: 6
	[OriginalName("KpiDataItem")]
	public abstract class KpiDataItem : INotifyPropertyChanged
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002700 File Offset: 0x00000900
		// (set) Token: 0x0600003E RID: 62 RVA: 0x00002708 File Offset: 0x00000908
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

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600003F RID: 63 RVA: 0x0000271C File Offset: 0x0000091C
		// (remove) Token: 0x06000040 RID: 64 RVA: 0x00002754 File Offset: 0x00000954
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000041 RID: 65 RVA: 0x00002789 File Offset: 0x00000989
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x04000052 RID: 82
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private KpiDataItemType _Type;
	}
}
