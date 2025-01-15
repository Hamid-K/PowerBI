using System;
using System.Collections.Generic;

namespace System.Data.Entity.Core.Common.Utils
{
	// Token: 0x020005F2 RID: 1522
	internal sealed class ByValueEqualityComparer : IEqualityComparer<object>
	{
		// Token: 0x06004A78 RID: 19064 RVA: 0x00108049 File Offset: 0x00106249
		private ByValueEqualityComparer()
		{
		}

		// Token: 0x06004A79 RID: 19065 RVA: 0x00108054 File Offset: 0x00106254
		public bool Equals(object x, object y)
		{
			if (object.Equals(x, y))
			{
				return true;
			}
			byte[] array = x as byte[];
			byte[] array2 = y as byte[];
			return array != null && array2 != null && ByValueEqualityComparer.CompareBinaryValues(array, array2);
		}

		// Token: 0x06004A7A RID: 19066 RVA: 0x0010808C File Offset: 0x0010628C
		public int GetHashCode(object obj)
		{
			if (obj == null)
			{
				return 0;
			}
			byte[] array = obj as byte[];
			if (array != null)
			{
				return ByValueEqualityComparer.ComputeBinaryHashCode(array);
			}
			return obj.GetHashCode();
		}

		// Token: 0x06004A7B RID: 19067 RVA: 0x001080B8 File Offset: 0x001062B8
		internal static int ComputeBinaryHashCode(byte[] bytes)
		{
			int num = 0;
			int i = 0;
			int num2 = Math.Min(bytes.Length, 7);
			while (i < num2)
			{
				num = (num << 5) ^ (int)bytes[i];
				i++;
			}
			return num;
		}

		// Token: 0x06004A7C RID: 19068 RVA: 0x001080E8 File Offset: 0x001062E8
		internal static bool CompareBinaryValues(byte[] first, byte[] second)
		{
			if (first.Length != second.Length)
			{
				return false;
			}
			for (int i = 0; i < first.Length; i++)
			{
				if (first[i] != second[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04001A39 RID: 6713
		internal static readonly ByValueEqualityComparer Default = new ByValueEqualityComparer();
	}
}
