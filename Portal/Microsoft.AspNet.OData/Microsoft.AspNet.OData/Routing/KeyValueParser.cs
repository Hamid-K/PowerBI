using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData;

namespace Microsoft.AspNet.OData.Routing
{
	// Token: 0x0200007A RID: 122
	internal static class KeyValueParser
	{
		// Token: 0x0600049D RID: 1181 RVA: 0x0000F2D4 File Offset: 0x0000D4D4
		public static Dictionary<string, string> ParseKeys(string segment)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			int i = 0;
			int num = 0;
			while (i < segment.Length)
			{
				if (segment[i] == '=')
				{
					string text = segment.Substring(num, i - num);
					if (string.IsNullOrWhiteSpace(text))
					{
						throw new ODataException(Error.Format(SRResources.NoKeyNameFoundInSegment, new object[] { num, segment }));
					}
					if (text.Contains("'"))
					{
						if (dictionary.Count != 0)
						{
							throw new ODataException(Error.Format(SRResources.NoKeyNameFoundInSegment, new object[] { num, segment }));
						}
						KeyValueParser.CheckSingleQuote(segment, segment);
						dictionary.Add(string.Empty, segment);
						return dictionary;
					}
					else
					{
						i++;
						num = i;
						while (i <= segment.Length)
						{
							if (i == segment.Length || segment[i] == ',')
							{
								string text2 = segment.Substring(num, i - num);
								if (string.IsNullOrWhiteSpace(text2))
								{
									throw new ODataException(Error.Format(SRResources.NoValueLiteralFoundInSegment, new object[] { text, num, segment }));
								}
								if (dictionary.ContainsKey(text))
								{
									throw new ODataException(Error.Format(SRResources.DuplicateKeyInSegment, new object[] { text, segment }));
								}
								KeyValueParser.CheckSingleQuote(text2, segment);
								dictionary.Add(text, text2);
								num = i + 1;
								break;
							}
							else
							{
								if (segment[i] == '\'')
								{
									for (i++; i <= segment.Length; i++)
									{
										if (i == segment.Length)
										{
											throw new ODataException(Error.Format(SRResources.UnterminatedStringLiteral, new object[] { num, segment }));
										}
										if (segment[i] == '\'')
										{
											if (i + 1 == segment.Length || segment[i + 1] != '\'')
											{
												break;
											}
											i++;
										}
									}
								}
								i++;
							}
						}
					}
				}
				i++;
			}
			if (dictionary.Count == 0 && !string.IsNullOrWhiteSpace(segment))
			{
				KeyValueParser.CheckSingleQuote(segment, segment);
				dictionary.Add(string.Empty, segment);
			}
			return dictionary;
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x0000F4D4 File Offset: 0x0000D6D4
		private static void CheckSingleQuote(string value, string segment)
		{
			if (value.StartsWith("'", StringComparison.Ordinal))
			{
				if (!KeyValueParser._stringLiteralRegex.IsMatch(value))
				{
					throw new ODataException(Error.Format(SRResources.LiteralHasABadFormat, new object[] { value, segment }));
				}
			}
			else
			{
				int num = value.Count((char c) => c == '\'');
				if (num != 0 && num != 2)
				{
					throw new ODataException(Error.Format(SRResources.InvalidSingleQuoteCountForNonStringLiteral, new object[] { value, segment }));
				}
				if (num != 0 && !value.EndsWith("'", StringComparison.Ordinal))
				{
					throw new ODataException(Error.Format(SRResources.LiteralHasABadFormat, new object[] { value, segment }));
				}
			}
		}

		// Token: 0x040000EC RID: 236
		private static readonly Regex _stringLiteralRegex = new Regex("^'([^']|'')*'$", RegexOptions.Compiled);
	}
}
