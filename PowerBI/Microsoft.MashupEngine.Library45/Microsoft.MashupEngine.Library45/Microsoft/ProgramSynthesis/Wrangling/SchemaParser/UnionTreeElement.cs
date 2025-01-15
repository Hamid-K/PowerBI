using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.SchemaParser
{
	// Token: 0x0200016E RID: 366
	[DataContract]
	public class UnionTreeElement<TRegion> : ListTreeElement<TRegion> where TRegion : IRegion<TRegion>
	{
		// Token: 0x0600082A RID: 2090 RVA: 0x000195C6 File Offset: 0x000177C6
		static UnionTreeElement()
		{
			TreeElement<TRegion>.RegisteredTypes.Add(typeof(UnionTreeElement<TRegion>));
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x000195AE File Offset: 0x000177AE
		public UnionTreeElement()
		{
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x000195DC File Offset: 0x000177DC
		public UnionTreeElement(string n, TRegion reg, List<TreeElement<TRegion>> m)
			: base(n, "Union", reg, m)
		{
		}
	}
}
