using System;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x02000805 RID: 2053
	public class DrdaParameterInfo
	{
		// Token: 0x17000F1E RID: 3870
		// (get) Token: 0x060040DD RID: 16605 RVA: 0x000DC9AF File Offset: 0x000DABAF
		// (set) Token: 0x060040DE RID: 16606 RVA: 0x000DC9B7 File Offset: 0x000DABB7
		public short InOutType
		{
			get
			{
				return this._inOutType;
			}
			set
			{
				this._inOutType = value;
			}
		}

		// Token: 0x17000F1F RID: 3871
		// (get) Token: 0x060040DF RID: 16607 RVA: 0x000DC9C0 File Offset: 0x000DABC0
		// (set) Token: 0x060040E0 RID: 16608 RVA: 0x000DC9C8 File Offset: 0x000DABC8
		public string Clob
		{
			get
			{
				return this._clob;
			}
			set
			{
				this._clob = value;
			}
		}

		// Token: 0x17000F20 RID: 3872
		// (get) Token: 0x060040E1 RID: 16609 RVA: 0x000DC9D1 File Offset: 0x000DABD1
		// (set) Token: 0x060040E2 RID: 16610 RVA: 0x000DC9D9 File Offset: 0x000DABD9
		public bool IsClob
		{
			get
			{
				return this._isClob;
			}
			set
			{
				this._isClob = value;
			}
		}

		// Token: 0x17000F21 RID: 3873
		// (get) Token: 0x060040E3 RID: 16611 RVA: 0x000DC9E2 File Offset: 0x000DABE2
		// (set) Token: 0x060040E4 RID: 16612 RVA: 0x000DC9EA File Offset: 0x000DABEA
		public int LobPosition
		{
			get
			{
				return this._lobPosition;
			}
			set
			{
				this._lobPosition = value;
			}
		}

		// Token: 0x17000F22 RID: 3874
		// (get) Token: 0x060040E5 RID: 16613 RVA: 0x000DC9F3 File Offset: 0x000DABF3
		// (set) Token: 0x060040E6 RID: 16614 RVA: 0x000DC9FB File Offset: 0x000DABFB
		public int LobLength
		{
			get
			{
				return this._lobLength;
			}
			set
			{
				this._lobLength = value;
			}
		}

		// Token: 0x17000F23 RID: 3875
		// (get) Token: 0x060040E7 RID: 16615 RVA: 0x000DCA04 File Offset: 0x000DAC04
		// (set) Token: 0x060040E8 RID: 16616 RVA: 0x000DCA0C File Offset: 0x000DAC0C
		public byte[] Blob
		{
			get
			{
				return this._lob;
			}
			set
			{
				this._lob = value;
			}
		}

		// Token: 0x17000F24 RID: 3876
		// (get) Token: 0x060040E9 RID: 16617 RVA: 0x000DCA15 File Offset: 0x000DAC15
		// (set) Token: 0x060040EA RID: 16618 RVA: 0x000DCA1D File Offset: 0x000DAC1D
		public ushort Precision
		{
			get
			{
				return this._precision;
			}
			set
			{
				this._precision = value;
			}
		}

		// Token: 0x17000F25 RID: 3877
		// (get) Token: 0x060040EB RID: 16619 RVA: 0x000DCA26 File Offset: 0x000DAC26
		// (set) Token: 0x060040EC RID: 16620 RVA: 0x000DCA2E File Offset: 0x000DAC2E
		public ushort Scale
		{
			get
			{
				return this._scale;
			}
			set
			{
				this._scale = value;
			}
		}

		// Token: 0x17000F26 RID: 3878
		// (get) Token: 0x060040ED RID: 16621 RVA: 0x000DCA37 File Offset: 0x000DAC37
		// (set) Token: 0x060040EE RID: 16622 RVA: 0x000DCA3F File Offset: 0x000DAC3F
		public short SqlType { get; set; }

		// Token: 0x17000F27 RID: 3879
		// (get) Token: 0x060040EF RID: 16623 RVA: 0x000DCA48 File Offset: 0x000DAC48
		// (set) Token: 0x060040F0 RID: 16624 RVA: 0x000DCA50 File Offset: 0x000DAC50
		public object Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x060040F1 RID: 16625 RVA: 0x000DCA59 File Offset: 0x000DAC59
		public DrdaParameterInfo(byte type, ushort length)
		{
			this._type = type;
			this._length = length;
			this.OriginalDateTimeString = null;
		}

		// Token: 0x060040F2 RID: 16626 RVA: 0x000DCA76 File Offset: 0x000DAC76
		public DrdaParameterInfo(byte type, short inOutType, ushort length, ushort precision, ushort scale, object value)
			: this(type, length)
		{
			this._precision = precision;
			this._scale = scale;
			this._value = value;
			this._inOutType = inOutType;
		}

		// Token: 0x17000F28 RID: 3880
		// (get) Token: 0x060040F3 RID: 16627 RVA: 0x000DCA9F File Offset: 0x000DAC9F
		// (set) Token: 0x060040F4 RID: 16628 RVA: 0x000DCAA7 File Offset: 0x000DACA7
		public byte Type
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
			}
		}

		// Token: 0x17000F29 RID: 3881
		// (get) Token: 0x060040F5 RID: 16629 RVA: 0x000DCAB0 File Offset: 0x000DACB0
		// (set) Token: 0x060040F6 RID: 16630 RVA: 0x000DCAB8 File Offset: 0x000DACB8
		public ushort Length
		{
			get
			{
				return this._length;
			}
			set
			{
				this._length = value;
			}
		}

		// Token: 0x17000F2A RID: 3882
		// (get) Token: 0x060040F7 RID: 16631 RVA: 0x000DCAC1 File Offset: 0x000DACC1
		// (set) Token: 0x060040F8 RID: 16632 RVA: 0x000DCAC9 File Offset: 0x000DACC9
		public bool MDDoverride { get; set; }

		// Token: 0x17000F2B RID: 3883
		// (get) Token: 0x060040F9 RID: 16633 RVA: 0x000DCAD2 File Offset: 0x000DACD2
		// (set) Token: 0x060040FA RID: 16634 RVA: 0x000DCADA File Offset: 0x000DACDA
		public short CCSID { get; set; }

		// Token: 0x17000F2C RID: 3884
		// (get) Token: 0x060040FB RID: 16635 RVA: 0x000DCAE3 File Offset: 0x000DACE3
		// (set) Token: 0x060040FC RID: 16636 RVA: 0x000DCAEB File Offset: 0x000DACEB
		public string OriginalDateTimeString { get; set; }

		// Token: 0x060040FD RID: 16637 RVA: 0x000DCAF4 File Offset: 0x000DACF4
		public DrdaParameterInfo Clone()
		{
			return new DrdaParameterInfo(this._type, this._length)
			{
				_clob = this._clob,
				_isClob = this._isClob,
				_lob = this._lob,
				_lobLength = this._lobLength,
				_lobPosition = this._lobPosition,
				_precision = this._precision,
				_scale = this._scale,
				_value = this._value,
				CCSID = this.CCSID,
				MDDoverride = this.MDDoverride,
				OriginalDateTimeString = this.OriginalDateTimeString
			};
		}

		// Token: 0x04002DA7 RID: 11687
		private byte _type;

		// Token: 0x04002DA8 RID: 11688
		private ushort _length;

		// Token: 0x04002DA9 RID: 11689
		private object _value;

		// Token: 0x04002DAA RID: 11690
		private ushort _precision;

		// Token: 0x04002DAB RID: 11691
		private int _lobPosition;

		// Token: 0x04002DAC RID: 11692
		private bool _isClob;

		// Token: 0x04002DAD RID: 11693
		private string _clob;

		// Token: 0x04002DAE RID: 11694
		private int _lobLength;

		// Token: 0x04002DAF RID: 11695
		private byte[] _lob;

		// Token: 0x04002DB0 RID: 11696
		private ushort _scale;

		// Token: 0x04002DB1 RID: 11697
		private short _inOutType;
	}
}
