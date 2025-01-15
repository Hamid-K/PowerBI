using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008C1 RID: 2241
	[PersistedWithinRequestOnly]
	internal sealed class RuntimeSortDataHolder : IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06007AB2 RID: 31410 RVA: 0x001F983D File Offset: 0x001F7A3D
		internal RuntimeSortDataHolder()
		{
		}

		// Token: 0x06007AB3 RID: 31411 RVA: 0x001F9848 File Offset: 0x001F7A48
		internal void NextRow(OnDemandProcessingContext odpContext, int depth)
		{
			DataFieldRow dataFieldRow = new DataFieldRow(odpContext.ReportObjectModel.FieldsImpl, true);
			if (this.m_firstRow == null)
			{
				this.m_firstRow = dataFieldRow;
				return;
			}
			if (this.m_dataRows == null)
			{
				this.m_dataRows = new ScalableList<DataFieldRow>(depth, odpContext.TablixProcessingScalabilityCache);
			}
			this.m_dataRows.Add(dataFieldRow);
		}

		// Token: 0x06007AB4 RID: 31412 RVA: 0x001F98A0 File Offset: 0x001F7AA0
		internal void Traverse(ProcessingStages operation, ITraversalContext traversalContext, IHierarchyObj owner)
		{
			Global.Tracer.Assert(ProcessingStages.UserSortFilter == operation || owner.InDataRowSortPhase, "Invalid call to RuntimeSortDataHolder.Traverse.  Must be in UserSortFilter stage or InDataRowSortPhase");
			if (this.m_firstRow == null)
			{
				return;
			}
			DataRowSortOwnerTraversalContext dataRowSortOwnerTraversalContext = traversalContext as DataRowSortOwnerTraversalContext;
			this.Traverse(this.m_firstRow, operation, dataRowSortOwnerTraversalContext, owner);
			if (this.m_dataRows != null)
			{
				for (int i = 0; i < this.m_dataRows.Count; i++)
				{
					this.Traverse(this.m_dataRows[i], operation, dataRowSortOwnerTraversalContext, owner);
				}
			}
		}

		// Token: 0x06007AB5 RID: 31413 RVA: 0x001F991B File Offset: 0x001F7B1B
		private void Traverse(DataFieldRow dataRow, ProcessingStages operation, DataRowSortOwnerTraversalContext context, IHierarchyObj owner)
		{
			dataRow.SetFields(owner.OdpContext.ReportObjectModel.FieldsImpl);
			if (operation == ProcessingStages.UserSortFilter)
			{
				owner.ReadRow();
				return;
			}
			context.SortOwner.PostDataRowSortNextRow();
		}

		// Token: 0x06007AB6 RID: 31414 RVA: 0x001F994C File Offset: 0x001F7B4C
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(RuntimeSortDataHolder.m_declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.FirstRow)
				{
					if (memberName != MemberName.DataRows)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						writer.Write(this.m_dataRows);
					}
				}
				else
				{
					writer.Write(this.m_firstRow);
				}
			}
		}

		// Token: 0x06007AB7 RID: 31415 RVA: 0x001F99B8 File Offset: 0x001F7BB8
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(RuntimeSortDataHolder.m_declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.FirstRow)
				{
					if (memberName != MemberName.DataRows)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						this.m_dataRows = reader.ReadRIFObject<ScalableList<DataFieldRow>>();
					}
				}
				else
				{
					this.m_firstRow = (DataFieldRow)reader.ReadRIFObject();
				}
			}
		}

		// Token: 0x06007AB8 RID: 31416 RVA: 0x001F9A26 File Offset: 0x001F7C26
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
		}

		// Token: 0x06007AB9 RID: 31417 RVA: 0x001F9A28 File Offset: 0x001F7C28
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeSortDataHolder;
		}

		// Token: 0x06007ABA RID: 31418 RVA: 0x001F9A2C File Offset: 0x001F7C2C
		public static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (RuntimeSortDataHolder.m_declaration == null)
			{
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeSortDataHolder, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
				{
					new MemberInfo(MemberName.FirstRow, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataFieldRow),
					new MemberInfo(MemberName.DataRows, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScalableList)
				});
			}
			return RuntimeSortDataHolder.m_declaration;
		}

		// Token: 0x1700285D RID: 10333
		// (get) Token: 0x06007ABB RID: 31419 RVA: 0x001F9A76 File Offset: 0x001F7C76
		public int Size
		{
			get
			{
				return ItemSizes.SizeOf(this.m_firstRow) + ItemSizes.SizeOf<DataFieldRow>(this.m_dataRows);
			}
		}

		// Token: 0x04003D50 RID: 15696
		private DataFieldRow m_firstRow;

		// Token: 0x04003D51 RID: 15697
		private ScalableList<DataFieldRow> m_dataRows;

		// Token: 0x04003D52 RID: 15698
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = RuntimeSortDataHolder.GetDeclaration();
	}
}
