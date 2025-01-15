using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Events
{
	// Token: 0x020001E2 RID: 482
	[DomName("ErrorEvent")]
	public class ErrorEvent : Event
	{
		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000FE8 RID: 4072 RVA: 0x000470A7 File Offset: 0x000452A7
		[DomName("message")]
		public string Message
		{
			get
			{
				return this.Error.Message;
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000FE9 RID: 4073 RVA: 0x000470B4 File Offset: 0x000452B4
		// (set) Token: 0x06000FEA RID: 4074 RVA: 0x000470BC File Offset: 0x000452BC
		[DomName("filename")]
		public string FileName { get; private set; }

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000FEB RID: 4075 RVA: 0x000470C5 File Offset: 0x000452C5
		// (set) Token: 0x06000FEC RID: 4076 RVA: 0x000470CD File Offset: 0x000452CD
		[DomName("lineno")]
		public int Line { get; private set; }

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000FED RID: 4077 RVA: 0x000470D6 File Offset: 0x000452D6
		// (set) Token: 0x06000FEE RID: 4078 RVA: 0x000470DE File Offset: 0x000452DE
		[DomName("colno")]
		public int Column { get; private set; }

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000FEF RID: 4079 RVA: 0x000470E7 File Offset: 0x000452E7
		// (set) Token: 0x06000FF0 RID: 4080 RVA: 0x000470EF File Offset: 0x000452EF
		[DomName("error")]
		public DomException Error { get; private set; }
	}
}
