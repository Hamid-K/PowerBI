using System;
using System.Runtime.CompilerServices;

// Token: 0x02000769 RID: 1897
[CompilerGenerated]
internal sealed class <bba5a8c5-e874-495c-b8ae-10208dde68db><PrivateImplementationDetails>
{
	// Token: 0x06003DA4 RID: 15780 RVA: 0x000CFA94 File Offset: 0x000CDC94
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
