using System;

namespace Microsoft.Fabric.Common
{
	// Token: 0x02000403 RID: 1027
	internal class Literal : StandaloneFunc
	{
		// Token: 0x060023E8 RID: 9192 RVA: 0x0006E2B8 File Offset: 0x0006C4B8
		public Literal(object value)
		{
			this.m_value = value;
		}

		// Token: 0x060023E9 RID: 9193 RVA: 0x0006E2C7 File Offset: 0x0006C4C7
		public override object Invoke(object[] args)
		{
			return this.m_value;
		}

		// Token: 0x060023EA RID: 9194 RVA: 0x0006E2CF File Offset: 0x0006C4CF
		public override string ToString()
		{
			if (this.m_value == null)
			{
				return string.Empty;
			}
			return this.m_value.ToString();
		}

		// Token: 0x0400163B RID: 5691
		private object m_value;

		// Token: 0x0400163C RID: 5692
		public static readonly Literal Empty = new Literal(null);
	}
}
