using System;
using System.Collections.Generic;
using Microsoft.OData;

namespace Microsoft.AspNet.OData.Formatter.Deserialization
{
	// Token: 0x020001BA RID: 442
	public sealed class ODataResourceWrapper : ODataItemBase
	{
		// Token: 0x06000E8D RID: 3725 RVA: 0x0003BEDF File Offset: 0x0003A0DF
		public ODataResourceWrapper(ODataResource item)
			: base(item)
		{
			this.NestedResourceInfos = new List<ODataNestedResourceInfoWrapper>();
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06000E8E RID: 3726 RVA: 0x0003BEF3 File Offset: 0x0003A0F3
		public ODataResource Resource
		{
			get
			{
				return base.Item as ODataResource;
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06000E8F RID: 3727 RVA: 0x0003BF00 File Offset: 0x0003A100
		// (set) Token: 0x06000E90 RID: 3728 RVA: 0x0003BF08 File Offset: 0x0003A108
		public IList<ODataNestedResourceInfoWrapper> NestedResourceInfos { get; private set; }
	}
}
