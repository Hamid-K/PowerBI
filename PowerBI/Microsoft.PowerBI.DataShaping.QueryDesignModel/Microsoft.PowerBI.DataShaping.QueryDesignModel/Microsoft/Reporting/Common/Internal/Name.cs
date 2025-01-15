using System;
using System.Globalization;

namespace Microsoft.Reporting.Common.Internal
{
	// Token: 0x02000287 RID: 647
	public struct Name : ICheckable, IEquatable<Name>, IEquatable<string>
	{
		// Token: 0x06001B9D RID: 7069 RVA: 0x0004D603 File Offset: 0x0004B803
		public Name(string value)
		{
			this._value = value;
		}

		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x06001B9E RID: 7070 RVA: 0x0004D60C File Offset: 0x0004B80C
		public string Value
		{
			get
			{
				return this._value ?? string.Empty;
			}
		}

		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x06001B9F RID: 7071 RVA: 0x0004D61D File Offset: 0x0004B81D
		public bool IsValid
		{
			get
			{
				return this._value != null;
			}
		}

		// Token: 0x06001BA0 RID: 7072 RVA: 0x0004D628 File Offset: 0x0004B828
		public static implicit operator string(Name name)
		{
			return name.Value;
		}

		// Token: 0x06001BA1 RID: 7073 RVA: 0x0004D631 File Offset: 0x0004B831
		public static explicit operator Name(string value)
		{
			return new Name(value);
		}

		// Token: 0x06001BA2 RID: 7074 RVA: 0x0004D639 File Offset: 0x0004B839
		public override string ToString()
		{
			return this.Value;
		}

		// Token: 0x06001BA3 RID: 7075 RVA: 0x0004D641 File Offset: 0x0004B841
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x06001BA4 RID: 7076 RVA: 0x0004D64E File Offset: 0x0004B84E
		public override bool Equals(object obj)
		{
			return obj is Name && this.Equals((Name)obj);
		}

		// Token: 0x06001BA5 RID: 7077 RVA: 0x0004D666 File Offset: 0x0004B866
		public bool Equals(Name other)
		{
			return this.Value == other.Value;
		}

		// Token: 0x06001BA6 RID: 7078 RVA: 0x0004D67A File Offset: 0x0004B87A
		public bool Equals(string other)
		{
			return this.Value == other;
		}

		// Token: 0x06001BA7 RID: 7079 RVA: 0x0004D688 File Offset: 0x0004B888
		public static bool operator ==(Name name1, Name name2)
		{
			return name1.Value == name2.Value;
		}

		// Token: 0x06001BA8 RID: 7080 RVA: 0x0004D69D File Offset: 0x0004B89D
		public static bool operator ==(string str, Name name)
		{
			return str == name.Value;
		}

		// Token: 0x06001BA9 RID: 7081 RVA: 0x0004D6AC File Offset: 0x0004B8AC
		public static bool operator ==(Name name, string str)
		{
			return name.Value == str;
		}

		// Token: 0x06001BAA RID: 7082 RVA: 0x0004D6BB File Offset: 0x0004B8BB
		public static bool operator !=(Name name1, Name name2)
		{
			return name1.Value != name2.Value;
		}

		// Token: 0x06001BAB RID: 7083 RVA: 0x0004D6D0 File Offset: 0x0004B8D0
		public static bool operator !=(string str, Name name)
		{
			return str != name.Value;
		}

		// Token: 0x06001BAC RID: 7084 RVA: 0x0004D6DF File Offset: 0x0004B8DF
		public static bool operator !=(Name name, string str)
		{
			return name.Value != str;
		}

		// Token: 0x06001BAD RID: 7085 RVA: 0x0004D6F0 File Offset: 0x0004B8F0
		public static bool IsValidDName(string strName)
		{
			if (string.IsNullOrEmpty(strName))
			{
				return false;
			}
			for (int i = 0; i < strName.Length; i++)
			{
				if (!Name.IsValidDNameChar(strName[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001BAE RID: 7086 RVA: 0x0004D72C File Offset: 0x0004B92C
		public static bool IsValidDNameChar(char ch)
		{
			UnicodeCategory unicodeCategory = char.GetUnicodeCategory(ch);
			if (unicodeCategory != UnicodeCategory.SpaceSeparator)
			{
				return (unicodeCategory - UnicodeCategory.Control > 3 && unicodeCategory != UnicodeCategory.OtherNotAssigned) || CharacterUtils.IsLineTerminator(ch);
			}
			return ch == ' ';
		}

		// Token: 0x04000F2A RID: 3882
		private const char ChSpace = ' ';

		// Token: 0x04000F2B RID: 3883
		private readonly string _value;
	}
}
