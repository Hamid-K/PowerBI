using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000015 RID: 21
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
	internal sealed class StringSyntaxAttribute : Attribute
	{
		// Token: 0x06000115 RID: 277 RVA: 0x00003146 File Offset: 0x00001346
		public StringSyntaxAttribute(string syntax)
		{
			this.Syntax = syntax;
			this.Arguments = Array.Empty<object>();
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00003160 File Offset: 0x00001360
		public StringSyntaxAttribute(string syntax, params object[] arguments)
		{
			this.Syntax = syntax;
			this.Arguments = arguments;
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000117 RID: 279 RVA: 0x00003176 File Offset: 0x00001376
		public string Syntax { get; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000118 RID: 280 RVA: 0x0000317E File Offset: 0x0000137E
		public object[] Arguments { get; }

		// Token: 0x04000099 RID: 153
		public const string CompositeFormat = "CompositeFormat";

		// Token: 0x0400009A RID: 154
		public const string DateOnlyFormat = "DateOnlyFormat";

		// Token: 0x0400009B RID: 155
		public const string DateTimeFormat = "DateTimeFormat";

		// Token: 0x0400009C RID: 156
		public const string EnumFormat = "EnumFormat";

		// Token: 0x0400009D RID: 157
		public const string GuidFormat = "GuidFormat";

		// Token: 0x0400009E RID: 158
		public const string Json = "Json";

		// Token: 0x0400009F RID: 159
		public const string NumericFormat = "NumericFormat";

		// Token: 0x040000A0 RID: 160
		public const string Regex = "Regex";

		// Token: 0x040000A1 RID: 161
		public const string TimeOnlyFormat = "TimeOnlyFormat";

		// Token: 0x040000A2 RID: 162
		public const string TimeSpanFormat = "TimeSpanFormat";

		// Token: 0x040000A3 RID: 163
		public const string Uri = "Uri";

		// Token: 0x040000A4 RID: 164
		public const string Xml = "Xml";
	}
}
