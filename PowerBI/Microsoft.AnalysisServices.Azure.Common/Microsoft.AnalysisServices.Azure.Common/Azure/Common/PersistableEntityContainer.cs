using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x0200005D RID: 93
	[DataContract]
	[KnownType(typeof(ServiceEntity))]
	[KnownType(typeof(DatabaseEntity))]
	[KnownType(typeof(VirtualServerEntity))]
	[KnownType(typeof(DatabaseBindingEntity))]
	[KnownType(typeof(DatabaseLastAccessTimeEntity))]
	[KnownType(typeof(BackupEntity))]
	public sealed class PersistableEntityContainer
	{
		// Token: 0x06000485 RID: 1157 RVA: 0x0000ED9F File Offset: 0x0000CF9F
		public PersistableEntityContainer()
		{
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x0001010A File Offset: 0x0000E30A
		public PersistableEntityContainer(IEnumerable<IPersistable> entities)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<IEnumerable<IPersistable>>(entities, "entities");
			this.Entities = entities;
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000487 RID: 1159 RVA: 0x00010124 File Offset: 0x0000E324
		// (set) Token: 0x06000488 RID: 1160 RVA: 0x0001012C File Offset: 0x0000E32C
		[DataMember]
		public IEnumerable<IPersistable> Entities { get; set; }
	}
}
