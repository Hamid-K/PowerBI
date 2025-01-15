using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Properties;
using System.Web.Http.Routing;

namespace System.Web.Http
{
	// Token: 0x02000028 RID: 40
	public class HttpRouteCollection : ICollection<IHttpRoute>, IEnumerable<IHttpRoute>, IEnumerable, IDisposable
	{
		// Token: 0x060000DD RID: 221 RVA: 0x00004265 File Offset: 0x00002465
		public HttpRouteCollection()
			: this("/")
		{
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00004274 File Offset: 0x00002474
		public HttpRouteCollection(string virtualPathRoot)
		{
			if (virtualPathRoot == null)
			{
				throw Error.ArgumentNull("virtualPathRoot");
			}
			Uri uri = new Uri(HttpRouteCollection._referenceBaseAddress, virtualPathRoot);
			this._virtualPathRoot = "/" + uri.GetComponents(UriComponents.Path, UriFormat.Unescaped);
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000DF RID: 223 RVA: 0x000042D5 File Offset: 0x000024D5
		public virtual string VirtualPathRoot
		{
			get
			{
				return this._virtualPathRoot;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual int Count
		{
			get
			{
				return this._collection.Count;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00003B5D File Offset: 0x00001D5D
		public virtual bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000029 RID: 41
		public virtual IHttpRoute this[int index]
		{
			get
			{
				return this._collection[index];
			}
		}

		// Token: 0x1700002A RID: 42
		public virtual IHttpRoute this[string name]
		{
			get
			{
				return this._dictionary[name];
			}
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00004308 File Offset: 0x00002508
		public virtual IHttpRouteData GetRouteData(HttpRequestMessage request)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			for (int i = 0; i < this._collection.Count; i++)
			{
				string virtualPathRoot = this.GetVirtualPathRoot(request.GetRequestContext());
				IHttpRouteData routeData = this._collection[i].GetRouteData(virtualPathRoot, request);
				if (routeData != null)
				{
					return routeData;
				}
			}
			return null;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00004360 File Offset: 0x00002560
		public virtual IHttpVirtualPathData GetVirtualPath(HttpRequestMessage request, string name, IDictionary<string, object> values)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			if (name == null)
			{
				throw Error.ArgumentNull("name");
			}
			IHttpRoute httpRoute;
			if (!this._dictionary.TryGetValue(name, out httpRoute))
			{
				throw Error.Argument("name", SRResources.RouteCollection_NameNotFound, new object[] { name });
			}
			IHttpVirtualPathData virtualPath = httpRoute.GetVirtualPath(request, values);
			if (virtualPath == null)
			{
				return null;
			}
			string text = this.GetVirtualPathRoot(request.GetRequestContext());
			if (!text.EndsWith("/", StringComparison.Ordinal))
			{
				text += "/";
			}
			return new HttpVirtualPathData(virtualPath.Route, text + virtualPath.VirtualPath);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000043FF File Offset: 0x000025FF
		private string GetVirtualPathRoot(HttpRequestContext requestContext)
		{
			if (requestContext != null)
			{
				return requestContext.VirtualPathRoot ?? string.Empty;
			}
			return this._virtualPathRoot;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x0000441C File Offset: 0x0000261C
		public IHttpRoute CreateRoute(string routeTemplate, object defaults, object constraints)
		{
			IDictionary<string, object> dictionary = new Dictionary<string, object>();
			return this.CreateRoute(routeTemplate, new HttpRouteValueDictionary(defaults), new HttpRouteValueDictionary(constraints), dictionary, null);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00004444 File Offset: 0x00002644
		public IHttpRoute CreateRoute(string routeTemplate, IDictionary<string, object> defaults, IDictionary<string, object> constraints, IDictionary<string, object> dataTokens)
		{
			return this.CreateRoute(routeTemplate, defaults, constraints, dataTokens, null);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00004454 File Offset: 0x00002654
		public virtual IHttpRoute CreateRoute(string routeTemplate, IDictionary<string, object> defaults, IDictionary<string, object> constraints, IDictionary<string, object> dataTokens, HttpMessageHandler handler)
		{
			HttpRouteValueDictionary httpRouteValueDictionary = new HttpRouteValueDictionary(defaults);
			HttpRouteValueDictionary httpRouteValueDictionary2 = new HttpRouteValueDictionary(constraints);
			HttpRouteValueDictionary httpRouteValueDictionary3 = new HttpRouteValueDictionary(dataTokens);
			foreach (KeyValuePair<string, object> keyValuePair in httpRouteValueDictionary2)
			{
				this.ValidateConstraint(routeTemplate, keyValuePair.Key, keyValuePair.Value);
			}
			return new HttpRoute(routeTemplate, httpRouteValueDictionary, httpRouteValueDictionary2, httpRouteValueDictionary3, handler);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000044D4 File Offset: 0x000026D4
		protected virtual void ValidateConstraint(string routeTemplate, string name, object constraint)
		{
			if (name == null)
			{
				throw Error.ArgumentNull("name");
			}
			if (constraint == null)
			{
				throw Error.ArgumentNull("constraint");
			}
			HttpRoute.ValidateConstraint(routeTemplate, name, constraint);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x000044FA File Offset: 0x000026FA
		void ICollection<IHttpRoute>.Add(IHttpRoute route)
		{
			throw Error.NotSupported(SRResources.Route_AddRemoveWithNoKeyNotSupported, new object[] { typeof(HttpRouteCollection).Name });
		}

		// Token: 0x060000EC RID: 236 RVA: 0x0000451E File Offset: 0x0000271E
		public virtual void Add(string name, IHttpRoute route)
		{
			if (name == null)
			{
				throw Error.ArgumentNull("name");
			}
			if (route == null)
			{
				throw Error.ArgumentNull("route");
			}
			this._dictionary.Add(name, route);
			this._collection.Add(route);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00004555 File Offset: 0x00002755
		public virtual void Clear()
		{
			this._dictionary.Clear();
			this._collection.Clear();
		}

		// Token: 0x060000EE RID: 238 RVA: 0x0000456D File Offset: 0x0000276D
		public virtual bool Contains(IHttpRoute item)
		{
			if (item == null)
			{
				throw Error.ArgumentNull("item");
			}
			return this._collection.Contains(item);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00004589 File Offset: 0x00002789
		public virtual bool ContainsKey(string name)
		{
			if (name == null)
			{
				throw Error.ArgumentNull("name");
			}
			return this._dictionary.ContainsKey(name);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000045A5 File Offset: 0x000027A5
		public virtual void CopyTo(IHttpRoute[] array, int arrayIndex)
		{
			this._collection.CopyTo(array, arrayIndex);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x000045B4 File Offset: 0x000027B4
		public virtual void CopyTo(KeyValuePair<string, IHttpRoute>[] array, int arrayIndex)
		{
			this._dictionary.CopyTo(array, arrayIndex);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000045C4 File Offset: 0x000027C4
		public virtual void Insert(int index, string name, IHttpRoute value)
		{
			if (name == null)
			{
				throw Error.ArgumentNull("name");
			}
			if (value == null)
			{
				throw Error.ArgumentNull("value");
			}
			if (this._collection[index] != null)
			{
				this._dictionary.Add(name, value);
				this._collection.Insert(index, value);
			}
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000044FA File Offset: 0x000026FA
		bool ICollection<IHttpRoute>.Remove(IHttpRoute route)
		{
			throw Error.NotSupported(SRResources.Route_AddRemoveWithNoKeyNotSupported, new object[] { typeof(HttpRouteCollection).Name });
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00004618 File Offset: 0x00002818
		public virtual bool Remove(string name)
		{
			if (name == null)
			{
				throw Error.ArgumentNull("name");
			}
			IHttpRoute httpRoute;
			if (this._dictionary.TryGetValue(name, out httpRoute))
			{
				bool flag = this._dictionary.Remove(name);
				this._collection.Remove(httpRoute);
				return flag;
			}
			return false;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x0000465E File Offset: 0x0000285E
		public virtual IEnumerator<IHttpRoute> GetEnumerator()
		{
			return this._collection.GetEnumerator();
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00004670 File Offset: 0x00002870
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.OnGetEnumerator();
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x0000465E File Offset: 0x0000285E
		protected virtual IEnumerator OnGetEnumerator()
		{
			return this._collection.GetEnumerator();
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00004678 File Offset: 0x00002878
		public virtual bool TryGetValue(string name, out IHttpRoute route)
		{
			return this._dictionary.TryGetValue(name, out route);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00004687 File Offset: 0x00002887
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00004698 File Offset: 0x00002898
		protected virtual void Dispose(bool disposing)
		{
			if (!this._disposed)
			{
				if (disposing)
				{
					HashSet<IDisposable> hashSet = new HashSet<IDisposable>();
					foreach (IHttpRoute httpRoute in this)
					{
						if (httpRoute.Handler != null)
						{
							hashSet.Add(httpRoute.Handler);
						}
					}
					foreach (IDisposable disposable in hashSet)
					{
						disposable.Dispose();
					}
				}
				this._disposed = true;
			}
		}

		// Token: 0x0400002C RID: 44
		private static readonly Uri _referenceBaseAddress = new Uri("http://localhost");

		// Token: 0x0400002D RID: 45
		private readonly string _virtualPathRoot;

		// Token: 0x0400002E RID: 46
		private readonly List<IHttpRoute> _collection = new List<IHttpRoute>();

		// Token: 0x0400002F RID: 47
		private readonly IDictionary<string, IHttpRoute> _dictionary = new Dictionary<string, IHttpRoute>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04000030 RID: 48
		private bool _disposed;
	}
}
