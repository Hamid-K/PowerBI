using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x020003FE RID: 1022
	public class ByteArray
	{
		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06001720 RID: 5920 RVA: 0x0004677B File Offset: 0x0004497B
		public byte[] Positions { get; }

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06001721 RID: 5921 RVA: 0x00046783 File Offset: 0x00044983
		public int Length
		{
			get
			{
				return this.Positions.Length;
			}
		}

		// Token: 0x06001722 RID: 5922 RVA: 0x00046790 File Offset: 0x00044990
		public ByteArray(bool direction, string[][] tokens)
		{
			this.Positions = new byte[tokens.Length];
			if (direction)
			{
				return;
			}
			for (int i = 0; i < tokens.Length; i++)
			{
				this.Update(i, (byte)tokens[i].Length);
			}
		}

		// Token: 0x06001723 RID: 5923 RVA: 0x000467D0 File Offset: 0x000449D0
		public ByteArray(ByteArray byteArray)
		{
			this.Positions = new byte[byteArray.Length];
			Array.Copy(byteArray.Positions, this.Positions, byteArray.Length);
		}

		// Token: 0x06001724 RID: 5924 RVA: 0x00046800 File Offset: 0x00044A00
		public ByteArray(int length)
		{
			this.Positions = new byte[length];
		}

		// Token: 0x06001725 RID: 5925 RVA: 0x00046814 File Offset: 0x00044A14
		public void Update(int index, byte pos)
		{
			this.Positions[index] = pos;
		}

		// Token: 0x06001726 RID: 5926 RVA: 0x0004681F File Offset: 0x00044A1F
		public byte GetByte(int index)
		{
			return this.Positions[index];
		}

		// Token: 0x06001727 RID: 5927 RVA: 0x00046829 File Offset: 0x00044A29
		public bool IsInitial()
		{
			return this.Positions.All((byte b) => b == 0);
		}

		// Token: 0x06001728 RID: 5928 RVA: 0x00046855 File Offset: 0x00044A55
		public static bool operator ==(ByteArray b1, ByteArray b2)
		{
			return (b1 == null && b2 == null) || (b2 != null && b1 != null && b1.Positions.SequenceEqual(b2.Positions));
		}

		// Token: 0x06001729 RID: 5929 RVA: 0x00046884 File Offset: 0x00044A84
		public static bool operator !=(ByteArray b1, ByteArray b2)
		{
			return !(b1 == b2);
		}

		// Token: 0x0600172A RID: 5930 RVA: 0x00046890 File Offset: 0x00044A90
		public override bool Equals(object obj)
		{
			if (!(obj is ByteArray))
			{
				return false;
			}
			ByteArray byteArray = (ByteArray)obj;
			return this.Positions.SequenceEqual(byteArray.Positions);
		}

		// Token: 0x0600172B RID: 5931 RVA: 0x00024CEC File Offset: 0x00022EEC
		protected bool Equals(ByteArray other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600172C RID: 5932 RVA: 0x000468BF File Offset: 0x00044ABF
		public override int GetHashCode()
		{
			return this.Positions.Aggregate(0, (int current, byte b) => (current * 31) ^ (int)b);
		}

		// Token: 0x0600172D RID: 5933 RVA: 0x000468EC File Offset: 0x00044AEC
		public override string ToString()
		{
			return FormattableString.Invariant(FormattableStringFactory.Create("({0})", new object[] { string.Join<byte>(",", this.Positions) }));
		}
	}
}
