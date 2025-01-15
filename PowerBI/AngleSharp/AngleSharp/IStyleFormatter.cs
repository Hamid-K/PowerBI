using System;
using System.Collections.Generic;

namespace AngleSharp
{
	// Token: 0x02000013 RID: 19
	public interface IStyleFormatter
	{
		// Token: 0x06000074 RID: 116
		string Sheet(IEnumerable<IStyleFormattable> rules);

		// Token: 0x06000075 RID: 117
		string Block(IEnumerable<IStyleFormattable> rules);

		// Token: 0x06000076 RID: 118
		string Declaration(string name, string value, bool important);

		// Token: 0x06000077 RID: 119
		string Declarations(IEnumerable<string> declarations);

		// Token: 0x06000078 RID: 120
		string Medium(bool exclusive, bool inverse, string type, IEnumerable<IStyleFormattable> constraints);

		// Token: 0x06000079 RID: 121
		string Constraint(string name, string value);

		// Token: 0x0600007A RID: 122
		string Rule(string name, string value);

		// Token: 0x0600007B RID: 123
		string Rule(string name, string prelude, string rules);

		// Token: 0x0600007C RID: 124
		string Style(string selector, IStyleFormattable rules);

		// Token: 0x0600007D RID: 125
		string Comment(string data);
	}
}
