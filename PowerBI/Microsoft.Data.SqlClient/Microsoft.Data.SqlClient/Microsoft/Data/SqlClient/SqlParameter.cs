using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Data;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Xml;
using Microsoft.Data.Common;
using Microsoft.Data.SqlClient.Server;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000092 RID: 146
	[TypeConverter(typeof(SqlParameter.SqlParameterConverter))]
	public sealed class SqlParameter : DbParameter, IDbDataParameter, IDataParameter, ICloneable
	{
		// Token: 0x06000BAD RID: 2989 RVA: 0x000222D0 File Offset: 0x000204D0
		public SqlParameter()
		{
			this._flags = SqlParameter.SqlParameterFlags.IsNull;
			this._actualSize = -1;
			this._direction = ParameterDirection.Input;
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x000222ED File Offset: 0x000204ED
		public SqlParameter(string parameterName, SqlDbType dbType)
			: this()
		{
			this.ParameterName = parameterName;
			this.SqlDbType = dbType;
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x00022303 File Offset: 0x00020503
		public SqlParameter(string parameterName, object value)
			: this()
		{
			this.ParameterName = parameterName;
			this.Value = value;
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x00022319 File Offset: 0x00020519
		public SqlParameter(string parameterName, SqlDbType dbType, int size)
			: this()
		{
			this.ParameterName = parameterName;
			this.SqlDbType = dbType;
			this.Size = size;
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x00022336 File Offset: 0x00020536
		public SqlParameter(string parameterName, SqlDbType dbType, int size, string sourceColumn)
			: this()
		{
			this.ParameterName = parameterName;
			this.SqlDbType = dbType;
			this.Size = size;
			this.SourceColumn = sourceColumn;
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x0002235B File Offset: 0x0002055B
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public SqlParameter(string parameterName, SqlDbType dbType, int size, ParameterDirection direction, bool isNullable, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, object value)
			: this(parameterName, dbType, size, sourceColumn)
		{
			this.Direction = direction;
			this.IsNullable = isNullable;
			this.PrecisionInternal = precision;
			this.ScaleInternal = scale;
			this.SourceVersion = sourceVersion;
			this.Value = value;
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x00022398 File Offset: 0x00020598
		public SqlParameter(string parameterName, SqlDbType dbType, int size, ParameterDirection direction, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, bool sourceColumnNullMapping, object value, string xmlSchemaCollectionDatabase, string xmlSchemaCollectionOwningSchema, string xmlSchemaCollectionName)
			: this()
		{
			this.ParameterName = parameterName;
			this.SqlDbType = dbType;
			this.Size = size;
			this.Direction = direction;
			this.PrecisionInternal = precision;
			this.ScaleInternal = scale;
			this.SourceColumn = sourceColumn;
			this.SourceVersion = sourceVersion;
			this.SourceColumnNullMapping = sourceColumnNullMapping;
			this.Value = value;
			if (!string.IsNullOrEmpty(xmlSchemaCollectionDatabase) || !string.IsNullOrEmpty(xmlSchemaCollectionOwningSchema) || !string.IsNullOrEmpty(xmlSchemaCollectionName))
			{
				this.EnsureXmlSchemaCollection();
				this._xmlSchemaCollection.Database = xmlSchemaCollectionDatabase;
				this._xmlSchemaCollection.OwningSchema = xmlSchemaCollectionOwningSchema;
				this._xmlSchemaCollection.Name = xmlSchemaCollectionName;
			}
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x00022444 File Offset: 0x00020644
		private SqlParameter(SqlParameter source)
			: this()
		{
			ADP.CheckArgumentNull(source, "source");
			source.CloneHelper(this);
			ICloneable cloneable = this._value as ICloneable;
			if (cloneable != null)
			{
				this._value = cloneable.Clone();
			}
		}

		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x06000BB5 RID: 2997 RVA: 0x00022484 File Offset: 0x00020684
		// (set) Token: 0x06000BB6 RID: 2998 RVA: 0x0002248C File Offset: 0x0002068C
		internal SqlCipherMetadata CipherMetadata { get; set; }

		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x06000BB7 RID: 2999 RVA: 0x00022495 File Offset: 0x00020695
		// (set) Token: 0x06000BB8 RID: 3000 RVA: 0x0002249F File Offset: 0x0002069F
		internal bool HasReceivedMetadata
		{
			get
			{
				return this.HasFlag(SqlParameter.SqlParameterFlags.HasReceivedMetadata);
			}
			set
			{
				this.SetFlag(SqlParameter.SqlParameterFlags.HasReceivedMetadata, value);
			}
		}

		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x06000BB9 RID: 3001 RVA: 0x000224AA File Offset: 0x000206AA
		internal byte NormalizationRuleVersion
		{
			get
			{
				SqlCipherMetadata cipherMetadata = this.CipherMetadata;
				if (cipherMetadata == null)
				{
					return 0;
				}
				return cipherMetadata.NormalizationRuleVersion;
			}
		}

		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x06000BBA RID: 3002 RVA: 0x000224C0 File Offset: 0x000206C0
		// (set) Token: 0x06000BBB RID: 3003 RVA: 0x000224E0 File Offset: 0x000206E0
		[Browsable(false)]
		public SqlCompareOptions CompareInfo
		{
			get
			{
				SqlCollation collation = this._collation;
				if (collation != null)
				{
					return collation.SqlCompareOptions;
				}
				return SqlCompareOptions.None;
			}
			set
			{
				SqlCollation collation = this._collation;
				SqlCompareOptions sqlCompareOptions = SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreNonSpace | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth | SqlCompareOptions.BinarySort | SqlCompareOptions.BinarySort2;
				if ((value & sqlCompareOptions) != value)
				{
					throw ADP.ArgumentOutOfRange("CompareInfo");
				}
				if (collation == null || collation.SqlCompareOptions != value)
				{
					this._collation = SqlCollation.FromLCIDAndSort((collation != null) ? collation.LCID : 0, value);
				}
			}
		}

		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x06000BBC RID: 3004 RVA: 0x0002252F File Offset: 0x0002072F
		// (set) Token: 0x06000BBD RID: 3005 RVA: 0x0002254C File Offset: 0x0002074C
		[ResCategory("XML")]
		public string XmlSchemaCollectionDatabase
		{
			get
			{
				SqlMetaDataXmlSchemaCollection xmlSchemaCollection = this._xmlSchemaCollection;
				return ((xmlSchemaCollection != null) ? xmlSchemaCollection.Database : null) ?? string.Empty;
			}
			set
			{
				this.EnsureXmlSchemaCollection().Database = value;
			}
		}

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x06000BBE RID: 3006 RVA: 0x0002255A File Offset: 0x0002075A
		// (set) Token: 0x06000BBF RID: 3007 RVA: 0x00022577 File Offset: 0x00020777
		[ResCategory("XML")]
		public string XmlSchemaCollectionOwningSchema
		{
			get
			{
				SqlMetaDataXmlSchemaCollection xmlSchemaCollection = this._xmlSchemaCollection;
				return ((xmlSchemaCollection != null) ? xmlSchemaCollection.OwningSchema : null) ?? string.Empty;
			}
			set
			{
				this.EnsureXmlSchemaCollection().OwningSchema = value;
			}
		}

		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x06000BC0 RID: 3008 RVA: 0x00022585 File Offset: 0x00020785
		// (set) Token: 0x06000BC1 RID: 3009 RVA: 0x000225A2 File Offset: 0x000207A2
		[ResCategory("XML")]
		public string XmlSchemaCollectionName
		{
			get
			{
				SqlMetaDataXmlSchemaCollection xmlSchemaCollection = this._xmlSchemaCollection;
				return ((xmlSchemaCollection != null) ? xmlSchemaCollection.Name : null) ?? string.Empty;
			}
			set
			{
				this.EnsureXmlSchemaCollection().Name = value;
			}
		}

		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x06000BC2 RID: 3010 RVA: 0x000225B0 File Offset: 0x000207B0
		// (set) Token: 0x06000BC3 RID: 3011 RVA: 0x000225BD File Offset: 0x000207BD
		[DefaultValue(false)]
		[ResCategory("Data")]
		public bool ForceColumnEncryption
		{
			get
			{
				return this.HasFlag(SqlParameter.SqlParameterFlags.ForceColumnEncryption);
			}
			set
			{
				this.SetFlag(SqlParameter.SqlParameterFlags.ForceColumnEncryption, value);
			}
		}

		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x06000BC4 RID: 3012 RVA: 0x000225CB File Offset: 0x000207CB
		// (set) Token: 0x06000BC5 RID: 3013 RVA: 0x000225D8 File Offset: 0x000207D8
		public override DbType DbType
		{
			get
			{
				return this.GetMetaTypeOnly().DbType;
			}
			set
			{
				MetaType metaType = this._metaType;
				if (metaType == null || metaType.DbType != value)
				{
					this.PropertyTypeChanging();
					this._metaType = MetaType.GetMetaTypeFromDbType(value);
				}
			}
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x0002260A File Offset: 0x0002080A
		public override void ResetDbType()
		{
			this.ResetSqlDbType();
		}

		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x06000BC7 RID: 3015 RVA: 0x00022612 File Offset: 0x00020812
		// (set) Token: 0x06000BC8 RID: 3016 RVA: 0x00022624 File Offset: 0x00020824
		[ResCategory("Data")]
		public override string ParameterName
		{
			get
			{
				return this._parameterName ?? string.Empty;
			}
			set
			{
				if (!string.IsNullOrEmpty(value) && value.Length >= 128 && (value[0] != '@' || value.Length > 128))
				{
					throw SQL.InvalidParameterNameLength(value);
				}
				if (this._parameterName != value)
				{
					this.PropertyChanging();
					this._parameterName = value;
					return;
				}
			}
		}

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x06000BC9 RID: 3017 RVA: 0x00022684 File Offset: 0x00020884
		// (set) Token: 0x06000BCA RID: 3018 RVA: 0x000226A4 File Offset: 0x000208A4
		[Browsable(false)]
		public int LocaleId
		{
			get
			{
				SqlCollation collation = this._collation;
				if (collation != null)
				{
					return collation.LCID;
				}
				return 0;
			}
			set
			{
				SqlCollation collation = this._collation;
				if ((long)value != (1048575L & (long)value))
				{
					throw ADP.ArgumentOutOfRange("LocaleId");
				}
				if (collation == null || collation.LCID != value)
				{
					this._collation = SqlCollation.FromLCIDAndSort(value, (collation != null) ? collation.SqlCompareOptions : SqlCompareOptions.None);
				}
			}
		}

		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x06000BCB RID: 3019 RVA: 0x000226F4 File Offset: 0x000208F4
		// (set) Token: 0x06000BCC RID: 3020 RVA: 0x000226FC File Offset: 0x000208FC
		[DefaultValue(0)]
		[ResCategory("Data")]
		public new byte Precision
		{
			get
			{
				return this.PrecisionInternal;
			}
			set
			{
				this.PrecisionInternal = value;
			}
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x00022705 File Offset: 0x00020905
		private bool ShouldSerializePrecision()
		{
			return this._precision > 0;
		}

		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x06000BCE RID: 3022 RVA: 0x00022710 File Offset: 0x00020910
		// (set) Token: 0x06000BCF RID: 3023 RVA: 0x00022718 File Offset: 0x00020918
		[DefaultValue(0)]
		[ResCategory("Data")]
		public new byte Scale
		{
			get
			{
				return this.ScaleInternal;
			}
			set
			{
				this.ScaleInternal = value;
			}
		}

		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x06000BD0 RID: 3024 RVA: 0x00022724 File Offset: 0x00020924
		// (set) Token: 0x06000BD1 RID: 3025 RVA: 0x00022754 File Offset: 0x00020954
		internal byte ScaleInternal
		{
			get
			{
				byte b = this._scale;
				SqlDbType metaSqlDbTypeOnly = this.GetMetaSqlDbTypeOnly();
				if (b == 0 && metaSqlDbTypeOnly == SqlDbType.Decimal)
				{
					b = this.ValueScale(this.SqlValue);
				}
				return b;
			}
			set
			{
				if (this._scale != value || !this.HasFlag(SqlParameter.SqlParameterFlags.HasScale))
				{
					this.PropertyChanging();
					this._scale = value;
					this.SetFlag(SqlParameter.SqlParameterFlags.HasScale, true);
					this._actualSize = -1;
				}
			}
		}

		// Token: 0x06000BD2 RID: 3026 RVA: 0x0002278C File Offset: 0x0002098C
		private bool ShouldSerializeScale()
		{
			return this._scale > 0;
		}

		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x06000BD3 RID: 3027 RVA: 0x00022797 File Offset: 0x00020997
		// (set) Token: 0x06000BD4 RID: 3028 RVA: 0x000227A4 File Offset: 0x000209A4
		[RefreshProperties(RefreshProperties.All)]
		[ResCategory("Data")]
		[DbProviderSpecificTypeProperty(true)]
		public SqlDbType SqlDbType
		{
			get
			{
				return this.GetMetaTypeOnly().SqlDbType;
			}
			set
			{
				MetaType metaType = this._metaType;
				if ((SqlDbType)24 == value)
				{
					throw SQL.InvalidSqlDbType(value);
				}
				if (metaType == null || metaType.SqlDbType != value)
				{
					this.PropertyTypeChanging();
					this._metaType = MetaType.GetMetaTypeFromSqlDbType(value, value == SqlDbType.Structured);
				}
			}
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x000227E7 File Offset: 0x000209E7
		private bool ShouldSerializeSqlDbType()
		{
			return this._metaType != null;
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x000227F2 File Offset: 0x000209F2
		public void ResetSqlDbType()
		{
			if (this._metaType != null)
			{
				this.PropertyTypeChanging();
				this._metaType = null;
			}
		}

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x06000BD7 RID: 3031 RVA: 0x0002280C File Offset: 0x00020A0C
		// (set) Token: 0x06000BD8 RID: 3032 RVA: 0x000228AD File Offset: 0x00020AAD
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public object SqlValue
		{
			get
			{
				if (this._udtLoadError != null)
				{
					throw this._udtLoadError;
				}
				if (this._value != null)
				{
					if (this._value == DBNull.Value)
					{
						return MetaType.GetNullSqlValue(this.GetMetaTypeOnly().SqlType);
					}
					if (this._value is INullable)
					{
						return this._value;
					}
					if (this._value is DateTime)
					{
						SqlDbType sqlDbType = this.GetMetaTypeOnly().SqlDbType;
						if (sqlDbType == SqlDbType.Date || sqlDbType == SqlDbType.DateTime2)
						{
							return this._value;
						}
					}
					return MetaType.GetSqlValueFromComVariant(this._value);
				}
				else
				{
					if (this._sqlBufferReturnValue != null)
					{
						return this._sqlBufferReturnValue.SqlValue;
					}
					return null;
				}
			}
			set
			{
				this.Value = value;
			}
		}

		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x06000BD9 RID: 3033 RVA: 0x000228B6 File Offset: 0x00020AB6
		// (set) Token: 0x06000BDA RID: 3034 RVA: 0x000228C7 File Offset: 0x00020AC7
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string UdtTypeName
		{
			get
			{
				return this._udtTypeName ?? string.Empty;
			}
			set
			{
				this._udtTypeName = value;
			}
		}

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x06000BDB RID: 3035 RVA: 0x000228D0 File Offset: 0x00020AD0
		// (set) Token: 0x06000BDC RID: 3036 RVA: 0x000228E1 File Offset: 0x00020AE1
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string TypeName
		{
			get
			{
				return this._typeName ?? string.Empty;
			}
			set
			{
				this._typeName = value;
				this.IsDerivedParameterTypeName = false;
			}
		}

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x06000BDD RID: 3037 RVA: 0x000228F4 File Offset: 0x00020AF4
		// (set) Token: 0x06000BDE RID: 3038 RVA: 0x00022948 File Offset: 0x00020B48
		[RefreshProperties(RefreshProperties.All)]
		[ResCategory("Data")]
		[TypeConverter(typeof(StringConverter))]
		public override object Value
		{
			get
			{
				if (this._udtLoadError != null)
				{
					throw this._udtLoadError;
				}
				if (this._value != null)
				{
					return this._value;
				}
				if (this._sqlBufferReturnValue == null)
				{
					return null;
				}
				if (this.ParameterIsSqlType)
				{
					return this._sqlBufferReturnValue.SqlValue;
				}
				return this._sqlBufferReturnValue.Value;
			}
			set
			{
				this._value = value;
				this._sqlBufferReturnValue = null;
				this._coercedValue = null;
				this._valueAsINullable = this._value as INullable;
				this.SetFlag(SqlParameter.SqlParameterFlags.IsSqlParameterSqlType, this._valueAsINullable != null);
				this.SetFlag(SqlParameter.SqlParameterFlags.IsNull, this._value == null || this._value == DBNull.Value || (this.HasFlag(SqlParameter.SqlParameterFlags.IsSqlParameterSqlType) && this._valueAsINullable.IsNull));
				this._udtLoadError = null;
				this._actualSize = -1;
			}
		}

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x06000BDF RID: 3039 RVA: 0x000229CF File Offset: 0x00020BCF
		// (set) Token: 0x06000BE0 RID: 3040 RVA: 0x000229D7 File Offset: 0x00020BD7
		[RefreshProperties(RefreshProperties.All)]
		[ResCategory("Data")]
		public override ParameterDirection Direction
		{
			get
			{
				return this._direction;
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
				throw ADP.InvalidParameterDirection(value);
			}
		}

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x06000BE1 RID: 3041 RVA: 0x00022A01 File Offset: 0x00020C01
		// (set) Token: 0x06000BE2 RID: 3042 RVA: 0x00022A0A File Offset: 0x00020C0A
		public override bool IsNullable
		{
			get
			{
				return this.HasFlag(SqlParameter.SqlParameterFlags.IsNullable);
			}
			set
			{
				this.SetFlag(SqlParameter.SqlParameterFlags.IsNullable, value);
			}
		}

		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x06000BE3 RID: 3043 RVA: 0x00022A14 File Offset: 0x00020C14
		// (set) Token: 0x06000BE4 RID: 3044 RVA: 0x00022A1C File Offset: 0x00020C1C
		public int Offset
		{
			get
			{
				return this._offset;
			}
			set
			{
				if (value < 0)
				{
					throw ADP.InvalidOffsetValue(value);
				}
				this._offset = value;
			}
		}

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x06000BE5 RID: 3045 RVA: 0x00022A30 File Offset: 0x00020C30
		// (set) Token: 0x06000BE6 RID: 3046 RVA: 0x00022A55 File Offset: 0x00020C55
		[ResCategory("Data")]
		public override int Size
		{
			get
			{
				int num = this._size;
				if (num == 0)
				{
					num = this.ValueSize(this.Value);
				}
				return num;
			}
			set
			{
				if (value != this._size)
				{
					if (value < -1)
					{
						throw ADP.InvalidSizeValue(value);
					}
					this.PropertyChanging();
					this._size = value;
				}
			}
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x00022A78 File Offset: 0x00020C78
		private void ResetSize()
		{
			if (this._size != 0)
			{
				this.PropertyChanging();
				this._size = 0;
			}
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x00022A8F File Offset: 0x00020C8F
		private bool ShouldSerializeSize()
		{
			return this._size != 0;
		}

		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x06000BE9 RID: 3049 RVA: 0x00022A9A File Offset: 0x00020C9A
		// (set) Token: 0x06000BEA RID: 3050 RVA: 0x00022AAB File Offset: 0x00020CAB
		[ResCategory("Update")]
		public override string SourceColumn
		{
			get
			{
				return this._sourceColumn ?? string.Empty;
			}
			set
			{
				this._sourceColumn = value;
			}
		}

		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x06000BEB RID: 3051 RVA: 0x00022AB4 File Offset: 0x00020CB4
		// (set) Token: 0x06000BEC RID: 3052 RVA: 0x00022ABD File Offset: 0x00020CBD
		[ResCategory("DataCategory_Update")]
		public override bool SourceColumnNullMapping
		{
			get
			{
				return this.HasFlag(SqlParameter.SqlParameterFlags.SourceColumnNullMapping);
			}
			set
			{
				this.SetFlag(SqlParameter.SqlParameterFlags.SourceColumnNullMapping, value);
			}
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x00022AC7 File Offset: 0x00020CC7
		[ResCategory("Data")]
		public override string ToString()
		{
			return this.ParameterName;
		}

		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x06000BEE RID: 3054 RVA: 0x00022AD0 File Offset: 0x00020CD0
		// (set) Token: 0x06000BEF RID: 3055 RVA: 0x00022AEE File Offset: 0x00020CEE
		[ResCategory("Update")]
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
				throw ADP.InvalidDataRowVersion(value);
			}
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x00022B28 File Offset: 0x00020D28
		object ICloneable.Clone()
		{
			return new SqlParameter(this);
		}

		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x06000BF1 RID: 3057 RVA: 0x00022B30 File Offset: 0x00020D30
		// (set) Token: 0x06000BF2 RID: 3058 RVA: 0x00022B38 File Offset: 0x00020D38
		private object CoercedValue
		{
			get
			{
				return this._coercedValue;
			}
			set
			{
				this._coercedValue = value;
			}
		}

		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x06000BF3 RID: 3059 RVA: 0x00022B41 File Offset: 0x00020D41
		internal bool CoercedValueIsDataFeed
		{
			get
			{
				if (this._coercedValue == null)
				{
					this.GetCoercedValue();
				}
				return this.HasFlag(SqlParameter.SqlParameterFlags.CoercedValueIsDataFeed);
			}
		}

		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x06000BF4 RID: 3060 RVA: 0x00022B5A File Offset: 0x00020D5A
		internal bool CoercedValueIsSqlType
		{
			get
			{
				if (this._coercedValue == null)
				{
					this.GetCoercedValue();
				}
				return this.HasFlag(SqlParameter.SqlParameterFlags.CoercedValueIsSqlType);
			}
		}

		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x06000BF5 RID: 3061 RVA: 0x00022B73 File Offset: 0x00020D73
		// (set) Token: 0x06000BF6 RID: 3062 RVA: 0x00022B7B File Offset: 0x00020D7B
		internal SqlCollation Collation
		{
			get
			{
				return this._collation;
			}
			set
			{
				this._collation = value;
			}
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x00022B84 File Offset: 0x00020D84
		private bool HasFlag(SqlParameter.SqlParameterFlags flag)
		{
			return (this._flags & flag) > SqlParameter.SqlParameterFlags.None;
		}

		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x06000BF8 RID: 3064 RVA: 0x00022B94 File Offset: 0x00020D94
		internal bool IsNull
		{
			get
			{
				if (this._internalMetaType.SqlDbType == SqlDbType.Udt)
				{
					this.SetFlag(SqlParameter.SqlParameterFlags.IsNull, this._value == null || this._value == DBNull.Value || (this.HasFlag(SqlParameter.SqlParameterFlags.IsSqlParameterSqlType) && this._valueAsINullable.IsNull));
				}
				return this.HasFlag(SqlParameter.SqlParameterFlags.IsNull);
			}
		}

		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x06000BF9 RID: 3065 RVA: 0x00022BED File Offset: 0x00020DED
		// (set) Token: 0x06000BFA RID: 3066 RVA: 0x00022BF5 File Offset: 0x00020DF5
		internal MetaType InternalMetaType
		{
			get
			{
				return this._internalMetaType;
			}
			set
			{
				this._internalMetaType = value;
			}
		}

		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x06000BFB RID: 3067 RVA: 0x00022C00 File Offset: 0x00020E00
		// (set) Token: 0x06000BFC RID: 3068 RVA: 0x00022C30 File Offset: 0x00020E30
		internal byte PrecisionInternal
		{
			get
			{
				byte b = this._precision;
				SqlDbType metaSqlDbTypeOnly = this.GetMetaSqlDbTypeOnly();
				if (b == 0 && SqlDbType.Decimal == metaSqlDbTypeOnly)
				{
					b = this.ValuePrecision(this.SqlValue);
				}
				return b;
			}
			set
			{
				SqlDbType sqlDbType = this.SqlDbType;
				if (sqlDbType == SqlDbType.Decimal && value > 38)
				{
					throw SQL.PrecisionValueOutOfRange(value);
				}
				if (this._precision != value)
				{
					this.PropertyChanging();
					this._precision = value;
				}
			}
		}

		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x06000BFD RID: 3069 RVA: 0x00022C6A File Offset: 0x00020E6A
		// (set) Token: 0x06000BFE RID: 3070 RVA: 0x00022C73 File Offset: 0x00020E73
		internal bool ParameterIsSqlType
		{
			get
			{
				return this.HasFlag(SqlParameter.SqlParameterFlags.IsSqlParameterSqlType);
			}
			set
			{
				this.SetFlag(SqlParameter.SqlParameterFlags.IsSqlParameterSqlType, value);
			}
		}

		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x06000BFF RID: 3071 RVA: 0x00022C80 File Offset: 0x00020E80
		internal string ParameterNameFixed
		{
			get
			{
				string text = this.ParameterName;
				if (text.Length > 0 && text[0] != '@')
				{
					text = "@" + text;
				}
				return text;
			}
		}

		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x06000C00 RID: 3072 RVA: 0x00022CB5 File Offset: 0x00020EB5
		internal bool SizeInferred
		{
			get
			{
				return this._size == 0;
			}
		}

		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x06000C01 RID: 3073 RVA: 0x00022CC0 File Offset: 0x00020EC0
		internal INullable ValueAsINullable
		{
			get
			{
				return this._valueAsINullable;
			}
		}

		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x06000C02 RID: 3074 RVA: 0x00022CC8 File Offset: 0x00020EC8
		// (set) Token: 0x06000C03 RID: 3075 RVA: 0x00022CD5 File Offset: 0x00020ED5
		internal bool IsDerivedParameterTypeName
		{
			get
			{
				return this.HasFlag(SqlParameter.SqlParameterFlags.IsDerivedParameterTypeName);
			}
			set
			{
				this.SetFlag(SqlParameter.SqlParameterFlags.IsDerivedParameterTypeName, value);
			}
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x00022CE4 File Offset: 0x00020EE4
		private void CloneHelper(SqlParameter destination)
		{
			destination._value = this._value;
			destination._direction = this._direction;
			destination._size = this._size;
			destination._offset = this._offset;
			destination._sourceColumn = this._sourceColumn;
			destination._sourceVersion = this._sourceVersion;
			destination._flags = this._flags & (SqlParameter.SqlParameterFlags.IsNull | SqlParameter.SqlParameterFlags.IsNullable | SqlParameter.SqlParameterFlags.IsSqlParameterSqlType | SqlParameter.SqlParameterFlags.SourceColumnNullMapping | SqlParameter.SqlParameterFlags.CoercedValueIsSqlType | SqlParameter.SqlParameterFlags.CoercedValueIsDataFeed | SqlParameter.SqlParameterFlags.ForceColumnEncryption | SqlParameter.SqlParameterFlags.IsDerivedParameterTypeName);
			destination._metaType = this._metaType;
			destination._collation = this._collation;
			if (this._xmlSchemaCollection != null)
			{
				destination.EnsureXmlSchemaCollection().CopyFrom(this._xmlSchemaCollection);
			}
			destination._udtTypeName = this._udtTypeName;
			destination._typeName = this._typeName;
			destination._udtLoadError = this._udtLoadError;
			destination._parameterName = this._parameterName;
			destination._precision = this._precision;
			destination._scale = this._scale;
			destination._sqlBufferReturnValue = this._sqlBufferReturnValue;
			destination._internalMetaType = this._internalMetaType;
			destination.CoercedValue = this.CoercedValue;
			destination._valueAsINullable = this._valueAsINullable;
			destination._actualSize = this._actualSize;
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x00022E00 File Offset: 0x00021000
		internal void CopyTo(SqlParameter destination)
		{
			ADP.CheckArgumentNull(destination, "destination");
			this.CloneHelper(destination);
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x00022E14 File Offset: 0x00021014
		internal object CompareExchangeParent(object value, object comparand)
		{
			object parent = this._parent;
			if (comparand == parent)
			{
				this._parent = value;
			}
			return parent;
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x00022E34 File Offset: 0x00021034
		private SqlMetaDataXmlSchemaCollection EnsureXmlSchemaCollection()
		{
			if (this._xmlSchemaCollection == null)
			{
				this._xmlSchemaCollection = new SqlMetaDataXmlSchemaCollection();
			}
			return this._xmlSchemaCollection;
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x00022E50 File Offset: 0x00021050
		internal void FixStreamDataForNonPLP()
		{
			object coercedValue = this.GetCoercedValue();
			if (!this.HasFlag(SqlParameter.SqlParameterFlags.CoercedValueIsDataFeed))
			{
				return;
			}
			this.SetFlag(SqlParameter.SqlParameterFlags.CoercedValueIsDataFeed, false);
			TextDataFeed textDataFeed = coercedValue as TextDataFeed;
			if (textDataFeed != null)
			{
				if (this.Size > 0)
				{
					char[] array = new char[this.Size];
					int num = textDataFeed._source.ReadBlock(array, 0, this.Size);
					this.CoercedValue = new string(array, 0, num);
					return;
				}
				this.CoercedValue = textDataFeed._source.ReadToEnd();
				return;
			}
			else
			{
				StreamDataFeed streamDataFeed = coercedValue as StreamDataFeed;
				if (streamDataFeed != null)
				{
					if (this.Size > 0)
					{
						byte[] array2 = new byte[this.Size];
						int i = 0;
						Stream source = streamDataFeed._source;
						while (i < this.Size)
						{
							int num2 = source.Read(array2, i, this.Size - i);
							if (num2 == 0)
							{
								break;
							}
							i += num2;
						}
						if (i < this.Size)
						{
							Array.Resize<byte>(ref array2, i);
						}
						this.CoercedValue = array2;
						return;
					}
					MemoryStream memoryStream = new MemoryStream();
					streamDataFeed._source.CopyTo(memoryStream);
					this.CoercedValue = memoryStream.ToArray();
					return;
				}
				else
				{
					XmlDataFeed xmlDataFeed = coercedValue as XmlDataFeed;
					if (xmlDataFeed != null)
					{
						this.CoercedValue = MetaType.GetStringFromXml(xmlDataFeed._source);
						return;
					}
					return;
				}
			}
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x00022F88 File Offset: 0x00021188
		private void GetActualFieldsAndProperties(out List<SmiExtendedMetaData> fields, out SmiMetaDataPropertyCollection props, out ParameterPeekAheadValue peekAhead)
		{
			fields = null;
			props = null;
			peekAhead = null;
			object coercedValue = this.GetCoercedValue();
			DataTable dataTable = coercedValue as DataTable;
			if (dataTable != null)
			{
				if (dataTable.Columns.Count <= 0)
				{
					throw SQL.NotEnoughColumnsInStructuredType();
				}
				fields = new List<SmiExtendedMetaData>(dataTable.Columns.Count);
				bool[] array = new bool[dataTable.Columns.Count];
				bool flag = false;
				if (dataTable.PrimaryKey != null && dataTable.PrimaryKey.Length != 0)
				{
					foreach (DataColumn dataColumn in dataTable.PrimaryKey)
					{
						array[dataColumn.Ordinal] = true;
						flag = true;
					}
				}
				for (int j = 0; j < dataTable.Columns.Count; j++)
				{
					fields.Add(MetaDataUtilsSmi.SmiMetaDataFromDataColumn(dataTable.Columns[j], dataTable));
					if (!flag && dataTable.Columns[j].Unique)
					{
						array[j] = true;
						flag = true;
					}
				}
				if (flag)
				{
					props = new SmiMetaDataPropertyCollection();
					props[SmiPropertySelector.UniqueKey] = new SmiUniqueKeyProperty(new List<bool>(array));
					return;
				}
			}
			else
			{
				SqlDataReader sqlDataReader = coercedValue as SqlDataReader;
				if (sqlDataReader != null)
				{
					fields = new List<SmiExtendedMetaData>(sqlDataReader.GetInternalSmiMetaData());
					if (fields.Count <= 0)
					{
						throw SQL.NotEnoughColumnsInStructuredType();
					}
					bool[] array2 = new bool[fields.Count];
					bool flag2 = false;
					for (int k = 0; k < fields.Count; k++)
					{
						SmiQueryMetaData smiQueryMetaData = fields[k] as SmiQueryMetaData;
						if (smiQueryMetaData != null && !smiQueryMetaData.IsKey.IsNull && smiQueryMetaData.IsKey.Value)
						{
							array2[k] = true;
							flag2 = true;
						}
					}
					if (flag2)
					{
						props = new SmiMetaDataPropertyCollection();
						props[SmiPropertySelector.UniqueKey] = new SmiUniqueKeyProperty(new List<bool>(array2));
						return;
					}
				}
				else
				{
					IEnumerable<SqlDataRecord> enumerable = coercedValue as IEnumerable<SqlDataRecord>;
					if (enumerable != null)
					{
						IEnumerator<SqlDataRecord> enumerator = enumerable.GetEnumerator();
						try
						{
							if (!enumerator.MoveNext())
							{
								throw SQL.IEnumerableOfSqlDataRecordHasNoRows();
							}
							SqlDataRecord sqlDataRecord = enumerator.Current;
							int fieldCount = sqlDataRecord.FieldCount;
							if (0 < fieldCount)
							{
								bool[] array3 = new bool[fieldCount];
								bool[] array4 = new bool[fieldCount];
								bool[] array5 = new bool[fieldCount];
								int num = -1;
								bool flag3 = false;
								bool flag4 = false;
								int num2 = 0;
								SmiOrderProperty.SmiColumnOrder[] array6 = new SmiOrderProperty.SmiColumnOrder[fieldCount];
								fields = new List<SmiExtendedMetaData>(fieldCount);
								for (int l = 0; l < fieldCount; l++)
								{
									SqlMetaData sqlMetaData = sqlDataRecord.GetSqlMetaData(l);
									fields.Add(MetaDataUtilsSmi.SqlMetaDataToSmiExtendedMetaData(sqlMetaData));
									if (sqlMetaData.IsUniqueKey)
									{
										array3[l] = true;
										flag3 = true;
									}
									if (sqlMetaData.UseServerDefault)
									{
										array4[l] = true;
										flag4 = true;
									}
									array6[l]._order = sqlMetaData.SortOrder;
									if (SortOrder.Unspecified != sqlMetaData.SortOrder)
									{
										if (fieldCount <= sqlMetaData.SortOrdinal)
										{
											throw SQL.SortOrdinalGreaterThanFieldCount(l, sqlMetaData.SortOrdinal);
										}
										if (array5[sqlMetaData.SortOrdinal])
										{
											throw SQL.DuplicateSortOrdinal(sqlMetaData.SortOrdinal);
										}
										array6[l]._sortOrdinal = sqlMetaData.SortOrdinal;
										array5[sqlMetaData.SortOrdinal] = true;
										if (sqlMetaData.SortOrdinal > num)
										{
											num = sqlMetaData.SortOrdinal;
										}
										num2++;
									}
								}
								if (flag3)
								{
									props = new SmiMetaDataPropertyCollection();
									props[SmiPropertySelector.UniqueKey] = new SmiUniqueKeyProperty(new List<bool>(array3));
								}
								if (flag4)
								{
									if (props == null)
									{
										props = new SmiMetaDataPropertyCollection();
									}
									props[SmiPropertySelector.DefaultFields] = new SmiDefaultFieldsProperty(new List<bool>(array4));
								}
								if (0 < num2)
								{
									if (num >= num2)
									{
										int num3 = 0;
										while (num3 < num2 && array5[num3])
										{
											num3++;
										}
										throw SQL.MissingSortOrdinal(num3);
									}
									if (props == null)
									{
										props = new SmiMetaDataPropertyCollection();
									}
									props[SmiPropertySelector.SortOrder] = new SmiOrderProperty(new List<SmiOrderProperty.SmiColumnOrder>(array6));
								}
								peekAhead = new ParameterPeekAheadValue
								{
									Enumerator = enumerator,
									FirstRecord = sqlDataRecord
								};
								enumerator = null;
								return;
							}
							throw SQL.NotEnoughColumnsInStructuredType();
						}
						finally
						{
							if (enumerator != null)
							{
								enumerator.Dispose();
							}
						}
					}
					DbDataReader dbDataReader = coercedValue as DbDataReader;
					if (dbDataReader != null)
					{
						DataTable schemaTable = dbDataReader.GetSchemaTable();
						if (schemaTable.Rows.Count <= 0)
						{
							throw SQL.NotEnoughColumnsInStructuredType();
						}
						int count = schemaTable.Rows.Count;
						fields = new List<SmiExtendedMetaData>(count);
						bool[] array7 = new bool[count];
						bool flag5 = false;
						int ordinal = schemaTable.Columns[SchemaTableColumn.IsKey].Ordinal;
						int ordinal2 = schemaTable.Columns[SchemaTableColumn.ColumnOrdinal].Ordinal;
						for (int m = 0; m < count; m++)
						{
							DataRow dataRow = schemaTable.Rows[m];
							SmiExtendedMetaData smiExtendedMetaData = MetaDataUtilsSmi.SmiMetaDataFromSchemaTableRow(dataRow);
							int n = m;
							if (!dataRow.IsNull(ordinal2))
							{
								n = (int)dataRow[ordinal2];
							}
							if (n >= count || n < 0)
							{
								throw SQL.InvalidSchemaTableOrdinals();
							}
							while (n > fields.Count)
							{
								fields.Add(null);
							}
							if (fields.Count == n)
							{
								fields.Add(smiExtendedMetaData);
							}
							else
							{
								if (fields[n] != null)
								{
									throw SQL.InvalidSchemaTableOrdinals();
								}
								fields[n] = smiExtendedMetaData;
							}
							if (!dataRow.IsNull(ordinal) && (bool)dataRow[ordinal])
							{
								array7[n] = true;
								flag5 = true;
							}
						}
						if (flag5)
						{
							props = new SmiMetaDataPropertyCollection();
							props[SmiPropertySelector.UniqueKey] = new SmiUniqueKeyProperty(new List<bool>(array7));
						}
					}
				}
			}
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x000234F8 File Offset: 0x000216F8
		internal byte GetActualScale()
		{
			if (this.ShouldSerializeScale())
			{
				return this.ScaleInternal;
			}
			if (this.GetMetaTypeOnly().IsVarTime)
			{
				return 7;
			}
			return this.ValueScale(this.CoercedValue);
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x00023524 File Offset: 0x00021724
		internal int GetActualSize()
		{
			MetaType metaType = this.InternalMetaType;
			SqlDbType sqlDbType = metaType.SqlDbType;
			if (this._actualSize == -1 || sqlDbType == SqlDbType.Udt)
			{
				this._actualSize = 0;
				object coercedValue = this.GetCoercedValue();
				bool flag = false;
				if (this.IsNull && !metaType.IsVarTime)
				{
					return 0;
				}
				if (sqlDbType == SqlDbType.Variant)
				{
					metaType = MetaType.GetMetaTypeFromValue(coercedValue, false);
					sqlDbType = MetaType.GetSqlDataType((int)metaType.TDSType, 0U, 0).SqlDbType;
					flag = true;
				}
				if (metaType.IsFixed)
				{
					this._actualSize = metaType.FixedLength;
				}
				else
				{
					int num = 0;
					if (sqlDbType <= SqlDbType.Char)
					{
						if (sqlDbType == SqlDbType.Binary)
						{
							goto IL_01F1;
						}
						if (sqlDbType != SqlDbType.Char)
						{
							goto IL_02C7;
						}
					}
					else
					{
						if (sqlDbType != SqlDbType.Image)
						{
							if (sqlDbType - SqlDbType.NChar > 2)
							{
								switch (sqlDbType)
								{
								case SqlDbType.Text:
								case SqlDbType.VarChar:
									goto IL_0179;
								case SqlDbType.Timestamp:
								case SqlDbType.VarBinary:
									goto IL_01F1;
								case SqlDbType.TinyInt:
								case SqlDbType.Variant:
								case (SqlDbType)24:
								case (SqlDbType)26:
								case (SqlDbType)27:
								case (SqlDbType)28:
								case SqlDbType.Date:
									goto IL_02C7;
								case SqlDbType.Xml:
									break;
								case SqlDbType.Udt:
									if (!this.IsNull)
									{
										num = AssemblyCache.GetLength(coercedValue);
										goto IL_02C7;
									}
									goto IL_02C7;
								case SqlDbType.Structured:
									num = -1;
									goto IL_02C7;
								case SqlDbType.Time:
									this._actualSize = (flag ? 5 : MetaType.GetTimeSizeFromScale(this.GetActualScale()));
									goto IL_02C7;
								case SqlDbType.DateTime2:
									this._actualSize = 3 + (flag ? 5 : MetaType.GetTimeSizeFromScale(this.GetActualScale()));
									goto IL_02C7;
								case SqlDbType.DateTimeOffset:
									this._actualSize = 5 + (flag ? 5 : MetaType.GetTimeSizeFromScale(this.GetActualScale()));
									goto IL_02C7;
								default:
									goto IL_02C7;
								}
							}
							num = ((!this.HasFlag(SqlParameter.SqlParameterFlags.IsNull) && !this.HasFlag(SqlParameter.SqlParameterFlags.CoercedValueIsDataFeed)) ? SqlParameter.StringSize(coercedValue, this.HasFlag(SqlParameter.SqlParameterFlags.CoercedValueIsSqlType)) : 0);
							this._actualSize = (this.ShouldSerializeSize() ? this.Size : 0);
							this._actualSize = ((this.ShouldSerializeSize() && this._actualSize <= num) ? this._actualSize : num);
							if (this._actualSize == -1)
							{
								this._actualSize = num;
							}
							this._actualSize <<= 1;
							goto IL_02C7;
						}
						goto IL_01F1;
					}
					IL_0179:
					num = ((!this.HasFlag(SqlParameter.SqlParameterFlags.IsNull) && !this.HasFlag(SqlParameter.SqlParameterFlags.CoercedValueIsDataFeed)) ? SqlParameter.StringSize(coercedValue, this.HasFlag(SqlParameter.SqlParameterFlags.CoercedValueIsSqlType)) : 0);
					this._actualSize = (this.ShouldSerializeSize() ? this.Size : 0);
					this._actualSize = ((this.ShouldSerializeSize() && this._actualSize <= num) ? this._actualSize : num);
					if (this._actualSize == -1)
					{
						this._actualSize = num;
						goto IL_02C7;
					}
					goto IL_02C7;
					IL_01F1:
					num = ((!this.HasFlag(SqlParameter.SqlParameterFlags.IsNull) && !this.HasFlag(SqlParameter.SqlParameterFlags.CoercedValueIsDataFeed)) ? SqlParameter.BinarySize(coercedValue, this.HasFlag(SqlParameter.SqlParameterFlags.CoercedValueIsSqlType)) : 0);
					this._actualSize = (this.ShouldSerializeSize() ? this.Size : 0);
					this._actualSize = ((this.ShouldSerializeSize() && this._actualSize <= num) ? this._actualSize : num);
					if (this._actualSize == -1)
					{
						this._actualSize = num;
					}
					IL_02C7:
					if (flag && num > 8000)
					{
						throw SQL.ParameterInvalidVariant(this.ParameterName);
					}
				}
			}
			return this._actualSize;
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x00023816 File Offset: 0x00021A16
		internal byte GetActualPrecision()
		{
			if (!this.ShouldSerializePrecision())
			{
				return this.ValuePrecision(this.CoercedValue);
			}
			return this.PrecisionInternal;
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x00023834 File Offset: 0x00021A34
		internal object GetCoercedValue()
		{
			if (this._coercedValue == null || this._internalMetaType.SqlDbType == SqlDbType.Udt)
			{
				bool flag = this.Value is DataFeed;
				if (this.IsNull || flag)
				{
					this._coercedValue = this.Value;
					this.SetFlag(SqlParameter.SqlParameterFlags.CoercedValueIsSqlType, this._coercedValue != null && this.HasFlag(SqlParameter.SqlParameterFlags.IsSqlParameterSqlType));
					this.SetFlag(SqlParameter.SqlParameterFlags.CoercedValueIsDataFeed, flag);
					this._actualSize = (this.IsNull ? 0 : (-1));
				}
				else
				{
					bool flag2;
					bool flag3;
					this._coercedValue = SqlParameter.CoerceValue(this.Value, this._internalMetaType, out flag2, out flag3, true);
					this.SetFlag(SqlParameter.SqlParameterFlags.CoercedValueIsDataFeed, flag2);
					this.SetFlag(SqlParameter.SqlParameterFlags.CoercedValueIsSqlType, this.HasFlag(SqlParameter.SqlParameterFlags.IsSqlParameterSqlType) && !flag3);
					this._actualSize = -1;
				}
			}
			return this._coercedValue;
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x00023901 File Offset: 0x00021B01
		internal int GetParameterSize()
		{
			if (!this.ShouldSerializeSize())
			{
				return this.ValueSize(this.CoercedValue);
			}
			return this.Size;
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x00023920 File Offset: 0x00021B20
		internal SmiParameterMetaData GetMetadataForTypeInfo()
		{
			if (this._internalMetaType == null)
			{
				this._internalMetaType = this.GetMetaTypeOnly();
			}
			ParameterPeekAheadValue parameterPeekAheadValue;
			return this.MetaDataForSmi(out parameterPeekAheadValue);
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x0002394C File Offset: 0x00021B4C
		internal SmiParameterMetaData MetaDataForSmi(out ParameterPeekAheadValue peekAhead)
		{
			peekAhead = null;
			MetaType metaType = this.ValidateTypeLengths();
			long num = (long)this.GetActualSize();
			long num2 = (long)this.Size;
			if (!metaType.IsLong)
			{
				if (metaType.SqlDbType == SqlDbType.NChar || metaType.SqlDbType == SqlDbType.NVarChar)
				{
					num /= 2L;
				}
				if (num > num2)
				{
					num2 = num;
				}
			}
			if (num2 == 0L)
			{
				if (metaType.SqlDbType == SqlDbType.Binary || metaType.SqlDbType == SqlDbType.VarBinary)
				{
					num2 = 8000L;
				}
				else if (metaType.SqlDbType == SqlDbType.Char || metaType.SqlDbType == SqlDbType.VarChar)
				{
					num2 = 8000L;
				}
				else if (metaType.SqlDbType == SqlDbType.NChar || metaType.SqlDbType == SqlDbType.NVarChar)
				{
					num2 = 4000L;
				}
			}
			else if ((num2 > 8000L && (SqlDbType.Binary == metaType.SqlDbType || SqlDbType.VarBinary == metaType.SqlDbType)) || (num2 > 8000L && (SqlDbType.Char == metaType.SqlDbType || SqlDbType.VarChar == metaType.SqlDbType)) || (num2 > 4000L && (SqlDbType.NChar == metaType.SqlDbType || SqlDbType.NVarChar == metaType.SqlDbType)))
			{
				num2 = -1L;
			}
			int num3 = this.LocaleId;
			if (num3 == 0 && metaType.IsCharType)
			{
				object obj = this.GetCoercedValue();
				if (obj is SqlString)
				{
					SqlString sqlString = (SqlString)obj;
					if (!sqlString.IsNull)
					{
						num3 = sqlString.LCID;
						goto IL_013E;
					}
				}
				num3 = CultureInfo.CurrentCulture.LCID;
			}
			IL_013E:
			SqlCompareOptions sqlCompareOptions = this.CompareInfo;
			if (sqlCompareOptions == SqlCompareOptions.None && metaType.IsCharType)
			{
				object obj = this.GetCoercedValue();
				if (obj is SqlString)
				{
					SqlString sqlString2 = (SqlString)obj;
					if (!sqlString2.IsNull)
					{
						sqlCompareOptions = sqlString2.SqlCompareOptions;
						goto IL_0192;
					}
				}
				sqlCompareOptions = SmiMetaData.GetDefaultForType(metaType.SqlDbType).CompareOptions;
			}
			IL_0192:
			string text = null;
			string text2 = null;
			string text3 = null;
			if (SqlDbType.Xml == metaType.SqlDbType)
			{
				text = this.XmlSchemaCollectionDatabase;
				text2 = this.XmlSchemaCollectionOwningSchema;
				text3 = this.XmlSchemaCollectionName;
			}
			else if (SqlDbType.Udt == metaType.SqlDbType || (SqlDbType.Structured == metaType.SqlDbType && !string.IsNullOrEmpty(this.TypeName)))
			{
				string[] array;
				if (metaType.SqlDbType == SqlDbType.Udt)
				{
					array = SqlParameter.ParseTypeName(this.UdtTypeName, true);
				}
				else
				{
					array = SqlParameter.ParseTypeName(this.TypeName, false);
				}
				if (array.Length == 1)
				{
					text3 = array[0];
				}
				else if (array.Length == 2)
				{
					text2 = array[0];
					text3 = array[1];
				}
				else
				{
					if (array.Length != 3)
					{
						throw ADP.ArgumentOutOfRange("names");
					}
					text = array[0];
					text2 = array[1];
					text3 = array[2];
				}
				if ((!string.IsNullOrEmpty(text) && 255 < text.Length) || (!string.IsNullOrEmpty(text2) && 255 < text2.Length) || (!string.IsNullOrEmpty(text3) && 255 < text3.Length))
				{
					throw ADP.ArgumentOutOfRange("names");
				}
			}
			byte b = this.GetActualPrecision();
			byte actualScale = this.GetActualScale();
			if (metaType.SqlDbType == SqlDbType.Decimal && b == 0)
			{
				b = 29;
			}
			List<SmiExtendedMetaData> list = null;
			SmiMetaDataPropertyCollection smiMetaDataPropertyCollection = null;
			if (metaType.SqlDbType == SqlDbType.Structured)
			{
				this.GetActualFieldsAndProperties(out list, out smiMetaDataPropertyCollection, out peekAhead);
			}
			return new SmiParameterMetaData(metaType.SqlDbType, num2, b, actualScale, (long)num3, sqlCompareOptions, null, SqlDbType.Structured == metaType.SqlDbType, list, smiMetaDataPropertyCollection, this.ParameterNameFixed, text, text2, text3, this.Direction);
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x0000BB08 File Offset: 0x00009D08
		[Conditional("DEBUG")]
		internal void AssertCachedPropertiesAreValid()
		{
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x0000BB08 File Offset: 0x00009D08
		[Conditional("DEBUG")]
		internal void AssertPropertiesAreValid(object value, bool? isSqlType = null, bool? isDataFeed = null, bool? isNull = null)
		{
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x00023C78 File Offset: 0x00021E78
		private SqlDbType GetMetaSqlDbTypeOnly()
		{
			MetaType metaType = this._metaType;
			if (metaType == null)
			{
				metaType = MetaType.GetDefaultMetaType();
			}
			return metaType.SqlDbType;
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x00023C9C File Offset: 0x00021E9C
		private MetaType GetMetaTypeOnly()
		{
			if (this._metaType != null)
			{
				return this._metaType;
			}
			if (this._value != null && DBNull.Value != this._value)
			{
				Type type = this._value.GetType();
				if (type == typeof(char))
				{
					this._value = this._value.ToString();
					type = typeof(string);
				}
				else if (type == typeof(char[]))
				{
					this._value = new string((char[])this._value);
					type = typeof(string);
				}
				return MetaType.GetMetaTypeFromType(type);
			}
			if (this._sqlBufferReturnValue != null)
			{
				Type typeFromStorageType = this._sqlBufferReturnValue.GetTypeFromStorageType(this.HasFlag(SqlParameter.SqlParameterFlags.IsSqlParameterSqlType));
				if (typeFromStorageType != null)
				{
					return MetaType.GetMetaTypeFromType(typeFromStorageType);
				}
			}
			return MetaType.GetDefaultMetaType();
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x00023D78 File Offset: 0x00021F78
		internal void Prepare(SqlCommand cmd)
		{
			if (this._metaType == null)
			{
				throw ADP.PrepareParameterType(cmd);
			}
			if (!this.ShouldSerializeSize() && !this._metaType.IsFixed)
			{
				throw ADP.PrepareParameterSize(cmd);
			}
			if (!this.ShouldSerializePrecision() && !this.ShouldSerializeScale() && this._metaType.SqlDbType == SqlDbType.Decimal)
			{
				throw ADP.PrepareParameterScale(cmd, this.SqlDbType.ToString());
			}
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x00023DE9 File Offset: 0x00021FE9
		private void PropertyChanging()
		{
			this._internalMetaType = null;
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x00023DF2 File Offset: 0x00021FF2
		private void PropertyTypeChanging()
		{
			this.PropertyChanging();
			this.CoercedValue = null;
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x00023E01 File Offset: 0x00022001
		internal void ResetParent()
		{
			this._parent = null;
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x00023E0A File Offset: 0x0002200A
		private void SetFlag(SqlParameter.SqlParameterFlags flag, bool value)
		{
			this._flags = (value ? (this._flags | flag) : (this._flags & ~flag));
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x00023E2C File Offset: 0x0002202C
		internal void SetSqlBuffer(SqlBuffer buff)
		{
			this._sqlBufferReturnValue = buff;
			this._value = null;
			this._coercedValue = null;
			this.SetFlag(SqlParameter.SqlParameterFlags.IsNull, this._sqlBufferReturnValue.IsNull);
			this.SetFlag(SqlParameter.SqlParameterFlags.CoercedValueIsDataFeed, false);
			this.SetFlag(SqlParameter.SqlParameterFlags.CoercedValueIsSqlType, false);
			this._udtLoadError = null;
			this._actualSize = -1;
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x00023E80 File Offset: 0x00022080
		internal void SetUdtLoadError(Exception e)
		{
			this._udtLoadError = e;
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x00023E8C File Offset: 0x0002208C
		internal void Validate(int index, bool isCommandProc)
		{
			MetaType metaTypeOnly = this.GetMetaTypeOnly();
			this._internalMetaType = metaTypeOnly;
			if (ADP.IsDirection(this, ParameterDirection.Output) && !ADP.IsDirection(this, ParameterDirection.ReturnValue) && !metaTypeOnly.IsFixed && !this.ShouldSerializeSize() && (this._value == null || Convert.IsDBNull(this._value)) && this.SqlDbType != SqlDbType.Timestamp && this.SqlDbType != SqlDbType.Udt && this.SqlDbType != SqlDbType.Xml && !metaTypeOnly.IsVarTime)
			{
				throw ADP.UninitializedParameterSize(index, metaTypeOnly.ClassType);
			}
			if (metaTypeOnly.SqlDbType != SqlDbType.Udt && this.Direction != ParameterDirection.Output)
			{
				this.GetCoercedValue();
			}
			if (metaTypeOnly.SqlDbType == SqlDbType.Udt)
			{
				if (string.IsNullOrEmpty(this.UdtTypeName))
				{
					throw SQL.MustSetUdtTypeNameForUdtParams();
				}
			}
			else if (!string.IsNullOrEmpty(this.UdtTypeName))
			{
				throw SQL.UnexpectedUdtTypeNameForNonUdtParams();
			}
			if (metaTypeOnly.SqlDbType == SqlDbType.Structured)
			{
				if (!isCommandProc && string.IsNullOrEmpty(this.TypeName))
				{
					throw SQL.MustSetTypeNameForParam(metaTypeOnly.TypeName, this.ParameterName);
				}
				if (this.Direction != ParameterDirection.Input)
				{
					throw SQL.UnsupportedTVPOutputParameter(this.Direction, this.ParameterName);
				}
				if (this.GetCoercedValue() == DBNull.Value)
				{
					throw SQL.DBNullNotSupportedForTVPValues(this.ParameterName);
				}
			}
			else if (!string.IsNullOrEmpty(this.TypeName))
			{
				throw SQL.UnexpectedTypeNameForNonStructParams(this.ParameterName);
			}
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x00023FD4 File Offset: 0x000221D4
		internal MetaType ValidateTypeLengths()
		{
			MetaType metaType = this.InternalMetaType;
			if (metaType.SqlDbType != SqlDbType.Udt && !metaType.IsFixed && !metaType.IsLong)
			{
				long num = (long)this.GetActualSize();
				long num2 = (long)this.Size;
				long num3;
				if (metaType.IsNCharType)
				{
					num3 = ((num2 * 2L > num) ? (num2 * 2L) : num);
				}
				else
				{
					num3 = ((num2 > num) ? num2 : num);
				}
				if (num3 > 8000L || this.HasFlag(SqlParameter.SqlParameterFlags.CoercedValueIsDataFeed) || num2 == -1L || num == -1L)
				{
					metaType = MetaType.GetMaxMetaTypeFromMetaType(metaType);
					this._metaType = metaType;
					this.InternalMetaType = metaType;
					if (!metaType.IsPlp)
					{
						if (metaType.SqlDbType == SqlDbType.Xml)
						{
							throw ADP.InvalidMetaDataValue();
						}
						if (metaType.SqlDbType == SqlDbType.NVarChar || metaType.SqlDbType == SqlDbType.VarChar || metaType.SqlDbType == SqlDbType.VarBinary)
						{
							this.Size = -1;
						}
					}
				}
			}
			return metaType;
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x000240B0 File Offset: 0x000222B0
		private byte ValuePrecision(object value)
		{
			if (!(value is SqlDecimal))
			{
				return this.ValuePrecisionCore(value);
			}
			SqlDecimal sqlDecimal = (SqlDecimal)value;
			if (sqlDecimal.IsNull)
			{
				return 0;
			}
			return sqlDecimal.Precision;
		}

		// Token: 0x06000C1F RID: 3103 RVA: 0x000240E8 File Offset: 0x000222E8
		private byte ValueScale(object value)
		{
			if (!(value is SqlDecimal))
			{
				return this.ValueScaleCore(value);
			}
			SqlDecimal sqlDecimal = (SqlDecimal)value;
			if (sqlDecimal.IsNull)
			{
				return 0;
			}
			return sqlDecimal.Scale;
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x00024120 File Offset: 0x00022320
		private int ValueSize(object value)
		{
			if (value is SqlString)
			{
				SqlString sqlString = (SqlString)value;
				if (sqlString.IsNull)
				{
					return 0;
				}
				return sqlString.Value.Length;
			}
			else
			{
				SqlChars sqlChars = value as SqlChars;
				if (sqlChars != null)
				{
					if (sqlChars.IsNull)
					{
						return 0;
					}
					return sqlChars.Value.Length;
				}
				else if (value is SqlBinary)
				{
					SqlBinary sqlBinary = (SqlBinary)value;
					if (sqlBinary.IsNull)
					{
						return 0;
					}
					return sqlBinary.Length;
				}
				else
				{
					SqlBytes sqlBytes = value as SqlBytes;
					if (sqlBytes != null)
					{
						if (sqlBytes.IsNull)
						{
							return 0;
						}
						return (int)sqlBytes.Length;
					}
					else
					{
						if (value is DataFeed)
						{
							return 0;
						}
						return this.ValueSizeCore(value);
					}
				}
			}
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x000241C0 File Offset: 0x000223C0
		private byte ValuePrecisionCore(object value)
		{
			if (value is decimal)
			{
				decimal num = (decimal)value;
				return num.Precision;
			}
			return 0;
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x000241EC File Offset: 0x000223EC
		private byte ValueScaleCore(object value)
		{
			if (value is decimal)
			{
				decimal num = (decimal)value;
				return (byte)((decimal.GetBits(num)[3] & 16711680) >> 16);
			}
			return 0;
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x0002421C File Offset: 0x0002241C
		private int ValueSizeCore(object value)
		{
			if (!ADP.IsNull(value))
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

		// Token: 0x06000C24 RID: 3108 RVA: 0x00024274 File Offset: 0x00022474
		internal static object CoerceValue(object value, MetaType destinationType, out bool coercedToDataFeed, out bool typeChanged, bool allowStreaming = true)
		{
			coercedToDataFeed = false;
			typeChanged = false;
			Type type = value.GetType();
			if (destinationType.ClassType != typeof(object) && destinationType.ClassType != type && (destinationType.SqlType != type || destinationType.SqlDbType == SqlDbType.Xml))
			{
				try
				{
					typeChanged = true;
					if (destinationType.ClassType == typeof(string))
					{
						if (type == typeof(SqlXml))
						{
							value = MetaType.GetStringFromXml(((SqlXml)value).CreateReader());
						}
						else if (type == typeof(SqlString))
						{
							typeChanged = false;
						}
						else if (typeof(XmlReader).IsAssignableFrom(type))
						{
							if (allowStreaming)
							{
								coercedToDataFeed = true;
								value = new XmlDataFeed((XmlReader)value);
							}
							else
							{
								value = MetaType.GetStringFromXml((XmlReader)value);
							}
						}
						else if (type == typeof(char[]))
						{
							value = new string((char[])value);
						}
						else if (type == typeof(SqlChars))
						{
							value = new string(((SqlChars)value).Value);
						}
						else
						{
							TextReader textReader = value as TextReader;
							if (textReader != null && allowStreaming)
							{
								coercedToDataFeed = true;
								value = new TextDataFeed(textReader);
							}
							else
							{
								value = Convert.ChangeType(value, destinationType.ClassType, null);
							}
						}
					}
					else if (destinationType.DbType == DbType.Currency && type == typeof(string))
					{
						value = decimal.Parse((string)value, NumberStyles.Currency, null);
					}
					else if (type == typeof(SqlBytes) && destinationType.ClassType == typeof(byte[]))
					{
						typeChanged = false;
					}
					else if (type == typeof(string) && destinationType.SqlDbType == SqlDbType.Time)
					{
						value = TimeSpan.Parse((string)value);
					}
					else if (type == typeof(string) && destinationType.SqlDbType == SqlDbType.DateTimeOffset)
					{
						value = DateTimeOffset.Parse((string)value, null);
					}
					else if (type == typeof(DateTime) && destinationType.SqlDbType == SqlDbType.DateTimeOffset)
					{
						value = new DateTimeOffset((DateTime)value);
					}
					else if (243 == destinationType.TDSType && (value is DataTable || value is DbDataReader || value is IEnumerable<SqlDataRecord>))
					{
						typeChanged = false;
					}
					else
					{
						if (destinationType.ClassType == typeof(byte[]) && allowStreaming)
						{
							Stream stream = value as Stream;
							if (stream != null)
							{
								coercedToDataFeed = true;
								value = new StreamDataFeed(stream);
								goto IL_02DC;
							}
						}
						value = Convert.ChangeType(value, destinationType.ClassType, null);
					}
					IL_02DC:;
				}
				catch (Exception ex)
				{
					if (!ADP.IsCatchableExceptionType(ex))
					{
						throw;
					}
					throw ADP.ParameterConversionFailed(value, destinationType.ClassType, ex);
				}
			}
			return value;
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x00024598 File Offset: 0x00022798
		private static int StringSize(object value, bool isSqlType)
		{
			if (isSqlType)
			{
				if (value is SqlString)
				{
					return ((SqlString)value).Value.Length;
				}
				SqlChars sqlChars = value as SqlChars;
				if (sqlChars != null)
				{
					return sqlChars.Value.Length;
				}
			}
			else
			{
				string text = value as string;
				if (text != null)
				{
					return text.Length;
				}
				char[] array = value as char[];
				if (array != null)
				{
					return array.Length;
				}
				if (value is char)
				{
					return 1;
				}
			}
			return 0;
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x00024604 File Offset: 0x00022804
		private static int BinarySize(object value, bool isSqlType)
		{
			if (isSqlType)
			{
				if (value is SqlBinary)
				{
					return ((SqlBinary)value).Length;
				}
				SqlBytes sqlBytes = value as SqlBytes;
				if (sqlBytes != null)
				{
					return sqlBytes.Value.Length;
				}
			}
			else
			{
				byte[] array = value as byte[];
				if (array != null)
				{
					return array.Length;
				}
				if (value is byte)
				{
					return 1;
				}
			}
			return 0;
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x00024658 File Offset: 0x00022858
		internal static string[] ParseTypeName(string typeName, bool isUdtTypeName)
		{
			string[] array;
			try
			{
				string text = (isUdtTypeName ? Strings.SQL_UDTTypeName : Strings.SQL_TypeName);
				array = MultipartIdentifier.ParseMultipartIdentifier(typeName, "[\"", "]\"", '.', 3, true, text, true);
			}
			catch (ArgumentException)
			{
				if (isUdtTypeName)
				{
					throw SQL.InvalidUdt3PartNameFormat();
				}
				throw SQL.InvalidParameterTypeNameFormat();
			}
			return array;
		}

		// Token: 0x04000303 RID: 771
		private MetaType _metaType;

		// Token: 0x04000304 RID: 772
		private SqlCollation _collation;

		// Token: 0x04000305 RID: 773
		private SqlMetaDataXmlSchemaCollection _xmlSchemaCollection;

		// Token: 0x04000306 RID: 774
		private string _udtTypeName;

		// Token: 0x04000307 RID: 775
		private string _typeName;

		// Token: 0x04000308 RID: 776
		private Exception _udtLoadError;

		// Token: 0x04000309 RID: 777
		private string _parameterName;

		// Token: 0x0400030A RID: 778
		private byte _precision;

		// Token: 0x0400030B RID: 779
		private byte _scale;

		// Token: 0x0400030C RID: 780
		private MetaType _internalMetaType;

		// Token: 0x0400030D RID: 781
		private SqlBuffer _sqlBufferReturnValue;

		// Token: 0x0400030E RID: 782
		private INullable _valueAsINullable;

		// Token: 0x0400030F RID: 783
		private int _actualSize;

		// Token: 0x04000310 RID: 784
		private object _value;

		// Token: 0x04000311 RID: 785
		private object _coercedValue;

		// Token: 0x04000312 RID: 786
		private object _parent;

		// Token: 0x04000313 RID: 787
		private ParameterDirection _direction;

		// Token: 0x04000314 RID: 788
		private int _size;

		// Token: 0x04000315 RID: 789
		private int _offset;

		// Token: 0x04000316 RID: 790
		private string _sourceColumn;

		// Token: 0x04000317 RID: 791
		private DataRowVersion _sourceVersion;

		// Token: 0x04000318 RID: 792
		private SqlParameter.SqlParameterFlags _flags;

		// Token: 0x020001D2 RID: 466
		internal sealed class SqlParameterConverter : ExpandableObjectConverter
		{
			// Token: 0x06001DC2 RID: 7618 RVA: 0x0007A348 File Offset: 0x00078548
			public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
			{
				return typeof(InstanceDescriptor) == destinationType || base.CanConvertTo(context, destinationType);
			}

			// Token: 0x06001DC3 RID: 7619 RVA: 0x0007A928 File Offset: 0x00078B28
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				if (destinationType == null)
				{
					throw ADP.ArgumentNull("destinationType");
				}
				if (typeof(InstanceDescriptor) == destinationType && value is SqlParameter)
				{
					return this.ConvertToInstanceDescriptor(value as SqlParameter);
				}
				return base.ConvertTo(context, culture, value, destinationType);
			}

			// Token: 0x06001DC4 RID: 7620 RVA: 0x0007A980 File Offset: 0x00078B80
			private InstanceDescriptor ConvertToInstanceDescriptor(SqlParameter p)
			{
				int num = 0;
				if (p.ShouldSerializeSqlDbType())
				{
					num |= 1;
				}
				if (p.ShouldSerializeSize())
				{
					num |= 2;
				}
				if (!string.IsNullOrEmpty(p.SourceColumn))
				{
					num |= 4;
				}
				if (p.Value != null)
				{
					num |= 8;
				}
				if (ParameterDirection.Input != p.Direction || p.IsNullable || p.ShouldSerializePrecision() || p.ShouldSerializeScale() || DataRowVersion.Current != p.SourceVersion)
				{
					num |= 16;
				}
				if (p.SourceColumnNullMapping || !string.IsNullOrEmpty(p.XmlSchemaCollectionDatabase) || !string.IsNullOrEmpty(p.XmlSchemaCollectionOwningSchema) || !string.IsNullOrEmpty(p.XmlSchemaCollectionName))
				{
					num |= 32;
				}
				Type[] array;
				object[] array2;
				switch (num)
				{
				case 0:
				case 1:
					array = new Type[]
					{
						typeof(string),
						typeof(SqlDbType)
					};
					array2 = new object[] { p.ParameterName, p.SqlDbType };
					break;
				case 2:
				case 3:
					array = new Type[]
					{
						typeof(string),
						typeof(SqlDbType),
						typeof(int)
					};
					array2 = new object[] { p.ParameterName, p.SqlDbType, p.Size };
					break;
				case 4:
				case 5:
				case 6:
				case 7:
					array = new Type[]
					{
						typeof(string),
						typeof(SqlDbType),
						typeof(int),
						typeof(string)
					};
					array2 = new object[] { p.ParameterName, p.SqlDbType, p.Size, p.SourceColumn };
					break;
				case 8:
					array = new Type[]
					{
						typeof(string),
						typeof(object)
					};
					array2 = new object[] { p.ParameterName, p.Value };
					break;
				default:
					if ((32 & num) == 0)
					{
						array = new Type[]
						{
							typeof(string),
							typeof(SqlDbType),
							typeof(int),
							typeof(ParameterDirection),
							typeof(bool),
							typeof(byte),
							typeof(byte),
							typeof(string),
							typeof(DataRowVersion),
							typeof(object)
						};
						array2 = new object[] { p.ParameterName, p.SqlDbType, p.Size, p.Direction, p.IsNullable, p.PrecisionInternal, p.ScaleInternal, p.SourceColumn, p.SourceVersion, p.Value };
					}
					else
					{
						array = new Type[]
						{
							typeof(string),
							typeof(SqlDbType),
							typeof(int),
							typeof(ParameterDirection),
							typeof(byte),
							typeof(byte),
							typeof(string),
							typeof(DataRowVersion),
							typeof(bool),
							typeof(object),
							typeof(string),
							typeof(string),
							typeof(string)
						};
						array2 = new object[]
						{
							p.ParameterName, p.SqlDbType, p.Size, p.Direction, p.PrecisionInternal, p.ScaleInternal, p.SourceColumn, p.SourceVersion, p.SourceColumnNullMapping, p.Value,
							p.XmlSchemaCollectionDatabase, p.XmlSchemaCollectionOwningSchema, p.XmlSchemaCollectionName
						};
					}
					break;
				}
				ConstructorInfo constructor = typeof(SqlParameter).GetConstructor(array);
				return new InstanceDescriptor(constructor, array2);
			}
		}

		// Token: 0x020001D3 RID: 467
		[Flags]
		private enum SqlParameterFlags : ushort
		{
			// Token: 0x04001424 RID: 5156
			None = 0,
			// Token: 0x04001425 RID: 5157
			IsNull = 1,
			// Token: 0x04001426 RID: 5158
			IsNullable = 2,
			// Token: 0x04001427 RID: 5159
			IsSqlParameterSqlType = 4,
			// Token: 0x04001428 RID: 5160
			SourceColumnNullMapping = 8,
			// Token: 0x04001429 RID: 5161
			CoercedValueIsSqlType = 16,
			// Token: 0x0400142A RID: 5162
			CoercedValueIsDataFeed = 32,
			// Token: 0x0400142B RID: 5163
			HasReceivedMetadata = 64,
			// Token: 0x0400142C RID: 5164
			ForceColumnEncryption = 128,
			// Token: 0x0400142D RID: 5165
			IsDerivedParameterTypeName = 256,
			// Token: 0x0400142E RID: 5166
			HasScale = 512
		}
	}
}
