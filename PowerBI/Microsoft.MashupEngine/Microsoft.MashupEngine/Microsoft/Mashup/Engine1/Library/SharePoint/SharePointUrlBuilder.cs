using System;
using System.Globalization;
using Microsoft.Mashup.Engine1.Library.File;
using Microsoft.Mashup.Engine1.Library.OData;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.SharePoint
{
	// Token: 0x02000410 RID: 1040
	internal sealed class SharePointUrlBuilder
	{
		// Token: 0x0600237A RID: 9082 RVA: 0x0006440C File Offset: 0x0006260C
		public SharePointUrlBuilder(string serviceUrl, string entity, string query)
			: this(SharePointUrlBuilder.GetUrl(serviceUrl, entity, query), SharePointApiVersion.SP14)
		{
		}

		// Token: 0x0600237B RID: 9083 RVA: 0x00064420 File Offset: 0x00062620
		public SharePointUrlBuilder(string url, SharePointApiVersion version = SharePointApiVersion.SP14)
		{
			this.suffix = SharePointUrlBuilder.GetSuffix(version);
			this.Url = SharePointUrlBuilder.GetUrl(url, this.suffix);
			this.Entity = SharePointUrlBuilder.GetEntity(this.Url, this.suffix);
			this.FeedUrl = this.GetFeedUrl(this.Url);
			this.HostUrl = SharePointUrlBuilder.GetHostUrl(this.Url);
			this.Path = SharePointUrlBuilder.GetPath(this.Url);
			this.Query = SharePointUrlBuilder.GetQuery(this.Url);
			this.ServiceUrl = SharePointUrlBuilder.GetServiceUrl(this.Url, this.suffix);
			this.SiteUrl = SharePointUrlBuilder.GetSiteUrl(this.Url, this.suffix);
			this.SitePath = SharePointUrlBuilder.GetSitePath(this.SiteUrl);
			this.Top = SharePointUrlBuilder.GetTop(this.Url);
			this.Skip = SharePointUrlBuilder.GetSkip(this.Url);
		}

		// Token: 0x0600237C RID: 9084 RVA: 0x00064510 File Offset: 0x00062710
		private SharePointUrlBuilder(SharePointUrlBuilder urlBuilder)
		{
			this.Url = urlBuilder.Url;
			this.Entity = urlBuilder.Entity;
			this.FeedUrl = urlBuilder.FeedUrl;
			this.HostUrl = urlBuilder.HostUrl;
			this.Path = urlBuilder.Path;
			this.Query = urlBuilder.Query;
			this.ServiceUrl = urlBuilder.ServiceUrl;
			this.SiteUrl = urlBuilder.SiteUrl;
			this.SitePath = urlBuilder.SitePath;
			this.suffix = urlBuilder.suffix;
		}

		// Token: 0x17000EA8 RID: 3752
		// (get) Token: 0x0600237D RID: 9085 RVA: 0x0006459B File Offset: 0x0006279B
		// (set) Token: 0x0600237E RID: 9086 RVA: 0x000645A3 File Offset: 0x000627A3
		public string Entity { get; private set; }

		// Token: 0x17000EA9 RID: 3753
		// (get) Token: 0x0600237F RID: 9087 RVA: 0x000645AC File Offset: 0x000627AC
		// (set) Token: 0x06002380 RID: 9088 RVA: 0x000645B4 File Offset: 0x000627B4
		public string FeedUrl { get; private set; }

		// Token: 0x17000EAA RID: 3754
		// (get) Token: 0x06002381 RID: 9089 RVA: 0x000645BD File Offset: 0x000627BD
		// (set) Token: 0x06002382 RID: 9090 RVA: 0x000645C5 File Offset: 0x000627C5
		public string HostUrl { get; private set; }

		// Token: 0x17000EAB RID: 3755
		// (get) Token: 0x06002383 RID: 9091 RVA: 0x000645CE File Offset: 0x000627CE
		// (set) Token: 0x06002384 RID: 9092 RVA: 0x000645D6 File Offset: 0x000627D6
		public string Path { get; private set; }

		// Token: 0x17000EAC RID: 3756
		// (get) Token: 0x06002385 RID: 9093 RVA: 0x000645DF File Offset: 0x000627DF
		// (set) Token: 0x06002386 RID: 9094 RVA: 0x000645E7 File Offset: 0x000627E7
		public string Query { get; private set; }

		// Token: 0x17000EAD RID: 3757
		// (get) Token: 0x06002387 RID: 9095 RVA: 0x000645F0 File Offset: 0x000627F0
		// (set) Token: 0x06002388 RID: 9096 RVA: 0x000645F8 File Offset: 0x000627F8
		public string ServiceUrl { get; private set; }

		// Token: 0x17000EAE RID: 3758
		// (get) Token: 0x06002389 RID: 9097 RVA: 0x00064601 File Offset: 0x00062801
		// (set) Token: 0x0600238A RID: 9098 RVA: 0x00064609 File Offset: 0x00062809
		public string SiteUrl { get; private set; }

		// Token: 0x17000EAF RID: 3759
		// (get) Token: 0x0600238B RID: 9099 RVA: 0x00064612 File Offset: 0x00062812
		// (set) Token: 0x0600238C RID: 9100 RVA: 0x0006461A File Offset: 0x0006281A
		public string SitePath { get; private set; }

		// Token: 0x17000EB0 RID: 3760
		// (get) Token: 0x0600238D RID: 9101 RVA: 0x00064623 File Offset: 0x00062823
		// (set) Token: 0x0600238E RID: 9102 RVA: 0x0006462B File Offset: 0x0006282B
		public string Url { get; private set; }

		// Token: 0x17000EB1 RID: 3761
		// (get) Token: 0x0600238F RID: 9103 RVA: 0x00064634 File Offset: 0x00062834
		// (set) Token: 0x06002390 RID: 9104 RVA: 0x0006463C File Offset: 0x0006283C
		public long? Top { get; private set; }

		// Token: 0x17000EB2 RID: 3762
		// (get) Token: 0x06002391 RID: 9105 RVA: 0x00064645 File Offset: 0x00062845
		// (set) Token: 0x06002392 RID: 9106 RVA: 0x0006464D File Offset: 0x0006284D
		public long? Skip { get; private set; }

		// Token: 0x17000EB3 RID: 3763
		// (get) Token: 0x06002393 RID: 9107 RVA: 0x00064656 File Offset: 0x00062856
		// (set) Token: 0x06002394 RID: 9108 RVA: 0x0006465E File Offset: 0x0006285E
		public string OrderBy { get; private set; }

		// Token: 0x06002395 RID: 9109 RVA: 0x00064668 File Offset: 0x00062868
		public static string AppendNodeToPath(string path, string node)
		{
			if (string.IsNullOrEmpty(node))
			{
				return path;
			}
			if (string.IsNullOrEmpty(path))
			{
				return node;
			}
			if (path[path.Length - 1] == '/' || node[0] == '/')
			{
				return path + node;
			}
			return path + "/" + node;
		}

		// Token: 0x06002396 RID: 9110 RVA: 0x000646BA File Offset: 0x000628BA
		private static string GetSuffix(SharePointApiVersion version)
		{
			if (version == SharePointApiVersion.SP15)
			{
				return "_api/web";
			}
			return "_vti_bin/ListData.svc";
		}

		// Token: 0x06002397 RID: 9111 RVA: 0x000646CC File Offset: 0x000628CC
		private static string GetEntity(string url, string suffix)
		{
			url = SharePointUrlBuilder.Unescape(new Uri(url).AbsolutePath).TrimEnd(FileHelper.DirectorySeparatorChars);
			int num = url.IndexOf(suffix, StringComparison.OrdinalIgnoreCase);
			if (num + suffix.Length < url.Length)
			{
				return url.Substring(num + suffix.Length).TrimStart(FileHelper.DirectorySeparatorChars);
			}
			return null;
		}

		// Token: 0x06002398 RID: 9112 RVA: 0x00064728 File Offset: 0x00062928
		private string GetFeedUrl(string url)
		{
			return SharePointUrlBuilder.Unescape(new UriBuilder(url)
			{
				Query = null
			}.Uri.AbsoluteUri).TrimEnd(FileHelper.DirectorySeparatorChars);
		}

		// Token: 0x06002399 RID: 9113 RVA: 0x00064750 File Offset: 0x00062950
		private string GetFeedUrl(string serviceUrl, string entity)
		{
			UriBuilder uriBuilder = new UriBuilder(serviceUrl);
			uriBuilder.Path = SharePointUrlBuilder.AppendNodeToPath(uriBuilder.Path, entity);
			return SharePointUrlBuilder.Unescape(uriBuilder.Uri.AbsoluteUri).TrimEnd(FileHelper.DirectorySeparatorChars);
		}

		// Token: 0x0600239A RID: 9114 RVA: 0x00064783 File Offset: 0x00062983
		private static string GetHostUrl(string url)
		{
			return SharePointUrlBuilder.Unescape(new Uri(url).GetLeftPart(UriPartial.Authority)).TrimEnd(FileHelper.DirectorySeparatorChars);
		}

		// Token: 0x0600239B RID: 9115 RVA: 0x000647A0 File Offset: 0x000629A0
		private static string GetPath(string url)
		{
			return SharePointQueryBuilder.GetPath(new UriBuilder(url).Query);
		}

		// Token: 0x0600239C RID: 9116 RVA: 0x000647B2 File Offset: 0x000629B2
		private static string GetQuery(string url)
		{
			return SharePointUrlBuilder.Unescape(new UriBuilder(url).Query).TrimStart(new char[] { '?' });
		}

		// Token: 0x0600239D RID: 9117 RVA: 0x000647D4 File Offset: 0x000629D4
		private static string GetServiceUrl(string url, string suffix)
		{
			int num = url.IndexOf(suffix, StringComparison.OrdinalIgnoreCase);
			return url.Substring(0, num + suffix.Length);
		}

		// Token: 0x0600239E RID: 9118 RVA: 0x000647FC File Offset: 0x000629FC
		private static string GetSiteUrl(string feedUrl, string suffix)
		{
			string text = SharePointUrlBuilder.Unescape(new Uri(feedUrl).AbsoluteUri).TrimEnd(FileHelper.DirectorySeparatorChars);
			int num = text.IndexOf(suffix, StringComparison.OrdinalIgnoreCase);
			return text.Substring(0, num).TrimEnd(FileHelper.DirectorySeparatorChars);
		}

		// Token: 0x0600239F RID: 9119 RVA: 0x0006483D File Offset: 0x00062A3D
		private static string GetSitePath(string siteUrl)
		{
			return new Uri(siteUrl).AbsolutePath.TrimEnd(FileHelper.DirectorySeparatorChars);
		}

		// Token: 0x060023A0 RID: 9120 RVA: 0x00064854 File Offset: 0x00062A54
		private static string GetUrl(string url, string suffix)
		{
			UriBuilder uriBuilder = new UriBuilder(ODataUriCommon.ConvertToUri(TextValue.New(url)));
			if (SharePointUrlBuilder.Unescape(uriBuilder.Path).IndexOf(suffix, StringComparison.OrdinalIgnoreCase) < 0)
			{
				uriBuilder.Path = SharePointUrlBuilder.AppendNodeToPath(uriBuilder.Path, suffix);
			}
			return SharePointUrlBuilder.Unescape(uriBuilder.Uri.AbsoluteUri).TrimEnd(FileHelper.DirectorySeparatorChars);
		}

		// Token: 0x060023A1 RID: 9121 RVA: 0x000648B3 File Offset: 0x00062AB3
		private static string GetUrl(string serviceUrl, string entity, string query)
		{
			UriBuilder uriBuilder = new UriBuilder(serviceUrl);
			uriBuilder.Path = SharePointUrlBuilder.AppendNodeToPath(uriBuilder.Path, entity);
			uriBuilder.Query = query;
			return SharePointUrlBuilder.Unescape(uriBuilder.Uri.AbsoluteUri).TrimEnd(FileHelper.DirectorySeparatorChars);
		}

		// Token: 0x060023A2 RID: 9122 RVA: 0x000648F0 File Offset: 0x00062AF0
		private static long? GetTop(string url)
		{
			string queryPart = new SharePointQueryBuilder(new UriBuilder(url).Query).GetQueryPart("$top");
			if (queryPart != null)
			{
				return new long?(long.Parse(queryPart, CultureInfo.InvariantCulture));
			}
			return null;
		}

		// Token: 0x060023A3 RID: 9123 RVA: 0x00064938 File Offset: 0x00062B38
		private static long? GetSkip(string url)
		{
			string queryPart = new SharePointQueryBuilder(new UriBuilder(url).Query).GetQueryPart("$skip");
			if (queryPart != null)
			{
				return new long?(long.Parse(queryPart, CultureInfo.InvariantCulture));
			}
			return null;
		}

		// Token: 0x060023A4 RID: 9124 RVA: 0x00064980 File Offset: 0x00062B80
		public SharePointUrlBuilder SetOrderBy(string fieldName)
		{
			SharePointUrlBuilder sharePointUrlBuilder = new SharePointUrlBuilder(this);
			SharePointQueryBuilder sharePointQueryBuilder = new SharePointQueryBuilder(sharePointUrlBuilder.Query);
			sharePointQueryBuilder.SetQueryPart("$orderby", fieldName);
			sharePointUrlBuilder.Query = sharePointQueryBuilder.Query;
			sharePointUrlBuilder.Url = SharePointUrlBuilder.GetUrl(sharePointUrlBuilder.ServiceUrl, sharePointUrlBuilder.Entity, sharePointUrlBuilder.Query);
			sharePointUrlBuilder.OrderBy = fieldName;
			return sharePointUrlBuilder;
		}

		// Token: 0x060023A5 RID: 9125 RVA: 0x000649E0 File Offset: 0x00062BE0
		public SharePointUrlBuilder SetPathFilter(string pathFilter)
		{
			SharePointUrlBuilder sharePointUrlBuilder = new SharePointUrlBuilder(this);
			SharePointQueryBuilder sharePointQueryBuilder = new SharePointQueryBuilder(sharePointUrlBuilder.Query);
			sharePointQueryBuilder.ReplacePathFilter(pathFilter);
			sharePointUrlBuilder.Url = SharePointUrlBuilder.GetUrl(sharePointUrlBuilder.ServiceUrl, sharePointUrlBuilder.Entity, sharePointQueryBuilder.Query);
			sharePointUrlBuilder.Path = SharePointUrlBuilder.GetPath(sharePointUrlBuilder.Url);
			sharePointUrlBuilder.Query = sharePointQueryBuilder.Query;
			return sharePointUrlBuilder;
		}

		// Token: 0x060023A6 RID: 9126 RVA: 0x00064A44 File Offset: 0x00062C44
		public SharePointUrlBuilder SetSkip(long count)
		{
			SharePointUrlBuilder sharePointUrlBuilder = new SharePointUrlBuilder(this);
			SharePointQueryBuilder sharePointQueryBuilder = new SharePointQueryBuilder(sharePointUrlBuilder.Query);
			long num = count;
			if (this.Skip != null)
			{
				num += this.Skip.Value;
			}
			long num2 = -1L;
			string queryPart = sharePointQueryBuilder.GetQueryPart("$top");
			if (queryPart != null)
			{
				long num3 = long.Parse(queryPart, CultureInfo.InvariantCulture);
				num2 = ((num3 > count) ? (num3 - count) : 0L);
			}
			string text = num.ToString(CultureInfo.InvariantCulture);
			sharePointQueryBuilder.SetQueryPart("$skip", text);
			if (num2 >= 0L)
			{
				string text2 = num2.ToString(CultureInfo.InvariantCulture);
				sharePointQueryBuilder.SetQueryPart("$top", text2);
			}
			sharePointUrlBuilder.Url = SharePointUrlBuilder.GetUrl(sharePointUrlBuilder.ServiceUrl, sharePointUrlBuilder.Entity, sharePointQueryBuilder.Query);
			sharePointUrlBuilder.Query = sharePointQueryBuilder.Query;
			return sharePointUrlBuilder;
		}

		// Token: 0x060023A7 RID: 9127 RVA: 0x00064B20 File Offset: 0x00062D20
		public SharePointUrlBuilder SetTop(long count)
		{
			SharePointUrlBuilder sharePointUrlBuilder = new SharePointUrlBuilder(this);
			SharePointQueryBuilder sharePointQueryBuilder = new SharePointQueryBuilder(sharePointUrlBuilder.Query);
			if (this.Top != null && this.Top.Value < count)
			{
				count = this.Top.Value;
			}
			string text = count.ToString(CultureInfo.InvariantCulture);
			sharePointQueryBuilder.SetQueryPart("$top", text);
			sharePointUrlBuilder.Url = SharePointUrlBuilder.GetUrl(sharePointUrlBuilder.ServiceUrl, sharePointUrlBuilder.Entity, sharePointQueryBuilder.Query);
			sharePointUrlBuilder.Query = sharePointQueryBuilder.Query;
			return sharePointUrlBuilder;
		}

		// Token: 0x060023A8 RID: 9128 RVA: 0x00064BB4 File Offset: 0x00062DB4
		public override string ToString()
		{
			return this.Url;
		}

		// Token: 0x060023A9 RID: 9129 RVA: 0x00064BBC File Offset: 0x00062DBC
		public static string Unescape(Uri uri)
		{
			return SharePointUrlBuilder.Unescape(uri.AbsoluteUri);
		}

		// Token: 0x060023AA RID: 9130 RVA: 0x00064BC9 File Offset: 0x00062DC9
		public static string Unescape(string urlPart)
		{
			return Uri.UnescapeDataString(urlPart).Replace("+", "%2B");
		}

		// Token: 0x04000E3E RID: 3646
		private const string SP14Suffix = "_vti_bin/ListData.svc";

		// Token: 0x04000E3F RID: 3647
		private const string SP15Suffix = "_api/web";

		// Token: 0x04000E40 RID: 3648
		private readonly string suffix;
	}
}
