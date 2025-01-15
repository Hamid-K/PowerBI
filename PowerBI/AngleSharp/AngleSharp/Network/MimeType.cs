using System;
using System.Collections.Generic;
using System.Linq;
using AngleSharp.Extensions;

namespace AngleSharp.Network
{
	// Token: 0x02000097 RID: 151
	public class MimeType : IEquatable<MimeType>
	{
		// Token: 0x0600047A RID: 1146 RVA: 0x0001C3E0 File Offset: 0x0001A5E0
		public MimeType(string value)
		{
			int num = 0;
			while (num < value.Length && value[num] != '/')
			{
				num++;
			}
			int num2 = num;
			while (num2 < value.Length && value[num2] != '+')
			{
				num2++;
			}
			int num3 = ((num2 < value.Length) ? num2 : num);
			while (num3 < value.Length && value[num3] != ';')
			{
				num3++;
			}
			this._general = value.Substring(0, num);
			this._media = ((num < value.Length) ? value.Substring(num + 1, Math.Min(num2, num3) - num - 1) : string.Empty);
			this._suffix = ((num2 < value.Length) ? value.Substring(num2 + 1, num3 - num2 - 1) : string.Empty);
			this._params = ((num3 < value.Length) ? value.Substring(num3 + 1).StripLeadingTrailingSpaces() : string.Empty);
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x0001C4D4 File Offset: 0x0001A6D4
		public string Content
		{
			get
			{
				if (this._media.Length != 0 || this._suffix.Length != 0)
				{
					string text = this._general + "/" + this._media;
					string text2 = ((this._suffix.Length > 0) ? ("+" + this._suffix) : string.Empty);
					return text + text2;
				}
				return this._general;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x0001C544 File Offset: 0x0001A744
		public string GeneralType
		{
			get
			{
				return this._general;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x0001C54C File Offset: 0x0001A74C
		public string MediaType
		{
			get
			{
				return this._media;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x0001C554 File Offset: 0x0001A754
		public string Suffix
		{
			get
			{
				return this._suffix;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x0001C55C File Offset: 0x0001A75C
		public IEnumerable<string> Keys
		{
			get
			{
				return (from m in this._params.Split(new char[] { ';' })
					where !string.IsNullOrEmpty(m)
					select m).Select(delegate(string m)
				{
					if (m.IndexOf('=') < 0)
					{
						return m;
					}
					return m.Substring(0, m.IndexOf('='));
				});
			}
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x0001C5C8 File Offset: 0x0001A7C8
		public string GetParameter(string key)
		{
			return (from m in this._params.Split(new char[] { ';' })
				where m.StartsWith(key + "=")
				select m.Substring(m.IndexOf('=') + 1)).FirstOrDefault<string>();
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x0001C634 File Offset: 0x0001A834
		public override string ToString()
		{
			if (this._media.Length != 0 || this._suffix.Length != 0 || this._params.Length != 0)
			{
				string text = this._general + "/" + this._media;
				string text2 = ((this._suffix.Length > 0) ? ("+" + this._suffix) : string.Empty);
				string text3 = ((this._params.Length > 0) ? (";" + this._params) : string.Empty);
				return text + text2 + text3;
			}
			return this._general;
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x0001C6D8 File Offset: 0x0001A8D8
		public bool Equals(MimeType other)
		{
			return this._general.Isi(other._general) && this._media.Isi(other._media) && this._suffix.Isi(other._suffix);
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x0001C714 File Offset: 0x0001A914
		public override bool Equals(object obj)
		{
			if (this != obj)
			{
				MimeType mimeType = obj as MimeType;
				return mimeType != null && this.Equals(mimeType);
			}
			return true;
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x0001C740 File Offset: 0x0001A940
		public override int GetHashCode()
		{
			return (this._general.GetHashCode() << 2) ^ (this._media.GetHashCode() << 1) ^ this._suffix.GetHashCode();
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x0001C769 File Offset: 0x0001A969
		public static bool operator ==(MimeType a, MimeType b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x0001C772 File Offset: 0x0001A972
		public static bool operator !=(MimeType a, MimeType b)
		{
			return !a.Equals(b);
		}

		// Token: 0x0400038B RID: 907
		private readonly string _general;

		// Token: 0x0400038C RID: 908
		private readonly string _media;

		// Token: 0x0400038D RID: 909
		private readonly string _suffix;

		// Token: 0x0400038E RID: 910
		private readonly string _params;
	}
}
