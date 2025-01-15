using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Microsoft.AnalysisServices.AdomdClient.Utilities;

namespace Microsoft.AnalysisServices.AdomdClient.Authentication
{
	// Token: 0x02000105 RID: 261
	internal sealed class DataverseAuthenticationHandle : AuthenticationHandle
	{
		// Token: 0x06000F0E RID: 3854 RVA: 0x00033056 File Offset: 0x00031256
		public DataverseAuthenticationHandle(AuthenticationHandle baseHandle, string dataverseOrgUrl, string dataverseFriendlyWorkspaceName, string dataverseFriendlyDatasetName)
			: base(AuthenticationEndpoint.Unknown, null, null)
		{
			this.baseHandle = baseHandle;
			this.dataverseOrgUrl = dataverseOrgUrl;
			this.workspaceName = dataverseFriendlyWorkspaceName;
			this.datasetName = dataverseFriendlyDatasetName;
			this.token = this.GetEmbeddedToken();
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x06000F0F RID: 3855 RVA: 0x0003308A File Offset: 0x0003128A
		public override string Principal
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x06000F10 RID: 3856 RVA: 0x00033091 File Offset: 0x00031291
		public override string AuthenticationScheme
		{
			get
			{
				return "EmbedToken";
			}
		}

		// Token: 0x06000F11 RID: 3857 RVA: 0x00033098 File Offset: 0x00031298
		public override string GetAccessToken()
		{
			if (this.GetRefreshByTimeAsFileTime() < DateTimeOffset.Now.ToFileTime())
			{
				this.token = this.GetEmbeddedToken();
			}
			return this.token.EmbedToken;
		}

		// Token: 0x06000F12 RID: 3858 RVA: 0x000330D1 File Offset: 0x000312D1
		public override long GetRefreshByTimeAsFileTime()
		{
			return Math.Min(this.embeddedTokenRefreshBy.ToFileTime(), this.baseHandle.GetRefreshByTimeAsFileTime());
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x000330F0 File Offset: 0x000312F0
		internal DataverseAuthenticationHandle.PowerBIDatasetDetails GetPowerBIDatasetDetails()
		{
			IDictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary.Add("Authorization", string.Format("{0} {1}", this.baseHandle.AuthenticationScheme, this.baseHandle.GetAccessToken()));
			dictionary.Add("x-ms-client-request-id", this.GetClientRequestId(true));
			DataverseAuthenticationHandle.PowerBIDatasetDetails powerBIDatasetDetails;
			try
			{
				string text;
				powerBIDatasetDetails = ConnectivityHelper.ExecuteJsonBasedHttpGetRequest<DataverseAuthenticationHandle.PowerBIDatasetDetails>(new Uri(string.Format(CultureInfo.InvariantCulture, "https://{0}/api/data/v9.0/GetPowerBiDatasetDetails(DatasetName='{1}',WorkspaceName='{2}')", this.dataverseOrgUrl, DataverseAuthenticationHandle.GetEncodedStringForOData(this.datasetName), DataverseAuthenticationHandle.GetEncodedStringForOData(this.workspaceName))), dictionary, DataverseAuthenticationHandle.defaultRequestOptions, 30000, DataverseAuthenticationHandle.datasetDetailsSerializer, out text);
			}
			catch (WebException ex)
			{
				throw DataverseAuthenticationHandle.ConvertDataverseRequestErrorToConnectionException(AuthenticationSR.DataverseRequest_GetDatasetDetails, ex, dictionary);
			}
			return powerBIDatasetDetails;
		}

		// Token: 0x06000F14 RID: 3860 RVA: 0x000331AC File Offset: 0x000313AC
		private DataverseAuthenticationHandle.PowerBIDatasetEmbedToken GetEmbeddedToken()
		{
			IDictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary.Add("Authorization", string.Format("{0} {1}", this.baseHandle.AuthenticationScheme, this.baseHandle.GetAccessToken()));
			dictionary.Add("x-ms-client-request-id", this.GetClientRequestId(false));
			DataverseAuthenticationHandle.PowerBIDatasetEmbedToken powerBIDatasetEmbedToken;
			DateTimeOffset dateTimeOffset;
			try
			{
				string text;
				powerBIDatasetEmbedToken = ConnectivityHelper.ExecuteJsonBasedHttpGetRequest<DataverseAuthenticationHandle.PowerBIDatasetEmbedToken>(new Uri(string.Format(CultureInfo.InvariantCulture, "https://{0}/api/data/v9.0/GetPowerBiDatasetEmbedToken(DatasetName='{1}',WorkspaceName='{2}')", this.dataverseOrgUrl, DataverseAuthenticationHandle.GetEncodedStringForOData(this.datasetName), DataverseAuthenticationHandle.GetEncodedStringForOData(this.workspaceName))), dictionary, DataverseAuthenticationHandle.defaultRequestOptions, 30000, DataverseAuthenticationHandle.embedTokenSerializer, out text);
				dateTimeOffset = DateTimeOffset.Parse(powerBIDatasetEmbedToken.EmbedTokenExpirationTime);
				if (dateTimeOffset.DateTime < DateTime.UtcNow)
				{
					throw new AdomdConnectionException(AuthenticationSR.Exception_DataverseRequestFailed(AuthenticationSR.DataverseRequest_GetEmbeddedToken, AuthenticationSR.DataverseRequest_UnexpectedResponse, text, Environment.NewLine), null);
				}
			}
			catch (WebException ex)
			{
				throw DataverseAuthenticationHandle.ConvertDataverseRequestErrorToConnectionException(AuthenticationSR.DataverseRequest_GetEmbeddedToken, ex, dictionary);
			}
			this.embeddedTokenRefreshBy = AuthenticationManager.CalculateAccessTokenRefreshBy(dateTimeOffset);
			return powerBIDatasetEmbedToken;
		}

		// Token: 0x06000F15 RID: 3861 RVA: 0x000332B8 File Offset: 0x000314B8
		private string GetClientRequestId(bool reuseIfAvailable)
		{
			if (!reuseIfAvailable || string.IsNullOrEmpty(this.clientRequestId))
			{
				this.clientRequestId = Guid.NewGuid().ToString();
			}
			return this.clientRequestId;
		}

		// Token: 0x06000F16 RID: 3862 RVA: 0x000332F4 File Offset: 0x000314F4
		private static string GetEncodedStringForOData(string stringToEscape)
		{
			return Uri.EscapeDataString(stringToEscape.Replace("'", "''"));
		}

		// Token: 0x06000F17 RID: 3863 RVA: 0x0003330C File Offset: 0x0003150C
		private static Exception ConvertDataverseRequestErrorToConnectionException(string action, WebException ex, IDictionary<string, string> headers)
		{
			string responseFromWebException = AsPaasHelper.GetResponseFromWebException(ex);
			return new AdomdConnectionException(AuthenticationSR.Exception_DataverseRequestFailed(action, responseFromWebException, AsPaasHelper.GetTechnicalDetailsFromDataverseResponse(ex.Response, headers), Environment.NewLine), ex);
		}

		// Token: 0x040008A7 RID: 2215
		private const string DataverseGetEmbeddedTokenEndpointFormat = "https://{0}/api/data/v9.0/GetPowerBiDatasetEmbedToken(DatasetName='{1}',WorkspaceName='{2}')";

		// Token: 0x040008A8 RID: 2216
		private const string DataverseGetDetailsEndpointFormat = "https://{0}/api/data/v9.0/GetPowerBiDatasetDetails(DatasetName='{1}',WorkspaceName='{2}')";

		// Token: 0x040008A9 RID: 2217
		private const string AuthorizationHeaderFormat = "{0} {1}";

		// Token: 0x040008AA RID: 2218
		private const int requestTimeoutInMs = 30000;

		// Token: 0x040008AB RID: 2219
		private readonly AuthenticationHandle baseHandle;

		// Token: 0x040008AC RID: 2220
		private readonly string dataverseOrgUrl;

		// Token: 0x040008AD RID: 2221
		private readonly string datasetName;

		// Token: 0x040008AE RID: 2222
		private readonly string workspaceName;

		// Token: 0x040008AF RID: 2223
		private static readonly DataContractJsonSerializer embedTokenSerializer = new DataContractJsonSerializer(typeof(DataverseAuthenticationHandle.PowerBIDatasetEmbedToken));

		// Token: 0x040008B0 RID: 2224
		private static readonly DataContractJsonSerializer datasetDetailsSerializer = new DataContractJsonSerializer(typeof(DataverseAuthenticationHandle.PowerBIDatasetDetails));

		// Token: 0x040008B1 RID: 2225
		private static readonly ConnectivityHelper.JsonHttpRequestOptions defaultRequestOptions = ConnectivityHelper.JsonHttpRequestOptions.SetContentLength | ConnectivityHelper.JsonHttpRequestOptions.AllowAutoRedirect | ConnectivityHelper.JsonHttpRequestOptions.RetryOnServiceUnavailable | ConnectivityHelper.JsonHttpRequestOptions.GetTechnicalDetails | ConnectivityHelper.JsonHttpRequestOptions.TargetingDataverse;

		// Token: 0x040008B2 RID: 2226
		private DataverseAuthenticationHandle.PowerBIDatasetEmbedToken token;

		// Token: 0x040008B3 RID: 2227
		private DateTimeOffset embeddedTokenRefreshBy;

		// Token: 0x040008B4 RID: 2228
		private string clientRequestId;

		// Token: 0x020001D6 RID: 470
		[DataContract]
		private sealed class PowerBIDatasetEmbedToken
		{
			// Token: 0x170006F6 RID: 1782
			// (get) Token: 0x06001402 RID: 5122 RVA: 0x000456C0 File Offset: 0x000438C0
			// (set) Token: 0x06001403 RID: 5123 RVA: 0x000456C8 File Offset: 0x000438C8
			[DataMember(Name = "EmbedToken")]
			public string EmbedToken { get; set; }

			// Token: 0x170006F7 RID: 1783
			// (get) Token: 0x06001404 RID: 5124 RVA: 0x000456D1 File Offset: 0x000438D1
			// (set) Token: 0x06001405 RID: 5125 RVA: 0x000456D9 File Offset: 0x000438D9
			[DataMember(Name = "EmbedTokenExpirationTime")]
			public string EmbedTokenExpirationTime { get; set; }
		}

		// Token: 0x020001D7 RID: 471
		[DataContract]
		internal sealed class PowerBIDatasetDetails
		{
			// Token: 0x170006F8 RID: 1784
			// (get) Token: 0x06001407 RID: 5127 RVA: 0x000456EA File Offset: 0x000438EA
			// (set) Token: 0x06001408 RID: 5128 RVA: 0x000456F2 File Offset: 0x000438F2
			[DataMember(Name = "PowerBiDatasetName")]
			public string PowerBIDatasetName { get; set; }

			// Token: 0x170006F9 RID: 1785
			// (get) Token: 0x06001409 RID: 5129 RVA: 0x000456FB File Offset: 0x000438FB
			// (set) Token: 0x0600140A RID: 5130 RVA: 0x00045703 File Offset: 0x00043903
			[DataMember(Name = "PowerBiWorkspaceName")]
			public string PowerBIWorkspaceName { get; set; }
		}
	}
}
