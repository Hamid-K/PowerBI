using System;
using System.Net.Http.Headers;

namespace System.Net.Http.Formatting
{
	// Token: 0x02000049 RID: 73
	internal struct ParsedMediaTypeHeaderValue
	{
		// Token: 0x060002D6 RID: 726 RVA: 0x00009CB0 File Offset: 0x00007EB0
		public ParsedMediaTypeHeaderValue(MediaTypeHeaderValue mediaTypeHeaderValue)
		{
			string text = (this._mediaType = mediaTypeHeaderValue.MediaType);
			this._delimiterIndex = text.IndexOf('/');
			this._isAllMediaRange = false;
			this._isSubtypeMediaRange = false;
			int length = text.Length;
			if (this._delimiterIndex == length - 2 && text[length - 1] == '*')
			{
				this._isSubtypeMediaRange = true;
				if (this._delimiterIndex == 1 && text[0] == '*')
				{
					this._isAllMediaRange = true;
				}
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x00009D2A File Offset: 0x00007F2A
		public bool IsAllMediaRange
		{
			get
			{
				return this._isAllMediaRange;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x00009D32 File Offset: 0x00007F32
		public bool IsSubtypeMediaRange
		{
			get
			{
				return this._isSubtypeMediaRange;
			}
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00009D3A File Offset: 0x00007F3A
		public bool TypesEqual(ref ParsedMediaTypeHeaderValue other)
		{
			return this._delimiterIndex == other._delimiterIndex && string.Compare(this._mediaType, 0, other._mediaType, 0, this._delimiterIndex, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00009D6C File Offset: 0x00007F6C
		public bool SubTypesEqual(ref ParsedMediaTypeHeaderValue other)
		{
			int num = this._mediaType.Length - this._delimiterIndex - 1;
			return num == other._mediaType.Length - other._delimiterIndex - 1 && string.Compare(this._mediaType, this._delimiterIndex + 1, other._mediaType, other._delimiterIndex + 1, num, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x040000D0 RID: 208
		private const char MediaRangeAsterisk = '*';

		// Token: 0x040000D1 RID: 209
		private const char MediaTypeSubtypeDelimiter = '/';

		// Token: 0x040000D2 RID: 210
		private readonly string _mediaType;

		// Token: 0x040000D3 RID: 211
		private readonly int _delimiterIndex;

		// Token: 0x040000D4 RID: 212
		private readonly bool _isAllMediaRange;

		// Token: 0x040000D5 RID: 213
		private readonly bool _isSubtypeMediaRange;
	}
}
