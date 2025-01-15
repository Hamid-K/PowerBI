using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures
{
	// Token: 0x02000CB5 RID: 3253
	[NullableContext(1)]
	[Nullable(new byte[] { 0, 1 })]
	public class FontCharacteristics : Tuple<bool, bool, float, string, Color>
	{
		// Token: 0x17000EF8 RID: 3832
		// (get) Token: 0x060053B3 RID: 21427 RVA: 0x00107F25 File Offset: 0x00106125
		public bool Bold
		{
			get
			{
				return base.Item1;
			}
		}

		// Token: 0x17000EF9 RID: 3833
		// (get) Token: 0x060053B4 RID: 21428 RVA: 0x00107F2D File Offset: 0x0010612D
		public bool Italic
		{
			get
			{
				return base.Item2;
			}
		}

		// Token: 0x17000EFA RID: 3834
		// (get) Token: 0x060053B5 RID: 21429 RVA: 0x00107F35 File Offset: 0x00106135
		public float FontSize
		{
			get
			{
				return base.Item3;
			}
		}

		// Token: 0x17000EFB RID: 3835
		// (get) Token: 0x060053B6 RID: 21430 RVA: 0x00107F3D File Offset: 0x0010613D
		public string FontBaseName
		{
			get
			{
				return base.Item4;
			}
		}

		// Token: 0x17000EFC RID: 3836
		// (get) Token: 0x060053B7 RID: 21431 RVA: 0x00107F45 File Offset: 0x00106145
		public Color Color
		{
			get
			{
				return base.Item5;
			}
		}

		// Token: 0x060053B8 RID: 21432 RVA: 0x00107F4D File Offset: 0x0010614D
		public FontCharacteristics(bool bold, bool italic, float fontSize, string fontBaseName, Color color)
			: base(bold, italic, fontSize, fontBaseName, color)
		{
		}

		// Token: 0x060053B9 RID: 21433 RVA: 0x00107F5C File Offset: 0x0010615C
		public bool EqualsExceptFontSize(object obj)
		{
			FontCharacteristics fontCharacteristics = obj as FontCharacteristics;
			return fontCharacteristics != null && this.Bold == fontCharacteristics.Bold && this.Italic == fontCharacteristics.Italic && this.FontBaseName == fontCharacteristics.FontBaseName && EqualityComparer<Color>.Default.Equals(this.Color, fontCharacteristics.Color);
		}

		// Token: 0x060053BA RID: 21434 RVA: 0x00107FBC File Offset: 0x001061BC
		public override string ToString()
		{
			return string.Format("{{ Size: {0}, Font: {1}, Color: {2}, Bold: {3}, Italic: {4} }}", new object[] { this.FontSize, this.FontBaseName, this.Color, this.Bold, this.Italic });
		}

		// Token: 0x060053BB RID: 21435 RVA: 0x0010801A File Offset: 0x0010621A
		public override int GetHashCode()
		{
			if (!this._hashcodeInitialized)
			{
				this._hashcode = base.GetHashCode();
				this._hashcodeInitialized = true;
			}
			return this._hashcode;
		}

		// Token: 0x040025BA RID: 9658
		private bool _hashcodeInitialized;

		// Token: 0x040025BB RID: 9659
		private int _hashcode;
	}
}
