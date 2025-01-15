using System;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.DslLibrary.Dates
{
	// Token: 0x02000819 RID: 2073
	public class ConstantDateTimeFormatPart : DateTimeFormatPart
	{
		// Token: 0x06002CA5 RID: 11429 RVA: 0x0007DB8D File Offset: 0x0007BD8D
		public ConstantDateTimeFormatPart(StringRegion constant)
			: this(constant.Value)
		{
		}

		// Token: 0x06002CA6 RID: 11430 RVA: 0x0007DB9B File Offset: 0x0007BD9B
		public ConstantDateTimeFormatPart(string constant)
			: this(constant, DateTimeFormatUtil.EscapeForPosix(constant))
		{
		}

		// Token: 0x06002CA7 RID: 11431 RVA: 0x0007DBAC File Offset: 0x0007BDAC
		private ConstantDateTimeFormatPart(string constant, string posixFormat)
			: base(DateTimeFormatUtil.Escape(constant), Optional<DateTimePart>.Nothing, Optional<Microsoft.ProgramSynthesis.DslLibrary.Token>.Nothing, constant.Length, constant.Length, posixFormat, posixFormat, null)
		{
			this.ConstantString = constant;
		}

		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x06002CA8 RID: 11432 RVA: 0x0007DBE5 File Offset: 0x0007BDE5
		public string ConstantString { get; }

		// Token: 0x06002CA9 RID: 11433 RVA: 0x0007DBF0 File Offset: 0x0007BDF0
		public override string FormatStringFor(DateTimeFormat.Target target, bool strict = true)
		{
			switch (target)
			{
			case DateTimeFormat.Target.PosixFormatting:
				return DateTimeFormatUtil.EscapeForPosix(this.ConstantString);
			case DateTimeFormat.Target.MomentJS:
				return DateTimeFormatUtil.EscapeForMomentJS(this.ConstantString);
			case DateTimeFormat.Target.DayJSFormatting:
				return DateTimeFormatUtil.EscapeForDayJS(this.ConstantString);
			case DateTimeFormat.Target.PowerAppsFormatting:
				return DateTimeFormatUtil.EscapeForPowerApps(this.ConstantString);
			}
			throw new NotImplementedException();
		}

		// Token: 0x06002CAA RID: 11434 RVA: 0x0007DC4E File Offset: 0x0007BE4E
		protected override string ToString(int value)
		{
			return this.ConstantString;
		}

		// Token: 0x06002CAB RID: 11435 RVA: 0x0007DC56 File Offset: 0x0007BE56
		protected internal override Optional<Record<StringRegion, int>> ParseNext(StringRegion sr)
		{
			if (sr.StartsWith(this.ConstantString))
			{
				return Record.Create<StringRegion, int>(sr.Slice(sr.Start, sr.Start + (uint)this.ConstantString.Length), 0).Some<Record<StringRegion, int>>();
			}
			return Optional<Record<StringRegion, int>>.Nothing;
		}

		// Token: 0x06002CAC RID: 11436 RVA: 0x0007DC95 File Offset: 0x0007BE95
		internal static DateTimeFormatPart FromConstantFormat(string format)
		{
			return new ConstantDateTimeFormatPart(DateTimeFormatUtil.Unescape(format));
		}

		// Token: 0x06002CAD RID: 11437 RVA: 0x0007DCA2 File Offset: 0x0007BEA2
		public override XElement RenderXML()
		{
			return new XElement("ConstantFormatPart", new XCData(this.ConstantString));
		}

		// Token: 0x06002CAE RID: 11438 RVA: 0x0007DCBE File Offset: 0x0007BEBE
		public new static ConstantDateTimeFormatPart TryParseFromXML(XElement literal)
		{
			if (!(literal.Name == "ConstantFormatPart"))
			{
				return null;
			}
			return new ConstantDateTimeFormatPart(literal.Value);
		}

		// Token: 0x0400155C RID: 5468
		internal const string XMLName = "ConstantFormatPart";
	}
}
