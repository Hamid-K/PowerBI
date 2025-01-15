using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000601 RID: 1537
	[Serializable]
	internal sealed class ReceiverInformation
	{
		// Token: 0x06005494 RID: 21652 RVA: 0x00162DB5 File Offset: 0x00160FB5
		internal ReceiverInformation()
		{
		}

		// Token: 0x06005495 RID: 21653 RVA: 0x00162DBD File Offset: 0x00160FBD
		internal ReceiverInformation(bool startHidden, int senderUniqueName)
		{
			this.m_startHidden = startHidden;
			this.m_senderUniqueName = senderUniqueName;
		}

		// Token: 0x17001F1B RID: 7963
		// (get) Token: 0x06005496 RID: 21654 RVA: 0x00162DD3 File Offset: 0x00160FD3
		// (set) Token: 0x06005497 RID: 21655 RVA: 0x00162DDB File Offset: 0x00160FDB
		internal bool StartHidden
		{
			get
			{
				return this.m_startHidden;
			}
			set
			{
				this.m_startHidden = value;
			}
		}

		// Token: 0x17001F1C RID: 7964
		// (get) Token: 0x06005498 RID: 21656 RVA: 0x00162DE4 File Offset: 0x00160FE4
		// (set) Token: 0x06005499 RID: 21657 RVA: 0x00162DEC File Offset: 0x00160FEC
		internal int SenderUniqueName
		{
			get
			{
				return this.m_senderUniqueName;
			}
			set
			{
				this.m_senderUniqueName = value;
			}
		}

		// Token: 0x0600549A RID: 21658 RVA: 0x00162DF8 File Offset: 0x00160FF8
		internal static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.None, new MemberInfoList
			{
				new MemberInfo(MemberName.StartHidden, Token.Boolean),
				new MemberInfo(MemberName.SenderUniqueName, Token.Int32)
			});
		}

		// Token: 0x04002CFF RID: 11519
		private bool m_startHidden;

		// Token: 0x04002D00 RID: 11520
		private int m_senderUniqueName;
	}
}
