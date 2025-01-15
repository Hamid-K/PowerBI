using System;
using System.Collections.Specialized;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x0200001C RID: 28
	public sealed class ReportUrlBuilder
	{
		// Token: 0x060003B4 RID: 948 RVA: 0x00009744 File Offset: 0x00007944
		internal ReportUrlBuilder(RenderingContext reportContext, ICatalogItemContext changedContext, string initialUrl, string replacementRoot)
		{
			ICatalogItemContext catalogItemContext;
			if (changedContext != null)
			{
				catalogItemContext = changedContext;
			}
			else
			{
				catalogItemContext = reportContext.TopLevelReportContext;
			}
			ICatalogItemContext catalogItemContext2;
			ReportUrl.BuildPathUri(catalogItemContext, initialUrl, null, out catalogItemContext2);
			this.m_catalogItemUrlBuilder = new CatalogItemUrlBuilder(catalogItemContext2, catalogItemContext2.ReportParameterLookup);
			this.m_replacementRoot = replacementRoot;
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x00009794 File Offset: 0x00007994
		internal ReportUrlBuilder(RenderingContext reportContext, string initialUrl, string replacementRoot)
		{
			ICatalogItemContext topLevelReportContext = reportContext.TopLevelReportContext;
			ICatalogItemContext catalogItemContext;
			ReportUrl.BuildPathUri(topLevelReportContext, initialUrl, null, out catalogItemContext);
			this.m_catalogItemUrlBuilder = new CatalogItemUrlBuilder(topLevelReportContext, topLevelReportContext.ReportParameterLookup);
			this.m_replacementRoot = replacementRoot;
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x000097DC File Offset: 0x000079DC
		internal ReportUrlBuilder(RenderingContext reportContext, string initialUrl, bool useReplacementRoot, bool addReportParameters)
		{
			ICatalogItemContext topLevelReportContext = reportContext.TopLevelReportContext;
			ICatalogItemContext catalogItemContext;
			string text = ReportUrl.BuildPathUri(topLevelReportContext, initialUrl, null, out catalogItemContext);
			this.m_catalogItemUrlBuilder = new CatalogItemUrlBuilder(topLevelReportContext, topLevelReportContext.ReportParameterLookup);
			if (addReportParameters)
			{
				this.m_catalogItemUrlBuilder.AppendReportParameters(reportContext.TopLevelReportContext.RSRequestParameters.ReportParameters);
			}
			this.m_useRepacementRoot = useReplacementRoot;
			bool flag;
			if (reportContext != null && reportContext.TopLevelReportContext.IsReportServerPathOrUrl(text, true, out flag))
			{
				this.m_replacementRoot = reportContext.ReplacementRoot;
			}
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x00009860 File Offset: 0x00007A60
		public override string ToString()
		{
			return this.m_catalogItemUrlBuilder.ToString();
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x00009870 File Offset: 0x00007A70
		public Uri ToUri()
		{
			string text;
			if (this.m_replacementRoot != null)
			{
				if (this.m_useRepacementRoot)
				{
					this.AddReplacementRoot();
					text = this.m_replacementRoot + UrlUtil.UrlEncode(this.m_catalogItemUrlBuilder.ToString());
				}
				else
				{
					text = this.m_catalogItemUrlBuilder.ToString();
				}
			}
			else
			{
				text = this.m_catalogItemUrlBuilder.ToString();
			}
			return new Uri(text);
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x000098D1 File Offset: 0x00007AD1
		public void AddReplacementRoot()
		{
			if (!this.m_hasReplacement)
			{
				this.m_hasReplacement = true;
				if (this.m_replacementRoot != null)
				{
					this.m_catalogItemUrlBuilder.AppendRenderingParameter("ReplacementRoot", this.m_replacementRoot);
				}
			}
		}

		// Token: 0x060003BA RID: 954 RVA: 0x00009900 File Offset: 0x00007B00
		public void AddParameters(NameValueCollection urlParameters, UrlParameterType parameterType)
		{
			switch (parameterType)
			{
			case UrlParameterType.ServerParameter:
				this.m_catalogItemUrlBuilder.AppendCatalogParameters(urlParameters);
				return;
			case UrlParameterType.ReportParameter:
				this.m_catalogItemUrlBuilder.AppendReportParameters(urlParameters);
				return;
			case UrlParameterType.RenderingParameter:
				this.m_catalogItemUrlBuilder.AppendRenderingParameters(urlParameters);
				return;
			default:
				return;
			}
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0000993B File Offset: 0x00007B3B
		public void AddParameter(string name, string val, UrlParameterType parameterType)
		{
			switch (parameterType)
			{
			case UrlParameterType.ServerParameter:
				this.m_catalogItemUrlBuilder.AppendCatalogParameter(name, val);
				return;
			case UrlParameterType.ReportParameter:
				this.m_catalogItemUrlBuilder.AppendReportParameter(name, val);
				return;
			case UrlParameterType.RenderingParameter:
				this.m_catalogItemUrlBuilder.AppendRenderingParameter(name, val);
				return;
			default:
				return;
			}
		}

		// Token: 0x04000071 RID: 113
		private string m_replacementRoot;

		// Token: 0x04000072 RID: 114
		private CatalogItemUrlBuilder m_catalogItemUrlBuilder;

		// Token: 0x04000073 RID: 115
		private bool m_hasReplacement;

		// Token: 0x04000074 RID: 116
		private bool m_useRepacementRoot = true;
	}
}
