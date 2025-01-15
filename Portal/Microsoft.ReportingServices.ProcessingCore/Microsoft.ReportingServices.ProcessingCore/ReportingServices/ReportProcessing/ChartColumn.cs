using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006FC RID: 1788
	[Serializable]
	internal sealed class ChartColumn
	{
		// Token: 0x1700232E RID: 9006
		// (get) Token: 0x0600637F RID: 25471 RVA: 0x0018B182 File Offset: 0x00189382
		// (set) Token: 0x06006380 RID: 25472 RVA: 0x0018B18A File Offset: 0x0018938A
		internal string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		// Token: 0x1700232F RID: 9007
		// (get) Token: 0x06006381 RID: 25473 RVA: 0x0018B193 File Offset: 0x00189393
		// (set) Token: 0x06006382 RID: 25474 RVA: 0x0018B19B File Offset: 0x0018939B
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

		// Token: 0x06006383 RID: 25475 RVA: 0x0018B1A4 File Offset: 0x001893A4
		internal void Initialize(InitializationContext context)
		{
			if (this.m_value != null)
			{
				this.m_value.Initialize("Value", context);
				context.ExprHostBuilder.OWCChartColumnsValue(this.m_value);
			}
		}

		// Token: 0x06006384 RID: 25476 RVA: 0x0018B1D4 File Offset: 0x001893D4
		internal static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.None, new MemberInfoList
			{
				new MemberInfo(MemberName.Name, Token.String),
				new MemberInfo(MemberName.Value, ObjectType.ExpressionInfo)
			});
		}

		// Token: 0x04003208 RID: 12808
		private string m_name;

		// Token: 0x04003209 RID: 12809
		private ExpressionInfo m_value;
	}
}
