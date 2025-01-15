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
		// Token: 0x06000F01 RID: 3841 RVA: 0x00032D26 File Offset: 0x00030F26
		public DataverseAuthenticationHandle(AuthenticationHandle baseHandle, string dataverseOrgUrl, string dataverseFriendlyWorkspaceName, string dataverseFriendlyDatasetName)
			: base(AuthenticationEndpoint.Unknown, null, null)
		{
			this.baseHandle = baseHandle;
			this.dataverseOrgUrl = dataverseOrgUrl;
			this.workspaceName = dataverseFriendlyWorkspaceName;
			this.datasetName = dataverseFriendlyDatasetName;
			this.token = this.GetEmbeddedToken();
		}

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x06000F02 RID: 3842 RVA: 0x00032D5A File Offset: 0x00030F5A
		public override string Principal
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x06000F03 RID: 3843 RVA: 0x00032D61 File Offset: 0x00030F61
		public override string AuthenticationScheme
		{
			get
			{
				return "EmbedToken";
			}
		}

		// Token: 0x06000F04 RID: 3844 RVA: 0x00032D68 File Offset: 0x00030F68
		public override string GetAccessToken()
		{
			if (this.GetRefreshByTimeAsFileTime() < DateTimeOffset.Now.ToFileTime())
			{
				this.token = this.GetEmbeddedToken();
			}
			return this.token.EmbedToken;
		}

		// Token: 0x06000F05 RID: 3845 RVA: 0x00032DA1 File Offset: 0x00030FA1
		public override long GetRefreshByTimeAsFileTime()
		{
			return Math.Min(this.embeddedTokenRefreshBy.ToFileTime(), this.baseHandle.GetRefreshByTimeAsFileTime());
		}

		// Token: 0x06000F06 RID: 3846 RVA: 0x00032DC0 File Offset: 0x00030FC0
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

		// Token: 0x06000F07 RID: 3847 RVA: 0x00032E7C File Offset: 0x0003107C
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

		// Token: 0x06000F08 RID: 3848 RVA: 0x00032F88 File Offset: 0x00031188
		private string GetClientRequestId(bool reuseIfAvailable)
		{
			if (!reuseIfAvailable || string.IsNullOrEmpty(this.clientRequestId))
			{
				this.clientRequestId = Guid.NewGuid().ToString();
			}
			return this.clientRequestId;
		}

		// Token: 0x06000F09 RID: 3849 RVA: 0x00032FC4 File Offset: 0x000311C4
		private static string GetEncodedStringForOData(string stringToEscape)
		{
			return Uri.EscapeDataString(stringToEscape.Replace("'", "''"));
		}

		// Token: 0x06000F0A RID: 3850 RVA: 0x00032FDC File Offset: 0x000311DC
		private static Exception ConvertDataverseRequestErrorToConnectionException(string action, WebException ex, IDictionary<string, string> headers)
		{
			string responseFromWebException = AsPaasHelper.GetResponseFromWebException(ex);
			return new AdomdConnectionException(AuthenticationSR.Exception_DataverseRequestFailed(action, responseFromWebException, AsPaasHelper.GetTechnicalDetailsFromDataverseResponse(ex.Response, headers), Environment.NewLine), ex);
		}

		// Token: 0x0400089A RID: 2202
		private const string DataverseGetEmbeddedTokenEndpointFormat = "https://{0}/api/data/v9.0/GetPowerBiDatasetEmbedToken(DatasetName='{1}',WorkspaceName='{2}')";

		// Token: 0x0400089B RID: 2203
		private const string DataverseGetDetailsEndpointFormat = "https://{0}/api/data/v9.0/GetPowerBiDatasetDetails(DatasetName='{1}',WorkspaceName='{2}')";

		// Token: 0x0400089C RID: 2204
		private const string AuthorizationHeaderFormat = "{0} {1}";

		// Token: 0x0400089D RID: 2205
		private const int requestTimeoutInMs = 30000;

		// Token: 0x0400089E RID: 2206
		private readonly AuthenticationHandle baseHandle;

		// Token: 0x0400089F RID: 2207
		private readonly string dataverseOrgUrl;

		// Token: 0x040008A0 RID: 2208
		private readonly string datasetName;

		// Token: 0x040008A1 RID: 2209
		private readonly string workspaceName;

		// Token: 0x040008A2 RID: 2210
		private static readonly DataContractJsonSerializer embedTokenSerializer = new DataContractJsonSerializer(typeof(DataverseAuthenticationHandle.PowerBIDatasetEmbedToken));

		// Token: 0x040008A3 RID: 2211
		private static readonly DataContractJsonSerializer datasetDetailsSerializer = new DataContractJsonSerializer(typeof(DataverseAuthenticationHandle.PowerBIDatasetDetails));

		// Token: 0x040008A4 RID: 2212
		private static readonly ConnectivityHelper.JsonHttpRequestOptions defaultRequestOptions = ConnectivityHelper.JsonHttpRequestOptions.SetContentLength | ConnectivityHelper.JsonHttpRequestOptions.AllowAutoRedirect | ConnectivityHelper.JsonHttpRequestOptions.RetryOnServiceUnavailable | ConnectivityHelper.JsonHttpRequestOptions.GetTechnicalDetails | ConnectivityHelper.JsonHttpRequestOptions.TargetingDataverse;

		// Token: 0x040008A5 RID: 2213
		private DataverseAuthenticationHandle.PowerBIDatasetEmbedToken token;

		// Token: 0x040008A6 RID: 2214
		private DateTimeOffset embeddedTokenRefreshBy;

		// Token: 0x040008A7 RID: 2215
		private string clientRequestId;

		// Token: 0x020001D6 RID: 470
		[DataContract]
		private sealed class PowerBIDatasetEmbedToken
		{
			// Token: 0x170006F0 RID: 1776
			// (get) Token: 0x060013F5 RID: 5109 RVA: 0x00045184 File Offset: 0x00043384
			// (set) Token: 0x060013F6 RID: 5110 RVA: 0x0004518C File Offset: 0x0004338C
			[DataMember(Name = "EmbedToken")]
			public string EmbedToken { get; set; }

			// Token: 0x170006F1 RID: 1777
			// (get) Token: 0x060013F7 RID: 5111 RVA: 0x00045195 File Offset: 0x00043395
			// (set) Token: 0x060013F8 RID: 5112 RVA: 0x0004519D File Offset: 0x0004339D
			[DataMember(Name = "EmbedTokenExpirationTime")]
			public string EmbedTokenExpirationTime { get; set; }
		}

		// Token: 0x020001D7 RID: 471
		[DataContract]
		internal sealed class PowerBIDatasetDetails
		{
			// Token: 0x170006F2 RID: 1778
			// (get) Token: 0x060013FA RID: 5114 RVA: 0x000451AE File Offset: 0x000433AE
			// (set) Token: 0x060013FB RID: 5115 RVA: 0x000451B6 File Offset: 0x000433B6
			[DataMember(Name = "PowerBiDatasetName")]
			public string PowerBIDatasetName { get; set; }

			// Token: 0x170006F3 RID: 1779
			// (get) Token: 0x060013FC RID: 5116 RVA: 0x000451BF File Offset: 0x000433BF
			// (set) Token: 0x060013FD RID: 5117 RVA: 0x000451C7 File Offset: 0x000433C7
			[DataMember(Name = "PowerBiWorkspaceName")]
			public string PowerBIWorkspaceName { get; set; }
		}
	}
}
