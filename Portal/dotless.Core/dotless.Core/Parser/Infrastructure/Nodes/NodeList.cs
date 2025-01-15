using System;
using System.Collections.Generic;

namespace dotless.Core.Parser.Infrastructure.Nodes
{
	// Token: 0x0200005F RID: 95
	public class NodeList : NodeList<Node>
	{
		// Token: 0x06000424 RID: 1060 RVA: 0x00015192 File Offset: 0x00013392
		public NodeList()
		{
			this.Inner = new List<Node>();
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x000151A5 File Offset: 0x000133A5
		public NodeList(params Node[] nodes)
			: this(nodes)
		{
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x000151AE File Offset: 0x000133AE
		public NodeList(IEnumerable<Node> nodes)
		{
			this.Inner = new List<Node>(nodes);
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x000151C2 File Offset: 0x000133C2
		public NodeList(NodeList nodes)
			: this(nodes)
		{
			base.IsReference = nodes.IsReference;
		}
	}
}
