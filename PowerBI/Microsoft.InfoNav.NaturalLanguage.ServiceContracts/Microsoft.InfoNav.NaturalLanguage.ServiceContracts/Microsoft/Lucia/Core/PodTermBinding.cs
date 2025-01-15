using System;
using System.Runtime.Serialization;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000105 RID: 261
	[DataContract(Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class PodTermBinding : ModelEntityTermBinding
	{
		// Token: 0x06000518 RID: 1304 RVA: 0x00009293 File Offset: 0x00007493
		public override T Accept<T>(TermBindingVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}
	}
}
