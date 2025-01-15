using System;
using System.IO;
using Microsoft.ProgramSynthesis.Wrangling.Session;

namespace Microsoft.ProgramSynthesis.Detection.FileType
{
	// Token: 0x02000AC0 RID: 2752
	public static class FileTypeInfoUtils
	{
		// Token: 0x0600451C RID: 17692 RVA: 0x000D8670 File Offset: 0x000D6870
		public static void AddInput(this ITextReaderInput session, FileTypeInfo info, int linesToLearn = 200)
		{
			using (TextReader textReader = info.CreateTextReader())
			{
				session.AddInput(textReader, linesToLearn);
			}
		}
	}
}
