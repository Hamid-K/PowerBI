using System;
using System.Runtime.Serialization;

namespace Microsoft.Lucia.Core
{
	// Token: 0x0200010A RID: 266
	[DataContract(Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class TableTermBinding : ModelEntityTermBinding
	{
		// Token: 0x06000534 RID: 1332 RVA: 0x00009459 File Offset: 0x00007659
		public override T Accept<T>(TermBindingVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}
	}
}
