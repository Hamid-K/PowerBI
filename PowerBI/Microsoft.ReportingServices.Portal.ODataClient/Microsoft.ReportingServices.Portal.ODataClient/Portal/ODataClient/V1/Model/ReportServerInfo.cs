using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000D9 RID: 217
	[Key("Id")]
	[OriginalName("ReportServerInfo")]
	public class ReportServerInfo : BaseEntityType, INotifyPropertyChanged
	{
		// Token: 0x0600099D RID: 2461 RVA: 0x000135E2 File Offset: 0x000117E2
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static ReportServerInfo CreateReportServerInfo(Guid ID)
		{
			return new ReportServerInfo
			{
				Id = ID
			};
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x0600099E RID: 2462 RVA: 0x000135F0 File Offset: 0x000117F0
		// (set) Token: 0x0600099F RID: 2463 RVA: 0x000135F8 File Offset: 0x000117F8
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

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x060009A0 RID: 2464 RVA: 0x0001360C File Offset: 0x0001180C
		// (set) Token: 0x060009A1 RID: 2465 RVA: 0x00013614 File Offset: 0x00011814
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("ReportServerUrl")]
		public string ReportServerUrl
		{
			get
			{
				return this._ReportServerUrl;
			}
			set
			{
				this._ReportServerUrl = value;
				this.OnPropertyChanged("ReportServerUrl");
			}
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x060009A2 RID: 2466 RVA: 0x00013628 File Offset: 0x00011828
		// (set) Token: 0x060009A3 RID: 2467 RVA: 0x00013630 File Offset: 0x00011830
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("VirtualDirectory")]
		public string VirtualDirectory
		{
			get
			{
				return this._VirtualDirectory;
			}
			set
			{
				this._VirtualDirectory = value;
				this.OnPropertyChanged("VirtualDirectory");
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x060009A4 RID: 2468 RVA: 0x00013644 File Offset: 0x00011844
		// (set) Token: 0x060009A5 RID: 2469 RVA: 0x0001364C File Offset: 0x0001184C
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("WebAppUrl")]
		public string WebAppUrl
		{
			get
			{
				return this._WebAppUrl;
			}
			set
			{
				this._WebAppUrl = value;
				this.OnPropertyChanged("WebAppUrl");
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x060009A6 RID: 2470 RVA: 0x00013660 File Offset: 0x00011860
		// (set) Token: 0x060009A7 RID: 2471 RVA: 0x00013668 File Offset: 0x00011868
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Roles")]
		public ObservableCollection<Role> Roles
		{
			get
			{
				return this._Roles;
			}
			set
			{
				this._Roles = value;
				this.OnPropertyChanged("Roles");
			}
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x060009A8 RID: 2472 RVA: 0x0001367C File Offset: 0x0001187C
		// (set) Token: 0x060009A9 RID: 2473 RVA: 0x00013684 File Offset: 0x00011884
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Policies")]
		public DataServiceCollection<SystemPolicy> Policies
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

		// Token: 0x14000066 RID: 102
		// (add) Token: 0x060009AA RID: 2474 RVA: 0x00013698 File Offset: 0x00011898
		// (remove) Token: 0x060009AB RID: 2475 RVA: 0x000136D0 File Offset: 0x000118D0
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x060009AC RID: 2476 RVA: 0x00013705 File Offset: 0x00011905
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x00013724 File Offset: 0x00011924
		[OriginalName("DeliveryExtensions")]
		public DataServiceQuery<Extension> DeliveryExtensions()
		{
			Uri uri;
			base.Context.TryGetUri(this, out uri);
			return base.Context.CreateFunctionQuery<Extension>(string.Join("/", from s in uri.Segments.Skip(base.Context.BaseUri.Segments.Length)
				select s.Trim(new char[] { '/' })), "Model.DeliveryExtensions", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x000137A4 File Offset: 0x000119A4
		[OriginalName("DeliveryUIExtensions")]
		public DataServiceQuery<Extension> DeliveryUIExtensions()
		{
			Uri uri;
			base.Context.TryGetUri(this, out uri);
			return base.Context.CreateFunctionQuery<Extension>(string.Join("/", from s in uri.Segments.Skip(base.Context.BaseUri.Segments.Length)
				select s.Trim(new char[] { '/' })), "Model.DeliveryUIExtensions", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x00013824 File Offset: 0x00011A24
		[OriginalName("DataExtensions")]
		public DataServiceQuery<Extension> DataExtensions()
		{
			Uri uri;
			base.Context.TryGetUri(this, out uri);
			return base.Context.CreateFunctionQuery<Extension>(string.Join("/", from s in uri.Segments.Skip(base.Context.BaseUri.Segments.Length)
				select s.Trim(new char[] { '/' })), "Model.DataExtensions", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x000138A4 File Offset: 0x00011AA4
		[OriginalName("RenderingExtensions")]
		public DataServiceQuery<Extension> RenderingExtensions()
		{
			Uri uri;
			base.Context.TryGetUri(this, out uri);
			return base.Context.CreateFunctionQuery<Extension>(string.Join("/", from s in uri.Segments.Skip(base.Context.BaseUri.Segments.Length)
				select s.Trim(new char[] { '/' })), "Model.RenderingExtensions", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x00013924 File Offset: 0x00011B24
		[OriginalName("ExtensionParameters")]
		public DataServiceQuery<ExtensionParameter> ExtensionParameters(string ExtensionName)
		{
			Uri uri;
			base.Context.TryGetUri(this, out uri);
			return base.Context.CreateFunctionQuery<ExtensionParameter>(string.Join("/", from s in uri.Segments.Skip(base.Context.BaseUri.Segments.Length)
				select s.Trim(new char[] { '/' })), "Model.ExtensionParameters", false, new UriOperationParameter[]
			{
				new UriOperationParameter("ExtensionName", ExtensionName)
			});
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x000139B0 File Offset: 0x00011BB0
		[OriginalName("RestrictedSettings")]
		public DataServiceQuery<Property> RestrictedSettings()
		{
			Uri uri;
			base.Context.TryGetUri(this, out uri);
			return base.Context.CreateFunctionQuery<Property>(string.Join("/", from s in uri.Segments.Skip(base.Context.BaseUri.Segments.Length)
				select s.Trim(new char[] { '/' })), "Model.RestrictedSettings", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x00013A30 File Offset: 0x00011C30
		[OriginalName("Settings")]
		public DataServiceQuery<Property> Settings()
		{
			Uri uri;
			base.Context.TryGetUri(this, out uri);
			return base.Context.CreateFunctionQuery<Property>(string.Join("/", from s in uri.Segments.Skip(base.Context.BaseUri.Segments.Length)
				select s.Trim(new char[] { '/' })), "Model.Settings", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x00013AB0 File Offset: 0x00011CB0
		[OriginalName("ServerProductInfo")]
		public DataServiceQuery<Property> ServerProductInfo()
		{
			Uri uri;
			base.Context.TryGetUri(this, out uri);
			return base.Context.CreateFunctionQuery<Property>(string.Join("/", from s in uri.Segments.Skip(base.Context.BaseUri.Segments.Length)
				select s.Trim(new char[] { '/' })), "Model.ServerProductInfo", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x00013B30 File Offset: 0x00011D30
		[OriginalName("SiteName")]
		public DataServiceQuerySingle<string> SiteName()
		{
			Uri uri;
			base.Context.TryGetUri(this, out uri);
			return base.Context.CreateFunctionQuerySingle<string>(string.Join("/", from s in uri.Segments.Skip(base.Context.BaseUri.Segments.Length)
				select s.Trim(new char[] { '/' })), "Model.SiteName", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x00013BB0 File Offset: 0x00011DB0
		[OriginalName("GetWebAppUrl")]
		public DataServiceQuerySingle<string> GetWebAppUrl()
		{
			Uri uri;
			base.Context.TryGetUri(this, out uri);
			return base.Context.CreateFunctionQuerySingle<string>(string.Join("/", from s in uri.Segments.Skip(base.Context.BaseUri.Segments.Length)
				select s.Trim(new char[] { '/' })), "Model.GetWebAppUrl", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x00013C30 File Offset: 0x00011E30
		[OriginalName("GetVirtualDirectory")]
		public DataServiceQuerySingle<string> GetVirtualDirectory()
		{
			Uri uri;
			base.Context.TryGetUri(this, out uri);
			return base.Context.CreateFunctionQuerySingle<string>(string.Join("/", from s in uri.Segments.Skip(base.Context.BaseUri.Segments.Length)
				select s.Trim(new char[] { '/' })), "Model.GetVirtualDirectory", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x00013CB0 File Offset: 0x00011EB0
		[OriginalName("ValidateExtensionSettings")]
		public DataServiceActionQuery<ExtensionParameter> ValidateExtensionSettings(ICollection<ParameterValue> ParameterValues, string ExtensionName)
		{
			EntityDescriptor entityDescriptor = base.Context.EntityTracker.TryGetEntityDescriptor(this);
			if (entityDescriptor == null)
			{
				throw new Exception("cannot find entity");
			}
			return new DataServiceActionQuery<ExtensionParameter>(base.Context, entityDescriptor.EditLink.OriginalString.Trim(new char[] { '/' }) + "/Model.ValidateExtensionSettings", new BodyOperationParameter[]
			{
				new BodyOperationParameter("ParameterValues", ParameterValues),
				new BodyOperationParameter("ExtensionName", ExtensionName)
			});
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x00013D30 File Offset: 0x00011F30
		[OriginalName("UpdateSettings")]
		public DataServiceActionQuerySingle<bool> UpdateSettings(ICollection<Property> PropertyValues)
		{
			EntityDescriptor entityDescriptor = base.Context.EntityTracker.TryGetEntityDescriptor(this);
			if (entityDescriptor == null)
			{
				throw new Exception("cannot find entity");
			}
			return new DataServiceActionQuerySingle<bool>(base.Context, entityDescriptor.EditLink.OriginalString.Trim(new char[] { '/' }) + "/Model.UpdateSettings", new BodyOperationParameter[]
			{
				new BodyOperationParameter("PropertyValues", PropertyValues)
			});
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x00013DA4 File Offset: 0x00011FA4
		[OriginalName("SetSystemPolicies")]
		public DataServiceActionQuerySingle<bool> SetSystemPolicies(ItemPolicy Policy)
		{
			EntityDescriptor entityDescriptor = base.Context.EntityTracker.TryGetEntityDescriptor(this);
			if (entityDescriptor == null)
			{
				throw new Exception("cannot find entity");
			}
			return new DataServiceActionQuerySingle<bool>(base.Context, entityDescriptor.EditLink.OriginalString.Trim(new char[] { '/' }) + "/Model.SetSystemPolicies", new BodyOperationParameter[]
			{
				new BodyOperationParameter("Policy", Policy)
			});
		}

		// Token: 0x04000481 RID: 1153
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private Guid _Id;

		// Token: 0x04000482 RID: 1154
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _ReportServerUrl;

		// Token: 0x04000483 RID: 1155
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _VirtualDirectory;

		// Token: 0x04000484 RID: 1156
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _WebAppUrl;

		// Token: 0x04000485 RID: 1157
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ObservableCollection<Role> _Roles = new ObservableCollection<Role>();

		// Token: 0x04000486 RID: 1158
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceCollection<SystemPolicy> _Policies = new DataServiceCollection<SystemPolicy>(null, TrackingMode.None);
	}
}
