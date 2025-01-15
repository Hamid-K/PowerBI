using System;
using System.CodeDom.Compiler;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x02000112 RID: 274
	[Key("Id")]
	[OriginalName("ReportModel")]
	public class ReportModel : DataSource
	{
		// Token: 0x06000BE7 RID: 3047 RVA: 0x000171E8 File Offset: 0x000153E8
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static ReportModel CreateReportModel(Guid ID, CatalogItemType type, bool hidden, long size, DateTimeOffset modifiedDate, DateTimeOffset createdDate, bool isFavorite, bool isEnabled, bool isOriginalConnectionStringExpressionBased, bool isConnectionStringOverridden, CredentialRetrievalType credentialRetrieval, bool isReference, bool hasDataSources)
		{
			return new ReportModel
			{
				Id = ID,
				Type = type,
				Hidden = hidden,
				Size = size,
				ModifiedDate = modifiedDate,
				CreatedDate = createdDate,
				IsFavorite = isFavorite,
				IsEnabled = isEnabled,
				IsOriginalConnectionStringExpressionBased = isOriginalConnectionStringExpressionBased,
				IsConnectionStringOverridden = isConnectionStringOverridden,
				CredentialRetrieval = credentialRetrieval,
				IsReference = isReference,
				HasDataSources = hasDataSources
			};
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06000BE8 RID: 3048 RVA: 0x0001725E File Offset: 0x0001545E
		// (set) Token: 0x06000BE9 RID: 3049 RVA: 0x00017266 File Offset: 0x00015466
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("HasDataSources")]
		public bool HasDataSources
		{
			get
			{
				return this._HasDataSources;
			}
			set
			{
				this._HasDataSources = value;
				this.OnPropertyChanged("HasDataSources");
			}
		}

		// Token: 0x04000568 RID: 1384
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _HasDataSources;
	}
}
