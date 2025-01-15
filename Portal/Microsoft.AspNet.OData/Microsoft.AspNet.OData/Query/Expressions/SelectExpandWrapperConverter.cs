using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData.Edm;
using Newtonsoft.Json;

namespace Microsoft.AspNet.OData.Query.Expressions
{
	// Token: 0x020000F5 RID: 245
	internal class SelectExpandWrapperConverter : JsonConverter
	{
		// Token: 0x06000856 RID: 2134 RVA: 0x000208C5 File Offset: 0x0001EAC5
		public override bool CanConvert(Type objectType)
		{
			if (objectType == null)
			{
				throw Error.ArgumentNull("objectType");
			}
			return objectType.IsAssignableFrom(typeof(ISelectExpandWrapper));
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x0001ACD5 File Offset: 0x00018ED5
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x000208EC File Offset: 0x0001EAEC
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			ISelectExpandWrapper selectExpandWrapper = value as ISelectExpandWrapper;
			if (selectExpandWrapper != null)
			{
				serializer.Serialize(writer, selectExpandWrapper.ToDictionary(SelectExpandWrapperConverter._mapperProvider));
			}
		}

		// Token: 0x0400027A RID: 634
		private static readonly Func<IEdmModel, IEdmStructuredType, IPropertyMapper> _mapperProvider = (IEdmModel model, IEdmStructuredType type) => new SelectExpandWrapperConverter.JsonPropertyNameMapper(model, type);

		// Token: 0x020002BE RID: 702
		private class JsonPropertyNameMapper : IPropertyMapper
		{
			// Token: 0x060012EE RID: 4846 RVA: 0x00042A68 File Offset: 0x00040C68
			public JsonPropertyNameMapper(IEdmModel model, IEdmStructuredType type)
			{
				this._model = model;
				this._type = type;
			}

			// Token: 0x060012EF RID: 4847 RVA: 0x00042A80 File Offset: 0x00040C80
			public string MapProperty(string propertyName)
			{
				IEdmProperty edmProperty = this._type.Properties().Single((IEdmProperty s) => s.Name == propertyName);
				JsonPropertyAttribute jsonProperty = SelectExpandWrapperConverter.JsonPropertyNameMapper.GetJsonProperty(this.GetPropertyInfo(edmProperty));
				if (jsonProperty != null && !string.IsNullOrWhiteSpace(jsonProperty.PropertyName))
				{
					return jsonProperty.PropertyName;
				}
				return edmProperty.Name;
			}

			// Token: 0x060012F0 RID: 4848 RVA: 0x00042AE4 File Offset: 0x00040CE4
			private PropertyInfo GetPropertyInfo(IEdmProperty property)
			{
				ClrPropertyInfoAnnotation annotationValue = this._model.GetAnnotationValue(property);
				if (annotationValue != null)
				{
					return annotationValue.ClrPropertyInfo;
				}
				return this._model.GetAnnotationValue(property.DeclaringType).ClrType.GetProperty(property.Name);
			}

			// Token: 0x060012F1 RID: 4849 RVA: 0x00042B29 File Offset: 0x00040D29
			private static JsonPropertyAttribute GetJsonProperty(PropertyInfo property)
			{
				return property.GetCustomAttributes(typeof(JsonPropertyAttribute), false).OfType<JsonPropertyAttribute>().SingleOrDefault<JsonPropertyAttribute>();
			}

			// Token: 0x040005B9 RID: 1465
			private IEdmModel _model;

			// Token: 0x040005BA RID: 1466
			private IEdmStructuredType _type;
		}
	}
}
