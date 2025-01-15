using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Data.Entity.Core.Common.Utils
{
	// Token: 0x020005F1 RID: 1521
	internal class ByValueComparer : IComparer
	{
		// Token: 0x06004A75 RID: 19061 RVA: 0x00107FA3 File Offset: 0x001061A3
		private ByValueComparer(IComparer comparer)
		{
			this.nonByValueComparer = comparer;
		}

		// Token: 0x06004A76 RID: 19062 RVA: 0x00107FB4 File Offset: 0x001061B4
		int IComparer.Compare(object x, object y)
		{
			if (x == y)
			{
				return 0;
			}
			if (x == DBNull.Value)
			{
				x = null;
			}
			if (y == DBNull.Value)
			{
				y = null;
			}
			if (x != null && y != null)
			{
				byte[] array = x as byte[];
				byte[] array2 = y as byte[];
				if (array != null && array2 != null)
				{
					int num = array.Length - array2.Length;
					if (num == 0)
					{
						int num2 = 0;
						while (num == 0 && num2 < array.Length)
						{
							byte b = array[num2];
							byte b2 = array2[num2];
							if (b != b2)
							{
								num = (int)(b - b2);
							}
							num2++;
						}
					}
					return num;
				}
			}
			return this.nonByValueComparer.Compare(x, y);
		}

		// Token: 0x04001A37 RID: 6711
		internal static readonly IComparer Default = new ByValueComparer(Comparer<object>.Default);

		// Token: 0x04001A38 RID: 6712
		private readonly IComparer nonByValueComparer;
	}
}
