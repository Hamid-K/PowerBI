using System;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Library.Soap;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000074 RID: 116
	internal sealed class ReportPageProperties : PageProperties
	{
		// Token: 0x0600048A RID: 1162 RVA: 0x00013C9C File Offset: 0x00011E9C
		public ReportPageProperties(RSService service, ExternalItemPath reportPath, double pageHeight, double pageWidth, double topMargin, double bottomMargin, double leftMargin, double rightMargin)
			: base(pageHeight, pageWidth, topMargin, bottomMargin, leftMargin, rightMargin)
		{
			this.Construct(service, reportPath);
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x00013CB7 File Offset: 0x00011EB7
		public ReportPageProperties(RSService service, ExternalItemPath reportPath)
		{
			this.Construct(service, reportPath);
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x00013CC7 File Offset: 0x00011EC7
		private void Construct(RSService service, ExternalItemPath reportPath)
		{
			this.m_service = service;
			this.m_reportPath = reportPath;
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x00013CD7 File Offset: 0x00011ED7
		internal ReportPageProperties(ItemProperties properties)
		{
			this.m_properties = properties;
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00013CE8 File Offset: 0x00011EE8
		public void Load()
		{
			using (MonitoredScope.New("ReportPageProperties.Load"))
			{
				Property[] array = this.InitializeNewPropertyArray();
				Property[] array2;
				if (this.m_properties == null)
				{
					GetPropertiesAction getPropertiesAction = this.m_service.GetPropertiesAction;
					getPropertiesAction.ActionParameters.ItemNamespace = ItemNamespaceEnum.PathBased;
					getPropertiesAction.ActionParameters.AllowEditSessionSyntax = true;
					getPropertiesAction.ActionParameters.ItemPath = this.m_reportPath.FullEditSessionIdentifier;
					getPropertiesAction.ActionParameters.RequestedProperties = array;
					getPropertiesAction.PerformActionNow();
					array2 = getPropertiesAction.ActionParameters.PropertyValues;
				}
				else
				{
					array2 = this.m_properties.FilterProperties(array);
				}
				if (array2 != null)
				{
					foreach (Property property in array2)
					{
						if (Localization.CatalogCultureCompare(property.Name, "TopMargin") == 0)
						{
							ReportPageProperties.ParsePropertyString(property.Value, ref this.m_topMargin);
						}
						else if (Localization.CatalogCultureCompare(property.Name, "BottomMargin") == 0)
						{
							ReportPageProperties.ParsePropertyString(property.Value, ref this.m_bottomMargin);
						}
						else if (Localization.CatalogCultureCompare(property.Name, "LeftMargin") == 0)
						{
							ReportPageProperties.ParsePropertyString(property.Value, ref this.m_leftMargin);
						}
						else if (Localization.CatalogCultureCompare(property.Name, "RightMargin") == 0)
						{
							ReportPageProperties.ParsePropertyString(property.Value, ref this.m_rightMargin);
						}
						else if (Localization.CatalogCultureCompare(property.Name, "PageHeight") == 0)
						{
							ReportPageProperties.ParsePropertyString(property.Value, ref this.m_pageHeight);
						}
						else if (Localization.CatalogCultureCompare(property.Name, "PageWidth") == 0)
						{
							ReportPageProperties.ParsePropertyString(property.Value, ref this.m_pageWidth);
						}
					}
				}
			}
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00013EB0 File Offset: 0x000120B0
		internal static bool IsLayout(string propertyName)
		{
			return propertyName == "BottomMargin" || propertyName == "LeftMargin" || propertyName == "PageHeight" || propertyName == "PageWidth" || propertyName == "RightMargin" || propertyName == "TopMargin";
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00013F10 File Offset: 0x00012110
		private static void ParsePropertyString(string val, ref double property)
		{
			double num = property;
			if (!double.TryParse(val, NumberStyles.Number, CultureInfo.InvariantCulture, out property))
			{
				property = num;
			}
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x00013F34 File Offset: 0x00012134
		internal Property[] InitializeNewPropertyArray()
		{
			Property[] array = new Property[6];
			for (int i = 0; i < 6; i++)
			{
				array[i] = new Property();
			}
			array[0].Name = "PageHeight";
			array[0].Value = base.PageHeight.ToString(CultureInfo.InvariantCulture);
			array[1].Name = "PageWidth";
			array[1].Value = base.PageWidth.ToString(CultureInfo.InvariantCulture);
			array[2].Name = "TopMargin";
			array[2].Value = base.TopMargin.ToString(CultureInfo.InvariantCulture);
			array[3].Name = "BottomMargin";
			array[3].Value = base.BottomMargin.ToString(CultureInfo.InvariantCulture);
			array[4].Name = "LeftMargin";
			array[4].Value = base.LeftMargin.ToString(CultureInfo.InvariantCulture);
			array[5].Name = "RightMargin";
			array[5].Value = base.RightMargin.ToString(CultureInfo.InvariantCulture);
			return array;
		}

		// Token: 0x0400023A RID: 570
		internal const int _PagePropertiesCount = 6;

		// Token: 0x0400023B RID: 571
		internal const string _PageWidth = "PageWidth";

		// Token: 0x0400023C RID: 572
		internal const string _PageHeight = "PageHeight";

		// Token: 0x0400023D RID: 573
		internal const string _TopMargin = "TopMargin";

		// Token: 0x0400023E RID: 574
		internal const string _BottomMargin = "BottomMargin";

		// Token: 0x0400023F RID: 575
		internal const string _LeftMargin = "LeftMargin";

		// Token: 0x04000240 RID: 576
		internal const string _RightMargin = "RightMargin";

		// Token: 0x04000241 RID: 577
		private ExternalItemPath m_reportPath;

		// Token: 0x04000242 RID: 578
		private RSService m_service;

		// Token: 0x04000243 RID: 579
		private ItemProperties m_properties;
	}
}
