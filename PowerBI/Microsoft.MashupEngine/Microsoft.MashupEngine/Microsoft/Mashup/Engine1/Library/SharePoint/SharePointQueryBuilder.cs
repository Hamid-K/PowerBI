using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Text;
using System.Web;
using Microsoft.Mashup.Engine1.Library.File;
using Microsoft.Mashup.Engine1.Library.Uris;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.SharePoint
{
	// Token: 0x02000403 RID: 1027
	internal sealed class SharePointQueryBuilder
	{
		// Token: 0x060022EE RID: 8942 RVA: 0x000616D4 File Offset: 0x0005F8D4
		public SharePointQueryBuilder(string query)
		{
			query = ((query == null) ? string.Empty : SharePointUrlBuilder.Unescape(query));
			NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(query);
			this.queryParts = new Dictionary<string, string>(nameValueCollection.Count);
			foreach (string text in nameValueCollection.AllKeys)
			{
				if (string.IsNullOrEmpty(text))
				{
					throw ValueException.NewExpressionError<Message0>(Strings.InvalidQueryParameter, null, null);
				}
				this.queryParts.Add(text, SharePointUrlBuilder.Unescape(nameValueCollection[text]));
			}
		}

		// Token: 0x17000E98 RID: 3736
		// (get) Token: 0x060022EF RID: 8943 RVA: 0x0006175C File Offset: 0x0005F95C
		public string Query
		{
			get
			{
				string text = string.Empty;
				foreach (KeyValuePair<string, string> keyValuePair in this.queryParts)
				{
					text = UriHelper.AddQueryPart(text, keyValuePair.Key, keyValuePair.Value);
				}
				return text;
			}
		}

		// Token: 0x060022F0 RID: 8944 RVA: 0x000617C0 File Offset: 0x0005F9C0
		public void AddQueryPartValue(string partKey, string partValue)
		{
			if (string.IsNullOrEmpty(partValue))
			{
				return;
			}
			string queryPart = this.GetQueryPart(partKey);
			this.SetQueryPart(partKey, (queryPart == null) ? partValue : SharePointQueryCompiler.WriteBinary("and", queryPart, partValue));
		}

		// Token: 0x060022F1 RID: 8945 RVA: 0x000617F8 File Offset: 0x0005F9F8
		public string GetQueryPart(string partKey)
		{
			if (string.IsNullOrEmpty(partKey))
			{
				return null;
			}
			string text;
			if (this.TryGetQueryPart(partKey, out text))
			{
				return text;
			}
			return null;
		}

		// Token: 0x060022F2 RID: 8946 RVA: 0x00061820 File Offset: 0x0005FA20
		public void ReplacePathFilter(string pathFilter)
		{
			string text;
			if (!this.TryGetQueryPart("$filter", out text))
			{
				this.SetQueryPart("$filter", pathFilter);
				return;
			}
			int num = text.IndexOf("(startswith(Path,", StringComparison.OrdinalIgnoreCase);
			if (num >= 0)
			{
				string text2 = SharePointQueryCompiler.GetFirstQuotedTextLiteral(text.Substring(num));
				int num2 = "(startswith(Path,{0}) and length(Path) eq {1})".Length + text2.Length - 3;
				int num3 = text.IndexOf("startswith(Path,", StringComparison.OrdinalIgnoreCase);
				if (num3 > 0)
				{
					text2 = SharePointQueryCompiler.GetFirstQuotedTextLiteral(text.Substring(num3));
					num2 += " or ".Length + "startswith(Path,{0})".Length + text2.Length - 3;
				}
				StringBuilder stringBuilder = new StringBuilder();
				if (num > 0)
				{
					stringBuilder.Append(text.Substring(0, num));
				}
				stringBuilder.Append(pathFilter);
				if (num + num2 < text.Length)
				{
					stringBuilder.Append(text.Substring(num + num2));
				}
				this.SetQueryPart("$filter", stringBuilder.ToString());
				return;
			}
			this.AddQueryPartValue("$filter", pathFilter);
		}

		// Token: 0x060022F3 RID: 8947 RVA: 0x00061922 File Offset: 0x0005FB22
		public void SetQueryPart(string partKey, string partValue)
		{
			if (string.IsNullOrEmpty(partKey))
			{
				return;
			}
			if (string.IsNullOrEmpty(partValue))
			{
				this.queryParts.Remove(partKey);
				return;
			}
			this.queryParts[partKey] = partValue;
		}

		// Token: 0x060022F4 RID: 8948 RVA: 0x00061950 File Offset: 0x0005FB50
		public bool TryGetQueryPart(string partKey, out string partValue)
		{
			return this.queryParts.TryGetValue(partKey, out partValue);
		}

		// Token: 0x060022F5 RID: 8949 RVA: 0x0006195F File Offset: 0x0005FB5F
		public override string ToString()
		{
			return this.Query;
		}

		// Token: 0x060022F6 RID: 8950 RVA: 0x00061968 File Offset: 0x0005FB68
		public static string CreatePathFilter(string path, FileHelper.FolderOptions options)
		{
			if (string.IsNullOrEmpty(path))
			{
				return null;
			}
			string text = SharePointQueryCompiler.QuotedText(path);
			if (FileHelper.EnumerateDeep(options))
			{
				string text2 = SharePointQueryCompiler.QuotedText(path + "/");
				return SharePointQueryBuilder.CreateEqualsWorkaround(text) + string.Format(CultureInfo.InvariantCulture, " or startswith(Path,{0})", text2);
			}
			return SharePointQueryBuilder.CreateEqualsWorkaround(text);
		}

		// Token: 0x060022F7 RID: 8951 RVA: 0x000619C4 File Offset: 0x0005FBC4
		public static string CreateEqualsWorkaround(string quotedPath)
		{
			int length = SharePointQueryCompiler.UnquotedText(quotedPath).Length;
			return string.Format(CultureInfo.InvariantCulture, "(startswith(Path,{0}) and length(Path) eq {1})", quotedPath, length);
		}

		// Token: 0x060022F8 RID: 8952 RVA: 0x000619F4 File Offset: 0x0005FBF4
		public static string GetPath(string query)
		{
			string text;
			if (SharePointQueryBuilder.TryGetQueryPart(query, "$filter", out text))
			{
				int num = text.IndexOf("(startswith(Path,", StringComparison.OrdinalIgnoreCase);
				if (num >= 0)
				{
					return SharePointQueryCompiler.UnquotedText(SharePointQueryCompiler.GetFirstQuotedTextLiteral(text.Substring(num)));
				}
			}
			return null;
		}

		// Token: 0x060022F9 RID: 8953 RVA: 0x00061A34 File Offset: 0x0005FC34
		public static string SetQueryPart(string query, string partKey, string partValue)
		{
			SharePointQueryBuilder sharePointQueryBuilder = new SharePointQueryBuilder(query);
			sharePointQueryBuilder.SetQueryPart(partKey, partValue);
			return sharePointQueryBuilder.Query;
		}

		// Token: 0x060022FA RID: 8954 RVA: 0x00061A49 File Offset: 0x0005FC49
		public static bool TryGetQueryPart(string query, string partKey, out string partValue)
		{
			return new SharePointQueryBuilder(query).TryGetQueryPart(partKey, out partValue);
		}

		// Token: 0x04000DE9 RID: 3561
		public const string PathKey = "Path";

		// Token: 0x04000DEA RID: 3562
		private const int FormatPlaceHolderLength = 3;

		// Token: 0x04000DEB RID: 3563
		private const string AndText = "and";

		// Token: 0x04000DEC RID: 3564
		private const string FormattedEqualsText = " eq ";

		// Token: 0x04000DED RID: 3565
		private const string FormattedOrText = " or ";

		// Token: 0x04000DEE RID: 3566
		private const string StartsWithText = "startswith";

		// Token: 0x04000DEF RID: 3567
		private const string PathDelimiter = "'";

		// Token: 0x04000DF0 RID: 3568
		private const string PathEqualsFilterPrefix = "(startswith(Path,";

		// Token: 0x04000DF1 RID: 3569
		private const string PathEqualsFilterFormat = "(startswith(Path,{0}) and length(Path) eq {1})";

		// Token: 0x04000DF2 RID: 3570
		private const string PathStartsWithFilterPrefix = "startswith(Path,";

		// Token: 0x04000DF3 RID: 3571
		private const string PathStartsWithFilterFormat = "startswith(Path,{0})";

		// Token: 0x04000DF4 RID: 3572
		private const string PathRecursiveFilterFormat = " or startswith(Path,{0})";

		// Token: 0x04000DF5 RID: 3573
		private readonly IDictionary<string, string> queryParts;
	}
}
