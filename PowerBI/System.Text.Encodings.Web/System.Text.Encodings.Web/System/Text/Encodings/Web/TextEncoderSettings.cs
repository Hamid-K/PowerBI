using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.Unicode;

namespace System.Text.Encodings.Web
{
	// Token: 0x0200002D RID: 45
	[NullableContext(1)]
	[Nullable(0)]
	public class TextEncoderSettings
	{
		// Token: 0x0600019B RID: 411 RVA: 0x00006094 File Offset: 0x00004294
		public TextEncoderSettings()
		{
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000609C File Offset: 0x0000429C
		public unsafe TextEncoderSettings(TextEncoderSettings other)
		{
			if (other == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.other);
			}
			this._allowedCodePointsBitmap = *other.GetAllowedCodePointsBitmap();
		}

		// Token: 0x0600019D RID: 413 RVA: 0x000060BE File Offset: 0x000042BE
		public TextEncoderSettings(params UnicodeRange[] allowedRanges)
		{
			if (allowedRanges == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.allowedRanges);
			}
			this.AllowRanges(allowedRanges);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x000060D6 File Offset: 0x000042D6
		public virtual void AllowCharacter(char character)
		{
			this._allowedCodePointsBitmap.AllowChar(character);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x000060E4 File Offset: 0x000042E4
		public virtual void AllowCharacters(params char[] characters)
		{
			if (characters == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.characters);
			}
			for (int i = 0; i < characters.Length; i++)
			{
				this._allowedCodePointsBitmap.AllowChar(characters[i]);
			}
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00006118 File Offset: 0x00004318
		public virtual void AllowCodePoints(IEnumerable<int> codePoints)
		{
			if (codePoints == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.codePoints);
			}
			foreach (int num in codePoints)
			{
				if (UnicodeUtility.IsBmpCodePoint((uint)num))
				{
					this._allowedCodePointsBitmap.AllowChar((char)num);
				}
			}
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00006178 File Offset: 0x00004378
		public virtual void AllowRange(UnicodeRange range)
		{
			if (range == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.range);
			}
			int firstCodePoint = range.FirstCodePoint;
			int length = range.Length;
			for (int i = 0; i < length; i++)
			{
				int num = firstCodePoint + i;
				this._allowedCodePointsBitmap.AllowChar((char)num);
			}
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x000061BC File Offset: 0x000043BC
		public virtual void AllowRanges(params UnicodeRange[] ranges)
		{
			if (ranges == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.ranges);
			}
			for (int i = 0; i < ranges.Length; i++)
			{
				this.AllowRange(ranges[i]);
			}
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x000061E9 File Offset: 0x000043E9
		public virtual void Clear()
		{
			this._allowedCodePointsBitmap = default(AllowedBmpCodePointsBitmap);
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x000061F7 File Offset: 0x000043F7
		public virtual void ForbidCharacter(char character)
		{
			this._allowedCodePointsBitmap.ForbidChar(character);
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00006208 File Offset: 0x00004408
		public virtual void ForbidCharacters(params char[] characters)
		{
			if (characters == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.characters);
			}
			for (int i = 0; i < characters.Length; i++)
			{
				this._allowedCodePointsBitmap.ForbidChar(characters[i]);
			}
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000623C File Offset: 0x0000443C
		public virtual void ForbidRange(UnicodeRange range)
		{
			if (range == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.range);
			}
			int firstCodePoint = range.FirstCodePoint;
			int length = range.Length;
			for (int i = 0; i < length; i++)
			{
				int num = firstCodePoint + i;
				this._allowedCodePointsBitmap.ForbidChar((char)num);
			}
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00006280 File Offset: 0x00004480
		public virtual void ForbidRanges(params UnicodeRange[] ranges)
		{
			if (ranges == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.ranges);
			}
			for (int i = 0; i < ranges.Length; i++)
			{
				this.ForbidRange(ranges[i]);
			}
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x000062AD File Offset: 0x000044AD
		public virtual IEnumerable<int> GetAllowedCodePoints()
		{
			int num;
			for (int i = 0; i <= 65535; i = num + 1)
			{
				if (this._allowedCodePointsBitmap.IsCharAllowed((char)i))
				{
					yield return i;
				}
				num = i;
			}
			yield break;
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x000062C0 File Offset: 0x000044C0
		internal readonly ref AllowedBmpCodePointsBitmap GetAllowedCodePointsBitmap()
		{
			if (base.GetType() == typeof(TextEncoderSettings))
			{
				return ref this._allowedCodePointsBitmap;
			}
			StrongBox<AllowedBmpCodePointsBitmap> strongBox = new StrongBox<AllowedBmpCodePointsBitmap>();
			foreach (int num in this.GetAllowedCodePoints())
			{
				if (num <= 65535)
				{
					strongBox.Value.AllowChar((char)num);
				}
			}
			return ref strongBox.Value;
		}

		// Token: 0x040000D6 RID: 214
		private AllowedBmpCodePointsBitmap _allowedCodePointsBitmap;
	}
}
