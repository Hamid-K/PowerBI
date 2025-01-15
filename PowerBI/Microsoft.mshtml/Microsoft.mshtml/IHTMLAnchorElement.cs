using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000397 RID: 919
	[TypeLibType(4160)]
	[DefaultMember("href")]
	[Guid("3050F1DA-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLAnchorElement
	{
		// Token: 0x1700138D RID: 5005
		// (get) Token: 0x06003AA8 RID: 15016
		// (set) Token: 0x06003AA7 RID: 15015
		[DispId(0)]
		[IndexerName("href")]
		string href
		{
			[DispId(0)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(0)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700138E RID: 5006
		// (get) Token: 0x06003AAA RID: 15018
		// (set) Token: 0x06003AA9 RID: 15017
		[DispId(1003)]
		string target
		{
			[TypeLibFunc(20)]
			[DispId(1003)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1003)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700138F RID: 5007
		// (get) Token: 0x06003AAC RID: 15020
		// (set) Token: 0x06003AAB RID: 15019
		[DispId(1005)]
		string rel
		{
			[DispId(1005)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1005)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17001390 RID: 5008
		// (get) Token: 0x06003AAE RID: 15022
		// (set) Token: 0x06003AAD RID: 15021
		[DispId(1006)]
		string rev
		{
			[TypeLibFunc(20)]
			[DispId(1006)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1006)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17001391 RID: 5009
		// (get) Token: 0x06003AB0 RID: 15024
		// (set) Token: 0x06003AAF RID: 15023
		[DispId(1007)]
		string urn
		{
			[DispId(1007)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1007)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17001392 RID: 5010
		// (get) Token: 0x06003AB2 RID: 15026
		// (set) Token: 0x06003AB1 RID: 15025
		[DispId(1008)]
		string Methods
		{
			[TypeLibFunc(20)]
			[DispId(1008)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(1008)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17001393 RID: 5011
		// (get) Token: 0x06003AB4 RID: 15028
		// (set) Token: 0x06003AB3 RID: 15027
		[DispId(-2147418112)]
		string name
		{
			[DispId(-2147418112)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147418112)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17001394 RID: 5012
		// (get) Token: 0x06003AB6 RID: 15030
		// (set) Token: 0x06003AB5 RID: 15029
		[DispId(1012)]
		string host
		{
			[DispId(1012)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1012)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17001395 RID: 5013
		// (get) Token: 0x06003AB8 RID: 15032
		// (set) Token: 0x06003AB7 RID: 15031
		[DispId(1013)]
		string hostname
		{
			[DispId(1013)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1013)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17001396 RID: 5014
		// (get) Token: 0x06003ABA RID: 15034
		// (set) Token: 0x06003AB9 RID: 15033
		[DispId(1014)]
		string pathname
		{
			[DispId(1014)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1014)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17001397 RID: 5015
		// (get) Token: 0x06003ABC RID: 15036
		// (set) Token: 0x06003ABB RID: 15035
		[DispId(1015)]
		string port
		{
			[DispId(1015)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1015)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17001398 RID: 5016
		// (get) Token: 0x06003ABE RID: 15038
		// (set) Token: 0x06003ABD RID: 15037
		[DispId(1016)]
		string protocol
		{
			[DispId(1016)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1016)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17001399 RID: 5017
		// (get) Token: 0x06003AC0 RID: 15040
		// (set) Token: 0x06003ABF RID: 15039
		[DispId(1017)]
		string search
		{
			[DispId(1017)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1017)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700139A RID: 5018
		// (get) Token: 0x06003AC2 RID: 15042
		// (set) Token: 0x06003AC1 RID: 15041
		[DispId(1018)]
		string hash
		{
			[DispId(1018)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1018)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700139B RID: 5019
		// (get) Token: 0x06003AC4 RID: 15044
		// (set) Token: 0x06003AC3 RID: 15043
		[DispId(-2147412097)]
		object onblur
		{
			[TypeLibFunc(20)]
			[DispId(-2147412097)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412097)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700139C RID: 5020
		// (get) Token: 0x06003AC6 RID: 15046
		// (set) Token: 0x06003AC5 RID: 15045
		[DispId(-2147412098)]
		object onfocus
		{
			[TypeLibFunc(20)]
			[DispId(-2147412098)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412098)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700139D RID: 5021
		// (get) Token: 0x06003AC8 RID: 15048
		// (set) Token: 0x06003AC7 RID: 15047
		[DispId(-2147416107)]
		string accessKey
		{
			[TypeLibFunc(20)]
			[DispId(-2147416107)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147416107)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700139E RID: 5022
		// (get) Token: 0x06003AC9 RID: 15049
		[DispId(1031)]
		string protocolLong
		{
			[DispId(1031)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700139F RID: 5023
		// (get) Token: 0x06003ACA RID: 15050
		[DispId(1030)]
		string mimeType
		{
			[DispId(1030)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170013A0 RID: 5024
		// (get) Token: 0x06003ACB RID: 15051
		[DispId(1032)]
		string nameProp
		{
			[DispId(1032)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170013A1 RID: 5025
		// (get) Token: 0x06003ACD RID: 15053
		// (set) Token: 0x06003ACC RID: 15052
		[DispId(-2147418097)]
		short tabIndex
		{
			[DispId(-2147418097)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147418097)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x06003ACE RID: 15054
		[DispId(-2147416112)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void focus();

		// Token: 0x06003ACF RID: 15055
		[DispId(-2147416110)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void blur();
	}
}
