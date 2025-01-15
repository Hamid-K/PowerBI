using System;
using System.Data;
using System.Data.Common;
using Microsoft.HostIntegration.Drda.Requester;

namespace Microsoft.HostIntegration.DrdaClient
{
	// Token: 0x020009FC RID: 2556
	public class DrdaParameter : DbParameter, ICloneable, IDbDataParameter, IDataParameter
	{
		// Token: 0x06005038 RID: 20536 RVA: 0x00141AF0 File Offset: 0x0013FCF0
		public DrdaParameter()
		{
			Trace.ApiEnterTrace("DrdaParameter()");
		}

		// Token: 0x06005039 RID: 20537 RVA: 0x00141B14 File Offset: 0x0013FD14
		public DrdaParameter(string name, object value)
			: this()
		{
			Trace.ApiEnterTrace("DrdaParameter(string, object)");
			this.ParameterName = name;
			this.Value = value;
		}

		// Token: 0x0600503A RID: 20538 RVA: 0x00141B34 File Offset: 0x0013FD34
		public DrdaParameter(string name, DrdaType drdaType)
			: this()
		{
			Trace.ApiEnterTrace("DrdaParameter(string, DrdaType)");
			this.ParameterName = name;
			this.DrdaType = drdaType;
		}

		// Token: 0x0600503B RID: 20539 RVA: 0x00141B54 File Offset: 0x0013FD54
		public DrdaParameter(string name, DrdaType drdaType, int size)
			: this()
		{
			Trace.ApiEnterTrace("DrdaParameter(string, DrdaType, int)");
			this.ParameterName = name;
			this.DrdaType = drdaType;
			this.Size = size;
		}

		// Token: 0x0600503C RID: 20540 RVA: 0x00141B7B File Offset: 0x0013FD7B
		public DrdaParameter(string name, DrdaType drdaType, int size, string srcColumn)
			: this()
		{
			Trace.ApiEnterTrace("DrdaParameter(string, DrdaType, int, string)");
			this.ParameterName = name;
			this.DrdaType = drdaType;
			this.Size = size;
			this.SourceColumn = srcColumn;
		}

		// Token: 0x0600503D RID: 20541 RVA: 0x00141BAC File Offset: 0x0013FDAC
		public DrdaParameter(string name, DrdaType drdaType, int size, ParameterDirection direction, bool isNullable, byte precision, byte scale, string srcColumn, DataRowVersion srcVersion, object value)
			: this()
		{
			Trace.ApiEnterTrace("DrdaParameter(string, DrdaType, int, ParameterDirection, bool, byte, vyte, string, DataRowVersion, object)");
			this.ParameterName = name;
			this.DrdaType = drdaType;
			this.Size = size;
			this.Direction = direction;
			this.IsNullable = isNullable;
			this.Precision = precision;
			this.Scale = scale;
			this.SourceColumn = srcColumn;
			this.SourceVersion = srcVersion;
			this.Value = value;
		}

		// Token: 0x0600503E RID: 20542 RVA: 0x00141C18 File Offset: 0x0013FE18
		public DrdaParameter(string name, DrdaType drdaType, int size, ParameterDirection direction, string sourceColumn, DataRowVersion sourceVersion, bool sourceColumnNullMapping, object value)
			: this()
		{
			this.ParameterName = name;
			this.DrdaType = drdaType;
			this.Size = size;
			this.Direction = direction;
			this.SourceColumn = sourceColumn;
			this.SourceVersion = sourceVersion;
			this.SourceColumnNullMapping = sourceColumnNullMapping;
			this.Value = value;
		}

		// Token: 0x0600503F RID: 20543 RVA: 0x00141C68 File Offset: 0x0013FE68
		private DrdaParameter(DrdaParameter source)
		{
			source.CopyToParameter(this);
		}

		// Token: 0x17001377 RID: 4983
		// (get) Token: 0x06005040 RID: 20544 RVA: 0x00141C89 File Offset: 0x0013FE89
		// (set) Token: 0x06005041 RID: 20545 RVA: 0x00141C91 File Offset: 0x0013FE91
		internal DrdaParameterCollection Parent
		{
			get
			{
				return this._parent;
			}
			set
			{
				this._parent = value;
			}
		}

		// Token: 0x17001378 RID: 4984
		// (get) Token: 0x06005042 RID: 20546 RVA: 0x00141C9A File Offset: 0x0013FE9A
		internal bool IsDataTypeSet
		{
			get
			{
				return this._isDataTypeSet;
			}
		}

		// Token: 0x17001379 RID: 4985
		// (get) Token: 0x06005043 RID: 20547 RVA: 0x00141CA4 File Offset: 0x0013FEA4
		internal DrdaParameterBinding Binding
		{
			get
			{
				if (this._binding == null)
				{
					this._binding = new DrdaParameterBinding();
					if (this.Value != null && !Convert.IsDBNull(this.Value))
					{
						this._binding.Type = DrdaMetaType.GetMetaTypeForObject(this.Value);
					}
				}
				return this._binding;
			}
		}

		// Token: 0x1700137A RID: 4986
		// (get) Token: 0x06005044 RID: 20548 RVA: 0x00141CF5 File Offset: 0x0013FEF5
		// (set) Token: 0x06005045 RID: 20549 RVA: 0x00141CFD File Offset: 0x0013FEFD
		public override ParameterDirection Direction
		{
			get
			{
				return this._direction;
			}
			set
			{
				if (value != this._direction)
				{
					this._direction = value;
					this.PropertyChanging();
				}
			}
		}

		// Token: 0x1700137B RID: 4987
		// (get) Token: 0x06005046 RID: 20550 RVA: 0x00141D15 File Offset: 0x0013FF15
		// (set) Token: 0x06005047 RID: 20551 RVA: 0x00141D22 File Offset: 0x0013FF22
		public override bool IsNullable
		{
			get
			{
				return this.Binding.IsNullable;
			}
			set
			{
				this.Binding.IsNullable = value;
			}
		}

		// Token: 0x1700137C RID: 4988
		// (get) Token: 0x06005048 RID: 20552 RVA: 0x00141D30 File Offset: 0x0013FF30
		// (set) Token: 0x06005049 RID: 20553 RVA: 0x00141D3D File Offset: 0x0013FF3D
		public override string ParameterName
		{
			get
			{
				return this.Binding.Name;
			}
			set
			{
				this.Binding.Name = value;
			}
		}

		// Token: 0x1700137D RID: 4989
		// (get) Token: 0x0600504A RID: 20554 RVA: 0x00141D4B File Offset: 0x0013FF4B
		// (set) Token: 0x0600504B RID: 20555 RVA: 0x00141D53 File Offset: 0x0013FF53
		public override string SourceColumn
		{
			get
			{
				return this._sourceColumn;
			}
			set
			{
				if (value != this._sourceColumn)
				{
					this.PropertyChanging();
				}
				this._sourceColumn = value;
			}
		}

		// Token: 0x1700137E RID: 4990
		// (get) Token: 0x0600504C RID: 20556 RVA: 0x00141D70 File Offset: 0x0013FF70
		// (set) Token: 0x0600504D RID: 20557 RVA: 0x00141D78 File Offset: 0x0013FF78
		public override bool SourceColumnNullMapping
		{
			get
			{
				return this._sourceColumnNullMapping;
			}
			set
			{
				if (value != this._sourceColumnNullMapping)
				{
					this.PropertyChanging();
				}
				this._sourceColumnNullMapping = value;
			}
		}

		// Token: 0x1700137F RID: 4991
		// (get) Token: 0x0600504E RID: 20558 RVA: 0x00141D90 File Offset: 0x0013FF90
		// (set) Token: 0x0600504F RID: 20559 RVA: 0x00141D98 File Offset: 0x0013FF98
		public override DataRowVersion SourceVersion
		{
			get
			{
				return this._sourceVersion;
			}
			set
			{
				if (value != this._sourceVersion)
				{
					this.PropertyChanging();
				}
				this._sourceVersion = value;
			}
		}

		// Token: 0x17001380 RID: 4992
		// (get) Token: 0x06005050 RID: 20560 RVA: 0x00141DB0 File Offset: 0x0013FFB0
		// (set) Token: 0x06005051 RID: 20561 RVA: 0x00141DC4 File Offset: 0x0013FFC4
		public override DbType DbType
		{
			get
			{
				return this.Binding.Type.DbType;
			}
			set
			{
				DrdaMetaType type = this.Binding.Type;
				this.Binding.Type = DrdaMetaType.GetMetaTypeForType(value);
				if (!this.Binding.Type.Equals(type))
				{
					this.PropertyChanging();
				}
				this._isDataTypeSet = true;
			}
		}

		// Token: 0x17001381 RID: 4993
		// (get) Token: 0x06005052 RID: 20562 RVA: 0x00141E0E File Offset: 0x0014000E
		// (set) Token: 0x06005053 RID: 20563 RVA: 0x00141E28 File Offset: 0x00140028
		public DrdaType DrdaType
		{
			get
			{
				return DataTypeConverter.ToDrdaType(this.Binding.Type.DrdaType);
			}
			set
			{
				DrdaMetaType type = this.Binding.Type;
				this.Binding.Type = DrdaMetaType.GetMetaTypeForType(DataTypeConverter.ToDrdaClientType(value));
				if (!this.Binding.Type.Equals(type))
				{
					this.PropertyChanging();
				}
				this._isDataTypeSet = true;
			}
		}

		// Token: 0x17001382 RID: 4994
		// (get) Token: 0x06005054 RID: 20564 RVA: 0x00141E77 File Offset: 0x00140077
		// (set) Token: 0x06005055 RID: 20565 RVA: 0x00141E84 File Offset: 0x00140084
		public override int Size
		{
			get
			{
				return this.Binding.Size;
			}
			set
			{
				if (value != this.Binding.Size)
				{
					this.PropertyChanging();
				}
				this.Binding.Size = value;
			}
		}

		// Token: 0x17001383 RID: 4995
		// (get) Token: 0x06005056 RID: 20566 RVA: 0x00141EA6 File Offset: 0x001400A6
		// (set) Token: 0x06005057 RID: 20567 RVA: 0x00141EB3 File Offset: 0x001400B3
		public override object Value
		{
			get
			{
				return this.Binding.Value;
			}
			set
			{
				if ((this.DrdaType == DrdaType.Numeric || this.DrdaType == DrdaType.Decimal) && value != this.Binding.Value)
				{
					this.PropertyChanging();
				}
				this.Binding.Value = value;
			}
		}

		// Token: 0x17001384 RID: 4996
		// (get) Token: 0x06005058 RID: 20568 RVA: 0x00141EE8 File Offset: 0x001400E8
		// (set) Token: 0x06005059 RID: 20569 RVA: 0x00141EF6 File Offset: 0x001400F6
		public override byte Precision
		{
			get
			{
				return (byte)this.Binding.Precision;
			}
			set
			{
				if (value != (byte)this.Binding.Precision)
				{
					this.PropertyChanging();
				}
				this.Binding.Precision = (short)value;
			}
		}

		// Token: 0x17001385 RID: 4997
		// (get) Token: 0x0600505A RID: 20570 RVA: 0x00141F19 File Offset: 0x00140119
		// (set) Token: 0x0600505B RID: 20571 RVA: 0x00141F27 File Offset: 0x00140127
		public override byte Scale
		{
			get
			{
				return (byte)this.Binding.Scale;
			}
			set
			{
				if (value != (byte)this.Binding.Scale)
				{
					this.PropertyChanging();
				}
				this.Binding.Scale = (short)value;
			}
		}

		// Token: 0x0600505C RID: 20572 RVA: 0x00141F4A File Offset: 0x0014014A
		public override void ResetDbType()
		{
			Trace.ApiEnterTrace();
			this.PropertyChanging();
			this._binding = null;
			this._isDataTypeSet = false;
		}

		// Token: 0x0600505D RID: 20573 RVA: 0x00141F65 File Offset: 0x00140165
		object ICloneable.Clone()
		{
			return new DrdaParameter(this);
		}

		// Token: 0x0600505E RID: 20574 RVA: 0x00141F70 File Offset: 0x00140170
		private void CopyToParameter(DrdaParameter destination)
		{
			this.Binding.CopyValueTo(destination.Binding);
			destination.Direction = this._direction;
			destination.SourceColumn = this._sourceColumn;
			destination.SourceVersion = this._sourceVersion;
			destination.SourceColumnNullMapping = this._sourceColumnNullMapping;
			destination._isDataTypeSet = this._isDataTypeSet;
		}

		// Token: 0x0600505F RID: 20575 RVA: 0x00141FCA File Offset: 0x001401CA
		private void PropertyChanging()
		{
			if (this.Parent != null)
			{
				this.Parent.PropertyChanging();
			}
		}

		// Token: 0x06005060 RID: 20576 RVA: 0x00141FE0 File Offset: 0x001401E0
		internal object CompareExchangeParent(object value, object comparand)
		{
			object parent = this.Parent;
			if (comparand == parent)
			{
				this._parent = (DrdaParameterCollection)value;
			}
			return parent;
		}

		// Token: 0x06005061 RID: 20577 RVA: 0x00142005 File Offset: 0x00140205
		internal void ResetParent()
		{
			this._parent = null;
		}

		// Token: 0x06005062 RID: 20578 RVA: 0x0014200E File Offset: 0x0014020E
		public override string ToString()
		{
			Trace.ApiEnterTrace();
			return this.ParameterName;
		}

		// Token: 0x06005063 RID: 20579 RVA: 0x0014201C File Offset: 0x0014021C
		internal void Derive(ISqlParameter parameter)
		{
			this.Direction = parameter.Direction;
			this.Scale = parameter.Scale;
			this.Precision = parameter.Precision;
			this.Size = parameter.Size;
			this.IsNullable = parameter.IsNullable;
			this.DrdaType = DataTypeConverter.ToDrdaType(parameter.DrdaType);
		}

		// Token: 0x06005064 RID: 20580 RVA: 0x00142078 File Offset: 0x00140278
		internal SqlParameter ToSqlParameter()
		{
			SqlParameter sqlParameter = new SqlParameter();
			this.CopyToParameter(sqlParameter);
			return sqlParameter;
		}

		// Token: 0x04003F56 RID: 16214
		private DrdaParameterBinding _binding;

		// Token: 0x04003F57 RID: 16215
		private DrdaParameterCollection _parent;

		// Token: 0x04003F58 RID: 16216
		private ParameterDirection _direction = ParameterDirection.Input;

		// Token: 0x04003F59 RID: 16217
		private string _sourceColumn;

		// Token: 0x04003F5A RID: 16218
		private DataRowVersion _sourceVersion = DataRowVersion.Current;

		// Token: 0x04003F5B RID: 16219
		private bool _sourceColumnNullMapping;

		// Token: 0x04003F5C RID: 16220
		private bool _isDataTypeSet;
	}
}
