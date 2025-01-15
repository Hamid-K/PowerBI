using System;

namespace Microsoft.Identity.Json.Bson
{
	// Token: 0x02000110 RID: 272
	internal class BsonRegex : BsonToken
	{
		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000DA2 RID: 3490 RVA: 0x00036813 File Offset: 0x00034A13
		// (set) Token: 0x06000DA3 RID: 3491 RVA: 0x0003681B File Offset: 0x00034A1B
		public BsonString Pattern { get; set; }

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000DA4 RID: 3492 RVA: 0x00036824 File Offset: 0x00034A24
		// (set) Token: 0x06000DA5 RID: 3493 RVA: 0x0003682C File Offset: 0x00034A2C
		public BsonString Options { get; set; }

		// Token: 0x06000DA6 RID: 3494 RVA: 0x00036835 File Offset: 0x00034A35
		public BsonRegex(string pattern, string options)
		{
			this.Pattern = new BsonString(pattern, false);
			this.Options = new BsonString(options, false);
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000DA7 RID: 3495 RVA: 0x00036857 File Offset: 0x00034A57
		public override BsonType Type
		{
			get
			{
				return BsonType.Regex;
			}
		}
	}
}
