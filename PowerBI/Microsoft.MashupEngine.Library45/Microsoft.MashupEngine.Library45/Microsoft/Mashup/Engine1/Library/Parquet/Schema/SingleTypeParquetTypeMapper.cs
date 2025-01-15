using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;
using ParquetSharp;
using ParquetSharp.Schema;

namespace Microsoft.Mashup.Engine1.Library.Parquet.Schema
{
	// Token: 0x02001FE5 RID: 8165
	internal class SingleTypeParquetTypeMapper : ParquetTypeMapper
	{
		// Token: 0x060110BE RID: 69822 RVA: 0x003ACE27 File Offset: 0x003AB027
		public SingleTypeParquetTypeMapper(ICollection<ValueKind> typeKinds, PhysicalType physicalType, LogicalTypeEnum logicalTypeType, ParquetTypeMapper inner)
		{
			this.typeKinds = typeKinds;
			this.physicalType = physicalType;
			this.logicalTypeType = logicalTypeType;
			this.inner = inner;
		}

		// Token: 0x17002CF8 RID: 11512
		// (get) Token: 0x060110BF RID: 69823 RVA: 0x003ACE4C File Offset: 0x003AB04C
		public ICollection<ValueKind> TypeKinds
		{
			get
			{
				return this.typeKinds;
			}
		}

		// Token: 0x17002CF9 RID: 11513
		// (get) Token: 0x060110C0 RID: 69824 RVA: 0x003ACE54 File Offset: 0x003AB054
		public PhysicalType PhysicalType
		{
			get
			{
				return this.physicalType;
			}
		}

		// Token: 0x17002CFA RID: 11514
		// (get) Token: 0x060110C1 RID: 69825 RVA: 0x003ACE5C File Offset: 0x003AB05C
		public LogicalTypeEnum LogicalTypeType
		{
			get
			{
				return this.logicalTypeType;
			}
		}

		// Token: 0x060110C2 RID: 69826 RVA: 0x003ACE64 File Offset: 0x003AB064
		public override bool TryMap(Node node, TypeValue typeValue, SchemaConfig config, out ParquetTypeMap typeMap)
		{
			ParquetTypeMapper.CheckType(node, typeValue, this.typeKinds);
			if (!this.inner.TryMap(node, typeValue, config, out typeMap))
			{
				return false;
			}
			if (typeMap.PhysicalType != this.PhysicalType)
			{
				throw ParquetTypeErrors.IncompatibleTypeError(typeValue, "PhysicalType", TextValue.New(ParquetTypeMap.GetPhysicalTypeName(this.PhysicalType)), TextValue.New(ParquetTypeMap.GetPhysicalTypeName(typeMap.PhysicalType)));
			}
			if (typeMap.LogicalTypeType != this.LogicalTypeType)
			{
				throw ParquetTypeErrors.IncompatibleTypeError(typeValue, "LogicalType", TextValue.New(ParquetTypeMap.GetLogicalTypeName(this.LogicalTypeType)), TextValue.New(ParquetTypeMap.GetLogicalTypeName(typeMap.LogicalTypeType)));
			}
			return true;
		}

		// Token: 0x060110C3 RID: 69827 RVA: 0x003ACF0F File Offset: 0x003AB10F
		public new static implicit operator SingleTypeParquetTypeMapper(ParquetTypeMap typeMap)
		{
			return new SingleTypeParquetTypeMapper(typeMap.TypeKinds, typeMap.PhysicalType, typeMap.LogicalTypeType, typeMap);
		}

		// Token: 0x04006717 RID: 26391
		private readonly ICollection<ValueKind> typeKinds;

		// Token: 0x04006718 RID: 26392
		private readonly PhysicalType physicalType;

		// Token: 0x04006719 RID: 26393
		private readonly LogicalTypeEnum logicalTypeType;

		// Token: 0x0400671A RID: 26394
		private readonly ParquetTypeMapper inner;
	}
}
