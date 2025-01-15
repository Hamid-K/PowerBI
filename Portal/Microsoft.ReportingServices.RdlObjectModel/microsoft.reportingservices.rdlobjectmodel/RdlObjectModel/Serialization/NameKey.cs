using System;

namespace Microsoft.ReportingServices.RdlObjectModel.Serialization
{
	// Token: 0x020002D7 RID: 727
	public class NameKey
	{
		// Token: 0x06001651 RID: 5713 RVA: 0x000335FD File Offset: 0x000317FD
		internal NameKey(string name, string ns)
		{
			this.name = name;
			this.ns = ns;
		}

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x06001652 RID: 5714 RVA: 0x00033613 File Offset: 0x00031813
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x06001653 RID: 5715 RVA: 0x0003361B File Offset: 0x0003181B
		public string Namespace
		{
			get
			{
				return this.ns;
			}
		}

		// Token: 0x06001654 RID: 5716 RVA: 0x00033624 File Offset: 0x00031824
		public override bool Equals(object other)
		{
			if (!(other is NameKey))
			{
				return false;
			}
			NameKey nameKey = (NameKey)other;
			return this.name == nameKey.name && this.ns == nameKey.ns;
		}

		// Token: 0x06001655 RID: 5717 RVA: 0x00033668 File Offset: 0x00031868
		public override int GetHashCode()
		{
			return ((this.ns == null) ? 0 : this.ns.GetHashCode()) ^ ((this.name == null) ? 0 : this.name.GetHashCode());
		}

		// Token: 0x06001656 RID: 5718 RVA: 0x00033697 File Offset: 0x00031897
		public static bool operator ==(NameKey a, NameKey b)
		{
			return a.name == b.name && a.ns == b.ns;
		}

		// Token: 0x06001657 RID: 5719 RVA: 0x000336BF File Offset: 0x000318BF
		public static bool operator !=(NameKey a, NameKey b)
		{
			return !(a == b);
		}

		// Token: 0x040006EB RID: 1771
		internal string ns;

		// Token: 0x040006EC RID: 1772
		internal string name;
	}
}
