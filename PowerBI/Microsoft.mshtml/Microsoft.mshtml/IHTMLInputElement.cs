using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200057B RID: 1403
	[Guid("3050F5D2-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLInputElement
	{
		// Token: 0x17002EA2 RID: 11938
		// (get) Token: 0x06008CEE RID: 36078
		// (set) Token: 0x06008CED RID: 36077
		[DispId(2000)]
		string type
		{
			[DispId(2000)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(2000)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17002EA3 RID: 11939
		// (get) Token: 0x06008CF0 RID: 36080
		// (set) Token: 0x06008CEF RID: 36079
		[DispId(-2147413011)]
		string value
		{
			[TypeLibFunc(20)]
			[DispId(-2147413011)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413011)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17002EA4 RID: 11940
		// (get) Token: 0x06008CF2 RID: 36082
		// (set) Token: 0x06008CF1 RID: 36081
		[DispId(-2147418112)]
		string name
		{
			[TypeLibFunc(20)]
			[DispId(-2147418112)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147418112)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17002EA5 RID: 11941
		// (get) Token: 0x06008CF4 RID: 36084
		// (set) Token: 0x06008CF3 RID: 36083
		[DispId(2001)]
		bool status
		{
			[DispId(2001)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(2001)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17002EA6 RID: 11942
		// (get) Token: 0x06008CF6 RID: 36086
		// (set) Token: 0x06008CF5 RID: 36085
		[DispId(-2147418036)]
		bool disabled
		{
			[DispId(-2147418036)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(-2147418036)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17002EA7 RID: 11943
		// (get) Token: 0x06008CF7 RID: 36087
		[DispId(-2147416108)]
		IHTMLFormElement form
		{
			[DispId(-2147416108)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17002EA8 RID: 11944
		// (get) Token: 0x06008CF9 RID: 36089
		// (set) Token: 0x06008CF8 RID: 36088
		[DispId(2002)]
		int size
		{
			[TypeLibFunc(20)]
			[DispId(2002)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(2002)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17002EA9 RID: 11945
		// (get) Token: 0x06008CFB RID: 36091
		// (set) Token: 0x06008CFA RID: 36090
		[DispId(2003)]
		int maxLength
		{
			[DispId(2003)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(2003)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x06008CFC RID: 36092
		[DispId(2004)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void select();

		// Token: 0x17002EAA RID: 11946
		// (get) Token: 0x06008CFE RID: 36094
		// (set) Token: 0x06008CFD RID: 36093
		[DispId(-2147412082)]
		object onchange
		{
			[TypeLibFunc(20)]
			[DispId(-2147412082)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412082)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17002EAB RID: 11947
		// (get) Token: 0x06008D00 RID: 36096
		// (set) Token: 0x06008CFF RID: 36095
		[DispId(-2147412102)]
		object onselect
		{
			[TypeLibFunc(20)]
			[DispId(-2147412102)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412102)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17002EAC RID: 11948
		// (get) Token: 0x06008D02 RID: 36098
		// (set) Token: 0x06008D01 RID: 36097
		[DispId(-2147413029)]
		string defaultValue
		{
			[DispId(-2147413029)]
			[TypeLibFunc(84)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(84)]
			[DispId(-2147413029)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17002EAD RID: 11949
		// (get) Token: 0x06008D04 RID: 36100
		// (set) Token: 0x06008D03 RID: 36099
		[DispId(2005)]
		bool readOnly
		{
			[DispId(2005)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(2005)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x06008D05 RID: 36101
		[DispId(2006)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLTxtRange createTextRange();

		// Token: 0x17002EAE RID: 11950
		// (get) Token: 0x06008D07 RID: 36103
		// (set) Token: 0x06008D06 RID: 36102
		[DispId(2007)]
		bool indeterminate
		{
			[DispId(2007)]
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(2007)]
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17002EAF RID: 11951
		// (get) Token: 0x06008D09 RID: 36105
		// (set) Token: 0x06008D08 RID: 36104
		[DispId(2008)]
		bool defaultChecked
		{
			[DispId(2008)]
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(4)]
			[DispId(2008)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17002EB0 RID: 11952
		// (get) Token: 0x06008D0B RID: 36107
		// (set) Token: 0x06008D0A RID: 36106
		[DispId(2009)]
		bool @checked
		{
			[TypeLibFunc(4)]
			[DispId(2009)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(4)]
			[DispId(2009)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17002EB1 RID: 11953
		// (get) Token: 0x06008D0D RID: 36109
		// (set) Token: 0x06008D0C RID: 36108
		[DispId(2012)]
		object border
		{
			[DispId(2012)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(2012)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17002EB2 RID: 11954
		// (get) Token: 0x06008D0F RID: 36111
		// (set) Token: 0x06008D0E RID: 36110
		[DispId(2013)]
		int vspace
		{
			[DispId(2013)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(2013)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17002EB3 RID: 11955
		// (get) Token: 0x06008D11 RID: 36113
		// (set) Token: 0x06008D10 RID: 36112
		[DispId(2014)]
		int hspace
		{
			[DispId(2014)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(2014)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17002EB4 RID: 11956
		// (get) Token: 0x06008D13 RID: 36115
		// (set) Token: 0x06008D12 RID: 36114
		[DispId(2010)]
		string alt
		{
			[TypeLibFunc(20)]
			[DispId(2010)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(2010)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17002EB5 RID: 11957
		// (get) Token: 0x06008D15 RID: 36117
		// (set) Token: 0x06008D14 RID: 36116
		[DispId(2011)]
		string src
		{
			[TypeLibFunc(20)]
			[DispId(2011)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(2011)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17002EB6 RID: 11958
		// (get) Token: 0x06008D17 RID: 36119
		// (set) Token: 0x06008D16 RID: 36118
		[DispId(2015)]
		string lowsrc
		{
			[DispId(2015)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(2015)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17002EB7 RID: 11959
		// (get) Token: 0x06008D19 RID: 36121
		// (set) Token: 0x06008D18 RID: 36120
		[DispId(2016)]
		string vrml
		{
			[DispId(2016)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(2016)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17002EB8 RID: 11960
		// (get) Token: 0x06008D1B RID: 36123
		// (set) Token: 0x06008D1A RID: 36122
		[DispId(2017)]
		string dynsrc
		{
			[DispId(2017)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(2017)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17002EB9 RID: 11961
		// (get) Token: 0x06008D1C RID: 36124
		[DispId(-2147412996)]
		string readyState
		{
			[DispId(-2147412996)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17002EBA RID: 11962
		// (get) Token: 0x06008D1D RID: 36125
		[DispId(2018)]
		bool complete
		{
			[DispId(2018)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002EBB RID: 11963
		// (get) Token: 0x06008D1F RID: 36127
		// (set) Token: 0x06008D1E RID: 36126
		[DispId(2019)]
		object loop
		{
			[DispId(2019)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(2019)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17002EBC RID: 11964
		// (get) Token: 0x06008D21 RID: 36129
		// (set) Token: 0x06008D20 RID: 36128
		[DispId(-2147418039)]
		string align
		{
			[TypeLibFunc(20)]
			[DispId(-2147418039)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147418039)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17002EBD RID: 11965
		// (get) Token: 0x06008D23 RID: 36131
		// (set) Token: 0x06008D22 RID: 36130
		[DispId(-2147412080)]
		object onload
		{
			[DispId(-2147412080)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412080)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17002EBE RID: 11966
		// (get) Token: 0x06008D25 RID: 36133
		// (set) Token: 0x06008D24 RID: 36132
		[DispId(-2147412083)]
		object onerror
		{
			[DispId(-2147412083)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412083)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17002EBF RID: 11967
		// (get) Token: 0x06008D27 RID: 36135
		// (set) Token: 0x06008D26 RID: 36134
		[DispId(-2147412084)]
		object onabort
		{
			[DispId(-2147412084)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412084)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17002EC0 RID: 11968
		// (get) Token: 0x06008D29 RID: 36137
		// (set) Token: 0x06008D28 RID: 36136
		[DispId(-2147418107)]
		int width
		{
			[DispId(-2147418107)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(-2147418107)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17002EC1 RID: 11969
		// (get) Token: 0x06008D2B RID: 36139
		// (set) Token: 0x06008D2A RID: 36138
		[DispId(-2147418106)]
		int height
		{
			[DispId(-2147418106)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(-2147418106)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17002EC2 RID: 11970
		// (get) Token: 0x06008D2D RID: 36141
		// (set) Token: 0x06008D2C RID: 36140
		[DispId(2020)]
		string Start
		{
			[DispId(2020)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(2020)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}
	}
}
