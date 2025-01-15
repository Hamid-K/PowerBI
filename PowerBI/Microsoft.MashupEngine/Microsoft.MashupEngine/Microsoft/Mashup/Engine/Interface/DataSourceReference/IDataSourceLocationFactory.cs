using System;

namespace Microsoft.Mashup.Engine.Interface.DataSourceReference
{
	// Token: 0x02000146 RID: 326
	public interface IDataSourceLocationFactory
	{
		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x060005AE RID: 1454
		string Protocol { get; }

		// Token: 0x060005AF RID: 1455
		IDataSourceLocation New();

		// Token: 0x060005B0 RID: 1456
		bool TryCreateFromResource(IResource resource, bool normalize, out IDataSourceLocation location);
	}
}
