using System;
using System.Configuration;

namespace System.Data.Entity.Internal.ConfigFile
{
	// Token: 0x0200014C RID: 332
	internal class DefaultConnectionFactoryElement : ConfigurationElement
	{
		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x0600158E RID: 5518 RVA: 0x000383E1 File Offset: 0x000365E1
		// (set) Token: 0x0600158F RID: 5519 RVA: 0x000383F3 File Offset: 0x000365F3
		[ConfigurationProperty("type", IsRequired = true)]
		public string FactoryTypeName
		{
			get
			{
				return (string)base["type"];
			}
			set
			{
				base["type"] = value;
			}
		}

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x06001590 RID: 5520 RVA: 0x00038401 File Offset: 0x00036601
		[ConfigurationProperty("parameters")]
		public ParameterCollection Parameters
		{
			get
			{
				return (ParameterCollection)base["parameters"];
			}
		}

		// Token: 0x06001591 RID: 5521 RVA: 0x00038413 File Offset: 0x00036613
		public Type GetFactoryType()
		{
			return Type.GetType(this.FactoryTypeName, true);
		}

		// Token: 0x040009DC RID: 2524
		private const string TypeKey = "type";

		// Token: 0x040009DD RID: 2525
		private const string ParametersKey = "parameters";
	}
}
