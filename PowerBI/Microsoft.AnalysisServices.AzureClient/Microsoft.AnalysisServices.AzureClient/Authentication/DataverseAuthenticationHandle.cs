using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Microsoft.AnalysisServices.AzureClient.Utilities;

namespace Microsoft.AnalysisServices.AzureClient.Authentication
{
	// Token: 0x0200001E RID: 30
	internal sealed class DataverseAuthenticationHandle : AuthenticationHandle
	{
		// Token: 0x060000D1 RID: 209 RVA: 0x00004126 File Offset: 0x00002326
		public DataverseAuthenticationHandle(AuthenticationHandle baseHandle, string dataverseOrgUrl, string dataverseFriendlyWorkspaceName, string dataverseFriendlyDatasetName)
			: base(AuthenticationEndpoint.Unknown, null, null)
		{
			this.baseHandle = baseHandle;
			this.dataverseOrgUrl = dataverseOrgUrl;
			this.workspaceName = dataverseFriendlyWorkspaceName;
			this.datasetName = dataverseFriendlyDatasetName;
			this.token = this.GetEmbeddedToken();
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x0000415A File Offset: 0x0000235A
		public override string Principal
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00004161 File Offset: 0x00002361
		public override string AuthenticationScheme
		{
			get
			{
				return "EmbedToken";
			}
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004168 File Offset: 0x00002368
		public override string GetAccessToken()
		{
			if (this.GetRefreshByTimeAsFileTime() < DateTimeOffset.Now.ToFileTime())
			{
				this.token = this.GetEmbeddedToken();
			}
			return this.token.EmbedToken;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000041A1 File Offset: 0x000023A1
		public override long GetRefreshByTimeAsFileTime()
		{
			return Math.Min(this.embeddedTokenRefreshBy.ToFileTime(), this.baseHandle.GetRefreshByTimeAsFileTime());
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x000041C0 File Offset: 0x000023C0
		internal DataverseAuthenticationHandle.PowerBIDatasetDetails GetPowerBIDatasetDetails()
		{
			IDictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary.Add("Authorization", string.Format("{0} {1}", this.baseHandle.AuthenticationScheme, this.baseHandle.GetAccessToken()));
			dictionary.Add("x-ms-client-request-id", this.GetClientRequestId(true));
			DataverseAuthenticationHandle.PowerBIDatasetDetails powerBIDatasetDetails;
			try
			{
				string text;
				powerBIDatasetDetails = ConnectivityHelper.ExecuteJsonBasedHttpGetRequest<DataverseAuthenticationHandle.PowerBIDatasetDetails>(new Uri(string.Format(CultureInfo.InvariantCulture, "https://{0}/api/data/v9.0/GetPowerBiDatasetDetails(DatasetName='{1}',WorkspaceName='{2}')", new object[]
				{
					this.dataverseOrgUrl,
					DataverseAuthenticationHandle.GetEncodedStringForOData(this.datasetName),
					DataverseAuthenticationHandle.GetEncodedStringForOData(this.workspaceName)
				})), dictionary, DataverseAuthenticationHandle.defaultRequestOptions, 30000, DataverseAuthenticationHandle.datasetDetailsSerializer, out text);
			}
			catch (WebException ex)
			{
				throw DataverseAuthenticationHandle.ConvertDataverseRequestErrorToConnectionException(AuthenticationSR.DataverseRequest_GetDatasetDetails, ex, dictionary);
			}
			return powerBIDatasetDetails;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00004288 File Offset: 0x00002488
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
				powerBIDatasetEmbedToken = ConnectivityHelper.ExecuteJsonBasedHttpGetRequest<DataverseAuthenticationHandle.PowerBIDatasetEmbedToken>(new Uri(string.Format(CultureInfo.InvariantCulture, "https://{0}/api/data/v9.0/GetPowerBiDatasetEmbedToken(DatasetName='{1}',WorkspaceName='{2}')", new object[]
				{
					this.dataverseOrgUrl,
					DataverseAuthenticationHandle.GetEncodedStringForOData(this.datasetName),
					DataverseAuthenticationHandle.GetEncodedStringForOData(this.workspaceName)
				})), dictionary, DataverseAuthenticationHandle.defaultRequestOptions, 30000, DataverseAuthenticationHandle.embedTokenSerializer, out text);
				dateTimeOffset = DateTimeOffset.Parse(powerBIDatasetEmbedToken.EmbedTokenExpirationTime);
				if (dateTimeOffset.DateTime < DateTime.UtcNow)
				{
					throw new ASAzureAdalWrapperException(AuthenticationSR.Exception_DataverseRequestFailed(AuthenticationSR.DataverseRequest_GetEmbeddedToken, AuthenticationSR.DataverseRequest_UnexpectedResponse, text, Environment.NewLine));
				}
			}
			catch (WebException ex)
			{
				throw DataverseAuthenticationHandle.ConvertDataverseRequestErrorToConnectionException(AuthenticationSR.DataverseRequest_GetEmbeddedToken, ex, dictionary);
			}
			this.embeddedTokenRefreshBy = AuthenticationManager.CalculateAccessTokenRefreshBy(dateTimeOffset);
			return powerBIDatasetEmbedToken;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00004398 File Offset: 0x00002598
		private string GetClientRequestId(bool reuseIfAvailable)
		{
			if (!reuseIfAvailable || string.IsNullOrEmpty(this.clientRequestId))
			{
				this.clientRequestId = Guid.NewGuid().ToString();
			}
			return this.clientRequestId;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000043D4 File Offset: 0x000025D4
		private static string GetEncodedStringForOData(string stringToEscape)
		{
			return Uri.EscapeDataString(stringToEscape.Replace("'", "''"));
		}

		// Token: 0x060000DA RID: 218 RVA: 0x000043EC File Offset: 0x000025EC
		private static Exception ConvertDataverseRequestErrorToConnectionException(string action, WebException ex, IDictionary<string, string> headers)
		{
			string responseFromWebException = AsPaasHelper.GetResponseFromWebException(ex);
			return new ASAzureAdalWrapperException(AuthenticationSR.Exception_DataverseRequestFailed(action, responseFromWebException, AsPaasHelper.GetTechnicalDetailsFromDataverseResponse(ex.Response, headers), Environment.NewLine), ex);
		}

		// Token: 0x04000077 RID: 119
		private const string DataverseGetEmbeddedTokenEndpointFormat = "https://{0}/api/data/v9.0/GetPowerBiDatasetEmbedToken(DatasetName='{1}',WorkspaceName='{2}')";

		// Token: 0x04000078 RID: 120
		private const string DataverseGetDetailsEndpointFormat = "https://{0}/api/data/v9.0/GetPowerBiDatasetDetails(DatasetName='{1}',WorkspaceName='{2}')";

		// Token: 0x04000079 RID: 121
		private const string AuthorizationHeaderFormat = "{0} {1}";

		// Token: 0x0400007A RID: 122
		private const int requestTimeoutInMs = 30000;

		// Token: 0x0400007B RID: 123
		private readonly AuthenticationHandle baseHandle;

		// Token: 0x0400007C RID: 124
		private readonly string dataverseOrgUrl;

		// Token: 0x0400007D RID: 125
		private readonly string datasetName;

		// Token: 0x0400007E RID: 126
		private readonly string workspaceName;

		// Token: 0x0400007F RID: 127
		private static readonly DataContractJsonSerializer embedTokenSerializer = new DataContractJsonSerializer(typeof(DataverseAuthenticationHandle.PowerBIDatasetEmbedToken));

		// Token: 0x04000080 RID: 128
		private static readonly DataContractJsonSerializer datasetDetailsSerializer = new DataContractJsonSerializer(typeof(DataverseAuthenticationHandle.PowerBIDatasetDetails));

		// Token: 0x04000081 RID: 129
		private static readonly ConnectivityHelper.JsonHttpRequestOptions defaultRequestOptions = ConnectivityHelper.JsonHttpRequestOptions.SetContentLength | ConnectivityHelper.JsonHttpRequestOptions.AllowAutoRedirect | ConnectivityHelper.JsonHttpRequestOptions.RetryOnServiceUnavailable | ConnectivityHelper.JsonHttpRequestOptions.GetTechnicalDetails | ConnectivityHelper.JsonHttpRequestOptions.TargetingDataverse;

		// Token: 0x04000082 RID: 130
		private DataverseAuthenticationHandle.PowerBIDatasetEmbedToken token;

		// Token: 0x04000083 RID: 131
		private DateTimeOffset embeddedTokenRefreshBy;

		// Token: 0x04000084 RID: 132
		private string clientRequestId;

		// Token: 0x02000050 RID: 80
		[DataContract]
		private sealed class PowerBIDatasetEmbedToken
		{
			// Token: 0x17000060 RID: 96
			// (get) Token: 0x06000242 RID: 578 RVA: 0x0000B000 File Offset: 0x00009200
			// (set) Token: 0x06000243 RID: 579 RVA: 0x0000B008 File Offset: 0x00009208
			[DataMember(Name = "EmbedToken")]
			public string EmbedToken { get; set; }

			// Token: 0x17000061 RID: 97
			// (get) Token: 0x06000244 RID: 580 RVA: 0x0000B011 File Offset: 0x00009211
			// (set) Token: 0x06000245 RID: 581 RVA: 0x0000B019 File Offset: 0x00009219
			[DataMember(Name = "EmbedTokenExpirationTime")]
			public string EmbedTokenExpirationTime { get; set; }
		}

		// Token: 0x02000051 RID: 81
		[DataContract]
		internal sealed class PowerBIDatasetDetails
		{
			// Token: 0x17000062 RID: 98
			// (get) Token: 0x06000247 RID: 583 RVA: 0x0000B02A File Offset: 0x0000922A
			// (set) Token: 0x06000248 RID: 584 RVA: 0x0000B032 File Offset: 0x00009232
			[DataMember(Name = "PowerBiDatasetName")]
			public string PowerBIDatasetName { get; set; }

			// Token: 0x17000063 RID: 99
			// (get) Token: 0x06000249 RID: 585 RVA: 0x0000B03B File Offset: 0x0000923B
			// (set) Token: 0x0600024A RID: 586 RVA: 0x0000B043 File Offset: 0x00009243
			[DataMember(Name = "PowerBiWorkspaceName")]
			public string PowerBIWorkspaceName { get; set; }
		}
	}
}
