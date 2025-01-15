using System;

namespace Microsoft.PowerBI.ExploreHost.Lucia
{
	// Token: 0x02000024 RID: 36
	public static class LuciaSessionOptionsExtensions
	{
		// Token: 0x060000B4 RID: 180 RVA: 0x000038D4 File Offset: 0x00001AD4
		internal static bool IsEmulation(this LuciaSessionOptions options)
		{
			return LuciaSessionOptionsExtensions.HasFlag(options, LuciaSessionOptions.Emulation);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000038DD File Offset: 0x00001ADD
		internal static bool IsLiveConnectToOnPremAS(this LuciaSessionOptions options)
		{
			return LuciaSessionOptionsExtensions.HasFlag(options, LuciaSessionOptions.LiveConnectToOnPremAS);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000038E6 File Offset: 0x00001AE6
		internal static bool IsLiveConnectToPBIService(this LuciaSessionOptions options)
		{
			return LuciaSessionOptionsExtensions.HasFlag(options, LuciaSessionOptions.LiveConnectToPBIService);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000038EF File Offset: 0x00001AEF
		internal static bool IsLiveConnectToPBIServiceOnPrem(this LuciaSessionOptions options)
		{
			return LuciaSessionOptionsExtensions.HasFlag(options, LuciaSessionOptions.LiveConnectToPBIServiceOnPrem);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000038F8 File Offset: 0x00001AF8
		internal static bool AllowNonOverlapPartialMatch(this LuciaSessionOptions options)
		{
			return LuciaSessionOptionsExtensions.HasFlag(options, LuciaSessionOptions.AllowNonOverlapPartialMatch);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00003902 File Offset: 0x00001B02
		private static bool HasFlag(LuciaSessionOptions options, LuciaSessionOptions flag)
		{
			return (options & flag) == flag;
		}
	}
}
