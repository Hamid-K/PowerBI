using System;
using System.Collections.Generic;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts.ConceptualSchema.Annotations;
using Microsoft.Reporting.QueryDesign.Edm.Internal;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x020001D4 RID: 468
	internal sealed class DaxCapabilitiesBuilder
	{
		// Token: 0x06001699 RID: 5785 RVA: 0x0003E440 File Offset: 0x0003C640
		public static DaxCapabilities BuildCapabilities(EntityDataModel model, IConceptualSchema schema, IFeatureSwitchProvider featureSwitchProvider)
		{
			if (!featureSwitchProvider.IsEnabled(FeatureSwitchKind.QDMConceptualSchema))
			{
				return DaxCapabilitiesBuilder.BuildCapabilities(model);
			}
			return DaxCapabilitiesBuilder.BuildCapabilities(schema);
		}

		// Token: 0x0600169A RID: 5786 RVA: 0x0003E459 File Offset: 0x0003C659
		public static DaxCapabilities BuildCapabilities(EntityDataModel model, IConceptualSchema schema, bool useConceptualSchema)
		{
			if (!useConceptualSchema)
			{
				return DaxCapabilitiesBuilder.BuildCapabilities(model);
			}
			return DaxCapabilitiesBuilder.BuildCapabilities(schema);
		}

		// Token: 0x0600169B RID: 5787 RVA: 0x0003E46B File Offset: 0x0003C66B
		public static DaxCapabilities BuildCapabilities(EntityDataModel model)
		{
			return DaxCapabilitiesBuilder.BuildCapabilities(model.ModelCapabilities, model.Version > EntityDataModel.VersionOnePointZero);
		}

		// Token: 0x0600169C RID: 5788 RVA: 0x0003E488 File Offset: 0x0003C688
		public static DaxCapabilities BuildCapabilities(IConceptualSchema schema)
		{
			return DaxCapabilitiesBuilder.BuildCapabilitiesFromAnnotation(schema.GetDaxCapabilitiesAnnotation());
		}

		// Token: 0x0600169D RID: 5789 RVA: 0x0003E498 File Offset: 0x0003C698
		public static DaxCapabilities BuildCapabilities(ModelCapabilities modelCapabilities, bool addDaxFunctionDivide)
		{
			HashSet<DaxFunctionKind> hashSet = new HashSet<DaxFunctionKind>();
			if (modelCapabilities.EncourageIsEmptyDAXFunctionUsage)
			{
				hashSet.Add(DaxFunctionKind.IsEmpty);
			}
			if (modelCapabilities.DaxFunctions.BinaryMinMax == BinaryMinMaxType.DefaultSupport)
			{
				hashSet.Add(DaxFunctionKind.BinaryMinMax);
			}
			if (modelCapabilities.DaxFunctions.StringMinMax == StringMinMaxType.DefaultSupport)
			{
				hashSet.Add(DaxFunctionKind.StringMinMax);
			}
			if (modelCapabilities.DaxFunctions.SummarizeColumns == SummarizeColumnsType.DefaultSupport)
			{
				hashSet.Add(DaxFunctionKind.SummarizeColumns);
				hashSet.Add(DaxFunctionKind.SelectColumns);
				hashSet.Add(DaxFunctionKind.GroupBy);
				hashSet.Add(DaxFunctionKind.IsOnOrAfter);
			}
			if (modelCapabilities.DaxFunctions.IsAfter == IsAfterType.DefaultSupport)
			{
				hashSet.Add(DaxFunctionKind.IsAfter);
			}
			if (modelCapabilities.DaxFunctions.TreatAs == TreatAsType.DefaultSupport)
			{
				hashSet.Add(DaxFunctionKind.TreatAs);
			}
			if (modelCapabilities.DaxFunctions.SampleAxisWithLocalMinMax == SampleAxisWithLocalMinMaxType.DefaultSupport)
			{
				hashSet.Add(DaxFunctionKind.SampleAxisWithLocalMinMax);
			}
			if (modelCapabilities.DaxFunctions.OptimizedNotInOperator == OptimizedNotInOperatorType.DefaultSupport)
			{
				hashSet.Add(DaxFunctionKind.OptimizedNotInOperator);
			}
			if (modelCapabilities.DaxFunctions.NonVisual == NonVisualType.DefaultSupport)
			{
				hashSet.Add(DaxFunctionKind.NonVisual);
			}
			HashSet<ModelCapabilitiesKind> hashSet2 = new HashSet<ModelCapabilitiesKind>();
			if (modelCapabilities.InOperator == InOperatorType.DefaultSupport)
			{
				hashSet2.Add(ModelCapabilitiesKind.InOperator);
			}
			if (modelCapabilities.VirtualColumns == VirtualColumnsType.DefaultSupport)
			{
				hashSet2.Add(ModelCapabilitiesKind.VirtualColumns);
			}
			if (modelCapabilities.TableConstructor == TableConstructorType.DefaultSupport)
			{
				hashSet2.Add(ModelCapabilitiesKind.TableConstructor);
			}
			if (modelCapabilities.IsMultidimensional())
			{
				hashSet2.Add(ModelCapabilitiesKind.DefaultMembers);
			}
			else
			{
				hashSet2.Add(ModelCapabilitiesKind.TreatAsSupportForAllColumns);
			}
			if (addDaxFunctionDivide)
			{
				hashSet.Add(DaxFunctionKind.Divide);
			}
			if (modelCapabilities.DaxFunctions.FormatByLocale == FormatByLocale.DefaultSupport)
			{
				hashSet.Add(DaxFunctionKind.FormatByLocale);
			}
			return new DaxCapabilities(hashSet, hashSet2);
		}

		// Token: 0x0600169E RID: 5790 RVA: 0x0003E604 File Offset: 0x0003C804
		public static DaxCapabilities BuildCapabilitiesFromAnnotation(DaxCapabilitiesAnnotation annotation)
		{
			HashSet<DaxFunctionKind> hashSet = new HashSet<DaxFunctionKind>();
			if (annotation.DaxFunctions.SupportsIsEmpty)
			{
				hashSet.Add(DaxFunctionKind.IsEmpty);
			}
			if (annotation.DaxFunctions.SupportsBinaryMinMax)
			{
				hashSet.Add(DaxFunctionKind.BinaryMinMax);
			}
			if (annotation.DaxFunctions.SupportsStringMinMax)
			{
				hashSet.Add(DaxFunctionKind.StringMinMax);
			}
			if (annotation.DaxFunctions.SupportsSummarizeColumns)
			{
				hashSet.Add(DaxFunctionKind.SummarizeColumns);
				hashSet.Add(DaxFunctionKind.SelectColumns);
				hashSet.Add(DaxFunctionKind.GroupBy);
				hashSet.Add(DaxFunctionKind.IsOnOrAfter);
			}
			if (annotation.DaxFunctions.SupportsIsAfter)
			{
				hashSet.Add(DaxFunctionKind.IsAfter);
			}
			if (annotation.DaxFunctions.SupportsTreatAs)
			{
				hashSet.Add(DaxFunctionKind.TreatAs);
			}
			if (annotation.DaxFunctions.SupportsSampleAxisWithLocalMinMax)
			{
				hashSet.Add(DaxFunctionKind.SampleAxisWithLocalMinMax);
			}
			if (annotation.DaxFunctions.SupportsOptimizedNotInOperator)
			{
				hashSet.Add(DaxFunctionKind.OptimizedNotInOperator);
			}
			if (annotation.DaxFunctions.SupportsNonVisual)
			{
				hashSet.Add(DaxFunctionKind.NonVisual);
			}
			HashSet<ModelCapabilitiesKind> hashSet2 = new HashSet<ModelCapabilitiesKind>();
			if (annotation.SupportsInOperator)
			{
				hashSet2.Add(ModelCapabilitiesKind.InOperator);
			}
			if (annotation.SupportsVirtualColumns)
			{
				hashSet2.Add(ModelCapabilitiesKind.VirtualColumns);
			}
			if (annotation.SupportsTableConstructor)
			{
				hashSet2.Add(ModelCapabilitiesKind.TableConstructor);
			}
			if (annotation.IsMultidimensional())
			{
				hashSet2.Add(ModelCapabilitiesKind.DefaultMembers);
			}
			else
			{
				hashSet2.Add(ModelCapabilitiesKind.TreatAsSupportForAllColumns);
			}
			if (annotation.DaxFunctions.SupportsDivide)
			{
				hashSet.Add(DaxFunctionKind.Divide);
			}
			if (annotation.DaxFunctions.SupportsFormatByLocale)
			{
				hashSet.Add(DaxFunctionKind.FormatByLocale);
			}
			return new DaxCapabilities(hashSet, hashSet2);
		}
	}
}
