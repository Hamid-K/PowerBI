using System;
using System.Collections.Specialized;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200004C RID: 76
	public interface IParametersTranslator
	{
		// Token: 0x0600023F RID: 575
		void GetParamsInstance(string paramsInstanceId, out ExternalItemPath itemPath, out NameValueCollection parameters);
	}
}
