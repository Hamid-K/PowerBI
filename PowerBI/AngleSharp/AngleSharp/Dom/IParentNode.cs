using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom
{
	// Token: 0x02000196 RID: 406
	[DomName("ParentNode")]
	[DomNoInterfaceObject]
	public interface IParentNode
	{
		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000E87 RID: 3719
		[DomName("children")]
		IHtmlCollection<IElement> Children { get; }

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000E88 RID: 3720
		[DomName("firstElementChild")]
		IElement FirstElementChild { get; }

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000E89 RID: 3721
		[DomName("lastElementChild")]
		IElement LastElementChild { get; }

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000E8A RID: 3722
		[DomName("childElementCount")]
		int ChildElementCount { get; }

		// Token: 0x06000E8B RID: 3723
		[DomName("append")]
		void Append(params INode[] nodes);

		// Token: 0x06000E8C RID: 3724
		[DomName("prepend")]
		void Prepend(params INode[] nodes);

		// Token: 0x06000E8D RID: 3725
		[DomName("querySelector")]
		IElement QuerySelector(string selectors);

		// Token: 0x06000E8E RID: 3726
		[DomName("querySelectorAll")]
		IHtmlCollection<IElement> QuerySelectorAll(string selectors);
	}
}
