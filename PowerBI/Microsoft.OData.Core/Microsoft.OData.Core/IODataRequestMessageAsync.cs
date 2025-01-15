using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;

namespace Microsoft.OData
{
	// Token: 0x0200003F RID: 63
	public interface IODataRequestMessageAsync : IODataRequestMessage
	{
		// Token: 0x06000226 RID: 550
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "This is intentionally a method.")]
		Task<Stream> GetStreamAsync();
	}
}
