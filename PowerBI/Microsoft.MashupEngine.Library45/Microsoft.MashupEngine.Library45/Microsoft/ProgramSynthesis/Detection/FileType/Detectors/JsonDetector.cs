using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Detection.FileType.Detectors
{
	// Token: 0x02000AC7 RID: 2759
	internal class JsonDetector : TextualFormatDetector
	{
		// Token: 0x17000C61 RID: 3169
		// (get) Token: 0x06004539 RID: 17721 RVA: 0x000D88CF File Offset: 0x000D6ACF
		internal override IEnumerable<FileType> SupportedFileTypes
		{
			get
			{
				return Seq.Of<FileType>(new FileType[] { FileType.Json });
			}
		}

		// Token: 0x17000C62 RID: 3170
		// (get) Token: 0x0600453A RID: 17722 RVA: 0x000D88E0 File Offset: 0x000D6AE0
		internal override IEnumerable<string> SupportedExtensions
		{
			get
			{
				return Seq.Of<string>(new string[] { "json" });
			}
		}

		// Token: 0x17000C63 RID: 3171
		// (get) Token: 0x0600453B RID: 17723 RVA: 0x0000A5FD File Offset: 0x000087FD
		internal override int Precedence
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x0600453C RID: 17724 RVA: 0x000D88F8 File Offset: 0x000D6AF8
		private bool TryParsePrefixInternal(string file, int offset, out int endOffset)
		{
			endOffset = offset;
			if (offset >= file.Length)
			{
				endOffset = file.Length;
				return true;
			}
			Match match = this._objectRegex.Match(file, offset);
			if (match.Success && match.Index == offset)
			{
				return this.TryParseObjectPrefix(file, offset + match.Length, out endOffset);
			}
			match = this._arrayRegex.Match(file, offset);
			if (match.Success && match.Index == offset)
			{
				return this.TryParseArrayPrefix(file, offset + match.Length, out endOffset);
			}
			match = this._valueRegex.Match(file, offset);
			if (match.Success && match.Index == offset)
			{
				endOffset = offset + match.Length;
				return true;
			}
			return false;
		}

		// Token: 0x0600453D RID: 17725 RVA: 0x000D89A8 File Offset: 0x000D6BA8
		private bool TryParseArrayPrefix(string file, int offset, out int endOffset)
		{
			endOffset = offset;
			Match match = this._arrayCloseRegex.Match(file, offset);
			if (match.Success && match.Index == offset)
			{
				endOffset = offset + match.Length;
				return true;
			}
			bool flag = false;
			while (offset < file.Length)
			{
				int num;
				if (!this.TryParsePrefixInternal(file, offset, out num))
				{
					endOffset = num;
					return false;
				}
				offset = num;
				if (offset >= file.Length)
				{
					endOffset = file.Length;
					return true;
				}
				Match match2 = this._commaRegex.Match(file, offset);
				if (!match2.Success || match2.Index != offset)
				{
					match2 = this._arrayCloseRegex.Match(file, offset);
					if (!match2.Success || match2.Index != offset)
					{
						endOffset = offset;
						return false;
					}
					endOffset = offset + match2.Length;
					flag = true;
				}
				else
				{
					offset += match2.Length;
				}
				if (flag)
				{
					return true;
				}
			}
			endOffset = file.Length;
			return true;
		}

		// Token: 0x0600453E RID: 17726 RVA: 0x000D8A84 File Offset: 0x000D6C84
		private bool TryParseObjectPrefix(string file, int offset, out int endOffset)
		{
			endOffset = offset;
			Match match = this._objectCloseRegex.Match(file, offset);
			if (match.Success && match.Index == offset)
			{
				endOffset = offset + match.Length;
				return true;
			}
			bool flag = false;
			while (offset < file.Length)
			{
				Match match2 = this._nonEmptyQuotedStringRegex.Match(file, offset);
				if (!match2.Success || match2.Index != offset)
				{
					endOffset = offset;
					return false;
				}
				offset += match2.Length;
				if (offset >= file.Length)
				{
					endOffset = file.Length;
					return true;
				}
				match2 = this._colonRegex.Match(file, offset);
				if (!match2.Success || match2.Index != offset)
				{
					endOffset = offset;
					return false;
				}
				offset += match2.Length;
				if (offset >= file.Length)
				{
					endOffset = file.Length;
					return true;
				}
				int num;
				if (!this.TryParsePrefixInternal(file, offset, out num))
				{
					endOffset = num;
					return false;
				}
				offset = num;
				if (offset >= file.Length)
				{
					endOffset = file.Length;
					return true;
				}
				match2 = this._commaRegex.Match(file, offset);
				if (!match2.Success || match2.Index != offset)
				{
					match2 = this._objectCloseRegex.Match(file, offset);
					if (!match2.Success || match2.Index != offset)
					{
						endOffset = offset;
						return false;
					}
					endOffset = offset + match2.Length;
					flag = true;
				}
				else
				{
					offset += match2.Length;
				}
				if (flag)
				{
					return true;
				}
			}
			endOffset = file.Length;
			return true;
		}

		// Token: 0x0600453F RID: 17727 RVA: 0x000D8BE0 File Offset: 0x000D6DE0
		private bool TryParsePrefix(string file)
		{
			Match match = this._objectRegex.Match(file, 0);
			if (!match.Success || match.Index > 1)
			{
				match = this._arrayRegex.Match(file, 0);
				if (!match.Success || match.Index > 0)
				{
					return false;
				}
			}
			int num;
			return this.TryParsePrefixInternal(file, match.Index, out num) || (float)num / (float)file.Length > 0.5f;
		}

		// Token: 0x06004540 RID: 17728 RVA: 0x000D8C54 File Offset: 0x000D6E54
		internal override FileType MatchFormat(FileTypeIdentifier caller, string header, string footer)
		{
			if (header.Length > 32768)
			{
				header = header.Substring(0, 32768);
			}
			int num = -1;
			for (int i = header.Length - 1; i >= 0; i--)
			{
				if (header[i] == ']' || header[i] == '}' || header[i] == ',' || header[i] == ':')
				{
					num = i + 1;
					break;
				}
			}
			if (num < 0)
			{
				return FileType.Unknown;
			}
			if (!this.TryParsePrefix(header.Substring(0, num)))
			{
				return FileType.Unknown;
			}
			return FileType.Json;
		}

		// Token: 0x04001F9E RID: 8094
		private const int HeaderSize = 32768;

		// Token: 0x04001F9F RID: 8095
		private const float Threshold = 0.5f;

		// Token: 0x04001FA0 RID: 8096
		private readonly Regex _objectRegex = new Regex("\\s*\\{", RegexOptions.Compiled);

		// Token: 0x04001FA1 RID: 8097
		private readonly Regex _arrayRegex = new Regex("\\s*\\[", RegexOptions.Compiled);

		// Token: 0x04001FA2 RID: 8098
		private readonly Regex _objectCloseRegex = new Regex("\\s*\\}", RegexOptions.Compiled);

		// Token: 0x04001FA3 RID: 8099
		private readonly Regex _arrayCloseRegex = new Regex("\\s*\\]", RegexOptions.Compiled);

		// Token: 0x04001FA4 RID: 8100
		private readonly Regex _nonEmptyQuotedStringRegex = new Regex("\\s*\"([^\\\\\n\"]|(\\\\.))+\"", RegexOptions.ExplicitCapture | RegexOptions.Compiled);

		// Token: 0x04001FA5 RID: 8101
		private readonly Regex _colonRegex = new Regex("\\s*:", RegexOptions.Compiled);

		// Token: 0x04001FA6 RID: 8102
		private readonly Regex _valueRegex = new Regex("\\s*((\"([^\\\\\n\"]|(\\\\.))*\")|(-?\\d+(\\.\\d+((e|E)(\\+|-)?\\d+)?)?)|true|false|null)", RegexOptions.ExplicitCapture | RegexOptions.Compiled);

		// Token: 0x04001FA7 RID: 8103
		private readonly Regex _commaRegex = new Regex("\\s*,", RegexOptions.Compiled);
	}
}
