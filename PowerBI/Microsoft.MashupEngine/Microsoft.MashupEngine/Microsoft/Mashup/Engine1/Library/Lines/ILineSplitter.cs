using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Lines
{
	// Token: 0x020009D0 RID: 2512
	internal interface ILineSplitter
	{
		// Token: 0x06004771 RID: 18289
		IFieldReader<IValueReference> GetFieldReader(ILineReader lineReader);
	}
}
