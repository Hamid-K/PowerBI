using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x02000118 RID: 280
	[Key("Id")]
	[OriginalName("SystemPolicy")]
	public class SystemPolicy : BaseEntityType, INotifyPropertyChanged
	{
		// Token: 0x06000C19 RID: 3097 RVA: 0x0001766B File Offset: 0x0001586B
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static SystemPolicy CreateSystemPolicy(Guid ID)
		{
			return new SystemPolicy
			{
				Id = ID
			};
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06000C1A RID: 3098 RVA: 0x00017679 File Offset: 0x00015879
		// (set) Token: 0x06000C1B RID: 3099 RVA: 0x00017681 File Offset: 0x00015881
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

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06000C1C RID: 3100 RVA: 0x00017695 File Offset: 0x00015895
		// (set) Token: 0x06000C1D RID: 3101 RVA: 0x0001769D File Offset: 0x0001589D
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

		// Token: 0x14000088 RID: 136
		// (add) Token: 0x06000C1E RID: 3102 RVA: 0x000176B4 File Offset: 0x000158B4
		// (remove) Token: 0x06000C1F RID: 3103 RVA: 0x000176EC File Offset: 0x000158EC
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000C20 RID: 3104 RVA: 0x00017721 File Offset: 0x00015921
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x04000579 RID: 1401
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private Guid _Id;

		// Token: 0x0400057A RID: 1402
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ObservableCollection<Policy> _Policies = new ObservableCollection<Policy>();
	}
}
