using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x0200002F RID: 47
	public interface ITinyEngine
	{
		// Token: 0x060000AB RID: 171
		string EscapeIdentifier(string identifier);

		// Token: 0x060000AC RID: 172
		string EscapeString(string s);

		// Token: 0x060000AD RID: 173
		string EscapeFieldIdentifier(string identifier);

		// Token: 0x060000AE RID: 174
		ITypeValue Type(TypeHandle kind);
	}
}
