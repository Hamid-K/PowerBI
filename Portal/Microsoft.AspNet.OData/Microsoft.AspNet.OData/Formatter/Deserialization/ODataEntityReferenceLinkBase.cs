using System;
using Microsoft.OData;

namespace Microsoft.AspNet.OData.Formatter.Deserialization
{
	// Token: 0x020001B5 RID: 437
	public class ODataEntityReferenceLinkBase : ODataItemBase
	{
		// Token: 0x06000E76 RID: 3702 RVA: 0x0003B530 File Offset: 0x00039730
		public ODataEntityReferenceLinkBase(ODataEntityReferenceLink item)
			: base(item)
		{
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06000E77 RID: 3703 RVA: 0x0003B539 File Offset: 0x00039739
		public ODataEntityReferenceLink EntityReferenceLink
		{
			get
			{
				return base.Item as ODataEntityReferenceLink;
			}
		}
	}
}
