using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.DslLibrary.Numbers;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Semantics.Numbers
{
	// Token: 0x02001D6E RID: 7534
	[Parseable("TryParseXML", ParseHumanReadableString = "TryParseHumanReadable")]
	public class RoundingSpec : Tuple<decimal, decimal, RoundingMode>, IRenderableLiteral
	{
		// Token: 0x0600FD7E RID: 64894 RVA: 0x0036284D File Offset: 0x00360A4D
		public RoundingSpec(decimal zero, decimal delta, RoundingMode mode)
			: base(zero, delta, mode)
		{
		}

		// Token: 0x17002A37 RID: 10807
		// (get) Token: 0x0600FD7F RID: 64895 RVA: 0x00362858 File Offset: 0x00360A58
		public decimal Zero
		{
			get
			{
				return base.Item1;
			}
		}

		// Token: 0x17002A38 RID: 10808
		// (get) Token: 0x0600FD80 RID: 64896 RVA: 0x00362860 File Offset: 0x00360A60
		public decimal Delta
		{
			get
			{
				return base.Item2;
			}
		}

		// Token: 0x17002A39 RID: 10809
		// (get) Token: 0x0600FD81 RID: 64897 RVA: 0x00362868 File Offset: 0x00360A68
		public RoundingMode Mode
		{
			get
			{
				return base.Item3;
			}
		}

		// Token: 0x0600FD82 RID: 64898 RVA: 0x00362870 File Offset: 0x00360A70
		public string RenderHumanReadable()
		{
			return FormattableString.Invariant(FormattableStringFactory.Create("\"({0}, {1}, {2})\"", new object[] { this.Zero, this.Delta, this.Mode }));
		}

		// Token: 0x0600FD83 RID: 64899 RVA: 0x003628BC File Offset: 0x00360ABC
		public XElement RenderXML()
		{
			return new XElement("RoundingSpec", new object[]
			{
				new XAttribute("Zero", this.Zero),
				new XAttribute("Delta", this.Delta),
				new XAttribute("Mode", this.Mode)
			});
		}

		// Token: 0x0600FD84 RID: 64900 RVA: 0x00362938 File Offset: 0x00360B38
		internal static RoundingSpec TryParseXML(XElement literal)
		{
			RoundingSpec roundingSpec;
			try
			{
				if (literal.Name != "RoundingSpec")
				{
					roundingSpec = null;
				}
				else
				{
					if (literal.HasElements)
					{
						throw new NotImplementedException("Unsupported RoundingSpec XML: " + ((literal != null) ? literal.ToString() : null));
					}
					RoundingMode roundingMode;
					if (!Enum.TryParse<RoundingMode>(literal.Attribute("Mode").Value, out roundingMode))
					{
						roundingSpec = null;
					}
					else
					{
						roundingSpec = new RoundingSpec(decimal.Parse(literal.Attribute("Zero").Value, CultureInfo.InvariantCulture), decimal.Parse(literal.Attribute("Delta").Value, CultureInfo.InvariantCulture), roundingMode);
					}
				}
			}
			catch
			{
				roundingSpec = null;
			}
			return roundingSpec;
		}

		// Token: 0x0600FD85 RID: 64901 RVA: 0x00362A08 File Offset: 0x00360C08
		internal static RoundingSpec TryParseHumanReadable(string literal)
		{
			if (literal.Length < 4 || !literal.StartsWith("\"(", StringComparison.Ordinal) || !literal.EndsWith(")\"", StringComparison.Ordinal))
			{
				throw new NotImplementedException("Unsupported RoundingSpec literal: " + literal);
			}
			string[] array = literal.Substring(2, literal.Length - 4).Split(new char[] { ',' });
			if (array.Length != 3)
			{
				throw new NotImplementedException("Unsupported RoundingSpec literal: " + literal);
			}
			RoundingMode roundingMode;
			if (!Enum.TryParse<RoundingMode>(array[2], out roundingMode))
			{
				return null;
			}
			return new RoundingSpec(decimal.Parse(array[0], CultureInfo.InvariantCulture), decimal.Parse(array[1], CultureInfo.InvariantCulture), roundingMode);
		}
	}
}
