using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices
{
	// Token: 0x02000096 RID: 150
	[Guid("663B8189-4FAF-48ef-A447-394B615D45F0")]
	public interface INamedComponentCollection : IModelComponentCollection, ICollection, IEnumerable
	{
		// Token: 0x0600073A RID: 1850
		string GetNewName();

		// Token: 0x0600073B RID: 1851
		string GetNewName(string prefix);

		// Token: 0x0600073C RID: 1852
		string GetNewID();

		// Token: 0x0600073D RID: 1853
		string GetNewID(string prefix);

		// Token: 0x0600073E RID: 1854
		bool ContainsName(string name);

		// Token: 0x0600073F RID: 1855
		bool Contains(string id);
	}
}
