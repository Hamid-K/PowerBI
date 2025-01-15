using System;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000065 RID: 101
	internal static class ExpressionBuilder
	{
		// Token: 0x0600020F RID: 527 RVA: 0x0000B85A File Offset: 0x00009A5A
		internal static string Build(string fieldName)
		{
			return ExpressionBuilder.FieldPrefix + fieldName + ExpressionBuilder.ValuePostfix;
		}

		// Token: 0x0400016F RID: 367
		internal static readonly string FieldPrefix = "=Fields!";

		// Token: 0x04000170 RID: 368
		internal static readonly string ValuePostfix = ".Value";
	}
}
