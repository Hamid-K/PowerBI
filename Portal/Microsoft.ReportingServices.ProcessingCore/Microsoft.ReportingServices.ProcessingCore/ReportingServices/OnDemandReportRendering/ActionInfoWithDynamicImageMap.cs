using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200020F RID: 527
	public sealed class ActionInfoWithDynamicImageMap : ActionInfo, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06001402 RID: 5122 RVA: 0x00051AA4 File Offset: 0x0004FCA4
		internal ActionInfoWithDynamicImageMap(Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext, Microsoft.ReportingServices.OnDemandReportRendering.ReportItem owner, IROMActionOwner romActionOwner)
			: this(renderingContext, new Microsoft.ReportingServices.ReportIntermediateFormat.Action(), owner, romActionOwner)
		{
		}

		// Token: 0x06001403 RID: 5123 RVA: 0x00051AB4 File Offset: 0x0004FCB4
		internal ActionInfoWithDynamicImageMap(Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext, Microsoft.ReportingServices.OnDemandReportRendering.ReportItem owner, IReportScope reportScope, IInstancePath instancePath, IROMActionOwner romActionOwner, bool chartConstructor)
			: this(renderingContext, reportScope, new Microsoft.ReportingServices.ReportIntermediateFormat.Action(), instancePath, owner, romActionOwner)
		{
			this.m_chartConstruction = chartConstructor;
		}

		// Token: 0x06001404 RID: 5124 RVA: 0x00051AD0 File Offset: 0x0004FCD0
		internal ActionInfoWithDynamicImageMap(Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext, Microsoft.ReportingServices.ReportIntermediateFormat.Action actionDef, Microsoft.ReportingServices.OnDemandReportRendering.ReportItem owner, IROMActionOwner romActionOwner)
			: this(renderingContext, owner.ReportScope, actionDef, owner.ReportItemDef, owner, romActionOwner)
		{
		}

		// Token: 0x06001405 RID: 5125 RVA: 0x00051AEC File Offset: 0x0004FCEC
		internal ActionInfoWithDynamicImageMap(Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext, IReportScope reportScope, Microsoft.ReportingServices.ReportIntermediateFormat.Action actionDef, IInstancePath instancePath, Microsoft.ReportingServices.OnDemandReportRendering.ReportItem owner, IROMActionOwner romActionOwner)
			: base(renderingContext, reportScope, actionDef, instancePath, owner, owner.ReportItemDef.ObjectType, owner.ReportItemDef.Name, romActionOwner)
		{
			base.IsDynamic = true;
		}

		// Token: 0x06001406 RID: 5126 RVA: 0x00051B27 File Offset: 0x0004FD27
		internal ActionInfoWithDynamicImageMap(Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext, ActionInfo renderAction, ImageMapAreasCollection renderImageMap)
			: base(renderingContext, renderAction)
		{
			base.IsDynamic = true;
			this.m_imageMapAreas = new ImageMapAreaInstanceCollection(renderImageMap);
		}

		// Token: 0x17000AB1 RID: 2737
		// (get) Token: 0x06001407 RID: 5127 RVA: 0x00051B44 File Offset: 0x0004FD44
		public ImageMapAreaInstanceCollection ImageMapAreaInstances
		{
			get
			{
				if (this.m_imageMapAreas == null)
				{
					this.m_imageMapAreas = new ImageMapAreaInstanceCollection();
				}
				return this.m_imageMapAreas;
			}
		}

		// Token: 0x06001408 RID: 5128 RVA: 0x00051B5F File Offset: 0x0004FD5F
		public ImageMapAreaInstance CreateImageMapAreaInstance(ImageMapArea.ImageMapAreaShape shape, float[] coordinates)
		{
			return this.CreateImageMapAreaInstance(shape, coordinates, null);
		}

		// Token: 0x06001409 RID: 5129 RVA: 0x00051B6C File Offset: 0x0004FD6C
		public ImageMapAreaInstance CreateImageMapAreaInstance(ImageMapArea.ImageMapAreaShape shape, float[] coordinates, string toolTip)
		{
			if (!this.m_chartConstruction && base.ReportElementOwner.CriGenerationPhase != ReportElement.CriGenerationPhases.Instance)
			{
				throw new RenderingObjectModelException(RPRes.rsErrorDuringROMDefinitionWriteback);
			}
			if (coordinates == null || coordinates.Length < 1)
			{
				throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterValue, new object[] { "coordinates" });
			}
			if (this.m_imageMapAreas == null)
			{
				this.m_imageMapAreas = new ImageMapAreaInstanceCollection();
			}
			return this.m_imageMapAreas.Add(shape, coordinates, toolTip);
		}

		// Token: 0x0600140A RID: 5130 RVA: 0x00051BDD File Offset: 0x0004FDDD
		[Obsolete("ActionInfoWithDynamicImageMap objects are completely volatile, so there is no reason to reuse the same instance of this class. Hence there is no need to support Update and SetNewContext methods.")]
		internal new void Update(ActionInfo newActionInfo)
		{
			Global.Tracer.Assert(false, "Update(...) should not be called on ActionInfoWithDynamicImageMap");
		}

		// Token: 0x0600140B RID: 5131 RVA: 0x00051BEF File Offset: 0x0004FDEF
		[Obsolete("ActionInfoWithDynamicImageMap objects are completely volatile, so there is no reason to reuse the same instance of this class. Hence there is no need to support Update and SetNewContext methods.")]
		internal override void SetNewContext()
		{
			Global.Tracer.Assert(false, "SetNewContext() should not be called on ActionInfoWithDynamicImageMap");
		}

		// Token: 0x0600140C RID: 5132 RVA: 0x00051C04 File Offset: 0x0004FE04
		void Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(ActionInfoWithDynamicImageMap.m_Declaration);
			while (writer.NextMember())
			{
				switch (writer.CurrentMember.MemberName)
				{
				case MemberName.Actions:
				{
					ActionInstance[] array = new ActionInstance[base.Actions.Count];
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = base.Actions[i].Instance;
					}
					Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable[] array2 = array;
					writer.Write(array2);
					break;
				}
				case MemberName.ActionDefinition:
					writer.Write(base.ActionDef);
					break;
				case MemberName.ImageMapAreas:
					writer.WriteRIFList<ImageMapAreaInstance>(this.ImageMapAreaInstances.InternalList);
					break;
				default:
					Global.Tracer.Assert(false);
					break;
				}
			}
		}

		// Token: 0x0600140D RID: 5133 RVA: 0x00051CC4 File Offset: 0x0004FEC4
		void Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(ActionInfoWithDynamicImageMap.m_Declaration);
			while (reader.NextMember())
			{
				switch (reader.CurrentMember.MemberName)
				{
				case MemberName.Actions:
					((ROMInstanceObjectCreator)reader.PersistenceHelper).StartActionInfoInstancesDeserialization(this);
					reader.ReadArrayOfRIFObjects<ActionInstance>();
					((ROMInstanceObjectCreator)reader.PersistenceHelper).CompleteActionInfoInstancesDeserialization();
					break;
				case MemberName.ActionDefinition:
					base.ActionDef = (Microsoft.ReportingServices.ReportIntermediateFormat.Action)reader.ReadRIFObject();
					break;
				case MemberName.ImageMapAreas:
					this.m_imageMapAreas = new ImageMapAreaInstanceCollection();
					reader.ReadListOfRIFObjects(this.m_imageMapAreas.InternalList);
					break;
				default:
					Global.Tracer.Assert(false);
					break;
				}
			}
		}

		// Token: 0x0600140E RID: 5134 RVA: 0x00051D80 File Offset: 0x0004FF80
		void Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x0600140F RID: 5135 RVA: 0x00051D8D File Offset: 0x0004FF8D
		Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ActionInfoWithDynamicImageMap;
		}

		// Token: 0x06001410 RID: 5136 RVA: 0x00051D94 File Offset: 0x0004FF94
		[SkipMemberStaticValidation(MemberName.ActionDefinition)]
		[SkipMemberStaticValidation(MemberName.Actions)]
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ActionInfoWithDynamicImageMap, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.ActionDefinition, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Action),
				new MemberInfo(MemberName.Actions, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectArray, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ActionInstance),
				new MemberInfo(MemberName.ImageMapAreas, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ImageMapAreaInstance)
			});
		}

		// Token: 0x0400097B RID: 2427
		private ImageMapAreaInstanceCollection m_imageMapAreas;

		// Token: 0x0400097C RID: 2428
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = ActionInfoWithDynamicImageMap.GetDeclaration();
	}
}
