using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom
{
	// Token: 0x0200017E RID: 382
	[DomName("ChildNode")]
	[DomNoInterfaceObject]
	public interface IChildNode
	{
		// Token: 0x06000DB8 RID: 3512
		[DomName("before")]
		void Before(params INode[] nodes);

		// Token: 0x06000DB9 RID: 3513
		[DomName("after")]
		void After(params INode[] nodes);

		// Token: 0x06000DBA RID: 3514
		[DomName("replace")]
		void Replace(params INode[] nodes);

		// Token: 0x06000DBB RID: 3515
		[DomName("remove")]
		void Remove();
	}
}
