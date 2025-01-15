using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web.Http;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.OData.UriParser;
using Microsoft.ReportingServices.Portal.ODataWebApi.Controllers.SegmentHandlers;

namespace Microsoft.ReportingServices.Portal.ODataWebApi.Controllers.Reflection
{
	// Token: 0x02000041 RID: 65
	public abstract class EntitySetReflectionODataController<T> : ReflectionODataController<T> where T : class
	{
		// Token: 0x060002FC RID: 764
		protected abstract IQueryable<T> GetEntitySet(string castName);

		// Token: 0x060002FD RID: 765
		protected abstract T GetEntity(string key, string castName);

		// Token: 0x060002FE RID: 766
		protected abstract bool DeleteEntity(string key);

		// Token: 0x060002FF RID: 767
		protected abstract bool AddEntity(T entity);

		// Token: 0x06000300 RID: 768 RVA: 0x0000C908 File Offset: 0x0000AB08
		protected virtual bool AddEntity(T entity, out T createdEntity)
		{
			createdEntity = entity;
			return this.AddEntity(entity);
		}

		// Token: 0x06000301 RID: 769
		protected abstract bool PutEntity(string key, T entity);

		// Token: 0x06000302 RID: 770
		protected abstract bool PatchEntity(string key, T entity, string[] delta);

		// Token: 0x06000303 RID: 771 RVA: 0x0000C918 File Offset: 0x0000AB18
		protected EntitySetReflectionODataController(ILogger logger)
			: base(logger)
		{
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000C921 File Offset: 0x0000AB21
		internal EntitySetReflectionODataController(ILogger logger, Dictionary<string, ISegmentHandler> handlers)
			: base(logger, handlers)
		{
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000C92C File Offset: 0x0000AB2C
		protected override object GetRoot(Microsoft.AspNet.OData.Routing.ODataPath oDataPath, out int index)
		{
			if (oDataPath.PathTemplate.StartsWith("~/entityset/key/cast"))
			{
				string entitySetName2 = EntitySetReflectionODataController<T>.ParseEntitySetName(oDataPath);
				string key2 = ((KeySegment)oDataPath.Segments[1]).Keys.FirstOrDefault<KeyValuePair<string, object>>().Value.ToString();
				string cast2 = ((TypeSegment)oDataPath.Segments[2]).Identifier;
				base.Logger.Trace(TraceLevel.Verbose, () => string.Format("Getting '{0}({1})/{2}'", entitySetName2, key2, cast2));
				index = 2;
				return this.GetEntity(key2, cast2);
			}
			if (oDataPath.PathTemplate.StartsWith("~/entityset/key"))
			{
				string entitySetName3 = EntitySetReflectionODataController<T>.ParseEntitySetName(oDataPath);
				string key = ((KeySegment)oDataPath.Segments[1]).Keys.FirstOrDefault<KeyValuePair<string, object>>().Value.ToString();
				base.Logger.Trace(TraceLevel.Verbose, () => string.Format("Getting '{0}({1})'", entitySetName3, key));
				index = 2;
				return this.GetEntity(key, null);
			}
			if (oDataPath.PathTemplate.StartsWith("~/entityset/cast"))
			{
				string entitySetName4 = EntitySetReflectionODataController<T>.ParseEntitySetName(oDataPath);
				string cast = ((TypeSegment)oDataPath.Segments[1]).Identifier;
				base.Logger.Trace(TraceLevel.Verbose, () => string.Format("Getting '{0}/{1}'", entitySetName4, cast));
				index = 1;
				return this.GetEntitySet(cast);
			}
			if (oDataPath.PathTemplate.StartsWith("~/entityset"))
			{
				string entitySetName = EntitySetReflectionODataController<T>.ParseEntitySetName(oDataPath);
				base.Logger.Trace(TraceLevel.Verbose, () => string.Format("Getting '{0}'", entitySetName));
				index = 1;
				return this.GetEntitySet(null);
			}
			throw new InvalidOperationException(string.Format("No entityset handler for path '{0}'", oDataPath.PathTemplate));
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000CB24 File Offset: 0x0000AD24
		protected static string ParseCastIfExists(Microsoft.AspNet.OData.Routing.ODataPath oDataPath)
		{
			for (int i = 1; i < oDataPath.Segments.Count; i++)
			{
				ODataPathSegment odataPathSegment = oDataPath.Segments[i];
				if (odataPathSegment is TypeSegment)
				{
					return ((TypeSegment)odataPathSegment).Identifier;
				}
			}
			return null;
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000CB6C File Offset: 0x0000AD6C
		protected static string ParseKeyIfExists(Microsoft.AspNet.OData.Routing.ODataPath oDataPath)
		{
			if (oDataPath.Segments.Count < 2)
			{
				return null;
			}
			return ((KeySegment)oDataPath.Segments[1]).Keys.FirstOrDefault<KeyValuePair<string, object>>().Value.ToString();
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000CBB1 File Offset: 0x0000ADB1
		protected static string ParseEntitySetName(Microsoft.AspNet.OData.Routing.ODataPath oDataPath)
		{
			if (oDataPath.Segments.Count == 0)
			{
				return null;
			}
			return ((EntitySetSegment)oDataPath.Segments[0]).EntitySet.Name;
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000CBE0 File Offset: 0x0000ADE0
		public IHttpActionResult Post(Microsoft.AspNet.OData.Routing.ODataPath oDataPath, T value)
		{
			base.Logger.Trace(TraceLevel.Verbose, () => string.Format("POST '{0}' ({1})", this.Request.RequestUri, oDataPath.PathTemplate));
			if (!base.ModelState.IsValid || value == null)
			{
				return base.BadRequest(base.GetModelStateValidationErrors());
			}
			T t;
			if (this.AddEntity(value, out t))
			{
				base.Logger.Trace(TraceLevel.Verbose, () => "Created");
				return this.Created<T>(t);
			}
			base.Logger.Trace(TraceLevel.Verbose, () => "Failed to add");
			return this.InternalServerError();
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000CCAC File Offset: 0x0000AEAC
		public IHttpActionResult Put(Microsoft.AspNet.OData.Routing.ODataPath oDataPath, T value)
		{
			base.Logger.Trace(TraceLevel.Verbose, () => string.Format("PUT '{0}' ({1})", this.Request.RequestUri, oDataPath.PathTemplate));
			if (!base.ModelState.IsValid)
			{
				return base.BadRequest(base.GetModelStateValidationErrors());
			}
			if (!(oDataPath.PathTemplate == "~/entityset/key"))
			{
				base.Logger.Trace(TraceLevel.Verbose, () => "Failed to update");
				return this.InternalServerError();
			}
			string entitySetName = ((EntitySetSegment)oDataPath.Segments[0]).EntitySet.Name;
			string key = ((KeySegment)oDataPath.Segments[1]).Keys.FirstOrDefault<KeyValuePair<string, object>>().Value.ToString();
			base.Logger.Trace(TraceLevel.Verbose, () => string.Format("Putting '{0}({1})'", entitySetName, key));
			if (this.PutEntity(key, value))
			{
				base.Logger.Trace(TraceLevel.Verbose, () => "Updated");
				return this.Updated<T>(value);
			}
			base.Logger.Trace(TraceLevel.Verbose, () => "Not Found");
			return base.NotFound();
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000CE38 File Offset: 0x0000B038
		public IHttpActionResult Patch(Microsoft.AspNet.OData.Routing.ODataPath oDataPath, Delta<T> value)
		{
			base.Logger.Trace(TraceLevel.Verbose, () => string.Format("PATCH '{0}' ({1})", this.Request.RequestUri, oDataPath.PathTemplate));
			if (!base.ModelState.IsValid)
			{
				return base.BadRequest(base.GetModelStateValidationErrors());
			}
			if (!(oDataPath.PathTemplate == "~/entityset/key"))
			{
				base.Logger.Trace(TraceLevel.Verbose, () => "Failed to patch");
				return this.InternalServerError();
			}
			string entitySetName = EntitySetReflectionODataController<T>.ParseEntitySetName(oDataPath);
			string key = EntitySetReflectionODataController<T>.ParseKeyIfExists(oDataPath);
			string text = EntitySetReflectionODataController<T>.ParseCastIfExists(oDataPath);
			T entity = this.GetEntity(key, text);
			value.Patch(entity);
			base.Logger.Trace(TraceLevel.Verbose, () => string.Format("Patching '{0}({1})'", entitySetName, key));
			if (this.PatchEntity(key, entity, value.GetChangedPropertyNames().ToArray<string>()))
			{
				base.Logger.Trace(TraceLevel.Verbose, () => "Patched");
				return this.Updated<T>(entity);
			}
			base.Logger.Trace(TraceLevel.Verbose, () => "Not Found");
			return base.NotFound();
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000CFB8 File Offset: 0x0000B1B8
		public IHttpActionResult Delete(Microsoft.AspNet.OData.Routing.ODataPath oDataPath)
		{
			base.Logger.Trace(TraceLevel.Verbose, () => string.Format("DELETE '{0}' ({1})", this.Request.RequestUri, oDataPath.PathTemplate));
			if (!base.ModelState.IsValid)
			{
				return base.BadRequest(base.GetModelStateValidationErrors());
			}
			if (!(oDataPath.PathTemplate == "~/entityset/key"))
			{
				base.Logger.Trace(TraceLevel.Verbose, () => "Failed to delete");
				return this.InternalServerError();
			}
			string entitySetName = EntitySetReflectionODataController<T>.ParseEntitySetName(oDataPath);
			string key = EntitySetReflectionODataController<T>.ParseKeyIfExists(oDataPath);
			base.Logger.Trace(TraceLevel.Verbose, () => string.Format("Putting '{0}({1})'", entitySetName, key));
			if (this.DeleteEntity(key))
			{
				base.Logger.Trace(TraceLevel.Verbose, () => "Deleted");
				return base.StatusCode(HttpStatusCode.NoContent);
			}
			base.Logger.Trace(TraceLevel.Verbose, () => "Not Found");
			return base.NotFound();
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000D110 File Offset: 0x0000B310
		internal int GetUtcOffsetInMinutes()
		{
			int num = 0;
			IEnumerable<string> enumerable;
			if (base.Request != null && base.Request.Headers.TryGetValues("TimeZoneOffset", out enumerable) && !int.TryParse(enumerable.FirstOrDefault<string>(), out num))
			{
				num = 0;
			}
			return num;
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600030E RID: 782 RVA: 0x0000D152 File Offset: 0x0000B352
		internal EntitySetReflectionODataController<T>.EntitySetReflectionODataControllerTestAcessor TestAcessor
		{
			get
			{
				return new EntitySetReflectionODataController<T>.EntitySetReflectionODataControllerTestAcessor(this);
			}
		}

		// Token: 0x02000071 RID: 113
		internal class EntitySetReflectionODataControllerTestAcessor
		{
			// Token: 0x060003AF RID: 943 RVA: 0x00010516 File Offset: 0x0000E716
			internal EntitySetReflectionODataControllerTestAcessor(EntitySetReflectionODataController<T> proxy)
			{
				this._proxyclass = proxy;
			}

			// Token: 0x060003B0 RID: 944 RVA: 0x00010525 File Offset: 0x0000E725
			internal IQueryable<T> GetEntitySet()
			{
				return this._proxyclass.GetEntitySet(null);
			}

			// Token: 0x060003B1 RID: 945 RVA: 0x00010533 File Offset: 0x0000E733
			internal T GetEntity(string key)
			{
				return this._proxyclass.GetEntity(key, null);
			}

			// Token: 0x060003B2 RID: 946 RVA: 0x00010542 File Offset: 0x0000E742
			internal bool DeleteEntity(string key)
			{
				return this._proxyclass.DeleteEntity(key);
			}

			// Token: 0x060003B3 RID: 947 RVA: 0x00010550 File Offset: 0x0000E750
			internal bool AddEntity(T entity)
			{
				return this._proxyclass.AddEntity(entity);
			}

			// Token: 0x060003B4 RID: 948 RVA: 0x0001055E File Offset: 0x0000E75E
			internal bool UpdateEntity(string key, T entity)
			{
				return this._proxyclass.PutEntity(key, entity);
			}

			// Token: 0x060003B5 RID: 949 RVA: 0x0001056D File Offset: 0x0000E76D
			internal bool PatchEntity(string key, T entity, string[] delta)
			{
				return this._proxyclass.PatchEntity(key, entity, delta);
			}

			// Token: 0x0400017A RID: 378
			private readonly EntitySetReflectionODataController<T> _proxyclass;
		}
	}
}
