using System;
using System.Collections.Generic;

namespace Microsoft.HostIntegration.Tracing
{
	// Token: 0x02000673 RID: 1651
	public class TracePointPropertyInformation : ITracePointPropertyInformation
	{
		// Token: 0x17000C41 RID: 3137
		// (get) Token: 0x06003788 RID: 14216 RVA: 0x000BAEC3 File Offset: 0x000B90C3
		public int Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x17000C42 RID: 3138
		// (get) Token: 0x06003789 RID: 14217 RVA: 0x000BAECB File Offset: 0x000B90CB
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000C43 RID: 3139
		// (get) Token: 0x0600378A RID: 14218 RVA: 0x000BAED3 File Offset: 0x000B90D3
		public PropertyType ValueType
		{
			get
			{
				return this.valueType;
			}
		}

		// Token: 0x17000C44 RID: 3140
		// (get) Token: 0x0600378B RID: 14219 RVA: 0x000BAEDB File Offset: 0x000B90DB
		public List<ITracePointPropertyEnumerationValue> EnumerationValues
		{
			get
			{
				return this.enumerationValues;
			}
		}

		// Token: 0x0600378C RID: 14220 RVA: 0x000BAEE3 File Offset: 0x000B90E3
		public TracePointPropertyInformation(string propertyName, int propertyIdentifier, PropertyType propertyValueType, List<ITracePointPropertyEnumerationValue> validEnumerationValues)
		{
			this.name = propertyName;
			this.identifier = propertyIdentifier;
			this.valueType = propertyValueType;
			this.enumerationValues = validEnumerationValues;
		}

		// Token: 0x0600378D RID: 14221 RVA: 0x000BAECB File Offset: 0x000B90CB
		public override string ToString()
		{
			return this.name;
		}

		// Token: 0x04001FC7 RID: 8135
		private int identifier;

		// Token: 0x04001FC8 RID: 8136
		private string name;

		// Token: 0x04001FC9 RID: 8137
		private PropertyType valueType;

		// Token: 0x04001FCA RID: 8138
		private List<ITracePointPropertyEnumerationValue> enumerationValues;
	}
}
