using System;
using System.Reflection;

namespace Microsoft.AspNet.OData.Query.Expressions
{
	// Token: 0x020000F8 RID: 248
	internal static class Linq2ObjectsComparisonMethods
	{
		// Token: 0x06000864 RID: 2148 RVA: 0x000209F0 File Offset: 0x0001EBF0
		public static bool AreByteArraysEqual(byte[] left, byte[] right)
		{
			if (left == right)
			{
				return true;
			}
			if (left == null || right == null)
			{
				return false;
			}
			if (left.Length != right.Length)
			{
				return false;
			}
			for (int i = 0; i < left.Length; i++)
			{
				if (left[i] != right[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x00020A2E File Offset: 0x0001EC2E
		public static bool AreByteArraysNotEqual(byte[] left, byte[] right)
		{
			return !Linq2ObjectsComparisonMethods.AreByteArraysEqual(left, right);
		}

		// Token: 0x0400027C RID: 636
		public static readonly MethodInfo AreByteArraysEqualMethodInfo = typeof(Linq2ObjectsComparisonMethods).GetMethod("AreByteArraysEqual");

		// Token: 0x0400027D RID: 637
		public static readonly MethodInfo AreByteArraysNotEqualMethodInfo = typeof(Linq2ObjectsComparisonMethods).GetMethod("AreByteArraysNotEqual");
	}
}
