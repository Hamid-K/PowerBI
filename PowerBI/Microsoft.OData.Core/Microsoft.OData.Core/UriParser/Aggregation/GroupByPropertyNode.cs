using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser.Aggregation
{
	// Token: 0x020001FA RID: 506
	public sealed class GroupByPropertyNode
	{
		// Token: 0x06001681 RID: 5761 RVA: 0x0003F23E File Offset: 0x0003D43E
		public GroupByPropertyNode(string name, SingleValueNode expression)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(name, "name");
			this.Name = name;
			this.Expression = expression;
		}

		// Token: 0x06001682 RID: 5762 RVA: 0x0003F26B File Offset: 0x0003D46B
		public GroupByPropertyNode(string name, SingleValueNode expression, IEdmTypeReference type)
			: this(name, expression)
		{
			this.typeReference = type;
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x06001683 RID: 5763 RVA: 0x0003F27C File Offset: 0x0003D47C
		// (set) Token: 0x06001684 RID: 5764 RVA: 0x0003F284 File Offset: 0x0003D484
		public string Name { get; private set; }

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x06001685 RID: 5765 RVA: 0x0003F28D File Offset: 0x0003D48D
		// (set) Token: 0x06001686 RID: 5766 RVA: 0x0003F295 File Offset: 0x0003D495
		public SingleValueNode Expression { get; private set; }

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x06001687 RID: 5767 RVA: 0x0003F29E File Offset: 0x0003D49E
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

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x06001688 RID: 5768 RVA: 0x0003F2B0 File Offset: 0x0003D4B0
		// (set) Token: 0x06001689 RID: 5769 RVA: 0x0003F2B8 File Offset: 0x0003D4B8
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

		// Token: 0x04000A27 RID: 2599
		private IList<GroupByPropertyNode> childTransformations = new List<GroupByPropertyNode>();

		// Token: 0x04000A28 RID: 2600
		private IEdmTypeReference typeReference;
	}
}
