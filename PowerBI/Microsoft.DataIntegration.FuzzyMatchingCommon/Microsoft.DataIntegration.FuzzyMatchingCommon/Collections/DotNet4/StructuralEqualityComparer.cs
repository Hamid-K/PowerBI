using System;
using System.Collections;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections.DotNet4
{
	// Token: 0x020000AC RID: 172
	[Serializable]
	internal class StructuralEqualityComparer : IEqualityComparer
	{
		// Token: 0x0600075F RID: 1887 RVA: 0x00027F78 File Offset: 0x00026178
		public bool Equals(object x, object y)
		{
			if (x == null)
			{
				return y == null;
			}
			IStructuralEquatable structuralEquatable = x as IStructuralEquatable;
			if (structuralEquatable != null)
			{
				return structuralEquatable.Equals(y, this);
			}
			return y != null && x.Equals(y);
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x00027FB0 File Offset: 0x000261B0
		public int GetHashCode(object obj)
		{
			if (obj == null)
			{
				return 0;
			}
			IStructuralEquatable structuralEquatable = obj as IStructuralEquatable;
			if (structuralEquatable != null)
			{
				return structuralEquatable.GetHashCode(this);
			}
			return obj.GetHashCode();
		}
	}
}
