using System;
using Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel;
using Microsoft.ReportingServices.ReportProcessing.Persistence;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000702 RID: 1794
	[Serializable]
	internal sealed class ChartDataLabel
	{
		// Token: 0x1700236C RID: 9068
		// (get) Token: 0x06006415 RID: 25621 RVA: 0x0018D1FF File Offset: 0x0018B3FF
		// (set) Token: 0x06006416 RID: 25622 RVA: 0x0018D207 File Offset: 0x0018B407
		internal bool Visible
		{
			get
			{
				return this.m_visible;
			}
			set
			{
				this.m_visible = value;
			}
		}

		// Token: 0x1700236D RID: 9069
		// (get) Token: 0x06006417 RID: 25623 RVA: 0x0018D210 File Offset: 0x0018B410
		// (set) Token: 0x06006418 RID: 25624 RVA: 0x0018D218 File Offset: 0x0018B418
		internal ExpressionInfo Value
		{
			get
			{
				return this.m_value;
			}
			set
			{
				this.m_value = value;
			}
		}

		// Token: 0x1700236E RID: 9070
		// (get) Token: 0x06006419 RID: 25625 RVA: 0x0018D221 File Offset: 0x0018B421
		// (set) Token: 0x0600641A RID: 25626 RVA: 0x0018D229 File Offset: 0x0018B429
		internal Style StyleClass
		{
			get
			{
				return this.m_styleClass;
			}
			set
			{
				this.m_styleClass = value;
			}
		}

		// Token: 0x1700236F RID: 9071
		// (get) Token: 0x0600641B RID: 25627 RVA: 0x0018D232 File Offset: 0x0018B432
		// (set) Token: 0x0600641C RID: 25628 RVA: 0x0018D23A File Offset: 0x0018B43A
		internal ChartDataLabel.Positions Position
		{
			get
			{
				return this.m_position;
			}
			set
			{
				this.m_position = value;
			}
		}

		// Token: 0x17002370 RID: 9072
		// (get) Token: 0x0600641D RID: 25629 RVA: 0x0018D243 File Offset: 0x0018B443
		// (set) Token: 0x0600641E RID: 25630 RVA: 0x0018D24B File Offset: 0x0018B44B
		internal int Rotation
		{
			get
			{
				return this.m_rotation;
			}
			set
			{
				this.m_rotation = value;
			}
		}

		// Token: 0x0600641F RID: 25631 RVA: 0x0018D254 File Offset: 0x0018B454
		internal void Initialize(InitializationContext context)
		{
			if (this.m_value != null)
			{
				this.m_value.Initialize("DataLabel", context);
				context.ExprHostBuilder.DataLabelValue(this.m_value);
			}
			if (this.m_styleClass != null)
			{
				context.ExprHostBuilder.DataLabelStyleStart();
				this.m_styleClass.Initialize(context);
				context.ExprHostBuilder.DataLabelStyleEnd();
			}
		}

		// Token: 0x06006420 RID: 25632 RVA: 0x0018D2B8 File Offset: 0x0018B4B8
		internal void SetExprHost(StyleExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null);
			exprHost.SetReportObjectModel(reportObjectModel);
			if (this.m_styleClass != null)
			{
				this.m_styleClass.SetStyleExprHost(exprHost);
			}
		}

		// Token: 0x06006421 RID: 25633 RVA: 0x0018D2EC File Offset: 0x0018B4EC
		internal static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.None, new MemberInfoList
			{
				new MemberInfo(MemberName.Visible, Token.Boolean),
				new MemberInfo(MemberName.Value, ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.StyleClass, ObjectType.Style),
				new MemberInfo(MemberName.Position, Token.Enum),
				new MemberInfo(MemberName.Rotation, Token.Int32)
			});
		}

		// Token: 0x0400323B RID: 12859
		private bool m_visible;

		// Token: 0x0400323C RID: 12860
		private ExpressionInfo m_value;

		// Token: 0x0400323D RID: 12861
		private Style m_styleClass;

		// Token: 0x0400323E RID: 12862
		private ChartDataLabel.Positions m_position;

		// Token: 0x0400323F RID: 12863
		private int m_rotation;

		// Token: 0x02000CD2 RID: 3282
		internal enum Positions
		{
			// Token: 0x04004EE1 RID: 20193
			Auto,
			// Token: 0x04004EE2 RID: 20194
			Top,
			// Token: 0x04004EE3 RID: 20195
			TopLeft,
			// Token: 0x04004EE4 RID: 20196
			TopRight,
			// Token: 0x04004EE5 RID: 20197
			Left,
			// Token: 0x04004EE6 RID: 20198
			Center,
			// Token: 0x04004EE7 RID: 20199
			Right,
			// Token: 0x04004EE8 RID: 20200
			BottomRight,
			// Token: 0x04004EE9 RID: 20201
			Bottom,
			// Token: 0x04004EEA RID: 20202
			BottomLeft
		}
	}
}
