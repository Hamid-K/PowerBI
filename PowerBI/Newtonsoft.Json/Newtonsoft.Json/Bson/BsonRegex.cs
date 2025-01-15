using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x02000111 RID: 273
	internal class BsonRegex : BsonToken
	{
		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000DBC RID: 3516 RVA: 0x00037167 File Offset: 0x00035367
		// (set) Token: 0x06000DBD RID: 3517 RVA: 0x0003716F File Offset: 0x0003536F
		public BsonString Pattern { get; set; }

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000DBE RID: 3518 RVA: 0x00037178 File Offset: 0x00035378
		// (set) Token: 0x06000DBF RID: 3519 RVA: 0x00037180 File Offset: 0x00035380
		public BsonString Options { get; set; }

		// Token: 0x06000DC0 RID: 3520 RVA: 0x00037189 File Offset: 0x00035389
		public BsonRegex(string pattern, string options)
		{
			this.Pattern = new BsonString(pattern, false);
			this.Options = new BsonString(options, false);
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000DC1 RID: 3521 RVA: 0x000371AB File Offset: 0x000353AB
		public override BsonType Type
		{
			get
			{
				return BsonType.Regex;
			}
		}
	}
}
