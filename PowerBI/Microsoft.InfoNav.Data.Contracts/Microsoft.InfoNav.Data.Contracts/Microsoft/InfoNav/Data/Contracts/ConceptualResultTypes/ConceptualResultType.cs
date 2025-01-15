using System;

namespace Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes
{
	// Token: 0x02000140 RID: 320
	public abstract class ConceptualResultType : IEquatable<ConceptualResultType>
	{
		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000833 RID: 2099
		public abstract ConceptualResultTypeKind Kind { get; }

		// Token: 0x06000834 RID: 2100
		public abstract bool Equals(ConceptualResultType other);

		// Token: 0x06000835 RID: 2101 RVA: 0x000110F0 File Offset: 0x0000F2F0
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ConceptualResultType);
		}

		// Token: 0x06000836 RID: 2102
		public abstract override int GetHashCode();
	}
}
