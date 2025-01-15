using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.AnalysisServices.Tabular.Serialization;

namespace Microsoft.AnalysisServices.Tabular.Tmdl
{
	// Token: 0x0200012E RID: 302
	[DebuggerDisplay("Indentation - size={Size}")]
	internal struct Indentation : IEquatable<Indentation>, IComparable<Indentation>
	{
		// Token: 0x0600148D RID: 5261 RVA: 0x0008BC05 File Offset: 0x00089E05
		public Indentation(string text)
		{
			this.Text = text;
			if (Indentation.IdentifyLeadingIndentation(text, out this.Type, out this.Size) < text.Length)
			{
				this.Type = IndentationType.Invalid;
				this.Size = -1;
			}
		}

		// Token: 0x0600148E RID: 5262 RVA: 0x0008BC36 File Offset: 0x00089E36
		private Indentation(string text, IndentationType type, int size)
		{
			this.Text = text;
			this.Type = type;
			this.Size = size;
		}

		// Token: 0x0600148F RID: 5263 RVA: 0x0008BC4D File Offset: 0x00089E4D
		public bool Equals(Indentation other)
		{
			return this.Size == other.Size;
		}

		// Token: 0x06001490 RID: 5264 RVA: 0x0008BC5D File Offset: 0x00089E5D
		public int CompareTo(Indentation other)
		{
			return this.Size - other.Size;
		}

		// Token: 0x06001491 RID: 5265 RVA: 0x0008BC6C File Offset: 0x00089E6C
		public override bool Equals(object obj)
		{
			if (obj is Indentation)
			{
				Indentation indentation = (Indentation)obj;
				return this.Equals(indentation);
			}
			return false;
		}

		// Token: 0x06001492 RID: 5266 RVA: 0x0008BC91 File Offset: 0x00089E91
		public override int GetHashCode()
		{
			return this.Size;
		}

		// Token: 0x06001493 RID: 5267 RVA: 0x0008BC99 File Offset: 0x00089E99
		public override string ToString()
		{
			return this.Text;
		}

		// Token: 0x06001494 RID: 5268 RVA: 0x0008BCA1 File Offset: 0x00089EA1
		public static implicit operator Indentation(string input)
		{
			return new Indentation(input);
		}

		// Token: 0x06001495 RID: 5269 RVA: 0x0008BCA9 File Offset: 0x00089EA9
		public static bool operator ==(Indentation left, Indentation right)
		{
			return left.Equals(right);
		}

		// Token: 0x06001496 RID: 5270 RVA: 0x0008BCB3 File Offset: 0x00089EB3
		public static bool operator !=(Indentation left, Indentation right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06001497 RID: 5271 RVA: 0x0008BCC0 File Offset: 0x00089EC0
		public static bool operator <(Indentation left, Indentation right)
		{
			return left.CompareTo(right) < 0;
		}

		// Token: 0x06001498 RID: 5272 RVA: 0x0008BCCD File Offset: 0x00089ECD
		public static bool operator >(Indentation left, Indentation right)
		{
			return left.CompareTo(right) > 0;
		}

		// Token: 0x06001499 RID: 5273 RVA: 0x0008BCDA File Offset: 0x00089EDA
		public static bool operator <=(Indentation left, Indentation right)
		{
			return left.CompareTo(right) <= 0;
		}

		// Token: 0x0600149A RID: 5274 RVA: 0x0008BCEA File Offset: 0x00089EEA
		public static bool operator >=(Indentation left, Indentation right)
		{
			return left.CompareTo(right) >= 0;
		}

		// Token: 0x0600149B RID: 5275 RVA: 0x0008BCFC File Offset: 0x00089EFC
		public static Indentation operator +(Indentation left, Indentation right)
		{
			if (left.Type == IndentationType.Invalid || right.Type == IndentationType.Invalid)
			{
				return new Indentation(string.Format("{0}{1}", left.Text, right.Text), IndentationType.Invalid, -1);
			}
			if (left.Size <= 0)
			{
				return right;
			}
			if (right.Size <= 0)
			{
				return left;
			}
			if (left.Type == right.Type)
			{
				return new Indentation(string.Format("{0}{1}", left.Text, right.Text), left.Type, left.Size + right.Size);
			}
			int num = left.Size % MetadataFormattingOptions.Current.IndentationSize;
			if (num == 0)
			{
				return new Indentation(string.Format("{0}{1}", left.Text, right.Text), IndentationType.Mixed, left.Size + right.Size);
			}
			if (right.Text[0] == '\t')
			{
				return new Indentation(string.Format("{0}{1}", left.Text, right.Text), IndentationType.Mixed, left.Size + right.Size - num);
			}
			return new Indentation(string.Format("{0}{1}", left.Text, right.Text));
		}

		// Token: 0x0600149C RID: 5276 RVA: 0x0008BE24 File Offset: 0x0008A024
		internal static int IdentifyLeadingIndentation(string text, out IndentationType type, out int size)
		{
			if (text.Length == 0)
			{
				type = IndentationType.None;
				size = 0;
				return 0;
			}
			int indentationSize = MetadataFormattingOptions.Current.IndentationSize;
			type = Indentation.IdentifyType(text[0]);
			switch (type)
			{
			case IndentationType.Spaces:
				size = 1;
				goto IL_0060;
			case IndentationType.Tabs:
				size = indentationSize;
				goto IL_0060;
			case IndentationType.Invalid:
				type = IndentationType.None;
				size = 0;
				return 0;
			}
			type = IndentationType.None;
			size = 0;
			return 0;
			IL_0060:
			for (int i = 1; i < text.Length; i++)
			{
				switch (Indentation.IdentifyType(text[i]))
				{
				case IndentationType.Spaces:
					if (type == IndentationType.Tabs)
					{
						type = IndentationType.Mixed;
					}
					size++;
					break;
				case IndentationType.Tabs:
					if (type == IndentationType.Tabs)
					{
						size += indentationSize;
					}
					else
					{
						type = IndentationType.Mixed;
						size += indentationSize - size % indentationSize;
					}
					break;
				case IndentationType.Mixed:
					return i;
				case IndentationType.Invalid:
					return i;
				default:
					return i;
				}
			}
			return text.Length;
		}

		// Token: 0x0600149D RID: 5277 RVA: 0x0008BF00 File Offset: 0x0008A100
		internal Indentation Increment(int level = 1)
		{
			bool flag = MetadataFormattingOptions.Current.IndentationMode != IndentationMode.Spaces;
			int indentationSize = MetadataFormattingOptions.Current.IndentationSize;
			int num = indentationSize * level;
			switch (this.Type)
			{
			case IndentationType.None:
				if (flag)
				{
					return new Indentation(new string('\t', level), IndentationType.Tabs, num);
				}
				return new Indentation(new string(' ', num), IndentationType.Spaces, num);
			case IndentationType.Spaces:
			case IndentationType.Mixed:
				num -= this.Size % indentationSize;
				return new Indentation(string.Format("{0}{1}", this.Text, new string(' ', num)), this.Type, this.Size + num);
			case IndentationType.Tabs:
				return new Indentation(string.Format("{0}{1}", this.Text, new string('\t', level)), IndentationType.Tabs, this.Size + num);
			default:
				throw TomInternalException.Create("Can't increment an unsupported Whitespace - type={0}", new object[] { this.Type });
			}
		}

		// Token: 0x0600149E RID: 5278 RVA: 0x0008BFF0 File Offset: 0x0008A1F0
		internal Indentation Increment(Indentation indentation, int level = 1)
		{
			if (this.Type == IndentationType.Invalid)
			{
				throw TomInternalException.Create("Can't increment an unsupported Whitespace - type={0}", new object[] { this.Type });
			}
			if (level == 1)
			{
				if (this.Type != IndentationType.None)
				{
					return this + indentation;
				}
				return indentation;
			}
			else
			{
				int indentationSize = MetadataFormattingOptions.Current.IndentationSize;
				IndentationType type = this.Type;
				bool flag;
				IndentationType indentationType;
				if (type != IndentationType.None)
				{
					if (type - IndentationType.Spaces > 1)
					{
						flag = this.Size % indentationSize == 0 && indentation.Size % indentationSize == 0;
						indentationType = IndentationType.Mixed;
					}
					else
					{
						flag = this.Type == indentation.Type || (this.Size % indentationSize == 0 && indentation.Size % indentationSize == 0);
						indentationType = ((this.Type == indentation.Type) ? this.Type : IndentationType.Mixed);
					}
				}
				else
				{
					flag = indentation.Type != IndentationType.Mixed || indentation.Size % indentationSize == 0;
					indentationType = indentation.Type;
				}
				StringBuilder stringBuilder = new StringBuilder(this.Text);
				for (int i = 0; i < level; i++)
				{
					stringBuilder.Append(indentation.Text);
				}
				if (flag)
				{
					return new Indentation(stringBuilder.ToString(), indentationType, this.Size + indentation.Size * level);
				}
				return new Indentation(stringBuilder.ToString());
			}
		}

		// Token: 0x0600149F RID: 5279 RVA: 0x0008C133 File Offset: 0x0008A333
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IndentationType IdentifyType(char c)
		{
			if (c == '\t')
			{
				return IndentationType.Tabs;
			}
			if (c != ' ')
			{
				return IndentationType.Invalid;
			}
			return IndentationType.Spaces;
		}

		// Token: 0x0400033D RID: 829
		public static readonly Indentation Empty = new Indentation(string.Empty);

		// Token: 0x0400033E RID: 830
		public readonly string Text;

		// Token: 0x0400033F RID: 831
		public readonly IndentationType Type;

		// Token: 0x04000340 RID: 832
		public readonly int Size;
	}
}
