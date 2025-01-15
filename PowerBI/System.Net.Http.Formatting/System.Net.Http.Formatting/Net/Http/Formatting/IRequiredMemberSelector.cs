using System;
using System.Reflection;

namespace System.Net.Http.Formatting
{
	// Token: 0x0200003B RID: 59
	public interface IRequiredMemberSelector
	{
		// Token: 0x0600023D RID: 573
		bool IsRequiredMember(MemberInfo member);
	}
}
