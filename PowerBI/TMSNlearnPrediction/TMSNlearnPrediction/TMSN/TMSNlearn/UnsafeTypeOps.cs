using System;
using System.IO;

namespace Microsoft.TMSN.TMSNlearn
{
	// Token: 0x02000460 RID: 1120
	internal abstract class UnsafeTypeOps<V>
	{
		// Token: 0x1700023A RID: 570
		// (get) Token: 0x0600174F RID: 5967
		public abstract int Size { get; }

		// Token: 0x06001750 RID: 5968
		public abstract void Apply(V[] array, Action<IntPtr> func);

		// Token: 0x06001751 RID: 5969
		public abstract void Write(V a, BinaryWriter writer);

		// Token: 0x06001752 RID: 5970
		public abstract V Read(BinaryReader reader);
	}
}
