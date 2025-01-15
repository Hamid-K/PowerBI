using System;
using System.Runtime.Serialization;
using Microsoft.SqlServer.XEvent.Internal;

namespace Microsoft.SqlServer.XEvent
{
	// Token: 0x0200004E RID: 78
	[Serializable]
	public class XEventInvalidEventTypeException : Exception
	{
		// Token: 0x0600018D RID: 397 RVA: 0x00003C74 File Offset: 0x00003C74
		protected XEventInvalidEventTypeException(SerializationInfo info, StreamingContext context)
		{
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00003CCC File Offset: 0x00003CCC
		public XEventInvalidEventTypeException(Type eventClass, string missingMember)
		{
			this.m_eventClass = eventClass;
			this.m_missingMember = missingMember;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00003C20 File Offset: 0x00003C20
		public XEventInvalidEventTypeException(string message, Exception inner)
			: base(message, inner)
		{
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00003C04 File Offset: 0x00003C04
		public XEventInvalidEventTypeException(string message)
			: base(message)
		{
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00003BE8 File Offset: 0x00003BE8
		public XEventInvalidEventTypeException()
		{
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000192 RID: 402 RVA: 0x00003CF4 File Offset: 0x00003CF4
		public virtual Type EventClassType
		{
			get
			{
				return this.m_eventClass;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000193 RID: 403 RVA: 0x00003D10 File Offset: 0x00003D10
		public virtual string MissingMember
		{
			get
			{
				return this.m_missingMember;
			}
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00004EB0 File Offset: 0x00004EB0
		public override string ToString()
		{
			if (this.m_eventClass != null)
			{
				return XEventInvalidEventTypeException.MakeMessage(this.m_eventClass, this.m_missingMember);
			}
			return this.Message;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00003D2C File Offset: 0x00003D2C
		protected static string MakeMessage(Type eventClass, string missingMember)
		{
			return string.Format(ResourcesMgr.GetString("InvalidEventTypeException"), eventClass.Name, missingMember);
		}

		// Token: 0x04000145 RID: 325
		protected Type m_eventClass;

		// Token: 0x04000146 RID: 326
		protected string m_missingMember;
	}
}
