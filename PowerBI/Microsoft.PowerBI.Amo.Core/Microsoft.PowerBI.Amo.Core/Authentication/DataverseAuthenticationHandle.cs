using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Microsoft.AnalysisServices.Utilities;

namespace Microsoft.AnalysisServices.Authentication
{
	// Token: 0x020000FA RID: 250
	internal sealed class DataverseAuthenticationHandle : AuthenticationHandle
	{
		// Token: 0x06000F9D RID: 3997 RVA: 0x00035972 File Offset: 0x00033B72
		public DataverseAuthenticationHandle(AuthenticationHandle baseHandle, string dataverseOrgUrl, string dataverseFriendlyWorkspaceName, string dataverseFriendlyDatasetName)
			: base(AuthenticationEndpoint.Unknown, null, null)
		{
			this.baseHandle = baseHandle;
			this.dataverseOrgUrl = dataverseOrgUrl;
			this.workspaceName = dataverseFriendlyWorkspaceName;
			this.datasetName = dataverseFriendlyDatasetName;
			this.token = this.GetEmbeddedToken();
		}

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x06000F9E RID: 3998 RVA: 0x000359A6 File Offset: 0x00033BA6
		public override string Principal
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x06000F9F RID: 3999 RVA: 0x000359AD File Offset: 0x00033BAD
		public override string AuthenticationScheme
		{
			get
			{
				return "EmbedToken";
			}
		}

		// Token: 0x06000FA0 RID: 4000 RVA: 0x000359B4 File Offset: 0x00033BB4
		public override string GetAccessToken()
		{
			if (this.GetRefreshByTimeAsFileTime() < DateTimeOffset.Now.ToFileTime())
			{
				this.token = this.GetEmbeddedToken();
			}
			return this.token.EmbedToken;
		}

		// Token: 0x06000FA1 RID: 4001 RVA: 0x000359ED File Offset: 0x00033BED
		public override long GetRefreshByTimeAsFileTime()
		{
			return Math.Min(this.embeddedTokenRefreshBy.ToFileTime(), this.baseHandle.GetRefreshByTimeAsFileTime());
		}

		// Token: 0x06000FA2 RID: 4002 RVA: 0x00035A0C File Offset: 0x00033C0C
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

		// Token: 0x06000FA3 RID: 4003 RVA: 0x00035AC8 File Offset: 0x00033CC8
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
					throw new ConnectionException(AuthenticationSR.Exception_DataverseRequestFailed(AuthenticationSR.DataverseRequest_GetEmbeddedToken, AuthenticationSR.DataverseRequest_UnexpectedResponse, text, Environment.NewLine));
				}
			}
			catch (WebException ex)
			{
				throw DataverseAuthenticationHandle.ConvertDataverseRequestErrorToConnectionException(AuthenticationSR.DataverseRequest_GetEmbeddedToken, ex, dictionary);
			}
			this.embeddedTokenRefreshBy = AuthenticationManager.CalculateAccessTokenRefreshBy(dateTimeOffset);
			return powerBIDatasetEmbedToken;
		}

		// Token: 0x06000FA4 RID: 4004 RVA: 0x00035BCC File Offset: 0x00033DCC
		private string GetClientRequestId(bool reuseIfAvailable)
		{
			if (!reuseIfAvailable || string.IsNullOrEmpty(this.clientRequestId))
			{
				this.clientRequestId = Guid.NewGuid().ToString();
			}
			return this.clientRequestId;
		}

		// Token: 0x06000FA5 RID: 4005 RVA: 0x00035C08 File Offset: 0x00033E08
		private static string GetEncodedStringForOData(string stringToEscape)
		{
			return Uri.EscapeDataString(stringToEscape.Replace("'", "''"));
		}

		// Token: 0x06000FA6 RID: 4006 RVA: 0x00035C20 File Offset: 0x00033E20
		private static Exception ConvertDataverseRequestErrorToConnectionException(string action, WebException ex, IDictionary<string, string> headers)
		{
			string responseFromWebException = AsPaasHelper.GetResponseFromWebException(ex);
			return new ConnectionException(AuthenticationSR.Exception_DataverseRequestFailed(action, responseFromWebException, AsPaasHelper.GetTechnicalDetailsFromDataverseResponse(ex.Response, headers), Environment.NewLine), ex);
		}

		// Token: 0x04000860 RID: 2144
		private const string DataverseGetEmbeddedTokenEndpointFormat = "https://{0}/api/data/v9.0/GetPowerBiDatasetEmbedToken(DatasetName='{1}',WorkspaceName='{2}')";

		// Token: 0x04000861 RID: 2145
		private const string DataverseGetDetailsEndpointFormat = "https://{0}/api/data/v9.0/GetPowerBiDatasetDetails(DatasetName='{1}',WorkspaceName='{2}')";

		// Token: 0x04000862 RID: 2146
		private const string AuthorizationHeaderFormat = "{0} {1}";

		// Token: 0x04000863 RID: 2147
		private const int requestTimeoutInMs = 30000;

		// Token: 0x04000864 RID: 2148
		private readonly AuthenticationHandle baseHandle;

		// Token: 0x04000865 RID: 2149
		private readonly string dataverseOrgUrl;

		// Token: 0x04000866 RID: 2150
		private readonly string datasetName;

		// Token: 0x04000867 RID: 2151
		private readonly string workspaceName;

		// Token: 0x04000868 RID: 2152
		private static readonly DataContractJsonSerializer embedTokenSerializer = new DataContractJsonSerializer(typeof(DataverseAuthenticationHandle.PowerBIDatasetEmbedToken));

		// Token: 0x04000869 RID: 2153
		private static readonly DataContractJsonSerializer datasetDetailsSerializer = new DataContractJsonSerializer(typeof(DataverseAuthenticationHandle.PowerBIDatasetDetails));

		// Token: 0x0400086A RID: 2154
		private static readonly ConnectivityHelper.JsonHttpRequestOptions defaultRequestOptions = ConnectivityHelper.JsonHttpRequestOptions.SetContentLength | ConnectivityHelper.JsonHttpRequestOptions.AllowAutoRedirect | ConnectivityHelper.JsonHttpRequestOptions.RetryOnServiceUnavailable | ConnectivityHelper.JsonHttpRequestOptions.GetTechnicalDetails | ConnectivityHelper.JsonHttpRequestOptions.TargetingDataverse;

		// Token: 0x0400086B RID: 2155
		private DataverseAuthenticationHandle.PowerBIDatasetEmbedToken token;

		// Token: 0x0400086C RID: 2156
		private DateTimeOffset embeddedTokenRefreshBy;

		// Token: 0x0400086D RID: 2157
		private string clientRequestId;

		// Token: 0x020001B3 RID: 435
		[DataContract]
		private sealed class PowerBIDatasetEmbedToken
		{
			// Token: 0x1700063B RID: 1595
			// (get) Token: 0x0600135E RID: 4958 RVA: 0x0004390C File Offset: 0x00041B0C
			// (set) Token: 0x0600135F RID: 4959 RVA: 0x00043914 File Offset: 0x00041B14
			[DataMember(Name = "EmbedToken")]
			public string EmbedToken { get; set; }

			// Token: 0x1700063C RID: 1596
			// (get) Token: 0x06001360 RID: 4960 RVA: 0x0004391D File Offset: 0x00041B1D
			// (set) Token: 0x06001361 RID: 4961 RVA: 0x00043925 File Offset: 0x00041B25
			[DataMember(Name = "EmbedTokenExpirationTime")]
			public string EmbedTokenExpirationTime { get; set; }
		}

		// Token: 0x020001B4 RID: 436
		[DataContract]
		internal sealed class PowerBIDatasetDetails
		{
			// Token: 0x1700063D RID: 1597
			// (get) Token: 0x06001363 RID: 4963 RVA: 0x00043936 File Offset: 0x00041B36
			// (set) Token: 0x06001364 RID: 4964 RVA: 0x0004393E File Offset: 0x00041B3E
			[DataMember(Name = "PowerBiDatasetName")]
			public string PowerBIDatasetName { get; set; }

			// Token: 0x1700063E RID: 1598
			// (get) Token: 0x06001365 RID: 4965 RVA: 0x00043947 File Offset: 0x00041B47
			// (set) Token: 0x06001366 RID: 4966 RVA: 0x0004394F File Offset: 0x00041B4F
			[DataMember(Name = "PowerBiWorkspaceName")]
			public string PowerBIWorkspaceName { get; set; }
		}
	}
}
