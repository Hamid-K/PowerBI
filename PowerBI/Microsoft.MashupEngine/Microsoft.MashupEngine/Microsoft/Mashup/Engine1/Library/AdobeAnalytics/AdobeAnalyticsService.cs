using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Http;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.AdobeAnalytics
{
	// Token: 0x02000F84 RID: 3972
	internal abstract class AdobeAnalyticsService
	{
		// Token: 0x0600689D RID: 26781 RVA: 0x001678AB File Offset: 0x00165AAB
		public AdobeAnalyticsService(IEngineHost host, AdobeAnalyticsOptions options)
		{
			this.host = host;
			this.options = options;
		}

		// Token: 0x17001E3A RID: 7738
		// (get) Token: 0x0600689E RID: 26782 RVA: 0x001678D0 File Offset: 0x00165AD0
		public string ClientId
		{
			get
			{
				if (this.clientId == null)
				{
					this.clientId = this.GetClientId();
				}
				return this.clientId;
			}
		}

		// Token: 0x17001E3B RID: 7739
		// (get) Token: 0x0600689F RID: 26783 RVA: 0x001678EC File Offset: 0x00165AEC
		public IEngineHost EngineHost
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x060068A0 RID: 26784
		public abstract IList<AdobeAnalyticsCube> GetCubes(AdobeAnalyticsCompany company);

		// Token: 0x060068A1 RID: 26785
		public abstract IList<AdobeAnalyticsDimension> GetDimensions(AdobeAnalyticsCube cube);

		// Token: 0x060068A2 RID: 26786
		public abstract IList<AdobeAnalyticsMeasure> GetMeasures(AdobeAnalyticsCube cube);

		// Token: 0x060068A3 RID: 26787
		public abstract IList<AdobeAnalyticsSegment> GetSegments(AdobeAnalyticsCube cube);

		// Token: 0x060068A4 RID: 26788
		protected abstract Value ExecuteRequest(AdobeAnalyticsRequest request);

		// Token: 0x060068A5 RID: 26789 RVA: 0x001678F4 File Offset: 0x00165AF4
		public virtual IEnumerable<AdobeAnalyticsCompany> GetCompanies()
		{
			string text;
			if (!(this.GetCredentials()[0] as OAuthCredential).Properties.TryGetValue("ClientId", out text))
			{
				throw DataSourceException.NewInvalidCredentialsError(this.host, AdobeAnalyticsService.Resource, Strings.AdobeCredentialDeprecated, null, null);
			}
			Value value = this.ExecuteRequest(AdobeAnalyticsRequest.NewDiscoveryRequest(text));
			ListValue asList = value["imsOrgs"].AsList;
			if (asList.IsEmpty)
			{
				throw ValueException.NewDataSourceError<Message0>(Strings.AdobeAnalyticsOrgCompaniesNotFound, value, null);
			}
			foreach (IValueReference valueReference in asList)
			{
				ListValue asList2 = ((Value)valueReference)["companies"].AsList;
				foreach (IValueReference valueReference2 in asList2)
				{
					Value value2 = (Value)valueReference2;
					if (value2["globalCompanyId"].IsNull)
					{
						throw ValueException.NewDataSourceError<Message0>(Strings.AdobeAnalyticsGlobalCompanyIdIsNull, value2, null);
					}
					yield return new AdobeAnalyticsCompany(value2["globalCompanyId"].AsString, value2["companyName"].AsString.ToLowerInvariant(), this);
				}
				IEnumerator<IValueReference> enumerator2 = null;
			}
			IEnumerator<IValueReference> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060068A6 RID: 26790 RVA: 0x00167904 File Offset: 0x00165B04
		protected ResourceCredentialCollection GetCredentials()
		{
			ResourceCredentialCollection resourceCredentialCollection;
			HttpServices.VerifyPermissionAndGetCredentials(this.host, AdobeAnalyticsService.Resource, out resourceCredentialCollection);
			if (resourceCredentialCollection.Count != 1 || !(resourceCredentialCollection[0] is OAuthCredential))
			{
				throw DataSourceException.NewInvalidCredentialsError(this.host, AdobeAnalyticsService.Resource, null, null, null);
			}
			return resourceCredentialCollection;
		}

		// Token: 0x060068A7 RID: 26791 RVA: 0x0016794F File Offset: 0x00165B4F
		private string GetClientId()
		{
			return ((OAuthCredential)this.GetCredentials()[0]).Properties["ClientId"];
		}

		// Token: 0x0400399B RID: 14747
		public const string CompanyKey = "org_companies";

		// Token: 0x0400399C RID: 14748
		public const string ReportNotReadyStatusCode = "report_not_ready";

		// Token: 0x0400399D RID: 14749
		public const string ErrorDescriptionKey = "error_description";

		// Token: 0x0400399E RID: 14750
		public const string ErrorUriKey = "error_uri";

		// Token: 0x0400399F RID: 14751
		public const string ImsOrgsKey = "imsOrgs";

		// Token: 0x040039A0 RID: 14752
		public const string CompaniesKey = "companies";

		// Token: 0x040039A1 RID: 14753
		public const string GlobalCompanyIdKey = "globalCompanyId";

		// Token: 0x040039A2 RID: 14754
		public const string CompanyNameKey = "companyName";

		// Token: 0x040039A3 RID: 14755
		public const string AllowedForReportingKey = "allowedForReporting";

		// Token: 0x040039A4 RID: 14756
		private string clientId;

		// Token: 0x040039A5 RID: 14757
		protected readonly IEngineHost host;

		// Token: 0x040039A6 RID: 14758
		protected readonly RetryPolicy retryPolicy = new RetryPolicy(10, 10);

		// Token: 0x040039A7 RID: 14759
		protected readonly AdobeAnalyticsOptions options;

		// Token: 0x040039A8 RID: 14760
		public static readonly IResource Resource = Microsoft.Mashup.Engine1.Library.Resource.New("AdobeAnalytics", "AdobeAnalytics");
	}
}
