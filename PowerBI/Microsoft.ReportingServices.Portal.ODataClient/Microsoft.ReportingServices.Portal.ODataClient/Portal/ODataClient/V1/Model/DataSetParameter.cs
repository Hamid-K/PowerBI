using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000AB RID: 171
	[OriginalName("DataSetParameter")]
	public class DataSetParameter : INotifyPropertyChanged
	{
		// Token: 0x17000259 RID: 601
		// (get) Token: 0x0600071C RID: 1820 RVA: 0x0000ECA4 File Offset: 0x0000CEA4
		// (set) Token: 0x0600071D RID: 1821 RVA: 0x0000ECAC File Offset: 0x0000CEAC
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

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x0600071E RID: 1822 RVA: 0x0000ECC0 File Offset: 0x0000CEC0
		// (set) Token: 0x0600071F RID: 1823 RVA: 0x0000ECC8 File Offset: 0x0000CEC8
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

		// Token: 0x1400004A RID: 74
		// (add) Token: 0x06000720 RID: 1824 RVA: 0x0000ECDC File Offset: 0x0000CEDC
		// (remove) Token: 0x06000721 RID: 1825 RVA: 0x0000ED14 File Offset: 0x0000CF14
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000722 RID: 1826 RVA: 0x0000ED49 File Offset: 0x0000CF49
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x0400037B RID: 891
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Name;

		// Token: 0x0400037C RID: 892
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Value;
	}
}
