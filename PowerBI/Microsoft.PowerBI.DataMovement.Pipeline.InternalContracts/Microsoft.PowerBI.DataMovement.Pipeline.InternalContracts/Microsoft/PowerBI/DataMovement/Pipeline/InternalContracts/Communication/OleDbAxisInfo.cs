using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x0200004A RID: 74
	[NullableContext(1)]
	[Nullable(0)]
	[DataContract]
	internal sealed class OleDbAxisInfo : OperationDataContract
	{
		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000153 RID: 339 RVA: 0x00002BC2 File Offset: 0x00000DC2
		// (set) Token: 0x06000154 RID: 340 RVA: 0x00002BCA File Offset: 0x00000DCA
		[DataMember(Name = "axisIndex", IsRequired = true, EmitDefaultValue = true)]
		internal uint AxisIndex { get; set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00002BD3 File Offset: 0x00000DD3
		// (set) Token: 0x06000156 RID: 342 RVA: 0x00002BDB File Offset: 0x00000DDB
		[DataMember(Name = "dimensionsCount", IsRequired = true, EmitDefaultValue = true)]
		internal uint DimensionsCount { get; set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00002BE4 File Offset: 0x00000DE4
		// (set) Token: 0x06000158 RID: 344 RVA: 0x00002BEC File Offset: 0x00000DEC
		[DataMember(Name = "coordinatesCount", IsRequired = true, EmitDefaultValue = true)]
		internal uint CoordinatesCount { get; set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000159 RID: 345 RVA: 0x00002BF5 File Offset: 0x00000DF5
		// (set) Token: 0x0600015A RID: 346 RVA: 0x00002BFD File Offset: 0x00000DFD
		[DataMember(Name = "dimensionColumnCounts", IsRequired = true, EmitDefaultValue = true)]
		internal uint[] DimensionColumnCounts { get; set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00002C06 File Offset: 0x00000E06
		// (set) Token: 0x0600015C RID: 348 RVA: 0x00002C0E File Offset: 0x00000E0E
		[DataMember(Name = "dimensionNames", IsRequired = true, EmitDefaultValue = true)]
		internal string[] DimensionNames { get; set; }
	}
}
