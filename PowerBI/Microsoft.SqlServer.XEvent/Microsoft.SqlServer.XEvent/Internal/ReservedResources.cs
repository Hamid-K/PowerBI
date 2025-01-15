using System;

namespace Microsoft.SqlServer.XEvent.Internal
{
	// Token: 0x0200004B RID: 75
	public static class ReservedResources
	{
		// Token: 0x06000182 RID: 386 RVA: 0x00003B64 File Offset: 0x00003B64
		public static string GetReservedResourceString(ReservedResources.ReservedResourceIds resId)
		{
			return ReservedResources.sm_ReservedResourceIdentifiers[(int)resId];
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00003B80 File Offset: 0x00003B80
		// Note: this type is marked as 'beforefieldinit'.
		static ReservedResources()
		{
			string text = "";
			ReservedResources.sm_ReservedResourceIdentifiers[0] = text;
			string text2 = "GuidColumnDescription";
			ReservedResources.sm_ReservedResourceIdentifiers[1] = text2;
			string text3 = "VersionColumnDescription";
			ReservedResources.sm_ReservedResourceIdentifiers[2] = text3;
			string text4 = "ChannelColumnDescription";
			ReservedResources.sm_ReservedResourceIdentifiers[3] = text4;
			string text5 = "KeywordColumnDescription";
			ReservedResources.sm_ReservedResourceIdentifiers[4] = text5;
		}

		// Token: 0x0400013C RID: 316
		private static readonly string[] sm_ReservedResourceIdentifiers = new string[5];

		// Token: 0x0200004C RID: 76
		public enum ReservedResourceIds
		{
			// Token: 0x0400013E RID: 318
			GuidColumn = 1,
			// Token: 0x0400013F RID: 319
			VersionColumn,
			// Token: 0x04000140 RID: 320
			ChannelColumn,
			// Token: 0x04000141 RID: 321
			KeywordColumn,
			// Token: 0x04000142 RID: 322
			FirstUserResourceId
		}
	}
}
