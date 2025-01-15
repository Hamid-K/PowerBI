using System;

namespace Microsoft.ProgramSynthesis.Detection.DomainTypes
{
	// Token: 0x02000AEF RID: 2799
	internal static class GeographicCoordinateParsingExtensions
	{
		// Token: 0x06004617 RID: 17943 RVA: 0x000DA918 File Offset: 0x000D8B18
		private static bool MatchBracketsAndCheckEndOfString(this string v, int index, char? openBracket, out int nextIndex)
		{
			nextIndex = index;
			v.ParseWhitespace(index, out index);
			if (openBracket != null)
			{
				char? c;
				if (!v.ParseCharacter(index, ')', out index, out c) && !v.ParseCharacter(index, ']', out index, out c) && !v.ParseCharacter(index, '}', out index, out c))
				{
					return false;
				}
				char? c2 = openBracket;
				int? num = ((c2 != null) ? new int?((int)c2.GetValueOrDefault()) : null);
				int num2 = 40;
				if ((num.GetValueOrDefault() == num2) & (num != null))
				{
					c2 = c;
					num = ((c2 != null) ? new int?((int)c2.GetValueOrDefault()) : null);
					num2 = 41;
					if (!((num.GetValueOrDefault() == num2) & (num != null)))
					{
						return false;
					}
				}
				c2 = openBracket;
				num = ((c2 != null) ? new int?((int)c2.GetValueOrDefault()) : null);
				num2 = 91;
				if ((num.GetValueOrDefault() == num2) & (num != null))
				{
					c2 = c;
					num = ((c2 != null) ? new int?((int)c2.GetValueOrDefault()) : null);
					num2 = 93;
					if (!((num.GetValueOrDefault() == num2) & (num != null)))
					{
						return false;
					}
				}
				c2 = openBracket;
				num = ((c2 != null) ? new int?((int)c2.GetValueOrDefault()) : null);
				num2 = 123;
				if ((num.GetValueOrDefault() == num2) & (num != null))
				{
					c2 = c;
					num = ((c2 != null) ? new int?((int)c2.GetValueOrDefault()) : null);
					num2 = 125;
					if (!((num.GetValueOrDefault() == num2) & (num != null)))
					{
						return false;
					}
				}
				v.ParseWhitespace(index, out index);
			}
			nextIndex = index;
			return index >= v.Length;
		}

		// Token: 0x06004618 RID: 17944 RVA: 0x000DAAF0 File Offset: 0x000D8CF0
		public static bool ParseLatLong(this string v, out double latitude, out double longitude)
		{
			latitude = (longitude = double.NaN);
			if (string.IsNullOrEmpty(v))
			{
				return false;
			}
			int num = 0;
			v.ParseWhitespace(num, out num);
			char? c;
			if (!v.ParseCharacter(num, '(', out num, out c) && !v.ParseCharacter(num, '[', out num, out c))
			{
				v.ParseCharacter(num, '{', out num, out c);
			}
			v.ParseWhitespace(num, out num);
			int num2 = num;
			if (v.ParseSignedLatLong(num, out num, out latitude, out longitude))
			{
				v.ParseWhitespace(num, out num);
				if (v.MatchBracketsAndCheckEndOfString(num, c, out num))
				{
					return true;
				}
				latitude = (longitude = double.NaN);
			}
			num = num2;
			if (v.ParseDmsVariantsLatLong(num, out num, out latitude, out longitude))
			{
				v.ParseWhitespace(num, out num);
				return v.MatchBracketsAndCheckEndOfString(num, c, out num);
			}
			return false;
		}

		// Token: 0x06004619 RID: 17945 RVA: 0x000DABC0 File Offset: 0x000D8DC0
		private static bool ParseSignedLatLong(this string v, int index, out int nextIndex, out double latitude, out double longitude)
		{
			nextIndex = index;
			latitude = (longitude = double.NaN);
			if (index >= v.Length)
			{
				return false;
			}
			double num;
			if (!v.ParseSignedReal(index, out num, out index, false))
			{
				return false;
			}
			v.ParseLatLongSeparator(index, out index);
			double num2;
			if (!v.ParseSignedReal(index, out num2, out nextIndex, false))
			{
				return false;
			}
			bool flag = num >= -90.0 && num <= 90.0;
			bool flag2 = num2 >= -90.0 && num2 <= 90.0;
			bool flag3 = num >= -180.0 && num <= 180.0;
			bool flag4 = num2 >= -180.0 && num2 <= 180.0;
			if (flag && flag4)
			{
				latitude = num;
				longitude = num2;
				return true;
			}
			if (flag2 && flag3)
			{
				latitude = num2;
				longitude = num;
				return true;
			}
			return false;
		}

		// Token: 0x0600461A RID: 17946 RVA: 0x000DACB0 File Offset: 0x000D8EB0
		private static bool ParseLatLongSeparator(this string v, int index, out int nextIndex)
		{
			int num = index;
			nextIndex = index;
			if (index >= v.Length)
			{
				return false;
			}
			v.ParseWhitespace(index, out index);
			if (v.ParseString(index, "and", out index) || v.ParseCharacter(index, '&', out index) || v.ParseCharacter(index, ',', out index))
			{
				v.ParseWhitespace(index, out nextIndex);
				return true;
			}
			nextIndex = index;
			return nextIndex == num;
		}

		// Token: 0x0600461B RID: 17947 RVA: 0x000DAD14 File Offset: 0x000D8F14
		private static bool ParseDmsVariantsLatLong(this string v, int index, out int nextIndex, out double latitude, out double longitude)
		{
			nextIndex = index;
			latitude = (longitude = double.NaN);
			if (index >= v.Length)
			{
				return false;
			}
			double num;
			double num2;
			if (!v.ParseLatLongDmsPart(index, out index, out num, out num2))
			{
				return false;
			}
			v.ParseWhitespace(index, out index);
			v.ParseLatLongSeparator(index, out index);
			v.ParseWhitespace(index, out index);
			double num3;
			double num4;
			if (!v.ParseLatLongDmsPart(index, out index, out num3, out num4))
			{
				latitude = num;
				longitude = num2;
				nextIndex = index;
				return true;
			}
			if ((!double.IsNaN(num) && !double.IsNaN(num3)) || (!double.IsNaN(num2) && !double.IsNaN(num4)))
			{
				return false;
			}
			latitude = (double.IsNaN(num) ? num3 : num);
			longitude = (double.IsNaN(num2) ? num4 : num2);
			nextIndex = index;
			return true;
		}

		// Token: 0x0600461C RID: 17948 RVA: 0x000DADD0 File Offset: 0x000D8FD0
		private static bool ParseDirection(this string v, int index, out int nextIndex, out char? direction)
		{
			direction = null;
			nextIndex = index;
			if (index >= v.Length)
			{
				return false;
			}
			char c = char.ToLowerInvariant(v[index]);
			if (c == 'n' || c == 's' || c == 'e' || c == 'w')
			{
				direction = new char?(c);
				nextIndex = index + 1;
				return true;
			}
			return false;
		}

		// Token: 0x0600461D RID: 17949 RVA: 0x000DAE28 File Offset: 0x000D9028
		private static bool ParseLatLongDmsPart(this string v, int index, out int nextIndex, out double latitude, out double longitude)
		{
			latitude = (longitude = double.NaN);
			nextIndex = index;
			if (index >= v.Length)
			{
				return false;
			}
			char? c;
			v.ParseDirection(index, out index, out c);
			v.ParseWhitespace(index, out index);
			double num;
			if (!v.ParseDegreesMinutesAndSeconds(index, out index, out num))
			{
				return false;
			}
			v.ParseWhitespace(index, out index);
			if (c == null && !v.ParseDirection(index, out index, out c))
			{
				return false;
			}
			if (c != null)
			{
				char valueOrDefault = c.GetValueOrDefault();
				if (valueOrDefault <= 'n')
				{
					if (valueOrDefault != 'e')
					{
						if (valueOrDefault != 'n')
						{
							return false;
						}
						if (num > 90.0)
						{
							return false;
						}
						latitude = num;
					}
					else
					{
						if (num > 180.0)
						{
							return false;
						}
						longitude = num;
					}
				}
				else if (valueOrDefault != 's')
				{
					if (valueOrDefault != 'w')
					{
						return false;
					}
					if (num > 180.0)
					{
						return false;
					}
					longitude = -num;
				}
				else
				{
					if (num > 90.0)
					{
						return false;
					}
					latitude = -num;
				}
				nextIndex = index;
				return true;
			}
			return false;
		}

		// Token: 0x0600461E RID: 17950 RVA: 0x000DAF20 File Offset: 0x000D9120
		private static bool ParseDegreesMinutesAndSeconds(this string v, int index, out int nextIndex, out double value)
		{
			nextIndex = index;
			value = double.NaN;
			if (index >= v.Length)
			{
				return false;
			}
			if (v.ParseReal(index, out value, out index, true))
			{
				v.ParseWhitespace(index, out index);
				v.ParseDegreeSeparator(index, out index);
				v.ParseWhitespace(index, out nextIndex);
				return true;
			}
			long num;
			if (!v.ParseDecimalDigits(index, out num, out index, new int?(3)) || num > 180L)
			{
				return false;
			}
			int num2 = (int)num;
			v.ParseWhitespace(index, out index);
			v.ParseDegreeSeparator(index, out index);
			v.ParseWhitespace(index, out index);
			return v.ParseMinutesAndSeconds(index, num2, out nextIndex, out value);
		}

		// Token: 0x0600461F RID: 17951 RVA: 0x000DAFBC File Offset: 0x000D91BC
		private static bool ParseMinutesAndSeconds(this string v, int index, int degrees, out int nextIndex, out double value)
		{
			nextIndex = index;
			value = double.NaN;
			if (index >= v.Length)
			{
				return false;
			}
			double num;
			if (v.ParseReal(index, out num, out index, true))
			{
				if (num >= 60.0)
				{
					return false;
				}
				v.ParseWhitespace(index, out index);
				v.ParseMinutesSeparator(index, out index);
				v.ParseWhitespace(index, out nextIndex);
				value = (double)degrees + num / 60.0;
				return true;
			}
			else
			{
				long num2;
				if (!v.ParseDecimalDigits(index, out num2, out index, new int?(2)))
				{
					value = (double)degrees;
					nextIndex = index;
					return true;
				}
				int num3 = (int)num2;
				if (num3 >= 60)
				{
					return false;
				}
				v.ParseWhitespace(index, out index);
				v.ParseMinutesSeparator(index, out index);
				v.ParseWhitespace(index, out index);
				return v.ParseSeconds(index, degrees, num3, out nextIndex, out value);
			}
		}

		// Token: 0x06004620 RID: 17952 RVA: 0x000DB080 File Offset: 0x000D9280
		private static bool ParseSeconds(this string v, int index, int degrees, int minutes, out int nextIndex, out double value)
		{
			value = (double)degrees + (double)minutes / 60.0;
			nextIndex = index;
			if (index >= v.Length)
			{
				return false;
			}
			double num;
			if (v.ParseReal(index, out num, out index, false))
			{
				if (num >= 60.0)
				{
					return false;
				}
				value += num / 3600.0;
			}
			v.ParseSecondsSeparator(index, out nextIndex);
			return true;
		}

		// Token: 0x06004621 RID: 17953 RVA: 0x000DB0E8 File Offset: 0x000D92E8
		private static bool ParseSecondsSeparator(this string v, int index, out int nextIndex)
		{
			nextIndex = index;
			if (index >= v.Length)
			{
				return false;
			}
			if (char.IsWhiteSpace(v[index]) || v[index] == '"' || v[index] == '“' || v[index] == '”')
			{
				nextIndex = index + 1;
				return true;
			}
			return false;
		}

		// Token: 0x06004622 RID: 17954 RVA: 0x000DB140 File Offset: 0x000D9340
		private static bool ParseMinutesSeparator(this string v, int index, out int nextIndex)
		{
			nextIndex = index;
			if (index >= v.Length)
			{
				return false;
			}
			if (char.IsWhiteSpace(v[index]) || v[index] == '\'' || v[index] == '`' || v[index] == '\u00b4' || v[index] == '‘' || v[index] == '’')
			{
				nextIndex = index + 1;
				return true;
			}
			return false;
		}

		// Token: 0x06004623 RID: 17955 RVA: 0x000DB1B4 File Offset: 0x000D93B4
		private static bool ParseDegreeSeparator(this string v, int index, out int nextIndex)
		{
			nextIndex = index;
			if (index >= v.Length)
			{
				return false;
			}
			if (char.IsWhiteSpace(v[index]) || v[index] == '°' || v[index] == '℃' || v[index] == '℉')
			{
				nextIndex = index + 1;
				return true;
			}
			return false;
		}
	}
}
