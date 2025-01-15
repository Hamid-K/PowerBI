using System;

namespace Microsoft.InfoNav
{
	// Token: 0x02000008 RID: 8
	public sealed class ActivityResult<T>
	{
		// Token: 0x06000004 RID: 4 RVA: 0x0000206F File Offset: 0x0000026F
		private ActivityResult(ActivityResultStatus status, T value = default(T), Exception exception = null)
		{
			this.Status = status;
			this.Value = value;
			this.Exception = exception;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000005 RID: 5 RVA: 0x0000208C File Offset: 0x0000028C
		public ActivityResultStatus Status { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002094 File Offset: 0x00000294
		public T Value { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000007 RID: 7 RVA: 0x0000209C File Offset: 0x0000029C
		public Exception Exception { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000020A4 File Offset: 0x000002A4
		public bool IsSuccessful
		{
			get
			{
				return this.Status == ActivityResultStatus.Success;
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000020AF File Offset: 0x000002AF
		public static ActivityResult<T> FromValue(T value)
		{
			return new ActivityResult<T>(ActivityResultStatus.Success, value, null);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000020BC File Offset: 0x000002BC
		public static ActivityResult<T> FromException(Exception exception)
		{
			return new ActivityResult<T>(ActivityResultStatus.Exception, default(T), exception);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000020DC File Offset: 0x000002DC
		public override string ToString()
		{
			return this.Status.ToString();
		}
	}
}
