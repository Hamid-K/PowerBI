using System;

namespace Microsoft.Lucia
{
	// Token: 0x02000010 RID: 16
	public static class ConditionalResult
	{
		// Token: 0x06000036 RID: 54 RVA: 0x000027A8 File Offset: 0x000009A8
		public static ConditionalResult<T> Create<T>(bool succeeded, T result)
		{
			return new ConditionalResult<T>(succeeded, result);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000027B1 File Offset: 0x000009B1
		public static ConditionalResult<global::System.ValueTuple<T1, T2>> Create<T1, T2>(bool succeeded, T1 item1, T2 item2)
		{
			return ConditionalResult.Create<global::System.ValueTuple<T1, T2>>(succeeded, new global::System.ValueTuple<T1, T2>(item1, item2));
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000027C0 File Offset: 0x000009C0
		public static ConditionalResult<global::System.ValueTuple<T1, T2, T3>> Create<T1, T2, T3>(bool succeeded, T1 item1, T2 item2, T3 item3)
		{
			return ConditionalResult.Create<global::System.ValueTuple<T1, T2, T3>>(succeeded, new global::System.ValueTuple<T1, T2, T3>(item1, item2, item3));
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000027D0 File Offset: 0x000009D0
		public static ConditionalResult<global::System.ValueTuple<T1, T2, T3, T4>> Create<T1, T2, T3, T4>(bool succeeded, T1 item1, T2 item2, T3 item3, T4 item4)
		{
			return ConditionalResult.Create<global::System.ValueTuple<T1, T2, T3, T4>>(succeeded, new global::System.ValueTuple<T1, T2, T3, T4>(item1, item2, item3, item4));
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000027E2 File Offset: 0x000009E2
		public static ConditionalResult<T> Success<T>(T result)
		{
			return ConditionalResult.Create<T>(true, result);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000027EB File Offset: 0x000009EB
		public static ConditionalResult<global::System.ValueTuple<T1, T2>> Success<T1, T2>(T1 item1, T2 item2)
		{
			return ConditionalResult.Success<global::System.ValueTuple<T1, T2>>(new global::System.ValueTuple<T1, T2>(item1, item2));
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000027F9 File Offset: 0x000009F9
		public static ConditionalResult<global::System.ValueTuple<T1, T2, T3>> Success<T1, T2, T3>(T1 item1, T2 item2, T3 item3)
		{
			return ConditionalResult.Success<global::System.ValueTuple<T1, T2, T3>>(new global::System.ValueTuple<T1, T2, T3>(item1, item2, item3));
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002808 File Offset: 0x00000A08
		public static ConditionalResult<global::System.ValueTuple<T1, T2, T3, T4>> Success<T1, T2, T3, T4>(T1 item1, T2 item2, T3 item3, T4 item4)
		{
			return ConditionalResult.Success<global::System.ValueTuple<T1, T2, T3, T4>>(new global::System.ValueTuple<T1, T2, T3, T4>(item1, item2, item3, item4));
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002818 File Offset: 0x00000A18
		public static FailedConditionalResult Failure()
		{
			return default(FailedConditionalResult);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002830 File Offset: 0x00000A30
		public static ConditionalResult<T> Failure<T>()
		{
			return ConditionalResult.Create<T>(false, default(T));
		}

		// Token: 0x06000040 RID: 64 RVA: 0x0000284C File Offset: 0x00000A4C
		public static ConditionalResult<global::System.ValueTuple<T1, T2>> Failure<T1, T2>()
		{
			return ConditionalResult.Failure<global::System.ValueTuple<T1, T2>>();
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002853 File Offset: 0x00000A53
		public static ConditionalResult<global::System.ValueTuple<T1, T2, T3>> Failure<T1, T2, T3>()
		{
			return ConditionalResult.Failure<global::System.ValueTuple<T1, T2, T3>>();
		}

		// Token: 0x06000042 RID: 66 RVA: 0x0000285A File Offset: 0x00000A5A
		public static ConditionalResult<global::System.ValueTuple<T1, T2, T3, T4>> Failure<T1, T2, T3, T4>()
		{
			return ConditionalResult.Failure<global::System.ValueTuple<T1, T2, T3, T4>>();
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002861 File Offset: 0x00000A61
		public static bool TryGetResult<T>(ConditionalResult<T> conditional, out T result)
		{
			result = conditional.Result;
			return conditional.Succeeded;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002878 File Offset: 0x00000A78
		public static bool TryGetResult<T1, T2>(ConditionalResult<global::System.ValueTuple<T1, T2>> conditional, out T1 item1, out T2 item2)
		{
			global::System.ValueTuple<T1, T2> result = conditional.Result;
			item1 = result.Item1;
			item2 = result.Item2;
			return conditional.Succeeded;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000028AC File Offset: 0x00000AAC
		public static bool TryGetResult<T1, T2, T3>(ConditionalResult<global::System.ValueTuple<T1, T2, T3>> conditional, out T1 item1, out T2 item2, out T3 item3)
		{
			global::System.ValueTuple<T1, T2, T3> result = conditional.Result;
			item1 = result.Item1;
			item2 = result.Item2;
			item3 = result.Item3;
			return conditional.Succeeded;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000028EC File Offset: 0x00000AEC
		public static bool TryGetResult<T1, T2, T3, T4>(ConditionalResult<global::System.ValueTuple<T1, T2, T3, T4>> conditional, out T1 item1, out T2 item2, out T3 item3, out T4 item4)
		{
			global::System.ValueTuple<T1, T2, T3, T4> result = conditional.Result;
			item1 = result.Item1;
			item2 = result.Item2;
			item3 = result.Item3;
			item4 = result.Item4;
			return conditional.Succeeded;
		}
	}
}
