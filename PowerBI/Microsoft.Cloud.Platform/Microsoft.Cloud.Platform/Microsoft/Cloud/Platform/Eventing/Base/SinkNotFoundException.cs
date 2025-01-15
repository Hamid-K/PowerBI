using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Eventing.Base
{
	// Token: 0x020003BE RID: 958
	[Serializable]
	public class SinkNotFoundException : MonitoredException
	{
		// Token: 0x06001DA0 RID: 7584 RVA: 0x0000EB75 File Offset: 0x0000CD75
		public SinkNotFoundException()
		{
		}

		// Token: 0x06001DA1 RID: 7585 RVA: 0x0000EB7D File Offset: 0x0000CD7D
		public SinkNotFoundException(string message)
			: base(message)
		{
		}

		// Token: 0x06001DA2 RID: 7586 RVA: 0x0000EB86 File Offset: 0x0000CD86
		public SinkNotFoundException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06001DA3 RID: 7587 RVA: 0x0000EB9E File Offset: 0x0000CD9E
		protected SinkNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x06001DA4 RID: 7588 RVA: 0x0007051B File Offset: 0x0006E71B
		public SinkNotFoundException(SinkIdentifier sid)
		{
			this.m_sid = sid;
		}

		// Token: 0x06001DA5 RID: 7589 RVA: 0x0007052A File Offset: 0x0006E72A
		public SinkNotFoundException(SinkIdentifier sid, Exception inner)
			: base(string.Empty, inner)
		{
			this.m_sid = sid;
		}

		// Token: 0x06001DA6 RID: 7590 RVA: 0x0007053F File Offset: 0x0006E73F
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			if (info != null)
			{
				info.AddValue("sink_id", this.m_sid, typeof(SinkIdentifier));
			}
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06001DA7 RID: 7591 RVA: 0x00070567 File Offset: 0x0006E767
		public override string Message
		{
			get
			{
				return string.Format(CultureInfo.CurrentCulture, "Sink '{0}' not found.", new object[] { this.m_sid });
			}
		}

		// Token: 0x04000A13 RID: 2579
		private SinkIdentifier m_sid;
	}
}
