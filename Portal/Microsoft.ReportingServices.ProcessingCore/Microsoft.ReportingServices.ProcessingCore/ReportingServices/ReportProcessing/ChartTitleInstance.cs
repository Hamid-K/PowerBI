using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000750 RID: 1872
	[Serializable]
	internal sealed class ChartTitleInstance
	{
		// Token: 0x060067E0 RID: 26592 RVA: 0x0019503C File Offset: 0x0019323C
		internal ChartTitleInstance(ReportProcessing.ProcessingContext pc, Chart chart, ChartTitle titleDef, string propertyName)
		{
			this.m_uniqueName = pc.CreateUniqueName();
			this.m_caption = pc.ReportRuntime.EvaluateChartTitleCaptionExpression(titleDef, chart.Name, propertyName);
			this.m_styleAttributeValues = Chart.CreateStyle(pc, titleDef.StyleClass, chart.Name + "." + propertyName, this.m_uniqueName);
		}

		// Token: 0x060067E1 RID: 26593 RVA: 0x0019509F File Offset: 0x0019329F
		internal ChartTitleInstance()
		{
		}

		// Token: 0x170024B7 RID: 9399
		// (get) Token: 0x060067E2 RID: 26594 RVA: 0x001950A7 File Offset: 0x001932A7
		// (set) Token: 0x060067E3 RID: 26595 RVA: 0x001950AF File Offset: 0x001932AF
		internal int UniqueName
		{
			get
			{
				return this.m_uniqueName;
			}
			set
			{
				this.m_uniqueName = value;
			}
		}

		// Token: 0x170024B8 RID: 9400
		// (get) Token: 0x060067E4 RID: 26596 RVA: 0x001950B8 File Offset: 0x001932B8
		// (set) Token: 0x060067E5 RID: 26597 RVA: 0x001950C0 File Offset: 0x001932C0
		internal string Caption
		{
			get
			{
				return this.m_caption;
			}
			set
			{
				this.m_caption = value;
			}
		}

		// Token: 0x170024B9 RID: 9401
		// (get) Token: 0x060067E6 RID: 26598 RVA: 0x001950C9 File Offset: 0x001932C9
		// (set) Token: 0x060067E7 RID: 26599 RVA: 0x001950D1 File Offset: 0x001932D1
		internal object[] StyleAttributeValues
		{
			get
			{
				return this.m_styleAttributeValues;
			}
			set
			{
				this.m_styleAttributeValues = value;
			}
		}

		// Token: 0x060067E8 RID: 26600 RVA: 0x001950DC File Offset: 0x001932DC
		internal static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.None, new MemberInfoList
			{
				new MemberInfo(MemberName.UniqueName, Token.Int32),
				new MemberInfo(MemberName.Caption, Token.String),
				new MemberInfo(MemberName.StyleAttributeValues, Token.Array, ObjectType.Variant)
			});
		}

		// Token: 0x0400336D RID: 13165
		private int m_uniqueName;

		// Token: 0x0400336E RID: 13166
		private string m_caption;

		// Token: 0x0400336F RID: 13167
		private object[] m_styleAttributeValues;
	}
}
