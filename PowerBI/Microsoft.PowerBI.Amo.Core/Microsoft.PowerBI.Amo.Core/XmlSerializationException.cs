using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.AnalysisServices
{
	// Token: 0x0200000C RID: 12
	[Guid("DB1F9035-ACC7-4f04-9DEF-4902A49BCD10")]
	[Serializable]
	public sealed class XmlSerializationException : AmoException
	{
		// Token: 0x06000049 RID: 73 RVA: 0x0000400F File Offset: 0x0000220F
		public XmlSerializationException()
		{
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00004017 File Offset: 0x00002217
		public XmlSerializationException(string message)
			: base(message)
		{
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00004020 File Offset: 0x00002220
		public XmlSerializationException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0000402A File Offset: 0x0000222A
		public XmlSerializationException(string message, Exception innerException, int lineNumber, int linePosition)
			: base(message, innerException)
		{
			this.lineNumber = lineNumber;
			this.linePosition = linePosition;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00004043 File Offset: 0x00002243
		private XmlSerializationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.lineNumber = info.GetInt32("LineNumber");
			this.linePosition = info.GetInt32("LinePosition");
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600004E RID: 78 RVA: 0x0000406F File Offset: 0x0000226F
		// (set) Token: 0x0600004F RID: 79 RVA: 0x00004077 File Offset: 0x00002277
		public int LineNumber
		{
			get
			{
				return this.lineNumber;
			}
			set
			{
				this.lineNumber = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00004080 File Offset: 0x00002280
		// (set) Token: 0x06000051 RID: 81 RVA: 0x00004088 File Offset: 0x00002288
		public int LinePosition
		{
			get
			{
				return this.linePosition;
			}
			set
			{
				this.linePosition = value;
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00004091 File Offset: 0x00002291
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("LineNumber", this.lineNumber);
			info.AddValue("LinePosition", this.linePosition);
			base.GetObjectData(info, context);
		}

		// Token: 0x04000054 RID: 84
		private int lineNumber;

		// Token: 0x04000055 RID: 85
		private int linePosition;
	}
}
