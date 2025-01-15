using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.OData.Core.Metadata
{
	// Token: 0x02000132 RID: 306
	[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Edm", Justification = "Camel-casing in class name.")]
	public sealed class ODataEdmPropertyAnnotation
	{
		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000BB8 RID: 3000 RVA: 0x0002CA0D File Offset: 0x0002AC0D
		// (set) Token: 0x06000BB9 RID: 3001 RVA: 0x0002CA15 File Offset: 0x0002AC15
		public ODataNullValueBehaviorKind NullValueReadBehaviorKind { get; set; }
	}
}
