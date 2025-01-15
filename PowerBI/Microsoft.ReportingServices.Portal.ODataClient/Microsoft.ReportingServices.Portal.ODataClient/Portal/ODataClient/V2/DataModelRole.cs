using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000040 RID: 64
	[Key("Id")]
	[OriginalName("DataModelRole")]
	public class DataModelRole : BaseEntityType, INotifyPropertyChanged
	{
		// Token: 0x060002B3 RID: 691 RVA: 0x00006CEA File Offset: 0x00004EEA
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static DataModelRole CreateDataModelRole(long ID, Guid modelRoleId)
		{
			return new DataModelRole
			{
				Id = ID,
				ModelRoleId = modelRoleId
			};
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x00006CFF File Offset: 0x00004EFF
		// (set) Token: 0x060002B5 RID: 693 RVA: 0x00006D07 File Offset: 0x00004F07
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Id")]
		public long Id
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

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x00006D1B File Offset: 0x00004F1B
		// (set) Token: 0x060002B7 RID: 695 RVA: 0x00006D23 File Offset: 0x00004F23
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("ModelRoleId")]
		public Guid ModelRoleId
		{
			get
			{
				return this._ModelRoleId;
			}
			set
			{
				this._ModelRoleId = value;
				this.OnPropertyChanged("ModelRoleId");
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x00006D37 File Offset: 0x00004F37
		// (set) Token: 0x060002B9 RID: 697 RVA: 0x00006D3F File Offset: 0x00004F3F
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("ModelRoleName")]
		public string ModelRoleName
		{
			get
			{
				return this._ModelRoleName;
			}
			set
			{
				this._ModelRoleName = value;
				this.OnPropertyChanged("ModelRoleName");
			}
		}

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x060002BA RID: 698 RVA: 0x00006D54 File Offset: 0x00004F54
		// (remove) Token: 0x060002BB RID: 699 RVA: 0x00006D8C File Offset: 0x00004F8C
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x060002BC RID: 700 RVA: 0x00006DC1 File Offset: 0x00004FC1
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x04000160 RID: 352
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private long _Id;

		// Token: 0x04000161 RID: 353
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private Guid _ModelRoleId;

		// Token: 0x04000162 RID: 354
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _ModelRoleName;
	}
}
