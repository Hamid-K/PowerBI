using System;
using AngleSharp.Extensions;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x02000367 RID: 871
	internal sealed class HtmlIsIndexElement : HtmlElement
	{
		// Token: 0x06001B38 RID: 6968 RVA: 0x00052FB8 File Offset: 0x000511B8
		public HtmlIsIndexElement(Document owner, string prefix = null)
			: base(owner, TagNames.IsIndex, prefix, NodeFlags.Special)
		{
		}

		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x06001B39 RID: 6969 RVA: 0x00052FC8 File Offset: 0x000511C8
		// (set) Token: 0x06001B3A RID: 6970 RVA: 0x00052FD0 File Offset: 0x000511D0
		public IHtmlFormElement Form { get; internal set; }

		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x06001B3B RID: 6971 RVA: 0x00052FD9 File Offset: 0x000511D9
		// (set) Token: 0x06001B3C RID: 6972 RVA: 0x00052FE6 File Offset: 0x000511E6
		public string Prompt
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Prompt);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Prompt, value, false);
			}
		}
	}
}
