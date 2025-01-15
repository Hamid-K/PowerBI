using System;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Storage.Database
{
	// Token: 0x02000034 RID: 52
	public sealed class CreateOrGetResult<T>
	{
		// Token: 0x06000138 RID: 312 RVA: 0x00004FE3 File Offset: 0x000031E3
		public CreateOrGetResult(bool added, T record)
		{
			this.Added = added;
			this.Record = record;
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00004FF9 File Offset: 0x000031F9
		// (set) Token: 0x0600013A RID: 314 RVA: 0x00005001 File Offset: 0x00003201
		public bool Added { get; private set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600013B RID: 315 RVA: 0x0000500A File Offset: 0x0000320A
		// (set) Token: 0x0600013C RID: 316 RVA: 0x00005012 File Offset: 0x00003212
		public T Record { get; private set; }

		// Token: 0x0600013D RID: 317 RVA: 0x0000501B File Offset: 0x0000321B
		public override string ToString()
		{
			return "<Added: {0}, Record: {1}>".FormatWithInvariantCulture(new object[] { this.Added, this.Record });
		}
	}
}
