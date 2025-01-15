using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x0200006D RID: 109
	[OriginalName("Role")]
	public class Role : INotifyPropertyChanged
	{
		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x060004DC RID: 1244 RVA: 0x0000A1B4 File Offset: 0x000083B4
		// (set) Token: 0x060004DD RID: 1245 RVA: 0x0000A1BC File Offset: 0x000083BC
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

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060004DE RID: 1246 RVA: 0x0000A1D0 File Offset: 0x000083D0
		// (set) Token: 0x060004DF RID: 1247 RVA: 0x0000A1D8 File Offset: 0x000083D8
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

		// Token: 0x14000035 RID: 53
		// (add) Token: 0x060004E0 RID: 1248 RVA: 0x0000A1EC File Offset: 0x000083EC
		// (remove) Token: 0x060004E1 RID: 1249 RVA: 0x0000A224 File Offset: 0x00008424
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x060004E2 RID: 1250 RVA: 0x0000A259 File Offset: 0x00008459
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x0400023C RID: 572
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Name;

		// Token: 0x0400023D RID: 573
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Description;
	}
}
