using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020000A8 RID: 168
	public class MetadataValues : RdlCollection<MetadataValue>
	{
		// Token: 0x0600074C RID: 1868 RVA: 0x0001B06A File Offset: 0x0001926A
		public void Add(string value)
		{
			base.Add(new MetadataValue(value));
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x0001B078 File Offset: 0x00019278
		public bool Contains(string value)
		{
			return this[value] != null;
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x0001B084 File Offset: 0x00019284
		public MetadataValues DeepClone()
		{
			MetadataValues metadataValues = new MetadataValues();
			foreach (MetadataValue metadataValue in this)
			{
				metadataValues.Add((MetadataValue)metadataValue.DeepClone());
			}
			return metadataValues;
		}

		// Token: 0x17000263 RID: 611
		public MetadataValue this[string value]
		{
			get
			{
				foreach (MetadataValue metadataValue in this)
				{
					if (string.Equals(metadataValue.Value, value, StringComparison.CurrentCulture))
					{
						return metadataValue;
					}
				}
				return null;
			}
		}
	}
}
