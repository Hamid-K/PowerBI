using System;

namespace Microsoft.OleDb
{
	// Token: 0x02001E58 RID: 7768
	public class Binding
	{
		// Token: 0x0600BEBA RID: 48826 RVA: 0x00269498 File Offset: 0x00267698
		public Binding(ref DBBINDING nativeBinding)
		{
			this.ordinal = nativeBinding.iOrdinal;
			this.valueOffset = nativeBinding.obValue;
			this.lengthOffset = nativeBinding.obLength;
			this.statusOffset = nativeBinding.obStatus;
			this.part = nativeBinding.dwPart;
			this.memoryOwner = nativeBinding.dwMemOwner;
			this.paramIO = nativeBinding.eParamIO;
			this.destMaxLength = nativeBinding.cbMaxLen;
			this.flags = nativeBinding.dwFlags;
			this.destType = nativeBinding.wType;
			this.precision = nativeBinding.bPrecision;
			this.scale = nativeBinding.bScale;
		}

		// Token: 0x0600BEBB RID: 48827 RVA: 0x0026953C File Offset: 0x0026773C
		protected Binding(Binding binding, DBORDINAL newOrdinal)
		{
			this.ordinal = newOrdinal;
			this.valueOffset = binding.valueOffset;
			this.lengthOffset = binding.lengthOffset;
			this.statusOffset = binding.statusOffset;
			this.part = binding.part;
			this.memoryOwner = binding.memoryOwner;
			this.paramIO = binding.paramIO;
			this.destMaxLength = binding.destMaxLength;
			this.flags = binding.flags;
			this.destType = binding.destType;
			this.precision = binding.precision;
			this.scale = binding.scale;
		}

		// Token: 0x17002EE4 RID: 12004
		// (get) Token: 0x0600BEBC RID: 48828 RVA: 0x002695DA File Offset: 0x002677DA
		public DBORDINAL Ordinal
		{
			get
			{
				return this.ordinal;
			}
		}

		// Token: 0x17002EE5 RID: 12005
		// (get) Token: 0x0600BEBD RID: 48829 RVA: 0x002695E2 File Offset: 0x002677E2
		public DBBYTEOFFSET ValueOffset
		{
			get
			{
				return this.valueOffset;
			}
		}

		// Token: 0x17002EE6 RID: 12006
		// (get) Token: 0x0600BEBE RID: 48830 RVA: 0x002695EA File Offset: 0x002677EA
		public DBBYTEOFFSET LengthOffset
		{
			get
			{
				return this.lengthOffset;
			}
		}

		// Token: 0x17002EE7 RID: 12007
		// (get) Token: 0x0600BEBF RID: 48831 RVA: 0x002695F2 File Offset: 0x002677F2
		public DBBYTEOFFSET StatusOffset
		{
			get
			{
				return this.statusOffset;
			}
		}

		// Token: 0x17002EE8 RID: 12008
		// (get) Token: 0x0600BEC0 RID: 48832 RVA: 0x002695FA File Offset: 0x002677FA
		public DBPART Part
		{
			get
			{
				return this.part;
			}
		}

		// Token: 0x17002EE9 RID: 12009
		// (get) Token: 0x0600BEC1 RID: 48833 RVA: 0x00269602 File Offset: 0x00267802
		public DBMEMOWNER MemoryOwner
		{
			get
			{
				return this.memoryOwner;
			}
		}

		// Token: 0x17002EEA RID: 12010
		// (get) Token: 0x0600BEC2 RID: 48834 RVA: 0x0026960A File Offset: 0x0026780A
		public DBPARAMIO ParamIO
		{
			get
			{
				return this.paramIO;
			}
		}

		// Token: 0x17002EEB RID: 12011
		// (get) Token: 0x0600BEC3 RID: 48835 RVA: 0x00269612 File Offset: 0x00267812
		public DBLENGTH DestMaxLength
		{
			get
			{
				return this.destMaxLength;
			}
		}

		// Token: 0x17002EEC RID: 12012
		// (get) Token: 0x0600BEC4 RID: 48836 RVA: 0x0026961A File Offset: 0x0026781A
		public uint Flags
		{
			get
			{
				return this.flags;
			}
		}

		// Token: 0x17002EED RID: 12013
		// (get) Token: 0x0600BEC5 RID: 48837 RVA: 0x00269622 File Offset: 0x00267822
		public DBTYPE DestType
		{
			get
			{
				return this.destType;
			}
		}

		// Token: 0x17002EEE RID: 12014
		// (get) Token: 0x0600BEC6 RID: 48838 RVA: 0x0026962A File Offset: 0x0026782A
		public byte Precision
		{
			get
			{
				return this.precision;
			}
		}

		// Token: 0x17002EEF RID: 12015
		// (get) Token: 0x0600BEC7 RID: 48839 RVA: 0x00269632 File Offset: 0x00267832
		public byte Scale
		{
			get
			{
				return this.scale;
			}
		}

		// Token: 0x0400611F RID: 24863
		private readonly DBORDINAL ordinal;

		// Token: 0x04006120 RID: 24864
		private readonly DBBYTEOFFSET valueOffset;

		// Token: 0x04006121 RID: 24865
		private readonly DBBYTEOFFSET lengthOffset;

		// Token: 0x04006122 RID: 24866
		private readonly DBBYTEOFFSET statusOffset;

		// Token: 0x04006123 RID: 24867
		private readonly DBPART part;

		// Token: 0x04006124 RID: 24868
		private readonly DBMEMOWNER memoryOwner;

		// Token: 0x04006125 RID: 24869
		private readonly DBPARAMIO paramIO;

		// Token: 0x04006126 RID: 24870
		private readonly DBLENGTH destMaxLength;

		// Token: 0x04006127 RID: 24871
		private readonly uint flags;

		// Token: 0x04006128 RID: 24872
		private readonly DBTYPE destType;

		// Token: 0x04006129 RID: 24873
		private readonly byte precision;

		// Token: 0x0400612A RID: 24874
		private readonly byte scale;
	}
}
