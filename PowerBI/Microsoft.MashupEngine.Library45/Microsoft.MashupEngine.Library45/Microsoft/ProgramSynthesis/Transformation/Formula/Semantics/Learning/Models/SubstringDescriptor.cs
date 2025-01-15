using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Models
{
	// Token: 0x020016E2 RID: 5858
	public class SubstringDescriptor : ProgramDescriptor, IEquatable<SubstringDescriptor>
	{
		// Token: 0x17002141 RID: 8513
		// (get) Token: 0x0600C354 RID: 50004 RVA: 0x002A0B18 File Offset: 0x0029ED18
		public int EndIndex
		{
			get
			{
				int num = this._endIndex.GetValueOrDefault();
				if (this._endIndex == null)
				{
					num = this.StartIndex + this.Substring.Length - 1;
					this._endIndex = new int?(num);
					return num;
				}
				return num;
			}
		}

		// Token: 0x17002142 RID: 8514
		// (get) Token: 0x0600C355 RID: 50005 RVA: 0x002A0B62 File Offset: 0x0029ED62
		public bool Infix
		{
			get
			{
				return !this.Prefix && !this.Suffix;
			}
		}

		// Token: 0x17002143 RID: 8515
		// (get) Token: 0x0600C356 RID: 50006 RVA: 0x002A0B77 File Offset: 0x0029ED77
		// (set) Token: 0x0600C357 RID: 50007 RVA: 0x002A0B7F File Offset: 0x0029ED7F
		public string Input { get; set; }

		// Token: 0x17002144 RID: 8516
		// (get) Token: 0x0600C358 RID: 50008 RVA: 0x002A0B88 File Offset: 0x0029ED88
		// (set) Token: 0x0600C359 RID: 50009 RVA: 0x002A0B90 File Offset: 0x0029ED90
		public StringInput InputColumn
		{
			get
			{
				return this._inputColumn;
			}
			set
			{
				this._toString = null;
				this._inputColumn = value;
			}
		}

		// Token: 0x17002145 RID: 8517
		// (get) Token: 0x0600C35A RID: 50010 RVA: 0x002A0BA0 File Offset: 0x0029EDA0
		// (set) Token: 0x0600C35B RID: 50011 RVA: 0x002A0BA8 File Offset: 0x0029EDA8
		public string Output { get; set; }

		// Token: 0x17002146 RID: 8518
		// (get) Token: 0x0600C35C RID: 50012 RVA: 0x002A0BB1 File Offset: 0x0029EDB1
		public bool Prefix
		{
			get
			{
				return this.StartIndex == 0;
			}
		}

		// Token: 0x17002147 RID: 8519
		// (get) Token: 0x0600C35D RID: 50013 RVA: 0x002A0BBC File Offset: 0x0029EDBC
		// (set) Token: 0x0600C35E RID: 50014 RVA: 0x002A0BC4 File Offset: 0x0029EDC4
		public int StartIndex { get; set; }

		// Token: 0x17002148 RID: 8520
		// (get) Token: 0x0600C35F RID: 50015 RVA: 0x002A0BD0 File Offset: 0x0029EDD0
		public string Substring
		{
			get
			{
				string text;
				if ((text = this._substring) == null)
				{
					text = (this._substring = this.Input.Slice(new int?(this.StartIndex), new int?(this.StartIndex + this.Output.Length), 1));
				}
				return text;
			}
		}

		// Token: 0x17002149 RID: 8521
		// (get) Token: 0x0600C360 RID: 50016 RVA: 0x002A0C20 File Offset: 0x0029EE20
		public bool Suffix
		{
			get
			{
				bool flag = this._suffix.GetValueOrDefault();
				if (this._suffix == null)
				{
					flag = this.EndIndex == this.Input.Length - 1;
					this._suffix = new bool?(flag);
					return flag;
				}
				return flag;
			}
		}

		// Token: 0x1700214A RID: 8522
		// (get) Token: 0x0600C361 RID: 50017 RVA: 0x002A0C6B File Offset: 0x0029EE6B
		public bool WholeColumn
		{
			get
			{
				return this.Prefix && this.Suffix;
			}
		}

		// Token: 0x0600C362 RID: 50018 RVA: 0x002A0C80 File Offset: 0x0029EE80
		public override bool IsCompatible(ProgramDescriptor otherProgramDescriptor)
		{
			SubstringDescriptor substringDescriptor = otherProgramDescriptor as SubstringDescriptor;
			return substringDescriptor != null && !(this.InputColumn.ColumnName != substringDescriptor.InputColumn.ColumnName) && ((this.WholeColumn && substringDescriptor.WholeColumn) || (this.StartIndex == substringDescriptor.StartIndex && this.EndIndex == substringDescriptor.EndIndex) || (this.AllowSplit && substringDescriptor.AllowSplit && this.SplitDescriptor == substringDescriptor.SplitDescriptor) || (this.Prefix && substringDescriptor.Prefix && this.FindEndDescriptors.Intersect(substringDescriptor.FindEndDescriptors).Any<SubstringFindProgramDescriptor>()) || (this.Suffix && substringDescriptor.Suffix && this.FindStartDescriptors.Intersect(substringDescriptor.FindStartDescriptors).Any<SubstringFindProgramDescriptor>()) || (this.Infix && substringDescriptor.Infix && this.FindStartDescriptors.Intersect(substringDescriptor.FindStartDescriptors).Any<SubstringFindProgramDescriptor>() && this.FindEndDescriptors.Intersect(substringDescriptor.FindEndDescriptors).Any<SubstringFindProgramDescriptor>()));
		}

		// Token: 0x1700214B RID: 8523
		// (get) Token: 0x0600C363 RID: 50019 RVA: 0x002A0DA4 File Offset: 0x0029EFA4
		public bool AllowFindEnd
		{
			get
			{
				IReadOnlyList<SubstringFindProgramDescriptor> findEndDescriptors = this.FindEndDescriptors;
				return findEndDescriptors != null && findEndDescriptors.Any<SubstringFindProgramDescriptor>();
			}
		}

		// Token: 0x1700214C RID: 8524
		// (get) Token: 0x0600C364 RID: 50020 RVA: 0x002A0DB7 File Offset: 0x0029EFB7
		public bool AllowFindStart
		{
			get
			{
				IReadOnlyList<SubstringFindProgramDescriptor> findStartDescriptors = this.FindStartDescriptors;
				return findStartDescriptors != null && findStartDescriptors.Any<SubstringFindProgramDescriptor>();
			}
		}

		// Token: 0x1700214D RID: 8525
		// (get) Token: 0x0600C365 RID: 50021 RVA: 0x002A0DCA File Offset: 0x0029EFCA
		public bool AllowSplit
		{
			get
			{
				return this.SplitDescriptor != null;
			}
		}

		// Token: 0x1700214E RID: 8526
		// (get) Token: 0x0600C366 RID: 50022 RVA: 0x002A0DD8 File Offset: 0x0029EFD8
		public IReadOnlyList<SubstringFindProgramDescriptor> FindEndDescriptors
		{
			get
			{
				IReadOnlyList<SubstringFindProgramDescriptor> readOnlyList;
				if ((readOnlyList = this._findEndDescriptors) == null)
				{
					readOnlyList = (this._findEndDescriptors = this.LoadFindEndDescriptors());
				}
				return readOnlyList;
			}
		}

		// Token: 0x1700214F RID: 8527
		// (get) Token: 0x0600C367 RID: 50023 RVA: 0x002A0E00 File Offset: 0x0029F000
		public IReadOnlyList<SubstringFindProgramDescriptor> FindStartDescriptors
		{
			get
			{
				IReadOnlyList<SubstringFindProgramDescriptor> readOnlyList;
				if ((readOnlyList = this._findStartDescriptors) == null)
				{
					readOnlyList = (this._findStartDescriptors = this.LoadFindStartDescriptors());
				}
				return readOnlyList;
			}
		}

		// Token: 0x17002150 RID: 8528
		// (get) Token: 0x0600C368 RID: 50024 RVA: 0x002A0E26 File Offset: 0x0029F026
		public ProgramOptionalOperator LowerCase
		{
			get
			{
				if (this._lowerCase != null)
				{
					return this._lowerCase.Value;
				}
				this.LoadCase();
				return this._lowerCase.GetValueOrDefault();
			}
		}

		// Token: 0x17002151 RID: 8529
		// (get) Token: 0x0600C369 RID: 50025 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public ProgramOptionalOperator ProperCase
		{
			get
			{
				return ProgramOptionalOperator.Unknown;
			}
		}

		// Token: 0x17002152 RID: 8530
		// (get) Token: 0x0600C36A RID: 50026 RVA: 0x002A0E54 File Offset: 0x0029F054
		public SplitProgramDescriptor SplitDescriptor
		{
			get
			{
				SplitProgramDescriptor splitProgramDescriptor;
				if ((splitProgramDescriptor = this._splitDescriptor) == null)
				{
					splitProgramDescriptor = (this._splitDescriptor = this.LoadSplitDescriptor());
				}
				return splitProgramDescriptor;
			}
		}

		// Token: 0x17002153 RID: 8531
		// (get) Token: 0x0600C36B RID: 50027 RVA: 0x002A0E7A File Offset: 0x0029F07A
		// (set) Token: 0x0600C36C RID: 50028 RVA: 0x002A0E82 File Offset: 0x0029F082
		public ProgramOptionalOperator Trim { get; set; }

		// Token: 0x17002154 RID: 8532
		// (get) Token: 0x0600C36D RID: 50029 RVA: 0x002A0E8B File Offset: 0x0029F08B
		public ProgramOptionalOperator UpperCase
		{
			get
			{
				if (this._upperCase != null)
				{
					return this._upperCase.Value;
				}
				this.LoadCase();
				return this._upperCase.GetValueOrDefault();
			}
		}

		// Token: 0x0600C36F RID: 50031 RVA: 0x002A0ECC File Offset: 0x0029F0CC
		private void LoadCase()
		{
			ReadOnlySpan<char> readOnlySpan = this.Substring.AsSpan();
			ReadOnlySpan<char> readOnlySpan2 = this.Output.AsSpan();
			bool flag = readOnlySpan.All((char c) => !char.IsLetter(c) || char.ToLowerInvariant(c) == c);
			bool flag2;
			if (!flag)
			{
				flag2 = readOnlySpan.Any((char c) => char.IsLetter(c) && char.ToLowerInvariant(c) == c);
			}
			else
			{
				flag2 = true;
			}
			bool flag3 = flag2;
			bool flag4 = readOnlySpan.All((char c) => char.ToUpperInvariant(c) == c);
			bool flag5;
			if (!flag4)
			{
				flag5 = readOnlySpan.Any((char c) => char.IsLetter(c) && char.ToUpperInvariant(c) == c);
			}
			else
			{
				flag5 = true;
			}
			bool flag6 = flag5;
			bool flag7 = readOnlySpan2.All((char c) => !char.IsLetter(c) || char.ToLowerInvariant(c) == c);
			bool flag8;
			if (!flag7)
			{
				flag8 = readOnlySpan2.Any((char c) => char.IsLetter(c) && char.ToLowerInvariant(c) == c);
			}
			else
			{
				flag8 = true;
			}
			bool flag9 = flag8;
			bool flag10 = readOnlySpan2.All((char c) => char.ToUpperInvariant(c) == c);
			bool flag11;
			if (!flag10)
			{
				flag11 = readOnlySpan2.Any((char c) => char.IsLetter(c) && char.ToUpperInvariant(c) == c);
			}
			else
			{
				flag11 = true;
			}
			this._lowerCase = new ProgramOptionalOperator?(flag11 ? ProgramOptionalOperator.Forbidden : ((flag6 && flag7) ? ProgramOptionalOperator.Required : ((flag && flag7) ? ProgramOptionalOperator.Optional : ProgramOptionalOperator.Unknown)));
			this._upperCase = new ProgramOptionalOperator?(flag9 ? ProgramOptionalOperator.Forbidden : ((flag3 && flag10) ? ProgramOptionalOperator.Required : ((flag4 && flag10) ? ProgramOptionalOperator.Optional : ProgramOptionalOperator.Unknown)));
		}

		// Token: 0x0600C370 RID: 50032 RVA: 0x002A1090 File Offset: 0x0029F290
		private unsafe IReadOnlyList<SubstringFindProgramDescriptor> LoadFindEndDescriptors()
		{
			ReadOnlySpan<char> readOnlySpan = this.Input.AsSpan();
			List<SubstringFindProgramDescriptor> list = new List<SubstringFindProgramDescriptor>();
			int[] findStartIndexOffsetRange = SubstringDescriptor._findStartIndexOffsetRange;
			for (int i = 0; i < findStartIndexOffsetRange.Length; i++)
			{
				int num = findStartIndexOffsetRange[i];
				int num2 = this.EndIndex + num;
				if (num2.IsValidIndex(readOnlySpan))
				{
					char findDelimiter = (char)(*readOnlySpan[num2]);
					if (((char)(*readOnlySpan[num2])).IsDelimiter())
					{
						int num3 = this.EndIndex - num2;
						if (num3 < 0 || num3 < this.Substring.Length)
						{
							int num4 = readOnlySpan.Take(num2 + 1).Count((char c) => c == findDelimiter);
							int num5 = -readOnlySpan.Skip(num2).Count((char c) => c == findDelimiter);
							list.Add(new SubstringFindProgramDescriptor
							{
								DelimiterIndex = num2,
								Delimiter = findDelimiter,
								Instance = num4,
								NegativeInstance = num5,
								Offset = num3
							});
						}
					}
				}
			}
			return list;
		}

		// Token: 0x0600C371 RID: 50033 RVA: 0x002A11A8 File Offset: 0x0029F3A8
		private unsafe IReadOnlyList<SubstringFindProgramDescriptor> LoadFindStartDescriptors()
		{
			ReadOnlySpan<char> readOnlySpan = this.Input.AsSpan();
			List<SubstringFindProgramDescriptor> list = new List<SubstringFindProgramDescriptor>();
			int[] findStartIndexOffsetRange = SubstringDescriptor._findStartIndexOffsetRange;
			for (int i = 0; i < findStartIndexOffsetRange.Length; i++)
			{
				int num = findStartIndexOffsetRange[i];
				int num2 = this.StartIndex + num;
				if (num2.IsValidIndex(readOnlySpan))
				{
					char findDelimiter = (char)(*readOnlySpan[num2]);
					if (((char)(*readOnlySpan[num2])).IsDelimiter())
					{
						int num3 = this.StartIndex - num2;
						if (num3 > 0 || -num3 < this.Substring.Length)
						{
							int num4 = readOnlySpan.Take(num2 + 1).Count((char c) => c == findDelimiter);
							int num5 = -readOnlySpan.Skip(num2).Count((char c) => c == findDelimiter);
							list.Add(new SubstringFindProgramDescriptor
							{
								DelimiterIndex = num2,
								Delimiter = findDelimiter,
								Instance = num4,
								NegativeInstance = num5,
								Offset = num3
							});
						}
					}
				}
			}
			return list;
		}

		// Token: 0x0600C372 RID: 50034 RVA: 0x002A12C0 File Offset: 0x0029F4C0
		private unsafe SplitProgramDescriptor LoadSplitDescriptor()
		{
			ReadOnlySpan<char> readOnlySpan = this.Input.AsSpan();
			ReadOnlySpan<char> readOnlySpan2 = this.Output.AsSpan();
			char prevChar = (char)((this.StartIndex != 0) ? (*readOnlySpan[this.StartIndex - 1]) : 0);
			char postChar = (char)((this.EndIndex + 1 < readOnlySpan.Length) ? (*readOnlySpan[this.EndIndex + 1]) : 0);
			bool flag = prevChar != '\0' && prevChar.IsDelimiter();
			bool flag2 = postChar != '\0' && postChar.IsDelimiter();
			if (((this.Prefix && flag2) || (this.Suffix && flag) || (flag && flag2 && prevChar == postChar)) && !readOnlySpan2.Any((char c) => c == prevChar || c == postChar))
			{
				char splitDelimiter = ((!this.Prefix) ? prevChar : postChar);
				int num = (this.Prefix ? 1 : (readOnlySpan.Take(this.StartIndex).Count((char c) => c == splitDelimiter) + 1));
				int num2 = (this.Suffix ? (-1) : (-readOnlySpan.Skip(this.StartIndex).Count((char c) => c == splitDelimiter) - 1));
				return new SplitProgramDescriptor
				{
					Delimiter = splitDelimiter,
					Instance = num,
					NegativeInstance = num2
				};
			}
			return null;
		}

		// Token: 0x0600C373 RID: 50035 RVA: 0x002A144E File Offset: 0x0029F64E
		public bool Equals(SubstringDescriptor other)
		{
			return other != null && this.ToEqualString() == other.ToEqualString();
		}

		// Token: 0x0600C374 RID: 50036 RVA: 0x002A146C File Offset: 0x0029F66C
		public override bool Equals(object other)
		{
			return this.Equals(other as SubstringDescriptor);
		}

		// Token: 0x0600C375 RID: 50037 RVA: 0x002A147A File Offset: 0x0029F67A
		public override int GetHashCode()
		{
			return this.ToEqualString().GetHashCode();
		}

		// Token: 0x0600C376 RID: 50038 RVA: 0x002A1487 File Offset: 0x0029F687
		public static bool operator ==(SubstringDescriptor left, SubstringDescriptor right)
		{
			return (left == null && right == null) || (left != null && left.Equals(right));
		}

		// Token: 0x0600C377 RID: 50039 RVA: 0x002A149D File Offset: 0x0029F69D
		public static bool operator !=(SubstringDescriptor left, SubstringDescriptor right)
		{
			return !(left == right);
		}

		// Token: 0x0600C378 RID: 50040 RVA: 0x002A14AC File Offset: 0x0029F6AC
		public string ToEqualString()
		{
			string text;
			if ((text = this._toEqualString) == null)
			{
				text = (this._toEqualString = this.ToString() + string.Format("{0}: {1}", "LowerCase", this.LowerCase) + string.Format("{0}: {1}", "UpperCase", this.UpperCase) + string.Format("{0}: {1}", "ProperCase", this.ProperCase));
			}
			return text;
		}

		// Token: 0x0600C379 RID: 50041 RVA: 0x002A1528 File Offset: 0x0029F728
		public override string ToString()
		{
			if (this._toString != null)
			{
				return this._toString;
			}
			List<string> list = new List<string>();
			if (this.WholeColumn)
			{
				list.Add("WholeColumn");
			}
			if (!this.WholeColumn)
			{
				if (this.Prefix)
				{
					list.Add("Prefix");
				}
				if (this.Infix)
				{
					list.Add("Infix");
				}
				if (this.Suffix)
				{
					list.Add("Suffix");
				}
			}
			if (this.LowerCase == ProgramOptionalOperator.Required)
			{
				list.Add("LowerCase");
			}
			if (this.UpperCase == ProgramOptionalOperator.Required)
			{
				list.Add("UpperCase");
			}
			List<string> list2 = new List<string>();
			if (this.AllowSplit)
			{
				list2.Add(string.Format("{0}", this.SplitDescriptor));
			}
			if (this.AllowFindStart)
			{
				list2.Add("FindStart" + this.FindStartDescriptors.Select((SubstringFindProgramDescriptor f) => f.ToString()).ToJoinString(" "));
			}
			if (this.AllowFindEnd)
			{
				list2.Add("  FindEnd" + this.FindEndDescriptors.Select((SubstringFindProgramDescriptor f) => f.ToString()).ToJoinString(" "));
			}
			string text = ((this.Substring == this.Output) ? this.Substring.ToCSharpPseudoLiteral() : (this.Substring.ToCSharpPseudoLiteral() + " -> " + this.Output.ToCSharpPseudoLiteral()));
			string text2 = (list2.Any<string>() ? (Environment.NewLine + list2.ToJoinNewlineString().Indent(7, false)) : string.Empty);
			string text3 = ((this.InputColumn != null) ? this.InputColumn.ColumnName : string.Empty);
			string text4;
			if ((text4 = this._toString) == null)
			{
				text4 = (this._toString = string.Format("{0,1}[{1}..{2}] {3}", new object[] { text3, this.StartIndex, this.EndIndex, text }) + "     " + list.ToJoinString(", ") + text2);
			}
			return text4;
		}

		// Token: 0x04004C0B RID: 19467
		private int? _endIndex;

		// Token: 0x04004C0C RID: 19468
		private StringInput _inputColumn;

		// Token: 0x04004C0D RID: 19469
		private string _substring;

		// Token: 0x04004C0E RID: 19470
		private bool? _suffix;

		// Token: 0x04004C0F RID: 19471
		private string _toEqualString;

		// Token: 0x04004C10 RID: 19472
		private string _toString;

		// Token: 0x04004C14 RID: 19476
		private IReadOnlyList<SubstringFindProgramDescriptor> _findEndDescriptors;

		// Token: 0x04004C15 RID: 19477
		private IReadOnlyList<SubstringFindProgramDescriptor> _findStartDescriptors;

		// Token: 0x04004C16 RID: 19478
		private ProgramOptionalOperator? _lowerCase;

		// Token: 0x04004C17 RID: 19479
		private SplitProgramDescriptor _splitDescriptor;

		// Token: 0x04004C18 RID: 19480
		private ProgramOptionalOperator? _upperCase;

		// Token: 0x04004C1A RID: 19482
		private static readonly int[] _findStartIndexOffsetRange = Utils.Range(-5, 3).ToArray<int>();
	}
}
