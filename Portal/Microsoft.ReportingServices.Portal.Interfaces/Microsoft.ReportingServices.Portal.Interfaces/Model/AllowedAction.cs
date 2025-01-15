using System;
using System.ComponentModel.DataAnnotations;

namespace Model
{
	// Token: 0x02000030 RID: 48
	public sealed class AllowedAction
	{
		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000113 RID: 275 RVA: 0x000029F5 File Offset: 0x00000BF5
		// (set) Token: 0x06000114 RID: 276 RVA: 0x000029FD File Offset: 0x00000BFD
		[Required]
		[Key]
		public string Action { get; set; }
	}
}
