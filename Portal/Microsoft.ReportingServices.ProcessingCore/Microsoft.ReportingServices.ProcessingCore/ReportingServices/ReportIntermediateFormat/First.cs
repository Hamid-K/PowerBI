using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000461 RID: 1121
	internal sealed class First : DataAggregate
	{
		// Token: 0x06003395 RID: 13205 RVA: 0x000E4DB2 File Offset: 0x000E2FB2
		internal override void Init()
		{
			this.m_value = null;
			this.m_updated = false;
		}

		// Token: 0x06003396 RID: 13206 RVA: 0x000E4DC2 File Offset: 0x000E2FC2
		internal override void Update(object[] expressions, IErrorContext iErrorContext)
		{
			if (!this.m_updated)
			{
				this.m_value = expressions[0];
				this.m_updated = true;
			}
		}

		// Token: 0x06003397 RID: 13207 RVA: 0x000E4DDC File Offset: 0x000E2FDC
		internal override object Result()
		{
			return this.m_value;
		}

		// Token: 0x1700174C RID: 5964
		// (get) Token: 0x06003398 RID: 13208 RVA: 0x000E4DE4 File Offset: 0x000E2FE4
		internal override DataAggregateInfo.AggregateTypes AggregateType
		{
			get
			{
				return DataAggregateInfo.AggregateTypes.First;
			}
		}

		// Token: 0x06003399 RID: 13209 RVA: 0x000E4DE7 File Offset: 0x000E2FE7
		internal override DataAggregate ConstructAggregator(OnDemandProcessingContext odpContext, DataAggregateInfo aggregateDef)
		{
			return new First();
		}

		// Token: 0x0600339A RID: 13210 RVA: 0x000E4DF0 File Offset: 0x000E2FF0
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(First.m_declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.Value)
				{
					if (memberName != MemberName.Updated)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						writer.Write(this.m_updated);
					}
				}
				else
				{
					writer.Write(this.m_value);
				}
			}
		}

		// Token: 0x0600339B RID: 13211 RVA: 0x000E4E58 File Offset: 0x000E3058
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(First.m_declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.Value)
				{
					if (memberName != MemberName.Updated)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						this.m_updated = reader.ReadBoolean();
					}
				}
				else
				{
					this.m_value = reader.ReadVariant();
				}
			}
		}

		// Token: 0x0600339C RID: 13212 RVA: 0x000E4EBE File Offset: 0x000E30BE
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false, "First should not resolve references");
		}

		// Token: 0x0600339D RID: 13213 RVA: 0x000E4ED0 File Offset: 0x000E30D0
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.First;
		}

		// Token: 0x0600339E RID: 13214 RVA: 0x000E4ED4 File Offset: 0x000E30D4
		public static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (First.m_declaration == null)
			{
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.First, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataAggregate, new List<MemberInfo>
				{
					new MemberInfo(MemberName.Value, Token.Object),
					new MemberInfo(MemberName.Updated, Token.Boolean)
				});
			}
			return First.m_declaration;
		}

		// Token: 0x1700174D RID: 5965
		// (get) Token: 0x0600339F RID: 13215 RVA: 0x000E4F21 File Offset: 0x000E3121
		public override int Size
		{
			get
			{
				return ItemSizes.SizeOf(this.m_value) + 1;
			}
		}

		// Token: 0x040019CD RID: 6605
		private object m_value;

		// Token: 0x040019CE RID: 6606
		private bool m_updated;

		// Token: 0x040019CF RID: 6607
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = First.GetDeclaration();
	}
}
