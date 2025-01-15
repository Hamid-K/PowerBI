using System;

namespace System.Data.Entity.Utilities
{
	// Token: 0x02000072 RID: 114
	internal static class BoolExtensions
	{
		// Token: 0x0600042B RID: 1067 RVA: 0x0000F7A8 File Offset: 0x0000D9A8
		internal static bool? Not(this bool? operand)
		{
			if (operand == null)
			{
				return null;
			}
			return new bool?(!operand.Value);
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0000F7D8 File Offset: 0x0000D9D8
		internal static bool? And(this bool? left, bool? right)
		{
			bool? flag;
			if (left != null && right != null)
			{
				flag = new bool?(left.Value && right.Value);
			}
			else if (left == null && right == null)
			{
				flag = null;
			}
			else if (left != null)
			{
				flag = (left.Value ? null : new bool?(false));
			}
			else
			{
				flag = (right.Value ? null : new bool?(false));
			}
			return flag;
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0000F874 File Offset: 0x0000DA74
		internal static bool? Or(this bool? left, bool? right)
		{
			bool? flag;
			if (left != null && right != null)
			{
				flag = new bool?(left.Value || right.Value);
			}
			else if (left == null && right == null)
			{
				flag = null;
			}
			else if (left != null)
			{
				flag = (left.Value ? new bool?(true) : null);
			}
			else
			{
				flag = (right.Value ? new bool?(true) : null);
			}
			return flag;
		}
	}
}
