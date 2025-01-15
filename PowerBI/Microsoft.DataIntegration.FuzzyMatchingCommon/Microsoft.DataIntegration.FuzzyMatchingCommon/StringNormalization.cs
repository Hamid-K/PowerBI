using System;
using System.Globalization;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon
{
	// Token: 0x02000022 RID: 34
	[Serializable]
	public static class StringNormalization
	{
		// Token: 0x060000B1 RID: 177 RVA: 0x00002F78 File Offset: 0x00001178
		public static void NormalizeString(StringNormalization.FoldStringFlags foldFlags, StringNormalization.LCMapStringFlags mapFlags, ArraySegment<char> sourceString, ISegmentAllocator<char> allocator, out ArraySegment<char> outputString)
		{
			if ((foldFlags & StringNormalization.FoldStringFlags.MAP_PRECOMPOSED) != StringNormalization.FoldStringFlags.NONE && (foldFlags & StringNormalization.FoldStringFlags.MAP_COMPOSITE) != StringNormalization.FoldStringFlags.NONE)
			{
				throw new ArgumentException("MAP_PRECOMPOSED & MAP_PRECOMPOSED can not be combined.");
			}
			bool flag = (foldFlags & StringNormalization.FoldStringFlags.MAP_PRECOMPOSED) > StringNormalization.FoldStringFlags.NONE;
			bool flag2 = (mapFlags & StringNormalization.LCMapStringFlags.NORM_IGNORENONSPACE) > StringNormalization.LCMapStringFlags.NONE;
			if (flag2)
			{
				foldFlags |= StringNormalization.FoldStringFlags.MAP_COMPOSITE;
			}
			if (foldFlags != StringNormalization.FoldStringFlags.NONE)
			{
				if (flag && flag2)
				{
					foldFlags &= ~StringNormalization.FoldStringFlags.MAP_PRECOMPOSED;
				}
				StringNormalization.FoldString(foldFlags, sourceString, allocator, out sourceString);
			}
			if (mapFlags != StringNormalization.LCMapStringFlags.NONE)
			{
				StringNormalization.LCMapString(mapFlags, sourceString, allocator, out sourceString);
			}
			if (flag && flag2)
			{
				StringNormalization.FoldString(StringNormalization.FoldStringFlags.MAP_PRECOMPOSED, sourceString, allocator, out sourceString);
			}
			outputString = sourceString;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00002FF4 File Offset: 0x000011F4
		public static void LCMapString(StringNormalization.LCMapStringFlags flags, ArraySegment<char> sourceString, ISegmentAllocator<char> allocator, out ArraySegment<char> outputString)
		{
			if ((flags & ~(StringNormalization.LCMapStringFlags.NORM_IGNORENONSPACE | StringNormalization.LCMapStringFlags.NORM_IGNORESYMBOLS | StringNormalization.LCMapStringFlags.LCMAP_LOWERCASE | StringNormalization.LCMapStringFlags.LCMAP_UPPERCASE | StringNormalization.LCMapStringFlags.LCMAP_HIRAGANA | StringNormalization.LCMapStringFlags.LCMAP_KATAKANA | StringNormalization.LCMapStringFlags.LCMAP_HALFWIDTH | StringNormalization.LCMapStringFlags.LCMAP_FULLWIDTH)) != StringNormalization.LCMapStringFlags.NONE)
			{
				throw new ArgumentException("Unsupported flag passed to LCMapString.");
			}
			if (((flags & StringNormalization.LCMapStringFlags.LCMAP_LOWERCASE) != StringNormalization.LCMapStringFlags.NONE && (flags & StringNormalization.LCMapStringFlags.LCMAP_UPPERCASE) != StringNormalization.LCMapStringFlags.NONE) || ((flags & StringNormalization.LCMapStringFlags.LCMAP_HIRAGANA) != StringNormalization.LCMapStringFlags.NONE && (flags & StringNormalization.LCMapStringFlags.LCMAP_KATAKANA) != StringNormalization.LCMapStringFlags.NONE) || ((flags & StringNormalization.LCMapStringFlags.LCMAP_HALFWIDTH) != StringNormalization.LCMapStringFlags.NONE && (flags & StringNormalization.LCMapStringFlags.LCMAP_FULLWIDTH) != StringNormalization.LCMapStringFlags.NONE))
			{
				throw new ArgumentException("Invalid combination of map flags.");
			}
			int num = 0;
			int num2 = sourceString.Count;
			if ((flags & StringNormalization.LCMapStringFlags.LCMAP_HALFWIDTH) != StringNormalization.LCMapStringFlags.NONE)
			{
				num2 *= 2;
			}
			outputString = allocator.New(num2);
			for (int i = 0; i < sourceString.Count; i++)
			{
				char c = sourceString.Array[sourceString.Offset + i];
				if (((flags & StringNormalization.LCMapStringFlags.NORM_IGNORESYMBOLS) == StringNormalization.LCMapStringFlags.NONE || !char.IsSymbol(c)) && ((flags & StringNormalization.LCMapStringFlags.NORM_IGNORENONSPACE) == StringNormalization.LCMapStringFlags.NONE || char.GetUnicodeCategory(c) != 5))
				{
					if ((flags & StringNormalization.LCMapStringFlags.LCMAP_LOWERCASE) != StringNormalization.LCMapStringFlags.NONE)
					{
						c = StringNormalization.m_toLowerMap[c];
					}
					else if ((flags & StringNormalization.LCMapStringFlags.LCMAP_UPPERCASE) != StringNormalization.LCMapStringFlags.NONE)
					{
						c = StringNormalization.m_toUpperMap[c];
					}
					if ((flags & StringNormalization.LCMapStringFlags.LCMAP_HIRAGANA) != StringNormalization.LCMapStringFlags.NONE)
					{
						c = StringNormalization.ToHiragana(c);
					}
					else if ((flags & StringNormalization.LCMapStringFlags.LCMAP_KATAKANA) != StringNormalization.LCMapStringFlags.NONE)
					{
						c = StringNormalization.ToKatakana(c);
					}
					if ((flags & StringNormalization.LCMapStringFlags.LCMAP_HALFWIDTH) != StringNormalization.LCMapStringFlags.NONE)
					{
						num += StringNormalization.ToHalfWidth(c, outputString.Array, outputString.Offset + num);
					}
					else
					{
						if ((flags & StringNormalization.LCMapStringFlags.LCMAP_FULLWIDTH) != StringNormalization.LCMapStringFlags.NONE)
						{
							c = StringNormalization.ToFullWidth(c);
						}
						outputString.Array[outputString.Offset + num++] = c;
					}
				}
			}
			if (num < outputString.Count)
			{
				outputString = new ArraySegment<char>(outputString.Array, outputString.Offset, num);
				return;
			}
			if (num > outputString.Count)
			{
				throw new Exception("Output length exceeded segment length.");
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x0000318F File Offset: 0x0000138F
		private static void ResizeOutputBuffer(ref char[] outputBuffer, ref char[] tempBuffer1, ref char[] tempBuffer2, int length)
		{
			if (outputBuffer.Length < length)
			{
				if (outputBuffer == tempBuffer1)
				{
					Array.Resize<char>(ref tempBuffer1, length);
					outputBuffer = tempBuffer1;
					return;
				}
				if (outputBuffer == tempBuffer2)
				{
					Array.Resize<char>(ref tempBuffer2, length);
					outputBuffer = tempBuffer2;
				}
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000031BC File Offset: 0x000013BC
		public static void FoldString(StringNormalization.FoldStringFlags flags, ArraySegment<char> sourceString, ISegmentAllocator<char> allocator, out ArraySegment<char> outputString)
		{
			if (flags != StringNormalization.FoldStringFlags.NONE && sourceString.Count > 0)
			{
				if ((flags & ~(StringNormalization.FoldStringFlags.MAP_FOLDCZONE | StringNormalization.FoldStringFlags.MAP_PRECOMPOSED | StringNormalization.FoldStringFlags.MAP_COMPOSITE | StringNormalization.FoldStringFlags.MAP_FOLDDIGITS | StringNormalization.FoldStringFlags.MAP_EXPAND_LIGATURES)) != StringNormalization.FoldStringFlags.NONE)
				{
					throw new ArgumentException("Unsupported flag passed to FoldString.");
				}
				if ((flags & StringNormalization.FoldStringFlags.MAP_PRECOMPOSED) != StringNormalization.FoldStringFlags.NONE && (flags & StringNormalization.FoldStringFlags.MAP_COMPOSITE) != StringNormalization.FoldStringFlags.NONE)
				{
					throw new ArgumentException("MAP_PRECOMPOSED & MAP_PRECOMPOSED can not be combined.");
				}
				string text = null;
				if ((flags & (StringNormalization.FoldStringFlags.MAP_FOLDCZONE | StringNormalization.FoldStringFlags.MAP_PRECOMPOSED | StringNormalization.FoldStringFlags.MAP_COMPOSITE)) != StringNormalization.FoldStringFlags.NONE)
				{
					string text2 = new string(sourceString.Array, sourceString.Offset, sourceString.Count);
					try
					{
						if ((flags & StringNormalization.FoldStringFlags.MAP_PRECOMPOSED) != StringNormalization.FoldStringFlags.NONE && (flags & StringNormalization.FoldStringFlags.MAP_FOLDCZONE) != StringNormalization.FoldStringFlags.NONE)
						{
							text = text2.Normalize(5);
						}
						else if ((flags & StringNormalization.FoldStringFlags.MAP_COMPOSITE) != StringNormalization.FoldStringFlags.NONE && (flags & StringNormalization.FoldStringFlags.MAP_FOLDCZONE) != StringNormalization.FoldStringFlags.NONE)
						{
							text = text2.Normalize(6);
						}
						else if ((flags & StringNormalization.FoldStringFlags.MAP_PRECOMPOSED) != StringNormalization.FoldStringFlags.NONE)
						{
							text = text2.Normalize(1);
						}
						else if ((flags & StringNormalization.FoldStringFlags.MAP_COMPOSITE) != StringNormalization.FoldStringFlags.NONE)
						{
							text = text2.Normalize(2);
						}
					}
					catch (ArgumentException)
					{
						text = text2;
					}
					outputString = allocator.New(text.Length);
					text.CopyToEx(0, outputString.Array, outputString.Offset, text.Length);
					sourceString = outputString;
				}
				if ((flags & StringNormalization.FoldStringFlags.MAP_EXPAND_LIGATURES) != StringNormalization.FoldStringFlags.NONE)
				{
					int num = 0;
					for (int i = 0; i < sourceString.Count; i++)
					{
						num += StringNormalization.ExpandLigatures(sourceString.Array[sourceString.Offset + i], null, 0);
					}
					if (num > sourceString.Count)
					{
						outputString = allocator.New(num);
						int num2 = 0;
						for (int j = 0; j < sourceString.Count; j++)
						{
							num2 += StringNormalization.ExpandLigatures(sourceString.Array[sourceString.Offset + j], outputString.Array, outputString.Offset + num2);
						}
						sourceString = outputString;
					}
				}
				if ((flags & StringNormalization.FoldStringFlags.MAP_FOLDDIGITS) != StringNormalization.FoldStringFlags.NONE)
				{
					outputString = allocator.New(sourceString.Count);
					for (int k = 0; k < sourceString.Count; k++)
					{
						outputString.Array[outputString.Offset + k] = StringNormalization.FoldDigit(sourceString.Array[sourceString.Offset + k]);
					}
					sourceString = outputString;
				}
			}
			outputString = sourceString;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000033C8 File Offset: 0x000015C8
		internal static int ExpandLigatures(char c, char[] outputBuffer, int outputStartIndex)
		{
			if (outputBuffer == null)
			{
				if (c <= 'ᐂ')
				{
					if (c <= 'և')
					{
						if (c <= 'ǌ')
						{
							if (c <= 'þ')
							{
								if (c <= 'ß')
								{
									if (c == 'Æ')
									{
										return 2;
									}
									switch (c)
									{
									case 'Ü':
										return 2;
									case 'Þ':
										return 2;
									case 'ß':
										return 2;
									}
								}
								else
								{
									if (c == 'æ')
									{
										return 2;
									}
									if (c == 'ü')
									{
										return 2;
									}
									if (c == 'þ')
									{
										return 2;
									}
								}
							}
							else if (c <= 'ĳ')
							{
								if (c == 'Ĳ')
								{
									return 2;
								}
								if (c == 'ĳ')
								{
									return 2;
								}
							}
							else
							{
								if (c == 'Œ')
								{
									return 2;
								}
								if (c == 'œ')
								{
									return 2;
								}
								switch (c)
								{
								case 'Ǆ':
									return 2;
								case 'ǅ':
									return 2;
								case 'ǆ':
									return 2;
								case 'Ǉ':
									return 2;
								case 'ǈ':
									return 2;
								case 'ǉ':
									return 2;
								case 'Ǌ':
									return 2;
								case 'ǋ':
									return 2;
								case 'ǌ':
									return 2;
								}
							}
						}
						else if (c <= 'ǽ')
						{
							if (c <= 'ǣ')
							{
								if (c == 'Ǣ')
								{
									return 2;
								}
								if (c == 'ǣ')
								{
									return 2;
								}
							}
							else
							{
								switch (c)
								{
								case 'Ǳ':
									return 2;
								case 'ǲ':
									return 2;
								case 'ǳ':
									return 2;
								default:
									if (c == 'Ǽ')
									{
										return 2;
									}
									if (c == 'ǽ')
									{
										return 2;
									}
									break;
								}
							}
						}
						else if (c <= 'ȹ')
						{
							if (c == 'ȸ')
							{
								return 2;
							}
							if (c == 'ȹ')
							{
								return 2;
							}
						}
						else
						{
							switch (c)
							{
							case 'ʩ':
								return 2;
							case 'ʪ':
								return 2;
							case 'ʫ':
								return 2;
							default:
								if (c == '\u0385')
								{
									return 2;
								}
								if (c == 'և')
								{
									return 2;
								}
								break;
							}
						}
					}
					else if (c <= 'ཀྵ')
					{
						if (c <= 'གྷ')
						{
							if (c <= 'ٸ')
							{
								switch (c)
								{
								case 'װ':
									return 2;
								case 'ױ':
									return 2;
								case 'ײ':
									return 2;
								default:
									switch (c)
									{
									case 'ٵ':
										return 2;
									case 'ٶ':
										return 2;
									case 'ٸ':
										return 2;
									}
									break;
								}
							}
							else
							{
								if (c == '\u06d6')
								{
									return 3;
								}
								if (c == '\u06d7')
								{
									return 3;
								}
								if (c == 'གྷ')
								{
									return 2;
								}
							}
						}
						else if (c <= 'དྷ')
						{
							if (c == 'ཌྷ')
							{
								return 2;
							}
							if (c == 'དྷ')
							{
								return 2;
							}
						}
						else
						{
							if (c == 'བྷ')
							{
								return 2;
							}
							if (c == 'ཛྷ')
							{
								return 2;
							}
							if (c == 'ཀྵ')
							{
								return 2;
							}
						}
					}
					else if (c <= '\u0fa2')
					{
						if (c <= '\u0f81')
						{
							switch (c)
							{
							case '\u0f73':
								return 2;
							case '\u0f74':
								break;
							case '\u0f75':
								return 2;
							case '\u0f76':
								return 2;
							case '\u0f77':
								return 3;
							case '\u0f78':
								return 2;
							case '\u0f79':
								return 3;
							default:
								if (c == '\u0f81')
								{
									return 2;
								}
								break;
							}
						}
						else
						{
							if (c == '\u0f93')
							{
								return 2;
							}
							if (c == '\u0f9d')
							{
								return 2;
							}
							if (c == '\u0fa2')
							{
								return 2;
							}
						}
					}
					else if (c <= '\u0fac')
					{
						if (c == '\u0fa7')
						{
							return 2;
						}
						if (c == '\u0fac')
						{
							return 2;
						}
					}
					else
					{
						if (c == '\u0fb9')
						{
							return 2;
						}
						if (c == 'ᐁ')
						{
							return 2;
						}
						if (c == 'ᐂ')
						{
							return 2;
						}
					}
				}
				else if (c <= 'ᕅ')
				{
					if (c <= 'ᒤ')
					{
						if (c <= 'ᑫ')
						{
							if (c <= 'ᐰ')
							{
								if (c == 'ᐯ')
								{
									return 2;
								}
								if (c == 'ᐰ')
								{
									return 2;
								}
							}
							else
							{
								if (c == 'ᑌ')
								{
									return 2;
								}
								if (c == 'ᑍ')
								{
									return 2;
								}
								if (c == 'ᑫ')
								{
									return 2;
								}
							}
						}
						else if (c <= 'ᒉ')
						{
							if (c == 'ᑬ')
							{
								return 2;
							}
							if (c == 'ᒉ')
							{
								return 2;
							}
						}
						else
						{
							if (c == 'ᒊ')
							{
								return 2;
							}
							if (c == 'ᒣ')
							{
								return 2;
							}
							if (c == 'ᒤ')
							{
								return 2;
							}
						}
					}
					else if (c <= 'ᓭ')
					{
						if (c <= 'ᓁ')
						{
							if (c == 'ᓀ')
							{
								return 2;
							}
							if (c == 'ᓁ')
							{
								return 2;
							}
						}
						else
						{
							if (c == 'ᓓ')
							{
								return 2;
							}
							if (c == 'ᓔ')
							{
								return 2;
							}
							if (c == 'ᓭ')
							{
								return 2;
							}
						}
					}
					else if (c <= 'ᔦ')
					{
						if (c == 'ᓮ')
						{
							return 2;
						}
						if (c == 'ᔦ')
						{
							return 2;
						}
					}
					else
					{
						if (c == 'ᔧ')
						{
							return 2;
						}
						if (c == 'ᕂ')
						{
							return 2;
						}
						if (c == 'ᕅ')
						{
							return 2;
						}
					}
				}
				else if (c <= 'ᴁ')
				{
					if (c <= 'ᙯ')
					{
						if (c <= 'ᕔ')
						{
							if (c == 'ᕓ')
							{
								return 2;
							}
							if (c == 'ᕔ')
							{
								return 2;
							}
						}
						else
						{
							if (c == 'ᕾ')
							{
								return 2;
							}
							if (c == 'ᖎ')
							{
								return 2;
							}
							if (c == 'ᙯ')
							{
								return 2;
							}
						}
					}
					else if (c <= 'ᬌ')
					{
						if (c == 'ᙰ')
						{
							return 2;
						}
						switch (c)
						{
						case 'ᬆ':
							return 2;
						case 'ᬈ':
							return 2;
						case 'ᬊ':
							return 2;
						case 'ᬌ':
							return 2;
						}
					}
					else
					{
						if (c == 'ᬒ')
						{
							return 2;
						}
						switch (c)
						{
						case '\u1b3b':
							return 2;
						case '\u1b3c':
						case '\u1b3e':
						case '\u1b3f':
						case '\u1b42':
							break;
						case '\u1b3d':
							return 2;
						case '\u1b40':
							return 2;
						case '\u1b41':
							return 2;
						case '\u1b43':
							return 2;
						default:
							if (c == 'ᴁ')
							{
								return 2;
							}
							break;
						}
					}
				}
				else if (c <= 'ゟ')
				{
					if (c <= 'ᴭ')
					{
						if (c == 'ᴂ')
						{
							return 2;
						}
						if (c == 'ᴭ')
						{
							return 2;
						}
					}
					else
					{
						if (c == 'ᵆ')
						{
							return 2;
						}
						if (c == '\u1fee')
						{
							return 2;
						}
						if (c == 'ゟ')
						{
							return 2;
						}
					}
				}
				else if (c <= '㋌')
				{
					if (c == 'ヿ')
					{
						return 2;
					}
					if (c == '㋌')
					{
						return 2;
					}
				}
				else
				{
					if (c == '㋎')
					{
						return 2;
					}
					switch (c)
					{
					case '㍲':
						return 2;
					case '㍳':
						return 2;
					case '㍴':
					case '㍻':
					case '㍼':
					case '㍽':
					case '㍾':
					case '㍿':
					case '㎈':
					case '㎉':
					case '㎑':
					case '㎒':
					case '㎓':
					case '㎔':
					case '㎧':
					case '㎨':
					case '㎪':
					case '㎫':
					case '㎬':
					case '㎭':
					case '㎮':
					case '㎯':
					case '㏂':
					case '㏆':
					case '㏇':
					case '㏒':
					case '㏕':
					case '㏖':
					case '㏘':
					case '㏙':
						break;
					case '㍵':
						return 2;
					case '㍶':
						return 2;
					case '㍷':
						return 2;
					case '㍸':
						return 2;
					case '㍹':
						return 2;
					case '㍺':
						return 2;
					case '㎀':
						return 2;
					case '㎁':
						return 2;
					case '㎂':
						return 2;
					case '㎃':
						return 2;
					case '㎄':
						return 2;
					case '㎅':
						return 2;
					case '㎆':
						return 2;
					case '㎇':
						return 2;
					case '㎊':
						return 2;
					case '㎋':
						return 2;
					case '㎌':
						return 2;
					case '㎍':
						return 2;
					case '㎎':
						return 2;
					case '㎏':
						return 2;
					case '㎐':
						return 2;
					case '㎕':
						return 2;
					case '㎖':
						return 2;
					case '㎗':
						return 2;
					case '㎘':
						return 2;
					case '㎙':
						return 2;
					case '㎚':
						return 2;
					case '㎛':
						return 2;
					case '㎜':
						return 2;
					case '㎝':
						return 2;
					case '㎞':
						return 2;
					case '㎟':
						return 2;
					case '㎠':
						return 2;
					case '㎡':
						return 2;
					case '㎢':
						return 2;
					case '㎣':
						return 2;
					case '㎤':
						return 2;
					case '㎥':
						return 2;
					case '㎦':
						return 2;
					case '㎩':
						return 2;
					case '㎰':
						return 2;
					case '㎱':
						return 2;
					case '㎲':
						return 2;
					case '㎳':
						return 2;
					case '㎴':
						return 2;
					case '㎵':
						return 2;
					case '㎶':
						return 2;
					case '㎷':
						return 2;
					case '㎸':
						return 2;
					case '㎹':
						return 2;
					case '㎺':
						return 2;
					case '㎻':
						return 2;
					case '㎼':
						return 2;
					case '㎽':
						return 2;
					case '㎾':
						return 2;
					case '㎿':
						return 2;
					case '㏀':
						return 2;
					case '㏁':
						return 2;
					case '㏃':
						return 2;
					case '㏄':
						return 2;
					case '㏅':
						return 2;
					case '㏈':
						return 2;
					case '㏉':
						return 2;
					case '㏊':
						return 2;
					case '㏋':
						return 2;
					case '㏌':
						return 2;
					case '㏍':
						return 2;
					case '㏎':
						return 2;
					case '㏏':
						return 2;
					case '㏐':
						return 2;
					case '㏑':
						return 2;
					case '㏓':
						return 2;
					case '㏔':
						return 2;
					case '㏗':
						return 2;
					case '㏚':
						return 2;
					case '㏛':
						return 2;
					case '㏜':
						return 2;
					case '㏝':
						return 2;
					default:
						switch (c)
						{
						case 'ﬀ':
							return 2;
						case 'ﬁ':
							return 2;
						case 'ﬂ':
							return 2;
						case 'ﬃ':
							return 3;
						case 'ﬄ':
							return 3;
						case 'ﬅ':
							return 2;
						case 'ﬆ':
							return 2;
						case 'ﬓ':
							return 2;
						case 'ﬔ':
							return 2;
						case 'ﬕ':
							return 2;
						case 'ﬖ':
							return 2;
						case 'ﬗ':
							return 2;
						case 'יִ':
							return 2;
						case 'ײַ':
							return 2;
						case 'שׁ':
							return 2;
						case 'שׂ':
							return 2;
						case 'שּׁ':
							return 2;
						case 'שּׂ':
							return 2;
						case 'אַ':
							return 2;
						case 'אָ':
							return 2;
						case 'אּ':
							return 2;
						case 'בּ':
							return 2;
						case 'גּ':
							return 2;
						case 'דּ':
							return 2;
						case 'הּ':
							return 2;
						case 'וּ':
							return 2;
						case 'זּ':
							return 2;
						case 'טּ':
							return 2;
						case 'יּ':
							return 2;
						case 'ךּ':
							return 2;
						case 'כּ':
							return 2;
						case 'לּ':
							return 2;
						case 'מּ':
							return 2;
						case 'נּ':
							return 2;
						case 'סּ':
							return 2;
						case 'ףּ':
							return 2;
						case 'פּ':
							return 2;
						case 'צּ':
							return 2;
						case 'קּ':
							return 2;
						case 'רּ':
							return 2;
						case 'שּ':
							return 2;
						case 'תּ':
							return 2;
						case 'וֹ':
							return 2;
						case 'בֿ':
							return 2;
						case 'כֿ':
							return 2;
						case 'פֿ':
							return 2;
						case 'ﭏ':
							return 2;
						case 'ﯪ':
							return 2;
						case 'ﯫ':
							return 2;
						case 'ﯬ':
							return 2;
						case 'ﯭ':
							return 2;
						case 'ﯮ':
							return 2;
						case 'ﯯ':
							return 2;
						case 'ﯰ':
							return 2;
						case 'ﯱ':
							return 2;
						case 'ﯲ':
							return 2;
						case 'ﯳ':
							return 2;
						case 'ﯴ':
							return 2;
						case 'ﯵ':
							return 2;
						case 'ﯶ':
							return 2;
						case 'ﯷ':
							return 2;
						case 'ﯸ':
							return 2;
						case 'ﯹ':
							return 2;
						case 'ﯺ':
							return 2;
						case 'ﯻ':
							return 2;
						case 'ﰀ':
							return 2;
						case 'ﰁ':
							return 2;
						case 'ﰂ':
							return 2;
						case 'ﰃ':
							return 2;
						case 'ﰄ':
							return 2;
						case 'ﰅ':
							return 2;
						case 'ﰆ':
							return 2;
						case 'ﰇ':
							return 2;
						case 'ﰈ':
							return 2;
						case 'ﰉ':
							return 2;
						case 'ﰊ':
							return 2;
						case 'ﰋ':
							return 2;
						case 'ﰌ':
							return 2;
						case 'ﰍ':
							return 2;
						case 'ﰎ':
							return 2;
						case 'ﰏ':
							return 2;
						case 'ﰐ':
							return 2;
						case 'ﰑ':
							return 2;
						case 'ﰒ':
							return 2;
						case 'ﰓ':
							return 2;
						case 'ﰔ':
							return 2;
						case 'ﰕ':
							return 2;
						case 'ﰖ':
							return 2;
						case 'ﰗ':
							return 2;
						case 'ﰘ':
							return 2;
						case 'ﰙ':
							return 2;
						case 'ﰚ':
							return 2;
						case 'ﰛ':
							return 2;
						case 'ﰜ':
							return 2;
						case 'ﰝ':
							return 2;
						case 'ﰞ':
							return 2;
						case 'ﰟ':
							return 2;
						case 'ﰠ':
							return 2;
						case 'ﰡ':
							return 2;
						case 'ﰢ':
							return 2;
						case 'ﰣ':
							return 2;
						case 'ﰤ':
							return 2;
						case 'ﰥ':
							return 2;
						case 'ﰦ':
							return 2;
						case 'ﰧ':
							return 2;
						case 'ﰨ':
							return 2;
						case 'ﰩ':
							return 2;
						case 'ﰪ':
							return 2;
						case 'ﰫ':
							return 2;
						case 'ﰬ':
							return 2;
						case 'ﰭ':
							return 2;
						case 'ﰮ':
							return 2;
						case 'ﰯ':
							return 2;
						case 'ﰰ':
							return 2;
						case 'ﰱ':
							return 2;
						case 'ﰲ':
							return 2;
						case 'ﰳ':
							return 2;
						case 'ﰴ':
							return 2;
						case 'ﰵ':
							return 2;
						case 'ﰶ':
							return 2;
						case 'ﰷ':
							return 2;
						case 'ﰸ':
							return 2;
						case 'ﰹ':
							return 2;
						case 'ﰺ':
							return 2;
						case 'ﰻ':
							return 2;
						case 'ﰼ':
							return 2;
						case 'ﰽ':
							return 2;
						case 'ﰾ':
							return 2;
						case 'ﰿ':
							return 2;
						case 'ﱀ':
							return 2;
						case 'ﱁ':
							return 2;
						case 'ﱂ':
							return 2;
						case 'ﱃ':
							return 2;
						case 'ﱄ':
							return 2;
						case 'ﱅ':
							return 2;
						case 'ﱆ':
							return 2;
						case 'ﱇ':
							return 2;
						case 'ﱈ':
							return 3;
						case 'ﱉ':
							return 3;
						case 'ﱊ':
							return 2;
						case 'ﱋ':
							return 2;
						case 'ﱌ':
							return 2;
						case 'ﱍ':
							return 2;
						case 'ﱎ':
							return 2;
						case 'ﱏ':
							return 2;
						case 'ﱐ':
							return 2;
						case 'ﱑ':
							return 2;
						case 'ﱒ':
							return 2;
						case 'ﱓ':
							return 2;
						case 'ﱔ':
							return 2;
						case 'ﱕ':
							return 2;
						case 'ﱖ':
							return 2;
						case 'ﱗ':
							return 2;
						case 'ﱘ':
							return 2;
						case 'ﱙ':
							return 2;
						case 'ﱚ':
							return 2;
						case 'ﱛ':
							return 2;
						case 'ﱜ':
							return 2;
						case 'ﱝ':
							return 2;
						case 'ﱞ':
							return 2;
						case 'ﱟ':
							return 2;
						case 'ﱠ':
							return 2;
						case 'ﱡ':
							return 2;
						case 'ﱢ':
							return 2;
						case 'ﱣ':
							return 2;
						case 'ﱤ':
							return 2;
						case 'ﱥ':
							return 2;
						case 'ﱦ':
							return 2;
						case 'ﱧ':
							return 2;
						case 'ﱨ':
							return 2;
						case 'ﱩ':
							return 2;
						case 'ﱪ':
							return 2;
						case 'ﱫ':
							return 2;
						case 'ﱬ':
							return 2;
						case 'ﱭ':
							return 2;
						case 'ﱮ':
							return 2;
						case 'ﱯ':
							return 2;
						case 'ﱰ':
							return 2;
						case 'ﱱ':
							return 2;
						case 'ﱲ':
							return 2;
						case 'ﱳ':
							return 2;
						case 'ﱴ':
							return 2;
						case 'ﱵ':
							return 2;
						case 'ﱶ':
							return 2;
						case 'ﱷ':
							return 2;
						case 'ﱸ':
							return 2;
						case 'ﱹ':
							return 2;
						case 'ﱺ':
							return 2;
						case 'ﱻ':
							return 2;
						case 'ﱼ':
							return 2;
						case 'ﱽ':
							return 2;
						case 'ﱾ':
							return 2;
						case 'ﱿ':
							return 2;
						case 'ﲀ':
							return 2;
						case 'ﲁ':
							return 2;
						case 'ﲂ':
							return 2;
						case 'ﲃ':
							return 2;
						case 'ﲄ':
							return 2;
						case 'ﲅ':
							return 2;
						case 'ﲆ':
							return 2;
						case 'ﲇ':
							return 2;
						case 'ﲈ':
							return 2;
						case 'ﲉ':
							return 2;
						case 'ﲊ':
							return 2;
						case 'ﲋ':
							return 2;
						case 'ﲌ':
							return 2;
						case 'ﲍ':
							return 2;
						case 'ﲎ':
							return 2;
						case 'ﲏ':
							return 2;
						case 'ﲐ':
							return 2;
						case 'ﲑ':
							return 2;
						case 'ﲒ':
							return 2;
						case 'ﲓ':
							return 2;
						case 'ﲔ':
							return 2;
						case 'ﲕ':
							return 2;
						case 'ﲖ':
							return 2;
						case 'ﲗ':
							return 2;
						case 'ﲘ':
							return 2;
						case 'ﲙ':
							return 2;
						case 'ﲚ':
							return 2;
						case 'ﲛ':
							return 2;
						case 'ﲜ':
							return 2;
						case 'ﲝ':
							return 2;
						case 'ﲞ':
							return 2;
						case 'ﲟ':
							return 2;
						case 'ﲠ':
							return 2;
						case 'ﲡ':
							return 2;
						case 'ﲢ':
							return 2;
						case 'ﲣ':
							return 2;
						case 'ﲤ':
							return 2;
						case 'ﲥ':
							return 2;
						case 'ﲦ':
							return 2;
						case 'ﲧ':
							return 2;
						case 'ﲨ':
							return 2;
						case 'ﲩ':
							return 2;
						case 'ﲪ':
							return 2;
						case 'ﲫ':
							return 2;
						case 'ﲬ':
							return 2;
						case 'ﲭ':
							return 2;
						case 'ﲮ':
							return 2;
						case 'ﲯ':
							return 3;
						case 'ﲰ':
							return 3;
						case 'ﲱ':
							return 2;
						case 'ﲲ':
							return 2;
						case 'ﲳ':
							return 2;
						case 'ﲴ':
							return 2;
						case 'ﲵ':
							return 2;
						case 'ﲶ':
							return 2;
						case 'ﲷ':
							return 2;
						case 'ﲸ':
							return 2;
						case 'ﲹ':
							return 2;
						case 'ﲺ':
							return 3;
						case 'ﲻ':
							return 2;
						case 'ﲼ':
							return 3;
						case 'ﲽ':
							return 2;
						case 'ﲾ':
							return 2;
						case 'ﲿ':
							return 2;
						case 'ﳀ':
							return 2;
						case 'ﳁ':
							return 2;
						case 'ﳂ':
							return 2;
						case 'ﳃ':
							return 2;
						case 'ﳄ':
							return 2;
						case 'ﳅ':
							return 2;
						case 'ﳆ':
							return 2;
						case 'ﳇ':
							return 2;
						case 'ﳈ':
							return 2;
						case 'ﳉ':
							return 2;
						case 'ﳊ':
							return 2;
						case 'ﳋ':
							return 2;
						case 'ﳌ':
							return 2;
						case 'ﳍ':
							return 2;
						case 'ﳎ':
							return 2;
						case 'ﳏ':
							return 2;
						case 'ﳐ':
							return 2;
						case 'ﳑ':
							return 2;
						case 'ﳒ':
							return 2;
						case 'ﳓ':
							return 2;
						case 'ﳔ':
							return 2;
						case 'ﳕ':
							return 2;
						case 'ﳖ':
							return 2;
						case 'ﳗ':
							return 2;
						case 'ﳘ':
							return 2;
						case 'ﳙ':
							return 2;
						case 'ﳚ':
							return 2;
						case 'ﳛ':
							return 2;
						case 'ﳜ':
							return 2;
						case 'ﳝ':
							return 2;
						case 'ﳞ':
							return 2;
						case 'ﳟ':
							return 2;
						case 'ﳠ':
							return 2;
						case 'ﳡ':
							return 2;
						case 'ﳢ':
							return 2;
						case 'ﳣ':
							return 2;
						case 'ﳤ':
							return 2;
						case 'ﳥ':
							return 2;
						case 'ﳦ':
							return 2;
						case 'ﳧ':
							return 2;
						case 'ﳨ':
							return 2;
						case 'ﳩ':
							return 2;
						case 'ﳪ':
							return 2;
						case 'ﳫ':
							return 2;
						case 'ﳬ':
							return 2;
						case 'ﳭ':
							return 2;
						case 'ﳮ':
							return 2;
						case 'ﳯ':
							return 2;
						case 'ﳰ':
							return 2;
						case 'ﳱ':
							return 2;
						case 'ﳲ':
							return 2;
						case 'ﳳ':
							return 2;
						case 'ﳴ':
							return 2;
						case 'ﳵ':
							return 2;
						case 'ﳶ':
							return 2;
						case 'ﳷ':
							return 2;
						case 'ﳸ':
							return 2;
						case 'ﳹ':
							return 2;
						case 'ﳺ':
							return 2;
						case 'ﳻ':
							return 2;
						case 'ﳼ':
							return 2;
						case 'ﳽ':
							return 2;
						case 'ﳾ':
							return 2;
						case 'ﳿ':
							return 2;
						case 'ﴀ':
							return 2;
						case 'ﴁ':
							return 2;
						case 'ﴂ':
							return 2;
						case 'ﴃ':
							return 2;
						case 'ﴄ':
							return 2;
						case 'ﴅ':
							return 2;
						case 'ﴆ':
							return 2;
						case 'ﴇ':
							return 2;
						case 'ﴈ':
							return 2;
						case 'ﴉ':
							return 2;
						case 'ﴊ':
							return 2;
						case 'ﴋ':
							return 2;
						case 'ﴌ':
							return 2;
						case 'ﴍ':
							return 2;
						case 'ﴎ':
							return 2;
						case 'ﴏ':
							return 2;
						case 'ﴐ':
							return 2;
						case 'ﴑ':
							return 2;
						case 'ﴒ':
							return 2;
						case 'ﴓ':
							return 2;
						case 'ﴔ':
							return 2;
						case 'ﴕ':
							return 2;
						case 'ﴖ':
							return 2;
						case 'ﴗ':
							return 2;
						case 'ﴘ':
							return 2;
						case 'ﴙ':
							return 2;
						case 'ﴚ':
							return 2;
						case 'ﴛ':
							return 2;
						case 'ﴜ':
							return 2;
						case 'ﴝ':
							return 2;
						case 'ﴞ':
							return 2;
						case 'ﴟ':
							return 2;
						case 'ﴠ':
							return 2;
						case 'ﴡ':
							return 2;
						case 'ﴢ':
							return 2;
						case 'ﴣ':
							return 2;
						case 'ﴤ':
							return 2;
						case 'ﴥ':
							return 2;
						case 'ﴦ':
							return 2;
						case 'ﴧ':
							return 2;
						case 'ﴨ':
							return 2;
						case 'ﴩ':
							return 2;
						case 'ﴪ':
							return 2;
						case 'ﴫ':
							return 2;
						case 'ﴬ':
							return 2;
						case 'ﴭ':
							return 2;
						case 'ﴮ':
							return 2;
						case 'ﴯ':
							return 2;
						case 'ﴰ':
							return 2;
						case 'ﴱ':
							return 2;
						case 'ﴲ':
							return 2;
						case 'ﴳ':
							return 2;
						case 'ﴴ':
							return 2;
						case 'ﴵ':
							return 2;
						case 'ﴶ':
							return 2;
						case 'ﴷ':
							return 2;
						case 'ﴸ':
							return 2;
						case 'ﴹ':
							return 2;
						case 'ﴺ':
							return 2;
						case 'ﴻ':
							return 2;
						case 'ﴼ':
							return 2;
						case 'ﴽ':
							return 2;
						case 'ﵐ':
							return 2;
						case 'ﵑ':
							return 2;
						case 'ﵒ':
							return 2;
						case 'ﵓ':
							return 2;
						case 'ﵔ':
							return 2;
						case 'ﵕ':
							return 2;
						case 'ﵖ':
							return 2;
						case 'ﵗ':
							return 2;
						case 'ﵘ':
							return 2;
						case 'ﵙ':
							return 2;
						case 'ﵚ':
							return 3;
						case 'ﵛ':
							return 3;
						case 'ﵜ':
							return 2;
						case 'ﵝ':
							return 2;
						case 'ﵞ':
							return 2;
						case 'ﵟ':
							return 2;
						case 'ﵠ':
							return 2;
						case 'ﵡ':
							return 2;
						case 'ﵢ':
							return 2;
						case 'ﵣ':
							return 2;
						case 'ﵤ':
							return 2;
						case 'ﵥ':
							return 2;
						case 'ﵦ':
							return 2;
						case 'ﵧ':
							return 2;
						case 'ﵨ':
							return 2;
						case 'ﵩ':
							return 2;
						case 'ﵪ':
							return 2;
						case 'ﵫ':
							return 2;
						case 'ﵬ':
							return 2;
						case 'ﵭ':
							return 2;
						case 'ﵮ':
							return 2;
						case 'ﵯ':
							return 2;
						case 'ﵰ':
							return 2;
						case 'ﵱ':
							return 2;
						case 'ﵲ':
							return 2;
						case 'ﵳ':
							return 2;
						case 'ﵴ':
							return 2;
						case 'ﵵ':
							return 2;
						case 'ﵶ':
							return 2;
						case 'ﵷ':
							return 2;
						case 'ﵸ':
							return 2;
						case 'ﵹ':
							return 2;
						case 'ﵺ':
							return 2;
						case 'ﵻ':
							return 2;
						case 'ﵼ':
							return 2;
						case 'ﵽ':
							return 2;
						case 'ﵾ':
							return 2;
						case 'ﵿ':
							return 2;
						case 'ﶀ':
							return 2;
						case 'ﶁ':
							return 2;
						case 'ﶂ':
							return 2;
						case 'ﶃ':
							return 2;
						case 'ﶄ':
							return 2;
						case 'ﶅ':
							return 2;
						case 'ﶆ':
							return 2;
						case 'ﶇ':
							return 2;
						case 'ﶈ':
							return 2;
						case 'ﶉ':
							return 2;
						case 'ﶊ':
							return 2;
						case 'ﶋ':
							return 2;
						case 'ﶌ':
							return 2;
						case 'ﶍ':
							return 2;
						case 'ﶎ':
							return 2;
						case 'ﶏ':
							return 2;
						case 'ﶒ':
							return 2;
						case 'ﶓ':
							return 2;
						case 'ﶔ':
							return 2;
						case 'ﶕ':
							return 2;
						case 'ﶖ':
							return 2;
						case 'ﶗ':
							return 2;
						case 'ﶘ':
							return 2;
						case 'ﶙ':
							return 2;
						case 'ﶚ':
							return 2;
						case 'ﶛ':
							return 2;
						case 'ﶜ':
							return 2;
						case 'ﶝ':
							return 2;
						case 'ﶞ':
							return 2;
						case 'ﶟ':
							return 2;
						case 'ﶠ':
							return 2;
						case 'ﶡ':
							return 2;
						case 'ﶢ':
							return 2;
						case 'ﶣ':
							return 2;
						case 'ﶤ':
							return 2;
						case 'ﶥ':
							return 2;
						case 'ﶦ':
							return 2;
						case 'ﶧ':
							return 2;
						case 'ﶨ':
							return 2;
						case 'ﶩ':
							return 2;
						case 'ﶪ':
							return 2;
						case 'ﶫ':
							return 2;
						case 'ﶬ':
							return 2;
						case 'ﶭ':
							return 2;
						case 'ﶮ':
							return 2;
						case 'ﶯ':
							return 2;
						case 'ﶰ':
							return 2;
						case 'ﶱ':
							return 2;
						case 'ﶲ':
							return 2;
						case 'ﶳ':
							return 2;
						case 'ﶴ':
							return 2;
						case 'ﶵ':
							return 2;
						case 'ﶶ':
							return 2;
						case 'ﶷ':
							return 2;
						case 'ﶸ':
							return 2;
						case 'ﶹ':
							return 2;
						case 'ﶺ':
							return 2;
						case 'ﶻ':
							return 2;
						case 'ﶼ':
							return 2;
						case 'ﶽ':
							return 2;
						case 'ﶾ':
							return 2;
						case 'ﶿ':
							return 2;
						case 'ﷀ':
							return 2;
						case 'ﷁ':
							return 2;
						case 'ﷂ':
							return 2;
						case 'ﷃ':
							return 3;
						case 'ﷄ':
							return 3;
						case 'ﷅ':
							return 2;
						case 'ﷆ':
							return 2;
						case 'ﷇ':
							return 2;
						case 'ﷹ':
							return 2;
						case 'ﹰ':
							return 2;
						case 'ﹱ':
							return 2;
						case 'ﹲ':
							return 2;
						case 'ﹴ':
							return 2;
						case 'ﹶ':
							return 2;
						case 'ﹷ':
							return 3;
						case 'ﹸ':
							return 2;
						case 'ﹹ':
							return 3;
						case 'ﹺ':
							return 2;
						case 'ﹻ':
							return 2;
						case 'ﹼ':
							return 2;
						case 'ﹽ':
							return 2;
						case 'ﹾ':
							return 2;
						case 'ﹿ':
							return 2;
						case 'ﻵ':
							return 2;
						case 'ﻶ':
							return 2;
						case 'ﻷ':
							return 2;
						case 'ﻸ':
							return 2;
						case 'ﻹ':
							return 2;
						case 'ﻺ':
							return 2;
						case 'ﻻ':
							return 2;
						case 'ﻼ':
							return 2;
						}
						break;
					}
				}
				return 1;
			}
			int num;
			if (c <= 'ᐂ')
			{
				if (c <= 'և')
				{
					if (c <= 'ǌ')
					{
						if (c <= 'þ')
						{
							if (c <= 'ß')
							{
								if (c == 'Æ')
								{
									num = outputStartIndex + 1;
									outputBuffer[outputStartIndex] = 'A';
									int num2 = num;
									num = num2 + 1;
									outputBuffer[num2] = 'E';
									goto IL_8A47;
								}
								switch (c)
								{
								case 'Ü':
								{
									num = outputStartIndex + 1;
									outputBuffer[outputStartIndex] = 'U';
									int num2 = num;
									num = num2 + 1;
									outputBuffer[num2] = 'E';
									goto IL_8A47;
								}
								case 'Þ':
								{
									num = outputStartIndex + 1;
									outputBuffer[outputStartIndex] = 'T';
									int num2 = num;
									num = num2 + 1;
									outputBuffer[num2] = 'H';
									goto IL_8A47;
								}
								case 'ß':
								{
									num = outputStartIndex + 1;
									outputBuffer[outputStartIndex] = 's';
									int num2 = num;
									num = num2 + 1;
									outputBuffer[num2] = 's';
									goto IL_8A47;
								}
								}
							}
							else
							{
								if (c == 'æ')
								{
									num = outputStartIndex + 1;
									outputBuffer[outputStartIndex] = 'a';
									int num2 = num;
									num = num2 + 1;
									outputBuffer[num2] = 'e';
									goto IL_8A47;
								}
								if (c == 'ü')
								{
									num = outputStartIndex + 1;
									outputBuffer[outputStartIndex] = 'u';
									int num2 = num;
									num = num2 + 1;
									outputBuffer[num2] = 'e';
									goto IL_8A47;
								}
								if (c == 'þ')
								{
									num = outputStartIndex + 1;
									outputBuffer[outputStartIndex] = 't';
									int num2 = num;
									num = num2 + 1;
									outputBuffer[num2] = 'h';
									goto IL_8A47;
								}
							}
						}
						else if (c <= 'ĳ')
						{
							if (c == 'Ĳ')
							{
								num = outputStartIndex + 1;
								outputBuffer[outputStartIndex] = 'I';
								int num2 = num;
								num = num2 + 1;
								outputBuffer[num2] = 'J';
								goto IL_8A47;
							}
							if (c == 'ĳ')
							{
								num = outputStartIndex + 1;
								outputBuffer[outputStartIndex] = 'i';
								int num2 = num;
								num = num2 + 1;
								outputBuffer[num2] = 'j';
								goto IL_8A47;
							}
						}
						else
						{
							if (c == 'Œ')
							{
								num = outputStartIndex + 1;
								outputBuffer[outputStartIndex] = 'O';
								int num2 = num;
								num = num2 + 1;
								outputBuffer[num2] = 'E';
								goto IL_8A47;
							}
							if (c == 'œ')
							{
								num = outputStartIndex + 1;
								outputBuffer[outputStartIndex] = 'o';
								int num2 = num;
								num = num2 + 1;
								outputBuffer[num2] = 'e';
								goto IL_8A47;
							}
							switch (c)
							{
							case 'Ǆ':
							{
								num = outputStartIndex + 1;
								outputBuffer[outputStartIndex] = 'D';
								int num2 = num;
								num = num2 + 1;
								outputBuffer[num2] = 'Ž';
								goto IL_8A47;
							}
							case 'ǅ':
							{
								num = outputStartIndex + 1;
								outputBuffer[outputStartIndex] = 'D';
								int num2 = num;
								num = num2 + 1;
								outputBuffer[num2] = 'ž';
								goto IL_8A47;
							}
							case 'ǆ':
							{
								num = outputStartIndex + 1;
								outputBuffer[outputStartIndex] = 'd';
								int num2 = num;
								num = num2 + 1;
								outputBuffer[num2] = 'ž';
								goto IL_8A47;
							}
							case 'Ǉ':
							{
								num = outputStartIndex + 1;
								outputBuffer[outputStartIndex] = 'L';
								int num2 = num;
								num = num2 + 1;
								outputBuffer[num2] = 'J';
								goto IL_8A47;
							}
							case 'ǈ':
							{
								num = outputStartIndex + 1;
								outputBuffer[outputStartIndex] = 'L';
								int num2 = num;
								num = num2 + 1;
								outputBuffer[num2] = 'j';
								goto IL_8A47;
							}
							case 'ǉ':
							{
								num = outputStartIndex + 1;
								outputBuffer[outputStartIndex] = 'l';
								int num2 = num;
								num = num2 + 1;
								outputBuffer[num2] = 'j';
								goto IL_8A47;
							}
							case 'Ǌ':
							{
								num = outputStartIndex + 1;
								outputBuffer[outputStartIndex] = 'N';
								int num2 = num;
								num = num2 + 1;
								outputBuffer[num2] = 'J';
								goto IL_8A47;
							}
							case 'ǋ':
							{
								num = outputStartIndex + 1;
								outputBuffer[outputStartIndex] = 'N';
								int num2 = num;
								num = num2 + 1;
								outputBuffer[num2] = 'j';
								goto IL_8A47;
							}
							case 'ǌ':
							{
								num = outputStartIndex + 1;
								outputBuffer[outputStartIndex] = 'n';
								int num2 = num;
								num = num2 + 1;
								outputBuffer[num2] = 'j';
								goto IL_8A47;
							}
							}
						}
					}
					else if (c <= 'ǽ')
					{
						if (c <= 'ǣ')
						{
							if (c == 'Ǣ')
							{
								num = outputStartIndex + 1;
								outputBuffer[outputStartIndex] = 'Ā';
								int num2 = num;
								num = num2 + 1;
								outputBuffer[num2] = 'Ē';
								goto IL_8A47;
							}
							if (c == 'ǣ')
							{
								num = outputStartIndex + 1;
								outputBuffer[outputStartIndex] = 'ā';
								int num2 = num;
								num = num2 + 1;
								outputBuffer[num2] = 'ē';
								goto IL_8A47;
							}
						}
						else
						{
							switch (c)
							{
							case 'Ǳ':
							{
								num = outputStartIndex + 1;
								outputBuffer[outputStartIndex] = 'D';
								int num2 = num;
								num = num2 + 1;
								outputBuffer[num2] = 'Z';
								goto IL_8A47;
							}
							case 'ǲ':
							{
								num = outputStartIndex + 1;
								outputBuffer[outputStartIndex] = 'D';
								int num2 = num;
								num = num2 + 1;
								outputBuffer[num2] = 'z';
								goto IL_8A47;
							}
							case 'ǳ':
							{
								num = outputStartIndex + 1;
								outputBuffer[outputStartIndex] = 'd';
								int num2 = num;
								num = num2 + 1;
								outputBuffer[num2] = 'z';
								goto IL_8A47;
							}
							default:
								if (c == 'Ǽ')
								{
									num = outputStartIndex + 1;
									outputBuffer[outputStartIndex] = 'Á';
									int num2 = num;
									num = num2 + 1;
									outputBuffer[num2] = 'É';
									goto IL_8A47;
								}
								if (c == 'ǽ')
								{
									num = outputStartIndex + 1;
									outputBuffer[outputStartIndex] = 'á';
									int num2 = num;
									num = num2 + 1;
									outputBuffer[num2] = 'é';
									goto IL_8A47;
								}
								break;
							}
						}
					}
					else if (c <= 'ȹ')
					{
						if (c == 'ȸ')
						{
							num = outputStartIndex + 1;
							outputBuffer[outputStartIndex] = 'D';
							int num2 = num;
							num = num2 + 1;
							outputBuffer[num2] = 'b';
							goto IL_8A47;
						}
						if (c == 'ȹ')
						{
							num = outputStartIndex + 1;
							outputBuffer[outputStartIndex] = 'Q';
							int num2 = num;
							num = num2 + 1;
							outputBuffer[num2] = 'p';
							goto IL_8A47;
						}
					}
					else
					{
						switch (c)
						{
						case 'ʩ':
						{
							num = outputStartIndex + 1;
							outputBuffer[outputStartIndex] = 'f';
							int num2 = num;
							num = num2 + 1;
							outputBuffer[num2] = 'n';
							goto IL_8A47;
						}
						case 'ʪ':
						{
							num = outputStartIndex + 1;
							outputBuffer[outputStartIndex] = 'l';
							int num2 = num;
							num = num2 + 1;
							outputBuffer[num2] = 's';
							goto IL_8A47;
						}
						case 'ʫ':
						{
							num = outputStartIndex + 1;
							outputBuffer[outputStartIndex] = 'l';
							int num2 = num;
							num = num2 + 1;
							outputBuffer[num2] = 'z';
							goto IL_8A47;
						}
						default:
							if (c == '\u0385')
							{
								num = outputStartIndex + 1;
								outputBuffer[outputStartIndex] = '\u00a8';
								int num2 = num;
								num = num2 + 1;
								outputBuffer[num2] = '\u0301';
								goto IL_8A47;
							}
							if (c == 'և')
							{
								num = outputStartIndex + 1;
								outputBuffer[outputStartIndex] = 'ե';
								int num2 = num;
								num = num2 + 1;
								outputBuffer[num2] = 'ւ';
								goto IL_8A47;
							}
							break;
						}
					}
				}
				else if (c <= 'ཀྵ')
				{
					if (c <= 'གྷ')
					{
						if (c <= 'ٸ')
						{
							switch (c)
							{
							case 'װ':
							{
								num = outputStartIndex + 1;
								outputBuffer[outputStartIndex] = 'ו';
								int num2 = num;
								num = num2 + 1;
								outputBuffer[num2] = 'ו';
								goto IL_8A47;
							}
							case 'ױ':
							{
								num = outputStartIndex + 1;
								outputBuffer[outputStartIndex] = 'ו';
								int num2 = num;
								num = num2 + 1;
								outputBuffer[num2] = 'י';
								goto IL_8A47;
							}
							case 'ײ':
							{
								num = outputStartIndex + 1;
								outputBuffer[outputStartIndex] = 'י';
								int num2 = num;
								num = num2 + 1;
								outputBuffer[num2] = 'י';
								goto IL_8A47;
							}
							default:
								switch (c)
								{
								case 'ٵ':
								{
									num = outputStartIndex + 1;
									outputBuffer[outputStartIndex] = 'ا';
									int num2 = num;
									num = num2 + 1;
									outputBuffer[num2] = 'ٴ';
									goto IL_8A47;
								}
								case 'ٶ':
								{
									num = outputStartIndex + 1;
									outputBuffer[outputStartIndex] = 'و';
									int num2 = num;
									num = num2 + 1;
									outputBuffer[num2] = 'ٴ';
									goto IL_8A47;
								}
								case 'ٸ':
								{
									num = outputStartIndex + 1;
									outputBuffer[outputStartIndex] = 'ي';
									int num2 = num;
									num = num2 + 1;
									outputBuffer[num2] = 'ٴ';
									goto IL_8A47;
								}
								}
								break;
							}
						}
						else
						{
							if (c == '\u06d6')
							{
								num = outputStartIndex + 1;
								outputBuffer[outputStartIndex] = 'ص';
								int num2 = num;
								num = num2 + 1;
								outputBuffer[num2] = 'Á';
								num2 = num;
								num = num2 + 1;
								outputBuffer[num2] = 'É';
								goto IL_8A47;
							}
							if (c == '\u06d7')
							{
								num = outputStartIndex + 1;
								outputBuffer[outputStartIndex] = 'ق';
								int num2 = num;
								num = num2 + 1;
								outputBuffer[num2] = 'Á';
								num2 = num;
								num = num2 + 1;
								outputBuffer[num2] = 'É';
								goto IL_8A47;
							}
							if (c == 'གྷ')
							{
								num = outputStartIndex + 1;
								outputBuffer[outputStartIndex] = 'ག';
								int num2 = num;
								num = num2 + 1;
								outputBuffer[num2] = '\u0fb7';
								goto IL_8A47;
							}
						}
					}
					else if (c <= 'དྷ')
					{
						if (c == 'ཌྷ')
						{
							num = outputStartIndex + 1;
							outputBuffer[outputStartIndex] = 'ཌ';
							int num2 = num;
							num = num2 + 1;
							outputBuffer[num2] = '\u0fb7';
							goto IL_8A47;
						}
						if (c == 'དྷ')
						{
							num = outputStartIndex + 1;
							outputBuffer[outputStartIndex] = 'ད';
							int num2 = num;
							num = num2 + 1;
							outputBuffer[num2] = '\u0fb7';
							goto IL_8A47;
						}
					}
					else
					{
						if (c == 'བྷ')
						{
							num = outputStartIndex + 1;
							outputBuffer[outputStartIndex] = 'བ';
							int num2 = num;
							num = num2 + 1;
							outputBuffer[num2] = '\u0fb7';
							goto IL_8A47;
						}
						if (c == 'ཛྷ')
						{
							num = outputStartIndex + 1;
							outputBuffer[outputStartIndex] = 'ཛ';
							int num2 = num;
							num = num2 + 1;
							outputBuffer[num2] = '\u0fb7';
							goto IL_8A47;
						}
						if (c == 'ཀྵ')
						{
							num = outputStartIndex + 1;
							outputBuffer[outputStartIndex] = 'ཀ';
							int num2 = num;
							num = num2 + 1;
							outputBuffer[num2] = '\u0fb5';
							goto IL_8A47;
						}
					}
				}
				else if (c <= '\u0fa2')
				{
					if (c <= '\u0f81')
					{
						switch (c)
						{
						case '\u0f73':
						{
							num = outputStartIndex + 1;
							outputBuffer[outputStartIndex] = '\u0f71';
							int num2 = num;
							num = num2 + 1;
							outputBuffer[num2] = '\u0f72';
							goto IL_8A47;
						}
						case '\u0f74':
							break;
						case '\u0f75':
						{
							num = outputStartIndex + 1;
							outputBuffer[outputStartIndex] = '\u0f71';
							int num2 = num;
							num = num2 + 1;
							outputBuffer[num2] = '\u0f74';
							goto IL_8A47;
						}
						case '\u0f76':
						{
							num = outputStartIndex + 1;
							outputBuffer[outputStartIndex] = '\u0fb2';
							int num2 = num;
							num = num2 + 1;
							outputBuffer[num2] = '\u0f80';
							goto IL_8A47;
						}
						case '\u0f77':
						{
							num = outputStartIndex + 1;
							outputBuffer[outputStartIndex] = '\u0fb2';
							int num2 = num;
							num = num2 + 1;
							outputBuffer[num2] = '\u0f71';
							num2 = num;
							num = num2 + 1;
							outputBuffer[num2] = '\u0f80';
							goto IL_8A47;
						}
						case '\u0f78':
						{
							num = outputStartIndex + 1;
							outputBuffer[outputStartIndex] = '\u0fb3';
							int num2 = num;
							num = num2 + 1;
							outputBuffer[num2] = '\u0f80';
							goto IL_8A47;
						}
						case '\u0f79':
						{
							num = outputStartIndex + 1;
							outputBuffer[outputStartIndex] = '\u0fb3';
							int num2 = num;
							num = num2 + 1;
							outputBuffer[num2] = '\u0f71';
							num2 = num;
							num = num2 + 1;
							outputBuffer[num2] = '\u0f80';
							goto IL_8A47;
						}
						default:
							if (c == '\u0f81')
							{
								num = outputStartIndex + 1;
								outputBuffer[outputStartIndex] = '\u0f71';
								int num2 = num;
								num = num2 + 1;
								outputBuffer[num2] = '\u0f80';
								goto IL_8A47;
							}
							break;
						}
					}
					else
					{
						if (c == '\u0f93')
						{
							num = outputStartIndex + 1;
							outputBuffer[outputStartIndex] = '\u0f92';
							int num2 = num;
							num = num2 + 1;
							outputBuffer[num2] = '\u0fb7';
							goto IL_8A47;
						}
						if (c == '\u0f9d')
						{
							num = outputStartIndex + 1;
							outputBuffer[outputStartIndex] = '\u0f9c';
							int num2 = num;
							num = num2 + 1;
							outputBuffer[num2] = '\u0fb7';
							goto IL_8A47;
						}
						if (c == '\u0fa2')
						{
							num = outputStartIndex + 1;
							outputBuffer[outputStartIndex] = '\u0fa1';
							int num2 = num;
							num = num2 + 1;
							outputBuffer[num2] = '\u0fb7';
							goto IL_8A47;
						}
					}
				}
				else if (c <= '\u0fac')
				{
					if (c == '\u0fa7')
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '\u0fa6';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u0fb7';
						goto IL_8A47;
					}
					if (c == '\u0fac')
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '\u0fab';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u0fb7';
						goto IL_8A47;
					}
				}
				else
				{
					if (c == '\u0fb9')
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '\u0f90';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u0fb5';
						goto IL_8A47;
					}
					if (c == 'ᐁ')
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'י';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05b4';
						goto IL_8A47;
					}
					if (c == 'ᐂ')
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ײ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05b7';
						goto IL_8A47;
					}
				}
			}
			else if (c <= 'ᕅ')
			{
				if (c <= 'ᒤ')
				{
					if (c <= 'ᑫ')
					{
						if (c <= 'ᐰ')
						{
							if (c == 'ᐯ')
							{
								num = outputStartIndex + 1;
								outputBuffer[outputStartIndex] = 'ש';
								int num2 = num;
								num = num2 + 1;
								outputBuffer[num2] = '\u05c1';
								goto IL_8A47;
							}
							if (c == 'ᐰ')
							{
								num = outputStartIndex + 1;
								outputBuffer[outputStartIndex] = 'ש';
								int num2 = num;
								num = num2 + 1;
								outputBuffer[num2] = '\u05c2';
								goto IL_8A47;
							}
						}
						else
						{
							if (c == 'ᑌ')
							{
								num = outputStartIndex + 1;
								outputBuffer[outputStartIndex] = 'שּ';
								int num2 = num;
								num = num2 + 1;
								outputBuffer[num2] = '\u05c1';
								goto IL_8A47;
							}
							if (c == 'ᑍ')
							{
								num = outputStartIndex + 1;
								outputBuffer[outputStartIndex] = 'שּ';
								int num2 = num;
								num = num2 + 1;
								outputBuffer[num2] = '\u05c2';
								goto IL_8A47;
							}
							if (c == 'ᑫ')
							{
								num = outputStartIndex + 1;
								outputBuffer[outputStartIndex] = 'א';
								int num2 = num;
								num = num2 + 1;
								outputBuffer[num2] = '\u05b7';
								goto IL_8A47;
							}
						}
					}
					else if (c <= 'ᒉ')
					{
						if (c == 'ᑬ')
						{
							num = outputStartIndex + 1;
							outputBuffer[outputStartIndex] = 'א';
							int num2 = num;
							num = num2 + 1;
							outputBuffer[num2] = '\u05b8';
							goto IL_8A47;
						}
						if (c == 'ᒉ')
						{
							num = outputStartIndex + 1;
							outputBuffer[outputStartIndex] = 'א';
							int num2 = num;
							num = num2 + 1;
							outputBuffer[num2] = '\u05bc';
							goto IL_8A47;
						}
					}
					else
					{
						if (c == 'ᒊ')
						{
							num = outputStartIndex + 1;
							outputBuffer[outputStartIndex] = 'ב';
							int num2 = num;
							num = num2 + 1;
							outputBuffer[num2] = '\u05bc';
							goto IL_8A47;
						}
						if (c == 'ᒣ')
						{
							num = outputStartIndex + 1;
							outputBuffer[outputStartIndex] = 'ג';
							int num2 = num;
							num = num2 + 1;
							outputBuffer[num2] = '\u05bc';
							goto IL_8A47;
						}
						if (c == 'ᒤ')
						{
							num = outputStartIndex + 1;
							outputBuffer[outputStartIndex] = 'ד';
							int num2 = num;
							num = num2 + 1;
							outputBuffer[num2] = '\u05bc';
							goto IL_8A47;
						}
					}
				}
				else if (c <= 'ᓭ')
				{
					if (c <= 'ᓁ')
					{
						if (c == 'ᓀ')
						{
							num = outputStartIndex + 1;
							outputBuffer[outputStartIndex] = 'ה';
							int num2 = num;
							num = num2 + 1;
							outputBuffer[num2] = '\u05bc';
							goto IL_8A47;
						}
						if (c == 'ᓁ')
						{
							num = outputStartIndex + 1;
							outputBuffer[outputStartIndex] = 'ו';
							int num2 = num;
							num = num2 + 1;
							outputBuffer[num2] = '\u05bc';
							goto IL_8A47;
						}
					}
					else
					{
						if (c == 'ᓓ')
						{
							num = outputStartIndex + 1;
							outputBuffer[outputStartIndex] = 'י';
							int num2 = num;
							num = num2 + 1;
							outputBuffer[num2] = '\u05bc';
							goto IL_8A47;
						}
						if (c == 'ᓔ')
						{
							num = outputStartIndex + 1;
							outputBuffer[outputStartIndex] = 'ך';
							int num2 = num;
							num = num2 + 1;
							outputBuffer[num2] = '\u05bc';
							goto IL_8A47;
						}
						if (c == 'ᓭ')
						{
							num = outputStartIndex + 1;
							outputBuffer[outputStartIndex] = 'ז';
							int num2 = num;
							num = num2 + 1;
							outputBuffer[num2] = '\u05bc';
							goto IL_8A47;
						}
					}
				}
				else if (c <= 'ᔦ')
				{
					if (c == 'ᓮ')
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ט';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05bc';
						goto IL_8A47;
					}
					if (c == 'ᔦ')
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'כ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05bc';
						goto IL_8A47;
					}
				}
				else
				{
					if (c == 'ᔧ')
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ל';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05bc';
						goto IL_8A47;
					}
					if (c == 'ᕂ')
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ס';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05bc';
						goto IL_8A47;
					}
					if (c == 'ᕅ')
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ף';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05bc';
						goto IL_8A47;
					}
				}
			}
			else if (c <= 'ᴁ')
			{
				if (c <= 'ᙯ')
				{
					if (c <= 'ᕔ')
					{
						if (c == 'ᕓ')
						{
							num = outputStartIndex + 1;
							outputBuffer[outputStartIndex] = 'מ';
							int num2 = num;
							num = num2 + 1;
							outputBuffer[num2] = '\u05bc';
							goto IL_8A47;
						}
						if (c == 'ᕔ')
						{
							num = outputStartIndex + 1;
							outputBuffer[outputStartIndex] = 'נ';
							int num2 = num;
							num = num2 + 1;
							outputBuffer[num2] = '\u05bc';
							goto IL_8A47;
						}
					}
					else
					{
						if (c == 'ᕾ')
						{
							num = outputStartIndex + 1;
							outputBuffer[outputStartIndex] = 'צ';
							int num2 = num;
							num = num2 + 1;
							outputBuffer[num2] = '\u05bc';
							goto IL_8A47;
						}
						if (c == 'ᖎ')
						{
							num = outputStartIndex + 1;
							outputBuffer[outputStartIndex] = 'ר';
							int num2 = num;
							num = num2 + 1;
							outputBuffer[num2] = '\u05bc';
							goto IL_8A47;
						}
						if (c == 'ᙯ')
						{
							num = outputStartIndex + 1;
							outputBuffer[outputStartIndex] = 'פ';
							int num2 = num;
							num = num2 + 1;
							outputBuffer[num2] = '\u05bc';
							goto IL_8A47;
						}
					}
				}
				else if (c <= 'ᬌ')
				{
					if (c == 'ᙰ')
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ק';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05bc';
						goto IL_8A47;
					}
					switch (c)
					{
					case 'ᬆ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ש';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05bc';
						goto IL_8A47;
					}
					case 'ᬈ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ת';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05bc';
						goto IL_8A47;
					}
					case 'ᬊ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ו';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05b9';
						goto IL_8A47;
					}
					case 'ᬌ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ב';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05bf';
						goto IL_8A47;
					}
					}
				}
				else
				{
					if (c == 'ᬒ')
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'כ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05bf';
						goto IL_8A47;
					}
					switch (c)
					{
					case '\u1b3b':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'פ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05bf';
						goto IL_8A47;
					}
					case '\u1b3c':
					case '\u1b3e':
					case '\u1b3f':
					case '\u1b42':
						break;
					case '\u1b3d':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'א';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ל';
						goto IL_8A47;
					}
					case '\u1b40':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ئ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ا';
						goto IL_8A47;
					}
					case '\u1b41':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ئ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ا';
						goto IL_8A47;
					}
					case '\u1b43':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ئ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ە';
						goto IL_8A47;
					}
					default:
						if (c == 'ᴁ')
						{
							num = outputStartIndex + 1;
							outputBuffer[outputStartIndex] = 'A';
							int num2 = num;
							num = num2 + 1;
							outputBuffer[num2] = 'E';
							goto IL_8A47;
						}
						break;
					}
				}
			}
			else if (c <= 'ゟ')
			{
				if (c <= 'ᴭ')
				{
					if (c == 'ᴂ')
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'a';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'e';
						goto IL_8A47;
					}
					if (c == 'ᴭ')
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'A';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'E';
						goto IL_8A47;
					}
				}
				else
				{
					if (c == 'ᵆ')
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'a';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'e';
						goto IL_8A47;
					}
					if (c == '\u1fee')
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '\u00a8';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u0301';
						goto IL_8A47;
					}
					if (c == 'ゟ')
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'よ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'り';
						goto IL_8A47;
					}
				}
			}
			else if (c <= '㋎')
			{
				if (c == 'ヿ')
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'コ';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = 'ト';
					goto IL_8A47;
				}
				if (c == '㋌')
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'H';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = 'g';
					goto IL_8A47;
				}
				if (c == '㋎')
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'e';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = 'V';
					goto IL_8A47;
				}
			}
			else
			{
				switch (c)
				{
				case '㍲':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'd';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = 'a';
					goto IL_8A47;
				}
				case '㍳':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'A';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = 'U';
					goto IL_8A47;
				}
				case '㍴':
				case '㍻':
				case '㍼':
				case '㍽':
				case '㍾':
				case '㍿':
				case '㎈':
				case '㎉':
				case '㎑':
				case '㎒':
				case '㎓':
				case '㎔':
				case '㎧':
				case '㎨':
				case '㎪':
				case '㎫':
				case '㎬':
				case '㎭':
				case '㎮':
				case '㎯':
				case '㏂':
				case '㏆':
				case '㏇':
				case '㏒':
				case '㏕':
				case '㏖':
				case '㏘':
				case '㏙':
					break;
				case '㍵':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'o';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = 'V';
					goto IL_8A47;
				}
				case '㍶':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'p';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = 'c';
					goto IL_8A47;
				}
				case '㍷':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'd';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = 'm';
					goto IL_8A47;
				}
				case '㍸':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = '㍷';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = '²';
					goto IL_8A47;
				}
				case '㍹':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = '㍷';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = '³';
					goto IL_8A47;
				}
				case '㍺':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'I';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = 'U';
					goto IL_8A47;
				}
				case '㎀':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'p';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = 'A';
					goto IL_8A47;
				}
				case '㎁':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'n';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = 'A';
					goto IL_8A47;
				}
				case '㎂':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'μ';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = 'A';
					goto IL_8A47;
				}
				case '㎃':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'm';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = 'A';
					goto IL_8A47;
				}
				case '㎄':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'k';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = 'A';
					goto IL_8A47;
				}
				case '㎅':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'K';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = 'B';
					goto IL_8A47;
				}
				case '㎆':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'M';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = 'B';
					goto IL_8A47;
				}
				case '㎇':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'G';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = 'B';
					goto IL_8A47;
				}
				case '㎊':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'p';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = 'F';
					goto IL_8A47;
				}
				case '㎋':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'n';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = 'F';
					goto IL_8A47;
				}
				case '㎌':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'μ';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = 'F';
					goto IL_8A47;
				}
				case '㎍':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'μ';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = 'g';
					goto IL_8A47;
				}
				case '㎎':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'm';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = 'g';
					goto IL_8A47;
				}
				case '㎏':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'k';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = 'g';
					goto IL_8A47;
				}
				case '㎐':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'H';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = 'z';
					goto IL_8A47;
				}
				case '㎕':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'μ';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = 'ℓ';
					goto IL_8A47;
				}
				case '㎖':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'm';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = 'ℓ';
					goto IL_8A47;
				}
				case '㎗':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'd';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = 'ℓ';
					goto IL_8A47;
				}
				case '㎘':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'k';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = 'ℓ';
					goto IL_8A47;
				}
				case '㎙':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'f';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = 'm';
					goto IL_8A47;
				}
				case '㎚':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'n';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = 'm';
					goto IL_8A47;
				}
				case '㎛':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'μ';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = 'm';
					goto IL_8A47;
				}
				case '㎜':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'm';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = 'm';
					goto IL_8A47;
				}
				case '㎝':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'c';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = 'm';
					goto IL_8A47;
				}
				case '㎞':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'k';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = 'm';
					goto IL_8A47;
				}
				case '㎟':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = '㎜';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = '²';
					goto IL_8A47;
				}
				case '㎠':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = '㎝';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = '²';
					goto IL_8A47;
				}
				case '㎡':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'm';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = '²';
					goto IL_8A47;
				}
				case '㎢':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = '㎞';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = '²';
					goto IL_8A47;
				}
				case '㎣':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = '㎜';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = '³';
					goto IL_8A47;
				}
				case '㎤':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = '㎝';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = '³';
					goto IL_8A47;
				}
				case '㎥':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'm';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = '³';
					goto IL_8A47;
				}
				case '㎦':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = '㎞';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = '³';
					goto IL_8A47;
				}
				case '㎩':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'P';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = 'a';
					goto IL_8A47;
				}
				case '㎰':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'p';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = 's';
					goto IL_8A47;
				}
				case '㎱':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'n';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = 's';
					goto IL_8A47;
				}
				case '㎲':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'μ';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = 's';
					goto IL_8A47;
				}
				case '㎳':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'm';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = 's';
					goto IL_8A47;
				}
				case '㎴':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'p';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = 'V';
					goto IL_8A47;
				}
				case '㎵':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'n';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = 'V';
					goto IL_8A47;
				}
				case '㎶':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'μ';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = 'V';
					goto IL_8A47;
				}
				case '㎷':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'm';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = 'V';
					goto IL_8A47;
				}
				case '㎸':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'k';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = 'V';
					goto IL_8A47;
				}
				case '㎹':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'M';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = 'V';
					goto IL_8A47;
				}
				case '㎺':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'p';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = 'W';
					goto IL_8A47;
				}
				case '㎻':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'n';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = 'W';
					goto IL_8A47;
				}
				case '㎼':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'μ';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = 'W';
					goto IL_8A47;
				}
				case '㎽':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'm';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = 'W';
					goto IL_8A47;
				}
				case '㎾':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'k';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = 'W';
					goto IL_8A47;
				}
				case '㎿':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'M';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = 'W';
					goto IL_8A47;
				}
				case '㏀':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'k';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = 'Ω';
					goto IL_8A47;
				}
				case '㏁':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'M';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = 'Ω';
					goto IL_8A47;
				}
				case '㏃':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'B';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = 'q';
					goto IL_8A47;
				}
				case '㏄':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'c';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = 'c';
					goto IL_8A47;
				}
				case '㏅':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'c';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = 'd';
					goto IL_8A47;
				}
				case '㏈':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'd';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = 'B';
					goto IL_8A47;
				}
				case '㏉':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'G';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = 'y';
					goto IL_8A47;
				}
				case '㏊':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'h';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = 'a';
					goto IL_8A47;
				}
				case '㏋':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'H';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = 'P';
					goto IL_8A47;
				}
				case '㏌':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'i';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = 'n';
					goto IL_8A47;
				}
				case '㏍':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'K';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = 'K';
					goto IL_8A47;
				}
				case '㏎':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'K';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = 'M';
					goto IL_8A47;
				}
				case '㏏':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'k';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = 't';
					goto IL_8A47;
				}
				case '㏐':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'l';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = 'm';
					goto IL_8A47;
				}
				case '㏑':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'l';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = 'n';
					goto IL_8A47;
				}
				case '㏓':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'l';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = 'x';
					goto IL_8A47;
				}
				case '㏔':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'm';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = 'b';
					goto IL_8A47;
				}
				case '㏗':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'P';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = 'H';
					goto IL_8A47;
				}
				case '㏚':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'P';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = 'R';
					goto IL_8A47;
				}
				case '㏛':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 's';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = 'r';
					goto IL_8A47;
				}
				case '㏜':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'S';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = 'v';
					goto IL_8A47;
				}
				case '㏝':
				{
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'W';
					int num2 = num;
					num = num2 + 1;
					outputBuffer[num2] = 'b';
					goto IL_8A47;
				}
				default:
					switch (c)
					{
					case 'ﬀ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'f';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'f';
						goto IL_8A47;
					}
					case 'ﬁ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'f';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'i';
						goto IL_8A47;
					}
					case 'ﬂ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'f';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'l';
						goto IL_8A47;
					}
					case 'ﬃ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'f';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'f';
						num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'i';
						goto IL_8A47;
					}
					case 'ﬄ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'f';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'f';
						num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'l';
						goto IL_8A47;
					}
					case 'ﬅ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ſ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 't';
						goto IL_8A47;
					}
					case 'ﬆ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 's';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 't';
						goto IL_8A47;
					}
					case '\ufb07':
					case '\ufb08':
					case '\ufb09':
					case '\ufb0a':
					case '\ufb0b':
					case '\ufb0c':
					case '\ufb0d':
					case '\ufb0e':
					case '\ufb0f':
					case '\ufb10':
					case '\ufb11':
					case '\ufb12':
					case '\ufb18':
					case '\ufb19':
					case '\ufb1a':
					case '\ufb1b':
					case '\ufb1c':
					case '\ufb1e':
					case 'ﬠ':
					case 'ﬡ':
					case 'ﬢ':
					case 'ﬣ':
					case 'ﬤ':
					case 'ﬥ':
					case 'ﬦ':
					case 'ﬧ':
					case 'ﬨ':
					case '﬩':
					case '\ufb37':
					case '\ufb3d':
					case '\ufb3f':
					case '\ufb42':
					case '\ufb45':
					case 'ﭐ':
					case 'ﭑ':
					case 'ﭒ':
					case 'ﭓ':
					case 'ﭔ':
					case 'ﭕ':
					case 'ﭖ':
					case 'ﭗ':
					case 'ﭘ':
					case 'ﭙ':
					case 'ﭚ':
					case 'ﭛ':
					case 'ﭜ':
					case 'ﭝ':
					case 'ﭞ':
					case 'ﭟ':
					case 'ﭠ':
					case 'ﭡ':
					case 'ﭢ':
					case 'ﭣ':
					case 'ﭤ':
					case 'ﭥ':
					case 'ﭦ':
					case 'ﭧ':
					case 'ﭨ':
					case 'ﭩ':
					case 'ﭪ':
					case 'ﭫ':
					case 'ﭬ':
					case 'ﭭ':
					case 'ﭮ':
					case 'ﭯ':
					case 'ﭰ':
					case 'ﭱ':
					case 'ﭲ':
					case 'ﭳ':
					case 'ﭴ':
					case 'ﭵ':
					case 'ﭶ':
					case 'ﭷ':
					case 'ﭸ':
					case 'ﭹ':
					case 'ﭺ':
					case 'ﭻ':
					case 'ﭼ':
					case 'ﭽ':
					case 'ﭾ':
					case 'ﭿ':
					case 'ﮀ':
					case 'ﮁ':
					case 'ﮂ':
					case 'ﮃ':
					case 'ﮄ':
					case 'ﮅ':
					case 'ﮆ':
					case 'ﮇ':
					case 'ﮈ':
					case 'ﮉ':
					case 'ﮊ':
					case 'ﮋ':
					case 'ﮌ':
					case 'ﮍ':
					case 'ﮎ':
					case 'ﮏ':
					case 'ﮐ':
					case 'ﮑ':
					case 'ﮒ':
					case 'ﮓ':
					case 'ﮔ':
					case 'ﮕ':
					case 'ﮖ':
					case 'ﮗ':
					case 'ﮘ':
					case 'ﮙ':
					case 'ﮚ':
					case 'ﮛ':
					case 'ﮜ':
					case 'ﮝ':
					case 'ﮞ':
					case 'ﮟ':
					case 'ﮠ':
					case 'ﮡ':
					case 'ﮢ':
					case 'ﮣ':
					case 'ﮤ':
					case 'ﮥ':
					case 'ﮦ':
					case 'ﮧ':
					case 'ﮨ':
					case 'ﮩ':
					case 'ﮪ':
					case 'ﮫ':
					case 'ﮬ':
					case 'ﮭ':
					case 'ﮮ':
					case 'ﮯ':
					case 'ﮰ':
					case 'ﮱ':
					case '\ufbb2':
					case '\ufbb3':
					case '\ufbb4':
					case '\ufbb5':
					case '\ufbb6':
					case '\ufbb7':
					case '\ufbb8':
					case '\ufbb9':
					case '\ufbba':
					case '\ufbbb':
					case '\ufbbc':
					case '\ufbbd':
					case '\ufbbe':
					case '\ufbbf':
					case '\ufbc0':
					case '\ufbc1':
					case '\ufbc2':
					case '\ufbc3':
					case '\ufbc4':
					case '\ufbc5':
					case '\ufbc6':
					case '\ufbc7':
					case '\ufbc8':
					case '\ufbc9':
					case '\ufbca':
					case '\ufbcb':
					case '\ufbcc':
					case '\ufbcd':
					case '\ufbce':
					case '\ufbcf':
					case '\ufbd0':
					case '\ufbd1':
					case '\ufbd2':
					case 'ﯓ':
					case 'ﯔ':
					case 'ﯕ':
					case 'ﯖ':
					case 'ﯗ':
					case 'ﯘ':
					case 'ﯙ':
					case 'ﯚ':
					case 'ﯛ':
					case 'ﯜ':
					case 'ﯝ':
					case 'ﯞ':
					case 'ﯟ':
					case 'ﯠ':
					case 'ﯡ':
					case 'ﯢ':
					case 'ﯣ':
					case 'ﯤ':
					case 'ﯥ':
					case 'ﯦ':
					case 'ﯧ':
					case 'ﯨ':
					case 'ﯩ':
					case 'ﯼ':
					case 'ﯽ':
					case 'ﯾ':
					case 'ﯿ':
					case '﴾':
					case '﴿':
					case '﵀':
					case '﵁':
					case '﵂':
					case '﵃':
					case '﵄':
					case '﵅':
					case '﵆':
					case '﵇':
					case '﵈':
					case '﵉':
					case '﵊':
					case '﵋':
					case '﵌':
					case '﵍':
					case '﵎':
					case '﵏':
					case '\ufd90':
					case '\ufd91':
					case '\ufdc8':
					case '\ufdc9':
					case '\ufdca':
					case '\ufdcb':
					case '\ufdcc':
					case '\ufdcd':
					case '\ufdce':
					case '﷏':
					case '\ufdd0':
					case '\ufdd1':
					case '\ufdd2':
					case '\ufdd3':
					case '\ufdd4':
					case '\ufdd5':
					case '\ufdd6':
					case '\ufdd7':
					case '\ufdd8':
					case '\ufdd9':
					case '\ufdda':
					case '\ufddb':
					case '\ufddc':
					case '\ufddd':
					case '\ufdde':
					case '\ufddf':
					case '\ufde0':
					case '\ufde1':
					case '\ufde2':
					case '\ufde3':
					case '\ufde4':
					case '\ufde5':
					case '\ufde6':
					case '\ufde7':
					case '\ufde8':
					case '\ufde9':
					case '\ufdea':
					case '\ufdeb':
					case '\ufdec':
					case '\ufded':
					case '\ufdee':
					case '\ufdef':
					case 'ﷰ':
					case 'ﷱ':
					case 'ﷲ':
					case 'ﷳ':
					case 'ﷴ':
					case 'ﷵ':
					case 'ﷶ':
					case 'ﷷ':
					case 'ﷸ':
					case 'ﷺ':
					case 'ﷻ':
					case '﷼':
					case '﷽':
					case '﷾':
					case '﷿':
					case '\ufe00':
					case '\ufe01':
					case '\ufe02':
					case '\ufe03':
					case '\ufe04':
					case '\ufe05':
					case '\ufe06':
					case '\ufe07':
					case '\ufe08':
					case '\ufe09':
					case '\ufe0a':
					case '\ufe0b':
					case '\ufe0c':
					case '\ufe0d':
					case '\ufe0e':
					case '\ufe0f':
					case '︐':
					case '︑':
					case '︒':
					case '︓':
					case '︔':
					case '︕':
					case '︖':
					case '︗':
					case '︘':
					case '︙':
					case '\ufe1a':
					case '\ufe1b':
					case '\ufe1c':
					case '\ufe1d':
					case '\ufe1e':
					case '\ufe1f':
					case '\ufe20':
					case '\ufe21':
					case '\ufe22':
					case '\ufe23':
					case '\ufe24':
					case '\ufe25':
					case '\ufe26':
					case '\ufe27':
					case '\ufe28':
					case '\ufe29':
					case '\ufe2a':
					case '\ufe2b':
					case '\ufe2c':
					case '\ufe2d':
					case '\ufe2e':
					case '\ufe2f':
					case '︰':
					case '︱':
					case '︲':
					case '\ufe33':
					case '\ufe34':
					case '︵':
					case '︶':
					case '︷':
					case '︸':
					case '︹':
					case '︺':
					case '︻':
					case '︼':
					case '︽':
					case '︾':
					case '︿':
					case '﹀':
					case '﹁':
					case '﹂':
					case '﹃':
					case '﹄':
					case '﹅':
					case '﹆':
					case '﹇':
					case '﹈':
					case '﹉':
					case '﹊':
					case '﹋':
					case '﹌':
					case '\ufe4d':
					case '\ufe4e':
					case '\ufe4f':
					case '﹐':
					case '﹑':
					case '﹒':
					case '\ufe53':
					case '﹔':
					case '﹕':
					case '﹖':
					case '﹗':
					case '﹘':
					case '﹙':
					case '﹚':
					case '﹛':
					case '﹜':
					case '﹝':
					case '﹞':
					case '﹟':
					case '﹠':
					case '﹡':
					case '﹢':
					case '﹣':
					case '﹤':
					case '﹥':
					case '﹦':
					case '\ufe67':
					case '﹨':
					case '﹩':
					case '﹪':
					case '﹫':
					case '\ufe6c':
					case '\ufe6d':
					case '\ufe6e':
					case '\ufe6f':
					case 'ﹳ':
					case '\ufe75':
					case 'ﺀ':
					case 'ﺁ':
					case 'ﺂ':
					case 'ﺃ':
					case 'ﺄ':
					case 'ﺅ':
					case 'ﺆ':
					case 'ﺇ':
					case 'ﺈ':
					case 'ﺉ':
					case 'ﺊ':
					case 'ﺋ':
					case 'ﺌ':
					case 'ﺍ':
					case 'ﺎ':
					case 'ﺏ':
					case 'ﺐ':
					case 'ﺑ':
					case 'ﺒ':
					case 'ﺓ':
					case 'ﺔ':
					case 'ﺕ':
					case 'ﺖ':
					case 'ﺗ':
					case 'ﺘ':
					case 'ﺙ':
					case 'ﺚ':
					case 'ﺛ':
					case 'ﺜ':
					case 'ﺝ':
					case 'ﺞ':
					case 'ﺟ':
					case 'ﺠ':
					case 'ﺡ':
					case 'ﺢ':
					case 'ﺣ':
					case 'ﺤ':
					case 'ﺥ':
					case 'ﺦ':
					case 'ﺧ':
					case 'ﺨ':
					case 'ﺩ':
					case 'ﺪ':
					case 'ﺫ':
					case 'ﺬ':
					case 'ﺭ':
					case 'ﺮ':
					case 'ﺯ':
					case 'ﺰ':
					case 'ﺱ':
					case 'ﺲ':
					case 'ﺳ':
					case 'ﺴ':
					case 'ﺵ':
					case 'ﺶ':
					case 'ﺷ':
					case 'ﺸ':
					case 'ﺹ':
					case 'ﺺ':
					case 'ﺻ':
					case 'ﺼ':
					case 'ﺽ':
					case 'ﺾ':
					case 'ﺿ':
					case 'ﻀ':
					case 'ﻁ':
					case 'ﻂ':
					case 'ﻃ':
					case 'ﻄ':
					case 'ﻅ':
					case 'ﻆ':
					case 'ﻇ':
					case 'ﻈ':
					case 'ﻉ':
					case 'ﻊ':
					case 'ﻋ':
					case 'ﻌ':
					case 'ﻍ':
					case 'ﻎ':
					case 'ﻏ':
					case 'ﻐ':
					case 'ﻑ':
					case 'ﻒ':
					case 'ﻓ':
					case 'ﻔ':
					case 'ﻕ':
					case 'ﻖ':
					case 'ﻗ':
					case 'ﻘ':
					case 'ﻙ':
					case 'ﻚ':
					case 'ﻛ':
					case 'ﻜ':
					case 'ﻝ':
					case 'ﻞ':
					case 'ﻟ':
					case 'ﻠ':
					case 'ﻡ':
					case 'ﻢ':
					case 'ﻣ':
					case 'ﻤ':
					case 'ﻥ':
					case 'ﻦ':
					case 'ﻧ':
					case 'ﻨ':
					case 'ﻩ':
					case 'ﻪ':
					case 'ﻫ':
					case 'ﻬ':
					case 'ﻭ':
					case 'ﻮ':
					case 'ﻯ':
					case 'ﻰ':
					case 'ﻱ':
					case 'ﻲ':
					case 'ﻳ':
					case 'ﻴ':
						break;
					case 'ﬓ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'մ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ն';
						goto IL_8A47;
					}
					case 'ﬔ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'մ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ե';
						goto IL_8A47;
					}
					case 'ﬕ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'մ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ի';
						goto IL_8A47;
					}
					case 'ﬖ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'վ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ն';
						goto IL_8A47;
					}
					case 'ﬗ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'մ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'խ';
						goto IL_8A47;
					}
					case 'יִ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'י';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05b4';
						goto IL_8A47;
					}
					case 'ײַ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ײ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05b7';
						goto IL_8A47;
					}
					case 'שׁ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ש';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05c1';
						goto IL_8A47;
					}
					case 'שׂ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ש';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05c2';
						goto IL_8A47;
					}
					case 'שּׁ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'שּ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05c1';
						goto IL_8A47;
					}
					case 'שּׂ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'שּ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05c2';
						goto IL_8A47;
					}
					case 'אַ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'א';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05b7';
						goto IL_8A47;
					}
					case 'אָ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'א';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05b8';
						goto IL_8A47;
					}
					case 'אּ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'א';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05bc';
						goto IL_8A47;
					}
					case 'בּ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ב';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05bc';
						goto IL_8A47;
					}
					case 'גּ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ג';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05bc';
						goto IL_8A47;
					}
					case 'דּ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ד';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05bc';
						goto IL_8A47;
					}
					case 'הּ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ה';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05bc';
						goto IL_8A47;
					}
					case 'וּ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ו';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05bc';
						goto IL_8A47;
					}
					case 'זּ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ז';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05bc';
						goto IL_8A47;
					}
					case 'טּ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ט';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05bc';
						goto IL_8A47;
					}
					case 'יּ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'י';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05bc';
						goto IL_8A47;
					}
					case 'ךּ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ך';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05bc';
						goto IL_8A47;
					}
					case 'כּ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'כ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05bc';
						goto IL_8A47;
					}
					case 'לּ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ל';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05bc';
						goto IL_8A47;
					}
					case 'מּ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'מ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05bc';
						goto IL_8A47;
					}
					case 'נּ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'נ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05bc';
						goto IL_8A47;
					}
					case 'סּ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ס';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05bc';
						goto IL_8A47;
					}
					case 'ףּ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ף';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05bc';
						goto IL_8A47;
					}
					case 'פּ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'פ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05bc';
						goto IL_8A47;
					}
					case 'צּ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'צ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05bc';
						goto IL_8A47;
					}
					case 'קּ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ק';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05bc';
						goto IL_8A47;
					}
					case 'רּ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ר';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05bc';
						goto IL_8A47;
					}
					case 'שּ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ש';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05bc';
						goto IL_8A47;
					}
					case 'תּ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ת';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05bc';
						goto IL_8A47;
					}
					case 'וֹ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ו';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05b9';
						goto IL_8A47;
					}
					case 'בֿ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ב';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05bf';
						goto IL_8A47;
					}
					case 'כֿ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'כ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05bf';
						goto IL_8A47;
					}
					case 'פֿ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'פ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05bf';
						goto IL_8A47;
					}
					case 'ﭏ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'א';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ל';
						goto IL_8A47;
					}
					case 'ﯪ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ئ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ا';
						goto IL_8A47;
					}
					case 'ﯫ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ئ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ا';
						goto IL_8A47;
					}
					case 'ﯬ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ئ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ە';
						goto IL_8A47;
					}
					case 'ﯭ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ئ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ە';
						goto IL_8A47;
					}
					case 'ﯮ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ئ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'و';
						goto IL_8A47;
					}
					case 'ﯯ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ئ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'و';
						goto IL_8A47;
					}
					case 'ﯰ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ئ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ۇ';
						goto IL_8A47;
					}
					case 'ﯱ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ئ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ۇ';
						goto IL_8A47;
					}
					case 'ﯲ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ئ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ۆ';
						goto IL_8A47;
					}
					case 'ﯳ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ئ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ۆ';
						goto IL_8A47;
					}
					case 'ﯴ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ئ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ۈ';
						goto IL_8A47;
					}
					case 'ﯵ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ئ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ۈ';
						goto IL_8A47;
					}
					case 'ﯶ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ئ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ې';
						goto IL_8A47;
					}
					case 'ﯷ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ئ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ې';
						goto IL_8A47;
					}
					case 'ﯸ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ئ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ې';
						goto IL_8A47;
					}
					case 'ﯹ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ئ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ى';
						goto IL_8A47;
					}
					case 'ﯺ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ئ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ى';
						goto IL_8A47;
					}
					case 'ﯻ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ئ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ى';
						goto IL_8A47;
					}
					case 'ﰀ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ئ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ج';
						goto IL_8A47;
					}
					case 'ﰁ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ئ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ح';
						goto IL_8A47;
					}
					case 'ﰂ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ئ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'م';
						goto IL_8A47;
					}
					case 'ﰃ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ئ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ى';
						goto IL_8A47;
					}
					case 'ﰄ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ئ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ي';
						goto IL_8A47;
					}
					case 'ﰅ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ب';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ج';
						goto IL_8A47;
					}
					case 'ﰆ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ب';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ح';
						goto IL_8A47;
					}
					case 'ﰇ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ب';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'خ';
						goto IL_8A47;
					}
					case 'ﰈ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ب';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'م';
						goto IL_8A47;
					}
					case 'ﰉ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ب';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ى';
						goto IL_8A47;
					}
					case 'ﰊ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ب';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ي';
						goto IL_8A47;
					}
					case 'ﰋ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ت';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ج';
						goto IL_8A47;
					}
					case 'ﰌ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ت';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ح';
						goto IL_8A47;
					}
					case 'ﰍ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ت';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'خ';
						goto IL_8A47;
					}
					case 'ﰎ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ت';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'م';
						goto IL_8A47;
					}
					case 'ﰏ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ت';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ى';
						goto IL_8A47;
					}
					case 'ﰐ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ت';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ي';
						goto IL_8A47;
					}
					case 'ﰑ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ث';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ج';
						goto IL_8A47;
					}
					case 'ﰒ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ث';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'م';
						goto IL_8A47;
					}
					case 'ﰓ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ث';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ى';
						goto IL_8A47;
					}
					case 'ﰔ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ث';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ي';
						goto IL_8A47;
					}
					case 'ﰕ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ج';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ح';
						goto IL_8A47;
					}
					case 'ﰖ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ج';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'م';
						goto IL_8A47;
					}
					case 'ﰗ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ح';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ج';
						goto IL_8A47;
					}
					case 'ﰘ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ح';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'م';
						goto IL_8A47;
					}
					case 'ﰙ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'خ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ج';
						goto IL_8A47;
					}
					case 'ﰚ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'خ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ح';
						goto IL_8A47;
					}
					case 'ﰛ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'خ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'م';
						goto IL_8A47;
					}
					case 'ﰜ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'س';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ج';
						goto IL_8A47;
					}
					case 'ﰝ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'س';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ح';
						goto IL_8A47;
					}
					case 'ﰞ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'س';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'خ';
						goto IL_8A47;
					}
					case 'ﰟ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'س';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'م';
						goto IL_8A47;
					}
					case 'ﰠ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ص';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ح';
						goto IL_8A47;
					}
					case 'ﰡ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ص';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'م';
						goto IL_8A47;
					}
					case 'ﰢ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ض';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ج';
						goto IL_8A47;
					}
					case 'ﰣ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ض';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ح';
						goto IL_8A47;
					}
					case 'ﰤ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ض';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'خ';
						goto IL_8A47;
					}
					case 'ﰥ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ض';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'م';
						goto IL_8A47;
					}
					case 'ﰦ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ط';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ح';
						goto IL_8A47;
					}
					case 'ﰧ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ط';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'م';
						goto IL_8A47;
					}
					case 'ﰨ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ظ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'م';
						goto IL_8A47;
					}
					case 'ﰩ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ع';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ج';
						goto IL_8A47;
					}
					case 'ﰪ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'A';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'E';
						goto IL_8A47;
					}
					case 'ﰫ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'U';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'E';
						goto IL_8A47;
					}
					case 'ﰬ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'T';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'H';
						goto IL_8A47;
					}
					case 'ﰭ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 's';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 's';
						goto IL_8A47;
					}
					case 'ﰮ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'a';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'e';
						goto IL_8A47;
					}
					case 'ﰯ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'u';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'e';
						goto IL_8A47;
					}
					case 'ﰰ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 't';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'h';
						goto IL_8A47;
					}
					case 'ﰱ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'I';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'J';
						goto IL_8A47;
					}
					case 'ﰲ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'i';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'j';
						goto IL_8A47;
					}
					case 'ﰳ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'O';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'E';
						goto IL_8A47;
					}
					case 'ﰴ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'o';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'e';
						goto IL_8A47;
					}
					case 'ﰵ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'D';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'Ž';
						goto IL_8A47;
					}
					case 'ﰶ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'D';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ž';
						goto IL_8A47;
					}
					case 'ﰷ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'd';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ž';
						goto IL_8A47;
					}
					case 'ﰸ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'L';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'J';
						goto IL_8A47;
					}
					case 'ﰹ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'L';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'j';
						goto IL_8A47;
					}
					case 'ﰺ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'l';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'j';
						goto IL_8A47;
					}
					case 'ﰻ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'N';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'J';
						goto IL_8A47;
					}
					case 'ﰼ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'N';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'j';
						goto IL_8A47;
					}
					case 'ﰽ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'n';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'j';
						goto IL_8A47;
					}
					case 'ﰾ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'Ā';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'Ē';
						goto IL_8A47;
					}
					case 'ﰿ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ā';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ē';
						goto IL_8A47;
					}
					case 'ﱀ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'D';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'Z';
						goto IL_8A47;
					}
					case 'ﱁ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'D';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'z';
						goto IL_8A47;
					}
					case 'ﱂ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'd';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'z';
						goto IL_8A47;
					}
					case 'ﱃ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'Á';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'É';
						goto IL_8A47;
					}
					case 'ﱄ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'á';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'é';
						goto IL_8A47;
					}
					case 'ﱅ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'f';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'f';
						goto IL_8A47;
					}
					case 'ﱆ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'f';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'i';
						goto IL_8A47;
					}
					case 'ﱇ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'f';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'l';
						goto IL_8A47;
					}
					case 'ﱈ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'f';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'f';
						num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'i';
						goto IL_8A47;
					}
					case 'ﱉ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'f';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'f';
						num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'l';
						goto IL_8A47;
					}
					case 'ﱊ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ſ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 't';
						goto IL_8A47;
					}
					case 'ﱋ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 's';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 't';
						goto IL_8A47;
					}
					case 'ﱌ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ו';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ו';
						goto IL_8A47;
					}
					case 'ﱍ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ו';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'י';
						goto IL_8A47;
					}
					case 'ﱎ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'י';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'י';
						goto IL_8A47;
					}
					case 'ﱏ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'A';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'U';
						goto IL_8A47;
					}
					case 'ﱐ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'B';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'q';
						goto IL_8A47;
					}
					case 'ﱑ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'c';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'c';
						goto IL_8A47;
					}
					case 'ﱒ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'c';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'd';
						goto IL_8A47;
					}
					case 'ﱓ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'c';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'm';
						goto IL_8A47;
					}
					case 'ﱔ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '㎝';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '²';
						goto IL_8A47;
					}
					case 'ﱕ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '㎝';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '³';
						goto IL_8A47;
					}
					case 'ﱖ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'd';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'a';
						goto IL_8A47;
					}
					case 'ﱗ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'd';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'B';
						goto IL_8A47;
					}
					case 'ﱘ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'd';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ℓ';
						goto IL_8A47;
					}
					case 'ﱙ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'd';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'm';
						goto IL_8A47;
					}
					case 'ﱚ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '㍷';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '²';
						goto IL_8A47;
					}
					case 'ﱛ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '㍷';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '³';
						goto IL_8A47;
					}
					case 'ﱜ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'e';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'V';
						goto IL_8A47;
					}
					case 'ﱝ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'f';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'm';
						goto IL_8A47;
					}
					case 'ﱞ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'G';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'B';
						goto IL_8A47;
					}
					case 'ﱟ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'G';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'y';
						goto IL_8A47;
					}
					case 'ﱠ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'h';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'a';
						goto IL_8A47;
					}
					case 'ﱡ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'H';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'g';
						goto IL_8A47;
					}
					case 'ﱢ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'H';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'P';
						goto IL_8A47;
					}
					case 'ﱣ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'H';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'z';
						goto IL_8A47;
					}
					case 'ﱤ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'i';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'n';
						goto IL_8A47;
					}
					case 'ﱥ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'I';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'U';
						goto IL_8A47;
					}
					case 'ﱦ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'k';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'Ω';
						goto IL_8A47;
					}
					case 'ﱧ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'k';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'A';
						goto IL_8A47;
					}
					case 'ﱨ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'K';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'B';
						goto IL_8A47;
					}
					case 'ﱩ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'k';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'g';
						goto IL_8A47;
					}
					case 'ﱪ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'K';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'K';
						goto IL_8A47;
					}
					case 'ﱫ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'k';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ℓ';
						goto IL_8A47;
					}
					case 'ﱬ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'k';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'm';
						goto IL_8A47;
					}
					case 'ﱭ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'K';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'M';
						goto IL_8A47;
					}
					case 'ﱮ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '㎞';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '²';
						goto IL_8A47;
					}
					case 'ﱯ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '㎞';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '³';
						goto IL_8A47;
					}
					case 'ﱰ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'k';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'V';
						goto IL_8A47;
					}
					case 'ﱱ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'k';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'W';
						goto IL_8A47;
					}
					case 'ﱲ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'k';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 't';
						goto IL_8A47;
					}
					case 'ﱳ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'l';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'm';
						goto IL_8A47;
					}
					case 'ﱴ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'l';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'n';
						goto IL_8A47;
					}
					case 'ﱵ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'l';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'x';
						goto IL_8A47;
					}
					case 'ﱶ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'm';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '²';
						goto IL_8A47;
					}
					case 'ﱷ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'm';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '³';
						goto IL_8A47;
					}
					case 'ﱸ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'M';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'Ω';
						goto IL_8A47;
					}
					case 'ﱹ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'm';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'A';
						goto IL_8A47;
					}
					case 'ﱺ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'M';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'B';
						goto IL_8A47;
					}
					case 'ﱻ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'm';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'b';
						goto IL_8A47;
					}
					case 'ﱼ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'm';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ℓ';
						goto IL_8A47;
					}
					case 'ﱽ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'm';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'g';
						goto IL_8A47;
					}
					case 'ﱾ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'm';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'm';
						goto IL_8A47;
					}
					case 'ﱿ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '㎜';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '²';
						goto IL_8A47;
					}
					case 'ﲀ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '㎜';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '³';
						goto IL_8A47;
					}
					case 'ﲁ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'm';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 's';
						goto IL_8A47;
					}
					case 'ﲂ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'μ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'A';
						goto IL_8A47;
					}
					case 'ﲃ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'μ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'F';
						goto IL_8A47;
					}
					case 'ﲄ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'μ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'g';
						goto IL_8A47;
					}
					case 'ﲅ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'μ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ℓ';
						goto IL_8A47;
					}
					case 'ﲆ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'μ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'm';
						goto IL_8A47;
					}
					case 'ﲇ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'μ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 's';
						goto IL_8A47;
					}
					case 'ﲈ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'μ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'V';
						goto IL_8A47;
					}
					case 'ﲉ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'μ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'W';
						goto IL_8A47;
					}
					case 'ﲊ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'm';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'V';
						goto IL_8A47;
					}
					case 'ﲋ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'M';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'V';
						goto IL_8A47;
					}
					case 'ﲌ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'm';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'W';
						goto IL_8A47;
					}
					case 'ﲍ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'M';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'W';
						goto IL_8A47;
					}
					case 'ﲎ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'n';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'A';
						goto IL_8A47;
					}
					case 'ﲏ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'n';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'F';
						goto IL_8A47;
					}
					case 'ﲐ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'n';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'm';
						goto IL_8A47;
					}
					case 'ﲑ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'n';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 's';
						goto IL_8A47;
					}
					case 'ﲒ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'n';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'V';
						goto IL_8A47;
					}
					case 'ﲓ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'n';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'W';
						goto IL_8A47;
					}
					case 'ﲔ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'o';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'V';
						goto IL_8A47;
					}
					case 'ﲕ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'P';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'a';
						goto IL_8A47;
					}
					case 'ﲖ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'p';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'A';
						goto IL_8A47;
					}
					case 'ﲗ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'p';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'c';
						goto IL_8A47;
					}
					case 'ﲘ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'p';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'F';
						goto IL_8A47;
					}
					case 'ﲙ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'P';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'H';
						goto IL_8A47;
					}
					case 'ﲚ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'P';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'R';
						goto IL_8A47;
					}
					case 'ﲛ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'p';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 's';
						goto IL_8A47;
					}
					case 'ﲜ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'p';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'V';
						goto IL_8A47;
					}
					case 'ﲝ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'p';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'W';
						goto IL_8A47;
					}
					case 'ﲞ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 's';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'r';
						goto IL_8A47;
					}
					case 'ﲟ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'S';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'v';
						goto IL_8A47;
					}
					case 'ﲠ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'W';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'b';
						goto IL_8A47;
					}
					case 'ﲡ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'D';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'b';
						goto IL_8A47;
					}
					case 'ﲢ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'Q';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'p';
						goto IL_8A47;
					}
					case 'ﲣ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'f';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'n';
						goto IL_8A47;
					}
					case 'ﲤ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'l';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 's';
						goto IL_8A47;
					}
					case 'ﲥ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'l';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'z';
						goto IL_8A47;
					}
					case 'ﲦ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'A';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'E';
						goto IL_8A47;
					}
					case 'ﲧ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'a';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'e';
						goto IL_8A47;
					}
					case 'ﲨ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'A';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'E';
						goto IL_8A47;
					}
					case 'ﲩ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'a';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'e';
						goto IL_8A47;
					}
					case 'ﲪ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '\u00a8';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u0301';
						goto IL_8A47;
					}
					case 'ﲫ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ե';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ւ';
						goto IL_8A47;
					}
					case 'ﲬ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ا';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ٴ';
						goto IL_8A47;
					}
					case 'ﲭ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'و';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ٴ';
						goto IL_8A47;
					}
					case 'ﲮ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ي';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ٴ';
						goto IL_8A47;
					}
					case 'ﲯ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ص';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'Á';
						num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'É';
						goto IL_8A47;
					}
					case 'ﲰ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ق';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'Á';
						num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'É';
						goto IL_8A47;
					}
					case 'ﲱ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ག';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u0fb7';
						goto IL_8A47;
					}
					case 'ﲲ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ཌ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u0fb7';
						goto IL_8A47;
					}
					case 'ﲳ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ད';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u0fb7';
						goto IL_8A47;
					}
					case 'ﲴ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'བ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u0fb7';
						goto IL_8A47;
					}
					case 'ﲵ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ཛ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u0fb7';
						goto IL_8A47;
					}
					case 'ﲶ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ཀ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u0fb5';
						goto IL_8A47;
					}
					case 'ﲷ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '\u0f71';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u0f72';
						goto IL_8A47;
					}
					case 'ﲸ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '\u0f71';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u0f74';
						goto IL_8A47;
					}
					case 'ﲹ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '\u0fb2';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u0f80';
						goto IL_8A47;
					}
					case 'ﲺ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '\u0fb2';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u0f71';
						num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u0f80';
						goto IL_8A47;
					}
					case 'ﲻ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '\u0fb3';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u0f80';
						goto IL_8A47;
					}
					case 'ﲼ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '\u0fb3';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u0f71';
						num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u0f80';
						goto IL_8A47;
					}
					case 'ﲽ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '\u0f71';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u0f80';
						goto IL_8A47;
					}
					case 'ﲾ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '\u0f92';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u0fb7';
						goto IL_8A47;
					}
					case 'ﲿ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '\u0f9c';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u0fb7';
						goto IL_8A47;
					}
					case 'ﳀ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '\u0fa1';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u0fb7';
						goto IL_8A47;
					}
					case 'ﳁ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '\u0fa6';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u0fb7';
						goto IL_8A47;
					}
					case 'ﳂ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '\u0fab';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u0fb7';
						goto IL_8A47;
					}
					case 'ﳃ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '\u0f90';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u0fb5';
						goto IL_8A47;
					}
					case 'ﳄ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'よ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'り';
						goto IL_8A47;
					}
					case 'ﳅ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'コ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ト';
						goto IL_8A47;
					}
					case 'ﳆ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'մ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ն';
						goto IL_8A47;
					}
					case 'ﳇ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'մ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ե';
						goto IL_8A47;
					}
					case 'ﳈ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'մ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ի';
						goto IL_8A47;
					}
					case 'ﳉ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'վ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ն';
						goto IL_8A47;
					}
					case 'ﳊ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'մ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'խ';
						goto IL_8A47;
					}
					case 'ﳋ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'י';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05b4';
						goto IL_8A47;
					}
					case 'ﳌ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ײ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05b7';
						goto IL_8A47;
					}
					case 'ﳍ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ש';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05c1';
						goto IL_8A47;
					}
					case 'ﳎ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ש';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05c2';
						goto IL_8A47;
					}
					case 'ﳏ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'שּ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05c1';
						goto IL_8A47;
					}
					case 'ﳐ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'שּ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05c2';
						goto IL_8A47;
					}
					case 'ﳑ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'א';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05b7';
						goto IL_8A47;
					}
					case 'ﳒ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'א';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05b8';
						goto IL_8A47;
					}
					case 'ﳓ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'א';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05bc';
						goto IL_8A47;
					}
					case 'ﳔ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ב';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05bc';
						goto IL_8A47;
					}
					case 'ﳕ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ג';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05bc';
						goto IL_8A47;
					}
					case 'ﳖ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ד';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05bc';
						goto IL_8A47;
					}
					case 'ﳗ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ה';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05bc';
						goto IL_8A47;
					}
					case 'ﳘ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ו';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05bc';
						goto IL_8A47;
					}
					case 'ﳙ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ז';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05bc';
						goto IL_8A47;
					}
					case 'ﳚ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ט';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05bc';
						goto IL_8A47;
					}
					case 'ﳛ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'י';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05bc';
						goto IL_8A47;
					}
					case 'ﳜ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ך';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05bc';
						goto IL_8A47;
					}
					case 'ﳝ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'כ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05bc';
						goto IL_8A47;
					}
					case 'ﳞ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ל';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05bc';
						goto IL_8A47;
					}
					case 'ﳟ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'מ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05bc';
						goto IL_8A47;
					}
					case 'ﳠ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'נ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05bc';
						goto IL_8A47;
					}
					case 'ﳡ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ס';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05bc';
						goto IL_8A47;
					}
					case 'ﳢ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ף';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05bc';
						goto IL_8A47;
					}
					case 'ﳣ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'פ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05bc';
						goto IL_8A47;
					}
					case 'ﳤ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'צ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05bc';
						goto IL_8A47;
					}
					case 'ﳥ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ק';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05bc';
						goto IL_8A47;
					}
					case 'ﳦ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ר';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05bc';
						goto IL_8A47;
					}
					case 'ﳧ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ש';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05bc';
						goto IL_8A47;
					}
					case 'ﳨ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ת';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05bc';
						goto IL_8A47;
					}
					case 'ﳩ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ו';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05b9';
						goto IL_8A47;
					}
					case 'ﳪ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ב';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05bf';
						goto IL_8A47;
					}
					case 'ﳫ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'כ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05bf';
						goto IL_8A47;
					}
					case 'ﳬ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'פ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u05bf';
						goto IL_8A47;
					}
					case 'ﳭ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'א';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ל';
						goto IL_8A47;
					}
					case 'ﳮ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ئ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ا';
						goto IL_8A47;
					}
					case 'ﳯ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ئ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ا';
						goto IL_8A47;
					}
					case 'ﳰ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ئ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ە';
						goto IL_8A47;
					}
					case 'ﳱ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ئ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ە';
						goto IL_8A47;
					}
					case 'ﳲ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ئ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'و';
						goto IL_8A47;
					}
					case 'ﳳ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ئ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'و';
						goto IL_8A47;
					}
					case 'ﳴ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ئ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ۇ';
						goto IL_8A47;
					}
					case 'ﳵ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ئ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ۇ';
						goto IL_8A47;
					}
					case 'ﳶ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ئ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ۆ';
						goto IL_8A47;
					}
					case 'ﳷ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ئ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ۆ';
						goto IL_8A47;
					}
					case 'ﳸ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ئ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ۈ';
						goto IL_8A47;
					}
					case 'ﳹ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ئ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ۈ';
						goto IL_8A47;
					}
					case 'ﳺ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ئ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ې';
						goto IL_8A47;
					}
					case 'ﳻ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ئ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ې';
						goto IL_8A47;
					}
					case 'ﳼ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ئ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ې';
						goto IL_8A47;
					}
					case 'ﳽ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ئ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ى';
						goto IL_8A47;
					}
					case 'ﳾ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ئ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ى';
						goto IL_8A47;
					}
					case 'ﳿ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ئ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ى';
						goto IL_8A47;
					}
					case 'ﴀ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ئ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ج';
						goto IL_8A47;
					}
					case 'ﴁ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ئ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ح';
						goto IL_8A47;
					}
					case 'ﴂ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ئ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'م';
						goto IL_8A47;
					}
					case 'ﴃ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ئ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ى';
						goto IL_8A47;
					}
					case 'ﴄ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ئ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ي';
						goto IL_8A47;
					}
					case 'ﴅ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ب';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ج';
						goto IL_8A47;
					}
					case 'ﴆ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ب';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ح';
						goto IL_8A47;
					}
					case 'ﴇ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ب';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'خ';
						goto IL_8A47;
					}
					case 'ﴈ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ب';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'م';
						goto IL_8A47;
					}
					case 'ﴉ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ب';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ى';
						goto IL_8A47;
					}
					case 'ﴊ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ب';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ي';
						goto IL_8A47;
					}
					case 'ﴋ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ت';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ج';
						goto IL_8A47;
					}
					case 'ﴌ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ت';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ح';
						goto IL_8A47;
					}
					case 'ﴍ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ت';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'خ';
						goto IL_8A47;
					}
					case 'ﴎ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ت';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'م';
						goto IL_8A47;
					}
					case 'ﴏ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ت';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ى';
						goto IL_8A47;
					}
					case 'ﴐ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ت';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ي';
						goto IL_8A47;
					}
					case 'ﴑ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ث';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ج';
						goto IL_8A47;
					}
					case 'ﴒ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ث';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'م';
						goto IL_8A47;
					}
					case 'ﴓ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ث';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ى';
						goto IL_8A47;
					}
					case 'ﴔ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ث';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ي';
						goto IL_8A47;
					}
					case 'ﴕ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ج';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ح';
						goto IL_8A47;
					}
					case 'ﴖ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ج';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'م';
						goto IL_8A47;
					}
					case 'ﴗ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ح';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ج';
						goto IL_8A47;
					}
					case 'ﴘ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ح';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'م';
						goto IL_8A47;
					}
					case 'ﴙ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'خ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ج';
						goto IL_8A47;
					}
					case 'ﴚ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'خ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ح';
						goto IL_8A47;
					}
					case 'ﴛ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'خ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'م';
						goto IL_8A47;
					}
					case 'ﴜ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'س';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ج';
						goto IL_8A47;
					}
					case 'ﴝ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'س';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ح';
						goto IL_8A47;
					}
					case 'ﴞ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'س';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'خ';
						goto IL_8A47;
					}
					case 'ﴟ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'س';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'م';
						goto IL_8A47;
					}
					case 'ﴠ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ص';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ح';
						goto IL_8A47;
					}
					case 'ﴡ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ص';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'م';
						goto IL_8A47;
					}
					case 'ﴢ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ض';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ج';
						goto IL_8A47;
					}
					case 'ﴣ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ض';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ح';
						goto IL_8A47;
					}
					case 'ﴤ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ض';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'خ';
						goto IL_8A47;
					}
					case 'ﴥ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ض';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'م';
						goto IL_8A47;
					}
					case 'ﴦ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ط';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ح';
						goto IL_8A47;
					}
					case 'ﴧ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ط';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'م';
						goto IL_8A47;
					}
					case 'ﴨ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ظ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'م';
						goto IL_8A47;
					}
					case 'ﴩ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ع';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ج';
						goto IL_8A47;
					}
					case 'ﴪ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'A';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'E';
						goto IL_8A47;
					}
					case 'ﴫ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'U';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'E';
						goto IL_8A47;
					}
					case 'ﴬ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'T';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'H';
						goto IL_8A47;
					}
					case 'ﴭ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 's';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 's';
						goto IL_8A47;
					}
					case 'ﴮ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'a';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'e';
						goto IL_8A47;
					}
					case 'ﴯ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'u';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'e';
						goto IL_8A47;
					}
					case 'ﴰ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 't';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'h';
						goto IL_8A47;
					}
					case 'ﴱ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'I';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'J';
						goto IL_8A47;
					}
					case 'ﴲ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'i';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'j';
						goto IL_8A47;
					}
					case 'ﴳ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'O';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'E';
						goto IL_8A47;
					}
					case 'ﴴ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'o';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'e';
						goto IL_8A47;
					}
					case 'ﴵ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'D';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'Ž';
						goto IL_8A47;
					}
					case 'ﴶ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'D';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ž';
						goto IL_8A47;
					}
					case 'ﴷ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'd';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ž';
						goto IL_8A47;
					}
					case 'ﴸ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'L';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'J';
						goto IL_8A47;
					}
					case 'ﴹ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'L';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'j';
						goto IL_8A47;
					}
					case 'ﴺ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'l';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'j';
						goto IL_8A47;
					}
					case 'ﴻ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'N';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'J';
						goto IL_8A47;
					}
					case 'ﴼ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'N';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'j';
						goto IL_8A47;
					}
					case 'ﴽ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'n';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'j';
						goto IL_8A47;
					}
					case 'ﵐ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'Ā';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'Ē';
						goto IL_8A47;
					}
					case 'ﵑ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ā';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ē';
						goto IL_8A47;
					}
					case 'ﵒ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'D';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'Z';
						goto IL_8A47;
					}
					case 'ﵓ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'D';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'z';
						goto IL_8A47;
					}
					case 'ﵔ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'd';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'z';
						goto IL_8A47;
					}
					case 'ﵕ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'Á';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'É';
						goto IL_8A47;
					}
					case 'ﵖ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'á';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'é';
						goto IL_8A47;
					}
					case 'ﵗ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'f';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'f';
						goto IL_8A47;
					}
					case 'ﵘ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'f';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'i';
						goto IL_8A47;
					}
					case 'ﵙ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'f';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'l';
						goto IL_8A47;
					}
					case 'ﵚ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'f';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'f';
						num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'i';
						goto IL_8A47;
					}
					case 'ﵛ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'f';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'f';
						num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'l';
						goto IL_8A47;
					}
					case 'ﵜ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ſ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 't';
						goto IL_8A47;
					}
					case 'ﵝ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 's';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 't';
						goto IL_8A47;
					}
					case 'ﵞ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ו';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ו';
						goto IL_8A47;
					}
					case 'ﵟ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ו';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'י';
						goto IL_8A47;
					}
					case 'ﵠ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'י';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'י';
						goto IL_8A47;
					}
					case 'ﵡ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'A';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'U';
						goto IL_8A47;
					}
					case 'ﵢ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'B';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'q';
						goto IL_8A47;
					}
					case 'ﵣ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'c';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'c';
						goto IL_8A47;
					}
					case 'ﵤ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'c';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'd';
						goto IL_8A47;
					}
					case 'ﵥ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'c';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'm';
						goto IL_8A47;
					}
					case 'ﵦ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '㎝';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '²';
						goto IL_8A47;
					}
					case 'ﵧ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '㎝';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '³';
						goto IL_8A47;
					}
					case 'ﵨ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'd';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'a';
						goto IL_8A47;
					}
					case 'ﵩ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'd';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'B';
						goto IL_8A47;
					}
					case 'ﵪ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'd';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ℓ';
						goto IL_8A47;
					}
					case 'ﵫ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'd';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'm';
						goto IL_8A47;
					}
					case 'ﵬ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '㍷';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '²';
						goto IL_8A47;
					}
					case 'ﵭ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '㍷';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '³';
						goto IL_8A47;
					}
					case 'ﵮ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'e';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'V';
						goto IL_8A47;
					}
					case 'ﵯ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'f';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'm';
						goto IL_8A47;
					}
					case 'ﵰ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'G';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'B';
						goto IL_8A47;
					}
					case 'ﵱ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'G';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'y';
						goto IL_8A47;
					}
					case 'ﵲ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'h';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'a';
						goto IL_8A47;
					}
					case 'ﵳ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'H';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'g';
						goto IL_8A47;
					}
					case 'ﵴ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'H';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'P';
						goto IL_8A47;
					}
					case 'ﵵ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'H';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'z';
						goto IL_8A47;
					}
					case 'ﵶ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'i';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'n';
						goto IL_8A47;
					}
					case 'ﵷ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'I';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'U';
						goto IL_8A47;
					}
					case 'ﵸ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'k';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'Ω';
						goto IL_8A47;
					}
					case 'ﵹ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'k';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'A';
						goto IL_8A47;
					}
					case 'ﵺ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'K';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'B';
						goto IL_8A47;
					}
					case 'ﵻ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'k';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'g';
						goto IL_8A47;
					}
					case 'ﵼ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'K';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'K';
						goto IL_8A47;
					}
					case 'ﵽ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'k';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ℓ';
						goto IL_8A47;
					}
					case 'ﵾ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'k';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'm';
						goto IL_8A47;
					}
					case 'ﵿ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'K';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'M';
						goto IL_8A47;
					}
					case 'ﶀ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '㎞';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '²';
						goto IL_8A47;
					}
					case 'ﶁ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '㎞';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '³';
						goto IL_8A47;
					}
					case 'ﶂ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'k';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'V';
						goto IL_8A47;
					}
					case 'ﶃ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'k';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'W';
						goto IL_8A47;
					}
					case 'ﶄ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'k';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 't';
						goto IL_8A47;
					}
					case 'ﶅ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'l';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'm';
						goto IL_8A47;
					}
					case 'ﶆ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'l';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'n';
						goto IL_8A47;
					}
					case 'ﶇ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'l';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'x';
						goto IL_8A47;
					}
					case 'ﶈ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'm';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '²';
						goto IL_8A47;
					}
					case 'ﶉ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'm';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '³';
						goto IL_8A47;
					}
					case 'ﶊ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'M';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'Ω';
						goto IL_8A47;
					}
					case 'ﶋ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'm';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'A';
						goto IL_8A47;
					}
					case 'ﶌ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'M';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'B';
						goto IL_8A47;
					}
					case 'ﶍ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'm';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'b';
						goto IL_8A47;
					}
					case 'ﶎ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'm';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ℓ';
						goto IL_8A47;
					}
					case 'ﶏ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'm';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'g';
						goto IL_8A47;
					}
					case 'ﶒ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'm';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'm';
						goto IL_8A47;
					}
					case 'ﶓ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '㎜';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '²';
						goto IL_8A47;
					}
					case 'ﶔ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '㎜';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '³';
						goto IL_8A47;
					}
					case 'ﶕ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'm';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 's';
						goto IL_8A47;
					}
					case 'ﶖ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'μ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'A';
						goto IL_8A47;
					}
					case 'ﶗ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'μ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'F';
						goto IL_8A47;
					}
					case 'ﶘ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'μ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'g';
						goto IL_8A47;
					}
					case 'ﶙ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'μ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ℓ';
						goto IL_8A47;
					}
					case 'ﶚ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'μ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'm';
						goto IL_8A47;
					}
					case 'ﶛ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'μ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 's';
						goto IL_8A47;
					}
					case 'ﶜ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'μ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'V';
						goto IL_8A47;
					}
					case 'ﶝ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'μ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'W';
						goto IL_8A47;
					}
					case 'ﶞ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'm';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'V';
						goto IL_8A47;
					}
					case 'ﶟ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'M';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'V';
						goto IL_8A47;
					}
					case 'ﶠ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'm';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'W';
						goto IL_8A47;
					}
					case 'ﶡ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'M';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'W';
						goto IL_8A47;
					}
					case 'ﶢ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'n';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'A';
						goto IL_8A47;
					}
					case 'ﶣ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'n';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'F';
						goto IL_8A47;
					}
					case 'ﶤ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'n';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'm';
						goto IL_8A47;
					}
					case 'ﶥ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'n';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 's';
						goto IL_8A47;
					}
					case 'ﶦ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'n';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'V';
						goto IL_8A47;
					}
					case 'ﶧ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'n';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'W';
						goto IL_8A47;
					}
					case 'ﶨ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'o';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'V';
						goto IL_8A47;
					}
					case 'ﶩ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'P';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'a';
						goto IL_8A47;
					}
					case 'ﶪ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'p';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'A';
						goto IL_8A47;
					}
					case 'ﶫ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'p';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'c';
						goto IL_8A47;
					}
					case 'ﶬ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'p';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'F';
						goto IL_8A47;
					}
					case 'ﶭ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'P';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'H';
						goto IL_8A47;
					}
					case 'ﶮ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'P';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'R';
						goto IL_8A47;
					}
					case 'ﶯ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'p';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 's';
						goto IL_8A47;
					}
					case 'ﶰ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'p';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'V';
						goto IL_8A47;
					}
					case 'ﶱ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'p';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'W';
						goto IL_8A47;
					}
					case 'ﶲ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 's';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'r';
						goto IL_8A47;
					}
					case 'ﶳ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'S';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'v';
						goto IL_8A47;
					}
					case 'ﶴ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'W';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'b';
						goto IL_8A47;
					}
					case 'ﶵ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'D';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'b';
						goto IL_8A47;
					}
					case 'ﶶ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'Q';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'p';
						goto IL_8A47;
					}
					case 'ﶷ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'f';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'n';
						goto IL_8A47;
					}
					case 'ﶸ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'l';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 's';
						goto IL_8A47;
					}
					case 'ﶹ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'l';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'z';
						goto IL_8A47;
					}
					case 'ﶺ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'A';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'E';
						goto IL_8A47;
					}
					case 'ﶻ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'a';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'e';
						goto IL_8A47;
					}
					case 'ﶼ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'A';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'E';
						goto IL_8A47;
					}
					case 'ﶽ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'a';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'e';
						goto IL_8A47;
					}
					case 'ﶾ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '\u00a8';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u0301';
						goto IL_8A47;
					}
					case 'ﶿ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ե';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ւ';
						goto IL_8A47;
					}
					case 'ﷀ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ا';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ٴ';
						goto IL_8A47;
					}
					case 'ﷁ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'و';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ٴ';
						goto IL_8A47;
					}
					case 'ﷂ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ي';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ٴ';
						goto IL_8A47;
					}
					case 'ﷃ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ص';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'Á';
						num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'É';
						goto IL_8A47;
					}
					case 'ﷄ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ق';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'Á';
						num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'É';
						goto IL_8A47;
					}
					case 'ﷅ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ག';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u0fb7';
						goto IL_8A47;
					}
					case 'ﷆ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ཌ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u0fb7';
						goto IL_8A47;
					}
					case 'ﷇ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ད';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u0fb7';
						goto IL_8A47;
					}
					case 'ﷹ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'བ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u0fb7';
						goto IL_8A47;
					}
					case 'ﹰ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ཛ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u0fb7';
						goto IL_8A47;
					}
					case 'ﹱ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'ཀ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u0fb5';
						goto IL_8A47;
					}
					case 'ﹲ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '\u0f71';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u0f72';
						goto IL_8A47;
					}
					case 'ﹴ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '\u0f71';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u0f74';
						goto IL_8A47;
					}
					case 'ﹶ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '\u0fb2';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u0f80';
						goto IL_8A47;
					}
					case 'ﹷ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '\u0fb2';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u0f71';
						num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u0f80';
						goto IL_8A47;
					}
					case 'ﹸ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '\u0fb3';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u0f80';
						goto IL_8A47;
					}
					case 'ﹹ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '\u0fb3';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u0f71';
						num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u0f80';
						goto IL_8A47;
					}
					case 'ﹺ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '\u0f71';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u0f80';
						goto IL_8A47;
					}
					case 'ﹻ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '\u0f92';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u0fb7';
						goto IL_8A47;
					}
					case 'ﹼ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '\u0f9c';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u0fb7';
						goto IL_8A47;
					}
					case 'ﹽ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '\u0fa1';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u0fb7';
						goto IL_8A47;
					}
					case 'ﹾ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '\u0fa6';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u0fb7';
						goto IL_8A47;
					}
					case 'ﹿ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '\u0fab';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u0fb7';
						goto IL_8A47;
					}
					case 'ﻵ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '\u0f90';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = '\u0fb5';
						goto IL_8A47;
					}
					case 'ﻶ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'よ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'り';
						goto IL_8A47;
					}
					case 'ﻷ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'コ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ト';
						goto IL_8A47;
					}
					case 'ﻸ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'մ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ն';
						goto IL_8A47;
					}
					case 'ﻹ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'մ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ե';
						goto IL_8A47;
					}
					case 'ﻺ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'մ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ի';
						goto IL_8A47;
					}
					case 'ﻻ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'վ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'ն';
						goto IL_8A47;
					}
					case 'ﻼ':
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'մ';
						int num2 = num;
						num = num2 + 1;
						outputBuffer[num2] = 'խ';
						goto IL_8A47;
					}
					default:
						if (c == '\ufffe')
						{
							num = outputStartIndex + 1;
							outputBuffer[outputStartIndex] = '\ufffe';
							goto IL_8A47;
						}
						break;
					}
					break;
				}
			}
			num = outputStartIndex + 1;
			outputBuffer[outputStartIndex] = c;
			IL_8A47:
			return num - outputStartIndex;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x0000BE20 File Offset: 0x0000A020
		internal static char ToFullWidth(char c)
		{
			switch (c)
			{
			case ' ':
				return '\u3000';
			case '!':
				return '！';
			case '"':
				return '＂';
			case '#':
				return '＃';
			case '$':
				return '＄';
			case '%':
				return '％';
			case '&':
				return '＆';
			case '\'':
				return '＇';
			case '(':
				return '（';
			case ')':
				return '）';
			case '*':
				return '＊';
			case '+':
				return '＋';
			case ',':
				return '，';
			case '-':
				return '－';
			case '.':
				return '．';
			case '/':
				return '／';
			case '0':
				return '０';
			case '1':
				return '１';
			case '2':
				return '２';
			case '3':
				return '３';
			case '4':
				return '４';
			case '5':
				return '５';
			case '6':
				return '６';
			case '7':
				return '７';
			case '8':
				return '８';
			case '9':
				return '９';
			case ':':
				return '：';
			case ';':
				return '；';
			case '<':
				return '＜';
			case '=':
				return '＝';
			case '>':
				return '＞';
			case '?':
				return '？';
			case '@':
				return '＠';
			case 'A':
				return 'Ａ';
			case 'B':
				return 'Ｂ';
			case 'C':
				return 'Ｃ';
			case 'D':
				return 'Ｄ';
			case 'E':
				return 'Ｅ';
			case 'F':
				return 'Ｆ';
			case 'G':
				return 'Ｇ';
			case 'H':
				return 'Ｈ';
			case 'I':
				return 'Ｉ';
			case 'J':
				return 'Ｊ';
			case 'K':
				return 'Ｋ';
			case 'L':
				return 'Ｌ';
			case 'M':
				return 'Ｍ';
			case 'N':
				return 'Ｎ';
			case 'O':
				return 'Ｏ';
			case 'P':
				return 'Ｐ';
			case 'Q':
				return 'Ｑ';
			case 'R':
				return 'Ｒ';
			case 'S':
				return 'Ｓ';
			case 'T':
				return 'Ｔ';
			case 'U':
				return 'Ｕ';
			case 'V':
				return 'Ｖ';
			case 'W':
				return 'Ｗ';
			case 'X':
				return 'Ｘ';
			case 'Y':
				return 'Ｙ';
			case 'Z':
				return 'Ｚ';
			case '[':
				return '［';
			case '\\':
			case '\u007f':
			case '\u0080':
			case '\u0081':
			case '\u0082':
			case '\u0083':
			case '\u0084':
			case '\u0085':
			case '\u0086':
			case '\u0087':
			case '\u0088':
			case '\u0089':
			case '\u008a':
			case '\u008b':
			case '\u008c':
			case '\u008d':
			case '\u008e':
			case '\u008f':
			case '\u0090':
			case '\u0091':
			case '\u0092':
			case '\u0093':
			case '\u0094':
			case '\u0095':
			case '\u0096':
			case '\u0097':
			case '\u0098':
			case '\u0099':
			case '\u009a':
			case '\u009b':
			case '\u009c':
			case '\u009d':
			case '\u009e':
			case '\u009f':
			case '\u00a0':
			case '¡':
			case '¤':
			case '§':
			case '\u00a8':
			case '©':
			case 'ª':
			case '«':
			case '\u00ad':
			case '®':
				break;
			case ']':
				return '］';
			case '^':
				return '\uff3e';
			case '_':
				return '\uff3f';
			case '`':
				return '\uff40';
			case 'a':
				return 'ａ';
			case 'b':
				return 'ｂ';
			case 'c':
				return 'ｃ';
			case 'd':
				return 'ｄ';
			case 'e':
				return 'ｅ';
			case 'f':
				return 'ｆ';
			case 'g':
				return 'ｇ';
			case 'h':
				return 'ｈ';
			case 'i':
				return 'ｉ';
			case 'j':
				return 'ｊ';
			case 'k':
				return 'ｋ';
			case 'l':
				return 'ｌ';
			case 'm':
				return 'ｍ';
			case 'n':
				return 'ｎ';
			case 'o':
				return 'ｏ';
			case 'p':
				return 'ｐ';
			case 'q':
				return 'ｑ';
			case 'r':
				return 'ｒ';
			case 's':
				return 'ｓ';
			case 't':
				return 'ｔ';
			case 'u':
				return 'ｕ';
			case 'v':
				return 'ｖ';
			case 'w':
				return 'ｗ';
			case 'x':
				return 'ｘ';
			case 'y':
				return 'ｙ';
			case 'z':
				return 'ｚ';
			case '{':
				return '｛';
			case '|':
				return '｜';
			case '}':
				return '｝';
			case '~':
				return '～';
			case '¢':
				return '￠';
			case '£':
				return '￡';
			case '¥':
				return '￥';
			case '¦':
				return '￤';
			case '¬':
				return '￢';
			case '\u00af':
				return '\uffe3';
			default:
				if (c == '₩')
				{
					return '￦';
				}
				switch (c)
				{
				case '｡':
					return '。';
				case '｢':
					return '「';
				case '｣':
					return '」';
				case '､':
					return '、';
				case '･':
					return '・';
				case 'ｦ':
					return 'ヲ';
				case 'ｧ':
					return 'ァ';
				case 'ｨ':
					return 'ィ';
				case 'ｩ':
					return 'ゥ';
				case 'ｪ':
					return 'ェ';
				case 'ｫ':
					return 'ォ';
				case 'ｬ':
					return 'ャ';
				case 'ｭ':
					return 'ュ';
				case 'ｮ':
					return 'ョ';
				case 'ｯ':
					return 'ッ';
				case 'ｰ':
					return 'ー';
				case 'ｱ':
					return 'ア';
				case 'ｲ':
					return 'イ';
				case 'ｳ':
					return 'ウ';
				case 'ｴ':
					return 'エ';
				case 'ｵ':
					return 'オ';
				case 'ｶ':
					return 'カ';
				case 'ｷ':
					return 'キ';
				case 'ｸ':
					return 'ク';
				case 'ｹ':
					return 'ケ';
				case 'ｺ':
					return 'コ';
				case 'ｻ':
					return 'サ';
				case 'ｼ':
					return 'シ';
				case 'ｽ':
					return 'ス';
				case 'ｾ':
					return 'セ';
				case 'ｿ':
					return 'ソ';
				case 'ﾀ':
					return 'タ';
				case 'ﾁ':
					return 'チ';
				case 'ﾂ':
					return 'ツ';
				case 'ﾃ':
					return 'テ';
				case 'ﾄ':
					return 'ト';
				case 'ﾅ':
					return 'ナ';
				case 'ﾆ':
					return 'ニ';
				case 'ﾇ':
					return 'ヌ';
				case 'ﾈ':
					return 'ネ';
				case 'ﾉ':
					return 'ノ';
				case 'ﾊ':
					return 'ハ';
				case 'ﾋ':
					return 'ヒ';
				case 'ﾌ':
					return 'フ';
				case 'ﾍ':
					return 'ヘ';
				case 'ﾎ':
					return 'ホ';
				case 'ﾏ':
					return 'マ';
				case 'ﾐ':
					return 'ミ';
				case 'ﾑ':
					return 'ム';
				case 'ﾒ':
					return 'メ';
				case 'ﾓ':
					return 'モ';
				case 'ﾔ':
					return 'ヤ';
				case 'ﾕ':
					return 'ユ';
				case 'ﾖ':
					return 'ヨ';
				case 'ﾗ':
					return 'ラ';
				case 'ﾘ':
					return 'リ';
				case 'ﾙ':
					return 'ル';
				case 'ﾚ':
					return 'レ';
				case 'ﾛ':
					return 'ロ';
				case 'ﾜ':
					return 'ワ';
				case 'ﾝ':
					return 'ン';
				case 'ﾞ':
					return '\u309b';
				case 'ﾟ':
					return '\u309c';
				case 'ﾠ':
					return 'ㅤ';
				case 'ﾡ':
					return 'ㄱ';
				case 'ﾢ':
					return 'ㄲ';
				case 'ﾣ':
					return 'ㄳ';
				case 'ﾤ':
					return 'ㄴ';
				case 'ﾥ':
					return 'ㄵ';
				case 'ﾦ':
					return 'ㄶ';
				case 'ﾧ':
					return 'ㄷ';
				case 'ﾨ':
					return 'ㄸ';
				case 'ﾩ':
					return 'ㄹ';
				case 'ﾪ':
					return 'ㄺ';
				case 'ﾫ':
					return 'ㄻ';
				case 'ﾬ':
					return 'ㄼ';
				case 'ﾭ':
					return 'ㄽ';
				case 'ﾮ':
					return 'ㄾ';
				case 'ﾯ':
					return 'ㄿ';
				case 'ﾰ':
					return 'ㅀ';
				case 'ﾱ':
					return 'ㅁ';
				case 'ﾲ':
					return 'ㅂ';
				case 'ﾳ':
					return 'ㅃ';
				case 'ﾴ':
					return 'ㅄ';
				case 'ﾵ':
					return 'ㅅ';
				case 'ﾶ':
					return 'ㅆ';
				case 'ﾷ':
					return 'ㅇ';
				case 'ﾸ':
					return 'ㅈ';
				case 'ﾹ':
					return 'ㅉ';
				case 'ﾺ':
					return 'ㅊ';
				case 'ﾻ':
					return 'ㅋ';
				case 'ﾼ':
					return 'ㅌ';
				case 'ﾽ':
					return 'ㅍ';
				case 'ﾾ':
					return 'ㅎ';
				case 'ￂ':
					return 'ㅏ';
				case 'ￃ':
					return 'ㅐ';
				case 'ￄ':
					return 'ㅑ';
				case 'ￅ':
					return 'ㅒ';
				case 'ￆ':
					return 'ㅓ';
				case 'ￇ':
					return 'ㅔ';
				case 'ￊ':
					return 'ㅕ';
				case 'ￋ':
					return 'ㅖ';
				case 'ￌ':
					return 'ㅗ';
				case 'ￍ':
					return 'ㅘ';
				case 'ￎ':
					return 'ㅙ';
				case 'ￏ':
					return 'ㅚ';
				case 'ￒ':
					return 'ㅛ';
				case 'ￓ':
					return 'ㅜ';
				case 'ￔ':
					return 'ㅝ';
				case 'ￕ':
					return 'ㅞ';
				case 'ￖ':
					return 'ㅟ';
				case 'ￗ':
					return 'ㅠ';
				case 'ￚ':
					return 'ㅡ';
				case 'ￛ':
					return 'ㅢ';
				case 'ￜ':
					return 'ㅣ';
				}
				break;
			}
			return c;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x0000C794 File Offset: 0x0000A994
		internal static int ToHalfWidth(char c, char[] outputBuffer, int outputStartIndex)
		{
			if (outputBuffer == null)
			{
				switch (c)
				{
				case 'ガ':
					return 2;
				case 'キ':
				case 'ク':
				case 'ケ':
				case 'コ':
				case 'サ':
				case 'シ':
				case 'ス':
				case 'セ':
				case 'ソ':
				case 'タ':
				case 'チ':
					break;
				case 'ギ':
					return 2;
				case 'グ':
					return 2;
				case 'ゲ':
					return 2;
				case 'ゴ':
					return 2;
				case 'ザ':
					return 2;
				case 'ジ':
					return 2;
				case 'ズ':
					return 2;
				case 'ゼ':
					return 2;
				case 'ゾ':
					return 2;
				case 'ダ':
					return 2;
				case 'ヂ':
					return 2;
				default:
					switch (c)
					{
					case 'ヅ':
						return 2;
					case 'テ':
					case 'ト':
					case 'ナ':
					case 'ニ':
					case 'ヌ':
					case 'ネ':
					case 'ノ':
					case 'ハ':
					case 'ヒ':
					case 'フ':
					case 'ヘ':
					case 'ホ':
						break;
					case 'デ':
						return 2;
					case 'ド':
						return 2;
					case 'バ':
						return 2;
					case 'パ':
						return 2;
					case 'ビ':
						return 2;
					case 'ピ':
						return 2;
					case 'ブ':
						return 2;
					case 'プ':
						return 2;
					case 'ベ':
						return 2;
					case 'ペ':
						return 2;
					case 'ボ':
						return 2;
					case 'ポ':
						return 2;
					default:
						if (c == 'ヴ')
						{
							return 2;
						}
						break;
					}
					break;
				}
				return 1;
			}
			int num;
			if (c <= '」')
			{
				if (c <= '。')
				{
					switch (c)
					{
					case '‘':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '\'';
						goto IL_171C;
					case '’':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '\'';
						goto IL_171C;
					case '‚':
					case '‛':
						break;
					case '“':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '"';
						goto IL_171C;
					case '”':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '"';
						goto IL_171C;
					default:
						switch (c)
						{
						case '\u3000':
							num = outputStartIndex + 1;
							outputBuffer[outputStartIndex] = ' ';
							goto IL_171C;
						case '、':
							num = outputStartIndex + 1;
							outputBuffer[outputStartIndex] = '､';
							goto IL_171C;
						case '。':
							num = outputStartIndex + 1;
							outputBuffer[outputStartIndex] = '｡';
							goto IL_171C;
						}
						break;
					}
				}
				else
				{
					if (c == '「')
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '｢';
						goto IL_171C;
					}
					if (c == '」')
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '｣';
						goto IL_171C;
					}
				}
			}
			else if (c <= '～')
			{
				switch (c)
				{
				case '\u3099':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾞ';
					goto IL_171C;
				case '\u309a':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾟ';
					goto IL_171C;
				case '\u309b':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾞ';
					goto IL_171C;
				case '\u309c':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾟ';
					goto IL_171C;
				case 'ゝ':
				case 'ゞ':
				case 'ゟ':
				case '゠':
				case 'ヮ':
				case 'ヰ':
				case 'ヱ':
				case 'ヵ':
				case 'ヶ':
				case 'ヷ':
				case 'ヸ':
				case 'ヹ':
				case 'ヺ':
				case 'ヽ':
				case 'ヾ':
				case 'ヿ':
				case '\u3100':
				case '\u3101':
				case '\u3102':
				case '\u3103':
				case '\u3104':
				case 'ㄅ':
				case 'ㄆ':
				case 'ㄇ':
				case 'ㄈ':
				case 'ㄉ':
				case 'ㄊ':
				case 'ㄋ':
				case 'ㄌ':
				case 'ㄍ':
				case 'ㄎ':
				case 'ㄏ':
				case 'ㄐ':
				case 'ㄑ':
				case 'ㄒ':
				case 'ㄓ':
				case 'ㄔ':
				case 'ㄕ':
				case 'ㄖ':
				case 'ㄗ':
				case 'ㄘ':
				case 'ㄙ':
				case 'ㄚ':
				case 'ㄛ':
				case 'ㄜ':
				case 'ㄝ':
				case 'ㄞ':
				case 'ㄟ':
				case 'ㄠ':
				case 'ㄡ':
				case 'ㄢ':
				case 'ㄣ':
				case 'ㄤ':
				case 'ㄥ':
				case 'ㄦ':
				case 'ㄧ':
				case 'ㄨ':
				case 'ㄩ':
				case 'ㄪ':
				case 'ㄫ':
				case 'ㄬ':
				case 'ㄭ':
				case 'ㄮ':
				case 'ㄯ':
				case '\u3130':
					break;
				case 'ァ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ｧ';
					goto IL_171C;
				case 'ア':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ｱ';
					goto IL_171C;
				case 'ィ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ｨ';
					goto IL_171C;
				case 'イ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ｲ';
					goto IL_171C;
				case 'ゥ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ｩ';
					goto IL_171C;
				case 'ウ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ｳ';
					goto IL_171C;
				case 'ェ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ｪ';
					goto IL_171C;
				case 'エ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ｴ';
					goto IL_171C;
				case 'ォ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ｫ';
					goto IL_171C;
				case 'オ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ｵ';
					goto IL_171C;
				case 'カ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ｶ';
					goto IL_171C;
				case 'ガ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ｶ';
					outputBuffer[num++] = 'ﾞ';
					goto IL_171C;
				case 'キ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ｷ';
					goto IL_171C;
				case 'ギ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ｷ';
					outputBuffer[num++] = 'ﾞ';
					goto IL_171C;
				case 'ク':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ｸ';
					goto IL_171C;
				case 'グ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ｸ';
					outputBuffer[num++] = 'ﾞ';
					goto IL_171C;
				case 'ケ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ｹ';
					goto IL_171C;
				case 'ゲ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ｹ';
					outputBuffer[num++] = 'ﾞ';
					goto IL_171C;
				case 'コ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ｺ';
					goto IL_171C;
				case 'ゴ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ｺ';
					outputBuffer[num++] = 'ﾞ';
					goto IL_171C;
				case 'サ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ｻ';
					goto IL_171C;
				case 'ザ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ｻ';
					outputBuffer[num++] = 'ﾞ';
					goto IL_171C;
				case 'シ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ｼ';
					goto IL_171C;
				case 'ジ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ｼ';
					outputBuffer[num++] = 'ﾞ';
					goto IL_171C;
				case 'ス':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ｽ';
					goto IL_171C;
				case 'ズ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ｽ';
					outputBuffer[num++] = 'ﾞ';
					goto IL_171C;
				case 'セ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ｾ';
					goto IL_171C;
				case 'ゼ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ｾ';
					outputBuffer[num++] = 'ﾞ';
					goto IL_171C;
				case 'ソ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ｿ';
					goto IL_171C;
				case 'ゾ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ｿ';
					outputBuffer[num++] = 'ﾞ';
					goto IL_171C;
				case 'タ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾀ';
					goto IL_171C;
				case 'ダ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾀ';
					outputBuffer[num++] = 'ﾞ';
					goto IL_171C;
				case 'チ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾁ';
					goto IL_171C;
				case 'ヂ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾁ';
					outputBuffer[num++] = 'ﾞ';
					goto IL_171C;
				case 'ッ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ｯ';
					goto IL_171C;
				case 'ツ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾂ';
					goto IL_171C;
				case 'ヅ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾂ';
					outputBuffer[num++] = 'ﾞ';
					goto IL_171C;
				case 'テ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾃ';
					goto IL_171C;
				case 'デ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾃ';
					outputBuffer[num++] = 'ﾞ';
					goto IL_171C;
				case 'ト':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾄ';
					goto IL_171C;
				case 'ド':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾄ';
					outputBuffer[num++] = 'ﾞ';
					goto IL_171C;
				case 'ナ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾅ';
					goto IL_171C;
				case 'ニ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾆ';
					goto IL_171C;
				case 'ヌ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾇ';
					goto IL_171C;
				case 'ネ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾈ';
					goto IL_171C;
				case 'ノ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾉ';
					goto IL_171C;
				case 'ハ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾊ';
					goto IL_171C;
				case 'バ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾊ';
					outputBuffer[num++] = 'ﾞ';
					goto IL_171C;
				case 'パ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾊ';
					outputBuffer[num++] = 'ﾟ';
					goto IL_171C;
				case 'ヒ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾋ';
					goto IL_171C;
				case 'ビ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾋ';
					outputBuffer[num++] = 'ﾞ';
					goto IL_171C;
				case 'ピ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾋ';
					outputBuffer[num++] = 'ﾟ';
					goto IL_171C;
				case 'フ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾌ';
					goto IL_171C;
				case 'ブ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾌ';
					outputBuffer[num++] = 'ﾞ';
					goto IL_171C;
				case 'プ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾌ';
					outputBuffer[num++] = 'ﾟ';
					goto IL_171C;
				case 'ヘ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾍ';
					goto IL_171C;
				case 'ベ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾍ';
					outputBuffer[num++] = 'ﾞ';
					goto IL_171C;
				case 'ペ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾍ';
					outputBuffer[num++] = 'ﾟ';
					goto IL_171C;
				case 'ホ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾎ';
					goto IL_171C;
				case 'ボ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾎ';
					outputBuffer[num++] = 'ﾞ';
					goto IL_171C;
				case 'ポ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾎ';
					outputBuffer[num++] = 'ﾟ';
					goto IL_171C;
				case 'マ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾏ';
					goto IL_171C;
				case 'ミ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾐ';
					goto IL_171C;
				case 'ム':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾑ';
					goto IL_171C;
				case 'メ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾒ';
					goto IL_171C;
				case 'モ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾓ';
					goto IL_171C;
				case 'ャ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ｬ';
					goto IL_171C;
				case 'ヤ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾔ';
					goto IL_171C;
				case 'ュ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ｭ';
					goto IL_171C;
				case 'ユ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾕ';
					goto IL_171C;
				case 'ョ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ｮ';
					goto IL_171C;
				case 'ヨ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾖ';
					goto IL_171C;
				case 'ラ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾗ';
					goto IL_171C;
				case 'リ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾘ';
					goto IL_171C;
				case 'ル':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾙ';
					goto IL_171C;
				case 'レ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾚ';
					goto IL_171C;
				case 'ロ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾛ';
					goto IL_171C;
				case 'ワ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾜ';
					goto IL_171C;
				case 'ヲ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ｦ';
					goto IL_171C;
				case 'ン':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾝ';
					goto IL_171C;
				case 'ヴ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ｳ';
					outputBuffer[num++] = 'ﾞ';
					goto IL_171C;
				case '・':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = '･';
					goto IL_171C;
				case 'ー':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ｰ';
					goto IL_171C;
				case 'ㄱ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾡ';
					goto IL_171C;
				case 'ㄲ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾢ';
					goto IL_171C;
				case 'ㄳ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾣ';
					goto IL_171C;
				case 'ㄴ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾤ';
					goto IL_171C;
				case 'ㄵ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾥ';
					goto IL_171C;
				case 'ㄶ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾦ';
					goto IL_171C;
				case 'ㄷ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾧ';
					goto IL_171C;
				case 'ㄸ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾨ';
					goto IL_171C;
				case 'ㄹ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾩ';
					goto IL_171C;
				case 'ㄺ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾪ';
					goto IL_171C;
				case 'ㄻ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾫ';
					goto IL_171C;
				case 'ㄼ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾬ';
					goto IL_171C;
				case 'ㄽ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾭ';
					goto IL_171C;
				case 'ㄾ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾮ';
					goto IL_171C;
				case 'ㄿ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾯ';
					goto IL_171C;
				case 'ㅀ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾰ';
					goto IL_171C;
				case 'ㅁ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾱ';
					goto IL_171C;
				case 'ㅂ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾲ';
					goto IL_171C;
				case 'ㅃ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾳ';
					goto IL_171C;
				case 'ㅄ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾴ';
					goto IL_171C;
				case 'ㅅ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾵ';
					goto IL_171C;
				case 'ㅆ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾶ';
					goto IL_171C;
				case 'ㅇ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾷ';
					goto IL_171C;
				case 'ㅈ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾸ';
					goto IL_171C;
				case 'ㅉ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾹ';
					goto IL_171C;
				case 'ㅊ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾺ';
					goto IL_171C;
				case 'ㅋ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾻ';
					goto IL_171C;
				case 'ㅌ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾼ';
					goto IL_171C;
				case 'ㅍ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾽ';
					goto IL_171C;
				case 'ㅎ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾾ';
					goto IL_171C;
				case 'ㅏ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ￂ';
					goto IL_171C;
				case 'ㅐ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ￃ';
					goto IL_171C;
				case 'ㅑ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ￄ';
					goto IL_171C;
				case 'ㅒ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ￅ';
					goto IL_171C;
				case 'ㅓ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ￆ';
					goto IL_171C;
				case 'ㅔ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ￇ';
					goto IL_171C;
				case 'ㅕ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ￊ';
					goto IL_171C;
				case 'ㅖ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ￋ';
					goto IL_171C;
				case 'ㅗ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ￌ';
					goto IL_171C;
				case 'ㅘ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ￍ';
					goto IL_171C;
				case 'ㅙ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ￎ';
					goto IL_171C;
				case 'ㅚ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ￏ';
					goto IL_171C;
				case 'ㅛ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ￒ';
					goto IL_171C;
				case 'ㅜ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ￓ';
					goto IL_171C;
				case 'ㅝ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ￔ';
					goto IL_171C;
				case 'ㅞ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ￕ';
					goto IL_171C;
				case 'ㅟ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ￖ';
					goto IL_171C;
				case 'ㅠ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ￗ';
					goto IL_171C;
				case 'ㅡ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ￚ';
					goto IL_171C;
				case 'ㅢ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ￛ';
					goto IL_171C;
				case 'ㅣ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ￜ';
					goto IL_171C;
				case 'ㅤ':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = 'ﾠ';
					goto IL_171C;
				default:
					switch (c)
					{
					case '！':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '!';
						goto IL_171C;
					case '＂':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '"';
						goto IL_171C;
					case '＃':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '#';
						goto IL_171C;
					case '＄':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '$';
						goto IL_171C;
					case '％':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '%';
						goto IL_171C;
					case '＆':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '&';
						goto IL_171C;
					case '＇':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '\'';
						goto IL_171C;
					case '（':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '(';
						goto IL_171C;
					case '）':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = ')';
						goto IL_171C;
					case '＊':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '*';
						goto IL_171C;
					case '＋':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '+';
						goto IL_171C;
					case '，':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = ',';
						goto IL_171C;
					case '－':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '-';
						goto IL_171C;
					case '．':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '.';
						goto IL_171C;
					case '／':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '/';
						goto IL_171C;
					case '０':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '0';
						goto IL_171C;
					case '１':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '1';
						goto IL_171C;
					case '２':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '2';
						goto IL_171C;
					case '３':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '3';
						goto IL_171C;
					case '４':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '4';
						goto IL_171C;
					case '５':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '5';
						goto IL_171C;
					case '６':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '6';
						goto IL_171C;
					case '７':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '7';
						goto IL_171C;
					case '８':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '8';
						goto IL_171C;
					case '９':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '9';
						goto IL_171C;
					case '：':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = ':';
						goto IL_171C;
					case '；':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = ';';
						goto IL_171C;
					case '＜':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '<';
						goto IL_171C;
					case '＝':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '=';
						goto IL_171C;
					case '＞':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '>';
						goto IL_171C;
					case '？':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '?';
						goto IL_171C;
					case '＠':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '@';
						goto IL_171C;
					case 'Ａ':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'A';
						goto IL_171C;
					case 'Ｂ':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'B';
						goto IL_171C;
					case 'Ｃ':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'C';
						goto IL_171C;
					case 'Ｄ':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'D';
						goto IL_171C;
					case 'Ｅ':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'E';
						goto IL_171C;
					case 'Ｆ':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'F';
						goto IL_171C;
					case 'Ｇ':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'G';
						goto IL_171C;
					case 'Ｈ':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'H';
						goto IL_171C;
					case 'Ｉ':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'I';
						goto IL_171C;
					case 'Ｊ':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'J';
						goto IL_171C;
					case 'Ｋ':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'K';
						goto IL_171C;
					case 'Ｌ':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'L';
						goto IL_171C;
					case 'Ｍ':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'M';
						goto IL_171C;
					case 'Ｎ':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'N';
						goto IL_171C;
					case 'Ｏ':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'O';
						goto IL_171C;
					case 'Ｐ':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'P';
						goto IL_171C;
					case 'Ｑ':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'Q';
						goto IL_171C;
					case 'Ｒ':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'R';
						goto IL_171C;
					case 'Ｓ':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'S';
						goto IL_171C;
					case 'Ｔ':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'T';
						goto IL_171C;
					case 'Ｕ':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'U';
						goto IL_171C;
					case 'Ｖ':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'V';
						goto IL_171C;
					case 'Ｗ':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'W';
						goto IL_171C;
					case 'Ｘ':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'X';
						goto IL_171C;
					case 'Ｙ':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'Y';
						goto IL_171C;
					case 'Ｚ':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'Z';
						goto IL_171C;
					case '［':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '[';
						goto IL_171C;
					case '］':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = ']';
						goto IL_171C;
					case '\uff3e':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '^';
						goto IL_171C;
					case '\uff3f':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '_';
						goto IL_171C;
					case '\uff40':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '`';
						goto IL_171C;
					case 'ａ':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'a';
						goto IL_171C;
					case 'ｂ':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'b';
						goto IL_171C;
					case 'ｃ':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'c';
						goto IL_171C;
					case 'ｄ':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'd';
						goto IL_171C;
					case 'ｅ':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'e';
						goto IL_171C;
					case 'ｆ':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'f';
						goto IL_171C;
					case 'ｇ':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'g';
						goto IL_171C;
					case 'ｈ':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'h';
						goto IL_171C;
					case 'ｉ':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'i';
						goto IL_171C;
					case 'ｊ':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'j';
						goto IL_171C;
					case 'ｋ':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'k';
						goto IL_171C;
					case 'ｌ':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'l';
						goto IL_171C;
					case 'ｍ':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'm';
						goto IL_171C;
					case 'ｎ':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'n';
						goto IL_171C;
					case 'ｏ':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'o';
						goto IL_171C;
					case 'ｐ':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'p';
						goto IL_171C;
					case 'ｑ':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'q';
						goto IL_171C;
					case 'ｒ':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'r';
						goto IL_171C;
					case 'ｓ':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 's';
						goto IL_171C;
					case 'ｔ':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 't';
						goto IL_171C;
					case 'ｕ':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'u';
						goto IL_171C;
					case 'ｖ':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'v';
						goto IL_171C;
					case 'ｗ':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'w';
						goto IL_171C;
					case 'ｘ':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'x';
						goto IL_171C;
					case 'ｙ':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'y';
						goto IL_171C;
					case 'ｚ':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = 'z';
						goto IL_171C;
					case '｛':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '{';
						goto IL_171C;
					case '｜':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '|';
						goto IL_171C;
					case '｝':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '}';
						goto IL_171C;
					case '～':
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '~';
						goto IL_171C;
					}
					break;
				}
			}
			else
			{
				switch (c)
				{
				case '￠':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = '¢';
					goto IL_171C;
				case '￡':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = '£';
					goto IL_171C;
				case '￢':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = '¬';
					goto IL_171C;
				case '\uffe3':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = '\u00af';
					goto IL_171C;
				case '￤':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = '¦';
					goto IL_171C;
				case '￥':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = '¥';
					goto IL_171C;
				case '￦':
					num = outputStartIndex + 1;
					outputBuffer[outputStartIndex] = '₩';
					goto IL_171C;
				default:
					if (c == '\ufffe')
					{
						num = outputStartIndex + 1;
						outputBuffer[outputStartIndex] = '\ufffe';
						goto IL_171C;
					}
					break;
				}
			}
			num = outputStartIndex + 1;
			outputBuffer[outputStartIndex] = c;
			IL_171C:
			return num - outputStartIndex;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x0000DEC0 File Offset: 0x0000C0C0
		internal static char ToKatakana(char c)
		{
			switch (c)
			{
			case 'ぁ':
				return 'ァ';
			case 'あ':
				return 'ア';
			case 'ぃ':
				return 'ィ';
			case 'い':
				return 'イ';
			case 'ぅ':
				return 'ゥ';
			case 'う':
				return 'ウ';
			case 'ぇ':
				return 'ェ';
			case 'え':
				return 'エ';
			case 'ぉ':
				return 'ォ';
			case 'お':
				return 'オ';
			case 'か':
				return 'カ';
			case 'が':
				return 'ガ';
			case 'き':
				return 'キ';
			case 'ぎ':
				return 'ギ';
			case 'く':
				return 'ク';
			case 'ぐ':
				return 'グ';
			case 'け':
				return 'ケ';
			case 'げ':
				return 'ゲ';
			case 'こ':
				return 'コ';
			case 'ご':
				return 'ゴ';
			case 'さ':
				return 'サ';
			case 'ざ':
				return 'ザ';
			case 'し':
				return 'シ';
			case 'じ':
				return 'ジ';
			case 'す':
				return 'ス';
			case 'ず':
				return 'ズ';
			case 'せ':
				return 'セ';
			case 'ぜ':
				return 'ゼ';
			case 'そ':
				return 'ソ';
			case 'ぞ':
				return 'ゾ';
			case 'た':
				return 'タ';
			case 'だ':
				return 'ダ';
			case 'ち':
				return 'チ';
			case 'ぢ':
				return 'ヂ';
			case 'っ':
				return 'ッ';
			case 'つ':
				return 'ツ';
			case 'づ':
				return 'ヅ';
			case 'て':
				return 'テ';
			case 'で':
				return 'デ';
			case 'と':
				return 'ト';
			case 'ど':
				return 'ド';
			case 'な':
				return 'ナ';
			case 'に':
				return 'ニ';
			case 'ぬ':
				return 'ヌ';
			case 'ね':
				return 'ネ';
			case 'の':
				return 'ノ';
			case 'は':
				return 'ハ';
			case 'ば':
				return 'バ';
			case 'ぱ':
				return 'パ';
			case 'ひ':
				return 'ヒ';
			case 'び':
				return 'ビ';
			case 'ぴ':
				return 'ピ';
			case 'ふ':
				return 'フ';
			case 'ぶ':
				return 'ブ';
			case 'ぷ':
				return 'プ';
			case 'へ':
				return 'ヘ';
			case 'べ':
				return 'ベ';
			case 'ぺ':
				return 'ペ';
			case 'ほ':
				return 'ホ';
			case 'ぼ':
				return 'ボ';
			case 'ぽ':
				return 'ポ';
			case 'ま':
				return 'マ';
			case 'み':
				return 'ミ';
			case 'む':
				return 'ム';
			case 'め':
				return 'メ';
			case 'も':
				return 'モ';
			case 'ゃ':
				return 'ャ';
			case 'や':
				return 'ヤ';
			case 'ゅ':
				return 'ュ';
			case 'ゆ':
				return 'ユ';
			case 'ょ':
				return 'ョ';
			case 'よ':
				return 'ヨ';
			case 'ら':
				return 'ラ';
			case 'り':
				return 'リ';
			case 'る':
				return 'ル';
			case 'れ':
				return 'レ';
			case 'ろ':
				return 'ロ';
			case 'ゎ':
				return 'ヮ';
			case 'わ':
				return 'ワ';
			case 'ゐ':
				return 'ヰ';
			case 'ゑ':
				return 'ヱ';
			case 'を':
				return 'ヲ';
			case 'ん':
				return 'ン';
			case 'ゔ':
				return 'ヴ';
			case 'ゝ':
				return 'ヽ';
			case 'ゞ':
				return 'ヾ';
			}
			return c;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x0000E25C File Offset: 0x0000C45C
		internal static char ToHiragana(char c)
		{
			switch (c)
			{
			case 'ァ':
				return 'ぁ';
			case 'ア':
				return 'あ';
			case 'ィ':
				return 'ぃ';
			case 'イ':
				return 'い';
			case 'ゥ':
				return 'ぅ';
			case 'ウ':
				return 'う';
			case 'ェ':
				return 'ぇ';
			case 'エ':
				return 'え';
			case 'ォ':
				return 'ぉ';
			case 'オ':
				return 'お';
			case 'カ':
				return 'か';
			case 'ガ':
				return 'が';
			case 'キ':
				return 'き';
			case 'ギ':
				return 'ぎ';
			case 'ク':
				return 'く';
			case 'グ':
				return 'ぐ';
			case 'ケ':
				return 'け';
			case 'ゲ':
				return 'げ';
			case 'コ':
				return 'こ';
			case 'ゴ':
				return 'ご';
			case 'サ':
				return 'さ';
			case 'ザ':
				return 'ざ';
			case 'シ':
				return 'し';
			case 'ジ':
				return 'じ';
			case 'ス':
				return 'す';
			case 'ズ':
				return 'ず';
			case 'セ':
				return 'せ';
			case 'ゼ':
				return 'ぜ';
			case 'ソ':
				return 'そ';
			case 'ゾ':
				return 'ぞ';
			case 'タ':
				return 'た';
			case 'ダ':
				return 'だ';
			case 'チ':
				return 'ち';
			case 'ヂ':
				return 'ぢ';
			case 'ッ':
				return 'っ';
			case 'ツ':
				return 'つ';
			case 'ヅ':
				return 'づ';
			case 'テ':
				return 'て';
			case 'デ':
				return 'で';
			case 'ト':
				return 'と';
			case 'ド':
				return 'ど';
			case 'ナ':
				return 'な';
			case 'ニ':
				return 'に';
			case 'ヌ':
				return 'ぬ';
			case 'ネ':
				return 'ね';
			case 'ノ':
				return 'の';
			case 'ハ':
				return 'は';
			case 'バ':
				return 'ば';
			case 'パ':
				return 'ぱ';
			case 'ヒ':
				return 'ひ';
			case 'ビ':
				return 'び';
			case 'ピ':
				return 'ぴ';
			case 'フ':
				return 'ふ';
			case 'ブ':
				return 'ぶ';
			case 'プ':
				return 'ぷ';
			case 'ヘ':
				return 'へ';
			case 'ベ':
				return 'べ';
			case 'ペ':
				return 'ぺ';
			case 'ホ':
				return 'ほ';
			case 'ボ':
				return 'ぼ';
			case 'ポ':
				return 'ぽ';
			case 'マ':
				return 'ま';
			case 'ミ':
				return 'み';
			case 'ム':
				return 'む';
			case 'メ':
				return 'め';
			case 'モ':
				return 'も';
			case 'ャ':
				return 'ゃ';
			case 'ヤ':
				return 'や';
			case 'ュ':
				return 'ゅ';
			case 'ユ':
				return 'ゆ';
			case 'ョ':
				return 'ょ';
			case 'ヨ':
				return 'よ';
			case 'ラ':
				return 'ら';
			case 'リ':
				return 'り';
			case 'ル':
				return 'る';
			case 'レ':
				return 'れ';
			case 'ロ':
				return 'ろ';
			case 'ヮ':
				return 'ゎ';
			case 'ワ':
				return 'わ';
			case 'ヰ':
				return 'ゐ';
			case 'ヱ':
				return 'ゑ';
			case 'ヲ':
				return 'を';
			case 'ン':
				return 'ん';
			case 'ヴ':
				return 'ゔ';
			case 'ヽ':
				return 'ゝ';
			case 'ヾ':
				return 'ゞ';
			}
			return c;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x0000E5F8 File Offset: 0x0000C7F8
		internal static char FoldDigit(char c)
		{
			if (c <= '๙')
			{
				if (c <= '৯')
				{
					if (c <= '¹')
					{
						if (c == '²')
						{
							return '2';
						}
						if (c == '³')
						{
							return '3';
						}
						if (c == '¹')
						{
							return '1';
						}
					}
					else if (c <= '۹')
					{
						switch (c)
						{
						case '٠':
							return '0';
						case '١':
							return '1';
						case '٢':
							return '2';
						case '٣':
							return '3';
						case '٤':
							return '4';
						case '٥':
							return '5';
						case '٦':
							return '6';
						case '٧':
							return '7';
						case '٨':
							return '8';
						case '٩':
							return '9';
						default:
							switch (c)
							{
							case '۰':
								return '0';
							case '۱':
								return '1';
							case '۲':
								return '2';
							case '۳':
								return '3';
							case '۴':
								return '4';
							case '۵':
								return '5';
							case '۶':
								return '6';
							case '۷':
								return '7';
							case '۸':
								return '8';
							case '۹':
								return '9';
							}
							break;
						}
					}
					else
					{
						switch (c)
						{
						case '०':
							return '0';
						case '१':
							return '1';
						case '२':
							return '2';
						case '३':
							return '3';
						case '४':
							return '4';
						case '५':
							return '5';
						case '६':
							return '6';
						case '७':
							return '7';
						case '८':
							return '8';
						case '९':
							return '9';
						default:
							switch (c)
							{
							case '০':
								return '0';
							case '১':
								return '1';
							case '২':
								return '2';
							case '৩':
								return '3';
							case '৪':
								return '4';
							case '৫':
								return '5';
							case '৬':
								return '6';
							case '৭':
								return '7';
							case '৮':
								return '8';
							case '৯':
								return '9';
							}
							break;
						}
					}
				}
				else if (c <= '௯')
				{
					if (c <= '૯')
					{
						switch (c)
						{
						case '੦':
							return '0';
						case '੧':
							return '1';
						case '੨':
							return '2';
						case '੩':
							return '3';
						case '੪':
							return '4';
						case '੫':
							return '5';
						case '੬':
							return '6';
						case '੭':
							return '7';
						case '੮':
							return '8';
						case '੯':
							return '9';
						default:
							switch (c)
							{
							case '૦':
								return '0';
							case '૧':
								return '1';
							case '૨':
								return '2';
							case '૩':
								return '3';
							case '૪':
								return '4';
							case '૫':
								return '5';
							case '૬':
								return '6';
							case '૭':
								return '7';
							case '૮':
								return '8';
							case '૯':
								return '9';
							}
							break;
						}
					}
					else
					{
						switch (c)
						{
						case '୦':
							return '0';
						case '୧':
							return '1';
						case '୨':
							return '2';
						case '୩':
							return '3';
						case '୪':
							return '4';
						case '୫':
							return '5';
						case '୬':
							return '6';
						case '୭':
							return '7';
						case '୮':
							return '8';
						case '୯':
							return '9';
						default:
							switch (c)
							{
							case '௧':
								return '1';
							case '௨':
								return '2';
							case '௩':
								return '3';
							case '௪':
								return '4';
							case '௫':
								return '5';
							case '௬':
								return '6';
							case '௭':
								return '7';
							case '௮':
								return '8';
							case '௯':
								return '9';
							}
							break;
						}
					}
				}
				else if (c <= '೯')
				{
					switch (c)
					{
					case '౦':
						return '0';
					case '౧':
						return '1';
					case '౨':
						return '2';
					case '౩':
						return '3';
					case '౪':
						return '4';
					case '౫':
						return '5';
					case '౬':
						return '6';
					case '౭':
						return '7';
					case '౮':
						return '8';
					case '౯':
						return '9';
					default:
						switch (c)
						{
						case '೦':
							return '0';
						case '೧':
							return '1';
						case '೨':
							return '2';
						case '೩':
							return '3';
						case '೪':
							return '4';
						case '೫':
							return '5';
						case '೬':
							return '6';
						case '೭':
							return '7';
						case '೮':
							return '8';
						case '೯':
							return '9';
						}
						break;
					}
				}
				else
				{
					switch (c)
					{
					case '൦':
						return '0';
					case '൧':
						return '1';
					case '൨':
						return '2';
					case '൩':
						return '3';
					case '൪':
						return '4';
					case '൫':
						return '5';
					case '൬':
						return '6';
					case '൭':
						return '7';
					case '൮':
						return '8';
					case '൯':
						return '9';
					default:
						switch (c)
						{
						case '๐':
							return '0';
						case '๑':
							return '1';
						case '๒':
							return '2';
						case '๓':
							return '3';
						case '๔':
							return '4';
						case '๕':
							return '5';
						case '๖':
							return '6';
						case '๗':
							return '7';
						case '๘':
							return '8';
						case '๙':
							return '9';
						}
						break;
					}
				}
			}
			else if (c <= '᭙')
			{
				if (c <= '፱')
				{
					if (c <= '༳')
					{
						switch (c)
						{
						case '໐':
							return '0';
						case '໑':
							return '1';
						case '໒':
							return '2';
						case '໓':
							return '3';
						case '໔':
							return '4';
						case '໕':
							return '5';
						case '໖':
							return '6';
						case '໗':
							return '7';
						case '໘':
							return '8';
						case '໙':
							return '9';
						default:
							switch (c)
							{
							case '༠':
								return '0';
							case '༡':
								return '1';
							case '༢':
								return '2';
							case '༣':
								return '3';
							case '༤':
								return '4';
							case '༥':
								return '5';
							case '༦':
								return '6';
							case '༧':
								return '7';
							case '༨':
								return '8';
							case '༩':
								return '9';
							case '༪':
								return '1';
							case '༫':
								return '2';
							case '༬':
								return '3';
							case '༭':
								return '4';
							case '༮':
								return '5';
							case '༯':
								return '6';
							case '༰':
								return '7';
							case '༱':
								return '8';
							case '༲':
								return '9';
							case '༳':
								return '0';
							}
							break;
						}
					}
					else
					{
						switch (c)
						{
						case '၀':
							return '0';
						case '၁':
							return '1';
						case '၂':
							return '2';
						case '၃':
							return '3';
						case '၄':
							return '4';
						case '၅':
							return '5';
						case '၆':
							return '6';
						case '၇':
							return '7';
						case '၈':
							return '8';
						case '၉':
							return '9';
						default:
							switch (c)
							{
							case '፩':
								return '1';
							case '፪':
								return '2';
							case '፫':
								return '3';
							case '፬':
								return '4';
							case '፭':
								return '5';
							case '፮':
								return '6';
							case '፯':
								return '7';
							case '፰':
								return '8';
							case '፱':
								return '9';
							}
							break;
						}
					}
				}
				else if (c <= '᠙')
				{
					switch (c)
					{
					case '០':
						return '0';
					case '១':
						return '1';
					case '២':
						return '2';
					case '៣':
						return '3';
					case '៤':
						return '4';
					case '៥':
						return '5';
					case '៦':
						return '6';
					case '៧':
						return '7';
					case '៨':
						return '8';
					case '៩':
						return '9';
					default:
						switch (c)
						{
						case '᠐':
							return '0';
						case '᠑':
							return '1';
						case '᠒':
							return '2';
						case '᠓':
							return '3';
						case '᠔':
							return '4';
						case '᠕':
							return '5';
						case '᠖':
							return '6';
						case '᠗':
							return '7';
						case '᠘':
							return '8';
						case '᠙':
							return '9';
						}
						break;
					}
				}
				else
				{
					switch (c)
					{
					case '᥆':
						return '0';
					case '᥇':
						return '1';
					case '᥈':
						return '2';
					case '᥉':
						return '3';
					case '᥊':
						return '4';
					case '᥋':
						return '5';
					case '᥌':
						return '6';
					case '᥍':
						return '7';
					case '᥎':
						return '8';
					case '᥏':
						return '9';
					default:
						switch (c)
						{
						case '᭐':
							return '0';
						case '᭑':
							return '1';
						case '᭒':
							return '2';
						case '᭓':
							return '3';
						case '᭔':
							return '4';
						case '᭕':
							return '5';
						case '᭖':
							return '6';
						case '᭗':
							return '7';
						case '᭘':
							return '8';
						case '᭙':
							return '9';
						}
						break;
					}
				}
			}
			else if (c <= '⓿')
			{
				if (c <= '⒐')
				{
					switch (c)
					{
					case '⁰':
						return '0';
					case 'ⁱ':
					case '\u2072':
					case '\u2073':
					case '⁺':
					case '⁻':
					case '⁼':
					case '⁽':
					case '⁾':
					case 'ⁿ':
						break;
					case '⁴':
						return '4';
					case '⁵':
						return '5';
					case '⁶':
						return '6';
					case '⁷':
						return '7';
					case '⁸':
						return '8';
					case '⁹':
						return '9';
					case '₀':
						return '0';
					case '₁':
						return '1';
					case '₂':
						return '2';
					case '₃':
						return '3';
					case '₄':
						return '4';
					case '₅':
						return '5';
					case '₆':
						return '6';
					case '₇':
						return '7';
					case '₈':
						return '8';
					case '₉':
						return '9';
					default:
						switch (c)
						{
						case '①':
							return '1';
						case '②':
							return '2';
						case '③':
							return '3';
						case '④':
							return '4';
						case '⑤':
							return '5';
						case '⑥':
							return '6';
						case '⑦':
							return '7';
						case '⑧':
							return '8';
						case '⑨':
							return '9';
						case '⑴':
							return '1';
						case '⑵':
							return '2';
						case '⑶':
							return '3';
						case '⑷':
							return '4';
						case '⑸':
							return '5';
						case '⑹':
							return '6';
						case '⑺':
							return '7';
						case '⑻':
							return '8';
						case '⑼':
							return '9';
						case '⒈':
							return '1';
						case '⒉':
							return '2';
						case '⒊':
							return '3';
						case '⒋':
							return '4';
						case '⒌':
							return '5';
						case '⒍':
							return '6';
						case '⒎':
							return '7';
						case '⒏':
							return '8';
						case '⒐':
							return '9';
						}
						break;
					}
				}
				else
				{
					if (c == '⓪')
					{
						return '0';
					}
					switch (c)
					{
					case '⓵':
						return '1';
					case '⓶':
						return '2';
					case '⓷':
						return '3';
					case '⓸':
						return '4';
					case '⓹':
						return '5';
					case '⓺':
						return '6';
					case '⓻':
						return '7';
					case '⓼':
						return '8';
					case '⓽':
						return '9';
					case '⓿':
						return '0';
					}
				}
			}
			else if (c <= '〇')
			{
				switch (c)
				{
				case '❶':
					return '1';
				case '❷':
					return '2';
				case '❸':
					return '3';
				case '❹':
					return '4';
				case '❺':
					return '5';
				case '❻':
					return '6';
				case '❼':
					return '7';
				case '❽':
					return '8';
				case '❾':
					return '9';
				case '❿':
				case '➉':
					break;
				case '➀':
					return '1';
				case '➁':
					return '2';
				case '➂':
					return '3';
				case '➃':
					return '4';
				case '➄':
					return '5';
				case '➅':
					return '6';
				case '➆':
					return '7';
				case '➇':
					return '8';
				case '➈':
					return '9';
				case '➊':
					return '1';
				case '➋':
					return '2';
				case '➌':
					return '3';
				case '➍':
					return '4';
				case '➎':
					return '5';
				case '➏':
					return '6';
				case '➐':
					return '7';
				case '➑':
					return '8';
				case '➒':
					return '9';
				default:
					if (c == '〇')
					{
						return '0';
					}
					break;
				}
			}
			else
			{
				switch (c)
				{
				case '〡':
					return '1';
				case '〢':
					return '2';
				case '〣':
					return '3';
				case '〤':
					return '4';
				case '〥':
					return '5';
				case '〦':
					return '6';
				case '〧':
					return '7';
				case '〨':
					return '8';
				case '〩':
					return '9';
				default:
					switch (c)
					{
					case '０':
						return '0';
					case '１':
						return '1';
					case '２':
						return '2';
					case '３':
						return '3';
					case '４':
						return '4';
					case '５':
						return '5';
					case '６':
						return '6';
					case '７':
						return '7';
					case '８':
						return '8';
					case '９':
						return '9';
					}
					break;
				}
			}
			return c;
		}

		// Token: 0x04000020 RID: 32
		internal static readonly CultureInfo CurrentCulture = CultureInfo.CurrentCulture;

		// Token: 0x04000021 RID: 33
		private static readonly CharacterMap m_toLowerMap = new CharacterMap(new CharacterMap.CharMap(char.ToLower));

		// Token: 0x04000022 RID: 34
		private static readonly CharacterMap m_toUpperMap = new CharacterMap(new CharacterMap.CharMap(char.ToUpper));

		// Token: 0x020000C7 RID: 199
		[Flags]
		public enum FoldStringFlags
		{
			// Token: 0x040001B8 RID: 440
			NONE = 0,
			// Token: 0x040001B9 RID: 441
			MAP_FOLDCZONE = 16,
			// Token: 0x040001BA RID: 442
			MAP_PRECOMPOSED = 32,
			// Token: 0x040001BB RID: 443
			MAP_COMPOSITE = 64,
			// Token: 0x040001BC RID: 444
			MAP_FOLDDIGITS = 128,
			// Token: 0x040001BD RID: 445
			MAP_EXPAND_LIGATURES = 8192
		}

		// Token: 0x020000C8 RID: 200
		[Flags]
		public enum LCMapStringFlags
		{
			// Token: 0x040001BF RID: 447
			NONE = 0,
			// Token: 0x040001C0 RID: 448
			NORM_IGNORECASE = 1,
			// Token: 0x040001C1 RID: 449
			NORM_IGNORENONSPACE = 2,
			// Token: 0x040001C2 RID: 450
			NORM_IGNORESYMBOLS = 4,
			// Token: 0x040001C3 RID: 451
			NORM_IGNOREKANATYPE = 65536,
			// Token: 0x040001C4 RID: 452
			NORM_IGNOREWIDTH = 131072,
			// Token: 0x040001C5 RID: 453
			LCMAP_LOWERCASE = 256,
			// Token: 0x040001C6 RID: 454
			LCMAP_UPPERCASE = 512,
			// Token: 0x040001C7 RID: 455
			LCMAP_HIRAGANA = 1048576,
			// Token: 0x040001C8 RID: 456
			LCMAP_KATAKANA = 2097152,
			// Token: 0x040001C9 RID: 457
			LCMAP_HALFWIDTH = 4194304,
			// Token: 0x040001CA RID: 458
			LCMAP_FULLWIDTH = 8388608
		}
	}
}
