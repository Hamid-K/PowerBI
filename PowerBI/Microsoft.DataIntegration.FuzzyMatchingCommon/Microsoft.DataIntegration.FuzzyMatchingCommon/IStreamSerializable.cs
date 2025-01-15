using System;
using System.IO;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon
{
	// Token: 0x02000024 RID: 36
	public interface IStreamSerializable<T>
	{
		// Token: 0x060000BD RID: 189
		void Write(Stream s);

		// Token: 0x060000BE RID: 190
		T Read(Stream s);
	}
}
