using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006F4 RID: 1780
	[Serializable]
	internal sealed class MatrixColumn
	{
		// Token: 0x170022DE RID: 8926
		// (get) Token: 0x060062A7 RID: 25255 RVA: 0x001892AB File Offset: 0x001874AB
		// (set) Token: 0x060062A8 RID: 25256 RVA: 0x001892B3 File Offset: 0x001874B3
		internal string Width
		{
			get
			{
				return this.m_width;
			}
			set
			{
				this.m_width = value;
			}
		}

		// Token: 0x170022DF RID: 8927
		// (get) Token: 0x060062A9 RID: 25257 RVA: 0x001892BC File Offset: 0x001874BC
		// (set) Token: 0x060062AA RID: 25258 RVA: 0x001892C4 File Offset: 0x001874C4
		internal double WidthValue
		{
			get
			{
				return this.m_widthValue;
			}
			set
			{
				this.m_widthValue = value;
			}
		}

		// Token: 0x060062AB RID: 25259 RVA: 0x001892CD File Offset: 0x001874CD
		internal void Initialize(InitializationContext context)
		{
			this.m_widthValue = context.ValidateSize(ref this.m_width, "Width");
		}

		// Token: 0x060062AC RID: 25260 RVA: 0x001892E8 File Offset: 0x001874E8
		internal static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.None, new MemberInfoList
			{
				new MemberInfo(MemberName.Width, Token.String),
				new MemberInfo(MemberName.WidthValue, Token.Double)
			});
		}

		// Token: 0x040031C1 RID: 12737
		private string m_width;

		// Token: 0x040031C2 RID: 12738
		private double m_widthValue;
	}
}
