using System;
using System.Runtime.Serialization;

namespace <CrtImplementationDetails>
{
	// Token: 0x0200000A RID: 10
	[Serializable]
	internal class ModuleLoadException : Exception
	{
		// Token: 0x06000063 RID: 99 RVA: 0x000019A0 File Offset: 0x000019A0
		protected ModuleLoadException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00001984 File Offset: 0x00001984
		public ModuleLoadException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00001968 File Offset: 0x00001968
		public ModuleLoadException(string message)
			: base(message)
		{
		}

		// Token: 0x04000042 RID: 66
		public const string Nested = "A nested exception occurred after the primary exception that caused the C++ module to fail to load.\n";
	}
}
