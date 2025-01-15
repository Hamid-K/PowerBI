using System;
using System.Data;
using System.Runtime.CompilerServices;
using Microsoft.PowerBI.DataMovement.Pipeline.Common.Serialization;
using Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication;

namespace Microsoft.PowerBI.DataMovement.Pipeline.Common.Ado
{
	// Token: 0x02000008 RID: 8
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class AdoReaderWriterMetadata
	{
		// Token: 0x0600000E RID: 14 RVA: 0x000020F0 File Offset: 0x000002F0
		internal AdoReaderWriterMetadata(IDataReader reader)
		{
			this.m_fieldCount = reader.FieldCount;
			Type[] array = new Type[this.m_fieldCount];
			for (int i = 0; i < this.m_fieldCount; i++)
			{
				array[i] = reader.GetFieldType(i);
			}
			this.Init(array);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002140 File Offset: 0x00000340
		internal AdoReaderWriterMetadata(AdoTableDescriptor adoTableDescriptor)
		{
			this.m_fieldCount = adoTableDescriptor.ColumnDescriptors.Length;
			Type[] array = new Type[this.m_fieldCount];
			for (int i = 0; i < this.m_fieldCount; i++)
			{
				array[i] = TypeCodeMapping.GetTypeFrom(adoTableDescriptor.ColumnDescriptors[i].ColumnType);
			}
			this.Init(array);
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000010 RID: 16 RVA: 0x0000219A File Offset: 0x0000039A
		internal int[] ColumnOffsets
		{
			get
			{
				return this.m_columnOffsets;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000021A2 File Offset: 0x000003A2
		internal ClrTypeCode[] ColumnTypes
		{
			get
			{
				return this.m_columnTypes;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000021AA File Offset: 0x000003AA
		internal int FieldCount
		{
			get
			{
				return this.m_fieldCount;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000021B2 File Offset: 0x000003B2
		internal bool HasVarDataFields
		{
			get
			{
				return this.m_hasVarDataFields;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000021BA File Offset: 0x000003BA
		internal int RowSizeFull
		{
			get
			{
				return this.m_rowSizeFull;
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000021C4 File Offset: 0x000003C4
		private void Init(Type[] dotNetColumnTypes)
		{
			this.m_columnTypes = new ClrTypeCode[this.m_fieldCount];
			this.m_columnSizes = new int[this.m_fieldCount];
			this.m_columnOffsets = new int[this.m_fieldCount];
			int num = (int)Math.Ceiling((double)this.m_fieldCount / 8.0);
			this.m_rowSizeFull = num;
			this.m_hasVarDataFields = false;
			for (int i = 0; i < this.m_fieldCount; i++)
			{
				ClrTypeCode typeCodeFrom = TypeCodeMapping.GetTypeCodeFrom(dotNetColumnTypes[i]);
				this.m_columnTypes[i] = typeCodeFrom;
				int lengthForType = TypeConversionUtil.GetLengthForType(typeCodeFrom);
				this.m_columnSizes[i] = lengthForType;
				this.m_columnOffsets[i] = this.m_rowSizeFull;
				this.m_rowSizeFull += lengthForType;
				this.m_hasVarDataFields |= TypeConversionUtil.IsVariableLengthType(typeCodeFrom);
			}
		}

		// Token: 0x04000014 RID: 20
		private int m_fieldCount;

		// Token: 0x04000015 RID: 21
		private ClrTypeCode[] m_columnTypes;

		// Token: 0x04000016 RID: 22
		private int[] m_columnSizes;

		// Token: 0x04000017 RID: 23
		private int[] m_columnOffsets;

		// Token: 0x04000018 RID: 24
		private int m_rowSizeFull;

		// Token: 0x04000019 RID: 25
		private bool m_hasVarDataFields;
	}
}
