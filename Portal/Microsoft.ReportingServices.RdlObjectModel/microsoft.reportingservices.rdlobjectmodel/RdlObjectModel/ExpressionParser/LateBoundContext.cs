using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x0200021C RID: 540
	internal class LateBoundContext : LookupContext
	{
		// Token: 0x06001236 RID: 4662 RVA: 0x00028EC5 File Offset: 0x000270C5
		internal LateBoundContext(IEnvironmentFilter filter)
			: base("LateBoundContext")
		{
			this.m_filter = filter;
		}

		// Token: 0x06001237 RID: 4663 RVA: 0x00028ED9 File Offset: 0x000270D9
		internal override bool TryMatchMember(string identifier, out MemberContext member)
		{
			if (this.m_filter.IsAllowedMember(identifier))
			{
				member = new MemberContext(identifier, MemberContext.MemberContextTypes.Unknown);
				return true;
			}
			member = null;
			return false;
		}

		// Token: 0x040005C9 RID: 1481
		private readonly IEnvironmentFilter m_filter;
	}
}
