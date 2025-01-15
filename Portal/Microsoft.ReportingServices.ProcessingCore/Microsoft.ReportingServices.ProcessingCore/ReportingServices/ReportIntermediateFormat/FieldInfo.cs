using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000410 RID: 1040
	internal class FieldInfo : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06002CDE RID: 11486 RVA: 0x000CE317 File Offset: 0x000CC517
		internal FieldInfo()
		{
			this.m_propertyErrorRegistered = new bool[0];
		}

		// Token: 0x06002CDF RID: 11487 RVA: 0x000CE32B File Offset: 0x000CC52B
		internal FieldInfo(List<int> aPropIndices, List<string> aPropNames)
		{
			this.m_propertyReaderIndices = aPropIndices;
			this.m_propertyNames = aPropNames;
			this.m_propertyErrorRegistered = new bool[aPropIndices.Count];
		}

		// Token: 0x17001599 RID: 5529
		// (get) Token: 0x06002CE0 RID: 11488 RVA: 0x000CE352 File Offset: 0x000CC552
		internal int PropertyCount
		{
			get
			{
				if (this.PropertyReaderIndices == null)
				{
					return 0;
				}
				return this.PropertyReaderIndices.Count;
			}
		}

		// Token: 0x1700159A RID: 5530
		// (get) Token: 0x06002CE1 RID: 11489 RVA: 0x000CE369 File Offset: 0x000CC569
		internal List<int> PropertyReaderIndices
		{
			get
			{
				return this.m_propertyReaderIndices;
			}
		}

		// Token: 0x1700159B RID: 5531
		// (get) Token: 0x06002CE2 RID: 11490 RVA: 0x000CE371 File Offset: 0x000CC571
		internal List<string> PropertyNames
		{
			get
			{
				return this.m_propertyNames;
			}
		}

		// Token: 0x06002CE3 RID: 11491 RVA: 0x000CE379 File Offset: 0x000CC579
		internal bool IsPropertyErrorRegistered(int aIndex)
		{
			return this.m_propertyErrorRegistered != null && this.m_propertyErrorRegistered[aIndex];
		}

		// Token: 0x06002CE4 RID: 11492 RVA: 0x000CE38D File Offset: 0x000CC58D
		internal void SetPropertyErrorRegistered(int aIndex)
		{
			this.m_propertyErrorRegistered[aIndex] = true;
		}

		// Token: 0x06002CE5 RID: 11493 RVA: 0x000CE398 File Offset: 0x000CC598
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.FieldInfo, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.FieldPropertyNames, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveList, Token.String),
				new MemberInfo(MemberName.FieldPropertyReaderIndices, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveList, Token.Int32)
			});
		}

		// Token: 0x06002CE6 RID: 11494 RVA: 0x000CE3DC File Offset: 0x000CC5DC
		public virtual void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(FieldInfo.m_declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.FieldPropertyNames)
				{
					if (memberName != MemberName.FieldPropertyReaderIndices)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						writer.WriteListOfPrimitives<int>(this.m_propertyReaderIndices);
					}
				}
				else
				{
					writer.WriteListOfPrimitives<string>(this.m_propertyNames);
				}
			}
		}

		// Token: 0x06002CE7 RID: 11495 RVA: 0x000CE444 File Offset: 0x000CC644
		public virtual void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(FieldInfo.m_declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.FieldPropertyNames)
				{
					if (memberName != MemberName.FieldPropertyReaderIndices)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						this.m_propertyReaderIndices = reader.ReadListOfPrimitives<int>();
					}
				}
				else
				{
					this.m_propertyNames = reader.ReadListOfPrimitives<string>();
				}
			}
		}

		// Token: 0x06002CE8 RID: 11496 RVA: 0x000CE4A9 File Offset: 0x000CC6A9
		public virtual void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x06002CE9 RID: 11497 RVA: 0x000CE4B6 File Offset: 0x000CC6B6
		public virtual Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.FieldInfo;
		}

		// Token: 0x0400180A RID: 6154
		private List<int> m_propertyReaderIndices;

		// Token: 0x0400180B RID: 6155
		private List<string> m_propertyNames;

		// Token: 0x0400180C RID: 6156
		[NonSerialized]
		internal bool ErrorRegistered;

		// Token: 0x0400180D RID: 6157
		[NonSerialized]
		internal bool Missing;

		// Token: 0x0400180E RID: 6158
		[NonSerialized]
		private readonly bool[] m_propertyErrorRegistered;

		// Token: 0x0400180F RID: 6159
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = FieldInfo.GetDeclaration();
	}
}
