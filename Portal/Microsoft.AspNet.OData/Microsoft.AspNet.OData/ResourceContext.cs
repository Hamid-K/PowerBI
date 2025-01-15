using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Routing;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.AspNet.OData.Formatter.Deserialization;
using Microsoft.AspNet.OData.Formatter.Serialization;
using Microsoft.AspNet.OData.Interfaces;
using Microsoft.AspNet.OData.Query.Expressions;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData
{
	// Token: 0x0200000C RID: 12
	public class ResourceContext
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002B90 File Offset: 0x00000D90
		// (set) Token: 0x0600002F RID: 47 RVA: 0x00002B9D File Offset: 0x00000D9D
		public HttpRequestMessage Request
		{
			get
			{
				return this.SerializerContext.Request;
			}
			set
			{
				this.SerializerContext.Request = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002BAB File Offset: 0x00000DAB
		// (set) Token: 0x06000031 RID: 49 RVA: 0x00002BB8 File Offset: 0x00000DB8
		public UrlHelper Url
		{
			get
			{
				return this.SerializerContext.Url;
			}
			set
			{
				this.SerializerContext.Url = value;
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002BC6 File Offset: 0x00000DC6
		public ResourceContext()
		{
			this.SerializerContext = new ODataSerializerContext();
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002BD9 File Offset: 0x00000DD9
		public ResourceContext(ODataSerializerContext serializerContext, IEdmStructuredTypeReference structuredType, object resourceInstance)
			: this(serializerContext, structuredType, ResourceContext.AsEdmResourceObject(resourceInstance, structuredType, serializerContext.Model))
		{
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002BF0 File Offset: 0x00000DF0
		private ResourceContext(ODataSerializerContext serializerContext, IEdmStructuredTypeReference structuredType, IEdmStructuredObject edmObject)
		{
			if (serializerContext == null)
			{
				throw Error.ArgumentNull("serializerContext");
			}
			this.SerializerContext = serializerContext;
			this.StructuredType = structuredType.StructuredDefinition();
			this.EdmObject = edmObject;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002C20 File Offset: 0x00000E20
		// (set) Token: 0x06000036 RID: 54 RVA: 0x00002C28 File Offset: 0x00000E28
		public ODataSerializerContext SerializerContext { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002C31 File Offset: 0x00000E31
		internal IWebApiRequestMessage InternalRequest
		{
			get
			{
				return this.SerializerContext.InternalRequest;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002C3E File Offset: 0x00000E3E
		// (set) Token: 0x06000039 RID: 57 RVA: 0x00002C4B File Offset: 0x00000E4B
		public IEdmModel EdmModel
		{
			get
			{
				return this.SerializerContext.Model;
			}
			set
			{
				this.SerializerContext.Model = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002C59 File Offset: 0x00000E59
		// (set) Token: 0x0600003B RID: 59 RVA: 0x00002C66 File Offset: 0x00000E66
		public IEdmNavigationSource NavigationSource
		{
			get
			{
				return this.SerializerContext.NavigationSource;
			}
			set
			{
				this.SerializerContext.NavigationSource = value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002C74 File Offset: 0x00000E74
		// (set) Token: 0x0600003D RID: 61 RVA: 0x00002C7C File Offset: 0x00000E7C
		public IEdmStructuredType StructuredType { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002C85 File Offset: 0x00000E85
		// (set) Token: 0x0600003F RID: 63 RVA: 0x00002C8D File Offset: 0x00000E8D
		public IEdmStructuredObject EdmObject { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002C96 File Offset: 0x00000E96
		// (set) Token: 0x06000041 RID: 65 RVA: 0x00002CB2 File Offset: 0x00000EB2
		public object ResourceInstance
		{
			get
			{
				if (this._resourceInstance == null)
				{
					this._resourceInstance = this.BuildResourceInstance();
				}
				return this._resourceInstance;
			}
			set
			{
				this._resourceInstance = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002CBB File Offset: 0x00000EBB
		internal IWebApiUrlHelper InternalUrlHelper
		{
			get
			{
				return this.SerializerContext.InternalUrlHelper;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002CC8 File Offset: 0x00000EC8
		// (set) Token: 0x06000044 RID: 68 RVA: 0x00002CD5 File Offset: 0x00000ED5
		public bool SkipExpensiveAvailabilityChecks
		{
			get
			{
				return this.SerializerContext.SkipExpensiveAvailabilityChecks;
			}
			set
			{
				this.SerializerContext.SkipExpensiveAvailabilityChecks = value;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002CE3 File Offset: 0x00000EE3
		// (set) Token: 0x06000046 RID: 70 RVA: 0x00002CEB File Offset: 0x00000EEB
		public IDictionary<string, object> DynamicComplexProperties { get; set; }

		// Token: 0x06000047 RID: 71 RVA: 0x00002CF4 File Offset: 0x00000EF4
		public object GetPropertyValue(string propertyName)
		{
			if (this.EdmObject == null)
			{
				throw Error.InvalidOperation(SRResources.EdmObjectNull, new object[] { typeof(ResourceContext).Name });
			}
			object obj;
			if (this.EdmObject.TryGetPropertyValue(propertyName, out obj))
			{
				return obj;
			}
			IEdmTypeReference edmType = this.EdmObject.GetEdmType();
			if (edmType == null)
			{
				throw Error.InvalidOperation(SRResources.EdmTypeCannotBeNull, new object[]
				{
					this.EdmObject.GetType().FullName,
					typeof(IEdmObject).Name
				});
			}
			throw Error.InvalidOperation(SRResources.PropertyNotFound, new object[]
			{
				edmType.ToTraceString(),
				propertyName
			});
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002DA0 File Offset: 0x00000FA0
		private object BuildResourceInstance()
		{
			if (this.EdmObject == null)
			{
				return null;
			}
			TypedEdmStructuredObject typedEdmStructuredObject = this.EdmObject as TypedEdmStructuredObject;
			if (typedEdmStructuredObject != null)
			{
				return typedEdmStructuredObject.Instance;
			}
			SelectExpandWrapper selectExpandWrapper = this.EdmObject as SelectExpandWrapper;
			if (selectExpandWrapper != null && selectExpandWrapper.UntypedInstance != null)
			{
				return selectExpandWrapper.UntypedInstance;
			}
			Type clrType = EdmLibHelpers.GetClrType(this.StructuredType, this.EdmModel);
			if (clrType == null)
			{
				throw new InvalidOperationException(Error.Format(SRResources.MappingDoesNotContainResourceType, new object[] { this.StructuredType.FullTypeName() }));
			}
			object obj = Activator.CreateInstance(clrType);
			foreach (IEdmStructuralProperty edmStructuralProperty in this.StructuredType.StructuralProperties())
			{
				object obj2;
				if (this.EdmObject.TryGetPropertyValue(edmStructuralProperty.Name, out obj2) && obj2 != null)
				{
					string clrPropertyName = EdmLibHelpers.GetClrPropertyName(edmStructuralProperty, this.EdmModel);
					if (TypeHelper.IsCollection(obj2.GetType()))
					{
						DeserializationHelpers.SetCollectionProperty(obj, edmStructuralProperty, obj2, clrPropertyName);
					}
					else
					{
						DeserializationHelpers.SetProperty(obj, clrPropertyName, obj2);
					}
				}
			}
			return obj;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002EC8 File Offset: 0x000010C8
		private static IEdmStructuredObject AsEdmResourceObject(object resourceInstance, IEdmStructuredTypeReference structuredType, IEdmModel model)
		{
			if (structuredType == null)
			{
				throw Error.ArgumentNull("structuredType");
			}
			IEdmStructuredObject edmStructuredObject = resourceInstance as IEdmStructuredObject;
			if (edmStructuredObject != null)
			{
				return edmStructuredObject;
			}
			if (structuredType.IsEntity())
			{
				return new TypedEdmEntityObject(resourceInstance, structuredType.AsEntity(), model);
			}
			return new TypedEdmComplexObject(resourceInstance, structuredType.AsComplex(), model);
		}

		// Token: 0x04000009 RID: 9
		private object _resourceInstance;
	}
}
