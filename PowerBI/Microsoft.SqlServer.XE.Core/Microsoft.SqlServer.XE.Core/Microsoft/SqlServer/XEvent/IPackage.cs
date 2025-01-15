using System;
using System.Collections.ObjectModel;

namespace Microsoft.SqlServer.XEvent
{
	// Token: 0x0200001C RID: 28
	public interface IPackage
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600007E RID: 126
		string Name { get; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600007F RID: 127
		string Description { get; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000080 RID: 128
		Guid PackageId { get; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000081 RID: 129
		Guid ModuleId { get; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000082 RID: 130
		IMetadataGeneration Generation { get; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000083 RID: 131
		ReadOnlyCollection<IEventMetadata> Events { get; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000084 RID: 132
		ReadOnlyCollection<IActionMetadata> Actions { get; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000085 RID: 133
		ReadOnlyCollection<IMapMetadata> Maps { get; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000086 RID: 134
		ReadOnlyCollection<ITargetMetadata> Targets { get; }
	}
}
