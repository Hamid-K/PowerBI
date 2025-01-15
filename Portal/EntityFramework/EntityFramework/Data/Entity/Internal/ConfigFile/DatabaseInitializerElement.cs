using System;
using System.Configuration;

namespace System.Data.Entity.Internal.ConfigFile
{
	// Token: 0x0200014B RID: 331
	internal class DatabaseInitializerElement : ConfigurationElement
	{
		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x0600158A RID: 5514 RVA: 0x000383A7 File Offset: 0x000365A7
		// (set) Token: 0x0600158B RID: 5515 RVA: 0x000383B9 File Offset: 0x000365B9
		[ConfigurationProperty("type", IsRequired = true)]
		public virtual string InitializerTypeName
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

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x0600158C RID: 5516 RVA: 0x000383C7 File Offset: 0x000365C7
		[ConfigurationProperty("parameters")]
		public virtual ParameterCollection Parameters
		{
			get
			{
				return (ParameterCollection)base["parameters"];
			}
		}

		// Token: 0x040009DA RID: 2522
		private const string TypeKey = "type";

		// Token: 0x040009DB RID: 2523
		private const string ParametersKey = "parameters";
	}
}
