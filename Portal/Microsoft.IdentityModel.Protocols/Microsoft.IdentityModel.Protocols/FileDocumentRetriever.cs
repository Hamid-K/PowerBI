using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Logging;

namespace Microsoft.IdentityModel.Protocols
{
	// Token: 0x02000005 RID: 5
	public class FileDocumentRetriever : IDocumentRetriever
	{
		// Token: 0x06000024 RID: 36 RVA: 0x000026C0 File Offset: 0x000008C0
		public async Task<string> GetDocumentAsync(string address, CancellationToken cancel)
		{
			if (string.IsNullOrWhiteSpace(address))
			{
				throw LogHelper.LogArgumentNullException("address");
			}
			string text;
			try
			{
				using (StreamReader reader = File.OpenText(address))
				{
					text = await reader.ReadToEndAsync().ConfigureAwait(false);
				}
			}
			catch (Exception ex)
			{
				throw LogHelper.LogExceptionMessage(new IOException(LogHelper.FormatInvariant("IDX20804: Unable to retrieve document from: '{0}'.", new object[] { address }), ex));
			}
			return text;
		}
	}
}
