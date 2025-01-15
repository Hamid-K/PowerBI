using System;
using System.CodeDom.Compiler;
using System.Linq;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000C2 RID: 194
	[Key("Id")]
	[OriginalName("LinkedReport")]
	public class LinkedReport : CatalogItem
	{
		// Token: 0x06000887 RID: 2183 RVA: 0x000115EC File Offset: 0x0000F7EC
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static LinkedReport CreateLinkedReport(Guid ID, CatalogItemType type, bool hidden, long size, DateTimeOffset modifiedDate, DateTimeOffset createdDate, bool isFavorite, bool hasParameters)
		{
			return new LinkedReport
			{
				Id = ID,
				Type = type,
				Hidden = hidden,
				Size = size,
				ModifiedDate = modifiedDate,
				CreatedDate = createdDate,
				IsFavorite = isFavorite,
				HasParameters = hasParameters
			};
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000888 RID: 2184 RVA: 0x0001163A File Offset: 0x0000F83A
		// (set) Token: 0x06000889 RID: 2185 RVA: 0x00011642 File Offset: 0x0000F842
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("HasParameters")]
		public bool HasParameters
		{
			get
			{
				return this._HasParameters;
			}
			set
			{
				this._HasParameters = value;
				this.OnPropertyChanged("HasParameters");
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x0600088A RID: 2186 RVA: 0x00011656 File Offset: 0x0000F856
		// (set) Token: 0x0600088B RID: 2187 RVA: 0x0001165E File Offset: 0x0000F85E
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Link")]
		public string Link
		{
			get
			{
				return this._Link;
			}
			set
			{
				this._Link = value;
				this.OnPropertyChanged("Link");
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x0600088C RID: 2188 RVA: 0x00011672 File Offset: 0x0000F872
		// (set) Token: 0x0600088D RID: 2189 RVA: 0x0001167A File Offset: 0x0000F87A
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Subscriptions")]
		public DataServiceCollection<Subscription> Subscriptions
		{
			get
			{
				return this._Subscriptions;
			}
			set
			{
				this._Subscriptions = value;
				this.OnPropertyChanged("Subscriptions");
			}
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x0600088E RID: 2190 RVA: 0x0001168E File Offset: 0x0000F88E
		// (set) Token: 0x0600088F RID: 2191 RVA: 0x00011696 File Offset: 0x0000F896
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("CacheRefreshPlans")]
		public DataServiceCollection<CacheRefreshPlan> CacheRefreshPlans
		{
			get
			{
				return this._CacheRefreshPlans;
			}
			set
			{
				this._CacheRefreshPlans = value;
				this.OnPropertyChanged("CacheRefreshPlans");
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000890 RID: 2192 RVA: 0x000116AA File Offset: 0x0000F8AA
		// (set) Token: 0x06000891 RID: 2193 RVA: 0x000116B2 File Offset: 0x0000F8B2
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("HistorySnapshotOptions")]
		public HistorySnapshotOptions HistorySnapshotOptions
		{
			get
			{
				return this._HistorySnapshotOptions;
			}
			set
			{
				this._HistorySnapshotOptions = value;
				this.OnPropertyChanged("HistorySnapshotOptions");
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000892 RID: 2194 RVA: 0x000116C6 File Offset: 0x0000F8C6
		// (set) Token: 0x06000893 RID: 2195 RVA: 0x000116CE File Offset: 0x0000F8CE
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("ReportHistorySnapshots")]
		public DataServiceCollection<ReportHistorySnapshot> ReportHistorySnapshots
		{
			get
			{
				return this._ReportHistorySnapshots;
			}
			set
			{
				this._ReportHistorySnapshots = value;
				this.OnPropertyChanged("ReportHistorySnapshots");
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000894 RID: 2196 RVA: 0x000116E2 File Offset: 0x0000F8E2
		// (set) Token: 0x06000895 RID: 2197 RVA: 0x000116EA File Offset: 0x0000F8EA
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("HistorySnapshots")]
		public DataServiceCollection<HistorySnapshot> HistorySnapshots
		{
			get
			{
				return this._HistorySnapshots;
			}
			set
			{
				this._HistorySnapshots = value;
				this.OnPropertyChanged("HistorySnapshots");
			}
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000896 RID: 2198 RVA: 0x000116FE File Offset: 0x0000F8FE
		// (set) Token: 0x06000897 RID: 2199 RVA: 0x00011706 File Offset: 0x0000F906
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("ParameterDefinitions")]
		public DataServiceCollection<ReportParameterDefinition> ParameterDefinitions
		{
			get
			{
				return this._ParameterDefinitions;
			}
			set
			{
				this._ParameterDefinitions = value;
				this.OnPropertyChanged("ParameterDefinitions");
			}
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x0001171C File Offset: 0x0000F91C
		[OriginalName("GetReportHistorySnapshotsOptions")]
		public DataServiceQuerySingle<ReportHistorySnapshotsOptions> GetReportHistorySnapshotsOptions()
		{
			Uri uri;
			base.Context.TryGetUri(this, out uri);
			return base.Context.CreateFunctionQuerySingle<ReportHistorySnapshotsOptions>(string.Join("/", from s in uri.Segments.Skip(base.Context.BaseUri.Segments.Length)
				select s.Trim(new char[] { '/' })), "Model.GetReportHistorySnapshotsOptions", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x0001179C File Offset: 0x0000F99C
		[OriginalName("GetCacheOptions")]
		public DataServiceQuerySingle<CacheOptions> GetCacheOptions()
		{
			Uri uri;
			base.Context.TryGetUri(this, out uri);
			return base.Context.CreateFunctionQuerySingle<CacheOptions>(string.Join("/", from s in uri.Segments.Skip(base.Context.BaseUri.Segments.Length)
				select s.Trim(new char[] { '/' })), "Model.GetCacheOptions", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x0001181C File Offset: 0x0000FA1C
		[OriginalName("SetReportHistorySnapshotsOptions")]
		public DataServiceActionQuery SetReportHistorySnapshotsOptions(ReportHistorySnapshotsOptions ReportHistorySnapshotsOptions)
		{
			EntityDescriptor entityDescriptor = base.Context.EntityTracker.TryGetEntityDescriptor(this);
			if (entityDescriptor == null)
			{
				throw new Exception("cannot find entity");
			}
			return new DataServiceActionQuery(base.Context, entityDescriptor.EditLink.OriginalString.Trim(new char[] { '/' }) + "/Model.SetReportHistorySnapshotsOptions", new BodyOperationParameter[]
			{
				new BodyOperationParameter("ReportHistorySnapshotsOptions", ReportHistorySnapshotsOptions)
			});
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x00011890 File Offset: 0x0000FA90
		[OriginalName("CreateSnapshot")]
		public DataServiceActionQuerySingle<string> CreateSnapshot()
		{
			EntityDescriptor entityDescriptor = base.Context.EntityTracker.TryGetEntityDescriptor(this);
			if (entityDescriptor == null)
			{
				throw new Exception("cannot find entity");
			}
			return new DataServiceActionQuerySingle<string>(base.Context, entityDescriptor.EditLink.OriginalString.Trim(new char[] { '/' }) + "/Model.CreateSnapshot", Array.Empty<BodyOperationParameter>());
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x000118F4 File Offset: 0x0000FAF4
		[OriginalName("DeleteSnapshot")]
		public DataServiceActionQuerySingle<bool> DeleteSnapshot(string HistoryId)
		{
			EntityDescriptor entityDescriptor = base.Context.EntityTracker.TryGetEntityDescriptor(this);
			if (entityDescriptor == null)
			{
				throw new Exception("cannot find entity");
			}
			return new DataServiceActionQuerySingle<bool>(base.Context, entityDescriptor.EditLink.OriginalString.Trim(new char[] { '/' }) + "/Model.DeleteSnapshot", new BodyOperationParameter[]
			{
				new BodyOperationParameter("HistoryId", HistoryId)
			});
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x00011968 File Offset: 0x0000FB68
		[OriginalName("SetCacheOptions")]
		public DataServiceActionQuery SetCacheOptions(CacheOptions cacheOptions)
		{
			EntityDescriptor entityDescriptor = base.Context.EntityTracker.TryGetEntityDescriptor(this);
			if (entityDescriptor == null)
			{
				throw new Exception("cannot find entity");
			}
			return new DataServiceActionQuery(base.Context, entityDescriptor.EditLink.OriginalString.Trim(new char[] { '/' }) + "/Model.SetCacheOptions", new BodyOperationParameter[]
			{
				new BodyOperationParameter("cacheOptions", cacheOptions)
			});
		}

		// Token: 0x04000416 RID: 1046
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _HasParameters;

		// Token: 0x04000417 RID: 1047
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Link;

		// Token: 0x04000418 RID: 1048
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceCollection<Subscription> _Subscriptions = new DataServiceCollection<Subscription>(null, TrackingMode.None);

		// Token: 0x04000419 RID: 1049
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceCollection<CacheRefreshPlan> _CacheRefreshPlans = new DataServiceCollection<CacheRefreshPlan>(null, TrackingMode.None);

		// Token: 0x0400041A RID: 1050
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private HistorySnapshotOptions _HistorySnapshotOptions;

		// Token: 0x0400041B RID: 1051
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceCollection<ReportHistorySnapshot> _ReportHistorySnapshots = new DataServiceCollection<ReportHistorySnapshot>(null, TrackingMode.None);

		// Token: 0x0400041C RID: 1052
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceCollection<HistorySnapshot> _HistorySnapshots = new DataServiceCollection<HistorySnapshot>(null, TrackingMode.None);

		// Token: 0x0400041D RID: 1053
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceCollection<ReportParameterDefinition> _ParameterDefinitions = new DataServiceCollection<ReportParameterDefinition>(null, TrackingMode.None);
	}
}
