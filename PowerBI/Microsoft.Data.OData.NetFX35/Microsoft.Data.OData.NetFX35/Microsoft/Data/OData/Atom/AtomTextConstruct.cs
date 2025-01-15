using System;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x02000221 RID: 545
	public sealed class AtomTextConstruct : ODataAnnotatable
	{
		// Token: 0x1700039A RID: 922
		// (get) Token: 0x0600101C RID: 4124 RVA: 0x0003CED2 File Offset: 0x0003B0D2
		// (set) Token: 0x0600101D RID: 4125 RVA: 0x0003CEDA File Offset: 0x0003B0DA
		public AtomTextConstructKind Kind { get; set; }

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x0600101E RID: 4126 RVA: 0x0003CEE3 File Offset: 0x0003B0E3
		// (set) Token: 0x0600101F RID: 4127 RVA: 0x0003CEEB File Offset: 0x0003B0EB
		public string Text { get; set; }

		// Token: 0x06001020 RID: 4128 RVA: 0x0003CEF4 File Offset: 0x0003B0F4
		public static AtomTextConstruct ToTextConstruct(string text)
		{
			return new AtomTextConstruct
			{
				Text = text
			};
		}

		// Token: 0x06001021 RID: 4129 RVA: 0x0003CF0F File Offset: 0x0003B10F
		public static implicit operator AtomTextConstruct(string text)
		{
			return AtomTextConstruct.ToTextConstruct(text);
		}
	}
}
