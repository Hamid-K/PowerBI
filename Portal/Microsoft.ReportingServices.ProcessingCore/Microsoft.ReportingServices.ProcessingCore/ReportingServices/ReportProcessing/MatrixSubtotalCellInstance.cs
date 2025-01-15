using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200073E RID: 1854
	[Serializable]
	internal sealed class MatrixSubtotalCellInstance : MatrixCellInstance
	{
		// Token: 0x060066FF RID: 26367 RVA: 0x00192BA8 File Offset: 0x00190DA8
		internal MatrixSubtotalCellInstance(int rowIndex, int colIndex, Matrix matrixDef, int cellDefIndex, ReportProcessing.ProcessingContext pc, out NonComputedUniqueNames nonComputedUniqueNames)
			: base(rowIndex, colIndex, matrixDef, cellDefIndex, pc, out nonComputedUniqueNames)
		{
			Global.Tracer.Assert(pc.HeadingInstance != null);
			Global.Tracer.Assert(pc.HeadingInstance.MatrixHeadingDef.Subtotal.StyleClass != null);
			this.m_subtotalHeadingInstance = pc.HeadingInstance;
		}

		// Token: 0x06006700 RID: 26368 RVA: 0x00192C08 File Offset: 0x00190E08
		internal MatrixSubtotalCellInstance()
		{
		}

		// Token: 0x17002469 RID: 9321
		// (get) Token: 0x06006701 RID: 26369 RVA: 0x00192C10 File Offset: 0x00190E10
		// (set) Token: 0x06006702 RID: 26370 RVA: 0x00192C18 File Offset: 0x00190E18
		internal MatrixHeadingInstance SubtotalHeadingInstance
		{
			get
			{
				return this.m_subtotalHeadingInstance;
			}
			set
			{
				this.m_subtotalHeadingInstance = value;
			}
		}

		// Token: 0x06006703 RID: 26371 RVA: 0x00192C24 File Offset: 0x00190E24
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.InstanceInfo, new MemberInfoList
			{
				new MemberInfo(MemberName.SubtotalHeadingInstance, Token.Reference, ObjectType.MatrixHeadingInstance)
			});
		}

		// Token: 0x0400332A RID: 13098
		[Reference]
		private MatrixHeadingInstance m_subtotalHeadingInstance;
	}
}
