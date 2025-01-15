using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x0200022C RID: 556
	internal class RdlFunctionDefinition
	{
		// Token: 0x060012C4 RID: 4804 RVA: 0x0002A38D File Offset: 0x0002858D
		internal RdlFunctionDefinition(string name, Type nodeType, params RdlFunctionArg[] args)
		{
			this.m_name = name;
			this.m_nodeType = nodeType;
			this.m_args = args;
		}

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x060012C5 RID: 4805 RVA: 0x0002A3AA File Offset: 0x000285AA
		internal Type NodeType
		{
			get
			{
				return this.m_nodeType;
			}
		}

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x060012C6 RID: 4806 RVA: 0x0002A3B2 File Offset: 0x000285B2
		internal string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x060012C7 RID: 4807 RVA: 0x0002A3BA File Offset: 0x000285BA
		internal RdlFunctionArg[] Args
		{
			get
			{
				return this.m_args;
			}
		}

		// Token: 0x040005E8 RID: 1512
		private readonly Type m_nodeType;

		// Token: 0x040005E9 RID: 1513
		private readonly string m_name;

		// Token: 0x040005EA RID: 1514
		private readonly RdlFunctionArg[] m_args;
	}
}
