using System;
using Microsoft.MachineLearning.Data;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Numeric
{
	// Token: 0x0200045C RID: 1116
	internal static class TerminateTester
	{
		// Token: 0x06001731 RID: 5937 RVA: 0x00086628 File Offset: 0x00084828
		internal static bool ShouldTerminate(ref VBuffer<float> x, ref VBuffer<float> xprev)
		{
			if (!FloatUtils.IsFinite(x.Values, x.Count))
			{
				return true;
			}
			if (x.IsDense && xprev.IsDense)
			{
				for (int i = 0; i < x.Length; i++)
				{
					if (x.Values[i] != xprev.Values[i])
					{
						return false;
					}
				}
			}
			else if (xprev.IsDense)
			{
				int j = 0;
				for (int k = 0; k < x.Count; k++)
				{
					int num = x.Indices[k];
					while (j < num)
					{
						if (xprev.Values[j++] != 0f)
						{
							return false;
						}
					}
					if (x.Values[k] != xprev.Values[j++])
					{
						return false;
					}
				}
				while (j < xprev.Length)
				{
					if (xprev.Values[j++] != 0f)
					{
						return false;
					}
				}
			}
			else if (x.IsDense)
			{
				int l = 0;
				for (int m = 0; m < xprev.Count; m++)
				{
					int num2 = xprev.Indices[m];
					while (l < num2)
					{
						if (x.Values[l++] != 0f)
						{
							return false;
						}
					}
					if (x.Values[l++] != xprev.Values[m])
					{
						return false;
					}
				}
				while (l < x.Length)
				{
					if (x.Values[l++] != 0f)
					{
						return false;
					}
				}
			}
			else
			{
				int n = 0;
				int num3 = 0;
				while (n < x.Count)
				{
					if (num3 >= xprev.Count)
					{
						break;
					}
					int num4 = x.Indices[n];
					int num5 = xprev.Indices[num3];
					if (num4 == num5)
					{
						if (x.Values[n++] != xprev.Values[num3++])
						{
							return false;
						}
					}
					else if (num4 < num5)
					{
						if (x.Values[n++] != 0f)
						{
							return false;
						}
					}
					else if (xprev.Values[num3++] != 0f)
					{
						return false;
					}
				}
				while (n < x.Count)
				{
					if (x.Values[n++] != 0f)
					{
						return false;
					}
				}
				while (num3 < xprev.Count)
				{
					if (xprev.Values[num3++] != 0f)
					{
						return false;
					}
				}
			}
			return true;
		}
	}
}
