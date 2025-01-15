using System;
using System.Globalization;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000218 RID: 536
	public class Pair<TFirst, TSecond>
	{
		// Token: 0x06000E1F RID: 3615 RVA: 0x000320ED File Offset: 0x000302ED
		public Pair(TFirst first, TSecond second)
		{
			this.m_first = first;
			this.m_second = second;
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000E20 RID: 3616 RVA: 0x00032103 File Offset: 0x00030303
		// (set) Token: 0x06000E21 RID: 3617 RVA: 0x0003210B File Offset: 0x0003030B
		public TFirst First
		{
			get
			{
				return this.m_first;
			}
			set
			{
				this.m_first = value;
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000E22 RID: 3618 RVA: 0x00032114 File Offset: 0x00030314
		// (set) Token: 0x06000E23 RID: 3619 RVA: 0x0003211C File Offset: 0x0003031C
		public TSecond Second
		{
			get
			{
				return this.m_second;
			}
			set
			{
				this.m_second = value;
			}
		}

		// Token: 0x06000E24 RID: 3620 RVA: 0x00032125 File Offset: 0x00030325
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "<{0}, {1}>", new object[] { this.m_first, this.m_second });
		}

		// Token: 0x0400058A RID: 1418
		private TFirst m_first;

		// Token: 0x0400058B RID: 1419
		private TSecond m_second;
	}
}
