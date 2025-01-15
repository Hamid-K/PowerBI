using System;
using System.Collections.Generic;
using Microsoft.OData;

namespace Microsoft.AspNet.OData.Formatter.Deserialization
{
	// Token: 0x020001BB RID: 443
	public sealed class ODataResourceSetWrapper : ODataItemBase
	{
		// Token: 0x06000E91 RID: 3729 RVA: 0x0003BF11 File Offset: 0x0003A111
		public ODataResourceSetWrapper(ODataResourceSet item)
			: base(item)
		{
			this.Resources = new List<ODataResourceWrapper>();
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06000E92 RID: 3730 RVA: 0x0003BF25 File Offset: 0x0003A125
		public ODataResourceSet ResourceSet
		{
			get
			{
				return base.Item as ODataResourceSet;
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06000E93 RID: 3731 RVA: 0x0003BF32 File Offset: 0x0003A132
		// (set) Token: 0x06000E94 RID: 3732 RVA: 0x0003BF3A File Offset: 0x0003A13A
		public IList<ODataResourceWrapper> Resources { get; private set; }
	}
}
