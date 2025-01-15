using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020001FD RID: 509
	internal sealed class CssMedium : CssNode, ICssMedium, ICssNode, IStyleFormattable
	{
		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x0600113D RID: 4413 RVA: 0x00047BF5 File Offset: 0x00045DF5
		public IEnumerable<MediaFeature> Features
		{
			get
			{
				return base.Children.OfType<MediaFeature>();
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x0600113E RID: 4414 RVA: 0x00047C02 File Offset: 0x00045E02
		IEnumerable<IMediaFeature> ICssMedium.Features
		{
			get
			{
				return this.Features;
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x0600113F RID: 4415 RVA: 0x00047C0A File Offset: 0x00045E0A
		// (set) Token: 0x06001140 RID: 4416 RVA: 0x00047C12 File Offset: 0x00045E12
		public string Type { get; internal set; }

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06001141 RID: 4417 RVA: 0x00047C1B File Offset: 0x00045E1B
		// (set) Token: 0x06001142 RID: 4418 RVA: 0x00047C23 File Offset: 0x00045E23
		public bool IsExclusive { get; internal set; }

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06001143 RID: 4419 RVA: 0x00047C2C File Offset: 0x00045E2C
		// (set) Token: 0x06001144 RID: 4420 RVA: 0x00047C34 File Offset: 0x00045E34
		public bool IsInverse { get; internal set; }

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06001145 RID: 4421 RVA: 0x00047C40 File Offset: 0x00045E40
		public string Constraints
		{
			get
			{
				IEnumerable<string> enumerable = this.Features.Select((MediaFeature m) => m.ToCss());
				return string.Join(" and ", enumerable);
			}
		}

		// Token: 0x06001146 RID: 4422 RVA: 0x00047C84 File Offset: 0x00045E84
		public bool Validate(RenderDevice device)
		{
			return (string.IsNullOrEmpty(this.Type) || CssMedium.KnownTypes.Contains(this.Type, StringComparison.Ordinal) != this.IsInverse) && !this.IsInvalid(device) && !this.Features.Any((MediaFeature m) => m.Validate(device) == this.IsInverse);
		}

		// Token: 0x06001147 RID: 4423 RVA: 0x00047CF8 File Offset: 0x00045EF8
		public override bool Equals(object obj)
		{
			CssMedium cssMedium = obj as CssMedium;
			if (cssMedium != null && cssMedium.IsExclusive == this.IsExclusive && cssMedium.IsInverse == this.IsInverse && cssMedium.Type.Is(this.Type) && cssMedium.Features.Count<MediaFeature>() == this.Features.Count<MediaFeature>())
			{
				using (IEnumerator<MediaFeature> enumerator = cssMedium.Features.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						MediaFeature feature = enumerator.Current;
						if (!this.Features.Any((MediaFeature m) => m.Name.Is(feature.Name) && m.Value.Is(feature.Value)))
						{
							return false;
						}
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06001148 RID: 4424 RVA: 0x00004749 File Offset: 0x00002949
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06001149 RID: 4425 RVA: 0x00047DC4 File Offset: 0x00045FC4
		public override void ToCss(TextWriter writer, IStyleFormatter formatter)
		{
			writer.Write(formatter.Medium(this.IsExclusive, this.IsInverse, this.Type, this.Features));
		}

		// Token: 0x0600114A RID: 4426 RVA: 0x00047DEA File Offset: 0x00045FEA
		private bool IsInvalid(RenderDevice device)
		{
			return this.IsInvalid(device, Keywords.Screen, RenderDevice.Kind.Screen) || this.IsInvalid(device, Keywords.Speech, RenderDevice.Kind.Speech) || this.IsInvalid(device, Keywords.Print, RenderDevice.Kind.Printer);
		}

		// Token: 0x0600114B RID: 4427 RVA: 0x00047E19 File Offset: 0x00046019
		private bool IsInvalid(RenderDevice device, string keyword, RenderDevice.Kind kind)
		{
			return keyword.Is(this.Type) && device.DeviceType == kind == this.IsInverse;
		}

		// Token: 0x04000A8C RID: 2700
		private static readonly string[] KnownTypes = new string[]
		{
			Keywords.Screen,
			Keywords.Speech,
			Keywords.Print,
			Keywords.All
		};
	}
}
