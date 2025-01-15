using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.AspNet.OData.Formatter.Serialization;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData.Builder
{
	// Token: 0x02000110 RID: 272
	public class NavigationSourceLinkBuilderAnnotation
	{
		// Token: 0x06000952 RID: 2386 RVA: 0x00027308 File Offset: 0x00025508
		public NavigationSourceLinkBuilderAnnotation()
		{
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x0002731C File Offset: 0x0002551C
		public NavigationSourceLinkBuilderAnnotation(IEdmNavigationSource navigationSource, IEdmModel model)
		{
			if (navigationSource == null)
			{
				throw Error.ArgumentNull("navigationSource");
			}
			if (model == null)
			{
				throw Error.ArgumentNull("model");
			}
			IEdmEntityType edmEntityType = navigationSource.EntityType();
			IEnumerable<IEdmEntityType> enumerable = model.FindAllDerivedTypes(edmEntityType).Cast<IEdmEntityType>();
			foreach (IEdmNavigationProperty edmNavigationProperty in edmEntityType.NavigationProperties())
			{
				Func<ResourceContext, IEdmNavigationProperty, Uri> func = (ResourceContext resourceContext, IEdmNavigationProperty navProperty) => resourceContext.GenerateNavigationPropertyLink(navProperty, false);
				this.AddNavigationPropertyLinkBuilder(edmNavigationProperty, new NavigationLinkBuilder(func, true));
			}
			bool derivedTypesDefineNavigationProperty = false;
			foreach (IEdmEntityType edmEntityType2 in enumerable)
			{
				foreach (IEdmNavigationProperty edmNavigationProperty2 in edmEntityType2.DeclaredNavigationProperties())
				{
					derivedTypesDefineNavigationProperty = true;
					Func<ResourceContext, IEdmNavigationProperty, Uri> func2 = (ResourceContext resourceContext, IEdmNavigationProperty navProperty) => resourceContext.GenerateNavigationPropertyLink(navProperty, true);
					this.AddNavigationPropertyLinkBuilder(edmNavigationProperty2, new NavigationLinkBuilder(func2, true));
				}
			}
			Func<ResourceContext, Uri> func3 = (ResourceContext resourceContext) => resourceContext.GenerateSelfLink(derivedTypesDefineNavigationProperty);
			this._idLinkBuilder = new SelfLinkBuilder<Uri>(func3, true);
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x000274A8 File Offset: 0x000256A8
		public NavigationSourceLinkBuilderAnnotation(IEdmNavigationSource navigationSource, SelfLinkBuilder<Uri> idLinkBuilder, SelfLinkBuilder<Uri> editLinkBuilder, SelfLinkBuilder<Uri> readLinkBuilder)
		{
			if (navigationSource == null)
			{
				throw Error.ArgumentNull("navigationSource");
			}
			this._idLinkBuilder = idLinkBuilder;
			this._editLinkBuilder = editLinkBuilder;
			this._readLinkBuilder = readLinkBuilder;
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x000274E0 File Offset: 0x000256E0
		public NavigationSourceLinkBuilderAnnotation(NavigationSourceConfiguration navigationSource)
		{
			if (navigationSource == null)
			{
				throw Error.ArgumentNull("navigationSource");
			}
			this._idLinkBuilder = navigationSource.GetIdLink();
			this._editLinkBuilder = navigationSource.GetEditLink();
			this._readLinkBuilder = navigationSource.GetReadLink();
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x00027530 File Offset: 0x00025730
		public void AddNavigationPropertyLinkBuilder(IEdmNavigationProperty navigationProperty, NavigationLinkBuilder linkBuilder)
		{
			this._navigationPropertyLinkBuilderLookup[navigationProperty] = linkBuilder;
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x00027540 File Offset: 0x00025740
		public virtual EntitySelfLinks BuildEntitySelfLinks(ResourceContext instanceContext, ODataMetadataLevel metadataLevel)
		{
			EntitySelfLinks entitySelfLinks = new EntitySelfLinks();
			entitySelfLinks.IdLink = this.BuildIdLink(instanceContext, metadataLevel);
			entitySelfLinks.EditLink = this.BuildEditLink(instanceContext, metadataLevel, entitySelfLinks.IdLink);
			entitySelfLinks.ReadLink = this.BuildReadLink(instanceContext, metadataLevel, entitySelfLinks.EditLink);
			return entitySelfLinks;
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x0002758A File Offset: 0x0002578A
		public virtual Uri BuildIdLink(ResourceContext instanceContext, ODataMetadataLevel metadataLevel)
		{
			if (instanceContext == null)
			{
				throw Error.ArgumentNull("instanceContext");
			}
			if (this._idLinkBuilder != null && (metadataLevel == ODataMetadataLevel.FullMetadata || (metadataLevel == ODataMetadataLevel.MinimalMetadata && !this._idLinkBuilder.FollowsConventions)))
			{
				return this._idLinkBuilder.Factory(instanceContext);
			}
			return null;
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x000275C9 File Offset: 0x000257C9
		internal Uri BuildIdLink(ResourceContext instanceContext)
		{
			return this.BuildIdLink(instanceContext, ODataMetadataLevel.FullMetadata);
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x000275D3 File Offset: 0x000257D3
		public virtual Uri BuildEditLink(ResourceContext instanceContext, ODataMetadataLevel metadataLevel, Uri idLink)
		{
			if (instanceContext == null)
			{
				throw Error.ArgumentNull("instanceContext");
			}
			if (this._editLinkBuilder != null && (metadataLevel == ODataMetadataLevel.FullMetadata || (metadataLevel == ODataMetadataLevel.MinimalMetadata && !this._editLinkBuilder.FollowsConventions)))
			{
				return this._editLinkBuilder.Factory(instanceContext);
			}
			return null;
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x00027612 File Offset: 0x00025812
		internal Uri BuildEditLink(ResourceContext instanceContext)
		{
			return this.BuildEditLink(instanceContext, ODataMetadataLevel.FullMetadata, null);
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x0002761D File Offset: 0x0002581D
		public virtual Uri BuildReadLink(ResourceContext instanceContext, ODataMetadataLevel metadataLevel, Uri editLink)
		{
			if (instanceContext == null)
			{
				throw Error.ArgumentNull("instanceContext");
			}
			if (this._readLinkBuilder != null && (metadataLevel == ODataMetadataLevel.FullMetadata || (metadataLevel == ODataMetadataLevel.MinimalMetadata && !this._readLinkBuilder.FollowsConventions)))
			{
				return this._readLinkBuilder.Factory(instanceContext);
			}
			return null;
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x0002765C File Offset: 0x0002585C
		internal Uri BuildReadLink(ResourceContext instanceContext)
		{
			return this.BuildReadLink(instanceContext, ODataMetadataLevel.FullMetadata, null);
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x00027668 File Offset: 0x00025868
		public virtual Uri BuildNavigationLink(ResourceContext instanceContext, IEdmNavigationProperty navigationProperty, ODataMetadataLevel metadataLevel)
		{
			if (instanceContext == null)
			{
				throw Error.ArgumentNull("instanceContext");
			}
			if (navigationProperty == null)
			{
				throw Error.ArgumentNull("navigationProperty");
			}
			NavigationLinkBuilder navigationLinkBuilder;
			if (this._navigationPropertyLinkBuilderLookup.TryGetValue(navigationProperty, out navigationLinkBuilder) && !navigationLinkBuilder.FollowsConventions && (metadataLevel == ODataMetadataLevel.MinimalMetadata || metadataLevel == ODataMetadataLevel.FullMetadata))
			{
				return navigationLinkBuilder.Factory(instanceContext, navigationProperty);
			}
			return null;
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x000276C0 File Offset: 0x000258C0
		internal Uri BuildNavigationLink(ResourceContext instanceContext, IEdmNavigationProperty navigationProperty)
		{
			if (instanceContext == null)
			{
				throw Error.ArgumentNull("instanceContext");
			}
			if (navigationProperty == null)
			{
				throw Error.ArgumentNull("navigationProperty");
			}
			NavigationLinkBuilder navigationLinkBuilder;
			if (this._navigationPropertyLinkBuilderLookup.TryGetValue(navigationProperty, out navigationLinkBuilder))
			{
				return navigationLinkBuilder.Factory(instanceContext, navigationProperty);
			}
			return null;
		}

		// Token: 0x040002FC RID: 764
		private readonly SelfLinkBuilder<Uri> _idLinkBuilder;

		// Token: 0x040002FD RID: 765
		private readonly SelfLinkBuilder<Uri> _editLinkBuilder;

		// Token: 0x040002FE RID: 766
		private readonly SelfLinkBuilder<Uri> _readLinkBuilder;

		// Token: 0x040002FF RID: 767
		private readonly Dictionary<IEdmNavigationProperty, NavigationLinkBuilder> _navigationPropertyLinkBuilderLookup = new Dictionary<IEdmNavigationProperty, NavigationLinkBuilder>();
	}
}
