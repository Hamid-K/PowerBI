using System;
using System.Runtime.Serialization;

namespace <CrtImplementationDetails>
{
	// Token: 0x0200002B RID: 43
	[Serializable]
	internal class ModuleLoadException : Exception
	{
		// Token: 0x060000DF RID: 223 RVA: 0x0000B460 File Offset: 0x0000A860
		protected ModuleLoadException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x0000B444 File Offset: 0x0000A844
		public ModuleLoadException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x0000B428 File Offset: 0x0000A828
		public ModuleLoadException(string message)
			: base(message)
		{
		}

		// Token: 0x04000060 RID: 96
		public const string Nested = "A nested exception occurred after the primary exception that caused the C++ module to fail to load.\n";
	}
}
