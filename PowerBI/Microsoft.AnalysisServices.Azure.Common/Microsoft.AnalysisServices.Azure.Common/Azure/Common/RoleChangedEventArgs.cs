using System;
using System.Fabric;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x02000086 RID: 134
	public sealed class RoleChangedEventArgs : EventArgs
	{
		// Token: 0x060004FC RID: 1276 RVA: 0x00010B67 File Offset: 0x0000ED67
		public RoleChangedEventArgs(ReplicaRole currentRole, ReplicaRole newRole)
		{
			this.CurrentRole = currentRole;
			this.NewRole = newRole;
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x00010B7D File Offset: 0x0000ED7D
		// (set) Token: 0x060004FE RID: 1278 RVA: 0x00010B85 File Offset: 0x0000ED85
		public ReplicaRole CurrentRole { get; private set; }

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060004FF RID: 1279 RVA: 0x00010B8E File Offset: 0x0000ED8E
		// (set) Token: 0x06000500 RID: 1280 RVA: 0x00010B96 File Offset: 0x0000ED96
		public ReplicaRole NewRole { get; private set; }
	}
}
