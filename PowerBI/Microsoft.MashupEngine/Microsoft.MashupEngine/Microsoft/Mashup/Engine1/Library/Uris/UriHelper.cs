using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Uris
{
	// Token: 0x020002B3 RID: 691
	public static class UriHelper
	{
		// Token: 0x06001B49 RID: 6985 RVA: 0x00038F30 File Offset: 0x00037130
		public static string AddQueryPart(string queryString, string key, string value)
		{
			string text = UriHelper.BuildQueryPart(key, value);
			if (string.IsNullOrEmpty(queryString))
			{
				return text;
			}
			return queryString + "&" + text;
		}

		// Token: 0x06001B4A RID: 6986 RVA: 0x00038F5C File Offset: 0x0003715C
		public static Uri AddQueryRecord(Uri originalUri, RecordValue newQuery)
		{
			Uri uri;
			try
			{
				NameValueCollection nameValueCollection = UriHelper.BuildQueryCollection(newQuery);
				uri = UriHelper.AddQueryRecord(originalUri, nameValueCollection);
			}
			catch (UriFormatException ex)
			{
				throw UriErrors.InputInvalid(newQuery, ex.Message);
			}
			return uri;
		}

		// Token: 0x06001B4B RID: 6987 RVA: 0x00038F9C File Offset: 0x0003719C
		public static Uri AddQueryRecord(Uri originalUri, Value newQuery)
		{
			if (!newQuery.IsNull)
			{
				return UriHelper.AddQueryRecord(originalUri, newQuery.AsRecord);
			}
			return originalUri;
		}

		// Token: 0x06001B4C RID: 6988 RVA: 0x00038FB4 File Offset: 0x000371B4
		public static Uri AddQueryRecord(Uri originalUri, NameValueCollection queryParameters)
		{
			if (queryParameters != null)
			{
				UriBuilder uriBuilder = new UriBuilder(originalUri);
				UriHelper.AddQueryRecord(uriBuilder, queryParameters);
				return uriBuilder.Uri;
			}
			return originalUri;
		}

		// Token: 0x06001B4D RID: 6989 RVA: 0x00038FCD File Offset: 0x000371CD
		public static Uri RemoveQueryKeys(Uri originalUri, HashSet<string> keysToRemove)
		{
			if (keysToRemove != null)
			{
				UriBuilder uriBuilder = new UriBuilder(originalUri);
				UriHelper.RemoveQueryKeys(uriBuilder, keysToRemove);
				return uriBuilder.Uri;
			}
			return originalUri;
		}

		// Token: 0x06001B4E RID: 6990 RVA: 0x00038FE8 File Offset: 0x000371E8
		private static void RemoveQueryKeys(UriBuilder uriBuilder, HashSet<string> keysToRemove)
		{
			NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(uriBuilder.Query);
			foreach (string text in nameValueCollection.AllKeys)
			{
				if (keysToRemove.Contains(text))
				{
					nameValueCollection.Remove(text);
				}
			}
			uriBuilder.Query = UriHelper.BuildQueryString(nameValueCollection);
		}

		// Token: 0x06001B4F RID: 6991 RVA: 0x00039038 File Offset: 0x00037238
		public static void AddQueryRecord(UriBuilder uriBuilder, NameValueCollection newQueryString)
		{
			NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(uriBuilder.Query);
			for (int i = 0; i < newQueryString.Count; i++)
			{
				string text = newQueryString.AllKeys[i];
				string[] values = newQueryString.GetValues(i);
				if (values != null && values.Length != 0)
				{
					nameValueCollection[text] = values[0];
					for (int j = 1; j < values.Length; j++)
					{
						nameValueCollection.Add(text, values[j]);
					}
				}
			}
			uriBuilder.Query = UriHelper.BuildQueryString(nameValueCollection);
		}

		// Token: 0x06001B50 RID: 6992 RVA: 0x000390AD File Offset: 0x000372AD
		public static string BuildQueryPart(string key, string value)
		{
			return UriMethods.EscapeDataString(key, false) + "=" + UriMethods.EscapeDataString(value, false);
		}

		// Token: 0x06001B51 RID: 6993 RVA: 0x000390C7 File Offset: 0x000372C7
		public static string BuildQueryString(RecordValue queryValue)
		{
			return UriHelper.BuildQueryString(UriHelper.BuildQueryCollection(queryValue));
		}

		// Token: 0x06001B52 RID: 6994 RVA: 0x000390D4 File Offset: 0x000372D4
		private static string BuildQueryString(NameValueCollection queryParameters)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < queryParameters.Count; i++)
			{
				string text = queryParameters.AllKeys[i];
				string[] values = queryParameters.GetValues(i);
				for (int j = 0; j < values.Length; j++)
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append('&');
					}
					stringBuilder.Append(UriMethods.EscapeDataString(text, false));
					stringBuilder.Append('=');
					stringBuilder.Append(UriMethods.EscapeDataString(values[j], false));
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001B53 RID: 6995 RVA: 0x0003915C File Offset: 0x0003735C
		private static NameValueCollection BuildQueryCollection(RecordValue queryParameters)
		{
			int count = queryParameters.Count;
			NameValueCollection nameValueCollection = new NameValueCollection(count);
			int i = 0;
			while (i < count)
			{
				string text = queryParameters.Keys[i];
				Value value = queryParameters[i];
				if (value.IsList)
				{
					using (IEnumerator<IValueReference> enumerator = value.AsList.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							IValueReference valueReference = enumerator.Current;
							nameValueCollection.Add(text, valueReference.Value.AsString);
						}
						goto IL_0082;
					}
					goto IL_0074;
				}
				goto IL_0074;
				IL_0082:
				i++;
				continue;
				IL_0074:
				nameValueCollection[text] = value.AsString;
				goto IL_0082;
			}
			return nameValueCollection;
		}

		// Token: 0x06001B54 RID: 6996 RVA: 0x00039204 File Offset: 0x00037404
		public static Uri CreateAbsoluteUriFromValue(TextValue value)
		{
			string @string = value.String;
			UriBuilder uriBuilder;
			ValueException ex;
			if (UriHelper.TryCreateAbsoluteUri(@string, out uriBuilder, out ex))
			{
				try
				{
					return uriBuilder.Uri;
				}
				catch (UriFormatException ex2)
				{
					throw UriErrors.InputInvalid(@string, ex2.Message);
				}
			}
			throw ex;
		}

		// Token: 0x06001B55 RID: 6997 RVA: 0x00039250 File Offset: 0x00037450
		public static RecordValue CreateQueryRecord(string query, bool produceLists = false)
		{
			NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(query);
			if (nameValueCollection.Count == 0)
			{
				return RecordValue.Empty;
			}
			RecordBuilder recordBuilder = new RecordBuilder(nameValueCollection.Count);
			new Value[nameValueCollection.Count];
			for (int i = 0; i < nameValueCollection.Count; i++)
			{
				if (nameValueCollection.AllKeys[i] == null)
				{
					throw ValueException.NewDataFormatError<Message0>(Strings.MalformedQueryString, TextValue.New(query), null);
				}
				Value value;
				if (produceLists)
				{
					string[] values = nameValueCollection.GetValues(i);
					if (values.Length == 1)
					{
						value = TextValue.New(values[0]);
					}
					else
					{
						value = ListValue.New(values.Select(new Func<string, TextValue>(TextValue.New)).Cast<IValueReference>());
					}
				}
				else
				{
					value = TextValue.New(nameValueCollection[i]);
				}
				recordBuilder.Add(nameValueCollection.AllKeys[i], value, value.Type);
			}
			return recordBuilder.ToRecord();
		}

		// Token: 0x06001B56 RID: 6998 RVA: 0x0003932C File Offset: 0x0003752C
		public static Uri CreateUriFromValue(TextValue value)
		{
			string asString = value.AsString;
			Uri uri;
			try
			{
				uri = new Uri(asString);
			}
			catch (UriFormatException ex)
			{
				throw UriErrors.InputInvalid(asString, ex.Message);
			}
			catch (ArgumentException)
			{
				throw UriErrors.InputInvalid(asString);
			}
			return uri;
		}

		// Token: 0x06001B57 RID: 6999 RVA: 0x0003937C File Offset: 0x0003757C
		public static Uri CreateRelativeUriFromValue(TextValue value)
		{
			string asString = value.AsString;
			Uri uri;
			try
			{
				uri = new Uri(asString, UriKind.Relative);
			}
			catch (UriFormatException ex)
			{
				throw UriErrors.InputInvalid(asString, ex.Message);
			}
			catch (ArgumentException)
			{
				throw UriErrors.InputInvalid(asString);
			}
			return uri;
		}

		// Token: 0x06001B58 RID: 7000 RVA: 0x000393D0 File Offset: 0x000375D0
		public static Uri Combine(Uri baseUri, TextValue additionalPathAndQuery)
		{
			Uri uri;
			try
			{
				UriBuilder uriBuilder = new UriBuilder(baseUri);
				UriBuilder uriBuilder2 = new UriBuilder(new Uri(new Uri("http://tmp"), UriHelper.CreateRelativeUriFromValue(additionalPathAndQuery)));
				string path = uriBuilder.Path;
				bool flag = path.Length > 0 && path[path.Length - 1] == '/';
				string text = uriBuilder2.Path;
				bool flag2 = text.Length > 0 && text[0] == '/';
				if (flag && flag2)
				{
					text = text.Substring(1);
				}
				else if (!flag && !flag2)
				{
					text = "/" + text;
				}
				UriBuilder uriBuilder3 = uriBuilder;
				uriBuilder3.Path += text;
				if (uriBuilder2.Query.Length > 0)
				{
					if (uriBuilder.Query.Length == 0)
					{
						uriBuilder.Query = uriBuilder2.Query.TrimStart(new char[] { '?' });
					}
					else
					{
						UriHelper.AddQueryRecord(uriBuilder, HttpUtility.ParseQueryString(uriBuilder2.Query));
					}
				}
				uri = uriBuilder.Uri;
			}
			catch (UriFormatException ex)
			{
				throw UriErrors.InputInvalid(additionalPathAndQuery, ex.Message);
			}
			return uri;
		}

		// Token: 0x06001B59 RID: 7001 RVA: 0x000394F8 File Offset: 0x000376F8
		public static bool EndsWithPathSeparator(string path)
		{
			return !string.IsNullOrEmpty(path) && path[path.Length - 1] == '/';
		}

		// Token: 0x06001B5A RID: 7002 RVA: 0x00039516 File Offset: 0x00037716
		public static bool ContainsPathSeparator(string path)
		{
			return path.Contains('/');
		}

		// Token: 0x06001B5B RID: 7003 RVA: 0x00039520 File Offset: 0x00037720
		public static string GetDirectoryName(string url)
		{
			UriBuilder uriBuilder = new UriBuilder(url);
			uriBuilder.Path = uriBuilder.Path.Substring(0, uriBuilder.Path.LastIndexOf('/'));
			return Uri.UnescapeDataString(uriBuilder.Uri.AbsoluteUri);
		}

		// Token: 0x06001B5C RID: 7004 RVA: 0x00039564 File Offset: 0x00037764
		public static string GetFileName(string url)
		{
			UriBuilder uriBuilder = new UriBuilder(url);
			return Uri.UnescapeDataString(uriBuilder.Path.Substring(uriBuilder.Path.LastIndexOf('/') + 1));
		}

		// Token: 0x06001B5D RID: 7005 RVA: 0x00039597 File Offset: 0x00037797
		public static void ValidateHttpsScheme(TextValue url)
		{
			if (!Uri.IsWellFormedUriString(url.String, UriKind.Absolute))
			{
				throw UriErrors.InputInvalid(url.String);
			}
			if (!UriHelper.IsHttpsUri(new Uri(url.String)))
			{
				throw UriErrors.InvalidHttpsScheme(url);
			}
		}

		// Token: 0x06001B5E RID: 7006 RVA: 0x000395CC File Offset: 0x000377CC
		public static bool TryCreateAbsoluteUriBuilder(string uriText, out UriBuilder uriBuilder)
		{
			ValueException ex;
			return UriHelper.TryCreateAbsoluteUri(uriText, out uriBuilder, out ex);
		}

		// Token: 0x06001B5F RID: 7007 RVA: 0x000395E4 File Offset: 0x000377E4
		public static string RemoveDefaultPort(UriBuilder builder)
		{
			string host = builder.Host;
			string text = builder.ToString();
			Uri uri = builder.Uri;
			if (uri.IsDefaultPort)
			{
				return uri.AbsoluteUri.Replace(uri.Host, host);
			}
			return text;
		}

		// Token: 0x06001B60 RID: 7008 RVA: 0x00039624 File Offset: 0x00037824
		public static UriBuilder CreateAbsoluteUriBuilderFromValue(TextValue value)
		{
			UriBuilder uriBuilder;
			ValueException ex;
			if (UriHelper.TryCreateAbsoluteUri(value.String, out uriBuilder, out ex))
			{
				return uriBuilder;
			}
			throw ex;
		}

		// Token: 0x06001B61 RID: 7009 RVA: 0x00039645 File Offset: 0x00037845
		public static string NormalizeUriComponent(string queryOrFragment)
		{
			if (queryOrFragment.Length > 1)
			{
				queryOrFragment = queryOrFragment.Substring(1);
			}
			return queryOrFragment;
		}

		// Token: 0x06001B62 RID: 7010 RVA: 0x0003965C File Offset: 0x0003785C
		public static bool TryCreateAbsoluteUri(string uriText, out Uri uri)
		{
			UriBuilder uriBuilder;
			ValueException ex;
			bool flag = UriHelper.TryCreateAbsoluteUri(uriText, out uriBuilder, out ex);
			uri = ((uriBuilder == null) ? null : uriBuilder.Uri);
			return flag;
		}

		// Token: 0x06001B63 RID: 7011 RVA: 0x00039681 File Offset: 0x00037881
		public static bool IsFileUri(Uri uri)
		{
			return Uri.UriSchemeFile.Equals(uri.Scheme, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06001B64 RID: 7012 RVA: 0x00039694 File Offset: 0x00037894
		public static bool IsFtpUri(Uri uri)
		{
			return Uri.UriSchemeFtp.Equals(uri.Scheme, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06001B65 RID: 7013 RVA: 0x000396A7 File Offset: 0x000378A7
		public static bool IsHttpsUri(Uri uri)
		{
			return Uri.UriSchemeHttps.Equals(uri.Scheme, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06001B66 RID: 7014 RVA: 0x000396BA File Offset: 0x000378BA
		public static bool IsHttpUri(Uri uri)
		{
			return Uri.UriSchemeHttp.Equals(uri.Scheme, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06001B67 RID: 7015 RVA: 0x000396CD File Offset: 0x000378CD
		public static bool IsWebUri(Uri uri)
		{
			return UriHelper.IsHttpUri(uri) || UriHelper.IsHttpsUri(uri);
		}

		// Token: 0x06001B68 RID: 7016 RVA: 0x000396E0 File Offset: 0x000378E0
		public static bool TryCreateAbsoluteUri(string uriText, out UriBuilder uriBuilder, out ValueException uriError)
		{
			bool flag;
			try
			{
				uriText = UriHelper.FixUncWebDavPath(uriText);
				uriBuilder = new UriBuilder(uriText);
				if (uriBuilder.Host.Length == 0 && UriHelper.hostPortRegex.IsMatch(uriText))
				{
					uriBuilder = new UriBuilder("http://" + uriText);
				}
				if (uriBuilder.Uri.HostNameType == UriHostNameType.IPv6)
				{
					uriBuilder.Host = UriHelper.NormalizeIPv6Host(uriBuilder.Host);
				}
				if (uriBuilder.UserName.Length != 0)
				{
					StringBuilder stringBuilder = new StringBuilder(uriBuilder.UserName);
					uriBuilder.UserName = stringBuilder.Replace("[", "%5B").Replace("]", "%5D").ToString();
					if (uriBuilder.Password.Length != 0)
					{
						stringBuilder = new StringBuilder(uriBuilder.Password);
						uriBuilder.Password = stringBuilder.Replace("[", "%5B").Replace("]", "%5D").ToString();
					}
				}
				if (uriBuilder.Query.Length != 0)
				{
					StringBuilder stringBuilder2 = new StringBuilder(uriBuilder.Query);
					uriBuilder.Query = stringBuilder2.Replace("[", "%5B").Replace("]", "%5D").ToString(1, stringBuilder2.Length - 1);
				}
				if (uriBuilder.Fragment.Length != 0)
				{
					StringBuilder stringBuilder3 = new StringBuilder(uriBuilder.Fragment);
					uriBuilder.Fragment = stringBuilder3.Replace("#", "%23").Replace("[", "%5B").Replace("]", "%5D")
						.ToString(3, stringBuilder3.Length - 3);
				}
				uriError = null;
				flag = true;
			}
			catch (UriFormatException ex)
			{
				uriBuilder = null;
				uriError = UriErrors.InputInvalid(uriText, ex.Message);
				flag = false;
			}
			catch (ArgumentException)
			{
				uriBuilder = null;
				uriError = UriErrors.InputInvalid(uriText);
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001B69 RID: 7017 RVA: 0x000398EC File Offset: 0x00037AEC
		public static string FixUncWebDavPath(string uriText)
		{
			Match match = UriHelper.webDavRegex.Match(uriText);
			if (!match.Success)
			{
				return uriText;
			}
			return "\\\\" + match.Groups[1].Value + "\\" + match.Groups[2].Value;
		}

		// Token: 0x06001B6A RID: 7018 RVA: 0x00039940 File Offset: 0x00037B40
		public static string NormalizeIPv6Host(string host)
		{
			return IPAddress.Parse(host).ToString();
		}

		// Token: 0x04000878 RID: 2168
		private static readonly Regex webDavRegex = new Regex("^\\\\\\\\([^\\\\@]+)@SSL\\\\(.*)$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		// Token: 0x04000879 RID: 2169
		private static readonly Regex hostPortRegex = new Regex("^[^:]+:[0-9]+.*$", RegexOptions.Compiled);

		// Token: 0x0400087A RID: 2170
		public const char PathSeparator = '/';

		// Token: 0x0400087B RID: 2171
		public const int UriMaxLength = 65519;

		// Token: 0x0400087C RID: 2172
		public const string Wildcard = "*";

		// Token: 0x0400087D RID: 2173
		private const string EncodedNumberSign = "%23";

		// Token: 0x0400087E RID: 2174
		private const string EncodedOpeningBracket = "%5B";

		// Token: 0x0400087F RID: 2175
		private const string EncodedClosingBracket = "%5D";
	}
}
