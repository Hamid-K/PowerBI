using System;
using System.Runtime.CompilerServices;

// Token: 0x02000509 RID: 1289
[CompilerGenerated]
internal sealed class <f611a610-6473-4894-8d18-e7e7c7ed19bb><PrivateImplementationDetails>
{
	// Token: 0x06002B90 RID: 11152 RVA: 0x00096164 File Offset: 0x00094364
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
