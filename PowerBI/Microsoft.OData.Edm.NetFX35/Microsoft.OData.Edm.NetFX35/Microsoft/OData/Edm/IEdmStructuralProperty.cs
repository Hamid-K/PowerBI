using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000140 RID: 320
	public interface IEdmStructuralProperty : IEdmProperty, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000622 RID: 1570
		string DefaultValueString { get; }

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000623 RID: 1571
		EdmConcurrencyMode ConcurrencyMode { get; }
	}
}
