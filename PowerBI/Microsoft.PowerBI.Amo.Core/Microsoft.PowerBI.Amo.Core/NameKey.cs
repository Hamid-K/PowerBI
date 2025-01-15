using System;

namespace Microsoft.AnalysisServices
{
	// Token: 0x0200000A RID: 10
	internal sealed class NameKey
	{
		// Token: 0x06000041 RID: 65 RVA: 0x00003F15 File Offset: 0x00002115
		internal NameKey(string name, string ns)
		{
			this.name = name;
			this.ns = ns;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00003F2C File Offset: 0x0000212C
		public override bool Equals(object other)
		{
			if (!(other is NameKey))
			{
				return false;
			}
			NameKey nameKey = (NameKey)other;
			return this.name == nameKey.name && this.ns == nameKey.ns;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003F70 File Offset: 0x00002170
		public override int GetHashCode()
		{
			return ((this.ns == null) ? 0 : this.ns.GetHashCode()) ^ ((this.name == null) ? 0 : this.name.GetHashCode());
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003F9F File Offset: 0x0000219F
		public static bool operator ==(NameKey a, NameKey b)
		{
			return a.name == b.name && a.ns == b.ns;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003FC7 File Offset: 0x000021C7
		public static bool operator !=(NameKey a, NameKey b)
		{
			return !(a == b);
		}

		// Token: 0x04000051 RID: 81
		internal string ns;

		// Token: 0x04000052 RID: 82
		internal string name;
	}
}
