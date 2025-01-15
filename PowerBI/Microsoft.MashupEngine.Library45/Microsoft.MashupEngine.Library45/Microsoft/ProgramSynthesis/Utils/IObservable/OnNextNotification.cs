using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Utils.IObservable
{
	// Token: 0x0200064E RID: 1614
	public sealed class OnNextNotification<T> : Notification<T>
	{
		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x06002309 RID: 8969 RVA: 0x00062CC0 File Offset: 0x00060EC0
		public T Value { get; }

		// Token: 0x0600230A RID: 8970 RVA: 0x00062CC8 File Offset: 0x00060EC8
		public OnNextNotification(T value)
		{
			this.Value = value;
		}

		// Token: 0x0600230B RID: 8971 RVA: 0x00062CD7 File Offset: 0x00060ED7
		public override void ApplyTo(IObserver<T> observer)
		{
			observer.OnNext(this.Value);
		}

		// Token: 0x0600230C RID: 8972 RVA: 0x00062CE8 File Offset: 0x00060EE8
		public override bool Equals(object obj)
		{
			OnNextNotification<T> onNextNotification = obj as OnNextNotification<T>;
			return onNextNotification != null && EqualityComparer<T>.Default.Equals(this.Value, onNextNotification.Value);
		}

		// Token: 0x0600230D RID: 8973 RVA: 0x00062D17 File Offset: 0x00060F17
		public override int GetHashCode()
		{
			return EqualityComparer<T>.Default.GetHashCode(this.Value);
		}

		// Token: 0x0600230E RID: 8974 RVA: 0x00062D29 File Offset: 0x00060F29
		public override string ToString()
		{
			return string.Format("OnNext({0})", this.Value);
		}
	}
}
