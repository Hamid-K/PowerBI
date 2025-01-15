using System;
using System.Collections.Generic;
using System.Security.Permissions;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200030A RID: 778
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class ReportElementInstance : BaseInstance, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06001CA3 RID: 7331 RVA: 0x00071D72 File Offset: 0x0006FF72
		internal ReportElementInstance(ReportElement reportElementDef)
			: base(reportElementDef.ReportScope)
		{
			this.m_reportElementDef = reportElementDef;
		}

		// Token: 0x1700100A RID: 4106
		// (get) Token: 0x06001CA4 RID: 7332 RVA: 0x00071D87 File Offset: 0x0006FF87
		public virtual StyleInstance Style
		{
			get
			{
				if (this.m_style == null)
				{
					this.m_style = new StyleInstance(this.m_reportElementDef, this.m_reportElementDef.ReportScope, this.m_reportElementDef.RenderingContext);
				}
				return this.m_style;
			}
		}

		// Token: 0x1700100B RID: 4107
		// (get) Token: 0x06001CA5 RID: 7333 RVA: 0x00071DBE File Offset: 0x0006FFBE
		internal ReportElement ReportElementDef
		{
			get
			{
				return this.m_reportElementDef;
			}
		}

		// Token: 0x06001CA6 RID: 7334 RVA: 0x00071DC6 File Offset: 0x0006FFC6
		internal override void SetNewContext()
		{
			base.SetNewContext();
			if (this.m_style != null)
			{
				this.m_style.SetNewContext();
			}
		}

		// Token: 0x06001CA7 RID: 7335 RVA: 0x00071DE1 File Offset: 0x0006FFE1
		protected override void ResetInstanceCache()
		{
		}

		// Token: 0x06001CA8 RID: 7336 RVA: 0x00071DE3 File Offset: 0x0006FFE3
		void Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			this.Serialize(writer);
		}

		// Token: 0x06001CA9 RID: 7337 RVA: 0x00071DEC File Offset: 0x0006FFEC
		void Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			this.Deserialize(reader);
		}

		// Token: 0x06001CAA RID: 7338 RVA: 0x00071DF5 File Offset: 0x0006FFF5
		void Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x06001CAB RID: 7339 RVA: 0x00071E02 File Offset: 0x00070002
		Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.GetObjectType()
		{
			return this.GetObjectType();
		}

		// Token: 0x06001CAC RID: 7340 RVA: 0x00071E0C File Offset: 0x0007000C
		internal virtual void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(ReportElementInstance.m_Declaration);
			while (writer.NextMember())
			{
				if (writer.CurrentMember.MemberName == MemberName.Style)
				{
					writer.Write(this.Style);
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x06001CAD RID: 7341 RVA: 0x00071E60 File Offset: 0x00070060
		internal virtual void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(ReportElementInstance.m_Declaration);
			while (reader.NextMember())
			{
				if (reader.CurrentMember.MemberName == MemberName.Style)
				{
					reader.ReadRIFObject();
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x06001CAE RID: 7342 RVA: 0x00071EAC File Offset: 0x000700AC
		internal virtual Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ReportElementInstance;
		}

		// Token: 0x06001CAF RID: 7343 RVA: 0x00071EB4 File Offset: 0x000700B4
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ReportElementInstance, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Style, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.StyleInstance)
			});
		}

		// Token: 0x04000F09 RID: 3849
		[NonSerialized]
		protected ReportElement m_reportElementDef;

		// Token: 0x04000F0A RID: 3850
		protected StyleInstance m_style;

		// Token: 0x04000F0B RID: 3851
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = ReportElementInstance.GetDeclaration();
	}
}
