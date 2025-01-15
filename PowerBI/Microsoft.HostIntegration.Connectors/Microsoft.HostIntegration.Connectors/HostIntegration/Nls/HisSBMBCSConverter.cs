using System;
using System.IO;
using System.Reflection;
using Microsoft.HostIntegration.StrictResources.Nls;

namespace Microsoft.HostIntegration.Nls
{
	// Token: 0x02000638 RID: 1592
	internal abstract class HisSBMBCSConverter : HisConverter
	{
		// Token: 0x060035A3 RID: 13731 RVA: 0x000B48A3 File Offset: 0x000B2AA3
		internal HisSBMBCSConverter(HisEncoding enc)
			: base(enc)
		{
		}

		// Token: 0x060035A4 RID: 13732 RVA: 0x000B48AC File Offset: 0x000B2AAC
		internal override int GetEbcdicByteCount(byte[] src, int index, int count)
		{
			HisConverter.StatusFlags statusFlags = HisConverter.StatusFlags.NoStatus;
			HisConverter.PositionFlags positionFlags = HisConverter.PositionFlags.AtSingleByte;
			HisConverter.StreamState streamState = HisConverter.StreamState.NotSet;
			return this.GetEbcdicByteCount(src, index, count, true, ref statusFlags, ref streamState, ref positionFlags);
		}

		// Token: 0x060035A5 RID: 13733 RVA: 0x000B48D0 File Offset: 0x000B2AD0
		internal override int GetUnicodeByteCount(byte[] src, int index, int count)
		{
			HisConverter.StatusFlags statusFlags = HisConverter.StatusFlags.NoStatus;
			HisConverter.PositionFlags positionFlags = HisConverter.PositionFlags.AtSingleByte;
			HisConverter.StreamState streamState = HisConverter.StreamState.NotSet;
			return this.GetUnicodeByteCount(src, index, count, true, ref statusFlags, ref streamState, ref positionFlags);
		}

		// Token: 0x060035A6 RID: 13734 RVA: 0x000B48F4 File Offset: 0x000B2AF4
		internal override int GetUnicodeCharCount(byte[] src, int index, int count)
		{
			HisConverter.StatusFlags statusFlags = HisConverter.StatusFlags.NoStatus;
			HisConverter.PositionFlags positionFlags = HisConverter.PositionFlags.AtSingleByte;
			HisConverter.StreamState streamState = HisConverter.StreamState.NotSet;
			return this.GetUnicodeCharCount(src, index, count, true, ref statusFlags, ref streamState, ref positionFlags);
		}

		// Token: 0x060035A7 RID: 13735 RVA: 0x000B4918 File Offset: 0x000B2B18
		internal override int EbcdicToUnicode(byte[] src, byte[] dest, int srcIndex, int srcLength, int destIndex, int totalDestLength, bool flush, ref int requiredLen, ref HisConverter.StreamState state, ref bool completed)
		{
			HisConverter.StatusFlags statusFlags = HisConverter.StatusFlags.NoStatus;
			HisConverter.PositionFlags positionFlags = HisConverter.PositionFlags.AtSingleByte;
			if (srcLength < 1 || srcLength > 2147483647)
			{
				throw new OutOfMemoryException(SR.InvalidInputBufferLength(srcLength));
			}
			if (totalDestLength - destIndex < 1)
			{
				throw new ArgumentException(SR.InvalidOutputBufferLength(totalDestLength));
			}
			return this.EbcdicToUnicode(src, dest, srcIndex, srcLength, destIndex, totalDestLength, flush, ref requiredLen, ref statusFlags, ref state, ref positionFlags);
		}

		// Token: 0x060035A8 RID: 13736 RVA: 0x000B497C File Offset: 0x000B2B7C
		internal override int UnicodeToEbcdic(byte[] src, byte[] dest, int srcIndex, int srcLength, int destIndex, int totalDestLength, bool flush, ref int requiredLen, ref HisConverter.StreamState state, ref bool completed)
		{
			HisConverter.StatusFlags statusFlags = HisConverter.StatusFlags.NoStatus;
			HisConverter.PositionFlags positionFlags = HisConverter.PositionFlags.AtSingleByte;
			if (srcLength < 1 || srcLength > 2147483647)
			{
				throw new ArgumentException(SR.InvalidInputBufferLength(srcLength));
			}
			if (totalDestLength - destIndex < 1)
			{
				throw new ArgumentException(SR.InvalidOutputBufferLength(totalDestLength));
			}
			return this.UnicodeToEbcdic(src, dest, srcIndex, srcLength, destIndex, totalDestLength, flush, ref requiredLen, ref statusFlags, ref state, ref positionFlags);
		}

		// Token: 0x060035A9 RID: 13737 RVA: 0x000B49E0 File Offset: 0x000B2BE0
		internal override int GetDoubleByteCutOffIndex(byte[] src, int index, int count, ref int requiredCount, ref HisConverter.StreamState parseState)
		{
			int num = 0;
			requiredCount = 0;
			if (count > src.Length)
			{
				throw new ArgumentException(SR.InvalidSourceCount);
			}
			int i;
			for (i = 0; i < count; i++)
			{
				if (parseState == HisConverter.StreamState.NotSet || parseState == HisConverter.StreamState.SI)
				{
					if (src[i + index] == 14)
					{
						parseState = HisConverter.StreamState.SO;
					}
					requiredCount++;
				}
				else
				{
					if (src[i + index] == 15 && num != 0)
					{
						throw new ArgumentException(SR.InvalidSISOPair);
					}
					if (src[i + index] == 15 && num == 0)
					{
						parseState = HisConverter.StreamState.SI;
					}
					else
					{
						num++;
						if (num == 2)
						{
							num = 0;
						}
					}
					requiredCount++;
				}
			}
			if (num != 0)
			{
				requiredCount--;
				return i - 1;
			}
			return i;
		}

		// Token: 0x060035AA RID: 13738 RVA: 0x000B4A7F File Offset: 0x000B2C7F
		internal override int GetMinimumByteSizeRequired(ref HisConverter.StreamState parseState)
		{
			if (parseState == HisConverter.StreamState.NotSet || parseState == HisConverter.StreamState.SI)
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x060035AB RID: 13739 RVA: 0x000B4A90 File Offset: 0x000B2C90
		private int UnicodeToEbcdic(byte[] src, byte[] dest, int srcIndex, int srcLength, int destIndex, int totalDestLength, bool flush, ref int requiredLen, ref HisConverter.StatusFlags status, ref HisConverter.StreamState state, ref HisConverter.PositionFlags flags)
		{
			HisConverter.PositionFlags positionFlags = HisConverter.PositionFlags.AtSingleByte;
			bool truncate = this.encoding.Truncate;
			byte[] leadBytes = this.GetLeadBytes();
			int i = 0;
			int num = 0;
			uint num2 = (uint)(srcLength * 2);
			byte[] array = new byte[num2];
			if (array == null)
			{
				throw new OutOfMemoryException(SR.OutOfMemory(num2));
			}
			bool flag = this.encoding.destinationCodePage != HisEncoding.HostCodePages.EBCDICJapaneseKatakana_290 && this.encoding.destinationCodePage != HisEncoding.HostCodePages.EBCDICLowerEnglish_37 && this.encoding.destinationCodePage != HisEncoding.HostCodePages.EBCDICJapaneseExtendedLowerEnglish_1027;
			HisConverter.StreamState streamState;
			if (state == HisConverter.StreamState.NotSet)
			{
				if (flag && this.encoding.DoubleByteStart)
				{
					streamState = HisConverter.StreamState.SO;
				}
				else
				{
					streamState = HisConverter.StreamState.SI;
				}
			}
			else
			{
				streamState = state;
			}
			while (i < srcLength)
			{
				if ((long)(num + 2) >= (long)((ulong)num2))
				{
					byte[] array2 = new byte[num2 + 1024U];
					if (array2 == null)
					{
						throw new OutOfMemoryException(SR.OutOfMemory(num2 + 1024U));
					}
					Buffer.BlockCopy(array, 0, array2, 0, array.Length);
					array = array2;
					num2 += 1024U;
				}
				bool flag2 = false;
				if (flag)
				{
					ushort num3 = 0;
					while ((int)num3 < leadBytes.Length && leadBytes[(int)num3] != 0)
					{
						if (src[i + srcIndex] >= leadBytes[(int)num3] && src[i + srcIndex] <= leadBytes[(int)(num3 + 1)])
						{
							flag2 = true;
							break;
						}
						num3 += 2;
					}
				}
				if (flag && flag2)
				{
					if (i + 1 >= srcLength)
					{
						if (streamState == HisConverter.StreamState.SO && this.encoding.AutomaticShiftInShiftOut)
						{
							array[num] = 15;
							num++;
							streamState = HisConverter.StreamState.SI;
							if (truncate && num > totalDestLength)
							{
								break;
							}
						}
						array[num] = 0;
						num++;
						positionFlags = HisConverter.PositionFlags.AtDoubleByte;
						status = HisConverter.StatusFlags.ExitNull;
						break;
					}
					if (streamState == HisConverter.StreamState.SI && this.encoding.AutomaticShiftInShiftOut)
					{
						if (truncate && num + 4 > totalDestLength)
						{
							break;
						}
						array[num] = 14;
						num++;
						streamState = HisConverter.StreamState.SO;
					}
					if (truncate)
					{
						if (this.encoding.AutomaticShiftInShiftOut)
						{
							if (num + 3 > totalDestLength)
							{
								break;
							}
						}
						else if (num + 2 > totalDestLength)
						{
							break;
						}
					}
					int num4 = this.ConvertToEbcdic(false, src, array, i + srcIndex, num);
					if (num4 != 0)
					{
						throw new Exception(SR.EBCDICToUnicodeError(num4));
					}
					i += 2;
					num += 2;
					positionFlags = HisConverter.PositionFlags.AtDoubleByte;
				}
				else if ((this.encoding.AutomaticShiftInShiftOut && (src[i + srcIndex] == 14 || src[i + srcIndex] == 15) && this.encoding.UseEnhancedEbcdicTable) || src[i + srcIndex] == 31 || src[i + srcIndex] == 30)
				{
					byte b;
					if (src[i + srcIndex] == 31)
					{
						b = 15;
					}
					else if (src[i + srcIndex] == 30)
					{
						b = 14;
					}
					else
					{
						b = src[i + srcIndex];
					}
					array[num] = b;
					positionFlags = HisConverter.PositionFlags.AtSingleByte;
					if (src[i + srcIndex] == 14 || src[i + srcIndex] == 30)
					{
						streamState = HisConverter.StreamState.SO;
					}
					if (src[i + srcIndex] == 15 || src[i + srcIndex] == 31)
					{
						streamState = HisConverter.StreamState.SI;
					}
					i++;
					num++;
				}
				else
				{
					if (streamState == HisConverter.StreamState.SO)
					{
						if (this.encoding.AutomaticShiftInShiftOut)
						{
							array[num] = 15;
							num++;
							streamState = HisConverter.StreamState.SI;
						}
						if (truncate && num > totalDestLength)
						{
							break;
						}
					}
					int num4 = this.ConvertToEbcdic(true, src, array, i + srcIndex, num);
					if (num4 != 0)
					{
						throw new Exception(SR.EBCDICToUnicodeError(num4));
					}
					i++;
					num++;
					positionFlags = HisConverter.PositionFlags.AtSingleByte;
					if (truncate && num > totalDestLength)
					{
						throw new Exception(SR.EBCDICToUnicodeError(num));
					}
				}
			}
			if (flush && streamState == HisConverter.StreamState.SO && this.encoding.AutomaticShiftInShiftOut)
			{
				array[num] = 15;
				num++;
				streamState = HisConverter.StreamState.SI;
			}
			if (num > totalDestLength)
			{
				if (totalDestLength > 0)
				{
					Memory.MemoryCopy(array, 0, dest, 0, (long)totalDestLength);
				}
				if (num > 2147483647)
				{
					requiredLen = int.MaxValue;
					status = HisConverter.StatusFlags.ExitOver;
				}
				else
				{
					requiredLen = num;
				}
				flags = positionFlags;
				state = streamState;
				return requiredLen;
			}
			int num5 = totalDestLength - destIndex;
			int num6 = ((num5 < num) ? num5 : num);
			Memory.MemoryCopy(array, 0, dest, destIndex, (long)num6);
			requiredLen = num;
			flags = positionFlags;
			state = streamState;
			return 0;
		}

		// Token: 0x060035AC RID: 13740 RVA: 0x000B4E60 File Offset: 0x000B3060
		private int EbcdicToUnicode(byte[] src, byte[] dest, int srcIndex, int srcLength, int destIndex, int totalDestLength, bool bFlush, ref int requiredLen, ref HisConverter.StatusFlags status, ref HisConverter.StreamState state, ref HisConverter.PositionFlags flags)
		{
			byte[] array = new byte[srcLength];
			if (array == null)
			{
				throw new OutOfMemoryException(SR.OutOfMemory(srcLength));
			}
			bool flag = this.encoding.destinationCodePage != HisEncoding.HostCodePages.EBCDICJapaneseKatakana_290 && this.encoding.destinationCodePage != HisEncoding.HostCodePages.EBCDICLowerEnglish_37 && this.encoding.destinationCodePage != HisEncoding.HostCodePages.EBCDICJapaneseExtendedLowerEnglish_1027;
			int i = 0;
			int num = 0;
			HisConverter.PositionFlags positionFlags = HisConverter.PositionFlags.AtSingleByte;
			HisConverter.StreamState streamState;
			if (state == HisConverter.StreamState.NotSet)
			{
				if (flag && this.encoding.DoubleByteStart)
				{
					streamState = HisConverter.StreamState.SO;
				}
				else
				{
					streamState = HisConverter.StreamState.SI;
				}
			}
			else
			{
				streamState = state;
			}
			while (i < srcLength)
			{
				if (num >= srcLength)
				{
					throw new OutOfMemoryException(SR.OutOfMemory(num));
				}
				if (flag)
				{
					if (streamState == HisConverter.StreamState.SI)
					{
						if (src[i + srcIndex] == 14)
						{
							streamState = HisConverter.StreamState.SO;
							positionFlags = HisConverter.PositionFlags.AtSingleByte;
							if (!this.encoding.AutomaticShiftInShiftOut)
							{
								if (this.encoding.UseEnhancedEbcdicTable)
								{
									int num2 = this.ConvertToUnicode(true, src, array, i + srcIndex, num);
									if (num2 != 0)
									{
										throw new Exception(SR.UnicodeToEBCDICError(num2));
									}
								}
								else
								{
									array[num] = 30;
								}
								num++;
							}
							i++;
							continue;
						}
					}
					else
					{
						if (i == 0 && src[i + srcIndex] == 14)
						{
							if (!this.encoding.AutomaticShiftInShiftOut)
							{
								if (this.encoding.UseEnhancedEbcdicTable)
								{
									int num2 = this.ConvertToUnicode(true, src, array, i + srcIndex, num);
									if (num2 != 0)
									{
										throw new Exception(SR.UnicodeToEBCDICError(num2));
									}
								}
								else
								{
									array[num] = 30;
								}
								num++;
							}
							i++;
							continue;
						}
						if (src[i + srcIndex] == 15)
						{
							streamState = HisConverter.StreamState.SI;
							positionFlags = HisConverter.PositionFlags.AtSingleByte;
							if (!this.encoding.AutomaticShiftInShiftOut)
							{
								if (this.encoding.UseEnhancedEbcdicTable)
								{
									int num2 = this.ConvertToUnicode(true, src, array, i + srcIndex, num);
									if (num2 != 0)
									{
										throw new Exception(SR.UnicodeToEBCDICError(num2));
									}
								}
								else
								{
									array[num] = 31;
								}
								num++;
							}
							i++;
							continue;
						}
					}
				}
				if (streamState == HisConverter.StreamState.SO)
				{
					if (i + 1 >= srcLength)
					{
						array[num] = 0;
						num++;
						positionFlags = HisConverter.PositionFlags.AtDoubleByte;
						status = HisConverter.StatusFlags.ExitNull;
						break;
					}
					int num2 = this.ConvertToUnicode(false, src, array, i + srcIndex, num);
					if (num2 != 0)
					{
						throw new Exception(SR.UnicodeToEBCDICError(num2));
					}
					i += 2;
					num += 2;
					positionFlags = HisConverter.PositionFlags.AtDoubleByte;
				}
				else
				{
					int num2 = this.ConvertToUnicode(true, src, array, i + srcIndex, num);
					if (num2 != 0)
					{
						throw new Exception(SR.UnicodeToEBCDICError(num2));
					}
					i++;
					num++;
					positionFlags = HisConverter.PositionFlags.AtSingleByte;
				}
			}
			int num3;
			if (num > totalDestLength)
			{
				num3 = totalDestLength - destIndex;
				int num4 = ((num3 < array.Length) ? num3 : array.Length);
				if (totalDestLength > 0)
				{
					Memory.MemoryCopy(dest, destIndex, array, 0, (long)num4);
				}
				requiredLen = num;
				flags = positionFlags;
				state = streamState;
				return requiredLen;
			}
			num3 = totalDestLength - destIndex;
			int num5 = ((num3 < num) ? num3 : num);
			Memory.MemoryCopy(array, 0, dest, 0, (long)num);
			requiredLen = num;
			flags = positionFlags;
			state = streamState;
			return 0;
		}

		// Token: 0x060035AD RID: 13741
		protected abstract bool IsLeadByte(char ch);

		// Token: 0x060035AE RID: 13742
		protected abstract int ConvertToEbcdic(bool bSingleByte, byte[] src, byte[] dest, int srcIndex, int destIndex);

		// Token: 0x060035AF RID: 13743
		protected abstract int ConvertToUnicode(bool bSingleByte, byte[] src, byte[] dest, int srcIndex, int destIndex);

		// Token: 0x060035B0 RID: 13744 RVA: 0x000B5144 File Offset: 0x000B3344
		internal override int GetUnicodeByteCount(byte[] src, int index, int count, bool flush, ref HisConverter.StatusFlags status, ref HisConverter.StreamState state, ref HisConverter.PositionFlags flags)
		{
			bool flag = this.encoding.destinationCodePage != HisEncoding.HostCodePages.EBCDICJapaneseKatakana_290 && this.encoding.destinationCodePage != HisEncoding.HostCodePages.EBCDICLowerEnglish_37 && this.encoding.destinationCodePage != HisEncoding.HostCodePages.EBCDICJapaneseExtendedLowerEnglish_1027;
			int i = 0;
			int num = 0;
			HisConverter.PositionFlags positionFlags = HisConverter.PositionFlags.AtSingleByte;
			HisConverter.StreamState streamState;
			if (state == HisConverter.StreamState.NotSet)
			{
				if (flag && this.encoding.DoubleByteStart)
				{
					streamState = HisConverter.StreamState.SO;
				}
				else
				{
					streamState = HisConverter.StreamState.SI;
				}
			}
			else
			{
				streamState = state;
			}
			while (i < count)
			{
				if (flag)
				{
					if (streamState == HisConverter.StreamState.SI)
					{
						if (src[i + index] == 14)
						{
							streamState = HisConverter.StreamState.SO;
							positionFlags = HisConverter.PositionFlags.AtSingleByte;
							if (!this.encoding.AutomaticShiftInShiftOut)
							{
								num++;
							}
							i++;
							continue;
						}
					}
					else
					{
						if (i == 0 && src[i + index] == 14)
						{
							if (!this.encoding.AutomaticShiftInShiftOut)
							{
								num++;
							}
							i++;
							continue;
						}
						if (src[i + index] == 15)
						{
							streamState = HisConverter.StreamState.SI;
							positionFlags = HisConverter.PositionFlags.AtSingleByte;
							if (!this.encoding.AutomaticShiftInShiftOut)
							{
								num++;
							}
							i++;
							continue;
						}
					}
				}
				if (streamState == HisConverter.StreamState.SO)
				{
					if (i + 1 >= count)
					{
						num++;
						positionFlags = HisConverter.PositionFlags.AtDoubleByte;
						status = HisConverter.StatusFlags.ExitNull;
						break;
					}
					i += 2;
					num += 2;
					positionFlags = HisConverter.PositionFlags.AtDoubleByte;
				}
				else
				{
					i++;
					num++;
					positionFlags = HisConverter.PositionFlags.AtSingleByte;
				}
			}
			int num2 = num;
			flags = positionFlags;
			state = streamState;
			return num2;
		}

		// Token: 0x060035B1 RID: 13745 RVA: 0x000B5274 File Offset: 0x000B3474
		internal override int GetUnicodeCharCount(byte[] src, int index, int count, bool flush, ref HisConverter.StatusFlags status, ref HisConverter.StreamState state, ref HisConverter.PositionFlags flags)
		{
			bool flag = this.encoding.destinationCodePage != HisEncoding.HostCodePages.EBCDICJapaneseKatakana_290 && this.encoding.destinationCodePage != HisEncoding.HostCodePages.EBCDICLowerEnglish_37 && this.encoding.destinationCodePage != HisEncoding.HostCodePages.EBCDICJapaneseExtendedLowerEnglish_1027;
			int i = 0;
			int num = 0;
			HisConverter.PositionFlags positionFlags = HisConverter.PositionFlags.AtSingleByte;
			HisConverter.StreamState streamState;
			if (state == HisConverter.StreamState.NotSet)
			{
				if (flag && this.encoding.DoubleByteStart)
				{
					streamState = HisConverter.StreamState.SO;
				}
				else
				{
					streamState = HisConverter.StreamState.SI;
				}
			}
			else
			{
				streamState = state;
			}
			while (i < count)
			{
				if (flag)
				{
					if (streamState == HisConverter.StreamState.SI)
					{
						if (src[i + index] == 14)
						{
							streamState = HisConverter.StreamState.SO;
							positionFlags = HisConverter.PositionFlags.AtSingleByte;
							if (!this.encoding.AutomaticShiftInShiftOut)
							{
								num++;
							}
							i++;
							continue;
						}
					}
					else
					{
						if (i == 0 && src[i + index] == 14)
						{
							if (!this.encoding.AutomaticShiftInShiftOut)
							{
								num++;
							}
							i++;
							continue;
						}
						if (src[i + index] == 15)
						{
							streamState = HisConverter.StreamState.SI;
							positionFlags = HisConverter.PositionFlags.AtSingleByte;
							if (!this.encoding.AutomaticShiftInShiftOut)
							{
								num++;
							}
							i++;
							continue;
						}
					}
				}
				if (streamState == HisConverter.StreamState.SO)
				{
					if (i + 1 >= count)
					{
						num++;
						positionFlags = HisConverter.PositionFlags.AtDoubleByte;
						status = HisConverter.StatusFlags.ExitNull;
						break;
					}
					i += 2;
					num++;
					positionFlags = HisConverter.PositionFlags.AtDoubleByte;
				}
				else
				{
					i++;
					num++;
					positionFlags = HisConverter.PositionFlags.AtSingleByte;
				}
			}
			int num2 = num;
			flags = positionFlags;
			state = streamState;
			return num2;
		}

		// Token: 0x060035B2 RID: 13746 RVA: 0x000B53A4 File Offset: 0x000B35A4
		internal override int GetEbcdicByteCount(byte[] src, int index, int count, bool flush, ref HisConverter.StatusFlags status, ref HisConverter.StreamState state, ref HisConverter.PositionFlags flags)
		{
			HisConverter.PositionFlags positionFlags = HisConverter.PositionFlags.AtSingleByte;
			byte[] leadBytes = this.GetLeadBytes();
			int i = 0;
			int num = 0;
			bool flag = this.encoding.destinationCodePage != HisEncoding.HostCodePages.EBCDICJapaneseKatakana_290 && this.encoding.destinationCodePage != HisEncoding.HostCodePages.EBCDICLowerEnglish_37 && this.encoding.destinationCodePage != HisEncoding.HostCodePages.EBCDICJapaneseExtendedLowerEnglish_1027;
			HisConverter.StreamState streamState;
			if (state == HisConverter.StreamState.NotSet)
			{
				if (flag && this.encoding.DoubleByteStart)
				{
					streamState = HisConverter.StreamState.SO;
				}
				else
				{
					streamState = HisConverter.StreamState.SI;
				}
			}
			else
			{
				streamState = state;
			}
			while (i < count)
			{
				bool flag2 = false;
				if (flag)
				{
					ushort num2 = 0;
					while ((int)num2 < leadBytes.Length && leadBytes[(int)num2] != 0)
					{
						if (src[i + index] >= leadBytes[(int)num2] && src[i + index] <= leadBytes[(int)(num2 + 1)])
						{
							flag2 = true;
							break;
						}
						num2 += 2;
					}
				}
				if (flag && flag2)
				{
					if (i + 1 >= count)
					{
						if (streamState == HisConverter.StreamState.SO && this.encoding.AutomaticShiftInShiftOut)
						{
							num++;
							streamState = HisConverter.StreamState.SI;
						}
						num++;
						positionFlags = HisConverter.PositionFlags.AtDoubleByte;
						status = HisConverter.StatusFlags.ExitNull;
						break;
					}
					if (streamState == HisConverter.StreamState.SI && this.encoding.AutomaticShiftInShiftOut)
					{
						num++;
						streamState = HisConverter.StreamState.SO;
					}
					i += 2;
					num += 2;
					positionFlags = HisConverter.PositionFlags.AtDoubleByte;
				}
				else if ((this.encoding.AutomaticShiftInShiftOut && (src[i + index] == 14 || src[i + index] == 15) && this.encoding.UseEnhancedEbcdicTable) || src[i + index] == 31 || src[i + index] == 30)
				{
					positionFlags = HisConverter.PositionFlags.AtSingleByte;
					if (src[i + index] == 14 || src[i + index] == 30)
					{
						streamState = HisConverter.StreamState.SO;
					}
					if (src[i + index] == 15 || src[i + index] == 31)
					{
						streamState = HisConverter.StreamState.SI;
					}
					i++;
					num++;
				}
				else
				{
					if (streamState == HisConverter.StreamState.SO && this.encoding.AutomaticShiftInShiftOut)
					{
						num++;
						streamState = HisConverter.StreamState.SI;
					}
					i++;
					num++;
					positionFlags = HisConverter.PositionFlags.AtSingleByte;
				}
			}
			if (flush && streamState == HisConverter.StreamState.SO && this.encoding.AutomaticShiftInShiftOut)
			{
				num++;
				streamState = HisConverter.StreamState.SI;
			}
			int num3 = num;
			flags = positionFlags;
			state = streamState;
			return num3;
		}

		// Token: 0x060035B3 RID: 13747 RVA: 0x000B5584 File Offset: 0x000B3784
		protected static BinaryReader GetDataBinaryReader(string resourceName)
		{
			string text = HisConverter.installPath.Value + "\\" + resourceName;
			BinaryReader binaryReader = null;
			try
			{
				binaryReader = new BinaryReader(new FileStream(text, FileMode.Open, FileAccess.Read));
			}
			catch (FileNotFoundException)
			{
				Assembly executingAssembly = Assembly.GetExecutingAssembly();
				string text2 = "Microsoft.HostIntegration.StrictResources.Nls." + resourceName.ToLower();
				Stream manifestResourceStream = executingAssembly.GetManifestResourceStream(text2);
				if (manifestResourceStream != null)
				{
					binaryReader = new BinaryReader(manifestResourceStream);
				}
			}
			if (binaryReader == null)
			{
				throw new NotSupportedException(SR.CodePageTableNotFound(text), new FileLoadException(SR.CodePageTableNotFound(text)));
			}
			return binaryReader;
		}

		// Token: 0x02000639 RID: 1593
		private enum BufferSizes
		{
			// Token: 0x04001EC7 RID: 7879
			InMax = 2147483647,
			// Token: 0x04001EC8 RID: 7880
			OutMax = 2147483647,
			// Token: 0x04001EC9 RID: 7881
			DefaultBufferSize = 100
		}

		// Token: 0x0200063A RID: 1594
		private enum ShiftInOutFlags
		{
			// Token: 0x04001ECB RID: 7883
			ShiftOut = 14,
			// Token: 0x04001ECC RID: 7884
			ShiftIn,
			// Token: 0x04001ECD RID: 7885
			IBMShiftOut = 30,
			// Token: 0x04001ECE RID: 7886
			IBMShiftIn
		}

		// Token: 0x0200063B RID: 1595
		protected enum FileLoadStatus
		{
			// Token: 0x04001ED0 RID: 7888
			Uninitialized,
			// Token: 0x04001ED1 RID: 7889
			LoadedSingleByteTableOnly,
			// Token: 0x04001ED2 RID: 7890
			LoadedBothTables
		}
	}
}
