using System;

namespace Microsoft.PowerBI.ServiceContracts
{
	// Token: 0x0200000D RID: 13
	public static class TryGetResult
	{
		// Token: 0x0600003D RID: 61 RVA: 0x00002C80 File Offset: 0x00000E80
		public static TryGetResult<T> CreateFromStruct<T>(T result) where T : struct
		{
			return new TryGetResult<T>(result, true);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002C89 File Offset: 0x00000E89
		public static TryGetResult<T?> CreateNullableFromStruct<T>(T? result) where T : struct
		{
			return new TryGetResult<T?>(result, true);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002C92 File Offset: 0x00000E92
		public static TryGetResult<T?> CreateNullable<T>(T? result) where T : struct
		{
			return new TryGetResult<T?>(result, result != null);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002CA1 File Offset: 0x00000EA1
		public static TryGetResult<T> Create<T>(T result) where T : class
		{
			return new TryGetResult<T>(result, result != null);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002CB4 File Offset: 0x00000EB4
		public static TryGetResult<T> FailedStruct<T>() where T : struct
		{
			return new TryGetResult<T>(default(T), false);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002CD0 File Offset: 0x00000ED0
		public static TryGetResult<T?> FailedNullable<T>() where T : struct
		{
			return new TryGetResult<T?>(null, false);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002CEC File Offset: 0x00000EEC
		public static TryGetResult<T> Failed<T>() where T : class
		{
			return new TryGetResult<T>(default(T), false);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002D08 File Offset: 0x00000F08
		public static TryGetResult<T> FailedStruct<T>(Exception e) where T : struct
		{
			return new TryGetResult<T>(default(T), false, e);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002D28 File Offset: 0x00000F28
		public static TryGetResult<T?> FailedNullable<T>(Exception e) where T : struct
		{
			return new TryGetResult<T?>(null, false, e);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002D48 File Offset: 0x00000F48
		public static TryGetResult<T> Failed<T>(Exception e) where T : class
		{
			return new TryGetResult<T>(default(T), false, e);
		}
	}
}
