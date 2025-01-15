using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000715 RID: 1813
	[Serializable]
	internal sealed class OffsetInfo : InfoBase
	{
		// Token: 0x06006523 RID: 25891 RVA: 0x0018F0E2 File Offset: 0x0018D2E2
		internal OffsetInfo()
		{
		}

		// Token: 0x06006524 RID: 25892 RVA: 0x0018F0EA File Offset: 0x0018D2EA
		internal OffsetInfo(long offset)
		{
			this.m_offset = offset;
		}

		// Token: 0x170023CE RID: 9166
		// (get) Token: 0x06006525 RID: 25893 RVA: 0x0018F0F9 File Offset: 0x0018D2F9
		// (set) Token: 0x06006526 RID: 25894 RVA: 0x0018F101 File Offset: 0x0018D301
		internal long Offset
		{
			get
			{
				return this.m_offset;
			}
			set
			{
				this.m_offset = value;
			}
		}

		// Token: 0x06006527 RID: 25895 RVA: 0x0018F10C File Offset: 0x0018D30C
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.InfoBase, new MemberInfoList
			{
				new MemberInfo(MemberName.Offset, Token.Int64)
			});
		}

		// Token: 0x040032A0 RID: 12960
		private long m_offset;
	}
}
