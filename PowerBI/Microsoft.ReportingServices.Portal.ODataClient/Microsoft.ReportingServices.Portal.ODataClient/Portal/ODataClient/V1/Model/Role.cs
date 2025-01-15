using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000B9 RID: 185
	[OriginalName("Role")]
	public class Role : INotifyPropertyChanged
	{
		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x060007E9 RID: 2025 RVA: 0x0001008C File Offset: 0x0000E28C
		// (set) Token: 0x060007EA RID: 2026 RVA: 0x00010094 File Offset: 0x0000E294
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

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x060007EB RID: 2027 RVA: 0x000100A8 File Offset: 0x0000E2A8
		// (set) Token: 0x060007EC RID: 2028 RVA: 0x000100B0 File Offset: 0x0000E2B0
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Description")]
		public string Description
		{
			get
			{
				return this._Description;
			}
			set
			{
				this._Description = value;
				this.OnPropertyChanged("Description");
			}
		}

		// Token: 0x14000052 RID: 82
		// (add) Token: 0x060007ED RID: 2029 RVA: 0x000100C4 File Offset: 0x0000E2C4
		// (remove) Token: 0x060007EE RID: 2030 RVA: 0x000100FC File Offset: 0x0000E2FC
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x060007EF RID: 2031 RVA: 0x00010131 File Offset: 0x0000E331
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x040003CE RID: 974
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Name;

		// Token: 0x040003CF RID: 975
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Description;
	}
}
