using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.SchemaParser
{
	// Token: 0x0200016F RID: 367
	[DataContract]
	public class SequenceTreeElement<TRegion> : ListTreeElement<TRegion> where TRegion : IRegion<TRegion>
	{
		// Token: 0x0600082D RID: 2093 RVA: 0x000195EC File Offset: 0x000177EC
		static SequenceTreeElement()
		{
			TreeElement<TRegion>.RegisteredTypes.Add(typeof(SequenceTreeElement<TRegion>));
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x000195AE File Offset: 0x000177AE
		public SequenceTreeElement()
		{
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x00019602 File Offset: 0x00017802
		public SequenceTreeElement(string n, TRegion reg, List<TreeElement<TRegion>> m)
			: base(n, "Sequence", reg, m)
		{
		}
	}
}
