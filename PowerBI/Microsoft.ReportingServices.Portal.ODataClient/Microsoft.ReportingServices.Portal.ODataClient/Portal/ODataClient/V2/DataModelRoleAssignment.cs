using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000042 RID: 66
	[Key("GroupUserName")]
	[OriginalName("DataModelRoleAssignment")]
	public class DataModelRoleAssignment : BaseEntityType, INotifyPropertyChanged
	{
		// Token: 0x060002C1 RID: 705 RVA: 0x00006DFB File Offset: 0x00004FFB
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static DataModelRoleAssignment CreateDataModelRoleAssignment(string groupUserName)
		{
			return new DataModelRoleAssignment
			{
				GroupUserName = groupUserName
			};
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x00006E09 File Offset: 0x00005009
		// (set) Token: 0x060002C3 RID: 707 RVA: 0x00006E11 File Offset: 0x00005011
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("GroupUserName")]
		public string GroupUserName
		{
			get
			{
				return this._GroupUserName;
			}
			set
			{
				this._GroupUserName = value;
				this.OnPropertyChanged("GroupUserName");
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x00006E25 File Offset: 0x00005025
		// (set) Token: 0x060002C5 RID: 709 RVA: 0x00006E2D File Offset: 0x0000502D
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("DataModelRoles")]
		public ObservableCollection<Guid> DataModelRoles
		{
			get
			{
				return this._DataModelRoles;
			}
			set
			{
				this._DataModelRoles = value;
				this.OnPropertyChanged("DataModelRoles");
			}
		}

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x060002C6 RID: 710 RVA: 0x00006E44 File Offset: 0x00005044
		// (remove) Token: 0x060002C7 RID: 711 RVA: 0x00006E7C File Offset: 0x0000507C
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x060002C8 RID: 712 RVA: 0x00006EB1 File Offset: 0x000050B1
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x04000164 RID: 356
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _GroupUserName;

		// Token: 0x04000165 RID: 357
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ObservableCollection<Guid> _DataModelRoles = new ObservableCollection<Guid>();
	}
}
