using System;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Core.UriParser.Visitors;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x02000239 RID: 569
	public sealed class EntityRangeVariableReferenceNode : SingleEntityNode
	{
		// Token: 0x06001469 RID: 5225 RVA: 0x00049760 File Offset: 0x00047960
		public EntityRangeVariableReferenceNode(string name, EntityRangeVariable rangeVariable)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(name, "name");
			ExceptionUtils.CheckArgumentNotNull<EntityRangeVariable>(rangeVariable, "rangeVariable");
			this.name = name;
			this.navigationSource = rangeVariable.NavigationSource;
			this.entityTypeReference = rangeVariable.EntityTypeReference;
			this.rangeVariable = rangeVariable;
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x0600146A RID: 5226 RVA: 0x000497AF File Offset: 0x000479AF
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x0600146B RID: 5227 RVA: 0x000497B7 File Offset: 0x000479B7
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return this.entityTypeReference;
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x0600146C RID: 5228 RVA: 0x000497BF File Offset: 0x000479BF
		public override IEdmEntityTypeReference EntityTypeReference
		{
			get
			{
				return this.entityTypeReference;
			}
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x0600146D RID: 5229 RVA: 0x000497C7 File Offset: 0x000479C7
		public EntityRangeVariable RangeVariable
		{
			get
			{
				return this.rangeVariable;
			}
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x0600146E RID: 5230 RVA: 0x000497CF File Offset: 0x000479CF
		public override IEdmNavigationSource NavigationSource
		{
			get
			{
				return this.navigationSource;
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x0600146F RID: 5231 RVA: 0x000497D7 File Offset: 0x000479D7
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.EntityRangeVariableReference;
			}
		}

		// Token: 0x06001470 RID: 5232 RVA: 0x000497DB File Offset: 0x000479DB
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x04000894 RID: 2196
		private readonly string name;

		// Token: 0x04000895 RID: 2197
		private readonly IEdmEntityTypeReference entityTypeReference;

		// Token: 0x04000896 RID: 2198
		private readonly EntityRangeVariable rangeVariable;

		// Token: 0x04000897 RID: 2199
		private readonly IEdmNavigationSource navigationSource;
	}
}
