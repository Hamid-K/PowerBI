using System;

namespace Microsoft.Cloud.Platform.Common
{
	// Token: 0x0200052C RID: 1324
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class EventsKitImplementationAttribute : Attribute
	{
		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x060028B5 RID: 10421 RVA: 0x00092554 File Offset: 0x00090754
		public Type EventsKit
		{
			get
			{
				return this.m_eventsKit;
			}
		}

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x060028B6 RID: 10422 RVA: 0x0009255C File Offset: 0x0009075C
		// (set) Token: 0x060028B7 RID: 10423 RVA: 0x00092564 File Offset: 0x00090764
		public EventsKitType EventsKitType { get; set; }

		// Token: 0x060028B8 RID: 10424 RVA: 0x0009256D File Offset: 0x0009076D
		public EventsKitImplementationAttribute(Type eventsKit)
		{
			this.m_eventsKit = eventsKit;
			this.EventsKitType = EventsKitType.Production;
		}

		// Token: 0x04000E86 RID: 3718
		private readonly Type m_eventsKit;
	}
}
