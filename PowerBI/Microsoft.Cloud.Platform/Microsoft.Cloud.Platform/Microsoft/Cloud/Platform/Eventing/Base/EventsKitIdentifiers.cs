using System;
using System.Globalization;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Eventing.Base
{
	// Token: 0x020003B5 RID: 949
	public sealed class EventsKitIdentifiers
	{
		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06001D49 RID: 7497 RVA: 0x0006FC7E File Offset: 0x0006DE7E
		// (set) Token: 0x06001D4A RID: 7498 RVA: 0x0006FC86 File Offset: 0x0006DE86
		public Guid FullId { get; private set; }

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06001D4B RID: 7499 RVA: 0x0006FC8F File Offset: 0x0006DE8F
		// (set) Token: 0x06001D4C RID: 7500 RVA: 0x0006FC97 File Offset: 0x0006DE97
		public long EventsKitId { get; private set; }

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06001D4D RID: 7501 RVA: 0x0006FCA0 File Offset: 0x0006DEA0
		// (set) Token: 0x06001D4E RID: 7502 RVA: 0x0006FCA8 File Offset: 0x0006DEA8
		public long EventId { get; private set; }

		// Token: 0x06001D4F RID: 7503 RVA: 0x0006FCB4 File Offset: 0x0006DEB4
		public EventsKitIdentifiers(Guid fullId)
		{
			this.FullId = fullId;
			long num = 0L;
			long num2 = 0L;
			byte[] array = fullId.ToByteArray();
			for (int i = 0; i < array.Length / 2; i++)
			{
				num |= (long)((long)((ulong)array[i]) << 8 * i);
				num2 |= (long)((long)((ulong)array[i + 8]) << 8 * i);
			}
			this.EventsKitId = num;
			this.EventId = num2;
		}

		// Token: 0x06001D50 RID: 7504 RVA: 0x0006FD18 File Offset: 0x0006DF18
		public EventsKitIdentifiers(long eventsKitId)
			: this(eventsKitId, 0L)
		{
		}

		// Token: 0x06001D51 RID: 7505 RVA: 0x0006FD24 File Offset: 0x0006DF24
		public EventsKitIdentifiers(long eventsKitId, long eventId)
		{
			ExtendedDiagnostics.EnsureArgumentIsPositive(eventsKitId, "eventsKitId");
			this.EventsKitId = eventsKitId;
			this.EventId = eventId;
			byte[] array = new byte[16];
			for (int i = 0; i < 8; i++)
			{
				array[i] = (byte)(eventsKitId >> 8 * i);
				array[i + 8] = (byte)(eventId >> 8 * i);
			}
			this.FullId = new Guid(array);
		}

		// Token: 0x06001D52 RID: 7506 RVA: 0x0006FD8C File Offset: 0x0006DF8C
		public static long GenerateUniqueLong()
		{
			long num = -1L;
			while (num <= 0L)
			{
				byte[] array = Guid.NewGuid().ToByteArray();
				num = 0L;
				for (int i = 0; i < array.Length / 2; i++)
				{
					num |= Convert.ToInt64((int)(array[i] ^ array[i + 8])) << i * 8;
				}
			}
			return num;
		}

		// Token: 0x06001D53 RID: 7507 RVA: 0x0006FDDD File Offset: 0x0006DFDD
		public static Guid GetEventsKitId(Guid eventId)
		{
			return new EventsKitIdentifiers(new EventsKitIdentifiers(eventId).EventsKitId).FullId;
		}

		// Token: 0x06001D54 RID: 7508 RVA: 0x0006FDF4 File Offset: 0x0006DFF4
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "<EventsKitId: {0}, EventId: {1}>", new object[] { this.EventsKitId, this.EventId });
		}
	}
}
