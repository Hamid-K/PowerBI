using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020002D0 RID: 720
	public sealed class ReportUrl
	{
		// Token: 0x06001B0A RID: 6922 RVA: 0x0006C0A1 File Offset: 0x0006A2A1
		internal ReportUrl(ReportUrl renderUrl)
		{
			this.m_renderUrl = renderUrl;
		}

		// Token: 0x06001B0B RID: 6923 RVA: 0x0006C0B0 File Offset: 0x0006A2B0
		internal ReportUrl(ICatalogItemContext itemContext, string initialUrl)
		{
			this.m_pathUri = new Uri(ReportUrl.BuildPathUri(itemContext, initialUrl, null));
		}

		// Token: 0x06001B0C RID: 6924 RVA: 0x0006C0CC File Offset: 0x0006A2CC
		internal ReportUrl(ICatalogItemContext itemContext, string initialUrl, bool checkProtocol, NameValueCollection unparsedParameters)
		{
			ICatalogItemContext catalogItemContext;
			this.m_pathUri = new Uri(ReportUrl.BuildPathUri(itemContext, checkProtocol, initialUrl, unparsedParameters, out catalogItemContext));
			if (this.m_pathUri != null && string.CompareOrdinal(this.m_pathUri.Scheme, "mailto") == 0)
			{
				string absoluteUri = this.m_pathUri.AbsoluteUri;
			}
		}

		// Token: 0x06001B0D RID: 6925 RVA: 0x0006C128 File Offset: 0x0006A328
		internal static string BuildPathUri(ICatalogItemContext currentICatalogItemContext, string initialUrl, NameValueCollection unparsedParameters)
		{
			ICatalogItemContext catalogItemContext;
			return ReportUrl.BuildPathUri(currentICatalogItemContext, true, initialUrl, unparsedParameters, out catalogItemContext);
		}

		// Token: 0x06001B0E RID: 6926 RVA: 0x0006C140 File Offset: 0x0006A340
		internal static string BuildPathUri(ICatalogItemContext currentICatalogItemContext, bool checkProtocol, string initialUrl, NameValueCollection unparsedParameters, out ICatalogItemContext newContext)
		{
			newContext = null;
			if (currentICatalogItemContext == null)
			{
				return initialUrl;
			}
			string text = null;
			try
			{
				text = currentICatalogItemContext.CombineUrl(initialUrl, checkProtocol, unparsedParameters, out newContext);
			}
			catch (RSException)
			{
				throw;
			}
			catch (Exception)
			{
				throw new RenderingObjectModelException(ErrorCode.rsMalformattedURL, Array.Empty<object>());
			}
			if (!currentICatalogItemContext.IsSupportedProtocol(text, checkProtocol))
			{
				throw new RenderingObjectModelException(ErrorCode.rsUnsupportedURLProtocol, new object[] { text });
			}
			return text;
		}

		// Token: 0x06001B0F RID: 6927 RVA: 0x0006C1B8 File Offset: 0x0006A3B8
		internal static string BuildDrillthroughUrl(ICatalogItemContext currentCatalogItemContext, string initialUrl, NameValueCollection parameters)
		{
			ICatalogItemContext catalogItemContext;
			if (ReportUrl.BuildPathUri(currentCatalogItemContext, true, initialUrl, parameters, out catalogItemContext) == null || catalogItemContext == null)
			{
				return null;
			}
			CatalogItemUrlBuilder catalogItemUrlBuilder = new CatalogItemUrlBuilder(catalogItemContext, catalogItemContext.ReportParameterLookup);
			catalogItemUrlBuilder.AppendReportParameters(parameters);
			return new Uri(catalogItemUrlBuilder.ToString()).AbsoluteUri;
		}

		// Token: 0x06001B10 RID: 6928 RVA: 0x0006C1FC File Offset: 0x0006A3FC
		internal static ReportUrl BuildHyperlinkUrl(Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext, ObjectType objectType, string objectName, string propertyName, ICatalogItemContext itemContext, string initialUrl)
		{
			ReportUrl reportUrl = null;
			if (initialUrl == null)
			{
				return null;
			}
			bool flag = false;
			try
			{
				string text = initialUrl;
				if (ReportUrl.IsSupportedUrl(text))
				{
					bool flag3;
					bool flag2 = itemContext.IsReportServerPathOrUrl(text, false, out flag3);
					NameValueCollection nameValueCollection = null;
					if (flag2 && flag3)
					{
						itemContext.PathManager.ExtractFromUrl(text, out text, out nameValueCollection);
						if (text == null || text.Length == 0)
						{
							flag = true;
							text = null;
							reportUrl = null;
						}
					}
					if (text != null)
					{
						reportUrl = new ReportUrl(itemContext, text, false, nameValueCollection);
					}
				}
				else
				{
					text = null;
					flag = true;
				}
			}
			catch (ItemNotFoundException)
			{
				flag = true;
			}
			catch (RenderingObjectModelException)
			{
				flag = true;
			}
			catch (RSException)
			{
				throw;
			}
			catch (Exception)
			{
				flag = true;
			}
			if (flag && Microsoft.ReportingServices.Diagnostics.ProcessingContext.Configuration != null)
			{
				renderingContext.OdpContext.ErrorContext.Register(ProcessingErrorCode.rsInvalidURLProtocol, Severity.Warning, objectType, objectName, propertyName, new string[]
				{
					initialUrl,
					string.Join(", ", Microsoft.ReportingServices.Diagnostics.ProcessingContext.Configuration.SupportedHyperlinkSchemes)
				});
			}
			return reportUrl;
		}

		// Token: 0x06001B11 RID: 6929 RVA: 0x0006C2F4 File Offset: 0x0006A4F4
		internal static bool IsSupportedUrl(string url)
		{
			Uri uri;
			if (!Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out uri))
			{
				return false;
			}
			if (uri.IsAbsoluteUri && Microsoft.ReportingServices.Diagnostics.ProcessingContext.Configuration != null)
			{
				IEnumerable<string> supportedHyperlinkSchemes = Microsoft.ReportingServices.Diagnostics.ProcessingContext.Configuration.SupportedHyperlinkSchemes;
				return supportedHyperlinkSchemes.Contains("*", StringComparer.InvariantCultureIgnoreCase) || supportedHyperlinkSchemes.Contains(uri.Scheme, StringComparer.InvariantCultureIgnoreCase);
			}
			return true;
		}

		// Token: 0x17000F39 RID: 3897
		// (get) Token: 0x06001B12 RID: 6930 RVA: 0x0006C34F File Offset: 0x0006A54F
		private bool IsOldSnapshot
		{
			get
			{
				return this.m_renderUrl != null;
			}
		}

		// Token: 0x06001B13 RID: 6931 RVA: 0x0006C35A File Offset: 0x0006A55A
		public Uri ToUri()
		{
			if (this.IsOldSnapshot)
			{
				return this.m_renderUrl.ToUri();
			}
			return this.m_pathUri;
		}

		// Token: 0x06001B14 RID: 6932 RVA: 0x0006C376 File Offset: 0x0006A576
		public override string ToString()
		{
			if (this.IsOldSnapshot)
			{
				return this.m_renderUrl.ToString();
			}
			return this.m_pathUri.AbsoluteUri;
		}

		// Token: 0x04000D68 RID: 3432
		private ReportUrl m_renderUrl;

		// Token: 0x04000D69 RID: 3433
		private Uri m_pathUri;
	}
}
