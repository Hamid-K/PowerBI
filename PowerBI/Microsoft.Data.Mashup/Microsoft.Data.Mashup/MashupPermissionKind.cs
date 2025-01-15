using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Data.Mashup
{
	// Token: 0x02000038 RID: 56
	public static class MashupPermissionKind
	{
		// Token: 0x060002D5 RID: 725 RVA: 0x0000B638 File Offset: 0x00009838
		internal static string From(QueryPermissionChallengeType queryPermissionChallengeType)
		{
			return DataSourceProperties.FromQueryPermission(queryPermissionChallengeType);
		}

		// Token: 0x0400017A RID: 378
		public const string NativeQuery = "NativeQuery";

		// Token: 0x0400017B RID: 379
		public const string Redirect = "Redirect";

		// Token: 0x0400017C RID: 380
		internal static string[] KnownPermissionKinds = new string[] { "NativeQuery", "Redirect" };
	}
}
