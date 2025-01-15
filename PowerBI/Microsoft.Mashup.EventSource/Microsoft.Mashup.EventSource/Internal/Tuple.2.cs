using System;
using System.Text;

namespace Microsoft.Internal
{
	// Token: 0x02000009 RID: 9
	[Serializable]
	internal class Tuple<T1>
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002200 File Offset: 0x00000400
		public T1 Item1
		{
			get
			{
				return this.m_Item1;
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002208 File Offset: 0x00000408
		public Tuple(T1 item1)
		{
			this.m_Item1 = item1;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002217 File Offset: 0x00000417
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("(");
			stringBuilder.Append(this.m_Item1);
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000017 RID: 23 RVA: 0x0000224D File Offset: 0x0000044D
		private int Size
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x0400000B RID: 11
		private readonly T1 m_Item1;
	}
}
