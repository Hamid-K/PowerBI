using System;

namespace NLog
{
	// Token: 0x02000017 RID: 23
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public sealed class MessageTemplateFormatMethodAttribute : Attribute
	{
		// Token: 0x0600044D RID: 1101 RVA: 0x000085B0 File Offset: 0x000067B0
		public MessageTemplateFormatMethodAttribute(string parameterName)
		{
			this.ParameterName = parameterName;
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x000085BF File Offset: 0x000067BF
		public string ParameterName { get; }
	}
}
