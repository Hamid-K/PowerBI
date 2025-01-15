using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data
{
	// Token: 0x02000007 RID: 7
	[ImmutableObject(true)]
	internal sealed class ConceptualPodParameter : IConceptualPodParameter
	{
		// Token: 0x0600000F RID: 15 RVA: 0x00002120 File Offset: 0x00000320
		internal ConceptualPodParameter(string name, IConceptualNavigationProperty navigationProperty)
		{
			this._name = name;
			this._navigationProperty = navigationProperty;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002136 File Offset: 0x00000336
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000011 RID: 17 RVA: 0x0000213E File Offset: 0x0000033E
		public IConceptualNavigationProperty NavigationProperty
		{
			get
			{
				return this._navigationProperty;
			}
		}

		// Token: 0x04000033 RID: 51
		private readonly string _name;

		// Token: 0x04000034 RID: 52
		private readonly IConceptualNavigationProperty _navigationProperty;
	}
}
