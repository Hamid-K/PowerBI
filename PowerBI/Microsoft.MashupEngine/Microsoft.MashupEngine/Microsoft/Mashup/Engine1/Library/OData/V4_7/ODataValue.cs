using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.OData.V4_7.Edm;
using Microsoft.Mashup.Engine1.Library.OData.V4_7.QueryBuilder;
using Microsoft.Mashup.Engine1.Library.OData.V4_7.Reader;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7
{
	// Token: 0x0200077C RID: 1916
	internal static class ODataValue
	{
		// Token: 0x06003869 RID: 14441 RVA: 0x000B5500 File Offset: 0x000B3700
		public static TableValue CreateServiceDocumentValue(ODataEnvironment environment, ODataServiceDocument serviceDocument)
		{
			ListValue listValue = ListValue.New(new ODataServiceDocumentEnumerable(environment, serviceDocument));
			TableTypeValue tableTypeValue = NavigationTableServices.AddDataColumnIsLeafMetadata(NavigationTableServices.GetODataNavigationTableType()).AsTableType;
			if (environment.TypeMetaValue != null)
			{
				RecordValue asRecord = tableTypeValue.MetaValue.Concatenate(environment.TypeMetaValue).AsRecord;
				tableTypeValue = tableTypeValue.NewMeta(asRecord).AsType.AsTableType;
			}
			return new ConnectionTestedTableValue(listValue.ToTable(tableTypeValue));
		}

		// Token: 0x0600386A RID: 14442 RVA: 0x000B5568 File Offset: 0x000B3768
		public static IValueReference CreateResourceSetValueReference(IEnumerable<IValueReference> resourceSetEnumerable, TypeValue resourceSetTypeValue)
		{
			ListValue listValue = ListValue.New(resourceSetEnumerable);
			if (resourceSetTypeValue.IsTableType)
			{
				return listValue.ToTable(resourceSetTypeValue.AsTableType);
			}
			return listValue;
		}

		// Token: 0x0600386B RID: 14443 RVA: 0x000B5594 File Offset: 0x000B3794
		public static IValueReference GetResourceValueReference(ODataEnvironment environment, GetReader getReader, ODataReaderWithResponse reader, TypeValue resourceType, Microsoft.OData.Edm.IEdmNavigationSource navigationSource = null)
		{
			IValueReference valueReference;
			try
			{
				using (ODataReaderEnumerator odataReaderEnumerator = new ODataReaderEnumerator(environment, getReader, reader, resourceType, false, navigationSource))
				{
					valueReference = (odataReaderEnumerator.MoveNext() ? odataReaderEnumerator.Current.Value : Value.Null);
				}
			}
			catch (ODataException ex)
			{
				throw ODataCommonErrors.ODataFailedToParseODataResult(environment.Host, ex, environment.ServiceUri, environment.Resource.Kind);
			}
			catch (ValueException ex2)
			{
				valueReference = new ExceptionValueReference(ex2);
			}
			return valueReference;
		}

		// Token: 0x0600386C RID: 14444 RVA: 0x000B5628 File Offset: 0x000B3828
		public static ListValue CreateCollectionValue(ODataEnvironment environment, IEnumerable<object> collection, TypeValue itemType)
		{
			List<IValueReference> list = new List<IValueReference>();
			foreach (object obj in collection)
			{
				list.Add(ODataStructuralValueConverter.ConvertPropertyComplexCollectionOrPrimitive(obj, itemType, new Func<object, Value>(ODataTypeServices.MarshalFromClr), environment));
			}
			return ListValue.New(list);
		}

		// Token: 0x0600386D RID: 14445 RVA: 0x000B5690 File Offset: 0x000B3890
		public static ValueException CreateErrorValueException(string resourceKind, Uri requestUri, ODataError error)
		{
			return DataSourceException.NewDataSourceError<Message2>(null, DataSourceException.DataSourceMessage("OData", error.Message), Resource.New(resourceKind, requestUri.AbsoluteUri), null, null);
		}

		// Token: 0x0600386E RID: 14446 RVA: 0x000B56B6 File Offset: 0x000B38B6
		public static Value CreatePropertyValue(ODataEnvironment environment, ODataPropertyWrapper property, TypeValue propertyType)
		{
			return ODataStructuralValueConverter.ConvertPropertyComplexCollectionOrPrimitive(property, propertyType, new Func<object, Value>(ODataTypeServices.MarshalFromClr), environment);
		}

		// Token: 0x0600386F RID: 14447 RVA: 0x000B56B6 File Offset: 0x000B38B6
		public static Value CreatePrimitiveValue(ODataEnvironment environment, object raw, TypeValue type)
		{
			return ODataStructuralValueConverter.ConvertPropertyComplexCollectionOrPrimitive(raw, type, new Func<object, Value>(ODataTypeServices.MarshalFromClr), environment);
		}

		// Token: 0x06003870 RID: 14448 RVA: 0x000B56CC File Offset: 0x000B38CC
		public static TableValue CreateNavigationSourceTableValue(ODataEnvironment environment, Uri uri, Microsoft.OData.Edm.IEdmNavigationSource navigationSource, TableTypeValue tableType)
		{
			Capabilities elementCapability = environment.Annotations.GetElementCapability(navigationSource);
			ODataPath odataPath;
			TableValue tableValue;
			if (environment.TryConvertToOpaquePath(uri, out odataPath))
			{
				tableValue = new QueryTableValue(new OptimizableQuery(new ODataQuery(environment, odataPath, navigationSource, navigationSource.EntityType(), tableType.ItemType, elementCapability, true)), tableType);
			}
			else
			{
				tableValue = ODataValue.CreateResourceSetValueReference(new ODataReaderEnumerable(environment, uri, tableType.ItemType, !(navigationSource is Microsoft.OData.Edm.IEdmSingleton), navigationSource, null), tableType).Value.AsTable;
			}
			return BinaryOperator.AddMeta.Invoke(tableValue, RecordValue.New(elementCapability.DisplayAnnotations.ToArray())).AsTable;
		}
	}
}
