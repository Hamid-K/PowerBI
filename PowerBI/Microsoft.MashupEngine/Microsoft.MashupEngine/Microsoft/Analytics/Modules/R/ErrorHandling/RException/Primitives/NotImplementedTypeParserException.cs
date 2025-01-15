using System;

namespace Microsoft.Analytics.Modules.R.ErrorHandling.RException.Primitives
{
	// Token: 0x02000165 RID: 357
	[Serializable]
	internal class NotImplementedTypeParserException : Exception
	{
		// Token: 0x060006C7 RID: 1735 RVA: 0x00005F33 File Offset: 0x00004133
		public NotImplementedTypeParserException()
		{
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x00002FDF File Offset: 0x000011DF
		public NotImplementedTypeParserException(string message)
			: base(message)
		{
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x0000B0C7 File Offset: 0x000092C7
		public NotImplementedTypeParserException(Type t)
			: base(t.ToString())
		{
		}
	}
}
