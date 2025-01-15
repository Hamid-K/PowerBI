using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace Microsoft.InfoNav.Data.Edm
{
	// Token: 0x02000010 RID: 16
	[ImmutableObject(true)]
	internal sealed class ConceptualEntityStatisticsComputer
	{
		// Token: 0x0600005F RID: 95 RVA: 0x00002F64 File Offset: 0x00001164
		internal ConceptualEntityStatistics Compute(int hierarchyCount, bool isHidden, bool isDateTable, IReadOnlyList<IConceptualProperty> defaultFieldSet, IReadOnlyList<IConceptualProperty> rowIdentifiers, IConceptualProperty defaultImage, IConceptualProperty defaultLabel, IReadOnlyList<ConceptualProperty> properties, IReadOnlyList<IConceptualProperty>[] orderByProperties)
		{
			ReadOnlyCollection<ConceptualPrimitiveType> readOnlyCollection = defaultFieldSet.Select((IConceptualProperty p) => p.ConceptualDataType).AsReadOnlyCollection<ConceptualPrimitiveType>();
			ReadOnlyCollection<ConceptualPrimitiveType> readOnlyCollection2 = rowIdentifiers.Select((IConceptualProperty p) => p.ConceptualDataType).AsReadOnlyCollection<ConceptualPrimitiveType>();
			bool flag = defaultImage != null;
			ConceptualPrimitiveType conceptualPrimitiveType = (flag ? defaultImage.ConceptualDataType : ConceptualPrimitiveType.Null);
			bool flag2 = defaultLabel != null;
			ConceptualPrimitiveType conceptualPrimitiveType2 = (flag2 ? defaultLabel.ConceptualDataType : ConceptualPrimitiveType.Null);
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			Dictionary<ConceptualPrimitiveType, int> dictionary = new Dictionary<ConceptualPrimitiveType, int>();
			Dictionary<ConceptualDataCategory, int> dictionary2 = new Dictionary<ConceptualDataCategory, int>();
			Dictionary<ConceptualDefaultAggregate, int> dictionary3 = new Dictionary<ConceptualDefaultAggregate, int>();
			for (int i = 0; i < properties.Count; i++)
			{
				ConceptualProperty conceptualProperty = properties[i];
				IConceptualMeasure conceptualMeasure = conceptualProperty as IConceptualMeasure;
				if (conceptualMeasure != null)
				{
					num++;
					if (conceptualMeasure.Kpi != null)
					{
						num2++;
					}
				}
				if (conceptualProperty.IsHidden)
				{
					num3++;
				}
				if (conceptualProperty.KeepUniqueRows)
				{
					num4++;
				}
				if (orderByProperties[i] != null && orderByProperties[i].Count > 0)
				{
					num5++;
				}
				dictionary.AddOrIncrement(conceptualProperty.ConceptualDataType);
				dictionary2.AddOrIncrement(conceptualProperty.ConceptualDataCategory);
				if (conceptualMeasure == null)
				{
					IConceptualColumn conceptualColumn = conceptualProperty as IConceptualColumn;
					if (conceptualColumn != null)
					{
						dictionary3.AddOrIncrement(conceptualColumn.ConceptualDefaultAggregate);
					}
				}
			}
			return new ConceptualEntityStatistics(properties.Count, num, num2, hierarchyCount, num3, num5, isDateTable, isHidden, flag2, conceptualPrimitiveType2, readOnlyCollection2, num4, flag, conceptualPrimitiveType, readOnlyCollection, dictionary, dictionary2, dictionary3);
		}
	}
}
