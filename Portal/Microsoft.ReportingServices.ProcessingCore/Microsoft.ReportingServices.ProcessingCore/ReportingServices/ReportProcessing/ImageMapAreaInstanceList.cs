using System;
using System.Collections;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200069A RID: 1690
	[Serializable]
	internal sealed class ImageMapAreaInstanceList : ArrayList
	{
		// Token: 0x06005C58 RID: 23640 RVA: 0x00179763 File Offset: 0x00177963
		internal ImageMapAreaInstanceList()
		{
		}

		// Token: 0x06005C59 RID: 23641 RVA: 0x0017976B File Offset: 0x0017796B
		internal ImageMapAreaInstanceList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x17002074 RID: 8308
		internal ImageMapAreaInstance this[int index]
		{
			get
			{
				return (ImageMapAreaInstance)base[index];
			}
		}

		// Token: 0x17002075 RID: 8309
		// (get) Token: 0x06005C5B RID: 23643 RVA: 0x00179782 File Offset: 0x00177982
		// (set) Token: 0x06005C5C RID: 23644 RVA: 0x0017978A File Offset: 0x0017798A
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

		// Token: 0x06005C5D RID: 23645 RVA: 0x00179794 File Offset: 0x00177994
		internal static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.None, new MemberInfoList
			{
				new MemberInfo(MemberName.UniqueName, Token.Int32)
			});
		}

		// Token: 0x04002F95 RID: 12181
		private int m_uniqueName;
	}
}
