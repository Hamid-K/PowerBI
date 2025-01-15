using System;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x0200027B RID: 635
	public sealed class AtomLinkMetadata : ODataAnnotatable
	{
		// Token: 0x060013F5 RID: 5109 RVA: 0x00049E5C File Offset: 0x0004805C
		public AtomLinkMetadata()
		{
		}

		// Token: 0x060013F6 RID: 5110 RVA: 0x00049E64 File Offset: 0x00048064
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
			this.hrefFromEpm = other.hrefFromEpm;
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x060013F7 RID: 5111 RVA: 0x00049ECF File Offset: 0x000480CF
		// (set) Token: 0x060013F8 RID: 5112 RVA: 0x00049ED7 File Offset: 0x000480D7
		public Uri Href { get; set; }

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x060013F9 RID: 5113 RVA: 0x00049EE0 File Offset: 0x000480E0
		// (set) Token: 0x060013FA RID: 5114 RVA: 0x00049EE8 File Offset: 0x000480E8
		public string Relation { get; set; }

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x060013FB RID: 5115 RVA: 0x00049EF1 File Offset: 0x000480F1
		// (set) Token: 0x060013FC RID: 5116 RVA: 0x00049EF9 File Offset: 0x000480F9
		public string MediaType { get; set; }

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x060013FD RID: 5117 RVA: 0x00049F02 File Offset: 0x00048102
		// (set) Token: 0x060013FE RID: 5118 RVA: 0x00049F0A File Offset: 0x0004810A
		public string HrefLang { get; set; }

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x060013FF RID: 5119 RVA: 0x00049F13 File Offset: 0x00048113
		// (set) Token: 0x06001400 RID: 5120 RVA: 0x00049F1B File Offset: 0x0004811B
		public string Title { get; set; }

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06001401 RID: 5121 RVA: 0x00049F24 File Offset: 0x00048124
		// (set) Token: 0x06001402 RID: 5122 RVA: 0x00049F2C File Offset: 0x0004812C
		public int? Length { get; set; }

		// Token: 0x040007AC RID: 1964
		private string hrefFromEpm;
	}
}
