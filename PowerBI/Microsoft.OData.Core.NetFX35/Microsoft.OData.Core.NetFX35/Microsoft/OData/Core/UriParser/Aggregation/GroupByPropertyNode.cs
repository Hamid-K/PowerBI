using System;
using System.Collections.Generic;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Aggregation
{
	// Token: 0x020002B1 RID: 689
	public sealed class GroupByPropertyNode
	{
		// Token: 0x060017B0 RID: 6064 RVA: 0x00050DF7 File Offset: 0x0004EFF7
		public GroupByPropertyNode(string name, SingleValueNode expression)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(name, "name");
			this.Name = name;
			this.Expression = expression;
		}

		// Token: 0x060017B1 RID: 6065 RVA: 0x00050E23 File Offset: 0x0004F023
		public GroupByPropertyNode(string name, SingleValueNode expression, IEdmTypeReference type)
			: this(name, expression)
		{
			this.typeReference = type;
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x060017B2 RID: 6066 RVA: 0x00050E34 File Offset: 0x0004F034
		// (set) Token: 0x060017B3 RID: 6067 RVA: 0x00050E3C File Offset: 0x0004F03C
		public string Name { get; private set; }

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x060017B4 RID: 6068 RVA: 0x00050E45 File Offset: 0x0004F045
		// (set) Token: 0x060017B5 RID: 6069 RVA: 0x00050E4D File Offset: 0x0004F04D
		public SingleValueNode Expression { get; private set; }

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x060017B6 RID: 6070 RVA: 0x00050E56 File Offset: 0x0004F056
		public IEdmTypeReference TypeReference
		{
			get
			{
				if (this.Expression == null)
				{
					return null;
				}
				return this.typeReference;
			}
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x060017B7 RID: 6071 RVA: 0x00050E68 File Offset: 0x0004F068
		// (set) Token: 0x060017B8 RID: 6072 RVA: 0x00050E70 File Offset: 0x0004F070
		public IList<GroupByPropertyNode> ChildTransformations
		{
			get
			{
				return this.childTransformations;
			}
			set
			{
				this.childTransformations = value;
			}
		}

		// Token: 0x04000A33 RID: 2611
		private IList<GroupByPropertyNode> childTransformations = new List<GroupByPropertyNode>();

		// Token: 0x04000A34 RID: 2612
		private IEdmTypeReference typeReference;
	}
}
