using System;
using System.CodeDom.Compiler;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000073 RID: 115
	[Key("Id")]
	[OriginalName("ReportModel")]
	public class ReportModel : DataSource
	{
		// Token: 0x06000520 RID: 1312 RVA: 0x0000A8C0 File Offset: 0x00008AC0
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

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000521 RID: 1313 RVA: 0x0000A936 File Offset: 0x00008B36
		// (set) Token: 0x06000522 RID: 1314 RVA: 0x0000A93E File Offset: 0x00008B3E
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

		// Token: 0x0400025A RID: 602
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _HasDataSources;
	}
}
