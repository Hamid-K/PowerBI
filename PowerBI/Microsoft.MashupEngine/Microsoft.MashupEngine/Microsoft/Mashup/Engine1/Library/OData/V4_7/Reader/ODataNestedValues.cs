using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7.Reader
{
	// Token: 0x0200079F RID: 1951
	internal class ODataNestedValues
	{
		// Token: 0x06003922 RID: 14626 RVA: 0x000B7CDD File Offset: 0x000B5EDD
		public ODataNestedValues(List<ODataNestedValue> inlineValues, Uri nextPage)
		{
			this.inlineValues = inlineValues;
			this.nextPage = nextPage;
		}

		// Token: 0x17001363 RID: 4963
		// (get) Token: 0x06003923 RID: 14627 RVA: 0x000B7CF3 File Offset: 0x000B5EF3
		public List<ODataNestedValue> InlineValues
		{
			get
			{
				return this.inlineValues;
			}
		}

		// Token: 0x17001364 RID: 4964
		// (get) Token: 0x06003924 RID: 14628 RVA: 0x000B7CFB File Offset: 0x000B5EFB
		public Uri NextPage
		{
			get
			{
				return this.nextPage;
			}
		}

		// Token: 0x04001D75 RID: 7541
		private readonly List<ODataNestedValue> inlineValues;

		// Token: 0x04001D76 RID: 7542
		private readonly Uri nextPage;
	}
}
