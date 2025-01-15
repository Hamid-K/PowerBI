using System;
using Microsoft.InfoNav.Data.Contracts;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x0200001A RID: 26
	public class DateTimeProviderFactory : IDateTimeProviderFactory
	{
		// Token: 0x060000F3 RID: 243 RVA: 0x0000595D File Offset: 0x00003B5D
		private DateTimeProviderFactory()
		{
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00005965 File Offset: 0x00003B65
		public IDateTimeProvider CreateDateTimeProvider(DateTime? anchorTime)
		{
			return new DateTimeProvider(anchorTime);
		}

		// Token: 0x04000098 RID: 152
		public static readonly DateTimeProviderFactory Instance = new DateTimeProviderFactory();
	}
}
