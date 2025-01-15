using System;
using System.Runtime.CompilerServices;

// Token: 0x02000669 RID: 1641
[CompilerGenerated]
internal sealed class <868055c8-a2e5-4039-b5eb-07da6295884c><PrivateImplementationDetails>
{
	// Token: 0x060036DD RID: 14045 RVA: 0x000B91E4 File Offset: 0x000B73E4
	internal static uint ComputeStringHash(string s)
	{
		uint num;
		if (s != null)
		{
			num = 2166136261U;
			for (int i = 0; i < s.Length; i++)
			{
				num = ((uint)s[i] ^ num) * 16777619U;
			}
		}
		return num;
	}
}
