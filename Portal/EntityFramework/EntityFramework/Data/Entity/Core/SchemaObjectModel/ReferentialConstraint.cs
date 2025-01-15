using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x02000306 RID: 774
	internal sealed class ReferentialConstraint : SchemaElement
	{
		// Token: 0x0600249F RID: 9375 RVA: 0x00067A13 File Offset: 0x00065C13
		public ReferentialConstraint(Relationship relationship)
			: base(relationship, null)
		{
		}

		// Token: 0x060024A0 RID: 9376 RVA: 0x00067A20 File Offset: 0x00065C20
		internal override void Validate()
		{
			base.Validate();
			this._principalRole.Validate();
			this._dependentRole.Validate();
			if (ReferentialConstraint.ReadyForFurtherValidation(this._principalRole) && ReferentialConstraint.ReadyForFurtherValidation(this._dependentRole))
			{
				IRelationshipEnd end = this._principalRole.End;
				IRelationshipEnd end2 = this._dependentRole.End;
				if (this._principalRole.Name == this._dependentRole.Name)
				{
					base.AddError(ErrorCode.SameRoleReferredInReferentialConstraint, EdmSchemaErrorSeverity.Error, Strings.SameRoleReferredInReferentialConstraint(this.ParentElement.Name));
				}
				bool flag;
				bool flag2;
				bool flag3;
				bool flag4;
				ReferentialConstraint.IsKeyProperty(this._dependentRole, end2.Type, out flag, out flag2, out flag3, out flag4);
				bool flag5;
				bool flag6;
				bool flag7;
				bool flag8;
				ReferentialConstraint.IsKeyProperty(this._principalRole, end.Type, out flag5, out flag6, out flag7, out flag8);
				if (!flag5)
				{
					base.AddError(ErrorCode.InvalidPropertyInRelationshipConstraint, EdmSchemaErrorSeverity.Error, Strings.InvalidFromPropertyInRelationshipConstraint(this.PrincipalRole.Name, end.Type.FQName, this.ParentElement.FQName));
					return;
				}
				bool flag9 = base.Schema.SchemaVersion <= 1.1;
				RelationshipMultiplicity relationshipMultiplicity = ((flag9 ? flag6 : flag7) ? RelationshipMultiplicity.ZeroOrOne : RelationshipMultiplicity.One);
				RelationshipMultiplicity relationshipMultiplicity2 = ((flag9 ? flag2 : flag3) ? RelationshipMultiplicity.ZeroOrOne : RelationshipMultiplicity.Many);
				end.Multiplicity = new RelationshipMultiplicity?(end.Multiplicity ?? relationshipMultiplicity);
				end2.Multiplicity = new RelationshipMultiplicity?(end2.Multiplicity ?? relationshipMultiplicity2);
				RelationshipMultiplicity? relationshipMultiplicity3 = end.Multiplicity;
				RelationshipMultiplicity relationshipMultiplicity4 = RelationshipMultiplicity.Many;
				if ((relationshipMultiplicity3.GetValueOrDefault() == relationshipMultiplicity4) & (relationshipMultiplicity3 != null))
				{
					base.AddError(ErrorCode.InvalidMultiplicityInRoleInRelationshipConstraint, EdmSchemaErrorSeverity.Error, Strings.InvalidMultiplicityFromRoleUpperBoundMustBeOne(this._principalRole.Name, this.ParentElement.Name));
				}
				else
				{
					if (flag2)
					{
						relationshipMultiplicity3 = end.Multiplicity;
						relationshipMultiplicity4 = RelationshipMultiplicity.One;
						if ((relationshipMultiplicity3.GetValueOrDefault() == relationshipMultiplicity4) & (relationshipMultiplicity3 != null))
						{
							string text = Strings.InvalidMultiplicityFromRoleToPropertyNullableV1(this._principalRole.Name, this.ParentElement.Name);
							base.AddError(ErrorCode.InvalidMultiplicityInRoleInRelationshipConstraint, EdmSchemaErrorSeverity.Error, text);
							goto IL_028E;
						}
					}
					if ((flag9 && !flag2) || (!flag9 && !flag3))
					{
						relationshipMultiplicity3 = end.Multiplicity;
						relationshipMultiplicity4 = RelationshipMultiplicity.One;
						if (!((relationshipMultiplicity3.GetValueOrDefault() == relationshipMultiplicity4) & (relationshipMultiplicity3 != null)))
						{
							string text2;
							if (flag9)
							{
								text2 = Strings.InvalidMultiplicityFromRoleToPropertyNonNullableV1(this._principalRole.Name, this.ParentElement.Name);
							}
							else
							{
								text2 = Strings.InvalidMultiplicityFromRoleToPropertyNonNullableV2(this._principalRole.Name, this.ParentElement.Name);
							}
							base.AddError(ErrorCode.InvalidMultiplicityInRoleInRelationshipConstraint, EdmSchemaErrorSeverity.Error, text2);
						}
					}
				}
				IL_028E:
				relationshipMultiplicity3 = end2.Multiplicity;
				relationshipMultiplicity4 = RelationshipMultiplicity.One;
				if (((relationshipMultiplicity3.GetValueOrDefault() == relationshipMultiplicity4) & (relationshipMultiplicity3 != null)) && base.Schema.DataModel == SchemaDataModelOption.ProviderDataModel)
				{
					base.AddError(ErrorCode.InvalidMultiplicityInRoleInRelationshipConstraint, EdmSchemaErrorSeverity.Error, Strings.InvalidMultiplicityToRoleLowerBoundMustBeZero(this._dependentRole.Name, this.ParentElement.Name));
				}
				if (!flag4 && !this.ParentElement.IsForeignKey && base.Schema.DataModel == SchemaDataModelOption.EntityDataModel)
				{
					base.AddError(ErrorCode.InvalidPropertyInRelationshipConstraint, EdmSchemaErrorSeverity.Error, Strings.InvalidToPropertyInRelationshipConstraint(this.DependentRole.Name, end2.Type.FQName, this.ParentElement.FQName));
				}
				if (flag)
				{
					relationshipMultiplicity3 = end2.Multiplicity;
					relationshipMultiplicity4 = RelationshipMultiplicity.Many;
					if ((relationshipMultiplicity3.GetValueOrDefault() == relationshipMultiplicity4) & (relationshipMultiplicity3 != null))
					{
						base.AddError(ErrorCode.InvalidMultiplicityInRoleInRelationshipConstraint, EdmSchemaErrorSeverity.Error, Strings.InvalidMultiplicityToRoleUpperBoundMustBeOne(end2.Name, this.ParentElement.Name));
					}
				}
				else
				{
					relationshipMultiplicity3 = end2.Multiplicity;
					relationshipMultiplicity4 = RelationshipMultiplicity.Many;
					if (!((relationshipMultiplicity3.GetValueOrDefault() == relationshipMultiplicity4) & (relationshipMultiplicity3 != null)))
					{
						base.AddError(ErrorCode.InvalidMultiplicityInRoleInRelationshipConstraint, EdmSchemaErrorSeverity.Error, Strings.InvalidMultiplicityToRoleUpperBoundMustBeMany(end2.Name, this.ParentElement.Name));
					}
				}
				if (this._dependentRole.RoleProperties.Count != this._principalRole.RoleProperties.Count)
				{
					base.AddError(ErrorCode.MismatchNumberOfPropertiesInRelationshipConstraint, EdmSchemaErrorSeverity.Error, Strings.MismatchNumberOfPropertiesinRelationshipConstraint);
					return;
				}
				for (int i = 0; i < this._dependentRole.RoleProperties.Count; i++)
				{
					if (this._dependentRole.RoleProperties[i].Property.Type != this._principalRole.RoleProperties[i].Property.Type)
					{
						base.AddError(ErrorCode.TypeMismatchRelationshipConstraint, EdmSchemaErrorSeverity.Error, Strings.TypeMismatchRelationshipConstraint(this._dependentRole.RoleProperties[i].Name, this._dependentRole.End.Type.Identity, this._principalRole.RoleProperties[i].Name, this._principalRole.End.Type.Identity, this.ParentElement.Name));
					}
				}
			}
		}

		// Token: 0x060024A1 RID: 9377 RVA: 0x00067EE0 File Offset: 0x000660E0
		private static bool ReadyForFurtherValidation(ReferentialConstraintRoleElement role)
		{
			if (role == null)
			{
				return false;
			}
			if (role.End == null)
			{
				return false;
			}
			if (role.RoleProperties.Count == 0)
			{
				return false;
			}
			using (IEnumerator<PropertyRefElement> enumerator = role.RoleProperties.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Property == null)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060024A2 RID: 9378 RVA: 0x00067F54 File Offset: 0x00066154
		private static void IsKeyProperty(ReferentialConstraintRoleElement roleElement, SchemaEntityType itemType, out bool isKeyProperty, out bool areAllPropertiesNullable, out bool isAnyPropertyNullable, out bool isSubsetOfKeyProperties)
		{
			isKeyProperty = true;
			areAllPropertiesNullable = true;
			isAnyPropertyNullable = false;
			isSubsetOfKeyProperties = true;
			if (itemType.KeyProperties.Count != roleElement.RoleProperties.Count)
			{
				isKeyProperty = false;
			}
			for (int i = 0; i < roleElement.RoleProperties.Count; i++)
			{
				if (isSubsetOfKeyProperties)
				{
					bool flag = false;
					for (int j = 0; j < itemType.KeyProperties.Count; j++)
					{
						if (itemType.KeyProperties[j].Property == roleElement.RoleProperties[i].Property)
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						isKeyProperty = false;
						isSubsetOfKeyProperties = false;
					}
				}
				areAllPropertiesNullable &= roleElement.RoleProperties[i].Property.Nullable;
				isAnyPropertyNullable |= roleElement.RoleProperties[i].Property.Nullable;
			}
		}

		// Token: 0x060024A3 RID: 9379 RVA: 0x0006802D File Offset: 0x0006622D
		protected override bool HandleAttribute(XmlReader reader)
		{
			return false;
		}

		// Token: 0x060024A4 RID: 9380 RVA: 0x00068030 File Offset: 0x00066230
		protected override bool HandleElement(XmlReader reader)
		{
			if (base.HandleElement(reader))
			{
				return true;
			}
			if (base.CanHandleElement(reader, "Principal"))
			{
				this.HandleReferentialConstraintPrincipalRoleElement(reader);
				return true;
			}
			if (base.CanHandleElement(reader, "Dependent"))
			{
				this.HandleReferentialConstraintDependentRoleElement(reader);
				return true;
			}
			return false;
		}

		// Token: 0x060024A5 RID: 9381 RVA: 0x0006806C File Offset: 0x0006626C
		internal void HandleReferentialConstraintPrincipalRoleElement(XmlReader reader)
		{
			this._principalRole = new ReferentialConstraintRoleElement(this);
			this._principalRole.Parse(reader);
		}

		// Token: 0x060024A6 RID: 9382 RVA: 0x00068086 File Offset: 0x00066286
		internal void HandleReferentialConstraintDependentRoleElement(XmlReader reader)
		{
			this._dependentRole = new ReferentialConstraintRoleElement(this);
			this._dependentRole.Parse(reader);
		}

		// Token: 0x060024A7 RID: 9383 RVA: 0x000680A0 File Offset: 0x000662A0
		internal override void ResolveTopLevelNames()
		{
			this._dependentRole.ResolveTopLevelNames();
			this._principalRole.ResolveTopLevelNames();
		}

		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x060024A8 RID: 9384 RVA: 0x000680B8 File Offset: 0x000662B8
		internal new IRelationship ParentElement
		{
			get
			{
				return (IRelationship)base.ParentElement;
			}
		}

		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x060024A9 RID: 9385 RVA: 0x000680C5 File Offset: 0x000662C5
		internal ReferentialConstraintRoleElement PrincipalRole
		{
			get
			{
				return this._principalRole;
			}
		}

		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x060024AA RID: 9386 RVA: 0x000680CD File Offset: 0x000662CD
		internal ReferentialConstraintRoleElement DependentRole
		{
			get
			{
				return this._dependentRole;
			}
		}

		// Token: 0x04000CFE RID: 3326
		private const char KEY_DELIMITER = ' ';

		// Token: 0x04000CFF RID: 3327
		private ReferentialConstraintRoleElement _principalRole;

		// Token: 0x04000D00 RID: 3328
		private ReferentialConstraintRoleElement _dependentRole;
	}
}
