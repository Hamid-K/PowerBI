using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Mashup.Common;

namespace Microsoft.Data.Mashup
{
	// Token: 0x02000035 RID: 53
	public sealed class MashupPartitionCoordinate : IEquatable<MashupPartitionCoordinate>
	{
		// Token: 0x060002BF RID: 703 RVA: 0x0000B15F File Offset: 0x0000935F
		internal MashupPartitionCoordinate(string[] parts)
		{
			this.parts = parts;
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x0000B170 File Offset: 0x00009370
		public string PartitionKey
		{
			get
			{
				if (this.parts.Length == 0)
				{
					return string.Empty;
				}
				int num = this.parts.Length * 2;
				for (int i = 0; i < this.parts.Length; i++)
				{
					num += this.parts[i].Length;
				}
				StringBuilder stringBuilder = new StringBuilder(num);
				for (int j = 0; j < this.parts.Length; j++)
				{
					if (j > 0)
					{
						stringBuilder.Append('/');
					}
					foreach (char c in this.parts[j])
					{
						if (c == '/' || c == '\\')
						{
							stringBuilder.Append('\\');
						}
						stringBuilder.Append(c);
					}
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x0000B230 File Offset: 0x00009430
		public MashupPartitionCoordinateType CoordinateType
		{
			get
			{
				return (MashupPartitionCoordinateType)this.parts.Length;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x0000B23A File Offset: 0x0000943A
		public string SectionName
		{
			get
			{
				if (this.parts.Length >= 1)
				{
					return this.parts[0];
				}
				return null;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x0000B251 File Offset: 0x00009451
		public string MemberName
		{
			get
			{
				if (this.parts.Length >= 2)
				{
					return this.parts[1];
				}
				return null;
			}
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000B268 File Offset: 0x00009468
		public string GetLetMemberName(int depth)
		{
			if (depth < 1)
			{
				throw new ArgumentException("depth", "depth");
			}
			if (depth <= this.parts.Length - 2)
			{
				return this.parts[depth + 1];
			}
			return null;
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000B298 File Offset: 0x00009498
		public static MashupPartitionCoordinate Create(string partitionKey)
		{
			if (partitionKey.Length == 0)
			{
				return MashupPartitionCoordinate.Empty;
			}
			List<string> list = new List<string>();
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < partitionKey.Length; i++)
			{
				char c = partitionKey[i];
				if (c != '/')
				{
					if (c == '\\')
					{
						i++;
						stringBuilder.Append(partitionKey[i]);
					}
					else
					{
						stringBuilder.Append(c);
					}
				}
				else
				{
					list.Add(stringBuilder.ToString());
					stringBuilder.Length = 0;
				}
			}
			list.Add(stringBuilder.ToString());
			return new MashupPartitionCoordinate(list.ToArray());
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000B32C File Offset: 0x0000952C
		public bool Equals(MashupPartitionCoordinate other)
		{
			if (this.parts.Length != other.parts.Length)
			{
				return false;
			}
			for (int i = 0; i < this.parts.Length; i++)
			{
				if (this.parts[i] != other.parts[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000B37C File Offset: 0x0000957C
		public override bool Equals(object obj)
		{
			MashupPartitionCoordinate mashupPartitionCoordinate = obj as MashupPartitionCoordinate;
			return mashupPartitionCoordinate != null && this.Equals(mashupPartitionCoordinate);
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000B39C File Offset: 0x0000959C
		public override int GetHashCode()
		{
			int num = this.parts.Length;
			foreach (string text in this.parts)
			{
				num += text.GetHashCode();
			}
			return num;
		}

		// Token: 0x04000166 RID: 358
		internal static readonly MashupPartitionCoordinate Empty = new MashupPartitionCoordinate(EmptyArray<string>.Instance);

		// Token: 0x04000167 RID: 359
		private readonly string[] parts;
	}
}
