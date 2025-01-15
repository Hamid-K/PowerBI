using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004FB RID: 1275
	[Serializable]
	public abstract class IDOwner : IInstancePath, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IGlobalIDOwner
	{
		// Token: 0x06004162 RID: 16738 RVA: 0x001137EC File Offset: 0x001119EC
		protected IDOwner()
		{
		}

		// Token: 0x06004163 RID: 16739 RVA: 0x001137F4 File Offset: 0x001119F4
		protected IDOwner(int id)
		{
			this.m_ID = id;
		}

		// Token: 0x17001B87 RID: 7047
		// (get) Token: 0x06004164 RID: 16740 RVA: 0x00113803 File Offset: 0x00111A03
		// (set) Token: 0x06004165 RID: 16741 RVA: 0x0011380B File Offset: 0x00111A0B
		public int ID
		{
			get
			{
				return this.m_ID;
			}
			set
			{
				this.m_ID = value;
			}
		}

		// Token: 0x17001B88 RID: 7048
		// (get) Token: 0x06004166 RID: 16742 RVA: 0x00113814 File Offset: 0x00111A14
		// (set) Token: 0x06004167 RID: 16743 RVA: 0x0011381C File Offset: 0x00111A1C
		public int GlobalID
		{
			get
			{
				return this.m_globalID;
			}
			set
			{
				this.m_globalID = value;
			}
		}

		// Token: 0x17001B89 RID: 7049
		// (get) Token: 0x06004168 RID: 16744 RVA: 0x00113825 File Offset: 0x00111A25
		internal string RenderingModelID
		{
			get
			{
				if (this.m_renderingModelID == null)
				{
					this.m_renderingModelID = this.m_globalID.ToString(CultureInfo.InvariantCulture);
				}
				return this.m_renderingModelID;
			}
		}

		// Token: 0x17001B8A RID: 7050
		// (get) Token: 0x06004169 RID: 16745 RVA: 0x0011384B File Offset: 0x00111A4B
		public InstancePathItem InstancePathItem
		{
			get
			{
				if (this.m_instancePathItem == null)
				{
					this.m_instancePathItem = this.CreateInstancePathItem();
				}
				return this.m_instancePathItem;
			}
		}

		// Token: 0x0600416A RID: 16746 RVA: 0x00113867 File Offset: 0x00111A67
		protected virtual InstancePathItem CreateInstancePathItem()
		{
			return new InstancePathItem();
		}

		// Token: 0x17001B8B RID: 7051
		// (get) Token: 0x0600416B RID: 16747 RVA: 0x00113870 File Offset: 0x00111A70
		internal string SubReportDefinitionPath
		{
			get
			{
				if (this.m_cachedDefinitionPath == null)
				{
					if (this.m_parentIDOwner != null)
					{
						this.m_cachedDefinitionPath = this.m_parentIDOwner.SubReportDefinitionPath;
					}
					else
					{
						this.m_cachedDefinitionPath = "";
					}
					if (this.InstancePathItem.Type == InstancePathItemType.SubReport)
					{
						this.m_cachedDefinitionPath = this.m_cachedDefinitionPath + "x" + this.m_ID.ToString(CultureInfo.InvariantCulture);
					}
				}
				return this.m_cachedDefinitionPath;
			}
		}

		// Token: 0x17001B8C RID: 7052
		// (get) Token: 0x0600416C RID: 16748 RVA: 0x001138E8 File Offset: 0x00111AE8
		public virtual List<InstancePathItem> InstancePath
		{
			get
			{
				if (this.m_cachedInstancePath == null)
				{
					this.m_cachedInstancePath = new List<InstancePathItem>();
					if (this.ParentInstancePath != null)
					{
						List<InstancePathItem> instancePath = this.ParentInstancePath.InstancePath;
						this.m_cachedInstancePath.AddRange(instancePath);
					}
					if (!this.InstancePathItem.IsEmpty)
					{
						this.m_cachedInstancePath.Add(this.InstancePathItem);
					}
				}
				return this.m_cachedInstancePath;
			}
		}

		// Token: 0x17001B8D RID: 7053
		// (get) Token: 0x0600416D RID: 16749 RVA: 0x0011394C File Offset: 0x00111B4C
		// (set) Token: 0x0600416E RID: 16750 RVA: 0x00113954 File Offset: 0x00111B54
		public IInstancePath ParentInstancePath
		{
			get
			{
				return this.m_parentIDOwner;
			}
			set
			{
				Global.Tracer.Assert(value == null || value is IDOwner, "((value != null) ? (value is IDOwner) : true)");
				this.m_parentIDOwner = (IDOwner)value;
			}
		}

		// Token: 0x17001B8E RID: 7054
		// (get) Token: 0x0600416F RID: 16751 RVA: 0x00113980 File Offset: 0x00111B80
		public virtual string UniqueName
		{
			get
			{
				return InstancePathItem.GenerateUniqueNameString(this.ID, this.InstancePath);
			}
		}

		// Token: 0x17001B8F RID: 7055
		// (get) Token: 0x06004170 RID: 16752 RVA: 0x00113993 File Offset: 0x00111B93
		internal bool IsClone
		{
			get
			{
				return this.m_isClone;
			}
		}

		// Token: 0x06004171 RID: 16753 RVA: 0x0011399B File Offset: 0x00111B9B
		internal virtual object PublishClone(AutomaticSubtotalContext context)
		{
			IDOwner idowner = (IDOwner)base.MemberwiseClone();
			idowner.m_ID = context.GenerateID();
			idowner.m_isClone = true;
			return idowner;
		}

		// Token: 0x06004172 RID: 16754 RVA: 0x001139BC File Offset: 0x00111BBC
		internal virtual void SetupCriRenderItemDef(ReportItem reportItem)
		{
			reportItem.m_parentIDOwner = this.m_parentIDOwner;
		}

		// Token: 0x06004173 RID: 16755 RVA: 0x001139CC File Offset: 0x00111BCC
		protected static IRIFReportDataScope FindReportDataScope(IInstancePath candidate)
		{
			IRIFReportDataScope irifreportDataScope = null;
			while (candidate != null && irifreportDataScope == null)
			{
				InstancePathItemType type = candidate.InstancePathItem.Type;
				if (type == InstancePathItemType.DataRegion || type - InstancePathItemType.Cell <= 3)
				{
					irifreportDataScope = (IRIFReportDataScope)candidate;
				}
				candidate = candidate.ParentInstancePath;
			}
			return irifreportDataScope;
		}

		// Token: 0x06004174 RID: 16756 RVA: 0x00113A0C File Offset: 0x00111C0C
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.IDOwner, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.ID, Token.Int32)
			});
		}

		// Token: 0x06004175 RID: 16757 RVA: 0x00113A3C File Offset: 0x00111C3C
		public virtual void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(IDOwner.m_Declaration);
			while (writer.NextMember())
			{
				if (writer.CurrentMember.MemberName == MemberName.ID)
				{
					writer.Write(this.m_ID);
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x06004176 RID: 16758 RVA: 0x00113A88 File Offset: 0x00111C88
		public virtual void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(IDOwner.m_Declaration);
			while (reader.NextMember())
			{
				if (reader.CurrentMember.MemberName == MemberName.ID)
				{
					this.m_ID = reader.ReadInt32();
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x06004177 RID: 16759 RVA: 0x00113AD4 File Offset: 0x00111CD4
		public virtual void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x06004178 RID: 16760 RVA: 0x00113AE1 File Offset: 0x00111CE1
		public virtual Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.IDOwner;
		}

		// Token: 0x04001DF4 RID: 7668
		protected int m_ID;

		// Token: 0x04001DF5 RID: 7669
		[NonSerialized]
		protected bool m_isClone;

		// Token: 0x04001DF6 RID: 7670
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = IDOwner.GetDeclaration();

		// Token: 0x04001DF7 RID: 7671
		[NonSerialized]
		protected string m_cachedDefinitionPath;

		// Token: 0x04001DF8 RID: 7672
		[NonSerialized]
		private InstancePathItem m_instancePathItem;

		// Token: 0x04001DF9 RID: 7673
		[NonSerialized]
		protected IDOwner m_parentIDOwner;

		// Token: 0x04001DFA RID: 7674
		[NonSerialized]
		protected List<InstancePathItem> m_cachedInstancePath;

		// Token: 0x04001DFB RID: 7675
		[NonSerialized]
		protected int m_globalID;

		// Token: 0x04001DFC RID: 7676
		[NonSerialized]
		protected string m_renderingModelID;
	}
}
