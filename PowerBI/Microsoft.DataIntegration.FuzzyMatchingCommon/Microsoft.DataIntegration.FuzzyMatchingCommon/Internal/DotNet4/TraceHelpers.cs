using System;
using System.Diagnostics;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Internal.DotNet4
{
	// Token: 0x02000035 RID: 53
	internal static class TraceHelpers
	{
		// Token: 0x06000193 RID: 403 RVA: 0x000114D1 File Offset: 0x0000F6D1
		[Conditional("PFXTRACE")]
		internal static void AddListener(TraceListener listener)
		{
		}

		// Token: 0x06000194 RID: 404 RVA: 0x000114D3 File Offset: 0x0000F6D3
		[Conditional("PFXTRACE")]
		internal static void SetVerbose()
		{
		}

		// Token: 0x06000195 RID: 405 RVA: 0x000114D5 File Offset: 0x0000F6D5
		[Conditional("DEBUG")]
		internal static void Assert(bool condition)
		{
		}

		// Token: 0x06000196 RID: 406 RVA: 0x000114D7 File Offset: 0x0000F6D7
		[Conditional("DEBUG")]
		internal static void Assert(bool condition, string message)
		{
		}

		// Token: 0x06000197 RID: 407 RVA: 0x000114D9 File Offset: 0x0000F6D9
		internal static void TraceInfo(string msg, params object[] args)
		{
		}

		// Token: 0x06000198 RID: 408 RVA: 0x000114DB File Offset: 0x0000F6DB
		[Conditional("PFXTRACE")]
		internal static void TraceWarning(string msg, params object[] args)
		{
		}

		// Token: 0x06000199 RID: 409 RVA: 0x000114DD File Offset: 0x0000F6DD
		[Conditional("PFXTRACE")]
		internal static void TraceError(string msg, params object[] args)
		{
		}
	}
}
