using System;
using System.Runtime.Serialization;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000107 RID: 263
	[DataContract(Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class PropertyTermBinding : PropertyTermBaseBinding
	{
		// Token: 0x0600052C RID: 1324 RVA: 0x00009415 File Offset: 0x00007615
		public override T Accept<T>(TermBindingVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}
	}
}
