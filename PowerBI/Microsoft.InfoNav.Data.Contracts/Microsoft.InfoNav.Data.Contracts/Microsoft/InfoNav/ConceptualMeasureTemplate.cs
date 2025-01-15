using System;
using System.ComponentModel;

namespace Microsoft.InfoNav
{
	// Token: 0x02000025 RID: 37
	[ImmutableObject(true)]
	public sealed class ConceptualMeasureTemplate
	{
		// Token: 0x06000088 RID: 136 RVA: 0x000027E0 File Offset: 0x000009E0
		internal ConceptualMeasureTemplate(string daxTemplateName)
		{
			this._daxTemplateName = daxTemplateName;
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000089 RID: 137 RVA: 0x000027EF File Offset: 0x000009EF
		internal string DaxTemplateName
		{
			get
			{
				return this._daxTemplateName;
			}
		}

		// Token: 0x040000B8 RID: 184
		private readonly string _daxTemplateName;
	}
}
