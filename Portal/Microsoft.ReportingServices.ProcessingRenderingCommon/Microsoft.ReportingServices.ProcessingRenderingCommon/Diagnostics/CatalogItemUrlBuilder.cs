using System;
using System.Collections.Specialized;
using System.Text;
using Microsoft.ReportingServices.Common;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000058 RID: 88
	internal sealed class CatalogItemUrlBuilder
	{
		// Token: 0x06000260 RID: 608 RVA: 0x000097FC File Offset: 0x000079FC
		public static string NameValueCollectionToQueryString(NameValueCollection parameters)
		{
			if (parameters == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			for (int i = 0; i < parameters.Count; i++)
			{
				string key = parameters.GetKey(i);
				string[] values = parameters.GetValues(i);
				if (values == null || values.Length == 0)
				{
					if (!flag)
					{
						stringBuilder.Append("&");
					}
					flag = false;
					stringBuilder.Append(UrlUtil.UrlEncode(key));
				}
				else
				{
					for (int j = 0; j < values.Length; j++)
					{
						if (!flag)
						{
							stringBuilder.Append("&");
						}
						flag = false;
						stringBuilder.Append(UrlUtil.UrlEncode(key));
						stringBuilder.Append("=");
						stringBuilder.Append(UrlUtil.UrlEncode(values[j]));
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000261 RID: 609 RVA: 0x000098BB File Offset: 0x00007ABB
		private CatalogItemUrlBuilder(IPathManager pathManager)
		{
			this.m_pathManager = pathManager;
		}

		// Token: 0x06000262 RID: 610 RVA: 0x000098CA File Offset: 0x00007ACA
		public CatalogItemUrlBuilder(string urlString)
		{
			this.m_urlString = new StringBuilder(urlString);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x000098DE File Offset: 0x00007ADE
		public CatalogItemUrlBuilder(ICatalogItemContext ctx)
			: this(ctx, false)
		{
		}

		// Token: 0x06000264 RID: 612 RVA: 0x000098E8 File Offset: 0x00007AE8
		public CatalogItemUrlBuilder(ICatalogItemContext ctx, IReportParameterLookup paramLookup)
			: this(ctx, false)
		{
			this.m_paramLookup = paramLookup;
		}

		// Token: 0x06000265 RID: 613 RVA: 0x000098F9 File Offset: 0x00007AF9
		public CatalogItemUrlBuilder(ICatalogItemContext ctx, bool isFolder)
		{
			this.m_pathTranslator = ctx.PathTranslator;
			this.m_pathManager = ctx.PathManager;
			this.Construct(ctx.HostRootUri, ctx.HostSpecificItemPath, false, true, isFolder);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000992E File Offset: 0x00007B2E
		public static CatalogItemUrlBuilder CreateNonServerBuilder(string serverVirtualFolderUrl, string itemPath, bool alreadyEscaped, bool addItemPathAsQuery)
		{
			CatalogItemUrlBuilder catalogItemUrlBuilder = new CatalogItemUrlBuilder(RSPathUtil.Instance);
			catalogItemUrlBuilder.Construct(serverVirtualFolderUrl, itemPath, alreadyEscaped, addItemPathAsQuery, false);
			return catalogItemUrlBuilder;
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00009945 File Offset: 0x00007B45
		private void Construct(string serverVirtualFolderUrl, string itemPath, bool alreadyEscaped, bool addItemPathAsQuery, bool isFolder)
		{
			this.m_urlString = this.m_pathManager.ConstructUrlBuilder(this.m_pathTranslator, serverVirtualFolderUrl, itemPath, alreadyEscaped, addItemPathAsQuery, isFolder);
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00009965 File Offset: 0x00007B65
		public override string ToString()
		{
			return this.m_urlString.ToString();
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00009974 File Offset: 0x00007B74
		public void AppendUnparsedParameters(NameValueCollection parameters)
		{
			for (int i = 0; i < parameters.Count; i++)
			{
				string key = parameters.GetKey(i);
				string[] values = parameters.GetValues(i);
				if (key != null)
				{
					if (values != null)
					{
						for (int j = 0; j < values.Length; j++)
						{
							this.AppendOneParameter(string.Empty, key, values[j], false);
						}
					}
					else
					{
						this.AppendOneParameter(string.Empty, key, null, false);
					}
				}
			}
		}

		// Token: 0x0600026A RID: 618 RVA: 0x000099D8 File Offset: 0x00007BD8
		public void AppendReportParameter(string name, string val)
		{
			if (!this.ReplaceParametersWithExecParameterId(new NameValueCollection { { name, val } }))
			{
				this.AppendOneParameter(CatalogItemUrlBuilder.EncodedReportParameterPrefix, name, val);
			}
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00009A09 File Offset: 0x00007C09
		private void InternalAppendReportParameters(NameValueCollection parameters)
		{
			this.AppendParameterCollection(CatalogItemUrlBuilder.EncodedReportParameterPrefix, parameters);
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00009A17 File Offset: 0x00007C17
		public void AppendReportParameters(NameValueCollection parameters)
		{
			if (!this.ReplaceParametersWithExecParameterId(parameters))
			{
				this.InternalAppendReportParameters(parameters);
			}
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00009A2C File Offset: 0x00007C2C
		private bool ReplaceParametersWithExecParameterId(NameValueCollection parameters)
		{
			string text = null;
			if (this.m_paramLookup != null && parameters != null)
			{
				text = this.m_paramLookup.GetReportParamsInstanceId(parameters);
			}
			if (text != null)
			{
				this.AppendCatalogParameter("StoredParametersID", text);
				return true;
			}
			return false;
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00009A65 File Offset: 0x00007C65
		public void AppendRenderingParameter(string name, string val)
		{
			this.AppendOneParameter(CatalogItemUrlBuilder.EncodedRenderingParameterPrefix, name, val);
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00009A74 File Offset: 0x00007C74
		public void AppendRenderingParameters(NameValueCollection parameters)
		{
			this.AppendParameterCollection(CatalogItemUrlBuilder.EncodedRenderingParameterPrefix, parameters);
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00009A82 File Offset: 0x00007C82
		public void AppendCatalogParameter(string name, string val)
		{
			this.AppendOneParameter(CatalogItemUrlBuilder.EncodedCatalogParameterPrefix, name, val);
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00009A91 File Offset: 0x00007C91
		public void AppendCatalogParameters(NameValueCollection parameters)
		{
			this.AppendParameterCollection(CatalogItemUrlBuilder.EncodedCatalogParameterPrefix, parameters);
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00009AA0 File Offset: 0x00007CA0
		private void AppendParameterCollection(string encodedPrefix, NameValueCollection parameters)
		{
			if (parameters != null)
			{
				for (int i = 0; i < parameters.Count; i++)
				{
					string key = parameters.GetKey(i);
					string[] values = parameters.GetValues(i);
					if (values == null)
					{
						this.AppendOneParameter(encodedPrefix, key, null);
					}
					else
					{
						for (int j = 0; j < values.Length; j++)
						{
							this.AppendOneParameter(encodedPrefix, key, values[j]);
						}
					}
				}
			}
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00009AF8 File Offset: 0x00007CF8
		private void AppendOneParameter(string encodedPrefix, string name, string val)
		{
			this.AppendOneParameter(encodedPrefix, name, val, true);
		}

		// Token: 0x06000274 RID: 628 RVA: 0x00009B04 File Offset: 0x00007D04
		private void AppendOneParameter(string encodedPrefix, string name, string val, bool addNullSuffix)
		{
			this.m_urlString.Append('&');
			if (val != null)
			{
				this.m_urlString.Append(encodedPrefix);
				this.m_urlString.Append(CatalogItemUrlBuilder.EncodeUrlParameter(name));
				this.m_urlString.Append("=");
				this.m_urlString.Append(CatalogItemUrlBuilder.EncodeUrlParameter(val));
				return;
			}
			this.m_urlString.Append(encodedPrefix);
			this.m_urlString.Append(CatalogItemUrlBuilder.EncodeUrlParameter(name));
			if (addNullSuffix)
			{
				this.m_urlString.Append(CatalogItemUrlBuilder.EncodedParameterNullSuffix);
			}
			this.m_urlString.Append("=");
			this.m_urlString.Append(bool.TrueString);
		}

		// Token: 0x06000275 RID: 629 RVA: 0x00009BBB File Offset: 0x00007DBB
		private static string EncodeUrlParameter(string param)
		{
			return UrlUtil.UrlEncode(param).Replace("'", "%27");
		}

		// Token: 0x04000148 RID: 328
		private StringBuilder m_urlString;

		// Token: 0x04000149 RID: 329
		private IReportParameterLookup m_paramLookup;

		// Token: 0x0400014A RID: 330
		private IPathTranslator m_pathTranslator;

		// Token: 0x0400014B RID: 331
		private IPathManager m_pathManager;

		// Token: 0x0400014C RID: 332
		private static readonly string EncodedParameterNullSuffix = UrlUtil.UrlEncode(":isnull");

		// Token: 0x0400014D RID: 333
		private static readonly string EncodedCatalogParameterPrefix = UrlUtil.UrlEncode("rs:");

		// Token: 0x0400014E RID: 334
		private static readonly string EncodedRenderingParameterPrefix = UrlUtil.UrlEncode("rc:");

		// Token: 0x0400014F RID: 335
		private static readonly string EncodedReportParameterPrefix = UrlUtil.UrlEncode("");

		// Token: 0x04000150 RID: 336
		private static readonly string EncodedUserNameParameterPrefix = UrlUtil.UrlEncode("dsu:");
	}
}
