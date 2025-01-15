using System;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000A7 RID: 167
	internal static class InterpretWarningsExtensions
	{
		// Token: 0x0600035F RID: 863 RVA: 0x00006C5D File Offset: 0x00004E5D
		public static bool HasDataModelLoadPartiallySuccessful(this InterpretWarnings warnings)
		{
			return warnings.HasFlag(InterpretWarnings.DataModelLoadPartiallySuccessful);
		}

		// Token: 0x06000360 RID: 864 RVA: 0x00006C67 File Offset: 0x00004E67
		public static bool HasLinguisticSchemaIsStillLoading(this InterpretWarnings warnings)
		{
			return warnings.HasFlag(InterpretWarnings.LinguisticSchemaIsStillLoading);
		}

		// Token: 0x06000361 RID: 865 RVA: 0x00006C71 File Offset: 0x00004E71
		public static bool HasInvalidDataModel(this InterpretWarnings warnings)
		{
			return warnings.HasFlag(InterpretWarnings.InvalidDataModel);
		}

		// Token: 0x06000362 RID: 866 RVA: 0x00006C7C File Offset: 0x00004E7C
		public static bool HasLinguisticSchemaNotAvailable(this InterpretWarnings warnings)
		{
			return warnings.HasFlag(InterpretWarnings.LinguisticSchemaNotAvailable);
		}

		// Token: 0x06000363 RID: 867 RVA: 0x00006C86 File Offset: 0x00004E86
		public static bool HasSchemaLanguageNotSupported(this InterpretWarnings warnings)
		{
			return warnings.HasFlag(InterpretWarnings.SchemaLanguageNotSupported);
		}

		// Token: 0x06000364 RID: 868 RVA: 0x00006C94 File Offset: 0x00004E94
		private static bool HasFlag(this InterpretWarnings warnings, InterpretWarnings inputWarning)
		{
			return (warnings & inputWarning) == inputWarning;
		}
	}
}
