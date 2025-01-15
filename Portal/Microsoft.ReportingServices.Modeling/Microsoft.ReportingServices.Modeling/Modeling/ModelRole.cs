using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.Modeling.Linguistics;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x02000092 RID: 146
	public sealed class ModelRole : ModelField, ILinguisticInfo
	{
		// Token: 0x060006D1 RID: 1745 RVA: 0x00015804 File Offset: 0x00013A04
		public ModelRole()
		{
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x0001580C File Offset: 0x00013A0C
		internal override void Reset()
		{
			base.Reset();
			this.m_singularName = string.Empty;
			this.m_pluralName = string.Empty;
			this.m_relatedRole = null;
			this.m_cardinality = Cardinality.One;
			this.m_optionality = Optionality.Optional;
			this.m_contextualName = RoleContextualName.Default;
			this.m_preferred = false;
			this.m_promoteLookup = false;
			this.m_expandInline = false;
			this.__hiddenFields = new ModelRole.HiddenFieldCollection(this);
			this.m_binding = null;
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x0001587C File Offset: 0x00013A7C
		internal ModelRole(ModelRole baseItem, bool markAsHidden)
			: base(baseItem, markAsHidden)
		{
			this.m_singularName = baseItem.m_singularName;
			this.m_pluralName = baseItem.m_pluralName;
			this.m_cardinality = baseItem.m_cardinality;
			this.m_optionality = baseItem.m_optionality;
			this.m_contextualName = baseItem.m_contextualName;
			this.m_preferred = baseItem.m_preferred;
			this.m_promoteLookup = baseItem.m_promoteLookup;
			this.m_expandInline = baseItem.m_expandInline;
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060006D4 RID: 1748 RVA: 0x000158F1 File Offset: 0x00013AF1
		public ILinguisticInfo Linguistics
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060006D5 RID: 1749 RVA: 0x000158F4 File Offset: 0x00013AF4
		// (set) Token: 0x060006D6 RID: 1750 RVA: 0x00015920 File Offset: 0x00013B20
		string ILinguisticInfo.SingularName
		{
			get
			{
				if (base.UseAutoName)
				{
					return this.GetAutoName(false);
				}
				if (this.m_singularName.Length == 0)
				{
					return this.Name;
				}
				return this.m_singularName;
			}
			set
			{
				base.CheckWriteable();
				this.m_singularName = value ?? string.Empty;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060006D7 RID: 1751 RVA: 0x00015938 File Offset: 0x00013B38
		bool ILinguisticInfo.IsSingularNameSet
		{
			get
			{
				return this.m_singularName.Length > 0;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060006D8 RID: 1752 RVA: 0x00015948 File Offset: 0x00013B48
		// (set) Token: 0x060006D9 RID: 1753 RVA: 0x00015974 File Offset: 0x00013B74
		string ILinguisticInfo.PluralName
		{
			get
			{
				if (base.UseAutoName)
				{
					return this.GetAutoName(true);
				}
				if (this.m_pluralName.Length == 0)
				{
					return this.Name;
				}
				return this.m_pluralName;
			}
			set
			{
				base.CheckWriteable();
				this.m_pluralName = value ?? string.Empty;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060006DA RID: 1754 RVA: 0x0001598C File Offset: 0x00013B8C
		bool ILinguisticInfo.IsPluralNameSet
		{
			get
			{
				return this.m_pluralName.Length > 0;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060006DB RID: 1755 RVA: 0x0001599C File Offset: 0x00013B9C
		// (set) Token: 0x060006DC RID: 1756 RVA: 0x000159A4 File Offset: 0x00013BA4
		public ModelRole RelatedRole
		{
			get
			{
				return this.m_relatedRole;
			}
			set
			{
				if (value == this)
				{
					throw new ValidationException(ModelingErrorCode.SelfReferentialRole, this, SRErrors.SelfReferentialRole(SRObjectDescriptor.FromScope(this)));
				}
				base.CheckWriteable();
				this.m_relatedRole = value;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060006DD RID: 1757 RVA: 0x000159CB File Offset: 0x00013BCB
		public ModelEntity RelatedEntity
		{
			get
			{
				if (this.m_relatedRole == null)
				{
					return null;
				}
				return this.m_relatedRole.Entity;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060006DE RID: 1758 RVA: 0x000159E2 File Offset: 0x00013BE2
		// (set) Token: 0x060006DF RID: 1759 RVA: 0x000159EA File Offset: 0x00013BEA
		public Cardinality Cardinality
		{
			get
			{
				return this.m_cardinality;
			}
			set
			{
				if (!EnumUtil.IsDefined<Cardinality>(value))
				{
					throw new InvalidEnumArgumentException();
				}
				base.CheckWriteable();
				this.m_cardinality = value;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060006E0 RID: 1760 RVA: 0x00015A07 File Offset: 0x00013C07
		// (set) Token: 0x060006E1 RID: 1761 RVA: 0x00015A0F File Offset: 0x00013C0F
		public Optionality Optionality
		{
			get
			{
				return this.m_optionality;
			}
			set
			{
				if (!EnumUtil.IsDefined<Optionality>(value))
				{
					throw new InvalidEnumArgumentException();
				}
				base.CheckWriteable();
				this.m_optionality = value;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060006E2 RID: 1762 RVA: 0x00015A2C File Offset: 0x00013C2C
		// (set) Token: 0x060006E3 RID: 1763 RVA: 0x00015A34 File Offset: 0x00013C34
		public RoleContextualName ContextualName
		{
			get
			{
				return this.m_contextualName;
			}
			set
			{
				if (!EnumUtil.IsDefined<RoleContextualName>(value))
				{
					throw new InvalidEnumArgumentException();
				}
				base.CheckWriteable();
				this.m_contextualName = value;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060006E4 RID: 1764 RVA: 0x00015A51 File Offset: 0x00013C51
		// (set) Token: 0x060006E5 RID: 1765 RVA: 0x00015A59 File Offset: 0x00013C59
		public bool Preferred
		{
			get
			{
				return this.m_preferred;
			}
			set
			{
				base.CheckWriteable();
				this.m_preferred = value;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060006E6 RID: 1766 RVA: 0x00015A68 File Offset: 0x00013C68
		// (set) Token: 0x060006E7 RID: 1767 RVA: 0x00015A70 File Offset: 0x00013C70
		public bool PromoteLookup
		{
			get
			{
				return this.m_promoteLookup;
			}
			set
			{
				base.CheckWriteable();
				this.m_promoteLookup = value;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060006E8 RID: 1768 RVA: 0x00015A7F File Offset: 0x00013C7F
		// (set) Token: 0x060006E9 RID: 1769 RVA: 0x00015A87 File Offset: 0x00013C87
		public bool ExpandInline
		{
			get
			{
				return this.m_expandInline;
			}
			set
			{
				base.CheckWriteable();
				this.m_expandInline = value;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060006EA RID: 1770 RVA: 0x00015A96 File Offset: 0x00013C96
		public ModelRole.HiddenFieldCollection HiddenFields
		{
			get
			{
				if (this.__hiddenFields == null)
				{
					this.__hiddenFields = this.BaseItem.HiddenFields.CloneFor(this);
				}
				return this.__hiddenFields;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060006EB RID: 1771 RVA: 0x00015ABD File Offset: 0x00013CBD
		// (set) Token: 0x060006EC RID: 1772 RVA: 0x00015AC5 File Offset: 0x00013CC5
		public RelationBinding Binding
		{
			get
			{
				return this.m_binding;
			}
			set
			{
				base.CheckWriteable();
				if (value != null)
				{
					value.SetContext(this);
				}
				if (this.m_binding != null)
				{
					this.m_binding.SetContext(null);
				}
				this.m_binding = value;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060006ED RID: 1773 RVA: 0x00015AF2 File Offset: 0x00013CF2
		internal override string ElementName
		{
			get
			{
				return "Role";
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060006EE RID: 1774 RVA: 0x00015AF9 File Offset: 0x00013CF9
		internal new ModelRole BaseItem
		{
			get
			{
				return (ModelRole)base.BaseItem;
			}
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x00015B06 File Offset: 0x00013D06
		protected override string GetAutoName()
		{
			return this.GetAutoName(this.m_cardinality == Cardinality.Many);
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x00015B17 File Offset: 0x00013D17
		private string GetAutoName(bool collection)
		{
			if (this.RelatedEntity == null)
			{
				return string.Empty;
			}
			if (!collection)
			{
				return this.RelatedEntity.Name;
			}
			return this.RelatedEntity.CollectionName;
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x00015B41 File Offset: 0x00013D41
		public static IEnumerable<RelationBinding> ListPotentialBindings(ModelEntity entity, ModelEntity relatedEntity)
		{
			return RelationBinding.ListBindings((entity != null) ? entity.Binding : null, (relatedEntity != null) ? relatedEntity.Binding : null, true);
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x00015B64 File Offset: 0x00013D64
		internal override void LoadCore(ModelingXmlReader xr, bool fragment)
		{
			base.LoadCore(xr, fragment);
			if (base.UseAutoName && (this.m_singularName.Length > 0 || this.m_pluralName.Length > 0))
			{
				xr.Validation.AddScopedError(ModelingErrorCode.InvalidLinguistics, SRErrors.InvalidLinguistics(xr.Validation.CurrentObjectDescriptor));
			}
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x00015BBC File Offset: 0x00013DBC
		internal override bool LoadXmlElement(ModelingXmlReader xr)
		{
			if (xr.IsDefaultNamespace)
			{
				string localName = xr.LocalName;
				if (localName != null)
				{
					switch (localName.Length)
					{
					case 8:
						if (localName == "Relation")
						{
							this.Binding = RelationBinding.FromReader(xr, true);
							return true;
						}
						break;
					case 9:
						if (localName == "Preferred")
						{
							this.m_preferred = xr.ReadValueAsBoolean();
							return true;
						}
						break;
					case 11:
					{
						char c = localName[0];
						if (c != 'C')
						{
							if (c == 'L')
							{
								if (localName == "Linguistics")
								{
									xr.LoadObject(new ModelRole.LinguisticsLoader(this));
									return true;
								}
							}
						}
						else if (localName == "Cardinality")
						{
							ModelRole.ConvertFromCombinedCardinality(xr.ReadValueAsEnum<CombinedCardinality>(), out this.m_cardinality, out this.m_optionality);
							return true;
						}
						break;
					}
					case 12:
					{
						char c = localName[0];
						if (c != 'E')
						{
							if (c == 'H')
							{
								if (localName == "HiddenFields")
								{
									this.HiddenFields.LoadRefs(xr, "FieldFolderItemID");
									return true;
								}
							}
						}
						else if (localName == "ExpandInline")
						{
							this.m_expandInline = xr.ReadValueAsBoolean();
							return true;
						}
						break;
					}
					case 13:
					{
						char c = localName[0];
						if (c != 'P')
						{
							if (c == 'R')
							{
								if (localName == "RelatedRoleID")
								{
									xr.Context.AddReference(this, xr.ReadReferenceByID("RelatedRoleID", false));
									return true;
								}
							}
						}
						else if (localName == "PromoteLookup")
						{
							this.m_promoteLookup = xr.ReadValueAsBoolean();
							return true;
						}
						break;
					}
					case 14:
						if (localName == "ContextualName")
						{
							this.m_contextualName = xr.ReadValueAsEnum<RoleContextualName>();
							return true;
						}
						break;
					}
				}
			}
			return base.LoadXmlElement(xr);
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x00015DB8 File Offset: 0x00013FB8
		internal override bool ProcessDeserializationReference(ModelingReference reference, DeserializationContext ctx)
		{
			if (reference.PropertyName == "RelatedRoleID")
			{
				this.RelatedRole = ctx.CurrentModel.TryGetModelItem<ModelRole>(reference, ctx.Validation);
				return true;
			}
			return base.ProcessDeserializationReference(reference, ctx);
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x00015DF0 File Offset: 0x00013FF0
		internal override void WriteXmlElements(ModelingXmlWriter xw)
		{
			base.WriteXmlElements(xw);
			if (!base.UseAutoName && (this.m_singularName.Length > 0 || this.m_pluralName.Length > 0))
			{
				xw.WriteStartElement("Linguistics");
				xw.WriteElementIfNonDefault<string>("SingularName", this.m_singularName);
				xw.WriteElementIfNonDefault<string>("PluralName", this.m_pluralName);
				xw.WriteEndElement();
			}
			xw.WriteReferenceElement("RelatedRoleID", this.m_relatedRole);
			xw.WriteElement("Cardinality", ModelRole.ConvertToCombinedCardinality(this.m_cardinality, this.m_optionality));
			xw.WriteElementIfNonDefault<RoleContextualName>("ContextualName", this.m_contextualName);
			this.HiddenFields.WriteRefsTo(xw, "HiddenFields", "FieldFolderItemID");
			xw.WriteElementIfNonDefault<bool>("Preferred", this.m_preferred);
			xw.WriteElementIfNonDefault<bool>("PromoteLookup", this.m_promoteLookup);
			xw.WriteElementIfNonDefault<bool>("ExpandInline", this.m_expandInline);
			base.WriteVariations(xw);
			if (this.m_binding != null && xw.ShouldWriteBindings)
			{
				this.m_binding.WriteTo(xw, true);
			}
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x00015F0C File Offset: 0x0001410C
		internal override void CompileCore(CompilationContext ctx)
		{
			base.CompileCore(ctx);
			if (!this.ValidateRelatedRoleReference(ctx, false))
			{
				return;
			}
			if (this.m_relatedRole.Model != this.Model)
			{
				ctx.AddScopedError(ModelingErrorCode.WrongSemanticModel, SRErrors.WrongSemanticModel_ModelItem("RelatedRoleID", ctx.CurrentObjectDescriptor, SRObjectDescriptor.FromScope(this.m_relatedRole)));
				return;
			}
			if (this.m_relatedRole.RelatedRole != this)
			{
				SRObjectDescriptor currentObjectDescriptor = ctx.CurrentObjectDescriptor;
				SRObjectDescriptor srobjectDescriptor = SRObjectDescriptor.FromScope(this.m_relatedRole);
				ctx.AddScopedError(ModelingErrorCode.RelatedRolesMismatch, SRErrors.RelatedRolesMismatch(currentObjectDescriptor, currentObjectDescriptor.ObjectName, srobjectDescriptor, srobjectDescriptor.ObjectName));
				return;
			}
			if (this.HiddenFields.CheckInvalidRefs(ctx, "HiddenFields", "FieldFolderItemID") && this.HiddenFields.Count > 0 && this.RelatedEntity != null)
			{
				ctx.PushContextEntity(this.RelatedEntity);
				try
				{
					foreach (ModelFieldFolderItem modelFieldFolderItem in this.HiddenFields)
					{
						ctx.CheckContextEntityMatch(modelFieldFolderItem, "HiddenFields.FieldFolderItemID", true);
					}
				}
				finally
				{
					ctx.PopContextEntity();
				}
			}
			if (this.m_promoteLookup)
			{
				if (base.Entity != null && !base.Entity.IsLookup)
				{
					ctx.AddScopedError(ModelingErrorCode.PromoteLookupForNonLookupEntity, SRErrors.PromoteLookupForNonLookupEntity(ctx.CurrentObjectDescriptor, SRObjectDescriptor.FromScope(base.Entity)));
				}
				if (this.RelatedEntity != null && !this.RelatedEntity.IsLookup)
				{
					ctx.AddScopedError(ModelingErrorCode.PromoteLookupForNonLookupEntity, SRErrors.PromoteLookupForNonLookupEntity(ctx.CurrentObjectDescriptor, SRObjectDescriptor.FromScope(this.RelatedEntity)));
				}
			}
			if (ctx.ShouldCheckBindings)
			{
				bool flag;
				this.CompileCheckBinding(ctx, out flag);
				if (flag && this.Optionality != Optionality.Required)
				{
					SRObjectDescriptor srobjectDescriptor2 = SRObjectDescriptor.FromScope(base.Entity);
					SRObjectDescriptor srobjectDescriptor3 = SRObjectDescriptor.FromScope(this.RelatedEntity);
					ctx.AddScopedWarning(ModelingErrorCode.InvalidOptionalityOfRoleForColumnBoundEntity, SRErrors.InvalidOptionalityOfRoleForColumnBoundEntity(ctx.CurrentObjectDescriptor, srobjectDescriptor2, srobjectDescriptor3, (base.Entity.Binding is ColumnBinding) ? srobjectDescriptor2 : srobjectDescriptor3));
				}
			}
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x00016110 File Offset: 0x00014310
		internal bool ValidateRelatedRoleReference(CompilationContext ctx, bool forceInvalidRefTargetCheck)
		{
			if (this.m_relatedRole == null)
			{
				ctx.AddScopedError(ModelingErrorCode.MissingRelatedRole, SRErrors.MissingRelatedRole(ctx.CurrentObjectDescriptor));
				return false;
			}
			if (this.m_relatedRole.IsInvalidRefTarget && (ctx.ShouldCheckInvalidRefsDuringCompilation || forceInvalidRefTargetCheck))
			{
				ctx.AddScopedError(ModelingErrorCode.ItemNotFound, SRErrors.ItemNotFound("RelatedRoleID", SRObjectDescriptor.FromScope(this), this.m_relatedRole.ID.ToString()));
				return false;
			}
			return true;
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x00016188 File Offset: 0x00014388
		private void CompileCheckBinding(CompilationContext ctx, out bool forColumnBoundEntity)
		{
			forColumnBoundEntity = false;
			Binding binding = ((this.RelatedEntity != null) ? this.RelatedEntity.Binding : null);
			DsvTable dsvTable = ((binding != null) ? binding.GetTable() : null);
			if (this.m_binding != null)
			{
				DsvRelation dsvRelation;
				if (this.m_binding.CheckBinding(ctx, "Relation", out dsvRelation))
				{
					if (this.m_relatedRole.Binding != null && dsvRelation != this.m_relatedRole.Binding.GetRelation())
					{
						if (this.m_binding.RelationEnd == RelationEnd.Source || this.m_binding.RelationEnd == this.m_relatedRole.Binding.RelationEnd)
						{
							ctx.AddScopedError(ModelingErrorCode.RoleRelationsMismatch, SRErrors.RoleRelationsMismatch(ctx.CurrentObjectDescriptor, SRObjectDescriptor.FromScope(this.m_relatedRole).ObjectName, this.m_binding.GetRelationDescriptor(), this.m_relatedRole.Binding.GetRelationDescriptor()));
							return;
						}
					}
					else
					{
						if (this.m_relatedRole.Binding != null && this.m_binding.RelationEnd == this.m_relatedRole.Binding.RelationEnd)
						{
							ctx.AddScopedError(ModelingErrorCode.RoleRelationEndsMismatch, SRErrors.RoleRelationEndsMismatch(ctx.CurrentObjectDescriptor, SRObjectDescriptor.FromScope(this.m_relatedRole).ObjectName, this.m_binding.RelationEnd, this.m_binding.GetRelationDescriptor()));
							return;
						}
						if (dsvTable != null && this.m_binding.GetTable() != dsvTable)
						{
							ctx.AddScopedError(ModelingErrorCode.InvalidRoleRelationTable, SRErrors.InvalidRoleRelationTable(ctx.CurrentObjectDescriptor, this.m_binding.RelationEnd, this.m_binding.GetRelationDescriptor(), Microsoft.ReportingServices.Modeling.Binding.GetTableDescriptor(dsvTable.Name)));
							return;
						}
						if (Microsoft.ReportingServices.Modeling.Binding.CheckColumns(this.m_binding.GetColumns(), binding, ctx, "Relation") && this.m_cardinality == Cardinality.One && !Microsoft.ReportingServices.Modeling.Binding.AreColumnsUnique(this.m_binding.GetColumns(), binding))
						{
							ctx.AddScopedWarning(ModelingErrorCode.NonUniqueRoleRelationColumns, SRErrors.NonUniqueRoleRelationColumns(ctx.CurrentObjectDescriptor, this.m_binding.RelationEnd, this.m_binding.GetRelationDescriptor(), Microsoft.ReportingServices.Modeling.Binding.GetTableDescriptor(dsvTable.Name)));
						}
					}
				}
				return;
			}
			if (this.m_relatedRole.Binding == null && base.Entity.Binding != null && base.Entity.Binding.GetTable() == dsvTable && ((base.Entity.Binding is ColumnBinding && this.RelatedEntity.Binding is TableBinding) || (base.Entity.Binding is TableBinding && this.RelatedEntity.Binding is ColumnBinding)))
			{
				forColumnBoundEntity = true;
				return;
			}
			ctx.AddScopedError(ModelingErrorCode.MissingBinding, SRErrors.MissingBinding_Role(ctx.CurrentObjectDescriptor));
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x00016413 File Offset: 0x00014613
		internal override ModelItem CreateLazyClone(bool markAsHidden)
		{
			return new ModelRole(this, markAsHidden);
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x0001641C File Offset: 0x0001461C
		internal override bool ResolveRequiredReferences(SemanticModel newModel)
		{
			if (!base.ResolveRequiredReferences(newModel))
			{
				return false;
			}
			if (this.m_relatedRole != null)
			{
				throw new InternalModelingException("Related role already set on ModelRole.");
			}
			if (this.BaseItem.RelatedRole != null)
			{
				this.m_relatedRole = newModel.LookupItemByID(this.BaseItem.RelatedRole.ID) as ModelRole;
			}
			return this.m_relatedRole != null;
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x00016480 File Offset: 0x00014680
		public static void ConvertFromCombinedCardinality(CombinedCardinality combinedCardinality, out Cardinality cardinality, out Optionality optionality)
		{
			switch (combinedCardinality)
			{
			case CombinedCardinality.One:
				cardinality = Cardinality.One;
				optionality = Optionality.Required;
				return;
			case CombinedCardinality.Many:
				cardinality = Cardinality.Many;
				optionality = Optionality.Required;
				return;
			case CombinedCardinality.OptionalOne:
				cardinality = Cardinality.One;
				optionality = Optionality.Optional;
				return;
			case CombinedCardinality.OptionalMany:
				cardinality = Cardinality.Many;
				optionality = Optionality.Optional;
				return;
			default:
				throw new InternalModelingException("Unknown CombinedCardinality value: " + combinedCardinality.ToString());
			}
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x000164DD File Offset: 0x000146DD
		public static CombinedCardinality ConvertToCombinedCardinality(Cardinality cardinality, Optionality optionality)
		{
			if (cardinality == Cardinality.One)
			{
				if (optionality != Optionality.Optional)
				{
					return CombinedCardinality.One;
				}
				return CombinedCardinality.OptionalOne;
			}
			else
			{
				if (cardinality != Cardinality.Many)
				{
					throw new InternalModelingException("Unknown Cardinality value: " + cardinality.ToString());
				}
				if (optionality != Optionality.Optional)
				{
					return CombinedCardinality.Many;
				}
				return CombinedCardinality.OptionalMany;
			}
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x00016510 File Offset: 0x00014710
		internal override void Serialize(IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(ModelRole.Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.Cardinality)
				{
					switch (memberName)
					{
					case MemberName.SingularName:
						writer.Write(this.m_singularName);
						break;
					case MemberName.PluralName:
						writer.Write(this.m_pluralName);
						break;
					case MemberName.RelatedRole:
						PersistenceHelper.WriteModelingObjectReference<ModelRole>(ref writer, this.m_relatedRole);
						break;
					case MemberName.Optionality:
						writer.Write((byte)this.m_optionality);
						break;
					case MemberName.ContextualName:
						writer.Write((byte)this.m_contextualName);
						break;
					case MemberName.Preferred:
						writer.Write(this.m_preferred);
						break;
					case MemberName.PromoteLookup:
						writer.Write(this.m_promoteLookup);
						break;
					case MemberName.ExpandInline:
						writer.Write(this.m_expandInline);
						break;
					case MemberName.HiddenFields:
						((IPersistable)this.HiddenFields).Serialize(writer);
						break;
					case MemberName.Binding:
						writer.Write(this.Binding);
						break;
					default:
						throw new InternalModelingException("Unexpected member: " + writer.CurrentMember.MemberName.ToString());
					}
				}
				else
				{
					writer.Write((byte)this.m_cardinality);
				}
			}
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x00016668 File Offset: 0x00014868
		internal override void Deserialize(IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			using (base.AllowWriteOperations())
			{
				reader.RegisterDeclaration(ModelRole.Declaration);
				while (reader.NextMember())
				{
					MemberName memberName = reader.CurrentMember.MemberName;
					if (memberName != MemberName.Cardinality)
					{
						switch (memberName)
						{
						case MemberName.SingularName:
							this.m_singularName = reader.ReadString();
							break;
						case MemberName.PluralName:
							this.m_pluralName = reader.ReadString();
							break;
						case MemberName.RelatedRole:
							this.m_relatedRole = PersistenceHelper.ReadModelingObjectReference<ModelRole>(ref reader, this);
							break;
						case MemberName.Optionality:
							this.m_optionality = (Optionality)reader.ReadByte();
							break;
						case MemberName.ContextualName:
							this.m_contextualName = (RoleContextualName)reader.ReadByte();
							break;
						case MemberName.Preferred:
							this.m_preferred = reader.ReadBoolean();
							break;
						case MemberName.PromoteLookup:
							this.m_promoteLookup = reader.ReadBoolean();
							break;
						case MemberName.ExpandInline:
							this.m_expandInline = reader.ReadBoolean();
							break;
						case MemberName.HiddenFields:
							((IPersistable)this.HiddenFields).Deserialize(reader);
							break;
						case MemberName.Binding:
							this.Binding = (RelationBinding)reader.ReadRIFObject();
							break;
						default:
							throw new InternalModelingException("Unexpected member: " + reader.CurrentMember.MemberName.ToString());
						}
					}
					else
					{
						this.m_cardinality = (Cardinality)reader.ReadByte();
					}
				}
			}
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x000167F4 File Offset: 0x000149F4
		internal override void ResolveReferences(Dictionary<ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems)
		{
			List<MemberReference> list;
			if (memberReferencesCollection.TryGetValue(ModelRole.Declaration.ObjectType, out list))
			{
				foreach (MemberReference memberReference in list)
				{
					if (memberReference.MemberName != MemberName.RelatedRole)
					{
						throw new InternalModelingException("Unexpected member: " + memberReference.MemberName.ToString());
					}
					this.m_relatedRole = PersistenceHelper.ResolveModelingObjectReference<ModelRole>(referenceableItems[memberReference.RefID]);
				}
			}
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x00016898 File Offset: 0x00014A98
		internal override ObjectType GetObjectType()
		{
			return ObjectType.ModelRole;
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000701 RID: 1793 RVA: 0x0001689C File Offset: 0x00014A9C
		private static Declaration Declaration
		{
			get
			{
				return ThreadingUtil.ReturnOnDemandValue<Declaration>(ref ModelRole.__declaration, ModelRole.__declarationLock, () => new Declaration(ObjectType.ModelRole, ObjectType.ModelField, new List<MemberInfo>
				{
					new MemberInfo(MemberName.SingularName, Token.String),
					new MemberInfo(MemberName.PluralName, Token.String),
					new MemberInfo(MemberName.RelatedRole, ObjectType.ModelRole, Token.Reference),
					new MemberInfo(MemberName.Cardinality, Token.Byte),
					new MemberInfo(MemberName.Optionality, Token.Byte),
					new MemberInfo(MemberName.ContextualName, Token.Byte),
					new MemberInfo(MemberName.Preferred, Token.Boolean),
					new MemberInfo(MemberName.PromoteLookup, Token.Boolean),
					new MemberInfo(MemberName.ExpandInline, Token.Boolean),
					new MemberInfo(MemberName.HiddenFields, ObjectType.HiddenFieldCollection),
					new MemberInfo(MemberName.Binding, ObjectType.RelationBinding)
				}));
			}
		}

		// Token: 0x04000344 RID: 836
		internal const string RoleElem = "Role";

		// Token: 0x04000345 RID: 837
		private const string LinguisticsElem = "Linguistics";

		// Token: 0x04000346 RID: 838
		private const string SingularNameElem = "SingularName";

		// Token: 0x04000347 RID: 839
		private const string PluralNameElem = "PluralName";

		// Token: 0x04000348 RID: 840
		private const string RelatedRoleIdElem = "RelatedRoleID";

		// Token: 0x04000349 RID: 841
		private const string CardinalityElem = "Cardinality";

		// Token: 0x0400034A RID: 842
		private const string ContextualNameElem = "ContextualName";

		// Token: 0x0400034B RID: 843
		private const string PreferredElem = "Preferred";

		// Token: 0x0400034C RID: 844
		private const string PromoteLookupElem = "PromoteLookup";

		// Token: 0x0400034D RID: 845
		private const string ExpandInlineElem = "ExpandInline";

		// Token: 0x0400034E RID: 846
		private const string HiddenFieldsElem = "HiddenFields";

		// Token: 0x0400034F RID: 847
		private const string FieldFolderItemIdElem = "FieldFolderItemID";

		// Token: 0x04000350 RID: 848
		private const string FieldFolderItemIdProperty = "HiddenFields.FieldFolderItemID";

		// Token: 0x04000351 RID: 849
		private string m_singularName;

		// Token: 0x04000352 RID: 850
		private string m_pluralName;

		// Token: 0x04000353 RID: 851
		private ModelRole m_relatedRole;

		// Token: 0x04000354 RID: 852
		private Cardinality m_cardinality;

		// Token: 0x04000355 RID: 853
		private Optionality m_optionality;

		// Token: 0x04000356 RID: 854
		private RoleContextualName m_contextualName;

		// Token: 0x04000357 RID: 855
		private bool m_preferred;

		// Token: 0x04000358 RID: 856
		private bool m_promoteLookup;

		// Token: 0x04000359 RID: 857
		private bool m_expandInline;

		// Token: 0x0400035A RID: 858
		private ModelRole.HiddenFieldCollection __hiddenFields;

		// Token: 0x0400035B RID: 859
		private RelationBinding m_binding;

		// Token: 0x0400035C RID: 860
		private static Declaration __declaration;

		// Token: 0x0400035D RID: 861
		private static readonly object __declarationLock = new object();

		// Token: 0x02000177 RID: 375
		private class LinguisticsLoader : ModelingXmlLoaderBase<ModelRole>
		{
			// Token: 0x06000FC2 RID: 4034 RVA: 0x000317B1 File Offset: 0x0002F9B1
			internal LinguisticsLoader(ModelRole item)
				: base(item)
			{
			}

			// Token: 0x06000FC3 RID: 4035 RVA: 0x000317BC File Offset: 0x0002F9BC
			public override bool LoadXmlElement(ModelingXmlReader xr)
			{
				if (xr.IsDefaultNamespace)
				{
					string localName = xr.LocalName;
					if (localName == "SingularName")
					{
						base.Item.Linguistics.SingularName = xr.ReadValueAsString();
						return true;
					}
					if (localName == "PluralName")
					{
						base.Item.Linguistics.PluralName = xr.ReadValueAsString();
						return true;
					}
				}
				return base.LoadXmlElement(xr);
			}
		}

		// Token: 0x02000178 RID: 376
		public sealed class HiddenFieldCollection : ModelItemCollection<ModelFieldFolderItem, ModelRole>, IPersistable
		{
			// Token: 0x06000FC4 RID: 4036 RVA: 0x0003182B File Offset: 0x0002FA2B
			internal HiddenFieldCollection(ModelRole parentItem)
				: base(parentItem)
			{
			}

			// Token: 0x06000FC5 RID: 4037 RVA: 0x00031834 File Offset: 0x0002FA34
			internal ModelRole.HiddenFieldCollection CloneFor(ModelRole newParentItem)
			{
				ModelRole.HiddenFieldCollection hiddenFieldCollection = new ModelRole.HiddenFieldCollection(newParentItem);
				hiddenFieldCollection.CopyFromBase(this);
				return hiddenFieldCollection;
			}

			// Token: 0x06000FC6 RID: 4038 RVA: 0x00031843 File Offset: 0x0002FA43
			void IPersistable.Serialize(IntermediateFormatWriter writer)
			{
				this.Serialize(writer);
			}

			// Token: 0x06000FC7 RID: 4039 RVA: 0x0003184C File Offset: 0x0002FA4C
			internal override void Serialize(IntermediateFormatWriter writer)
			{
				base.Serialize(writer);
				writer.RegisterDeclaration(ModelRole.HiddenFieldCollection.Declaration);
				while (writer.NextMember())
				{
					if (writer.CurrentMember.MemberName != MemberName.Items)
					{
						throw new InternalModelingException("Unexpected member: " + writer.CurrentMember.MemberName.ToString());
					}
					PersistenceHelper.WriteModelingObjectReferences<ModelFieldFolderItem>(ref writer, this);
				}
			}

			// Token: 0x06000FC8 RID: 4040 RVA: 0x000318BB File Offset: 0x0002FABB
			void IPersistable.Deserialize(IntermediateFormatReader reader)
			{
				this.Deserialize(reader);
			}

			// Token: 0x06000FC9 RID: 4041 RVA: 0x000318C4 File Offset: 0x0002FAC4
			internal override void Deserialize(IntermediateFormatReader reader)
			{
				base.Deserialize(reader);
				reader.RegisterDeclaration(ModelRole.HiddenFieldCollection.Declaration);
				while (reader.NextMember())
				{
					if (reader.CurrentMember.MemberName != MemberName.Items)
					{
						throw new InternalModelingException("Unexpected member: " + reader.CurrentMember.MemberName.ToString());
					}
					reader.ReadListOfReferencesNoResolution(this);
				}
			}

			// Token: 0x06000FCA RID: 4042 RVA: 0x00031934 File Offset: 0x0002FB34
			void IPersistable.ResolveReferences(Dictionary<ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems)
			{
				using (this.AllowWriteOperations())
				{
					List<MemberReference> list;
					if (memberReferencesCollection.TryGetValue(ModelRole.HiddenFieldCollection.Declaration.ObjectType, out list))
					{
						foreach (MemberReference memberReference in list)
						{
							if (memberReference.MemberName != MemberName.Items)
							{
								throw new InternalModelingException("Unexpected member: " + memberReference.MemberName.ToString());
							}
							base.Add(PersistenceHelper.ResolveModelingObjectReference<ModelFieldFolderItem>(referenceableItems[memberReference.RefID]));
						}
					}
				}
			}

			// Token: 0x06000FCB RID: 4043 RVA: 0x000319F8 File Offset: 0x0002FBF8
			ObjectType IPersistable.GetObjectType()
			{
				return ObjectType.HiddenFieldCollection;
			}

			// Token: 0x170003DD RID: 989
			// (get) Token: 0x06000FCC RID: 4044 RVA: 0x000319FC File Offset: 0x0002FBFC
			private static Declaration Declaration
			{
				get
				{
					return ThreadingUtil.ReturnOnDemandValue<Declaration>(ref ModelRole.HiddenFieldCollection.__declaration, ModelRole.HiddenFieldCollection.__declarationLock, () => new Declaration(ObjectType.HiddenFieldCollection, ObjectType.ModelItemCollection, new List<MemberInfo>
					{
						new MemberInfo(MemberName.Items, ObjectType.RIFObjectList, Token.Reference, ObjectType.ModelFieldFolderItem)
					}));
				}
			}

			// Token: 0x04000687 RID: 1671
			private static Declaration __declaration;

			// Token: 0x04000688 RID: 1672
			private static readonly object __declarationLock = new object();
		}
	}
}
