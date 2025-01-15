using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.OData
{
	// Token: 0x02000747 RID: 1863
	public class NavigationLinkWrapperPropertyLookupValue
	{
		// Token: 0x0600372B RID: 14123 RVA: 0x000B00B4 File Offset: 0x000AE2B4
		public NavigationLinkWrapperPropertyLookupValue(IODataNavigationLinkWrapper navigationLinkWrapper, NavigationLinkWrapperInlineEntries value)
		{
			this.value = value;
			this.navigationLinkWrapper = navigationLinkWrapper;
		}

		// Token: 0x170012FB RID: 4859
		// (get) Token: 0x0600372C RID: 14124 RVA: 0x000B00CA File Offset: 0x000AE2CA
		public IODataNavigationLinkWrapper NavigationLinkWrapper
		{
			get
			{
				return this.navigationLinkWrapper;
			}
		}

		// Token: 0x170012FC RID: 4860
		// (get) Token: 0x0600372D RID: 14125 RVA: 0x000B00D2 File Offset: 0x000AE2D2
		public List<Func<TypeValue, IValueReference>> InlineEntries
		{
			get
			{
				return this.value.InlineEntries;
			}
		}

		// Token: 0x170012FD RID: 4861
		// (get) Token: 0x0600372E RID: 14126 RVA: 0x000B00DF File Offset: 0x000AE2DF
		public Uri NextPage
		{
			get
			{
				return this.value.NextPage;
			}
		}

		// Token: 0x04001C67 RID: 7271
		private readonly NavigationLinkWrapperInlineEntries value;

		// Token: 0x04001C68 RID: 7272
		private readonly IODataNavigationLinkWrapper navigationLinkWrapper;
	}
}
