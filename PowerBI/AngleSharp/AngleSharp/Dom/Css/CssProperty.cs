using System;
using System.IO;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020001FF RID: 511
	internal abstract class CssProperty : CssNode, ICssProperty, ICssNode, IStyleFormattable
	{
		// Token: 0x0600115C RID: 4444 RVA: 0x0004805D File Offset: 0x0004625D
		internal CssProperty(string name, PropertyFlags flags = PropertyFlags.None)
		{
			this._name = name;
			this._flags = flags;
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x0600115D RID: 4445 RVA: 0x00048073 File Offset: 0x00046273
		public string Value
		{
			get
			{
				if (this._value == null)
				{
					return Keywords.Initial;
				}
				return this._value.CssText;
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x0600115E RID: 4446 RVA: 0x0004808E File Offset: 0x0004628E
		public bool IsInherited
		{
			get
			{
				return ((this._flags & PropertyFlags.Inherited) == PropertyFlags.Inherited && this.IsInitial) || (this._value != null && this._value.CssText.Is(Keywords.Inherit));
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x0600115F RID: 4447 RVA: 0x000480C4 File Offset: 0x000462C4
		public bool IsAnimatable
		{
			get
			{
				return (this._flags & PropertyFlags.Animatable) == PropertyFlags.Animatable;
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06001160 RID: 4448 RVA: 0x000480D1 File Offset: 0x000462D1
		public bool IsInitial
		{
			get
			{
				return this._value == null || this._value.CssText.Is(Keywords.Initial);
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06001161 RID: 4449 RVA: 0x000480F2 File Offset: 0x000462F2
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06001162 RID: 4450 RVA: 0x000480FA File Offset: 0x000462FA
		// (set) Token: 0x06001163 RID: 4451 RVA: 0x00048102 File Offset: 0x00046302
		public bool IsImportant
		{
			get
			{
				return this._important;
			}
			set
			{
				this._important = value;
			}
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06001164 RID: 4452 RVA: 0x0004810B File Offset: 0x0004630B
		public string CssText
		{
			get
			{
				return this.ToCss();
			}
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06001165 RID: 4453 RVA: 0x00048113 File Offset: 0x00046313
		internal bool HasValue
		{
			get
			{
				return this._value != null;
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06001166 RID: 4454 RVA: 0x0004811E File Offset: 0x0004631E
		internal bool CanBeHashless
		{
			get
			{
				return (this._flags & PropertyFlags.Hashless) == PropertyFlags.Hashless;
			}
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06001167 RID: 4455 RVA: 0x0004812B File Offset: 0x0004632B
		internal bool CanBeUnitless
		{
			get
			{
				return (this._flags & PropertyFlags.Unitless) == PropertyFlags.Unitless;
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06001168 RID: 4456 RVA: 0x00048138 File Offset: 0x00046338
		internal bool CanBeInherited
		{
			get
			{
				return (this._flags & PropertyFlags.Inherited) == PropertyFlags.Inherited;
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06001169 RID: 4457 RVA: 0x00048145 File Offset: 0x00046345
		internal bool IsShorthand
		{
			get
			{
				return (this._flags & PropertyFlags.Shorthand) == PropertyFlags.Shorthand;
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x0600116A RID: 4458
		internal abstract IValueConverter Converter { get; }

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x0600116B RID: 4459 RVA: 0x00048154 File Offset: 0x00046354
		// (set) Token: 0x0600116C RID: 4460 RVA: 0x0004815C File Offset: 0x0004635C
		internal IPropertyValue DeclaredValue
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x0600116D RID: 4461 RVA: 0x00048168 File Offset: 0x00046368
		internal bool TrySetValue(CssValue newValue)
		{
			IPropertyValue propertyValue = this.Converter.Convert(newValue ?? CssValue.Initial);
			if (propertyValue != null)
			{
				this._value = propertyValue;
				return true;
			}
			return false;
		}

		// Token: 0x0600116E RID: 4462 RVA: 0x00048198 File Offset: 0x00046398
		public override void ToCss(TextWriter writer, IStyleFormatter formatter)
		{
			writer.Write(formatter.Declaration(this.Name, this.Value, this.IsImportant));
		}

		// Token: 0x04000A92 RID: 2706
		private readonly PropertyFlags _flags;

		// Token: 0x04000A93 RID: 2707
		private readonly string _name;

		// Token: 0x04000A94 RID: 2708
		private bool _important;

		// Token: 0x04000A95 RID: 2709
		private IPropertyValue _value;
	}
}
