using System;
using System.Runtime.Serialization;

namespace <CrtImplementationDetails>
{
	// Token: 0x020000AC RID: 172
	[Serializable]
	internal class ModuleLoadException : Exception
	{
		// Token: 0x060001F6 RID: 502 RVA: 0x00018B88 File Offset: 0x00018B88
		protected ModuleLoadException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00018B6C File Offset: 0x00018B6C
		public ModuleLoadException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00018B50 File Offset: 0x00018B50
		public ModuleLoadException(string message)
			: base(message)
		{
		}

		// Token: 0x0400023A RID: 570
		public const string Nested = "A nested exception occurred after the primary exception that caused the C++ module to fail to load.\n";
	}
}
