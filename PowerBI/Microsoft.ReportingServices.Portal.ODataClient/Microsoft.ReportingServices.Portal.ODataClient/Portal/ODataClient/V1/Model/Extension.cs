using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000DA RID: 218
	[OriginalName("Extension")]
	public class Extension : INotifyPropertyChanged
	{
		// Token: 0x060009BC RID: 2492 RVA: 0x00013E35 File Offset: 0x00012035
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static Extension CreateExtension(ExtensionType extensionType, bool visible)
		{
			return new Extension
			{
				ExtensionType = extensionType,
				Visible = visible
			};
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x060009BD RID: 2493 RVA: 0x00013E4A File Offset: 0x0001204A
		// (set) Token: 0x060009BE RID: 2494 RVA: 0x00013E52 File Offset: 0x00012052
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("ExtensionType")]
		public ExtensionType ExtensionType
		{
			get
			{
				return this._ExtensionType;
			}
			set
			{
				this._ExtensionType = value;
				this.OnPropertyChanged("ExtensionType");
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x060009BF RID: 2495 RVA: 0x00013E66 File Offset: 0x00012066
		// (set) Token: 0x060009C0 RID: 2496 RVA: 0x00013E6E File Offset: 0x0001206E
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

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x060009C1 RID: 2497 RVA: 0x00013E82 File Offset: 0x00012082
		// (set) Token: 0x060009C2 RID: 2498 RVA: 0x00013E8A File Offset: 0x0001208A
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("LocalizedName")]
		public string LocalizedName
		{
			get
			{
				return this._LocalizedName;
			}
			set
			{
				this._LocalizedName = value;
				this.OnPropertyChanged("LocalizedName");
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x060009C3 RID: 2499 RVA: 0x00013E9E File Offset: 0x0001209E
		// (set) Token: 0x060009C4 RID: 2500 RVA: 0x00013EA6 File Offset: 0x000120A6
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Visible")]
		public bool Visible
		{
			get
			{
				return this._Visible;
			}
			set
			{
				this._Visible = value;
				this.OnPropertyChanged("Visible");
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x060009C5 RID: 2501 RVA: 0x00013EBA File Offset: 0x000120BA
		// (set) Token: 0x060009C6 RID: 2502 RVA: 0x00013EC2 File Offset: 0x000120C2
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Parameters")]
		public ObservableCollection<ExtensionParameter> Parameters
		{
			get
			{
				return this._Parameters;
			}
			set
			{
				this._Parameters = value;
				this.OnPropertyChanged("Parameters");
			}
		}

		// Token: 0x14000067 RID: 103
		// (add) Token: 0x060009C7 RID: 2503 RVA: 0x00013ED8 File Offset: 0x000120D8
		// (remove) Token: 0x060009C8 RID: 2504 RVA: 0x00013F10 File Offset: 0x00012110
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x060009C9 RID: 2505 RVA: 0x00013F45 File Offset: 0x00012145
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x04000488 RID: 1160
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ExtensionType _ExtensionType;

		// Token: 0x04000489 RID: 1161
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Name;

		// Token: 0x0400048A RID: 1162
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _LocalizedName;

		// Token: 0x0400048B RID: 1163
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _Visible;

		// Token: 0x0400048C RID: 1164
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ObservableCollection<ExtensionParameter> _Parameters = new ObservableCollection<ExtensionParameter>();
	}
}
