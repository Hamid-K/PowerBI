using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData
{
	// Token: 0x02000051 RID: 81
	public class ODataQueryContext
	{
		// Token: 0x06000220 RID: 544 RVA: 0x0000A414 File Offset: 0x00008614
		public ODataQueryContext(IEdmModel model, Type elementClrType, ODataPath path)
		{
			if (model == null)
			{
				throw Error.ArgumentNull("model");
			}
			if (elementClrType == null)
			{
				throw Error.ArgumentNull("elementClrType");
			}
			this.ElementType = model.GetEdmType(elementClrType);
			if (this.ElementType == null)
			{
				throw Error.Argument("elementClrType", SRResources.ClrTypeNotInModel, new object[] { elementClrType.FullName });
			}
			this.ElementClrType = elementClrType;
			this.Model = model;
			this.Path = path;
			this.NavigationSource = ODataQueryContext.GetNavigationSource(this.Model, this.ElementType, path);
			this.GetPathContext();
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000A4B0 File Offset: 0x000086B0
		public ODataQueryContext(IEdmModel model, IEdmType elementType, ODataPath path)
		{
			if (model == null)
			{
				throw Error.ArgumentNull("model");
			}
			if (elementType == null)
			{
				throw Error.ArgumentNull("elementType");
			}
			this.Model = model;
			this.ElementType = elementType;
			this.Path = path;
			this.NavigationSource = ODataQueryContext.GetNavigationSource(this.Model, this.ElementType, path);
			this.GetPathContext();
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000A512 File Offset: 0x00008712
		internal ODataQueryContext(IEdmModel model, Type elementClrType)
			: this(model, elementClrType, null)
		{
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000A51D File Offset: 0x0000871D
		internal ODataQueryContext(IEdmModel model, IEdmType elementType)
			: this(model, elementType, null)
		{
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000224 RID: 548 RVA: 0x0000A528 File Offset: 0x00008728
		public DefaultQuerySettings DefaultQuerySettings
		{
			get
			{
				if (this._defaultQuerySettings == null)
				{
					this._defaultQuerySettings = ((this.RequestContainer == null) ? new DefaultQuerySettings() : ServiceProviderServiceExtensions.GetRequiredService<DefaultQuerySettings>(this.RequestContainer));
				}
				return this._defaultQuerySettings;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000225 RID: 549 RVA: 0x0000A558 File Offset: 0x00008758
		// (set) Token: 0x06000226 RID: 550 RVA: 0x0000A560 File Offset: 0x00008760
		public IEdmModel Model { get; private set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000227 RID: 551 RVA: 0x0000A569 File Offset: 0x00008769
		// (set) Token: 0x06000228 RID: 552 RVA: 0x0000A571 File Offset: 0x00008771
		public IEdmType ElementType { get; private set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000229 RID: 553 RVA: 0x0000A57A File Offset: 0x0000877A
		// (set) Token: 0x0600022A RID: 554 RVA: 0x0000A582 File Offset: 0x00008782
		public IEdmNavigationSource NavigationSource { get; private set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600022B RID: 555 RVA: 0x0000A58B File Offset: 0x0000878B
		// (set) Token: 0x0600022C RID: 556 RVA: 0x0000A593 File Offset: 0x00008793
		public Type ElementClrType { get; internal set; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600022D RID: 557 RVA: 0x0000A59C File Offset: 0x0000879C
		// (set) Token: 0x0600022E RID: 558 RVA: 0x0000A5A4 File Offset: 0x000087A4
		public ODataPath Path { get; private set; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600022F RID: 559 RVA: 0x0000A5AD File Offset: 0x000087AD
		// (set) Token: 0x06000230 RID: 560 RVA: 0x0000A5B5 File Offset: 0x000087B5
		public IServiceProvider RequestContainer { get; internal set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000231 RID: 561 RVA: 0x0000A5BE File Offset: 0x000087BE
		// (set) Token: 0x06000232 RID: 562 RVA: 0x0000A5C6 File Offset: 0x000087C6
		internal IEdmProperty TargetProperty { get; private set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000233 RID: 563 RVA: 0x0000A5CF File Offset: 0x000087CF
		// (set) Token: 0x06000234 RID: 564 RVA: 0x0000A5D7 File Offset: 0x000087D7
		internal IEdmStructuredType TargetStructuredType { get; private set; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000235 RID: 565 RVA: 0x0000A5E0 File Offset: 0x000087E0
		// (set) Token: 0x06000236 RID: 566 RVA: 0x0000A5E8 File Offset: 0x000087E8
		internal string TargetName { get; private set; }

		// Token: 0x06000237 RID: 567 RVA: 0x0000A5F4 File Offset: 0x000087F4
		private static IEdmNavigationSource GetNavigationSource(IEdmModel model, IEdmType elementType, ODataPath odataPath)
		{
			IEdmNavigationSource edmNavigationSource = ((odataPath != null) ? odataPath.NavigationSource : null);
			if (edmNavigationSource != null)
			{
				return edmNavigationSource;
			}
			IEdmEntityContainer entityContainer = model.EntityContainer;
			if (entityContainer == null)
			{
				return null;
			}
			List<IEdmEntitySet> list = (from e in entityContainer.EntitySets()
				where e.EntityType() == elementType
				select e).ToList<IEdmEntitySet>();
			if (list.Count == 1)
			{
				return list[0];
			}
			return null;
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000A65C File Offset: 0x0000885C
		private void GetPathContext()
		{
			if (this.Path != null)
			{
				IEdmProperty edmProperty;
				IEdmStructuredType edmStructuredType;
				string text;
				EdmLibHelpers.GetPropertyAndStructuredTypeFromPath(this.Path.Segments, out edmProperty, out edmStructuredType, out text);
				this.TargetProperty = edmProperty;
				this.TargetStructuredType = edmStructuredType;
				this.TargetName = text;
				return;
			}
			this.TargetStructuredType = this.ElementType as IEdmStructuredType;
		}

		// Token: 0x040000AC RID: 172
		private DefaultQuerySettings _defaultQuerySettings;
	}
}
