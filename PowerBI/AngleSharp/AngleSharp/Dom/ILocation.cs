using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom
{
	// Token: 0x0200018E RID: 398
	[DomName("Location")]
	public interface ILocation : IUrlUtilities
	{
		// Token: 0x06000E49 RID: 3657
		[DomName("assign")]
		void Assign(string url);

		// Token: 0x06000E4A RID: 3658
		[DomName("replace")]
		void Replace(string url);

		// Token: 0x06000E4B RID: 3659
		[DomName("reload")]
		void Reload();
	}
}
