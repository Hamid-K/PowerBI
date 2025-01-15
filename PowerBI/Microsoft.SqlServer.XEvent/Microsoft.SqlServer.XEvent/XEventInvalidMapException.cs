using System;
using System.Runtime.Serialization;
using Microsoft.SqlServer.XEvent.Internal;

namespace Microsoft.SqlServer.XEvent
{
	// Token: 0x0200004F RID: 79
	[Serializable]
	public class XEventInvalidMapException : Exception
	{
		// Token: 0x06000196 RID: 406 RVA: 0x00003C74 File Offset: 0x00003C74
		protected XEventInvalidMapException(SerializationInfo info, StreamingContext context)
		{
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00004EEC File Offset: 0x00004EEC
		public XEventInvalidMapException(Type enumType, string mapName, int value)
			: base(XEventInvalidMapException.MakeMessage(enumType, mapName, value))
		{
			this.m_enumType = enumType;
			this.m_mapName = mapName;
			this.m_value = value;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00003C20 File Offset: 0x00003C20
		public XEventInvalidMapException(string message, Exception inner)
			: base(message, inner)
		{
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00003C04 File Offset: 0x00003C04
		public XEventInvalidMapException(string message)
			: base(message)
		{
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00003BE8 File Offset: 0x00003BE8
		public XEventInvalidMapException()
		{
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600019B RID: 411 RVA: 0x00003D58 File Offset: 0x00003D58
		public virtual Type EnumType
		{
			get
			{
				return this.m_enumType;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600019C RID: 412 RVA: 0x00003D74 File Offset: 0x00003D74
		public virtual int DuplicatedValue
		{
			get
			{
				return this.m_value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600019D RID: 413 RVA: 0x00003D90 File Offset: 0x00003D90
		public virtual string MapName
		{
			get
			{
				return this.m_mapName;
			}
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00004F24 File Offset: 0x00004F24
		public override string ToString()
		{
			if (this.m_enumType != null)
			{
				return XEventInvalidMapException.MakeMessage(this.m_enumType, this.m_mapName, this.m_value);
			}
			return this.Message;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00003DAC File Offset: 0x00003DAC
		private static string MakeMessage(Type enumType, string mapName, int value)
		{
			return string.Format(ResourcesMgr.GetString("InvalidMapException"), enumType.Name, mapName, value);
		}

		// Token: 0x04000147 RID: 327
		private int m_value;

		// Token: 0x04000148 RID: 328
		private Type m_enumType;

		// Token: 0x04000149 RID: 329
		private string m_mapName;
	}
}
