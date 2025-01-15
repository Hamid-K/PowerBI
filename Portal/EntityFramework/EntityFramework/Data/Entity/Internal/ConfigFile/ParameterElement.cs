using System;
using System.Configuration;
using System.Globalization;

namespace System.Data.Entity.Internal.ConfigFile
{
	// Token: 0x02000151 RID: 337
	internal class ParameterElement : ConfigurationElement
	{
		// Token: 0x060015B2 RID: 5554 RVA: 0x0003869D File Offset: 0x0003689D
		public ParameterElement(int key)
		{
			this.Key = key;
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x060015B3 RID: 5555 RVA: 0x000386AC File Offset: 0x000368AC
		// (set) Token: 0x060015B4 RID: 5556 RVA: 0x000386B4 File Offset: 0x000368B4
		internal int Key { get; private set; }

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x060015B5 RID: 5557 RVA: 0x000386BD File Offset: 0x000368BD
		// (set) Token: 0x060015B6 RID: 5558 RVA: 0x000386CF File Offset: 0x000368CF
		[ConfigurationProperty("value", IsRequired = true)]
		public string ValueString
		{
			get
			{
				return (string)base["value"];
			}
			set
			{
				base["value"] = value;
			}
		}

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x060015B7 RID: 5559 RVA: 0x000386DD File Offset: 0x000368DD
		// (set) Token: 0x060015B8 RID: 5560 RVA: 0x000386EF File Offset: 0x000368EF
		[ConfigurationProperty("type", DefaultValue = "System.String")]
		public string TypeName
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

		// Token: 0x060015B9 RID: 5561 RVA: 0x00038700 File Offset: 0x00036900
		public object GetTypedParameterValue()
		{
			Type type = Type.GetType(this.TypeName, true);
			return Convert.ChangeType(this.ValueString, type, CultureInfo.InvariantCulture);
		}

		// Token: 0x040009EB RID: 2539
		private const string ValueKey = "value";

		// Token: 0x040009EC RID: 2540
		private const string TypeKey = "type";
	}
}
