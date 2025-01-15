using System;
using Microsoft.SqlServer.XEvent.Internal;

namespace Microsoft.SqlServer.XEvent
{
	// Token: 0x0200000A RID: 10
	[AttributeUsage(AttributeTargets.Enum, Inherited = true)]
	public sealed class XEventMapAttribute : Attribute
	{
		// Token: 0x06000161 RID: 353 RVA: 0x00002C7C File Offset: 0x00002C7C
		public XEventMapAttribute(string name, string descriptionKey)
		{
			XEventPackageRegistrar.CheckNamingRules(name);
			this.m_name = name;
			this.m_descriptionKey = descriptionKey;
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000162 RID: 354 RVA: 0x00002CAC File Offset: 0x00002CAC
		public string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00002CC8 File Offset: 0x00002CC8
		public string DescriptionKey
		{
			get
			{
				return this.m_descriptionKey;
			}
		}

		// Token: 0x040000F4 RID: 244
		private string m_name;

		// Token: 0x040000F5 RID: 245
		private string m_descriptionKey;
	}
}
