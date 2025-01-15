using System;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x020007A6 RID: 1958
	public sealed class ConsistencyToken
	{
		// Token: 0x17000EE1 RID: 3809
		// (get) Token: 0x06003ED2 RID: 16082 RVA: 0x000D27D1 File Offset: 0x000D09D1
		// (set) Token: 0x06003ED3 RID: 16083 RVA: 0x000D27D9 File Offset: 0x000D09D9
		public byte[] Bytes
		{
			get
			{
				return this.bytes;
			}
			set
			{
				this.bytes = value;
			}
		}

		// Token: 0x06003ED4 RID: 16084 RVA: 0x000D27E2 File Offset: 0x000D09E2
		public ConsistencyToken(byte[] bytes)
		{
			this.bytes = bytes;
		}

		// Token: 0x06003ED5 RID: 16085 RVA: 0x000D27F4 File Offset: 0x000D09F4
		public override bool Equals(object o)
		{
			if (!(o is ConsistencyToken))
			{
				return false;
			}
			ConsistencyToken consistencyToken = (ConsistencyToken)o;
			int num = this.bytes.Length;
			if (num != consistencyToken.bytes.Length)
			{
				return false;
			}
			for (int i = 0; i < num; i++)
			{
				if (this.bytes[i] != consistencyToken.bytes[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003ED6 RID: 16086 RVA: 0x000D284C File Offset: 0x000D0A4C
		public override int GetHashCode()
		{
			int num = this.hash;
			if (num == 0)
			{
				int num2 = this.bytes.Length;
				for (int i = 0; i < num2; i++)
				{
					num ^= (int)this.bytes[i];
				}
				this.hash = num;
			}
			return num;
		}

		// Token: 0x06003ED7 RID: 16087 RVA: 0x000D288B File Offset: 0x000D0A8B
		public override string ToString()
		{
			return BitUtils.ConvertToHexString(this.bytes);
		}

		// Token: 0x0400295B RID: 10587
		private byte[] bytes;

		// Token: 0x0400295C RID: 10588
		private int hash;
	}
}
