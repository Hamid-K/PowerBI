using System;

namespace System.Threading.Tasks.Dataflow.Internal
{
	// Token: 0x02000034 RID: 52
	internal abstract class JoinBlockTargetBase
	{
		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060001D7 RID: 471
		internal abstract bool IsDecliningPermanently { get; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001D8 RID: 472
		internal abstract bool HasAtLeastOneMessageAvailable { get; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001D9 RID: 473
		internal abstract bool HasAtLeastOnePostponedMessage { get; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060001DA RID: 474
		internal abstract int NumberOfMessagesAvailableOrPostponed { get; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060001DB RID: 475
		internal abstract bool HasTheHighestNumberOfMessagesAvailable { get; }

		// Token: 0x060001DC RID: 476
		internal abstract bool ReserveOneMessage();

		// Token: 0x060001DD RID: 477
		internal abstract bool ConsumeReservedMessage();

		// Token: 0x060001DE RID: 478
		internal abstract bool ConsumeOnePostponedMessage();

		// Token: 0x060001DF RID: 479
		internal abstract void ReleaseReservedMessage();

		// Token: 0x060001E0 RID: 480
		internal abstract void ClearReservation();

		// Token: 0x060001E1 RID: 481 RVA: 0x000081CC File Offset: 0x000063CC
		public void Complete()
		{
			this.CompleteCore(null, false, false);
		}

		// Token: 0x060001E2 RID: 482
		internal abstract void CompleteCore(Exception exception, bool dropPendingMessages, bool releaseReservedMessages);

		// Token: 0x060001E3 RID: 483
		internal abstract void CompleteOncePossible();
	}
}
