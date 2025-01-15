using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x02000403 RID: 1027
	internal sealed class DataRecordObjectView : ObjectView<DbDataRecord>, ITypedList
	{
		// Token: 0x06002FC5 RID: 12229 RVA: 0x00096EE0 File Offset: 0x000950E0
		internal DataRecordObjectView(IObjectViewData<DbDataRecord> viewData, object eventDataSource, RowType rowType, Type propertyComponentType)
			: base(viewData, eventDataSource)
		{
			if (!typeof(IDataRecord).IsAssignableFrom(propertyComponentType))
			{
				propertyComponentType = typeof(IDataRecord);
			}
			this._rowType = rowType;
			this._propertyDescriptorsCache = MaterializedDataRecord.CreatePropertyDescriptorCollection(this._rowType, propertyComponentType, true);
		}

		// Token: 0x06002FC6 RID: 12230 RVA: 0x00096F30 File Offset: 0x00095130
		private static PropertyInfo GetTypedIndexer(Type type)
		{
			PropertyInfo propertyInfo = null;
			if (typeof(IList).IsAssignableFrom(type) || typeof(ITypedList).IsAssignableFrom(type) || typeof(IListSource).IsAssignableFrom(type))
			{
				foreach (PropertyInfo propertyInfo2 in from p in type.GetInstanceProperties()
					where p.IsPublic()
					select p)
				{
					if (propertyInfo2.GetIndexParameters().Length != 0 && propertyInfo2.PropertyType != typeof(object))
					{
						propertyInfo = propertyInfo2;
						if (propertyInfo.Name == "Item")
						{
							break;
						}
					}
				}
			}
			return propertyInfo;
		}

		// Token: 0x06002FC7 RID: 12231 RVA: 0x0009700C File Offset: 0x0009520C
		private static Type GetListItemType(Type type)
		{
			Type type2;
			if (typeof(Array).IsAssignableFrom(type))
			{
				type2 = type.GetElementType();
			}
			else
			{
				PropertyInfo typedIndexer = DataRecordObjectView.GetTypedIndexer(type);
				if (typedIndexer != null)
				{
					type2 = typedIndexer.PropertyType;
				}
				else
				{
					type2 = type;
				}
			}
			return type2;
		}

		// Token: 0x06002FC8 RID: 12232 RVA: 0x00097050 File Offset: 0x00095250
		PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors)
		{
			PropertyDescriptorCollection propertyDescriptorCollection;
			if (listAccessors == null || listAccessors.Length == 0)
			{
				propertyDescriptorCollection = this._propertyDescriptorsCache;
			}
			else
			{
				PropertyDescriptor propertyDescriptor = listAccessors[listAccessors.Length - 1];
				FieldDescriptor fieldDescriptor = propertyDescriptor as FieldDescriptor;
				if (fieldDescriptor != null && fieldDescriptor.EdmProperty != null && fieldDescriptor.EdmProperty.TypeUsage.EdmType.BuiltInTypeKind == BuiltInTypeKind.RowType)
				{
					propertyDescriptorCollection = MaterializedDataRecord.CreatePropertyDescriptorCollection((RowType)fieldDescriptor.EdmProperty.TypeUsage.EdmType, typeof(IDataRecord), true);
				}
				else
				{
					propertyDescriptorCollection = TypeDescriptor.GetProperties(DataRecordObjectView.GetListItemType(propertyDescriptor.PropertyType));
				}
			}
			return propertyDescriptorCollection;
		}

		// Token: 0x06002FC9 RID: 12233 RVA: 0x000970DA File Offset: 0x000952DA
		string ITypedList.GetListName(PropertyDescriptor[] listAccessors)
		{
			return this._rowType.Name;
		}

		// Token: 0x04001012 RID: 4114
		private readonly PropertyDescriptorCollection _propertyDescriptorsCache;

		// Token: 0x04001013 RID: 4115
		private readonly RowType _rowType;
	}
}
