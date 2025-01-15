using System;

namespace Microsoft.IdentityModel.Json.Bson
{
	// Token: 0x02000111 RID: 273
	internal class BsonRegex : BsonToken
	{
		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000DB2 RID: 3506 RVA: 0x0003700F File Offset: 0x0003520F
		// (set) Token: 0x06000DB3 RID: 3507 RVA: 0x00037017 File Offset: 0x00035217
		public BsonString Pattern { get; set; }

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000DB4 RID: 3508 RVA: 0x00037020 File Offset: 0x00035220
		// (set) Token: 0x06000DB5 RID: 3509 RVA: 0x00037028 File Offset: 0x00035228
		public BsonString Options { get; set; }

		// Token: 0x06000DB6 RID: 3510 RVA: 0x00037031 File Offset: 0x00035231
		public BsonRegex(string pattern, string options)
		{
			this.Pattern = new BsonString(pattern, false);
			this.Options = new BsonString(options, false);
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000DB7 RID: 3511 RVA: 0x00037053 File Offset: 0x00035253
		public override BsonType Type
		{
			get
			{
				return BsonType.Regex;
			}
		}
	}
}
