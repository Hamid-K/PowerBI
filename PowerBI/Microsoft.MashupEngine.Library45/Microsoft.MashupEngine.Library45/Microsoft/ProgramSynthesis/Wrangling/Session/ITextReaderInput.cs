using System;
using System.IO;

namespace Microsoft.ProgramSynthesis.Wrangling.Session
{
	// Token: 0x0200010E RID: 270
	public interface ITextReaderInput
	{
		// Token: 0x06000624 RID: 1572
		void AddInput(TextReader reader, int linesToLearn);
	}
}
