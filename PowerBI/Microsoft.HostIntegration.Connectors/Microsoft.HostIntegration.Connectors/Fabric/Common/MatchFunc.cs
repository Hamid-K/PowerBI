using System;
using System.Text.RegularExpressions;

namespace Microsoft.Fabric.Common
{
	// Token: 0x0200040C RID: 1036
	internal class MatchFunc : BinaryFunc
	{
		// Token: 0x0600240D RID: 9229 RVA: 0x0006E8ED File Offset: 0x0006CAED
		private MatchFunc()
		{
		}

		// Token: 0x0600240E RID: 9230 RVA: 0x0006E8F8 File Offset: 0x0006CAF8
		protected override object InvokeBinary(object arg1, object arg2)
		{
			string text = arg2 as string;
			if (text == null)
			{
				throw new ArgumentException("arg2 must be a regular expression");
			}
			string text2 = arg1 as string;
			if (text2 == null)
			{
				if (arg1 == null)
				{
					return false;
				}
				text2 = arg1.ToString();
			}
			return new Regex(text).IsMatch(text2);
		}

		// Token: 0x0600240F RID: 9231 RVA: 0x0006E948 File Offset: 0x0006CB48
		public override PropertyFunc Bind(FuncArguments args)
		{
			string text;
			if (args.Count == 2 && args.GetLiteralArg<string>(1, out text))
			{
				args.RemoveAt(1);
				return new PatternFunc(text);
			}
			return this;
		}

		// Token: 0x06002410 RID: 9232 RVA: 0x0006E978 File Offset: 0x0006CB78
		public override string ToString()
		{
			return "match";
		}

		// Token: 0x0400164F RID: 5711
		public static readonly MatchFunc Singleton = new MatchFunc();
	}
}
