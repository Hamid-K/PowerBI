using System;
using System.Data.Entity.Core.Mapping.ViewGeneration.Structures;
using System.Runtime.Serialization;

namespace System.Data.Entity.Core
{
	// Token: 0x020002D8 RID: 728
	[Serializable]
	internal class InternalMappingException : EntityException
	{
		// Token: 0x0600231E RID: 8990 RVA: 0x0006357D File Offset: 0x0006177D
		internal InternalMappingException()
		{
		}

		// Token: 0x0600231F RID: 8991 RVA: 0x00063585 File Offset: 0x00061785
		internal InternalMappingException(string message)
			: base(message)
		{
		}

		// Token: 0x06002320 RID: 8992 RVA: 0x0006358E File Offset: 0x0006178E
		internal InternalMappingException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06002321 RID: 8993 RVA: 0x00063598 File Offset: 0x00061798
		protected InternalMappingException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x06002322 RID: 8994 RVA: 0x000635A2 File Offset: 0x000617A2
		internal InternalMappingException(string message, ErrorLog errorLog)
			: base(message)
		{
			this.m_errorLog = errorLog;
		}

		// Token: 0x06002323 RID: 8995 RVA: 0x000635B2 File Offset: 0x000617B2
		internal InternalMappingException(string message, ErrorLog.Record record)
			: base(message)
		{
			this.m_errorLog = new ErrorLog();
			this.m_errorLog.AddEntry(record);
		}

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x06002324 RID: 8996 RVA: 0x000635D2 File Offset: 0x000617D2
		internal ErrorLog ErrorLog
		{
			get
			{
				return this.m_errorLog;
			}
		}

		// Token: 0x04000C0B RID: 3083
		private readonly ErrorLog m_errorLog;
	}
}
