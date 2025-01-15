using System;
using System.Runtime.Serialization;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x0200005E RID: 94
	[DataContract]
	[KnownType(typeof(ServiceEntity))]
	[KnownType(typeof(DatabaseEntity))]
	[KnownType(typeof(VirtualServerEntity))]
	[KnownType(typeof(DatabaseBindingEntity))]
	[KnownType(typeof(DatabaseLastAccessTimeEntity))]
	[KnownType(typeof(BackupEntity))]
	public sealed class PersistableEntityFullInformation
	{
		// Token: 0x06000489 RID: 1161 RVA: 0x0000ED9F File Offset: 0x0000CF9F
		public PersistableEntityFullInformation()
		{
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x00010135 File Offset: 0x0000E335
		public PersistableEntityFullInformation(IPersistable inMemoryValue, IPersistable inStoreValue)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<IPersistable>(inMemoryValue, "inMemoryValue");
			ExtendedDiagnostics.EnsureArgumentNotNull<IPersistable>(inStoreValue, "inStoreValue");
			this.CachedValue = inMemoryValue;
			this.PersistentStoredValue = inStoreValue;
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600048B RID: 1163 RVA: 0x00010161 File Offset: 0x0000E361
		// (set) Token: 0x0600048C RID: 1164 RVA: 0x00010169 File Offset: 0x0000E369
		[DataMember]
		public IPersistable CachedValue { get; set; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600048D RID: 1165 RVA: 0x00010172 File Offset: 0x0000E372
		// (set) Token: 0x0600048E RID: 1166 RVA: 0x0001017A File Offset: 0x0000E37A
		[DataMember]
		public IPersistable PersistentStoredValue { get; set; }
	}
}
