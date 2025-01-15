using System;
using System.Data.Entity.Internal;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x0200023F RID: 575
	[Obsolete("EdmMetadata is no longer used. The Code First Migrations <see cref=\"EdmModelDiffer\" /> is used instead.")]
	public class EdmMetadata
	{
		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x06001E39 RID: 7737 RVA: 0x000545A5 File Offset: 0x000527A5
		// (set) Token: 0x06001E3A RID: 7738 RVA: 0x000545AD File Offset: 0x000527AD
		public int Id { get; set; }

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x06001E3B RID: 7739 RVA: 0x000545B6 File Offset: 0x000527B6
		// (set) Token: 0x06001E3C RID: 7740 RVA: 0x000545BE File Offset: 0x000527BE
		public string ModelHash { get; set; }

		// Token: 0x06001E3D RID: 7741 RVA: 0x000545C8 File Offset: 0x000527C8
		public static string TryGetModelHash(DbContext context)
		{
			Check.NotNull<DbContext>(context, "context");
			DbCompiledModel codeFirstModel = context.InternalContext.CodeFirstModel;
			if (codeFirstModel != null)
			{
				return new ModelHashCalculator().Calculate(codeFirstModel);
			}
			return null;
		}
	}
}
