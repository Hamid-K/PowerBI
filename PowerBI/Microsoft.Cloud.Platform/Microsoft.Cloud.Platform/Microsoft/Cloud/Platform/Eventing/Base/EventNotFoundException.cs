using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Cloud.Platform.Eventing.Base
{
	// Token: 0x020003BD RID: 957
	[Serializable]
	public class EventNotFoundException : ItemNotFoundException
	{
		// Token: 0x06001D99 RID: 7577 RVA: 0x0007049F File Offset: 0x0006E69F
		public EventNotFoundException()
		{
		}

		// Token: 0x06001D9A RID: 7578 RVA: 0x000704A7 File Offset: 0x0006E6A7
		public EventNotFoundException(string message)
			: base(message)
		{
		}

		// Token: 0x06001D9B RID: 7579 RVA: 0x000704B0 File Offset: 0x0006E6B0
		public EventNotFoundException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06001D9C RID: 7580 RVA: 0x000704BA File Offset: 0x0006E6BA
		protected EventNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x06001D9D RID: 7581 RVA: 0x000704C4 File Offset: 0x0006E6C4
		public EventNotFoundException(EventIdentifier eid)
		{
			this.m_eid = eid;
		}

		// Token: 0x06001D9E RID: 7582 RVA: 0x000704D3 File Offset: 0x0006E6D3
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			if (info != null)
			{
				info.AddValue("event_id", this.m_eid, typeof(EventIdentifier));
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06001D9F RID: 7583 RVA: 0x000704FB File Offset: 0x0006E6FB
		public override string Message
		{
			get
			{
				return string.Format(CultureInfo.CurrentCulture, "Event '{0}' not found.", new object[] { this.m_eid });
			}
		}

		// Token: 0x04000A12 RID: 2578
		private EventIdentifier m_eid;
	}
}
