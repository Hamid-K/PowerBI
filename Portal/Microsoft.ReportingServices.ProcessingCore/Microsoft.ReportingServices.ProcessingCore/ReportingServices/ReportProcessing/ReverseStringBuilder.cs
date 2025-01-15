using System;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000662 RID: 1634
	internal class ReverseStringBuilder
	{
		// Token: 0x06005AAE RID: 23214 RVA: 0x00175419 File Offset: 0x00173619
		public ReverseStringBuilder()
			: this(16)
		{
		}

		// Token: 0x06005AAF RID: 23215 RVA: 0x00175423 File Offset: 0x00173623
		public ReverseStringBuilder(int capacity)
		{
			this.m_buffer = new char[capacity];
			this.m_pos = capacity - 1;
		}

		// Token: 0x06005AB0 RID: 23216 RVA: 0x00175440 File Offset: 0x00173640
		public void Append(string str)
		{
			int length = str.Length;
			this.EnsureCapacity(length);
			for (int i = 0; i < length; i++)
			{
				char[] buffer = this.m_buffer;
				int pos = this.m_pos;
				this.m_pos = pos - 1;
				buffer[pos] = str[i];
			}
		}

		// Token: 0x06005AB1 RID: 23217 RVA: 0x00175488 File Offset: 0x00173688
		public void Append(char c)
		{
			this.EnsureCapacity(1);
			char[] buffer = this.m_buffer;
			int pos = this.m_pos;
			this.m_pos = pos - 1;
			buffer[pos] = c;
		}

		// Token: 0x06005AB2 RID: 23218 RVA: 0x001754B8 File Offset: 0x001736B8
		private void EnsureCapacity(int lengthNeeded)
		{
			int num = this.m_buffer.Length;
			if (this.m_pos < 0 || num - this.m_pos < lengthNeeded)
			{
				int num2 = num - this.m_pos - 1;
				int num3 = Math.Max(lengthNeeded, num) * 2;
				int num4 = num3 - num2 - 1;
				char[] array = new char[num3];
				Array.Copy(this.m_buffer, this.m_pos + 1, array, num4 + 1, num2);
				this.m_buffer = array;
				this.m_pos = num4;
			}
		}

		// Token: 0x06005AB3 RID: 23219 RVA: 0x00175528 File Offset: 0x00173728
		public override string ToString()
		{
			return new string(this.m_buffer, this.m_pos + 1, this.m_buffer.Length - this.m_pos - 1);
		}

		// Token: 0x04002F27 RID: 12071
		private char[] m_buffer;

		// Token: 0x04002F28 RID: 12072
		private int m_pos;
	}
}
