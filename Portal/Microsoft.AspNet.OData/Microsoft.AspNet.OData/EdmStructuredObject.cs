using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData
{
	// Token: 0x0200003E RID: 62
	[NonValidatingParameterBinding]
	public abstract class EdmStructuredObject : Delta, IEdmStructuredObject, IEdmObject
	{
		// Token: 0x0600017F RID: 383 RVA: 0x000075C0 File Offset: 0x000057C0
		protected EdmStructuredObject(IEdmStructuredType edmType)
			: this(edmType, false)
		{
		}

		// Token: 0x06000180 RID: 384 RVA: 0x000075CA File Offset: 0x000057CA
		protected EdmStructuredObject(IEdmStructuredTypeReference edmType)
			: this(edmType.StructuredDefinition(), edmType.IsNullable)
		{
		}

		// Token: 0x06000181 RID: 385 RVA: 0x000075E0 File Offset: 0x000057E0
		protected EdmStructuredObject(IEdmStructuredType edmType, bool isNullable)
		{
			if (edmType == null)
			{
				throw Error.ArgumentNull("edmType");
			}
			this._expectedEdmType = edmType;
			this._actualEdmType = edmType;
			this.IsNullable = isNullable;
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000182 RID: 386 RVA: 0x0000762C File Offset: 0x0000582C
		// (set) Token: 0x06000183 RID: 387 RVA: 0x00007634 File Offset: 0x00005834
		public IEdmStructuredType ExpectedEdmType
		{
			get
			{
				return this._expectedEdmType;
			}
			set
			{
				if (value == null)
				{
					throw Error.PropertyNull();
				}
				if (!this._actualEdmType.IsOrInheritsFrom(value))
				{
					throw Error.InvalidOperation(SRResources.DeltaEntityTypeNotAssignable, new object[]
					{
						this._actualEdmType.ToTraceString(),
						value.ToTraceString()
					});
				}
				this._expectedEdmType = value;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000184 RID: 388 RVA: 0x00007687 File Offset: 0x00005887
		// (set) Token: 0x06000185 RID: 389 RVA: 0x00007690 File Offset: 0x00005890
		public IEdmStructuredType ActualEdmType
		{
			get
			{
				return this._actualEdmType;
			}
			set
			{
				if (value == null)
				{
					throw Error.PropertyNull();
				}
				if (!value.IsOrInheritsFrom(this._expectedEdmType))
				{
					throw Error.InvalidOperation(SRResources.DeltaEntityTypeNotAssignable, new object[]
					{
						value.ToTraceString(),
						this._expectedEdmType.ToTraceString()
					});
				}
				this._actualEdmType = value;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000186 RID: 390 RVA: 0x000076E3 File Offset: 0x000058E3
		// (set) Token: 0x06000187 RID: 391 RVA: 0x000076EB File Offset: 0x000058EB
		public bool IsNullable { get; set; }

		// Token: 0x06000188 RID: 392 RVA: 0x000076F4 File Offset: 0x000058F4
		public override void Clear()
		{
			this._container.Clear();
			this._setProperties.Clear();
		}

		// Token: 0x06000189 RID: 393 RVA: 0x0000770C File Offset: 0x0000590C
		public override bool TrySetPropertyValue(string name, object value)
		{
			if (this._actualEdmType.FindProperty(name) != null || this._actualEdmType.IsOpen)
			{
				this._setProperties.Add(name);
				this._container[name] = value;
				return true;
			}
			return false;
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00007748 File Offset: 0x00005948
		public override bool TryGetPropertyValue(string name, out object value)
		{
			IEdmProperty edmProperty = this._actualEdmType.FindProperty(name);
			if (edmProperty == null && !this._actualEdmType.IsOpen)
			{
				value = null;
				return false;
			}
			if (this._container.ContainsKey(name))
			{
				value = this._container[name];
				return true;
			}
			value = EdmStructuredObject.GetDefaultValue(edmProperty.Type);
			this._container[name] = value;
			return true;
		}

		// Token: 0x0600018B RID: 395 RVA: 0x000077B4 File Offset: 0x000059B4
		public override bool TryGetPropertyType(string name, out Type type)
		{
			IEdmProperty edmProperty = this._actualEdmType.FindProperty(name);
			if (edmProperty != null)
			{
				type = EdmStructuredObject.GetClrTypeForUntypedDelta(edmProperty.Type);
				return true;
			}
			if (this._actualEdmType.IsOpen && this._container.ContainsKey(name))
			{
				type = this._container[name].GetType();
				return true;
			}
			type = null;
			return false;
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00007814 File Offset: 0x00005A14
		public Dictionary<string, object> TryGetDynamicProperties()
		{
			if (!this._actualEdmType.IsOpen)
			{
				return new Dictionary<string, object>();
			}
			return this._container.Where((KeyValuePair<string, object> p) => this._actualEdmType.FindProperty(p.Key) == null).ToDictionary((KeyValuePair<string, object> property) => property.Key, (KeyValuePair<string, object> property) => property.Value);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x0000788E File Offset: 0x00005A8E
		public override IEnumerable<string> GetChangedPropertyNames()
		{
			return this._setProperties;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00007896 File Offset: 0x00005A96
		public override IEnumerable<string> GetUnchangedPropertyNames()
		{
			return (from p in this._actualEdmType.Properties()
				select p.Name).Except(this.GetChangedPropertyNames());
		}

		// Token: 0x0600018F RID: 399 RVA: 0x000078D2 File Offset: 0x00005AD2
		public IEdmTypeReference GetEdmType()
		{
			return this._actualEdmType.ToEdmTypeReference(this.IsNullable);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x000078E8 File Offset: 0x00005AE8
		internal static object GetDefaultValue(IEdmTypeReference propertyType)
		{
			bool flag = propertyType.IsCollection();
			if (propertyType.IsNullable && !flag)
			{
				return null;
			}
			Type clrTypeForUntypedDelta = EdmStructuredObject.GetClrTypeForUntypedDelta(propertyType);
			if (propertyType.IsPrimitive() || (flag && propertyType.AsCollection().ElementType().IsPrimitive()))
			{
				return Activator.CreateInstance(clrTypeForUntypedDelta);
			}
			return Activator.CreateInstance(clrTypeForUntypedDelta, new object[] { propertyType });
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00007948 File Offset: 0x00005B48
		internal static Type GetClrTypeForUntypedDelta(IEdmTypeReference edmType)
		{
			switch (edmType.TypeKind())
			{
			case EdmTypeKind.Primitive:
				return EdmLibHelpers.GetClrType(edmType.AsPrimitive(), EdmCoreModel.Instance);
			case EdmTypeKind.Entity:
				return typeof(EdmEntityObject);
			case EdmTypeKind.Complex:
				return typeof(EdmComplexObject);
			case EdmTypeKind.Collection:
			{
				IEdmTypeReference edmTypeReference = edmType.AsCollection().ElementType();
				if (edmTypeReference.IsPrimitive())
				{
					Type clrTypeForUntypedDelta = EdmStructuredObject.GetClrTypeForUntypedDelta(edmTypeReference);
					return typeof(List<>).MakeGenericType(new Type[] { clrTypeForUntypedDelta });
				}
				if (edmTypeReference.IsComplex())
				{
					return typeof(EdmComplexObjectCollection);
				}
				if (edmTypeReference.IsEntity())
				{
					return typeof(EdmEntityObjectCollection);
				}
				if (edmTypeReference.IsEnum())
				{
					return typeof(EdmEnumObjectCollection);
				}
				break;
			}
			case EdmTypeKind.Enum:
				return typeof(EdmEnumObject);
			}
			throw Error.InvalidOperation(SRResources.UnsupportedEdmType, new object[]
			{
				edmType.ToTraceString(),
				edmType.TypeKind()
			});
		}

		// Token: 0x04000066 RID: 102
		private Dictionary<string, object> _container = new Dictionary<string, object>();

		// Token: 0x04000067 RID: 103
		private HashSet<string> _setProperties = new HashSet<string>();

		// Token: 0x04000068 RID: 104
		private IEdmStructuredType _expectedEdmType;

		// Token: 0x04000069 RID: 105
		private IEdmStructuredType _actualEdmType;
	}
}
