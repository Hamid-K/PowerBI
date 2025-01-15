using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x0200041F RID: 1055
	internal abstract class MapStyleContainer : IStyleContainer, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06002E2A RID: 11818 RVA: 0x000D27D0 File Offset: 0x000D09D0
		internal MapStyleContainer()
		{
		}

		// Token: 0x06002E2B RID: 11819 RVA: 0x000D27D8 File Offset: 0x000D09D8
		internal MapStyleContainer(Map map)
		{
			this.m_map = map;
		}

		// Token: 0x170015F7 RID: 5623
		// (get) Token: 0x06002E2C RID: 11820 RVA: 0x000D27E7 File Offset: 0x000D09E7
		// (set) Token: 0x06002E2D RID: 11821 RVA: 0x000D27EF File Offset: 0x000D09EF
		public Style StyleClass
		{
			get
			{
				return this.m_styleClass;
			}
			set
			{
				this.m_styleClass = value;
			}
		}

		// Token: 0x170015F8 RID: 5624
		// (get) Token: 0x06002E2E RID: 11822 RVA: 0x000D27F8 File Offset: 0x000D09F8
		IInstancePath IStyleContainer.InstancePath
		{
			get
			{
				return this.m_map;
			}
		}

		// Token: 0x170015F9 RID: 5625
		// (get) Token: 0x06002E2F RID: 11823 RVA: 0x000D2800 File Offset: 0x000D0A00
		Microsoft.ReportingServices.ReportProcessing.ObjectType IStyleContainer.ObjectType
		{
			get
			{
				return Microsoft.ReportingServices.ReportProcessing.ObjectType.Map;
			}
		}

		// Token: 0x170015FA RID: 5626
		// (get) Token: 0x06002E30 RID: 11824 RVA: 0x000D2804 File Offset: 0x000D0A04
		string IStyleContainer.Name
		{
			get
			{
				return this.m_map.Name;
			}
		}

		// Token: 0x06002E31 RID: 11825 RVA: 0x000D2811 File Offset: 0x000D0A11
		internal virtual void Initialize(InitializationContext context)
		{
			if (this.m_styleClass != null)
			{
				this.m_styleClass.Initialize(context);
			}
		}

		// Token: 0x06002E32 RID: 11826 RVA: 0x000D2828 File Offset: 0x000D0A28
		internal virtual object PublishClone(AutomaticSubtotalContext context)
		{
			MapStyleContainer mapStyleContainer = (MapStyleContainer)base.MemberwiseClone();
			mapStyleContainer.m_map = context.CurrentMapClone;
			if (this.m_styleClass != null)
			{
				mapStyleContainer.m_styleClass = (Style)this.m_styleClass.PublishClone(context);
			}
			return mapStyleContainer;
		}

		// Token: 0x06002E33 RID: 11827 RVA: 0x000D286E File Offset: 0x000D0A6E
		internal virtual void SetExprHost(StyleExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null);
			exprHost.SetReportObjectModel(reportObjectModel);
			if (this.m_styleClass != null)
			{
				this.m_styleClass.SetStyleExprHost(exprHost);
			}
		}

		// Token: 0x06002E34 RID: 11828 RVA: 0x000D28A0 File Offset: 0x000D0AA0
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapStyleContainer, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Map, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Map, Token.Reference),
				new MemberInfo(MemberName.StyleClass, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Style)
			});
		}

		// Token: 0x06002E35 RID: 11829 RVA: 0x000D28EC File Offset: 0x000D0AEC
		public virtual void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(MapStyleContainer.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.StyleClass)
				{
					if (memberName == MemberName.Map)
					{
						writer.WriteReference(this.m_map);
					}
					else
					{
						Global.Tracer.Assert(false);
					}
				}
				else
				{
					writer.Write(this.m_styleClass);
				}
			}
		}

		// Token: 0x06002E36 RID: 11830 RVA: 0x000D2958 File Offset: 0x000D0B58
		public virtual void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(MapStyleContainer.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.StyleClass)
				{
					if (memberName == MemberName.Map)
					{
						this.m_map = reader.ReadReference<Map>(this);
					}
					else
					{
						Global.Tracer.Assert(false);
					}
				}
				else
				{
					this.m_styleClass = (Style)reader.ReadRIFObject();
				}
			}
		}

		// Token: 0x06002E37 RID: 11831 RVA: 0x000D29C8 File Offset: 0x000D0BC8
		public virtual void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference> list;
			if (memberReferencesCollection.TryGetValue(MapStyleContainer.m_Declaration.ObjectType, out list))
			{
				foreach (Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference memberReference in list)
				{
					if (memberReference.MemberName == MemberName.Map)
					{
						Global.Tracer.Assert(referenceableItems.ContainsKey(memberReference.RefID));
						this.m_map = (Map)referenceableItems[memberReference.RefID];
					}
					else
					{
						Global.Tracer.Assert(false);
					}
				}
			}
		}

		// Token: 0x06002E38 RID: 11832 RVA: 0x000D2A6C File Offset: 0x000D0C6C
		public virtual Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapStyleContainer;
		}

		// Token: 0x04001863 RID: 6243
		[Reference]
		protected Map m_map;

		// Token: 0x04001864 RID: 6244
		protected Style m_styleClass;

		// Token: 0x04001865 RID: 6245
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = MapStyleContainer.GetDeclaration();
	}
}
