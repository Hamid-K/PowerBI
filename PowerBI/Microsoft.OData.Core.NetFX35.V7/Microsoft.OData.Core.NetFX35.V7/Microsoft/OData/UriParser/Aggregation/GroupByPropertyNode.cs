using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser.Aggregation
{
	// Token: 0x020001CE RID: 462
	public sealed class GroupByPropertyNode
	{
		// Token: 0x060011F3 RID: 4595 RVA: 0x000320D5 File Offset: 0x000302D5
		public GroupByPropertyNode(string name, SingleValueNode expression)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(name, "name");
			this.Name = name;
			this.Expression = expression;
		}

		// Token: 0x060011F4 RID: 4596 RVA: 0x00032102 File Offset: 0x00030302
		public GroupByPropertyNode(string name, SingleValueNode expression, IEdmTypeReference type)
			: this(name, expression)
		{
			this.typeReference = type;
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x060011F5 RID: 4597 RVA: 0x00032113 File Offset: 0x00030313
		// (set) Token: 0x060011F6 RID: 4598 RVA: 0x0003211B File Offset: 0x0003031B
		public string Name { get; private set; }

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x060011F7 RID: 4599 RVA: 0x00032124 File Offset: 0x00030324
		// (set) Token: 0x060011F8 RID: 4600 RVA: 0x0003212C File Offset: 0x0003032C
		public SingleValueNode Expression { get; private set; }

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x060011F9 RID: 4601 RVA: 0x00032135 File Offset: 0x00030335
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

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x060011FA RID: 4602 RVA: 0x00032147 File Offset: 0x00030347
		// (set) Token: 0x060011FB RID: 4603 RVA: 0x0003214F File Offset: 0x0003034F
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

		// Token: 0x0400091E RID: 2334
		private IList<GroupByPropertyNode> childTransformations = new List<GroupByPropertyNode>();

		// Token: 0x0400091F RID: 2335
		private IEdmTypeReference typeReference;
	}
}
