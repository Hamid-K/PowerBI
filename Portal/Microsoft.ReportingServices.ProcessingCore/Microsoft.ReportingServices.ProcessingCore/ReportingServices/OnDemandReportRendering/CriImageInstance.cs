using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000335 RID: 821
	internal sealed class CriImageInstance : ImageInstance
	{
		// Token: 0x06001EB1 RID: 7857 RVA: 0x000767F3 File Offset: 0x000749F3
		internal CriImageInstance(Image reportItemDef)
			: base(reportItemDef)
		{
			Global.Tracer.Assert(this.m_reportElementDef.CriOwner != null, "Expected CRI Owner");
		}

		// Token: 0x1700113E RID: 4414
		// (get) Token: 0x06001EB2 RID: 7858 RVA: 0x00076819 File Offset: 0x00074A19
		// (set) Token: 0x06001EB3 RID: 7859 RVA: 0x00076821 File Offset: 0x00074A21
		public override byte[] ImageData
		{
			get
			{
				return this.m_imageData;
			}
			set
			{
				if (this.m_reportElementDef.CriGenerationPhase != ReportElement.CriGenerationPhases.Instance)
				{
					throw new RenderingObjectModelException(RPRes.rsErrorDuringROMDefinitionWriteback);
				}
				this.m_imageData = value;
			}
		}

		// Token: 0x1700113F RID: 4415
		// (get) Token: 0x06001EB4 RID: 7860 RVA: 0x00076843 File Offset: 0x00074A43
		// (set) Token: 0x06001EB5 RID: 7861 RVA: 0x0007684B File Offset: 0x00074A4B
		public override string StreamName
		{
			get
			{
				return this.m_streamName;
			}
			internal set
			{
				this.m_streamName = value;
			}
		}

		// Token: 0x17001140 RID: 4416
		// (get) Token: 0x06001EB6 RID: 7862 RVA: 0x00076854 File Offset: 0x00074A54
		// (set) Token: 0x06001EB7 RID: 7863 RVA: 0x000768B8 File Offset: 0x00074AB8
		public override string MIMEType
		{
			get
			{
				if (!this.m_mimeTypeEvaluated)
				{
					this.m_mimeTypeEvaluated = true;
					if (base.ImageDef.ImageDef.MIMEType != null && !base.ImageDef.ImageDef.MIMEType.IsExpression)
					{
						this.m_mimeType = base.ImageDef.MIMEType.Value;
					}
				}
				return this.m_mimeType;
			}
			set
			{
				if (this.m_reportElementDef.CriGenerationPhase == ReportElement.CriGenerationPhases.None || (this.m_reportElementDef.CriGenerationPhase == ReportElement.CriGenerationPhases.Instance && !base.ImageDef.MIMEType.IsExpression))
				{
					throw new RenderingObjectModelException(RPRes.rsErrorDuringROMWriteback);
				}
				this.m_mimeTypeEvaluated = true;
				this.m_mimeType = value;
			}
		}

		// Token: 0x17001141 RID: 4417
		// (get) Token: 0x06001EB8 RID: 7864 RVA: 0x0007690B File Offset: 0x00074B0B
		public override TypeCode TagDataType
		{
			get
			{
				return TypeCode.Empty;
			}
		}

		// Token: 0x17001142 RID: 4418
		// (get) Token: 0x06001EB9 RID: 7865 RVA: 0x0007690E File Offset: 0x00074B0E
		public override object Tag
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001143 RID: 4419
		// (get) Token: 0x06001EBA RID: 7866 RVA: 0x00076911 File Offset: 0x00074B11
		internal override string ImageDataId
		{
			get
			{
				return this.StreamName;
			}
		}

		// Token: 0x17001144 RID: 4420
		// (get) Token: 0x06001EBB RID: 7867 RVA: 0x00076919 File Offset: 0x00074B19
		public override ActionInfoWithDynamicImageMapCollection ActionInfoWithDynamicImageMapAreas
		{
			get
			{
				if (this.m_actionInfoImageMapAreas == null)
				{
					this.m_actionInfoImageMapAreas = new ActionInfoWithDynamicImageMapCollection();
				}
				return this.m_actionInfoImageMapAreas;
			}
		}

		// Token: 0x17001145 RID: 4421
		// (get) Token: 0x06001EBC RID: 7868 RVA: 0x00076934 File Offset: 0x00074B34
		internal override bool IsNullImage
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001EBD RID: 7869 RVA: 0x00076937 File Offset: 0x00074B37
		internal override List<string> GetFieldsUsedInValueExpression()
		{
			return null;
		}

		// Token: 0x06001EBE RID: 7870 RVA: 0x0007693C File Offset: 0x00074B3C
		public override ActionInfoWithDynamicImageMap CreateActionInfoWithDynamicImageMap()
		{
			if (base.ReportElementDef.CriGenerationPhase != ReportElement.CriGenerationPhases.Instance)
			{
				throw new RenderingObjectModelException(RPRes.rsErrorDuringROMDefinitionWriteback);
			}
			if (this.m_actionInfoImageMapAreas == null)
			{
				this.m_actionInfoImageMapAreas = new ActionInfoWithDynamicImageMapCollection();
			}
			return this.m_actionInfoImageMapAreas.Add(base.RenderingContext, base.ImageDef, base.ImageDef);
		}

		// Token: 0x06001EBF RID: 7871 RVA: 0x00076992 File Offset: 0x00074B92
		protected override void ResetInstanceCache()
		{
			base.ResetInstanceCache();
			this.m_streamName = null;
			this.m_imageData = null;
			this.m_mimeTypeEvaluated = false;
			this.m_mimeType = null;
			this.m_actionInfoImageMapAreas = null;
		}

		// Token: 0x06001EC0 RID: 7872 RVA: 0x000769C0 File Offset: 0x00074BC0
		internal override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(CriImageInstance.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.MIMEType)
				{
					switch (memberName)
					{
					case MemberName.ImageData:
						writer.Write(this.m_imageData);
						continue;
					case MemberName.Actions:
					{
						ActionInstance[] array = null;
						if (base.ImageDef.ActionInfo != null)
						{
							array = new ActionInstance[base.ImageDef.ActionInfo.Actions.Count];
							for (int i = 0; i < array.Length; i++)
							{
								array[i] = base.ImageDef.ActionInfo.Actions[i].Instance;
							}
						}
						Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable[] array2 = array;
						writer.Write(array2);
						continue;
					}
					case MemberName.ImageMapAreas:
						writer.WriteRIFList<ActionInfoWithDynamicImageMap>(this.ActionInfoWithDynamicImageMapAreas.InternalList);
						continue;
					}
					Global.Tracer.Assert(false);
				}
				else
				{
					string text = null;
					if (base.ImageDef.MIMEType != null && base.ImageDef.MIMEType.IsExpression)
					{
						text = this.m_mimeType;
					}
					writer.Write(text);
				}
			}
		}

		// Token: 0x06001EC1 RID: 7873 RVA: 0x00076AF4 File Offset: 0x00074CF4
		internal override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(CriImageInstance.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.MIMEType)
				{
					switch (memberName)
					{
					case MemberName.ImageData:
						this.m_imageData = reader.ReadByteArray();
						continue;
					case MemberName.Actions:
						((ROMInstanceObjectCreator)reader.PersistenceHelper).StartActionInfoInstancesDeserialization(base.ImageDef.ActionInfo);
						reader.ReadArrayOfRIFObjects<ActionInstance>();
						((ROMInstanceObjectCreator)reader.PersistenceHelper).CompleteActionInfoInstancesDeserialization();
						continue;
					case MemberName.ImageMapAreas:
						this.m_actionInfoImageMapAreas = new ActionInfoWithDynamicImageMapCollection();
						reader.ReadListOfRIFObjects(this.m_actionInfoImageMapAreas.InternalList);
						continue;
					}
					Global.Tracer.Assert(false);
				}
				else
				{
					string text = reader.ReadString();
					if (base.ImageDef.MIMEType != null && base.ImageDef.MIMEType.IsExpression)
					{
						this.m_mimeTypeEvaluated = true;
						this.m_mimeType = text;
					}
					else
					{
						Global.Tracer.Assert(text == null, "(mimeType == null)");
					}
				}
			}
		}

		// Token: 0x06001EC2 RID: 7874 RVA: 0x00076C1A File Offset: 0x00074E1A
		internal override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ImageInstance;
		}

		// Token: 0x06001EC3 RID: 7875 RVA: 0x00076C24 File Offset: 0x00074E24
		[SkipMemberStaticValidation(MemberName.Actions)]
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ImageInstance, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ReportItemInstance, new List<MemberInfo>
			{
				new MemberInfo(MemberName.ImageData, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveTypedArray, Token.Byte),
				new MemberInfo(MemberName.MIMEType, Token.String),
				new MemberInfo(MemberName.Actions, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectArray, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ActionInstance),
				new MemberInfo(MemberName.ImageMapAreas, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ActionInfoWithDynamicImageMap)
			});
		}

		// Token: 0x04000FA7 RID: 4007
		private byte[] m_imageData;

		// Token: 0x04000FA8 RID: 4008
		private string m_mimeType;

		// Token: 0x04000FA9 RID: 4009
		private ActionInfoWithDynamicImageMapCollection m_actionInfoImageMapAreas;

		// Token: 0x04000FAA RID: 4010
		[NonSerialized]
		private string m_streamName;

		// Token: 0x04000FAB RID: 4011
		[NonSerialized]
		private bool m_mimeTypeEvaluated;

		// Token: 0x04000FAC RID: 4012
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = CriImageInstance.GetDeclaration();
	}
}
