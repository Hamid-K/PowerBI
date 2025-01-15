using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices
{
	// Token: 0x02000094 RID: 148
	[Guid("BFF9CB56-3205-452b-B02C-C9603FCDF2C3")]
	public interface IModelComponentCollection : ICollection, IEnumerable
	{
		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000730 RID: 1840
		IModelComponent Parent { get; }

		// Token: 0x06000731 RID: 1841
		bool Contains(IModelComponent obj);

		// Token: 0x06000732 RID: 1842
		void Remove(IModelComponent obj);

		// Token: 0x06000733 RID: 1843
		void Remove(IModelComponent obj, bool cleanUp);
	}
}
