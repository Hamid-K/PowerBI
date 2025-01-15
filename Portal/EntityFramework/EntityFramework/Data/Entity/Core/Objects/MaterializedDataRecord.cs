using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Globalization;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x02000410 RID: 1040
	internal sealed class MaterializedDataRecord : DbDataRecord, IExtendedDataRecord, IDataRecord, ICustomTypeDescriptor
	{
		// Token: 0x0600312C RID: 12588 RVA: 0x0009C9DC File Offset: 0x0009ABDC
		internal MaterializedDataRecord(MetadataWorkspace workspace, TypeUsage edmUsage, object[] values)
		{
			this._workspace = workspace;
			this._edmUsage = edmUsage;
			this._values = values;
		}

		// Token: 0x17000989 RID: 2441
		// (get) Token: 0x0600312D RID: 12589 RVA: 0x0009C9FC File Offset: 0x0009ABFC
		public DataRecordInfo DataRecordInfo
		{
			get
			{
				if (this._recordInfo == null)
				{
					if (this._workspace == null)
					{
						this._recordInfo = new DataRecordInfo(this._edmUsage);
					}
					else
					{
						this._recordInfo = new DataRecordInfo(this._workspace.GetOSpaceTypeUsage(this._edmUsage));
					}
				}
				return this._recordInfo;
			}
		}

		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x0600312E RID: 12590 RVA: 0x0009CA4E File Offset: 0x0009AC4E
		public override int FieldCount
		{
			get
			{
				return this._values.Length;
			}
		}

		// Token: 0x1700098B RID: 2443
		public override object this[int ordinal]
		{
			get
			{
				return this.GetValue(ordinal);
			}
		}

		// Token: 0x1700098C RID: 2444
		public override object this[string name]
		{
			get
			{
				return this.GetValue(this.GetOrdinal(name));
			}
		}

		// Token: 0x06003131 RID: 12593 RVA: 0x0009CA70 File Offset: 0x0009AC70
		public override bool GetBoolean(int ordinal)
		{
			return (bool)this._values[ordinal];
		}

		// Token: 0x06003132 RID: 12594 RVA: 0x0009CA7F File Offset: 0x0009AC7F
		public override byte GetByte(int ordinal)
		{
			return (byte)this._values[ordinal];
		}

		// Token: 0x06003133 RID: 12595 RVA: 0x0009CA90 File Offset: 0x0009AC90
		public override long GetBytes(int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			int num = 0;
			byte[] array = (byte[])this._values[ordinal];
			num = array.Length;
			if (fieldOffset > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("fieldOffset", Strings.ADP_InvalidSourceBufferIndex(num.ToString(CultureInfo.InvariantCulture), fieldOffset.ToString(CultureInfo.InvariantCulture)));
			}
			int num2 = (int)fieldOffset;
			if (buffer == null)
			{
				return (long)num;
			}
			try
			{
				if (num2 < num)
				{
					if (num2 + length > num)
					{
						num -= num2;
					}
					else
					{
						num = length;
					}
				}
				Array.Copy(array, num2, buffer, bufferOffset, num);
			}
			catch (Exception ex)
			{
				if (ex.IsCatchableExceptionType())
				{
					num = array.Length;
					if (length < 0)
					{
						throw new IndexOutOfRangeException(Strings.ADP_InvalidDataLength(((long)length).ToString(CultureInfo.InvariantCulture)));
					}
					if (bufferOffset < 0 || bufferOffset >= buffer.Length)
					{
						throw new ArgumentOutOfRangeException("bufferOffset", Strings.ADP_InvalidDestinationBufferIndex(length.ToString(CultureInfo.InvariantCulture), bufferOffset.ToString(CultureInfo.InvariantCulture)));
					}
					if (fieldOffset < 0L || fieldOffset >= (long)num)
					{
						throw new ArgumentOutOfRangeException("fieldOffset", Strings.ADP_InvalidSourceBufferIndex(length.ToString(CultureInfo.InvariantCulture), fieldOffset.ToString(CultureInfo.InvariantCulture)));
					}
					if (num + bufferOffset > buffer.Length)
					{
						throw new IndexOutOfRangeException(Strings.ADP_InvalidBufferSizeOrIndex(num.ToString(CultureInfo.InvariantCulture), bufferOffset.ToString(CultureInfo.InvariantCulture)));
					}
				}
				throw;
			}
			return (long)num;
		}

		// Token: 0x06003134 RID: 12596 RVA: 0x0009CBE4 File Offset: 0x0009ADE4
		public override char GetChar(int ordinal)
		{
			return ((string)this.GetValue(ordinal))[0];
		}

		// Token: 0x06003135 RID: 12597 RVA: 0x0009CBF8 File Offset: 0x0009ADF8
		public override long GetChars(int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			int num = 0;
			string text = (string)this._values[ordinal];
			num = text.Length;
			if (fieldOffset > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("fieldOffset", Strings.ADP_InvalidSourceBufferIndex(num.ToString(CultureInfo.InvariantCulture), fieldOffset.ToString(CultureInfo.InvariantCulture)));
			}
			int num2 = (int)fieldOffset;
			if (buffer == null)
			{
				return (long)num;
			}
			try
			{
				if (num2 < num)
				{
					if (num2 + length > num)
					{
						num -= num2;
					}
					else
					{
						num = length;
					}
				}
				text.CopyTo(num2, buffer, bufferOffset, num);
			}
			catch (Exception ex)
			{
				if (ex.IsCatchableExceptionType())
				{
					num = text.Length;
					if (length < 0)
					{
						throw new IndexOutOfRangeException(Strings.ADP_InvalidDataLength(((long)length).ToString(CultureInfo.InvariantCulture)));
					}
					if (bufferOffset < 0 || bufferOffset >= buffer.Length)
					{
						throw new ArgumentOutOfRangeException("bufferOffset", Strings.ADP_InvalidDestinationBufferIndex(buffer.Length.ToString(CultureInfo.InvariantCulture), bufferOffset.ToString(CultureInfo.InvariantCulture)));
					}
					if (fieldOffset < 0L || fieldOffset >= (long)num)
					{
						throw new ArgumentOutOfRangeException("fieldOffset", Strings.ADP_InvalidSourceBufferIndex(num.ToString(CultureInfo.InvariantCulture), fieldOffset.ToString(CultureInfo.InvariantCulture)));
					}
					if (num + bufferOffset > buffer.Length)
					{
						throw new IndexOutOfRangeException(Strings.ADP_InvalidBufferSizeOrIndex(num.ToString(CultureInfo.InvariantCulture), bufferOffset.ToString(CultureInfo.InvariantCulture)));
					}
				}
				throw;
			}
			return (long)num;
		}

		// Token: 0x06003136 RID: 12598 RVA: 0x0009CD58 File Offset: 0x0009AF58
		public DbDataRecord GetDataRecord(int ordinal)
		{
			return (DbDataRecord)this._values[ordinal];
		}

		// Token: 0x06003137 RID: 12599 RVA: 0x0009CD67 File Offset: 0x0009AF67
		public DbDataReader GetDataReader(int i)
		{
			return this.GetDbDataReader(i);
		}

		// Token: 0x06003138 RID: 12600 RVA: 0x0009CD70 File Offset: 0x0009AF70
		public override string GetDataTypeName(int ordinal)
		{
			return this.GetMember(ordinal).TypeUsage.EdmType.Name;
		}

		// Token: 0x06003139 RID: 12601 RVA: 0x0009CD88 File Offset: 0x0009AF88
		public override DateTime GetDateTime(int ordinal)
		{
			return (DateTime)this._values[ordinal];
		}

		// Token: 0x0600313A RID: 12602 RVA: 0x0009CD97 File Offset: 0x0009AF97
		public override decimal GetDecimal(int ordinal)
		{
			return (decimal)this._values[ordinal];
		}

		// Token: 0x0600313B RID: 12603 RVA: 0x0009CDA6 File Offset: 0x0009AFA6
		public override double GetDouble(int ordinal)
		{
			return (double)this._values[ordinal];
		}

		// Token: 0x0600313C RID: 12604 RVA: 0x0009CDB5 File Offset: 0x0009AFB5
		public override Type GetFieldType(int ordinal)
		{
			return this.GetMember(ordinal).TypeUsage.EdmType.ClrType ?? typeof(object);
		}

		// Token: 0x0600313D RID: 12605 RVA: 0x0009CDDB File Offset: 0x0009AFDB
		public override float GetFloat(int ordinal)
		{
			return (float)this._values[ordinal];
		}

		// Token: 0x0600313E RID: 12606 RVA: 0x0009CDEA File Offset: 0x0009AFEA
		public override Guid GetGuid(int ordinal)
		{
			return (Guid)this._values[ordinal];
		}

		// Token: 0x0600313F RID: 12607 RVA: 0x0009CDF9 File Offset: 0x0009AFF9
		public override short GetInt16(int ordinal)
		{
			return (short)this._values[ordinal];
		}

		// Token: 0x06003140 RID: 12608 RVA: 0x0009CE08 File Offset: 0x0009B008
		public override int GetInt32(int ordinal)
		{
			return (int)this._values[ordinal];
		}

		// Token: 0x06003141 RID: 12609 RVA: 0x0009CE17 File Offset: 0x0009B017
		public override long GetInt64(int ordinal)
		{
			return (long)this._values[ordinal];
		}

		// Token: 0x06003142 RID: 12610 RVA: 0x0009CE26 File Offset: 0x0009B026
		public override string GetName(int ordinal)
		{
			return this.GetMember(ordinal).Name;
		}

		// Token: 0x06003143 RID: 12611 RVA: 0x0009CE34 File Offset: 0x0009B034
		public override int GetOrdinal(string name)
		{
			if (this._fieldNameLookup == null)
			{
				this._fieldNameLookup = new FieldNameLookup(this);
			}
			return this._fieldNameLookup.GetOrdinal(name);
		}

		// Token: 0x06003144 RID: 12612 RVA: 0x0009CE56 File Offset: 0x0009B056
		public override string GetString(int ordinal)
		{
			return (string)this._values[ordinal];
		}

		// Token: 0x06003145 RID: 12613 RVA: 0x0009CE65 File Offset: 0x0009B065
		public override object GetValue(int ordinal)
		{
			return this._values[ordinal];
		}

		// Token: 0x06003146 RID: 12614 RVA: 0x0009CE70 File Offset: 0x0009B070
		public override int GetValues(object[] values)
		{
			Check.NotNull<object[]>(values, "values");
			int num = Math.Min(values.Length, this.FieldCount);
			for (int i = 0; i < num; i++)
			{
				values[i] = this._values[i];
			}
			return num;
		}

		// Token: 0x06003147 RID: 12615 RVA: 0x0009CEB0 File Offset: 0x0009B0B0
		private EdmMember GetMember(int ordinal)
		{
			return this.DataRecordInfo.FieldMetadata[ordinal].FieldType;
		}

		// Token: 0x06003148 RID: 12616 RVA: 0x0009CED6 File Offset: 0x0009B0D6
		public override bool IsDBNull(int ordinal)
		{
			return DBNull.Value == this._values[ordinal];
		}

		// Token: 0x06003149 RID: 12617 RVA: 0x0009CEE7 File Offset: 0x0009B0E7
		AttributeCollection ICustomTypeDescriptor.GetAttributes()
		{
			return TypeDescriptor.GetAttributes(this, true);
		}

		// Token: 0x0600314A RID: 12618 RVA: 0x0009CEF0 File Offset: 0x0009B0F0
		string ICustomTypeDescriptor.GetClassName()
		{
			return null;
		}

		// Token: 0x0600314B RID: 12619 RVA: 0x0009CEF3 File Offset: 0x0009B0F3
		string ICustomTypeDescriptor.GetComponentName()
		{
			return null;
		}

		// Token: 0x0600314C RID: 12620 RVA: 0x0009CEF8 File Offset: 0x0009B0F8
		private PropertyDescriptorCollection InitializePropertyDescriptors()
		{
			if (this._values == null)
			{
				return null;
			}
			if (this._propertyDescriptors == null && this._values.Length != 0)
			{
				this._propertyDescriptors = MaterializedDataRecord.CreatePropertyDescriptorCollection(this.DataRecordInfo.RecordType.EdmType as StructuralType, typeof(MaterializedDataRecord), true);
			}
			return this._propertyDescriptors;
		}

		// Token: 0x0600314D RID: 12621 RVA: 0x0009CF54 File Offset: 0x0009B154
		internal static PropertyDescriptorCollection CreatePropertyDescriptorCollection(StructuralType structuralType, Type componentType, bool isReadOnly)
		{
			List<PropertyDescriptor> list = new List<PropertyDescriptor>();
			if (structuralType != null)
			{
				foreach (EdmMember edmMember in structuralType.Members)
				{
					if (edmMember.BuiltInTypeKind == BuiltInTypeKind.EdmProperty)
					{
						EdmProperty edmProperty = (EdmProperty)edmMember;
						FieldDescriptor fieldDescriptor = new FieldDescriptor(componentType, isReadOnly, edmProperty);
						list.Add(fieldDescriptor);
					}
				}
			}
			return new PropertyDescriptorCollection(list.ToArray());
		}

		// Token: 0x0600314E RID: 12622 RVA: 0x0009CFD8 File Offset: 0x0009B1D8
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			return ((ICustomTypeDescriptor)this).GetProperties(null);
		}

		// Token: 0x0600314F RID: 12623 RVA: 0x0009CFE4 File Offset: 0x0009B1E4
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
		{
			bool flag = attributes != null && attributes.Length != 0;
			PropertyDescriptorCollection propertyDescriptorCollection = this.InitializePropertyDescriptors();
			if (propertyDescriptorCollection == null)
			{
				return propertyDescriptorCollection;
			}
			MaterializedDataRecord.FilterCache filterCache = this._filterCache;
			if (flag && filterCache != null && filterCache.IsValid(attributes))
			{
				return filterCache.FilteredProperties;
			}
			if (!flag && propertyDescriptorCollection != null)
			{
				return propertyDescriptorCollection;
			}
			if (this._attrCache == null && attributes != null && attributes.Length != 0)
			{
				this._attrCache = new Dictionary<object, AttributeCollection>();
				foreach (object obj in this._propertyDescriptors)
				{
					FieldDescriptor fieldDescriptor = (FieldDescriptor)obj;
					object[] customAttributes = fieldDescriptor.GetValue(this).GetType().GetCustomAttributes(false);
					Attribute[] array = new Attribute[customAttributes.Length];
					customAttributes.CopyTo(array, 0);
					this._attrCache.Add(fieldDescriptor, new AttributeCollection(array));
				}
			}
			propertyDescriptorCollection = new PropertyDescriptorCollection(null);
			foreach (object obj2 in this._propertyDescriptors)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj2;
				if (this._attrCache[propertyDescriptor].Matches(attributes))
				{
					propertyDescriptorCollection.Add(propertyDescriptor);
				}
			}
			if (flag)
			{
				this._filterCache = new MaterializedDataRecord.FilterCache
				{
					Attributes = attributes,
					FilteredProperties = propertyDescriptorCollection
				};
			}
			return propertyDescriptorCollection;
		}

		// Token: 0x06003150 RID: 12624 RVA: 0x0009D15C File Offset: 0x0009B35C
		object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}

		// Token: 0x04001034 RID: 4148
		private FieldNameLookup _fieldNameLookup;

		// Token: 0x04001035 RID: 4149
		private DataRecordInfo _recordInfo;

		// Token: 0x04001036 RID: 4150
		private readonly MetadataWorkspace _workspace;

		// Token: 0x04001037 RID: 4151
		private readonly TypeUsage _edmUsage;

		// Token: 0x04001038 RID: 4152
		private readonly object[] _values;

		// Token: 0x04001039 RID: 4153
		private PropertyDescriptorCollection _propertyDescriptors;

		// Token: 0x0400103A RID: 4154
		private MaterializedDataRecord.FilterCache _filterCache;

		// Token: 0x0400103B RID: 4155
		private Dictionary<object, AttributeCollection> _attrCache;

		// Token: 0x02000A15 RID: 2581
		private class FilterCache
		{
			// Token: 0x060060DE RID: 24798 RVA: 0x0014CBF0 File Offset: 0x0014ADF0
			public bool IsValid(Attribute[] other)
			{
				if (other == null || this.Attributes == null)
				{
					return false;
				}
				if (this.Attributes.Length != other.Length)
				{
					return false;
				}
				for (int i = 0; i < other.Length; i++)
				{
					if (!this.Attributes[i].Match(other[i]))
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x04002937 RID: 10551
			public Attribute[] Attributes;

			// Token: 0x04002938 RID: 10552
			public PropertyDescriptorCollection FilteredProperties;
		}
	}
}
