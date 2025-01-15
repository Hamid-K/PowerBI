using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Http;
using Microsoft.Mashup.Engine1.Library.Json;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.GoogleAnalytics
{
	// Token: 0x02000B2C RID: 2860
	internal class GoogleAnalyticsServiceV1 : IGoogleAnalyticsService
	{
		// Token: 0x06004F56 RID: 20310 RVA: 0x001091B8 File Offset: 0x001073B8
		public GoogleAnalyticsServiceV1(IEngineHost host)
		{
			this.host = host;
			this.serverUrl = this.host.Hook(() => new GoogleAnalyticsServerUrl()).V1ServerBaseUrl;
		}

		// Token: 0x170018C5 RID: 6341
		// (get) Token: 0x06004F57 RID: 20311 RVA: 0x00109207 File Offset: 0x00107407
		public IEngineHost Host
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x06004F58 RID: 20312 RVA: 0x00109210 File Offset: 0x00107410
		public Value GetReport(string viewId, GoogleAnalyticsQueryExpression compiledExpression)
		{
			if (compiledExpression.Measures.Count == 0)
			{
				return RecordValue.New(Keys.New("reports"), new Value[] { ListValue.New(new Value[] { RecordValue.New(Keys.New("data"), new Value[] { RecordValue.Empty }) }) });
			}
			return this.ExecuteRequest(new UriBuilder(compiledExpression.Uri), compiledExpression.Body);
		}

		// Token: 0x06004F59 RID: 20313 RVA: 0x00109288 File Offset: 0x00107488
		private Value ExecuteRequest(UriBuilder uri, RecordValue body = null)
		{
			ResourceCredentialCollection resourceCredentialCollection;
			HttpServices.VerifyPermissionAndGetCredentials(this.host, GoogleAnalyticsServiceV1.resource, out resourceCredentialCollection);
			if (resourceCredentialCollection.Count != 1 || !(resourceCredentialCollection[0] is OAuthCredential))
			{
				throw DataSourceException.NewInvalidCredentialsError(this.host, GoogleAnalyticsServiceV1.resource, null, null, null);
			}
			OAuthCredential oauthCredential = (OAuthCredential)resourceCredentialCollection[0];
			uri.Scheme = this.serverUrl.Scheme;
			uri.Host = this.serverUrl.Host;
			uri.Port = this.serverUrl.Port;
			string text;
			if (oauthCredential.Properties.TryGetValue("gmail", out text))
			{
				string text2 = "quotaUser=" + Uri.EscapeDataString(text);
				if (uri.Query != null && uri.Query.Length > 1)
				{
					uri.Query = uri.Query.Substring(1) + "&" + text2;
				}
				else
				{
					uri.Query = text2;
				}
			}
			BinaryValue binaryValue = null;
			if (body != null)
			{
				binaryValue = JsonModule.Json.FromValue.Invoke(body).AsBinary;
			}
			GoogleAnalyticsRequestPolicy googleAnalyticsRequestPolicy = new GoogleAnalyticsRequestPolicy(this.host);
			Request request = Request.Create(this.host, "GoogleAnalytics", null, TextValue.New(uri.ToString()), null, binaryValue, null, null, null, null, null, null, null, googleAnalyticsRequestPolicy.RetryPolicy);
			Value value;
			try
			{
				using (Response response = request.GetResponse(resourceCredentialCollection, new Request.SecurityExceptionCreator(googleAnalyticsRequestPolicy.TryGetSecurityException), false))
				{
					Encoding encoding = Encoding.UTF8;
					if (!string.IsNullOrEmpty(response.CharacterSet))
					{
						encoding = Encoding.GetEncoding(response.CharacterSet);
					}
					using (StreamReader streamReader = new StreamReader(response.GetResponseStream(), encoding))
					{
						value = JsonParser.Parse(streamReader, null);
					}
				}
			}
			catch (ResponseException ex)
			{
				Message2 message = Strings.WebRequestFailed(request.ResourceKind, ex.InnerException.Message);
				throw DataSourceException.NewDataSourceError<Message2>(this.host, message, GoogleAnalyticsServiceV1.resource, "Url", TextValue.New(request.Uri.OriginalString), TypeValue.Text, null);
			}
			return value;
		}

		// Token: 0x06004F5A RID: 20314 RVA: 0x001094AC File Offset: 0x001076AC
		public void DownloadMetadata(GoogleAnalyticsProperty property, out IList<GoogleAnalyticsCubeObject> measures, out IList<GoogleAnalyticsCubeObject> dimensions)
		{
			measures = new List<GoogleAnalyticsCubeObject>();
			dimensions = new List<GoogleAnalyticsCubeObject>();
			ListValue asList = this.ExecuteRequest(new UriBuilder
			{
				Path = "/analytics/v3/metadata/ga/columns"
			}, null)["items"].AsList;
			string text = null;
			string text2 = null;
			string text3 = null;
			foreach (IValueReference valueReference in asList)
			{
				Value value = valueReference.Value;
				string asString = value["id"].AsString;
				Value value2 = value["attributes"];
				string asString2 = value2["type"].AsString;
				string asString3 = value2["dataType"].AsString;
				string asString4 = value2["group"].AsString;
				string asString5 = value2["uiName"].AsString;
				GoogleAnalyticsCubeObjectStatus googleAnalyticsCubeObjectStatus = ((value2["status"].AsString == "PUBLIC") ? GoogleAnalyticsCubeObjectStatus.Public : GoogleAnalyticsCubeObjectStatus.Deprecated);
				string asString6 = value2["description"].AsString;
				if (asString == "ga:dimensionXX")
				{
					text2 = GoogleAnalyticsServiceV1.TrimNamePlaceholder(asString5);
					text = asString4;
				}
				else if (asString == "ga:metricXX")
				{
					text3 = GoogleAnalyticsServiceV1.TrimNamePlaceholder(asString5);
				}
				else
				{
					int num = 0;
					int num2 = 0;
					Value value3;
					Value value4;
					if (value2.TryGetValue("minTemplateIndex", out value3) && value2.TryGetValue("maxTemplateIndex", out value4))
					{
						num = int.Parse(value3.AsString, CultureInfo.InvariantCulture);
						num2 = int.Parse(value4.AsString, CultureInfo.InvariantCulture);
					}
					for (int i = num; i <= num2; i++)
					{
						string text4;
						string text5;
						string text6;
						GoogleAnalyticsCubeObjectPath googleAnalyticsCubeObjectPath;
						if (num == 0)
						{
							text4 = asString5;
							text5 = asString6;
							text6 = asString;
							googleAnalyticsCubeObjectPath = new GoogleAnalyticsCubeObjectPath(new string[] { asString4 });
						}
						else
						{
							string text7 = i.ToString(CultureInfo.InvariantCulture);
							if (asString5.Contains(GoogleAnalyticsServiceV1.CubeObjectTemplateOrdinalPlaceholder))
							{
								text4 = asString5.Replace(GoogleAnalyticsServiceV1.CubeObjectTemplateOrdinalPlaceholder, text7);
							}
							else
							{
								text4 = asString5 + " " + text7;
							}
							text5 = asString6.Replace(GoogleAnalyticsServiceV1.CubeObjectTemplateOrdinalPlaceholder, i.ToString(CultureInfo.InvariantCulture));
							text6 = asString.Replace(GoogleAnalyticsServiceV1.CubeObjectTemplateOrdinalPlaceholder, text7);
							googleAnalyticsCubeObjectPath = new GoogleAnalyticsCubeObjectPath(new string[]
							{
								asString4,
								GoogleAnalyticsServiceV1.TrimNamePlaceholder(asString5)
							});
						}
						GoogleAnalyticsCubeObject googleAnalyticsCubeObject = new GoogleAnalyticsCubeObject(text6, text4, text5, GoogleAnalyticsServiceV1.DecodeGoogleAnalyticsDataType(text6, asString3), googleAnalyticsCubeObjectPath, googleAnalyticsCubeObjectStatus, (asString2 == "METRIC") ? GoogleAnalyticsCubeObjectKind.Measure : GoogleAnalyticsCubeObjectKind.Dimension, asString5);
						if (asString2 == "METRIC")
						{
							measures.Add(googleAnalyticsCubeObject);
						}
						else if (asString2 == "DIMENSION")
						{
							dimensions.Add(googleAnalyticsCubeObject);
						}
					}
				}
			}
			if (property != null)
			{
				foreach (Value value5 in this.DownloadList(new UriBuilder
				{
					Path = string.Concat(new string[] { "/analytics/v3/management/accounts/", property.AccountID, "/webproperties/", property.ID, "/customDimensions" })
				}))
				{
					dimensions.Add(new GoogleAnalyticsCubeObject(value5["id"].AsString, value5["name"].AsString, string.Empty, GoogleAnalyticsDataType.String, new GoogleAnalyticsCubeObjectPath(new string[] { text, text2 }), GoogleAnalyticsCubeObjectStatus.Public, GoogleAnalyticsCubeObjectKind.Dimension, text2));
				}
				foreach (Value value6 in this.DownloadList(new UriBuilder
				{
					Path = string.Concat(new string[] { "/analytics/v3/management/accounts/", property.AccountID, "/webproperties/", property.ID, "/customMetrics" })
				}))
				{
					measures.Add(new GoogleAnalyticsCubeObject(value6["id"].AsString, value6["name"].AsString, string.Empty, GoogleAnalyticsServiceV1.DecodeGoogleAnalyticsDataType(value6["id"].AsString, value6["type"].AsString), new GoogleAnalyticsCubeObjectPath(new string[] { text, text3 }), GoogleAnalyticsCubeObjectStatus.Public, GoogleAnalyticsCubeObjectKind.Measure, text3));
				}
			}
		}

		// Token: 0x06004F5B RID: 20315 RVA: 0x00109968 File Offset: 0x00107B68
		public List<Value> DownloadList(UriBuilder uriBuilder)
		{
			int num = 1;
			List<Value> list = new List<Value>();
			int asInteger;
			int asInteger2;
			do
			{
				uriBuilder.Query = uriBuilder.Query + "start-index=" + num.ToString(CultureInfo.InvariantCulture);
				Value value = this.ExecuteRequest(uriBuilder, null);
				asInteger = value["itemsPerPage"].AsInteger32;
				asInteger2 = value["totalResults"].AsInteger32;
				foreach (IValueReference valueReference in value["items"].AsList)
				{
					list.Add(valueReference.Value);
				}
				num += asInteger;
			}
			while (num - 1 < asInteger2 && asInteger > 0);
			return list;
		}

		// Token: 0x06004F5C RID: 20316 RVA: 0x00109A30 File Offset: 0x00107C30
		public static GoogleAnalyticsDataType DecodeGoogleAnalyticsDataType(string id, string dataType)
		{
			if (id.Equals("ga:date"))
			{
				return GoogleAnalyticsDataType.Date;
			}
			if (dataType == "CURRENCY")
			{
				return GoogleAnalyticsDataType.Currency;
			}
			if (dataType == "FLOAT")
			{
				return GoogleAnalyticsDataType.Float;
			}
			if (dataType == "INTEGER")
			{
				return GoogleAnalyticsDataType.Integer;
			}
			if (dataType == "PERCENT")
			{
				return GoogleAnalyticsDataType.Percent;
			}
			if (dataType == "STRING")
			{
				return GoogleAnalyticsDataType.String;
			}
			if (!(dataType == "TIME"))
			{
				return GoogleAnalyticsDataType.String;
			}
			return GoogleAnalyticsDataType.Time;
		}

		// Token: 0x06004F5D RID: 20317 RVA: 0x00109AAC File Offset: 0x00107CAC
		public static IValueReference MarshallGoogleAnalyticsDataType(TextValue value, GoogleAnalyticsDataType type, bool dimensionColumn)
		{
			string asString = value.AsString;
			if (dimensionColumn && asString == "(other)")
			{
				return new DelayedValue(delegate
				{
					throw ValueException.NewDataFormatError<Message0>(Strings.DimensionContainsRollup, value, null);
				});
			}
			IValueReference valueReference;
			try
			{
				switch (type)
				{
				case GoogleAnalyticsDataType.Currency:
				{
					decimal num;
					if (decimal.TryParse(asString, NumberStyles.Number, CultureInfo.InvariantCulture, out num))
					{
						return NumberValue.New(num);
					}
					return NumberValue.New(decimal.Parse(asString, NumberStyles.Float, CultureInfo.InvariantCulture));
				}
				case GoogleAnalyticsDataType.Date:
				{
					int num2 = int.Parse(asString.Substring(0, 4), CultureInfo.InvariantCulture);
					int num3 = int.Parse(asString.Substring(4, 2), CultureInfo.InvariantCulture);
					int num4 = int.Parse(asString.Substring(6, 2), CultureInfo.InvariantCulture);
					return DateValue.New(num2, num3, num4);
				}
				case GoogleAnalyticsDataType.Float:
					return NumberValue.New(double.Parse(asString, CultureInfo.InvariantCulture));
				case GoogleAnalyticsDataType.Integer:
					return NumberValue.New(long.Parse(asString, CultureInfo.InvariantCulture));
				case GoogleAnalyticsDataType.Percent:
					return NumberValue.New(double.Parse(asString, CultureInfo.InvariantCulture) / 100.0);
				case GoogleAnalyticsDataType.Time:
					return DurationValue.New(0, 0, 0, double.Parse(asString, CultureInfo.InvariantCulture));
				}
				valueReference = value;
			}
			catch (FormatException)
			{
				valueReference = new ExceptionValueReference(ValueException.NewDataFormatError<Message1>(Strings.GoogleAnalyticsUnexpectedResponse(type), value, null));
			}
			return valueReference;
		}

		// Token: 0x06004F5E RID: 20318 RVA: 0x00109C50 File Offset: 0x00107E50
		public IList<GoogleAnalyticsAccount> GetAccounts()
		{
			if (this.accounts == null)
			{
				List<Value> list = this.DownloadList(new UriBuilder
				{
					Scheme = this.serverUrl.Scheme,
					Path = "/analytics/v3/management/accounts"
				});
				this.accounts = new GoogleAnalyticsAccount[list.Count];
				for (int i = 0; i < list.Count; i++)
				{
					Value value = list[i];
					string asString = value["id"].AsString;
					string asString2 = value["name"].AsString;
					string asString3 = value["childLink"]["href"].AsString;
					this.accounts[i] = new GoogleAnalyticsAccountV1(this, asString, asString2, asString3);
				}
			}
			return this.accounts;
		}

		// Token: 0x06004F5F RID: 20319 RVA: 0x00109D16 File Offset: 0x00107F16
		public DateTime GetFixedNow()
		{
			return this.host.QueryService<ICurrentTimeService>().FixedUtcNow.AdjustForTimeZone(this.host.QueryService<ITimeZoneService>().DefaultTimeZone);
		}

		// Token: 0x06004F60 RID: 20320 RVA: 0x00109D3D File Offset: 0x00107F3D
		private static string TrimNamePlaceholder(string name)
		{
			if (name.EndsWith(" XX", StringComparison.Ordinal))
			{
				return name.Substring(0, name.Length - 3);
			}
			return name;
		}

		// Token: 0x04002A9E RID: 10910
		private readonly IEngineHost host;

		// Token: 0x04002A9F RID: 10911
		private readonly Uri serverUrl;

		// Token: 0x04002AA0 RID: 10912
		private static readonly string CubeObjectTemplateOrdinalPlaceholder = "XX";

		// Token: 0x04002AA1 RID: 10913
		public static readonly IResource resource = Resource.New("GoogleAnalytics", "GoogleAnalytics");

		// Token: 0x04002AA2 RID: 10914
		private IList<GoogleAnalyticsAccount> accounts;
	}
}
