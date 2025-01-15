using System;

namespace Microsoft.ReportingServices.ReportIntermediateFormat.Persistence
{
	// Token: 0x0200002D RID: 45
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	internal sealed class SkipMemberStaticValidationAttribute : Attribute
	{
		// Token: 0x060001C7 RID: 455 RVA: 0x000082CE File Offset: 0x000064CE
		internal SkipMemberStaticValidationAttribute(MemberName member)
		{
			this.m_member = member;
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x000082DD File Offset: 0x000064DD
		public MemberName Member
		{
			get
			{
				return this.m_member;
			}
		}

		// Token: 0x0400012E RID: 302
		private MemberName m_member;
	}
}
