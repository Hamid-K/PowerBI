using System;
using System.Text;

namespace NLog.Internal
{
	// Token: 0x0200013A RID: 314
	internal class ReusableBuilderCreator : ReusableObjectCreator<StringBuilder>
	{
		// Token: 0x06000F89 RID: 3977 RVA: 0x0002796C File Offset: 0x00025B6C
		public ReusableBuilderCreator()
			: base(new StringBuilder(), delegate(StringBuilder sb)
			{
				sb.ClearBuilder();
			})
		{
		}
	}
}
