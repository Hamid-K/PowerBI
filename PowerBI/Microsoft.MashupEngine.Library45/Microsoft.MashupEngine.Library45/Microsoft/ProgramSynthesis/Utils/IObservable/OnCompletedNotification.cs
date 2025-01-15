using System;

namespace Microsoft.ProgramSynthesis.Utils.IObservable
{
	// Token: 0x02000650 RID: 1616
	public sealed class OnCompletedNotification<T> : Notification<T>
	{
		// Token: 0x06002315 RID: 8981 RVA: 0x00062DAE File Offset: 0x00060FAE
		public override void ApplyTo(IObserver<T> observer)
		{
			observer.OnCompleted();
		}

		// Token: 0x06002316 RID: 8982 RVA: 0x00062DB6 File Offset: 0x00060FB6
		public override bool Equals(object obj)
		{
			return obj is OnCompletedNotification<T>;
		}

		// Token: 0x06002317 RID: 8983 RVA: 0x00062DC1 File Offset: 0x00060FC1
		public override int GetHashCode()
		{
			return typeof(T).GetHashCode() ^ 8510;
		}

		// Token: 0x06002318 RID: 8984 RVA: 0x00062DD8 File Offset: 0x00060FD8
		public override string ToString()
		{
			return "OnCompleted()";
		}
	}
}
