using System;
using System.IO;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000207 RID: 519
	internal sealed class DeclarationCondition : CssNode, IConditionFunction, ICssNode, IStyleFormattable
	{
		// Token: 0x0600138B RID: 5003 RVA: 0x0004A96C File Offset: 0x00048B6C
		public DeclarationCondition(CssProperty property, CssValue value)
		{
			this._property = property;
			this._value = value;
		}

		// Token: 0x0600138C RID: 5004 RVA: 0x0004A982 File Offset: 0x00048B82
		public bool Check()
		{
			return !(this._property is CssUnknownProperty) && this._property.TrySetValue(this._value);
		}

		// Token: 0x0600138D RID: 5005 RVA: 0x0004A9A4 File Offset: 0x00048BA4
		public override void ToCss(TextWriter writer, IStyleFormatter formatter)
		{
			writer.Write("(");
			writer.Write(formatter.Declaration(this._property.Name, this._value.CssText, this._property.IsImportant));
			writer.Write(")");
		}

		// Token: 0x04000AAD RID: 2733
		private readonly CssProperty _property;

		// Token: 0x04000AAE RID: 2734
		private readonly CssValue _value;
	}
}
