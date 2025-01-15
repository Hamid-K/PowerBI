using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeResultWriter
{
	// Token: 0x02000041 RID: 65
	internal sealed class DataMemberInstanceWriter : DsrCalculationContainerWriterBase
	{
		// Token: 0x06000161 RID: 353 RVA: 0x000047CD File Offset: 0x000029CD
		internal GroupWriter BeginGroup()
		{
			base.Writer.BeginProperty(base.DsrNames.Group);
			base.CreateAndBeginChild<GroupWriter>(ref this._groupWriter);
			return this._groupWriter;
		}

		// Token: 0x06000162 RID: 354 RVA: 0x000047F8 File Offset: 0x000029F8
		internal CollectionWriter<DataShapeWriter> BeginDataShapes()
		{
			base.Writer.BeginProperty(base.DsrNames.DataShapes);
			base.CreateAndBeginChild<DataShapeWriter>(ref this._dataShapesWriter);
			return this._dataShapesWriter;
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00004823 File Offset: 0x00002A23
		internal CollectionWriter<DataMemberWriter> BeginDataMembers()
		{
			base.Writer.BeginProperty(base.DsrNames.Members);
			base.CreateAndBeginChild<DataMemberWriter>(ref this._membersWriter);
			return this._membersWriter;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000484E File Offset: 0x00002A4E
		internal CollectionWriter<DataIntersectionWriter> BeginIntersections()
		{
			base.Writer.BeginProperty(base.DsrNames.Intersections);
			base.CreateAndBeginChild<DataIntersectionWriter>(ref this._intersectionsWriter);
			return this._intersectionsWriter;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00004879 File Offset: 0x00002A79
		internal void WriteRestartFlag(RestartFlag flag)
		{
			if (flag != RestartFlag.Append)
			{
				base.Writer.WriteProperty(base.DsrNames.RestartFlag, (int)flag);
			}
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00004895 File Offset: 0x00002A95
		internal void WriteRestartKind(RestartKind kind)
		{
			if (kind != RestartKind.None)
			{
				base.Writer.WriteProperty(base.DsrNames.RestartKind, (int)kind);
			}
		}

		// Token: 0x040000A0 RID: 160
		private GroupWriter _groupWriter;

		// Token: 0x040000A1 RID: 161
		private CollectionWriter<DataShapeWriter> _dataShapesWriter;

		// Token: 0x040000A2 RID: 162
		private CollectionWriter<DataMemberWriter> _membersWriter;

		// Token: 0x040000A3 RID: 163
		private CollectionWriter<DataIntersectionWriter> _intersectionsWriter;
	}
}
