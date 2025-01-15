using System;

namespace Microsoft.OleDb.Marshallers
{
	// Token: 0x02001FD0 RID: 8144
	public interface IMarshaller
	{
		// Token: 0x17003037 RID: 12343
		// (get) Token: 0x0600C703 RID: 50947
		int NativeSizeInBytes { get; }

		// Token: 0x17003038 RID: 12344
		// (get) Token: 0x0600C704 RID: 50948
		VARTYPE Type { get; }

		// Token: 0x0600C705 RID: 50949
		void Cleanup(IntPtr native);
	}
}
