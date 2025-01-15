using System;

namespace Microsoft.OleDb.Marshallers
{
	// Token: 0x02001FD1 RID: 8145
	public interface IMarshaller<T> : IMarshaller
	{
		// Token: 0x0600C706 RID: 50950
		T GetManaged(IntPtr native);

		// Token: 0x0600C707 RID: 50951
		void GetNative(T managed, IntPtr native);
	}
}
