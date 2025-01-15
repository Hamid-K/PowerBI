using System;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x020004A6 RID: 1190
	public static class TargetLanguages
	{
		// Token: 0x06001ABA RID: 6842 RVA: 0x0005063C File Offset: 0x0004E83C
		public static string Extension(string language)
		{
			string text;
			if (!(language == "CSharp"))
			{
				if (!(language == "Python"))
				{
					if (!(language == "TypeScript"))
					{
						throw new NotImplementedException("Unknown target languages: " + language);
					}
					text = "ts";
				}
				else
				{
					text = "py";
				}
			}
			else
			{
				text = "cs";
			}
			return text;
		}

		// Token: 0x04000D24 RID: 3364
		public const string CSharp = "CSharp";

		// Token: 0x04000D25 RID: 3365
		public const string Python = "Python";

		// Token: 0x04000D26 RID: 3366
		public const string TS = "TypeScript";
	}
}
