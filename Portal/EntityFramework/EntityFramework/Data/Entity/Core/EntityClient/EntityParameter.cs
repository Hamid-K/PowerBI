using System;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.Internal;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Metadata.Edm.Provider;
using System.Data.Entity.Resources;
using System.Globalization;

namespace System.Data.Entity.Core.EntityClient
{
	// Token: 0x020005DF RID: 1503
	public class EntityParameter : DbParameter, IDbDataParameter, IDataParameter
	{
		// Token: 0x0600494B RID: 18763 RVA: 0x0010441E File Offset: 0x0010261E
		public EntityParameter()
		{
		}

		// Token: 0x0600494C RID: 18764 RVA: 0x00104426 File Offset: 0x00102626
		public EntityParameter(string parameterName, DbType dbType)
		{
			this.SetParameterNameWithValidation(parameterName, "parameterName");
			this.DbType = dbType;
		}

		// Token: 0x0600494D RID: 18765 RVA: 0x00104441 File Offset: 0x00102641
		public EntityParameter(string parameterName, DbType dbType, int size)
		{
			this.SetParameterNameWithValidation(parameterName, "parameterName");
			this.DbType = dbType;
			this.Size = size;
		}

		// Token: 0x0600494E RID: 18766 RVA: 0x00104463 File Offset: 0x00102663
		public EntityParameter(string parameterName, DbType dbType, int size, string sourceColumn)
		{
			this.SetParameterNameWithValidation(parameterName, "parameterName");
			this.DbType = dbType;
			this.Size = size;
			this.SourceColumn = sourceColumn;
		}

		// Token: 0x0600494F RID: 18767 RVA: 0x00104490 File Offset: 0x00102690
		public EntityParameter(string parameterName, DbType dbType, int size, ParameterDirection direction, bool isNullable, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, object value)
		{
			this.SetParameterNameWithValidation(parameterName, "parameterName");
			this.DbType = dbType;
			this.Size = size;
			this.Direction = direction;
			this.IsNullable = isNullable;
			this.Precision = precision;
			this.Scale = scale;
			this.SourceColumn = sourceColumn;
			this.SourceVersion = sourceVersion;
			this.Value = value;
		}

		// Token: 0x06004950 RID: 18768 RVA: 0x001044F8 File Offset: 0x001026F8
		private EntityParameter(EntityParameter source)
			: this()
		{
			source.CloneHelper(this);
			ICloneable cloneable = this._value as ICloneable;
			if (cloneable != null)
			{
				this._value = cloneable.Clone();
			}
		}

		// Token: 0x17000E73 RID: 3699
		// (get) Token: 0x06004951 RID: 18769 RVA: 0x0010452D File Offset: 0x0010272D
		// (set) Token: 0x06004952 RID: 18770 RVA: 0x0010453E File Offset: 0x0010273E
		public override string ParameterName
		{
			get
			{
				return this._parameterName ?? "";
			}
			set
			{
				this.SetParameterNameWithValidation(value, "value");
			}
		}

		// Token: 0x06004953 RID: 18771 RVA: 0x0010454C File Offset: 0x0010274C
		private void SetParameterNameWithValidation(string parameterName, string argumentName)
		{
			if (!string.IsNullOrEmpty(parameterName) && !DbCommandTree.IsValidParameterName(parameterName))
			{
				throw new ArgumentException(Strings.EntityClient_InvalidParameterName(parameterName), argumentName);
			}
			this.PropertyChanging();
			this._parameterName = parameterName;
		}

		// Token: 0x17000E74 RID: 3700
		// (get) Token: 0x06004954 RID: 18772 RVA: 0x00104578 File Offset: 0x00102778
		// (set) Token: 0x06004955 RID: 18773 RVA: 0x001045F0 File Offset: 0x001027F0
		public override DbType DbType
		{
			get
			{
				if (this._dbType == null)
				{
					if (this._edmType != null)
					{
						return EntityParameter.GetDbTypeFromEdm(this._edmType);
					}
					if (this._value == null)
					{
						return DbType.String;
					}
					try
					{
						return TypeHelpers.ConvertClrTypeToDbType(this._value.GetType());
					}
					catch (ArgumentException ex)
					{
						throw new InvalidOperationException(Strings.EntityClient_CannotDeduceDbType, ex);
					}
				}
				return this._dbType.Value;
			}
			set
			{
				this.PropertyChanging();
				this._dbType = new DbType?(value);
			}
		}

		// Token: 0x17000E75 RID: 3701
		// (get) Token: 0x06004956 RID: 18774 RVA: 0x00104604 File Offset: 0x00102804
		// (set) Token: 0x06004957 RID: 18775 RVA: 0x0010460C File Offset: 0x0010280C
		public virtual EdmType EdmType
		{
			get
			{
				return this._edmType;
			}
			set
			{
				if (value != null && !Helper.IsScalarType(value))
				{
					throw new InvalidOperationException(Strings.EntityClient_EntityParameterEdmTypeNotScalar(value.FullName));
				}
				this.PropertyChanging();
				this._edmType = value;
			}
		}

		// Token: 0x17000E76 RID: 3702
		// (get) Token: 0x06004958 RID: 18776 RVA: 0x00104637 File Offset: 0x00102837
		// (set) Token: 0x06004959 RID: 18777 RVA: 0x00104653 File Offset: 0x00102853
		public new virtual byte Precision
		{
			get
			{
				if (this._precision == null)
				{
					return 0;
				}
				return this._precision.Value;
			}
			set
			{
				this.PropertyChanging();
				this._precision = new byte?(value);
			}
		}

		// Token: 0x17000E77 RID: 3703
		// (get) Token: 0x0600495A RID: 18778 RVA: 0x00104667 File Offset: 0x00102867
		// (set) Token: 0x0600495B RID: 18779 RVA: 0x00104683 File Offset: 0x00102883
		public new virtual byte Scale
		{
			get
			{
				if (this._scale == null)
				{
					return 0;
				}
				return this._scale.Value;
			}
			set
			{
				this.PropertyChanging();
				this._scale = new byte?(value);
			}
		}

		// Token: 0x17000E78 RID: 3704
		// (get) Token: 0x0600495C RID: 18780 RVA: 0x00104697 File Offset: 0x00102897
		// (set) Token: 0x0600495D RID: 18781 RVA: 0x001046A0 File Offset: 0x001028A0
		public override object Value
		{
			get
			{
				return this._value;
			}
			set
			{
				if (this._dbType == null && this._edmType == null)
				{
					DbType dbType = DbType.String;
					if (this._value != null)
					{
						dbType = TypeHelpers.ConvertClrTypeToDbType(this._value.GetType());
					}
					DbType dbType2 = DbType.String;
					if (value != null)
					{
						dbType2 = TypeHelpers.ConvertClrTypeToDbType(value.GetType());
					}
					if (dbType != dbType2)
					{
						this.PropertyChanging();
					}
				}
				this._value = value;
			}
		}

		// Token: 0x17000E79 RID: 3705
		// (get) Token: 0x0600495E RID: 18782 RVA: 0x00104701 File Offset: 0x00102901
		internal virtual bool IsDirty
		{
			get
			{
				return this._isDirty;
			}
		}

		// Token: 0x17000E7A RID: 3706
		// (get) Token: 0x0600495F RID: 18783 RVA: 0x00104709 File Offset: 0x00102909
		internal virtual bool IsDbTypeSpecified
		{
			get
			{
				return this._dbType != null;
			}
		}

		// Token: 0x17000E7B RID: 3707
		// (get) Token: 0x06004960 RID: 18784 RVA: 0x00104716 File Offset: 0x00102916
		internal virtual bool IsDirectionSpecified
		{
			get
			{
				return this._direction > (ParameterDirection)0;
			}
		}

		// Token: 0x17000E7C RID: 3708
		// (get) Token: 0x06004961 RID: 18785 RVA: 0x00104721 File Offset: 0x00102921
		internal virtual bool IsIsNullableSpecified
		{
			get
			{
				return this._isNullable != null;
			}
		}

		// Token: 0x17000E7D RID: 3709
		// (get) Token: 0x06004962 RID: 18786 RVA: 0x0010472E File Offset: 0x0010292E
		internal virtual bool IsPrecisionSpecified
		{
			get
			{
				return this._precision != null;
			}
		}

		// Token: 0x17000E7E RID: 3710
		// (get) Token: 0x06004963 RID: 18787 RVA: 0x0010473B File Offset: 0x0010293B
		internal virtual bool IsScaleSpecified
		{
			get
			{
				return this._scale != null;
			}
		}

		// Token: 0x17000E7F RID: 3711
		// (get) Token: 0x06004964 RID: 18788 RVA: 0x00104748 File Offset: 0x00102948
		internal virtual bool IsSizeSpecified
		{
			get
			{
				return this._size != null;
			}
		}

		// Token: 0x17000E80 RID: 3712
		// (get) Token: 0x06004965 RID: 18789 RVA: 0x00104758 File Offset: 0x00102958
		// (set) Token: 0x06004966 RID: 18790 RVA: 0x00104774 File Offset: 0x00102974
		[RefreshProperties(RefreshProperties.All)]
		[EntityResCategory("DataCategory_Data")]
		[EntityResDescription("DbParameter_Direction")]
		public override ParameterDirection Direction
		{
			get
			{
				ParameterDirection direction = this._direction;
				if (direction == (ParameterDirection)0)
				{
					return ParameterDirection.Input;
				}
				return direction;
			}
			set
			{
				if (this._direction == value)
				{
					return;
				}
				if (value - ParameterDirection.Input <= 2 || value == ParameterDirection.ReturnValue)
				{
					this.PropertyChanging();
					this._direction = value;
					return;
				}
				string name = typeof(ParameterDirection).Name;
				object name2 = typeof(ParameterDirection).Name;
				int num = (int)value;
				throw new ArgumentOutOfRangeException(name, Strings.ADP_InvalidEnumerationValue(name2, num.ToString(CultureInfo.InvariantCulture)));
			}
		}

		// Token: 0x17000E81 RID: 3713
		// (get) Token: 0x06004967 RID: 18791 RVA: 0x001047D9 File Offset: 0x001029D9
		// (set) Token: 0x06004968 RID: 18792 RVA: 0x001047F5 File Offset: 0x001029F5
		public override bool IsNullable
		{
			get
			{
				return this._isNullable == null || this._isNullable.Value;
			}
			set
			{
				this._isNullable = new bool?(value);
			}
		}

		// Token: 0x17000E82 RID: 3714
		// (get) Token: 0x06004969 RID: 18793 RVA: 0x00104804 File Offset: 0x00102A04
		// (set) Token: 0x0600496A RID: 18794 RVA: 0x00104840 File Offset: 0x00102A40
		[EntityResCategory("DataCategory_Data")]
		[EntityResDescription("DbParameter_Size")]
		public override int Size
		{
			get
			{
				int num = ((this._size != null) ? this._size.Value : 0);
				if (num == 0)
				{
					num = EntityParameter.ValueSize(this.Value);
				}
				return num;
			}
			set
			{
				if (this._size == null || this._size.Value != value)
				{
					if (value < -1)
					{
						throw new ArgumentException(Strings.ADP_InvalidSizeValue(value.ToString(CultureInfo.InvariantCulture)));
					}
					this.PropertyChanging();
					if (value == 0)
					{
						this._size = null;
						return;
					}
					this._size = new int?(value);
				}
			}
		}

		// Token: 0x17000E83 RID: 3715
		// (get) Token: 0x0600496B RID: 18795 RVA: 0x001048A8 File Offset: 0x00102AA8
		// (set) Token: 0x0600496C RID: 18796 RVA: 0x001048C6 File Offset: 0x00102AC6
		[EntityResCategory("DataCategory_Update")]
		[EntityResDescription("DbParameter_SourceColumn")]
		public override string SourceColumn
		{
			get
			{
				string sourceColumn = this._sourceColumn;
				if (sourceColumn == null)
				{
					return string.Empty;
				}
				return sourceColumn;
			}
			set
			{
				this._sourceColumn = value;
			}
		}

		// Token: 0x17000E84 RID: 3716
		// (get) Token: 0x0600496D RID: 18797 RVA: 0x001048CF File Offset: 0x00102ACF
		// (set) Token: 0x0600496E RID: 18798 RVA: 0x001048D7 File Offset: 0x00102AD7
		public override bool SourceColumnNullMapping
		{
			get
			{
				return this._sourceColumnNullMapping;
			}
			set
			{
				this._sourceColumnNullMapping = value;
			}
		}

		// Token: 0x17000E85 RID: 3717
		// (get) Token: 0x0600496F RID: 18799 RVA: 0x001048E0 File Offset: 0x00102AE0
		// (set) Token: 0x06004970 RID: 18800 RVA: 0x00104900 File Offset: 0x00102B00
		[EntityResCategory("DataCategory_Update")]
		[EntityResDescription("DbParameter_SourceVersion")]
		public override DataRowVersion SourceVersion
		{
			get
			{
				DataRowVersion sourceVersion = this._sourceVersion;
				if (sourceVersion == (DataRowVersion)0)
				{
					return DataRowVersion.Current;
				}
				return sourceVersion;
			}
			set
			{
				if (value <= DataRowVersion.Current)
				{
					if (value != DataRowVersion.Original && value != DataRowVersion.Current)
					{
						goto IL_0032;
					}
				}
				else if (value != DataRowVersion.Proposed && value != DataRowVersion.Default)
				{
					goto IL_0032;
				}
				this._sourceVersion = value;
				return;
				IL_0032:
				string name = typeof(DataRowVersion).Name;
				object name2 = typeof(DataRowVersion).Name;
				int num = (int)value;
				throw new ArgumentOutOfRangeException(name, Strings.ADP_InvalidEnumerationValue(name2, num.ToString(CultureInfo.InvariantCulture)));
			}
		}

		// Token: 0x06004971 RID: 18801 RVA: 0x00104975 File Offset: 0x00102B75
		public override void ResetDbType()
		{
			if (this._dbType != null || this._edmType != null)
			{
				this.PropertyChanging();
			}
			this._edmType = null;
			this._dbType = null;
		}

		// Token: 0x06004972 RID: 18802 RVA: 0x001049A5 File Offset: 0x00102BA5
		private void PropertyChanging()
		{
			this._isDirty = true;
		}

		// Token: 0x06004973 RID: 18803 RVA: 0x001049AE File Offset: 0x00102BAE
		private static int ValueSize(object value)
		{
			return EntityParameter.ValueSizeCore(value);
		}

		// Token: 0x06004974 RID: 18804 RVA: 0x001049B6 File Offset: 0x00102BB6
		internal virtual EntityParameter Clone()
		{
			return new EntityParameter(this);
		}

		// Token: 0x06004975 RID: 18805 RVA: 0x001049C0 File Offset: 0x00102BC0
		private void CloneHelper(EntityParameter destination)
		{
			destination._value = this._value;
			destination._direction = this._direction;
			destination._size = this._size;
			destination._sourceColumn = this._sourceColumn;
			destination._sourceVersion = this._sourceVersion;
			destination._sourceColumnNullMapping = this._sourceColumnNullMapping;
			destination._isNullable = this._isNullable;
			destination._parameterName = this._parameterName;
			destination._dbType = this._dbType;
			destination._edmType = this._edmType;
			destination._precision = this._precision;
			destination._scale = this._scale;
		}

		// Token: 0x06004976 RID: 18806 RVA: 0x00104A60 File Offset: 0x00102C60
		internal virtual TypeUsage GetTypeUsage()
		{
			if (!this.IsTypeConsistent)
			{
				throw new InvalidOperationException(Strings.EntityClient_EntityParameterInconsistentEdmType(this._edmType.FullName, this._parameterName));
			}
			TypeUsage typeUsage;
			if (this._edmType != null)
			{
				typeUsage = TypeUsage.Create(this._edmType);
			}
			else if (!DbTypeMap.TryGetModelTypeUsage(this.DbType, out typeUsage))
			{
				PrimitiveType primitiveType;
				if (this.DbType != DbType.Object || this.Value == null || !ClrProviderManifest.Instance.TryGetPrimitiveType(this.Value.GetType(), out primitiveType) || (!Helper.IsSpatialType(primitiveType) && !Helper.IsHierarchyIdType(primitiveType)))
				{
					throw new InvalidOperationException(Strings.EntityClient_UnsupportedDbType(this.DbType.ToString(), this.ParameterName));
				}
				typeUsage = EdmProviderManifest.Instance.GetCanonicalModelTypeUsage(primitiveType.PrimitiveTypeKind);
			}
			return typeUsage;
		}

		// Token: 0x06004977 RID: 18807 RVA: 0x00104B2D File Offset: 0x00102D2D
		internal virtual void ResetIsDirty()
		{
			this._isDirty = false;
		}

		// Token: 0x17000E86 RID: 3718
		// (get) Token: 0x06004978 RID: 18808 RVA: 0x00104B38 File Offset: 0x00102D38
		private bool IsTypeConsistent
		{
			get
			{
				if (this._edmType == null || this._dbType == null)
				{
					return true;
				}
				DbType dbTypeFromEdm = EntityParameter.GetDbTypeFromEdm(this._edmType);
				DbType? dbType;
				DbType dbType2;
				if (dbTypeFromEdm == DbType.String)
				{
					dbType = this._dbType;
					dbType2 = DbType.String;
					if (!((dbType.GetValueOrDefault() == dbType2) & (dbType != null)))
					{
						dbType = this._dbType;
						dbType2 = DbType.AnsiString;
						if (!((dbType.GetValueOrDefault() == dbType2) & (dbType != null)) && dbTypeFromEdm != DbType.AnsiStringFixedLength)
						{
							return dbTypeFromEdm == DbType.StringFixedLength;
						}
					}
					return true;
				}
				dbType = this._dbType;
				dbType2 = dbTypeFromEdm;
				return (dbType.GetValueOrDefault() == dbType2) & (dbType != null);
			}
		}

		// Token: 0x06004979 RID: 18809 RVA: 0x00104BD4 File Offset: 0x00102DD4
		private static DbType GetDbTypeFromEdm(EdmType edmType)
		{
			PrimitiveType primitiveType = Helper.AsPrimitive(edmType);
			if (Helper.IsSpatialType(primitiveType))
			{
				return DbType.Object;
			}
			DbType dbType;
			if (DbCommandDefinition.TryGetDbTypeFromPrimitiveType(primitiveType, out dbType))
			{
				return dbType;
			}
			return DbType.AnsiString;
		}

		// Token: 0x0600497A RID: 18810 RVA: 0x00104C00 File Offset: 0x00102E00
		private void ResetSize()
		{
			if (this._size != null)
			{
				this.PropertyChanging();
				this._size = null;
			}
		}

		// Token: 0x0600497B RID: 18811 RVA: 0x00104C21 File Offset: 0x00102E21
		private bool ShouldSerializeSize()
		{
			return this._size != null && this._size.Value != 0;
		}

		// Token: 0x0600497C RID: 18812 RVA: 0x00104C40 File Offset: 0x00102E40
		internal virtual void CopyTo(DbParameter destination)
		{
			this.CloneHelper((EntityParameter)destination);
		}

		// Token: 0x0600497D RID: 18813 RVA: 0x00104C50 File Offset: 0x00102E50
		internal virtual object CompareExchangeParent(object value, object comparand)
		{
			object parent = this._parent;
			if (comparand == parent)
			{
				this._parent = value;
			}
			return parent;
		}

		// Token: 0x0600497E RID: 18814 RVA: 0x00104C70 File Offset: 0x00102E70
		internal virtual void ResetParent()
		{
			this._parent = null;
		}

		// Token: 0x0600497F RID: 18815 RVA: 0x00104C79 File Offset: 0x00102E79
		public override string ToString()
		{
			return this.ParameterName;
		}

		// Token: 0x06004980 RID: 18816 RVA: 0x00104C84 File Offset: 0x00102E84
		private static int ValueSizeCore(object value)
		{
			if (!EntityUtil.IsNull(value))
			{
				string text = value as string;
				if (text != null)
				{
					return text.Length;
				}
				byte[] array = value as byte[];
				if (array != null)
				{
					return array.Length;
				}
				char[] array2 = value as char[];
				if (array2 != null)
				{
					return array2.Length;
				}
				if (value is byte || value is char)
				{
					return 1;
				}
			}
			return 0;
		}

		// Token: 0x040019E9 RID: 6633
		private string _parameterName;

		// Token: 0x040019EA RID: 6634
		private DbType? _dbType;

		// Token: 0x040019EB RID: 6635
		private EdmType _edmType;

		// Token: 0x040019EC RID: 6636
		private byte? _precision;

		// Token: 0x040019ED RID: 6637
		private byte? _scale;

		// Token: 0x040019EE RID: 6638
		private bool _isDirty;

		// Token: 0x040019EF RID: 6639
		private object _value;

		// Token: 0x040019F0 RID: 6640
		private object _parent;

		// Token: 0x040019F1 RID: 6641
		private ParameterDirection _direction;

		// Token: 0x040019F2 RID: 6642
		private int? _size;

		// Token: 0x040019F3 RID: 6643
		private string _sourceColumn;

		// Token: 0x040019F4 RID: 6644
		private DataRowVersion _sourceVersion;

		// Token: 0x040019F5 RID: 6645
		private bool _sourceColumnNullMapping;

		// Token: 0x040019F6 RID: 6646
		private bool? _isNullable;
	}
}
