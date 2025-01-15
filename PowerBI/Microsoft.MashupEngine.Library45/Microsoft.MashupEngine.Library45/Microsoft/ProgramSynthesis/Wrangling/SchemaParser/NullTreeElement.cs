using System;
using System.Runtime.Serialization;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.SchemaParser
{
	// Token: 0x0200016A RID: 362
	[DataContract]
	public class NullTreeElement<TRegion> : TreeElement<TRegion> where TRegion : IRegion<TRegion>
	{
		// Token: 0x06000819 RID: 2073 RVA: 0x0001945F File Offset: 0x0001765F
		static NullTreeElement()
		{
			TreeElement<TRegion>.RegisteredTypes.Add(typeof(NullTreeElement<TRegion>));
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x00019475 File Offset: 0x00017675
		public NullTreeElement(string n)
			: base(n, "Null")
		{
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x00019483 File Offset: 0x00017683
		public NullTreeElement()
			: base(null, "Null")
		{
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public override bool IsInside(TRegion r)
		{
			return false;
		}
	}
}
