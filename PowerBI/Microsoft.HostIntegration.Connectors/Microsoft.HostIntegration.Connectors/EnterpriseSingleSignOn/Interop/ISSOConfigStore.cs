using System;
using System.Runtime.InteropServices;

namespace Microsoft.EnterpriseSingleSignOn.Interop
{
	// Token: 0x020004AC RID: 1196
	[InterfaceType(ComInterfaceType.InterfaceIsDual)]
	[Guid("846B1755-EFF2-43E3-89D6-B216E6B57507")]
	[CoClass(typeof(SSOConfigStore))]
	[ComImport]
	public interface ISSOConfigStore
	{
		// Token: 0x06002937 RID: 10551
		void SetConfigInfo(string applicationName, string identifier, IPropertyBag properties);

		// Token: 0x06002938 RID: 10552
		void GetConfigInfo(string applicationName, string identifier, int flags, IPropertyBag properties);

		// Token: 0x06002939 RID: 10553
		void DeleteConfigInfo(string applicationName, string identifier);
	}
}
