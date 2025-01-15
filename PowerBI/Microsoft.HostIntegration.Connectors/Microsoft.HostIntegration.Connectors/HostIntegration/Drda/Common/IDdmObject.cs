using System;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x02000815 RID: 2069
	public interface IDdmObject
	{
		// Token: 0x0600419D RID: 16797
		void Read(DdmReader reader);

		// Token: 0x0600419E RID: 16798
		void Write(DdmWriter writer);
	}
}
