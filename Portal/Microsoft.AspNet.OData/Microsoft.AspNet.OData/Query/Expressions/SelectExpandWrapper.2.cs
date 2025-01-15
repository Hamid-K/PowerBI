using System;
using System.Collections.Generic;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.AspNet.OData.Formatter.Serialization;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData.Query.Expressions
{
	// Token: 0x020000F3 RID: 243
	internal abstract class SelectExpandWrapper : IEdmEntityObject, IEdmStructuredObject, IEdmObject, ISelectExpandWrapper
	{
		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000841 RID: 2113 RVA: 0x00020578 File Offset: 0x0001E778
		// (set) Token: 0x06000842 RID: 2114 RVA: 0x00020580 File Offset: 0x0001E780
		public PropertyContainer Container { get; set; }

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000843 RID: 2115 RVA: 0x00020589 File Offset: 0x0001E789
		// (set) Token: 0x06000844 RID: 2116 RVA: 0x00020591 File Offset: 0x0001E791
		public string ModelID { get; set; }

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000845 RID: 2117 RVA: 0x0002059A File Offset: 0x0001E79A
		// (set) Token: 0x06000846 RID: 2118 RVA: 0x000205A2 File Offset: 0x0001E7A2
		public object UntypedInstance { get; set; }

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000847 RID: 2119 RVA: 0x000205AB File Offset: 0x0001E7AB
		// (set) Token: 0x06000848 RID: 2120 RVA: 0x000205B3 File Offset: 0x0001E7B3
		public string InstanceType { get; set; }

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000849 RID: 2121 RVA: 0x000205BC File Offset: 0x0001E7BC
		// (set) Token: 0x0600084A RID: 2122 RVA: 0x000205C4 File Offset: 0x0001E7C4
		public bool UseInstanceForProperties { get; set; }

		// Token: 0x0600084B RID: 2123 RVA: 0x000205D0 File Offset: 0x0001E7D0
		public IEdmTypeReference GetEdmType()
		{
			IEdmModel model = this.GetModel();
			if (this.InstanceType == null)
			{
				Type elementType = this.GetElementType();
				return model.GetEdmTypeReference(elementType);
			}
			IEdmStructuredType edmStructuredType = model.FindType(this.InstanceType) as IEdmStructuredType;
			IEdmEntityType edmEntityType = edmStructuredType as IEdmEntityType;
			if (edmEntityType != null)
			{
				return edmEntityType.ToEdmTypeReference(true);
			}
			return edmStructuredType.ToEdmTypeReference(true);
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x00020628 File Offset: 0x0001E828
		public bool TryGetPropertyValue(string propertyName, out object value)
		{
			if (this.Container != null)
			{
				this._containerDict = this._containerDict ?? this.Container.ToDictionary(SelectExpandWrapper.DefaultPropertyMapper, true);
				if (this._containerDict.TryGetValue(propertyName, out value))
				{
					return true;
				}
			}
			if (this.UseInstanceForProperties && this.UntypedInstance != null)
			{
				if (this.GetEdmType() is IEdmComplexTypeReference)
				{
					this._typedEdmStructuredObject = this._typedEdmStructuredObject ?? new TypedEdmComplexObject(this.UntypedInstance, this.GetEdmType() as IEdmComplexTypeReference, this.GetModel());
				}
				else
				{
					this._typedEdmStructuredObject = this._typedEdmStructuredObject ?? new TypedEdmEntityObject(this.UntypedInstance, this.GetEdmType() as IEdmEntityTypeReference, this.GetModel());
				}
				return this._typedEdmStructuredObject.TryGetPropertyValue(propertyName, out value);
			}
			value = null;
			return false;
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x000206F8 File Offset: 0x0001E8F8
		public IDictionary<string, object> ToDictionary()
		{
			return this.ToDictionary(SelectExpandWrapper._mapperProvider);
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x00020708 File Offset: 0x0001E908
		public IDictionary<string, object> ToDictionary(Func<IEdmModel, IEdmStructuredType, IPropertyMapper> mapperProvider)
		{
			if (mapperProvider == null)
			{
				throw Error.ArgumentNull("mapperProvider");
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			IEdmStructuredType edmStructuredType = this.GetEdmType().AsStructured().StructuredDefinition();
			IPropertyMapper propertyMapper = mapperProvider(this.GetModel(), edmStructuredType);
			if (propertyMapper == null)
			{
				throw Error.InvalidOperation(SRResources.InvalidPropertyMapper, new object[]
				{
					typeof(IPropertyMapper).FullName,
					edmStructuredType.FullTypeName()
				});
			}
			if (this.Container != null)
			{
				dictionary = this.Container.ToDictionary(propertyMapper, false);
			}
			if (this.UseInstanceForProperties && this.UntypedInstance != null)
			{
				foreach (IEdmStructuralProperty edmStructuralProperty in edmStructuredType.StructuralProperties())
				{
					object obj;
					if (this.TryGetPropertyValue(edmStructuralProperty.Name, out obj))
					{
						string text = propertyMapper.MapProperty(edmStructuralProperty.Name);
						if (string.IsNullOrWhiteSpace(text))
						{
							throw Error.InvalidOperation(SRResources.InvalidPropertyMapping, new object[] { edmStructuralProperty.Name });
						}
						dictionary[text] = obj;
					}
				}
			}
			return dictionary;
		}

		// Token: 0x0600084F RID: 2127
		protected abstract Type GetElementType();

		// Token: 0x06000850 RID: 2128 RVA: 0x0002082C File Offset: 0x0001EA2C
		private IEdmModel GetModel()
		{
			return ModelContainer.GetModel(this.ModelID);
		}

		// Token: 0x0400026F RID: 623
		private static readonly IPropertyMapper DefaultPropertyMapper = new IdentityPropertyMapper();

		// Token: 0x04000270 RID: 624
		private static readonly Func<IEdmModel, IEdmStructuredType, IPropertyMapper> _mapperProvider = (IEdmModel m, IEdmStructuredType t) => SelectExpandWrapper.DefaultPropertyMapper;

		// Token: 0x04000271 RID: 625
		private Dictionary<string, object> _containerDict;

		// Token: 0x04000272 RID: 626
		private TypedEdmStructuredObject _typedEdmStructuredObject;
	}
}
