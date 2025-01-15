using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ReportingServices.Editions;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000054 RID: 84
	internal static class KnownExtensions
	{
		// Token: 0x0600024A RID: 586 RVA: 0x00008C8C File Offset: 0x00006E8C
		static KnownExtensions()
		{
			KnownExtensions.m_knownDataExtensions.Add("Microsoft.ReportingServices.DataExtensions.SqlConnectionWrapper,Microsoft.ReportingServices.DataExtensions".ToUpperInvariant());
			KnownExtensions.m_knownDataExtensions.Add("Microsoft.ReportingServices.DataExtensions.SqlAzureConnectionWrapper,Microsoft.ReportingServices.DataExtensions".ToUpperInvariant());
			KnownExtensions.m_knownDataExtensions.Add("Microsoft.ReportingServices.DataExtensions.OleDbConnectionWrapper,Microsoft.ReportingServices.DataExtensions".ToUpperInvariant());
			KnownExtensions.m_knownDataExtensions.Add("Microsoft.ReportingServices.DataExtensions.AdoMdConnection,Microsoft.ReportingServices.DataExtensions".ToUpperInvariant());
			KnownExtensions.m_knownDataExtensions.Add("Microsoft.ReportingServices.DataExtensions.OracleClientConnectionWrapper,Microsoft.ReportingServices.DataExtensions".ToUpperInvariant());
			KnownExtensions.m_knownDataExtensions.Add("Microsoft.ReportingServices.DataExtensions.SqlDwConnectionWrapper,Microsoft.ReportingServices.DataExtensions".ToUpperInvariant());
			KnownExtensions.m_knownDataExtensions.Add("Microsoft.ReportingServices.DataExtensions.OdbcConnectionWrapper,Microsoft.ReportingServices.DataExtensions".ToUpperInvariant());
			KnownExtensions.m_knownDataExtensions.Add("Microsoft.ReportingServices.DataExtensions.XmlDPConnection,Microsoft.ReportingServices.DataExtensions".ToUpperInvariant());
			KnownExtensions.m_knownDataExtensions.Add("Microsoft.SqlServer.Dts.DtsClient.DtsConnection,Microsoft.SqlServer.Dts.DtsClient".ToUpperInvariant());
			KnownExtensions.m_knownDataExtensions.Add("Microsoft.SqlServer.SapManagedProvider.SAPConnection,Microsoft.SqlServer.SapManagedProvider".ToUpperInvariant());
			KnownExtensions.m_knownDataExtensions.Add("Microsoft.ReportingServices.SemanticQueryEngine.SemanticQueryConnection,Microsoft.ReportingServices.SemanticQueryEngine".ToUpperInvariant());
			KnownExtensions.m_knownDataExtensions.Add("Microsoft.ReportingServices.DataExtensions.SharePointList.SPListConnection,Microsoft.ReportingServices.DataExtensions".ToUpperInvariant());
			KnownExtensions.m_knownAuthenticationExtensions.Add("Microsoft.ReportingServices.Authentication.WindowsAuthentication,Microsoft.ReportingServices.Authorization".ToUpperInvariant());
			KnownExtensions.m_knownAuthorizationExtensions.Add("Microsoft.ReportingServices.Authorization.WindowsAuthorization,Microsoft.ReportingServices.Authorization".ToUpperInvariant());
			KnownExtensions.m_knownSQExtensions.Add("Microsoft.ReportingServices.SemanticQueryEngine.Sql.MSSQL.MSSqlSQCommand,Microsoft.ReportingServices.SemanticQueryEngine".ToUpperInvariant());
			KnownExtensions.m_knownSQExtensions.Add("Microsoft.ReportingServices.SemanticQueryEngine.Sql.MSSQLADW.MSSqlAdwSQCommand,Microsoft.ReportingServices.SemanticQueryEngine".ToUpperInvariant());
			KnownExtensions.m_knownSQExtensions.Add("Microsoft.ReportingServices.SemanticQueryEngine.Sql.Oracle.OraSqlSQCommand,Microsoft.ReportingServices.SemanticQueryEngine".ToUpperInvariant());
			KnownExtensions.m_knownSQExtensions.Add("Microsoft.AnalysisServices.Modeling.QueryExecution.ASSemanticQueryCommand,Microsoft.AnalysisServices.Modeling".ToUpperInvariant());
			KnownExtensions.m_knownModelGenExtensions.Add("Microsoft.ReportingServices.SemanticQueryEngine.Sql.MSSQL.MsSqlModelGenerator,Microsoft.ReportingServices.SemanticQueryEngine".ToUpperInvariant());
			KnownExtensions.m_knownModelGenExtensions.Add("Microsoft.ReportingServices.SemanticQueryEngine.Sql.MSSQLADW.MsSqlAdwModelGenerator,Microsoft.ReportingServices.SemanticQueryEngine".ToUpperInvariant());
			KnownExtensions.m_knownModelGenExtensions.Add("Microsoft.ReportingServices.SemanticQueryEngine.Sql.Oracle.OraSqlModelGenerator,Microsoft.ReportingServices.SemanticQueryEngine".ToUpperInvariant());
			KnownExtensions.m_knownModelGenExtensions.Add("Microsoft.AnalysisServices.Modeling.Generation.ModelGeneratorExtention,Microsoft.AnalysisServices.Modeling".ToUpperInvariant());
			KnownExtensions.m_knownDeliveryExtensions.Add("Microsoft.ReportingServices.FileShareDeliveryProvider.FileShareProvider,ReportingServicesFileShareDeliveryProvider".ToUpperInvariant());
			KnownExtensions.m_knownDeliveryExtensions.Add("Microsoft.ReportingServices.PowerBIDeliveryProvider.PowerBIDeliveryProvider,ReportingServicesPowerBIDeliveryProvider".ToUpperInvariant());
			KnownExtensions.m_knownDeliveryExtensions.Add("Microsoft.ReportingServices.EmailDeliveryProvider.EmailProvider,ReportingServicesEmailDeliveryProvider".ToUpperInvariant());
			KnownExtensions.m_knownDeliveryExtensions.Add("Microsoft.ReportingServices.SharePoint.SharePointDeliveryExtension.DocumentLibraryProvider,ReportingServicesSharePointDeliveryExtension".ToUpperInvariant());
			KnownExtensions.m_knownDeliveryExtensions.Add("Microsoft.ReportingServices.NullDeliveryProvider.NullProvider,ReportingServicesNullDeliveryProvider".ToUpperInvariant());
			KnownExtensions.m_knownRenderingExtensions.Add("Microsoft.ReportingServices.Rendering.XmlDataRenderer.XmlDataReport,Microsoft.ReportingServices.XmlRendering".ToUpperInvariant());
			KnownExtensions.m_knownRenderingExtensions.Add("Microsoft.ReportingServices.Rendering.NullRenderer.NullReport,Microsoft.ReportingServices.NullRendering".ToUpperInvariant());
			KnownExtensions.m_knownRenderingExtensions.Add("Microsoft.ReportingServices.Rendering.CsvRenderer.CsvReport,Microsoft.ReportingServices.CsvRendering".ToUpperInvariant());
			KnownExtensions.m_knownRenderingExtensions.Add("Microsoft.ReportingServices.Rendering.DataRenderer.AtomDataReport,Microsoft.ReportingServices.DataRendering".ToUpperInvariant());
			KnownExtensions.m_knownRenderingExtensions.Add("Microsoft.ReportingServices.Rendering.ImageRenderer.ImageRenderer,Microsoft.ReportingServices.ImageRendering".ToUpperInvariant());
			KnownExtensions.m_knownRenderingExtensions.Add("Microsoft.ReportingServices.Rendering.ImageRenderer.PDFRenderer,Microsoft.ReportingServices.ImageRendering".ToUpperInvariant());
			KnownExtensions.m_knownRenderingExtensions.Add("Microsoft.ReportingServices.Rendering.ImageRenderer.AccessiblePDFRenderer,Microsoft.ReportingServices.ImageRendering".ToUpperInvariant());
			KnownExtensions.m_knownRenderingExtensions.Add("Microsoft.ReportingServices.Rendering.ImageRenderer.RGDIRenderer,Microsoft.ReportingServices.ImageRendering".ToUpperInvariant());
			KnownExtensions.m_knownRenderingExtensions.Add("Microsoft.ReportingServices.Rendering.HtmlRenderer.Html40RenderingExtension,Microsoft.ReportingServices.HtmlRendering".ToUpperInvariant());
			KnownExtensions.m_knownRenderingExtensions.Add("Microsoft.ReportingServices.Rendering.HtmlRenderer.Html32RenderingExtension,Microsoft.ReportingServices.HtmlRendering".ToUpperInvariant());
			KnownExtensions.m_knownRenderingExtensions.Add("Microsoft.ReportingServices.Rendering.HtmlRenderer.MHtmlRenderingExtension,Microsoft.ReportingServices.HtmlRendering".ToUpperInvariant());
			KnownExtensions.m_knownRenderingExtensions.Add("Microsoft.ReportingServices.Rendering.ExcelRenderer.ExcelRenderer,Microsoft.ReportingServices.ExcelRendering".ToUpperInvariant());
			KnownExtensions.m_knownRenderingExtensions.Add("Microsoft.ReportingServices.Rendering.ExcelOpenXmlRenderer.ExcelOpenXmlRenderer,Microsoft.ReportingServices.ExcelRendering".ToUpperInvariant());
			KnownExtensions.m_knownRenderingExtensions.Add("Microsoft.ReportingServices.Rendering.RPLRendering.RPLRenderer,Microsoft.ReportingServices.RPLRendering".ToUpperInvariant());
			KnownExtensions.m_knownRenderingExtensions.Add("Microsoft.ReportingServices.Rendering.JsonRendering.JsonRenderer,Microsoft.ReportingServices.JsonRendering".ToUpperInvariant());
			KnownExtensions.m_knownRenderingExtensions.Add("Microsoft.ReportingServices.Rendering.JsonRplRendering.JsonRplRenderer,Microsoft.ReportingServices.JsonRplRendering".ToUpperInvariant());
			KnownExtensions.m_knownRenderingExtensions.Add("Microsoft.ReportingServices.Rendering.WordRenderer.WordDocumentRenderer,Microsoft.ReportingServices.WordRendering".ToUpperInvariant());
			KnownExtensions.m_knownRenderingExtensions.Add("Microsoft.ReportingServices.Rendering.WordRenderer.WordOpenXmlRenderer.WordOpenXmlDocumentRenderer,Microsoft.ReportingServices.WordRendering".ToUpperInvariant());
			KnownExtensions.m_knownRenderingExtensions.Add("Microsoft.ReportingServices.Rendering.PowerPointRendering.PptxRenderingExtension,Microsoft.ReportingServices.PowerPointRendering".ToUpperInvariant());
			KnownExtensions.m_knownRenderingExtensions.Add("Microsoft.ReportingServices.Rendering.RPLRendering.TestRPLRenderer,Microsoft.ReportingServices.TestRPLRenderer".ToUpperInvariant());
			KnownExtensions.m_knownRenderingExtensions.Add("Microsoft.ReportingServices.Rendering.ROMRendering.ROMRenderer,Microsoft.ReportingServices.ROMRenderer".ToUpperInvariant());
			KnownExtensions.m_knownRenderingExtensions.Add("Microsoft.ReportingServices.Rendering.DataRenderer.JsonDataReport,Microsoft.ReportingServices.DataRendering".ToUpperInvariant());
			KnownExtensions.m_knownEventExtensions.Add("Microsoft.ReportingServices.Library.HistorySnapShotCreatedHandler,ReportingServicesLibrary".ToUpperInvariant());
			KnownExtensions.m_knownEventExtensions.Add("Microsoft.ReportingServices.Library.TimedSubscriptionHandler,ReportingServicesLibrary".ToUpperInvariant());
			KnownExtensions.m_knownEventExtensions.Add("Microsoft.ReportingServices.Library.ReportExecutionSnapshotUpdateEventHandler,ReportingServicesLibrary".ToUpperInvariant());
			KnownExtensions.m_expressRenderingExtensions.Add("Microsoft.ReportingServices.Rendering.HtmlRenderer.Html40RenderingExtension,Microsoft.ReportingServices.HtmlRendering".ToUpperInvariant());
			KnownExtensions.m_expressRenderingExtensions.Add("Microsoft.ReportingServices.Rendering.ImageRenderer.ImageRenderer,Microsoft.ReportingServices.ImageRendering".ToUpperInvariant());
			KnownExtensions.m_expressRenderingExtensions.Add("Microsoft.ReportingServices.Rendering.ImageRenderer.RGDIRenderer,Microsoft.ReportingServices.ImageRendering".ToUpperInvariant());
			KnownExtensions.m_expressRenderingExtensions.Add("Microsoft.ReportingServices.Rendering.ExcelRenderer.ExcelRenderer,Microsoft.ReportingServices.ExcelRendering".ToUpperInvariant());
			KnownExtensions.m_expressRenderingExtensions.Add("Microsoft.ReportingServices.Rendering.ExcelOpenXmlRenderer.ExcelOpenXmlRenderer,Microsoft.ReportingServices.ExcelRendering".ToUpperInvariant());
			KnownExtensions.m_expressRenderingExtensions.Add("Microsoft.ReportingServices.Rendering.ImageRenderer.PDFRenderer,Microsoft.ReportingServices.ImageRendering".ToUpperInvariant());
			KnownExtensions.m_expressRenderingExtensions.Add("Microsoft.ReportingServices.Rendering.ImageRenderer.AccessiblePDFRenderer,Microsoft.ReportingServices.ImageRendering".ToUpperInvariant());
			KnownExtensions.m_expressRenderingExtensions.Add("Microsoft.ReportingServices.Rendering.RPLRendering.RPLRenderer,Microsoft.ReportingServices.RPLRendering".ToUpperInvariant());
			KnownExtensions.m_expressRenderingExtensions.Add("Microsoft.ReportingServices.Rendering.JsonRendering.JsonRenderer,Microsoft.ReportingServices.JsonRendering".ToUpperInvariant());
			KnownExtensions.m_expressRenderingExtensions.Add("Microsoft.ReportingServices.Rendering.JsonRplRendering.JsonRplRenderer,Microsoft.ReportingServices.JsonRplRendering".ToUpperInvariant());
			KnownExtensions.m_expressRenderingExtensions.Add("Microsoft.ReportingServices.Rendering.WordRenderer.WordDocumentRenderer,Microsoft.ReportingServices.WordRendering".ToUpperInvariant());
			KnownExtensions.m_expressRenderingExtensions.Add("Microsoft.ReportingServices.Rendering.WordRenderer.WordOpenXmlRenderer.WordOpenXmlDocumentRenderer,Microsoft.ReportingServices.WordRendering".ToUpperInvariant());
			KnownExtensions.m_expressRenderingExtensions.Add("Microsoft.ReportingServices.Rendering.PowerPointRendering.PptxRenderingExtension,Microsoft.ReportingServices.PowerPointRendering".ToUpperInvariant());
			KnownExtensions.m_expressRenderingExtensions.Add("Microsoft.ReportingServices.Rendering.RPLRendering.TestRPLRenderer,Microsoft.ReportingServices.TestRPLRenderer".ToUpperInvariant());
			KnownExtensions.m_expressRenderingExtensions.Add("Microsoft.ReportingServices.Rendering.ROMRendering.ROMRenderer,Microsoft.ReportingServices.ROMRenderer".ToUpperInvariant());
			KnownExtensions.m_expressRenderingExtensions.Add("Microsoft.ReportingServices.Rendering.DataRenderer.AtomDataReport,Microsoft.ReportingServices.DataRendering".ToUpperInvariant());
			KnownExtensions.m_expressHiddenRenderingExtensions.Add("Microsoft.ReportingServices.Rendering.ImageRenderer.ImageRenderer,Microsoft.ReportingServices.ImageRendering".ToUpperInvariant());
			KnownExtensions.m_expressDataExtensions.Add("Microsoft.ReportingServices.DataExtensions.SqlConnectionWrapper,Microsoft.ReportingServices.DataExtensions".ToUpperInvariant());
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000928C File Offset: 0x0000748C
		internal static bool IsKnownExtension(string extensionType, string configTypeAndAssembly, string assemblyFullName)
		{
			if (!KnownExtensions.IsSqlSigned(assemblyFullName))
			{
				return false;
			}
			if (extensionType == "Data")
			{
				return KnownExtensions.IsInList(KnownExtensions.m_knownDataExtensions, configTypeAndAssembly);
			}
			if (extensionType == "Authentication")
			{
				return KnownExtensions.IsInList(KnownExtensions.m_knownAuthenticationExtensions, configTypeAndAssembly);
			}
			if (extensionType == "Security")
			{
				return KnownExtensions.IsInList(KnownExtensions.m_knownAuthorizationExtensions, configTypeAndAssembly);
			}
			if (extensionType == "SemanticQuery")
			{
				return KnownExtensions.IsInList(KnownExtensions.m_knownSQExtensions, configTypeAndAssembly);
			}
			if (extensionType == "ModelGeneration")
			{
				return KnownExtensions.IsInList(KnownExtensions.m_knownModelGenExtensions, configTypeAndAssembly);
			}
			if (extensionType == "Delivery")
			{
				return KnownExtensions.IsInList(KnownExtensions.m_knownDeliveryExtensions, configTypeAndAssembly);
			}
			if (extensionType == "Render")
			{
				return KnownExtensions.IsInList(KnownExtensions.m_knownRenderingExtensions, configTypeAndAssembly);
			}
			if (extensionType == "EventProcessing")
			{
				return KnownExtensions.IsInList(KnownExtensions.m_knownEventExtensions, configTypeAndAssembly);
			}
			return extensionType == "ReportDefinitionCustomization" && KnownExtensions.IsInList(KnownExtensions.m_knownReportDefinitionCustomizationExtensions, configTypeAndAssembly);
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00009388 File Offset: 0x00007588
		internal static bool IsKnownExtension(string extensionType, Type extensionClassType)
		{
			if (extensionClassType == null || string.IsNullOrEmpty(extensionClassType.AssemblyQualifiedName))
			{
				return false;
			}
			string[] array = extensionClassType.AssemblyQualifiedName.Split(new char[] { ',' });
			return KnownExtensions.IsKnownExtension(extensionType, array.First<string>() + "," + array[1].Trim(), array.Last<string>());
		}

		// Token: 0x0600024D RID: 589 RVA: 0x000093E8 File Offset: 0x000075E8
		internal static bool IsRestrictedRenderingExtension(SkuType sku, string configTypeAndAssembly)
		{
			return (sku == SkuType.SsrsExpress || sku == SkuType.SsrsWeb) && !KnownExtensions.IsInList(KnownExtensions.m_expressRenderingExtensions, configTypeAndAssembly);
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00009404 File Offset: 0x00007604
		internal static bool IsHiddenRenderingExtension(SkuType sku, string configTypeAndAssembly)
		{
			return (sku == SkuType.SsrsExpress || sku == SkuType.SsrsWeb) && KnownExtensions.IsInList(KnownExtensions.m_expressHiddenRenderingExtensions, configTypeAndAssembly);
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000941D File Offset: 0x0000761D
		internal static bool IsRestrictedDataExtension(SkuType sku, string configTypeAndAssembly)
		{
			return (sku == SkuType.SsrsExpress || sku == SkuType.SsrsWeb) && !KnownExtensions.IsInList(KnownExtensions.m_expressDataExtensions, configTypeAndAssembly);
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000943C File Offset: 0x0000763C
		private static bool IsInList(List<string> list, string configTypeAndAssembly)
		{
			using (List<string>.Enumerator enumerator = list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Contains(configTypeAndAssembly.ToUpperInvariant()))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00009498 File Offset: 0x00007698
		private static bool IsSqlSigned(string assemblyFullName)
		{
			return assemblyFullName.Contains("PublicKeyToken=89845dcd8080cc91") || assemblyFullName.Contains("PublicKeyToken=ba138330840163b5");
		}

		// Token: 0x04000120 RID: 288
		private static List<string> m_knownDataExtensions = new List<string>();

		// Token: 0x04000121 RID: 289
		private static List<string> m_knownAuthenticationExtensions = new List<string>();

		// Token: 0x04000122 RID: 290
		private static List<string> m_knownAuthorizationExtensions = new List<string>();

		// Token: 0x04000123 RID: 291
		private static List<string> m_knownSQExtensions = new List<string>();

		// Token: 0x04000124 RID: 292
		private static List<string> m_knownModelGenExtensions = new List<string>();

		// Token: 0x04000125 RID: 293
		private static List<string> m_knownDeliveryExtensions = new List<string>();

		// Token: 0x04000126 RID: 294
		private static List<string> m_knownRenderingExtensions = new List<string>();

		// Token: 0x04000127 RID: 295
		private static List<string> m_knownEventExtensions = new List<string>();

		// Token: 0x04000128 RID: 296
		private static List<string> m_knownReportDefinitionCustomizationExtensions = new List<string>();

		// Token: 0x04000129 RID: 297
		private static List<string> m_expressRenderingExtensions = new List<string>();

		// Token: 0x0400012A RID: 298
		private static List<string> m_expressHiddenRenderingExtensions = new List<string>();

		// Token: 0x0400012B RID: 299
		private static List<string> m_expressDataExtensions = new List<string>();
	}
}
