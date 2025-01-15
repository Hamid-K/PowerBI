using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// Token: 0x020005D9 RID: 1497
[CompilerGenerated]
internal sealed class <fa2731a4-b8df-42ec-a59c-4417eb13e59d><PrivateImplementationDetails>
{
	// Token: 0x0600340E RID: 13326 RVA: 0x000ADD78 File Offset: 0x000ABF78
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

	// Token: 0x04001CBF RID: 7359 RVA: 0x001C228B File Offset: 0x001C048B
	// Note: this field is marked with 'hasfieldrva'.
	internal static readonly <fa2731a4-b8df-42ec-a59c-4417eb13e59d><PrivateImplementationDetails>.__StaticArrayInitTypeSize=60 CFF3CA2A16070983D70D7C831CBD41FC032E2892;

	// Token: 0x020005DA RID: 1498
	[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 60)]
	private struct __StaticArrayInitTypeSize=60
	{
	}
}
