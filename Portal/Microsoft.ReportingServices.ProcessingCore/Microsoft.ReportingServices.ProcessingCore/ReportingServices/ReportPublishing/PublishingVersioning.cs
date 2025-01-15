using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportPublishing
{
	// Token: 0x0200038C RID: 908
	internal class PublishingVersioning
	{
		// Token: 0x06002522 RID: 9506 RVA: 0x000B183E File Offset: 0x000AFA3E
		internal PublishingVersioning(IConfiguration configuration, PublishingContextBase publishingContext)
		{
			this.m_configuration = configuration;
			this.m_publishingContext = publishingContext;
			this.m_configVersion = ReportProcessingCompatibilityVersion.GetCompatibilityVersion(this.m_configuration);
		}

		// Token: 0x170013A2 RID: 5026
		// (get) Token: 0x06002523 RID: 9507 RVA: 0x000B1865 File Offset: 0x000AFA65
		internal RenderMode RenderMode
		{
			get
			{
				if (this.m_publishingContext.IsRdlx)
				{
					return RenderMode.RenderEdit;
				}
				return RenderMode.FullOdp;
			}
		}

		// Token: 0x06002524 RID: 9508 RVA: 0x000B1878 File Offset: 0x000AFA78
		private static RdlVersionedFeatures CreateRdlFeatureVersioningStructure()
		{
			RdlVersionedFeatures rdlVersionedFeatures = new RdlVersionedFeatures();
			rdlVersionedFeatures.Add(RdlFeatures.SharedDataSetReferences, 0, RenderMode.FullOdp);
			rdlVersionedFeatures.Add(RdlFeatures.Image_Embedded, 200, RenderMode.Both);
			rdlVersionedFeatures.Add(RdlFeatures.Sort_Group_Applied, 0, RenderMode.FullOdp);
			rdlVersionedFeatures.Add(RdlFeatures.DeferredSort, 100, RenderMode.RenderEdit);
			rdlVersionedFeatures.Add(RdlFeatures.Sort_DataRegion, 0, RenderMode.FullOdp);
			rdlVersionedFeatures.Add(RdlFeatures.Filters, 0, RenderMode.FullOdp);
			rdlVersionedFeatures.Add(RdlFeatures.Lookup, 0, RenderMode.FullOdp);
			rdlVersionedFeatures.Add(RdlFeatures.RunningValue, 0, RenderMode.FullOdp);
			rdlVersionedFeatures.Add(RdlFeatures.Previous, 0, RenderMode.FullOdp);
			rdlVersionedFeatures.Add(RdlFeatures.RowNumber, 0, RenderMode.FullOdp);
			rdlVersionedFeatures.Add(RdlFeatures.GroupParent, 0, RenderMode.FullOdp);
			rdlVersionedFeatures.Add(RdlFeatures.Variables, 0, RenderMode.FullOdp);
			rdlVersionedFeatures.Add(RdlFeatures.SubReports, 0, RenderMode.FullOdp);
			rdlVersionedFeatures.Add(RdlFeatures.AutomaticSubtotals, 0, RenderMode.FullOdp);
			rdlVersionedFeatures.Add(RdlFeatures.DomainScope, 0, RenderMode.FullOdp);
			rdlVersionedFeatures.Add(RdlFeatures.InScope, 0, RenderMode.FullOdp);
			rdlVersionedFeatures.Add(RdlFeatures.Level, 0, RenderMode.FullOdp);
			rdlVersionedFeatures.Add(RdlFeatures.CreateDrillthroughContext, 0, RenderMode.FullOdp);
			rdlVersionedFeatures.Add(RdlFeatures.UserSort, 0, RenderMode.FullOdp);
			rdlVersionedFeatures.Add(RdlFeatures.AggregatesOfAggregates, 0, RenderMode.FullOdp);
			rdlVersionedFeatures.Add(RdlFeatures.PageHeaderFooter, 0, RenderMode.FullOdp);
			rdlVersionedFeatures.Add(RdlFeatures.SortGroupExpression_OnlySimpleField, 0, RenderMode.FullOdp);
			rdlVersionedFeatures.Add(RdlFeatures.PeerGroups, 0, RenderMode.FullOdp);
			rdlVersionedFeatures.Add(RdlFeatures.ImageTag, 100, RenderMode.RenderEdit);
			rdlVersionedFeatures.Add(RdlFeatures.ReportSectionName, 100, RenderMode.RenderEdit);
			rdlVersionedFeatures.Add(RdlFeatures.EmbeddingMode, 200, RenderMode.RenderEdit);
			rdlVersionedFeatures.Add(RdlFeatures.EmbeddingMode_Inline, 0, RenderMode.FullOdp);
			rdlVersionedFeatures.Add(RdlFeatures.ReportSection_LayoutDirection, 200, RenderMode.RenderEdit);
			rdlVersionedFeatures.Add(RdlFeatures.ThemeFonts, 200, RenderMode.RenderEdit);
			rdlVersionedFeatures.Add(RdlFeatures.TablixHierarchy_EnableDrilldown, 200, RenderMode.RenderEdit);
			rdlVersionedFeatures.Add(RdlFeatures.ChartHierarchy_EnableDrilldown, 200, RenderMode.RenderEdit);
			rdlVersionedFeatures.Add(RdlFeatures.ScopesCollection, 200, RenderMode.RenderEdit);
			rdlVersionedFeatures.Add(RdlFeatures.ThemeColors, 200, RenderMode.RenderEdit);
			rdlVersionedFeatures.Add(RdlFeatures.Report_Code, 0, RenderMode.FullOdp);
			rdlVersionedFeatures.Add(RdlFeatures.Report_Classes, 0, RenderMode.FullOdp);
			rdlVersionedFeatures.Add(RdlFeatures.Report_CodeModules, 0, RenderMode.FullOdp);
			rdlVersionedFeatures.Add(RdlFeatures.ComplexExpression, 0, RenderMode.FullOdp);
			rdlVersionedFeatures.Add(RdlFeatures.BackgroundImageFitting, 200, RenderMode.RenderEdit);
			rdlVersionedFeatures.Add(RdlFeatures.BackgroundImageTransparency, 200, RenderMode.RenderEdit);
			rdlVersionedFeatures.Add(RdlFeatures.LabelData_KeyFields, 200, RenderMode.Both);
			rdlVersionedFeatures.Add(RdlFeatures.ImageTagsCollection, 200, RenderMode.RenderEdit);
			rdlVersionedFeatures.Add(RdlFeatures.CellLevelFormatting, 200, RenderMode.RenderEdit);
			rdlVersionedFeatures.Add(RdlFeatures.ParametersLayout, 300, RenderMode.FullOdp);
			rdlVersionedFeatures.Add(RdlFeatures.DefaultFontFamily, 400, RenderMode.FullOdp);
			rdlVersionedFeatures.Add(RdlFeatures.AggregateIndicatorField, 500, RenderMode.FullOdp);
			rdlVersionedFeatures.VerifyAllFeaturesAreAdded();
			return rdlVersionedFeatures;
		}

		// Token: 0x06002525 RID: 9509 RVA: 0x000B1A8C File Offset: 0x000AFC8C
		internal bool IsRdlFeatureRestricted(RdlFeatures feature)
		{
			return !PublishingVersioning.m_rdlFeatureVersioningStructure.IsRdlFeatureAllowed(feature, this.m_configVersion, this.m_publishingContext.PublishingVersioning.RenderMode);
		}

		// Token: 0x0400158D RID: 5517
		private readonly IConfiguration m_configuration;

		// Token: 0x0400158E RID: 5518
		private readonly PublishingContextBase m_publishingContext;

		// Token: 0x0400158F RID: 5519
		private readonly int m_configVersion;

		// Token: 0x04001590 RID: 5520
		private static readonly RdlVersionedFeatures m_rdlFeatureVersioningStructure = PublishingVersioning.CreateRdlFeatureVersioningStructure();
	}
}
