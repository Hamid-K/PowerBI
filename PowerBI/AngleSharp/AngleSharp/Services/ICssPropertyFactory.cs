using System;
using System.Collections.Generic;
using AngleSharp.Dom.Css;

namespace AngleSharp.Services
{
	// Token: 0x02000028 RID: 40
	internal interface ICssPropertyFactory
	{
		// Token: 0x06000121 RID: 289
		CssProperty Create(string name);

		// Token: 0x06000122 RID: 290
		CssProperty CreateFont(string name);

		// Token: 0x06000123 RID: 291
		CssProperty CreateLonghand(string name);

		// Token: 0x06000124 RID: 292
		CssProperty[] CreateLonghandsFor(string name);

		// Token: 0x06000125 RID: 293
		CssShorthandProperty CreateShorthand(string name);

		// Token: 0x06000126 RID: 294
		CssProperty CreateViewport(string name);

		// Token: 0x06000127 RID: 295
		string[] GetLonghands(string name);

		// Token: 0x06000128 RID: 296
		IEnumerable<string> GetShorthands(string name);

		// Token: 0x06000129 RID: 297
		bool IsAnimatable(string name);

		// Token: 0x0600012A RID: 298
		bool IsLonghand(string name);

		// Token: 0x0600012B RID: 299
		bool IsShorthand(string name);
	}
}
