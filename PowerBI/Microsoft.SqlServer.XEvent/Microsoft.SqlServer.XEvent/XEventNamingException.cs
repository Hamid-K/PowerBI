using System;
using System.Runtime.Serialization;
using Microsoft.SqlServer.XEvent.Internal;

namespace Microsoft.SqlServer.XEvent
{
	// Token: 0x02000050 RID: 80
	[Serializable]
	public class XEventNamingException : Exception
	{
		// Token: 0x060001A0 RID: 416 RVA: 0x00003C74 File Offset: 0x00003C74
		protected XEventNamingException(SerializationInfo info, StreamingContext context)
		{
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00004F64 File Offset: 0x00004F64
		public XEventNamingException(string name, int len)
			: base(XEventNamingException.MakeMessage(name))
		{
			this.m_name = name;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00003C20 File Offset: 0x00003C20
		public XEventNamingException(string message, Exception inner)
			: base(message, inner)
		{
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00003C04 File Offset: 0x00003C04
		public XEventNamingException(string message)
			: base(message)
		{
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00003BE8 File Offset: 0x00003BE8
		public XEventNamingException()
		{
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x00003DDC File Offset: 0x00003DDC
		public virtual string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00004F8C File Offset: 0x00004F8C
		public override string ToString()
		{
			if (this.m_name != null)
			{
				return XEventNamingException.MakeMessage(this.m_name);
			}
			return this.Message;
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00003DF8 File Offset: 0x00003DF8
		protected static string MakeMessage(string name)
		{
			return string.Format(ResourcesMgr.GetString("NamingException"), name, 128U);
		}

		// Token: 0x0400014A RID: 330
		protected string m_name;
	}
}
