using System;
using System.Globalization;

namespace Microsoft.InfoNav.Common
{
	// Token: 0x0200007C RID: 124
	public sealed class Version : IComparable<Version>, IEquatable<Version>
	{
		// Token: 0x0600049A RID: 1178 RVA: 0x0000BFDF File Offset: 0x0000A1DF
		public Version(int major, int minor, int patch = 0, string prerel = null)
		{
			this._major = major;
			this._minor = minor;
			this._patch = patch;
			this._prerel = prerel;
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600049B RID: 1179 RVA: 0x0000C004 File Offset: 0x0000A204
		public int Major
		{
			get
			{
				return this._major;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600049C RID: 1180 RVA: 0x0000C00C File Offset: 0x0000A20C
		public int Minor
		{
			get
			{
				return this._minor;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600049D RID: 1181 RVA: 0x0000C014 File Offset: 0x0000A214
		public int Patch
		{
			get
			{
				return this._patch;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600049E RID: 1182 RVA: 0x0000C01C File Offset: 0x0000A21C
		public string Prerel
		{
			get
			{
				return this._prerel;
			}
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x0000C024 File Offset: 0x0000A224
		public static bool operator ==(Version version1, Version version2)
		{
			return Version.CompareTo(version1, version2) == 0;
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x0000C030 File Offset: 0x0000A230
		public static bool operator !=(Version version1, Version version2)
		{
			return !(version1 == version2);
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x0000C03C File Offset: 0x0000A23C
		public static bool operator >(Version version1, Version version2)
		{
			return Version.CompareTo(version1, version2) > 0;
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x0000C048 File Offset: 0x0000A248
		public static bool operator >=(Version version1, Version version2)
		{
			return Version.CompareTo(version1, version2) >= 0;
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x0000C057 File Offset: 0x0000A257
		public static bool operator <(Version version1, Version version2)
		{
			return Version.CompareTo(version1, version2) < 0;
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x0000C063 File Offset: 0x0000A263
		public static bool operator <=(Version version1, Version version2)
		{
			return Version.CompareTo(version1, version2) <= 0;
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x0000C074 File Offset: 0x0000A274
		internal static bool TryParse(string versionString, out Version version)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			string text = null;
			Version.Part part = Version.Part.Major;
			int i = 0;
			int num4 = 0;
			int length = versionString.Length;
			while (i <= length)
			{
				char c = ((i == length) ? '.' : versionString[i]);
				if (c == '-' || c == '.')
				{
					if (i == num4)
					{
						version = null;
						return false;
					}
					int num5;
					if (!int.TryParse(versionString.Substring(num4, i - num4), NumberStyles.Integer, CultureInfo.InvariantCulture, out num5))
					{
						version = null;
						return false;
					}
					switch (part)
					{
					case Version.Part.Major:
						num = num5;
						break;
					case Version.Part.Minor:
						num2 = num5;
						break;
					case Version.Part.Patch:
						num3 = num5;
						break;
					default:
						version = null;
						return false;
					}
					part++;
					num4 = i + 1;
					if (c == '-')
					{
						if (part != Version.Part.Prerel)
						{
							version = null;
							return false;
						}
						text = versionString.Remove(0, i + 1);
						if (!Version.ValidPrerel(text))
						{
							version = null;
							return false;
						}
						i = length;
					}
				}
				else if (c < '0' || c > '9')
				{
					version = null;
					return false;
				}
				i++;
			}
			version = new Version(num, num2, num3, text);
			return true;
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x0000C180 File Offset: 0x0000A380
		private static bool ValidPrerel(string prerel)
		{
			foreach (char c in prerel)
			{
				if (('A' > c || c > 'Z') && ('a' > c || c > 'z') && ('0' > c || c > '9') && c != '-' && c != '.')
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x0000C1D4 File Offset: 0x0000A3D4
		public override string ToString()
		{
			if (string.IsNullOrEmpty(this._prerel))
			{
				return StringUtil.FormatInvariant("{0}.{1}.{2}", this._major, this._minor, this._patch);
			}
			return StringUtil.FormatInvariant("{0}.{1}.{2}-{3}", new object[] { this._major, this._minor, this._patch, this._prerel });
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x0000C25D File Offset: 0x0000A45D
		public int CompareTo(Version other)
		{
			return Version.CompareTo(this, other);
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x0000C268 File Offset: 0x0000A468
		private static int? ParseIntNullable(string str)
		{
			int num;
			if (int.TryParse(str, NumberStyles.Integer, CultureInfo.InvariantCulture, out num))
			{
				return new int?(num);
			}
			return null;
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x0000C295 File Offset: 0x0000A495
		public override int GetHashCode()
		{
			return Hashing.CombineHash(this._major, this._minor, this._patch, Version._prerelComparer.GetHashCode(this._prerel ?? string.Empty));
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x0000C2C7 File Offset: 0x0000A4C7
		public override bool Equals(object obj)
		{
			return this.Equals(obj as Version);
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x0000C2D5 File Offset: 0x0000A4D5
		public bool Equals(Version other)
		{
			return other != null && this.CompareTo(other) == 0;
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x0000C2E8 File Offset: 0x0000A4E8
		private static int CompareTo(Version version1, Version version2)
		{
			if (version1 == version2)
			{
				return 0;
			}
			if (version1 == null)
			{
				return -1;
			}
			if (version2 == null)
			{
				return 1;
			}
			int num = version1._major.CompareTo(version2._major);
			if (num != 0)
			{
				return num;
			}
			num = version1._minor.CompareTo(version2._minor);
			if (num != 0)
			{
				return num;
			}
			num = version1._patch.CompareTo(version2._patch);
			if (num != 0)
			{
				return num;
			}
			return Version.ComparePrerel(version1._prerel, version2._prerel);
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x0000C364 File Offset: 0x0000A564
		private static int ComparePrerel(string v1, string v2)
		{
			int? num = Version.ParseIntNullable(v1);
			int? num2 = Version.ParseIntNullable(v2);
			if (num != null)
			{
				if (num2 != null)
				{
					return num.Value.CompareTo(num2.Value);
				}
				return -1;
			}
			else
			{
				if (num2 != null)
				{
					return 1;
				}
				return Math.Sign(-Version._prerelComparer.Compare(v1, v2));
			}
		}

		// Token: 0x0400010C RID: 268
		private static readonly StringComparer _prerelComparer = StringComparer.Ordinal;

		// Token: 0x0400010D RID: 269
		private readonly int _major;

		// Token: 0x0400010E RID: 270
		private readonly int _minor;

		// Token: 0x0400010F RID: 271
		private readonly int _patch;

		// Token: 0x04000110 RID: 272
		private readonly string _prerel;

		// Token: 0x020000CC RID: 204
		private enum Part
		{
			// Token: 0x04000214 RID: 532
			Major,
			// Token: 0x04000215 RID: 533
			Minor,
			// Token: 0x04000216 RID: 534
			Patch,
			// Token: 0x04000217 RID: 535
			Prerel
		}
	}
}
