using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006CC RID: 1740
	[Serializable]
	public abstract class IDOwner
	{
		// Token: 0x06005D86 RID: 23942 RVA: 0x0017DD82 File Offset: 0x0017BF82
		protected IDOwner()
		{
		}

		// Token: 0x06005D87 RID: 23943 RVA: 0x0017DD8A File Offset: 0x0017BF8A
		protected IDOwner(int id)
		{
			this.m_ID = id;
		}

		// Token: 0x170020B8 RID: 8376
		// (get) Token: 0x06005D88 RID: 23944 RVA: 0x0017DD99 File Offset: 0x0017BF99
		// (set) Token: 0x06005D89 RID: 23945 RVA: 0x0017DDA1 File Offset: 0x0017BFA1
		internal int ID
		{
			get
			{
				return this.m_ID;
			}
			set
			{
				this.m_ID = value;
			}
		}

		// Token: 0x06005D8A RID: 23946 RVA: 0x0017DDAC File Offset: 0x0017BFAC
		internal static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.None, new MemberInfoList
			{
				new MemberInfo(MemberName.ID, Token.Int32)
			});
		}

		// Token: 0x04002FD8 RID: 12248
		protected int m_ID;
	}
}
