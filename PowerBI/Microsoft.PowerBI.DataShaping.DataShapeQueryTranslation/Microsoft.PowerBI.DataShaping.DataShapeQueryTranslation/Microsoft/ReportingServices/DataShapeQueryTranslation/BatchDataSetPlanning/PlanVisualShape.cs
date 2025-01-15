using System;
using System.Collections.Generic;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning
{
	// Token: 0x020001A3 RID: 419
	internal sealed class PlanVisualShape : IEquatable<PlanVisualShape>, IStructuredToString
	{
		// Token: 0x06000EC1 RID: 3777 RVA: 0x0003BC98 File Offset: 0x00039E98
		public PlanVisualShape(IReadOnlyList<VisualAxis> visualAxes, string isDensifiedColumnName)
		{
			this.VisualAxes = visualAxes;
			this.IsDensifiedColumnName = isDensifiedColumnName;
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000EC2 RID: 3778 RVA: 0x0003BCAE File Offset: 0x00039EAE
		public IReadOnlyList<VisualAxis> VisualAxes { get; }

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000EC3 RID: 3779 RVA: 0x0003BCB6 File Offset: 0x00039EB6
		public string IsDensifiedColumnName { get; }

		// Token: 0x06000EC4 RID: 3780 RVA: 0x0003BCBE File Offset: 0x00039EBE
		public override bool Equals(object obj)
		{
			return this.Equals(obj as PlanVisualShape);
		}

		// Token: 0x06000EC5 RID: 3781 RVA: 0x0003BCCC File Offset: 0x00039ECC
		public bool Equals(PlanVisualShape other)
		{
			return other != null && this.VisualAxes.SequenceEqualReadOnly(other.VisualAxes) && this.IsDensifiedColumnName == other.IsDensifiedColumnName;
		}

		// Token: 0x06000EC6 RID: 3782 RVA: 0x0003BCF7 File Offset: 0x00039EF7
		public override int GetHashCode()
		{
			return Hashing.CombineHash(Hashing.CombineHashReadonly<VisualAxis>(this.VisualAxes, null), Hashing.GetHashCode<string>(this.IsDensifiedColumnName, StringComparer.Ordinal));
		}

		// Token: 0x06000EC7 RID: 3783 RVA: 0x0003BD1A File Offset: 0x00039F1A
		public void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("VisualShape");
			builder.WriteAttribute<string>("IsDensifiedColumnName", this.IsDensifiedColumnName, false, false);
			builder.WriteProperty<IReadOnlyList<VisualAxis>>("VisualAxes", this.VisualAxes, false);
			builder.EndObject();
		}
	}
}
