using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.AnalysisServices.Tabular.Extensions;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular.Tmdl
{
	// Token: 0x02000133 RID: 307
	[DebuggerDisplay("ObjectName - qualifiedName=\"{FullyQualifiedName}\"")]
	internal struct ObjectName : IEquatable<ObjectName>, IComparable<ObjectName>
	{
		// Token: 0x060014B2 RID: 5298 RVA: 0x0008C288 File Offset: 0x0008A488
		public ObjectName(params string[] parts)
		{
			this.parts = parts;
			this.FullyQualifiedName = this.parts.Select(new Func<string, string>(ObjectName.EscapeName)).Join('.');
		}

		// Token: 0x060014B3 RID: 5299 RVA: 0x0008C2B8 File Offset: 0x0008A4B8
		internal ObjectName(ObjectPath path)
		{
			this.parts = new string[path.Count];
			for (int i = 0; i < path.Count; i++)
			{
				this.parts[i] = path[i].Value;
			}
			this.FullyQualifiedName = this.parts.Select(new Func<string, string>(ObjectName.EscapeName)).Join('.');
		}

		// Token: 0x060014B4 RID: 5300 RVA: 0x0008C324 File Offset: 0x0008A524
		private ObjectName(string fullyQualifiedName, IList<string> parts)
		{
			this.parts = new string[parts.Count];
			for (int i = 0; i < parts.Count; i++)
			{
				this.parts[i] = parts[i];
			}
			this.FullyQualifiedName = fullyQualifiedName;
		}

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x060014B5 RID: 5301 RVA: 0x0008C369 File Offset: 0x0008A569
		public string Name
		{
			get
			{
				if (this.parts == null || this.parts.Length == 0)
				{
					return null;
				}
				return this.parts[this.parts.Length - 1];
			}
		}

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x060014B6 RID: 5302 RVA: 0x0008C38F File Offset: 0x0008A58F
		public string FullyQualifiedName { get; }

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x060014B7 RID: 5303 RVA: 0x0008C397 File Offset: 0x0008A597
		public bool IsMultiPartName
		{
			get
			{
				return this.parts != null && this.parts.Length > 1;
			}
		}

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x060014B8 RID: 5304 RVA: 0x0008C3AE File Offset: 0x0008A5AE
		public bool IsEmpty
		{
			get
			{
				return string.IsNullOrEmpty(this.FullyQualifiedName);
			}
		}

		// Token: 0x060014B9 RID: 5305 RVA: 0x0008C3BC File Offset: 0x0008A5BC
		public static ObjectName Parse(string input)
		{
			ObjectName objectName;
			int num;
			string text;
			if (!ObjectName.TryParse(input, 0, out objectName, out num, out text))
			{
				throw new TmdlParserException(ParsingError.InvalidName, TomSR.Exception_ObjetNameParsingError(input, text));
			}
			if (num != -1 && num < input.Length)
			{
				while (char.IsWhiteSpace(input, num))
				{
					num++;
					if (num >= input.Length)
					{
						return objectName;
					}
				}
				throw new TmdlParserException(ParsingError.InvalidName, TomSR.Exception_ObjetNameParsingError(input, TomSR.ObjetNameParseError_NotFullyConsumed));
			}
			return objectName;
		}

		// Token: 0x060014BA RID: 5306 RVA: 0x0008C420 File Offset: 0x0008A620
		public static bool TryParse(string input, int startIndex, out ObjectName result, out int endIndex, out string error)
		{
			Utils.Verify(startIndex <= input.Length);
			result = default(ObjectName);
			while (startIndex < input.Length && char.IsWhiteSpace(input, startIndex))
			{
				startIndex++;
			}
			if (startIndex == input.Length)
			{
				endIndex = -1;
				error = TomSR.ObjetNameParseError_Empty;
				return false;
			}
			List<string> list = new List<string>();
			endIndex = startIndex;
			int num = startIndex;
			bool flag = false;
			while (!flag)
			{
				bool flag2 = input[num] == '\'';
				if (!flag2)
				{
					char c;
					for (;;)
					{
						c = input[endIndex];
						if (c == '\'')
						{
							break;
						}
						bool flag3;
						if (c != '.' && c != '=')
						{
							if (char.IsControl(c))
							{
								goto Block_18;
							}
							flag3 = char.IsWhiteSpace(c);
						}
						else
						{
							flag3 = true;
						}
						if (!flag3)
						{
							endIndex++;
						}
						if (flag3 || endIndex >= input.Length)
						{
							goto IL_01B8;
						}
					}
					error = TomSR.ObjetNameParseError_NeedQuote(c.ToString(), endIndex.ToString("G"));
					return false;
					Block_18:
					error = TomSR.ObjetNameParseError_ControlChar;
					return false;
				}
				endIndex++;
				if (endIndex == input.Length)
				{
					error = TomSR.ObjetNameParseError_OpenPart("'");
					return false;
				}
				bool flag4 = false;
				do
				{
					char c2 = input[endIndex];
					if (char.IsControl(c2))
					{
						goto Block_5;
					}
					if (c2 == '\'')
					{
						if (endIndex + 1 < input.Length && input[endIndex + 1] == '\'')
						{
							endIndex += 2;
						}
						else
						{
							flag4 = true;
							endIndex++;
						}
					}
					else
					{
						endIndex++;
					}
				}
				while (!flag4 && endIndex < input.Length);
				if (endIndex == input.Length)
				{
					if (!flag4)
					{
						error = TomSR.ObjetNameParseError_OpenPart(input.Substring(num));
						return false;
					}
					goto IL_01B8;
				}
				else
				{
					if (input[endIndex] != '=' && input[endIndex] != '.' && !char.IsWhiteSpace(input, endIndex))
					{
						error = TomSR.ObjetNameParseError_SingleQuoteNeedEscape;
						return false;
					}
					goto IL_01B8;
				}
				Block_5:
				error = TomSR.ObjetNameParseError_ControlChar;
				return false;
				IL_01B8:
				if (endIndex == num)
				{
					error = TomSR.ObjetNameParseError_EmptyPart;
					return false;
				}
				if (flag2)
				{
					list.Add(input.UnescapeString('\'', num + 1, endIndex - num - 2));
				}
				else
				{
					list.Add((endIndex < input.Length) ? input.Substring(num, endIndex - num) : input.Substring(num));
				}
				if (endIndex == input.Length || input[endIndex] == '=' || char.IsWhiteSpace(input, endIndex))
				{
					flag = true;
				}
				else
				{
					endIndex++;
					if (endIndex == input.Length || input[endIndex] == '=' || input[endIndex] == '.' || char.IsWhiteSpace(input, endIndex))
					{
						error = TomSR.ObjetNameParseError_EndWithDot;
						return false;
					}
					num = endIndex;
				}
			}
			result = new ObjectName((endIndex < input.Length) ? input.Substring(startIndex, endIndex - startIndex) : input.Substring(startIndex), list);
			if (endIndex == input.Length)
			{
				endIndex = -1;
			}
			error = null;
			return true;
		}

		// Token: 0x060014BB RID: 5307 RVA: 0x0008C6D9 File Offset: 0x0008A8D9
		public override string ToString()
		{
			return this.FullyQualifiedName;
		}

		// Token: 0x060014BC RID: 5308 RVA: 0x0008C6E1 File Offset: 0x0008A8E1
		public bool Equals(ObjectName other)
		{
			if (this.IsEmpty)
			{
				return other.IsEmpty;
			}
			return !other.IsEmpty && string.Compare(this.FullyQualifiedName, other.FullyQualifiedName, StringComparison.InvariantCulture) == 0;
		}

		// Token: 0x060014BD RID: 5309 RVA: 0x0008C714 File Offset: 0x0008A914
		public override bool Equals(object obj)
		{
			if (obj is ObjectName)
			{
				ObjectName objectName = (ObjectName)obj;
				return this.Equals(objectName);
			}
			return false;
		}

		// Token: 0x060014BE RID: 5310 RVA: 0x0008C739 File Offset: 0x0008A939
		public override int GetHashCode()
		{
			if (this.FullyQualifiedName != null)
			{
				return this.FullyQualifiedName.GetHashCode();
			}
			return 0;
		}

		// Token: 0x060014BF RID: 5311 RVA: 0x0008C750 File Offset: 0x0008A950
		public int CompareTo(ObjectName other)
		{
			return (this.Name ?? string.Empty).CompareTo(other.Name);
		}

		// Token: 0x060014C0 RID: 5312 RVA: 0x0008C76D File Offset: 0x0008A96D
		public static implicit operator string(ObjectName name)
		{
			return name.FullyQualifiedName;
		}

		// Token: 0x060014C1 RID: 5313 RVA: 0x0008C776 File Offset: 0x0008A976
		public static explicit operator ObjectName(string input)
		{
			return ObjectName.Parse(input);
		}

		// Token: 0x060014C2 RID: 5314 RVA: 0x0008C77E File Offset: 0x0008A97E
		public static bool operator ==(ObjectName left, ObjectName right)
		{
			return left.Equals(right);
		}

		// Token: 0x060014C3 RID: 5315 RVA: 0x0008C788 File Offset: 0x0008A988
		public static bool operator !=(ObjectName left, ObjectName right)
		{
			return !left.Equals(right);
		}

		// Token: 0x060014C4 RID: 5316 RVA: 0x0008C798 File Offset: 0x0008A998
		private static string EscapeName(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				return string.Empty;
			}
			int num = 0;
			while (num < name.Length && !ObjectName.RequiresQuotes(name[num]))
			{
				num++;
			}
			if (num == name.Length)
			{
				return name;
			}
			StringBuilder stringBuilder = new StringBuilder(name.Length + 10);
			stringBuilder.Append('\'');
			stringBuilder.Append(name.Substring(0, num));
			stringBuilder.Append(name.EscapeString('\'', num, name.Length - num));
			stringBuilder.Append('\'');
			return stringBuilder.ToString();
		}

		// Token: 0x060014C5 RID: 5317 RVA: 0x0008C829 File Offset: 0x0008AA29
		private static bool RequiresQuotes(char c)
		{
			return c == '\'' || c == '.' || c == '=' || char.IsWhiteSpace(c);
		}

		// Token: 0x0400034A RID: 842
		internal readonly string[] parts;
	}
}
