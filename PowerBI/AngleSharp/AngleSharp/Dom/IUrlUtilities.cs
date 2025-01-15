using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom
{
	// Token: 0x020001A3 RID: 419
	[DomName("URLUtils")]
	[DomNoInterfaceObject]
	public interface IUrlUtilities
	{
		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000ED5 RID: 3797
		// (set) Token: 0x06000ED6 RID: 3798
		[DomName("href")]
		string Href { get; set; }

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000ED7 RID: 3799
		// (set) Token: 0x06000ED8 RID: 3800
		[DomName("protocol")]
		string Protocol { get; set; }

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000ED9 RID: 3801
		// (set) Token: 0x06000EDA RID: 3802
		[DomName("host")]
		string Host { get; set; }

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000EDB RID: 3803
		// (set) Token: 0x06000EDC RID: 3804
		[DomName("hostname")]
		string HostName { get; set; }

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000EDD RID: 3805
		// (set) Token: 0x06000EDE RID: 3806
		[DomName("port")]
		string Port { get; set; }

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000EDF RID: 3807
		// (set) Token: 0x06000EE0 RID: 3808
		[DomName("pathname")]
		string PathName { get; set; }

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000EE1 RID: 3809
		// (set) Token: 0x06000EE2 RID: 3810
		[DomName("search")]
		string Search { get; set; }

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000EE3 RID: 3811
		// (set) Token: 0x06000EE4 RID: 3812
		[DomName("hash")]
		string Hash { get; set; }

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000EE5 RID: 3813
		// (set) Token: 0x06000EE6 RID: 3814
		[DomName("username")]
		string UserName { get; set; }

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000EE7 RID: 3815
		// (set) Token: 0x06000EE8 RID: 3816
		[DomName("password")]
		string Password { get; set; }

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000EE9 RID: 3817
		[DomName("origin")]
		string Origin { get; }
	}
}
