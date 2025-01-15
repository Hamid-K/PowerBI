using System;
using System.Collections.Concurrent;
using System.Reflection;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData.Formatter.Serialization
{
	// Token: 0x0200019F RID: 415
	internal abstract class TypedEdmStructuredObject : IEdmStructuredObject, IEdmObject
	{
		// Token: 0x06000DB1 RID: 3505 RVA: 0x00037054 File Offset: 0x00035254
		protected TypedEdmStructuredObject(object instance, IEdmStructuredTypeReference edmType, IEdmModel edmModel)
		{
			this.Instance = instance;
			this._edmType = edmType;
			this._type = ((instance == null) ? null : instance.GetType());
			this.Model = edmModel;
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06000DB2 RID: 3506 RVA: 0x00037083 File Offset: 0x00035283
		// (set) Token: 0x06000DB3 RID: 3507 RVA: 0x0003708B File Offset: 0x0003528B
		public object Instance { get; private set; }

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06000DB4 RID: 3508 RVA: 0x00037094 File Offset: 0x00035294
		// (set) Token: 0x06000DB5 RID: 3509 RVA: 0x0003709C File Offset: 0x0003529C
		public IEdmModel Model { get; private set; }

		// Token: 0x06000DB6 RID: 3510 RVA: 0x000370A5 File Offset: 0x000352A5
		public IEdmTypeReference GetEdmType()
		{
			return this._edmType;
		}

		// Token: 0x06000DB7 RID: 3511 RVA: 0x000370B0 File Offset: 0x000352B0
		public bool TryGetPropertyValue(string propertyName, out object value)
		{
			if (this.Instance == null)
			{
				value = null;
				return false;
			}
			Func<object, object> orCreatePropertyGetter = TypedEdmStructuredObject.GetOrCreatePropertyGetter(this._type, propertyName, this._edmType, this.Model);
			if (orCreatePropertyGetter == null)
			{
				value = null;
				return false;
			}
			value = orCreatePropertyGetter(this.Instance);
			return true;
		}

		// Token: 0x06000DB8 RID: 3512 RVA: 0x000370FC File Offset: 0x000352FC
		internal static Func<object, object> GetOrCreatePropertyGetter(Type type, string propertyName, IEdmStructuredTypeReference edmType, IEdmModel model)
		{
			Tuple<string, Type> tuple = Tuple.Create<string, Type>(propertyName, type);
			Func<object, object> func;
			if (!TypedEdmStructuredObject._propertyGetterCache.TryGetValue(tuple, out func))
			{
				IEdmProperty edmProperty = edmType.FindProperty(propertyName);
				if (edmProperty != null && model != null)
				{
					propertyName = EdmLibHelpers.GetClrPropertyName(edmProperty, model) ?? propertyName;
				}
				func = TypedEdmStructuredObject.CreatePropertyGetter(type, propertyName);
				TypedEdmStructuredObject._propertyGetterCache[tuple] = func;
			}
			return func;
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x00037154 File Offset: 0x00035354
		private static Func<object, object> CreatePropertyGetter(Type type, string propertyName)
		{
			PropertyInfo property = type.GetProperty(propertyName);
			if (property == null)
			{
				return null;
			}
			return new Func<object, object>(new PropertyHelper(property).GetValue);
		}

		// Token: 0x040003EB RID: 1003
		private static readonly ConcurrentDictionary<Tuple<string, Type>, Func<object, object>> _propertyGetterCache = new ConcurrentDictionary<Tuple<string, Type>, Func<object, object>>();

		// Token: 0x040003EC RID: 1004
		private IEdmStructuredTypeReference _edmType;

		// Token: 0x040003ED RID: 1005
		private Type _type;
	}
}
