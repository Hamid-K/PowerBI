using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.OData
{
	// Token: 0x02000746 RID: 1862
	public class NavigationLinkWrapperInlineEntries
	{
		// Token: 0x06003728 RID: 14120 RVA: 0x000B008E File Offset: 0x000AE28E
		public NavigationLinkWrapperInlineEntries(List<Func<TypeValue, IValueReference>> inlineEntries, Uri nextPage)
		{
			this.inlineEntries = inlineEntries;
			this.nextPage = nextPage;
		}

		// Token: 0x170012F9 RID: 4857
		// (get) Token: 0x06003729 RID: 14121 RVA: 0x000B00A4 File Offset: 0x000AE2A4
		public List<Func<TypeValue, IValueReference>> InlineEntries
		{
			get
			{
				return this.inlineEntries;
			}
		}

		// Token: 0x170012FA RID: 4858
		// (get) Token: 0x0600372A RID: 14122 RVA: 0x000B00AC File Offset: 0x000AE2AC
		public Uri NextPage
		{
			get
			{
				return this.nextPage;
			}
		}

		// Token: 0x04001C65 RID: 7269
		private readonly List<Func<TypeValue, IValueReference>> inlineEntries;

		// Token: 0x04001C66 RID: 7270
		private readonly Uri nextPage;
	}
}
