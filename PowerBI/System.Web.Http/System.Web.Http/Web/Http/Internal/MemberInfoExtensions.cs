using System;
using System.Reflection;

namespace System.Web.Http.Internal
{
	// Token: 0x02000182 RID: 386
	internal static class MemberInfoExtensions
	{
		// Token: 0x060009FF RID: 2559 RVA: 0x00019CBB File Offset: 0x00017EBB
		public static TAttribute[] GetCustomAttributes<TAttribute>(this MemberInfo member, bool inherit) where TAttribute : class
		{
			if (member == null)
			{
				throw Error.ArgumentNull("member");
			}
			return (TAttribute[])member.GetCustomAttributes(typeof(TAttribute), inherit);
		}
	}
}
