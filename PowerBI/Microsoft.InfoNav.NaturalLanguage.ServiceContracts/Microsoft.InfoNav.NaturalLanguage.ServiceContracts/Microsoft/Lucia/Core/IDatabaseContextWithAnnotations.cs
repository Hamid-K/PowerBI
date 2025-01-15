using System;
using System.Threading;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000095 RID: 149
	public interface IDatabaseContextWithAnnotations : IDatabaseContextWithLsdlContentType, IDatabaseContext, IDisposable
	{
		// Token: 0x060002A0 RID: 672
		void AnnotateConceptualSchema(IConceptualSchema schema, CancellationToken cancellationToken);
	}
}
