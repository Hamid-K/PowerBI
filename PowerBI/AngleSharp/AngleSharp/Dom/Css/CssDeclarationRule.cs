using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AngleSharp.Extensions;
using AngleSharp.Parser.Css;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002F8 RID: 760
	internal abstract class CssDeclarationRule : CssRule, ICssProperties, IEnumerable<ICssProperty>, IEnumerable
	{
		// Token: 0x060015EF RID: 5615 RVA: 0x0004DE0B File Offset: 0x0004C00B
		internal CssDeclarationRule(CssRuleType type, string name, CssParser parser)
			: base(type, parser)
		{
			this._name = name;
		}

		// Token: 0x1700059F RID: 1439
		public string this[string propertyName]
		{
			get
			{
				return this.GetValue(propertyName);
			}
		}

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x060015F1 RID: 5617 RVA: 0x0004827A File Offset: 0x0004647A
		public IEnumerable<CssProperty> Declarations
		{
			get
			{
				return base.Children.OfType<CssProperty>();
			}
		}

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x060015F2 RID: 5618 RVA: 0x0004DE25 File Offset: 0x0004C025
		public int Length
		{
			get
			{
				return this.Declarations.Count<CssProperty>();
			}
		}

		// Token: 0x060015F3 RID: 5619 RVA: 0x0004DE1C File Offset: 0x0004C01C
		public string GetPropertyValue(string propertyName)
		{
			return this.GetValue(propertyName);
		}

		// Token: 0x060015F4 RID: 5620 RVA: 0x0000C295 File Offset: 0x0000A495
		public string GetPropertyPriority(string propertyName)
		{
			return null;
		}

		// Token: 0x060015F5 RID: 5621 RVA: 0x0004DE32 File Offset: 0x0004C032
		public void SetProperty(string propertyName, string propertyValue, string priority = null)
		{
			this.SetValue(propertyName, propertyValue);
		}

		// Token: 0x060015F6 RID: 5622 RVA: 0x0004DE3C File Offset: 0x0004C03C
		public string RemoveProperty(string propertyName)
		{
			foreach (CssProperty cssProperty in this.Declarations)
			{
				if (cssProperty.HasValue && cssProperty.Name.Is(propertyName))
				{
					string value = cssProperty.Value;
					base.RemoveChild(cssProperty);
					return value;
				}
			}
			return null;
		}

		// Token: 0x060015F7 RID: 5623 RVA: 0x0004DEAC File Offset: 0x0004C0AC
		public IEnumerator<ICssProperty> GetEnumerator()
		{
			return this.Declarations.GetEnumerator();
		}

		// Token: 0x060015F8 RID: 5624 RVA: 0x0004DEB9 File Offset: 0x0004C0B9
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060015F9 RID: 5625 RVA: 0x0004DEC4 File Offset: 0x0004C0C4
		internal void SetProperty(CssProperty property)
		{
			foreach (CssProperty cssProperty in this.Declarations)
			{
				if (cssProperty.Name.Is(property.Name))
				{
					base.ReplaceChild(cssProperty, property);
					return;
				}
			}
			base.AppendChild(property);
		}

		// Token: 0x060015FA RID: 5626 RVA: 0x0004DF30 File Offset: 0x0004C130
		public override void ToCss(TextWriter writer, IStyleFormatter formatter)
		{
			CssDeclarationRule.FormatTransporter formatTransporter = new CssDeclarationRule.FormatTransporter(this.Declarations);
			string text = formatter.Style("@" + this._name, formatTransporter);
			writer.Write(text);
		}

		// Token: 0x060015FB RID: 5627
		protected abstract CssProperty CreateNewProperty(string name);

		// Token: 0x060015FC RID: 5628 RVA: 0x0004DF70 File Offset: 0x0004C170
		protected string GetValue(string propertyName)
		{
			foreach (CssProperty cssProperty in this.Declarations)
			{
				if (cssProperty.HasValue && cssProperty.Name.Is(propertyName))
				{
					return cssProperty.Value;
				}
			}
			return string.Empty;
		}

		// Token: 0x060015FD RID: 5629 RVA: 0x0004DFDC File Offset: 0x0004C1DC
		protected void SetValue(string propertyName, string valueText)
		{
			foreach (CssProperty cssProperty in this.Declarations)
			{
				if (cssProperty.Name.Is(propertyName))
				{
					CssValue cssValue = base.Parser.ParseValue(valueText);
					cssProperty.TrySetValue(cssValue);
					return;
				}
			}
			CssProperty cssProperty2 = this.CreateNewProperty(propertyName);
			if (cssProperty2 != null)
			{
				CssValue cssValue2 = base.Parser.ParseValue(valueText);
				cssProperty2.TrySetValue(cssValue2);
				base.AppendChild(cssProperty2);
			}
		}

		// Token: 0x04000C8A RID: 3210
		private readonly string _name;

		// Token: 0x02000501 RID: 1281
		private struct FormatTransporter : IStyleFormattable
		{
			// Token: 0x0600262F RID: 9775 RVA: 0x00062C65 File Offset: 0x00060E65
			public FormatTransporter(IEnumerable<CssProperty> properties)
			{
				this._properties = properties.Where((CssProperty m) => m.HasValue);
			}

			// Token: 0x06002630 RID: 9776 RVA: 0x00062C94 File Offset: 0x00060E94
			public void ToCss(TextWriter writer, IStyleFormatter formatter)
			{
				IEnumerable<string> enumerable = this._properties.Select((CssProperty m) => m.ToCss(formatter));
				string text = formatter.Declarations(enumerable);
				writer.Write(text);
			}

			// Token: 0x04001225 RID: 4645
			private readonly IEnumerable<CssProperty> _properties;
		}
	}
}
