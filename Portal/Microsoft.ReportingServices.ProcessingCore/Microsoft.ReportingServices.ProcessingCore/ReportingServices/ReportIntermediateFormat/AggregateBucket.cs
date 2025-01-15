using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004B3 RID: 1203
	public abstract class AggregateBucket<T> : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable where T : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06003C1E RID: 15390 RVA: 0x00103AFC File Offset: 0x00101CFC
		internal AggregateBucket()
		{
		}

		// Token: 0x06003C1F RID: 15391 RVA: 0x00103B04 File Offset: 0x00101D04
		internal AggregateBucket(int level)
		{
			this.m_aggregates = new List<T>();
			this.m_level = level;
		}

		// Token: 0x170019C5 RID: 6597
		// (get) Token: 0x06003C20 RID: 15392 RVA: 0x00103B1E File Offset: 0x00101D1E
		// (set) Token: 0x06003C21 RID: 15393 RVA: 0x00103B26 File Offset: 0x00101D26
		public List<T> Aggregates
		{
			get
			{
				return this.m_aggregates;
			}
			set
			{
				this.m_aggregates = value;
			}
		}

		// Token: 0x170019C6 RID: 6598
		// (get) Token: 0x06003C22 RID: 15394 RVA: 0x00103B2F File Offset: 0x00101D2F
		// (set) Token: 0x06003C23 RID: 15395 RVA: 0x00103B37 File Offset: 0x00101D37
		public int Level
		{
			get
			{
				return this.m_level;
			}
			set
			{
				this.m_level = value;
			}
		}

		// Token: 0x06003C24 RID: 15396
		protected abstract Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetSpecificDeclaration();

		// Token: 0x06003C25 RID: 15397
		public abstract Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType();

		// Token: 0x06003C26 RID: 15398 RVA: 0x00103B40 File Offset: 0x00101D40
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(this.GetSpecificDeclaration());
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.Aggregates)
				{
					if (memberName != MemberName.Level)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						writer.Write(this.m_level);
					}
				}
				else
				{
					writer.Write<T>(this.m_aggregates);
				}
			}
		}

		// Token: 0x06003C27 RID: 15399 RVA: 0x00103BB0 File Offset: 0x00101DB0
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(this.GetSpecificDeclaration());
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.Aggregates)
				{
					if (memberName != MemberName.Level)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						this.m_level = reader.ReadInt32();
					}
				}
				else
				{
					this.m_aggregates = reader.ReadGenericListOfRIFObjects<T>();
				}
			}
		}

		// Token: 0x06003C28 RID: 15400 RVA: 0x00103C1D File Offset: 0x00101E1D
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false, "No references to resolve.");
		}

		// Token: 0x04001C51 RID: 7249
		private List<T> m_aggregates;

		// Token: 0x04001C52 RID: 7250
		private int m_level;
	}
}
