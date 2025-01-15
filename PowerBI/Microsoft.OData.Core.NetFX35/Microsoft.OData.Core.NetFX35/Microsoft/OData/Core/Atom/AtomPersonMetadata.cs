using System;

namespace Microsoft.OData.Core.Atom
{
	// Token: 0x02000017 RID: 23
	public sealed class AtomPersonMetadata : ODataAnnotatable
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00003438 File Offset: 0x00001638
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x00003440 File Offset: 0x00001640
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

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00003449 File Offset: 0x00001649
		// (set) Token: 0x060000BA RID: 186 RVA: 0x00003451 File Offset: 0x00001651
		public Uri Uri { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000BB RID: 187 RVA: 0x0000345A File Offset: 0x0000165A
		// (set) Token: 0x060000BC RID: 188 RVA: 0x00003462 File Offset: 0x00001662
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

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000BD RID: 189 RVA: 0x0000346B File Offset: 0x0000166B
		// (set) Token: 0x060000BE RID: 190 RVA: 0x00003473 File Offset: 0x00001673
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

		// Token: 0x060000BF RID: 191 RVA: 0x0000347C File Offset: 0x0000167C
		public static AtomPersonMetadata ToAtomPersonMetadata(string name)
		{
			return new AtomPersonMetadata
			{
				Name = name
			};
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00003497 File Offset: 0x00001697
		public static implicit operator AtomPersonMetadata(string name)
		{
			return AtomPersonMetadata.ToAtomPersonMetadata(name);
		}

		// Token: 0x040000BF RID: 191
		private string name;

		// Token: 0x040000C0 RID: 192
		private string email;

		// Token: 0x040000C1 RID: 193
		private string uriFromEpm;
	}
}
