using System;

namespace Microsoft.ReportingServices.ReportIntermediateFormat.Persistence
{
	// Token: 0x0200054B RID: 1355
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	internal sealed class SkipMemberStaticValidationAttribute : Attribute
	{
		// Token: 0x060049B3 RID: 18867 RVA: 0x001374EE File Offset: 0x001356EE
		internal SkipMemberStaticValidationAttribute(MemberName member)
		{
			this.m_member = member;
		}

		// Token: 0x17001DE2 RID: 7650
		// (get) Token: 0x060049B4 RID: 18868 RVA: 0x001374FD File Offset: 0x001356FD
		public MemberName Member
		{
			get
			{
				return this.m_member;
			}
		}

		// Token: 0x040020AE RID: 8366
		private MemberName m_member;
	}
}
