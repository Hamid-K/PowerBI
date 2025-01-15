using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.OData
{
	// Token: 0x020000C3 RID: 195
	[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Edm", Justification = "Camel-casing in class name.")]
	public sealed class ODataEdmPropertyAnnotation
	{
		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000780 RID: 1920 RVA: 0x000153B1 File Offset: 0x000135B1
		// (set) Token: 0x06000781 RID: 1921 RVA: 0x000153B9 File Offset: 0x000135B9
		public ODataNullValueBehaviorKind NullValueReadBehaviorKind { get; set; }
	}
}
