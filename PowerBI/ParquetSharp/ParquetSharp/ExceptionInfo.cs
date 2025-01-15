using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ParquetSharp
{
	// Token: 0x02000037 RID: 55
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class ExceptionInfo
	{
		// Token: 0x0600016A RID: 362 RVA: 0x000058B0 File Offset: 0x00003AB0
		public static void Check(IntPtr exceptionInfo)
		{
			if (exceptionInfo == IntPtr.Zero)
			{
				return;
			}
			string text = StringUtil.PtrToStringUtf8(ExceptionInfo.ExceptionInfo_Type(exceptionInfo));
			string text2 = StringUtil.PtrToStringUtf8(ExceptionInfo.ExceptionInfo_Message(exceptionInfo));
			StatusCode statusCode = ExceptionInfo.ExceptionInfo_StatusCode(exceptionInfo);
			ExceptionInfo.ExceptionInfo_Free(exceptionInfo);
			if ((int)statusCode == -1)
			{
				throw new ParquetException(text, text2);
			}
			throw new ParquetStatusException(text, text2, statusCode);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00005910 File Offset: 0x00003B10
		public static TValue Return<[Nullable(2)] TValue>(ExceptionInfo.GetAction<TValue> getter)
		{
			TValue tvalue;
			ExceptionInfo.Check(getter(out tvalue));
			return tvalue;
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00005930 File Offset: 0x00003B30
		public static TValue Return<[Nullable(2)] TArg0, [Nullable(2)] TValue>(TArg0 arg0, ExceptionInfo.GetAction<TArg0, TValue> getter)
		{
			TValue tvalue;
			ExceptionInfo.Check(getter(arg0, out tvalue));
			return tvalue;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00005950 File Offset: 0x00003B50
		public static TValue Return<[Nullable(2)] TArg0, [Nullable(2)] TArg1, [Nullable(2)] TValue>(TArg0 arg0, TArg1 arg1, ExceptionInfo.GetAction<TArg0, TArg1, TValue> getter)
		{
			TValue tvalue;
			ExceptionInfo.Check(getter(arg0, arg1, out tvalue));
			return tvalue;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00005974 File Offset: 0x00003B74
		public static TValue Return<[Nullable(2)] TArg0, [Nullable(2)] TArg1, [Nullable(2)] TArg2, [Nullable(2)] TValue>(TArg0 arg0, TArg1 arg1, TArg2 arg2, ExceptionInfo.GetAction<TArg0, TArg1, TArg2, TValue> getter)
		{
			TValue tvalue;
			ExceptionInfo.Check(getter(arg0, arg1, arg2, out tvalue));
			return tvalue;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00005998 File Offset: 0x00003B98
		public static TValue Return<[Nullable(2)] TValue>(ParquetHandle handle, ExceptionInfo.GetFunction<TValue> getter)
		{
			TValue tvalue = ExceptionInfo.Return<TValue>(handle.IntPtr, getter);
			GC.KeepAlive(handle);
			return tvalue;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x000059AC File Offset: 0x00003BAC
		public static TValue Return<[Nullable(2)] TValue>(IntPtr handle, ExceptionInfo.GetFunction<TValue> getter)
		{
			TValue tvalue;
			ExceptionInfo.Check(getter(handle, out tvalue));
			return tvalue;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x000059CC File Offset: 0x00003BCC
		public static TValue Return<[Nullable(2)] TValue>(ParquetHandle handle, ParquetHandle arg0, ExceptionInfo.GetFunction<IntPtr, TValue> getter)
		{
			TValue tvalue = ExceptionInfo.Return<IntPtr, TValue>(handle.IntPtr, arg0.IntPtr, getter);
			GC.KeepAlive(handle);
			return tvalue;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x000059E8 File Offset: 0x00003BE8
		public static TValue Return<[Nullable(2)] TArg0, [Nullable(2)] TValue>(ParquetHandle handle, TArg0 arg0, ExceptionInfo.GetFunction<TArg0, TValue> getter)
		{
			TValue tvalue = ExceptionInfo.Return<TArg0, TValue>(handle.IntPtr, arg0, getter);
			GC.KeepAlive(handle);
			return tvalue;
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00005A00 File Offset: 0x00003C00
		public static TValue Return<[Nullable(2)] TArg0, [Nullable(2)] TValue>(IntPtr handle, TArg0 arg0, ExceptionInfo.GetFunction<TArg0, TValue> getter)
		{
			TValue tvalue;
			ExceptionInfo.Check(getter(handle, arg0, out tvalue));
			return tvalue;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00005A24 File Offset: 0x00003C24
		public static TValue Return<[Nullable(2)] TArg0, [Nullable(2)] TArg1, [Nullable(2)] TValue>(ParquetHandle handle, TArg0 arg0, TArg1 arg1, ExceptionInfo.GetFunction<TArg0, TArg1, TValue> getter)
		{
			TValue tvalue = ExceptionInfo.Return<TArg0, TArg1, TValue>(handle.IntPtr, arg0, arg1, getter);
			GC.KeepAlive(handle);
			return tvalue;
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00005A3C File Offset: 0x00003C3C
		public static TValue Return<[Nullable(2)] TArg0, [Nullable(2)] TArg1, [Nullable(2)] TValue>(IntPtr handle, TArg0 arg0, TArg1 arg1, ExceptionInfo.GetFunction<TArg0, TArg1, TValue> getter)
		{
			TValue tvalue;
			ExceptionInfo.Check(getter(handle, arg0, arg1, out tvalue));
			return tvalue;
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00005A60 File Offset: 0x00003C60
		public static string ReturnString(IntPtr handle, ExceptionInfo.GetFunction<IntPtr> getter, [Nullable(2)] Action<IntPtr> deleter = null)
		{
			IntPtr intPtr;
			ExceptionInfo.Check(getter(handle, out intPtr));
			return ExceptionInfo.ConvertPtrToString(handle, deleter, intPtr);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00005A88 File Offset: 0x00003C88
		public static string ReturnString(ParquetHandle handle, ExceptionInfo.GetFunction<IntPtr> getter, [Nullable(2)] Action<IntPtr> deleter = null)
		{
			IntPtr intPtr;
			ExceptionInfo.Check(getter(handle.IntPtr, out intPtr));
			return ExceptionInfo.ConvertPtrToString(handle, deleter, intPtr);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00005AB4 File Offset: 0x00003CB4
		private static string ConvertPtrToString(IntPtr handle, [Nullable(2)] Action<IntPtr> deleter, IntPtr value)
		{
			string text = StringUtil.PtrToStringUtf8(value);
			if (deleter != null)
			{
				deleter(value);
			}
			return text;
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00005ACC File Offset: 0x00003CCC
		private static string ConvertPtrToString(ParquetHandle handle, [Nullable(2)] Action<IntPtr> deleter, IntPtr value)
		{
			string text = StringUtil.PtrToStringUtf8(value);
			if (deleter != null)
			{
				deleter(value);
			}
			GC.KeepAlive(handle);
			return text;
		}

		// Token: 0x0600017A RID: 378
		[DllImport("ParquetSharpNative")]
		private static extern void ExceptionInfo_Free(IntPtr exceptionInfo);

		// Token: 0x0600017B RID: 379
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ExceptionInfo_Type(IntPtr exceptionInfo);

		// Token: 0x0600017C RID: 380
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ExceptionInfo_Message(IntPtr exceptionInfo);

		// Token: 0x0600017D RID: 381
		[DllImport("ParquetSharpNative")]
		private static extern StatusCode ExceptionInfo_StatusCode(IntPtr exceptionInfo);

		// Token: 0x02000103 RID: 259
		// (Invoke) Token: 0x0600092A RID: 2346
		[NullableContext(0)]
		public delegate IntPtr GetAction<[Nullable(2)] TValue>(out TValue value);

		// Token: 0x02000104 RID: 260
		// (Invoke) Token: 0x0600092E RID: 2350
		[NullableContext(0)]
		public delegate IntPtr GetAction<[Nullable(2)] in TArg0, [Nullable(2)] TValue>(TArg0 arg0, out TValue value);

		// Token: 0x02000105 RID: 261
		// (Invoke) Token: 0x06000932 RID: 2354
		[NullableContext(0)]
		public delegate IntPtr GetAction<[Nullable(2)] in TArg0, [Nullable(2)] in TArg1, [Nullable(2)] TValue>(TArg0 arg0, TArg1 arg1, out TValue value);

		// Token: 0x02000106 RID: 262
		// (Invoke) Token: 0x06000936 RID: 2358
		[NullableContext(2)]
		[Nullable(0)]
		public delegate IntPtr GetAction<in TArg0, in TArg1, in TArg2, TValue>(TArg0 arg0, TArg1 arg1, TArg2 arg2, out TValue value);

		// Token: 0x02000107 RID: 263
		// (Invoke) Token: 0x0600093A RID: 2362
		[NullableContext(0)]
		public delegate IntPtr GetFunction<[Nullable(2)] TValue>(IntPtr handle, out TValue value);

		// Token: 0x02000108 RID: 264
		// (Invoke) Token: 0x0600093E RID: 2366
		[NullableContext(0)]
		public delegate IntPtr GetFunction<[Nullable(2)] in TArg0, [Nullable(2)] TValue>(IntPtr handle, TArg0 arg0, out TValue value);

		// Token: 0x02000109 RID: 265
		// (Invoke) Token: 0x06000942 RID: 2370
		[NullableContext(0)]
		public delegate IntPtr GetFunction<[Nullable(2)] in TArg0, [Nullable(2)] in TArg1, [Nullable(2)] TValue>(IntPtr handle, TArg0 arg0, TArg1 arg1, out TValue value);
	}
}
