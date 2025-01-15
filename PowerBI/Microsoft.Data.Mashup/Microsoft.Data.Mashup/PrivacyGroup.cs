using System;

namespace Microsoft.Data.Mashup
{
	// Token: 0x0200004D RID: 77
	public static class PrivacyGroup
	{
		// Token: 0x040001D6 RID: 470
		public const string None = "None";

		// Token: 0x040001D7 RID: 471
		public const string Public = "Public";

		// Token: 0x040001D8 RID: 472
		public const string Organizational = "Organizational";

		// Token: 0x040001D9 RID: 473
		public const string Private = "Private";

		// Token: 0x040001DA RID: 474
		internal static string[] KnownPrivacyGroups = new string[] { "None", "Public", "Organizational", "Private" };
	}
}
