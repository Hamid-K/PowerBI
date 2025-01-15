using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Linq;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000065 RID: 101
	[Key("Id")]
	[OriginalName("PowerBIUserInfo")]
	public class PowerBIUserInfo : BaseEntityType, INotifyPropertyChanged
	{
		// Token: 0x06000473 RID: 1139 RVA: 0x0000982F File Offset: 0x00007A2F
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static PowerBIUserInfo CreatePowerBIUserInfo(Guid ID, PowerBIUserStatus status)
		{
			return new PowerBIUserInfo
			{
				Id = ID,
				Status = status
			};
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000474 RID: 1140 RVA: 0x00009844 File Offset: 0x00007A44
		// (set) Token: 0x06000475 RID: 1141 RVA: 0x0000984C File Offset: 0x00007A4C
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

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x00009860 File Offset: 0x00007A60
		// (set) Token: 0x06000477 RID: 1143 RVA: 0x00009868 File Offset: 0x00007A68
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

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000478 RID: 1144 RVA: 0x0000987C File Offset: 0x00007A7C
		// (set) Token: 0x06000479 RID: 1145 RVA: 0x00009884 File Offset: 0x00007A84
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

		// Token: 0x1400002E RID: 46
		// (add) Token: 0x0600047A RID: 1146 RVA: 0x00009898 File Offset: 0x00007A98
		// (remove) Token: 0x0600047B RID: 1147 RVA: 0x000098D0 File Offset: 0x00007AD0
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x0600047C RID: 1148 RVA: 0x00009905 File Offset: 0x00007B05
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x00009924 File Offset: 0x00007B24
		[OriginalName("IsEnabled")]
		public DataServiceQuerySingle<bool> IsEnabled()
		{
			Uri uri;
			base.Context.TryGetUri(this, out uri);
			return base.Context.CreateFunctionQuerySingle<bool>(string.Join("/", from s in uri.Segments.Skip(base.Context.BaseUri.Segments.Length)
				select s.Trim(new char[] { '/' })), "Model.IsEnabled", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x000099A4 File Offset: 0x00007BA4
		[OriginalName("Logout")]
		public DataServiceActionQuery Logout()
		{
			EntityDescriptor entityDescriptor = base.Context.EntityTracker.TryGetEntityDescriptor(this);
			if (entityDescriptor == null)
			{
				throw new Exception("cannot find entity");
			}
			return new DataServiceActionQuery(base.Context, entityDescriptor.EditLink.OriginalString.Trim(new char[] { '/' }) + "/Model.Logout", Array.Empty<BodyOperationParameter>());
		}

		// Token: 0x04000214 RID: 532
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private Guid _Id;

		// Token: 0x04000215 RID: 533
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _UserName;

		// Token: 0x04000216 RID: 534
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private PowerBIUserStatus _Status;
	}
}
