using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.DslLibrary.Dates
{
	// Token: 0x02000870 RID: 2160
	public class NumericDateTimeFormatPart : DateTimeFormatPart
	{
		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x06002F05 RID: 12037 RVA: 0x000882C4 File Offset: 0x000864C4
		public int MaximumValue { get; }

		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x06002F06 RID: 12038 RVA: 0x000882CC File Offset: 0x000864CC
		public int MinimumValue { get; }

		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x06002F07 RID: 12039 RVA: 0x000882D4 File Offset: 0x000864D4
		public bool AllowLeadingZeros { get; }

		// Token: 0x17000840 RID: 2112
		// (get) Token: 0x06002F08 RID: 12040 RVA: 0x000882DC File Offset: 0x000864DC
		public override bool UniqueParse
		{
			get
			{
				return !this.AllowLeadingZeros && (base.MatchedPart != DateTimePart.Millisecond.Some<DateTimePart>() || base.MaximumLength <= 3);
			}
		}

		// Token: 0x06002F09 RID: 12041 RVA: 0x0008830C File Offset: 0x0008650C
		public NumericDateTimeFormatPart(char formatChar, DateTimePart matchedPart, int minimumLength, int maximumLength, int minValue, int maxValue, Func<int, int> parseFunc = null, Func<int, int> outputFunc = null, string posixOutputFormatString = null, string posixParsingFormatString = null, FormatAttributes attributes = null)
			: base(string.Concat<char>(Enumerable.Repeat<char>(formatChar, minimumLength)), matchedPart.Some<DateTimePart>(), Microsoft.ProgramSynthesis.DslLibrary.Token.GetStaticTokenByName("Digits").Some<Optional<Token>>(), minimumLength, maximumLength, posixOutputFormatString, posixParsingFormatString, attributes)
		{
			if (minimumLength <= 0 || minimumLength > maximumLength)
			{
				throw new ArgumentException("Minimum length must be positive and no more than the maximum length.", "minimumLength");
			}
			this.MinimumValue = minValue;
			if (maximumLength > 9)
			{
				throw new ArgumentException("Maximum length may not exceed 9 because values must be ints.", "maximumLength");
			}
			this.MaximumValue = maxValue;
			Func<int, int> func = parseFunc;
			if (parseFunc == null && (func = NumericDateTimeFormatPart.<>c.<>9__13_0) == null)
			{
				func = (NumericDateTimeFormatPart.<>c.<>9__13_0 = (int i) => i);
			}
			this._parseFunc = func;
			Func<int, int> func2 = outputFunc;
			if (outputFunc == null && (func2 = NumericDateTimeFormatPart.<>c.<>9__13_1) == null)
			{
				func2 = (NumericDateTimeFormatPart.<>c.<>9__13_1 = (int i) => i);
			}
			this._outputFunc = func2;
			if (attributes != null && attributes.Attributes.ContainsKey("allowLeadingZeros"))
			{
				this.AllowLeadingZeros = true;
				attributes.MarkAttributeAsHandled("allowLeadingZeros");
			}
			if (attributes != null && attributes.Attributes.ContainsKey("disallow24Hour"))
			{
				attributes.MarkAttributeAsHandled("disallow24Hour");
			}
			if (attributes != null && attributes.HasUnhandledAttributes)
			{
				throw new ArgumentException(FormattableString.Invariant(FormattableStringFactory.Create("Unsupported attributes for format char '{0}': {1}", new object[] { formatChar, attributes })), "attributes");
			}
		}

		// Token: 0x06002F0A RID: 12042 RVA: 0x0008846C File Offset: 0x0008666C
		public override string FormatStringFor(DateTimeFormat.Target target, bool strict = true)
		{
			string baseFormatString = base.BaseFormatString;
			if (baseFormatString != null)
			{
				switch (baseFormatString.Length)
				{
				case 1:
				{
					char c = baseFormatString[0];
					if (c <= 'V')
					{
						if (c != 'H')
						{
							if (c != 'M')
							{
								if (c == 'V')
								{
									if (base.Attributes == null)
									{
										string text;
										switch (target)
										{
										case DateTimeFormat.Target.PosixFormatting:
											text = (strict ? null : "%V");
											break;
										case DateTimeFormat.Target.PosixParsing:
											text = (strict ? null : "%V");
											break;
										case DateTimeFormat.Target.MomentJS:
											text = "W";
											break;
										case DateTimeFormat.Target.DayJSFormatting:
											text = null;
											break;
										case DateTimeFormat.Target.PowerAppsFormatting:
											text = null;
											break;
										default:
											throw new NotImplementedException("unsupported target: " + target.ToString());
										}
										return text;
									}
									if (base.Attributes.Equals(DateTimeFormatPart.AllowLeadingZerosFormatAttributes))
									{
										string text;
										switch (target)
										{
										case DateTimeFormat.Target.PosixFormatting:
											text = (strict ? null : "%V");
											break;
										case DateTimeFormat.Target.PosixParsing:
											text = "%V";
											break;
										case DateTimeFormat.Target.MomentJS:
											text = "W";
											break;
										case DateTimeFormat.Target.DayJSFormatting:
											text = null;
											break;
										case DateTimeFormat.Target.PowerAppsFormatting:
											text = null;
											break;
										default:
											throw new NotImplementedException("unsupported target: " + target.ToString());
										}
										return text;
									}
									throw new NotImplementedException("unsupported format: " + ((this != null) ? this.ToString() : null));
								}
							}
							else
							{
								if (base.Attributes == null)
								{
									string text;
									switch (target)
									{
									case DateTimeFormat.Target.PosixFormatting:
										text = (strict ? null : "%m");
										break;
									case DateTimeFormat.Target.PosixParsing:
										text = (strict ? null : "%m");
										break;
									case DateTimeFormat.Target.MomentJS:
										text = "M";
										break;
									case DateTimeFormat.Target.DayJSFormatting:
										text = "M";
										break;
									case DateTimeFormat.Target.PowerAppsFormatting:
										text = "m";
										break;
									default:
										throw new NotImplementedException("unsupported target: " + target.ToString());
									}
									return text;
								}
								if (base.Attributes.Equals(DateTimeFormatPart.AllowLeadingZerosFormatAttributes))
								{
									string text;
									switch (target)
									{
									case DateTimeFormat.Target.PosixFormatting:
										text = (strict ? null : "%m");
										break;
									case DateTimeFormat.Target.PosixParsing:
										text = "%m";
										break;
									case DateTimeFormat.Target.MomentJS:
										text = "M";
										break;
									case DateTimeFormat.Target.DayJSFormatting:
										text = "M";
										break;
									case DateTimeFormat.Target.PowerAppsFormatting:
										text = "m";
										break;
									default:
										throw new NotImplementedException("unsupported target: " + target.ToString());
									}
									return text;
								}
								throw new NotImplementedException("unsupported format: " + ((this != null) ? this.ToString() : null));
							}
						}
						else
						{
							if (base.Attributes == null || base.Attributes.Equals(DateTimeFormatPart.AllowLeadingZerosFormatAttributes) || base.Attributes.Equals(DateTimeFormatPart.Disallow24HourFormatAttributes))
							{
								string text;
								switch (target)
								{
								case DateTimeFormat.Target.PosixFormatting:
									text = (strict ? null : "%H");
									break;
								case DateTimeFormat.Target.PosixParsing:
									text = (strict ? null : "%H");
									break;
								case DateTimeFormat.Target.MomentJS:
									text = "H";
									break;
								case DateTimeFormat.Target.DayJSFormatting:
									text = "H";
									break;
								case DateTimeFormat.Target.PowerAppsFormatting:
									text = "h";
									break;
								default:
									throw new NotImplementedException("unsupported target: " + target.ToString());
								}
								return text;
							}
							if (base.Attributes.Equals(DateTimeFormatPart.AllowLeadingZerosAndDisallow24HourFormatAttributes))
							{
								string text;
								switch (target)
								{
								case DateTimeFormat.Target.PosixFormatting:
									text = (strict ? null : "%H");
									break;
								case DateTimeFormat.Target.PosixParsing:
									text = "%H";
									break;
								case DateTimeFormat.Target.MomentJS:
									text = "H";
									break;
								case DateTimeFormat.Target.DayJSFormatting:
									text = "H";
									break;
								case DateTimeFormat.Target.PowerAppsFormatting:
									text = "h";
									break;
								default:
									throw new NotImplementedException("unsupported target: " + target.ToString());
								}
								return text;
							}
							throw new NotImplementedException("unsupported format: " + ((this != null) ? this.ToString() : null));
						}
					}
					else if (c <= 'q')
					{
						switch (c)
						{
						case 'd':
							if (base.Attributes == null)
							{
								string text;
								switch (target)
								{
								case DateTimeFormat.Target.PosixFormatting:
									text = (strict ? null : "%d");
									break;
								case DateTimeFormat.Target.PosixParsing:
									text = (strict ? null : "%d");
									break;
								case DateTimeFormat.Target.MomentJS:
									text = "D";
									break;
								case DateTimeFormat.Target.DayJSFormatting:
									text = "D";
									break;
								case DateTimeFormat.Target.PowerAppsFormatting:
									text = "d";
									break;
								default:
									throw new NotImplementedException("unsupported target: " + target.ToString());
								}
								return text;
							}
							if (base.Attributes.Equals(DateTimeFormatPart.AllowLeadingZerosFormatAttributes))
							{
								string text;
								switch (target)
								{
								case DateTimeFormat.Target.PosixFormatting:
									text = (strict ? null : "%d");
									break;
								case DateTimeFormat.Target.PosixParsing:
									text = "%d";
									break;
								case DateTimeFormat.Target.MomentJS:
									text = "D";
									break;
								case DateTimeFormat.Target.DayJSFormatting:
									text = "D";
									break;
								case DateTimeFormat.Target.PowerAppsFormatting:
									text = "d";
									break;
								default:
									throw new NotImplementedException("unsupported target: " + target.ToString());
								}
								return text;
							}
							throw new NotImplementedException("unsupported format: " + ((this != null) ? this.ToString() : null));
						case 'e':
						case 'g':
						case 'k':
						case 'l':
							break;
						case 'f':
							if (base.Attributes == null)
							{
								string text;
								switch (target)
								{
								case DateTimeFormat.Target.PosixFormatting:
									text = null;
									break;
								case DateTimeFormat.Target.PosixParsing:
									text = null;
									break;
								case DateTimeFormat.Target.MomentJS:
									text = "S";
									break;
								case DateTimeFormat.Target.DayJSFormatting:
									text = null;
									break;
								case DateTimeFormat.Target.PowerAppsFormatting:
									text = null;
									break;
								default:
									throw new NotImplementedException("unsupported target: " + target.ToString());
								}
								return text;
							}
							throw new NotImplementedException("unsupported format: " + ((this != null) ? this.ToString() : null));
						case 'h':
							if (base.Attributes == null)
							{
								string text;
								switch (target)
								{
								case DateTimeFormat.Target.PosixFormatting:
									text = (strict ? null : "%I");
									break;
								case DateTimeFormat.Target.PosixParsing:
									text = (strict ? null : "%I");
									break;
								case DateTimeFormat.Target.MomentJS:
									text = "h";
									break;
								case DateTimeFormat.Target.DayJSFormatting:
									text = "h";
									break;
								case DateTimeFormat.Target.PowerAppsFormatting:
									text = "h";
									break;
								default:
									throw new NotImplementedException("unsupported target: " + target.ToString());
								}
								return text;
							}
							if (base.Attributes.Equals(DateTimeFormatPart.AllowLeadingZerosFormatAttributes))
							{
								string text;
								switch (target)
								{
								case DateTimeFormat.Target.PosixFormatting:
									text = (strict ? null : "%I");
									break;
								case DateTimeFormat.Target.PosixParsing:
									text = "%I";
									break;
								case DateTimeFormat.Target.MomentJS:
									text = "h";
									break;
								case DateTimeFormat.Target.DayJSFormatting:
									text = "h";
									break;
								case DateTimeFormat.Target.PowerAppsFormatting:
									text = "h";
									break;
								default:
									throw new NotImplementedException("unsupported target: " + target.ToString());
								}
								return text;
							}
							throw new NotImplementedException("unsupported format: " + ((this != null) ? this.ToString() : null));
						case 'i':
							return null;
						case 'j':
							if (base.Attributes == null)
							{
								string text;
								switch (target)
								{
								case DateTimeFormat.Target.PosixFormatting:
									text = null;
									break;
								case DateTimeFormat.Target.PosixParsing:
									text = null;
									break;
								case DateTimeFormat.Target.MomentJS:
									text = "DDD";
									break;
								case DateTimeFormat.Target.DayJSFormatting:
									text = null;
									break;
								case DateTimeFormat.Target.PowerAppsFormatting:
									text = null;
									break;
								default:
									throw new NotImplementedException("unsupported target: " + target.ToString());
								}
								return text;
							}
							if (base.Attributes.Equals(DateTimeFormatPart.AllowLeadingZerosFormatAttributes))
							{
								string text;
								switch (target)
								{
								case DateTimeFormat.Target.PosixFormatting:
									text = (strict ? null : "%j");
									break;
								case DateTimeFormat.Target.PosixParsing:
									text = "%j";
									break;
								case DateTimeFormat.Target.MomentJS:
									text = "DDD";
									break;
								case DateTimeFormat.Target.DayJSFormatting:
									text = null;
									break;
								case DateTimeFormat.Target.PowerAppsFormatting:
									text = null;
									break;
								default:
									throw new NotImplementedException("unsupported target: " + target.ToString());
								}
								return text;
							}
							throw new NotImplementedException("unsupported format: " + ((this != null) ? this.ToString() : null));
						case 'm':
							if (base.Attributes == null)
							{
								string text;
								switch (target)
								{
								case DateTimeFormat.Target.PosixFormatting:
									text = (strict ? null : "%M");
									break;
								case DateTimeFormat.Target.PosixParsing:
									text = (strict ? null : "%M");
									break;
								case DateTimeFormat.Target.MomentJS:
									text = "m";
									break;
								case DateTimeFormat.Target.DayJSFormatting:
									text = "m";
									break;
								case DateTimeFormat.Target.PowerAppsFormatting:
									text = "m";
									break;
								default:
									throw new NotImplementedException("unsupported target: " + target.ToString());
								}
								return text;
							}
							if (base.Attributes.Equals(DateTimeFormatPart.AllowLeadingZerosFormatAttributes))
							{
								string text;
								switch (target)
								{
								case DateTimeFormat.Target.PosixFormatting:
									text = (strict ? null : "%M");
									break;
								case DateTimeFormat.Target.PosixParsing:
									text = "%M";
									break;
								case DateTimeFormat.Target.MomentJS:
									text = "m";
									break;
								case DateTimeFormat.Target.DayJSFormatting:
									text = "m";
									break;
								case DateTimeFormat.Target.PowerAppsFormatting:
									text = "m";
									break;
								default:
									throw new NotImplementedException("unsupported target: " + target.ToString());
								}
								return text;
							}
							throw new NotImplementedException("unsupported format: " + ((this != null) ? this.ToString() : null));
						default:
							if (c == 'q')
							{
								if (base.Attributes == null)
								{
									string text;
									switch (target)
									{
									case DateTimeFormat.Target.PosixFormatting:
										text = null;
										break;
									case DateTimeFormat.Target.PosixParsing:
										text = null;
										break;
									case DateTimeFormat.Target.MomentJS:
										text = "Q";
										break;
									case DateTimeFormat.Target.DayJSFormatting:
										text = null;
										break;
									case DateTimeFormat.Target.PowerAppsFormatting:
										text = null;
										break;
									default:
										throw new NotImplementedException("unsupported target: " + target.ToString());
									}
									return text;
								}
								throw new NotImplementedException("unsupported format: " + ((this != null) ? this.ToString() : null));
							}
							break;
						}
					}
					else if (c != 's')
					{
						if (c == 'y')
						{
							return null;
						}
					}
					else
					{
						if (base.Attributes == null)
						{
							string text;
							switch (target)
							{
							case DateTimeFormat.Target.PosixFormatting:
								text = (strict ? null : "%S");
								break;
							case DateTimeFormat.Target.PosixParsing:
								text = (strict ? null : "%S");
								break;
							case DateTimeFormat.Target.MomentJS:
								text = (strict ? null : "s");
								break;
							case DateTimeFormat.Target.DayJSFormatting:
								text = (strict ? null : "s");
								break;
							case DateTimeFormat.Target.PowerAppsFormatting:
								text = (strict ? null : "s");
								break;
							default:
								throw new NotImplementedException("unsupported target: " + target.ToString());
							}
							return text;
						}
						if (base.Attributes.Equals(DateTimeFormatPart.AllowLeadingZerosFormatAttributes))
						{
							string text;
							switch (target)
							{
							case DateTimeFormat.Target.PosixFormatting:
								text = (strict ? null : "%S");
								break;
							case DateTimeFormat.Target.PosixParsing:
								text = "%S";
								break;
							case DateTimeFormat.Target.MomentJS:
								text = (strict ? null : "s");
								break;
							case DateTimeFormat.Target.DayJSFormatting:
								text = (strict ? null : "s");
								break;
							case DateTimeFormat.Target.PowerAppsFormatting:
								text = (strict ? null : "s");
								break;
							default:
								throw new NotImplementedException("unsupported target: " + target.ToString());
							}
							return text;
						}
						throw new NotImplementedException("unsupported format: " + ((this != null) ? this.ToString() : null));
					}
					break;
				}
				case 2:
				{
					char c = baseFormatString[0];
					if (c <= 'Y')
					{
						if (c <= 'M')
						{
							if (c != 'H')
							{
								if (c == 'M')
								{
									if (baseFormatString == "MM")
									{
										if (base.Attributes == null)
										{
											string text;
											switch (target)
											{
											case DateTimeFormat.Target.PosixFormatting:
												text = "%m";
												break;
											case DateTimeFormat.Target.PosixParsing:
												text = (strict ? null : "%m");
												break;
											case DateTimeFormat.Target.MomentJS:
												text = "MM";
												break;
											case DateTimeFormat.Target.DayJSFormatting:
												text = "MM";
												break;
											case DateTimeFormat.Target.PowerAppsFormatting:
												text = "mm";
												break;
											default:
												throw new NotImplementedException("unsupported target: " + target.ToString());
											}
											return text;
										}
										throw new NotImplementedException("unsupported format: " + ((this != null) ? this.ToString() : null));
									}
								}
							}
							else if (baseFormatString == "HH")
							{
								if (base.Attributes == null || base.Attributes.Equals(DateTimeFormatPart.Disallow24HourFormatAttributes))
								{
									string text;
									switch (target)
									{
									case DateTimeFormat.Target.PosixFormatting:
										text = "%H";
										break;
									case DateTimeFormat.Target.PosixParsing:
										text = (strict ? null : "%H");
										break;
									case DateTimeFormat.Target.MomentJS:
										text = "HH";
										break;
									case DateTimeFormat.Target.DayJSFormatting:
										text = "HH";
										break;
									case DateTimeFormat.Target.PowerAppsFormatting:
										text = "hh";
										break;
									default:
										throw new NotImplementedException("unsupported target: " + target.ToString());
									}
									return text;
								}
								throw new NotImplementedException("unsupported format: " + ((this != null) ? this.ToString() : null));
							}
						}
						else if (c != 'V')
						{
							if (c == 'Y')
							{
								if (baseFormatString == "YY")
								{
									return null;
								}
							}
						}
						else if (baseFormatString == "VV")
						{
							if (base.Attributes == null)
							{
								string text;
								switch (target)
								{
								case DateTimeFormat.Target.PosixFormatting:
									text = (strict ? null : "%V");
									break;
								case DateTimeFormat.Target.PosixParsing:
									text = (strict ? null : "%V");
									break;
								case DateTimeFormat.Target.MomentJS:
									text = "WW";
									break;
								case DateTimeFormat.Target.DayJSFormatting:
									text = null;
									break;
								case DateTimeFormat.Target.PowerAppsFormatting:
									text = null;
									break;
								default:
									throw new NotImplementedException("unsupported target: " + target.ToString());
								}
								return text;
							}
							throw new NotImplementedException("unsupported format: " + ((this != null) ? this.ToString() : null));
						}
					}
					else if (c <= 'm')
					{
						switch (c)
						{
						case 'd':
							if (baseFormatString == "dd")
							{
								if (base.Attributes == null)
								{
									string text;
									switch (target)
									{
									case DateTimeFormat.Target.PosixFormatting:
										text = "%d";
										break;
									case DateTimeFormat.Target.PosixParsing:
										text = (strict ? null : "%d");
										break;
									case DateTimeFormat.Target.MomentJS:
										text = "DD";
										break;
									case DateTimeFormat.Target.DayJSFormatting:
										text = "DD";
										break;
									case DateTimeFormat.Target.PowerAppsFormatting:
										text = "dd";
										break;
									default:
										throw new NotImplementedException("unsupported target: " + target.ToString());
									}
									return text;
								}
								throw new NotImplementedException("unsupported format: " + ((this != null) ? this.ToString() : null));
							}
							break;
						case 'e':
						case 'g':
							break;
						case 'f':
							if (baseFormatString == "ff")
							{
								if (base.Attributes == null)
								{
									string text;
									switch (target)
									{
									case DateTimeFormat.Target.PosixFormatting:
										text = null;
										break;
									case DateTimeFormat.Target.PosixParsing:
										text = null;
										break;
									case DateTimeFormat.Target.MomentJS:
										text = "SS";
										break;
									case DateTimeFormat.Target.DayJSFormatting:
										text = null;
										break;
									case DateTimeFormat.Target.PowerAppsFormatting:
										text = null;
										break;
									default:
										throw new NotImplementedException("unsupported target: " + target.ToString());
									}
									return text;
								}
								throw new NotImplementedException("unsupported format: " + ((this != null) ? this.ToString() : null));
							}
							break;
						case 'h':
							if (baseFormatString == "hh")
							{
								if (base.Attributes == null)
								{
									string text;
									switch (target)
									{
									case DateTimeFormat.Target.PosixFormatting:
										text = "%I";
										break;
									case DateTimeFormat.Target.PosixParsing:
										text = (strict ? null : "%I");
										break;
									case DateTimeFormat.Target.MomentJS:
										text = "hh";
										break;
									case DateTimeFormat.Target.DayJSFormatting:
										text = "hh";
										break;
									case DateTimeFormat.Target.PowerAppsFormatting:
										text = "hh";
										break;
									default:
										throw new NotImplementedException("unsupported target: " + target.ToString());
									}
									return text;
								}
								throw new NotImplementedException("unsupported format: " + ((this != null) ? this.ToString() : null));
							}
							break;
						default:
							if (c == 'm')
							{
								if (baseFormatString == "mm")
								{
									if (base.Attributes == null)
									{
										string text;
										switch (target)
										{
										case DateTimeFormat.Target.PosixFormatting:
											text = "%M";
											break;
										case DateTimeFormat.Target.PosixParsing:
											text = (strict ? null : "%M");
											break;
										case DateTimeFormat.Target.MomentJS:
											text = "mm";
											break;
										case DateTimeFormat.Target.DayJSFormatting:
											text = "mm";
											break;
										case DateTimeFormat.Target.PowerAppsFormatting:
											text = "mm";
											break;
										default:
											throw new NotImplementedException("unsupported target: " + target.ToString());
										}
										return text;
									}
									throw new NotImplementedException("unsupported format: " + ((this != null) ? this.ToString() : null));
								}
							}
							break;
						}
					}
					else if (c != 's')
					{
						if (c == 'y')
						{
							if (baseFormatString == "yy")
							{
								if (base.Attributes == null)
								{
									string text;
									switch (target)
									{
									case DateTimeFormat.Target.PosixFormatting:
										text = "%y";
										break;
									case DateTimeFormat.Target.PosixParsing:
										text = (strict ? null : "%y");
										break;
									case DateTimeFormat.Target.MomentJS:
										text = "YY";
										break;
									case DateTimeFormat.Target.DayJSFormatting:
										text = "YY";
										break;
									case DateTimeFormat.Target.PowerAppsFormatting:
										text = "yy";
										break;
									default:
										throw new NotImplementedException("unsupported target: " + target.ToString());
									}
									return text;
								}
								throw new NotImplementedException("unsupported format: " + ((this != null) ? this.ToString() : null));
							}
						}
					}
					else if (baseFormatString == "ss")
					{
						if (base.Attributes == null)
						{
							string text;
							switch (target)
							{
							case DateTimeFormat.Target.PosixFormatting:
								text = "%S";
								break;
							case DateTimeFormat.Target.PosixParsing:
								text = (strict ? null : "%S");
								break;
							case DateTimeFormat.Target.MomentJS:
								text = (strict ? null : "ss");
								break;
							case DateTimeFormat.Target.DayJSFormatting:
								text = (strict ? null : "ss");
								break;
							case DateTimeFormat.Target.PowerAppsFormatting:
								text = (strict ? null : "ss");
								break;
							default:
								throw new NotImplementedException("unsupported target: " + target.ToString());
							}
							return text;
						}
						throw new NotImplementedException("unsupported format: " + ((this != null) ? this.ToString() : null));
					}
					break;
				}
				case 3:
				{
					char c = baseFormatString[0];
					if (c != 'f')
					{
						if (c != 'j')
						{
							if (c == 'y')
							{
								if (baseFormatString == "yyy")
								{
									return null;
								}
							}
						}
						else if (baseFormatString == "jjj")
						{
							if (base.Attributes == null)
							{
								string text;
								switch (target)
								{
								case DateTimeFormat.Target.PosixFormatting:
									text = "%j";
									break;
								case DateTimeFormat.Target.PosixParsing:
									text = (strict ? null : "%j");
									break;
								case DateTimeFormat.Target.MomentJS:
									text = "DDDD";
									break;
								case DateTimeFormat.Target.DayJSFormatting:
									text = null;
									break;
								case DateTimeFormat.Target.PowerAppsFormatting:
									text = null;
									break;
								default:
									throw new NotImplementedException("unsupported target: " + target.ToString());
								}
								return text;
							}
							throw new NotImplementedException("unsupported format: " + ((this != null) ? this.ToString() : null));
						}
					}
					else if (baseFormatString == "fff")
					{
						if (base.Attributes == null)
						{
							string text;
							switch (target)
							{
							case DateTimeFormat.Target.PosixFormatting:
								text = null;
								break;
							case DateTimeFormat.Target.PosixParsing:
								text = null;
								break;
							case DateTimeFormat.Target.MomentJS:
								text = "SSS";
								break;
							case DateTimeFormat.Target.DayJSFormatting:
								text = "SSS";
								break;
							case DateTimeFormat.Target.PowerAppsFormatting:
								text = null;
								break;
							default:
								throw new NotImplementedException("unsupported target: " + target.ToString());
							}
							return text;
						}
						throw new NotImplementedException("unsupported format: " + ((this != null) ? this.ToString() : null));
					}
					break;
				}
				case 4:
				{
					char c = baseFormatString[0];
					if (c != 'Y')
					{
						if (c != 'f')
						{
							if (c == 'y')
							{
								if (baseFormatString == "yyyy")
								{
									if (base.Attributes == null)
									{
										string text;
										switch (target)
										{
										case DateTimeFormat.Target.PosixFormatting:
											text = "%Y";
											break;
										case DateTimeFormat.Target.PosixParsing:
											text = "%Y";
											break;
										case DateTimeFormat.Target.MomentJS:
											text = "YYYY";
											break;
										case DateTimeFormat.Target.DayJSFormatting:
											text = "YYYY";
											break;
										case DateTimeFormat.Target.PowerAppsFormatting:
											text = "yyyy";
											break;
										default:
											throw new NotImplementedException("unsupported target: " + target.ToString());
										}
										return text;
									}
									throw new NotImplementedException("unsupported format: " + ((this != null) ? this.ToString() : null));
								}
							}
						}
						else if (baseFormatString == "ffff")
						{
							if (base.Attributes == null)
							{
								string text;
								switch (target)
								{
								case DateTimeFormat.Target.PosixFormatting:
									text = null;
									break;
								case DateTimeFormat.Target.PosixParsing:
									text = null;
									break;
								case DateTimeFormat.Target.MomentJS:
									text = null;
									break;
								case DateTimeFormat.Target.DayJSFormatting:
									text = null;
									break;
								case DateTimeFormat.Target.PowerAppsFormatting:
									text = null;
									break;
								default:
									throw new NotImplementedException("unsupported target: " + target.ToString());
								}
								return text;
							}
							throw new NotImplementedException("unsupported format: " + ((this != null) ? this.ToString() : null));
						}
					}
					else if (baseFormatString == "YYYY")
					{
						return null;
					}
					break;
				}
				case 5:
				{
					char c = baseFormatString[0];
					if (c != 'f')
					{
						if (c == 'y')
						{
							if (baseFormatString == "yyyyy")
							{
								return null;
							}
						}
					}
					else if (baseFormatString == "fffff")
					{
						if (base.Attributes == null)
						{
							string text;
							switch (target)
							{
							case DateTimeFormat.Target.PosixFormatting:
								text = null;
								break;
							case DateTimeFormat.Target.PosixParsing:
								text = null;
								break;
							case DateTimeFormat.Target.MomentJS:
								text = null;
								break;
							case DateTimeFormat.Target.DayJSFormatting:
								text = null;
								break;
							case DateTimeFormat.Target.PowerAppsFormatting:
								text = null;
								break;
							default:
								throw new NotImplementedException("unsupported target: " + target.ToString());
							}
							return text;
						}
						throw new NotImplementedException("unsupported format: " + ((this != null) ? this.ToString() : null));
					}
					break;
				}
				case 6:
					if (baseFormatString == "ffffff")
					{
						if (base.Attributes == null)
						{
							string text;
							switch (target)
							{
							case DateTimeFormat.Target.PosixFormatting:
								text = null;
								break;
							case DateTimeFormat.Target.PosixParsing:
								text = null;
								break;
							case DateTimeFormat.Target.MomentJS:
								text = null;
								break;
							case DateTimeFormat.Target.DayJSFormatting:
								text = null;
								break;
							case DateTimeFormat.Target.PowerAppsFormatting:
								text = null;
								break;
							default:
								throw new NotImplementedException("unsupported target: " + target.ToString());
							}
							return text;
						}
						throw new NotImplementedException("unsupported format: " + ((this != null) ? this.ToString() : null));
					}
					break;
				case 7:
					if (baseFormatString == "fffffff")
					{
						if (base.Attributes == null)
						{
							string text;
							switch (target)
							{
							case DateTimeFormat.Target.PosixFormatting:
								text = null;
								break;
							case DateTimeFormat.Target.PosixParsing:
								text = null;
								break;
							case DateTimeFormat.Target.MomentJS:
								text = null;
								break;
							case DateTimeFormat.Target.DayJSFormatting:
								text = null;
								break;
							case DateTimeFormat.Target.PowerAppsFormatting:
								text = null;
								break;
							default:
								throw new NotImplementedException("unsupported target: " + target.ToString());
							}
							return text;
						}
						throw new NotImplementedException("unsupported format: " + ((this != null) ? this.ToString() : null));
					}
					break;
				}
			}
			throw new NotImplementedException("unsupported format part: " + base.BaseFormatString);
		}

		// Token: 0x06002F0B RID: 12043 RVA: 0x000899C4 File Offset: 0x00087BC4
		internal static Optional<int> TryParse(string str, int start, int end)
		{
			int num = 0;
			for (int i = start; i < end; i++)
			{
				char c = str[i];
				if (c < '0' || c > '9')
				{
					return Optional<int>.Nothing;
				}
				num *= 10;
				num += (int)(c - '0');
			}
			return num.Some<int>();
		}

		// Token: 0x06002F0C RID: 12044 RVA: 0x00089A09 File Offset: 0x00087C09
		internal override IEnumerable<Record<StringRegion, int>> ParseAllNext(StringRegion sr)
		{
			char c = sr.Source[(int)sr.Start];
			if (c < '0' || c > '9')
			{
				yield break;
			}
			uint len = (uint)((c == '0' && !this.AllowLeadingZeros) ? base.MinimumLength : base.MaximumLength);
			while ((ulong)len >= (ulong)((long)base.MinimumLength))
			{
				if (len <= sr.Length)
				{
					uint num = sr.Start + len;
					Optional<int> optional = this._ParseSubString(sr.Source, (int)sr.Start, (int)num);
					if (optional.HasValue)
					{
						yield return Record.Create<StringRegion, int>(sr.Slice(sr.Start, num), optional.Value);
					}
				}
				uint num2 = len;
				len = num2 - 1U;
			}
			yield break;
		}

		// Token: 0x06002F0D RID: 12045 RVA: 0x00089A20 File Offset: 0x00087C20
		internal override Optional<int> ParseFullString(string str)
		{
			if (str.Length > base.MaximumLength || str.Length < base.MinimumLength || (str.Length > base.MinimumLength && !this.AllowLeadingZeros && str[0] == '0'))
			{
				return Optional<int>.Nothing;
			}
			return this._ParseSubString(str, 0, str.Length);
		}

		// Token: 0x06002F0E RID: 12046 RVA: 0x00089A7E File Offset: 0x00087C7E
		private Optional<int> _ParseSubString(string str, int start, int end)
		{
			return (from parsed in NumericDateTimeFormatPart.TryParse(str, start, end)
				where parsed >= this.MinimumValue && parsed <= this.MaximumValue
				select parsed).Select(this._parseFunc);
		}

		// Token: 0x06002F0F RID: 12047 RVA: 0x00089AA4 File Offset: 0x00087CA4
		protected internal override Optional<Record<StringRegion, int>> ParseNext(StringRegion sr)
		{
			return this.ParseAllNext(sr).MaybeFirst<Record<StringRegion, int>>();
		}

		// Token: 0x06002F10 RID: 12048 RVA: 0x00089AB4 File Offset: 0x00087CB4
		protected override string ToString(int value)
		{
			return this._outputFunc(value).ToString(CultureInfo.InvariantCulture).PadLeft(base.MinimumLength, '0');
		}

		// Token: 0x17000841 RID: 2113
		// (get) Token: 0x06002F11 RID: 12049 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override bool IsNumeric
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002F12 RID: 12050 RVA: 0x00089AE7 File Offset: 0x00087CE7
		public override XElement RenderXML()
		{
			return base.RenderXML("NumericFormatPart");
		}

		// Token: 0x06002F13 RID: 12051 RVA: 0x00089AF4 File Offset: 0x00087CF4
		public new static NumericDateTimeFormatPart TryParseFromXML(XElement literal)
		{
			if (literal.Name != "NumericFormatPart")
			{
				return null;
			}
			Optional<FormatAttributes> optional = DateTimeFormatPart.ParseAttributesFromXML(literal);
			XAttribute xattribute = literal.Attribute("BaseFormatString");
			if (!optional.HasValue || xattribute == null)
			{
				return null;
			}
			NumericDateTimeFormatPart numericDateTimeFormatPart = DateTimeFormatPart.Create(xattribute.Value, optional.Value) as NumericDateTimeFormatPart;
			if (numericDateTimeFormatPart != null)
			{
				return numericDateTimeFormatPart;
			}
			return null;
		}

		// Token: 0x04001703 RID: 5891
		private readonly Func<int, int> _outputFunc;

		// Token: 0x04001704 RID: 5892
		private readonly Func<int, int> _parseFunc;

		// Token: 0x04001705 RID: 5893
		internal const string XMLName = "NumericFormatPart";
	}
}
