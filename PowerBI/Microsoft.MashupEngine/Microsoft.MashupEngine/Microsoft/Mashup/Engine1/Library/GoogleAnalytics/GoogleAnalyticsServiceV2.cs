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
	// Token: 0x02000B2F RID: 2863
	internal class GoogleAnalyticsServiceV2 : IGoogleAnalyticsService
	{
		// Token: 0x06004F67 RID: 20327 RVA: 0x00109DA4 File Offset: 0x00107FA4
		public GoogleAnalyticsServiceV2(IEngineHost host)
		{
			this.host = host;
			this.adminUrl = this.host.Hook(() => new GoogleAnalyticsServerUrl()).V2AdminServerBaseUrl;
			this.dataUrl = this.host.Hook(() => new GoogleAnalyticsServerUrl()).V2DataServerBaseUrl;
		}

		// Token: 0x170018C6 RID: 6342
		// (get) Token: 0x06004F68 RID: 20328 RVA: 0x00109E28 File Offset: 0x00108028
		public IEngineHost Host
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x06004F69 RID: 20329 RVA: 0x00109E30 File Offset: 0x00108030
		public Value GetReport(string viewId, GoogleAnalyticsQueryExpression compiledExpression)
		{
			if (compiledExpression.Measures.Count == 0)
			{
				return RecordValue.New(Keys.New("rows", "metricHeaders"), new Value[]
				{
					ListValue.Empty,
					ListValue.Empty
				});
			}
			UriBuilder uriBuilder = new UriBuilder();
			uriBuilder.Path = string.Format(CultureInfo.InvariantCulture, "/v1beta/{0}:runReport", compiledExpression.PropertyName);
			return this.ExecuteRequest(this.dataUrl, uriBuilder, compiledExpression.Body);
		}

		// Token: 0x06004F6A RID: 20330 RVA: 0x00109EAC File Offset: 0x001080AC
		private Value ExecuteRequest(Uri baseUri, UriBuilder uri, RecordValue body = null)
		{
			ResourceCredentialCollection resourceCredentialCollection;
			HttpServices.VerifyPermissionAndGetCredentials(this.host, GoogleAnalyticsServiceV1.resource, out resourceCredentialCollection);
			if (resourceCredentialCollection.Count != 1 || !(resourceCredentialCollection[0] is OAuthCredential))
			{
				throw DataSourceException.NewInvalidCredentialsError(this.host, GoogleAnalyticsServiceV2.resource, null, null, null);
			}
			OAuthCredential oauthCredential = (OAuthCredential)resourceCredentialCollection[0];
			uri.Port = baseUri.Port;
			uri.Scheme = baseUri.Scheme;
			uri.Host = baseUri.Host;
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
				throw DataSourceException.NewDataSourceError<Message2>(this.host, message, GoogleAnalyticsServiceV2.resource, "Url", TextValue.New(request.Uri.OriginalString), TypeValue.Text, null);
			}
			return value;
		}

		// Token: 0x06004F6B RID: 20331 RVA: 0x0010A0C0 File Offset: 0x001082C0
		private void AddMetadataObjectsToList(ListValue items, GoogleAnalyticsCubeObjectKind kind, IList<GoogleAnalyticsCubeObject> target)
		{
			foreach (IValueReference valueReference in items)
			{
				Value value = valueReference.Value;
				string asString = value["apiName"].AsString;
				Value value2;
				string text;
				if (value.TryGetValue("type", out value2))
				{
					text = value["type"].AsString;
				}
				else
				{
					text = "TYPE_STRING";
				}
				string text2 = asString;
				Value value3;
				if (value.TryGetValue("uiName", out value3))
				{
					text2 = value3.AsString;
				}
				string text3 = string.Empty;
				if (value.TryGetValue("description", out value3))
				{
					text3 = value3.AsString;
				}
				target.Add(new GoogleAnalyticsCubeObject(asString, asString, text3, GoogleAnalyticsServiceV2.DecodeGoogleAnalyticsDataType(asString, text), new GoogleAnalyticsCubeObjectPath(new string[0]), GoogleAnalyticsCubeObjectStatus.Public, kind, text2));
			}
		}

		// Token: 0x06004F6C RID: 20332 RVA: 0x0010A1A8 File Offset: 0x001083A8
		public void DownloadMetadata(GoogleAnalyticsProperty property, out IList<GoogleAnalyticsCubeObject> measures, out IList<GoogleAnalyticsCubeObject> dimensions)
		{
			measures = new List<GoogleAnalyticsCubeObject>();
			dimensions = new List<GoogleAnalyticsCubeObject>();
			UriBuilder uriBuilder = new UriBuilder();
			uriBuilder.Path = string.Format(CultureInfo.InvariantCulture, "v1beta/{0}/metadata", property.ID);
			Value value = this.ExecuteRequest(this.dataUrl, uriBuilder, null);
			ListValue asList = value["dimensions"].AsList;
			ListValue asList2 = value["metrics"].AsList;
			this.AddMetadataObjectsToList(asList, GoogleAnalyticsCubeObjectKind.Dimension, dimensions);
			this.AddMetadataObjectsToList(asList2, GoogleAnalyticsCubeObjectKind.Measure, measures);
		}

		// Token: 0x06004F6D RID: 20333 RVA: 0x0010A228 File Offset: 0x00108428
		public List<Value> DownloadList(UriBuilder uriBuilder)
		{
			List<Value> list = new List<Value>();
			this.AddPagesOfResultsToList<Value>(list, uriBuilder, "properties", (Value value) => value);
			return list;
		}

		// Token: 0x06004F6E RID: 20334 RVA: 0x0010A268 File Offset: 0x00108468
		public static GoogleAnalyticsDataType DecodeGoogleAnalyticsDataType(string id, string dataType)
		{
			if (id.Equals("date"))
			{
				return GoogleAnalyticsDataType.Date;
			}
			if (dataType != null)
			{
				switch (dataType.Length)
				{
				case 9:
					if (!(dataType == "TYPE_FEET"))
					{
						return GoogleAnalyticsDataType.String;
					}
					break;
				case 10:
				{
					char c = dataType[5];
					if (c != 'F')
					{
						if (c != 'H')
						{
							if (c != 'M')
							{
								return GoogleAnalyticsDataType.String;
							}
							if (!(dataType == "TYPE_MILES"))
							{
								return GoogleAnalyticsDataType.String;
							}
						}
						else
						{
							if (!(dataType == "TYPE_HOURS"))
							{
								return GoogleAnalyticsDataType.String;
							}
							return GoogleAnalyticsDataType.Hours;
						}
					}
					else if (!(dataType == "TYPE_FLOAT"))
					{
						return GoogleAnalyticsDataType.String;
					}
					break;
				}
				case 11:
				{
					char c = dataType[5];
					if (c != 'M')
					{
						if (c != 'S')
						{
							return GoogleAnalyticsDataType.String;
						}
						if (!(dataType == "TYPE_STRING"))
						{
							return GoogleAnalyticsDataType.String;
						}
						return GoogleAnalyticsDataType.String;
					}
					else if (!(dataType == "TYPE_METERS"))
					{
						return GoogleAnalyticsDataType.String;
					}
					break;
				}
				case 12:
				{
					char c = dataType[5];
					if (c <= 'M')
					{
						if (c != 'I')
						{
							if (c != 'M')
							{
								return GoogleAnalyticsDataType.String;
							}
							if (!(dataType == "TYPE_MINUTES"))
							{
								return GoogleAnalyticsDataType.String;
							}
							return GoogleAnalyticsDataType.Minutes;
						}
						else
						{
							if (!(dataType == "TYPE_INTEGER"))
							{
								return GoogleAnalyticsDataType.String;
							}
							return GoogleAnalyticsDataType.Integer;
						}
					}
					else if (c != 'P')
					{
						if (c != 'S')
						{
							return GoogleAnalyticsDataType.String;
						}
						if (!(dataType == "TYPE_SECONDS"))
						{
							return GoogleAnalyticsDataType.String;
						}
						return GoogleAnalyticsDataType.Time;
					}
					else
					{
						if (!(dataType == "TYPE_PERCENT"))
						{
							return GoogleAnalyticsDataType.String;
						}
						return GoogleAnalyticsDataType.Percent;
					}
					break;
				}
				case 13:
					if (!(dataType == "TYPE_CURRENCY"))
					{
						return GoogleAnalyticsDataType.String;
					}
					return GoogleAnalyticsDataType.Currency;
				case 14:
				case 16:
					return GoogleAnalyticsDataType.String;
				case 15:
					if (!(dataType == "TYPE_KILOMETERS"))
					{
						return GoogleAnalyticsDataType.String;
					}
					break;
				case 17:
					if (!(dataType == "TYPE_MILLISECONDS"))
					{
						return GoogleAnalyticsDataType.String;
					}
					return GoogleAnalyticsDataType.Milliseconds;
				default:
					return GoogleAnalyticsDataType.String;
				}
				return GoogleAnalyticsDataType.Float;
			}
			return GoogleAnalyticsDataType.String;
		}

		// Token: 0x06004F6F RID: 20335 RVA: 0x0010A42C File Offset: 0x0010862C
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
					if (asString.Length != 8)
					{
						return new ExceptionValueReference(ValueException.NewDataFormatError<Message1>(Strings.GoogleAnalyticsUnexpectedResponse(type), value, null));
					}
					int num2 = int.Parse(asString.Substring(0, 4), CultureInfo.InvariantCulture);
					int num3 = int.Parse(asString.Substring(4, 2), CultureInfo.InvariantCulture);
					int num4 = int.Parse(asString.Substring(6, 2), CultureInfo.InvariantCulture);
					return DateValue.New(num2, num3, num4);
				}
				case GoogleAnalyticsDataType.Float:
					return NumberValue.New(double.Parse(asString, CultureInfo.InvariantCulture));
				case GoogleAnalyticsDataType.Hours:
					return DurationValue.New(0, 0, 0, double.Parse(asString, CultureInfo.InvariantCulture) * 3600.0);
				case GoogleAnalyticsDataType.Integer:
					return NumberValue.New(long.Parse(asString, CultureInfo.InvariantCulture));
				case GoogleAnalyticsDataType.Milliseconds:
					return DurationValue.New(0, 0, 0, double.Parse(asString, CultureInfo.InvariantCulture) / 1000.0);
				case GoogleAnalyticsDataType.Minutes:
					return DurationValue.New(0, 0, 0, double.Parse(asString, CultureInfo.InvariantCulture) * 60.0);
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

		// Token: 0x06004F70 RID: 20336 RVA: 0x0010A668 File Offset: 0x00108868
		public IList<GoogleAnalyticsAccount> GetAccounts()
		{
			if (this.accounts == null)
			{
				UriBuilder uriBuilder = new UriBuilder();
				uriBuilder.Path = "/v1beta/accounts";
				this.accounts = new List<GoogleAnalyticsAccount>();
				this.AddPagesOfResultsToList<GoogleAnalyticsAccount>(this.accounts, uriBuilder, "accounts", delegate(Value account)
				{
					string asString = account["name"].AsString;
					string text = asString;
					Value value;
					if (account.TryGetValue("displayName", out value) && value.IsText)
					{
						text = value.AsString;
					}
					string text2 = string.Format(CultureInfo.InvariantCulture, "https://analyticsadmin.googleapis.com/v1beta/properties?filter=parent:{0}", asString);
					return new GoogleAnalyticsAccountV2(this, asString, text, text2);
				});
			}
			return this.accounts;
		}

		// Token: 0x06004F71 RID: 20337 RVA: 0x0010A6BD File Offset: 0x001088BD
		public DateTime GetFixedNow()
		{
			return this.host.QueryService<ICurrentTimeService>().FixedUtcNow.AdjustForTimeZone(this.host.QueryService<ITimeZoneService>().DefaultTimeZone);
		}

		// Token: 0x06004F72 RID: 20338 RVA: 0x0010A6E4 File Offset: 0x001088E4
		private void AddPagesOfResultsToList<T>(IList<T> target, UriBuilder firstPage, string key, Func<Value, T> parse)
		{
			Value @null = Value.Null;
			do
			{
				UriBuilder uriBuilder = new UriBuilder();
				uriBuilder.Path = firstPage.Path;
				if (firstPage.Query != null && firstPage.Query.StartsWith("?", StringComparison.Ordinal))
				{
					uriBuilder.Query = firstPage.Query.Substring(1);
				}
				else
				{
					uriBuilder.Query = firstPage.Query;
				}
				if (!@null.IsNull)
				{
					string text = string.Format(CultureInfo.InvariantCulture, "pageToken={0}", @null.AsText.AsString);
					GoogleAnalyticsServiceV2.AddQuery(uriBuilder, text);
				}
				Value value = this.ExecuteRequest(this.adminUrl, uriBuilder, null);
				foreach (IValueReference valueReference in value[key].AsList)
				{
					target.Add(parse(valueReference.Value));
				}
				value.TryGetValue("nextPageToken", out @null);
			}
			while (!@null.IsNull);
		}

		// Token: 0x06004F73 RID: 20339 RVA: 0x00109D3D File Offset: 0x00107F3D
		private static string TrimNamePlaceholder(string name)
		{
			if (name.EndsWith(" XX", StringComparison.Ordinal))
			{
				return name.Substring(0, name.Length - 3);
			}
			return name;
		}

		// Token: 0x06004F74 RID: 20340 RVA: 0x0010A7F0 File Offset: 0x001089F0
		private static void AddQuery(UriBuilder uriBuilder, string query)
		{
			if (uriBuilder.Query != null && uriBuilder.Query.Length > 0)
			{
				uriBuilder.Query = uriBuilder.Query.Substring(1) + "&" + query;
				return;
			}
			uriBuilder.Query = query;
		}

		// Token: 0x04002AA6 RID: 10918
		private readonly IEngineHost host;

		// Token: 0x04002AA7 RID: 10919
		private readonly Uri adminUrl;

		// Token: 0x04002AA8 RID: 10920
		private readonly Uri dataUrl;

		// Token: 0x04002AA9 RID: 10921
		public static readonly IResource resource = Resource.New("GoogleAnalytics", "GoogleAnalytics");

		// Token: 0x04002AAA RID: 10922
		private IList<GoogleAnalyticsAccount> accounts;
	}
}
