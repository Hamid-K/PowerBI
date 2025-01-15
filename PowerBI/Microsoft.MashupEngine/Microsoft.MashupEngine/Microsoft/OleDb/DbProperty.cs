using System;
using Microsoft.OleDb.Marshallers;

namespace Microsoft.OleDb
{
	// Token: 0x02001E96 RID: 7830
	public sealed class DbProperty
	{
		// Token: 0x0600C1A3 RID: 49571 RVA: 0x0026F06A File Offset: 0x0026D26A
		public DbProperty(DBPROPID id, object value)
		{
			this.id = id;
			this.value = value;
		}

		// Token: 0x0600C1A4 RID: 49572 RVA: 0x0026F080 File Offset: 0x0026D280
		public static DbProperty NotSupported(DBPROPID id)
		{
			return new DbProperty(id, DbProperty.notSupported);
		}

		// Token: 0x17002F5C RID: 12124
		// (get) Token: 0x0600C1A5 RID: 49573 RVA: 0x0026F08D File Offset: 0x0026D28D
		public bool IsSupported
		{
			get
			{
				return this.value != DbProperty.notSupported;
			}
		}

		// Token: 0x17002F5D RID: 12125
		// (get) Token: 0x0600C1A6 RID: 49574 RVA: 0x0026F09F File Offset: 0x0026D29F
		public DBPROPID ID
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x17002F5E RID: 12126
		// (get) Token: 0x0600C1A7 RID: 49575 RVA: 0x0026F0A7 File Offset: 0x0026D2A7
		public object Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x0600C1A8 RID: 49576 RVA: 0x0026F0B0 File Offset: 0x0026D2B0
		public unsafe static DbProperty[] GetProperties(uint countProperties, DBPROP* nativeProperties)
		{
			DbProperty[] array = new DbProperty[countProperties];
			for (int i = 0; i < array.Length; i++)
			{
				DBPROP* ptr = nativeProperties + i;
				array[i] = new DbProperty(ptr->dwPropertyID, VariantMarshaller.Instance.GetManaged((IntPtr)((void*)(&ptr->variant))));
			}
			return array;
		}

		// Token: 0x0400619F RID: 24991
		private static object notSupported = new object();

		// Token: 0x040061A0 RID: 24992
		private DBPROPID id;

		// Token: 0x040061A1 RID: 24993
		private object value;
	}
}
