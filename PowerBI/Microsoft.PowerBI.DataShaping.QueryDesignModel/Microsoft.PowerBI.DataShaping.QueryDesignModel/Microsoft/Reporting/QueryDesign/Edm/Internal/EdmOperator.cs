using System;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x0200020F RID: 527
	internal sealed class EdmOperator
	{
		// Token: 0x06001899 RID: 6297 RVA: 0x000434D1 File Offset: 0x000416D1
		internal EdmOperator(string name, ConceptualResultType parameterType, ConceptualResultType conceptualResultType)
		{
			this.Name = name;
			this.ParameterType = parameterType;
			this.ConceptualReturnType = conceptualResultType;
		}

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x0600189A RID: 6298 RVA: 0x000434EE File Offset: 0x000416EE
		public string Name { get; }

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x0600189B RID: 6299 RVA: 0x000434F6 File Offset: 0x000416F6
		public ConceptualResultType ParameterType { get; }

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x0600189C RID: 6300 RVA: 0x000434FE File Offset: 0x000416FE
		public ConceptualResultType ConceptualReturnType { get; }
	}
}
