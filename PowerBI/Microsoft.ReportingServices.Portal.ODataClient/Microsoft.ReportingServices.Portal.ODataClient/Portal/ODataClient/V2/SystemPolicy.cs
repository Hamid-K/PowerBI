using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x0200004A RID: 74
	[Key("Id")]
	[OriginalName("SystemPolicy")]
	public class SystemPolicy : BaseEntityType, INotifyPropertyChanged
	{
		// Token: 0x0600034F RID: 847 RVA: 0x00007E09 File Offset: 0x00006009
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static SystemPolicy CreateSystemPolicy(Guid ID)
		{
			return new SystemPolicy
			{
				Id = ID
			};
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000350 RID: 848 RVA: 0x00007E17 File Offset: 0x00006017
		// (set) Token: 0x06000351 RID: 849 RVA: 0x00007E1F File Offset: 0x0000601F
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Id")]
		public Guid Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				this._Id = value;
				this.OnPropertyChanged("Id");
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000352 RID: 850 RVA: 0x00007E33 File Offset: 0x00006033
		// (set) Token: 0x06000353 RID: 851 RVA: 0x00007E3B File Offset: 0x0000603B
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Policies")]
		public ObservableCollection<Policy> Policies
		{
			get
			{
				return this._Policies;
			}
			set
			{
				this._Policies = value;
				this.OnPropertyChanged("Policies");
			}
		}

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x06000354 RID: 852 RVA: 0x00007E50 File Offset: 0x00006050
		// (remove) Token: 0x06000355 RID: 853 RVA: 0x00007E88 File Offset: 0x00006088
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000356 RID: 854 RVA: 0x00007EBD File Offset: 0x000060BD
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x040001A6 RID: 422
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private Guid _Id;

		// Token: 0x040001A7 RID: 423
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ObservableCollection<Policy> _Policies = new ObservableCollection<Policy>();
	}
}
