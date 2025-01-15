using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000600 RID: 1536
	[Serializable]
	internal sealed class SenderInformation
	{
		// Token: 0x0600548B RID: 21643 RVA: 0x00162D02 File Offset: 0x00160F02
		internal SenderInformation()
		{
		}

		// Token: 0x0600548C RID: 21644 RVA: 0x00162D0A File Offset: 0x00160F0A
		internal SenderInformation(bool startHidden, int[] containerUniqueNames)
		{
			this.m_startHidden = startHidden;
			this.m_receiverUniqueNames = new IntList();
			this.m_containerUniqueNames = containerUniqueNames;
		}

		// Token: 0x17001F18 RID: 7960
		// (get) Token: 0x0600548D RID: 21645 RVA: 0x00162D2B File Offset: 0x00160F2B
		// (set) Token: 0x0600548E RID: 21646 RVA: 0x00162D33 File Offset: 0x00160F33
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

		// Token: 0x17001F19 RID: 7961
		// (get) Token: 0x0600548F RID: 21647 RVA: 0x00162D3C File Offset: 0x00160F3C
		// (set) Token: 0x06005490 RID: 21648 RVA: 0x00162D44 File Offset: 0x00160F44
		internal IntList ReceiverUniqueNames
		{
			get
			{
				return this.m_receiverUniqueNames;
			}
			set
			{
				this.m_receiverUniqueNames = value;
			}
		}

		// Token: 0x17001F1A RID: 7962
		// (get) Token: 0x06005491 RID: 21649 RVA: 0x00162D4D File Offset: 0x00160F4D
		// (set) Token: 0x06005492 RID: 21650 RVA: 0x00162D55 File Offset: 0x00160F55
		internal int[] ContainerUniqueNames
		{
			get
			{
				return this.m_containerUniqueNames;
			}
			set
			{
				this.m_containerUniqueNames = value;
			}
		}

		// Token: 0x06005493 RID: 21651 RVA: 0x00162D60 File Offset: 0x00160F60
		internal static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.None, new MemberInfoList
			{
				new MemberInfo(MemberName.Hidden, Token.Boolean),
				new MemberInfo(MemberName.ReceiverUniqueNames, ObjectType.IntList),
				new MemberInfo(MemberName.ContainerUniqueNames, Token.TypedArray)
			});
		}

		// Token: 0x04002CFC RID: 11516
		private bool m_startHidden;

		// Token: 0x04002CFD RID: 11517
		private IntList m_receiverUniqueNames;

		// Token: 0x04002CFE RID: 11518
		private int[] m_containerUniqueNames;
	}
}
