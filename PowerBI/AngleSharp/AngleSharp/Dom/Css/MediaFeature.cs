using System;
using System.IO;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000205 RID: 517
	internal abstract class MediaFeature : CssNode, IMediaFeature, ICssNode, IStyleFormattable
	{
		// Token: 0x0600137E RID: 4990 RVA: 0x0004A7A8 File Offset: 0x000489A8
		internal MediaFeature(string name)
		{
			this._name = name;
			this._min = name.StartsWith("min-");
			this._max = name.StartsWith("max-");
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x0600137F RID: 4991 RVA: 0x0004A7D9 File Offset: 0x000489D9
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06001380 RID: 4992 RVA: 0x0004A7E1 File Offset: 0x000489E1
		public bool IsMinimum
		{
			get
			{
				return this._min;
			}
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06001381 RID: 4993 RVA: 0x0004A7E9 File Offset: 0x000489E9
		public bool IsMaximum
		{
			get
			{
				return this._max;
			}
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06001382 RID: 4994 RVA: 0x0004A7F1 File Offset: 0x000489F1
		public string Value
		{
			get
			{
				if (!this.HasValue)
				{
					return string.Empty;
				}
				return this._value.CssText;
			}
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x06001383 RID: 4995 RVA: 0x0004A80C File Offset: 0x00048A0C
		public bool HasValue
		{
			get
			{
				return this._value != null && this._value.Count > 0;
			}
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x06001384 RID: 4996
		internal abstract IValueConverter Converter { get; }

		// Token: 0x06001385 RID: 4997 RVA: 0x0004A828 File Offset: 0x00048A28
		internal bool TrySetValue(CssValue value)
		{
			bool flag;
			if (value == null)
			{
				flag = !this.IsMinimum && !this.IsMaximum && this.Converter.HasDefault();
			}
			else
			{
				flag = this.Converter.Convert(value) != null;
			}
			if (flag)
			{
				this._value = value;
			}
			return flag;
		}

		// Token: 0x06001386 RID: 4998
		public abstract bool Validate(RenderDevice device);

		// Token: 0x06001387 RID: 4999 RVA: 0x0004A878 File Offset: 0x00048A78
		public override void ToCss(TextWriter writer, IStyleFormatter formatter)
		{
			string text = (this.HasValue ? this.Value : null);
			writer.Write(formatter.Constraint(this._name, text));
		}

		// Token: 0x04000AA9 RID: 2729
		private readonly bool _min;

		// Token: 0x04000AAA RID: 2730
		private readonly bool _max;

		// Token: 0x04000AAB RID: 2731
		private readonly string _name;

		// Token: 0x04000AAC RID: 2732
		private CssValue _value;
	}
}
