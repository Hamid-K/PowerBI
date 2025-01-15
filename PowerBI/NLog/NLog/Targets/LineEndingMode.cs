using System;
using System.ComponentModel;
using System.Globalization;
using JetBrains.Annotations;
using NLog.Internal;

namespace NLog.Targets
{
	// Token: 0x02000043 RID: 67
	[TypeConverter(typeof(LineEndingMode.LineEndingModeConverter))]
	public sealed class LineEndingMode : IEquatable<LineEndingMode>
	{
		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060006A9 RID: 1705 RVA: 0x000107AC File Offset: 0x0000E9AC
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060006AA RID: 1706 RVA: 0x000107B4 File Offset: 0x0000E9B4
		public string NewLineCharacters
		{
			get
			{
				return this._newLineCharacters;
			}
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x000107BC File Offset: 0x0000E9BC
		private LineEndingMode()
		{
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x000107C4 File Offset: 0x0000E9C4
		private LineEndingMode(string name, string newLineCharacters)
		{
			this._name = name;
			this._newLineCharacters = newLineCharacters;
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x000107DC File Offset: 0x0000E9DC
		public static LineEndingMode FromString([NotNull] string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Equals(LineEndingMode.CRLF.Name, StringComparison.OrdinalIgnoreCase))
			{
				return LineEndingMode.CRLF;
			}
			if (name.Equals(LineEndingMode.LF.Name, StringComparison.OrdinalIgnoreCase))
			{
				return LineEndingMode.LF;
			}
			if (name.Equals(LineEndingMode.CR.Name, StringComparison.OrdinalIgnoreCase))
			{
				return LineEndingMode.CR;
			}
			if (name.Equals(LineEndingMode.Default.Name, StringComparison.OrdinalIgnoreCase))
			{
				return LineEndingMode.Default;
			}
			if (name.Equals(LineEndingMode.Null.Name, StringComparison.OrdinalIgnoreCase))
			{
				return LineEndingMode.Null;
			}
			if (name.Equals(LineEndingMode.None.Name, StringComparison.OrdinalIgnoreCase))
			{
				return LineEndingMode.None;
			}
			throw new ArgumentOutOfRangeException("name", name, "LineEndingMode is out of range");
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x0001089D File Offset: 0x0000EA9D
		public static bool operator ==(LineEndingMode mode1, LineEndingMode mode2)
		{
			if (mode1 == null)
			{
				return mode2 == null;
			}
			return mode2 != null && mode1.NewLineCharacters == mode2.NewLineCharacters;
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x000108BD File Offset: 0x0000EABD
		public static bool operator !=(LineEndingMode mode1, LineEndingMode mode2)
		{
			if (mode1 == null)
			{
				return mode2 != null;
			}
			return mode2 == null || mode1.NewLineCharacters != mode2.NewLineCharacters;
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x000108DD File Offset: 0x0000EADD
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x000108E5 File Offset: 0x0000EAE5
		public override int GetHashCode()
		{
			if (this._newLineCharacters == null)
			{
				return 0;
			}
			return this._newLineCharacters.GetHashCode();
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x000108FC File Offset: 0x0000EAFC
		public override bool Equals(object obj)
		{
			LineEndingMode lineEndingMode;
			return obj != null && (this == obj || ((lineEndingMode = obj as LineEndingMode) != null && this.Equals(lineEndingMode)));
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x00010927 File Offset: 0x0000EB27
		public bool Equals(LineEndingMode other)
		{
			return other != null && (this == other || string.Equals(this._newLineCharacters, other._newLineCharacters));
		}

		// Token: 0x04000123 RID: 291
		public static readonly LineEndingMode Default = new LineEndingMode("Default", EnvironmentHelper.NewLine);

		// Token: 0x04000124 RID: 292
		public static readonly LineEndingMode CRLF = new LineEndingMode("CRLF", "\r\n");

		// Token: 0x04000125 RID: 293
		public static readonly LineEndingMode CR = new LineEndingMode("CR", "\r");

		// Token: 0x04000126 RID: 294
		public static readonly LineEndingMode LF = new LineEndingMode("LF", "\n");

		// Token: 0x04000127 RID: 295
		public static readonly LineEndingMode Null = new LineEndingMode("Null", "\0");

		// Token: 0x04000128 RID: 296
		public static readonly LineEndingMode None = new LineEndingMode("None", string.Empty);

		// Token: 0x04000129 RID: 297
		private readonly string _name;

		// Token: 0x0400012A RID: 298
		private readonly string _newLineCharacters;

		// Token: 0x02000226 RID: 550
		public class LineEndingModeConverter : TypeConverter
		{
			// Token: 0x06001511 RID: 5393 RVA: 0x00037E2F File Offset: 0x0003602F
			public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
			{
				return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
			}

			// Token: 0x06001512 RID: 5394 RVA: 0x00037E50 File Offset: 0x00036050
			public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
			{
				string text = value as string;
				if (text == null)
				{
					return base.ConvertFrom(context, culture, value);
				}
				return LineEndingMode.FromString(text);
			}
		}
	}
}
