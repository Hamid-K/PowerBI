using System;
using System.Collections.Generic;

namespace System.Data.Entity.Core.Common.Utils
{
	// Token: 0x020005FE RID: 1534
	internal class TrailingSpaceComparer : IEqualityComparer<object>
	{
		// Token: 0x06004B19 RID: 19225 RVA: 0x00109CE2 File Offset: 0x00107EE2
		private TrailingSpaceComparer()
		{
		}

		// Token: 0x06004B1A RID: 19226 RVA: 0x00109CEC File Offset: 0x00107EEC
		bool IEqualityComparer<object>.Equals(object x, object y)
		{
			string text = x as string;
			if (text != null)
			{
				string text2 = y as string;
				if (text2 != null)
				{
					return TrailingSpaceStringComparer.Instance.Equals(text, text2);
				}
			}
			return TrailingSpaceComparer._template.Equals(x, y);
		}

		// Token: 0x06004B1B RID: 19227 RVA: 0x00109D28 File Offset: 0x00107F28
		int IEqualityComparer<object>.GetHashCode(object obj)
		{
			string text = obj as string;
			if (text != null)
			{
				return TrailingSpaceStringComparer.Instance.GetHashCode(text);
			}
			return TrailingSpaceComparer._template.GetHashCode(obj);
		}

		// Token: 0x04001A47 RID: 6727
		internal static readonly TrailingSpaceComparer Instance = new TrailingSpaceComparer();

		// Token: 0x04001A48 RID: 6728
		private static readonly IEqualityComparer<object> _template = EqualityComparer<object>.Default;
	}
}
