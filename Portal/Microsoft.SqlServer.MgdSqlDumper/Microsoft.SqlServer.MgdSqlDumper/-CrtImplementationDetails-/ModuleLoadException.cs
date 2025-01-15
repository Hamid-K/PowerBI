using System;
using System.Runtime.Serialization;

namespace <CrtImplementationDetails>
{
	// Token: 0x0200000D RID: 13
	[Serializable]
	internal class ModuleLoadException : Exception
	{
		// Token: 0x0600006F RID: 111 RVA: 0x00005768 File Offset: 0x00004B68
		protected ModuleLoadException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x06000070 RID: 112 RVA: 0x0000574C File Offset: 0x00004B4C
		public ModuleLoadException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00005730 File Offset: 0x00004B30
		public ModuleLoadException(string message)
			: base(message)
		{
		}

		// Token: 0x04000072 RID: 114
		public const string Nested = "A nested exception occurred after the primary exception that caused the C++ module to fail to load.\n";
	}
}
