using System;
using Microsoft.HostIntegration.Drda.Requester;

namespace Microsoft.HostIntegration.DrdaClient
{
	// Token: 0x020009A8 RID: 2472
	internal abstract class DrdaBinding
	{
		// Token: 0x06004C83 RID: 19587 RVA: 0x001324DC File Offset: 0x001306DC
		public DrdaBinding()
		{
		}

		// Token: 0x06004C84 RID: 19588 RVA: 0x001324FC File Offset: 0x001306FC
		protected bool InferProperties(bool UseHIS2013Constants)
		{
			bool flag = true;
			DrdaMetaType drdaMetaType;
			if (!this._isValueSet || this._value == null)
			{
				drdaMetaType = this._type;
			}
			else if (this._value is DBNull)
			{
				drdaMetaType = this._type;
			}
			else
			{
				if (this._isTypeSet)
				{
					drdaMetaType = DrdaMetaType.GetMetaTypeForType(this.Type.DrdaType);
				}
				else
				{
					drdaMetaType = DrdaMetaType.GetMetaTypeForObject(this._value);
				}
				if (this._isTypeSet && drdaMetaType.ClientType != this._type.ClientType)
				{
					flag = false;
				}
			}
			if (drdaMetaType.IsFixed && drdaMetaType.FixedLength > 0)
			{
				if (!this._isPrecisionSet)
				{
					byte precision = drdaMetaType.Precision;
				}
				else
				{
					short precision2 = this._precision;
				}
			}
			else if (drdaMetaType.DrdaType == DrdaClientType.Decimal || drdaMetaType.DrdaType == DrdaClientType.Numeric)
			{
				if (!this._isPrecisionSet)
				{
					byte precision3 = drdaMetaType.Precision;
				}
				else
				{
					short precision4 = this._precision;
				}
			}
			else
			{
				if (this._value != null && this._value is string)
				{
					flag = false;
				}
				short precision5 = this._precision;
			}
			return flag;
		}

		// Token: 0x17001283 RID: 4739
		// (get) Token: 0x06004C85 RID: 19589 RVA: 0x001325F5 File Offset: 0x001307F5
		// (set) Token: 0x06004C86 RID: 19590 RVA: 0x001325FD File Offset: 0x001307FD
		public short Precision
		{
			get
			{
				return this._precision;
			}
			set
			{
				this._isPrecisionSet = true;
				this._precision = value;
			}
		}

		// Token: 0x17001284 RID: 4740
		// (get) Token: 0x06004C87 RID: 19591 RVA: 0x0013260D File Offset: 0x0013080D
		// (set) Token: 0x06004C88 RID: 19592 RVA: 0x00132615 File Offset: 0x00130815
		public short Scale
		{
			get
			{
				return this._scale;
			}
			set
			{
				this._isScaleSet = true;
				this._scale = value;
			}
		}

		// Token: 0x17001285 RID: 4741
		// (get) Token: 0x06004C89 RID: 19593 RVA: 0x00132625 File Offset: 0x00130825
		// (set) Token: 0x06004C8A RID: 19594 RVA: 0x0013262D File Offset: 0x0013082D
		public int Size
		{
			get
			{
				return this._size;
			}
			set
			{
				this._isSizeSet = true;
				this._size = value;
			}
		}

		// Token: 0x17001286 RID: 4742
		// (get) Token: 0x06004C8B RID: 19595 RVA: 0x0013263D File Offset: 0x0013083D
		// (set) Token: 0x06004C8C RID: 19596 RVA: 0x00132645 File Offset: 0x00130845
		public DrdaMetaType Type
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
				this._isTypeSet = true;
			}
		}

		// Token: 0x17001287 RID: 4743
		// (get) Token: 0x06004C8D RID: 19597 RVA: 0x00132655 File Offset: 0x00130855
		// (set) Token: 0x06004C8E RID: 19598 RVA: 0x0013265D File Offset: 0x0013085D
		public bool IsNullable
		{
			get
			{
				return this._nullable;
			}
			set
			{
				this._nullable = value;
			}
		}

		// Token: 0x17001288 RID: 4744
		// (get) Token: 0x06004C8F RID: 19599 RVA: 0x00132666 File Offset: 0x00130866
		// (set) Token: 0x06004C90 RID: 19600 RVA: 0x0013266E File Offset: 0x0013086E
		public bool IsLob
		{
			get
			{
				return this._isLob;
			}
			set
			{
				this._isLob = value;
			}
		}

		// Token: 0x17001289 RID: 4745
		// (get) Token: 0x06004C91 RID: 19601 RVA: 0x00132677 File Offset: 0x00130877
		// (set) Token: 0x06004C92 RID: 19602 RVA: 0x0013267F File Offset: 0x0013087F
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		// Token: 0x1700128A RID: 4746
		// (get) Token: 0x06004C93 RID: 19603 RVA: 0x00132688 File Offset: 0x00130888
		// (set) Token: 0x06004C94 RID: 19604 RVA: 0x00132690 File Offset: 0x00130890
		public object Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._isValueSet = true;
				this._value = value;
			}
		}

		// Token: 0x06004C95 RID: 19605 RVA: 0x001326A0 File Offset: 0x001308A0
		public void CopyValueTo(DrdaBinding destination)
		{
			destination._name = this._name;
			destination._value = this._value;
			destination._isValueSet = this._isValueSet;
			destination._type = this._type;
			destination._isTypeSet = this._isTypeSet;
			destination._precision = this._precision;
			destination._isPrecisionSet = this._isPrecisionSet;
			destination._scale = this._scale;
			destination._isScaleSet = this._isScaleSet;
			destination._size = this._size;
			destination._isSizeSet = this._isSizeSet;
			destination._nullable = this._nullable;
			destination._isLob = this._isLob;
		}

		// Token: 0x04003C92 RID: 15506
		protected string _name = string.Empty;

		// Token: 0x04003C93 RID: 15507
		protected object _value;

		// Token: 0x04003C94 RID: 15508
		protected bool _isValueSet;

		// Token: 0x04003C95 RID: 15509
		protected DrdaMetaType _type = DrdaMetaType.GetDefaultMetaType();

		// Token: 0x04003C96 RID: 15510
		protected bool _isTypeSet;

		// Token: 0x04003C97 RID: 15511
		protected short _precision;

		// Token: 0x04003C98 RID: 15512
		protected bool _isPrecisionSet;

		// Token: 0x04003C99 RID: 15513
		protected short _scale;

		// Token: 0x04003C9A RID: 15514
		protected bool _isScaleSet;

		// Token: 0x04003C9B RID: 15515
		protected int _size;

		// Token: 0x04003C9C RID: 15516
		protected bool _isSizeSet;

		// Token: 0x04003C9D RID: 15517
		protected bool _nullable;

		// Token: 0x04003C9E RID: 15518
		protected bool _isLob;
	}
}
