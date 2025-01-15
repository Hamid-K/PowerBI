using System;
using System.Collections;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections.DotNet4
{
	// Token: 0x020000AD RID: 173
	[Serializable]
	internal class StructuralComparer : IComparer
	{
		// Token: 0x06000762 RID: 1890 RVA: 0x00027FE4 File Offset: 0x000261E4
		public int Compare(object x, object y)
		{
			if (x == null)
			{
				if (y != null)
				{
					return -1;
				}
				return 0;
			}
			else
			{
				if (y == null)
				{
					return 1;
				}
				IStructuralComparable structuralComparable = x as IStructuralComparable;
				if (structuralComparable != null)
				{
					return structuralComparable.CompareTo(y, this);
				}
				return Comparer.Default.Compare(x, y);
			}
		}
	}
}
