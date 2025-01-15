using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008BA RID: 2234
	[PersistedWithinRequestOnly]
	internal sealed class RuntimeDataShapeIntersection : RuntimeCellWithContents
	{
		// Token: 0x060079F9 RID: 31225 RVA: 0x001F7059 File Offset: 0x001F5259
		internal RuntimeDataShapeIntersection()
		{
		}

		// Token: 0x060079FA RID: 31226 RVA: 0x001F7061 File Offset: 0x001F5261
		internal RuntimeDataShapeIntersection(RuntimeDataShapeGroupLeafObjReference owner, DataShapeMember outerGroupingMember, DataShapeMember innerGroupingMember, bool innermost)
			: base(owner, outerGroupingMember, innerGroupingMember, innermost)
		{
		}

		// Token: 0x060079FB RID: 31227 RVA: 0x001F7070 File Offset: 0x001F5270
		protected override List<Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem> GetCellContents(Cell cell)
		{
			DataShapeIntersection dataShapeIntersection = cell as DataShapeIntersection;
			if (dataShapeIntersection != null && dataShapeIntersection.HasInnerGroupTreeHierarchy)
			{
				return dataShapeIntersection.DataShapes;
			}
			return null;
		}

		// Token: 0x060079FC RID: 31228 RVA: 0x001F7097 File Offset: 0x001F5297
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataShapeIntersection;
		}

		// Token: 0x060079FD RID: 31229 RVA: 0x001F709E File Offset: 0x001F529E
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
		}

		// Token: 0x060079FE RID: 31230 RVA: 0x001F70A7 File Offset: 0x001F52A7
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
		}

		// Token: 0x060079FF RID: 31231 RVA: 0x001F70B0 File Offset: 0x001F52B0
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false, base.GetType().Name + " should not resolve references");
		}

		// Token: 0x06007A00 RID: 31232 RVA: 0x001F70D4 File Offset: 0x001F52D4
		public new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (RuntimeDataShapeIntersection.m_declaration == null)
			{
				List<MemberInfo> list = new List<MemberInfo>();
				RuntimeDataShapeIntersection.m_declaration = new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataShapeIntersection, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeCellWithContents, list);
			}
			return RuntimeDataShapeIntersection.m_declaration;
		}

		// Token: 0x04003D24 RID: 15652
		[NonSerialized]
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = RuntimeDataShapeIntersection.GetDeclaration();
	}
}
