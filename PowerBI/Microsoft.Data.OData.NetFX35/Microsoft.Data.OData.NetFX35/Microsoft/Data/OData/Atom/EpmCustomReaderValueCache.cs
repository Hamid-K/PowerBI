using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x020001F8 RID: 504
	internal sealed class EpmCustomReaderValueCache
	{
		// Token: 0x06000E90 RID: 3728 RVA: 0x000347C8 File Offset: 0x000329C8
		internal EpmCustomReaderValueCache()
		{
			this.customEpmValues = new List<KeyValuePair<EntityPropertyMappingInfo, string>>();
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000E91 RID: 3729 RVA: 0x000347DB File Offset: 0x000329DB
		internal IEnumerable<KeyValuePair<EntityPropertyMappingInfo, string>> CustomEpmValues
		{
			get
			{
				return this.customEpmValues;
			}
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x00034800 File Offset: 0x00032A00
		internal bool Contains(EntityPropertyMappingInfo epmInfo)
		{
			return Enumerable.Any<KeyValuePair<EntityPropertyMappingInfo, string>>(this.customEpmValues, (KeyValuePair<EntityPropertyMappingInfo, string> epmValue) => object.ReferenceEquals(epmValue.Key, epmInfo));
		}

		// Token: 0x06000E93 RID: 3731 RVA: 0x00034831 File Offset: 0x00032A31
		internal void Add(EntityPropertyMappingInfo epmInfo, string value)
		{
			this.customEpmValues.Add(new KeyValuePair<EntityPropertyMappingInfo, string>(epmInfo, value));
		}

		// Token: 0x04000572 RID: 1394
		private readonly List<KeyValuePair<EntityPropertyMappingInfo, string>> customEpmValues;
	}
}
