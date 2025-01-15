using System;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Dom;

namespace AngleSharp.Services
{
	// Token: 0x02000029 RID: 41
	public interface IDocumentFactory
	{
		// Token: 0x0600012C RID: 300
		Task<IDocument> CreateAsync(IBrowsingContext context, CreateDocumentOptions options, CancellationToken cancellationToken);
	}
}
