using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Defaults
{
	// Token: 0x0200003A RID: 58
	[ImmutableObject(true)]
	internal sealed class DefaultActivityType : IActivityType
	{
		// Token: 0x060002A2 RID: 674 RVA: 0x0000829F File Offset: 0x0000649F
		private DefaultActivityType()
		{
		}

		// Token: 0x04000096 RID: 150
		internal static readonly DefaultActivityType Instance = new DefaultActivityType();
	}
}
