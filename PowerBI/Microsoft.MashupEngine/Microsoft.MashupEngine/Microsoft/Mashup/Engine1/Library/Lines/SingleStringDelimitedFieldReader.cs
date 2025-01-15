using System;

namespace Microsoft.Mashup.Engine1.Library.Lines
{
	// Token: 0x020009C7 RID: 2503
	internal class SingleStringDelimitedFieldReader : AnyStringDelimitedFieldReader
	{
		// Token: 0x06004750 RID: 18256 RVA: 0x000EEED1 File Offset: 0x000ED0D1
		public SingleStringDelimitedFieldReader(ILineReader reader, bool quoting, string delimiter)
			: base(reader, quoting, new string[] { delimiter })
		{
		}
	}
}
