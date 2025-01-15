using System;
using System.IO;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000204 RID: 516
	internal abstract class DocumentFunction : CssNode, IDocumentFunction, ICssNode, IStyleFormattable
	{
		// Token: 0x06001379 RID: 4985 RVA: 0x0004A764 File Offset: 0x00048964
		internal DocumentFunction(string name, string data)
		{
			this._name = name;
			this._data = data;
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x0600137A RID: 4986 RVA: 0x0004A77A File Offset: 0x0004897A
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x0600137B RID: 4987 RVA: 0x0004A782 File Offset: 0x00048982
		public string Data
		{
			get
			{
				return this._data;
			}
		}

		// Token: 0x0600137C RID: 4988
		public abstract bool Matches(Url url);

		// Token: 0x0600137D RID: 4989 RVA: 0x0004A78A File Offset: 0x0004898A
		public override void ToCss(TextWriter writer, IStyleFormatter formatter)
		{
			writer.Write(this._name.CssFunction(this._data.CssString()));
		}

		// Token: 0x04000AA7 RID: 2727
		private readonly string _name;

		// Token: 0x04000AA8 RID: 2728
		private readonly string _data;
	}
}
