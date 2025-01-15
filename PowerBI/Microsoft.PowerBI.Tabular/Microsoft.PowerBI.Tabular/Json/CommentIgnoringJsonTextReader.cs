using System;
using System.IO;

namespace Microsoft.AnalysisServices.Tabular.Json
{
	// Token: 0x020001B1 RID: 433
	internal class CommentIgnoringJsonTextReader : JsonTextReader
	{
		// Token: 0x06001A74 RID: 6772 RVA: 0x000AF5C5 File Offset: 0x000AD7C5
		public CommentIgnoringJsonTextReader(TextReader reader)
			: base(reader)
		{
		}

		// Token: 0x06001A75 RID: 6773 RVA: 0x000AF5D0 File Offset: 0x000AD7D0
		public override bool Read()
		{
			bool flag;
			do
			{
				flag = base.Read();
			}
			while (flag && base.TokenType == 5);
			return flag;
		}
	}
}
