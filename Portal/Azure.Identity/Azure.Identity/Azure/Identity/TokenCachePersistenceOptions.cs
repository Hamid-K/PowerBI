using System;

namespace Azure.Identity
{
	// Token: 0x02000084 RID: 132
	public class TokenCachePersistenceOptions
	{
		// Token: 0x1700013A RID: 314
		// (get) Token: 0x0600046B RID: 1131 RVA: 0x0000DB4C File Offset: 0x0000BD4C
		// (set) Token: 0x0600046C RID: 1132 RVA: 0x0000DB54 File Offset: 0x0000BD54
		public string Name { get; set; }

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x0000DB5D File Offset: 0x0000BD5D
		// (set) Token: 0x0600046E RID: 1134 RVA: 0x0000DB65 File Offset: 0x0000BD65
		public bool UnsafeAllowUnencryptedStorage { get; set; }

		// Token: 0x0600046F RID: 1135 RVA: 0x0000DB6E File Offset: 0x0000BD6E
		internal TokenCachePersistenceOptions Clone()
		{
			return new TokenCachePersistenceOptions
			{
				Name = this.Name,
				UnsafeAllowUnencryptedStorage = this.UnsafeAllowUnencryptedStorage
			};
		}
	}
}
