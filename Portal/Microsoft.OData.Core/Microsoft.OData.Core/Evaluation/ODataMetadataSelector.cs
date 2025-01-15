using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Evaluation
{
	// Token: 0x0200025B RID: 603
	public abstract class ODataMetadataSelector
	{
		// Token: 0x06001B38 RID: 6968 RVA: 0x00053DE2 File Offset: 0x00051FE2
		public virtual IEnumerable<IEdmNavigationProperty> SelectNavigationProperties(IEdmStructuredType type, IEnumerable<IEdmNavigationProperty> navigationProperties)
		{
			return navigationProperties;
		}

		// Token: 0x06001B39 RID: 6969 RVA: 0x00053DE2 File Offset: 0x00051FE2
		public virtual IEnumerable<IEdmOperation> SelectBindableOperations(IEdmStructuredType type, IEnumerable<IEdmOperation> bindableOperations)
		{
			return bindableOperations;
		}

		// Token: 0x06001B3A RID: 6970 RVA: 0x00053DE2 File Offset: 0x00051FE2
		public virtual IEnumerable<IEdmStructuralProperty> SelectStreamProperties(IEdmStructuredType type, IEnumerable<IEdmStructuralProperty> selectedStreamProperties)
		{
			return selectedStreamProperties;
		}
	}
}
