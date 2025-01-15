using System;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000060 RID: 96
	public static class LinguisticSchemaServicesBuilderOptionsExtensions
	{
		// Token: 0x06000175 RID: 373 RVA: 0x000043A2 File Offset: 0x000025A2
		public static bool UseEmptyServices(this LinguisticSchemaServicesBuilderOptions options)
		{
			return options.HasOption(LinguisticSchemaServicesBuilderOptions.UseEmptyServices);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x000043AB File Offset: 0x000025AB
		private static bool HasOption(this LinguisticSchemaServicesBuilderOptions options, LinguisticSchemaServicesBuilderOptions flag)
		{
			return (options & flag) == flag;
		}
	}
}
