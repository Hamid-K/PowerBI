using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000B7 RID: 183
	[OriginalName("Property")]
	public class Property : INotifyPropertyChanged
	{
		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x060007D8 RID: 2008 RVA: 0x0000FEE8 File Offset: 0x0000E0E8
		// (set) Token: 0x060007D9 RID: 2009 RVA: 0x0000FEF0 File Offset: 0x0000E0F0
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

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x060007DA RID: 2010 RVA: 0x0000FF04 File Offset: 0x0000E104
		// (set) Token: 0x060007DB RID: 2011 RVA: 0x0000FF0C File Offset: 0x0000E10C
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

		// Token: 0x14000050 RID: 80
		// (add) Token: 0x060007DC RID: 2012 RVA: 0x0000FF20 File Offset: 0x0000E120
		// (remove) Token: 0x060007DD RID: 2013 RVA: 0x0000FF58 File Offset: 0x0000E158
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x060007DE RID: 2014 RVA: 0x0000FF8D File Offset: 0x0000E18D
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x040003C8 RID: 968
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Name;

		// Token: 0x040003C9 RID: 969
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Value;
	}
}
