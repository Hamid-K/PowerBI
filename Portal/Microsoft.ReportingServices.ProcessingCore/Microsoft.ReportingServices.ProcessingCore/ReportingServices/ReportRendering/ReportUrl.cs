using System;
using System.Collections.Specialized;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x0200001B RID: 27
	public sealed class ReportUrl
	{
		// Token: 0x060003AB RID: 939 RVA: 0x0000952C File Offset: 0x0000772C
		internal ReportUrl(ICatalogItemContext catContext, string initialUrl)
		{
			this.m_pathUri = new Uri(ReportUrl.BuildPathUri(catContext, initialUrl, null, out this.m_newICatalogItemContext));
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0000954D File Offset: 0x0000774D
		internal ReportUrl(RenderingContext reportContext, string initialUrl)
		{
			this.m_reportContext = reportContext;
			this.m_pathUri = new Uri(ReportUrl.BuildPathUri(reportContext.TopLevelReportContext, initialUrl, null, out this.m_newICatalogItemContext));
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0000957C File Offset: 0x0000777C
		internal ReportUrl(RenderingContext reportContext, string initialUrl, bool checkProtocol, NameValueCollection unparsedParameters, bool useReplacementRoot)
		{
			this.m_reportContext = reportContext;
			this.m_pathUri = new Uri(ReportUrl.BuildPathUri(reportContext.TopLevelReportContext, checkProtocol, initialUrl, unparsedParameters, out this.m_newICatalogItemContext));
			bool flag;
			if (useReplacementRoot && reportContext.TopLevelReportContext.IsReportServerPathOrUrl(this.m_pathUri.AbsoluteUri, checkProtocol, out flag))
			{
				this.m_replacementRoot = reportContext.ReplacementRoot;
			}
		}

		// Token: 0x060003AE RID: 942 RVA: 0x000095E1 File Offset: 0x000077E1
		internal static string BuildPathUri(ICatalogItemContext currentICatalogItemContext, string initialUrl, NameValueCollection unparsedParameters, out ICatalogItemContext newContext)
		{
			return ReportUrl.BuildPathUri(currentICatalogItemContext, true, initialUrl, unparsedParameters, out newContext);
		}

		// Token: 0x060003AF RID: 943 RVA: 0x000095F0 File Offset: 0x000077F0
		internal static string BuildPathUri(ICatalogItemContext currentCatalogItemContext, bool checkProtocol, string initialUrl, NameValueCollection unparsedParameters, out ICatalogItemContext newContext)
		{
			newContext = null;
			if (currentCatalogItemContext == null)
			{
				return initialUrl;
			}
			string text = null;
			try
			{
				text = currentCatalogItemContext.CombineUrl(initialUrl, checkProtocol, unparsedParameters, out newContext);
			}
			catch (UriFormatException)
			{
				throw new RenderingObjectModelException(ErrorCode.rsMalformattedURL, Array.Empty<object>());
			}
			if (!currentCatalogItemContext.IsSupportedProtocol(text, checkProtocol))
			{
				throw new RenderingObjectModelException(ErrorCode.rsUnsupportedURLProtocol, new object[] { text });
			}
			return text;
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00009658 File Offset: 0x00007858
		internal static ReportUrl BuildHyperLinkURL(string hyperLinkUrlValue, RenderingContext renderingContext)
		{
			ReportUrl reportUrl = null;
			try
			{
				if (hyperLinkUrlValue != null)
				{
					bool flag;
					if (renderingContext.TopLevelReportContext.IsReportServerPathOrUrl(hyperLinkUrlValue, false, out flag) && flag)
					{
						NameValueCollection nameValueCollection;
						renderingContext.TopLevelReportContext.PathManager.ExtractFromUrl(hyperLinkUrlValue, out hyperLinkUrlValue, out nameValueCollection);
						if (hyperLinkUrlValue == null || hyperLinkUrlValue.Length == 0)
						{
							return null;
						}
						reportUrl = new ReportUrl(renderingContext, hyperLinkUrlValue, false, nameValueCollection, true);
					}
					else
					{
						reportUrl = new ReportUrl(renderingContext, hyperLinkUrlValue, false, null, true);
					}
				}
			}
			catch
			{
				return null;
			}
			return reportUrl;
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x000096D4 File Offset: 0x000078D4
		public override string ToString()
		{
			return this.m_pathUri.AbsoluteUri;
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x000096E4 File Offset: 0x000078E4
		public Uri ToUri()
		{
			Uri uri = this.m_pathUri;
			if (this.m_replacementRoot != null)
			{
				uri = new ReportUrlBuilder(this.m_reportContext, this.m_pathUri.AbsoluteUri, this.m_replacementRoot).ToUri();
			}
			return uri;
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x00009723 File Offset: 0x00007923
		public ReportUrlBuilder GetUrlBuilder(string initialUrl, bool useReplacementRoot)
		{
			return new ReportUrlBuilder(this.m_reportContext, this.m_newICatalogItemContext, initialUrl, useReplacementRoot ? this.m_replacementRoot : null);
		}

		// Token: 0x0400006D RID: 109
		private Uri m_pathUri;

		// Token: 0x0400006E RID: 110
		private string m_replacementRoot;

		// Token: 0x0400006F RID: 111
		private RenderingContext m_reportContext;

		// Token: 0x04000070 RID: 112
		private ICatalogItemContext m_newICatalogItemContext;
	}
}
