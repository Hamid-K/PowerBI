using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000E8 RID: 232
	[Key("Id")]
	[OriginalName("PowerBIUserInfo")]
	public class PowerBIUserInfo : BaseEntityType, INotifyPropertyChanged
	{
		// Token: 0x06000A42 RID: 2626 RVA: 0x000148F3 File Offset: 0x00012AF3
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static PowerBIUserInfo CreatePowerBIUserInfo(Guid ID, PowerBIUserStatus status)
		{
			return new PowerBIUserInfo
			{
				Id = ID,
				Status = status
			};
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000A43 RID: 2627 RVA: 0x00014908 File Offset: 0x00012B08
		// (set) Token: 0x06000A44 RID: 2628 RVA: 0x00014910 File Offset: 0x00012B10
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

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000A45 RID: 2629 RVA: 0x00014924 File Offset: 0x00012B24
		// (set) Token: 0x06000A46 RID: 2630 RVA: 0x0001492C File Offset: 0x00012B2C
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("UserName")]
		public string UserName
		{
			get
			{
				return this._UserName;
			}
			set
			{
				this._UserName = value;
				this.OnPropertyChanged("UserName");
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000A47 RID: 2631 RVA: 0x00014940 File Offset: 0x00012B40
		// (set) Token: 0x06000A48 RID: 2632 RVA: 0x00014948 File Offset: 0x00012B48
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Status")]
		public PowerBIUserStatus Status
		{
			get
			{
				return this._Status;
			}
			set
			{
				this._Status = value;
				this.OnPropertyChanged("Status");
			}
		}

		// Token: 0x1400006F RID: 111
		// (add) Token: 0x06000A49 RID: 2633 RVA: 0x0001495C File Offset: 0x00012B5C
		// (remove) Token: 0x06000A4A RID: 2634 RVA: 0x00014994 File Offset: 0x00012B94
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000A4B RID: 2635 RVA: 0x000149C9 File Offset: 0x00012BC9
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x040004B8 RID: 1208
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private Guid _Id;

		// Token: 0x040004B9 RID: 1209
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _UserName;

		// Token: 0x040004BA RID: 1210
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private PowerBIUserStatus _Status;
	}
}
