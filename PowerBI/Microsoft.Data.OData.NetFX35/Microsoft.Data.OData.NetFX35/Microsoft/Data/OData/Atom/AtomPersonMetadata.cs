using System;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x0200027D RID: 637
	public sealed class AtomPersonMetadata : ODataAnnotatable
	{
		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x0600140B RID: 5131 RVA: 0x00049FA0 File Offset: 0x000481A0
		// (set) Token: 0x0600140C RID: 5132 RVA: 0x00049FA8 File Offset: 0x000481A8
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x0600140D RID: 5133 RVA: 0x00049FB1 File Offset: 0x000481B1
		// (set) Token: 0x0600140E RID: 5134 RVA: 0x00049FB9 File Offset: 0x000481B9
		public Uri Uri { get; set; }

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x0600140F RID: 5135 RVA: 0x00049FC2 File Offset: 0x000481C2
		// (set) Token: 0x06001410 RID: 5136 RVA: 0x00049FCA File Offset: 0x000481CA
		public string Email
		{
			get
			{
				return this.email;
			}
			set
			{
				this.email = value;
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06001411 RID: 5137 RVA: 0x00049FD3 File Offset: 0x000481D3
		// (set) Token: 0x06001412 RID: 5138 RVA: 0x00049FDB File Offset: 0x000481DB
		internal string UriFromEpm
		{
			get
			{
				return this.uriFromEpm;
			}
			set
			{
				this.uriFromEpm = value;
			}
		}

		// Token: 0x06001413 RID: 5139 RVA: 0x00049FE4 File Offset: 0x000481E4
		public static AtomPersonMetadata ToAtomPersonMetadata(string name)
		{
			return new AtomPersonMetadata
			{
				Name = name
			};
		}

		// Token: 0x06001414 RID: 5140 RVA: 0x00049FFF File Offset: 0x000481FF
		public static implicit operator AtomPersonMetadata(string name)
		{
			return AtomPersonMetadata.ToAtomPersonMetadata(name);
		}

		// Token: 0x040007B6 RID: 1974
		private string name;

		// Token: 0x040007B7 RID: 1975
		private string email;

		// Token: 0x040007B8 RID: 1976
		private string uriFromEpm;
	}
}
