using System;

namespace Microsoft.IdentityModel.Logging
{
	// Token: 0x0200000C RID: 12
	internal struct NonPII
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00002FA5 File Offset: 0x000011A5
		// (set) Token: 0x0600006C RID: 108 RVA: 0x00002FAD File Offset: 0x000011AD
		public object Argument { get; set; }

		// Token: 0x0600006D RID: 109 RVA: 0x00002FB6 File Offset: 0x000011B6
		public NonPII(object argument)
		{
			this.Argument = argument;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002FBF File Offset: 0x000011BF
		public override string ToString()
		{
			object argument = this.Argument;
			return ((argument != null) ? argument.ToString() : null) ?? "Null";
		}
	}
}
