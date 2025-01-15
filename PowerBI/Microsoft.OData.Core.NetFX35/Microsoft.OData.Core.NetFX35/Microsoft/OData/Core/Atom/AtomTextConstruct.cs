using System;

namespace Microsoft.OData.Core.Atom
{
	// Token: 0x0200001A RID: 26
	public sealed class AtomTextConstruct : ODataAnnotatable
	{
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000CE RID: 206 RVA: 0x0000350C File Offset: 0x0000170C
		// (set) Token: 0x060000CF RID: 207 RVA: 0x00003514 File Offset: 0x00001714
		public AtomTextConstructKind Kind { get; set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x0000351D File Offset: 0x0000171D
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x00003525 File Offset: 0x00001725
		public string Text { get; set; }

		// Token: 0x060000D2 RID: 210 RVA: 0x00003530 File Offset: 0x00001730
		public static AtomTextConstruct ToTextConstruct(string text)
		{
			return new AtomTextConstruct
			{
				Text = text
			};
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x0000354B File Offset: 0x0000174B
		public static implicit operator AtomTextConstruct(string text)
		{
			return AtomTextConstruct.ToTextConstruct(text);
		}
	}
}
