using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Values;
using Microsoft.InfoNav.Data.Contracts.ConceptualSchema.Annotations;

namespace Microsoft.InfoNav.Data.Edm
{
	// Token: 0x02000009 RID: 9
	internal sealed class DaxCapabilitiesAnnotationProviderBuilder
	{
		// Token: 0x06000017 RID: 23 RVA: 0x000022D6 File Offset: 0x000004D6
		internal static DaxCapabilitiesAnnotationProvider BuildDaxCapabilitiesProvider(IEdmEntityContainer edmEntityContainer, IEdmModel edmModel, Version modelVersion)
		{
			return new DaxCapabilitiesAnnotationProvider(DaxCapabilitiesAnnotationProviderBuilder.BuildDaxCapabilities(edmEntityContainer, edmModel, modelVersion));
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000022E8 File Offset: 0x000004E8
		private static DaxCapabilitiesAnnotation BuildDaxCapabilities(IEdmEntityContainer edmEntityContainer, IEdmModel edmModel, Version modelVersion)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			bool flag6 = false;
			bool flag7 = false;
			bool flag8 = false;
			bool flag9 = false;
			bool flag10 = false;
			bool flag11 = false;
			bool flag12 = false;
			bool flag13 = false;
			bool flag14 = false;
			bool flag15 = false;
			bool flag16 = false;
			bool flag17 = modelVersion == null || modelVersion > DaxCapabilitiesAnnotationProviderBuilder.Version1_0;
			bool flag18 = false;
			bool flag19 = false;
			bool flag20 = false;
			bool flag21 = false;
			bool flag22 = false;
			bool flag23 = false;
			bool flag24 = false;
			bool flag25 = false;
			bool flag26 = false;
			bool flag27 = false;
			bool flag28 = false;
			bool flag29 = false;
			bool flag30 = false;
			bool flag31 = false;
			if (edmEntityContainer != null)
			{
				IEdmStringValue annotationValue = ExtensionMethods.GetAnnotationValue<IEdmStringValue>(edmModel, edmEntityContainer, "http://schemas.microsoft.com/sqlbi/2010/10/edm/extensions", "EntityContainer");
				if (annotationValue != null)
				{
					XElement xelement = XElement.Parse(annotationValue.Value).Element(EdmConstants.ModelCapabilities);
					if (xelement != null)
					{
						XElement xelement2 = xelement.Element(EdmConstants.DaxFunctions);
						flag = DaxCapabilitiesAnnotationProviderBuilder.HasValue(xelement, EdmConstants.GroupByValidation, "Enforced");
						flag2 = DaxCapabilitiesAnnotationProviderBuilder.HasValue(xelement, EdmConstants.QueryAggregateUsage, "Discourage");
						flag3 = DaxCapabilitiesAnnotationProviderBuilder.HasValue(xelement, EdmConstants.CrossFilteringWithinTable, "Always");
						flag4 = DaxCapabilitiesAnnotationProviderBuilder.HasValue(xelement, EdmConstants.QueryBatching, "1");
						flag5 = xelement.Element(EdmConstants.Variables) != null && !DaxCapabilitiesAnnotationProviderBuilder.HasValue(xelement, EdmConstants.Variables, "0");
						flag6 = DaxCapabilitiesAnnotationProviderBuilder.HasValue(xelement, EdmConstants.InOperator, "1");
						flag7 = DaxCapabilitiesAnnotationProviderBuilder.HasValue(xelement, EdmConstants.VirtualColumns, "1");
						flag8 = DaxCapabilitiesAnnotationProviderBuilder.HasValue(xelement, EdmConstants.TableConstructor, "1");
						flag9 = DaxCapabilitiesAnnotationProviderBuilder.HasValue(xelement, EdmConstants.MultiColumnFiltering, "LimitedToGroupByColumns");
						flag10 = DaxCapabilitiesAnnotationProviderBuilder.HasValue(xelement, EdmConstants.DataSourceVariables, "1");
						flag11 = DaxCapabilitiesAnnotationProviderBuilder.HasValue(xelement, EdmConstants.ExecutionMetrics, "1");
						flag12 = DaxCapabilitiesAnnotationProviderBuilder.HasValue(xelement, EdmConstants.FiveStateKpiRange, "1");
						flag13 = DaxCapabilitiesAnnotationProviderBuilder.HasValue(xelement, EdmConstants.VisualCalculations, "1");
						flag16 = DaxCapabilitiesAnnotationProviderBuilder.GetBooleanElementOrDefault(xelement, EdmConstants.EncourageIsEmptyDAXFunctionalUsageElem, false);
						if (xelement2 != null)
						{
							flag18 = DaxCapabilitiesAnnotationProviderBuilder.HasValue(xelement2, EdmConstants.BinaryMinMax, "1");
							flag24 = DaxCapabilitiesAnnotationProviderBuilder.HasValue(xelement2, EdmConstants.TreatAs, "1");
							flag25 = DaxCapabilitiesAnnotationProviderBuilder.HasValue(xelement2, EdmConstants.StringMinMax, "1");
							flag27 = DaxCapabilitiesAnnotationProviderBuilder.HasValue(xelement2, EdmConstants.OptimizedNotInOperator, "1");
							flag28 = DaxCapabilitiesAnnotationProviderBuilder.HasValue(xelement2, EdmConstants.NonVisual, "1");
							flag26 = DaxCapabilitiesAnnotationProviderBuilder.HasValue(xelement2, EdmConstants.SampleAxisWithLocalMinMax, "1");
							flag21 = DaxCapabilitiesAnnotationProviderBuilder.HasValue(xelement2, EdmConstants.SubstituteWithIndex, "1");
							flag22 = DaxCapabilitiesAnnotationProviderBuilder.HasValue(xelement2, EdmConstants.SummarizeColumns, "1");
							flag29 = DaxCapabilitiesAnnotationProviderBuilder.HasValue(xelement2, EdmConstants.LeftOuterJoin, "1");
							flag30 = DaxCapabilitiesAnnotationProviderBuilder.HasValue(xelement2, EdmConstants.IsAfter, "1");
							flag31 = DaxCapabilitiesAnnotationProviderBuilder.HasValue(xelement2, EdmConstants.FormatByLocale, "1");
							flag19 = flag22;
							flag20 = flag22;
							flag23 = flag22;
						}
						if (flag12)
						{
							flag14 = true;
						}
						else
						{
							flag15 = flag24;
						}
					}
				}
			}
			return new DaxCapabilitiesAnnotation(flag, flag2, flag3, flag4, flag5, flag6, flag7, flag8, flag9, flag10, flag11, flag12, flag14, flag13, new DaxFunctionsAnnotation(flag15, flag16, flag17, flag18, flag21, flag22, flag19, flag20, flag23, flag24, flag25, flag26, flag27, flag28, flag29, flag30, flag31));
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000025FC File Offset: 0x000007FC
		private static bool HasValue(XElement containingElement, XName elementName, string expectedValue)
		{
			XElement xelement = containingElement.Element(elementName);
			return xelement != null && xelement.Value == expectedValue;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002624 File Offset: 0x00000824
		private static bool GetBooleanElementOrDefault(XElement containingElement, XName elementName, bool defaultValue)
		{
			XElement xelement = containingElement.Element(elementName);
			if (xelement == null)
			{
				return defaultValue;
			}
			string value = xelement.Value;
			if (value == null)
			{
				return defaultValue;
			}
			return XmlConvert.ToBoolean(value);
		}

		// Token: 0x04000037 RID: 55
		private static readonly Version Version1_0 = new Version(1, 0);
	}
}
