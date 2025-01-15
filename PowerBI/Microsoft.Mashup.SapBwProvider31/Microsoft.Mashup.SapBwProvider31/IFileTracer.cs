using System;
using System.Collections.Generic;
using SAP.Middleware.Connector;

namespace Microsoft.Mashup.SapBwProvider
{
	// Token: 0x02000034 RID: 52
	public interface IFileTracer
	{
		// Token: 0x060002B5 RID: 693
		string GetFilename(string extension, string traceHash, int step, string functionName, string status);

		// Token: 0x060002B6 RID: 694
		void TraceData(Func<byte[]> getData, string filename);

		// Token: 0x060002B7 RID: 695
		void TraceFunctionInvoke(IRfcFunction function, SapBwCommand command, bool? producedRfcException);

		// Token: 0x060002B8 RID: 696
		void TraceMdxInfo(string mdx, List<MdxColumn> columns, string traceHash, object[][] columnInfos, string cubeName);

		// Token: 0x060002B9 RID: 697
		void TraceRfcFunction(IRfcFunction function, List<string> skipElements, string filename);

		// Token: 0x060002BA RID: 698
		void TraceStatelessFunction(IRfcFunction function, string traceHash, string traceIdentifier, string status);

		// Token: 0x060002BB RID: 699
		void TraceString(Func<string> getString, string filename);
	}
}
