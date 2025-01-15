using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x02000063 RID: 99
	public sealed class RelationBinding : Binding, IPersistable
	{
		// Token: 0x06000404 RID: 1028 RVA: 0x0000D94D File Offset: 0x0000BB4D
		public RelationBinding(string name)
			: base(name)
		{
			this.Init(ref this.m_cachedDsvRelation);
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0000D962 File Offset: 0x0000BB62
		public RelationBinding(string name, RelationEnd relationEnd)
			: this(name)
		{
			if (!EnumUtil.IsDefined<RelationEnd>(relationEnd))
			{
				throw new InvalidEnumArgumentException();
			}
			this.m_end = relationEnd;
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0000D980 File Offset: 0x0000BB80
		private void Init(ref CachedDsvItem<DsvRelation> cachedDsvRelation)
		{
			cachedDsvRelation = new CachedDsvItem<DsvRelation>(new CreatorGetter<DataSourceView>(base.GetDataSourceView), (DataSourceView dsv) => dsv.Relations[base.Name]);
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000407 RID: 1031 RVA: 0x0000D9A1 File Offset: 0x0000BBA1
		public RelationEnd RelationEnd
		{
			get
			{
				return this.m_end;
			}
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0000D9A9 File Offset: 0x0000BBA9
		public DsvRelation GetRelation()
		{
			return this.m_cachedDsvRelation.GetItem();
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0000D9B8 File Offset: 0x0000BBB8
		public override DsvTable GetTable()
		{
			DsvRelation relation = this.GetRelation();
			if (relation == null)
			{
				return null;
			}
			if (this.m_end != RelationEnd.Source)
			{
				return relation.TargetTable;
			}
			return relation.SourceTable;
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0000D9E8 File Offset: 0x0000BBE8
		public ReadOnlyCollection<DsvColumn> GetColumns()
		{
			DsvRelation relation = this.GetRelation();
			if (relation == null)
			{
				return null;
			}
			if (this.m_end != RelationEnd.Source)
			{
				return relation.TargetColumns;
			}
			return relation.SourceColumns;
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0000DA18 File Offset: 0x0000BC18
		public ReadOnlyCollection<DsvColumn> GetReverseColumns()
		{
			DsvRelation relation = this.GetRelation();
			if (relation == null)
			{
				return null;
			}
			if (this.m_end != RelationEnd.Source)
			{
				return relation.SourceColumns;
			}
			return relation.TargetColumns;
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0000DA48 File Offset: 0x0000BC48
		internal static RelationBinding FromReader(ModelingXmlReader xr, bool readRelationEnd)
		{
			string text = null;
			RelationEnd relationEnd = RelationEnd.Source;
			xr.CheckElement("Relation");
			if (xr.MoveToAttribute("Name"))
			{
				text = xr.ReadValueAsString();
				xr.MoveToElement();
			}
			if (readRelationEnd)
			{
				if (xr.MoveToAttribute("RelationEnd"))
				{
					relationEnd = xr.ReadValueAsEnum<RelationEnd>();
					xr.MoveToElement();
				}
				else
				{
					xr.Validation.AddScopedError(ModelingErrorCode.MissingRelationEnd, SRErrors.MissingRelationEnd(xr.Validation.CurrentObjectDescriptor));
				}
			}
			xr.Skip();
			return new RelationBinding(text, relationEnd);
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0000DAC9 File Offset: 0x0000BCC9
		internal override void WriteTo(ModelingXmlWriter xw)
		{
			this.WriteTo(xw, true);
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0000DAD3 File Offset: 0x0000BCD3
		internal void WriteTo(ModelingXmlWriter xw, bool writeRelationEnd)
		{
			xw.WriteStartElement("Relation");
			xw.WriteAttribute("Name", base.Name);
			if (writeRelationEnd)
			{
				xw.WriteAttribute("RelationEnd", this.RelationEnd);
			}
			xw.WriteEndElement();
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0000DB10 File Offset: 0x0000BD10
		internal bool CheckBinding(CompilationContext ctx, string propertyName, out DsvRelation relation)
		{
			if (!base.CheckBinding())
			{
				relation = null;
				return false;
			}
			relation = this.GetRelation();
			if (relation == null)
			{
				ctx.AddScopedError(ModelingErrorCode.InvalidBinding, SRErrors.InvalidBinding(propertyName, ctx.CurrentObjectDescriptor, Binding.GetRelationDescriptor(base.Name)));
				return false;
			}
			return true;
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0000DB4D File Offset: 0x0000BD4D
		internal SRObjectDescriptor GetRelationDescriptor()
		{
			return Binding.GetRelationDescriptor(base.Name);
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0000DB5A File Offset: 0x0000BD5A
		internal static IEnumerable<RelationBinding> ListBindings(Binding sourceParentBinding, Binding targetParentBinding, bool useRelationEnd)
		{
			if (sourceParentBinding == null || targetParentBinding == null)
			{
				yield break;
			}
			DataSourceView dataSourceView = sourceParentBinding.GetDataSourceView();
			if (dataSourceView == null || targetParentBinding.GetDataSourceView() != dataSourceView)
			{
				yield break;
			}
			DsvTable sourceParentTable = sourceParentBinding.GetTable();
			DsvTable targetParentTable = targetParentBinding.GetTable();
			if (sourceParentTable == null || targetParentTable == null)
			{
				yield break;
			}
			foreach (DsvRelation relation in dataSourceView.Relations)
			{
				if (relation.SourceTable == sourceParentTable && relation.TargetTable == targetParentTable && Binding.CheckColumns(relation.SourceColumns, sourceParentBinding) && Binding.CheckColumns(relation.TargetColumns, targetParentBinding))
				{
					if (useRelationEnd)
					{
						yield return new RelationBinding(relation.Name, RelationEnd.Target);
					}
					else
					{
						yield return new RelationBinding(relation.Name);
					}
				}
				if (useRelationEnd && relation.SourceTable == targetParentTable && relation.TargetTable == sourceParentTable && Binding.CheckColumns(relation.SourceColumns, targetParentBinding) && Binding.CheckColumns(relation.TargetColumns, sourceParentBinding))
				{
					yield return new RelationBinding(relation.Name, RelationEnd.Source);
				}
				relation = null;
			}
			IndirectReadOnlyCollection<DsvRelation>.Enumerator enumerator = default(IndirectReadOnlyCollection<DsvRelation>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0000DB78 File Offset: 0x0000BD78
		internal RelationBinding()
		{
			this.Init(ref this.m_cachedDsvRelation);
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0000DB8C File Offset: 0x0000BD8C
		internal override void Serialize(IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(RelationBinding.Declaration);
			while (writer.NextMember())
			{
				if (writer.CurrentMember.MemberName != MemberName.RelationEnd)
				{
					throw new InternalModelingException("Unexpected member: " + writer.CurrentMember.MemberName.ToString());
				}
				writer.Write((byte)this.m_end);
			}
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0000DC00 File Offset: 0x0000BE00
		internal override void Deserialize(IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(RelationBinding.Declaration);
			while (reader.NextMember())
			{
				if (reader.CurrentMember.MemberName != MemberName.RelationEnd)
				{
					throw new InternalModelingException("Unexpected member: " + reader.CurrentMember.MemberName.ToString());
				}
				this.m_end = (RelationEnd)reader.ReadByte();
			}
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0000DC74 File Offset: 0x0000BE74
		internal override void ResolveReferences(Dictionary<ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems)
		{
			throw new InternalModelingException("ResolveReferences is not supported.");
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0000DC80 File Offset: 0x0000BE80
		internal override ObjectType GetObjectType()
		{
			return ObjectType.RelationBinding;
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000417 RID: 1047 RVA: 0x0000DC84 File Offset: 0x0000BE84
		private static Declaration Declaration
		{
			get
			{
				return ThreadingUtil.ReturnOnDemandValue<Declaration>(ref RelationBinding.__declaration, RelationBinding.__declarationLock, () => new Declaration(ObjectType.RelationBinding, ObjectType.Binding, new List<MemberInfo>
				{
					new MemberInfo(MemberName.RelationEnd, Token.Byte)
				}));
			}
		}

		// Token: 0x04000238 RID: 568
		internal const string RelationElem = "Relation";

		// Token: 0x04000239 RID: 569
		private const string RelationEndAttr = "RelationEnd";

		// Token: 0x0400023A RID: 570
		private RelationEnd m_end;

		// Token: 0x0400023B RID: 571
		private readonly CachedDsvItem<DsvRelation> m_cachedDsvRelation;

		// Token: 0x0400023C RID: 572
		private static Declaration __declaration;

		// Token: 0x0400023D RID: 573
		private static readonly object __declarationLock = new object();
	}
}
