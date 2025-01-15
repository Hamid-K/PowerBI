using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000AF RID: 175
	[OriginalName("CatalogItemParameter")]
	public class CatalogItemParameter : INotifyPropertyChanged
	{
		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000738 RID: 1848 RVA: 0x0000EEDC File Offset: 0x0000D0DC
		// (set) Token: 0x06000739 RID: 1849 RVA: 0x0000EEE4 File Offset: 0x0000D0E4
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

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x0600073A RID: 1850 RVA: 0x0000EEF8 File Offset: 0x0000D0F8
		// (set) Token: 0x0600073B RID: 1851 RVA: 0x0000EF00 File Offset: 0x0000D100
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Value")]
		public string Value
		{
			get
			{
				return this._Value;
			}
			set
			{
				this._Value = value;
				this.OnPropertyChanged("Value");
			}
		}

		// Token: 0x1400004C RID: 76
		// (add) Token: 0x0600073C RID: 1852 RVA: 0x0000EF14 File Offset: 0x0000D114
		// (remove) Token: 0x0600073D RID: 1853 RVA: 0x0000EF4C File Offset: 0x0000D14C
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x0600073E RID: 1854 RVA: 0x0000EF81 File Offset: 0x0000D181
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x04000385 RID: 901
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Name;

		// Token: 0x04000386 RID: 902
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Value;
	}
}
