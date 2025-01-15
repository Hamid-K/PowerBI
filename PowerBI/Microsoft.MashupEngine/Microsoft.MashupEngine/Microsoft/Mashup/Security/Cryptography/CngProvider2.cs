using System;
using System.Security.Cryptography;

namespace Microsoft.Mashup.Security.Cryptography
{
	// Token: 0x02001FF3 RID: 8179
	public static class CngProvider2
	{
		// Token: 0x17003042 RID: 12354
		// (get) Token: 0x0600C75E RID: 51038 RVA: 0x0027AFF5 File Offset: 0x002791F5
		public static CngProvider MicrosoftPrimitiveAlgorithmProvider
		{
			get
			{
				if (CngProvider2.s_primitiveAlgorithmProvider == null)
				{
					CngProvider2.s_primitiveAlgorithmProvider = new CngProvider("Microsoft Primitive Provider");
				}
				return CngProvider2.s_primitiveAlgorithmProvider;
			}
		}

		// Token: 0x040065DA RID: 26074
		private static CngProvider s_primitiveAlgorithmProvider;
	}
}
