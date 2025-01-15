using System;

namespace NLog.Internal
{
	// Token: 0x02000139 RID: 313
	internal class ReusableBufferCreator : ReusableObjectCreator<char[]>
	{
		// Token: 0x06000F88 RID: 3976 RVA: 0x0002793F File Offset: 0x00025B3F
		public ReusableBufferCreator(int capacity)
			: base(new char[capacity], delegate(char[] b)
			{
			})
		{
		}
	}
}
