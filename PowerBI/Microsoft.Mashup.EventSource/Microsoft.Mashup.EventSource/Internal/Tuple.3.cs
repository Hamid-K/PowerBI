using System;
using System.Text;

namespace Microsoft.Internal
{
	// Token: 0x0200000A RID: 10
	[Serializable]
	public class Tuple<T1, T2>
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002250 File Offset: 0x00000450
		public T1 Item1
		{
			get
			{
				return this.m_Item1;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002258 File Offset: 0x00000458
		public T2 Item2
		{
			get
			{
				return this.m_Item2;
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002260 File Offset: 0x00000460
		public Tuple(T1 item1, T2 item2)
		{
			this.m_Item1 = item1;
			this.m_Item2 = item2;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002278 File Offset: 0x00000478
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("(");
			stringBuilder.Append(this.m_Item1);
			stringBuilder.Append(", ");
			stringBuilder.Append(this.m_Item2);
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000022D7 File Offset: 0x000004D7
		private int Size
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x0400000C RID: 12
		private readonly T1 m_Item1;

		// Token: 0x0400000D RID: 13
		private readonly T2 m_Item2;
	}
}
