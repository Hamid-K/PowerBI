using System;

namespace Microsoft.ReportingServices.Design.Serialization
{
	// Token: 0x02000399 RID: 921
	internal class NameKey
	{
		// Token: 0x06001E61 RID: 7777 RVA: 0x0007C8AB File Offset: 0x0007AAAB
		internal NameKey(string name, string ns)
		{
			this.name = name;
			this.ns = ns;
		}

		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x06001E62 RID: 7778 RVA: 0x0007C8C1 File Offset: 0x0007AAC1
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x06001E63 RID: 7779 RVA: 0x0007C8C9 File Offset: 0x0007AAC9
		public string Namespace
		{
			get
			{
				return this.ns;
			}
		}

		// Token: 0x06001E64 RID: 7780 RVA: 0x0007C8D4 File Offset: 0x0007AAD4
		public override bool Equals(object other)
		{
			if (!(other is NameKey))
			{
				return false;
			}
			NameKey nameKey = (NameKey)other;
			return this.name == nameKey.name && this.ns == nameKey.ns;
		}

		// Token: 0x06001E65 RID: 7781 RVA: 0x0007C918 File Offset: 0x0007AB18
		public override int GetHashCode()
		{
			return ((this.ns == null) ? 0 : this.ns.GetHashCode()) ^ ((this.name == null) ? 0 : this.name.GetHashCode());
		}

		// Token: 0x06001E66 RID: 7782 RVA: 0x0007C947 File Offset: 0x0007AB47
		public static bool operator ==(NameKey a, NameKey b)
		{
			return a.name == b.name && a.ns == b.ns;
		}

		// Token: 0x06001E67 RID: 7783 RVA: 0x0007C96F File Offset: 0x0007AB6F
		public static bool operator !=(NameKey a, NameKey b)
		{
			return !(a == b);
		}

		// Token: 0x04000CDB RID: 3291
		internal string ns;

		// Token: 0x04000CDC RID: 3292
		internal string name;
	}
}
