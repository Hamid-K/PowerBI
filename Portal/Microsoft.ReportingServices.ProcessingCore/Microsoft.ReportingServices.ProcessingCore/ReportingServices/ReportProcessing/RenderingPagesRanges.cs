using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006CF RID: 1743
	[Serializable]
	internal struct RenderingPagesRanges
	{
		// Token: 0x170020C1 RID: 8385
		// (get) Token: 0x06005DAC RID: 23980 RVA: 0x0017EA92 File Offset: 0x0017CC92
		// (set) Token: 0x06005DAD RID: 23981 RVA: 0x0017EA9A File Offset: 0x0017CC9A
		internal int StartPage
		{
			get
			{
				return this.m_startPage;
			}
			set
			{
				this.m_startPage = value;
			}
		}

		// Token: 0x170020C2 RID: 8386
		// (get) Token: 0x06005DAE RID: 23982 RVA: 0x0017EAA3 File Offset: 0x0017CCA3
		// (set) Token: 0x06005DAF RID: 23983 RVA: 0x0017EAAB File Offset: 0x0017CCAB
		internal int StartRow
		{
			get
			{
				return this.m_startPage;
			}
			set
			{
				this.m_startPage = value;
			}
		}

		// Token: 0x170020C3 RID: 8387
		// (get) Token: 0x06005DB0 RID: 23984 RVA: 0x0017EAB4 File Offset: 0x0017CCB4
		// (set) Token: 0x06005DB1 RID: 23985 RVA: 0x0017EABC File Offset: 0x0017CCBC
		internal int EndPage
		{
			get
			{
				return this.m_endPage;
			}
			set
			{
				this.m_endPage = value;
			}
		}

		// Token: 0x170020C4 RID: 8388
		// (get) Token: 0x06005DB2 RID: 23986 RVA: 0x0017EAC5 File Offset: 0x0017CCC5
		// (set) Token: 0x06005DB3 RID: 23987 RVA: 0x0017EACD File Offset: 0x0017CCCD
		internal int NumberOfDetails
		{
			get
			{
				return this.m_endPage;
			}
			set
			{
				this.m_endPage = value;
			}
		}

		// Token: 0x06005DB4 RID: 23988 RVA: 0x0017EAD8 File Offset: 0x0017CCD8
		internal static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.None, new MemberInfoList
			{
				new MemberInfo(MemberName.StartPage, Token.Int32),
				new MemberInfo(MemberName.EndPage, Token.Int32)
			});
		}

		// Token: 0x04002FE4 RID: 12260
		private int m_startPage;

		// Token: 0x04002FE5 RID: 12261
		private int m_endPage;
	}
}
