using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.SchemaParser
{
	// Token: 0x0200016C RID: 364
	[DataContract]
	public abstract class ListTreeElement<TRegion> : ConcTreeElement<TRegion> where TRegion : IRegion<TRegion>
	{
		// Token: 0x06000822 RID: 2082 RVA: 0x000194F8 File Offset: 0x000176F8
		static ListTreeElement()
		{
			TreeElement<TRegion>.RegisteredTypes.Add(typeof(ListTreeElement<TRegion>));
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x0001950E File Offset: 0x0001770E
		protected ListTreeElement()
		{
			this.Children = new List<TreeElement<TRegion>>();
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000824 RID: 2084 RVA: 0x00019521 File Offset: 0x00017721
		// (set) Token: 0x06000825 RID: 2085 RVA: 0x00019529 File Offset: 0x00017729
		[DataMember]
		public List<TreeElement<TRegion>> Children { get; protected set; }

		// Token: 0x06000826 RID: 2086 RVA: 0x00019534 File Offset: 0x00017734
		protected ListTreeElement(string n, string type, TRegion reg, List<TreeElement<TRegion>> m)
			: base(n, type, reg)
		{
			this.Children = m;
			foreach (TreeElement<TRegion> treeElement in this.Children)
			{
				treeElement.Parent = this;
			}
		}
	}
}
