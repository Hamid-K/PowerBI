using System;
using System.Runtime.CompilerServices;

// Token: 0x02000A80 RID: 2688
[CompilerGenerated]
internal sealed class <c98b5b6f-a76e-400d-a518-0c889a923ea0><PrivateImplementationDetails>
{
	// Token: 0x06005391 RID: 21393 RVA: 0x00155240 File Offset: 0x00153440
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
