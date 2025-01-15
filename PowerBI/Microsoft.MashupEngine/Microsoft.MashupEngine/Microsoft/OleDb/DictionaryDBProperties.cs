using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.OleDb
{
	// Token: 0x02001E94 RID: 7828
	public sealed class DictionaryDBProperties : IDBProperties
	{
		// Token: 0x0600C18A RID: 49546 RVA: 0x0026E620 File Offset: 0x0026C820
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

		// Token: 0x17002F59 RID: 12121
		// (get) Token: 0x0600C18B RID: 49547 RVA: 0x0026E686 File Offset: 0x0026C886
		public Dictionary<DBPROPID, object> Dictionary
		{
			get
			{
				return this.values;
			}
		}

		// Token: 0x0600C18C RID: 49548 RVA: 0x0026E68E File Offset: 0x0026C88E
		public void AddPropertyInfo(PropertyInfo propertyInfo)
		{
			this.propertyInfos.Add(propertyInfo.ID, propertyInfo);
			this.values.Add(propertyInfo.ID, propertyInfo.Default);
		}

		// Token: 0x0600C18D RID: 49549 RVA: 0x0026E6BC File Offset: 0x0026C8BC
		unsafe int IDBProperties.GetProperties(uint countPropertyIDSets, DBPROPIDSET* nativePropertyIDSets, out uint _countPropertySets, out DBPROPSET* _nativePropertySets)
		{
			_countPropertySets = 0U;
			_nativePropertySets = (IntPtr)((UIntPtr)0);
			DbPropertyIDSet[] propertyIDSets = this.GetPropertyIDSets(countPropertyIDSets, nativePropertyIDSets);
			DbPropertySet[] propertySets = this.GetPropertySets(propertyIDSets, this.propertyInfos.Values);
			int propertyStatus;
			using (ComHeap comHeap = new ComHeap())
			{
				uint num;
				DBPROPSET* ptr;
				DbPropertySets.GetPropertySets(propertySets, comHeap, out num, out ptr);
				if (num > 0U)
				{
					_countPropertySets = num;
					_nativePropertySets = ptr;
					comHeap.Commit();
				}
				propertyStatus = DictionaryDBProperties.GetPropertyStatus(propertyIDSets, propertySets);
			}
			return propertyStatus;
		}

		// Token: 0x0600C18E RID: 49550 RVA: 0x0026E738 File Offset: 0x0026C938
		unsafe int IDBProperties.GetPropertyInfo(uint countPropertyIDSets, DBPROPIDSET* nativePropertyIDSets, out uint _countPropertyInfoSets, out DBPROPINFOSET* _nativePropertyInfoSets, char** _nativeDescriptions)
		{
			_countPropertyInfoSets = 0U;
			_nativePropertyInfoSets = (IntPtr)((UIntPtr)0);
			if (_nativeDescriptions != null)
			{
				*(IntPtr*)_nativeDescriptions = (IntPtr)((UIntPtr)0);
			}
			DbPropertyIDSet[] propertyIDSets = this.GetPropertyIDSets(countPropertyIDSets, nativePropertyIDSets);
			DbPropertyInfoSet[] propertyInfoSets = DictionaryDBProperties.GetPropertyInfoSets(propertyIDSets, this.propertyInfos.Values);
			int propertyInfoStatus;
			using (ComHeap comHeap = new ComHeap())
			{
				uint num;
				DBPROPINFOSET* ptr;
				char* ptr2;
				this.GetPropertyInfoSets(propertyInfoSets, comHeap, out num, out ptr, out ptr2);
				if (num > 0U)
				{
					_countPropertyInfoSets = num;
					_nativePropertyInfoSets = ptr;
					if (_nativeDescriptions != null)
					{
						*(IntPtr*)_nativeDescriptions = ptr2;
					}
					comHeap.Commit();
				}
				propertyInfoStatus = DictionaryDBProperties.GetPropertyInfoStatus(propertyIDSets, propertyInfoSets);
			}
			return propertyInfoStatus;
		}

		// Token: 0x0600C18F RID: 49551 RVA: 0x0026E7CC File Offset: 0x0026C9CC
		unsafe int IDBProperties.SetProperties(uint cPropertySets, DBPROPSET* nativePropertySets)
		{
			DbPropertySet[] propertySets = DbPropertySets.GetPropertySets(cPropertySets, nativePropertySets);
			bool flag = true;
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < propertySets.Length; i++)
			{
				DbProperty[] properties = propertySets[i].Properties;
				int j = 0;
				while (j < properties.Length)
				{
					DbProperty dbProperty = properties[j];
					DBPROPID id = dbProperty.ID;
					object value = dbProperty.Value;
					PropertyInfo propertyInfo;
					if (!this.propertyInfos.TryGetValue(id, out propertyInfo) || propertyInfo.Type != DictionaryDBProperties.GetVarType(value))
					{
						goto IL_00A3;
					}
					if (object.Equals(value, this.values[id]))
					{
						num2++;
					}
					else
					{
						if ((propertyInfo.Flags & DBPROPFLAGS.WRITE) != DBPROPFLAGS.WRITE)
						{
							goto IL_00A3;
						}
						this.values[id] = value;
						num2++;
					}
					IL_00C8:
					j++;
					continue;
					IL_00A3:
					if (nativePropertySets[i].rgProperties[j].dwOptions == DBPROPOPTIONS.REQUIRED)
					{
						flag = false;
						goto IL_00C8;
					}
					goto IL_00C8;
				}
				num += properties.Length;
			}
			if (num == num2)
			{
				return 0;
			}
			if (num2 != 0 && flag)
			{
				return 265946;
			}
			return -2147217887;
		}

		// Token: 0x0600C190 RID: 49552 RVA: 0x0026E8E2 File Offset: 0x0026CAE2
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
			if (value is bool)
			{
				return VARTYPE.BOOL;
			}
			return VARTYPE.NULL;
		}

		// Token: 0x0600C191 RID: 49553 RVA: 0x0026E904 File Offset: 0x0026CB04
		private static int GetPropertyInfoStatus(DbPropertyIDSet[] propertyIDSets, DbPropertyInfoSet[] propertyInfoSets)
		{
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < propertyIDSets.Length; i++)
			{
				DbPropertyIDSet dbPropertyIDSet = propertyIDSets[i];
				DbPropertyInfoSet dbPropertyInfoSet = propertyInfoSets[i];
				if (dbPropertyIDSet.IDs.Length != 0)
				{
					num += dbPropertyIDSet.IDs.Length;
					for (int j = 0; j < dbPropertyInfoSet.Infos.Length; j++)
					{
						if (dbPropertyInfoSet.Infos[j].Flags != DBPROPFLAGS.NOTSUPPORTED)
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

		// Token: 0x0600C192 RID: 49554 RVA: 0x0026E984 File Offset: 0x0026CB84
		private static int GetPropertyStatus(DbPropertyIDSet[] propertyIDSets, DbPropertySet[] propertySets)
		{
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < propertyIDSets.Length; i++)
			{
				DbPropertyIDSet dbPropertyIDSet = propertyIDSets[i];
				DbPropertySet dbPropertySet = propertySets[i];
				if (dbPropertyIDSet.IDs.Length != 0)
				{
					num += dbPropertyIDSet.IDs.Length;
					for (int j = 0; j < dbPropertySet.Properties.Length; j++)
					{
						if (dbPropertySet.Properties[j].IsSupported)
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

		// Token: 0x0600C193 RID: 49555 RVA: 0x0026EA04 File Offset: 0x0026CC04
		private unsafe DbPropertyIDSet[] GetPropertyIDSets(uint countPropertyIDSets, DBPROPIDSET* nativePropertyIDSets)
		{
			DbPropertyIDSet[] array = new DbPropertyIDSet[countPropertyIDSets];
			for (int i = 0; i < array.Length; i++)
			{
				DBPROPIDSET* ptr = nativePropertyIDSets + i;
				DBPROPID[] array2 = new DBPROPID[ptr->cPropertyIDs];
				Guid group = DictionaryDBProperties.GetGroup(ptr->guidPropertySet);
				for (int j = 0; j < array2.Length; j++)
				{
					array2[j] = ptr->rgPropertyIDs[j];
				}
				array[i] = new DbPropertyIDSet(group, array2);
			}
			return array;
		}

		// Token: 0x0600C194 RID: 49556 RVA: 0x0026EA7C File Offset: 0x0026CC7C
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

		// Token: 0x0600C195 RID: 49557 RVA: 0x0026EB24 File Offset: 0x0026CD24
		private unsafe void GetPropertyInfoSets(DbPropertyInfoSet[] propertyInfoSets, ComHeap heap, out uint countPropertyInfoSets, out DBPROPINFOSET* nativePropertyInfoSets, out char* nativeDescriptions)
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
				DbPropertyInfoSet dbPropertyInfoSet = propertyInfoSets[k];
				DBPROPINFOSET* ptr = nativePropertyInfoSets / (IntPtr)sizeof(DBPROPINFOSET) + k * sizeof(DBPROPINFOSET);
				PropertyInfo[] infos2 = dbPropertyInfoSet.Infos;
				DBPROPINFO* ptr2 = (DBPROPINFO*)heap.AllocArray(infos2.Length, sizeof(DBPROPINFO));
				for (int l = 0; l < infos2.Length; l++)
				{
					PropertyInfo propertyInfo = infos2[l];
					DBPROPINFO* ptr3 = ptr2 + l;
					ptr3->description = nativeDescriptions / 2 + num * 2;
					ptr3->dwFlags = propertyInfo.Flags;
					ptr3->dwPropertyID = propertyInfo.ID;
					ptr3->vtType = propertyInfo.Type;
					Variant.Init(&ptr3->vValues);
					num += propertyInfo.Description.Length + 1;
				}
				ptr->guidPropertySet = dbPropertyInfoSet.Group;
				ptr->countPropertyInfos = (uint)infos2.Length;
				ptr->propertyInfos = ptr2;
			}
		}

		// Token: 0x0600C196 RID: 49558 RVA: 0x0026EC88 File Offset: 0x0026CE88
		private static DbPropertyInfoSet[] GetPropertyInfoSets(IList<DbPropertyIDSet> propertyIDSets, IEnumerable<PropertyInfo> propertyInfos)
		{
			Dictionary<Guid, List<PropertyInfo>> groups = DictionaryDBProperties.GetGroups(propertyInfos);
			if (propertyIDSets.Count == 0)
			{
				return DictionaryDBProperties.GetPropertyInfoSets(groups);
			}
			return DictionaryDBProperties.GetPropertyInfoSets(groups, propertyIDSets);
		}

		// Token: 0x0600C197 RID: 49559 RVA: 0x0026ECB4 File Offset: 0x0026CEB4
		private static DbPropertyInfoSet[] GetPropertyInfoSets(Dictionary<Guid, List<PropertyInfo>> groups)
		{
			DbPropertyInfoSet[] array = new DbPropertyInfoSet[groups.Count];
			int num = 0;
			foreach (KeyValuePair<Guid, List<PropertyInfo>> keyValuePair in groups)
			{
				array[num] = new DbPropertyInfoSet(keyValuePair.Key, keyValuePair.Value.ToArray());
				num++;
			}
			return array;
		}

		// Token: 0x0600C198 RID: 49560 RVA: 0x0026ED2C File Offset: 0x0026CF2C
		private static DbPropertyInfoSet[] GetPropertyInfoSets(Dictionary<Guid, List<PropertyInfo>> groups, IList<DbPropertyIDSet> propertyIDSets)
		{
			DbPropertyInfoSet[] array = new DbPropertyInfoSet[propertyIDSets.Count];
			for (int i = 0; i < propertyIDSets.Count; i++)
			{
				DbPropertyIDSet dbPropertyIDSet = propertyIDSets[i];
				List<PropertyInfo> list;
				PropertyInfo[] array2;
				if (groups.TryGetValue(dbPropertyIDSet.Group, out list))
				{
					if (dbPropertyIDSet.IDs.Length == 0)
					{
						array2 = list.ToArray();
					}
					else
					{
						array2 = DictionaryDBProperties.GetPropertyInfos(dbPropertyIDSet.IDs, list);
					}
				}
				else
				{
					array2 = new PropertyInfo[0];
				}
				array[i] = new DbPropertyInfoSet(dbPropertyIDSet.Group, array2);
			}
			return array;
		}

		// Token: 0x0600C199 RID: 49561 RVA: 0x0026EDAC File Offset: 0x0026CFAC
		private static PropertyInfo[] GetPropertyInfos(IList<DBPROPID> propertyIDs, IList<PropertyInfo> propertyInfos)
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

		// Token: 0x0600C19A RID: 49562 RVA: 0x0026EE3D File Offset: 0x0026D03D
		private DbPropertySet[] GetPropertySets(IList<DbPropertyIDSet> propertyIDSets, IEnumerable<PropertyInfo> propertyInfos)
		{
			if (propertyIDSets.Count == 0)
			{
				return this.GetPropertySets(DictionaryDBProperties.GetGroups(propertyInfos));
			}
			return this.GetPropertySets(propertyIDSets);
		}

		// Token: 0x0600C19B RID: 49563 RVA: 0x0026EE5C File Offset: 0x0026D05C
		private DbPropertySet[] GetPropertySets(Dictionary<Guid, List<PropertyInfo>> groups)
		{
			DbPropertySet[] array = new DbPropertySet[groups.Count];
			int num = 0;
			foreach (KeyValuePair<Guid, List<PropertyInfo>> keyValuePair in groups)
			{
				array[num] = new DbPropertySet(keyValuePair.Key, this.GetProperties(keyValuePair.Value));
				num++;
			}
			return array;
		}

		// Token: 0x0600C19C RID: 49564 RVA: 0x0026EED4 File Offset: 0x0026D0D4
		private DbPropertySet[] GetPropertySets(IList<DbPropertyIDSet> propertyIDSets)
		{
			DbPropertySet[] array = new DbPropertySet[propertyIDSets.Count];
			for (int i = 0; i < propertyIDSets.Count; i++)
			{
				DbProperty[] properties = this.GetProperties(propertyIDSets[i].IDs);
				array[i] = new DbPropertySet(propertyIDSets[i].Group, properties);
			}
			return array;
		}

		// Token: 0x0600C19D RID: 49565 RVA: 0x0026EF28 File Offset: 0x0026D128
		private DbProperty[] GetProperties(IList<PropertyInfo> propertyInfos)
		{
			DbProperty[] array = new DbProperty[propertyInfos.Count];
			for (int i = 0; i < array.Length; i++)
			{
				DBPROPID id = propertyInfos[i].ID;
				array[i] = new DbProperty(id, this.values[id]);
			}
			return array;
		}

		// Token: 0x0600C19E RID: 49566 RVA: 0x0026EF74 File Offset: 0x0026D174
		private DbProperty[] GetProperties(IList<DBPROPID> propertyIDs)
		{
			DbProperty[] array = new DbProperty[propertyIDs.Count];
			for (int i = 0; i < propertyIDs.Count; i++)
			{
				DBPROPID dbpropid = propertyIDs[i];
				object obj;
				DbProperty dbProperty;
				if (this.values.TryGetValue(dbpropid, out obj))
				{
					dbProperty = new DbProperty(dbpropid, obj);
				}
				else
				{
					dbProperty = DbProperty.NotSupported(dbpropid);
				}
				array[i] = dbProperty;
			}
			return array;
		}

		// Token: 0x0600C19F RID: 49567 RVA: 0x0026EFD0 File Offset: 0x0026D1D0
		private static Dictionary<Guid, List<PropertyInfo>> GetGroups(IEnumerable<PropertyInfo> propertyInfos)
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

		// Token: 0x0400619B RID: 24987
		private readonly Dictionary<DBPROPID, PropertyInfo> propertyInfos;

		// Token: 0x0400619C RID: 24988
		private readonly Dictionary<DBPROPID, object> values;
	}
}
