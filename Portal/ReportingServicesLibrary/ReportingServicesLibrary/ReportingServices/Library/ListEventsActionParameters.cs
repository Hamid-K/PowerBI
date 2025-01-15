using System;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000105 RID: 261
	internal sealed class ListEventsActionParameters : RSSoapActionParameters
	{
		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000A77 RID: 2679 RVA: 0x00027BD7 File Offset: 0x00025DD7
		// (set) Token: 0x06000A78 RID: 2680 RVA: 0x00027BDF File Offset: 0x00025DDF
		public Event[] Events
		{
			get
			{
				return this.m_events;
			}
			set
			{
				this.m_events = value;
			}
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x00005BF2 File Offset: 0x00003DF2
		internal override void Validate()
		{
		}

		// Token: 0x04000491 RID: 1169
		private Event[] m_events;
	}
}
