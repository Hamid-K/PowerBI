using System;
using System.Security.Cryptography;
using System.Text;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x020011B5 RID: 4533
	internal abstract class Alias
	{
		// Token: 0x170020C0 RID: 8384
		// (get) Token: 0x060077E1 RID: 30689 RVA: 0x001A0491 File Offset: 0x0019E691
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170020C1 RID: 8385
		// (get) Token: 0x060077E2 RID: 30690 RVA: 0x001A0491 File Offset: 0x0019E691
		public virtual string OriginalName
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170020C2 RID: 8386
		// (get) Token: 0x060077E3 RID: 30691 RVA: 0x001A0499 File Offset: 0x0019E699
		public bool IsEmpty
		{
			get
			{
				return string.IsNullOrEmpty(this.name);
			}
		}

		// Token: 0x170020C3 RID: 8387
		// (get) Token: 0x060077E4 RID: 30692
		public abstract bool IsMitigated { get; }

		// Token: 0x060077E5 RID: 30693 RVA: 0x001A04A6 File Offset: 0x0019E6A6
		public static Alias NewNativeAlias(string name)
		{
			return new Alias.NativeAlias(name);
		}

		// Token: 0x060077E6 RID: 30694 RVA: 0x001A04AE File Offset: 0x0019E6AE
		public static Alias NewNativeAlias(ConstantSqlString name)
		{
			return new Alias.NativeAlias(name.String);
		}

		// Token: 0x060077E7 RID: 30695 RVA: 0x001A04BC File Offset: 0x0019E6BC
		public static Alias NewAssignedAlias(string name, SqlSettings sqlSettings)
		{
			return new Alias.AssignedAlias(name, sqlSettings);
		}

		// Token: 0x060077E8 RID: 30696 RVA: 0x001A04C5 File Offset: 0x0019E6C5
		public static bool TryNewAssignedAlias(string name, SqlSettings sqlSettings, out Alias alias)
		{
			alias = new Alias.AssignedAlias(name, sqlSettings);
			if (alias.Name.IndexOfAny(sqlSettings.InvalidIdentifierCharacters) != -1)
			{
				alias = null;
				return false;
			}
			return true;
		}

		// Token: 0x060077E9 RID: 30697 RVA: 0x001A04EC File Offset: 0x0019E6EC
		private static string MakeSqlIdentifier(string identifier, SqlSettings sqlSettings)
		{
			if (identifier.Length == 0)
			{
				return "_";
			}
			string text = Alias.ReplaceInvalidCharacters(identifier, sqlSettings);
			if (text.Length > sqlSettings.MaxIdentifierLength || !(text == identifier))
			{
				return Alias.GetUniqueIdentifier(text, identifier, sqlSettings.MaxIdentifierLength);
			}
			return text;
		}

		// Token: 0x060077EA RID: 30698 RVA: 0x001A0538 File Offset: 0x0019E738
		private static string ReplaceInvalidCharacters(string identifier, SqlSettings sqlSettings)
		{
			foreach (char c in sqlSettings.InvalidIdentifierCharacters)
			{
				identifier = identifier.Replace(c, '_');
			}
			return identifier;
		}

		// Token: 0x060077EB RID: 30699 RVA: 0x001A056C File Offset: 0x0019E76C
		private static string GetUniqueIdentifier(string identifier, string originalIdentifier, int maxLength)
		{
			string text = Convert.ToBase64String(Alias.sha.ComputeHash(Encoding.UTF8.GetBytes(originalIdentifier)));
			int num = maxLength - 28;
			if (num < 0)
			{
				return text.Substring(0, maxLength);
			}
			return ((identifier.Length > num) ? identifier.Substring(0, num) : identifier) + text;
		}

		// Token: 0x060077EC RID: 30700 RVA: 0x001A05C0 File Offset: 0x0019E7C0
		public override bool Equals(object obj)
		{
			Alias alias = obj as Alias;
			return alias != null && this.name == alias.name;
		}

		// Token: 0x060077ED RID: 30701 RVA: 0x001A05EA File Offset: 0x0019E7EA
		public override int GetHashCode()
		{
			return this.name.GetHashCode();
		}

		// Token: 0x0400411C RID: 16668
		private const string DefaultIdentifier = "_";

		// Token: 0x0400411D RID: 16669
		private const int IdentifierHashCodeLength = 28;

		// Token: 0x0400411E RID: 16670
		private static readonly SHA1 sha = new SHA1CryptoServiceProvider();

		// Token: 0x0400411F RID: 16671
		private string name;

		// Token: 0x04004120 RID: 16672
		public static readonly Alias Underscore = Alias.NewNativeAlias("_");

		// Token: 0x04004121 RID: 16673
		public static readonly Alias InnerSource = Alias.NewNativeAlias("$Inner");

		// Token: 0x04004122 RID: 16674
		public static readonly Alias OrderedSource = Alias.NewNativeAlias("$Ordered");

		// Token: 0x04004123 RID: 16675
		public static readonly Alias OuterSource = Alias.NewNativeAlias("$Outer");

		// Token: 0x04004124 RID: 16676
		public static readonly Alias ScalarColumn = Alias.NewNativeAlias("$Item");

		// Token: 0x04004125 RID: 16677
		public static readonly Alias VirtualTable = Alias.NewNativeAlias("$Table");

		// Token: 0x04004126 RID: 16678
		public static readonly Alias TicksPerDay = Alias.NewNativeAlias("$tpd");

		// Token: 0x04004127 RID: 16679
		public static readonly Alias LeftSource = Alias.NewNativeAlias("L");

		// Token: 0x04004128 RID: 16680
		public static readonly Alias RightSource = Alias.NewNativeAlias("R");

		// Token: 0x04004129 RID: 16681
		public static readonly Alias PagedRowNumberName = Alias.NewNativeAlias("$Row");

		// Token: 0x0400412A RID: 16682
		public static readonly Alias PagedSource = Alias.NewNativeAlias("$Paged");

		// Token: 0x0400412B RID: 16683
		public static readonly Alias PivotSource = Alias.NewNativeAlias("$Pivot");

		// Token: 0x0400412C RID: 16684
		public static readonly Alias Temp = Alias.NewNativeAlias("$temp");

		// Token: 0x0400412D RID: 16685
		public static readonly Alias Inserted = Alias.NewNativeAlias(SqlLanguageStrings.InsertedSqlString);

		// Token: 0x0400412E RID: 16686
		public static readonly Alias Deleted = Alias.NewNativeAlias(SqlLanguageStrings.DeletedSqlString);

		// Token: 0x020011B6 RID: 4534
		private sealed class NativeAlias : Alias
		{
			// Token: 0x060077F0 RID: 30704 RVA: 0x001A06F0 File Offset: 0x0019E8F0
			public NativeAlias(string name)
			{
				this.name = name;
			}

			// Token: 0x170020C4 RID: 8388
			// (get) Token: 0x060077F1 RID: 30705 RVA: 0x00002105 File Offset: 0x00000305
			public override bool IsMitigated
			{
				get
				{
					return false;
				}
			}
		}

		// Token: 0x020011B7 RID: 4535
		private sealed class AssignedAlias : Alias
		{
			// Token: 0x060077F2 RID: 30706 RVA: 0x001A06FF File Offset: 0x0019E8FF
			public AssignedAlias(string name, SqlSettings sqlSettings)
			{
				this.name = Alias.MakeSqlIdentifier(name, sqlSettings);
				this.isMitigated = name != this.name;
				this.originalName = name;
			}

			// Token: 0x170020C5 RID: 8389
			// (get) Token: 0x060077F3 RID: 30707 RVA: 0x001A072D File Offset: 0x0019E92D
			public override string OriginalName
			{
				get
				{
					return this.originalName;
				}
			}

			// Token: 0x170020C6 RID: 8390
			// (get) Token: 0x060077F4 RID: 30708 RVA: 0x001A0735 File Offset: 0x0019E935
			public override bool IsMitigated
			{
				get
				{
					return this.isMitigated;
				}
			}

			// Token: 0x0400412F RID: 16687
			private readonly bool isMitigated;

			// Token: 0x04004130 RID: 16688
			private readonly string originalName;
		}
	}
}
