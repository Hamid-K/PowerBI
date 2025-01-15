using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008ED RID: 2285
	[PersistedWithinRequestOnly]
	public abstract class RuntimeGroupObj : RuntimeHierarchyObj
	{
		// Token: 0x06007DB5 RID: 32181 RVA: 0x002071FB File Offset: 0x002053FB
		protected RuntimeGroupObj()
		{
		}

		// Token: 0x06007DB6 RID: 32182 RVA: 0x00207203 File Offset: 0x00205403
		protected RuntimeGroupObj(OnDemandProcessingContext odpContext, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, int level)
			: base(odpContext, objectType, level)
		{
		}

		// Token: 0x170028F8 RID: 10488
		// (get) Token: 0x06007DB7 RID: 32183 RVA: 0x0020720E File Offset: 0x0020540E
		// (set) Token: 0x06007DB8 RID: 32184 RVA: 0x00207216 File Offset: 0x00205416
		internal RuntimeGroupLeafObjReference LastChild
		{
			get
			{
				return this.m_lastChild;
			}
			set
			{
				this.m_lastChild = value;
			}
		}

		// Token: 0x170028F9 RID: 10489
		// (get) Token: 0x06007DB9 RID: 32185 RVA: 0x0020721F File Offset: 0x0020541F
		// (set) Token: 0x06007DBA RID: 32186 RVA: 0x00207227 File Offset: 0x00205427
		internal RuntimeGroupLeafObjReference FirstChild
		{
			get
			{
				return this.m_firstChild;
			}
			set
			{
				this.m_firstChild = value;
			}
		}

		// Token: 0x170028FA RID: 10490
		// (get) Token: 0x06007DBB RID: 32187 RVA: 0x00207230 File Offset: 0x00205430
		internal virtual int RecursiveLevel
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x06007DBC RID: 32188 RVA: 0x00207234 File Offset: 0x00205434
		internal void AddChild(RuntimeGroupLeafObjReference child)
		{
			if (null != this.m_lastChild)
			{
				using (this.m_lastChild.PinValue())
				{
					this.m_lastChild.Value().NextLeaf = child;
					goto IL_003E;
				}
			}
			this.m_firstChild = child;
			IL_003E:
			using (child.PinValue())
			{
				RuntimeGroupLeafObj runtimeGroupLeafObj = child.Value();
				runtimeGroupLeafObj.PrevLeaf = this.m_lastChild;
				runtimeGroupLeafObj.NextLeaf = null;
				runtimeGroupLeafObj.Parent = (RuntimeGroupObjReference)this.m_selfReference;
			}
			this.m_lastChild = child;
		}

		// Token: 0x06007DBD RID: 32189 RVA: 0x002072E0 File Offset: 0x002054E0
		internal void InsertToSortTree(RuntimeGroupLeafObjReference groupLeaf)
		{
			using (this.m_hierarchyRoot.PinValue())
			{
				RuntimeGroupRootObj runtimeGroupRootObj = (RuntimeGroupRootObj)this.m_hierarchyRoot.Value();
				Microsoft.ReportingServices.ReportIntermediateFormat.Grouping grouping = runtimeGroupRootObj.HierarchyDef.Grouping;
				if (runtimeGroupRootObj.ProcessSecondPassSorting)
				{
					Global.Tracer.Assert(this.m_grouping != null, "(m_grouping != null)");
					runtimeGroupRootObj.LastChild = groupLeaf;
					Global.Tracer.Assert(grouping != null, "(null != groupingDef)");
					object obj = this.m_odpContext.ReportRuntime.EvaluateRuntimeExpression(this.m_expression, Microsoft.ReportingServices.ReportProcessing.ObjectType.Grouping, grouping.Name, "Sort");
					this.m_grouping.NextRow(obj);
				}
				else
				{
					Global.Tracer.Assert(runtimeGroupRootObj.HierarchyDef.HasFilters || runtimeGroupRootObj.HierarchyDef.HasInnerFilters, "(groupRoot.HierarchyDef.HasFilters || groupRoot.HierarchyDef.HasInnerFilters)");
					this.AddChild(groupLeaf);
				}
			}
		}

		// Token: 0x06007DBE RID: 32190 RVA: 0x002073D0 File Offset: 0x002055D0
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(RuntimeGroupObj.m_declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.FirstChild)
				{
					if (memberName != MemberName.LastChild)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						writer.Write(this.m_lastChild);
					}
				}
				else
				{
					writer.Write(this.m_firstChild);
				}
			}
		}

		// Token: 0x06007DBF RID: 32191 RVA: 0x00207444 File Offset: 0x00205644
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(RuntimeGroupObj.m_declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.FirstChild)
				{
					if (memberName != MemberName.LastChild)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						this.m_lastChild = (RuntimeGroupLeafObjReference)reader.ReadRIFObject();
					}
				}
				else
				{
					this.m_firstChild = (RuntimeGroupLeafObjReference)reader.ReadRIFObject();
				}
			}
		}

		// Token: 0x06007DC0 RID: 32192 RVA: 0x002074C1 File Offset: 0x002056C1
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
		}

		// Token: 0x06007DC1 RID: 32193 RVA: 0x002074C3 File Offset: 0x002056C3
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGroupObj;
		}

		// Token: 0x06007DC2 RID: 32194 RVA: 0x002074CC File Offset: 0x002056CC
		public new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (RuntimeGroupObj.m_declaration == null)
			{
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGroupObj, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeHierarchyObj, new List<MemberInfo>
				{
					new MemberInfo(MemberName.LastChild, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGroupLeafObjReference),
					new MemberInfo(MemberName.FirstChild, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGroupLeafObjReference)
				});
			}
			return RuntimeGroupObj.m_declaration;
		}

		// Token: 0x170028FB RID: 10491
		// (get) Token: 0x06007DC3 RID: 32195 RVA: 0x00207523 File Offset: 0x00205723
		public override int Size
		{
			get
			{
				return base.Size + ItemSizes.SizeOf(this.m_lastChild) + ItemSizes.SizeOf(this.m_firstChild);
			}
		}

		// Token: 0x04003E01 RID: 15873
		protected RuntimeGroupLeafObjReference m_lastChild;

		// Token: 0x04003E02 RID: 15874
		protected RuntimeGroupLeafObjReference m_firstChild;

		// Token: 0x04003E03 RID: 15875
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = RuntimeGroupObj.GetDeclaration();
	}
}
