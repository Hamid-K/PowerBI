using System;
using System.Globalization;
using System.Text.RegularExpressions;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000168 RID: 360
	[CannotApplyEqualityOperator]
	public class ActivityType : IEquatable<ActivityType>
	{
		// Token: 0x0600095B RID: 2395 RVA: 0x00020354 File Offset: 0x0001E554
		static ActivityType()
		{
			byte b = Convert.ToByte('0');
			int i = 0;
			while (i < 10)
			{
				ActivityType.s_isValidChar[(int)b] = true;
				i++;
				b += 1;
			}
			b = Convert.ToByte('A');
			int j = 0;
			while (j < 26)
			{
				ActivityType.s_isValidChar[(int)b] = true;
				j++;
				b += 1;
			}
			ActivityType.s_shortNameRegex = new Regex("^[A-Z0-9]{4}$", RegexOptions.Compiled);
			ActivityType.s_empty = new ActivityType("0000");
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x000203D3 File Offset: 0x0001E5D3
		public ActivityType(string shortName)
		{
			this.Set(shortName);
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x000203E3 File Offset: 0x0001E5E3
		public ActivityType(int shortNameId)
		{
			this.m_shortNameId = shortNameId;
			this.m_shortName = ActivityType.ToShortName(shortNameId);
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x000203FE File Offset: 0x0001E5FE
		public ActivityType Set(string shortName)
		{
			this.m_shortName = shortName;
			this.m_shortNameId = ActivityType.ToShortNameId(shortName);
			return this;
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x0600095F RID: 2399 RVA: 0x00020414 File Offset: 0x0001E614
		public int ShortNameId
		{
			get
			{
				return this.m_shortNameId;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000960 RID: 2400 RVA: 0x0002041C File Offset: 0x0001E61C
		public string ShortName
		{
			get
			{
				return this.m_shortName;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000961 RID: 2401 RVA: 0x00020424 File Offset: 0x0001E624
		public static ActivityType Empty
		{
			get
			{
				return ActivityType.s_empty;
			}
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x0002042C File Offset: 0x0001E62C
		private static int ToShortNameId(string shortName)
		{
			if (shortName.Length != 4)
			{
				ActivityType.ValidateShortName(shortName);
			}
			int num = 0;
			for (int i = 0; i < shortName.Length; i++)
			{
				byte b = Convert.ToByte(shortName[i]);
				if (!ActivityType.s_isValidChar[(int)b])
				{
					ActivityType.ValidateShortName(shortName);
				}
				num = (num << 8) | (int)b;
			}
			return num;
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x00020480 File Offset: 0x0001E680
		private static string ToShortName(int shortNameId)
		{
			string text = string.Empty;
			if (shortNameId > 0)
			{
				char[] array = new char[4];
				for (int i = 0; i < array.Length; i++)
				{
					int num = (shortNameId >> 8 * (3 - i)) & 255;
					array[i] = Convert.ToChar(num);
				}
				text = new string(array);
				ActivityType.ValidateShortName(text);
			}
			return text;
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x000204D4 File Offset: 0x0001E6D4
		private static void ValidateShortName([NotNull] string shortName)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(shortName, "shortName");
			if (!ActivityType.s_shortNameRegex.IsMatch(shortName))
			{
				string text = string.Format(CultureInfo.CurrentCulture, "Activity type short name '{0}' is not valid", new object[] { shortName });
				throw new ArgumentOutOfRangeException("shortName", shortName, text);
			}
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x00020520 File Offset: 0x0001E720
		public bool Equals(ActivityType other)
		{
			return other != null && this.ShortNameId.Equals(other.ShortNameId);
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x00020549 File Offset: 0x0001E749
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ActivityType);
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x00020557 File Offset: 0x0001E757
		public override int GetHashCode()
		{
			return this.ShortNameId;
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x0002055F File Offset: 0x0001E75F
		public override string ToString()
		{
			return this.ShortName;
		}

		// Token: 0x04000387 RID: 903
		private const string c_emptyType = "0000";

		// Token: 0x04000388 RID: 904
		private const string c_shortNamePattern = "^[A-Z0-9]{4}$";

		// Token: 0x04000389 RID: 905
		private static Regex s_shortNameRegex;

		// Token: 0x0400038A RID: 906
		private static ActivityType s_empty;

		// Token: 0x0400038B RID: 907
		private static bool[] s_isValidChar = new bool[256];

		// Token: 0x0400038C RID: 908
		private int m_shortNameId;

		// Token: 0x0400038D RID: 909
		private string m_shortName;
	}
}
