using System;
using System.ComponentModel;

namespace Microsoft.PowerBI.Data.ModelSchemaAnalysis
{
	// Token: 0x0200000A RID: 10
	[ImmutableObject(true)]
	public sealed class MeasureCalculationKindResult
	{
		// Token: 0x06000014 RID: 20 RVA: 0x00002129 File Offset: 0x00000329
		private MeasureCalculationKindResult(MeasureCalculationKindResultStatus status, MeasureCalculationKind metadata = null)
		{
			this.CalculationKind = metadata;
			this.Status = status;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000015 RID: 21 RVA: 0x0000213F File Offset: 0x0000033F
		public MeasureCalculationKind CalculationKind { get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002147 File Offset: 0x00000347
		public MeasureCalculationKindResultStatus Status { get; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000017 RID: 23 RVA: 0x0000214F File Offset: 0x0000034F
		public bool IsSuccessful
		{
			get
			{
				return this.Status == MeasureCalculationKindResultStatus.Success;
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000215A File Offset: 0x0000035A
		public static implicit operator MeasureCalculationKindResult(MeasureCalculationKind calculationKind)
		{
			return MeasureCalculationKindResult.FromCalculationKind(calculationKind);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002162 File Offset: 0x00000362
		public static implicit operator MeasureCalculationKindResult(MeasureCalculationKindResultStatus status)
		{
			return MeasureCalculationKindResult.FromErrorStatus(status);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000216A File Offset: 0x0000036A
		public static MeasureCalculationKindResult FromCalculationKind(MeasureCalculationKind calculationKind)
		{
			return new MeasureCalculationKindResult(MeasureCalculationKindResultStatus.Success, calculationKind);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002173 File Offset: 0x00000373
		public static MeasureCalculationKindResult FromErrorStatus(MeasureCalculationKindResultStatus status)
		{
			return new MeasureCalculationKindResult(status, null);
		}
	}
}
