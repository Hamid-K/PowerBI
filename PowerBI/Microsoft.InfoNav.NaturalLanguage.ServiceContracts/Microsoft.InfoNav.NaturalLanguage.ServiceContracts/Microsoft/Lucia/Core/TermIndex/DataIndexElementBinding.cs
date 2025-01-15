using System;
using System.ComponentModel;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.Lucia.Core.TermIndex
{
	// Token: 0x0200015B RID: 347
	[ImmutableObject(true)]
	public sealed class DataIndexElementBinding
	{
		// Token: 0x060006E5 RID: 1765 RVA: 0x0000BD32 File Offset: 0x00009F32
		public DataIndexElementBinding(string conceptualEntity, string conceptualProperty)
		{
			this.ConceptualEntity = conceptualEntity;
			this.ConceptualProperty = conceptualProperty;
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x060006E6 RID: 1766 RVA: 0x0000BD48 File Offset: 0x00009F48
		public string ConceptualEntity { get; }

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x060006E7 RID: 1767 RVA: 0x0000BD50 File Offset: 0x00009F50
		public string ConceptualProperty { get; }

		// Token: 0x060006E8 RID: 1768 RVA: 0x0000BD58 File Offset: 0x00009F58
		public EdmPropertyRef ToEdmPropertyRef()
		{
			return new EdmPropertyRef(this.ConceptualEntity, this.ConceptualProperty);
		}
	}
}
