using System;

namespace Microsoft.OleDb
{
	// Token: 0x02001E98 RID: 7832
	internal static class DbPropertySets
	{
		// Token: 0x0600C1AD RID: 49581 RVA: 0x0026F138 File Offset: 0x0026D338
		public unsafe static DbPropertySet[] GetPropertySets(uint countPropertySets, DBPROPSET* nativePropertySets)
		{
			DbPropertySet[] array = new DbPropertySet[countPropertySets];
			for (int i = 0; i < array.Length; i++)
			{
				DBPROPSET* ptr = nativePropertySets + i;
				DbProperty[] properties = DbProperty.GetProperties(ptr->cProperties, ptr->rgProperties);
				array[i] = new DbPropertySet(ptr->guidPropertySet, properties);
			}
			return array;
		}

		// Token: 0x0600C1AE RID: 49582 RVA: 0x0026F18C File Offset: 0x0026D38C
		public unsafe static void GetPropertySets(DbPropertySet[] propertySets, ComHeap heap, out uint countPropertySets, out DBPROPSET* nativePropertySets)
		{
			nativePropertySets = heap.AllocArray(propertySets.Length, sizeof(DBPROPSET));
			countPropertySets = (uint)propertySets.Length;
			for (int i = 0; i < propertySets.Length; i++)
			{
				DbProperty[] properties = propertySets[i].Properties;
				DBPROP* ptr = (DBPROP*)heap.AllocArray(properties.Length, sizeof(DBPROP));
				for (int j = 0; j < properties.Length; j++)
				{
					DbProperty dbProperty = properties[j];
					DBPROP* ptr2 = ptr + j;
					if (dbProperty.IsSupported)
					{
						ptr2->colid = default(DBID);
						ptr2->dwOptions = DBPROPOPTIONS.REQUIRED;
						ptr2->dwPropertyID = dbProperty.ID;
						ptr2->dwStatus = DBPROPSTATUS.OK;
						Variant.SetValue(&ptr2->variant, heap, dbProperty.Value);
					}
					else
					{
						ptr2->colid = default(DBID);
						ptr2->dwOptions = DBPROPOPTIONS.REQUIRED;
						ptr2->dwPropertyID = dbProperty.ID;
						ptr2->dwStatus = DBPROPSTATUS.NOTSUPPORTED;
						Variant.Init(&ptr2->variant);
					}
				}
				UIntPtr uintPtr = (UIntPtr)(nativePropertySets + (IntPtr)i * (IntPtr)sizeof(DBPROPSET));
				uintPtr.guidPropertySet = propertySets[i].Group;
				uintPtr.cProperties = (uint)properties.Length;
				uintPtr.rgProperties = ptr;
			}
		}
	}
}
