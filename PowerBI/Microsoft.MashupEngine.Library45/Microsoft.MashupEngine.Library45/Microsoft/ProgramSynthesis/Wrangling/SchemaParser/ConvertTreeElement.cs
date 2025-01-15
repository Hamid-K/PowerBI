using System;
using System.Runtime.Serialization;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.SchemaParser
{
	// Token: 0x02000171 RID: 369
	[DataContract]
	public class ConvertTreeElement<TRegionParent, TRegionChild> : TreeElement<TRegionParent> where TRegionParent : IRegion<TRegionParent> where TRegionChild : IRegion<TRegionChild>
	{
		// Token: 0x06000833 RID: 2099 RVA: 0x0001963F File Offset: 0x0001783F
		static ConvertTreeElement()
		{
			TreeElement<TRegionParent>.RegisteredTypes.Add(typeof(ConvertTreeElement<TRegionParent, TRegionChild>));
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x000194A7 File Offset: 0x000176A7
		public ConvertTreeElement()
		{
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x00019655 File Offset: 0x00017855
		public ConvertTreeElement(string n, TRegionChild reg, TreeElement<TRegionChild> child)
			: base(n, "Convert")
		{
			this.Child = child;
			this.Region = reg;
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000836 RID: 2102 RVA: 0x00019671 File Offset: 0x00017871
		// (set) Token: 0x06000837 RID: 2103 RVA: 0x00019679 File Offset: 0x00017879
		[DataMember]
		public TreeElement<TRegionChild> Child { get; set; }

		// Token: 0x06000838 RID: 2104 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public override bool IsInside(TRegionParent r)
		{
			return false;
		}

		// Token: 0x04000386 RID: 902
		[DataMember]
		public TRegionChild Region;
	}
}
