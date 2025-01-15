using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;

namespace Microsoft.OData
{
	// Token: 0x02000041 RID: 65
	public interface IODataResponseMessageAsync : IODataResponseMessage
	{
		// Token: 0x0600022D RID: 557
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "This is intentionally a method.")]
		Task<Stream> GetStreamAsync();
	}
}
