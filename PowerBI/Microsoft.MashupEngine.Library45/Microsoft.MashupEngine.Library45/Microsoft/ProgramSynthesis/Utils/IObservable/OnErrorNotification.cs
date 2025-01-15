using System;

namespace Microsoft.ProgramSynthesis.Utils.IObservable
{
	// Token: 0x0200064F RID: 1615
	public sealed class OnErrorNotification<T> : Notification<T>
	{
		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x0600230F RID: 8975 RVA: 0x00062D40 File Offset: 0x00060F40
		public Exception Exception { get; }

		// Token: 0x06002310 RID: 8976 RVA: 0x00062D48 File Offset: 0x00060F48
		public OnErrorNotification(Exception exception)
		{
			this.Exception = exception;
		}

		// Token: 0x06002311 RID: 8977 RVA: 0x00062D57 File Offset: 0x00060F57
		public override void ApplyTo(IObserver<T> observer)
		{
			observer.OnError(this.Exception);
		}

		// Token: 0x06002312 RID: 8978 RVA: 0x00062D68 File Offset: 0x00060F68
		public override bool Equals(object obj)
		{
			OnErrorNotification<T> onErrorNotification = obj as OnErrorNotification<T>;
			return onErrorNotification != null && this.Exception == onErrorNotification.Exception;
		}

		// Token: 0x06002313 RID: 8979 RVA: 0x00062D8F File Offset: 0x00060F8F
		public override int GetHashCode()
		{
			return this.Exception.GetHashCode();
		}

		// Token: 0x06002314 RID: 8980 RVA: 0x00062D9C File Offset: 0x00060F9C
		public override string ToString()
		{
			return string.Format("OnError({0})", this.Exception);
		}
	}
}
