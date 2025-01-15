using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020003BC RID: 956
	[Serializable]
	internal sealed class ScopeLookupTable : IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x170013E4 RID: 5092
		// (get) Token: 0x060026BE RID: 9918 RVA: 0x000B98A4 File Offset: 0x000B7AA4
		// (set) Token: 0x060026BF RID: 9919 RVA: 0x000B98AC File Offset: 0x000B7AAC
		internal Hashtable LookupTable
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

		// Token: 0x170013E5 RID: 5093
		// (get) Token: 0x060026C0 RID: 9920 RVA: 0x000B98B5 File Offset: 0x000B7AB5
		// (set) Token: 0x060026C1 RID: 9921 RVA: 0x000B98BD File Offset: 0x000B7ABD
		internal int LookupInt
		{
			get
			{
				return this.m_lookupInt;
			}
			set
			{
				this.m_lookupInt = value;
			}
		}

		// Token: 0x060026C2 RID: 9922 RVA: 0x000B98C8 File Offset: 0x000B7AC8
		internal void Clear()
		{
			Hashtable lookupTable = this.m_lookupTable;
			if (lookupTable != null)
			{
				lookupTable.Clear();
			}
		}

		// Token: 0x060026C3 RID: 9923 RVA: 0x000B98E8 File Offset: 0x000B7AE8
		internal void Add(GroupingList scopeDefs, List<object>[] scopeValues, int value)
		{
			if (scopeValues == null || scopeValues.Length == 0)
			{
				Global.Tracer.Assert(scopeDefs == null || scopeDefs.Count == 0, "(null == scopeDefs || 0 == scopeDefs.Count)");
				this.m_lookupInt = value;
				return;
			}
			bool flag = true;
			if (this.m_lookupTable == null)
			{
				this.m_lookupTable = new Hashtable();
				flag = false;
			}
			Hashtable hashtable = this.m_lookupTable;
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < scopeValues.Length; i++)
			{
				List<object> list = scopeValues[i];
				if (list == null)
				{
					num2++;
				}
				else
				{
					num = list.Count;
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
							hashtable2 = (Hashtable)hashtable[list[j]];
						}
						else
						{
							hashtable2 = null;
						}
						if (hashtable2 == null)
						{
							hashtable2 = new Hashtable();
							hashtable.Add(list[j], hashtable2);
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

		// Token: 0x060026C4 RID: 9924 RVA: 0x000B9A34 File Offset: 0x000B7C34
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

		// Token: 0x060026C5 RID: 9925 RVA: 0x000B9A78 File Offset: 0x000B7C78
		internal int Lookup(GroupingList scopeDefs, List<object>[] scopeValues)
		{
			object obj = null;
			if (scopeValues == null || scopeValues.Length == 0)
			{
				Global.Tracer.Assert(scopeDefs == null || scopeDefs.Count == 0, "(null == scopeDefs || 0 == scopeDefs.Count)");
				obj = this.m_lookupInt;
			}
			else
			{
				Hashtable hashtable = this.m_lookupTable;
				int num = 0;
				for (int i = 0; i < scopeValues.Length; i++)
				{
					List<object> list = scopeValues[i];
					if (list == null)
					{
						num++;
					}
					else
					{
						hashtable = (Hashtable)hashtable[num];
						for (int j = 0; j < list.Count; j++)
						{
							obj = hashtable[list[j]];
							if (i < scopeValues.Length - 1 || j < list.Count - 1)
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
			Global.Tracer.Assert(obj is int, "(value is int)");
			return (int)obj;
		}

		// Token: 0x060026C6 RID: 9926 RVA: 0x000B9B98 File Offset: 0x000B7D98
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScopeLookupTable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.LookupInt, Token.Int32),
				new MemberInfo(MemberName.LookupTable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.NLevelVariantHashtable, Token.Object)
			});
		}

		// Token: 0x060026C7 RID: 9927 RVA: 0x000B9BE4 File Offset: 0x000B7DE4
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(ScopeLookupTable.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.LookupTable)
				{
					if (memberName == MemberName.LookupInt)
					{
						writer.Write(this.m_lookupInt);
					}
					else
					{
						Global.Tracer.Assert(false);
					}
				}
				else
				{
					writer.WriteNLevelVariantHashtable(this.m_lookupTable);
				}
			}
		}

		// Token: 0x060026C8 RID: 9928 RVA: 0x000B9C50 File Offset: 0x000B7E50
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(ScopeLookupTable.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.LookupTable)
				{
					if (memberName == MemberName.LookupInt)
					{
						this.m_lookupInt = reader.ReadInt32();
					}
					else
					{
						Global.Tracer.Assert(false);
					}
				}
				else
				{
					this.m_lookupTable = reader.ReadNLevelVariantHashtable();
				}
			}
		}

		// Token: 0x060026C9 RID: 9929 RVA: 0x000B9CBA File Offset: 0x000B7EBA
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
		}

		// Token: 0x060026CA RID: 9930 RVA: 0x000B9CBC File Offset: 0x000B7EBC
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScopeLookupTable;
		}

		// Token: 0x170013E6 RID: 5094
		// (get) Token: 0x060026CB RID: 9931 RVA: 0x000B9CC3 File Offset: 0x000B7EC3
		public int Size
		{
			get
			{
				return 4 + ItemSizes.SizeOf(this.m_lookupTable);
			}
		}

		// Token: 0x04001658 RID: 5720
		private int m_lookupInt;

		// Token: 0x04001659 RID: 5721
		private Hashtable m_lookupTable;

		// Token: 0x0400165A RID: 5722
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = ScopeLookupTable.GetDeclaration();
	}
}
