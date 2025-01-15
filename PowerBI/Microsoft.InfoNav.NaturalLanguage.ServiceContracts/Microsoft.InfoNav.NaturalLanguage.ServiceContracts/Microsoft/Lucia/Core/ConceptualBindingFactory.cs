using System;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000082 RID: 130
	public static class ConceptualBindingFactory
	{
		// Token: 0x0600024F RID: 591 RVA: 0x00005816 File Offset: 0x00003A16
		public static ConceptualEntityBinding CreateEntityBinding(string conceptualEntity, string schema = null)
		{
			return new ConceptualEntityBinding(conceptualEntity, schema);
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000581F File Offset: 0x00003A1F
		public static ConceptualPropertyBinding CreatePropertyBinding(string conceptualEntity, string conceptualProperty, string variationSource = null, string variationSet = null, string schema = null)
		{
			return new ConceptualPropertyBinding(conceptualEntity, conceptualProperty, variationSource, variationSet, schema);
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000582C File Offset: 0x00003A2C
		public static ConceptualHierarchyBinding CreateHierarchyBinding(string conceptualEntity, string variationSource, string variationSet, string hierarchy, string schema = null)
		{
			return new ConceptualHierarchyBinding(conceptualEntity, variationSource, variationSet, hierarchy, schema);
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00005839 File Offset: 0x00003A39
		public static ConceptualHierarchyLevelBinding CreateHierarchyLevelBinding(string conceptualEntity, string variationSource, string variationSet, string hierarchy, string hierarchyLevel, string schema = null)
		{
			return new ConceptualHierarchyLevelBinding(conceptualEntity, variationSource, variationSet, hierarchy, hierarchyLevel, schema);
		}
	}
}
