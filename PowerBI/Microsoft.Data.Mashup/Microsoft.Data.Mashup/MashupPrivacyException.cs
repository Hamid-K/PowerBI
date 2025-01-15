using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.Data.Mashup
{
	// Token: 0x0200003A RID: 58
	[Serializable]
	public class MashupPrivacyException : MashupSecurityException
	{
		// Token: 0x060002DB RID: 731 RVA: 0x0000B6BB File Offset: 0x000098BB
		public MashupPrivacyException(string message)
			: base(message)
		{
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000B6C4 File Offset: 0x000098C4
		public MashupPrivacyException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000B6CE File Offset: 0x000098CE
		protected MashupPrivacyException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060002DE RID: 734 RVA: 0x0000B6D8 File Offset: 0x000098D8
		public override string Reason
		{
			get
			{
				return "PrivacyError";
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060002DF RID: 735 RVA: 0x0000B6DF File Offset: 0x000098DF
		public override IEnumerable<DataSource> DataSources
		{
			get
			{
				return new DataSource[0];
			}
		}
	}
}
