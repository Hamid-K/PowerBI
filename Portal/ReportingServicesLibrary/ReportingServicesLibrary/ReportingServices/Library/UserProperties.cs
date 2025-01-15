using System;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200007C RID: 124
	internal sealed class UserProperties : PropertyCollection
	{
		// Token: 0x0600053E RID: 1342 RVA: 0x00014A8E File Offset: 0x00012C8E
		internal UserProperties()
		{
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x000157EB File Offset: 0x000139EB
		internal UserProperties(Property[] properties)
			: base(properties)
		{
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x00014B52 File Offset: 0x00012D52
		internal UserProperties(string propertyXml)
			: base(propertyXml)
		{
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000541 RID: 1345 RVA: 0x000157F4 File Offset: 0x000139F4
		internal string AADAuthToken
		{
			get
			{
				return base["AADAuthToken"];
			}
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x00015801 File Offset: 0x00013A01
		internal string PrepareForSaving()
		{
			return base.ToXml();
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x0001580C File Offset: 0x00013A0C
		internal Property[] AddReservedProperties(string serviceToken)
		{
			Property[] array = new Property[base.Count + 1];
			for (int i = 0; i < base.Count; i++)
			{
				array[i] = new Property
				{
					Name = base.GetName(i),
					Value = base.GetValue(i)
				};
			}
			array[base.Count] = new Property();
			array[base.Count].Name = "AADAuthToken";
			array[base.Count].Value = serviceToken;
			return array;
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x00005BEF File Offset: 0x00003DEF
		protected override bool IsReadOnlyProperty(string propertyName)
		{
			return false;
		}

		// Token: 0x0400025F RID: 607
		internal const string AADAuthTokenProperty = "AADAuthToken";
	}
}
