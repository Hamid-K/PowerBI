using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Explanations.Default
{
	// Token: 0x020019C1 RID: 6593
	public class DefaultExplanationTemplateProvider : IExplanationTemplateProvider
	{
		// Token: 0x0600D73F RID: 55103 RVA: 0x002DB41C File Offset: 0x002D961C
		public DefaultExplanationTemplateProvider()
			: this(new CultureInfo("en-US"))
		{
		}

		// Token: 0x0600D740 RID: 55104 RVA: 0x002DB42E File Offset: 0x002D962E
		public DefaultExplanationTemplateProvider(CultureInfo culture)
		{
			this._culture = culture;
		}

		// Token: 0x0600D741 RID: 55105 RVA: 0x002DB440 File Offset: 0x002D9640
		public string FormatDelimiter(string delimiter)
		{
			if (delimiter != null)
			{
				switch (delimiter.Length)
				{
				case 0:
					return "nothing";
				case 1:
				{
					char c = delimiter[0];
					if (c <= '/')
					{
						if (c <= ' ')
						{
							if (c == '\n')
							{
								return "line break";
							}
							if (c == ' ')
							{
								return "space";
							}
						}
						else
						{
							if (c == '$')
							{
								return "dollar sign";
							}
							switch (c)
							{
							case ',':
								return "comma";
							case '-':
								return "dash";
							case '.':
								return "period";
							case '/':
								return "slash";
							}
						}
					}
					else if (c <= ';')
					{
						if (c == ':')
						{
							return "colon";
						}
						if (c == ';')
						{
							return "semicolon";
						}
					}
					else
					{
						if (c == '\\')
						{
							return "backslash";
						}
						if (c == '_')
						{
							return "underscore";
						}
					}
					break;
				}
				case 2:
					if (delimiter == "\r\n")
					{
						return "line break";
					}
					break;
				}
			}
			return "\"" + delimiter + "\"";
		}

		// Token: 0x0600D742 RID: 55106 RVA: 0x002DB564 File Offset: 0x002D9764
		public string FormatReplacement(string key, object value)
		{
			if (value is int)
			{
				return ((int)value).ToString("#,##0.########################", this._culture);
			}
			if (value is double)
			{
				return ((double)value).ToString("#,##0.########################", this._culture);
			}
			if (value is decimal)
			{
				return ((decimal)value).ToString("#,##0.########################", this._culture);
			}
			string text = value as string;
			if (text != null)
			{
				if (!(key == "ColumnName"))
				{
					return text;
				}
				return DefaultExplanationTemplateProvider.CleanColumnName(text);
			}
			else
			{
				if (value == null)
				{
					return null;
				}
				return value.ToString();
			}
		}

		// Token: 0x0600D743 RID: 55107 RVA: 0x002DB604 File Offset: 0x002D9804
		public string Template(string key)
		{
			Dictionary<string, string> dictionary;
			if (!DefaultExplanationTemplateProvider._templateByCulture.TryGetValue(this._culture, out dictionary))
			{
				Dictionary<CultureInfo, Dictionary<string, string>> templateByCulture = DefaultExplanationTemplateProvider._templateByCulture;
				lock (templateByCulture)
				{
					dictionary = (DefaultExplanationTemplateProvider._templateByCulture[this._culture] = DefaultExplanationTemplateProvider.LoadTemplates(this._culture));
				}
			}
			string text;
			if (dictionary == null || !dictionary.TryGetValue(key, out text))
			{
				return null;
			}
			return text;
		}

		// Token: 0x0600D744 RID: 55108 RVA: 0x00004FAE File Offset: 0x000031AE
		private static string CleanColumnName(string columnName)
		{
			return columnName;
		}

		// Token: 0x0600D745 RID: 55109 RVA: 0x002DB684 File Offset: 0x002D9884
		private static Dictionary<string, string> LoadTemplates(CultureInfo culture)
		{
			Dictionary<string, string> templates = new Dictionary<string, string>();
			if (culture.Name != "en-US")
			{
				return templates;
			}
			templates.Set("Add/LeftWholeColumn/RightWholeColumn", "Add column {RightColumnName} to column {LeftColumnName}.").Set("Add/LeftWholeColumn/RightExtractNumber", "Add a number extracted from column {RightColumnName} to column {LeftColumnName}.").Set("Add/LeftWholeColumn/RightParseNumber", "Add column {RightColumnName} to column {LeftColumnName}.")
				.Set("Add/LeftWholeColumn/RightConstant", "Add {RightConstant} to column {LeftColumnName}.")
				.Set("Add/LeftExtractNumber/RightWholeColumn", "Add column {RightColumnName} to a number extracted from column {LeftColumnName}.")
				.Set("Add/LeftExtractNumber/RightExtractNumber", "Add a number extracted from column {RightColumnName} to a number extracted from column {LeftColumnName}.")
				.Set("Add/LeftExtractNumber/RightParseNumber", "Add column {RightColumnName} from column {LeftColumnName}.")
				.Set("Add/LeftExtractNumber/RightConstant", "Add {RightConstant} to a number extracted from column {LeftColumnName}.")
				.Set("Add/LeftParseNumber/RightExtractNumber", "Add column {RightColumnName} to a number extracted from column {LeftColumnName}.")
				.Set("Add/LeftParseNumber/RightParseNumber", "Add column {RightColumnName} to column {LeftColumnName}.")
				.Set("Add/LeftParseNumber/RightConstant", "Add {RightConstant} to column {LeftColumnName}.");
			templates.Set("Subtract/LeftWholeColumn/RightWholeColumn", "Subtract column {RightColumnName} from column {LeftColumnName}.").Set("Subtract/LeftWholeColumn/RightExtractNumber", "Subtract column a number extracted from column {RightColumnName} from column {LeftColumnName}.").Set("Subtract/LeftWholeColumn/RightParseNumber", "Subtract column {RightColumnName} from column {LeftColumnName}.")
				.Set("Subtract/LeftWholeColumn/RightConstant", "Subtract {RightConstant} from column {LeftColumnName}.")
				.Set("Subtract/LeftExtractNumber/RightWholeColumn", "Subtract column {RightColumnName} from a number extracted from column {LeftColumnName}.")
				.Set("Subtract/LeftExtractNumber/RightExtractNumber", "Subtract a number extracted from column {RightColumnName} from a number extracted from column {LeftColumnName}.")
				.Set("Subtract/LeftExtractNumber/RightParseNumber", "Subtract column {RightColumnName} from column {LeftColumnName}.")
				.Set("Subtract/LeftExtractNumber/RightConstant", "Subtract {RightConstant} from a number extracted from column {LeftColumnName}.")
				.Set("Subtract/LeftParseNumber/RightExtractNumber", "Subtract column {RightColumnName} from a number extracted from column {LeftColumnName}.")
				.Set("Subtract/LeftParseNumber/RightParseNumber", "Subtract column {RightColumnName} from column {LeftColumnName}.")
				.Set("Subtract/LeftParseNumber/RightConstant", "Subtract {RightConstant} from column {LeftColumnName}.");
			templates.Set("Multiply/LeftWholeColumn/RightWholeColumn", "Multiply column {LeftColumnName} by column {RightColumnName}.").Set("Multiply/LeftWholeColumn/RightExtractNumber", "Multiply column a number extracted from column {LeftColumnName} by column {RightColumnName}.").Set("Multiply/LeftWholeColumn/RightParseNumber", "Multiply column {LeftColumnName} by column {RightColumnName}.")
				.Set("Multiply/LeftWholeColumn/RightConstant", "Multiply column {LeftColumnName} by {RightConstant}.")
				.Set("Multiply/LeftExtractNumber/RightWholeColumn", "Multiply column {LeftColumnName} by a number extracted from column {RightColumnName}.")
				.Set("Multiply/LeftExtractNumber/RightExtractNumber", "Multiply a number extracted from column {LeftColumnName} by a number extracted from column {RightColumnName}.")
				.Set("Multiply/LeftExtractNumber/RightParseNumber", "Multiply column {LeftColumnName} by column {RightColumnName}.")
				.Set("Multiply/LeftExtractNumber/RightConstant", "Multiply a number extracted from column {LeftColumnName} by {RightConstant}.")
				.Set("Multiply/LeftParseNumber/RightExtractNumber", "Multiply column {LeftColumnName} by a number extracted from column {RightColumnName}.")
				.Set("Multiply/LeftParseNumber/RightParseNumber", "Multiply column {LeftColumnName} by column {RightColumnName}.")
				.Set("Multiply/LeftParseNumber/RightConstant", "Multiply column {LeftColumnName} by {RightConstant}.");
			templates.Set("Divide/LeftWholeColumn/RightWholeColumn", "Divide column {LeftColumnName} by column {RightColumnName}.").Set("Divide/LeftWholeColumn/RightExtractNumber", "Divide column a number extracted from column {LeftColumnName} by column {RightColumnName}.").Set("Divide/LeftWholeColumn/RightParseNumber", "Divide column {LeftColumnName} by column {RightColumnName}.")
				.Set("Divide/LeftWholeColumn/RightConstant", "Divide column {LeftColumnName} by {RightConstant}.")
				.Set("Divide/LeftExtractNumber/RightWholeColumn", "Divide column {LeftColumnName} by a number extracted from column {RightColumnName}.")
				.Set("Divide/LeftExtractNumber/RightExtractNumber", "Divide a number extracted from column {LeftColumnName} by a number extracted from column {RightColumnName}.")
				.Set("Divide/LeftExtractNumber/RightParseNumber", "Divide column {LeftColumnName} by column {RightColumnName}.")
				.Set("Divide/LeftExtractNumber/RightConstant", "Divide a number extracted from column {LeftColumnName} by {RightConstant}.")
				.Set("Divide/LeftParseNumber/RightExtractNumber", "Divide column {LeftColumnName} by a number extracted from column {RightColumnName}.")
				.Set("Divide/LeftParseNumber/RightParseNumber", "Divide column {LeftColumnName} by column {RightColumnName}.")
				.Set("Divide/LeftParseNumber/RightConstant", "Divide column {LeftColumnName} by {RightConstant}.");
			templates.Set("Sum", "Add columns {ColumnNames}.").Set("Product", "Multiply columns {ColumnNames}.").Set("Average", "Average columns {ColumnNames}.");
			templates.Set("Number/Format", "Format column {ColumnName}.").Set("ParseNumber", "Parse a number from column {ColumnName}.").Set("ExtractNumber", "Extract a number from column {ColumnName}.")
				.Set("ExtractNumber/Format", "Extract a number from column {ColumnName} and format the result.");
			templates.Set("Number/Round/Down", "Round column {ColumnName} down by {Delta}").Set("Number/Round/Up", "Round column {ColumnName} up by {Delta}").Set("Number/Round/Nearest", "Round column {ColumnName} to the nearest {Delta}");
			(from p in templates
				where p.Key.StartsWith("Number/") && !p.Key.EndsWith("/Format")
				select p.Key).ToList<string>().ForEach(delegate(string key)
			{
				templates[key + "/Format"] = templates[key] + " and format the result.";
			});
			(from p in templates
				where p.Key.StartsWith("Number/")
				select p.Key).ToList<string>().ForEach(delegate(string key)
			{
				templates["Parse" + key] = templates[key];
			});
			templates.Set("DateTime/Format", "Format column {ColumnName}.").Set("ParseDateTime", "Parse a date from column {ColumnName}.").Set("ExtractDateTime", "Extract a date from column {ColumnName}.")
				.Set("ExtractDateTime/Format", templates["ExtractDateTime"]);
			string text = "Round column {ColumnName} down by ";
			templates.Set("DateTime/Round/Down/Second", text + "second").Set("DateTime/Round/Down/Minute", text + "minute").Set("DateTime/Round/Down/Hour", text + "hour")
				.Set("DateTime/Round/Down/Day", text + "day")
				.Set("DateTime/Round/Down/Week", text + "week")
				.Set("DateTime/Round/Down/Quarter", text + "quarter")
				.Set("DateTime/Round/Down/Month", text + "month")
				.Set("DateTime/Round/Down/Year", text + "year");
			text = "Round column {ColumnName} up by ";
			templates.Set("DateTime/Round/Up/Second", text + "second").Set("DateTime/Round/Up/Minute", text + "minute").Set("DateTime/Round/Up/Hour", text + "hour")
				.Set("DateTime/Round/Up/Day", text + "day")
				.Set("DateTime/Round/Up/Week", text + "week")
				.Set("DateTime/Round/Up/Quarter", text + "quarter")
				.Set("DateTime/Round/Up/Month", text + "month")
				.Set("DateTime/Round/Up/Year", text + "year");
			text = "Round column {ColumnName} to the nearest ";
			templates.Set("DateTime/Round/Nearest/Second", text + "second").Set("DateTime/Round/Nearest/Minute", text + "minute").Set("DateTime/Round/Nearest/Hour", text + "hour")
				.Set("DateTime/Round/Nearest/Day", text + "day")
				.Set("DateTime/Round/Nearest/Week", text + "week")
				.Set("DateTime/Round/Nearest/Quarter", text + "quarter")
				.Set("DateTime/Round/Nearest/Month", text + "month")
				.Set("DateTime/Round/Nearest/Year", text + "year");
			templates.Set("DateTime/Round/Up/Week/LastDay", "Round column {ColumnName} up to the last day of the week").Set("DateTime/Round/Up/Month/LastDay", "Round column {ColumnName} up to the last day of the month").Set("DateTime/Round/Up/Quarter/LastDay", "Round column {ColumnName} up to the last day of the quarter")
				.Set("DateTime/Round/Up/Year/LastDay", "Round column {ColumnName} up to the last day of the year");
			templates.Set("DateTime/Part/Second", "Extract the second from column {ColumnName}").Set("DateTime/Part/Minute", "Extract the minute from column {ColumnName}").Set("DateTime/Part/Hour", "Extract the hour from column {ColumnName}")
				.Set("DateTime/Part/WeekDay", "Extract the day of week from column {ColumnName}")
				.Set("DateTime/Part/MonthDay", "Extract the day of month from column {ColumnName}")
				.Set("DateTime/Part/MonthWeek", "Extract the week of month from column {ColumnName}")
				.Set("DateTime/Part/MonthDays", "Extract the number of days in month from column {ColumnName}")
				.Set("DateTime/Part/Month", "Extract the month from column {ColumnName}")
				.Set("DateTime/Part/QuarterDay", "Extract the day of the quarter from column {ColumnName}")
				.Set("DateTime/Part/QuarterWeek", "Extract the week of the quarter from column {ColumnName}")
				.Set("DateTime/Part/QuarterDays", "Extract the number of days in the quarter from column {ColumnName}")
				.Set("DateTime/Part/Quarter", "Extract the quarter from column {ColumnName}")
				.Set("DateTime/Part/YearDay", "Extract the day of year from column {ColumnName}")
				.Set("DateTime/Part/YearWeek", "Extract the week of year from column {ColumnName}")
				.Set("DateTime/Part/YearDays", "Extract the number of days in the year from column {ColumnName}")
				.Set("DateTime/Part/Year", "Extract the year from column {ColumnName}");
			(from p in templates
				where p.Key.StartsWith("DateTime/") && !p.Key.EndsWith("/Format")
				select p.Key).ToList<string>().ForEach(delegate(string key)
			{
				templates[key + "/Format"] = templates[key] + " and format the result.";
			});
			(from p in templates
				where p.Key.StartsWith("DateTime/")
				select p.Key).ToList<string>().ForEach(delegate(string key)
			{
				templates["Parse" + key] = templates[key];
			});
			templates.Set("Length", "Get the length of column {ColumnName}.").Set("Trim", "Trim column {ColumnName}.").Set("Replace", "Replace {FindText} with {ReplaceText} in column {ColumnName}.")
				.Set("LowerCase", "Convert column {ColumnName} to lower case.")
				.Set("UpperCase", "Convert column {ColumnName} to upper case.")
				.Set("ProperCase", "Convert column {ColumnName} to proper case.");
			templates.Set("Split", "Extract text in column {ColumnName}.").Set("Split/1", "Split column {ColumnName} by {Delimiter} and take the first segment.").Set("Split/-1", "Split column {ColumnName} by {Delimiter} and take the last segment.")
				.Set("Split/N", "Split column {ColumnName} by {Delimiter} and take segment {N}.")
				.Set("Split/-N", "Split column {ColumnName} by {Delimiter} and take the {N} to last segment.");
			templates.Set("SlicePrefix", "Take text from the beginning of column {ColumnName}.").Set("SlicePrefix/Absolute/1", "Take the first character in column {ColumnName}.").Set("SlicePrefix/Absolute/N", "Take the first {N} characters in column {ColumnName}.")
				.Set("SlicePrefix/Absolute/-1", "Take all text except the last character in column {ColumnName}.")
				.Set("SlicePrefix/Absolute/-N", "Take all text except the last {N} characters in column {ColumnName}.")
				.Set("SlicePrefix/Find", "Take text before {Delimiter} in column {ColumnName}.");
			templates.Set("SliceSuffix", "Take text from the end of column {ColumnName}.").Set("SliceSuffix/Absolute/1", "Extract all text except the first character in column {ColumnName}.").Set("SliceSuffix/Absolute/N", "Extract all text except the first {N} characters in column {ColumnName}.")
				.Set("SliceSuffix/Absolute/-1", "Extract the last character in column {ColumnName}.")
				.Set("SliceSuffix/Absolute/-N", "Extract the last {N} characters in column {ColumnName}.")
				.Set("SliceSuffix/Find", "Extract text after {Delimiter} in column {ColumnName}.")
				.Set("SliceInfix", "Extract text from column {ColumnName}.")
				.Set("SliceInfix/LeftAbsolute/RightAbsolute", "Take {Length} characters in column {ColumnName} starting at position {LeftPosition}.")
				.Set("SliceInfix/LeftAbsolute/-RightAbsolute", "Take text between position {LeftPosition} and {RightPosition} characters from the end in column {ColumnName}.")
				.Set("SliceInfix/-LeftAbsolute/RightAbsolute", "Take text between {LeftPosition} characters from the end and {RightPosition} in column {ColumnName}.")
				.Set("SliceInfix/-LeftAbsolute/-RightAbsolute", "Take text between {LeftPosition} characters from the end and {RightPosition} characters from the end in column {ColumnName}.");
			templates.Set("Concat", "Build a string from:" + Environment.NewLine + "{List}");
			(from p in templates
				where !p.Value.EndsWith(".") && p.Key != "Concat"
				select p.Key).ToList<string>().ForEach(delegate(string key)
			{
				Dictionary<string, string> templates2 = templates;
				templates2[key] += ".";
			});
			return templates;
		}

		// Token: 0x040052A0 RID: 21152
		private const string _numberFormat = "#,##0.########################";

		// Token: 0x040052A1 RID: 21153
		private readonly CultureInfo _culture;

		// Token: 0x040052A2 RID: 21154
		private static readonly Dictionary<CultureInfo, Dictionary<string, string>> _templateByCulture = new Dictionary<CultureInfo, Dictionary<string, string>>();
	}
}
