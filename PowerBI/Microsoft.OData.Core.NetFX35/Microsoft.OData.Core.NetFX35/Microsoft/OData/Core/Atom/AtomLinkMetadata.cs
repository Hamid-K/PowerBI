using System;

namespace Microsoft.OData.Core.Atom
{
	// Token: 0x02000015 RID: 21
	public sealed class AtomLinkMetadata : ODataAnnotatable
	{
		// Token: 0x0600009F RID: 159 RVA: 0x0000321F File Offset: 0x0000141F
		public AtomLinkMetadata()
		{
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003228 File Offset: 0x00001428
		internal AtomLinkMetadata(AtomLinkMetadata other)
		{
			if (other == null)
			{
				return;
			}
			this.Relation = other.Relation;
			this.Href = other.Href;
			this.HrefLang = other.HrefLang;
			this.Title = other.Title;
			this.MediaType = other.MediaType;
			this.Length = other.Length;
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00003287 File Offset: 0x00001487
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x0000328F File Offset: 0x0000148F
		public Uri Href { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00003298 File Offset: 0x00001498
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x000032A0 File Offset: 0x000014A0
		public string Relation { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x000032A9 File Offset: 0x000014A9
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x000032B1 File Offset: 0x000014B1
		public string MediaType { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x000032BA File Offset: 0x000014BA
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x000032C2 File Offset: 0x000014C2
		public string HrefLang { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x000032CB File Offset: 0x000014CB
		// (set) Token: 0x060000AA RID: 170 RVA: 0x000032D3 File Offset: 0x000014D3
		public string Title { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000AB RID: 171 RVA: 0x000032DC File Offset: 0x000014DC
		// (set) Token: 0x060000AC RID: 172 RVA: 0x000032E4 File Offset: 0x000014E4
		public int? Length { get; set; }
	}
}
