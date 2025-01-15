using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics.Internal;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000041 RID: 65
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public class DictionaryDBProperties : IDBProperties
	{
		// Token: 0x0600023D RID: 573 RVA: 0x00006E34 File Offset: 0x00005034
		public DictionaryDBProperties(params PropertyInfo[] propertyInfos)
		{
			this.propertyInfos = new Dictionary<DBPROPID, PropertyInfo>();
			this.values = new Dictionary<DBPROPID, object>();
			foreach (PropertyInfo propertyInfo in propertyInfos)
			{
				this.propertyInfos.Add(propertyInfo.ID, propertyInfo);
				this.values.Add(propertyInfo.ID, propertyInfo.Default);
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600023E RID: 574 RVA: 0x00006E9A File Offset: 0x0000509A
		public Dictionary<DBPROPID, object> Values
		{
			get
			{
				return this.values;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600023F RID: 575 RVA: 0x00006EA2 File Offset: 0x000050A2
		public Dictionary<DBPROPID, PropertyInfo> PropertyInfos
		{
			get
			{
				return this.propertyInfos;
			}
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00006EAC File Offset: 0x000050AC
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		unsafe int IDBProperties.GetProperties(uint countPropertyIDSets, DBPROPIDSET* nativePropertyIDSets, out uint countPropertySets1, out DBPROPSET* nativePropertySets1)
		{
			countPropertySets1 = 0U;
			nativePropertySets1 = (IntPtr)((UIntPtr)0);
			DictionaryDBProperties.PropertyIDSet[] propertyIDSets = this.GetPropertyIDSets(countPropertyIDSets, nativePropertyIDSets);
			DictionaryDBProperties.PropertySet[] propertySets = this.GetPropertySets(propertyIDSets, this.propertyInfos.Values);
			int propertyStatus;
			using (ComHeap comHeap = new ComHeap())
			{
				uint num;
				DBPROPSET* ptr;
				this.GetPropertySets(propertySets, comHeap, out num, out ptr);
				if (num > 0U)
				{
					countPropertySets1 = num;
					nativePropertySets1 = ptr;
					comHeap.Commit();
				}
				propertyStatus = DictionaryDBProperties.GetPropertyStatus(propertyIDSets, propertySets);
			}
			return propertyStatus;
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00006F28 File Offset: 0x00005128
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		unsafe int IDBProperties.GetPropertyInfo(uint countPropertyIDSets, DBPROPIDSET* nativePropertyIDSets, out uint countPropertyInfoSets1, out DBPROPINFOSET* nativePropertyInfoSets1, char** nativeDescriptions1)
		{
			countPropertyInfoSets1 = 0U;
			nativePropertyInfoSets1 = (IntPtr)((UIntPtr)0);
			if (nativeDescriptions1 != null)
			{
				*(IntPtr*)nativeDescriptions1 = (IntPtr)((UIntPtr)0);
			}
			DictionaryDBProperties.PropertyIDSet[] propertyIDSets = this.GetPropertyIDSets(countPropertyIDSets, nativePropertyIDSets);
			DictionaryDBProperties.PropertyInfoSet[] propertyInfoSets = this.GetPropertyInfoSets(propertyIDSets, this.propertyInfos.Values);
			int propertyInfoStatus;
			using (ComHeap comHeap = new ComHeap())
			{
				uint num;
				DBPROPINFOSET* ptr;
				char* ptr2;
				this.GetPropertyInfoSets(propertyInfoSets, comHeap, out num, out ptr, out ptr2);
				if (num > 0U)
				{
					countPropertyInfoSets1 = num;
					nativePropertyInfoSets1 = ptr;
					if (nativeDescriptions1 != null)
					{
						*(IntPtr*)nativeDescriptions1 = ptr2;
					}
					comHeap.Commit();
				}
				propertyInfoStatus = DictionaryDBProperties.GetPropertyInfoStatus(propertyIDSets, propertyInfoSets);
			}
			return propertyInfoStatus;
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00006FBC File Offset: 0x000051BC
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		unsafe int IDBProperties.SetProperties(uint propertySetCount, DBPROPSET* nativePropertySets)
		{
			DictionaryDBProperties.PropertySet[] propertySets = this.GetPropertySets(propertySetCount, nativePropertySets);
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < propertySets.Length; i++)
			{
				DictionaryDBProperties.PropertySet propertySet = propertySets[i];
				DictionaryDBProperties.Property[] properties = propertySet.Properties;
				for (int j = 0; j < properties.Length; j++)
				{
					DictionaryDBProperties.Property property = properties[j];
					DBPROPID id = property.ID;
					object value = property.Value;
					if (this.TrySetValue(id, value))
					{
						num2++;
					}
					else if (nativePropertySets[i].Properties[j].Options == DBPROPOPTIONS.REQUIRED)
					{
						TraceSourceBase<OleDbBaseTraceSource>.Tracer.TraceWarning("DataMovement.Pipeline.OleDbBase.SetProperties Required property {0}:{1} is not supported by transfer provider.", new object[]
						{
							propertySet.Group,
							nativePropertySets[i].Properties[j].PropertyID
						});
					}
					else
					{
						TraceSourceBase<OleDbBaseTraceSource>.Tracer.TraceWarning("DataMovement.Pipeline.OleDbBase.SetProperties Optional property {0}:{1} is not supported by transfer provider.", new object[]
						{
							propertySet.Group,
							nativePropertySets[i].Properties[j].PropertyID
						});
					}
				}
				num += properties.Length;
			}
			if (num == num2)
			{
				return 0;
			}
			if (num2 != 0)
			{
				return 265946;
			}
			return -2147217887;
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00007114 File Offset: 0x00005314
		private bool TrySetValue(DBPROPID propertyID, object value)
		{
			PropertyInfo propertyInfo;
			if (this.propertyInfos.TryGetValue(propertyID, out propertyInfo) && propertyInfo.IsWritable)
			{
				VARTYPE varType = DictionaryDBProperties.GetVarType(value);
				if (propertyInfo.Type == varType)
				{
					this.values[propertyID] = value;
					return true;
				}
				if (propertyInfo.Type == VARTYPE.I8 && varType == VARTYPE.I4)
				{
					this.values[propertyID] = value;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00007176 File Offset: 0x00005376
		private static VARTYPE GetVarType(object value)
		{
			if (value is string)
			{
				return VARTYPE.BSTR;
			}
			if (value is int)
			{
				return VARTYPE.I4;
			}
			if (value is short)
			{
				return VARTYPE.I2;
			}
			if (value is bool)
			{
				return VARTYPE.BOOL;
			}
			return VARTYPE.NULL;
		}

		// Token: 0x06000245 RID: 581 RVA: 0x000071A4 File Offset: 0x000053A4
		private static int GetPropertyInfoStatus(DictionaryDBProperties.PropertyIDSet[] propertyIDSets, DictionaryDBProperties.PropertyInfoSet[] propertyInfoSets)
		{
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < propertyIDSets.Length; i++)
			{
				DictionaryDBProperties.PropertyIDSet propertyIDSet = propertyIDSets[i];
				DictionaryDBProperties.PropertyInfoSet propertyInfoSet = propertyInfoSets[i];
				if (propertyIDSet.IDs.Length != 0)
				{
					num += propertyIDSet.IDs.Length;
					for (int j = 0; j < propertyInfoSet.Infos.Length; j++)
					{
						if (propertyInfoSet.Infos[j].Flags != DBPROPFLAGS.NOTSUPPORTED)
						{
							num2++;
						}
					}
				}
			}
			if (num == num2)
			{
				return 0;
			}
			if (num2 == 0)
			{
				return -2147217887;
			}
			return 265946;
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00007224 File Offset: 0x00005424
		private static int GetPropertyStatus(DictionaryDBProperties.PropertyIDSet[] propertyIDSets, DictionaryDBProperties.PropertySet[] propertySets)
		{
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < propertyIDSets.Length; i++)
			{
				DictionaryDBProperties.PropertyIDSet propertyIDSet = propertyIDSets[i];
				DictionaryDBProperties.PropertySet propertySet = propertySets[i];
				if (propertyIDSet.IDs.Length != 0)
				{
					num += propertyIDSet.IDs.Length;
					for (int j = 0; j < propertySet.Properties.Length; j++)
					{
						if (propertySet.Properties[j].IsSupported)
						{
							num2++;
						}
					}
				}
			}
			if (num == num2)
			{
				return 0;
			}
			if (num2 == 0)
			{
				return -2147217887;
			}
			return 265946;
		}

		// Token: 0x06000247 RID: 583 RVA: 0x000072A4 File Offset: 0x000054A4
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		private unsafe DictionaryDBProperties.PropertyIDSet[] GetPropertyIDSets(uint countPropertyIDSets, DBPROPIDSET* nativePropertyIDSets)
		{
			DictionaryDBProperties.PropertyIDSet[] array = new DictionaryDBProperties.PropertyIDSet[countPropertyIDSets];
			for (int i = 0; i < array.Length; i++)
			{
				DBPROPIDSET* ptr = nativePropertyIDSets + i;
				DBPROPID[] array2 = new DBPROPID[ptr->PropertyIDCount];
				Guid group = DictionaryDBProperties.GetGroup(ptr->PropertySet);
				for (int j = 0; j < array2.Length; j++)
				{
					array2[j] = ptr->PropertyIDs[j];
				}
				array[i] = new DictionaryDBProperties.PropertyIDSet(group, array2);
			}
			return array;
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000731C File Offset: 0x0000551C
		public static Guid GetGroup(Guid guid)
		{
			if (guid == DBPROPGROUP.ColumnAll)
			{
				return DBPROPGROUP.Column;
			}
			if (guid == DBPROPGROUP.DataSourceAll)
			{
				return DBPROPGROUP.DataSource;
			}
			if (guid == DBPROPGROUP.DataSourceInfoAll)
			{
				return DBPROPGROUP.DataSourceInfo;
			}
			if (guid == DBPROPGROUP.DBInitAll)
			{
				return DBPROPGROUP.DBInit;
			}
			if (guid == DBPROPGROUP.IndexAll)
			{
				return DBPROPGROUP.Index;
			}
			if (guid == DBPROPGROUP.RowsetAll)
			{
				return DBPROPGROUP.Rowset;
			}
			if (guid == DBPROPGROUP.TableAll)
			{
				return DBPROPGROUP.Table;
			}
			if (guid == DBPROPGROUP.SessionAll)
			{
				return DBPROPGROUP.Session;
			}
			return guid;
		}

		// Token: 0x06000249 RID: 585 RVA: 0x000073C4 File Offset: 0x000055C4
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private unsafe void GetPropertyInfoSets([global::System.Runtime.CompilerServices.Nullable(1)] DictionaryDBProperties.PropertyInfoSet[] propertyInfoSets, [global::System.Runtime.CompilerServices.Nullable(1)] ComHeap heap, out uint countPropertyInfoSets, out DBPROPINFOSET* nativePropertyInfoSets, out char* nativeDescriptions)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < propertyInfoSets.Length; i++)
			{
				PropertyInfo[] infos = propertyInfoSets[i].Infos;
				for (int j = 0; j < infos.Length; j++)
				{
					stringBuilder.Append(infos[j].Description);
					stringBuilder.Append('\0');
				}
			}
			nativeDescriptions = heap.AllocString(stringBuilder.ToString());
			int num = 0;
			nativePropertyInfoSets = heap.AllocArray(propertyInfoSets.Length, sizeof(DBPROPINFOSET));
			countPropertyInfoSets = (uint)propertyInfoSets.Length;
			for (int k = 0; k < propertyInfoSets.Length; k++)
			{
				DictionaryDBProperties.PropertyInfoSet propertyInfoSet = propertyInfoSets[k];
				DBPROPINFOSET* ptr = nativePropertyInfoSets / (IntPtr)sizeof(DBPROPINFOSET) + k * sizeof(DBPROPINFOSET);
				PropertyInfo[] infos2 = propertyInfoSet.Infos;
				DBPROPINFO* ptr2 = (DBPROPINFO*)heap.AllocArray(infos2.Length, sizeof(DBPROPINFO));
				for (int l = 0; l < infos2.Length; l++)
				{
					PropertyInfo propertyInfo = infos2[l];
					DBPROPINFO* ptr3 = ptr2 + l;
					ptr3->Description = nativeDescriptions / 2 + num * 2;
					ptr3->Flags = propertyInfo.Flags;
					ptr3->PropertyID = propertyInfo.ID;
					ptr3->Type = propertyInfo.Type;
					Variant.Init(&ptr3->Values);
					num += propertyInfo.Description.Length + 1;
				}
				ptr->GuidPropertySet = propertyInfoSet.Group;
				ptr->CountPropertyInfos = (uint)infos2.Length;
				ptr->PropertyInfos = ptr2;
			}
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00007528 File Offset: 0x00005728
		private DictionaryDBProperties.PropertyInfoSet[] GetPropertyInfoSets(IList<DictionaryDBProperties.PropertyIDSet> propertyIDSets, IEnumerable<PropertyInfo> propertyInfos)
		{
			Dictionary<Guid, List<PropertyInfo>> groups = this.GetGroups(propertyInfos);
			if (propertyIDSets.Count == 0)
			{
				return this.GetPropertyInfoSets(groups);
			}
			return this.GetPropertyInfoSets(groups, propertyIDSets);
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00007558 File Offset: 0x00005758
		private DictionaryDBProperties.PropertyInfoSet[] GetPropertyInfoSets(Dictionary<Guid, List<PropertyInfo>> groups)
		{
			DictionaryDBProperties.PropertyInfoSet[] array = new DictionaryDBProperties.PropertyInfoSet[groups.Count];
			int num = 0;
			foreach (KeyValuePair<Guid, List<PropertyInfo>> keyValuePair in groups)
			{
				array[num] = new DictionaryDBProperties.PropertyInfoSet(keyValuePair.Key, keyValuePair.Value.ToArray());
				num++;
			}
			return array;
		}

		// Token: 0x0600024C RID: 588 RVA: 0x000075D0 File Offset: 0x000057D0
		private DictionaryDBProperties.PropertyInfoSet[] GetPropertyInfoSets(Dictionary<Guid, List<PropertyInfo>> groups, IList<DictionaryDBProperties.PropertyIDSet> propertyIDSets)
		{
			DictionaryDBProperties.PropertyInfoSet[] array = new DictionaryDBProperties.PropertyInfoSet[propertyIDSets.Count];
			for (int i = 0; i < propertyIDSets.Count; i++)
			{
				DictionaryDBProperties.PropertyIDSet propertyIDSet = propertyIDSets[i];
				List<PropertyInfo> list;
				PropertyInfo[] array2;
				if (groups.TryGetValue(propertyIDSet.Group, out list))
				{
					if (propertyIDSet.IDs.Length == 0)
					{
						array2 = list.ToArray();
					}
					else
					{
						array2 = this.GetPropertyInfos(propertyIDSet.IDs, list);
					}
				}
				else
				{
					array2 = new PropertyInfo[0];
				}
				array[i] = new DictionaryDBProperties.PropertyInfoSet(propertyIDSet.Group, array2);
			}
			return array;
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00007650 File Offset: 0x00005850
		private PropertyInfo[] GetPropertyInfos(IList<DBPROPID> propertyIDs, IList<PropertyInfo> propertyInfos)
		{
			Dictionary<DBPROPID, PropertyInfo> dictionary = new Dictionary<DBPROPID, PropertyInfo>();
			for (int i = 0; i < propertyInfos.Count; i++)
			{
				dictionary.Add(propertyInfos[i].ID, propertyInfos[i]);
			}
			PropertyInfo[] array = new PropertyInfo[propertyIDs.Count];
			for (int j = 0; j < propertyIDs.Count; j++)
			{
				PropertyInfo propertyInfo;
				if (!dictionary.TryGetValue(propertyIDs[j], out propertyInfo))
				{
					propertyInfo = new PropertyInfo(DBPROPGROUP.Error, propertyIDs[j], string.Empty, 0, VARTYPE.I4, DBPROPFLAGS.NOTSUPPORTED);
				}
				array[j] = propertyInfo;
			}
			return array;
		}

		// Token: 0x0600024E RID: 590 RVA: 0x000076E1 File Offset: 0x000058E1
		private DictionaryDBProperties.PropertySet[] GetPropertySets(IList<DictionaryDBProperties.PropertyIDSet> propertyIDSets, IEnumerable<PropertyInfo> propertyInfos)
		{
			if (propertyIDSets.Count == 0)
			{
				return this.GetPropertySets(this.GetGroups(propertyInfos));
			}
			return this.GetPropertySets(propertyIDSets);
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00007700 File Offset: 0x00005900
		private DictionaryDBProperties.PropertySet[] GetPropertySets(Dictionary<Guid, List<PropertyInfo>> groups)
		{
			DictionaryDBProperties.PropertySet[] array = new DictionaryDBProperties.PropertySet[groups.Count];
			int num = 0;
			foreach (KeyValuePair<Guid, List<PropertyInfo>> keyValuePair in groups)
			{
				array[num] = new DictionaryDBProperties.PropertySet(keyValuePair.Key, this.GetProperties(keyValuePair.Value));
				num++;
			}
			return array;
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00007778 File Offset: 0x00005978
		private DictionaryDBProperties.PropertySet[] GetPropertySets(IList<DictionaryDBProperties.PropertyIDSet> propertyIDSets)
		{
			DictionaryDBProperties.PropertySet[] array = new DictionaryDBProperties.PropertySet[propertyIDSets.Count];
			for (int i = 0; i < propertyIDSets.Count; i++)
			{
				DictionaryDBProperties.Property[] properties = this.GetProperties(propertyIDSets[i].IDs);
				array[i] = new DictionaryDBProperties.PropertySet(propertyIDSets[i].Group, properties);
			}
			return array;
		}

		// Token: 0x06000251 RID: 593 RVA: 0x000077CC File Offset: 0x000059CC
		private DictionaryDBProperties.Property[] GetProperties(IList<PropertyInfo> propertyInfos)
		{
			DictionaryDBProperties.Property[] array = new DictionaryDBProperties.Property[propertyInfos.Count];
			for (int i = 0; i < array.Length; i++)
			{
				DBPROPID id = propertyInfos[i].ID;
				array[i] = new DictionaryDBProperties.Property(id, this.values[id]);
			}
			return array;
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00007818 File Offset: 0x00005A18
		private DictionaryDBProperties.Property[] GetProperties(IList<DBPROPID> propertyIDs)
		{
			DictionaryDBProperties.Property[] array = new DictionaryDBProperties.Property[propertyIDs.Count];
			for (int i = 0; i < propertyIDs.Count; i++)
			{
				DBPROPID dbpropid = propertyIDs[i];
				object obj;
				DictionaryDBProperties.Property property;
				if (this.values.TryGetValue(dbpropid, out obj))
				{
					property = new DictionaryDBProperties.Property(dbpropid, obj);
				}
				else
				{
					property = DictionaryDBProperties.Property.NotSupported(dbpropid);
				}
				array[i] = property;
			}
			return array;
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00007874 File Offset: 0x00005A74
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		private unsafe DictionaryDBProperties.PropertySet[] GetPropertySets(uint countPropertySets, DBPROPSET* nativePropertySets)
		{
			DictionaryDBProperties.PropertySet[] array = new DictionaryDBProperties.PropertySet[countPropertySets];
			for (int i = 0; i < array.Length; i++)
			{
				DBPROPSET* ptr = nativePropertySets + i;
				DictionaryDBProperties.Property[] properties = this.GetProperties(ptr->PropertyCount, ptr->Properties);
				array[i] = new DictionaryDBProperties.PropertySet(ptr->PropertySet, properties);
			}
			return array;
		}

		// Token: 0x06000254 RID: 596 RVA: 0x000078C8 File Offset: 0x00005AC8
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		private unsafe DictionaryDBProperties.Property[] GetProperties(uint countProperties, DBPROP* nativeProperties)
		{
			DictionaryDBProperties.Property[] array = new DictionaryDBProperties.Property[countProperties];
			for (int i = 0; i < array.Length; i++)
			{
				DBPROP* ptr = nativeProperties + i;
				array[i] = new DictionaryDBProperties.Property(ptr->PropertyID, Variant.GetObject(&ptr->Variant));
			}
			return array;
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00007914 File Offset: 0x00005B14
		private unsafe void GetPropertySets(DictionaryDBProperties.PropertySet[] propertySets, ComHeap heap, out uint countPropertySets, [global::System.Runtime.CompilerServices.Nullable(0)] out DBPROPSET* nativePropertySets)
		{
			nativePropertySets = heap.AllocArray(propertySets.Length, sizeof(DBPROPSET));
			countPropertySets = (uint)propertySets.Length;
			for (int i = 0; i < propertySets.Length; i++)
			{
				DictionaryDBProperties.PropertySet propertySet = propertySets[i];
				DictionaryDBProperties.Property[] properties = propertySet.Properties;
				DBPROP* ptr = (DBPROP*)heap.AllocArray(properties.Length, sizeof(DBPROP));
				for (int j = 0; j < properties.Length; j++)
				{
					DictionaryDBProperties.Property property = properties[j];
					DBPROP* ptr2 = ptr + j;
					if (property.IsSupported)
					{
						ptr2->ColId = default(DBID);
						ptr2->Options = DBPROPOPTIONS.REQUIRED;
						ptr2->PropertyID = property.ID;
						ptr2->Status = DBPROPSTATUS.OK;
						Variant.SetValue(&ptr2->Variant, heap, property.Value);
					}
					else
					{
						ptr2->ColId = default(DBID);
						ptr2->Options = DBPROPOPTIONS.REQUIRED;
						ptr2->PropertyID = property.ID;
						ptr2->Status = DBPROPSTATUS.NOTSUPPORTED;
						Variant.Init(&ptr2->Variant);
						TraceSourceBase<OleDbBaseTraceSource>.Tracer.TraceWarning("Requested property {0}:{1} is not supported in the current context", new object[] { propertySet.Group, property.ID });
					}
				}
				UIntPtr uintPtr = (UIntPtr)(nativePropertySets + (IntPtr)i * (IntPtr)sizeof(DBPROPSET));
				uintPtr.PropertySet = propertySets[i].Group;
				uintPtr.PropertyCount = (uint)properties.Length;
				uintPtr.Properties = ptr;
			}
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00007A74 File Offset: 0x00005C74
		private Dictionary<Guid, List<PropertyInfo>> GetGroups(IEnumerable<PropertyInfo> propertyInfos)
		{
			Dictionary<Guid, List<PropertyInfo>> dictionary = new Dictionary<Guid, List<PropertyInfo>>();
			foreach (PropertyInfo propertyInfo in propertyInfos)
			{
				List<PropertyInfo> list;
				if (!dictionary.TryGetValue(propertyInfo.Group, out list))
				{
					list = new List<PropertyInfo>();
					dictionary.Add(propertyInfo.Group, list);
				}
				list.Add(propertyInfo);
			}
			return dictionary;
		}

		// Token: 0x04000091 RID: 145
		private readonly Dictionary<DBPROPID, PropertyInfo> propertyInfos;

		// Token: 0x04000092 RID: 146
		private readonly Dictionary<DBPROPID, object> values;

		// Token: 0x020000EA RID: 234
		[global::System.Runtime.CompilerServices.Nullable(0)]
		internal class PropertyIDSet
		{
			// Token: 0x060004B1 RID: 1201 RVA: 0x0000E4DF File Offset: 0x0000C6DF
			internal PropertyIDSet(Guid propertyGroup, DBPROPID[] ids)
			{
				this.propertyGroup = propertyGroup;
				this.ids = ids;
			}

			// Token: 0x170000F9 RID: 249
			// (get) Token: 0x060004B2 RID: 1202 RVA: 0x0000E4F5 File Offset: 0x0000C6F5
			public Guid Group
			{
				get
				{
					return this.propertyGroup;
				}
			}

			// Token: 0x170000FA RID: 250
			// (get) Token: 0x060004B3 RID: 1203 RVA: 0x0000E4FD File Offset: 0x0000C6FD
			public DBPROPID[] IDs
			{
				get
				{
					return this.ids;
				}
			}

			// Token: 0x040003FE RID: 1022
			private Guid propertyGroup;

			// Token: 0x040003FF RID: 1023
			private DBPROPID[] ids;
		}

		// Token: 0x020000EB RID: 235
		[global::System.Runtime.CompilerServices.Nullable(0)]
		internal class Property
		{
			// Token: 0x060004B4 RID: 1204 RVA: 0x0000E505 File Offset: 0x0000C705
			internal Property(DBPROPID id, object value)
			{
				this.id = id;
				this.value = value;
			}

			// Token: 0x170000FB RID: 251
			// (get) Token: 0x060004B5 RID: 1205 RVA: 0x0000E51B File Offset: 0x0000C71B
			public bool IsSupported
			{
				get
				{
					return this.value != DictionaryDBProperties.Property.notSupported;
				}
			}

			// Token: 0x170000FC RID: 252
			// (get) Token: 0x060004B6 RID: 1206 RVA: 0x0000E52D File Offset: 0x0000C72D
			public DBPROPID ID
			{
				get
				{
					return this.id;
				}
			}

			// Token: 0x170000FD RID: 253
			// (get) Token: 0x060004B7 RID: 1207 RVA: 0x0000E535 File Offset: 0x0000C735
			public object Value
			{
				get
				{
					return this.value;
				}
			}

			// Token: 0x060004B8 RID: 1208 RVA: 0x0000E53D File Offset: 0x0000C73D
			public static DictionaryDBProperties.Property NotSupported(DBPROPID id)
			{
				return new DictionaryDBProperties.Property(id, DictionaryDBProperties.Property.notSupported);
			}

			// Token: 0x04000400 RID: 1024
			private static object notSupported = new object();

			// Token: 0x04000401 RID: 1025
			private DBPROPID id;

			// Token: 0x04000402 RID: 1026
			private object value;
		}

		// Token: 0x020000EC RID: 236
		[global::System.Runtime.CompilerServices.Nullable(0)]
		internal class PropertySet
		{
			// Token: 0x060004BA RID: 1210 RVA: 0x0000E556 File Offset: 0x0000C756
			internal PropertySet(Guid propertyGroup, DictionaryDBProperties.Property[] properties)
			{
				this.propertyGroup = propertyGroup;
				this.properties = properties;
			}

			// Token: 0x170000FE RID: 254
			// (get) Token: 0x060004BB RID: 1211 RVA: 0x0000E56C File Offset: 0x0000C76C
			public Guid Group
			{
				get
				{
					return this.propertyGroup;
				}
			}

			// Token: 0x170000FF RID: 255
			// (get) Token: 0x060004BC RID: 1212 RVA: 0x0000E574 File Offset: 0x0000C774
			public DictionaryDBProperties.Property[] Properties
			{
				get
				{
					return this.properties;
				}
			}

			// Token: 0x04000403 RID: 1027
			private Guid propertyGroup;

			// Token: 0x04000404 RID: 1028
			private DictionaryDBProperties.Property[] properties;
		}

		// Token: 0x020000ED RID: 237
		[global::System.Runtime.CompilerServices.Nullable(0)]
		internal class PropertyInfoSet
		{
			// Token: 0x060004BD RID: 1213 RVA: 0x0000E57C File Offset: 0x0000C77C
			internal PropertyInfoSet(Guid propertyGroup, PropertyInfo[] propertyInfos)
			{
				this.propertyGroup = propertyGroup;
				this.propertyInfos = propertyInfos;
			}

			// Token: 0x17000100 RID: 256
			// (get) Token: 0x060004BE RID: 1214 RVA: 0x0000E592 File Offset: 0x0000C792
			public Guid Group
			{
				get
				{
					return this.propertyGroup;
				}
			}

			// Token: 0x17000101 RID: 257
			// (get) Token: 0x060004BF RID: 1215 RVA: 0x0000E59A File Offset: 0x0000C79A
			public PropertyInfo[] Infos
			{
				get
				{
					return this.propertyInfos;
				}
			}

			// Token: 0x04000405 RID: 1029
			private Guid propertyGroup;

			// Token: 0x04000406 RID: 1030
			private PropertyInfo[] propertyInfos;
		}
	}
}
