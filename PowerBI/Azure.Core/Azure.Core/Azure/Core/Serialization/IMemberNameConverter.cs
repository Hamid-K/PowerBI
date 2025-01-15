using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Azure.Core.Serialization
{
	// Token: 0x020000C4 RID: 196
	[NullableContext(1)]
	public interface IMemberNameConverter
	{
		// Token: 0x06000689 RID: 1673
		[return: Nullable(2)]
		string ConvertMemberName(MemberInfo member);
	}
}
