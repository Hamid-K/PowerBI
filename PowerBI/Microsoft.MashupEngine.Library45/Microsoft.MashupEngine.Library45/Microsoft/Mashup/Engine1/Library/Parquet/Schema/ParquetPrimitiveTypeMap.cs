using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;
using ParquetSharp;

namespace Microsoft.Mashup.Engine1.Library.Parquet.Schema
{
	// Token: 0x02001F7E RID: 8062
	internal abstract class ParquetPrimitiveTypeMap : ParquetTypeMap
	{
		// Token: 0x06010E9B RID: 69275 RVA: 0x003A4B4B File Offset: 0x003A2D4B
		public ParquetPrimitiveTypeMap(PhysicalType physicalType, LogicalTypeEnum logicalTypeEnum, Func<LogicalType> logicalTypeCtor, TypeValue typeValue, TypeFacets facets = null, int? typeLength = null)
			: base(physicalType, logicalTypeEnum, logicalTypeCtor, facets)
		{
			this.typeValue = typeValue;
			this.typeLength = typeLength;
		}

		// Token: 0x17002CCA RID: 11466
		// (get) Token: 0x06010E9C RID: 69276 RVA: 0x003A4B68 File Offset: 0x003A2D68
		public override ICollection<ValueKind> TypeKinds
		{
			get
			{
				return new ValueKind[] { this.typeValue.TypeKind };
			}
		}

		// Token: 0x17002CCB RID: 11467
		// (get) Token: 0x06010E9D RID: 69277 RVA: 0x003A4B7E File Offset: 0x003A2D7E
		public TypeValue TypeValue
		{
			get
			{
				return this.typeValue;
			}
		}

		// Token: 0x17002CCC RID: 11468
		// (get) Token: 0x06010E9E RID: 69278 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public virtual bool IsOleDbCompatible
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002CCD RID: 11469
		// (get) Token: 0x06010E9F RID: 69279 RVA: 0x00002C72 File Offset: 0x00000E72
		public virtual Type Type
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002CCE RID: 11470
		// (get) Token: 0x06010EA0 RID: 69280 RVA: 0x003A4B86 File Offset: 0x003A2D86
		public int? TypeLength
		{
			get
			{
				return this.typeLength;
			}
		}

		// Token: 0x06010EA1 RID: 69281
		public abstract ListStatistics GetStatistics(Statistics statistics);

		// Token: 0x040065DD RID: 26077
		private readonly TypeValue typeValue;

		// Token: 0x040065DE RID: 26078
		private readonly int? typeLength;
	}
}
