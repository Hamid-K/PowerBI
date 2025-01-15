using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020000A6 RID: 166
	public class MetadataProperties : RdlCollection<MetadataProperty>
	{
		// Token: 0x0600073A RID: 1850 RVA: 0x0001AE6D File Offset: 0x0001906D
		public void Add(string name, string description)
		{
			base.Add(new MetadataProperty(name, description));
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x0001AE7C File Offset: 0x0001907C
		public bool Contains(string name)
		{
			return this[name] != null;
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x0001AE88 File Offset: 0x00019088
		public MetadataProperties DeepClone()
		{
			MetadataProperties metadataProperties = new MetadataProperties();
			foreach (MetadataProperty metadataProperty in this)
			{
				metadataProperties.Add((MetadataProperty)metadataProperty.DeepClone());
			}
			return metadataProperties;
		}

		// Token: 0x1700025E RID: 606
		public MetadataProperty this[string name]
		{
			get
			{
				foreach (MetadataProperty metadataProperty in this)
				{
					if (string.Equals(metadataProperty.Name, name, StringComparison.CurrentCulture))
					{
						return metadataProperty;
					}
				}
				return null;
			}
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x0001AF3C File Offset: 0x0001913C
		internal MetadataProperty GetProperty(string name, string description)
		{
			MetadataProperty metadataProperty = this[name];
			if (metadataProperty != null)
			{
				return metadataProperty;
			}
			metadataProperty = new MetadataProperty(name, description);
			base.Add(metadataProperty);
			return metadataProperty;
		}
	}
}
