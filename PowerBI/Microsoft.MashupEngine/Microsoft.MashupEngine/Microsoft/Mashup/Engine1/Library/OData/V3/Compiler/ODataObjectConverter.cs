using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Edm;
using Microsoft.Data.OData;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.OData.V3.Compiler
{
	// Token: 0x020008F5 RID: 2293
	public static class ODataObjectConverter
	{
		// Token: 0x0600416B RID: 16747 RVA: 0x000DBA80 File Offset: 0x000D9C80
		internal static ODataCollectionValue GetODataCollectionValue(IEngineHost engineHost, ListValue listValue, IEdmCollectionType edmCollectionType, IResource resource)
		{
			ODataCollectionValue odataCollectionValue = new ODataCollectionValue();
			odataCollectionValue.TypeName = ODataTypeServices.GetEdmCollectionFullTypeName(edmCollectionType);
			List<object> list = new List<object>();
			odataCollectionValue.Items = list;
			foreach (IValueReference valueReference in listValue)
			{
				Value value = valueReference.Value;
				object obj;
				if (edmCollectionType.ElementType.TypeKind() == EdmTypeKind.Complex)
				{
					if (!value.IsRecord)
					{
						throw ValueException.NewExpressionError<Message1>(Strings.ODataFunctionParameterRecordTypeMismatch(value.Type.Kind), value, null);
					}
					IEdmComplexType edmComplexType = (IEdmComplexType)edmCollectionType.ElementType.Definition;
					obj = ODataObjectConverter.GetODataComplexValue(engineHost, value.AsRecord, edmComplexType, resource);
				}
				else
				{
					try
					{
						obj = ValueMarshaller.MarshalToClr(value);
					}
					catch (ValueException ex)
					{
						throw ValueException.NewExpressionError<Message1>(Strings.ODataFunctionParameterConversionNotSupported(value.Type.Kind), value, ex);
					}
				}
				list.Add(obj);
			}
			return odataCollectionValue;
		}

		// Token: 0x0600416C RID: 16748 RVA: 0x000DBB88 File Offset: 0x000D9D88
		internal static ODataComplexValue GetODataComplexValue(IEngineHost engineHost, RecordValue recordValue, IEdmComplexType edmComplexType, IResource resource)
		{
			ODataComplexValue odataComplexValue = new ODataComplexValue();
			List<ODataProperty> list = new List<ODataProperty>();
			odataComplexValue.Properties = list;
			odataComplexValue.TypeName = edmComplexType.FullName();
			List<IEdmProperty> list2 = edmComplexType.Properties().ToList<IEdmProperty>();
			foreach (NamedValue namedValue in recordValue.GetFields())
			{
				ODataProperty odataProperty = new ODataProperty
				{
					Name = namedValue.Key
				};
				Value value = namedValue.Value;
				IEdmProperty edmProperty = edmComplexType.FindProperty(namedValue.Key);
				if (edmProperty != null)
				{
					EdmTypeKind edmTypeKind = edmProperty.Type.TypeKind();
					list2.Remove(edmProperty);
					if (edmTypeKind == EdmTypeKind.Complex)
					{
						if (!value.IsRecord)
						{
							throw DataSourceException.NewDataSourceError<Message1>(engineHost, Strings.ODataFunctionParameterRecordTypeMismatch(value.Type.TypeKind), resource, "Property", value, value.Type, null);
						}
						odataProperty.Value = ODataObjectConverter.GetODataComplexValue(engineHost, value.AsRecord, (IEdmComplexType)edmProperty.Type.Definition, resource);
					}
					else if (edmTypeKind == EdmTypeKind.Collection)
					{
						if (!value.IsList)
						{
							throw DataSourceException.NewDataSourceError<Message1>(engineHost, Strings.ODataFunctionParameterCollectionTypeMismatch(value.Type.TypeKind), resource, "Property", value, value.Type, null);
						}
						odataProperty.Value = ODataObjectConverter.GetODataCollectionValue(engineHost, value.AsList, (IEdmCollectionType)edmProperty.Type.Definition, resource);
					}
					else
					{
						try
						{
							odataProperty.Value = ValueMarshaller.MarshalToClr(value);
						}
						catch (ValueException ex)
						{
							throw ValueException.NewExpressionError<Message1>(Strings.ODataFunctionParameterConversionNotSupported(value.Type.Kind), value, ex);
						}
					}
					list.Add(odataProperty);
				}
			}
			string[] array = (from p in list2
				where !p.Type.IsNullable
				select p into p1
				select p1.Name).ToArray<string>();
			if (array.Any<string>())
			{
				throw ValueException.NewExpressionError<Message2>(Strings.ODataFunctionParameterRecordTypeMissingProperties(string.Join(",", array.ToArray<string>()), odataComplexValue.TypeName), recordValue, null);
			}
			return odataComplexValue;
		}
	}
}
