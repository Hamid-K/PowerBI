using System;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData.Builder
{
	// Token: 0x02000121 RID: 289
	public class FunctionConfiguration : OperationConfiguration
	{
		// Token: 0x060009DF RID: 2527 RVA: 0x00028A36 File Offset: 0x00026C36
		internal FunctionConfiguration(ODataModelBuilder builder, string name)
			: base(builder, name)
		{
			this.IncludeInServiceDocument = true;
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x060009E0 RID: 2528 RVA: 0x000032B9 File Offset: 0x000014B9
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.Function;
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x060009E1 RID: 2529 RVA: 0x00028A47 File Offset: 0x00026C47
		// (set) Token: 0x060009E2 RID: 2530 RVA: 0x00028A4F File Offset: 0x00026C4F
		public new bool IsComposable
		{
			get
			{
				return base.IsComposable;
			}
			set
			{
				base.IsComposable = value;
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x060009E3 RID: 2531 RVA: 0x000102A1 File Offset: 0x0000E4A1
		public override bool IsSideEffecting
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x060009E4 RID: 2532 RVA: 0x00028A58 File Offset: 0x00026C58
		// (set) Token: 0x060009E5 RID: 2533 RVA: 0x00028A60 File Offset: 0x00026C60
		public bool SupportedInFilter { get; set; }

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x060009E6 RID: 2534 RVA: 0x00028A69 File Offset: 0x00026C69
		// (set) Token: 0x060009E7 RID: 2535 RVA: 0x00028A71 File Offset: 0x00026C71
		public bool SupportedInOrderBy { get; set; }

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x060009E8 RID: 2536 RVA: 0x00028A7A File Offset: 0x00026C7A
		// (set) Token: 0x060009E9 RID: 2537 RVA: 0x00028A82 File Offset: 0x00026C82
		public bool IncludeInServiceDocument { get; set; }

		// Token: 0x060009EA RID: 2538 RVA: 0x00028A8C File Offset: 0x00026C8C
		public FunctionConfiguration HasFunctionLink(Func<ResourceContext, Uri> functionLinkFactory, bool followsConventions)
		{
			if (functionLinkFactory == null)
			{
				throw Error.ArgumentNull("functionLinkFactory");
			}
			if (!this.IsBindable || this.BindingParameter.TypeConfiguration.Kind != EdmTypeKind.Entity)
			{
				throw Error.InvalidOperation(SRResources.HasFunctionLinkRequiresBindToEntity, new object[] { base.Name });
			}
			base.OperationLinkBuilder = new OperationLinkBuilder(functionLinkFactory, followsConventions);
			base.FollowsConventions = followsConventions;
			return this;
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x00028AF1 File Offset: 0x00026CF1
		public Func<ResourceContext, Uri> GetFunctionLink()
		{
			if (base.OperationLinkBuilder == null)
			{
				return null;
			}
			return base.OperationLinkBuilder.LinkFactory;
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x00028B08 File Offset: 0x00026D08
		public FunctionConfiguration HasFeedFunctionLink(Func<ResourceSetContext, Uri> functionLinkFactory, bool followsConventions)
		{
			if (functionLinkFactory == null)
			{
				throw Error.ArgumentNull("functionLinkFactory");
			}
			if (!this.IsBindable || this.BindingParameter.TypeConfiguration.Kind != EdmTypeKind.Collection || ((CollectionTypeConfiguration)this.BindingParameter.TypeConfiguration).ElementType.Kind != EdmTypeKind.Entity)
			{
				throw Error.InvalidOperation(SRResources.HasFunctionLinkRequiresBindToCollectionOfEntity, new object[] { base.Name });
			}
			base.OperationLinkBuilder = new OperationLinkBuilder(functionLinkFactory, followsConventions);
			base.FollowsConventions = followsConventions;
			return this;
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x00028B8A File Offset: 0x00026D8A
		public Func<ResourceSetContext, Uri> GetFeedFunctionLink()
		{
			if (base.OperationLinkBuilder == null)
			{
				return null;
			}
			return base.OperationLinkBuilder.FeedLinkFactory;
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x00028BA1 File Offset: 0x00026DA1
		public FunctionConfiguration ReturnsFromEntitySet<TEntityType>(string entitySetName) where TEntityType : class
		{
			base.ReturnsFromEntitySetImplementation<TEntityType>(entitySetName);
			return this;
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x00028BAB File Offset: 0x00026DAB
		public FunctionConfiguration ReturnsCollectionFromEntitySet<TElementEntityType>(string entitySetName) where TElementEntityType : class
		{
			base.ReturnsCollectionFromEntitySetImplementation<TElementEntityType>(entitySetName);
			return this;
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x00028BB5 File Offset: 0x00026DB5
		public FunctionConfiguration Returns(Type clrReturnType)
		{
			if (clrReturnType == null)
			{
				throw Error.ArgumentNull("clrReturnType");
			}
			base.ReturnsImplementation(clrReturnType);
			return this;
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x00028BD3 File Offset: 0x00026DD3
		public FunctionConfiguration Returns<TReturnType>()
		{
			return this.Returns(typeof(TReturnType));
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x00028BE5 File Offset: 0x00026DE5
		public FunctionConfiguration ReturnsCollection<TReturnElementType>()
		{
			base.ReturnsCollectionImplementation<TReturnElementType>();
			return this;
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x00028BEE File Offset: 0x00026DEE
		public FunctionConfiguration SetBindingParameter(string name, IEdmTypeConfiguration bindingParameterType)
		{
			base.SetBindingParameterImplementation(name, bindingParameterType);
			return this;
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x00028BF9 File Offset: 0x00026DF9
		public FunctionConfiguration ReturnsEntityViaEntitySetPath<TEntityType>(string entitySetPath) where TEntityType : class
		{
			if (string.IsNullOrEmpty(entitySetPath))
			{
				throw Error.ArgumentNull("entitySetPath");
			}
			base.ReturnsEntityViaEntitySetPathImplementation<TEntityType>(entitySetPath.Split(new char[] { '/' }));
			return this;
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x00028C26 File Offset: 0x00026E26
		public FunctionConfiguration ReturnsEntityViaEntitySetPath<TEntityType>(params string[] entitySetPath) where TEntityType : class
		{
			base.ReturnsEntityViaEntitySetPathImplementation<TEntityType>(entitySetPath);
			return this;
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x00028C30 File Offset: 0x00026E30
		public FunctionConfiguration ReturnsCollectionViaEntitySetPath<TElementEntityType>(string entitySetPath) where TElementEntityType : class
		{
			if (string.IsNullOrEmpty(entitySetPath))
			{
				throw Error.ArgumentNull("entitySetPath");
			}
			base.ReturnsCollectionViaEntitySetPathImplementation<TElementEntityType>(entitySetPath.Split(new char[] { '/' }));
			return this;
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x00028C5D File Offset: 0x00026E5D
		public FunctionConfiguration ReturnsCollectionViaEntitySetPath<TElementEntityType>(params string[] entitySetPath) where TElementEntityType : class
		{
			base.ReturnsCollectionViaEntitySetPathImplementation<TElementEntityType>(entitySetPath);
			return this;
		}
	}
}
