using System;
using System.Diagnostics;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x02000060 RID: 96
	[DebuggerDisplay("[Calculation] Id={Id}")]
	internal sealed class Calculation : IContextItem, IIdentifiable, IDataBoundItem, IStructuredToString
	{
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x00005976 File Offset: 0x00003B76
		// (set) Token: 0x060001F4 RID: 500 RVA: 0x0000597E File Offset: 0x00003B7E
		public Identifier Id { get; set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x00005987 File Offset: 0x00003B87
		// (set) Token: 0x060001F6 RID: 502 RVA: 0x0000598F File Offset: 0x00003B8F
		public Expression Value { get; set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x00005998 File Offset: 0x00003B98
		// (set) Token: 0x060001F8 RID: 504 RVA: 0x000059A0 File Offset: 0x00003BA0
		public Candidate<bool> SuppressJoinPredicate { get; set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x000059A9 File Offset: 0x00003BA9
		// (set) Token: 0x060001FA RID: 506 RVA: 0x000059B1 File Offset: 0x00003BB1
		public bool? RespectInstanceFilters { get; set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001FB RID: 507 RVA: 0x000059BA File Offset: 0x00003BBA
		// (set) Token: 0x060001FC RID: 508 RVA: 0x000059C2 File Offset: 0x00003BC2
		public string NativeReferenceName { get; set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060001FD RID: 509 RVA: 0x000059CB File Offset: 0x00003BCB
		// (set) Token: 0x060001FE RID: 510 RVA: 0x000059D3 File Offset: 0x00003BD3
		public bool IsContextOnly { get; set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060001FF RID: 511 RVA: 0x000059DC File Offset: 0x00003BDC
		public ObjectType ObjectType
		{
			get
			{
				return ObjectType.Calculation;
			}
		}

		// Token: 0x06000200 RID: 512 RVA: 0x000059E0 File Offset: 0x00003BE0
		public void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("Calculation");
			builder.WriteAttribute<Identifier>("Id", this.Id, false, false);
			builder.WriteAttribute<string>("NativeReferenceName", this.NativeReferenceName, false, false);
			if (this.IsContextOnly)
			{
				builder.WriteAttribute<bool>("IsContextOnly", this.IsContextOnly, false, false);
			}
			builder.EndObject();
		}
	}
}
