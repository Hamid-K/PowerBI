using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.OData
{
	// Token: 0x02000029 RID: 41
	[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Edm", Justification = "Camel-casing in class name.")]
	public sealed class ODataEdmPropertyAnnotation
	{
		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000176 RID: 374 RVA: 0x0000406D File Offset: 0x0000226D
		// (set) Token: 0x06000177 RID: 375 RVA: 0x00004075 File Offset: 0x00002275
		public ODataNullValueBehaviorKind NullValueReadBehaviorKind { get; set; }
	}
}
