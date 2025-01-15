using System;

namespace Microsoft.PowerBI.DataExtension.Contracts.Internal
{
	// Token: 0x02000019 RID: 25
	public interface IDataSourceInfo
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000064 RID: 100
		string Name { get; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000065 RID: 101
		string Extension { get; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000066 RID: 102
		string ConnectionString { get; }
	}
}
