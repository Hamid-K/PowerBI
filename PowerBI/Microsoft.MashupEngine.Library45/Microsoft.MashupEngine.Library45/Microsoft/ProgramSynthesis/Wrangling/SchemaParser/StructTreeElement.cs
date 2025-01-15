using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.SchemaParser
{
	// Token: 0x0200016D RID: 365
	[DataContract]
	public class StructTreeElement<TRegion> : ListTreeElement<TRegion> where TRegion : IRegion<TRegion>
	{
		// Token: 0x06000827 RID: 2087 RVA: 0x00019598 File Offset: 0x00017798
		static StructTreeElement()
		{
			TreeElement<TRegion>.RegisteredTypes.Add(typeof(StructTreeElement<TRegion>));
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x000195AE File Offset: 0x000177AE
		public StructTreeElement()
		{
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x000195B6 File Offset: 0x000177B6
		public StructTreeElement(string n, TRegion reg, List<TreeElement<TRegion>> m)
			: base(n, "Struct", reg, m)
		{
		}
	}
}
