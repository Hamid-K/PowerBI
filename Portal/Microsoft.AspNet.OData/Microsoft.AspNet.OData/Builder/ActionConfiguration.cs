using System;
using System.Collections.Generic;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData.Builder
{
	// Token: 0x02000127 RID: 295
	public class ActionConfiguration : OperationConfiguration
	{
		// Token: 0x06000A29 RID: 2601 RVA: 0x000298C4 File Offset: 0x00027AC4
		internal ActionConfiguration(ODataModelBuilder builder, string name)
			: base(builder, name)
		{
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000A2A RID: 2602 RVA: 0x000102A1 File Offset: 0x0000E4A1
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.Action;
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000A2B RID: 2603 RVA: 0x000032B9 File Offset: 0x000014B9
		public override bool IsSideEffecting
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x000298D0 File Offset: 0x00027AD0
		public ActionConfiguration HasActionLink(Func<ResourceContext, Uri> actionLinkFactory, bool followsConventions)
		{
			if (actionLinkFactory == null)
			{
				throw Error.ArgumentNull("actionLinkFactory");
			}
			if (!this.IsBindable || this.BindingParameter.TypeConfiguration.Kind != EdmTypeKind.Entity)
			{
				throw Error.InvalidOperation(SRResources.HasActionLinkRequiresBindToEntity, new object[] { base.Name });
			}
			base.OperationLinkBuilder = new OperationLinkBuilder(actionLinkFactory, followsConventions);
			base.FollowsConventions = followsConventions;
			return this;
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x00028AF1 File Offset: 0x00026CF1
		public Func<ResourceContext, Uri> GetActionLink()
		{
			if (base.OperationLinkBuilder == null)
			{
				return null;
			}
			return base.OperationLinkBuilder.LinkFactory;
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x00029938 File Offset: 0x00027B38
		public ActionConfiguration HasFeedActionLink(Func<ResourceSetContext, Uri> actionLinkFactory, bool followsConventions)
		{
			if (actionLinkFactory == null)
			{
				throw Error.ArgumentNull("actionLinkFactory");
			}
			if (!this.IsBindable || this.BindingParameter.TypeConfiguration.Kind != EdmTypeKind.Collection || ((CollectionTypeConfiguration)this.BindingParameter.TypeConfiguration).ElementType.Kind != EdmTypeKind.Entity)
			{
				throw Error.InvalidOperation(SRResources.HasActionLinkRequiresBindToCollectionOfEntity, new object[] { base.Name });
			}
			base.OperationLinkBuilder = new OperationLinkBuilder(actionLinkFactory, followsConventions);
			base.FollowsConventions = followsConventions;
			return this;
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x00028B8A File Offset: 0x00026D8A
		public Func<ResourceSetContext, Uri> GetFeedActionLink()
		{
			if (base.OperationLinkBuilder == null)
			{
				return null;
			}
			return base.OperationLinkBuilder.FeedLinkFactory;
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x000299BA File Offset: 0x00027BBA
		public ActionConfiguration ReturnsFromEntitySet<TEntityType>(string entitySetName) where TEntityType : class
		{
			base.ReturnsFromEntitySetImplementation<TEntityType>(entitySetName);
			return this;
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x000299C4 File Offset: 0x00027BC4
		public ActionConfiguration ReturnsFromEntitySet<TEntityType>(EntitySetConfiguration<TEntityType> entitySetConfiguration) where TEntityType : class
		{
			if (entitySetConfiguration == null)
			{
				throw Error.ArgumentNull("entitySetConfiguration");
			}
			base.NavigationSource = entitySetConfiguration.EntitySet;
			base.ReturnType = base.ModelBuilder.GetTypeConfigurationOrNull(typeof(TEntityType));
			return this;
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x000299FC File Offset: 0x00027BFC
		public ActionConfiguration ReturnsCollectionFromEntitySet<TElementEntityType>(string entitySetName) where TElementEntityType : class
		{
			base.ReturnsCollectionFromEntitySetImplementation<TElementEntityType>(entitySetName);
			return this;
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x00029A08 File Offset: 0x00027C08
		public ActionConfiguration ReturnsCollectionFromEntitySet<TElementEntityType>(EntitySetConfiguration<TElementEntityType> entitySetConfiguration) where TElementEntityType : class
		{
			if (entitySetConfiguration == null)
			{
				throw Error.ArgumentNull("entitySetConfiguration");
			}
			Type typeFromHandle = typeof(IEnumerable<TElementEntityType>);
			base.NavigationSource = entitySetConfiguration.EntitySet;
			IEdmTypeConfiguration typeConfigurationOrNull = base.ModelBuilder.GetTypeConfigurationOrNull(typeof(TElementEntityType));
			base.ReturnType = new CollectionTypeConfiguration(typeConfigurationOrNull, typeFromHandle);
			return this;
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x00029A60 File Offset: 0x00027C60
		public ActionConfiguration Returns(Type clrReturnType)
		{
			if (clrReturnType == null)
			{
				throw Error.ArgumentNull("clrReturnType");
			}
			IEdmTypeConfiguration typeConfigurationOrNull = base.ModelBuilder.GetTypeConfigurationOrNull(clrReturnType);
			if (typeConfigurationOrNull is EntityTypeConfiguration)
			{
				throw Error.InvalidOperation(SRResources.ReturnEntityWithoutEntitySet, new object[] { typeConfigurationOrNull.FullName });
			}
			base.ReturnsImplementation(clrReturnType);
			return this;
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x00029AB8 File Offset: 0x00027CB8
		public ActionConfiguration Returns<TReturnType>()
		{
			Type typeFromHandle = typeof(TReturnType);
			return this.Returns(typeFromHandle);
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x00029AD8 File Offset: 0x00027CD8
		public ActionConfiguration ReturnsCollection<TReturnElementType>()
		{
			Type typeFromHandle = typeof(TReturnElementType);
			IEdmTypeConfiguration typeConfigurationOrNull = base.ModelBuilder.GetTypeConfigurationOrNull(typeFromHandle);
			if (typeConfigurationOrNull is EntityTypeConfiguration)
			{
				throw Error.InvalidOperation(SRResources.ReturnEntityCollectionWithoutEntitySet, new object[] { typeConfigurationOrNull.FullName });
			}
			base.ReturnsCollectionImplementation<TReturnElementType>();
			return this;
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x00028BEE File Offset: 0x00026DEE
		public ActionConfiguration SetBindingParameter(string name, IEdmTypeConfiguration bindingParameterType)
		{
			base.SetBindingParameterImplementation(name, bindingParameterType);
			return this;
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x00029B26 File Offset: 0x00027D26
		public ActionConfiguration ReturnsEntityViaEntitySetPath<TEntityType>(string entitySetPath) where TEntityType : class
		{
			if (string.IsNullOrEmpty(entitySetPath))
			{
				throw Error.ArgumentNull("entitySetPath");
			}
			base.ReturnsEntityViaEntitySetPathImplementation<TEntityType>(entitySetPath.Split(new char[] { '/' }));
			return this;
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x00029B53 File Offset: 0x00027D53
		public ActionConfiguration ReturnsEntityViaEntitySetPath<TEntityType>(params string[] entitySetPath) where TEntityType : class
		{
			base.ReturnsEntityViaEntitySetPathImplementation<TEntityType>(entitySetPath);
			return this;
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x00029B5D File Offset: 0x00027D5D
		public ActionConfiguration ReturnsCollectionViaEntitySetPath<TElementEntityType>(string entitySetPath) where TElementEntityType : class
		{
			if (string.IsNullOrEmpty(entitySetPath))
			{
				throw Error.ArgumentNull("entitySetPath");
			}
			base.ReturnsCollectionViaEntitySetPathImplementation<TElementEntityType>(entitySetPath.Split(new char[] { '/' }));
			return this;
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x00029B8A File Offset: 0x00027D8A
		public ActionConfiguration ReturnsCollectionViaEntitySetPath<TElementEntityType>(params string[] entitySetPath) where TElementEntityType : class
		{
			base.ReturnsCollectionViaEntitySetPathImplementation<TElementEntityType>(entitySetPath);
			return this;
		}
	}
}
