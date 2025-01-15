using System;
using System.Collections;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006AD RID: 1709
	[Serializable]
	internal sealed class ScopeLookupTable
	{
		// Token: 0x17002088 RID: 8328
		// (get) Token: 0x06005CA0 RID: 23712 RVA: 0x00179C03 File Offset: 0x00177E03
		// (set) Token: 0x06005CA1 RID: 23713 RVA: 0x00179C0B File Offset: 0x00177E0B
		internal object LookupTable
		{
			get
			{
				return this.m_lookupTable;
			}
			set
			{
				this.m_lookupTable = value;
			}
		}

		// Token: 0x06005CA2 RID: 23714 RVA: 0x00179C14 File Offset: 0x00177E14
		internal void Clear()
		{
			Hashtable hashtable = this.m_lookupTable as Hashtable;
			if (hashtable != null)
			{
				hashtable.Clear();
			}
		}

		// Token: 0x06005CA3 RID: 23715 RVA: 0x00179C38 File Offset: 0x00177E38
		internal void Add(GroupingList scopeDefs, VariantList[] scopeValues, int value)
		{
			if (scopeValues == null || scopeValues.Length == 0)
			{
				Global.Tracer.Assert(scopeDefs == null || scopeDefs.Count == 0, "(null == scopeDefs || 0 == scopeDefs.Count)");
				this.m_lookupTable = value;
				return;
			}
			bool flag = true;
			if (this.m_lookupTable == null)
			{
				this.m_lookupTable = new Hashtable();
				flag = false;
			}
			Hashtable hashtable = (Hashtable)this.m_lookupTable;
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < scopeValues.Length; i++)
			{
				VariantList variantList = scopeValues[i];
				if (variantList == null)
				{
					num2++;
				}
				else
				{
					num = variantList.Count;
					if (i == scopeValues.Length - 1)
					{
						num--;
					}
					this.GetNullScopeEntries(num2, ref hashtable, ref flag);
					for (int j = 0; j < num; j++)
					{
						Hashtable hashtable2;
						if (flag)
						{
							hashtable2 = (Hashtable)hashtable[variantList[j]];
						}
						else
						{
							hashtable2 = null;
						}
						if (hashtable2 == null)
						{
							hashtable2 = new Hashtable();
							hashtable.Add(variantList[j], hashtable2);
							flag = false;
						}
						hashtable = hashtable2;
					}
					num2 = 0;
				}
			}
			object obj = 1;
			if (scopeValues[scopeValues.Length - 1] != null)
			{
				obj = scopeValues[scopeValues.Length - 1][num];
			}
			else
			{
				this.GetNullScopeEntries(num2, ref hashtable, ref flag);
			}
			Global.Tracer.Assert(!hashtable.Contains(obj), "(!hashEntries.Contains(lastKey))");
			hashtable.Add(obj, value);
		}

		// Token: 0x06005CA4 RID: 23716 RVA: 0x00179D8C File Offset: 0x00177F8C
		private void GetNullScopeEntries(int nullScopes, ref Hashtable hashEntries, ref bool lookup)
		{
			Hashtable hashtable = null;
			if (lookup)
			{
				hashtable = (Hashtable)hashEntries[nullScopes];
			}
			if (hashtable == null)
			{
				hashtable = new Hashtable();
				hashEntries.Add(nullScopes, hashtable);
				lookup = false;
			}
			hashEntries = hashtable;
		}

		// Token: 0x06005CA5 RID: 23717 RVA: 0x00179DD0 File Offset: 0x00177FD0
		internal int Lookup(GroupingList scopeDefs, VariantList[] scopeValues)
		{
			object obj = null;
			if (scopeValues == null || scopeValues.Length == 0)
			{
				Global.Tracer.Assert(scopeDefs == null || scopeDefs.Count == 0, "(null == scopeDefs || 0 == scopeDefs.Count)");
				obj = this.m_lookupTable;
			}
			else
			{
				Hashtable hashtable = (Hashtable)this.m_lookupTable;
				int num = 0;
				for (int i = 0; i < scopeValues.Length; i++)
				{
					VariantList variantList = scopeValues[i];
					if (variantList == null)
					{
						num++;
					}
					else
					{
						hashtable = (Hashtable)hashtable[num];
						for (int j = 0; j < variantList.Count; j++)
						{
							obj = hashtable[variantList[j]];
							if (i < scopeValues.Length - 1 || j < variantList.Count - 1)
							{
								hashtable = (Hashtable)obj;
								Global.Tracer.Assert(hashtable != null, "(null != hashEntries)");
							}
						}
						num = 0;
					}
				}
				if (scopeValues[scopeValues.Length - 1] == null)
				{
					hashtable = (Hashtable)hashtable[num];
					obj = hashtable[1];
				}
			}
			Global.Tracer.Assert(obj is int);
			return (int)obj;
		}

		// Token: 0x06005CA6 RID: 23718 RVA: 0x00179EE8 File Offset: 0x001780E8
		internal static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.None, new MemberInfoList
			{
				new MemberInfo(MemberName.LookupTable, Token.Object)
			});
		}

		// Token: 0x04002F97 RID: 12183
		private object m_lookupTable;
	}
}
